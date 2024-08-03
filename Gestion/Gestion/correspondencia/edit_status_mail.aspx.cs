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
	public class edit_status_mail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblReference;
		protected System.Web.UI.WebControls.RadioButtonList rblStatus;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.TextBox txtResponse;
		protected System.Web.UI.WebControls.Label lblSender;
		protected System.Web.UI.WebControls.TextBox txtSummary;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.TextBox txtResponseDate;
		protected System.Web.UI.WebControls.Label lblAddressee;

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
				txtResponseDate.Text = DateTime.Now.ToShortDateString();
				getStatus();
			}
			activeResponse();

		}

		public void activeResponse()
		{
			txtResponse.Visible = false;
			if (rblStatus.SelectedItem.Value == "Concluido")
				txtResponse.Visible = true;

			if (rblStatus.SelectedItem.Value == "Tramite" && firmaUsuario.getRuleSeguimiento(int.Parse(Session["uid"].ToString())) == "Si" )
				txtResponse.Visible = true;
		}

		private void getStatus()
		{
			txtResponse.Visible = false;
			DataSet ds = Documents.GetDocumentsRelations(int.Parse(Request.QueryString["id"].ToString()));
			string sStatus = String.Empty;

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				lblVolante.Text		= Documents.getTreeVolante( dr["documento_bis_id"].ToString())+ dr["volante"].ToString();
				lblReference.Text	= dr["referencia"].ToString();
				txtSubject.Text		= dr["asunto"].ToString();
				txtSummary.Text		= dr["resumen"].ToString();
				lblSender.Text		= dr["fromName"].ToString() + dr["fromArea"].ToString() ;
				lblAddressee.Text	= dr["toName"].ToString() + dr["toArea"].ToString() ;
				sStatus 			= dr["estatus"].ToString();
				
				rblStatus.Items.FindByValue(sStatus).Selected = true;
			}

			this.btnSave.Enabled = true;
			if (sStatus == "Concluido")
			{
				txtResponse.Visible = true;
				txtResponse.Text = Documents.getResponse(int.Parse(Request.QueryString["id"].ToString()));
				this.btnSave.Enabled = false;
			}

			string sSeguimiento = firmaUsuario.getRuleSeguimiento(int.Parse(Session["uid"].ToString()));
			
			if (sSeguimiento == "Si" && sStatus == "Tramite" )
			{
				txtResponse.Visible = true;
				txtResponse.Text = Documents.getResponse(int.Parse(Request.QueryString["id"].ToString()));
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sStatus = rblStatus.SelectedItem.Value;
			
			Documents.Document_Status(int.Parse(Request.QueryString["id"].ToString()), txtResponse.Text, sStatus, txtResponseDate.Text);
		}



	}
}
