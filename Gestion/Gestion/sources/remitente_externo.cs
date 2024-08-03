using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for document_type.
	/// </summary>
	public class remitente_externo
	{
		public remitente_externo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataSet GetRecords(int nUser)
		{
			string sSql = "select * from sof_remitente_externo where id_empleado = " +nUser + " and nvl(eliminado,'0') = '0' order by dependencia ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetDataSet(string sWhere)
		{

			StringBuilder sSql = new StringBuilder();
			sSql.Append("select * from sof_remitente_externo");
			
			if (sWhere != "")
				sSql.Append(" where " + sWhere);
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql.ToString());
			return ds;
		}

		public static DataSet GetDataSet(string sWhere, string sOrder)
		{

			StringBuilder sSql = new StringBuilder();
			sSql.Append("select * from sof_remitente_externo");
			
			if (sWhere != "")
				sSql.Append(" where " + sWhere);
			if (sOrder != "")
				sSql.Append(" order by " + sOrder);
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql.ToString());
			return ds;
		}

		public static string GetName(int nRemitenteId)
		{

			string sWhere = " remitente_externo_id = " + nRemitenteId;
			DataSet ds = GetDataSet(sWhere);
			DataView dv = ds.Tables[0].DefaultView;
		
			return dv[0]["nombre"].ToString();
		}

		public static DataSet GetRemitente(int nDocumentId)
		{

			string sSql = "select sof_remitente_externo.* from sof_remitente_externo, sof_documento ";
			string sWhere = "where sof_documento.documento_id = " + nDocumentId + " " +
							"and sof_remitente_externo.remitente_externo_id = sof_documento.remitente_id";
			sSql += sWhere;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			return ds;

		}
		public static void CreateRemitente(string sName, string sDep)
		{

			OracleParameter [] oParam = new OracleParameter[2];
			OracleCommand cmd = new OracleCommand();

			oParam[0] = cmd.Parameters.Add("pName",OracleType.VarChar, 100);
			oParam[0].Direction = ParameterDirection.Input;
			oParam[0].Value = sName;

			oParam[1] = cmd.Parameters.Add("pDep",OracleType.VarChar, 100);
			oParam[1].Direction = ParameterDirection.Input;
			oParam[1].Value = sDep;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure,"Gestion.CreateRemitente", oParam));

			cmd.Parameters.Clear();
			cmd.Dispose();
		}


	}
}
