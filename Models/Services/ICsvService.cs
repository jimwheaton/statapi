using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public interface ICsvService
    {
        Task<int> Import(TextReader textReader);
    }
}
