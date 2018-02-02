using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToCSVMerge
{
    public static class CsvCreator
    {
        public const string delimiter = ",";
        public const string fileName = "/output.csv";

        private static StreamWriter csvString;
        private static CsvWriter csv;
        private static string path;


        public static void SetPath(string _path)
        {
            path = _path;
        }

        public static void WriteRecordsToCsv(DataTable table)
        {
            if (csvString == null)
            {
                csvString = new StreamWriter(path + fileName);
                csv = new CsvWriter(csvString);
                csv.Configuration.Delimiter = delimiter;
                foreach (DataColumn column in table.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();
            }

            using (table)
            {
                foreach (DataRow row in table.Rows)
                {
                    for (var i = 0; i < table.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
        }

        public static void Close()
        {
            csvString.Close();
            csvString.Dispose();
        }

    }
}
