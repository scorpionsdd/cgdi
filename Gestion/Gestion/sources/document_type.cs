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
	public class document_type
	{
		public document_type()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataSet GetRecords(int nUserId)
		{
			string sTemp = String.Empty;
			string sSql = String.Empty;

			string sWhere = getDocumentByArea(nUserId);
			sSql = "select * from sof_tipo_documento " + sWhere;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		private static string getDocumentByArea(int nUserId)
		{
			string sSql = "select max(documento_tipo) documento_tipo, max(id_tipo_documento_empleado) id_tipo_documento_empleado, max(id_tipo_documento_area) id_tipo_documento_area " +
				"From sof_regla Where id_empleado = " +  nUserId; //+ " And eliminado = '0' ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);

			string sWhere = "Where 1=1";
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if ( dr["documento_tipo"].ToString().ToUpper() == "AREA")
					sWhere = " Where id_area = " + dr["id_tipo_documento_area"].ToString() + " And eliminado = '0' order by tipo_documento ";
				else
					sWhere = " Where id_empleado = " + dr["id_tipo_documento_empleado"].ToString() + " And eliminado = '0' order by tipo_documento "; 
			}
			return sWhere;
		}

		private static DataSet getReglaDocumentType(int nUserId)
		{
			string sSql =	"Select documento_tipo, id_tipo_documento_empleado, id_tipo_documento_area " +
							"From sof_regla " +
							"Where id_empleado = " +  nUserId + " And eliminado = '0' ";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],CommandType.Text, sSql);

			return ds;
		}



		public static DataSet GetDocumentType(string sWhere)
		{
			string sSql = "select * from sof_tipo_documento where " + sWhere;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetDocumentType(string sWhere, string sOrder)
		{

			string sSql = "select * from sof_tipo_documento where " + sWhere + " order by " + sOrder;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static DataSet GetDocumentTypeRel(string sWhere, string sOrder)
		{

			StringBuilder sSql = new StringBuilder();
			sSql.Append("select sof_documento.*, sof_tipo_documento.tipo_documento from sof_documento, sof_tipo_documento");
			
			if (sWhere != "")
				sSql.Append(" where " + sWhere);
			if (sOrder != "")
				sSql.Append(" order by " + sOrder);
			
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql.ToString());
			return ds;
		}

		public static void CreateDocumentType(string sType)
		{

			OracleParameter [] parms = {new OracleParameter("pType",OracleType.VarChar, 100)};

			parms[0].Value = sType;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
										CommandType.StoredProcedure,"Gestion.CreateDocumentType", parms));
		
		}

		public static int DeleteRecords(int nRecordId)
		{
			string sSql = "update sof_tipo_documento set eliminado = '1' where tipo_documento_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return nVal;
		}

		public static int UpdateRecords(int nRecordId, string sTipoDocumento)
		{
			string sSql = "update sof_tipo_documento set tipo_documento = '" + sTipoDocumento + "' where tipo_documento_id = " + nRecordId;
			int nVal = OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text,sSql);
			return nVal;
		}

		public static int CreateRecords(string sTipoDocumento, int nUserId)
		{

			OracleParameter [] sqlParams = {new OracleParameter("pDocumentType", OracleType.VarChar, 100),
											new OracleParameter("pUserAreaId", null),
											new OracleParameter("pUserArea", OracleType.VarChar),
											new OracleParameter("nResult", null)
										   };
			
			sqlParams[0].Direction = ParameterDirection.Input;
			sqlParams[0].Value = sTipoDocumento;
			int nTemp = 0;
			DataSet ds = getReglaDocumentType(nUserId);

			if (ds.Tables[0].Rows[0]["documento_tipo"].ToString() == "Area")
				nTemp = int.Parse(ds.Tables[0].Rows[0]["id_tipo_documento_area"].ToString() );
			else
				nTemp = int.Parse(ds.Tables[0].Rows[0]["id_tipo_documento_empleado"].ToString() );
				

			sqlParams[1].Direction = ParameterDirection.Input;
			sqlParams[1].Value = nTemp;

			sqlParams[2].Direction = ParameterDirection.Input;
			sqlParams[2].Value = ds.Tables[0].Rows[0]["documento_tipo"].ToString();

			sqlParams[3].Direction = ParameterDirection.InputOutput;
			sqlParams[3].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"], CommandType.StoredProcedure, "Gestion.document_type_create", sqlParams);
					
			object oVal = sqlParams[3].Value;
			return Convert.ToInt32(oVal.ToString());
		}

		public static string getUseCatalog(string sUserID)
		{
			string sSql = "select sof_regla.tipo_documento_catalogo from sof_regla where id_empleado = " + sUserID ;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "No";
		}

		public static int getID(string sTipo_Documento, int nUser)
		{
			string sSql = string.Empty;
			int nId = 0;
			DataSet ds = getReglaDocumentType( nUser);
			
			if (ds.Tables[0].Rows[0]["documento_tipo"].ToString() == "Area")
			{
				nId = int.Parse(ds.Tables[0].Rows[0]["id_tipo_documento_area"].ToString() );
				sSql = "select sof_tipo_documento.tipo_documento_id from sof_tipo_documento Where tipo_documento = '" + sTipo_Documento + "'" + " and id_area = " + nId;
			}
			else
			{
				nId = int.Parse(ds.Tables[0].Rows[0]["id_tipo_documento_empleado"].ToString() );
				sSql = "Select sof_tipo_documento.tipo_documento_id From sof_tipo_documento Where tipo_documento = '" + sTipo_Documento + "'" + " and id_empleado = " + nId;
			}
			
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return int.Parse(oObj.ToString());
			else
				return CreateRecords(sTipo_Documento, nUser );
		}

		public static string getTipoDocumento(string sID)
		{
			string sSql = "select sof_tipo_documento.tipo_documento from sof_tipo_documento where tipo_documento_id = " + sID;
			object oObj = OracleHelper.ExecuteScalar(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			if (! (oObj == null) )
				return oObj.ToString();
			else
				return "No existe el tipo de documento No. " + sID;
		}


	}
}
