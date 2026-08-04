using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace CourtApp.Infrastructure.Services
{
    public static class CsvReaderService
    {
        public static List<T> Read<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);

            using var csv = new CsvReader(reader,
                new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim
                });

            return csv.GetRecords<T>().ToList();
        }
    }
}
