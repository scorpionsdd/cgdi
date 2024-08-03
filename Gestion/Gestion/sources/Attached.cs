using System;
using System.Data;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using System.Configuration;
using BComponents.DataAccessLayer;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Net;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Attached.
	/// </summary>
	public class Attached
	{
		private const string cnsItem0 = "<TR><TD colSpan='4' class='header-gray' align='middle'> <a href='' onclick=\"return(viewAttach('<@PATHFILE@>'));\"><@FILE@></a></TD></TR>";

		public Attached()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static DataSet GetAttach(int nDocumentId, string sMailType)
		{
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, "select sof_anexo.*, sof_documento.remitente_id from sof_anexo, sof_documento where sof_anexo.documento_id = " + nDocumentId + " and sof_documento.documento_id = sof_anexo.documento_id and sof_anexo.tipo_correo = '" + sMailType + "'" );
			return ds;

		}

		public static string GetFileAttach(int nDocumentId, string sMailType)
		{
			string sTmp = String.Empty;
			string sDocumentId = nDocumentId.ToString();


			while (sDocumentId != "0")
			{
				sTmp += GetTreeAttach(sDocumentId, sMailType);
				sDocumentId = Documents.GetDocumentBis(int.Parse(sDocumentId));
			}
          


				return sTmp;

				
        }
		
		
		private static string GetTreeAttach(string sDocumentId, string sMailType )
		{
			string sSql =	"SELECT sof_documento.documento_id, sof_documento.documento_bis_id, sof_documento.id_empleado, " + 
							"sof_anexo.archivo, sof_empleados.clave_empleado " +
							"FROM sof_documento, sof_anexo, sof_empleados where sof_documento.documento_id = " + sDocumentId + 
							" AND sof_anexo.documento_id = sof_documento.documento_id " +
							" AND sof_anexo.tipo_correo = '" + sMailType + "'" +
							" And sof_empleados.id_empleado = sof_documento.id_empleado " ;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return CreateAttachHtml(ds);

		}
		private static string CreateAttachHtml(DataSet ds)
		{
			string sHtml = String.Empty;
			//string sUrl = @"http://172.27.201.86/gestion/user_files/";
			string sUrl = AppSettingsReader.GetValue("URLFilesPath");

			if (ds.Tables[0].Rows.Count > 0)
			{
				string sTemp = string.Empty;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					
                    bool SiExiste = File.Exists($"C:\\inetpub\\wwwroot\\Gestion\\user_files\\{dr["clave_empleado"]}\\{dr["archivo"]}");
					

                        sTemp = cnsItem0;
					if (SiExiste)
					{
                        
                        sTemp = sTemp.Replace("<@PATHFILE@>", sUrl + dr["clave_empleado"].ToString() + "/" + dr["archivo"].ToString());
						sTemp = sTemp.Replace("<@FILE@>", dr["archivo"].ToString());
						sHtml += sTemp;
					}
					//else
					//{
					//	sHtml = cnsItem0;
					//	sHtml = sHtml.Replace("<@PATHFILE@>", sUrl + "error404.aspx");
					//	//sHtml = sHtml.Replace("<@FILE@>", dr["archivo"].ToString());

     //               }
				}
			}
			return sHtml + "\n";
		}

		public static string getReference(int nDocumentId)
		{
			string sSql = "select referencia from sof_documento where documento_id = " + nDocumentId;
			Object oReference = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
				
			return oReference.ToString();
		}

		public static DataSet GetFileAnexo(int nAnexoId, string sMailType)
		{
			string sSql =	"SELECT sof_anexo.archivo, sof_documento.id_empleado " +
							"FROM sof_anexo, sof_documento " +
							"WHERE sof_anexo.anexo_id = " + nAnexoId + " " +
							"AND sof_anexo.tipo_correo = '" + sMailType + "'" + " " +
							"AND sof_documento.documento_id = sof_anexo.documento_id ";
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}
	

		public static int AttachedInsert(int nDocumentId, string sFile, string sPath, string sMailType )
		{
			OracleParameter[] parms = new OracleParameter[4];


			parms[0] = new OracleParameter("pDocumentId", null);
			parms[0].Value = nDocumentId;

			parms[1] = new OracleParameter("pFile", OracleType.VarChar, 80);
			parms[1].Value = sFile;

			parms[2] = new OracleParameter("pPath", OracleType.VarChar, 80);
			parms[2].Value = sPath;


			parms[3] = new OracleParameter("pMailType", OracleType.Char, 1);
			parms[3].Value = sMailType;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.AttachedInsert", parms));

			return nVal;
		}

  		public static void AttachedDelete(int nAttachId)
		{
			OracleParameter [] oParam = new OracleParameter[1];

			oParam[0] = new OracleParameter ("pAttachId", null);
			oParam[0].Value = nAttachId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.AttachedDelete", oParam));

		}

		public static string GetResponseTurnar(int nDocumentTurnarId)
		{
			string sSql = "SELECT observacion FROM sof_estatus_turnar WHERE documento_turnar_id = " + nDocumentTurnarId + " and estatus = '3'";
			Object oResponse = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return (oResponse != null ? oResponse.ToString() : "" );
		}

		public static bool HasResponseComplete(string sDocumentId)
		{
			string sSqlTotal = "select count(*) from sof_documento_turnar where eliminado = 0 and documento_id = " + sDocumentId;
			string sSqlResponse = "select count(*) from sof_documento_turnar where documento_id = " + sDocumentId + " AND estatus = '3' and eliminado = 0 ";
			
			int nTotalItems = int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSqlTotal).ToString());
			int nTotalRes = int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSqlResponse).ToString());
			
			return (nTotalItems != nTotalRes);
		}

	}
}
