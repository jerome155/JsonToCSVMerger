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

        private static StreamWriter streamWriter;
        private static CsvWriter csv;
        private static string path;

        private static DataColumnCollection previousDataColumns;


        public static void SetPath(string _path)
        {
            path = _path;
        }


        public static void WriteRecordsToCsv(DataTable table)
        {
            DataTable outTable = new DataTable();

            //case: the format has changed.
            Boolean isSame = true;
            if (previousDataColumns != null)
            {
                foreach (DataColumn dataCol in table.Columns)
                {
                    if (!previousDataColumns.Contains(dataCol.ColumnName))
                    {
                        isSame = false;
                        break;
                    }
                }
            }
            
            if (!isSame & streamWriter != null)
            {
                Console.WriteLine("Difference in Header detected. Rewriting csv file.");
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = null;
                StreamReader streamReader = new StreamReader(path + fileName);
                CsvReader csv = new CsvReader(streamReader);

                //read header contents
                csv.Read();
                csv.ReadHeader();
                string[] headerRow = csv.Context.HeaderRecord;

                foreach (string headerField in headerRow)
                {
                    outTable.Columns.Add(headerField);
                }

                //read contents
                while (csv.Read())
                {
                    var row = outTable.NewRow();
                    foreach (DataColumn column in outTable.Columns)
                    {
                        row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
                    }
                    outTable.Rows.Add(row);
                }

                previousDataColumns = outTable.Columns;
                streamReader.Close();
                streamReader.Dispose();

                //merge table & outtable
                //add missing columns
                foreach (DataColumn column in table.Columns)
                {
                    if (!outTable.Columns.Contains(column.ColumnName))
                    {
                        outTable.Columns.Add(column.ColumnName);
                    }
                }
                //merge existing records and new ones
                foreach (DataRow row in table.Rows)
                {
                    DataRow newRow = outTable.NewRow();
                    foreach (DataColumn dataCol in row.Table.Columns)
                    {
                        newRow[dataCol.ColumnName] = row[dataCol.ColumnName];
                    }
                    
                    outTable.Rows.Add(newRow);
                }
            } else
            {
                previousDataColumns = table.Columns;
                outTable = table;
            }
           

            //create a new streamwriter, write header
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(path + fileName, false);
                csv = new CsvWriter(streamWriter);
                csv.Configuration.Delimiter = delimiter;
                foreach (DataColumn column in outTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();
            }

            //write new rows
            using (outTable)
            {
                foreach (DataRow row in outTable.Rows)
                {
                    for (var i = 0; i < outTable.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
        }

        public static void Close()
        {
            streamWriter.Close();
            streamWriter.Dispose();
        }

    }
}
