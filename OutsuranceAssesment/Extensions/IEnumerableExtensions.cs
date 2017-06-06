using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OutsuranceAssesment.Extensions
{
	/// <summary>
	/// Class extensions for IEnumerable
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Extension to convert IEnumerable<DataRow> to DataTable
		/// </summary>
		/// <typeparam name="T">Must be DataRow</typeparam>
		/// <param name="source">The instance implementing IEnumerable<DataRow></param>
		/// <returns>A DataTable with all the Columns and DataRows added</returns>
		public static DataTable ToDataTable<T>(this IEnumerable<T> source)
		{
			// retrieve the properties (columns)
			PropertyInfo[] properties = typeof(T).GetProperties();

			// create the target table
			DataTable output = new DataTable();

			// add the columns to the table
			foreach (var prop in properties)
			{
				output.Columns.Add(prop.Name, prop.PropertyType);
			}

			// add the data into the table
			foreach (var item in source)
			{
				DataRow row = output.NewRow();

				foreach (var prop in properties)
				{
					row[prop.Name] = prop.GetValue(item, null);
				}

				output.Rows.Add(row);
			}

			// return the target table
			return output;
		}
	}
}
