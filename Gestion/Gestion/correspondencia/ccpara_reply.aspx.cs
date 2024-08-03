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
	/// Summary description for reply alternate
	/// </summary>
	public class ccpara_reply : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtReply;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;

		protected System.Web.UI.WebControls.TextBox txtTurnarId;
		protected System.Web.UI.WebControls.TextBox txtState;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.TextBox txtSummary;
		protected System.Web.UI.WebControls.Label lblSender;
		protected System.Web.UI.WebControls.Label lblAddressee;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.Label lblDocumentDate;
		protected System.Web.UI.WebControls.Label lblRegistryDate;
		protected System.Web.UI.WebControls.Label lblReference;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtClick;
	
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
				txtTurnarId.Text	= Request.QueryString["turnadoid"];
				txtState.Text		= Request.QueryString["state"];
				txtClick.Text		= "document.forms[0]['txtDate'].focus();";
				ReplyBind();
			}
		}


		private void ReplyBind()
		{
			DataSet ds = Document_Alternate.GetStatusCCparaFix(gnId, gsState);
			txtDate.Text = DateTime.Now.ToShortDateString();
			if (ds.Tables[0].Rows.Count > 0 )
			{
				txtDate.Text = ds.Tables[0].Rows[0]["dateReply"].ToString().Substring(0,10);
				txtReply.Text = ds.Tables[0].Rows[0]["Reply"].ToString();
			}
			ds.Dispose();

			int nDocumentId = Document_Alternate.getDocumentCcparaId(gnId);
			txtDocumentId.Text	= nDocumentId.ToString();
			ds = Documents.GetDocumentsRelations(nDocumentId);
			if (ds.Tables[0].Rows.Count > 0 )
			{
				lblVolante.Text			= Documents.getTreeVolante( ds.Tables[0].Rows[0]["documento_bis_id"].ToString() ) + ds.Tables[0].Rows[0]["volante"].ToString();
				lblDocumentDate.Text	= ds.Tables[0].Rows[0]["fecha_documento_fuente"].ToString();
				lblRegistryDate.Text	= ds.Tables[0].Rows[0]["fecha_elaboracion"].ToString();
				txtSubject.Text			= ds.Tables[0].Rows[0]["asunto"].ToString();
				txtSummary.Text			= ds.Tables[0].Rows[0]["resumen"].ToString();
				lblAddressee.Text		= ds.Tables[0].Rows[0]["toName"].ToString() + " / " + ds.Tables[0].Rows[0]["toArea"].ToString();
				lblSender.Text			= ds.Tables[0].Rows[0]["fromName"].ToString() + " / " + ds.Tables[0].Rows[0]["fromArea"].ToString();
				lblReference.Text		= ds.Tables[0].Rows[0]["referencia"].ToString();
			}
			ds.Dispose();

		}


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sStatus = gsState;
			
			string sList = String.Empty;
			Documents documents = new Documents();
			
			sList = documents.SearchDependencyCC(gnCcparaId.ToString(), gnId.ToString());
			
			string [] arr = sList.Split(',');
			string sUserOwner = String.Empty;
			string sStatusLetter = String.Empty;

			for (int i=0; i < arr.Length; i+=2)
			{
				Documents.EstatusCCResponse(long.Parse(arr[i]), long.Parse(arr[i+1]), txtReply.Text, txtDate.Text, sStatus, "Create");
			}

			gsClick = "CloseWindow();";


		}

 		public int gnId
		{
			get {return Convert.ToInt32(txtTurnarId.Text);}	
			set {txtTurnarId.Text = value.ToString();}
		}

		public int gnCcparaId
		{
			get {return Convert.ToInt32(txtDocumentId.Text);}	
			set {txtDocumentId.Text = value.ToString();}
		}

		public string gsState
		{
			get {return txtState.Text;}	
			set {txtState.Text = value;}
		}

		public string gsClick
		{
			get {return txtClick.Text;}	
			set {txtClick.Text = value;}
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
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}
}
