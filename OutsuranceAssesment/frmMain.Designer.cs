namespace OutsuranceAssesment
{
	partial class frmMain
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
			this.lblSourceFilePath = new System.Windows.Forms.Label();
			this.tbSourceFilePath = new System.Windows.Forms.TextBox();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.lblOutputFoler = new System.Windows.Forms.Label();
			this.tbOuputFolder = new System.Windows.Forms.TextBox();
			this.btnBrowseOutputFolder = new System.Windows.Forms.Button();
			this.btnProcess = new System.Windows.Forms.Button();
			this.ssStatusStrip = new System.Windows.Forms.StatusStrip();
			this.tslblStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsSpring = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.dlgPickOutputFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.dlgOpenSourceFile = new System.Windows.Forms.OpenFileDialog();
			this.bwFileProcessor = new System.ComponentModel.BackgroundWorker();
			this.ssStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblSourceFilePath
			// 
			this.lblSourceFilePath.AutoSize = true;
			this.lblSourceFilePath.Location = new System.Drawing.Point(13, 13);
			this.lblSourceFilePath.Name = "lblSourceFilePath";
			this.lblSourceFilePath.Size = new System.Drawing.Size(84, 13);
			this.lblSourceFilePath.TabIndex = 0;
			this.lblSourceFilePath.Text = "Source file path:";
			// 
			// tbSourceFilePath
			// 
			this.tbSourceFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSourceFilePath.Location = new System.Drawing.Point(102, 10);
			this.tbSourceFilePath.Name = "tbSourceFilePath";
			this.tbSourceFilePath.Size = new System.Drawing.Size(493, 20);
			this.tbSourceFilePath.TabIndex = 1;
			// 
			// btnBrowseSource
			// 
			this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseSource.Location = new System.Drawing.Point(601, 8);
			this.btnBrowseSource.Name = "btnBrowseSource";
			this.btnBrowseSource.Size = new System.Drawing.Size(25, 23);
			this.btnBrowseSource.TabIndex = 2;
			this.btnBrowseSource.Text = "...";
			this.btnBrowseSource.UseVisualStyleBackColor = true;
			this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
			// 
			// lblOutputFoler
			// 
			this.lblOutputFoler.AutoSize = true;
			this.lblOutputFoler.Location = new System.Drawing.Point(26, 50);
			this.lblOutputFoler.Name = "lblOutputFoler";
			this.lblOutputFoler.Size = new System.Drawing.Size(71, 13);
			this.lblOutputFoler.TabIndex = 0;
			this.lblOutputFoler.Text = "Output folder:";
			// 
			// tbOuputFolder
			// 
			this.tbOuputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOuputFolder.Location = new System.Drawing.Point(102, 46);
			this.tbOuputFolder.Name = "tbOuputFolder";
			this.tbOuputFolder.Size = new System.Drawing.Size(493, 20);
			this.tbOuputFolder.TabIndex = 3;
			// 
			// btnBrowseOutputFolder
			// 
			this.btnBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseOutputFolder.Location = new System.Drawing.Point(601, 43);
			this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
			this.btnBrowseOutputFolder.Size = new System.Drawing.Size(25, 23);
			this.btnBrowseOutputFolder.TabIndex = 4;
			this.btnBrowseOutputFolder.Text = "...";
			this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
			this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowseOutputFolder_Click);
			// 
			// btnProcess
			// 
			this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProcess.Location = new System.Drawing.Point(510, 76);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(116, 23);
			this.btnProcess.TabIndex = 5;
			this.btnProcess.Text = "Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// ssStatusStrip
			// 
			this.ssStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblStatusLabel,
            this.tslblStatus,
            this.tsSpring,
            this.tsProgressBar});
			this.ssStatusStrip.Location = new System.Drawing.Point(0, 108);
			this.ssStatusStrip.Name = "ssStatusStrip";
			this.ssStatusStrip.Size = new System.Drawing.Size(638, 22);
			this.ssStatusStrip.TabIndex = 6;
			this.ssStatusStrip.Text = "statusStrip1";
			// 
			// tslblStatusLabel
			// 
			this.tslblStatusLabel.Name = "tslblStatusLabel";
			this.tslblStatusLabel.Size = new System.Drawing.Size(42, 17);
			this.tslblStatusLabel.Text = "Status:";
			// 
			// tslblStatus
			// 
			this.tslblStatus.Name = "tslblStatus";
			this.tslblStatus.Size = new System.Drawing.Size(39, 17);
			this.tslblStatus.Text = "Ready";
			// 
			// tsSpring
			// 
			this.tsSpring.Name = "tsSpring";
			this.tsSpring.Size = new System.Drawing.Size(340, 17);
			this.tsSpring.Spring = true;
			this.tsSpring.Text = "Progress:";
			this.tsSpring.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tsProgressBar
			// 
			this.tsProgressBar.AutoToolTip = true;
			this.tsProgressBar.Name = "tsProgressBar";
			this.tsProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsProgressBar.RightToLeftLayout = true;
			this.tsProgressBar.Size = new System.Drawing.Size(200, 16);
			this.tsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.tsProgressBar.ToolTipText = "Progress...";
			// 
			// dlgOpenSourceFile
			// 
			this.dlgOpenSourceFile.DefaultExt = "csv";
			this.dlgOpenSourceFile.FileName = "data.csv";
			this.dlgOpenSourceFile.Filter = "CSV files|*.csv";
			this.dlgOpenSourceFile.RestoreDirectory = true;
			this.dlgOpenSourceFile.Title = "Select the source file";
			this.dlgOpenSourceFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgOpenSourceFile_FileOk);
			// 
			// bwFileProcessor
			// 
			this.bwFileProcessor.WorkerReportsProgress = true;
			this.bwFileProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwFileProcessor_DoWork);
			this.bwFileProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwFileProcessor_ProgressChanged);
			this.bwFileProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwFileProcessor_RunWorkerCompleted);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(638, 130);
			this.Controls.Add(this.ssStatusStrip);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.btnBrowseOutputFolder);
			this.Controls.Add(this.tbOuputFolder);
			this.Controls.Add(this.btnBrowseSource);
			this.Controls.Add(this.tbSourceFilePath);
			this.Controls.Add(this.lblOutputFoler);
			this.Controls.Add(this.lblSourceFilePath);
			this.MinimumSize = new System.Drawing.Size(654, 168);
			this.Name = "frmMain";
			this.Text = "Outsurance Assesment";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ssStatusStrip.ResumeLayout(false);
			this.ssStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblSourceFilePath;
		private System.Windows.Forms.TextBox tbSourceFilePath;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.Label lblOutputFoler;
		private System.Windows.Forms.TextBox tbOuputFolder;
		private System.Windows.Forms.Button btnBrowseOutputFolder;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.StatusStrip ssStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel tslblStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel tslblStatus;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.FolderBrowserDialog dlgPickOutputFolder;
		private System.Windows.Forms.OpenFileDialog dlgOpenSourceFile;
		private System.Windows.Forms.ToolStripStatusLabel tsSpring;
		private System.ComponentModel.BackgroundWorker bwFileProcessor;
	}
}

