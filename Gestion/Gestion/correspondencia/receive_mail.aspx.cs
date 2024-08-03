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
	public class frmReceiveMail : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}

		}

		public string createAttach(int nId)
		{
			return Attached.GetFileAttach(nId,"T");
		}

		protected string GetStatus()
		{
			int alternateId = int.Parse(Request.QueryString["turnadoid"]);
			return Document_Alternate.GetStatus(alternateId);
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
			this.ID = "frmReceiveMail";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
