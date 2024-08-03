using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Tipo de solicitud.
	/// </summary>
	public class TipoSolicitud
	{
		public TipoSolicitud()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetTipoSolicitud(string pVigente)
		{
			string sSql = "";
			if (pVigente == "V")
			{
				sSql =	"SELECT ID_TIPO_SOLICITUD, TIPO_SOLICITUD FROM SOF_TIPO_SOLICITUD " +
						"WHERE ELIMINADO = '0' ORDER BY TIPO_SOLICITUD";
			}
			else
			{
				sSql =	"SELECT ID_TIPO_SOLICITUD, TIPO_SOLICITUD FROM SOF_TIPO_SOLICITUD ORDER BY TIPO_SOLICITUD ";
			}

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetTipoSolicitudByID(int pIdTipoSolicitud )
		{
			string sSql = "SELECT ID_TIPO_SOLICITUD, TIPO_SOLICITUD FROM SOF_TIPO_SOLICITUD " +
						  "WHERE ID_TIPO_SOLICITUD = " + pIdTipoSolicitud;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static int Create(string pTipoSolicitud)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pTipoSolicitud", OracleType.VarChar),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = pTipoSolicitud;
			oParam[1].Direction = ParameterDirection.InputOutput;
			oParam[1].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_solicitud_create",oParam);

			object obj = oParam[1].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int pIdTipoSolicitud, string pTipoSolicitud)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pIdTipoSolicitud", null),
											new OracleParameter("pTipoSolicitud", OracleType.VarChar)
										};
			
			oParam[0].Value = pIdTipoSolicitud;
			oParam[1].Value = pTipoSolicitud;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_solicitud_update",oParam);

		}

		public static void Remove(int pIdTipoSolicitud)
		{

			OracleParameter [] oParam = {new OracleParameter("pIdTipoSolicitud", null)
										};
			
			oParam[0].Value = pIdTipoSolicitud;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_tipo_solicitud_remove",oParam);

		}

	}
}
