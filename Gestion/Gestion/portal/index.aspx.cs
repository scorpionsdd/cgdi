using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Web.Services;
using System.Configuration;

namespace Gestion.Portal
{
	/// <summary>
	/// Summary description for index.
	/// </summary>
	public class index : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session.IsNewSession)
            {
                Response.Redirect("/gestion/default.aspx", false);
                return;
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
        #endregion
        [WebMethod]
        public static void SessionTimeout(string uid)
        {
            HttpContext context = HttpContext.Current;
            //context.Session["TimeOut"] = context.Session.Timeout = 1;
            context.Session["sAppServer"] = ConfigurationManager.AppSettings["ConnectionString"];


            string queryIdUser =
                   "SELECT *FROM SOF_EMPLEADOS WHERE ID_EMPLEADO=" + uid + "";
            string queryRol =
                 "SELECT *FROM SOF_ROL WHERE EMPLEADO_ID=" + uid + "";
            using (OracleConnection connection = new OracleConnection(context.Session["sAppServer"].ToString()))
            {
                OracleCommand command = new OracleCommand(queryIdUser);
                command.Connection = connection;
                try
                {
                    connection.Open();

                    OracleDataAdapter aIdUser = new OracleDataAdapter(queryIdUser, connection);
                    OracleCommandBuilder builder = new OracleCommandBuilder(aIdUser);
                    DataSet dsIdUser = new DataSet();
                    aIdUser.Fill(dsIdUser);

                    if (dsIdUser.Tables[0].Rows.Count != 0)
                    {
                        context.Session["key"] = dsIdUser.Tables[0].Rows[0]["PASSWORD"];
                        context.Session["Nombre"] = dsIdUser.Tables[0].Rows[0]["LOGIN"];
                        context.Session["uid"] = dsIdUser.Tables[0].Rows[0]["ID_EMPLEADO"];

                    }

                    OracleDataAdapter aRol = new OracleDataAdapter(queryRol, connection);
                    OracleCommandBuilder builderRol = new OracleCommandBuilder(aRol);
                    DataSet dsRol = new DataSet();
                    aRol.Fill(dsRol);
                    if (dsRol.Tables[0].Rows.Count != 0)
                    {
                        context.Session["rol"] = dsRol.Tables[0].Rows[0]["ROL"];
                    }
                    else
                    {
                        context.Session["rol"] = "ALL";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //Session.Timeout = Session.Timeout + 20;


        }

        public void SetFrames()
        {
            HttpContext context = HttpContext.Current;
            string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}
