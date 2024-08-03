using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Tipo de atención.
	/// </summary>
	public class TipoAtencion
	{
		public TipoAtencion()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetTipoAtencion(string pVigente)
		{
			string sSql = "";
			if (pVigente == "V")
			{
				sSql =	"SELECT ID_TIPO_ATENCION, TIPO_ATENCION FROM SOF_TIPO_ATENCION " +
						"WHERE ELIMINADO = '0' ORDER BY TIPO_ATENCION";
			}
			else
			{
				sSql =	"SELECT ID_TIPO_ATENCION, TIPO_ATENCION FROM SOF_TIPO_ATENCION ORDER BY TIPO_ATENCION ";
			}

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetTipoAtencionByID(int pIdTipoAtencion )
		{
			string sSql = "SELECT ID_TIPO_ATENCION, TIPO_ATENCION FROM SOF_TIPO_ATENCION " +
						  "WHERE ID_TIPO_ATENCION = " + pIdTipoAtencion;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static int Create(string pTipoAtencion)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pTipoAtencion", OracleType.VarChar),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = pTipoAtencion;
			oParam[1].Direction = ParameterDirection.InputOutput;
			oParam[1].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_atencion_create",oParam);

			object obj = oParam[1].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int pIdTipoAtencion, string pTipoAtencion)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pIdTipoAtencion", null),
											new OracleParameter("pTipoAtencion", OracleType.VarChar)
										};
			
			oParam[0].Value = pIdTipoAtencion;
			oParam[1].Value = pTipoAtencion;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_atencion_update",oParam);

		}

		public static void Remove(int pIdTipoAtencion)
		{

			OracleParameter [] oParam = {new OracleParameter("pIdTipoAtencion", null)
										};
			
			oParam[0].Value = pIdTipoAtencion;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_atencion_remove",oParam);

		}

	}
}
