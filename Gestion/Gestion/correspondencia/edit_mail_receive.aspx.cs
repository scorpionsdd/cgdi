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
using System.Text;
using Gestion.BusinessLogicLayer;


namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for insert_mail_addressee.
	/// </summary>
	public class edit_mail_receive : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.TextBox txtFecha;
		protected System.Web.UI.WebControls.TextBox txtRespuesta;
		protected System.Web.UI.WebControls.Button btnSend;
		public static int gnDocumentAlternateId;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			gnDocumentAlternateId = Convert.ToInt32(Request.QueryString["id"]);

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
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			this.ID = "edit_take_turns";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void btnSend_Click(object sender, System.EventArgs e)
		{
			int nVal = Documents.DocumentTurnarInsert(gnDocumentAlternateId,"R","1",txtRespuesta.Text);
			Response.Redirect("receive_mail.aspx");
		}



	}
}
