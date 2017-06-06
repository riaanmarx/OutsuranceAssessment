using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace OutsuranceAssesment.Extensions
{
	/// <summary>
	/// Class to handle File IO for CSV files
	/// </summary>
	public static class DataTableExtensions
	{
		/// <summary>
		/// Load data from a CSV file into a DataTable
		/// </summary>
		/// <param name="path">The path to the CSV file containing the data to load into the table</param>
		/// <param name="isFirstRowHeader">Whether the file contains a header row</param>
		/// <returns></returns>
		/// <remarks>implemented as a class extension to improve readability</remarks>
		public static DataTable LoadFromCSV(this DataTable self, string path, bool isFirstRowHeader)
		{
			// validate inputs
			if (string.IsNullOrEmpty(path))
				throw new ArgumentNullException("path","Valid path required");
			if (!File.Exists(path))
				throw new FileNotFoundException("CSV file not found");

			// check that the application is not being built as a 64bit app
			// NOTE: the JET.OLEDB.4.0 provider is not compatible with 64 bit hosts, so the application must be built in x86 
			if (IntPtr.Size == 8)
			{
				throw new InvalidOperationException("The Jet 4.0 OLE DB provider is not compatible with the 64 bit platform. Please reconfigure the host application to be built as an x86 assembly.");
			}
			
			string headerpresent = isFirstRowHeader ? "Yes" : "No";	// con string value for header present
			string pathOnly = Path.GetDirectoryName(path);			// con string value for folder containing file
			string fileName = Path.GetFileName(path);				// con string value for filename

			// construct the select all command text
			string sql = @"SELECT * FROM [" + fileName + "]";

			// open the connection using CSV OLE DB provider
			using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={pathOnly};Extended Properties=\"Text;HDR={headerpresent}\""))
			using (OleDbCommand command = new OleDbCommand(sql, connection))
			using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
			{
				self.Locale = CultureInfo.CurrentCulture;
				adapter.Fill(self);
				return self;
			}
		}

		/// <summary>
		/// Write the data in the DataTable to a text, CSV formatted file
		/// </summary>
		/// <param name="self"></param>
		/// <param name="path">The path to which the data must be written</param>
		/// <param name="isFirstRowHeader">Whether a columns header row must be written</param>
		/// <param name="columns">Optional - the names of the subset of columns you want to export</param>
		public static void WriteToCSV(this DataTable self, string path, bool isFirstRowHeader, string[] columns = null)
		{
			// validate inputs
			if (string.IsNullOrEmpty(path))
				throw new ArgumentNullException("path", "The output path is required");
			if (!Directory.Exists(Path.GetDirectoryName(path)))
				throw new FileNotFoundException("The target path must exist");
			
			// build the CSV text
			StringBuilder sbCSVText = new StringBuilder();

			// if we specified the columns to be included
			if (columns != null)
			{
				// validate all columns specified exists
				if (columns.Count(t => !self.Columns.Contains(t)) > 0)
					throw new ArgumentOutOfRangeException("columns", "One or more of the columns specified does not exist in the dataset");

				// if we must write the headers
				if (isFirstRowHeader)
					sbCSVText.AppendLine(string.Join(",", columns));

				// write the data lines
				foreach (DataRow row in self.DefaultView.ToTable(false, columns).Rows)
				{
					IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
					sbCSVText.AppendLine(string.Join(",", fields));
				}

			}

			else
			{
				// no columns specified

				// if we must write the column headers
				if (isFirstRowHeader)
				{
					// retrieve all the column names
					IEnumerable<string> columnNames = self.Columns.Cast<DataColumn>().
														Select(column => column.ColumnName);
					// write the column headers into the buffer
					sbCSVText.AppendLine(string.Join(",", columnNames));
				}

				// write the data into the buffer
				foreach (DataRow row in self.Rows)
				{
					IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
					sbCSVText.AppendLine(string.Join(",", fields));
				}
			}
			// write all the text into the specified file
			File.WriteAllText(path, sbCSVText.ToString());
		}
	}
}
