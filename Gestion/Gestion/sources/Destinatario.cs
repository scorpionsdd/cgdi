using System;
using System.Data;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using System.Configuration;
using BComponents.DataAccessLayer;

namespace Gestion.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Destinatario.
	/// </summary>
	public class Destinatario
	{
		public Destinatario()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet GetDestinatario(string sAppServer)
		{
			DataSet ds = OracleHelper.ExecuteDataset(sAppServer,
				CommandType.Text, "select * from int_v_usuarios order by nombre");
			return ds;

		}

	}
}
