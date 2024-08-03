using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for withcopyfor.
	/// </summary>
	public class WithCopyFor
	{
		public WithCopyFor()
		{
		}

		public static DataSet GetWithCopyForDisponibility(int nDocumentId)
		{
			String sSql = "Select id_empleado, apellidoNombre destinatario from sof_empleados " +
				" where id_empleado not in (" +
				" select id_destinatario from sof_ccpara where documento_id = " + nDocumentId + " and eliminado = '0')" +
				" and fecha_inicio <= '21/09/2007' And fecha_fin >= '21/09/2007'" +
				" order by destinatario" ;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			return ds;

		}

		public static DataSet GetWithCopyFor(int nDocumentId)
		{
			String sSql = "Select sof_ccpara.ccpara_id, sof_ccpara.id_destinatario, sof_empleados.nombre destinatario," +
				"sof_areas.area " +
				"From sof_ccpara, sof_empleados, sof_areas " +
				"Where sof_ccpara.documento_id = " + nDocumentId +
				" And sof_ccpara.eliminado = '0' " +
				" And sof_empleados.id_empleado = sof_ccpara.id_destinatario " +
				" And sof_areas.id_area = sof_empleados.id_area " +
				"order by destinatario";

			DataSet ds  = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetWithCopyFor(int nDocumentId, string cHistory)
		{
			String sSql =	"Select sof_ccpara.ccpara_id, sof_ccpara.id_destinatario," +
							"Decode(Sof_CCPara.Estatus,'0','Sin Abrir','1','En Trámite','2','Returnado','3','Concluido', 'Sin/Turnar') Status," +
							"sof_empleados.nombre destinatario," +
							"sof_areas.area " +
							"From sof_ccpara, sof_empleados, sof_areas " +
							"Where sof_ccpara.documento_id = " + nDocumentId +
							" And sof_ccpara.eliminado = '0' " +
							" And sof_empleados.id_empleado = sof_ccpara.id_destinatario " +
							" And sof_areas.id_area = sof_ccpara.id_destinatario_area " +
							"order by destinatario";

			DataSet ds  = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
					CommandType.Text, sSql);
			return ds;

		}

		public static int WithCopyForInsert(int nDocumentId, string sTargetsId )
		{
			OracleParameter [] oParam = {new OracleParameter("pDocumentId",  null),
										 new OracleParameter("pDestinatario", OracleType.VarChar)
										};

			oParam[0].Value = nDocumentId;
			oParam[1].Value = sTargetsId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.WithCopyFor_Create", oParam));
			return nVal;

		}

		public static void WithCopyForUpdate(int nId, int nDestinatarioId )
		{
			OracleParameter [] oParam = {new OracleParameter("pWithCopyForId",null),
										 new OracleParameter("pDestinatarioId",null)
										};

			oParam[0].Value = nId;
			oParam[1].Value = nDestinatarioId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.WithCopyForUpdate", oParam));
		}


		public static void WithCopyForDelete(int nId)
		{
			OracleParameter [] oParam = {new OracleParameter("pWithCopyForId",null)};

			oParam[0].Value = nId;

			int nVal = Convert.ToInt32(OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "Gestion.WithCopyForDelete", oParam));
		}

	}
}
