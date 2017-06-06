using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OutsuranceAssesment.Extensions;
using UnitTestProject1.Properties;

namespace UnitTestProject1
{
	[TestClass]
	public class utCIEnumerableExtensions
	{
		[TestMethod]
		public void ToDataTableTest()
		{
			// load some test data into a DataTable
			DataTable dt = new DataTable().LoadFromCSV(Settings.Default.TestCSVFilePath, true);

			// create an IEnumerable of anonymous data row
			var iEnum = (
						 from row in dt.AsEnumerable()
						 select new
						 {
							 FirstName = row.Field<string>("FirstName"),
							 LastName = row.Field<string>("LastName")
						 }
					 );
			// convert the IEnum back into a DataTable
			DataTable target = iEnum.ToDataTable();

			Assert.IsNotNull(target,"Null DataTable was returned");
			Assert.IsTrue(target.Rows.Count > 0, "No data rows were populated");
			Assert.IsTrue(target.Rows.Count == dt.Rows.Count,"Not all the rows were populated");
			Assert.IsTrue(target.Columns.Count == 2, "Columns was not populated");
		}
	}
}
