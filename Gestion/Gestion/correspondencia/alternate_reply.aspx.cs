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
using System.IO;

using Gestion.BusinessLogicLayer;


namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for reply alternate
	/// </summary>
	public class alternate_reply : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.Label lblDocumentDate;
		protected System.Web.UI.WebControls.Label lblRegistryDate;
		protected System.Web.UI.WebControls.Label lblReference;
		protected System.Web.UI.WebControls.Button btnAttach;
		protected System.Web.UI.WebControls.TextBox txtSenderName;
		protected System.Web.UI.WebControls.TextBox txtSenderArea;

		protected System.Web.UI.WebControls.TextBox txtAddresseName;
		protected System.Web.UI.WebControls.TextBox txtReply;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtAddresseArea;

		protected System.Web.UI.WebControls.TextBox txtTurnarId;
		protected System.Web.UI.WebControls.TextBox txtState;
		protected System.Web.UI.WebControls.DataGrid dgAttach;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.TextBox txtSummary;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.DropDownList ddlTipoRespuesta;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTipoRespuesta;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRespuesta;
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
				txtTurnarId.Text	= Request.QueryString["id"];
				txtState.Text		= Request.QueryString["state"];
				txtClick.Text		= "document.forms[0]['txtDate'].focus();";
				ReplyBind();
			}
		}


		private void ReplyBind()
		{
			// Obtiene estatus del tipo de respuesta
			DataSet dss = Document_Alternate.GetLastStatus(gnId);
			string sLastStatus = "3";
			if (dss.Tables[0].Rows.Count > 0 )
			{
				sLastStatus = dss.Tables[0].Rows[0]["Estatus"].ToString();
			}
	
			if (sLastStatus == "3" || sLastStatus == "0") 
			{
				this.btnSave.Enabled = false;
			}

			this.btnSave.Enabled = true;

			//DataSet ds = Document_Alternate.GetStatusFix(gnId, gsState);
			DataSet ds = Document_Alternate.GetStatusFix(gnId, sLastStatus);
			txtDate.Text = DateTime.Now.ToShortDateString();
			if (ds.Tables[0].Rows.Count > 0 )
			{
				txtDate.Text = ds.Tables[0].Rows[0]["dateReply"].ToString().Substring(0,10);
				txtReply.Text = ds.Tables[0].Rows[0]["Reply"].ToString();
			}
			ds.Dispose();


			int nDocumentId = Document_Alternate.getDocumentId(gnId);
			ds = Documents.GetDocumentsRelations(nDocumentId);
			if (ds.Tables[0].Rows.Count > 0 )
			{
				lblVolante.Text			= Documents.getTreeVolante( ds.Tables[0].Rows[0]["documento_bis_id"].ToString() ) + ds.Tables[0].Rows[0]["volante"].ToString();
				lblDocumentDate.Text	= ds.Tables[0].Rows[0]["fecha_documento_fuente"].ToString();
				lblRegistryDate.Text	= ds.Tables[0].Rows[0]["fecha_elaboracion"].ToString();
				txtSubject.Text			= ds.Tables[0].Rows[0]["asunto"].ToString();
				txtSummary.Text			= ds.Tables[0].Rows[0]["resumen"].ToString();
				txtAddresseName.Text	= ds.Tables[0].Rows[0]["toName"].ToString();
				txtAddresseArea.Text	= ds.Tables[0].Rows[0]["toArea"].ToString();	
				txtSenderName.Text		= ds.Tables[0].Rows[0]["fromName"].ToString();
				txtSenderArea.Text		= ds.Tables[0].Rows[0]["fromArea"].ToString();
				lblReference.Text		= ds.Tables[0].Rows[0]["referencia"].ToString();
				ddlTipoRespuesta.SelectedValue = sLastStatus;
			}
			ds.Dispose();

		}


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//
			//string sStatus = gsState;
			string sStatus = ddlTipoRespuesta.SelectedValue;
			string sDocumentId = Document_Alternate.getDocumentId(gnId).ToString();
			string sDocTurnar  = gnId.ToString();

  			Documents.EstatusTurnaResponse(long.Parse(sDocumentId), long.Parse(sDocTurnar), txtReply.Text, txtDate.Text, sStatus, "Create");

			string sCascadeResp = Documents.getCascadeResp(int.Parse(sDocumentId));
			
			if ( sCascadeResp == "Si")
			{
				string sList = String.Empty;
				string sListTur = String.Empty;
				Documents documents = new Documents();
			
				//sList = documents.SearchDependency(sDocumentId, sDocTurnar);
				sList = documents.getVolanteRel(sDocumentId);
			
				string [] arr = sList.Split('|');

				string sUserOwner = String.Empty;
				string sStatusLetter = String.Empty;
				for (int i=1; i < arr.Length; i+=2)
				{
					sListTur = documents.getVolWithTurnado(arr[i-1].ToString(), arr[i].ToString());
					string [] arrTur = sListTur.Split(',');
					if (arrTur.Length > 0) 
					{
						for (int j=1; j < arrTur.Length; j+=2)
						{
							Documents.EstatusTurnaResponse(long.Parse(arrTur[j-1]), long.Parse(arrTur[j]), txtReply.Text, txtDate.Text, sStatus, "Create");
						}
					}
				}
			}
			gsClick = "CloseWindow();";
		}

 		public int gnId
		{
			get {return Convert.ToInt32(txtTurnarId.Text);}	
			set {txtTurnarId.Text = value.ToString();}
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
			this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.ddlTipoRespuesta.SelectedIndexChanged += new System.EventHandler(this.ddlTipoRespuesta_SelectedIndexChanged);
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAttach_Click(object sender, System.EventArgs e)
		{
			string sDocumentId = Document_Alternate.getDocumentId(gnId).ToString();
			Response.Redirect("/gestion/correspondencia/edit_attach_reply.aspx?tid=" + gnId + "&id=" + sDocumentId + "&state=" + gsState + "&mailtype=R" );
			 
		}
		public string createAttach()
		{

			int nDocumentId = Document_Alternate.getDocumentId(gnId);
			return Attached.GetFileAttach(nDocumentId, "R");
		}

		private void ddlTipoRespuesta_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string sStatus = this.ddlTipoRespuesta.SelectedValue;
			DataSet ds = Document_Alternate.GetStatusFix(gnId, sStatus);
			if (ds.Tables[0].Rows.Count > 0 )
			{
				txtDate.Text = ds.Tables[0].Rows[0]["dateReply"].ToString().Substring(0,10);
				txtReply.Text = ds.Tables[0].Rows[0]["Reply"].ToString();
			}
			ds.Dispose();
		}
	}
}
