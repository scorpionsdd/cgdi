using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using BComponents.DataAccessLayer;
using Log.Layer.Business;
using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Model;
using System.Collections.Generic;
using Log.Layer.Model.Extension;
using System.Linq;

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

		public static DataSet GetRolesByID(int rolId, string uid, string ip, string sessionId, string expedient)
		{
			string sSql =	"Select rol.rol_id, emp.nombre, rol.rol, emp.id_empleado From sof_rol rol, sof_empleados emp " +
				" Where rol.empleado_id = emp.id_empleado " +
				" and rol.eliminado = '0' " +
				" and rol_id = " +  rolId + 
				" order by emp.nombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
			#region LOG
			var fieldMetadata = new List<Item> { new Item { text = "Rol", value = "rol_id" },
					new Item { text="Empleado", value="ID_EMPLEADO",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}" } ,
					new Item { text="Rol", value="ROL" },
					new Item { text="Empleado", isOutput=true },
					new Item { text="Rol", isOutput=true  },
					};
            var metadata = ControlLog.GetInstance().GetMetadata(null, null
                    , fieldMetadata
                    , null, enuActionTrack.Retrieve, ds
                    );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
            , new LogSystem(Convert.ToInt32(uid), "Pantalla Lista de Roles / Edicion de Roles", enuAction.Retrieve.GetDescription(), "sof_rol , sof_empleados", sSql, ip, sessionId, expedient, metadata.result
            , string.Format("Rol consultado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "Rol").value)
            , string.Format("Empleado consultado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "Empleado").value)
            ));
            #endregion
            return ds;
		}


		public static int Create(string empleadoId, string rol, string uid, string ip, string sessionId, string expedient)
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
            #region Log
            List<Item> fieldMetadata = new List<Item> {
                { new Item { label="Empleado", text ="EMPLEADO_ID", value = "pEmpleadoId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}" } },
                { new Item { label="Rol", text ="ROL", value = "pRol" } },
					new Item { text="EMPLEADO_ID", isOutput=true },
                    new Item { text="ROL", isOutput=true  },
            };
            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, null
                , fieldMetadata
                , null, enuActionTrack.Create
                );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
            , new LogSystem(Convert.ToInt32(uid), "Pantalla Lista de Roles / Edicion de Roles", enuAction.Create.GetDescription(), "sp_roles_create", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result
            , string.Format("Rol creado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "ROL").value)
            , string.Format("Empleado creado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "EMPLEADO_ID").value)
            ));
            #endregion
            object obj = oParam[2].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(int rolId, string empleadoId, string rol, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pRolId", null),
											new OracleParameter("pEmpleadoId", OracleType.VarChar),
											new OracleParameter("pRol", OracleType.VarChar)
										};
			
			oParam[0].Value = rolId;
			oParam[1].Value = empleadoId;
			oParam[2].Value = rol;
            #region Log
            OracleParameter[] oParamMetadata = {
                                            new OracleParameter("pRolId", OracleType.Number),
                                            new OracleParameter("outCursor", OracleType.Cursor)};
            List<Item> fieldMetadata = new List<Item> {
            { new Item { label="Rol", text ="ROL", value = "pRol" } },
            { new Item { label="Empleado", text ="EMPLEADO_ID", value = "pEmpleadoId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}" } },
            new Item { text="EMPLEADO_ID", isOutput=true },
            new Item { text="ROL", isOutput=true  },
            };
            oParamMetadata[0].Value = rolId;
            oParamMetadata[1].Direction = ParameterDirection.Output;

            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, oParamMetadata
                , fieldMetadata
                , "SP_ROLES_UPDATE_LOG", enuActionTrack.Update
                );
            #endregion
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_roles_update",oParam);
            #region Log
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
                , new LogSystem(Convert.ToInt32(uid), "Pantalla Lista de Roles / Edicion de Roles", enuAction.Update.GetDescription(), "sp_roles_update", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result
            , string.Format("Rol actualizado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "ROL").value)
            , string.Format("Empleado actualizado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "EMPLEADO_ID").value)
            ));
            #endregion

        }

        public static void Remove(int rolId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {new OracleParameter("pRolId", null)
										};
			
			oParam[0].Value = rolId;
			var fieldMetadata = new List<Item> { new Item { text = "Rol", value = "pRolId" },
					new Item { text="Empleado", value="pRolId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO in (select EMPLEADO_ID from sof_rol where rol_id={0})" } ,
					new Item { text="Rol", value="pRolId",sentence="select rol from sof_rol where rol_id={0}" },
                    new Item { text="Empleado", isOutput=true },
                    new Item { text="Rol", isOutput=true  },
               };
            var metadata = ControlLog.GetInstance().GetMetadata(oParam, null
               , fieldMetadata
               , null, enuActionTrack.Delete, null
               );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
            , new LogSystem(Convert.ToInt32(uid), "Pantalla Lista de Roles / Edicion de Roles", enuAction.Delete.GetDescription(), "sp_roles_remove", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result
            , string.Format("Rol eliminado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "Rol").value)
            , string.Format("Empleado eliminado {0}", fieldMetadata.FirstOrDefault(x => x.isOutput.HasValue && x.isOutput.Value && x.text == "Empleado").value)
            ));
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
			CommandType.StoredProcedure, "sp_roles_remove", oParam);

        }

	}
}
