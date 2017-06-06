using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OutsuranceAssesment
{
	/// <summary>
	/// A class for specifying the processing context
	/// </summary>
	public class CProcessingContext
	{
		/// <summary>
		/// constructor to specify properties
		/// </summary>
		/// <param name="sourceFilePath"></param>
		/// <param name="outputFolder"></param>
		public CProcessingContext(string sourceFilePath, string outputFolder)
		{
			// set the properties
			SourceFilePath = sourceFilePath;
			OutputFolder = outputFolder;
		}


		/// <summary>
		/// Path to the source file
		/// </summary>
		public string SourceFilePath { get; set; }
		/// <summary>
		/// Path for the output folder
		/// </summary>
		public string OutputFolder { get; set; }

	}
}
