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
	/// Summary description for Adressee.
	/// </summary>
	public class Adressee
	{
		public Adressee()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetAdressee()
		{
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, "select * from int_v_usuarios order by nombre");
			return ds;

		}

		public static DataSet GetAdressee(string sWhere, string sOrder)
		{
			StringBuilder sSql		= new StringBuilder();
			StringBuilder sSqlArea	= new StringBuilder();

			sSqlArea.Append("(select distinct clave_area, area from int_v_usuarios where area is not null) sof_area, ");

			sSql.Append("Select ");
			sSql.Append("sof_documento_turnar.documento_turnar_id, sof_documento.tipo_remitente, ");
			sSql.Append("sof_documento_turnar.documento_id, sof_documento_turnar.destinatario_id, ");
			sSql.Append("sof_documento_turnar.destinatario_area_id, sof_area.area area, ");
			sSql.Append("int_v_usuarios.nombre destinatario, int_v_usuarios.id_usuario, ");
			sSql.Append("sof_documento_turnar.instruccion, ");
			sSql.Append("sof_tipo_tramite.tipo_tramite, ");
			sSql.Append("decode(sof_documento_turnar.estatus,'0','Nuevo','1','Tramite','2','Returnado','Concluido') status, ");
			sSql.Append("'id='||sof_documento_turnar.documento_id||'&sendertype='||sof_documento.tipo_remitente||'&to='||sof_documento_turnar.destinatario_id||'&turnarid='||sof_documento_turnar.documento_turnar_id paraKeys ");
			sSql.Append("From int_v_usuarios, " + sSqlArea.ToString() + " sof_documento_turnar, sof_documento, sof_tipo_tramite ");

			if (sWhere != "")
				sSql.Append(" Where " + sWhere);
			if (sOrder != "")
				sSql.Append(" Order by " + sOrder);

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}


		public static DataSet GetAdressee(string sWhere, string sOrder, string cHist)
		{
			StringBuilder sSql		= new StringBuilder();

			sSql.Append("Select ");
			sSql.Append("sof_documento_turnar.documento_turnar_id, sof_documento.tipo_remitente, ");
			sSql.Append("sof_documento_turnar.documento_id, sof_documento_turnar.id_destinatario, ");
			sSql.Append("sof_documento_turnar.id_destinatario_area, sof_areas.area, ");
			sSql.Append("sof_empleados.nombre destinatario, sof_empleados.id_empleado, ");
			sSql.Append("sof_documento_turnar.Instruccion, Sof_Documento_Turnar.Estatus_Verifica, Sof_Documento_Turnar.Estatus_Seguimiento, ");
			sSql.Append("sof_tipo_tramite.tipo_tramite, ");
			sSql.Append("decode(sof_documento_turnar.estatus,'0','Sin/Abrir','1','Tramite','2','Returnado','Concluido') status, ");
			sSql.Append("'id='||sof_documento_turnar.documento_id||'&sendertype='||sof_documento.tipo_remitente||'&to='||sof_documento_turnar.id_destinatario||'&turnarid='||sof_documento_turnar.documento_turnar_id paraKeys ");
			sSql.Append("From sof_empleados, sof_areas, sof_documento_turnar, sof_documento, sof_tipo_tramite ");

			if (sWhere != "")
				sSql.Append(" Where " + sWhere);
			if (sOrder != "")
				sSql.Append(" Order by " + sOrder);

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}


		public static DataSet GetAdresseeByDocument(int nDocumentId)
		{
			StringBuilder sSql = new StringBuilder();

			sSql.Append("Select ");
			sSql.Append("sof_documento_turnar.documento_turnar_id, sof_documento.tipo_remitente,");
			sSql.Append("sof_documento_turnar.documento_id, sof_documento_turnar.id_destinatario,");
			sSql.Append("sof_documento_turnar.id_destinatario_area id_area, sof_areas.area,");
			sSql.Append("sof_empleados.nombre destinatario, sof_empleados.id_empleado,");
			sSql.Append("sof_documento_turnar.tipo_tramite_id, sof_documento_turnar.instruccion,");
			sSql.Append("sof_tipo_tramite.tipo_tramite ");

			sSql.Append("From ");
			sSql.Append("sof_empleados, sof_areas, sof_documento_turnar, sof_documento, sof_tipo_tramite ");
			sSql.Append("Where ");
			sSql.Append("sof_documento_turnar.documento_id	= " + nDocumentId + " ");
			sSql.Append("And sof_documento_turnar.eliminado	= " + '0' + " ");
			sSql.Append("And sof_empleados.id_empleado	= sof_documento_turnar.id_destinatario ");
			sSql.Append("And sof_areas.id_area	= sof_documento_turnar.id_destinatario_area ");
			sSql.Append("And sof_documento.documento_id = " + nDocumentId + " ");
			sSql.Append("And sof_tipo_tramite.tipo_tramite_id = sof_documento_turnar.tipo_tramite_id ");
			sSql.Append("Order By sof_documento_turnar.documento_turnar_id");

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}

		public static DataSet GetAdresseeByDocument(int nDocumentId, string sTipoInstruccion)
		{
			StringBuilder sSql		= new StringBuilder();
			StringBuilder sSqlArea	= new StringBuilder();

			sSqlArea.Append("(select distinct clave_area, area from int_v_usuarios where area is not null) sof_area, ");

			sSql.Append("Select distinct (sof_documento_turnar.instruccion) instruccion ");

			sSql.Append("From ");
			sSql.Append("int_v_usuarios, " + sSqlArea.ToString() + " sof_documento_turnar, sof_documento, sof_tipo_tramite ");
			sSql.Append("Where ");
			sSql.Append("sof_documento_turnar.documento_id		= " + nDocumentId + " ");
			sSql.Append("and sof_documento_turnar.eliminado			= " + '0' + " ");
			sSql.Append("and int_v_usuarios.id_usuario			= sof_documento_turnar.destinatario_id ");
			sSql.Append("and sof_area.clave_area				= sof_documento_turnar.destinatario_area_id ");
			sSql.Append("and sof_documento.documento_id			= " + nDocumentId + " ");
			sSql.Append("and sof_tipo_tramite.tipo_tramite_id	= sof_documento_turnar.tipo_tramite_id ");
			sSql.Append("order by sof_documento_turnar.documento_turnar_id");

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}


		public static DataSet GetAdresseeByDocumentDesahogo(int nDocumentId)
		{
			StringBuilder sSql		= new StringBuilder();
			StringBuilder sSqlArea	= new StringBuilder();

			sSqlArea.Append("(select distinct clave_area, area from int_v_usuarios where area is not null) sof_area, ");

			sSql.Append("Select sof_estatus_turnar.observacion, sof_documento_turnar.estatus, sof_documento_turnar.instruccion, hint_v_usuarios.nombre destinatario ");

			sSql.Append("From ");
			sSql.Append("hint_v_usuarios, " + sSqlArea.ToString() + " sof_documento_turnar, sof_documento, sof_tipo_tramite, sof_estatus_turnar ");
			sSql.Append("Where ");
			sSql.Append("sof_documento_turnar.documento_id		= " + nDocumentId + " ");
			sSql.Append("and sof_documento_turnar.eliminado		= " + '0' + " ");
			sSql.Append("and hint_v_usuarios.id_usuario			= sof_documento_turnar.destinatario_id ");
			sSql.Append("and sof_area.clave_area				= sof_documento_turnar.destinatario_area_id ");
			sSql.Append("and sof_documento.documento_id			= sof_documento_turnar.documento_id ") ;
			sSql.Append("and sof_tipo_tramite.tipo_tramite_id	= sof_documento_turnar.tipo_tramite_id ");
			sSql.Append("and sof_estatus_turnar.documento_turnar_id	= sof_documento_turnar.documento_turnar_id ");
			sSql.Append("and sof_estatus_turnar.estatus = sof_documento_turnar.estatus ");
			sSql.Append("order by sof_documento_turnar.documento_turnar_id");
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}


		public static DataSet GetTurnados(int nDocumentId, string sTurnar)
		{

			StringBuilder sSqlArea = new StringBuilder();
			StringBuilder sSql = new StringBuilder();

			sSqlArea.Append(" sof_areas sof_destinatario_area, ");
			sSql.Append("select ");
			sSql.Append("sof_destinatario_titular.nombre, ");
			sSql.Append("sof_destinatario_titular.tipo_usuario, ");
			sSql.Append("sof_destinatario_area.area, ");
			sSql.Append("sof_documento_turnar.instruccion, ");
			sSql.Append("sof_tipo_tramite.tipo_tramite ");
			sSql.Append("from " + sSqlArea.ToString() + " sof_empleados sof_destinatario_titular, sof_documento_turnar, sof_tipo_tramite ");
			sSql.Append("where sof_documento_turnar.documento_id = " + nDocumentId + " and sof_documento_turnar.eliminado = '" + "0'");
			if (sTurnar != "*")
				sSql.Append("and sof_documento_turnar.id_destinatario = " + sTurnar + " ");

			sSql.Append("and sof_tipo_tramite.tipo_tramite_id = sof_documento_turnar.tipo_tramite_id ");
			sSql.Append("and sof_destinatario_area.id_area = sof_documento_turnar.id_destinatario_area ");
			sSql.Append("and sof_destinatario_titular.id_empleado = sof_documento_turnar.id_destinatario ");
			sSql.Append("order by sof_documento_turnar.documento_turnar_id");
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}


		public static DataSet GetAdresseeTurnar(string sWhere, string sOrder)
		{
			StringBuilder sSql = new StringBuilder();

			sSql.Append("select sof_documento_turnar.*, int_v_usuarios.nombre destinatario, int_v_usuarios.id_usuario " +
						" from int_v_usuarios, sof_documento_turnar, sof_tipo_tramite, sof_tipo_tramite ");
			if (sWhere != "")
				sSql.Append(" Where " + sWhere);
			if (sOrder != "")
				sSql.Append(" Order by " + sOrder);

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql.ToString());
			return ds;
		}

		public static DataSet GetAdresseeDisponibility(string sWhere, string sOrder)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.Append("select apellidonombre|| ' / ' || area destinatario, id_empleado from sof_empleados, sof_areas ");
			if (sWhere != "")
				sSql.Append(" Where " + sWhere);
			if (sOrder != "")
				sSql.Append(" Order by " + sOrder);

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql.ToString());
			return ds;

		}

		public static int AdresseeInsert(int nDocumentId, string sTargetsId, string sInstruction)
		{
			OracleParameter [] oParam = new OracleParameter[3];

			oParam[0] =  new OracleParameter("pDocumentId", null);
			oParam[0].Value = nDocumentId;

			oParam[1] = new OracleParameter("pDestinatario", OracleType.VarChar);
			oParam[1].Value = sTargetsId;

			oParam[2] = new OracleParameter("pInstruction", OracleType.VarChar, 512);
			oParam[2].Value = sInstruction;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Addresse_Create", oParam));

			return nVal;

		}

		public static void AdresseeUpdate(int nId, string sArea, string sInstruccion, int nTipoTramiteId )
		{
			OracleParameter [] oParam = new OracleParameter[4];

			oParam[0] = new OracleParameter("pDocumentoTurnarId", null);
			oParam[0].Value = nId;

			oParam[1] = new OracleParameter("pAreaId", OracleType.VarChar);
			oParam[1].Value = sArea == "" ? " " : sArea;

			oParam[2] = new OracleParameter("pInstruccion", OracleType.VarChar, 512);
			oParam[2].Value = sInstruccion == "" ? " " : sInstruccion;

			oParam[3] = new OracleParameter("pTipoTramiteId", null);
			oParam[3].Value = nTipoTramiteId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.AddresseeUpdate", oParam));

		}

		public static void AdresseeDelete(int nId)
		{
			OracleParameter [] oParam = new OracleParameter[1];

			oParam[0] = new OracleParameter("pDocumentoTurnarId", null);
			oParam[0].Value = nId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.AddresseeDelete", oParam));

		}

		public static int AddresseeTurnarInsert(int nDocumentId, string sAddressee)
							
		{
			OracleParameter [] oParam = new OracleParameter[2];

			oParam[0] = new OracleParameter("pDocumentId", null);
			oParam[0].Value = nDocumentId;

			oParam[1] = new OracleParameter("pAddressee", OracleType.VarChar);
			oParam[1].Value = sAddressee;


			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.Document_Addresse_Create",oParam));

			return nVal;
		}

	}
}
