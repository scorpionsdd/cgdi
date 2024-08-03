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
	public class TipoTramite
	{
		public TipoTramite()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetRecords()
		{
			string sSql = "select tipo_tramite, tipo_tramite_id from sof_tipo_tramite ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}

		public static long Insert(string sItemCode, string sItemName, int nItemIsClassID, long nItemPosition, 
									long nItemParentID)
			
		{
 
			OracleParameter [] oParam = {	
											new OracleParameter("pItemID", null),
											new OracleParameter("pItemCode", OracleType.VarChar),
											new OracleParameter("pItemName", OracleType.VarChar),
											new OracleParameter("pItemIsClassID", null),
											new OracleParameter("pItemPosition", null),
											new OracleParameter("pItemParentID", null)
										};
			

			oParam[0].Value = 0;
			oParam[0].Direction = ParameterDirection.InputOutput;
			oParam[1].Value = sItemCode;
			oParam[2].Value = sItemName;
			oParam[3].Value = nItemIsClassID;
			oParam[4].Value = nItemPosition;
			oParam[5].Value = nItemParentID;

			
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["Desarrollo"], CommandType.StoredProcedure, 
						"Create_Code_Ing", oParam);
			

			object obj = oParam[0].Value;
			long nID = long.Parse(obj.ToString());
			return nID;

		}
	}
}
