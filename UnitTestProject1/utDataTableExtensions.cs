using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using OutsuranceAssesment.Extensions;
using UnitTestProject1.Properties;
using System.IO;

namespace UnitTestProject1
{
	[TestClass]
	public class utDataTableExtensions
	{
		/// <summary>
		/// Test that a CSV file can be loaded successfully
		/// </summary>
		[TestMethod]
		public void LoadTableFromCSVTest()
		{
			// assert that the test CSV exists
			Assert.IsTrue(File.Exists(Settings.Default.TestCSVFilePath), "The test CSV file does not exist");

			// just load the test file
			DataTable tst = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath,true);

			//check that the data table contains the data
			Assert.IsNotNull(tst,"The data table object is null");
			Assert.IsFalse(tst.HasErrors, "The DataTable reports that it has some errors");
			Assert.IsTrue(tst.Rows.Count > 0,"The DataTable does not contain any rows");
			Assert.IsTrue(tst.Rows.Count == 8, "The DataTable contains the wrong number of rows");
			Assert.IsTrue(tst.Columns.Count == 4, "The DataTable contains the wrong number of columns");
		}
		
		/// <summary>
		/// Check that the LoadTable From CSV function handles an empty path correctly
		/// </summary>
		[TestMethod]
		public void LoadTableFromCSVTest_EmptyPath()
		{
			try
			{
				// load a file that does not exist
				DataTable tst = new DataTable().LoadFromCSV("", true);
				Assert.Fail("Empty file path was not handled");
			}
			catch (ArgumentNullException)
			{
				// this is the expected behavior
			}
			catch(Exception)
			{
				Assert.Fail("Empty file path was handled incorrect");
			}

		}

		/// <summary>
		/// Check that the LoadTable From CSV function handles a bad path correctly
		/// </summary>
		[TestMethod]
		public void LoadTableFromCSVTest_NonExistingPath()
		{
			try
			{
				// load a file that does not exist
				DataTable tst = new DataTable().LoadFromCSV("somenoneexistingfile.csv", true);
				Assert.Fail("Non-existing file path was handled incorrect");
			}
			catch(FileNotFoundException)
			{
				// this is the expected behavior
			}
			catch (Exception )
			{
				Assert.Fail("Non-existing file path was handled incorrect");
			}
			
		}

		/// <summary>
		/// Check that the Write table to CSV handles an empty path
		/// </summary>
		[TestMethod]
		public void WriteToCSVTest_EmptyPath()
		{
			try
			{
				DataTable tst = new DataTable();
				tst.WriteToCSV("", false);
				Assert.Fail("Empty file path was not handled");
			}
			catch (ArgumentNullException)
			{
				// this is the expected behavior
			}
			catch (Exception)
			{
				Assert.Fail("Empty file path was handled incorrect");
			}

		}

		/// <summary>
		/// Test that the write table to csv handles an non-existing target folder
		/// </summary>
		[TestMethod]
		public void WriteToCSVTest_NonExistingPath()
		{
			try
			{
				DataTable tst = new DataTable();
				tst.WriteToCSV(@"d:\some\nonexisting\folder\out.txt", false);
				Assert.Fail("Non-existing file path was handled incorrect");
			}
			catch (FileNotFoundException)
			{
				// this is the expected behavior
			}
			catch (Exception)
			{
				Assert.Fail("Non-existing file path was handled incorrect");
			}

		}

		/// <summary>
		/// Test that the write table to csv handles an non-existing column
		/// </summary>
		[TestMethod]
		public void WriteToCSVTest_NonExistingColumn()
		{
			try
			{
				DataTable tst = new DataTable();
				tst.WriteToCSV(@"c:\temp\out.txt", false, new string[] { "column1" });
				Assert.Fail("Non-existing column was handled incorrect");
			}
			catch (ArgumentOutOfRangeException)
			{
				// this is the expected behavior
			}
			catch (Exception)
			{
				Assert.Fail("Non-existing column was handled incorrect");
			}

		}

		[TestMethod]
		public void WriteToCSVTest_withHeaders()
		{
			// load some sample data
			DataTable tst = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath, true);

			// write the data to the file with headers
			tst.WriteToCSV(Settings.Default.TestCSVTargetPath, true);
			// read the data from the file with the headers
			DataTable rd = new DataTable().LoadFromCSV(Settings.Default.TestCSVTargetPath, true);
			// check that the two sets of data are similar
			Assert.IsNotNull(rd, "Read datatable is null");
			Assert.IsNotNull(rd.Rows,"Read datatable's rows is null");
			Assert.IsTrue(rd.Rows.Count == tst.Rows.Count, "Incorrect number of rows loaded");
			Assert.IsTrue(rd.Columns.Count == tst.Columns.Count, "Incorrect number of columns loaded");
		}


		[TestMethod]
		public void WriteToCSVTest_woHeaders()
		{
			// load some sample data
			DataTable tst = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath, true);
			
			// write the file without headers
			tst.WriteToCSV(Settings.Default.TestCSVTargetPath, false);
			// read the file without headers
			DataTable rd = new DataTable().LoadFromCSV(Settings.Default.TestCSVTargetPath, false);
			// check that the two sets of data are similar even though column names will not match
			Assert.IsNotNull(rd, "Read datatable is null - no headers");
			Assert.IsNotNull(rd.Rows, "Read datatable's rows is null - no headers");
			Assert.IsTrue(rd.Rows.Count == tst.Rows.Count, "Incorrect number of rows loaded - no headers");
			Assert.IsTrue(rd.Columns.Count == tst.Columns.Count, "Incorrect number of columns loaded - no headers");
		}

		[TestMethod]
		public void WriteToCSVTest_withHeadersAndColumns()
		{
			// load some sample data
			DataTable tst = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath, true);

			// write the data to the file with headers
			tst.WriteToCSV(Settings.Default.TestCSVTargetPath, true, new string[] {"FirstName","LastName" });
			// read the data from the file with the headers
			DataTable rd = new DataTable().LoadFromCSV(Settings.Default.TestCSVTargetPath, true);
			// check that the two sets of data are similar
			Assert.IsNotNull(rd, "Read datatable is null");
			Assert.IsNotNull(rd.Rows, "Read datatable's rows is null");
			Assert.IsTrue(rd.Rows.Count == tst.Rows.Count, "Incorrect number of rows loaded");
			Assert.IsTrue(rd.Columns.Count == 2, "Incorrect number of columns loaded");
		}

		[TestMethod]
		public void WriteToCSVTest_woHeadersAndColumns()
		{
			// load some sample data
			DataTable tst = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath, true);

			// write the file without headers
			tst.WriteToCSV(Settings.Default.TestCSVTargetPath, false, new string[] { "FirstName", "LastName" });
			// read the file without headers
			DataTable rd = new DataTable().LoadFromCSV(Settings.Default.TestCSVTargetPath, false);
			// check that the two sets of data are similar even though column names will not match
			Assert.IsNotNull(rd, "Read datatable is null - no headers");
			Assert.IsNotNull(rd.Rows, "Read datatable's rows is null - no headers");
			Assert.IsTrue(rd.Rows.Count == tst.Rows.Count, "Incorrect number of rows loaded - no headers");
			Assert.IsTrue(rd.Columns.Count == 2, "Incorrect number of columns loaded - no headers");
		}
	}
}
