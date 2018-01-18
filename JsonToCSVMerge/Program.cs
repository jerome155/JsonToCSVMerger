//#define debug

using System;
using System.Data;

namespace JsonToCSVMerge
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            if (args.Length < 1)
            {
                Console.WriteLine("Please specify folder path as argument.");
                Console.ReadLine();
                System.Environment.Exit(0);
            }
            else
            {
                path = args[0];
            }
            string[] files = loadFiles(path);

            DataTable table = JsonReader.Run(files);
            
            CsvCreator.WriteCsv(table, path);
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        private static string[] loadFiles(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            Console.WriteLine("Found " + files.Length + " files.");
            return files;
        }
    }
}

