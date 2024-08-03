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
	public class firmaUsuario
	{
		public firmaUsuario()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		private static string GetArea(int nUserId)
		{
			string sSql = "select sof_areas.cve_area from sof_regla, sof_areas, sof_empleados Where sof_regla.id_empleado = " + nUserId +
				" And sof_regla.id_firma = sof_empleados.id_empleado and sof_empleados.id_area = sof_areas.id_area and sof_regla.eliminado=0 ";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return oObj.ToString().Substring(0,2);
		}

		public static int DeleteRecords(int nRecordId)
		{
			string sSql = "update sof_firma set eliminado = '1' where firma_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return nVal;
		}

		public static int UpdateRecords(int nRecordId, int nFirmaUsuarioId)
		{
			string sSql = "update sof_firma set firma_id_empleado = '" + nFirmaUsuarioId + " where firma_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text,sSql);
			return nVal; 
		}

		public static int CreateRecords(int nFirmaUsuarioId, int nUserId)
		{
			string sSql = "insert into sof_firma (firma_id, firma_id_empleado, id_empleado, eliminado) values (s_firma.nextval, " + nFirmaUsuarioId + "," + nUserId + ",'0')";
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return nVal;
		}

		public static string getFirma(int nUserId)
		{
			string sSql	= "select sof_empleados.nombre from sof_empleados where sof_empleados.id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static DataSet getFirmaArea(int nUserId, string sDate)
		{

			string sArea = "'" + GetArea(nUserId) + "%'";
			string sSql	 = "Select apellidonombre nombre, id_empleado From sof_empleados, sof_areas " + 
							"Where sof_empleados.id_area = sof_areas.id_area And sof_areas.cve_area like " + sArea + " And sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate + "' and sof_empleados.eliminado = '0'  order by sof_empleados.apellidoNombre";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet getRegla(int nUserId)
		{
			string sSql	 = "select  sof_regla.*,  sof_destinatario_area.cve_area destClaveArea, sof_remitente_area.cve_area RemClaveArea, " +
						   "sof_destinatario.nombre DestNombre, sof_Remitente.nombre RemNombre " +
						   "From sof_regla, sof_areas sof_destinatario_area, sof_areas sof_remitente_area, " +
						   "sof_empleados sof_destinatario, sof_empleados sof_remitente " +
						   "Where sof_regla.id_empleado = " + nUserId + " and sof_regla.eliminado = '0' " +
						   "And sof_destinatario_area.id_area = sof_regla.id_destinatario_area " +
						   "And sof_remitente_area.id_area = sof_regla.id_remitente_area " +
						   "And sof_destinatario.id_empleado = sof_regla.id_destinatario_titular " +
						   "And sof_remitente.id_empleado = sof_regla.id_remitente_titular ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static string getReglaFolio(int nUserId)
		{
			string sTmp = "0";
			string sSql	 = "select id_folio from sof_regla where id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (oObj != null)
				sTmp = oObj.ToString();
			return sTmp;
		}

		public static string isBoss(int nUserId)
		{
			string sTmp = "0";
			string sSql	 = "select clave_area from sof_empleados where id_empleado = " + nUserId + " and categoria > '12C'";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (oObj != null)
				sTmp = oObj.ToString();
			return sTmp;
		}

		
		public static string getRuleStatusChange(int nUserId)
		{
			string sSql	 = "select max(cambia_estatus) cambia_estatus from sof_regla where id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (oObj != null)
			{
				if ( (oObj.ToString() ==  "") || (oObj.ToString() ==  "No"))
					return "No";

				else
					return "Si";

			}
			return "No";
		}

		public static string getRuleSeguimiento(int nUserId)
		{
			string sSql	 = "select seguimiento from sof_regla where id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "No";
		}
		
	}
}
