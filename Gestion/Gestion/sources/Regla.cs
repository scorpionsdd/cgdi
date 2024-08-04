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
using System.Collections.Generic;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Regla
	{
		public Regla()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataSet GetRegla(int ruleId, string uid, string ip, string sessionId, string expedient)
		{
			string sAlias = "sof_empleados sof_rem_emp, " +
							"sof_empleados sof_des_emp, " +
							"sof_areas sof_rem_area, " +
							"sof_areas sof_des_area, sof_empleados sof_sign, sof_areas sof_folio, " +
							"sof_empleados sof_tipo_doc_emp, " +
							"sof_areas sof_tipo_doc_area, " +
							"sof_empleados sof_rem_ext_emp, " +
							"sof_areas sof_rem_ext_area ";

			string sSql =	"Select sof_regla.*, sof_rem_emp.nombre remitenteNombre, sof_des_emp.nombre destinatarioNombre, " +
							"sof_rem_area.area remitenteArea, sof_des_area.area destinatarioArea, " +
							"sof_rem_area.cve_area RemitenteClaveArea, sof_des_area.cve_area DestinatarioClaveArea, " +
							"sof_sign.nombre signNombre, sof_folio.cve_area folioClaveArea, sof_folio.area folioArea, " +
							"sof_empleados.nombre usuarioNombre, " +
							"sof_tipo_doc_emp.apellidonombre tipo_doc_emp, " +
							"sof_tipo_doc_area.area tipo_doc_area, " +
							"decode(nvl(sof_regla.id_remitente_externo,0),0, sof_rem_ext_area.area,  sof_rem_ext_emp.apellidonombre) rem_ext " +
							"From sof_regla, sof_empleados, " + sAlias +
							" Where 1=1" +
							" And sof_regla.regla_id = " + ruleId +
							" And sof_empleados.id_empleado = sof_regla.id_empleado " +
							" And sof_rem_emp.id_empleado = sof_regla.id_remitente_titular " +
							" And sof_des_emp.id_empleado = sof_regla.id_destinatario_titular " +
							" And sof_rem_area.id_area = sof_regla.id_remitente_area " +
							" And sof_des_area.id_area = sof_regla.id_destinatario_area " +
							" And sof_regla.id_firma = sof_sign.id_empleado " +
							" And sof_regla.id_folio = sof_folio.id_area " +
							" And sof_tipo_doc_emp.id_empleado(+) = sof_regla.id_tipo_documento_empleado " +
							" And sof_tipo_doc_area.id_area(+) = sof_regla.id_tipo_documento_area " +
							" And sof_rem_ext_emp.id_empleado(+) = sof_regla.ID_REMITENTE_EXTERNO " +
							" And sof_rem_ext_area.id_area(+) = sof_empleados.id_area ";
            DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			#region LOG
			var metadata = ControlLog.GetInstance().GetMetadata(null, null, null
					, new List<Item> { new Item { text = "Regla", value = "REGLA_ID" } }
					, null, enuActionTrack.Retrieve, ds
					);
			ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Reglas / Editor de Regla", enuAction.Retrieve.GetDescription(), "sof_regla, sof_empleados", sSql, ip, sessionId, expedient, metadata.result)); 
			#endregion
			return ds;
		}

		public static DataSet GetReglas(string curtDate, string keyword, string uid, string ip)
		{
			string sAlias = "sof_empleados sof_rem_emp, sof_empleados sof_des_emp, sof_areas sof_rem_area, " +
					"sof_areas sof_des_area, sof_empleados sof_sign, sof_areas sof_folio ";

			string sSql = "Select sof_regla.*, sof_rem_emp.nombre remitenteNombre, sof_des_emp.nombre destinatarioNombre, " +
				"sof_rem_area.area remitenteArea, sof_des_area.area destinatarioArea, " +
				"sof_rem_area.cve_area RemitenteClaveArea, sof_des_area.cve_area DestinatarioClaveArea, " +
				"sof_sign.nombre signNombre, sof_folio.cve_area folioClaveArea, sof_folio.area folioArea, " +
				"sof_empleados.apellidonombre usuarioNombre " +
				"From sof_regla, sof_empleados, " + sAlias +
				" Where sof_regla.fecha_inicio <= '" + curtDate + "' and sof_regla.fecha_fin >= '" + curtDate + "'" +
				" And sof_empleados.id_empleado = sof_regla.id_empleado " +
				" And sof_rem_emp.id_empleado = sof_regla.id_remitente_titular " +
				" And sof_des_emp.id_empleado = sof_regla.id_destinatario_titular " +
				" And sof_rem_area.id_area = sof_regla.id_remitente_area " +
				" And sof_des_area.id_area = sof_regla.id_destinatario_area " +
				" And sof_regla.id_firma = sof_sign.id_empleado " +
				" And sof_regla.id_folio = sof_folio.id_area " +
				" Order by usuarioNombre " ;            
            DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
            return ds;
		}


		public static string GetAreaParameter(int nUserId)
		{
			string sSql = "select destinatario_area_id from sof_regla where id_empleado = " + nUserId + " and eliminado = '0'" ;
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
			string sSql = "select destinatario_titular_id from sof_regla where id_empleado = " + nUserId + " AND eliminado = '0'";
			Object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString();
			else
				return "0";

		}

		public static string GetUserRegistry(int nUserId)
		{
			StringBuilder sUsers = new StringBuilder();
			string sAreaId = string.Empty;
			
			string sSql = "SELECT id_empleado, destinatario_area_id FROM sof_regla, sof_mapeo_regla " +
						  "WHERE sof_regla.id_empleado = " + nUserId + " AND sof_regla.eliminado = '0' " +
						  "AND sof_mapeo_regla.regla_id =  sof_regla.regla_id";

			string sUserType = Users.GetUserType(nUserId);

			if (sAreaId == "0")
			{
				sAreaId = Users.GetAreaJefeId(nUserId);
				if (sAreaId != "0")
					sSql = "select id_usuario id_empleado, clave_area from  hint_v_usuarios where hint_v_usuarios.id_usuario = " + nUserId + " (FECHA_INICIO <= '20/09/2007') AND  (FECHA_FIN >= '20/09/2007')";
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
			string sAreaId = GetAreaParameter(nUserId);
			string userType = GetUserType(nUserId);
			bool bIsBoss = false;

			if (sAreaId == "0")
			{
				sAreaId = Users.GetAreaJefeId(nUserId);
				if (sAreaId == "0")
					return nUserId.ToString() + "," + Users.GetAreaId(nUserId);
				else
				{
					sAreaId = GetAreaAllTrimRight(sAreaId);
				}

				//sAreaId = GetAreaAllTrimRight(sAreaId, nLevel);
				sSql = "select id_usuario, clave_area FROM hint_v_usuarios where hint_v_usuarios.clave_area like '" + sAreaId + "%'" + " and tabulador > '12C' ";
				bIsBoss = true;
			}
			else
			{
				sAreaId = GetAreaAllTrimRight(sAreaId, nLevel);
				sSql = "select destinatario_titular_id id_usuario, destinatario_area_id from sof_regla where sof_regla.destinatario_area_id like '" + sAreaId + "%'" ;
			}

			if ( userType == "T" || userType == "S" )
				sSql = "select sof_regla.destinatario_titular_id, sof_regla.destinatario_area_id from sof_regla where sof_regla.id_empleado = " + nUserId;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach( DataRow dr in ds.Tables[0].Rows)
				{
					if (bIsBoss)
						sUsers.Append(nUserId.ToString()+ ",").Append(dr[1].ToString()+ ",");
					else
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
				sAreaId = Users.GetAreaJefeId(nUserId);

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


		public static int Create(string CAMBIA_ESTATUS,
			string SEGUIMIENTO,
			string TIPO_DOCUMENTO,
			string REMITENTE_EXTERNO,
			string TIPO_USUARIO,
			string REMITENTE_EXT_CATALOGO,
			string TIPO_DOCUMENTO_CATALOGO,
			string REM_INCLUYE_OPER,
			string TUR_ARBOL,
			string CONFIRMA_CONCLUIR,
			string CONCLUIR_ACUSE_CCPARA,
			string RESPONDER_CASCADA,
			int ID_EMPLEADO,
			int ID_REMITENTE_AREA,
			int ID_REMITENTE_TITULAR,
			int ID_DESTINATARIO_AREA,
			int ID_DESTINATARIO_TITULAR,
			int ID_FIRMA,
			int ID_FOLIO,
			int ID_TIPO_DOCUMENTO_EMPLEADO,
			int ID_TIPO_DOCUMENTO_AREA,
			int ID_REMITENTE_EXTERNO,
			string DOCUMENTO_TIPO,
			string ELIMINADO,
			string FECHA_INICIO,
			string FECHA_FIN
            , string uid, string ip, string sessionId, string expedient
            )
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pCAMBIA_ESTATUS", OracleType.VarChar),
											new OracleParameter("pSEGUIMIENTO", OracleType.VarChar),
											new OracleParameter("pTIPO_DOCUMENTO", OracleType.VarChar),
											new OracleParameter("pREMITENTE_EXTERNO", OracleType.VarChar),
											new OracleParameter("pTIPO_USUARIO", OracleType.VarChar),
											new OracleParameter("pREMITENTE_EXT_CATALOGO", OracleType.VarChar),
											new OracleParameter("pTIPO_DOCUMENTO_CATALOGO", OracleType.VarChar),
											new OracleParameter("pREM_INCLUYE_OPER", OracleType.VarChar),
											new OracleParameter("pTUR_ARBOL", OracleType.VarChar),
											new OracleParameter("pCONFIRMA_CONCLUIR", OracleType.VarChar),
											new OracleParameter("pCONCLUIR_ACUSE_CCPARA", OracleType.VarChar),
											new OracleParameter("pRESPONDER_CASCADA", OracleType.VarChar),
											new OracleParameter("pID_EMPLEADO", null),
											new OracleParameter("pID_REMITENTE_AREA", null),
											new OracleParameter("pID_REMITENTE_TITULAR", null),
											new OracleParameter("pID_DESTINATARIO_AREA", null),
											new OracleParameter("pID_DESTINATARIO_TITULAR", null),
											new OracleParameter("pID_FIRMA", null),
											new OracleParameter("pID_FOLIO", null),
											new OracleParameter("pID_TIPO_DOCUMENTO_EMPLEADO", null),
											new OracleParameter("pID_TIPO_DOCUMENTO_AREA", null),
											new OracleParameter("pID_REMITENTE_EXTERNO", null),
											new OracleParameter("pDOCUMENTO_TIPO",OracleType.VarChar),
											new OracleParameter("pELIMINADO",OracleType.VarChar),
											new OracleParameter("pFECHA_INICIO",OracleType.VarChar),
											new OracleParameter("pFECHA_FIN",OracleType.VarChar)
										};
			
			oParam[0].Value = CAMBIA_ESTATUS;
			oParam[1].Value = SEGUIMIENTO;
			oParam[2].Value = TIPO_DOCUMENTO;
			oParam[3].Value = REMITENTE_EXTERNO;
			oParam[4].Value = TIPO_USUARIO;
			oParam[5].Value = REMITENTE_EXT_CATALOGO;
			oParam[6].Value = TIPO_DOCUMENTO_CATALOGO;
			oParam[7].Value = REM_INCLUYE_OPER;
			oParam[8].Value = TUR_ARBOL;
			oParam[9].Value = CONFIRMA_CONCLUIR;
			oParam[10].Value = CONCLUIR_ACUSE_CCPARA;
			oParam[11].Value = RESPONDER_CASCADA;
			oParam[12].Value = ID_EMPLEADO;
			oParam[13].Value = ID_REMITENTE_AREA;
			oParam[14].Value = ID_REMITENTE_TITULAR;
			oParam[15].Value = ID_DESTINATARIO_AREA;
			oParam[16].Value = ID_DESTINATARIO_TITULAR;
			oParam[17].Value = ID_FIRMA;
			oParam[18].Value = ID_FOLIO;
			oParam[19].Value = ID_TIPO_DOCUMENTO_EMPLEADO;
			oParam[20].Value = ID_TIPO_DOCUMENTO_AREA;
			oParam[21].Value = ID_REMITENTE_EXTERNO;
			oParam[22].Value = DOCUMENTO_TIPO;
			oParam[23].Value = ELIMINADO;
			oParam[24].Value = FECHA_INICIO;
			oParam[25].Value = FECHA_FIN;
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_regla_create",oParam);
            #region Log
            List<Item> fieldMetadata = new List<Item> {
                { new Item { label="Al imprimir el volante cambia de Pendiente a Trámite?", text ="CAMBIA_ESTATUS", value = "pCAMBIA_ESTATUS" } },
                { new Item { label="Seguimiento Externo? ", text ="SEGUIMIENTO", value = "pSEGUIMIENTO" } },
                { new Item { label="Los Tipo Documento son por Empleado?", text ="TIPO_DOCUMENTO", value = "pTIPO_DOCUMENTO" } },
                { new Item { label="Los Remitentes Externos son por Empleado?", text ="REMITENTE_EXTERNO", value = "pREMITENTE_EXTERNO" } },
                { new Item { label="Tipo Usuario", text ="TIPO_USUARIO", value = "pTIPO_USUARIO" } },
                { new Item { label=" Usa Catálogo Remitente Externo", text ="REMITENTE_EXT_CATALOGO", value = "pREMITENTE_EXT_CATALOGO" } },
                { new Item { label="Usa Catálogo Tipo Documento?", text ="TIPO_DOCUMENTO_CATALOGO", value = "pTIPO_DOCUMENTO_CATALOGO" } },
                { new Item { label="Remitente Incluye Operativos? ", text ="REM_INCLUYE_OPER", value = "pREM_INCLUYE_OPER" } },
                { new Item { label=" Al Turnar Despliega Operativos?", text ="TUR_ARBOL", value = "pTUR_ARBOL" } },
                { new Item { label="Confirna Concluir, el Volante? ", text ="CONFIRMA_CONCLUIR", value = "pCONFIRMA_CONCLUIR" } },
                { new Item { label="Se concluye un volante al acusar de recibo un turno con copia", text ="CONCLUIR_ACUSE_CCPARA", value = "pCONCLUIR_ACUSE_CCPARA" } },
                { new Item { label="La Respuesta es en Cascada? ", text ="RESPONDER_CASCADA", value = "pRESPONDER_CASCADA" } },
                { new Item { label="Usuario", text ="ID_EMPLEADO", value = "pID_EMPLEADO",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
                { new Item { label="Remitente Area", text ="ID_REMITENTE_AREA", value = "pID_REMITENTE_AREA" , sentence = "select area \"text\" From sof_Areas WHERE id_area={0}"} },
                { new Item { label="Remitente", text ="ID_REMITENTE_TITULAR", value = "pID_REMITENTE_TITULAR" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
                { new Item { label="Destinatario Area", text ="ID_DESTINATARIO_AREA", value = "pID_DESTINATARIO_AREA", sentence = "select area \"text\" From sof_Areas WHERE id_area={0}" } },
                { new Item { label="Destinatario", text ="ID_DESTINATARIO_TITULAR", value = "pID_DESTINATARIO_TITULAR",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
                { new Item { label="Firma", text ="ID_FIRMA", value = "pID_FIRMA",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
                { new Item { label="Folio", text ="ID_FOLIO", value = "pID_FOLIO" , sentence = "select area \"text\" From sof_Areas WHERE id_area={0}"} },
                { new Item { label="Tipo Documento Empleado", text ="ID_TIPO_DOCUMENTO_EMPLEADO", value = "pID_TIPO_DOCUMENTO_EMPLEADO" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
                { new Item { label="Tipo Documento Empleado Area", text ="ID_TIPO_DOCUMENTO_AREA", value = "pID_TIPO_DOCUMENTO_AREA", sentence = "select area \"text\" From sof_Areas WHERE id_area={0}" } },
                { new Item { label="Remitente Externo", text ="ID_REMITENTE_EXTERNO", value = "pID_REMITENTE_EXTERNO" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
                { new Item { label="Remitente Externo Area", text ="DOCUMENTO_TIPO", value = "pDOCUMENTO_TIPO" } },
                { new Item { label="Eliminado", text ="ELIMINADO", value = "pELIMINADO" } },
                { new Item { label="Fecha de Vigencia", text ="FECHA_INICIO", value = "pFECHA_INICIO" } },
                { new Item { label="Fecha de Vigencia Fin", text ="FECHA_FIN", value = "pFECHA_FIN" } }
            };
            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, null
                , fieldMetadata
                , null, enuActionTrack.Create
                );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Reglas / Editor de Reglas", enuAction.Create.GetDescription(), "sp_regla_create", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result));
            #endregion


            //			object obj = oParam[4].Value;
            return 1;
		}

		public static void Update(int REGLA_ID,
			string CAMBIA_ESTATUS,
			string SEGUIMIENTO,
			string TIPO_DOCUMENTO,
			string REMITENTE_EXTERNO,
			string TIPO_USUARIO,
			string REMITENTE_EXT_CATALOGO,
			string TIPO_DOCUMENTO_CATALOGO,
			string REM_INCLUYE_OPER,
			string TUR_ARBOL,
			string CONFIRMA_CONCLUIR,
			string CONCLUIR_ACUSE_CCPARA,
			string RESPONDER_CASCADA,
			int ID_EMPLEADO,
			int ID_REMITENTE_AREA,
			int ID_REMITENTE_TITULAR,
			int ID_DESTINATARIO_AREA,
			int ID_DESTINATARIO_TITULAR,
			int ID_FIRMA,
			int ID_FOLIO,
			int ID_TIPO_DOCUMENTO_EMPLEADO,
			int ID_TIPO_DOCUMENTO_AREA,
			int ID_REMITENTE_EXTERNO,
			string DOCUMENTO_TIPO,
			string ELIMINADO,
			string FECHA_INICIO,
			string FECHA_FIN
            , string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {
                                            new OracleParameter("pREGLA_ID", OracleType.Number),
                                            new OracleParameter("pCAMBIA_ESTATUS", OracleType.VarChar),
											new OracleParameter("pSEGUIMIENTO", OracleType.VarChar),
											new OracleParameter("pTIPO_DOCUMENTO", OracleType.VarChar),
											new OracleParameter("pREMITENTE_EXTERNO", OracleType.VarChar),
                                            new OracleParameter("pTIPO_USUARIO", OracleType.VarChar),
                                            new OracleParameter("pREMITENTE_EXT_CATALOGO", OracleType.VarChar),
                                            new OracleParameter("pTIPO_DOCUMENTO_CATALOGO", OracleType.VarChar),
                                            new OracleParameter("pREM_INCLUYE_OPER", OracleType.VarChar),
                                            new OracleParameter("pTUR_ARBOL", OracleType.VarChar),
                                            new OracleParameter("pCONFIRMA_CONCLUIR", OracleType.VarChar),
                                            new OracleParameter("pCONCLUIR_ACUSE_CCPARA", OracleType.VarChar),
                                            new OracleParameter("pRESPONDER_CASCADA", OracleType.VarChar),
                                            new OracleParameter("pID_EMPLEADO", OracleType.Number),
                                            new OracleParameter("pID_REMITENTE_AREA", OracleType.Number),
                                            new OracleParameter("pID_REMITENTE_TITULAR", OracleType.Number),
                                            new OracleParameter("pID_DESTINATARIO_AREA", OracleType.Number),
                                            new OracleParameter("pID_DESTINATARIO_TITULAR", OracleType.Number),
                                            new OracleParameter("pID_FIRMA", OracleType.Number),
                                            new OracleParameter("pID_FOLIO", OracleType.Number),
                                            new OracleParameter("pID_TIPO_DOCUMENTO_EMPLEADO", OracleType.Number),
                                            new OracleParameter("pID_TIPO_DOCUMENTO_AREA", OracleType.Number),
                                            new OracleParameter("pID_REMITENTE_EXTERNO", OracleType.Number),
                                            new OracleParameter("pDOCUMENTO_TIPO",OracleType.VarChar),
                                            new OracleParameter("pELIMINADO",OracleType.VarChar),
                                            new OracleParameter("pFECHA_INICIO",OracleType.VarChar),
                                            new OracleParameter("pFECHA_FIN",OracleType.VarChar)
                                            
										};
			oParam[0].Value = REGLA_ID;
			oParam[1].Value = CAMBIA_ESTATUS;
			oParam[2].Value = SEGUIMIENTO;
			oParam[3].Value = TIPO_DOCUMENTO;
			oParam[4].Value = REMITENTE_EXTERNO;
            oParam[5].Value = TIPO_USUARIO;
            oParam[6].Value = REMITENTE_EXT_CATALOGO;
            oParam[7].Value = TIPO_DOCUMENTO_CATALOGO;
            oParam[8].Value = REM_INCLUYE_OPER;
            oParam[9].Value = TUR_ARBOL;
            oParam[10].Value = CONFIRMA_CONCLUIR;
            oParam[11].Value = CONCLUIR_ACUSE_CCPARA;
            oParam[12].Value = RESPONDER_CASCADA;
            oParam[13].Value = ID_EMPLEADO;
            oParam[14].Value = ID_REMITENTE_AREA;
            oParam[15].Value = ID_REMITENTE_TITULAR;
            oParam[16].Value = ID_DESTINATARIO_AREA;
            oParam[17].Value = ID_DESTINATARIO_TITULAR;
            oParam[18].Value = ID_FIRMA;
            oParam[19].Value = ID_FOLIO;
            oParam[20].Value = ID_TIPO_DOCUMENTO_EMPLEADO;
            oParam[21].Value = ID_TIPO_DOCUMENTO_AREA;
            oParam[22].Value = ID_REMITENTE_EXTERNO;
            oParam[23].Value = DOCUMENTO_TIPO;
            oParam[24].Value = ELIMINADO;
            oParam[25].Value = FECHA_INICIO;
            oParam[26].Value = FECHA_FIN;
			#region Log
			OracleParameter[] oParamMetadata = {
											new OracleParameter("pREGLA_ID", OracleType.Number),
											new OracleParameter("outCursor", OracleType.Cursor)};
			List<Item> fieldMetadata = new List<Item> {
            { new Item { label="Al imprimir el volante cambia de Pendiente a Trámite?", text ="CAMBIA_ESTATUS", value = "pCAMBIA_ESTATUS" } },
            { new Item { label="Seguimiento Externo? ", text ="SEGUIMIENTO", value = "pSEGUIMIENTO" } },
            { new Item { label="Los Tipo Documento son por Empleado?", text ="TIPO_DOCUMENTO", value = "pTIPO_DOCUMENTO" } },
            { new Item { label="Los Remitentes Externos son por Empleado?", text ="REMITENTE_EXTERNO", value = "pREMITENTE_EXTERNO" } },
            { new Item { label="Tipo Usuario", text ="TIPO_USUARIO", value = "pTIPO_USUARIO" } },
            { new Item { label=" Usa Catálogo Remitente Externo", text ="REMITENTE_EXT_CATALOGO", value = "pREMITENTE_EXT_CATALOGO" } },
            { new Item { label="Usa Catálogo Tipo Documento?", text ="TIPO_DOCUMENTO_CATALOGO", value = "pTIPO_DOCUMENTO_CATALOGO" } },
            { new Item { label="Remitente Incluye Operativos? ", text ="REM_INCLUYE_OPER", value = "pREM_INCLUYE_OPER" } },
            { new Item { label=" Al Turnar Despliega Operativos?", text ="TUR_ARBOL", value = "pTUR_ARBOL" } },
            { new Item { label="Confirna Concluir, el Volante? ", text ="CONFIRMA_CONCLUIR", value = "pCONFIRMA_CONCLUIR" } },
            { new Item { label="Se concluye un volante al acusar de recibo un turno con copia", text ="CONCLUIR_ACUSE_CCPARA", value = "pCONCLUIR_ACUSE_CCPARA" } },
            { new Item { label="La Respuesta es en Cascada? ", text ="RESPONDER_CASCADA", value = "pRESPONDER_CASCADA" } },
            { new Item { label="Usuario", text ="ID_EMPLEADO", value = "pID_EMPLEADO",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
            { new Item { label="Remitente Area", text ="ID_REMITENTE_AREA", value = "pID_REMITENTE_AREA" , sentence = "select area \"text\" From sof_Areas WHERE id_area={0}"} },
            { new Item { label="Remitente", text ="ID_REMITENTE_TITULAR", value = "pID_REMITENTE_TITULAR" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
            { new Item { label="Destinatario Area", text ="ID_DESTINATARIO_AREA", value = "pID_DESTINATARIO_AREA", sentence = "select area \"text\" From sof_Areas WHERE id_area={0}" } },
            { new Item { label="Destinatario", text ="ID_DESTINATARIO_TITULAR", value = "pID_DESTINATARIO_TITULAR",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
            { new Item { label="Firma", text ="ID_FIRMA", value = "pID_FIRMA",sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}" } },
            { new Item { label="Folio", text ="ID_FOLIO", value = "pID_FOLIO" , sentence = "select area \"text\" From sof_Areas WHERE id_area={0}"} },
            { new Item { label="Tipo Documento Empleado", text ="ID_TIPO_DOCUMENTO_EMPLEADO", value = "pID_TIPO_DOCUMENTO_EMPLEADO" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
            { new Item { label="Tipo Documento Empleado Area", text ="ID_TIPO_DOCUMENTO_AREA", value = "pID_TIPO_DOCUMENTO_AREA", sentence = "select area \"text\" From sof_Areas WHERE id_area={0}" } },
            { new Item { label="Remitente Externo", text ="ID_REMITENTE_EXTERNO", value = "pID_REMITENTE_EXTERNO" ,sentence="SELECT nombre \"text\" FROM sof_empleados WHERE ID_EMPLEADO ={0}"} },
            { new Item { label="Remitente Externo Area", text ="DOCUMENTO_TIPO", value = "pDOCUMENTO_TIPO" } },
            { new Item { label="Eliminado", text ="ELIMINADO", value = "pELIMINADO" } },
            { new Item { label="Fecha de Vigencia", text ="FECHA_INICIO", value = "pFECHA_INICIO" } }

            };
			oParamMetadata[0].Value = REGLA_ID;
			oParamMetadata[1].Direction = ParameterDirection.Output;
			
			var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, oParamMetadata
				, fieldMetadata
				, "SP_REGLA_UPDATE_LOG", enuActionTrack.Update
				); 
			#endregion
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_regla_update",oParam);
			#region Log
			ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
				, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Reglas / Editor de Reglas", enuAction.Update.GetDescription(), "sp_regla_update", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result)); 
			#endregion

		}

        public static void Remove(int ruleId, string endDate, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {new OracleParameter("pReglaId", null),
										 new OracleParameter("pEndDate",OracleType.VarChar)
										};
			
			oParam[0].Value = ruleId;
			oParam[1].Value = endDate;
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_regla_remove",oParam);
            var metadata = ControlLog.GetInstance().GetMetadata(null, oParam, null
				, new List<Item> { new Item { text = "Regla", value = "pReglaId" }, new Item { text = "Fecha Fin", value = "pEndDate" } }
				, null, enuActionTrack.Delete, null
				);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Reglas / Editor de Reglas", enuAction.Delete.GetDescription(), "sp_regla_remove", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result));
        }


	}
}
