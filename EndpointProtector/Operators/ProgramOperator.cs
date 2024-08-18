using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EndpointProtector.Operators
{
	public class ProgramOperator : IProgramOperator
	{

		public async ValueTask HandleProgramManagement(ProcessTraceData data)
		{
			if (data.SessionID is 0)
			{
				return;
			}

			List<string[]> allLineFields = new List<string[]>();
			var textStream = new System.IO.StringReader(data.CommandLine);
			using (var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(textStream))
			{
				parser.Delimiters = new string[] { " " };
				parser.HasFieldsEnclosedInQuotes = true; // <--- !!!
				string[] fields;
				while ((fields = parser.ReadFields()) != null)
				{
					allLineFields.Add(fields);
				}
			}


			//var toLowerImageFileName = data.ImageFileName.ToLower();
			//var exeEndIndex = data.CommandLine.ToLower().IndexOf(toLowerImageFileName) + toLowerImageFileName.Length;
			//var path = data.CommandLine.Substring(0, exeEndIndex).Replace("\\??\\", string.Empty);

			//if (string.IsNullOrWhiteSpace(path) is false)
			//{

			//}

		}
	}
}
