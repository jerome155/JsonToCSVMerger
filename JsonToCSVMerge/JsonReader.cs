using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToCSVMerge
{
    public static class JsonReader
    {
        private static bool isHeaderCreated;
        private static List<String> nestedHeaderNames = null;

        public static DataTable Run(string[] files)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < files.Length; i++)
            {
#if debug
                if (i > 2)
                {
                    continue;
                    Console.WriteLine("Running mode: Debug.");
                }
#endif
                Console.WriteLine("Evaluating document " + i + ": " + files[i]);
                table = TransformJsonFileToDataTable(files[i], table);
            }
            return table;
        }

        private static DataTable TransformJsonFileToDataTable(string file, DataTable table)
        {
            string jsonContent = "";
            using (StreamReader r = new StreamReader(file))
            {
                List<string> jsonObjects = SplitConcatenatedJson(r, jsonContent);

                for (int j = 0; j < jsonObjects.Count; j++)
                {
                    dynamic jsonInput = null;
                    try
                    {
                        jsonInput = JsonConvert.DeserializeObject(jsonObjects[j]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Encountered a reading error: Entry " + j);
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    //Create header if not yet existing.
                    if (!isHeaderCreated)
                    {
                        table = CreateDataTableHeader(jsonInput, table);
                    }

                    //Add the new row
                    table = AddRowToTable(jsonInput, table);
                }
            }
            return table;

        }

        private static List<string> SplitConcatenatedJson(StreamReader r, string jsonContent)
        {
            jsonContent = r.ReadToEnd();
            List<string> jsonObjects = new List<string>();

            string[] splitContent = jsonContent.Split(new string[] { "}\n{" }, StringSplitOptions.None);
            jsonObjects = splitContent.ToList();

            for (int i = 0; i < jsonObjects.Count; i++)
            {
                if (jsonObjects.Count <= 1)
                {
                    //do nothing
                }
                else if (i == 0)
                {
                    jsonObjects[i] = jsonObjects[i] + "}";
                }
                else if (i == jsonObjects.Count - 1)
                {
                    jsonObjects[i] = "{" + jsonObjects[i];
                }
                else
                {
                    jsonObjects[i] = "{" + jsonObjects[i] + "}";
                }
            }
            return jsonObjects;
        }

        private static DataTable CreateDataTableHeader(dynamic jsonInput, DataTable mainTable)
        {
            Console.WriteLine("Building Table Header.");
            foreach (dynamic token in jsonInput)
            {
                if (Convert.ToString(token.Value).Contains("{"))
                {
                    nestedHeaderNames = DeserializeNestedObjectHeader(Convert.ToString(token.Name), Convert.ToString(token.Value));
                    foreach (string element in nestedHeaderNames)
                    {
                        if (!mainTable.Columns.Contains(element))
                        {
                            mainTable.Columns.Add(element);
                            isHeaderCreated = true;
                        }
                    }
                }
                else
                {
                    if (!mainTable.Columns.Contains(token.Name))
                    {
                        mainTable.Columns.Add(token.Name);
                        isHeaderCreated = true;
                    }
                }

            }
            return mainTable;
        }

        private static DataTable AddRowToTable(dynamic jsonInput, DataTable mainTable)
        {
            DataRow newRow = mainTable.NewRow();
            foreach (dynamic token in jsonInput)
            {
                if (Convert.ToString(token.Value).Contains("{"))
                {
                    List<string> nestedValues = DeserializeNestedObjectValues(nestedHeaderNames, Convert.ToString(token.Value));
                    for (int i = 0; i < nestedHeaderNames.Count(); i++)
                    {
                        newRow[nestedHeaderNames[i]] = nestedValues[i];
                    }
                }
                else
                {
                    string tempValue = token.Value;
                    tempValue.Replace("\"", "");
                    newRow[token.Name] = tempValue;
                }
            }
            mainTable.Rows.Add(newRow);
            return mainTable;
        }



        public static List<String> DeserializeNestedObjectHeader(string name, string value)
        {
            List<String> nestedHeaders = new List<String>();
            while (value.IndexOf("\r") >= 0 || value.IndexOf("}") >= 0)
            {
                if (value.IndexOf("\r") >= 0)
                {
                    if (value.Substring(0, value.IndexOf("\r")).Length > 2)
                    {
                        nestedHeaders.Add(value.Substring(0, value.IndexOf("\r")));
                        value = value.Substring(value.IndexOf("\r") + 1, value.Length - value.IndexOf("\r") - 1);
                    }
                    else
                    {
                        value = value.Substring(value.IndexOf("\r") + 1, value.Length - value.IndexOf("\r") - 1);
                    }
                }
                else
                {
                    value = "";
                }
            }
            for (int i = 0; i < nestedHeaders.Count; i++)
            {
                string tempHeader = nestedHeaders[i];
                tempHeader = tempHeader.Replace("{", "");
                tempHeader = tempHeader.Replace("}", "");
                tempHeader = tempHeader.Replace("\n", "");
                tempHeader = tempHeader.Replace("\r", "");
                tempHeader = tempHeader.Replace("\\", "");
                tempHeader = tempHeader.Substring(tempHeader.IndexOf("\"") + 1);
                //Last element
                if (tempHeader.IndexOf("\"") >= 0)
                {
                    tempHeader = tempHeader.Substring(0, tempHeader.IndexOf("\""));
                }
                nestedHeaders[i] = tempHeader;
            }
            return nestedHeaders;
        }

        public static List<String> DeserializeNestedObjectValues(List<string> names, string value)
        {
            value = value.Replace("{", "");
            value = value.Replace("}", "");
            value = value.Replace("\n", "");
            value = value.Replace("\r", "");
            value = value.Replace("\\", "");

            List<String> nestedValues = new List<String>();
            foreach (string name in names)
            {
                string nameValue = value.Substring(value.IndexOf(name) + name.Length + 1);
                nameValue = nameValue.Substring(nameValue.IndexOf(":") + 2);

                if (nameValue.IndexOf(",") >= 0)
                {
                    nameValue = nameValue.Substring(0, nameValue.IndexOf(","));
                }

                //remove quotes if there
                if (nameValue.IndexOf("\"") >= 0)
                {
                    nameValue = nameValue.Substring(1, nameValue.Length - 2);
                }
                nestedValues.Add(nameValue);
            }
            return nestedValues;
        }
    }
}
