using System;
using System.Data;
using System.Collections;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using System.IO;

// Components Banobras
using BComponents.DataAccessLayer;
using Gestion.WebComponents;
using BCCXMLDOMParser;
using System.Web.WebSockets;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Documents.
	/// </summary>
	public class Documents
	{
		private string sListParent = string.Empty;
		private string sList = string.Empty;

		private const string cnsQueryPath = @"C:\GESTION\DATA\";
		private const string cnsQueryFile = "BCGQueryDocuments.xml";
        private const string cnsDocumentsSend = "BCGQueryDocumentsByVolante.xml";


		private const string  cnsColumns = "Select * ";
		private const string  cnsColumnsTurnado = "Select sd.*, " +
			"Sof_Documento_Turnar.Documento_ID, " +
			"Sof_Documento_Turnar.id_destinatario_area TurnadoAreaId, " +
			"Sof_Turnado_Area.Area TurnadoArea, " +
			"Sof_Turnado_Titular.Nombre TurnadoNombre, " +
			"Decode(Sof_Documento_Turnar.Estatus,0,'Sin/Abrir',1,'Trámite',2,'Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"Sof_Documento_Turnar.Instruccion From (";

		private const string cnsSelects = "Select " +
			"Sof_documento.Documento_id, Sof_Documento.Asunto, Sof_Documento.Resumen, " +
			"Sof_Documento.id_empleado, Sof_Documento.Volante, Sof_Documento.Tipo_Remitente, Sof_Documento.Referencia, " +
			"Sof_Documento.Fecha_Documento_Fuente, Sof_Documento.Fecha_Elaboracion, Sof_Documento.Documento_Bis_Id, " +
			"<@SENDERAREA@>, <@SENDERMANAGER@>, " +
			"Sof_Documento.id_destinatario_area	DestinatarioAreaId, Sof_Destinatario_Area.Area DestinatarioArea, " +
			"Sof_Destinatario_Titular.Nombre DestinatarioNombre, Sof_Documento.Estatus StatusVolante ";

		private const string cnsSelectTurnado = " Select " +
			"Sof_Documento_Turnar.Documento_ID, " +
			"Sof_Documento_Turnar.id_destinatario_area TurnadoAreaId, " +    
			"Sof_Turnado_Area.Area TurnadoArea, " +
			"Sof_Turnado_Titular.Nombre	TurnadoNombre, " +
			"Decode(Sof_Documento_Turnar.Estatus,0,'Sin/Abrir',1,'Trámite',2,'Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"Sof_Documento_Turnar.Instruccion ";

		private const string cnsSelectCCPara = "Select " +
			"Sof_CCPara.Documento_ID, " +
			"Sof_CCPara.id_Destinatario_Area TurnadoAreaId, " +
			"Sof_Turnado_Titular.Area TurnadoArea, " +
			"Sof_Turnado_Titular.Nombre TurnadoNombre, " +
			"Decode(Sof_CCPAra.Estatus,0,'Sin/Abrir',1,'Trámite',2,'Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"('') Instruccion ";

		private const string cnsFrom = "From Sof_Documento, Sof_Tipo_Documento, " +
			"sof_areas Sof_Destinatario_Area, " +
			"sof_empleados Sof_Destinatario_Titular, " +
			"<@SENDERAREA@>, <@SENDERMANAGER@> " ;

		private const string cnsWhere = "Where Sof_Documento.id_empleado in (<@USERS@>) " +
			"and Sof_Tipo_Documento.Tipo_Documento_ID = Sof_Documento.Tipo_Documento_ID " +
			"and Sof_Documento.Estatus 	like <@STATUSVOLANTE@> " +
			"and sof_Documento.Tipo_Remitente = <@SENDERTYPE@> " +
			"and <@AREAID@> = Sof_Documento.id_remitente_area " +
			"and <@AREAMANAGER@> = Sof_Documento.id_remitente " +
			"and Sof_Destinatario_Area.id_area(+) = Sof_Documento.id_destinatario_area  " +
			"and sof_Destinatario_Titular.id_empleado(+) = Sof_Documento.id_destinatario ";
			
		private const string cnsFromTurnado = " " +
			"sof_documento_turnar, " +
			"sof_areas Sof_Turnado_Area, " +
			"sof_empleados Sof_Turnado_Titular ";
		
		private const string cnsFromCCPara = " From " +
			"Sof_CCPara, " +
			"sof_areas Sof_Turnado_Area, " +
			"sof_empleados Sof_Turnado_Titular ";

		private const string cnsWhereTurnado = " Where " +
			"sof_documento_turnar.Eliminado = '0'" + " " +
			"<@STATUSRECEIVE@> " +
			"and Sof_Turnado_Area.id_area(+) = sof_documento_turnar.id_destinatario_area " +
			"and Sof_Turnado_Titular.id_empleado(+) = sof_documento_turnar.id_destinatario ";

		private const string cnsWhereCCPara = " Where " +
			"Sof_CCPara.Eliminado = '0'" + " " +
			"and Sof_CCPara.Estatus <@STATUSRECEIVE@> " +
			"and Sof_Turnado_Area.id_area(+) = Sof_CCPara.id_destinatario_area " +
			"and Sof_Turnado_Titular.id_empleado(+) = Sof_CCPara.id_destinatario ";


		private const string cnsWhereSinTurnar = "Where sof_documento.id_empleado in (<@USERS@>) " +
			"and sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id " +
			"and sof_documento.tipo_remitente = <@SENDERTYPE@> " +
			"and <@AREAID@> = sof_documento.id_remitente_area " +
			"and <@AREAMANAGER@> = sof_documento.id_remitente " +
			"and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area  " +
			"and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario " +

			"and sof_documento_turnar.Documento_ID(+) = sof_documento.Documento_ID " +
			"and sof_documento_turnar.Eliminado(+) = '0'" + " " +
			"and sof_turnado_area.id_area(+) = sof_documento_turnar.id_destinatario_area " +
			"and sof_turnado_titular.id_empleado(+) = sof_documento_turnar.id_destinatario " +

			"and sof_documento_turnar.Documento_ID is null " ;

		private const string cnsOrder = " order by turnadoAreaId, volante";
		
		private const string  cnsColumnsT = "Select " +
			"documento_id, asunto, resumen, id_empleado, volante, referencia, fecha_documento_fuente, fecha_elaboracion, documento_bis_id, " +
			"destinatarioAreaId, destinatarioArea, destinatarioNombre, statusVolante, turnadoAreaId, " +
			"turnadoArea, turnadoNombre, statusTurnadoId, statusTurnado, instruccion, documento_turnar_id, " +
			"fecha_Desde, fecha_Hasta, destinatarioClaveArea, turnadoClaveArea, nombreProyecto, id_tipo_solicitud, id_tipo_atencion, " +
			"tipoSolicitud, tipoAtencion " +
			"From ( ";

		private const string cnsSelectsT = "Select sof_documento.documento_id, sof_documento.asunto, sof_documento.resumen, " +
			"sof_documento.id_empleado, sof_documento.volante, sof_documento.referencia, sof_documento.fecha_documento_fuente, " +
			"sof_documento.fecha_elaboracion, sof_documento.documento_bis_id, sof_documento.id_destinatario_area destinatarioAreaId, " +
			"sof_destinatario_area.area destinatarioArea, sof_destinatario_titular.nombre destinatarioNombre, " +
			"sof_documento.estatus statusVolante, sof_documento_turnar.id_destinatario_area turnadoAreaId, " +
			"sof_turnado_area.area turnadoArea, sof_turnado_titular.nombre turnadoNombre, sof_documento_turnar.estatus statusTurnadoId, " +
			"decode(sof_documento_turnar.estatus,'0','Sin Abrir','1','En Trámite','2','Returnado','3','Concluido') statusTurnado, " +
			"sof_documento_turnar.instruccion, sof_documento_turnar.documento_turnar_id, " +
			"sof_estatus_turnar.fecha_desde, " +
			"decode( to_char(sof_estatus_turnar.fecha_hasta,'dd/mm/yyyy'),'01/01/2049', sysdate, sof_estatus_turnar.fecha_hasta) fecha_hasta, " +
			"sof_destinatario_titular.clave_area destinatarioClaveArea, sof_turnado_titular.clave_area turnadoClaveArea, " +
			"sof_documento.nombre_proyecto nombreProyecto, sof_documento.id_tipo_solicitud, sof_documento.id_tipo_atencion, " +
			"sof_tipo_solicitud.tipo_solicitud tipoSolicitud, sof_tipo_atencion.tipo_atencion tipoAtencion ";

		private const string cnsFromT = " From " +
			"sof_documento, " +
			"sof_areas sof_turnado_area, " +
			"sof_empleados sof_turnado_titular, " +
			"sof_areas sof_destinatario_area, " +
			"sof_empleados sof_destinatario_titular," +
			"sof_documento_turnar, sof_estatus_turnar, sof_mapeo_recibidos, " +
			"sof_tipo_solicitud, sof_tipo_atencion ";
			

		//"and sof_documento_turnar.id_destinatario_area in (<@ALTERNATEAREAID@>) " +
		private const string cnsWhereT = "Where 1=1 " +
			"and sof_mapeo_recibidos.id_empleado = <@USERS@> " +
			"and sof_documento_turnar.id_destinatario = sof_mapeo_recibidos.id_destinatario " +
			"and sof_documento_turnar.eliminado = '0' " +
			"and sof_documento_turnar.estatus <@STATUSTURNADO@> " +
			"and sof_turnado_area.id_area(+) = sof_documento_turnar.id_destinatario_area " +
			"and sof_turnado_titular.id_empleado(+) = sof_documento_turnar.id_destinatario " +
			"and sof_documento.documento_id = sof_documento_turnar.documento_id " +
			"and sof_documento.estatus <> 'Pendiente' " +
			"And sof_mapeo_recibidos.fecha_inicio <= TRUNC(sof_documento.fecha_elaboracion) " +
			"And sof_mapeo_recibidos.fecha_fin >= TRUNC(sof_documento.fecha_elaboracion) " +
			"and TRUNC(sof_documento.fecha_elaboracion) >= to_date('<@FROMDATE@>') " +
			"and TRUNC(sof_documento.fecha_elaboracion) <= to_date('<@TODATE@>') " +
			"and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area  " +
			"and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario " +
			"and sof_estatus_turnar.documento_turnar_id = sof_documento_turnar.documento_turnar_id " +
			"and sof_estatus_turnar.estatus = 0 " +
			"and sof_tipo_solicitud.id_tipo_solicitud = sof_documento.id_tipo_solicitud " +
			"and sof_tipo_atencion.id_tipo_atencion = sof_documento.id_tipo_atencion" +
			")";

		private const string cnsOrderT = " order by destinatarioAreaId, turnadoAreaId, turnadoNombre, volante";

		private const string  cnsColumnsOrder = "Select "  +
			"Sof_Documento.Documento_ID, sof_documento.asunto, sof_documento.resumen, sof_documento.id_empleado, sof_documento.volante, " +
			"Sof_Documento.Tipo_Remitente, Sof_Documento.Referencia, Sof_Documento.Fecha_Documento_Fuente, Sof_Documento.Fecha_Elaboracion, " +
			"sof_documento.documento_bis_id, <@SENDERAREACOLUMN@>, <@SENDERNAMECOLUMN@>, " +
			"sof_documento.id_destinatario_area	destinatarioAreaId, sof_destinatario_area.area destinatarioArea, " +
			"sof_destinatario_titular.nombre destinatarioNombre, sof_documento.estatus StatusVolante, sof_documento.observacion ";

		private const string cnsFromOrder = "From Sof_Documento, " +
			"sof_areas Sof_Destinatario_Area, " +
			"sof_empleados Sof_Destinatario_Titular, " + 
			"<@SENDERAREA@>, <@SENDERNAME@> ";
			

		private const string cnsWhereOrder = "Where 1=1" +
			" And Sof_documento.id_empleado IN (<@USERS@>) " +
			" And Sof_documento.id_Destinatario_area <@SENDAREAID@> " +
			" And Sof_documento.Estatus LIKE <@STATUSVOLANTE@> " +
			" And Sof_documento.Tipo_Documento_Id LIKE <@DOCUMENTTYPE@> " +
			" And Sof_documento.Tipo_Remitente = <@SENDERTYPE@> " +
			" <@SENDERFIELDAREA@> <@SENDERFIELDAREANAME@> " +
			" And Sof_Destinatario_Titular.id_empleado(+) = Sof_Documento.id_destinatario " +
			" And Sof_Destinatario_Area.id_area(+) = sof_documento.id_destinatario_area " +
			" And (lower(Sof_Documento.asunto) like '%<@TEXTSEARCH@>%' OR lower(Sof_Documento.resumen) like '%<@TEXTSEARCH@>%')";

		private const string cnsWhereOrderSinTurnar = "Where sof_documento.id_empleado in (<@USERS@>) " +
			"and <@AREAID@> = sof_documento.id_remitente_area " +
			"and <@AREAMANAGER@> = sof_documento.id_remitente " +
			"and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area  " +
			"and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario " +
			"and sof_documento_turnar.Documento_ID(+) = sof_documento.Documento_ID " +
			"and sof_documento_turnar.Eliminado(+) = '0'" + " " +
			"and sof_turnado_area.id_area(+) = sof_documento_turnar.id_destinatario_area " +
			"and sof_turnado_nombre.id_empleado(+) = sof_documento_turnar.id_destinatario " +
			"and sof_documento_turnar.Documento_ID is null " ;

		private const string cnsFromOrderSinTurnarCCP = "From " +
			"Sof_Documento, " +
			"sof_areas Sof_Destinatario_Area, " +
			"sof_areas Sof_Turnado_Area, " +
			"sof_empleados Sof_Turnado_Nombre, " +
			"sof_empleados Sof_Destinatario_Titular, Sof_CCPara Sof_Documento_Turnar, " +
			"<@SENDERAREA@>,  <@SENDERMANAGER@> ";

		private const string cnsWhereOrderSinTurnarCCP = "Where sof_documento.id_empleado in (<@USERS@>) " +
			"and <@AREAID@> = sof_documento.id_remitente_area " +
			"and <@AREAMANAGER@> = sof_documento.id_remitente " +
			"and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area " +
			"and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario " +
			"and sof_documento_turnar.Documento_ID(+) = sof_documento.Documento_id " +
			"and sof_documento_turnar.Eliminado(+) = '0'" + " " +
			"and sof_turnado_area.id_empleado(+) = sof_documento_turnar.id_destinatario " +
			"and sof_turnado_nombre.id_empleado(+) = sof_documento_turnar.id_destinatario " +
			"and sof_documento_turnar.Documento_ID is null " ;

		private const string cnsIncludeTurnar =	"Decode(Sof_Documento_Turnar.estatus,0,'Sin/Abrir',1,'Trámite',2,'Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"Sof_Documento_Turnar.Instruccion, Sof_Turnado_Nombre.Nombre TurnadoNombre, Sof_Turnado_Area.Area TurnadoArea ";

		private const string cnsIncludeCCPara =	"Decode(Sof_Documento_CCPara.Estatus,0,'Sin/Abrir',1,'Trámite',2,'Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"('') Instruccion, Sof_Turnado_Nombre.Nombre TurnadoNombre, Sof_Turnado_Area.Area TurnadoArea ";


		private const string cnsOrderOrder = " order by sd.destinatarioAreaId, sd.volante DESC ";

		private const string cnsSelectsWC = "Select " +
			"sof_ccpara.ccpara_id, " +
			"sof_documento.documento_id, " +
			"sof_documento.documento_bis_id, "+
			"sof_documento.asunto, " +
			"sof_documento.resumen, " +
			"sof_documento.id_empleado, " +
			"sof_documento.volante, " +
			"sof_documento.tipo_remitente, " +
			"sof_documento.referencia, " +
			"sof_documento.instruccion, " +
			"sof_documento.fecha_documento_fuente, " +
			"sof_documento.fecha_elaboracion, " +
			"sof_documento.id_destinatario_area destinatarioAreaId, " +
			"sof_destinatario_area.area destinatarioArea, " +
			"('') turnadoNombre, " +
			"sof_destinatario_titular.nombre destinatarioNombre, " +
			"sof_ccpara.estatus statusCcparaId, " +
			"decode(sof_ccpara.estatus,'0','Sin Abrir','1','En Trámite','2','Returnado','3','Concluido') statusCcpara, " +
			"sof_ccpara.id_destinatario_area turnadoAreaId, " +
			"sof_ccpara.id_destinatario turnadoId, " +
			"sof_ccpara_area.area turnadoArea, " +
			"sof_destinatario_titular.clave_area DestinatarioClaveArea, " +
			"sof_ccpara_titular.clave_area TurnadoClaveArea ";

		private const string cnsFromWC = "From "  +
			"sof_documento, " +
			"sof_areas sof_destinatario_area, " +
			"sof_areas sof_ccpara_area, " +
			"sof_empleados sof_destinatario_titular, " +
			"sof_empleados sof_ccpara_titular, "+
			"sof_ccpara, sof_mapeo_recibidos ";

		private const string cnsWhereWC = "Where 1=1 " +
			"and sof_mapeo_recibidos.id_empleado = <@USERS@> " +
			"and sof_ccpara.id_destinatario = sof_mapeo_recibidos.id_destinatario " +
			"and sof_ccpara.eliminado = '0' " +
			"and sof_ccpara.estatus <@STATUS@> " +
			"and sof_ccpara_titular.id_empleado(+) = sof_ccpara.id_destinatario " +
			"and sof_ccpara_area.id_area(+) = sof_ccpara.id_destinatario_area " +
			"and sof_documento.documento_id = sof_ccpara.documento_id " +
			"And sof_mapeo_recibidos.fecha_inicio <= TRUNC(sof_documento.fecha_elaboracion) " +
			"And sof_mapeo_recibidos.fecha_fin >= TRUNC(sof_documento.fecha_elaboracion) " +
			"and TRUNC(sof_documento.fecha_elaboracion) >= to_date('<@FROMDATE@>') " +
			"and TRUNC(sof_documento.fecha_elaboracion) <= to_date('<@TODATE@>') " +
			"and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area " +
			"and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario ";

		private const string cnsOrderWC = " order by destinatarioAreaId, turnadoAreaId, volante";


		private const string cnsExternalSenderSQL = "SELECT " +
			"Sof_Documento.documento_id, " +
			"sof_documento.asunto, " + 
			"sof_documento.anexo, " +
			"sof_documento.resumen, " +
			"sof_documento.id_empleado, " +
			"sof_documento.volante, " +
			"Sof_Documento.tipo_remitente, " +
			"Sof_Documento.referencia, " +
			"Sof_Documento.fecha_documento_fuente, " +
			"Sof_Documento.fecha_elaboracion, " +
			"sof_documento.documento_bis_id, " +
			"sof_remitente_externo.Dependencia RemitenteArea, " +
			"sof_documento.tipo_remitente, " +
			"sof_remitente_externo_titular.Nombre RemitenteNombre, " + 
			"sof_documento.id_destinatario_area destinatarioAreaId, " +
			"Sof_documento.id_remitente_area, " +
			"sof_destinatario_area.area destinatarioArea, " +
			"sof_destinatario_titular.nombre destinatarioNombre, " +
			"sof_documento.Estatus StatusVolante, " +
			"sof_destinatario_titular.clave_empleado " +
			"FROM Sof_Documento, " +
			"sof_areas Sof_Destinatario_Area, " +
			"sof_empleados Sof_Destinatario_Titular, " +
			"sof_remitente_externo, " +
			"sof_remitente_externo_titular " +
			"WHERE Sof_documento.id_empleado = <@USERID@> " +
			"AND Sof_documento.Estatus LIKE <@STATUS@> " +
			"AND Sof_documento.id_remitente_area LIKE <@SENDERID@> " +
			"AND Sof_documento.Tipo_Remitente = 'E' " +
			"<@FROMDATE@> " +
			"<@TODATE@> " +
			"AND Sof_Remitente_Externo.remitente_externo_id(+) = Sof_Documento.id_remitente_Area " +
			"AND Sof_Remitente_Externo_Titular.Remitente_Externo_Titular_ID(+) = Sof_Documento.id_remitente " +
			"AND Sof_Destinatario_Titular.id_empleado(+) = Sof_Documento.id_destinatario " +
			"AND Sof_Destinatario_Area.id_area(+) = sof_documento.id_destinatario_Area " +
			"ORDER BY Sof_Documento.id_remitente_area, Sof_Documento.volante ";

		private const string cnsInternalSenderSQL = "SELECT " +
			"Sof_Documento.documento_id, " +
			"sof_documento.asunto, " +
			"sof_documento.anexo, " +
			"sof_documento.resumen, " +
			"sof_documento.id_empleado, " + 
			"sof_documento.volante, " + 
			"Sof_Documento.tipo_remitente, " + 
			"Sof_Documento.referencia, " + 
			"Sof_Documento.fecha_documento_fuente, " + 
			"Sof_Documento.fecha_elaboracion, " + 
			"sof_documento.documento_bis_id, " + 
			"Sof_Remitente_Area.area RemitenteArea, " + 
			"Sof_Remitente_Titular.Nombre RemitenteNombre, " + 
			"sof_documento.id_destinatario_area destinatarioAreaId, " + 
			"Sof_Documento.id_remitente_area, " + 
			"sof_destinatario_area.area destinatarioArea, " + 
			"sof_destinatario_titular.nombre destinatarioNombre, " + 
			"sof_documento.Estatus StatusVolante  " +
			"FROM " + 			
			"Sof_Documento, " +
			"sof_areas Sof_Destinatario_Area, " +
			"sof_empleados Sof_Destinatario_Titular,  " +
			"sof_areas Sof_Remitente_Area, " +
			"sof_empleados Sof_Remitente_titular  " +
			"WHERE 	" +
			"Sof_documento.id_empleado = <@USERID@>  " + 
			"AND Sof_documento.Estatus LIKE <@STATUS@>  " +
			"AND Sof_documento.Tipo_Remitente = 'I' " +
			"<@FROMDATE@> " +
			"<@TODATE@> " +
			"AND Sof_Documento.id_remitente_area like <@SENDERID@> " +
			"AND Sof_Documento.id_remitente = Sof_Remitente_titular.id_empleado(+) " +
			"AND Sof_Documento.id_remitente_Area = sof_remitente_area.id_area(+) " +
			"AND Sof_Destinatario_Titular.id_empleado(+) = Sof_Documento.id_destinatario " + 
			"AND Sof_Destinatario_Area.id_area(+) = sof_documento.id_destinatario_area " +
			"ORDER BY Sof_Documento.id_remitente_area, Sof_Documento.volante ";

		public Documents()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static DataSet GetDocument()
		{
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, "select * from sof_documento");
			return ds;

		}

		public static DataSet GetDocumentsRelations(int nDocumentId)
		{

			string sSqlFromArea = "sof_areas sof_remitente_area, ";
			string sSqlFromTitular = "sof_empleados sof_remitente_interno_titular, sof_puesto sof_remitente_puesto,";

			string sSqlToArea = "sof_areas sof_destinatario, ";
			string sSqlToTitular = "sof_empleados sof_destinatario_titular, sof_puesto sof_destinatario_puesto,";

			string sSql		=	"select sof_documento.anexo, sof_documento.resumen, sof_documento.referencia, sof_documento.fecha_documento_fuente," +
				"sof_documento.asunto, sof_documento.requiere_tramite, sof_destinatario.area toArea, sof_destinatario_titular.nombre toName," +
				"sof_destinatario_puesto.descripcion toJob, sof_documento.volante, sof_documento.tipo_documento_id, sof_documento.documento_bis_id," +
				"sof_documento.id_destinatario_area toAreaId, sof_destinatario_titular.id_empleado toTitularId, sof_documento.id_firma, " +
				"sof_documento.fecha_elaboracion, sof_remitente_area.area toArea, sof_tipo_documento.tipo_documento, sof_documento.tipo_remitente, " +
				"sof_documento.estatus, sof_remitente_interno_titular.tipo_usuario TipoUsuarioFrom," + 
				"sof_destinatario_titular.tipo_usuario TipoUsuarioTo, ";


			string sDecode	=	"	decode(sof_documento.tipo_remitente, 'E', sof_remitente_externo.dependencia, sof_remitente_area.area) fromArea, " +
				"	decode(sof_documento.tipo_remitente, 'E', sof_remitente_externo.remitente_externo_id, sof_remitente_area.id_area) fromAreaId, " +
				"	decode(sof_documento.tipo_remitente, 'E', sof_remitente_externo_titular.nombre, sof_remitente_interno_titular.nombre) fromName, " +
				"	decode(sof_documento.tipo_remitente, 'E', sof_remitente_externo_titular.remitente_externo_titular_id, sof_remitente_interno_titular.id_empleado) fromTitularId, " +
				"	decode(sof_documento.tipo_remitente, 'E', sof_remitente_externo_titular.puesto, sof_remitente_puesto.descripcion) fromJob, " +
				//								" sof_destinatario_titular.clave_area toClaveArea, " +
				" sof_firma.apellidoNombre signName, " +
				" sof_remitente_area.cve_area fromClaveArea, " +
				" sof_destinatario.cve_area toClaveArea, " +
				" sof_tipo_solicitud.tipo_solicitud, " +
				" sof_tipo_atencion.tipo_atencion, " +
				" sof_documento.id_tipo_solicitud, " +
				" sof_documento.id_tipo_atencion, " +
				" sof_documento.fecha_atencion, " + 
				" sof_documento.nombre_proyecto ";
			string sFrom	=	"FROM sof_documento, "+
				"sof_remitente_externo, "+
				"sof_remitente_externo_titular, " +
				sSqlFromArea + 
				sSqlFromTitular + 
				sSqlToArea +
				sSqlToTitular +
				"sof_tipo_documento, " +
				"sof_empleados sof_firma, " +
				"sof_tipo_solicitud, " +
				"sof_tipo_atencion ";

			string sWhere	=	"WHERE sof_documento.documento_id = " + nDocumentId + " " +
				"and sof_remitente_externo.remitente_externo_id(+) = sof_documento.id_remitente_area " +
				"and sof_remitente_externo_titular.remitente_externo_titular_id(+) = sof_documento.id_remitente " +
				"and sof_remitente_area.id_area(+) = sof_documento.id_remitente_area " +
				"and sof_remitente_interno_titular.id_empleado(+) = sof_documento.id_remitente " +
				"and sof_remitente_puesto.puesto_id(+) = sof_remitente_interno_titular.id_puesto " +
				"and sof_destinatario.id_area = sof_documento.id_destinatario_area " +
				//"and sof_destinatario_titular.id_empleado = sof_documento.id_firma " +
                "and sof_destinatario_titular.id_empleado = sof_documento.id_destinatario " +
                "and sof_destinatario_puesto.puesto_id = sof_destinatario_titular.id_puesto " +
				"and sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id " +
				"and sof_firma.id_empleado = sof_documento.id_firma " +
				"and sof_tipo_solicitud.id_tipo_solicitud = sof_documento.id_tipo_solicitud " + 
				"and sof_tipo_atencion.id_tipo_atencion = sof_documento.id_tipo_atencion ";

			sSql += (sDecode + sFrom + sWhere);
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;

		}

		public static DataSet GetDocument(string sWhere, string sOrder)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.Append("select * from sof_documento ");

			if (sWhere != "")
				sSql.Append(" where " + sWhere);
			if (sOrder != "")
				sSql.Append(" order by " + sOrder);

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql.ToString());
			return ds;

		}
		
		public static int Document_Create(string sMailType, int nFromAreaId, int nToAreaId, int nFromTitularId, int nToTitularId, 
			int nDocumentTypeId, string sDate, string sReference, string sSubject, string sAttached,
			string sSummary, string sStatus, int nDocumentBisId, int nUserCreate, string sRequire, 
			int nVolante, int nFirmaId, int nSolicitudId, int nAtencionId, string sFechaAtencion, string sNombreProyecto)
		{

			OracleParameter [] oParam = {new OracleParameter("pMailType", OracleType.VarChar),
											new OracleParameter("pFromAreaId", null),
											new OracleParameter("pToAreaId", null),
											new OracleParameter("pFromTitularId", null),
											new OracleParameter("pToTitularId", null),
											new OracleParameter("pDocumentTypeId", null),
											new OracleParameter("pDate",OracleType.VarChar),
											new OracleParameter("pReference",OracleType.VarChar, 60),
											new OracleParameter("pSubject",OracleType.VarChar, 1024),
											new OracleParameter("pAnexo",OracleType.VarChar, 1024),
											new OracleParameter("pSummary",OracleType.VarChar, 4000),
											new OracleParameter("pStatus",OracleType.VarChar),
											new OracleParameter("pDocumentBisId", null),
											new OracleParameter("pUserCreate", null),
											new OracleParameter("pStepRequire",OracleType.VarChar),
											new OracleParameter("pVolante", null),
											new OracleParameter("pFirmaId", null),
											new OracleParameter("pTipoSolicitudId", null),
											new OracleParameter("pTipoAtencionId", null),
											new OracleParameter("pFechaAtencion",OracleType.VarChar, 10),
											new OracleParameter("pNombreProyecto",OracleType.VarChar, 100),
											new OracleParameter("pResult", null)
										};

			oParam[0].Value = sMailType;
			oParam[1].Value = nFromAreaId;
			oParam[2].Value = nToAreaId;
			oParam[3].Value = nFromTitularId;
			oParam[4].Value = nToTitularId;
			oParam[5].Value = nDocumentTypeId;
			oParam[6].Value = sDate;
			oParam[7].Value = sReference == "" ? " " : sReference;
			oParam[8].Value = sSubject;
			oParam[9].Value = sAttached == "" ? " " : sAttached;
			oParam[10].Value = sSummary == "" ? " " : sSummary;
			oParam[11].Value = sStatus;
			oParam[12].Value = nDocumentBisId;
			oParam[13].Value = nUserCreate;
			oParam[14].Value = sRequire;
			oParam[15].Value = nVolante;
			oParam[16].Value = nFirmaId;
			oParam[17].Value = nSolicitudId;
			oParam[18].Value = nAtencionId;
			oParam[19].Value = sFechaAtencion;
			oParam[20].Value = sNombreProyecto;

			oParam[21].Direction = ParameterDirection.InputOutput;
			oParam[21].Value = 0;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Create",oParam));

			object obj = oParam[21].Value;
			nVal = int.Parse(obj.ToString());
			return nVal;
		}

		public static int Document_Update(int nDocumentId, string sMailType, int nFromAreaId, int nToAreaId, int nFromTitularId, int nToTitularId, 
			int nDocumentTypeId, string sDate, string sReference, string sSubject, string sAttached, 
			string sSummary, string sStatus, int nDocumentBisId, int nUserCreate, 
			string sStepRequire, int nFirmaId, int nSolicitudId, int nAtencionId, string sFechaAtencion, string sNombreProyecto)
		{

			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null),
											new OracleParameter("pMailType",OracleType.VarChar),
											new OracleParameter("pFromAreaId", null),
											new OracleParameter("pToAreaId", null),
											new OracleParameter("pFromTitularId", null),	
											new OracleParameter("pToTitularId", null),
											new OracleParameter("pDocumentTypeId", null),
											new OracleParameter("pDate",OracleType.VarChar),
											new OracleParameter("pReference",OracleType.VarChar, 60),
											new OracleParameter("pSubject",OracleType.VarChar, 1024),
											new OracleParameter("pAttached",OracleType.VarChar, 1024),
											new OracleParameter("pSummary",OracleType.VarChar, 4000),
											new OracleParameter("pStatus",OracleType.VarChar),
											new OracleParameter("pDocumentBisId", null),
											new OracleParameter("pUserCreate", null),
											new OracleParameter("pStepRequire",OracleType.VarChar),
											new OracleParameter("pFirmaId", null),
											new OracleParameter("pTipoSolicitudId", null),
											new OracleParameter("pTipoAtencionId", null),
											new OracleParameter("pFechaAtencion",OracleType.VarChar, 10),
											new OracleParameter("pNombreProyecto",OracleType.VarChar, 100)
										};

			oParam[0].Value = nDocumentId;
			oParam[1].Value = sMailType;
			oParam[2].Value = nFromAreaId;
			oParam[3].Value = nToAreaId;
			oParam[4].Value = nFromTitularId;
			oParam[5].Value = nToTitularId;
			oParam[6].Value = nDocumentTypeId;
			oParam[7].Value = sDate;
			oParam[8].Value = sReference == "" ? " " : sReference;
			oParam[9].Value = sSubject == "" ? " " : sSubject;
			oParam[10].Value = sAttached;
			oParam[11].Value = sSummary == "" ? " " : sSummary;
			oParam[12].Value = sStatus;
			oParam[13].Value = nDocumentBisId;
			oParam[14].Value = nUserCreate;
			oParam[15].Value = sStepRequire;
			oParam[16].Value = nFirmaId;
			oParam[17].Value = nSolicitudId;
			oParam[18].Value = nAtencionId;
			oParam[19].Value = sFechaAtencion;
			oParam[20].Value = sNombreProyecto;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Update",oParam));
			return nVal;
		}

		public static int DocumentDelete(int nDocumentId) 
		{

			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null)};
			oParam[0].Value = nDocumentId;

			int nVal =	Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.DocumentDelete",oParam));
			return nVal;
		}


		public static int DocumentSetStatus(int nDocumentoTurnarId, string sStatus)
		{
			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null),
											new OracleParameter("pStatus", null)};

			oParam[0].Value = nDocumentoTurnarId;
			oParam[1].Value = sStatus;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.DocumentSetStatus",oParam));
			return nVal;
		}

		public static string getCascadeResp(int nDocumentoTurnarId)
		{
			string sSQl = "select max(regla.responder_cascada) from sof_regla regla where id_empleado in (select id_empleado from sof_documento where documento_id = " + nDocumentoTurnarId.ToString()+ ")";

			object oCascadeResp = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQl);
			
			if (! (oCascadeResp == null) )
				return oCascadeResp.ToString();

			return "No";
		}

        //public static void WriteToLog(string Mensaje)
        //{
        //    string path = @"c:\Temp\LogCGD.txt";
        //    // This text is added only once to the file.
        //    if (!File.Exists(path))
        //    {
        //        // Create a file to write to.
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            sw.WriteLine(Mensaje);
        //        }
        //    }
        //    else
        //    {
        //        using (StreamWriter sw = File.AppendText(path))
        //        {
        //            sw.WriteLine(Mensaje);
        //        }
        //    }
        //}

        public static DataSet DocumentsPendingsSend(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
			string sStatus, string sStatusReceive, string sAreaId, 
			string sTextSearch, bool bIncludeTurnar, bool bIncludeCC)
		{

			XMLDOM oXMLDOM = new XMLDOM();
			StringBuilder oSQL = new StringBuilder();
			if (bIncludeTurnar && bIncludeCC)
				oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByArea"));
			else if (bIncludeTurnar)
				oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByTurnados"));
			else if (bIncludeCC)
				oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByCCPara"));
			else
				oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByArea"));
			
		
			oSQL.Replace("<@TURNARSTATUS@>", GetReceiveStatus( nUserId.ToString(), sStatusReceive));
			oSQL.Replace("<@CCPARASTATUS@>", GetCCParaStatus( nUserId.ToString(), sStatusReceive));

			if (dElaborationDateFrom != String.Empty && dElaborationDateTo != String.Empty) 
			{
				oSQL.Replace("<@FROMDATE@>", dElaborationDateFrom);
				oSQL.Replace("<@TODATE@>", dElaborationDateTo);
			}
			else 
			{
				oSQL.Replace("<@FROMDATE@>", "01/01/2004");
				oSQL.Replace("<@TODATE@>", DateTime.Now.ToShortDateString());
			}

			string[] aSend = Users.GetUserRegistry(nUserId).Split(',');
			string sSendId = Users.GetUserRegistry(nUserId);
			string sSendAreaId = string.Empty;

			//for (int i = 0; i < aSend.Length; i++)
			//{
			//	sSendId += aSend[i] + ",";
			//}

			for (int i = 1; i < aSend.Length; i += 2)
			{
				sSendAreaId += aSend[i] + ",";
			}

			oSQL.Replace("<@USERSID@>", sSendId.TrimEnd(','));
			//
			if (sSendAreaId.Substring(0, 1) == "%")
				oSQL.Replace("<@SENDAREAID@>", " LIKE '%'");
			else
				oSQL.Replace("<@SENDAREAID@>", " IN (" + sSendAreaId.TrimEnd(',') + ")");
			//
			oSQL.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower() );

			if (sAreaId != "*")
			{
				oSQL.Replace("<@ADDRESSEEID@>", sAreaId);
			}
			else
			{
				oSQL.Replace("<@ADDRESSEEID@>", "%");
			}
			if (sStatus == "Todos")
			{
				oSQL.Replace("<@STATUSDOCUMENT@>", "%");
			}
			else
			{
				oSQL.Replace("<@STATUSDOCUMENT@>", sStatus);
			}
			
			oSQL.Replace("\n", " ");
			oSQL.Replace("\t", " ");
			oSQL.Replace("\r", " ");

			// WriteToLog("Inicia ejecución de QRY DataSet: " + DateTime.Now.ToString());
			//WriteToLog("Querie: " + oSQL.ToString());
			DataSet ds;

            try
			{
			ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());

			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			//WriteToLog("Termina ejecución de QRY DataSet: " + DateTime.Now.ToString());

			return ds;

		}


		public static OracleDataReader DocumentsPendingsSend(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
			string sStatus, string sStatusReceive, string sAreaId, string sOrder, 
			bool bIncludeTurnar, bool bIncludeCC, int nDocumentType, string sVolante, 
			string sTextSearch, string sOrderBy)
		{

			StringBuilder sSqlUsers = new StringBuilder();

			XMLDOM oXMLDOM = new XMLDOM();
			StringBuilder oSQL = new StringBuilder();
			oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByVolante"));

			if (sStatus == "Todos")
			{
				oSQL.Replace("<@STATUSVOLANTE@>", "%");
			}
			else
			{
				oSQL.Replace("<@STATUSVOLANTE@>", sStatus);
			}
			if (nDocumentType == 0)
			{
				oSQL.Replace("<@DOCUMENTTYPE@>", "%");
			}
			else
			{
				oSQL.Replace("<@DOCUMENTTYPE@>", nDocumentType.ToString());
			}
			sSqlUsers.Append(Users.GetUserRegistry(nUserId));

			oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

			oSQL.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

			if (sAreaId == "*")
			{
				oSQL.Replace("<@SENDAREAID@>", "LIKE '%'");
			}
			else
			{
				oSQL.Replace("<@SENDAREAID@>", " = " + sAreaId);
			}
			oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

			oSQL.Replace("<@FROMDATE@>", dElaborationDateFrom);
			oSQL.Replace("<@TODATE@>", dElaborationDateTo);
           // WriteToLog("Inicia ejecución de QRY OracleDataReader: " + DateTime.Now.ToString());
            //WriteToLog("Querie: " + oSQL.ToString());
            OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());
            //WriteToLog("Termina ejecución de QRY OracleDataReader: " + DateTime.Now.ToString());
            return dr;
		
		}

        public static OracleDataReader FilesPendinsSend(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
            string sStatus, string sStatusReceive, string sAreaId, string sOrder,
            bool bIncludeTurnar, bool bIncludeCC, int nDocumentType, string sVolante,
            string sTextSearch, string sOrderBy)
        {

            StringBuilder sSqlUsers = new StringBuilder();

            XMLDOM oXMLDOM = new XMLDOM();
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByVolanteFile"));

            if (sStatus == "Todos")
            {
                oSQL.Replace("<@STATUSVOLANTE@>", "%");
            }
            else
            {
                oSQL.Replace("<@STATUSVOLANTE@>", sStatus);
            }
            if (nDocumentType == 0)
            {
                oSQL.Replace("<@DOCUMENTTYPE@>", "%");
            }
            else
            {
                oSQL.Replace("<@DOCUMENTTYPE@>", nDocumentType.ToString());
            }
            sSqlUsers.Append(Users.GetUserRegistry(nUserId));

            oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

            oSQL.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

            if (sAreaId == "*")
            {
                oSQL.Replace("<@SENDAREAID@>", "LIKE '%'");
            }
            else
            {
                oSQL.Replace("<@SENDAREAID@>", " = " + sAreaId);
            }
            oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

            oSQL.Replace("<@FROMDATE@>", dElaborationDateFrom);
            oSQL.Replace("<@TODATE@>", dElaborationDateTo);
            // WriteToLog("Inicia ejecución de QRY OracleDataReader: " + DateTime.Now.ToString());
            //WriteToLog("Querie: " + oSQL.ToString());
            OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());
            //WriteToLog("Termina ejecución de QRY OracleDataReader: " + DateTime.Now.ToString());
            return dr;

        }

        public static OracleDataReader FilesAreaPendinsSend(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
          string sStatus, string sStatusReceive, string sAreaId, string sOrder,
          bool bIncludeTurnar, bool bIncludeCC, int nDocumentType, string sVolante,
          string sTextSearch, string sOrderBy)
        {
            StringBuilder sSqlUsers = new StringBuilder();

            XMLDOM oXMLDOM = new XMLDOM();
            StringBuilder oSQL = new StringBuilder();
            if (bIncludeTurnar && bIncludeCC)
                oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByArea"));
            else if (bIncludeTurnar)
                oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByTurnados"));
            else if (bIncludeCC)
                oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByCCPara"));
            else
                oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByArea"));


            oSQL.Replace("<@TURNARSTATUS@>", GetReceiveStatus(nUserId.ToString(), sStatusReceive));
            oSQL.Replace("<@CCPARASTATUS@>", GetCCParaStatus(nUserId.ToString(), sStatusReceive));

            if (dElaborationDateFrom != String.Empty && dElaborationDateTo != String.Empty)
            {
                oSQL.Replace("<@FROMDATE@>", dElaborationDateFrom);
                oSQL.Replace("<@TODATE@>", dElaborationDateTo);
            }
            else
            {
                oSQL.Replace("<@FROMDATE@>", "01/01/2004");
                oSQL.Replace("<@TODATE@>", DateTime.Now.ToShortDateString());
            }

            string[] aSend = Users.GetUserRegistry(nUserId).Split(',');
            string sSendId = Users.GetUserRegistry(nUserId);
            string sSendAreaId = string.Empty;

            //for (int i = 0; i < aSend.Length; i++)
            //{
            //	sSendId += aSend[i] + ",";
            //}

            for (int i = 1; i < aSend.Length; i += 2)
            {
                sSendAreaId += aSend[i] + ",";
            }

            oSQL.Replace("<@USERSID@>", sSendId.TrimEnd(','));
            //
            if (sSendAreaId.Substring(0, 1) == "%")
                oSQL.Replace("<@SENDAREAID@>", " LIKE '%'");
            else
                oSQL.Replace("<@SENDAREAID@>", " IN (" + sSendAreaId.TrimEnd(',') + ")");
            //
            oSQL.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

            if (sAreaId != "*")
            {
                oSQL.Replace("<@ADDRESSEEID@>", sAreaId);
            }
            else
            {
                oSQL.Replace("<@ADDRESSEEID@>", "%");
            }
            if (sStatus == "Todos")
            {
                oSQL.Replace("<@STATUSDOCUMENT@>", "%");
            }
            else
            {
                oSQL.Replace("<@STATUSDOCUMENT@>", sStatus);
            }

            oSQL.Replace("\n", " ");
            oSQL.Replace("\t", " ");
            oSQL.Replace("\r", " ");

            // WriteToLog("Inicia ejecución de QRY DataSet: " + DateTime.Now.ToString());
            //WriteToLog("Querie: " + oSQL.ToString());

            OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());
            
            try
            {
                OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            //WriteToLog("Termina ejecución de QRY DataSet: " + DateTime.Now.ToString());

            return dr;


        }

        public static ICollection DocumentsPendingsSendJAS(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
            string sStatus, string sStatusReceive, string sAreaId, string sOrder,
            bool bIncludeTurnar, bool bIncludeCC, int nDocumentType, string sVolante,
            string sTextSearch, string sOrderBy)
        {

            StringBuilder sSqlUsers = new StringBuilder();

            XMLDOM oXMLDOM = new XMLDOM();
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryDocumentsByVolante"));

            if (sStatus == "Todos")
                oSQL.Replace("<@STATUSVOLANTE@>", "%");
            else
                oSQL.Replace("<@STATUSVOLANTE@>", sStatus);

            if (nDocumentType == 0)
                oSQL.Replace("<@DOCUMENTTYPE@>", "%");
            else
                oSQL.Replace("<@DOCUMENTTYPE@>", nDocumentType.ToString());

            sSqlUsers.Append(Users.GetUserRegistry(nUserId));

            oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

            oSQL.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

            if (sAreaId == "*")
                oSQL.Replace("<@SENDAREAID@>", "LIKE '%'");
            else
                oSQL.Replace("<@SENDAREAID@>", " = " + sAreaId);

            oSQL.Replace("<@USERS@>", sSqlUsers.ToString());

            oSQL.Replace("<@FROMDATE@>", dElaborationDateFrom);
            oSQL.Replace("<@TODATE@>", dElaborationDateTo);
           // WriteToLog("Inicia ejecución de QRY DataSet JAS: " + DateTime.Now.ToString());
            //WriteToLog("Querie: " + oSQL.ToString());
            DataSet dr = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, oSQL.ToString());
            DataTable dt = dr.Tables[0];
            DataView dv = new DataView(dt);
          //  WriteToLog("Termina ejecución de QRY DataSet JAS: " + DateTime.Now.ToString());
            return (ICollection)dv;
        }

        public static OracleDataReader GetDocumentsSend(int nUserId, string dElaborationDateFrom, string dElaborationDateTo,
			string sStatus, string sStatusReceive, string sAreaId, string sOrder, 
			bool bIncludeTurnar, bool bIncludeCC, int nDocumentType, string sVolante, 
			string sTextSearch, string sOrderBy)
		{
			string sElaborationDate = String.Empty;
			string sTmp = String.Empty;
			string sAlias = String.Empty;
			string sIncludeArea = String.Empty;

			StringBuilder sSql = new StringBuilder();
			StringBuilder sSqlUsers = new StringBuilder();

			if (dElaborationDateFrom != "" && dElaborationDateTo != "")
				sElaborationDate = " and trunc(sof_documento.fecha_elaboracion) >= to_date('" + dElaborationDateFrom + "') and trunc(fecha_elaboracion) <= to_date('" + dElaborationDateTo + "') ";

			sSqlUsers.Append(Users.GetUserRegistry(nUserId));

			// Armar SQL para Remitentes Internos
			sSql.Append("Select * From (");
			sSql.Append("Select <@DOCUMENT@><@TURN@> From ( " );
			sTmp =	cnsColumnsOrder.Replace("<@SENDERAREACOLUMN@>", "Sof_Remitente_Area.Area RemitenteArea");
			sTmp = sTmp.Replace("<@SENDERNAMECOLUMN@>", "Sof_Remitente_Titular.Nombre RemitenteNombre");
			sSql.Append(sTmp + " \n");

			sTmp = cnsFromOrder.Replace("<@SENDERAREA@>","sof_areas sof_remitente_area ");
			sTmp = sTmp.Replace("<@SENDERNAME@>","sof_empleados");
			sSql.Append(sTmp + " \n ");

			if (sStatus == "Todos")
				sTmp = cnsWhereOrder.Replace("<@STATUSVOLANTE@>", "'%'");
			else
				sTmp = cnsWhereOrder.Replace("<@STATUSVOLANTE@>", "'" + sStatus + "'");

			if (nDocumentType == 0)
				sTmp = sTmp.Replace("<@DOCUMENTTYPE@>", "'%'");
			else
				sTmp = sTmp.Replace("<@DOCUMENTTYPE@>", nDocumentType.ToString());

			sTmp = sTmp.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

			sTmp = sTmp.Replace("<@SENDERTYPE@>", "'I'");
		
			//sTmp = sTmp.Replace("<@USERS@>", sSqlUsers.ToString());

			string[] aSend = sSqlUsers.ToString().Split(',');
			string sSendId = string.Empty;
			string sSendAreaId = string.Empty;
			for (int i=0; i < aSend.Length; i+=2)
			{
				sSendId += aSend[i] + ",";
			}

			for (int i=1; i < aSend.Length; i+=2)
			{
				sSendAreaId += aSend[i] + ",";
			}
			sTmp = sTmp.Replace("<@USERS@>", sSendId.TrimEnd(','));

			if (sSendAreaId.Substring(0,1) == "%")
				sTmp = sTmp.Replace("<@SENDAREAID@>", " LIKE '%'");
			else
				sTmp = sTmp.Replace("<@SENDAREAID@>", " IN (" + sSendAreaId.TrimEnd(',') + ")");


			sTmp = sTmp.Replace("<@SENDERFIELDAREA@>"," And sof_remitente_area.id_area(+) = sof_documento.id_remitente_area");
			sTmp = sTmp.Replace("<@SENDERFIELDAREANAME@>", " And sof_remitente_titular.id_empleado(+) = Sof_Documento.id_remitente");
			sTmp += sElaborationDate;

	
			// Armar SQL para Remitentes Externos
			sSql.Append(sTmp + " \n " + " UNION ALL " + " \n ");
			sTmp =	cnsColumnsOrder.Replace("<@SENDERAREACOLUMN@>", "Sof_Remitente_Externo.Dependencia RemitenteArea");
			sTmp = sTmp.Replace("<@SENDERNAMECOLUMN@>", "Sof_Remitente_Externo_Titular.Nombre RemitenteNombre");
			sSql.Append(sTmp + " \n");

			sTmp = cnsFromOrder.Replace("<@SENDERAREA@>"," Sof_Remitente_Externo ");
			sTmp = sTmp.Replace("<@SENDERNAME@>"," Sof_Remitente_Externo_titular ");
			sSql.Append(sTmp + " \n ");

			if (sStatus == "Todos")
				sTmp = cnsWhereOrder.Replace("<@STATUSVOLANTE@>", "'%'");
			else
				sTmp = cnsWhereOrder.Replace("<@STATUSVOLANTE@>", "'" + sStatus + "'");

			if (nDocumentType == 0)
				sTmp = sTmp.Replace("<@DOCUMENTTYPE@>", "'%'");
			else
				sTmp = sTmp.Replace("<@DOCUMENTTYPE@>", nDocumentType.ToString());

			sTmp = sTmp.Replace("<@TEXTSEARCH@>", sTextSearch.ToLower());

			sTmp = sTmp.Replace("<@SENDERTYPE@>", "'E'");
			//sTmp = sTmp.Replace("<@USERS@>", sSqlUsers.ToString());

			sTmp = sTmp.Replace("<@USERS@>", sSendId.TrimEnd(','));
			if (sSendAreaId.Substring(0,1) == "%")
				sTmp = sTmp.Replace("<@SENDAREAID@>", " LIKE '%'");
			else
				sTmp = sTmp.Replace("<@SENDAREAID@>", " IN (" + sSendAreaId.TrimEnd(',') + ")");

			sTmp = sTmp.Replace("<@SENDERFIELDAREA@>","And Sof_Remitente_Externo.Remitente_Externo_ID(+) = Sof_Documento.id_remitente_area");
			sTmp = sTmp.Replace("<@SENDERFIELDAREANAME@>", "And Sof_Remitente_Externo_Titular.Remitente_Externo_Titular_ID(+) = Sof_Documento.id_remitente");
			sTmp += sElaborationDate;

			if (sStatus == "Pendiente")
				sSql.Append(sTmp + " ) SD, sof_documento_relacionado WHERE sof_documento_relacionado.documento_id = sd.documento_id and (sof_documento_relacionado.estatus = sd.statusvolante  or sof_documento_relacionado.estatus = 'Sin Tramite') " + "\n");
			else
				sSql.Append(sTmp + " ) SD, sof_documento_relacionado WHERE sof_documento_relacionado.documento_id = sd.documento_id(+) and sof_documento_relacionado.estatus = sd.statusvolante " + "\n");

			sSql = sSql.Replace("<@DOCUMENT@>","SD.*");
			
			sSql = sSql.Replace("<@TURN@>", ", ('') TurnadoArea, decode(statusvolante,'Pendiente',sysdate,'Tramite', sysdate, 'Concluido',fecha_hasta) fecha_corte");
			if (sOrderBy == "QRY")
				sSql.Append(cnsOrderOrder);
			else
				sSql.Append(" order by sd.volante ");

			if (bIncludeTurnar && bIncludeCC)
			{
				sSql.Append(" ) sdaux ");
			}
			else if (bIncludeTurnar) 
			{
				sSql.Append(" ) sdaux where sdaux.documento_id in (select documento_id from sof_documento_turnar where eliminado = '0')");
			} 
			else if (bIncludeCC) 
			{
				sSql.Append(" ) sdaux where sdaux.documento_id in (select documento_id from sof_ccpara where eliminado = '0')");
			}
			else
			{
				sSql.Append(" ) sdaux ");
			}


			OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return dr;
		}

		

		public static DataSet DocumentsPendingsByArea(int nUserId, string dElaborationDateFrom, string dElaborationDateTo, string dDocumentDateFrom, string dDocumentDateTo, string sStatus)
		{

			string sElaborationDate = string.Empty;
			string sDocumentDate = string.Empty;
			string sOrder = " order by sof_documento.volante ";
			StringBuilder sSql = new StringBuilder();
			StringBuilder sSqlSender = new StringBuilder();
			StringBuilder sSqlAddreesse = new StringBuilder();
			StringBuilder sSqlAlternate = new StringBuilder();

			if (dElaborationDateFrom != "" && dElaborationDateTo != "")
				sElaborationDate = " and trunc(fecha_elaboracion) >= to_date('" + dElaborationDateFrom + "') and trunc(fecha_elaboracion) <= to_date('" + dElaborationDateTo + "')";

			if (dDocumentDateFrom != "" && dDocumentDateTo != "")
				sDocumentDate = "and trunc(fecha_documento_fuente) >= to_date('" + dDocumentDateFrom + "') and trunc(fecha_documento_fuente) <= to_date('" + dDocumentDateTo + "')";

			sSqlSender.Append("sof_areas sof_remitente_area, ");
			sSqlAddreesse.Append("sof_areas sof_destinatario_area, ");
			sSqlAlternate.Append("sof_areas sof_turnado_area, ");

			sSql.Append("select sof_documento.documento_id, sof_documento.asunto, sof_documento.anexo, sof_documento.resumen, ");
			sSql.Append("sof_documento.volante, sof_documento.tipo_remitente, sof_documento.referencia, sof_documento.fecha_documento_fuente, ");
			sSql.Append("sof_documento.fecha_elaboracion, ");
			sSql.Append("sof_turnado_titular.nombre turnadoNombre, sof_turnado_area.area turnadoArea,");
			sSql.Append("decode(sof_documento.tipo_remitente,'I',sof_remitente_area.area, sof_remitente_externo_area.dependencia) remitenteArea,");
			sSql.Append("decode(sof_documento.tipo_remitente,'I',sof_remitente_titular.nombre, sof_remitente_externo_titular.nombre) remitenteNombre, ");
			sSql.Append("sof_destinatario_area.area destinatarioArea, sof_destinatario_titular.nombre destinatarioNombre ");

			sSql.Append("from sof_documento, sof_tipo_documento, ");
			sSql.Append(sSqlSender.ToString() + " " + sSqlAddreesse.ToString() + " " + sSqlAlternate.ToString()+ " ");
			sSql.Append("sof_empleados sof_remitente_titular,");
			sSql.Append("sof_remitente_externo sof_remitente_externo_area, sof_remitente_externo_titular, ");
			sSql.Append("sof_documento_turnar,");
			sSql.Append("sof_empleados sof_destinatario_titular ");
						
			sSql.Append("where sof_documento.id_empleado = " + nUserId + " " + sElaborationDate + " " +  sDocumentDate + " ");
			sSql.Append("and sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id ");
			sSql.Append("and sof_documento.estatus = '" + sStatus + "' ");
			sSql.Append("and sof_remitente_area.id_area(+) = sof_documento.id_remitente_area ");
			sSql.Append("and sof_remitente_titular.id_empleado(+) = sof_documento.id_remitente ");
			sSql.Append("and sof_remitente_externo_area.remitente_externo_id(+) = sof_documento.id_remitente_area ");
			sSql.Append("and sof_remitente_externo_titular.remitente_externo_titular_id(+) = sof_documento.id_remitente ");
			sSql.Append("and sof_documento_turnar.documento_id(+) = sof_documento.documento_id ");

			sSql.Append("and sof_destinatario_area.id_area(+) = sof_documento.id_destinatario_area");
			sSql.Append("and sof_destinatario_titular.id_empleado(+) = sof_documento.id_destinatario ");

			sSql.Append("and sof_turnado_area.id_area(+) = sof_documento_turnar.id_destinatario_area ");
			sSql.Append("and sof_turnado_titular.id_empleado(+) = sof_documento_turnar.id_destinatario ");
			sSql.Append(sOrder);
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			
			return ds;
		}

		public static DataSet DocumentsSendNotResponse(int nUserId)
		{

			string sSql =	"SELECT sof_documento.DOCUMENTO_ID, " +
				"sof_documento.id_remitente, " +
				"sof_documento.TIPO_DOCUMENTO_ID, " +
				"sof_documento.FECHA, " +
				"sof_documento.REFERENCIA, " +
				"sof_documento.ASUNTO, " +
				"sof_documento.FECHA_ELABORACION, " +
				"sof_documento.FECHA_ENVIO, " +
				"sof_documento.DOCUMENTO_BIS_ID, " +
				"sof_documento_turnar.documento_turnar_id, " +
				"sof_tipo_documento.tipo_documento, " +
				"sof_empleados.nombre " +
				"FROM		sof_documento, sof_documento_turnar, sof_tipo_documento, " +
				"sof_empleados " +
				"WHERE		sof_documento.id_remitente = " + nUserId + " " +
				"and		sof_documento_turnar.documento_id = sof_documento.documento_id " +
				"and		sof_documento_turnar.proceso = 'E'" + " " +
				"and		sof_documento_turnar.estatus = '1'" + " " + 
				"and		sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id " +
				"and		sof_empleados.id_empleado = sof_documento_turnar.id_destinatario " +
				"order by sof_documento.DOCUMENTO_ID ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql.ToString());
			return ds;

		}

		public static DataSet DocumentsSendAndResponse(int nUserId)
		{

			string sSql = "select sof_documento.DOCUMENTO_ID, " +
				"sof_documento.id_remitente, " +
				"sof_documento.TIPO_DOCUMENTO_ID, " +
				"sof_documento.FECHA, " +
				"sof_documento.REFERENCIA, " +
				"sof_documento.ASUNTO, " +
				"sof_documento.FECHA_ELABORACION, " +
				"sof_documento.FECHA_ENVIO, " +
				"sof_documento.DOCUMENTO_BIS_ID, " +
				"sof_documento_turnar.documento_turnar_id, " +
				"sof_tipo_documento.tipo_documento, " +
				"sof_empleados.nombre " +
				"from sof_documento, sof_documento_turnar, sof_tipo_documento, sof_empleados " +
				"where		sof_documento.id_remitente = " + nUserId + " " +
				"and		sof_documento_turnar.documento_id = sof_documento.documento_id " +
				"and		sof_documento_turnar.proceso = 'E'" + " " +
				"and		sof_documento_turnar.estatus = '2'" + " " + 
				"and		sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id " +
				"and		sof_empleados.id_empleado = sof_documento_turnar.id_destinatario " +
				"Order by sof_documento.DOCUMENTO_ID ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
				
			return ds;

		}

		public static DataSet DocumentsReceive(int nUserId)
		{

			string sSql =	"SELECT " +
				"sof_documento.id_Remitente, " +
				"SOF_DOCUMENTO.TIPO_DOCUMENTO_ID, " +
				"SOF_DOCUMENTO.FECHA, " +
				"SOF_DOCUMENTO.REFERENCIA, " + 
				"SOF_DOCUMENTO.ASUNTO, " +
				"SOF_DOCUMENTO.DOCUMENTO_BIS_ID, " +
				"SOF_TIPO_DOCUMENTO.TIPO_DOCUMENTO, " +
				"SOF_DOCUMENTO_TURNAR.* " +
				"FROM SOF_DOCUMENTO, SOF_TIPO_DOCUMENTO, SOF_DOCUMENTO_TURNAR " +
				"WHERE SOF_DOCUMENTO_TURNAR.ID_DESTINATARIO   = " + nUserId + " " + 
				"AND SOF_DOCUMENTO_TURNAR.estatus = '1' " +
				"AND SOF_DOCUMENTO_TURNAR.proceso = 'E' " +
				"AND SOF_DOCUMENTO.documento_id = SOF_DOCUMENTO_TURNAR.documento_id " +
				"AND SOF_TIPO_DOCUMENTO.tipo_documento_id = sof_documento.tipo_documento_id";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;

		}

		public static DataSet Documents_Alternate(int nUserId, string sAlternate, string sStatus, int nLevel, string fromDate, string toDate)
		{
			string dElaborationDateFrom = string.Empty;
			string dElaborationDateTo = string.Empty;
			string dDocumentDateFrom = string.Empty;
			string dDocumentDateTo = string.Empty;
			string sElaborationDate = string.Empty;
			string sDocumentDate = string.Empty;
			string sTmp;
			string sAreaId = "*";
			StringBuilder sSql = new StringBuilder();
			StringBuilder sSqlUsers = new StringBuilder();

			if (sAlternate == "0")
			{
				if (dElaborationDateFrom != "" && dElaborationDateTo != "")
					sElaborationDate = " and trunc(sof_documento.fecha_elaboracion) >= to_date('" + dElaborationDateFrom + "') and trunc(fecha_elaboracion) <= to_date('" + dElaborationDateTo + "') ";

				if (dDocumentDateFrom != "" && dDocumentDateTo != "")
					sDocumentDate = " and trunc(sof_documento.fecha_documento_fuente) >= to_date('" + dDocumentDateFrom + "') and trunc(fecha_documento_fuente) <= to_date('" + dDocumentDateTo + "') ";

				sSql.Append(cnsColumnsT + " \n");
				sTmp = cnsSelectsT;

				sSql.Append(sTmp + " \n ");

				sTmp = cnsFromT;
				sSql.Append(sTmp + " \n ");

				if (sStatus == "1")
					sTmp = cnsWhereT.Replace("<@STATUSTURNADO@>", " IN ('1','2') " );
				else if (sStatus != "4")
					sTmp = cnsWhereT.Replace("<@STATUSTURNADO@>", " = '" + sStatus + "' " );
				else 
					sTmp = cnsWhereT.Replace("<@STATUSTURNADO@>", " LIKE " + "'%' ");

				sTmp = sTmp.Replace("<@FROMDATE@>",fromDate);
				sTmp = sTmp.Replace("<@TODATE@>",toDate);

				sTmp = sTmp.Replace("<@USERS@>", nUserId.ToString());

				sSql.Append(sTmp + " \n ");

				if (sElaborationDate != "")
					sTmp += sElaborationDate;

				if (sDocumentDate != "")
					sTmp += sDocumentDate;

				if (sAreaId != "*")
					sTmp += " and sof_documento_turnar.id_destinatario_area = '" + sAreaId + "' ";
			
				sSql.Append(cnsOrderT);
			
			}
			else
			{

				if (dElaborationDateFrom != "" && dElaborationDateTo != "")
					sElaborationDate = " and trunc(sof_documento.fecha_elaboracion) >= to_date('" + dElaborationDateFrom + "') and trunc(fecha_elaboracion) <= to_date('" + dElaborationDateTo + "') ";

				if (dDocumentDateFrom != "" && dDocumentDateTo != "")
					sDocumentDate = " and trunc(sof_documento.fecha_documento_fuente) >= to_date('" + dDocumentDateFrom + "') and trunc(fecha_documento_fuente) <= to_date('" + dDocumentDateTo + "') ";

				//sSqlUsers.Append(Users.GetUserAlternate(nUserId, nLevel));

				sTmp = cnsSelectsWC;

				sSql.Append(sTmp + " \n ");

				sTmp = cnsFromWC;
				sSql.Append(sTmp + " \n ");

				if (sStatus == "1")
					sTmp = cnsWhereWC.Replace("<@STATUS@>", " IN ('1','2') " );
				else if (sStatus != "4")
					sTmp = cnsWhereWC.Replace("<@STATUS@>", " = '" + sStatus + "' " );
				else 
					sTmp = cnsWhereWC.Replace("<@STATUS@>", " LIKE " + "'%' ");

				sTmp = sTmp.Replace("<@FROMDATE@>",fromDate);
				sTmp = sTmp.Replace("<@TODATE@>",toDate);

				sTmp = sTmp.Replace("<@USERS@>", nUserId.ToString());

				sSql.Append(sTmp + " \n ");

				if (sElaborationDate != "")
					sTmp += sElaborationDate;

				if (sDocumentDate != "")
					sTmp += sDocumentDate;

				if (sAreaId != "*")
					sTmp += " and sof_documento_turnar.id_destinatario_area = '" + sAreaId + "' ";
			
				sSql.Append(cnsOrderWC);

			}
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			
			//Write to file
			//StreamWriter sw = new
	
			//StreamWriter(arg1, arg2);
			//             Constructor with 2 arguments:
			//             1) Path location of file 
			//             2) Append or Overwrite Data
			//StreamWriter(@"c:\Inetpub\wwwroot\gestion\user_files\fileIO.txt",true);
	
			//This will append the given input to the file since arg2 is true
			//sw.Write(sSql.ToString() +  "\n" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath);
	
			//Cleanup 
			//sw.Close();


			//Cleanup
			//sw.Close();

			return ds;
		}


		public static int DocumentReceiveUpdate(int nDocument_Turnar_Id, string sStatus, string sObservaciones)

		{
			OracleParameter [] oParam = {	new OracleParameter("pDocumentTurnarId", null),
											new OracleParameter("pStatus",OracleType.VarChar),
											new OracleParameter("pObservaciones",OracleType.VarChar)
										};

			oParam[0].Value = nDocument_Turnar_Id;
			oParam[1].Value = sStatus;
			oParam[2].Value = sObservaciones;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.DocumentReceiveUpdate",oParam));
			return nVal;
		}

		public static int DocumentReceiveDelete(int nDocument_Turnar_Id, string sTatus)

		{
			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null)};
			oParam[0].Value = nDocument_Turnar_Id;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.DocumentReceiveDelete",oParam));
			return nVal;
		}

		public static int DocumentTurnarInsert(int nDocumentTurnarId, string sProceso, string sStatus, string sObservaciones)
							
		{
			OracleParameter [] oParam = {	new OracleParameter("pDocumentTurnarId", null),
											new OracleParameter("pStatus", OracleType.VarChar),
											new OracleParameter("pProceso",OracleType.VarChar),
											new OracleParameter("pObservaciones",OracleType.VarChar)
										};
			

			oParam[0].Value = nDocumentTurnarId;
			oParam[1].Value = sStatus;
			oParam[2].Value = sProceso;
			oParam[3].Value = sObservaciones;
			
			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Alternate_Create",oParam));
			return nVal;
		}
		

		public static DataView GetAlternate(string sDocumentId, string sTo)
		{

			string sSql = "select " +
				"SOF_DOCUMENTO.*, " +
				"SOF_TIPO_DOCUMENTO.TIPO_DOCUMENTO, " +
				"SOF_DOCUMENTO_TURNAR.ID_DESTINATARIO, " +
				"DE.NOMBRE		DENOMBRE, " +
				"DE.AREA		DEAREA, " +
				"PARA.NOMBRE	PARANOMBRE, " +
				"PARA.AREA		PARAAREA ";
			sSql += " from SOF_DOCUMENTO, SOF_TIPO_DOCUMENTO, SOF_DOCUMENTO_TURNAR, sof_emleados de, sof_emleados para " +
				"where SOF_DOCUMENTO.DOCUMENTO_ID = " + sDocumentId + " " +
				" and  SOF_DOCUMENTO.ID_REMITENTE = de.id_empleado " +
				" and  SOF_DOCUMENTO_TURNAR.DOCUMENTO_ID  =  SOF_DOCUMENTO.DOCUMENTO_ID " +
				" and  SOF_DOCUMENTO_TURNAR.ID_DESTINATARIO  =  " + sTo + " " +
				" and  SOF_TIPO_DOCUMENTO.tipo_documento_id = SOF_DOCUMENTO.tipo_documento_id " +
				" and  de.id_empleado = SOF_DOCUMENTO.id_remitente " +
				" and  para.id_empleado = SOF_DOCUMENTO_TURNAR.id_destinatario"; 
			
			DataView dv = OracleHelper.ExecuteDataset1(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);

			return (dv);
		}


		public static string GetSenderType(int nDocumentId)
		{

			string sSenderType = string.Empty;
			string sTurnar = string.Empty;

			DataSet ds = GetDocument("documento_id = " + nDocumentId, "");			
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				sSenderType = dr["tipo_remitente"].ToString();
			}

			string sSql = "select ('R') from sof_documento_turnar, sof_documento where sof_documento.documento_id = " + nDocumentId;
			sSql += " and sof_documento_turnar.documento_id = " + nDocumentId;

			object oTurnar = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text,sSql);
			if (! (oTurnar == null) )
				sTurnar = oTurnar.ToString();

			return sSenderType+sTurnar;

		}

		public static string GetAreaId(int nDocumentId)
		{

			string sSql =	"select sof_empleados.id_area " +
				"from sof_documento, sof_empleados where sof_documento.documento_id = " + nDocumentId + " " +
				"and sof_empleados.id_empleado = sof_documento.id_empleado";

			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text,sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "Sin Area";
		}

	
		public static DataSet GetStatus(int nDocumentId)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.Append("Select ");
			sSql.Append("sof_documento.volante,  sof_documento.asunto, sof_documento.resumen, ");
			sSql.Append("sof_documento.referencia,");
			sSql.Append("sof_empleados.nombre,");
			sSql.Append("sof_documento.estatus ");
			sSql.Append("From sof_documento, sof_empleados ");
			sSql.Append("Where ");
			sSql.Append("sof_documento.documento_id = " + nDocumentId + " ");
			sSql.Append("and sof_empleados.id_empleado = sof_documento.id_destinatario ");

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}

		public static int PutStatus(int nDocumentoId, string sStatus, string sDateResponse)
		{
			OracleParameter [] oParam = {	new OracleParameter("pDocumentId", null),
											new OracleParameter("pStatus", OracleType.VarChar),
											new OracleParameter("pDate", OracleType.VarChar),

			};
			oParam[0].Value = nDocumentoId;
			oParam[1].Value = sStatus;
			oParam[2].Value = sDateResponse;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Status_Create",oParam));
			return nVal;
		}

		public static int PutStatusUpdate(int nDocumentId, string sStatus, string dDateResponse)
		{
			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null),
											new OracleParameter("pStatus", OracleType.VarChar),
											new OracleParameter("pDate", OracleType.VarChar)
										};

			oParam[0].Value = nDocumentId;
			oParam[1].Value = sStatus;
			oParam[2].Value = dDateResponse;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Status_Update", oParam));
			return nVal;
		}


		public static int getNextVolante(int nUser)
		{

			string sAreaId = firmaUsuario.isBoss(nUser);
			if (sAreaId == "0")
				sAreaId = firmaUsuario.getReglaFolio(nUser);

			OracleParameter [] oParam = {new OracleParameter("pAreaId", OracleType.VarChar),
											new OracleParameter("pResult", null)};
												

			oParam[0].Value = sAreaId;
			oParam[1].Value = 0;
			oParam[1].Direction = ParameterDirection.InputOutput;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.StoredProcedure, "Gestion.Folio_Turnar_Create", oParam);
			object obj = oParam[1].Value;
			int nVal = int.Parse(obj.ToString());
			return nVal;

		}

		public static string getVolanteUpComing(int nUser)
		{

			string sSql = "Select sof_folio_turnar.folio + 1 from sof_regla, sof_folio_turnar Where sof_regla.id_empleado = " + nUser + " and sof_folio_turnar.id_area = sof_regla.id_folio ";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "0";
		}
		
		
		public static string GetVolanteNumber(int nDocumentId)
		{

			string sSql = "select volante from sof_documento where sof_documento.documento_id = " + nDocumentId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text,sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "Sin numero de volante";

		}

		public static DataSet GetWithCopyFor(int nDocumentId)
		{

			string sHtml = String.Empty;

			string sSql =	"select sof_empleados.nombre, sof_areas.area " +
				"from sof_ccpara, sof_empleados, sof_areas " +
				"where sof_ccpara.documento_id = " + nDocumentId + " " +
				"and sof_ccpara.eliminado = '" + "0" + "' " +
				"and sof_empleados.id_empleado = sof_ccpara.id_destinatario " +
				"and sof_areas.id_area = sof_empleados.id_area ";
							
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static string AutorUser(int nDocumentId)
		{

			string sSql = "select sof_empleados.nombre from sof_documento, sof_empleados " +
				"where sof_documento.documento_id = " + nDocumentId +  " " +
				"and sof_empleados.id_empleado = sof_documento.id_empleado ";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (! (oObj == null) )
				return (oObj.ToString());
			else
				return "No existe el usuario del documento: " + nDocumentId;
		}

		public static int getDocumentId(int nId)
		{
			string sSql = "select documento_id from sof_documento_turnar where documento_turnar_id = " + nId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return oObj == null ? 0 : int.Parse(oObj.ToString());
		}

		public static string GetDocumentBis(int nId)
		{
			string sSql = "select documento_bis_id from sof_documento where documento_id = " + nId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return oObj == null ? "0" : oObj.ToString();
		}


		public static DataSet GetDocumentTurnar(int nDocumentTurnarId, string sTipoTurno)
		{
			string sSql = String.Empty;
			sSql = "select id_destinatario, id_area id_destinatario_area from sof_ccpara, sof_empleados " +
				" where sof_ccpara.ccpara_id = " + nDocumentTurnarId + 
				" and sof_empleados.id_empleado = sof_ccpara.id_destinatario ";

			if (sTipoTurno == "T")
				sSql ="select id_destinatario, id_destinatario_area from sof_documento_turnar where documento_turnar_id = " + nDocumentTurnarId;
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static void saveResponse(int nDocumentId, string sResponse, string sStatus, string sDateResponse)
		{
			string sSql ="update sof_documento set estatus = '" + sStatus + "', observacion = '" + sResponse + "', fecha_envio = to_date('" + sDateResponse + "') where documento_id = " + nDocumentId;
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
		}

		public static void Document_Status(int nDocumentId, string sResponse, string sStatus, string sDateResponse)
		{
			OracleParameter [] oParam = {new OracleParameter("pDocumentId", null),
											new OracleParameter("pStatus",OracleType.VarChar, 20),
											new OracleParameter("pDateResponse",OracleType.VarChar),	
											new OracleParameter("pResponse",OracleType.VarChar, 512),
			};
			oParam[0].Value = nDocumentId;
			oParam[1].Value = sStatus;
			oParam[2].Value = sDateResponse;
			oParam[3].Value = sResponse;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Status",oParam);
		}


		public static string getResponse(int nDocumentId)
		{
			string sSql ="SELECT observacion FROM sof_documento WHERE documento_id = " + nDocumentId;
			return (OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
		}

		public static DataSet getResponseDS(int nDocumentId)
		{
			string sSql = "SELECT  Sof_Documento.Observacion, Sof_Estatus_Turnar.fecha_desde " +
				"FROM Sof_Documento, Sof_Documento_Turnar, Sof_Estatus_Turnar " +
				"WHERE Sof_Documento.Documento_ID = " + nDocumentId + " " +
				"AND Sof_Documento_Turnar.Documento_ID = Sof_Documento.Documento_ID " +
				"AND Sof_Documento_Turnar.Estatus = '3' " +
				"AND Sof_Estatus_Turnar.Documento_Turnar_ID = Sof_Documento_Turnar.Documento_Turnar_ID " +
				"AND Sof_Estatus_Turnar.Estatus = '3' " +
				"group by sof_documento.Observacion,  Sof_Estatus_Turnar.Fecha_Desde";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;

		}

		public static string getVolanteCurrent(int nDocumentId)
		{
			string sSql ="select volante from sof_documento where documento_id = " + nDocumentId;
			return (OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
		}

		public static string getVolanteReturnado(string sDocumentID, int nUser)
		{
			string sSql ="select volante from sof_documento where documento_bis_id = " + sDocumentID + " and id_empleado = " + nUser;
				
			object oObj =  OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "*";
		}

		public static string getTreeVolante(string sDocumento_Bis_ID)
		{
			string sTmpTree = String.Empty;
			string sVal		= String.Empty;
			string sBisID	= String.Empty;

			sBisID = sDocumento_Bis_ID;
			while (sBisID != "0")
			{
				sVal =	getTree(ref sBisID);
				sTmpTree += sVal;
			}

			string[] aVolante = sTmpTree.Split('-');
			
			sTmpTree = "";
			for (int i = aVolante.Length-1; i >= 0; i--)
			{
				if (aVolante[i].ToString() != "")
					sTmpTree += aVolante[i].ToString() + "-";
			}

			return sTmpTree;
		}

		
		public static string getTree(ref string sBisID )
		{
			string sSql = "select sof_documento.documento_id, sof_documento.documento_bis_id, sof_documento.Volante from sof_documento Where documento_id = " + sBisID;
			string sTmp = String.Empty;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			sBisID = "0";
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				sTmp = dr["volante"].ToString() + "-" ;
				sBisID = dr["documento_bis_id"].ToString();
			}
			return sTmp;
		}

		public static void FinalizeTurnar(string sDocumentId)
		{
			string sSql ="select " +
				"sof_documento_turnar.estatus, " +
				"sof_documento_turnar.documento_id, " +
				"sof_documento_turnar.documento_turnar_id " +
				"from " +
				"sof_documento_turnar " +
				"where sof_documento_turnar.documento_id = " + sDocumentId + " " +
				"and sof_documento_turnar.eliminado = '0' " ;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			int nCount = 0;
			int nTotalRecords = 99;
			string sAnswer = String.Empty;
			string sTmp = String.Empty;
			if (ds.Tables[0].Rows.Count > 0)
			{
				nTotalRecords = ds.Tables[0].Rows.Count;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					sTmp = Attached.GetResponseTurnar( Convert.ToInt32(dr["documento_turnar_id"].ToString()));
					if (sTmp != "")
					{
						sAnswer += sTmp + "\n";
						nCount++;
					}
				}
			}
			if (nCount == nTotalRecords)
			{
				sSql = "UPDATE sof_documento set estatus = 'Concluido', observacion = '" + CleanString.InputText(sAnswer, 0) + "' " + 
					"WHERE documento_id = " + sDocumentId;
			
				OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			}
			else
			{
				sSql = "UPDATE sof_documento SET estatus = 'Tramite', observacion = '', fecha_envio = '' " + 
					" WHERE documento_id = " + sDocumentId;
			
				OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			}
			
		}

		public static void FinalizeTurnar(string sDocumentId, string sResponse, string sStatus, string sDate)
		{
			string sSql = "UPDATE sof_documento SET estatus = '" + sStatus + "', observacion = '" + CleanString.InputText(sResponse, 0) + "', fecha_envio = to_date('" + sDate + "')" +
				" WHERE documento_id = " + sDocumentId;
			
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
		}

		public static DataSet GetDocumentByUser(int nUserID) 
		{
			string sSql = "SELECT Sof_Documento.Documento_ID, Sof_Documento.Documento_BIS_ID, Sof_Documento.Volante,"  +
				" Sof_Documento.id_empleado, Sof_Documento.Estatus" +
				" FROM Sof_Documento " +
				" WHERE id_empleado = " + nUserID +
				" ORDER BY Volante ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetRestableceCascade(int nDocumentID)
		{
			string sSql = "SELECT  sof_documento.documento_id, sof_documento.documento_bis_id, sof_documento.volante," +
				" sof_documento.estatus, length(sof_documento.observacion) lenobs " +
				" FROM Sof_Documento " +
				" connect by prior documento_id = documento_bis_id " +
				" start with documento_id = " + nDocumentID +
				" ORDER BY Documento_BIS_ID ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetResponseCascade(int nDocumentID) 
		{

			string sSql = "SELECT * FROM ( " +
				" SELECT  sof_documento.Documento_ID," +
				" Sof_Documento.Documento_BIS_ID, " +
				" sof_Documento.Volante," +
				" Sof_Documento.Estatus," +
				" Sof_Documento.id_empleado" +
				" FROM Sof_Documento " +
				" CONNECT BY PRIOR Documento_ID = Documento_BIS_ID " +
				" start with documento_id = " + nDocumentID +
				" ORDER BY Documento_BIS_ID) A, Sof_Respuesta " +
				" WHERE Sof_Respuesta.id_empleado(+) =  A.id_empleado";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;

		}

		public static void PutResponseCascade(int nDocumentID, string sStatus) 
		{
			string sSql = "Update sof_documento set estatus = '" + sStatus + "'" +
				" Where Documento_ID = " + nDocumentID;
			
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
		}

		public static void FinalizeTurnar(object oDocumentId, int nDocumentTurnarId)
		{
			FinalizeTurnar(getDocumentId(nDocumentTurnarId).ToString());
		}

		public static void FinalizeCcpara(int nDocumentId)
		{
			
		}

		public static void FinalizeTurnarCorrection(string sDocumentId)
		{
			string sSql ="SELECT " +
				"Sof_documento_turnar.Estatus, " +
				"Sof_documento_turnar.Documento_ID, " +
				"Sof_documento_turnar.Documento_Turnar_id, " +
				"Sof_Documento.Observacion " +
				"FROM " +
				"Sof_Documento_Turnar, Sof_Documento " +
				"WHERE sof_documento_turnar.documento_id = " + sDocumentId + " " +
				"AND sof_documento_turnar.eliminado = '0' "  +
				"AND Sof_Documento.Documento_ID = Sof_Documento_Turnar.Documento_ID";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			int nCount = 0;
			int nTotalRecords = 99;
			string sAnswer = String.Empty;
			string sTmp = String.Empty;
			string sObservacion = String.Empty;

			if (ds.Tables[0].Rows.Count > 0)
			{
				sObservacion = ds.Tables[0].Rows[0]["Observacion"].ToString();
				nTotalRecords = ds.Tables[0].Rows.Count;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					sTmp = Attached.GetResponseTurnar( Convert.ToInt32(dr["documento_turnar_id"].ToString()));
					if (sTmp != "")
					{
						sAnswer += sTmp + "\n";
						nCount++;
					}
				}
			}
			if (nCount == nTotalRecords)
			{
				if (sAnswer == sObservacion)
				{
					sSql = "update sof_documento set estatus = 'Tramite', observacion = '' " +
						"where documento_id = " + sDocumentId;

					OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
				}
			}
		}

		public static string SearchParent(string sDocumentId)
		{
			string sSql ="SELECT " +
				"sof_documento.documento_id," +
				"sof_documento.documento_bis_id " +
				"FROM " +
				"Sof_Documento " +
				"WHERE sof_documento.documento_id = " + sDocumentId;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			string sDocId = sDocumentId;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["documento_bis_id"].ToString() != "0")
					return (SearchParent( ds.Tables[0].Rows[0]["documento_bis_id"].ToString()));
				else
					return sDocId;
			} 
			else 
			{
				return sDocId;
			}
		}

		public string SearchChild(string sDocumentId)
		{
			ArrayList documentArray = new ArrayList();
			string sSql ="SELECT sof_documento.documento_id, sof_documento.documento_bis_id " +
				"FROM Sof_Documento WHERE sof_documento.documento_bis_id = " + sDocumentId;

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			sList += sDocumentId + ",";
			
			foreach (DataRow r in ds.Tables[0].Rows)
			{
				documentArray.Add(r["documento_id"].ToString());
			}

			for (int i=0; i < documentArray.Count; i++)
			{
				SearchChild( documentArray[i].ToString());
			}
				 
			return sList;
		}

		public string SearchDepend(string documentId, string alternateId, string idx)
		{
			ArrayList documentArray = new ArrayList();

			if 	(documentId != string.Empty) 
			{
				string sSql ="SELECT sof_documento_turnar.documento_turnar_id, sof_documento_turnar.documento_id, sof_documento_turnar.documento_bis_id " +
					"FROM Sof_Documento_Turnar WHERE sof_documento_turnar.documento_id = " + documentId + " and eliminado = '0'" ;

				DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			
				foreach (DataRow r in ds.Tables[0].Rows)
				{
					documentArray.Add( r["documento_turnar_id"].ToString() + "," +r["documento_bis_id"].ToString() );
				}
			}
			sList += "(" + idx + ")," + documentId + "," + alternateId + ",";

			for (int i = 0; i < documentArray.Count; i++)
			{
				string [] aList = documentArray[i].ToString().Split(',');
				SearchDepend( aList[1].ToString(), aList[0].ToString(), i.ToString() );
			}
				 
			return sList;
		}

		public string SearchDependency(string documentId, string alternateId)
		{
			string documentParent = doTree(long.Parse(documentId), long.Parse(alternateId));
			return documentParent;
			//			string [] aList = documentParent.Split(',');
			//			return SearchDown(aList[1].ToString());
		}

		public string SearchDependencyCC(string documentId, string alternateId)
		{
			string documentParent = doTreeCC(long.Parse(documentId), long.Parse(alternateId));
			return documentParent;
		}


		private string SearchUp(string documentId, string alternateId)
		{
			string sSql = "SELECT documento_turnar_id, documento_id, documento_bis_id FROM Sof_Documento_Turnar WHERE sof_documento_turnar.documento_bis_id = " + documentId;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			sListParent += documentId + "," + alternateId + ",";
			if (ds.Tables[0].Rows.Count > 0)
				SearchUp(ds.Tables[0].Rows[0]["documento_id"].ToString(), ds.Tables[0].Rows[0]["documento_turnar_id"].ToString());
			return sListParent;
		}

		private string SearchDown(string documentBisId)
		{
			string sSql ="SELECT sof_documento_turnar.documento_bis_id FROM Sof_Documento_Turnar WHERE documento_id = " + documentBisId;

			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			
			sList  += documentBisId + ",";

			if (oObj != null && oObj.ToString() != string.Empty)
			{
				SearchDown(oObj.ToString());
			}
			return sList;
		}

		

		public static TimeSpan getDaysPass(int nDocumentId, string sStatus)
		{
			string sSql ="SELECT fecha_desde, fecha_hasta " +
				"FROM Sof_Documento_Relacionado WHERE sof_documento_relacionado.Documento_ID = " + nDocumentId + " AND sof_documento_relacionado.Estatus = '" + sStatus + "'";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);

			if (ds.Tables[0].Rows.Count > 0)
			{
				DateTime dDateTo = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_hasta"].ToString());
				if (ds.Tables[0].Rows[0]["fecha_hasta"].ToString().Substring(0,10) == "01/01/2049" )
				{
					dDateTo = DateTime.Now;
				}
				return  dDateTo - Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_desde"].ToString()) ;
			} 

			return (DateTime.Now - DateTime.Now);
		}

		public static OracleDataReader GetDocuments(int nUserId, string sSenderId, string sStatus, string senderType, string fromDate, string toDate)
		{

			StringBuilder sSQL = new StringBuilder();

			if (senderType == "E")
				sSQL.Append(cnsExternalSenderSQL).Replace("<@USERID@>",nUserId.ToString());
			else
				sSQL.Append(cnsInternalSenderSQL).Replace("<@USERID@>",nUserId.ToString());

			switch (sStatus) 
			{
				case "1":
					sSQL.Replace("<@STATUS@>","'Pendiente'");
					break;
				case "2":
					sSQL.Replace("<@STATUS@>","'Tramite'");
					break;
				case "3":
					sSQL.Replace("<@STATUS@>","'Concluido'");
					break;
				default:
					sSQL.Replace("<@STATUS@>","'%'");
					break;
			}

			if (fromDate != String.Empty)
				sSQL.Replace("<@FROMDATE@>"," AND TRUNC(FECHA_ELABORACION) >= TO_DATE('" + fromDate + "')");

			if (toDate != String.Empty)
				sSQL.Replace("<@TODATE@>", " AND TRUNC(FECHA_ELABORACION) <= TO_DATE('" + toDate + "')");

			if (sSenderId == String.Empty) 
				sSQL.Replace("<@SENDERID@>","'%'");
			else
				sSQL.Replace("<@SENDERID@>",sSenderId);

			return OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSQL.ToString());

		}

		private static string GetReceiveStatus(string sUserID, string sStatusReceive)
		{
			string sStringSeguimiento = "";
			if ( Document_Alternate.GetDisplayColumnStatus( sUserID ) )
			{
				switch(sStatusReceive)       
				{         
					case "4":  // Seguimiento
						sStringSeguimiento = " And Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO = '1' ";
						break;
					case "3":  // Todos
						sStringSeguimiento = " And Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO like '%' ";
						break;
					case "2":  // Concluidos           
						sStringSeguimiento = " And Sof_Documento_Turnar.ESTATUS_VERIFICA = '1' ";
						break;
					default:   // Pendientes     
						sStringSeguimiento = " And Sof_Documento_Turnar.ESTATUS_VERIFICA <> '1' AND Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO <> '1' ";
						break;
				}
			}
			else
			{
				switch(sStatusReceive)
				{         
					case "4":  // Seguimiento
						sStringSeguimiento = " and Sof_Documento_Turnar.Estatus Like '%' ";
						break;
					case "3":  // Todos
						sStringSeguimiento = " and Sof_Documento_Turnar.Estatus Like '%' ";
						break;
					case "2": //  Concluidos           
						sStringSeguimiento = " and Sof_Documento_Turnar.Estatus = '3' ";
						break;
					default:            
						sStringSeguimiento =   " and Sof_Documento_Turnar.Estatus in ('0','1','2') ";
						break;
				}
			}
			return sStringSeguimiento;
		}

		private static string GetCCParaStatus(string sUserID, string sStatusReceive)
		{
			string sStringSeguimiento = "";
		{
			switch(sStatusReceive)
			{         
				case "4":  // Seguimiento
					sStringSeguimiento = " and Sof_CCPara.Estatus Like '%' ";
					break;
				case "3":  // Todos
					sStringSeguimiento = " and Sof_CCPara.Estatus Like '%' ";
					break;
				case "2": //  Concluidos           
					sStringSeguimiento = " and Sof_CCPara.Estatus = '3' ";
					break;
				default:            
					sStringSeguimiento =   " and Sof_CCPara.Estatus in ('0','1','2') ";
					break;
			}
		}
			return sStringSeguimiento;
		}

		public static int getOwnerDocument(int documentID)
		{
			string sSql ="select id_empleado from sof_documento where documento_id = " + documentID;
				
			object oObj =  OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			if (! (oObj == null) )
				return int.Parse(oObj.ToString());
			else
				return 0;
		}


		public static DataSet Statistics(string areaId, string toDate, string fromDate, string idEmpleado, string tipoEstadistica)
		{
			string sSql = "select ";
			if (tipoEstadistica == "R") 
			{

				sSql += " fromAreaId, sum(pendiente) pendiente, sum(tramite) tramite, sum(concluido) concluido, sum(sinTramite) sinTramite, sum(total) total, max(sof_areas.cve_area) fromClaveArea, max(sof_areas.area) area " +
					"from ( " +
					"select sof_documento.id_destinatario_area toAreaId, nvl(sof_documento_turnar.id_destinatario_area,0) fromAreaId, decode(sof_documento_turnar.estatus,'0',(1),(0)) pendiente, " +
					"decode(sof_documento_turnar.estatus,'1',(1),decode(sof_documento_turnar.estatus,'2',(1),0)) tramite, decode(sof_documento_turnar.estatus,'3',(1),(0)) concluido, " +
					"(0) sinTramite, (1) total " +
					"FROM  sof_documento, sof_documento_turnar, sof_mapeo_recibidos " +
					"WHERE " +
					"sof_mapeo_recibidos.id_empleado = " + idEmpleado + " and " +
					"sof_mapeo_recibidos.fecha_inicio <= TRUNC(sof_documento.fecha_elaboracion) And " +
					"sof_mapeo_recibidos.fecha_fin >= TRUNC(sof_documento.fecha_elaboracion) and " +
					"sof_documento_turnar.id_destinatario = sof_mapeo_recibidos.id_destinatario and " +
					"sof_documento.estatus <> 'Pendiente' And " +
					"trunc(sof_documento.fecha_elaboracion) >= '" + toDate + "'" + " and " +
					"trunc(sof_documento.fecha_elaboracion) <= '" + fromDate + "'" + " and " +
					"sof_documento.id_empleado > 0  and " +
					"sof_documento_turnar.documento_id = sof_documento.documento_id  and " +
					"sof_documento_turnar.eliminado = '0' " +
					") tmp, sof_areas  " +
					" where tmp.fromAreaId = sof_areas.id_area " +
					"group by fromAreaId " +
					"order by fromAreaId " ;

			}
			else 
			{
				sSql += "toAreaId, fromAreaId, pendiente, tramite, concluido, sinTramite, total, decode(fromAreaId,'0', toArea.area, decode(fromArea.area,'','Sin Turnar',fromArea.area ) ) Area, nvl(fromArea.cve_area,'0') FromClaveArea ";
				sSql += "from ( " ;
				sSql += "select toAreaId, fromAreaId, sum(pendiente) pendiente, sum(tramite) tramite, sum(concluido) concluido, sum(sinTramite) sinTramite, sum(total) total " ;
				sSql += "from ( " ;
				sSql += "select sof_documento.id_destinatario_area toAreaId, " ;
				sSql += "nvl(sof_documento_turnar.id_destinatario_area,0) fromAreaId, " ;
				sSql += "decode(sof_documento_turnar.estatus,'0',(1),(0)) pendiente, " ;
				sSql += "decode(sof_documento_turnar.estatus,'1',(1), decode(sof_documento_turnar.estatus,'2',(1),0)) tramite, " ;
				sSql += "decode(sof_documento_turnar.estatus,'3',(1),(0)) concluido, " ;
				sSql += "decode(nvl(sof_documento_turnar.id_destinatario_area,0),0,1,0) sinTramite, " ;
				sSql += "(1) total " ;
				sSql += "from  sof_documento, " ;
                sSql += "sof_documento_turnar " ;
				sSql += "where sof_documento.id_destinatario_area = " + areaId + " " ;
				sSql += "and trunc(sof_documento.fecha_elaboracion) >= '" + toDate + "' " ;
				sSql += "and trunc(sof_documento.fecha_elaboracion) <= '" + fromDate + "' " ;
				sSql += "and sof_documento.id_empleado > 0 " ;
				sSql += "and sof_documento_turnar.documento_id(+) = sof_documento.documento_id " ;
				sSql += "and sof_documento_turnar.eliminado(+) = '0' " ;
				sSql += ") " ;
				sSql += "group by toAreaId, fromAreaId " ;
				sSql += ") sdf, sof_areas toArea, sof_areas fromArea " ;
				sSql += "where toArea.id_area(+) = sdf.toAreaId " ;
				sSql += "And fromArea.id_area(+) = sdf.fromAreaId order by fromAreaId ";

//				sSql += " toAreaId, fromAreaId, pendiente, tramite, concluido, sinTramite, total, decode(fromAreaId,'0', toArea.area, decode(fromArea.area,'','Sin Turnar',fromArea.area ) ) Area, nvl(fromArea.cve_area,'0') from ClaveArea";
//				sSql += " from (select toAreaId, fromAreaId, sum(pendiente) pendiente, sum(tramite) tramite, sum(concluido) concluido, sum(sinTramite) sinTramite, sum(total) total " ;
//				sSql += " from (";
//				sSql += " select id_destinatario_area toAreaId, (0) fromAreaId, " ;
//				sSql += " decode(estatus,'Pendiente',(1),(0)) pendiente, " ;
//				sSql += " decode(estatus,'Tramite',(1),(0)) tramite, " ;
//				sSql += " decode(estatus,'Concluido',(1),(0)) concluido," ;
//				sSql += " decode(estatus,'Sin Tramite',(1),(0)) sinTramite, (1) total" ;
//				sSql += " from  sof_documento " ;
//				//sSql += " where  sof_documento.id_destinatario_area = " + areaId + " ";
//				sSql += " where  sof_documento.id_empleado = " + idEmpleado + " ";
//				sSql += " and  trunc(sof_documento.fecha_elaboracion) >= '" + toDate + "'";
//				sSql += " and  trunc(sof_documento.fecha_elaboracion) <= '" + fromDate + "'";
//				sSql += " and  sof_documento.id_empleado > 0 ";
//				sSql += " union all";
//				sSql += " select sof_documento.id_destinatario_area toAreaId, nvl(sof_documento_turnar.id_destinatario_area,0) fromAreaId," ;
//				sSql += " decode(sof_documento_turnar.estatus,'0',(1),(0)) pendiente,";
//				sSql += " decode(sof_documento_turnar.estatus,'1',(1),decode(sof_documento_turnar.estatus,'2',(1),0)) tramite,";
//				sSql += " decode(sof_documento_turnar.estatus,'3',(1),(0)) concluido,";
//				sSql += " (0) sinTramite, (1) total";
//				sSql += " from  sof_documento, sof_documento_turnar";
//				//			sSql += " where sof_documento.id_destinatario_area = " + areaId + " ";
//				sSql += " where  sof_documento.id_empleado = " + idEmpleado + " ";
//				sSql += " and	trunc(sof_documento.fecha_elaboracion) >= '" + toDate + "'";
//				sSql += " and   trunc(sof_documento.fecha_elaboracion) <= '" + fromDate + "'";
//				sSql += " and	sof_documento.id_empleado > 0 ";
//				sSql += " and	sof_documento_turnar.documento_id = sof_documento.documento_id ";
//				sSql += " and	sof_documento_turnar.eliminado = '0'";
//				sSql += " union all";
//				sSql += " select id_destinatario_area toAreaId, (999999999) fromAreaId,";
//				sSql += " (0) pendiente,";
//				sSql += " (0) tramite,";
//				sSql += " (0) concluido,";
//				sSql += " (0) sinTramite, (1) total";
//				sSql += " from  sof_documento";
//				//			sSql += " where   sof_documento.id_destinatario_area = '" + areaId + "'";
//				sSql += " where  sof_documento.id_empleado = " + idEmpleado + " ";
//				sSql += " and   trunc(sof_documento.fecha_elaboracion) >= '" + toDate + "'";
//				sSql += " and   trunc(sof_documento.fecha_elaboracion) <= '" + fromDate + "'";
//				sSql += " and   sof_documento.id_empleado > 0";
//				sSql += " and  sof_documento.documento_id not in (select documento_id from sof_documento_turnar where eliminado = '0')";
//				sSql += " ) temp";
//				sSql += " group by fromAreaId, toAreaId";
//				sSql += " ) sdf, sof_areas toArea,";
//				sSql += " sof_areas fromArea";
//				sSql += " where toArea.id_area(+) = sdf.toAreaId";
//				sSql += " and fromArea.id_area(+) = sdf.fromAreaId";
//				sSql += " order by fromAreaId";
			}
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public string getVolanteRel(string idDoc)
		{
			long nDoc = Convert.ToInt32(idDoc);
			bool bNotFound = true;
			string sVolanteRel = string.Empty;
			OracleDataReader dr;
			do
			{
				dr = getBisIdNext(nDoc);
				
				bNotFound = true;
				if (dr.Read())
				{
					nDoc = Convert.ToInt32(dr["documento_bis_id"].ToString());
					if (nDoc > 0)
					{
						bNotFound = false;
						sVolanteRel += dr["documento_id"].ToString() + "|" + nDoc.ToString() + "|";
					}
					
				}
			}
			while (! bNotFound);

			return sVolanteRel.TrimEnd('|');
		}

		public string getVolWithTurnado(string idDoc, string idBis)
		{
	
			long nDoc = Convert.ToInt32(idDoc);
			bool bNotFound = true;
			string sDocumentoRel = string.Empty;
			OracleDataReader dr;

			string sSQL = "select documento_id, documento_turnar_id, documento_bis_id from sof_documento_turnar where documento_id = " + idBis + " and documento_bis_id = " +idDoc;
			dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQL);

			do
			{
				bNotFound = true;
				if (dr.Read())
				{
					bNotFound = false;
					sDocumentoRel += dr["documento_id"].ToString() + "," + dr["documento_turnar_id"].ToString() + ",";
				}
			}
			while (! bNotFound);

			return sDocumentoRel.TrimEnd(',');
		}


		private string doTree(long idDoc, long idTur)
		{
			long nDoc = idDoc;
			long nTur = idTur;
			long nId = 0;
			string sReturn = string.Empty;

			sReturn = idDoc.ToString();
			OracleDataReader dr;
			OracleDataReader drLast;

			bool bNotFound = true;
			do 
			{
				dr = getBisIdLast(nDoc, nTur, "T");
				
				bNotFound = true;
				if (dr.Read())
				{
					bNotFound = false;
					nDoc = long.Parse(dr["bisId"].ToString());
					nTur = long.Parse(dr["idT"].ToString());
					sReturn = dr["bisId"].ToString();
					dr.Close();
				}
			}
			while (! bNotFound);

			dr.Close();

			nId = long.Parse(sReturn);
			if (nId == 0)
			{
				nId = idDoc;
				nTur = idTur;
			}

			//drLast = getIdCurrent(nId, 0, "T"); 
			drLast = getIdCurrent(nId, nTur, "T"); //Asg. 9/02/2011 14:53

			if (drLast.Read())
			{
				sReturn = drLast["Id"].ToString() + "," + drLast["Idt"].ToString() + ",";
			}
			drLast.Close();
			
			bNotFound = true;
			do 
			{
				dr = getBisIdFirst(nId, "T");
				bNotFound = true;
				if (dr.Read())
				{
					bNotFound = false;
					nId = long.Parse(dr["Id"].ToString());
					sReturn += dr["Id"].ToString() + "," + dr["Idt"].ToString() + ",";
					dr.Close();
				}
			}
			while (! bNotFound);

			dr.Close();
			drLast.Close();
			return sReturn.TrimEnd(',');
		}


		private string doTreeCC(long idDoc, long idTur)
		{
			long nDoc = idDoc;
			long nTur = idTur;
			long nId = 0;
			string sReturn = string.Empty;

			sReturn = idDoc.ToString();
			OracleDataReader dr;
			OracleDataReader drLast;

			bool bNotFound = true;
			do 
			{
				dr = getBisIdLast(nDoc, nTur, "C");
				
				bNotFound = true;
				if (dr.Read())
				{
					bNotFound = false;
					nDoc = long.Parse(dr["bisId"].ToString());
					nTur = long.Parse(dr["idT"].ToString());
					sReturn = dr["bisId"].ToString();
					dr.Close();
				}
			}
			while (! bNotFound);

			dr.Close();

			nId = long.Parse(sReturn);
			if (nId == 0)
			{
				nId = idDoc;
				nTur = idTur;
			}
		
			drLast = getIdCurrent(nId, nTur, "C");
			if (drLast.Read())
				sReturn = drLast["Id"].ToString() + "," + drLast["Idt"].ToString() + ",";
			else
				sReturn = "";

			drLast.Close();
			
			bNotFound = true;
			do 
			{
				dr = getBisIdFirst(nId, "C");
				bNotFound = true;
				if (dr.Read())
				{
					bNotFound = false;
					nId = long.Parse(dr["Id"].ToString());
					sReturn += dr["Id"].ToString() + "," + dr["Idt"].ToString() + ",";
					dr.Close();
				}
			}
			while (! bNotFound);

			dr.Close();
			drLast.Close();
			return sReturn.TrimEnd(',');
		}


		private OracleDataReader getIdCurrent(long nId, long nIdTur, string documentType)
		{
			//string sSQL = "select documento_id Id, documento_turnar_id idt from sof_documento_turnar where documento_id = " + nId + " and documento_turnar_id = " + nIdTur;
			string sSQL = "select documento_id Id, documento_turnar_id idt from sof_documento_turnar where documento_id = " + nId + " and nvl(documento_bis_id,0) = " + nIdTur;
			//			string xxxx = "select documento_id Id, documento_turnar_id idt from sof_documento_turnar where documento_id = 295087 and nvl(documento_bis_id,0) = 0";

			if (documentType == "C")
				sSQL = "select documento_id Id, ccpara_id idt from sof_ccpara where documento_id = " + nId + " and ccpara_id = " + nIdTur;

			return OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQL);
		}

		private OracleDataReader getBisIdLast(long nId, long nIdT, string documentType)
		{
			string sSQL = "select nvl(documento_bis_id,0) bisId, documento_turnar_id idt from sof_documento_turnar where documento_id = " + nId + " and documento_turnar_id = " + nIdT;
			if (documentType == "C")
				sSQL = "select nvl(documento_bis_id,0) bisId, ccpara_id idt from sof_ccpara where documento_id = " + nId + " and ccpara_id = " + nIdT;

			return OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQL);
		}

		private OracleDataReader getBisIdFirst(long nId, string documentType)
		{
			string sSQL = "select documento_id Id, documento_turnar_id idt from sof_documento_turnar where documento_bis_id = " + nId;
			//			string xxxx = "select documento_id Id, documento_turnar_id idt from sof_documento_turnar where documento_bis_id = 295087

			if (documentType == "C")
				sSQL = "select documento_id Id, ccpara_id idt from sof_ccpara where documento_bis_id = " + nId;

			return OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQL);
		}

		private OracleDataReader getBisIdNext(long nId)
		{
			string sSQL = "select documento_id, documento_bis_id from sof_documento where documento_id = " + nId;
	
			return OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSQL);
		}


		public static void EstatusTurnaResponse(long nId, long nTurnarId, string sReply, string sReplyDate, string sStatus, string sCrud)
		{

			OracleParameter [] sqlParams = {
											   new OracleParameter("pId", null),
											   new OracleParameter("pIdTurnar", null),
											   new OracleParameter("pStatus",OracleType.VarChar),
											   new OracleParameter("pDateReply",OracleType.VarChar),
											   new OracleParameter("pReply",OracleType.VarChar, 512),
											   new OracleParameter("pCrud",OracleType.VarChar, 12)
										   };

			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nTurnarId;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sStatus;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sReplyDate;

			sqlParams[4].Direction = ParameterDirection.Input;
			sqlParams[4].Value = sReply;

			
			sqlParams[5].Direction = ParameterDirection.Input;
			sqlParams[5].Value = sCrud;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure, "sp_estatus_turnar_crear", sqlParams);

		}


		public static void EstatusCCResponse(long nId, long nTurnarId, string sReply, string sReplyDate, string sStatus, string sCrud)
		{

            OracleParameter[] sqlParams = {
                                               new OracleParameter("pId", null),
                                               new OracleParameter("pIdTurnar", null),
                                               new OracleParameter("pStatus", OracleType.VarChar),
                                               new OracleParameter("pDateReply", OracleType.VarChar),
                                               new OracleParameter("pReply", OracleType.VarChar, 512),
                                               new OracleParameter("pCrud", OracleType.VarChar, 12)
                                           };

			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nTurnarId;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sStatus;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sReplyDate;

			sqlParams[4].Direction = ParameterDirection.Input;
			sqlParams[4].Value = sReply;

			sqlParams[5].Direction = ParameterDirection.Input;
			sqlParams[5].Value = sCrud;


			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure, "sp_estatus_ccpara_crear", sqlParams);

		}


	}
}
