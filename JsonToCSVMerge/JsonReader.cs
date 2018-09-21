using JsonToCSVMerge;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;

namespace JsonToCSVMerger
{
    class JsonReader
    {

        //public event EventHandler<int> OnNextDocument;

        public void Run(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            Array argArray = (Array)e.Argument;
            string[] inputFiles = (string[])argArray.GetValue(0);
            DataTable header = (DataTable)argArray.GetValue(1);

            for (int i = 0; i < inputFiles.Length; i++)
            {
                DataTable table = ReadJson(inputFiles[i], header);
                Console.WriteLine("Evaluating document " + i + ": " + inputFiles[i]);
                int percentDone = (int)((float)i / (float)inputFiles.Count()*(float)100);
                worker.ReportProgress(percentDone);
                CsvCreator.WriteRecordsToCsv(table);
            }
            worker.ReportProgress(101);
        }

        public DataTable ReadJson(string inputFile, DataTable header = null)
        {
            DataTable table = new DataTable();
            if (header != null)
            {
                foreach (DataColumn headerCol in header.Columns)
                {
                    table.Columns.Add(headerCol.ColumnName);
                }
            }
            
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
                            addAll(outputObjects, recursivelyExtractJsonObject(kvp.Key + "_", convObj));
                        } else
                        {
                            outputObjects.Add(kvp);
                        }
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
            return table;
        }


        private IDictionary<string, object> recursivelyExtractJsonObject(string objectName, JObject obj)
        {
            JObject convObj = (JObject)obj;
            IDictionary<string, object> outputObjs = convObj.ToObject<IDictionary<string, object>>();
            IDictionary<string, object> containedObjs = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> recObj in outputObjs)
            {
                if (recObj.Value == null)
                {
                    continue;
                }
                if (recObj.Value.GetType().Equals(typeof(JObject)))
                {
                    addAll(containedObjs, recursivelyExtractJsonObject(objectName + recObj.Key + "_", (JObject)recObj.Value));
                }
            }
            List<string> keys = new List<string>(outputObjs.Keys);
            foreach (string key in keys)
            {
                object tempObj = outputObjs[key];
                outputObjs.Remove(key);
                outputObjs.Add(objectName + key, tempObj);
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
