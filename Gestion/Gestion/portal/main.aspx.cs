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


namespace Gestion.Portal
{
	/// <summary>
	/// Summary description for main.
	/// </summary>
	public class main : System.Web.UI.Page
	{
	    public string gsToDoQuickBar = "";
		public string gsTasksQuickBar = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}

			if (! Page.IsPostBack)
			{
				BOComponents.WorkFlow oTaskBars = new BOComponents.WorkFlow();
				gsToDoQuickBar = oTaskBars.ToDoQuickBar((string)Session["sAppServer"], Session["uid"].ToString() );
				gsTasksQuickBar = oTaskBars.QuickBar((string)Session["sAppServer"], Session["uid"].ToString(), Session["rol"].ToString());
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
	}
}
