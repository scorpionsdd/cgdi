using System;

using Aced.Misc;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for RegistryRead.
	/// </summary>
	public class RegistryRead
	{
			private const string
			cnsRegistryKeyName = "Software\\Banobras\\Gestion\\Bandeja Salida\\Correspondencia Enviada",

			//let's save the string in the default value of the registry key
			cfgStringValueName = "URLFilesPath",
			cfgStringDesarrollo = "Desarrollo",
			cfgStringProduccion = "Produccion",
			cfgStringURLFilesPath = "URLFilesPath";

			private static string _stringValue;
			private static string _sDesarrollo;
			private static string _sProduccion;
			private static string _sURLFilesPath;

		public RegistryRead()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void GetRegistryKeys(string elementType)
		{
			using (AcedRegistry config = new AcedRegistry(AcedRootKey.LocalMachine,
					   cnsRegistryKeyName, false))
			{
				config.Get(cfgStringValueName, ref _stringValue);
				config.Get(cfgStringDesarrollo, ref _sDesarrollo);
				config.Get(cfgStringProduccion, ref _sProduccion);
				config.Get(cfgStringURLFilesPath, ref _sURLFilesPath);
			}

		}


	}
}
