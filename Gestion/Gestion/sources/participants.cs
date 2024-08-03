using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for participants.
	/// </summary>
	public class participants
	{
		public participants()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static DataSet GetUser()
		{
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, "select * from int_v_usuarios order by nombre");
			return ds;
		}

		public static string getSenderInclude(string sUserId)
		{
			string sSql = "Select sof_regla.rem_incluye_oper From sof_regla Where id_empleado = " +  sUserId;
			Object oSender = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oSender != null && oSender.ToString() != "")
				return oSender.ToString();
			else
				return "No";
		}

		public static string getTurnarArbol(string sUserId)
		{
			string sSql = "select sof_regla.tur_arbol from sof_regla where id_empleado = " +  sUserId;
			Object oSender = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oSender != null && oSender.ToString() != "")
				return oSender.ToString();
			else
				return "No";
		}

		public static string GetConcluirAcuseCcpara(string sUserId)
		{
			string sSql = "select sof_regla.concluir_acuse_ccpara from sof_regla where id_empleado = " +  sUserId;
			Object oSender = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oSender != null)
				return  oSender.ToString() == "" ? "No" : oSender.ToString();
			else
				return "No";
		}

	}
}
