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
using System.Collections.Generic;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for MapeoRegla.
	/// </summary>
	public class MapeoRegla
	{
		public MapeoRegla()
		{
		}

		public static DataSet GetMapeoRegla(string sFilter, string uid, string ip, string sessionId, string expedient)
		{
			string sSql = "Select " +
				"mr.rowid, mr.id_empleado, usr.apellidonombre nombre, mr.id_empleado_bis, addr.apellidonombre nombreAdd "+
				"From sof_mapeo_regla mr, sof_empleados usr, sof_empleados addr " +
				"Where mr.id_empleado = usr.id_empleado " +
				"And mr.id_empleado_bis = addr.id_empleado " +
				"Order by usr.apellidonombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Enviados / Lista Enviados", enuAction.Retrieve.GetDescription(), "sof_mapeo_regla mr, sof_empleados usr, sof_empleados addr", sSql, ip, sessionId, expedient));

            return ds;
		}

		public static DataSet GetMapeoReglaRowId(string rowId, string uid, string ip, string sessionId, string expedient)
		{
			string sSql = "Select " +
				"mr.rowid, mr.id_empleado, usr.apellidonombre nombre, mr.id_empleado_bis, addr.apellidonombre nombreAdd "+
				"From sof_mapeo_regla mr, sof_empleados usr, sof_empleados addr " +
				"Where mr.rowId = '" + rowId + "' " +
				"And mr.id_empleado = usr.id_empleado " +
				"And mr.id_empleado_bis = addr.id_empleado " +
				"Order by usr.apellidonombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Enviados / Edición Correo Enviado", enuAction.Retrieve.GetDescription(), "sof_mapeo_regla mr, sof_empleados usr, sof_empleados addr", sSql, ip, sessionId, expedient));

            return ds;
		}

		public static int Create(int empId, int empBisId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pEmpleadoId", null),
											new OracleParameter("pEmpleadoBisId", null),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = empId;
			oParam[1].Value = empBisId;
			oParam[2].Direction = ParameterDirection.InputOutput;
			oParam[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_mapeo_regla_create",oParam);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Enviados / Edición Correo Enviado", enuAction.Create.GetDescription(), "sp_mapeo_regla_create", string.Join(",", oParam.ToList()), ip, sessionId, expedient));
            object obj = oParam[2].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(string rowId, int empId, int empBisId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pRowId", OracleType.VarChar),
											new OracleParameter("pEmpleadoId", null),
											new OracleParameter("pEmpleadoBisId", null)
										};
			
			oParam[0].Value = rowId;
			oParam[1].Value = empId;
			oParam[2].Value = empBisId;
            #region Log
            OracleParameter[] oParamMetadata = {
                                            new OracleParameter("pRowId", OracleType.VarChar),
                                            new OracleParameter("outCursor", OracleType.Cursor)};
            List<Item> fieldMetadata = new List<Item> {
            { new Item { label="id del empleado", text ="id_empleado", value = "pEmpleadoId" } },
            { new Item { label="Destinatario", text ="id_destinatario", value = "pEmpleadoBisId" } },
            };
            oParamMetadata[0].Value = rowId;
            oParamMetadata[1].Direction = ParameterDirection.Output;
            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, oParamMetadata
                , fieldMetadata
                , "SP_MAPEO_RECIBIDOS_UPDATE_LOG", enuActionTrack.Update
                );
            #endregion
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_mapeo_regla_update",oParam);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Enviados / Edición Correo Enviado", enuAction.Update.GetDescription(), "sp_mapeo_regla_update", string.Join(",", oParam.ToList()), ip, sessionId, expedient, metadata.result));
        }

		public static void Remove(string rowId, string uid, string ip, string sessionId, string expedient)
		{
			OracleParameter [] oParam = {new OracleParameter("pRowId", OracleType.VarChar)};
			oParam[0].Value = rowId;
			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_mapeo_regla_remove",oParam);

            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Enviados / Edición Correo Enviado", enuAction.Delete.GetDescription(), "sp_mapeo_regla_remove", string.Join(",", oParam.ToList()), ip, sessionId, expedient));
        }


	}
}
