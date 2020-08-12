using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessModule
{
	internal static class Diagnostics
	{
		//todo: should probably be pulled from a config file, but this is just for my purposes for now...
		private const string DiagnosticFilePath = @"F:\logs\CopyDiagnosticResults.txt";

		/// <summary> Don't use this method for logging!! Only for internal diagnostics! </summary>
		public static void WriteDiagnostic(string text)
		{
			using (var file = new FileStream(DiagnosticFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
			{
				using (var writer = new StreamWriter(file))
				{
					writer.WriteLine(text);
				}
			}
		}
	}
}
