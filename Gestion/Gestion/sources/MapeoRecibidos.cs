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
	/// Summary description for MapeoRecibidos.
	/// </summary>
	public class MapeoRecibidos
	{
		public MapeoRecibidos()
		{
		}

		public static DataSet GetMapeoRecibidos(string sFilter, string uid, string ip, string sessionId, string expedient)
		{
			string sSql = "Select " +
				"mr.rowid, mr.id_empleado, usr.apellidonombre nombre, mr.id_destinatario, addr.apellidonombre nombreAdd "+
				"From sof_mapeo_recibidos mr, sof_empleados usr, sof_empleados addr " +
				"Where mr.id_empleado = usr.id_empleado " +
				"And mr.id_destinatario = addr.id_empleado " +
				"Order by usr.apellidonombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Recibidos / Lista Recibidos", enuAction.Retrieve.GetDescription(), "sof_mapeo_recibidos mr, sof_empleados usr, sof_empleados addr", sSql, ip, sessionId, expedient));

            return ds;
		}

		public static DataSet GetMapeoRecibidosRowId(string rowId, string uid, string ip, string sessionId, string expedient)
		{
			string sSql = "Select " +
				"mr.rowid, mr.id_empleado, usr.apellidonombre nombre, mr.id_destinatario, addr.apellidonombre nombreAdd "+
				"From sof_mapeo_recibidos mr, sof_empleados usr, sof_empleados addr " +
				"Where mr.rowId = '" + rowId + "' " +
				"And mr.id_empleado = usr.id_empleado " +
				"And mr.id_destinatario = addr.id_empleado " +
				"Order by usr.apellidonombre";

			DataSet ds = OracleHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sSql);
            var metadata = ControlLog.GetInstance().GetMetadata(null, null
                    , new List<Item> { new Item { text = "Mapeo Recibido", value = "rowid" } ,
                                    { new Item { text="Usuario", value="rowid",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO in (select ID_EMPLEADO from sof_mapeo_recibidos where rowid={0})" } },
									{ new Item { text="Destinatario", value="rowid",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO in (select ID_DESTINATARIO from sof_mapeo_recibidos where rowid={0})"} },
                    }
                    , null, enuActionTrack.Retrieve, ds
                    );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Recibidos / Edici�n Correo Recibido", enuAction.Retrieve.GetDescription(), "sof_mapeo_recibidos mr, sof_empleados usr, sof_empleados addr", sSql, ip, sessionId, expedient,metadata.result));

            return ds;
		}

		public static int Create(int empId, int destId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pEmpleadoId", null),
											new OracleParameter("pDestinatarioId", null),
											new OracleParameter("pResult", null)
										};
			
			oParam[0].Value = empId;
			oParam[1].Value = destId;
			oParam[2].Direction = ParameterDirection.InputOutput;
			oParam[2].Value = 0;

			OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_mapeo_recibidos_create",oParam);
            List<Item> fieldMetadata = new List<Item> {
                { new Item { label="Usuario", text ="ID_EMPLEADO", value = "pEmpleadoId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}" } },
                { new Item { label="Destinatario", text ="ID_DESTINATARIO", value = "pDestinatarioId" ,sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}"} },
            };
            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, null
                , fieldMetadata
                , null, enuActionTrack.Create
                );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Recibidos / Edici�n Correo Recibido", enuAction.Create.GetDescription(), "sp_mapeo_recibidos_create", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient,metadata.result));
            object obj = oParam[2].Value;
			return int.Parse(obj.ToString());
		}

		public static void Update(string rowId, int empId, int destId, string uid, string ip, string sessionId, string expedient)
		{

			OracleParameter [] oParam = {	
											new OracleParameter("pRowId", OracleType.VarChar),
											new OracleParameter("pEmpleadoId", OracleType.Number),
											new OracleParameter("pDestinatarioId", OracleType.Number)
										};
			
			oParam[0].Value = rowId;
			oParam[1].Value = empId;
			oParam[2].Value = destId;
            #region Log
            OracleParameter[] oParamMetadata = {
                                            new OracleParameter("pRowId", OracleType.VarChar),
                                            new OracleParameter("outCursor", OracleType.Cursor)};
            List<Item> fieldMetadata = new List<Item> {
                { new Item { label="Usuario", text ="ID_EMPLEADO", value = "pEmpleadoId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}" } },
                { new Item { label="Destinatario", text ="ID_DESTINATARIO", value = "pDestinatarioId" ,sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO={0}"} },
            };
            oParamMetadata[0].Value = rowId;
            oParamMetadata[1].Direction = ParameterDirection.Output;
            var metadata = ControlLog.GetInstance().GetMetadata(OracleHelper.ExecuteReader, oParam, oParamMetadata
                , fieldMetadata
                , "SP_MAPEO_RECIBIDOS_UPDATE_LOG", enuActionTrack.Update
                );
            #endregion
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
				CommandType.StoredProcedure, "sp_mapeo_recibidos_update",oParam);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
				, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Recibidos / Edici�n Correo Recibido", enuAction.Update.GetDescription(), "sp_mapeo_recibidos_update", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result));
        }

		public static void Remove(string rowId, string uid, string ip, string sessionId, string expedient)
		{
			OracleParameter [] oParam = {new OracleParameter("pRowId", OracleType.VarChar)};
			oParam[0].Value = rowId;
            var metadata = ControlLog.GetInstance().GetMetadata(oParam, null
			   , new List<Item> { 
				   new Item { text = "Mapeo Recibido", value = "pRowId" },
                    { new Item { text="Usuario", value="pRowId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO in (select ID_EMPLEADO from sof_mapeo_recibidos where rowid={0})" } },
                    { new Item { text="Destinatario", value="pRowId",sentence="select sof_empleados.apellidonombre||'-'||sof_areas.area apearea from cgestion.sof_empleados, cgestion.sof_areas Where sof_empleados.id_area = sof_areas.id_area(+) AND ID_EMPLEADO in (select ID_DESTINATARIO from sof_mapeo_recibidos where rowid={0})"} },
               }
			   , null, enuActionTrack.Delete, null
			   );
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
			, new LogSystem(Convert.ToInt32(uid), "Pantalla Vista de Recibidos / Edici�n Correo Recibido", enuAction.Delete.GetDescription(), "sp_mapeo_recibidos_remove", string.Join(",", oParam.Select(x => string.Format("{0}={1}", x.ParameterName, x.Value)).ToList()), ip, sessionId, expedient, metadata.result));
            OracleHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["ConnectionString"],
			CommandType.StoredProcedure, "sp_mapeo_recibidos_remove", oParam);
        }
	}
}
