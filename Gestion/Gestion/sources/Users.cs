using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;
using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Extension;
using Log.Layer.Business;
using Log.Layer.Model.Model;
using System.Linq;
using System.Security.Cryptography;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Users
	{
		public Users()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetUsers()
		{
			string sSql = "select * from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area " +
				"Order by sof_areas.Cve_area, sof_empleados.Categoria";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetUsers(string sWhere)
		{
			string sSql = "select sof_empleados.*, sof_areas.area, sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where " + sWhere;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetUsersCurrent(string sDate)
		{
			string sSql = "Select sof_empleados.id_empleado, sof_empleados.nombre, sof_empleados.apellidoNombre, sof_empleados.clave_empleado, "+
				"sof_empleados.tipo_usuario, sof_empleados.categoria, sof_empleados.tipo_usuario, sof_empleados.id_area," +
				"sof_empleados.id_puesto, sof_empleados.fecha_inicio, sof_empleados.fecha_fin, sof_areas.area, sof_areas.cve_area " +
				"From sof_empleados, sof_areas Where sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate + "'" +
				" And sof_areas.id_area(+) = sof_empleados.id_area and sof_empleados.eliminado = '0' order by sof_empleados.nombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetUsersByDate(string sDate, string sFilter,string uid,string ip, string sessionId, string expedient)
		{
			string sSql = "Select sof_empleados.id_empleado, sof_empleados.nombre, sof_empleados.apellidoNombre, sof_empleados.clave_empleado, "+
				"sof_empleados.tipo_usuario, sof_empleados.categoria, sof_empleados.tipo_usuario, sof_empleados.id_area," +
				"sof_empleados.id_puesto, sof_empleados.fecha_inicio, sof_empleados.fecha_fin, sof_areas.area, sof_areas.cve_area " +
				"From sof_empleados, sof_areas Where sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate + "'" +
				" And sof_areas.id_area(+) = sof_empleados.id_area and sof_empleados.eliminado = '0' order by sof_areas.cve_area, sof_empleados.nombre";

			if (sFilter != "")
				sSql = "Select sof_empleados.*, sof_areas.area, sof_areas.cve_area from sof_empleados, sof_areas Where sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate + "'" +
						   " And sof_areas.id_area(+) = sof_empleados.id_area and sof_empleados.eliminado = '0' And ( lower(sof_empleados.nombre) LIKE '%" + sFilter.ToLower() + "%'" + " Or sof_empleados.clave_empleado = '" + sFilter + "' ) order by sof_areas.cve_area, sof_empleados.nombre";            

            DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Empleados / Lista Empleados", enuAction.Retrieve.GetDescription(), "sof_empleados, sof_areas", sSql, ip, sessionId, expedient));

            return ds;
		}

		public static DataSet GetUsersById(int userId, string uid, string ip, string sessionId, string expedient)
		{
			string sSql = "Select sof_empleados.id_empleado, sof_empleados.nombre, sof_empleados.apellidoNombre, sof_empleados.clave_empleado, "+
				"sof_empleados.tipo_usuario, sof_empleados.categoria, sof_empleados.tipo_usuario, sof_empleados.id_area," +
				"sof_empleados.id_puesto, sof_empleados.login, sof_empleados.password, sof_empleados.fecha_inicio, sof_empleados.fecha_fin, sof_areas.area, sof_areas.cve_area, sof_puesto.clave_puesto, sof_puesto.descripcion puesto From sof_empleados, sof_areas, sof_puesto Where sof_empleados.id_empleado = " + userId + " " +
				"And sof_areas.id_area(+) = sof_empleados.id_area And sof_puesto.puesto_id(+) = sof_empleados.id_puesto";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Empleados / Edicion Empleado", enuAction.Retrieve.GetDescription(), "sof_empleados, sof_areas, sof_puesto", sSql, ip, sessionId, expedient));

            return ds;
		}



		public static DataSet GetAreaTitular(string sAreaId, string sUserId, string sRemDes, string sDate)
		{
			string sTmp = String.Empty;
			string sSql = String.Empty;
			string sCategoria = "12C";
			if (sAreaId == "") 
			{
				if (sRemDes == "R")
					sTmp = Users.GetAreaKeyByRemitenteId(int.Parse(sUserId) ).TrimEnd('0');
				else
					sTmp = Users.GetAreaKeyByDestinatarioId(int.Parse(sUserId) ).TrimEnd('0');
			}
			else
			{
				sTmp = GetAreaKey(int.Parse(sAreaId)).TrimEnd('0');
			}

			if (sTmp.Length == 1)
				sTmp += "0%";
			else
				sTmp += "%";


			if (participants.getSenderInclude(sUserId) == "Si")
			{
				//if ( (sTmp.Substring(0,1) == "2" && sTmp.Length-1 == 3 ) && (sTmp.Substring(1,2) == "10" || sTmp.Substring(1,2) == "20" || sTmp.Substring(1,2) == "30") )
				if (sTmp.Length == 1)
					sTmp = sTmp.Substring(0,2) + "0%";
				else
					sTmp = sTmp.Substring(0,2) + "%";

				sCategoria = "00A";
			}

			sSql = "Select sof_empleados.id_empleado, sof_empleados.apellidonombre nombre From sof_empleados, Sof_Areas " +  
				" Where sof_areas.cve_area like '" + sTmp + "' And sof_empleados.id_area = sof_areas.id_area And sof_empleados.categoria > '"  + sCategoria + "' " + 
				" And sof_empleados.fecha_inicio <= '" + sDate +  "' And sof_empleados.fecha_fin >= '" + sDate + "' " +
				" And sof_empleados.eliminado = '0' " +
				" order by apellidonombre";


			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}

		public static string GetJob(int nUserId)
		{
			string sSql = "select sof_puesto.descripcion From sof_empleados, sof_puesto " +
				"Where sof_empleados.id_empleado = " + nUserId +
				"  And sof_puesto.puesto_id = sof_empleados.id_puesto";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (!(oObj == null))
				return oObj.ToString();
			else
				return "el id " + nUserId + " no tiene puesto asignado";
		}


		public static DataView GetUsersTree()
		{

			string sSql =	"select " +
							"clave_area, " +
							"area, " +
							"nombre, " +
							"status, " +
							"tabulador, " +
							"substr(clave_area,1,status+1) patron " +
							"from 	( "+
							"select " +
							"clave_area, " +
							"area, " +
							"nombre, " +
 							"decode(substr(tabulador,1,2),'19',1,'17',decode(clave_area,'100000',6,2),'15',decode(clave_area,'100000',6,3),'14',decode(clave_area,'100000',6,4),'13',decode(clave_area,'100000',6,5),6) status, " +
							"tabulador " +
							"from 	int_v_usuarios ) " +
							"order by clave_area, status ";

			DataView view = OracleHelper.ExecuteDataset1(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			return view;
		}

		public static DataSet GetUsersTreeDs()
		{

			string sSql =	"select " +
				"clave_area, " +
				"area, " +
				"nombre, " +
				"status, " +
				"id_usuario," +
				"tabulador, " +
				"substr(clave_area,1,status+1) patron " +
				"from 	( "+
				"select " +
				"clave_area, " +
				"area, " +
				"nombre, " +
				"decode(substr(tabulador,1,2),'19',1,'17',decode(clave_area,'100000',6,2),'15',decode(clave_area,'100000',6,3),'14',decode(clave_area,'100000',6,4),'13',decode(clave_area,'100000',6,5),6) status, " +
				"id_usuario, " +
				"tabulador " +
				"from 	int_v_usuarios ) " +
				"order by clave_area, status ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			return ds;
		}


		public static DataSet GetArea(string sDate)
		{
			string sSql = "select id_area, area From sof_Areas Where fecha_inicio <= '" + sDate + "' And  fecha_fin >= '"+ sDate + "' Order By area ";
			//string sSql = "select id_area, cve_area||' '|| area  area From sof_Areas Where 1=1 Order By cve_area ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetAreaByDate(string sDate)
		{
			string sSql = "select id_area, area From sof_Areas Where fecha_inicio <= '" + sDate + "' And  fecha_fin >= '"+ sDate + "' Order By area ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		
		public static DataSet GetArea_Hist()
		{
			string sSql = "select id_area, cve_area||' '||area  area from sof_areas Order by cve_area";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetArea(int nUser)
		{
			string sAreaId = GetAreaId(nUser).Substring(0,2);

			string sSql =	 "Select clave_area area_id, area  from  " +
							"(Select clave_area , max(area) area  from  hint_v_usuarios where area <> ' ' group by clave_area) order by area ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}



		public static DataSet GetAreaByUser(int nUser)
		{
			string sAreaId = GetAreaParameter(nUser);
			string sClaveArea = GetAreaKey(int.Parse(sAreaId));
			string sSql = "Select sof_areas.id_area areaId, sof_areas.cve_area||' -  '||sof_areas.area area From sof_areas";
			sSql += " WHERE sof_areas.cve_area LIKE '" + sClaveArea.Trim('0') + "%'"  ;
			sSql += " Order By sof_areas.cve_area";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static string GetPuesto(int nUserId)
		{
			string sSql = "select puesto from sof_puesto, sof_empleados where d_empleado = " + nUserId + " and puesto_id = id.puesto";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static DataSet GetPuesto()
		{
			string sSql = "select puesto_id, descripcion from sof_puesto order by descripcion ";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static string GetAreaId(int nUserId)
		{
			string sSql = "select sof_areas.cve_area from sof_empleados, sof_areas where sof_empleados.id_empleado = " + nUserId + " and sof_areas.id_area = sof_empleados.id_area And sof_empleados.eliminado = '0'";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static string GetAreaKeyByRemitenteId(int nUserId)
		{
			string sSql = "select sof_areas.cve_area from sof_areas, sof_regla " +
				"Where sof_regla.id_empleado = " + nUserId + " And sof_regla.eliminado = '0' " +
				"And sof_areas.id_area = sof_regla.id_remitente_area";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static string GetAreaKeyByDestinatarioId(int nUserId)
		{
			string sSql = "select sof_areas.cve_area from sof_areas, sof_regla " +
				"Where sof_regla.id_empleado = " + nUserId + " And sof_regla.eliminado = '0' " +
				"And sof_areas.id_area = sof_regla.id_destinatario_area";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}


		public static string GetAreaKey(int nAreaId)
		{
			string sSql = "select sof_areas.cve_area from sof_areas where sof_areas.id_area = " + nAreaId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}

		public static string GetUserKey(int empleadoId)
		{
			string sSql = "select sof_empleados.clave_empleado from sof_empleados Where sof_empleados.id_empleado = " + empleadoId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return oObj.ToString();
		}


		public static string GetAreaJefeId(int nUserId)
		{
			string sSql = "select clave_area from hint_v_usuarios where id_usuario = " + nUserId + " and tabulador > '12C' ";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";
		}


		public static string GetAreaParameter(int nUserId)
		{
			string sSql = "select id_destinatario_area from sof_regla where id_empleado = " + nUserId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";

		}
	
		public static string GetUserType(int nUserId)
		{
			string sSql = "select sof_regla.tipo_usuario from sof_regla where id_empleado = " + nUserId + " AND eliminado = '0'";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";

		}

		public static string GetTitularParameter(int nUserId)
		{
			string sSql = "select destinatario_titular_id from sof_regla where id_empleado = " + nUserId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";

		}

		
		public static string GetUsersByArea(int nUserId)
		{
			StringBuilder sUsers = new StringBuilder();
			string sAreaId = GetResponsible(nUserId);
			if (sAreaId != "0")
				sAreaId = GetAreaTrimRight(sAreaId);
			else
				sAreaId = GetAreaTrimRight(GetAreaId(nUserId));


			string sSql = "select id_usuario from hint_v_usuarios where hint_v_usuarios.clave_area like '" + sAreaId + "%' and hint_v_usuarios.tabulador > '12C' " ;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
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
			string sSql = "select id_empleado_bis from sof_mapeo_regla where id_empleado = " + nUserId;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
					sUsers.Append(dr[0].ToString() + "," );
				}
				return sUsers.ToString().TrimEnd(',');
			}
			else
				return nUserId.ToString();

		}


		public static string GetUserAlternate(int nUserId, int nLevel)
		{
			StringBuilder sUsers = new StringBuilder();
			string sSql = string.Empty;
			string sAreaId = GetAreaParameter(nUserId);
			string userType = GetUserType(nUserId);

			if (sAreaId == "0")
			{
				string sUserKey = GetUserIdWithPars(nUserId);
				return sUserKey;
				
			}
			else
			{
				sSql =	"select " +
						" nvl(sof_regla.id_destinatario_titular,0) id_destinatario_titular, "+
						"nvl(sof_regla.id_destinatario_area,0) id_destinatario_area " +
						"From sof_regla, sof_mapeo_regla Where sof_mapeo_regla.id_empleado = " + nUserId + " " +
						"And sof_regla.id_empleado = sof_mapeo_regla.id_empleado_bis ";
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
			string sAreaId = GetAreaParameter(nUserId);

			if (sAreaId == "0")
				sAreaId = GetAreaJefeId(nUserId);

			return GetAreaAllTrimRight(sAreaId).Length;
			
		}

		public static string GetResponsible(int nUserId)
		{
			string sSql = "select destinatario_area_id from sof_regla where id_empleado = " + nUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql) ;
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";
		}


		private static string GetAreaTrimRight(string sAreaId)
		{
			string sTmp = sAreaId;
			int nEndPos;
			for (int i = 0; i < sAreaId.Length; i++)
			{
				nEndPos = sTmp.LastIndexOf('0');
				if (nEndPos > 0)
					sTmp = sTmp.Substring(0,nEndPos);
				else
					break;
			}
			if (sTmp.Length == 1)
				sTmp += "0";
			return sTmp;
		}

		private static string GetUserIdWithPars(int nUserId)
		{
			string sUserId = string.Empty;
			string sSql =	"Select sof_empleados_hist.id_empleado, sof_empleados_hist.id_area " +
							"From sof_empleados, sof_empleados sof_empleados_hist " +
							"Where sof_empleados.id_empleado = " + nUserId +
							"And sof_empleados_hist.clave_empleado = sof_empleados.clave_empleado";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
					sUserId += dr["id_empleado"].ToString() + "," + dr["id_area"].ToString() + ",";
				}
				sUserId = sUserId.Remove(sUserId.ToString().Length-1,1);
			}

			return sUserId;

		}

		private static string GetAreaAllTrimRight(string sAreaId)
		{
			string sTmp = sAreaId;
			for (int i = 0; i < sAreaId.Length; i++)
			{
				if (sTmp.EndsWith("0"))
					sTmp = sTmp.Substring(0,sTmp.Length-1);
				else
					break;
			}
			return sTmp;
		}

		private static string GetAreaAllTrimRight(string sAreaId, int nLevel)
		{
			string sTmp = sAreaId;
			if (GetAreaAllTrimRight(sAreaId).Length < nLevel)
			{
				int nEndPos;
				for (int i = 0; i < nLevel; i++)
				{
					nEndPos = sTmp.LastIndexOf('0');
					if (nEndPos > 0)
						sTmp = sTmp.Substring(0,nEndPos);
					else
						break;
				}
			}
			return sTmp;
		}

		public static string GetConfirmConclude(int nUserId)
		{
			string sSql = "select confirma_concluir from sof_regla where id_empleado = " + nUserId;
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString() == "" ? "No" : oObj.ToString();
			else
				return "No";
		}

		public static string GetOwner(string sDocumentID, int sAlternateID)
		{
			string sSql = "select id_empleado from sof_documento where documento_id = " + sAlternateID;
			if (sDocumentID != null)
				sSql = "select id_empleado from sof_documento where documento_id = " + sDocumentID;

			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString() == "" ? "No" : oObj.ToString();
			else
				return "No";
		}

		public static int Create(string claveEmpleado, string nombreApellido, string apellidoNombre, string categoria,
								 string tipoEmpleado, int areaId, string claveArea, int puestoId, string clavePuesto, 
								 string login, string password, string startDate, string endDate,string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pNombreApellido", OracleType.VarChar),
											new OracleParameter("pApellidoNombre", OracleType.VarChar),
											new OracleParameter("pAreaId", null),
											new OracleParameter("pCveArea", OracleType.VarChar),
											new OracleParameter("pCvePuesto", OracleType.VarChar),
											new OracleParameter("pCategoria", OracleType.VarChar),
											new OracleParameter("pTipoEmpleado", OracleType.VarChar),
											new OracleParameter("pStartDate", OracleType.VarChar),
											new OracleParameter("pEndDate", OracleType.VarChar),
											new OracleParameter("pLogin", OracleType.VarChar, 60),
											new OracleParameter("pPassword", OracleType.VarChar, 60),
											new OracleParameter("pExpediente", OracleType.VarChar),
											new OracleParameter("pPuestoId", null),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = nombreApellido;
			oParam[1].Value = apellidoNombre;
			oParam[2].Value = areaId;
			oParam[3].Value = claveArea;
			oParam[4].Value = clavePuesto;
			oParam[5].Value = categoria;
			oParam[6].Value = tipoEmpleado;
			oParam[7].Value = startDate;
			oParam[8].Value = endDate;
			oParam[9].Value = login;
			oParam[10].Value = password;
			oParam[11].Value = claveEmpleado;
			oParam[12].Value = puestoId;
			oParam[13].Direction = ParameterDirection.InputOutput;
			oParam[13].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
						CommandType.StoredProcedure, "sp_empleados_create",oParam);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
            , new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Empleados / Edicion Empleados", enuAction.Create.GetDescription(), "sp_empleados_create", string.Join(",", oParam.ToList()), ip, sessionId, expedient));
            object obj = oParam[13].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int empleadoId, string nombreApellido, string apellidoNombre, 
			int areaId, string claveArea, string clavePuesto, string categoria, string tipoEmpleado, 
			string startDate, string endDate, string login, string password, string expediente, int puestoId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	new OracleParameter("pEmpleadoId", OracleType.VarChar),
											new OracleParameter("pNombreApellido", OracleType.VarChar),
											new OracleParameter("pApellidoNombre", OracleType.VarChar),
											new OracleParameter("pAreaId", null),
											new OracleParameter("pCveArea", OracleType.VarChar),
											new OracleParameter("pCvePuesto", OracleType.VarChar),
											new OracleParameter("pCategoria", OracleType.VarChar),
											new OracleParameter("pTipoEmpleado", OracleType.VarChar),
											new OracleParameter("pStartDate", OracleType.VarChar),
											new OracleParameter("pEndDate", OracleType.VarChar),
											new OracleParameter("pLogin", OracleType.VarChar, 60),
											new OracleParameter("pPassword", OracleType.VarChar, 60),
											new OracleParameter("pExpediente", OracleType.VarChar),
											new OracleParameter("pPuestoId",OracleType.VarChar, 12)
										};
			
			oParam[0].Value = empleadoId;
			oParam[1].Value = nombreApellido;
			oParam[2].Value = apellidoNombre;
			oParam[3].Value = areaId;
			oParam[4].Value = claveArea;
			oParam[5].Value = clavePuesto;
			oParam[6].Value = categoria;
			oParam[7].Value = tipoEmpleado;
			oParam[8].Value = startDate;
			oParam[9].Value = endDate;
			oParam[10].Value = login;
			oParam[11].Value = password;
			oParam[12].Value = expediente;
			oParam[13].Value = puestoId;


			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_empleados_update",oParam);

            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Empleados / Edicion Empleados", enuAction.Update.GetDescription(), "sp_empleados_update", string.Join(",", oParam.ToList()), ip, sessionId, expedient));
        }

		public static void Remove(int empleadoId, string eliminado, string endDate, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {new OracleParameter("pEmpleadoId", null),
										 new OracleParameter("pEliminado", OracleType.Char),
										 new OracleParameter("pEndDate", OracleType.VarChar)
										};
			
			oParam[0].Value = empleadoId;
			oParam[1].Value = eliminado;
			oParam[2].Value = endDate;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_empleados_remove",oParam);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Empleados / Edicion Empleados", enuAction.Delete.GetDescription(), "sp_empleados_remove", string.Join(",", oParam.ToList()), ip, sessionId, expedient));
        }


	}
}
