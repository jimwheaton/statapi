using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Services;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Data.SqlClient;
using FastMember;
using CsvHelper.Configuration;
using Data;
using System.Threading.Tasks;

namespace Services
{
    public class CsvService : ICsvService
    {
        private readonly string _connectionString;
        private readonly string _stagingTableName;
        private CsvConfiguration _csvConfig;
        private string[] _stagingTableCols;
        private StatContext _statContext;

        public CsvService(string connectionString, 
                          string stagingTableName,
                          string[] stagingTableCols,
                          CsvConfiguration csvConfig,
                          StatContext statContext)
        {
            _connectionString = connectionString;
            _stagingTableName = stagingTableName;
            _stagingTableCols = stagingTableCols;
            _csvConfig = csvConfig;
            _statContext = statContext;
        }

        public async Task<int> Import(TextReader textReader)
        {
            var csvReader = new CsvReader(textReader, _csvConfig);
            var records = csvReader.GetRecords<DataStaging>().ToList();

            await _statContext.Database.ExecuteSqlCommandAsync("Clean");

            using (var bulkCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.TableLock))
            using (var reader = ObjectReader.Create(records, _stagingTableCols))
            {
                for(var i=0; i<_stagingTableCols.Length; i++)
                {
                    bulkCopy.ColumnMappings.Add(i, i + 1);
                }
                bulkCopy.DestinationTableName = _stagingTableName;
                await bulkCopy.WriteToServerAsync(reader);
            }

            return await _statContext.Database.ExecuteSqlCommandAsync("LoadModelFromStaging");
        }
    }
}
