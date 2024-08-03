using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Users.
	/// </summary>
	public class Puestos
	{
		public Puestos()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetPuestos(string sFilter)
		{
			string sSql = "Select * From sof_puesto Where 1=1 And Eliminado = '0' order by clave_puesto";

			if (sFilter != "")
				sSql = " Select * From sof_puesto Where 1=1 " +
					" And ( lower(descripcion) LIKE '%" + sFilter.ToLower() + "%'" + " Or lower(clave_puesto) like '%" + sFilter.ToLower() + "%') " +
					" And Eliminado = '0' " +
					" order by clave_puesto";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static int Create(string clavePuesto, string puesto)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pClavePuesto", OracleType.VarChar),
											new OracleParameter("pPuesto", OracleType.VarChar),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = clavePuesto;
			oParam[1].Value = puesto;
			oParam[2].Direction = ParameterDirection.InputOutput;
			oParam[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_puestos_create",oParam);

			object obj = oParam[2].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int puestoId, string clavePuesto, string puesto)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pPuestoId", null),
											new OracleParameter("pClavePuesto", OracleType.VarChar),
											new OracleParameter("pPuesto", OracleType.VarChar)
										};
			
			oParam[0].Value = puestoId;
			oParam[1].Value = clavePuesto;
			oParam[2].Value = puesto;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_puestos_update",oParam);

		}

		public static void Remove(int puestoId)
		{

			OracleParameter [] oParam = {new OracleParameter("pPuestoId", null)
										};
			
			oParam[0].Value = puestoId;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_puestos_remove",oParam);

		}

		public static DataSet GetPuestoById(int puestoId)
		{
			string sSql = "select *  from sof_puesto Where puesto_id = " + puestoId;
			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


	}
}
