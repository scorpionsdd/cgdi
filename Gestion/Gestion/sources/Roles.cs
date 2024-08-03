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
	public class Roles
	{
		public Roles()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static DataSet GetRoles()
		{
			string sSql =	"Select rol.rol_id, emp.nombre nombre, rol.rol rol From sof_rol rol, sof_empleados emp " +
							" Where rol.empleado_id = emp.id_empleado " +
							" and rol.eliminado = '0' order by emp.nombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}

		public static DataSet GetRolesByID(int rolId )
		{
			string sSql =	"Select rol.rol_id, emp.nombre, rol.rol, emp.id_empleado From sof_rol rol, sof_empleados emp " +
				" Where rol.empleado_id = emp.id_empleado " +
				" and rol.eliminado = '0' " +
				" and rol_id = " +  rolId + 
				" order by emp.nombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			return ds;
		}


		public static int Create(string empleadoId, string rol)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pEmpleadoId", OracleType.VarChar),
											new OracleParameter("pRol", OracleType.VarChar),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = empleadoId;
			oParam[1].Value = rol;
			oParam[2].Direction = ParameterDirection.InputOutput;
			oParam[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_roles_create",oParam);

			object obj = oParam[2].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int rolId, string empleadoId, string rol)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pRolId", null),
											new OracleParameter("pEmpleadoId", OracleType.VarChar),
											new OracleParameter("pRol", OracleType.VarChar)
										};
			
			oParam[0].Value = rolId;
			oParam[1].Value = empleadoId;
			oParam[2].Value = rol;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_roles_update",oParam);

		}

		public static void Remove(int rolId)
		{

			OracleParameter [] oParam = {new OracleParameter("pRolId", null)
										};
			
			oParam[0].Value = rolId;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_roles_remove",oParam);

		}

	}
}
