using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
// Components Banobras
using BComponents.DataAccessLayer;
using BCCXMLDOMParser;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Document_Alternate
	{
		
		public const string cnsColumnsOrderTurnado = "Select " +
			"('Turnado') Tipo_Turno, " +
			"Sof_Documento_Turnar.Documento_ID, " +
			"Decode(Sof_Documento_Turnar.Estatus,'0','Sin Abrir','1','En Trámite','2','Returnado','3','Concluido', 'Sin/Turnar') StatusReceive, " +
			"Sof_Documento_Turnar.Instruccion Instruccion, " +
			"Sof_Turnado_Nombre.Nombre TurnadoNombre, " +
			"Sof_Turnado_Area.Area TurnadoArea ";

		public const string cnsFromOrderTurnado = "FROM " +
			"Sof_Documento_Turnar, " +
			"(select DISTINCT Clave_Area, Area FROM Hint_V_Usuarios WHERE Area is not null) Sof_Turnado_Area, " +
			"(select DISTINCT ID_Usuario, Nombre FROM Hint_V_Usuarios WHERE Area is not null) Sof_Turnado_Nombre ";
				
		public const string cnsWhereOrderTurnado = "WHERE " +
			"Sof_Documento_Turnar.Documento_ID like '%' " +
			"and Sof_Documento_Turnar.Eliminado = '0' " +
			"<@STATUSRECEIVE@> " +
			"and Sof_Turnado_Nombre.ID_Usuario = Sof_Documento_Turnar.Destinatario_ID " +
			"and Sof_Turnado_Area.Clave_Area = Sof_Documento_Turnar.Destinatario_Area_ID ";


		public const string cnsColumnsOrderCCPara = "select " +
			"('CCpara') Tipo_Turno, " +
			"Sof_CCPara.Documento_ID," +
			"Decode(Sof_CCPara.Estatus,'0','Sin Abrir','1','En Trámite','2','Returnado','3','Concluido', 'Sin/Turnar') StatusReceive," +
			"('') Instruccion," +
			"Sof_Turnado_Nombre.Nombre TurnadoNombre," +
			"Sof_Turnado_Area.Area TurnadoArea ";


		public const string cnsFromOrderCCPara = "FROM " +
			"Sof_CCPara, " +
			"(Select DISTINCT Clave_Area, Area FROM Hint_V_Usuarios WHERE Area is not null) Sof_Turnado_Area, " +
			"(Select DISTINCT ID_Usuario, Clave_Area, Nombre FROM Hint_V_Usuarios WHERE Area is not null) Sof_Turnado_Nombre ";

		public const string cnsWhereOrderCCPara = "WHERE " +
			"Sof_CCPara.Documento_ID Like '%' " +
			"And Sof_CCPara.Eliminado = '0' " +
			"And Sof_CCPara.Estatus  <@STATUSRECEIVE@> " +
			"And Sof_Turnado_Nombre.ID_Usuario = Sof_CCPara.Destinatario_ID " +
			"And Sof_Turnado_Area.Clave_Area = Sof_CCPara.Destinatario_Area_ID ";
		
		private const string cnsQueryFile = "BCGStatusQueryStrings.xml";
		private const string cnsQueryPath = @"\Gestion\Data\";
		
		public Document_Alternate()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetDataSet()
		{
			string sSql =	"Select sof_documento.fecha_elaboracion, sof_documento_turnar.documento_turnar_id " +
				"From " +
				"sof_documento, sof_documento_turnar " +
				"Where sof_documento.documento_id = sof_documento_turnar.documento_id";
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}

		
		public static DataSet GetDataSet(int nId)
		{
			string sSql =	"Select sof_estatus_turnar.fecha_desde dateReply, sof_estatus_turnar.observacion Reply, sof_documento_turnar.estatus " +
				"From " +
				"sof_estatus_turnar, sof_documento_turnar " +
				"Where sof_documento_turnar.documento_turnar_id = " + nId + " " +
				"and sof_estatus_turnar.documento_turnar_id = sof_documento_turnar.documento_turnar_id " +
				"and sof_estatus_turnar.estatus = sof_documento_turnar.estatus ";
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}


		public static DataSet GetStatusFix(int nId, string sStatus)
		{
			string sSql = "Select sof_estatus_turnar.fecha_desde dateReply, sof_estatus_turnar.observacion Reply, sof_documento_turnar.estatus " +
				"From " +
				"sof_estatus_turnar, sof_documento_turnar " +
				"Where sof_documento_turnar.documento_turnar_id = " + nId + " " +
				"and sof_estatus_turnar.documento_turnar_id = sof_documento_turnar.documento_turnar_id " +
				"and sof_estatus_turnar.estatus = " + sStatus;
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetLastStatus(int nId)
		{
			string  sSql = "select nvl(max(ESTATUS),-1) estatus from sof_estatus_respuesta where documento_turnar_id = " + nId + " and eliminado = '0'" +
				"and ESTATUS_RESPUESTA_ID in (select max(ESTATUS_RESPUESTA_ID) from sof_estatus_respuesta where DOCUMENTO_TURNAR_ID = " + nId + ")";
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetStatusCCparaFix(int nId, string sStatus)
		{
			string sSql = "Select sof_estatus_ccpara.fecha_desde dateReply, sof_estatus_ccpara.observacion Reply, sof_ccpara.estatus " +
				"From " +
				"sof_estatus_ccpara, sof_ccpara " +
				"Where sof_ccpara.ccpara_id = " + nId + " " +
				"and sof_estatus_ccpara.ccpara_id = sof_ccpara.ccpara_id " +
				"and sof_estatus_ccpara.estatus = '3' ";
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			return ds;
		}


		public static void Alternate_Bis(int nId, int nDocument_Bis_Id)
		{
			
			OracleParameter [] sqlParams = {new OracleParameter("pDocumentTurnarId", null),
											new OracleParameter("pDocumentId", null)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nDocument_Bis_Id;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Alternate_Bis", sqlParams);
								
		}

		public static void Ccpara_Bis(int nId, int nDocument_Bis_Id)
		{
			
			OracleParameter [] sqlParams = {new OracleParameter("pDocumentTurnarId", null),
											   new OracleParameter("pDocumentId", null)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nDocument_Bis_Id;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Ccpara_Bis", sqlParams);
								
		}

		public static void Document_Alternate_Update(int nTurnarId, string sStatus)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pDocumentTurnarId", null),
											   new OracleParameter("pStatus", OracleType.VarChar)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nTurnarId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sStatus;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Document_Alternate_Update", sqlParams);
					
		}

		public static void Estatus_Turnar_Create(int nTurnarId, string sDateReply, string sReply, string sStatus)
		{
			OracleParameter [] sqlParams = {new OracleParameter("pIdTurnar", null),
											   new OracleParameter("pStatus", OracleType.VarChar),
											   new OracleParameter("pDateReply", OracleType.VarChar),
											   new OracleParameter("pReply", OracleType.VarChar, 512)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nTurnarId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sStatus;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sDateReply;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sReply;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Estatus_Turnar_Create", sqlParams);
					
		}

		public static void Estatus_Turnar_Cascade(int nDocumentId, string sDateReply, string sReply, string sStatus)
		{
			OracleParameter [] sqlParams = {new OracleParameter("pDocumentId", null),
											   new OracleParameter("pStatus", OracleType.VarChar),
											   new OracleParameter("pDateReply", OracleType.VarChar),
											   new OracleParameter("pReply", OracleType.VarChar, 512)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nDocumentId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sStatus;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sDateReply;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sReply;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Estatus_Turnar_Cascade", sqlParams);
					
		}


		public static void Estatus_Turnar_Update(int nTurnarId, string sReply, string sStatus)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pIdTurnar", null),
											   new OracleParameter("pStatus", OracleType.VarChar),
											   new OracleParameter("pReply", OracleType.VarChar, 512)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nTurnarId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sStatus;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sReply;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Estatus_Turnar_Update", sqlParams);
					
		}

		public static int Estatus_Turnar_Verify(int nTurnarId, string sStatus)
		{
			string sSql = "select count(*) from sof_estatus_turnar where documento_turnar_id = " + nTurnarId + " AND estatus = '" + sStatus + "'";
			return int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
		}

		public static void Document_Ccpara_Update(int nCcparaId, string sStatus)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pCcparaId", null),
											   new OracleParameter("pStatus", OracleType.VarChar)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nCcparaId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = sStatus;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Document_Ccpara_Update", sqlParams);
					
		}

		
		public static void Estatus_Ccpara_Create(int nDocumentoId, int nCcparaId, string sStatus, string sDateReply, string sReply)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pDocumentoId", null),
											   new OracleParameter("pCcparaId", null),
											   new OracleParameter("pStatus", OracleType.VarChar),
											   new OracleParameter("pDateReply", OracleType.VarChar),
											   new OracleParameter("pReply", OracleType.VarChar, 512)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nDocumentoId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nCcparaId;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sStatus ;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sStatus != "3" ? DateTime.Now.ToShortDateString() : sDateReply;

			sqlParams[4].Direction = ParameterDirection.Input;
			sqlParams[4].Value = sReply == null ? "" : sReply;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Estatus_Ccpara_Create", sqlParams);
		}

		public static void Estatus_Ccpara_Update(int nDocumentId, int nCcparaId, string sStatus, string sDateReply, string sReply)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pDocumentoId", null),
											   new OracleParameter("pCcparaId", null),
											   new OracleParameter("pStatus", OracleType.VarChar),
											   new OracleParameter("pDateReply", OracleType.VarChar),
											   new OracleParameter("pReply", OracleType.VarChar,512),

			};
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = nDocumentId;

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nCcparaId;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = sStatus;

			sqlParams[3].Direction = ParameterDirection.Input;
			sqlParams[3].Value = sDateReply;

			sqlParams[4].Direction = ParameterDirection.Input;
			sqlParams[4].Value = sReply;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.StoredProcedure,"Gestion.Estatus_CcPara_Update", sqlParams);
					
		}

		public static int Estatus_Ccpara_Verify(int nId, int nCcParaId, string sStatus)
		{
			string sSql = "select count(*) from sof_estatus_ccpara where ccpara_id = " + nCcParaId + " and estatus = '" + sStatus + "'";
			return int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
				
		}

		public static DataSet getResponse(int nTurnarId)
		{
			string sSql = "select observacion, fecha_desde from sof_estatus_turnar where documento_turnar_id = " +  nTurnarId + " and estatus = '3'" ;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static int getDocumentId(int nTurnadoId)
		{
			string sSql = "select sof_documento.documento_id from sof_documento, sof_documento_turnar where sof_documento_turnar.documento_turnar_id = " + nTurnadoId +
				"and sof_documento.documento_id = sof_documento_turnar.documento_id ";
			return int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
				
		}

		public static int getDocumentBisId(int nTurnadoId)
		{
			string sSql = "select sof_documento.documento_bis_id from sof_documento, sof_documento_turnar where sof_documento_turnar.documento_turnar_id = " + nTurnadoId +
				"and sof_documento.documento_id = sof_documento_turnar.documento_id ";
			return int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
				
		}


		public static int getDocumentCcparaId(int nTurnadoId)
		{
			string sSql = "select sof_documento.documento_id from sof_documento, sof_ccpara " +
				" where sof_ccpara.ccpara_id = " + nTurnadoId +
				" and sof_documento.documento_id = sof_ccpara.documento_id ";
			return int.Parse(OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql).ToString());
				
		}

		public static void UpdateEstatusVerifica(string sTurnar_ID, string sFlag)
		{
			string sSql = "update sof_documento_turnar set estatus_verifica = " + sFlag + " where documento_turnar_id = " + sTurnar_ID;
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
		}

		public static void UpdateEstatusSeguimiento(string sTurnar_ID, string sFlag)
		{
			string sSql = "UPDATE sof_documento_turnar SET estatus_seguimiento = " + sFlag + " WHERE documento_turnar_id = " + sTurnar_ID;
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
		}

		public static Boolean GetDisplayColumnStatus(string sUserId)
		{
			string sSql = "select confirma_concluir from sof_regla where id_empleado = " + sUserId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString() == "Si" ? true : false;
			else
				return false;
		}

		public static int GetBisId(int alternateId)
		{
			string sSql = "select documento_bis_id from sof_documento_turnar where documento_turnar_id = " + alternateId + " and eliminado = '0' ";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null && oObj.ToString() != string.Empty)
				return int.Parse(oObj.ToString());
			else
				return 0;
		}

		public static int GetDocumentAlternateId(int documentId)
		{
			string sSql = "select documento_turnar_id from sof_documento_turnar where documento_bis_id = " + documentId + " and eliminado = '0' ";
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return int.Parse(oObj.ToString());
			else
				return 0;
		}


		public static string GetStatus(int alternateId)
		{
			string sSql = "select estatus from sof_documento_turnar where documento_turnar_id = " + alternateId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return (oObj.ToString());
			else
				return "0";
		}

		public static DataSet DocumentsPending(string sDocumento_ID, string sStatusReceive)
		{

			// Armar SQL para Sof_Documento_Turnar
			StringBuilder sTemp = new StringBuilder();
			StringBuilder sSql = new StringBuilder();

			sSql.Append(cnsColumnsOrderTurnado + "\n");
			sSql.Append(cnsFromOrderTurnado + "\n");

			if (sStatusReceive == "3")   // Todos
				sTemp.Append(cnsWhereOrderTurnado.Replace("<@DOCUMENTOID@>", " LIKE '%'"));
			else
				sTemp.Append(cnsWhereOrderTurnado.Replace("<@DOCUMENTOID@>", " = " + sDocumento_ID));

			if (sStatusReceive == "3")   // Todos
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus LIKE '%'");
			else if (sStatusReceive == "2")
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus  = '3' ");
			else if (sStatusReceive == "1")
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus  <> '3' ");
		
			sSql.Append(sTemp.ToString() + " \n " + " UNION ALL " + " \n ");

			// Armar SQL para Documento_CCPara
			sSql.Append(cnsColumnsOrderCCPara + " \n ");
			sSql.Append(cnsFromOrderCCPara + " \n ");

			sTemp.Remove(0,sTemp.Length);
			if (sStatusReceive == "3")   // Todos
				sTemp.Append(cnsWhereOrderCCPara.Replace("<@DOCUMENTOID@>", "LIKE '%'"));
			else
				sTemp.Append(cnsWhereOrderCCPara.Replace("<@DOCUMENTOID@>", " = " + sDocumento_ID));
			
			if (sStatusReceive == "3")   // Todos
				sTemp.Replace("<@STATUSRECEIVE@>", " LIKE '%'");
			else if (sStatusReceive == "2")
				sTemp.Replace("<@STATUSRECEIVE@>", " = '3' ");
			else if (sStatusReceive == "1")
				sTemp.Replace("<@STATUSRECEIVE@>", " <> '3' ");

			sSql.Append(sTemp.ToString() + " \n ");

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;

		}

		public static OracleDataReader DocumentsPendingDR(string sDocumento_ID, string sStatusReceive)
		{

			// Armar SQL para Sof_Documento_Turnar
			StringBuilder sTemp = new StringBuilder();
			StringBuilder sSql = new StringBuilder();

			sSql.Append(cnsColumnsOrderTurnado + "\n");
			sSql.Append(cnsFromOrderTurnado + "\n");

			if (sStatusReceive == "3")   // Todos
				sTemp.Append(cnsWhereOrderTurnado.Replace("<@DOCUMENTOID@>", " LIKE '%'"));
			else
				sTemp.Append(cnsWhereOrderTurnado.Replace("<@DOCUMENTOID@>", " = " + sDocumento_ID));

			if (sStatusReceive == "3")   // Todos
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus LIKE '%'");
			else if (sStatusReceive == "2")
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus = '3' ");
			else if (sStatusReceive == "1")
				sTemp.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus <> '3' ");
		
			sSql.Append(sTemp.ToString() + " \n " + " UNION ALL " + " \n ");

			// Armar SQL para Documento_CCPara
			sSql.Append(cnsColumnsOrderCCPara + " \n ");
			sSql.Append(cnsFromOrderCCPara + " \n ");

			sTemp.Remove(0,sTemp.Length);
			if (sStatusReceive == "3")   // Todos
				sTemp.Append(cnsWhereOrderCCPara.Replace("<@DOCUMENTOID@>", "LIKE '%'"));
			else
				sTemp.Append(cnsWhereOrderCCPara.Replace("<@DOCUMENTOID@>", " = " + sDocumento_ID));
			
			if (sStatusReceive == "3")   // Todos
				sTemp.Replace("<@STATUSRECEIVE@>", " LIKE '%'");
			else if (sStatusReceive == "2")
				sTemp.Replace("<@STATUSRECEIVE@>", " = '3' ");
			else if (sStatusReceive == "1")
				sTemp.Replace("<@STATUSRECEIVE@>", " <> '3' ");

			sSql.Append(sTemp.ToString() + " \n ");

			OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return dr;

		}

		public static string CreateSelectTurnado(int nUserId, string sStatusReceive)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.Append(Document_Alternate.cnsColumnsOrderTurnado + "\n");
			sSql.Append(Document_Alternate.cnsFromOrderTurnado + "\n");
			sSql.Append(Document_Alternate.cnsWhereOrderTurnado + "\n");

			if ( GetDisplayColumnStatus(nUserId.ToString()) )
			{
				switch(sStatusReceive)       
				{         
					case "4":  //Seguimiento
						sSql.Replace("<@STATUSRECEIVE@>", " And Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO = '1' ");
						break;                  
					case "3":  // Todos
						sSql.Replace("<@STATUSRECEIVE@>", " And Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO like '%' ");
						break;
					case "2": // Concluidos           
						sSql.Replace("<@STATUSRECEIVE@>", " And Sof_Documento_Turnar.ESTATUS_VERIFICA = '1' ");
						break;
					default:            
						sSql.Replace("<@STATUSRECEIVE@>", " And (Sof_Documento_Turnar.ESTATUS_VERIFICA <> '1' AND Sof_Documento_Turnar.ESTATUS_SEGUIMIENTO <> '1') ");
						break;      
				}

			}
			else
			{

				if (sStatusReceive == "3")   // Todos
					sSql.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus LIKE '%'");
				else if (sStatusReceive == "2")
					sSql.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus = '3' ");
				else if (sStatusReceive == "1")
					sSql.Replace("<@STATUSRECEIVE@>", "and sof_documento_turnar.Estatus in ('0','1','2') ");
			}

			return sSql.ToString();

		}

		public static string CreateSelectCCPara(string sStatusReceive)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.Append(Document_Alternate.cnsColumnsOrderCCPara + " \n ");
			sSql.Append(Document_Alternate.cnsFromOrderCCPara + " \n ");
			sSql.Append(Document_Alternate.cnsWhereOrderCCPara + " \n ");

			if (sStatusReceive == "3")   // Todos
				sSql.Replace("<@STATUSRECEIVE@>", " LIKE '%'");
			else if (sStatusReceive == "2")
				sSql.Replace("<@STATUSRECEIVE@>", " = '3' ");
			else if (sStatusReceive == "1")
				sSql.Replace("<@STATUSRECEIVE@>", " in ('0','1','2') ");

			return sSql.ToString();
		}

		public static DateTime CalculateToDate(int nDocumentTurnId)
		{
			string sSql = "select max(fecha_hasta) from sof_estatus_turnar where documento_turnar_id = " + nDocumentTurnId;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);
			if (oObj != null)
				return oObj.ToString() != String.Empty ? Convert.ToDateTime(oObj.ToString()) : DateTime.Now;
			else
				return DateTime.Now;
		}

		public static OracleDataReader CalculateDaysByDocument(int nDocumentId)
		{

			XMLDOM oXMLDOM = new XMLDOM();
			StringBuilder oSQL = new StringBuilder();
			oSQL.Append(oXMLDOM.GetElementText(cnsQueryFile, cnsQueryPath, "qryStatusByDocument"));
			oSQL.Replace("<@DOCUMENTID@>", nDocumentId.ToString());

			OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, oSQL.ToString());
			return dr;
		}


		public static int CalculateHolidayDays(DateTime dFromDate, DateTime dToDate)
		{
			TimeSpan diff = dToDate.Subtract(dFromDate);
			return diff.Days;
		}


	}
}
