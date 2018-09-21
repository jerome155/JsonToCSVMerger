namespace JsonToCSVMerger
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpLoadFile = new System.Windows.Forms.GroupBox();
            this.btnShowBrowseDialog = new System.Windows.Forms.Button();
            this.btnRemoveHeader = new System.Windows.Forms.Button();
            this.btnGenerateHeader = new System.Windows.Forms.Button();
            this.listboxHeaderFiles = new System.Windows.Forms.CheckedListBox();
            this.grpMappings = new System.Windows.Forms.GroupBox();
            this.txtFoundDistincts = new System.Windows.Forms.TextBox();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.listboxMappings = new System.Windows.Forms.CheckedListBox();
            this.btnAddMapping = new System.Windows.Forms.Button();
            this.txtMappingTo = new System.Windows.Forms.TextBox();
            this.txtMappingFrom = new System.Windows.Forms.TextBox();
            this.gridHeaders = new System.Windows.Forms.DataGridView();
            this.btnConvert = new System.Windows.Forms.Button();
            this.progBarProgress = new System.Windows.Forms.ProgressBar();
            this.grpFolder = new System.Windows.Forms.GroupBox();
            this.btnShowFolderDialog = new System.Windows.Forms.Button();
            this.txtConversionFolderPath = new System.Windows.Forms.TextBox();
            this.grpLoadFile.SuspendLayout();
            this.grpMappings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaders)).BeginInit();
            this.grpFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLoadFile
            // 
            this.grpLoadFile.Controls.Add(this.btnShowBrowseDialog);
            this.grpLoadFile.Controls.Add(this.btnRemoveHeader);
            this.grpLoadFile.Controls.Add(this.btnGenerateHeader);
            this.grpLoadFile.Controls.Add(this.listboxHeaderFiles);
            this.grpLoadFile.Location = new System.Drawing.Point(5, 70);
            this.grpLoadFile.Name = "grpLoadFile";
            this.grpLoadFile.Size = new System.Drawing.Size(868, 183);
            this.grpLoadFile.TabIndex = 0;
            this.grpLoadFile.TabStop = false;
            this.grpLoadFile.Text = "Load Header File(s)";
            // 
            // btnShowBrowseDialog
            // 
            this.btnShowBrowseDialog.Location = new System.Drawing.Point(832, 18);
            this.btnShowBrowseDialog.Name = "btnShowBrowseDialog";
            this.btnShowBrowseDialog.Size = new System.Drawing.Size(30, 27);
            this.btnShowBrowseDialog.TabIndex = 5;
            this.btnShowBrowseDialog.Text = "...";
            this.btnShowBrowseDialog.UseVisualStyleBackColor = true;
            this.btnShowBrowseDialog.Click += new System.EventHandler(this.btnShowBrowseDialog_Click);
            // 
            // btnRemoveHeader
            // 
            this.btnRemoveHeader.Location = new System.Drawing.Point(651, 149);
            this.btnRemoveHeader.Name = "btnRemoveHeader";
            this.btnRemoveHeader.Size = new System.Drawing.Size(103, 27);
            this.btnRemoveHeader.TabIndex = 4;
            this.btnRemoveHeader.Text = "Remove";
            this.btnRemoveHeader.UseVisualStyleBackColor = true;
            this.btnRemoveHeader.Click += new System.EventHandler(this.btnRemoveHeader_Click);
            // 
            // btnGenerateHeader
            // 
            this.btnGenerateHeader.Location = new System.Drawing.Point(760, 149);
            this.btnGenerateHeader.Name = "btnGenerateHeader";
            this.btnGenerateHeader.Size = new System.Drawing.Size(103, 27);
            this.btnGenerateHeader.TabIndex = 3;
            this.btnGenerateHeader.Text = "Generate Header";
            this.btnGenerateHeader.UseVisualStyleBackColor = true;
            this.btnGenerateHeader.Click += new System.EventHandler(this.btnGenerateHeader_Click);
            // 
            // listboxHeaderFiles
            // 
            this.listboxHeaderFiles.FormattingEnabled = true;
            this.listboxHeaderFiles.Location = new System.Drawing.Point(6, 19);
            this.listboxHeaderFiles.Name = "listboxHeaderFiles";
            this.listboxHeaderFiles.Size = new System.Drawing.Size(819, 124);
            this.listboxHeaderFiles.TabIndex = 2;
            // 
            // grpMappings
            // 
            this.grpMappings.Controls.Add(this.txtFoundDistincts);
            this.grpMappings.Controls.Add(this.btnRemoveSelected);
            this.grpMappings.Controls.Add(this.listboxMappings);
            this.grpMappings.Controls.Add(this.btnAddMapping);
            this.grpMappings.Controls.Add(this.txtMappingTo);
            this.grpMappings.Controls.Add(this.txtMappingFrom);
            this.grpMappings.Controls.Add(this.gridHeaders);
            this.grpMappings.Location = new System.Drawing.Point(5, 259);
            this.grpMappings.Name = "grpMappings";
            this.grpMappings.Size = new System.Drawing.Size(868, 481);
            this.grpMappings.TabIndex = 1;
            this.grpMappings.TabStop = false;
            this.grpMappings.Text = "Define Mappings";
            // 
            // txtFoundDistincts
            // 
            this.txtFoundDistincts.Enabled = false;
            this.txtFoundDistincts.Location = new System.Drawing.Point(6, 250);
            this.txtFoundDistincts.Multiline = true;
            this.txtFoundDistincts.Name = "txtFoundDistincts";
            this.txtFoundDistincts.Size = new System.Drawing.Size(855, 32);
            this.txtFoundDistincts.TabIndex = 9;
            this.txtFoundDistincts.Text = "[Found Distinct Values]";
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(767, 448);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(95, 27);
            this.btnRemoveSelected.TabIndex = 8;
            this.btnRemoveSelected.Text = "Remove";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            // 
            // listboxMappings
            // 
            this.listboxMappings.FormattingEnabled = true;
            this.listboxMappings.Location = new System.Drawing.Point(6, 318);
            this.listboxMappings.Name = "listboxMappings";
            this.listboxMappings.Size = new System.Drawing.Size(855, 124);
            this.listboxMappings.TabIndex = 7;
            // 
            // btnAddMapping
            // 
            this.btnAddMapping.Location = new System.Drawing.Point(767, 288);
            this.btnAddMapping.Name = "btnAddMapping";
            this.btnAddMapping.Size = new System.Drawing.Size(95, 27);
            this.btnAddMapping.TabIndex = 4;
            this.btnAddMapping.Text = "Add";
            this.btnAddMapping.UseVisualStyleBackColor = true;
            // 
            // txtMappingTo
            // 
            this.txtMappingTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMappingTo.Location = new System.Drawing.Point(421, 290);
            this.txtMappingTo.Name = "txtMappingTo";
            this.txtMappingTo.Size = new System.Drawing.Size(343, 22);
            this.txtMappingTo.TabIndex = 6;
            this.txtMappingTo.Text = "[Map To]";
            // 
            // txtMappingFrom
            // 
            this.txtMappingFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMappingFrom.Location = new System.Drawing.Point(6, 290);
            this.txtMappingFrom.Name = "txtMappingFrom";
            this.txtMappingFrom.Size = new System.Drawing.Size(409, 22);
            this.txtMappingFrom.TabIndex = 5;
            this.txtMappingFrom.Text = "[Map From]";
            // 
            // gridHeaders
            // 
            this.gridHeaders.AllowUserToAddRows = false;
            this.gridHeaders.AllowUserToDeleteRows = false;
            this.gridHeaders.AllowUserToOrderColumns = true;
            this.gridHeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHeaders.Location = new System.Drawing.Point(6, 19);
            this.gridHeaders.Name = "gridHeaders";
            this.gridHeaders.ReadOnly = true;
            this.gridHeaders.Size = new System.Drawing.Size(855, 224);
            this.gridHeaders.TabIndex = 0;
            this.gridHeaders.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridHeaders_ColumnHeaderMouseClick);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(778, 746);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(95, 27);
            this.btnConvert.TabIndex = 9;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // progBarProgress
            // 
            this.progBarProgress.Location = new System.Drawing.Point(5, 746);
            this.progBarProgress.Name = "progBarProgress";
            this.progBarProgress.Size = new System.Drawing.Size(767, 27);
            this.progBarProgress.Step = 1;
            this.progBarProgress.TabIndex = 10;
            // 
            // grpFolder
            // 
            this.grpFolder.Controls.Add(this.btnShowFolderDialog);
            this.grpFolder.Controls.Add(this.txtConversionFolderPath);
            this.grpFolder.Location = new System.Drawing.Point(5, 12);
            this.grpFolder.Name = "grpFolder";
            this.grpFolder.Size = new System.Drawing.Size(868, 52);
            this.grpFolder.TabIndex = 11;
            this.grpFolder.TabStop = false;
            this.grpFolder.Text = "Conversion Folder";
            // 
            // btnShowFolderDialog
            // 
            this.btnShowFolderDialog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowFolderDialog.Location = new System.Drawing.Point(832, 17);
            this.btnShowFolderDialog.Name = "btnShowFolderDialog";
            this.btnShowFolderDialog.Size = new System.Drawing.Size(30, 27);
            this.btnShowFolderDialog.TabIndex = 8;
            this.btnShowFolderDialog.Text = "...";
            this.btnShowFolderDialog.UseVisualStyleBackColor = true;
            this.btnShowFolderDialog.Click += new System.EventHandler(this.btnShowFolderDialog_Click);
            // 
            // txtConversionFolderPath
            // 
            this.txtConversionFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConversionFolderPath.Location = new System.Drawing.Point(8, 19);
            this.txtConversionFolderPath.Name = "txtConversionFolderPath";
            this.txtConversionFolderPath.Size = new System.Drawing.Size(817, 22);
            this.txtConversionFolderPath.TabIndex = 6;
            this.txtConversionFolderPath.Text = "[Path]";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 778);
            this.Controls.Add(this.grpFolder);
            this.Controls.Add(this.progBarProgress);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.grpMappings);
            this.Controls.Add(this.grpLoadFile);
            this.Name = "Main";
            this.Text = "Json To CSV Merger";
            this.grpLoadFile.ResumeLayout(false);
            this.grpMappings.ResumeLayout(false);
            this.grpMappings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaders)).EndInit();
            this.grpFolder.ResumeLayout(false);
            this.grpFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLoadFile;
        private System.Windows.Forms.CheckedListBox listboxHeaderFiles;
        private System.Windows.Forms.GroupBox grpMappings;
        private System.Windows.Forms.Button btnGenerateHeader;
        private System.Windows.Forms.DataGridView gridHeaders;
        private System.Windows.Forms.Button btnAddMapping;
        private System.Windows.Forms.TextBox txtMappingTo;
        private System.Windows.Forms.TextBox txtMappingFrom;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.CheckedListBox listboxMappings;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ProgressBar progBarProgress;
        private System.Windows.Forms.Button btnRemoveHeader;
        private System.Windows.Forms.Button btnShowBrowseDialog;
        private System.Windows.Forms.TextBox txtFoundDistincts;
        private System.Windows.Forms.GroupBox grpFolder;
        private System.Windows.Forms.Button btnShowFolderDialog;
        private System.Windows.Forms.TextBox txtConversionFolderPath;
    }
}