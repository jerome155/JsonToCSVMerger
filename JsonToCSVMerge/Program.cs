//#define debug

using JsonToCSVMerger;
using System;
using System.Data;
using System.Windows.Forms;

namespace JsonToCSVMerge
{
    
    class Program
    {

        private static string[] mainArgs;

        [STAThread]
        static void Main(string[] args)
        {
            mainArgs = args;
            Form main = new Main();
            Application.Run(main);
        }
    }
}

