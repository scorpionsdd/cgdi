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
using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for receive_mail.
	/// </summary>
	public class change_status_receive : System.Web.UI.Page
	{
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (! Page.IsPostBack)
			{
				ReplyBind(int.Parse(Request.QueryString["id"]), Request.QueryString["state"]);
			}
		}

		private void ReplyBind(int nId, string sStatus )
		{

			string sDate = DateTime.Now.ToShortDateString();

			Document_Alternate.Estatus_Turnar_Create(nId, sDate, "", sStatus);
			Document_Alternate.Document_Alternate_Update(nId, sStatus);

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
