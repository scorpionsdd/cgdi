using System;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using System.Globalization;

using Gestion.BusinessLogicLayer;
using Aced.Misc;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ExcelConvert
	{
		private const string
			cnsRegistryKeyName = "Software\\Banobras\\Gestion\\Bandeja Salida\\Correspondencia Enviada",

			//let's save the string in the default value of the registry key
			cfgStringValueName = "URLFilesPath",
			cfgStringDesarrollo = "Desarrollo",
			cfgStringProduccion = "Produccion",
			cfgStringURLFilesPath = "URLFilesPath",
			cfgStringGeneratedFilesPath = "GeneratedFilesPath";

		private static string _stringValue;
		private static string _sDesarrollo;
		private static string _sProduccion;
		private static string _sURLFilesPath;
		private static string _GeneratedFilesPath;
		private const double cnsDefaultHight = 12.75;

		public ExcelConvert()
		{
			//
			// TODO: Add constructor logic here
			// 
		}
		public void Create(string sTemplate, string sFileCreate, string sSheet)
		{

			// creating Excel Application
 		    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();  // Creates a new Excel Application

			excelApp.Visible = true;  // Makes Excel visible to the user.

			// The following line adds a new workbook
			//Microsoft.Office.Interop.Excel.Workbook newWorkbook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
			
			// The following code opens an existing workbook
			string workbookPath = "c:/inetpub/wwwroot/gestion/user_files/ReportDesigner/Templates/" + sTemplate;  // Add your own path here
			
			
			Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
				false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, 
				false,  0, true, false, false);
			
			// The following gets the Worksheets collection
			Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;

			// The following gets sSheet for editing
			Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(sSheet);
			
			// The following gets cell A1 for editing
			Microsoft.Office.Interop.Excel.Range excelCell = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("A11", "A11");

			// The following sets cell A1's value to "Hi There"
			excelCell.Value2 = "Hi There";
			sFileCreate = "data/";

			excelWorksheet.SaveAs("c:/" + sFileCreate, 5, "", "", false, false, true, false, false, true);

			excelWorksheet = null;
			
			

		}
		public string CreateExcelWorkbook(string workbookPath, string sSheet, OracleDataReader dr, string ElaborationDateFrom, string ElaborationDateTo, string sUserId)
		{

			using (AcedRegistry config = new AcedRegistry(AcedRootKey.LocalMachine,
					   cnsRegistryKeyName, false))
			{
				config.Get(cfgStringValueName, ref _stringValue);
				config.Get(cfgStringDesarrollo, ref _sDesarrollo);
				config.Get(cfgStringProduccion, ref _sProduccion);
				config.Get(cfgStringURLFilesPath, ref _sURLFilesPath);
				config.Get(cfgStringGeneratedFilesPath, ref _GeneratedFilesPath);
			}

			Microsoft.Office.Interop.Excel.Application oXL;
			Microsoft.Office.Interop.Excel._Workbook oWB;
			Microsoft.Office.Interop.Excel._Worksheet oSheet;
			Microsoft.Office.Interop.Excel.Range oRng;



			try
			{
				GC.Collect(); // clean up any other excel guys hangin' around...
		
				string sUserKey = Users.GetUserKey(int.Parse(sUserId));
				string strCurrentDir = _GeneratedFilesPath + sUserKey + "\\";
				RemoveFiles(strCurrentDir); // utility method to clean up old files

				System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
				//System.Globalization.CultureInfo  newCulture = new System.Globalization.CultureInfo(Microsoft.Office.Interop.Excel.Application. ExcelApp.LanguageSettings.LanguageID(Microsoft.Office.Core.MsoAppLanguageID.msoLanguageIDUI));								
				//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");            
				Microsoft.Office.Interop.Excel.Application tExcel = new Application();
				CultureInfo cSystemCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo cExcelCulture = new CultureInfo("es-ES", false);
				System.Threading.Thread.CurrentThread.CurrentCulture = cExcelCulture; //oldCI;



				oXL = new Microsoft.Office.Interop.Excel.Application();
				oXL.Visible = false;
				//Get a new workbook.
				oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(workbookPath));

				//oWB = oXL.Workbooks.Open(workbookPath, 0, false, 5, "", "", false, 
				// 	  Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false,  0, true, false, false);

				oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
				//get our Data     
				// build the sheet contents
				int iRow = 11;
				string sLetterIni = "A";
				string sLetterEnd = "I";

				string sMergeIni = "F";
				string sMergeEnd = "G";

				oSheet.Cells[3,7]= "Del:  " + ElaborationDateFrom + " AL:  " + ElaborationDateTo;
				double nCellHight = 0.0;
				
				
				while (dr.Read())
				{      
					oSheet.get_Range(sMergeIni + iRow.ToString(), sMergeEnd + iRow.ToString()).Cells.MergeCells = true;
					
					oSheet.Cells[iRow,1]= dr.GetOracleValue(4).ToString();
					oSheet.Cells[iRow,2]= dr.GetOracleValue(7).ToString().Substring(0,10) + "\n" + dr.GetOracleValue(6).ToString();
					oSheet.Cells[iRow,3]= dr.GetOracleValue(8).ToString() + "\n";
					
					oSheet.Cells[iRow,4]= dr.GetOracleValue(14).ToString() + "\n" +  dr.GetOracleValue(13).ToString();
					oSheet.Cells[iRow,5]= dr.GetOracleValue(11).ToString() + "\n" +  dr.GetOracleValue(10).ToString();

					oSheet.Cells[iRow,6]= dr.GetOracleValue(2).ToString();
					//string sTurnados = GetTurnados(int.Parse(dr.GetOracleValue(0).ToString()));
					string sDesaHogo = GetDesahogo(int.Parse(dr.GetOracleValue(0).ToString()));

					//oSheet.get_Range("H" + iRow.ToString(), "I" + iRow.ToString()).Cells.MergeCells = true;
					//oSheet.Cells[iRow,8]= sTurnados;
					//oSheet.Cells[iRow,9]= sDesaHogo;
					
					oSheet.Cells[iRow,8]= sDesaHogo;

					if ( dr.IsDBNull(16) )
						oSheet.Cells[iRow,9]="";
					else
						oSheet.Cells[iRow,9]= dr.GetOracleValue(16).ToString();

					oSheet.get_Range(sLetterIni + iRow.ToString(), sLetterEnd + iRow.ToString()).Font.Bold = true;
					oSheet.get_Range(sLetterIni + iRow.ToString(), sLetterEnd + iRow.ToString()).Font.Size = 8;

					oSheet.get_Range("F" + iRow.ToString(), sLetterEnd + iRow.ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
					oRng = oSheet.get_Range(sLetterIni + iRow.ToString(), "Z" + iRow.ToString());
				
					ArrayList arrCellHight = new ArrayList();
					if (dr.GetOracleValue(2).ToString().Length == 0)
						oRng.RowHeight = nCellHight;
					else 
					{
						arrCellHight.Add( GetCellHight(dr.GetOracleValue(2).ToString(), 50) );
						arrCellHight.Add( GetCellHight(sDesaHogo, 30) );
						arrCellHight.Sort(0, arrCellHight.Count, null);
						oRng.RowHeight = double.Parse(arrCellHight[arrCellHight.Count-1].ToString());
					}

					iRow++;
				}// end while

				dr.Close();
				dr = null;				
				//oSheet.Cells[11, 1] = "My Example Here";
				//Format A1:Z1 as bold, vertical alignment = center.
				//AutoFit columns A:Z.
				oRng = oSheet.get_Range("A11", "Z12");
				//oRng.EntireColumn.AutoFit();
				//oRng.RowHeight = 95;
				
				oXL.Visible = false;
				oXL.UserControl = false;
				string strFile = "report" + System.DateTime.Now.Ticks.ToString() + ".xls";
				oWB.SaveAs( _GeneratedFilesPath + sUserKey + "\\" + strFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,null,null,false,false,Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared,false,false,null,null,null);
				// Need all following code to clean up and extingush all references!!!
				oWB.Close(null,null,null);
				oXL.Workbooks.Close();
				oXL.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject (oRng);
				System.Runtime.InteropServices.Marshal.ReleaseComObject (oXL);
				System.Runtime.InteropServices.Marshal.ReleaseComObject (oSheet);
				System.Runtime.InteropServices.Marshal.ReleaseComObject (oWB);
				oSheet = null;
				oWB = null;
				oXL = null;
				GC.Collect();  // force final cleanup!
		
				string  sExportFile = _sURLFilesPath + sUserKey + "/" + strFile;
				//string  sExportFile = _GeneratedFilesPath + sUserKey + "\\" + strFile;
				//string sHRef = "<a href='' onclick=\"return(doOperation('ViewExcel','<@PATHFILE@>'));\">El archivo está listo</a>";
				//sHRef = sHRef.Replace("<@PATHFILE@>", sExportFile);
				return sExportFile;
			}
			catch( Exception ex ) 
			{
				String errorMessage;
				errorMessage = "Error: ";
				errorMessage = String.Concat( errorMessage, ex.Message );
				errorMessage = String.Concat( errorMessage, " Line: " );
				errorMessage = String.Concat( errorMessage, ex.Source );          
				//errLabel.Text= errorMessage ;
				//return errorMessage;
				throw (ex);
			}

		}

		private string GetTurnados(int nDocumentId)
		{
			string sTurnados = string.Empty;
			DataSet ds = Adressee.GetAdresseeByDocument(nDocumentId);
			foreach (DataRow r in ds.Tables[0].Rows)
			{
				sTurnados += r["destinatario"].ToString() + " [" + r["instruccion"].ToString() + "]" + "\n";
			}

			/*  DataSet ds1 = Adressee.GetAdresseeByDocument(nDocumentId, "Instruccion");

				foreach (DataRow r in ds1.Tables[0].Rows)
				{
					sTurnados += r["instruccion"].ToString() + "\n";
				}
			*/

			return sTurnados;
		}


		private string GetDesahogo(int nDocumentId)
		{
			string sDesahogo = string.Empty;
			DataSet ds = Adressee.GetAdresseeByDocumentDesahogo(nDocumentId);
			
			foreach (DataRow r in ds.Tables[0].Rows)
			{
				sDesahogo += r["destinatario"].ToString() + " [" + r["instruccion"].ToString() + "]" + "\n";
				if ( r["estatus"].ToString() == "3")
					sDesahogo += "  Desahogo: " + r["Observacion"].ToString() + "\n";
			}

			return sDesahogo;
		}


		private double GetCellHight(string sText, double nCharByCell)
		{
			double nCellHight = sText.Length / nCharByCell * cnsDefaultHight;
			if (nCellHight > 409)
				nCellHight = 400.0;

			return nCellHight;
		}

		private void RemoveFiles(string strPath)
		{			 
			System.IO.DirectoryInfo di = new DirectoryInfo(strPath);
			if (!di.Exists)
			{
				di.Create();
			}

			FileInfo[] fiArr = di.GetFiles();
			foreach (FileInfo fri in fiArr)
			{
               
				if(fri.Extension.ToString() ==".xls" || fri.Extension.ToString()==".csv")
				{
					TimeSpan min = new TimeSpan(0,0,60,0,0);
					if(fri.CreationTime < DateTime.Now.Subtract(min))
					{
						fri.Delete();
					}
				}
			}
           
		}

	}
}
