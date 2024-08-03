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
	/// Summary description for CreateMail.
	/// </summary>
	public class document_display: System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnCreate;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Label lblDocumentType;
		protected System.Web.UI.WebControls.Label lblDocumentDate;
		protected System.Web.UI.WebControls.Label lblReference;
		protected System.Web.UI.WebControls.Label lblTargetName;
		protected System.Web.UI.WebControls.Label lblSummary;
		protected System.Web.UI.WebControls.Label lblTargetJob;
		protected System.Web.UI.WebControls.Label lblAnexo;
		protected System.Web.UI.WebControls.Label lblSubject;
		protected System.Web.UI.WebControls.Label lblFromArea;
		protected System.Web.UI.WebControls.Label lblFromTitular;
		protected System.Web.UI.WebControls.Label lblFromJob;
		protected System.Web.UI.WebControls.Label lblTArea;
		protected System.Web.UI.WebControls.Label lblToArea;
		protected System.Web.UI.WebControls.Label lblToTitular;
		protected System.Web.UI.WebControls.Label lblToJob;
		protected System.Web.UI.WebControls.Label lblStepRequire;
		protected System.Web.UI.WebControls.Label lblTipoSolicitud;
		protected System.Web.UI.WebControls.Label lblTipoAtencion;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtSend;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtMailType;
		protected System.Web.UI.WebControls.TextBox txtQueryType;
		protected System.Web.UI.WebControls.Label lblFechaAtencion;
		protected System.Web.UI.WebControls.Label lblNombreProyecto;
		protected System.Web.UI.WebControls.Button btnReturn;

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
				txtDocumentId.Text	= Request.QueryString["id"];
				txtSend.Text		= Request.QueryString["sender"];
				txtAction.Text      = Request.QueryString["action"];
				txtMailType.Text	= Request.QueryString["mailtype"];
				txtQueryType.Text	= Request.QueryString["querytype"];

				if (gsSend == "TURN")
					btnReturn.Visible = false;
				Document();
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
			this.btnReturn.Click += new System.EventHandler(this.btnCreate_Click);
			this.ID = "CreateMail";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void Document() 
		{
			
			DataSet ds			= Documents.GetDocumentsRelations(gnDocumentId);

			if (ds.Tables[0].Rows.Count > 0 ) {
				DataRow r = ds.Tables[0].Rows[0];

				lblDocumentType.Text	= r["tipo_documento"].ToString();
				lblDocumentDate.Text	= r["fecha_documento_fuente"].ToString().Substring(0,10);
				lblReference.Text		= r["referencia"].ToString();
				lblSubject.Text			= r["asunto"].ToString();
				lblSummary.Text			= r["resumen"].ToString();
				
				lblFromArea.Text		= r["fromArea"].ToString();
				lblFromTitular.Text		= r["fromNAme"].ToString();
				lblFromJob.Text			= r["fromJob"].ToString();

				lblToArea.Text			= r["toArea"].ToString();
				lblToTitular.Text		= r["toName"].ToString();
				lblToJob.Text			= r["toJob"].ToString();

				lblAnexo.Text			= r["anexo"].ToString();
				lblStepRequire.Text		= r["requiere_tramite"].ToString();

				lblTipoSolicitud.Text    = r["tipo_solicitud"].ToString();
				lblTipoAtencion.Text    = r["tipo_atencion"].ToString();
				lblFechaAtencion.Text    = r["fecha_atencion"].ToString();
				lblNombreProyecto.Text    = r["nombre_proyecto"].ToString();
			}
		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("/Gestion/Correspondencia/mail_editor.aspx?id=" + gnDocumentId + "&action=" + txtAction.Text + "&mailtype=" + txtMailType.Text + "&filter=" + Request.QueryString["filter"]);
		}

		public int gnDocumentId 
		{
			get{ return int.Parse(txtDocumentId.Text);}
			set{ txtDocumentId.Text = value.ToString();}
		}
			
		public string gsSend
		{
			get{ return txtSend.Text;}
			set{ txtSend.Text = value;}
		}

	}
}
