using JsonToCSVMerge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonToCSVMerger
{

    public partial class Main : Form
    {

        private DataTable headerTable;
        private bool isMappingActive = false;
        bool IsMappingActive
        {
            get
            {
                return isMappingActive;
            }
            set
            {
                isMappingActive = value;
                changeMappingActive(value);
            }
        }

        private bool isHeaderActive = false;
        bool IsHeaderActive
        {
            get
            {
                return isHeaderActive;
            }
            set
            {
                isHeaderActive = value;
                ChangeHeaderActive(value);
                btnRemoveHeader.Enabled = false;
            }
        }
        public Main()
        {
            InitializeComponent();
            IsMappingActive = false;
            IsHeaderActive = false;
        }

        

        private void changeMappingActive(bool isActive)
        {
            listboxMappings.Enabled = isActive;
            txtMappingFrom.Enabled = isActive;
            txtMappingTo.Enabled = isActive;
            btnAddMapping.Enabled = isActive;
            gridHeaders.Enabled = isActive;
            btnRemoveSelected.Enabled = isActive;
        }

        private void ChangeHeaderActive(bool isActive)
        {
            btnGenerateHeader.Enabled = isActive;
            btnRemoveHeader.Enabled = isActive;
            btnShowBrowseDialog.Enabled = isActive;
            listboxHeaderFiles.Enabled = isActive;
        }

        private void btnShowBrowseDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in fileDialog.FileNames)
                {
                    listboxHeaderFiles.Items.Add(fileName);
                    listboxHeaderFiles.SetItemChecked(listboxHeaderFiles.Items.IndexOf(fileName), true);
                }
                btnRemoveHeader.Enabled = true;
            }
        }

        private void btnRemoveHeader_Click(object sender, EventArgs e)
        {
            List<string> toRemoveItems = new List<string>();
            foreach (string listBox in listboxHeaderFiles.CheckedItems)
            {
                toRemoveItems.Add(listBox);
            }

            foreach (string removeListBox in toRemoveItems)
            {
                listboxHeaderFiles.Items.Remove(removeListBox);
            }
            if (listboxHeaderFiles.Items.Count == 0)
            {
                btnRemoveHeader.Enabled = false;
            }
        }

        private void btnGenerateHeader_Click(object sender, EventArgs e)
        {
            //TODO: Uncomment if needed
            //IsMappingActive = true;

            JsonReader jsonReader = new JsonReader();
            List<DataTable> tables = new List<DataTable>();
            foreach (string headerFile in listboxHeaderFiles.CheckedItems)
            {
                if (System.IO.File.Exists(headerFile))
                {
                    DataTable table = jsonReader.ReadJson(headerFile);
                    tables.Add(table);
                }
            }
            DataTable outTable = tables[0];
            for (int i = 1; i < tables.Count; i++)
            {
                foreach (DataColumn column in tables[i].Columns)
                {
                    if (!outTable.Columns.Contains(column.ColumnName))
                    {
                        outTable.Columns.Add(column.ColumnName);
                    }
                }
                foreach (DataRow row in tables[i].Rows)
                {
                    DataRow newRow = outTable.NewRow();
                    foreach (DataColumn dataCol in row.Table.Columns)
                    {
                        newRow[dataCol.ColumnName] = row[dataCol.ColumnName];
                    }

                    outTable.Rows.Add(newRow);
                }
            }

            gridHeaders.DataSource = outTable;
            headerTable = outTable;
        }

        private void gridHeaders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<string> distinctValues = gridHeaders.Rows.Cast<DataGridViewRow>()
                .Select(x => x.Cells[e.ColumnIndex].Value.ToString())
                .Distinct()
                .ToList();
            txtFoundDistincts.Text = string.Join(",", distinctValues.ToArray());
        }

        private void btnShowFolderDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtConversionFolderPath.Text = folderDialog.SelectedPath;
            }
            IsHeaderActive = true;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            //if path is given
            if (!System.IO.Directory.Exists(txtConversionFolderPath.Text))
            {
                MessageBox.Show("Path to conversion folder invalid.");
                return;
            }

            string[] files = loadFiles(txtConversionFolderPath.Text);

            CsvCreator.SetPath(txtConversionFolderPath.Text);

            JsonReader reader = new JsonReader();
            //reader.OnNextDocument += setProgressBar;

            object args = new object[2] { files, headerTable };

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(reader.Run);
            worker.ProgressChanged += new ProgressChangedEventHandler(setProgressBar);
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(argument: args);

            btnConvert.Enabled = false;

        }

        private string[] loadFiles(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.json", System.IO.SearchOption.AllDirectories);
            Console.WriteLine("Found " + files.Length + " files.");
            return files;
        }

        private void setProgressBar(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 100)
            {
                btnConvert.Enabled = true;
                progBarProgress.Value = 0;
            } else
            {
                progBarProgress.Value = e.ProgressPercentage;
            }
           
        }
    }
}
