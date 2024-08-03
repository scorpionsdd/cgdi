//using System.Data.OracleClient;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Gestion
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            Application["error_page"] = "/gestion/central/exceptions/exception.aspx";
            Application["exit_page"] = "/gestion/central/login/logout.aspx";
            Application["main_page"] = "/gestion/portal/main.aspx";

        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            //Session["sAppServer"] = "Data Source=cgestion;User ID=cgestion;Password=cgestion";
            Session["sAppServer"] = ConfigurationManager.AppSettings["ConnectionString"];
            //Session["uid"] = 0;
            //Session["user_name"] = "";
            //Session.Timeout = 2;
            Session["TimeOut"] = Session.Timeout = 480;
            Session["rxID"] = "";
            Session["rxtID"] = "";

            //Bloque de Cambios realizado por RGRG
            if (global::Gestion.Properties.Settings.Default.SET_FORMAT)
            {
                string format = global::Gestion.Properties.Settings.Default.GLOBAL_FORMAT;
                OracleConnection cn = new OracleConnection();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.Connection = cn;
                cn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                cn.Open();
                cmd1.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = '"+ format + "'";
                int r = cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                cn.Close();
            }
            //fin bloque de cambio

        }




        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {

        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }

    }
}