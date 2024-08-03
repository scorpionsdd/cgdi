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
	public class ExternalSender
	{
		public ExternalSender()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetRecords(int nUserId)
		{
			string sTemp = getSenderByArea(nUserId);
			if (sTemp == "0")
				sTemp = nUserId.ToString();

			string sSql = "select * from sof_remitente_externo where nvl(eliminado,'0') = '0' and id_empleado = " + sTemp + " order by dependencia";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
							
			return ds;
		}

		public static OracleDataReader GetRecordsDr(int nUserId)
		{
			string sTemp = getSenderByArea(nUserId);
			if (sTemp == "0")
				sTemp = nUserId.ToString();

			string sSql = "Select remitente_externo_id, dependencia From sof_remitente_externo Where nvl(eliminado,'0') = '0' and id_empleado = " + sTemp + " order by dependencia";
			OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return dr;
		}


		public static int DeleteRecords(int nRecordId)
		{
			string sSql = "update sof_remitente_externo set eliminado = '1' where remitente_externo_id = " + nRecordId;
			string sSqlDel = "select count(*) from sof_documento where REMITENTE_AREA_ID = " + nRecordId;
			int nVal = 0;

			object oCount = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSqlDel);
			if (int.Parse(oCount.ToString()) == 0)
			{
				nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			}
			return nVal;
		}
		public static int UpdateRecords(int nRecordId, string sDependencia)
		{
			string sSql = "update sof_remitente_externo set dependencia = '" + sDependencia + "' where remitente_externo_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text,sSql);
			return nVal;
		}

		public static int CreateRecords(string sDependencia, int nUserId)
		{
			string sSql = "remitente_externo_create";
			OracleParameter [] sqlParams = {new OracleParameter("pDependencia",OracleType.VarChar,100),
											new OracleParameter("pUserId", null),
											new OracleParameter("pResult", null)
										   };
			
			string sTemp = getSenderByArea(nUserId);
			if (sTemp == "0")
				sTemp = nUserId.ToString();

			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = sDependencia;
			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = int.Parse(sTemp);
			sqlParams[2].Direction = ParameterDirection.InputOutput;
			sqlParams[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.StoredProcedure, sSql, sqlParams);
					
			object oVal = sqlParams[2].Value;
			return Convert.ToInt32(oVal.ToString());
		}

		public static int CreateTitular(int nSender, string sTitularName)
		{
			string sSql = "remitente_externo_titular_create";
			OracleParameter [] sqlParams = {new OracleParameter("pSender", null),
											new OracleParameter("pTitularName", null),
											new OracleParameter("pResult", null)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nSender;
			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sTitularName;
			sqlParams[2].Direction = ParameterDirection.InputOutput;
			sqlParams[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.StoredProcedure, sSql, sqlParams);
					
			object oVal = sqlParams[2].Value;
			return Convert.ToInt32(oVal.ToString());
		}


		private static string getSenderByArea(int nUserId)
		{
			string sSql = "select nvl(id_remitente_externo,0) from sof_regla where id_empleado = " +  nUserId;
			Object oSender = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oSender != null)
				return oSender.ToString();
			else
				return "0";
		}

		public static string GetDependencia(int nRemitenteId)
		{
			string sWhere = "where sof_remitente_externo.remitente_externo_id = " + nRemitenteId + " ";
			string sSql = "select sof_remitente_externo.dependencia from sof_remitente_externo ";
			sSql += sWhere;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "0";
		}

		public static string getUseCatalog(string sUserID)
		{
			string sSql = "select remitente_ext_catalogo from sof_regla where id_empleado = " + sUserID + " and eliminado=0";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "No";
		}

		public static int getAreaID(string sDependencia, int nUser)
		{
			string sSql = "select max(sof_remitente_externo.remitente_externo_id) from sof_remitente_externo where dependencia = '" + sDependencia + "'" + " and id_empleado = " + nUser;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if ( oObj.ToString() != "") 
				return int.Parse(oObj.ToString());
			else
				return CreateRecords(sDependencia, nUser );
		}

		public static string getName(string sSenderID)
		{
			string sSql = "select sof_remitente_externo.dependencia from sof_remitente_externo where remitente_externo_id = " + sSenderID;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "No existe el Remitente numero " + sSenderID;
		}
		
	}
}
