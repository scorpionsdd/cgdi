using System;
using System.IO;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for AppSettingsReader.
	/// </summary>
	public sealed class AppSettingsReader
	{
		
		private static string _URLFilesPath;

		// Constructors
		private AppSettingsReader ()
		{
		}
		static AppSettingsReader()
		{
			_URLFilesPath = @"SOFTWARE\Banobras\Gestion\Bandeja Salida\Correspondencia Enviada\";
		}
		
		public static string GetValue (string name)
		{
            
			string ruta = Registry.LocalMachine.OpenSubKey(_URLFilesPath).GetValue(name).ToString();


            if ((ruta !=null))
            {
                return Registry.LocalMachine.OpenSubKey(_URLFilesPath).GetValue(name).ToString();
            }
            else
            {
                Console.WriteLine("Archivo no existente.");
				return null;
            }
            //return  @"http://172.27.201.86/gestion/user_files/";
        }
	}
}
