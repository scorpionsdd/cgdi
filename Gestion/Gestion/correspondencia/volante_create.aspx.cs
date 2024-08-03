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
	/// Summary description for delete_mail.
	/// </summary>
	public class volante_create : System.Web.UI.Page
	{
		protected int gnDocumentId;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.LinkButton lnkClose;

		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (!Page.IsPostBack)
			{
				gnDocumentId = Convert.ToInt32(Request["id"]);
				lblVolante.Text = Documents.GetVolanteNumber(gnDocumentId);
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
			this.lnkClose.Click += new System.EventHandler(this.lnkClose_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void lnkClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/gestion/portal/main.aspx");
		}
	}
}
