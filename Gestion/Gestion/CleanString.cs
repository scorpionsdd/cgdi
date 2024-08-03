using System;
using System.Text;

namespace Gestion.WebComponents
{
	/// <summary>
	/// A sample class to clean the input into web pages 
	/// </summary>
	public sealed class CleanString {

		public static string InputText(string inputString, int maxLength) {

			
			StringBuilder retVal = new StringBuilder();

			// check incoming parameters for null or blank string
			if ((inputString != null) && (inputString != String.Empty)) {
				inputString = inputString.Trim();

				//chop the string incase the client-side max length
				//fields are bypassed to prevent buffer over-runs

				//if (inputString.Length > maxLength)
				//	inputString = inputString.Substring(0, inputString.Length);

				//convert some harmful symbols incase the regular
				//expression validators are changed
				for (int i = 0; i < inputString.Length; i++) {
					switch (inputString[i]) {
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}

				// Replace single quotes with white space
				retVal.Replace("'", " ");
			}

			return retVal.ToString();
			
		}
	}
}
