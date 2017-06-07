using OutsuranceAssesment.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutsuranceAssesment.Extensions;
using System.Threading;
using System.Diagnostics;

namespace OutsuranceAssesment
{
	/// <summary>
	/// Main form class for Outsurance Assessment application
	/// </summary>
	public partial class frmMain : Form
	{
		#region // Constants ...
		const string OUTPUTPATH_NAMES = "Output1.txt";
		const string OUTPUTPATH_ADDRESSES = "Output2.txt";
		#endregion

		#region // Form constructor ...
		public frmMain()
		{
			InitializeComponent();
		}
		#endregion

		#region // Private form functions ...
		
		/// <summary>
		/// Show the source file open dialog
		/// </summary>
		private void ShowOpenFileDialog()
		{
			// show the open file dialog
			dlgOpenSourceFile.ShowDialog();
		}

		/// <summary>
		/// Show the Output folder picker dialog
		/// </summary>
		private void ShowOutputFolderDialog()
		{
			// show the open file dialog
			if (dlgPickOutputFolder.ShowDialog() == DialogResult.OK)
			{
				// manually raise "fileOK" event to maintain conformation with open dialog
				dlgPickOutputFolder_FolderOk(this, null);
			}
		}
		
		/// <summary>
		/// Set the default values for the controls on the form
		/// </summary>
		private void InitializaFormValues()
		{
			// retrieve the folder containing the current executable
			string exeFolderPath
				= Path.GetDirectoryName(Application.ExecutablePath);

			// default the source file to the data.csv provided in the current executable's folder if it exists
			string sourceFilePath = Path.Combine(exeFolderPath, Settings.Default.DefaultSource);
			if (File.Exists(sourceFilePath))
				tbSourceFilePath.Text = sourceFilePath;

			// default the output folder to the current executable's folder if it exists
			string outputFolder = exeFolderPath;
			if (Directory.Exists(outputFolder))
				tbOuputFolder.Text = exeFolderPath;

			// default the folder for the open source file dialog
			dlgOpenSourceFile.InitialDirectory = exeFolderPath;

			// default the folder for the folder picker dialog
			dlgPickOutputFolder.SelectedPath = exeFolderPath;
		}
		
		/// <summary>
		/// Launch a file using the default OS editor
		/// </summary>
		/// <param name="file"></param>
		private void LaunchFile(string fileName)
		{
			// launch a new process for the specified file
			Process.Start(fileName);
		}

		#endregion
		
		#region // Processing-related functions ...
		/// <summary>
		/// Retrieve the count aggregates per FirstName and LastName
		/// </summary>
		/// <param name="source">DataTable containing original data as retrieved from CSV file</param>
		/// <returns>DataTable containing aggregate counts</returns>
		private DataTable GetNameAggregateCountsTable(DataTable source)
		{
			// Construct the aggregate result set and return it as a table
			return (
						 from row in source.AsEnumerable()
						 group row by row.Field<string>("LastName") into freq
						 orderby freq.Key
						 select new
						 {
							 Name = freq.Key,
							 Frequency = freq.Count()
						 }
					 ).Union(
						 from row in source.AsEnumerable()
						 group row by row.Field<string>("FirstName") into freq
						 orderby freq.Key
						 select new
						 {
							 Name = freq.Key,
							 Frequency = freq.Count()
						 }
					).ToDataTable();
		}

		/// <summary>
		/// Extract the street name from an address line which is in the form "XX YYY ZZ"
		/// where XX is the number, YY is the street name and ZZ is an indication of the street "type"(Rd, street, avenue,road etc.)
		/// </summary>
		/// <param name="addressline"></param>
		/// <returns></returns>
		private string ExtractStreetname(string addressline)
		{
			// split the line using spaces, and report the second element
			return addressline.Split(' ')[1];
		}

		/// <summary>
		/// Construct a DataTable containing the Address data, and a derived separate [Streetname] column
		/// </summary>
		/// <param name="source">DataTable containing original data as retrieved from CSV file</param>
		/// <returns></returns>
		private DataTable GetExtendedAddressTable(DataTable source)
		{
			// Construct the new data set and return it as a DataTable
			return (from row in source.AsEnumerable()
					select new
					{
						Address = row.Field<string>("Address"),
						Streetname = ExtractStreetname(row.Field<string>("Address"))
					}).ToDataTable();
		}

		/// <summary>
		/// Background worker function to process the file and save results
		/// </summary>
		/// <returns></returns>
		/// <remarks>Threading this task is not needed in this small sample data set, but in case larger files may later be processed using this, lets run it in its own thread not to lock up the UI</remarks>
		public void bwFileProcessor_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				// extract/typecast the job context
				CProcessingContext args = e.Argument as CProcessingContext;

				// report the current state and progress
				bwFileProcessor.ReportProgress(0, "Retrieving data");

				// retrieve the data from the source CSV file
				using (DataTable Source = new DataTable()
					.LoadFromCSV(args.SourceFilePath, true))
				{
					// report the current state and progress
					bwFileProcessor.ReportProgress(10, "Aggregating counts");

					#region // Step 1: Report aggregate counts per FirstName and LastName ...
					// count the frequency per Last name and first name
					DataTable results = GetNameAggregateCountsTable(Source);

					// report the current state and progress
					bwFileProcessor.ReportProgress(50, "Writing Name aggregates output file");

					// sort the table per requirements
					results.DefaultView.Sort = "Frequency DESC, Name ASC";

					// write the view to the output file
					string outputPath = Path.Combine(args.OutputFolder, OUTPUTPATH_NAMES);
					results.DefaultView.ToTable()
						.WriteToCSV(outputPath, false);
					#endregion

					// report the current state and progress
					bwFileProcessor.ReportProgress(60, "Sorting addresses");
					
					#region // Step 2: Report addresses sorted per street names ...
					// create a new data table with a derived column for sorting purposes
					DataTable resultsAddresses = GetExtendedAddressTable(Source);

					// report the current state and progress
					bwFileProcessor.ReportProgress(90, "Writing addresses output file");

					// sort the view per requirements
					resultsAddresses.DefaultView.Sort = "Streetname ASC";

					// write the view contents to the output file
					outputPath = Path.Combine(args.OutputFolder, OUTPUTPATH_ADDRESSES);
					resultsAddresses.DefaultView.ToTable()
						.WriteToCSV(outputPath, false, new string[] { "Address" });
					#endregion
				}

