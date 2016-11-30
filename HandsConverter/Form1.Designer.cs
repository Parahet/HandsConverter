namespace HandsConverter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ConvertBtn = new System.Windows.Forms.Button();
            this.SourceFilePathTxb = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BrowseFolderBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BrowseFileBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BrowseFolderOutBtn = new System.Windows.Forms.Button();
            this.OutputFolderPath = new System.Windows.Forms.TextBox();
            this.ConvertProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressbarLbl = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConvertBtn
            // 
            this.ConvertBtn.Location = new System.Drawing.Point(194, 123);
            this.ConvertBtn.Name = "ConvertBtn";
            this.ConvertBtn.Size = new System.Drawing.Size(75, 23);
            this.ConvertBtn.TabIndex = 0;
            this.ConvertBtn.Text = "Convert";
            this.ConvertBtn.UseVisualStyleBackColor = true;
            this.ConvertBtn.Click += new System.EventHandler(this.ConvertBtn_Click);
            // 
            // SourceFilePathTxb
            // 
            this.SourceFilePathTxb.Location = new System.Drawing.Point(6, 19);
            this.SourceFilePathTxb.Name = "SourceFilePathTxb";
            this.SourceFilePathTxb.ReadOnly = true;
            this.SourceFilePathTxb.Size = new System.Drawing.Size(241, 20);
            this.SourceFilePathTxb.TabIndex = 1;
            this.SourceFilePathTxb.Text = "D:\\converter\\H2.txt";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BrowseFolderBtn
            // 
            this.BrowseFolderBtn.Location = new System.Drawing.Point(6, 45);
            this.BrowseFolderBtn.Name = "BrowseFolderBtn";
            this.BrowseFolderBtn.Size = new System.Drawing.Size(126, 23);
            this.BrowseFolderBtn.TabIndex = 2;
            this.BrowseFolderBtn.Text = "Browse folder...";
            this.BrowseFolderBtn.UseVisualStyleBackColor = true;
            this.BrowseFolderBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(303, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BrowseFileBtn
            // 
            this.BrowseFileBtn.Location = new System.Drawing.Point(138, 45);
            this.BrowseFileBtn.Name = "BrowseFileBtn";
            this.BrowseFileBtn.Size = new System.Drawing.Size(109, 23);
            this.BrowseFileBtn.TabIndex = 4;
            this.BrowseFileBtn.Text = "Browse file...";
            this.BrowseFileBtn.UseVisualStyleBackColor = true;
            this.BrowseFileBtn.Click += new System.EventHandler(this.BrowseFileBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SourceFilePathTxb);
            this.groupBox1.Controls.Add(this.BrowseFileBtn);
            this.groupBox1.Controls.Add(this.BrowseFolderBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BrowseFolderOutBtn);
            this.groupBox2.Controls.Add(this.OutputFolderPath);
            this.groupBox2.Location = new System.Drawing.Point(303, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 76);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output path";
            // 
            // BrowseFolderOutBtn
            // 
            this.BrowseFolderOutBtn.Location = new System.Drawing.Point(7, 45);
            this.BrowseFolderOutBtn.Name = "BrowseFolderOutBtn";
            this.BrowseFolderOutBtn.Size = new System.Drawing.Size(102, 23);
            this.BrowseFolderOutBtn.TabIndex = 1;
            this.BrowseFolderOutBtn.Text = "Browse folder...";
            this.BrowseFolderOutBtn.UseVisualStyleBackColor = true;
            this.BrowseFolderOutBtn.Click += new System.EventHandler(this.BrowseFolderOutBtn_Click);
            // 
            // OutputFolderPath
            // 
            this.OutputFolderPath.Location = new System.Drawing.Point(7, 18);
            this.OutputFolderPath.Name = "OutputFolderPath";
            this.OutputFolderPath.ReadOnly = true;
            this.OutputFolderPath.Size = new System.Drawing.Size(251, 20);
            this.OutputFolderPath.TabIndex = 0;
            this.OutputFolderPath.Text = "D:\\converter\\temp";
            // 
            // ConvertProgressBar
            // 
            this.ConvertProgressBar.Location = new System.Drawing.Point(12, 94);
            this.ConvertProgressBar.Name = "ConvertProgressBar";
            this.ConvertProgressBar.Size = new System.Drawing.Size(505, 23);
            this.ConvertProgressBar.TabIndex = 7;
            // 
            // ProgressbarLbl
            // 
            this.ProgressbarLbl.AutoSize = true;
            this.ProgressbarLbl.Location = new System.Drawing.Point(530, 100);
            this.ProgressbarLbl.Name = "ProgressbarLbl";
            this.ProgressbarLbl.Size = new System.Drawing.Size(21, 13);
            this.ProgressbarLbl.TabIndex = 8;
            this.ProgressbarLbl.Text = "- %";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(474, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "SercretButton";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Converted: -; Skipped -  of -";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(474, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 194);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ProgressbarLbl);
            this.Controls.Add(this.ConvertProgressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ConvertBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "HandsConverter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConvertBtn;
        private System.Windows.Forms.TextBox SourceFilePathTxb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BrowseFolderBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BrowseFileBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BrowseFolderOutBtn;
        private System.Windows.Forms.TextBox OutputFolderPath;
        private System.Windows.Forms.ProgressBar ConvertProgressBar;
        private System.Windows.Forms.Label ProgressbarLbl;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
    }
}

