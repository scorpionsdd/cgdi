using System;
using System.Data;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Areas
	{
		public Areas()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetAreasByDate(string sDate, string sFilter)
		{
			string sSql = "Select * From sof_areas " +
				"Where fecha_inicio <= '" + sDate + "' And fecha_fin >= '" + sDate + "' " +
				"Order By cve_area";

			if (sFilter != "")
				sSql = "Select sof_empleados.*, sof_areas.area, sof_areas.cve_area from sof_empleados, sof_areas Where sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate + "' " +
					" And sof_areas.id_area(+) = sof_empleados.id_area and sof_empleados.eliminado = '0' " +
					" And ( lower(area) LIKE '%" + sFilter.ToLower() + "%'" + " Or cve_area = '" + sFilter + "' ) Order By cve_area ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

        
		public static DataSet GetAreaTitular(string sAreaId, string sUserID)
		{

			string sTmp = String.Empty;
			string sSql = String.Empty;

			sTmp = sAreaId.TrimEnd('0') + "%";

			if (sTmp.Length-1 == 2 && sTmp.Substring(0,1) == "2")
				sTmp = sAreaId.Substring(0,3) + "%";

			sSql = "select id_usuario, apellidonombre nombre from hint_v_usuarios where clave_area like '" + sTmp + "' and tabulador > '12C' order by apellidonombre";

			if (participants.getSenderInclude(sUserID) == "Si")
			{
				if ( (sTmp.Substring(0,1) == "2" && sTmp.Length-1 == 3 ) && (sTmp.Substring(1,2) == "10" || sTmp.Substring(1,2) == "20" || sTmp.Substring(1,2) == "30") )
					sTmp = sTmp.Substring(0,2) + "0%";
				else
					sTmp = sTmp.Substring(0,2) + "%";

				sSql = "select id_usuario, apellidonombre nombre from hint_v_usuarios where clave_area like '" + sTmp + "' order by apellidonombre";
			}

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}


		public static DataSet GetArea()
		{
			string sSql = "SELECT id_area, substr(area,1,90) area FROM sof_areas WHERE (FECHA_INICIO <= sysdate) AND  (FECHA_FIN >= sysdate) ORDER BY area ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

        public static DataSet GetArea_()
        {
            string sSql = "SELECT Cve_area, substr(area,1,90) area FROM sof_mapeo_recibidos a,sof_areas b,sof_empleados c WHERE   a.id_empleado= c.id_empleado  AND  a.id_area= c.id_area  AND  b.Cve_Area LIKE '1%' AND (FECHA_INICIO <= sysdate) AND  (FECHA_FIN >= sysdate) ORDER BY area ";
            DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            return ds;
        }
        public static OracleDataReader GetCboArea()
		{
			string sSql = "SELECT id_area, substr(area,1,90) area FROM sof_areas WHERE (FECHA_INICIO <= sysdate) AND (FECHA_FIN >= sysdate) order by area ";
			OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return dr;
		}

		public static DataSet GetAreaHistorical()
		{
			string sSql = "SELECT id_area, cve_area || ' - ' || substr(area,1,90) area FROM sof_areas ORDER BY area ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetArea(int nUser)
		{
			int sAreaId = GetAreaId(nUser);
			string sSql = "Select area_id, area  FROM sof_areas WHERE ( id_area = sAreaId )";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static DataSet GetAreaById(int areaId)
		{
			string sSql = "select *  from sof_areas Where id_area = " + areaId;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static DataSet GetAreaByUser(int nUser)
		{
			int sAreaId = GetAreaParameter(nUser);
			string sSqlArea = "(select distinct clave_area, area from hint_v_usuarios where area is not null) fromArea";
			string sSql = "select fromArea.clave_area areaId, fromArea.area area  from " + sSqlArea;
			sSql += " WHERE fromArea.id_area = " + sAreaId;
			sSql += " Order By fromArea.Area";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static string GetPuesto(int nUserId)
		{
			string sSql = "select puesto from hint_v_usuarios where id_usuario = " + nUserId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}


		public static int GetAreaId(int nUserId)
		{
			string sSql = "select id_area from hint_v_usuarios where id_usuario = " + nUserId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return int.Parse( oObj.ToString() );
		}

		public static int GetAreaJefeId(int nUserId)
		{
			string sSql = "select id_area from sof_empleados where id_empleado = " + nUserId + " and tabulador > '12C' ";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return int.Parse( oObj.ToString() );
			else
				return 0;
		}


		public static int GetAreaParameter(int nUserId)
		{
			string sSql = "SELECT destinatario_area_id FROM sof_regla WHERE id_empleado = " + nUserId + " AND eliminado = '0' ";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return int.Parse( oObj.ToString() );
			else
				return 0;

		}
	
		public static string GetTitularParameter(int nUserId)
		{
			string sSql = "SELECT destinatario_titular_id FROM sof_regla WHERE id_empleado = " + nUserId + " AND eliminado = '0'";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";
		}

		
		public static string GetUsersByArea(int nUserId)
		{
			StringBuilder sUsers = new StringBuilder();
			int sAreaId = GetResponsible(nUserId);
			if (sAreaId == 0)
				sAreaId = GetAreaId(nUserId);

			string sSql = "select id_usuario from hint_v_usuarios where hint_v_usuarios.id_area = " + sAreaId + " AND hint_v_usuarios.tabulador > '12C' " ;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows){
					sUsers.Append(dr["id_usuario"].ToString()+ ",");
				}
				return sUsers.ToString().Remove(sUsers.ToString().Length-1,1);
			}
			else
				return "0";

		}
		
		public static string GetUserRegistry(int nUserId)
		{
			StringBuilder sUsers = new StringBuilder();
			int sAreaId = GetAreaParameter(nUserId);
			string sSql = "select id_empleado, destinatario_area_id from sof_regla where sof_regla.destinatario_area_id = '" + sAreaId + "'" ;

			string sUserType = Users.GetUserType(nUserId);

			if (sAreaId == 0)
			{
				sAreaId = GetAreaJefeId(nUserId);
				if (sAreaId != 0)
					sSql = "select id_usuario id_empleado, clave_area from  hint_v_usuarios where hint_v_usuarios.id_usuario = " + nUserId;
			} else {
				if (sUserType == "T")
					sSql = "select id_usuario,clave_area id_empleado from  hint_v_usuarios where hint_v_usuarios.id_usuario = " + nUserId;
				if (sUserType == "M")
					sSql = "select id_empleado, area_id from sof_permiso where sof_permiso.usuario_origen_id = " + nUserId;
			}

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
					if (sUserType == "S")
						sUsers.Append(dr[0].ToString()+ ",").Append( "%,");
					else
						sUsers.Append(dr[0].ToString()+ ",").Append(dr[1].ToString()+ ",");
				}
				return sUsers.ToString().TrimEnd(',');
			}
			else
				return nUserId.ToString() + "," + sAreaId;

		}


		public static string GetUserAlternate(int nUserId, int nLevel)
		{
			StringBuilder sUsers = new StringBuilder();
			string sSql;
			int sAreaId = GetAreaParameter(nUserId);
			string userType = Users.GetUserType(nUserId);

			if (sAreaId == 0)
			{
				return nUserId.ToString() + "," + GetAreaId(nUserId);
			}
			else
			{
				sSql = "select sof_regla.id_destinatario_titular, sof_regla.id_destinatario_area From sof_regla, sof_mapeo_regla where sof_regla.id_empleado = " +  nUserId + " sof_mapeo_regla.id_empleado = sof_regla.id_empleado";
			}
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
					sUsers.Append(dr[0].ToString()+ ",").Append(dr[1].ToString()+ ",");
				}
				return sUsers.ToString().Remove(sUsers.ToString().Length-1,1);
			}
			else
				return "0";

		}


		public static int GetAreaLevel(int nUserId)
		{
			int sAreaId = GetAreaParameter(nUserId);

			if (sAreaId == 0)
				sAreaId = GetAreaJefeId(nUserId);

			//return GetAreaAllTrimRight(sAreaId).Length;
			return 0;
			
		}

		public static string GetClaveArea(int areaId)
		{
			string sSql = "select CVE_AREA from sof_areas where ID_AREA = " + areaId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static string GetClavePuesto(int puestoId)
		{
			string sSql = "select CLAVE_PUESTO from sof_puesto where PUESTO_ID = " + puestoId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		private static int GetResponsible(int nUserId)
		{
			string sSql = "select destinatario_area_id from sof_regla where id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql) ;
			if (oObj != null)
				return int.Parse(oObj.ToString() );
			else
				return 0;
		}


		public static int Create(string claveArea, string area, string startDate, string endDate)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pClaveArea", OracleType.VarChar),
											new OracleParameter("pArea", OracleType.VarChar),
											new OracleParameter("pStartDate", OracleType.VarChar),
											new OracleParameter("pEndDate", OracleType.VarChar),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = claveArea;
			oParam[1].Value = area;
			oParam[2].Value = startDate;
			oParam[3].Value = endDate;
			oParam[4].Direction = ParameterDirection.InputOutput;
			oParam[4].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_areas_create",oParam);

			object obj = oParam[4].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int areaId, string claveArea, string area, string endDate)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pAreaId", null),
											new OracleParameter("pClaveArea", OracleType.VarChar),
											new OracleParameter("pArea", OracleType.VarChar),
											new OracleParameter("pStartDate", OracleType.VarChar)
										};
			
			oParam[0].Value = areaId;
			oParam[1].Value = claveArea;
			oParam[2].Value = area;
			oParam[3].Value = endDate;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_areas_update",oParam);

		}

		public static void Remove(int areaId, string endDate)
		{

			OracleParameter [] oParam = {new OracleParameter("pAreaId", null),
										 new OracleParameter("pEndDate",OracleType.VarChar)
										};
			
			oParam[0].Value = areaId;
			oParam[1].Value = endDate;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_areas_remove",oParam);

		}


	}
}
