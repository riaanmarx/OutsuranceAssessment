using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutsuranceAssesment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnitTestProject1.Properties;

namespace UnitTestProject1
{
	[TestClass]
	public class utCProcessingContext
	{
		[TestMethod]
		public void CProcessingContextTest()
		{
			// check that the constructor sets the properties correctly after instantiation
			CProcessingContext context = new CProcessingContext("source", "output");
			Assert.IsTrue(context.SourceFilePath == "source", "Constructor is not setting SourceFilePath property correctly");
			Assert.IsTrue(context.OutputFolder == "output", "Constructor is not setting OutputFolder property correctly");

			// check that property setters work correctly
			context.SourceFilePath = "newsource";
			Assert.IsTrue(context.SourceFilePath == "newsource", "SourceFilePath setter is not setting SourceFilePath property correctly");
			Assert.IsTrue(context.OutputFolder == "output", "SourceFilePath setter is interfering with OutputFolder property value");

			context.OutputFolder = "newoutput";
			Assert.IsTrue(context.OutputFolder == "newoutput", "OutputFolder setter is not setting OutputFolder property correctly");
			Assert.IsTrue(context.SourceFilePath == "newsource", "OutputFolder setter is interfering with the SourceFilePath property value");
		}

		[TestMethod]
		public void ProcessTest()
		{
			frmMain t = new frmMain();
			string targetfolder = Path.GetDirectoryName(Settings.Default.TestCSVTargetPath);
			t.bwFileProcessor_DoWork(this, new System.ComponentModel.DoWorkEventArgs(new CProcessingContext(Settings.Default.TestCSVFilePath, targetfolder)));
			Assert.IsTrue(File.Exists(Path.Combine(targetfolder, "output1.txt")), "Aggregate file was not created");
			Assert.IsTrue(File.Exists(Path.Combine(targetfolder, "output2.txt")), "Address file was not created");
		}
	}

}