				// report the current state and progress
				bwFileProcessor.ReportProgress(100, "Completed");
			}
			catch (Exception ex)
			{
				// we don't want to handle exceptions in the background thread.
				//	 let them be bubbled to the worker's completion event.
				throw;	
			}
		}

		/// <summary>
		/// Update the UI's progress display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bwFileProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// update the current state label
			tslblStatus.Text = (string)e.UserState;
			// update the progress bar
			tsProgressBar.Value = e.ProgressPercentage;
		}

		/// <summary>
		/// Raised when processing has completed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bwFileProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// if there was an exception during processing
			if (e.Error != null)
			{
				// report the error back to the user
				tslblStatus.Text = $"Exception: {e.Error.Message}";
				MessageBox.Show($"An exception occurred during processing: {e.Error.Message}");
			}

			// enable the process button, so the file can be processed again
			btnProcess.Enabled = true;

			// if the file was processed successfully,
			if (e.Error == null)
			{
				// enable the result buttons
				btnOpenAddressFile.Enabled = true;
				btnOpenNamesFile.Enabled = true;

				// show a message when completed successfully
				MessageBox.Show($"File was processed successfully");
			}
		}

		#endregion

		#region // Form & control event handlers ...
		/// <summary>
		/// Validate that all fields have been filled in as needed before processing
		/// </summary>
		/// <returns>A boolean, True if form is complete, False if not</returns>
		private bool isFormValid()
		{
			// check if source is empty
			if (string.IsNullOrEmpty(this.tbSourceFilePath.Text))
			{
				MessageBox.Show("Please select a source file before processing.","Invalid field data", MessageBoxButtons.OK);
				return false;
			}
			// check if source is valid
			if (!File.Exists(this.tbSourceFilePath.Text))
			{
				MessageBox.Show("Please select an existing source file before processing.", "Invalid field data", MessageBoxButtons.OK);
				return false;
			}
			// check if target folder is empty
			if (string.IsNullOrEmpty(this.tbOuputFolder.Text))
			{
				MessageBox.Show("Please select an output folder before processing.", "Invalid field data", MessageBoxButtons.OK);
				return false;
			}
			// check if source is valid
			if (!Directory.Exists(this.tbOuputFolder.Text))
			{
				MessageBox.Show("Please select an existing output folder before processing.", "Invalid field data", MessageBoxButtons.OK);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Event handler: btnProcess.Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProcess_Click(object sender, EventArgs e)
		{
			try
			{
				// validate form
				if (!this.isFormValid()) return;

				// disable the process button
				btnProcess.Enabled = false;

				// start the processing of the file
				bwFileProcessor.RunWorkerAsync(
					new CProcessingContext(tbSourceFilePath.Text, tbOuputFolder.Text));
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An exception occurred while starting to process the file: {ex.Message}");
			}
			
		}

		/// <summary>
		/// Event handler: btnBrowseSource.Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBrowseSource_Click(object sender, EventArgs e)
		{
			try
			{
				// show the open file dialog
				ShowOpenFileDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An exception occurred while showing the open dialog: {ex.Message}");
			}
		}

		/// <summary>
		/// Event handler: dlgOpenSourceFile.FileOk 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgOpenSourceFile_FileOk(object sender, CancelEventArgs e)
		{
			// set the text box to the selected file's path
			tbSourceFilePath.Text = dlgOpenSourceFile.FileName;
		}

		/// <summary>
		/// Event handler: dlgOpenSourceFile.FolderOk 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgPickOutputFolder_FolderOk(object sender, CancelEventArgs e)
		{
			// set the text box to the selected file's path
			tbOuputFolder.Text = dlgPickOutputFolder.SelectedPath;
		}

		/// <summary>
		/// Event handler: frmMain.Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				// set the default form values
				InitializaFormValues();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An exception occurred initializing the application: {ex.Message}");
			}
		}

		/// <summary>
		/// Event handler: btnBrowseOutputFolder.Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBrowseOutputFolder_Click(object sender, EventArgs e)
		{
			try
			{
				ShowOutputFolderDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An exception occurred while opening the pick output folder dialog: {ex.Message}");
			}
		}
		
		/// <summary>
		/// Event handler: btnOpenNamesFile.Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenNamesFile_Click(object sender, EventArgs e)
		{
			// construct the output file path
			string filePath = Path.Combine(tbOuputFolder.Text, OUTPUTPATH_NAMES);
			// Open the output file in the user's preferred text editor
			LaunchFile(filePath);
		}

		/// <summary>
		/// Event handler: btnOpenAddressFile.Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenAddressFile_Click(object sender, EventArgs e)
		{
			// construct the output file path
			string filePath = Path.Combine(tbOuputFolder.Text, OUTPUTPATH_ADDRESSES);
			// Open the output file in the user's preferred text editor
			LaunchFile(filePath);
		}
		#endregion
	}
}
