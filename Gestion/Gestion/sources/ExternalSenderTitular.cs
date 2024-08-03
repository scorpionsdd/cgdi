using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class ExternalSenderTitular
	{
		public ExternalSenderTitular()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetRecords(int nRecordId)
		{
			string sWhere = "where sof_remitente_externo_titular.remitente_externo_id = " + nRecordId;
			sWhere +=	"and sof_remitente_externo.remitente_externo_id =  sof_remitente_externo_titular.remitente_externo_id " +
						"and sof_remitente_externo_titular.eliminado = '0'";
			string sSql =	"select " +
							"sof_remitente_externo.dependencia, " +
							"sof_remitente_externo_titular.remitente_externo_titular_id, " +
							"sof_remitente_externo_titular.nombre, " +
							"sof_remitente_externo_titular.puesto, " +
							"sof_remitente_externo_titular.titulo " +
							"from sof_remitente_externo, sof_remitente_externo_titular ";
			sSql += sWhere + " order by sof_remitente_externo_titular.nombre ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
							CommandType.Text, sSql);
			return ds;
		}

		public static int DeleteRecords(int nRecordId)
		{
			string sSql = "update sof_remitente_externo_titular set eliminado = '1' where remitente_externo_titular_id = " + nRecordId;
			string sSqlDel = "select count(*) from sof_documento where remitente_area_id = " + nRecordId + " and tipo_remitente = 'E'";
			int nVal = 0;
			
			object oCount = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text,sSqlDel);

			if (int.Parse(oCount.ToString()) == 0)
			{
				nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
					CommandType.Text,sSql);
			}
			return nVal;
		}

		public static int UpdateRecords(int nRecordId, string sName, string sPuesto, string sTitulo)
		{
			string sSql = "update sof_remitente_externo_titular set nombre = '" + sName + "' , puesto = '" + sPuesto + "', titulo = '" + sTitulo + "' where remitente_externo_titular_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text,sSql);
			return nVal;
		}

		public static int CreateRecords(int nRemitenteId, string sNombre, string sPuesto, string sTitulo)
		{
			string sSql = "remitente_titular_create";

			OracleParameter [] sqlParams = {new OracleParameter("pRemitenteId", null),
											new OracleParameter("pNombre",OracleType.VarChar, 100),
											new OracleParameter("pPuesto",OracleType.VarChar, 100),
											new OracleParameter("pTitulo",OracleType.VarChar, 20),
											new OracleParameter("pResult", null)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nRemitenteId;
			
			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sNombre;
			
			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sPuesto;
			
			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sTitulo;
			
			sqlParams[4].Direction = ParameterDirection.InputOutput;
			sqlParams[4].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.StoredProcedure, sSql, sqlParams);
					
			object oVal = sqlParams[4].Value;
			return Convert.ToInt32(oVal.ToString());
		}


		public static string GetDependenciaId(int nTitularId)
		{
			string sWhere = "where sof_remitente_externo_titular.remitente_externo_titular_id = " + nTitularId + " " +
				" and sof_remitente_externo.remitente_externo_id = sof_remitente_externo_titular.remitente_externo_id ";
			string sSql = "select sof_remitente_externo.remitente_externo_id from sof_remitente_externo, sof_remitente_externo_titular ";
			sSql += sWhere;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "0";
		}

		public static string GetPuesto(int nTitularId)
		{
			string sSql		= "select sof_remitente_externo_titular.puesto from sof_remitente_externo_titular ";
			string sWhere	= "where sof_remitente_externo_titular.remitente_externo_titular_id = " + nTitularId;
			sSql += sWhere;
			
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
				
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "Sin Titular";
		}

		public static int getTitularID(int nSenderID, string sTitular)
		{
			string sSql = "select sof_remitente_externo_titular.remitente_externo_titular_id from sof_remitente_externo_titular where nombre = '" + sTitular + "'" + " and REMITENTE_EXTERNO_ID = " + nSenderID;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return int.Parse(oObj.ToString());
			else
				return CreateRecords(nSenderID, sTitular, ".", ".");
		}

		public static string getName(string sTitularID)
		{
			string sSql = "select sof_remitente_externo_titular.nombre from sof_remitente_externo_titular where remitente_externo_titular_id = " + sTitularID;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "No existe el Remitente numero " + sTitularID;
		}


	}
}
