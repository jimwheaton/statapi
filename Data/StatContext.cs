using Microsoft.EntityFrameworkCore;
using System;
using Models;

namespace Data
{
    public class StatContext : DbContext
    {
        public StatContext(DbContextOptions<StatContext> options) : base(options)
        {
        }

        public DbSet<DataStaging> DataStaging { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Keyword> Keywords  { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<KeywordRank> KeywordRanks { get; set; }
        
    }
}
