using JsonToCSVMerge;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace JsonToCSVMerger
{
    class JsonReader
    {

        private DataTable table;

        public void run(string[] inputFiles)
        {
            for (int i = 0; i < inputFiles.Length; i++)
            {
                readJson(inputFiles[i]);
                Console.WriteLine("Evaluating document " + i + ": " + inputFiles[i]);
                CsvCreator.WriteRecordsToCsv(table);
                table.Rows.Clear();
            }
        }

        public void readJson(string inputFile)
        {

            using (StreamReader r = new StreamReader(inputFile))
            {
                List<string> jsonObjects = SplitConcatenatedJson(r);

                for (int j = 0; j < jsonObjects.Count; j++)
                {
                    IDictionary<string, object> jsonOutput = null;
                    try
                    {
                        jsonOutput = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonObjects[j]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Encountered a reading error: Entry " + j);
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    IDictionary<string, object> outputObjects = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> kvp in jsonOutput)
                    {
                        if (kvp.Value.GetType().Equals(typeof(JObject)))
                        {
                            JObject convObj = (JObject)kvp.Value;
                            addAll(outputObjects, recursivelyExtractJsonObject(convObj));
                        } else
                        {
                            outputObjects.Add(kvp);
                        }
                    }

                    if (table == null)
                    {
                        table = new DataTable();
                    }

                    DataRow row = table.NewRow();
                    foreach (KeyValuePair<string, object> kvp in outputObjects)
                    {
                        
                        if (!table.Columns.Contains(kvp.Key))
                        {
                            table.Columns.Add(kvp.Key);
                        }
                        row[kvp.Key] = kvp.Value;
                        
                    }
                    table.Rows.Add(row);
                }
            }
        }


        private IDictionary<string, object> recursivelyExtractJsonObject(JObject obj)
        {
            JObject convObj = (JObject)obj;
            IDictionary<string, object> outputObjs = convObj.ToObject<IDictionary<string, object>>();
            IDictionary<string, object> containedObjs = new Dictionary<string, object>();
            foreach (object recObj in outputObjs.Values)
            {
                if (recObj == null)
                {
                    continue;
                }
                if (recObj.GetType().Equals(typeof(JObject)))
                {
                    addAll(containedObjs, recursivelyExtractJsonObject((JObject)recObj));
                }
            }
            addAll(outputObjs, containedObjs);
            return outputObjs;
        }

        private IDictionary<string, object> addAll(IDictionary<string, object> into, IDictionary<string, object> outOf)
        {
            foreach (KeyValuePair<string, object> outObj in outOf)
            {
                into.Add(outObj);
            }
            return into;
        }

        private static List<string> SplitConcatenatedJson(StreamReader r)
        {
            String jsonContent = r.ReadToEnd();
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
    }
}
