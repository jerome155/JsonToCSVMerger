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

        public static void WriteCsv(DataTable table, string path)
        {
            StreamWriter csvString = new StreamWriter(path + fileName);
            using (var csv = new CsvWriter(csvString))
            {
                csv.Configuration.Delimiter = delimiter;

                using (table)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

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
            csvString.Close();
        }
    }
}
