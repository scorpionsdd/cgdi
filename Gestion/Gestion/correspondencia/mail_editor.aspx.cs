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
	/// Summary description for mail_editor.
	/// </summary>
	public class mail_editor : System.Web.UI.Page
	{
		private const string cHeader = "<TR><TD valign=top nowrap><b>Tipo de Documento</b></TD>" +
                  "<TD valign=top colspan=4><@DOCUMENT_TYPE@></TD>" +
                  "<TD valign=top><b>Fecha del Documento:</b>&nbsp;&nbsp;<@APPLICATION_DATE@></TD>" +
                  "<TD valign=top><b>Fecha de Registro:</b>&nbsp;&nbsp;<@REGISTER_DATE@></TD>" +
                  "<TD valign=top><b>Volante No.:</b></TD>" +
                  "<TD valign=top align=right nowrap><font color=red><B><@TRANSACTION_NUMBER@></B></font></TD></TR>";

		private const string cnsItem0 = "<TR><TD colSpan='4' class='header-gray' align='middle'> <a href='' onclick=\"return(viewAttach('<@PATHFILE@>'));\"><@FILE@></a></TD></TR>";

		protected System.Web.UI.WebControls.HyperLink btnSend;
		protected System.Web.UI.WebControls.TextBox txtQueryType;
		protected System.Web.UI.WebControls.LinkButton lnkHeader;
		protected System.Web.UI.WebControls.LinkButton btnAddressee;
		protected System.Web.UI.WebControls.LinkButton btnWitCopyBy;
		protected System.Web.UI.WebControls.LinkButton btnAttach;
		protected System.Web.UI.WebControls.DataGrid dgWithCopyFor;
		protected System.Web.UI.WebControls.DataGrid dgAttach;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtBind;
		protected System.Web.UI.WebControls.TextBox txtMailType;
		protected System.Web.UI.WebControls.TextBox txtHeader;
		protected System.Web.UI.WebControls.TextBox txtAreaId;
		protected System.Web.UI.WebControls.TextBox txtStatus;
		protected System.Web.UI.WebControls.TextBox txtElaborationDateFrom;
		protected System.Web.UI.WebControls.TextBox txtElaborationDateTo;
		protected System.Web.UI.WebControls.DataGrid dgAddreesse;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if ( ! Page.IsPostBack)
			{
				txtDocumentId.Text	= Request.QueryString["id"];
				txtQueryType.Text	= Request.QueryString["QueryType"];
				txtAction.Text		= Request.QueryString["action"];
				txtBind.Text		= Request.QueryString["id"] + "&sendertype=" + Request.QueryString["action"] + "&to=*";
				txtMailType.Text	= Request.QueryString["mailtype"];
				txtHeader.Text		= CreateHeader(documentId);

				string[] aFilter = Request.QueryString["filter"].Split(',');
				txtAreaId.Text = aFilter[0];
				txtStatus.Text = aFilter[1];
				txtElaborationDateFrom.Text	= aFilter[2];
				txtElaborationDateTo.Text	= aFilter[3];

				CreateAddressee(documentId);
				CreateWithCopyFor(documentId);

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
			this.lnkHeader.Click += new System.EventHandler(this.lnkHeader_Click);
			this.btnAddressee.Click += new System.EventHandler(this.btnAddressee_Click);
			this.btnWitCopyBy.Click += new System.EventHandler(this.btnWitCopyBy_Click);
			this.btnAttach.Click += new System.EventHandler(this.lbtnAttach_Click);
			this.dgAddreesse.SelectedIndexChanged += new System.EventHandler(this.dgAddreesse_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/gestion/correspondencia/edit_mail_addressee.aspx?id=" + txtDocumentId.Text);
		}

		private string CreateHeader(int nDocumentType)
		{
			
			StringBuilder sTmp = new StringBuilder();
			string sWhere = "sof_documento.documento_id = " + nDocumentType + " and sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id";

			DataSet ds = document_type.GetDocumentTypeRel(sWhere, "");

			this.btnAddressee.Enabled = true;
			this.btnWitCopyBy.Enabled = true;
			this.btnAttach.Enabled = true;

			if (gsAction == "COPIA")
				btnAddressee.Enabled = false;

			foreach (DataRow r in ds.Tables[0].Rows)
			{
				sTmp.Append(cHeader.Replace("<@DOCUMENT_TYPE@>",r["tipo_documento"].ToString()));
				sTmp.Replace("<@APPLICATION_DATE@>",r["fecha_documento_fuente"].ToString().Substring(0,10));
				sTmp.Replace("<@REGISTER_DATE@>",r["fecha_elaboracion"].ToString());
				sTmp.Replace("<@TRANSACTION_NUMBER@>",Documents.getTreeVolante( r["documento_bis_id"].ToString()) + r["volante"].ToString());

				if (r["estatus"].ToString() == "Concluido")
				{
					this.btnAddressee.Enabled = false;
					this.btnWitCopyBy.Enabled = false;
					this.btnAttach.Enabled = false;
				}
			}
			ds.Dispose();
			return sTmp.ToString();
		}

		private void btnWitCopyBy_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("/gestion/correspondencia/edit_mail_withcopyby.aspx?id=" + txtDocumentId.Text + "&action=" + txtAction.Text + "&filter=" + Request["filter"].ToString());
		}

		private void lbtnAttach_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("/gestion/correspondencia/edit_mail_attach.aspx?id=" + txtDocumentId.Text + "&action=" + txtAction.Text + "&filter=" + Request["filter"].ToString() + "&mailtype=T" );
		}

		private void btnAddressee_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/gestion/correspondencia/edit_mail_addressee.aspx?id=" + txtDocumentId.Text + "&action=" + txtAction.Text + "&filter=" + Request["filter"].ToString());
		}

		private void CreateAddressee(int nDocumentId)
		{
			string sWhere = "sof_documento_turnar.documento_id=" + txtDocumentId.Text.ToString() +
							" and sof_documento_turnar.eliminado = '0' " +
							" and sof_empleados.id_empleado = sof_documento_turnar.id_destinatario " +
							" and sof_areas.id_area  = sof_documento_turnar.id_destinatario_area " +
							" and sof_documento.documento_id = " + nDocumentId + " " +
							" and sof_tipo_tramite.tipo_tramite_id = sof_documento_turnar.tipo_tramite_id ";
		    string sOrder = "nombre";

			dgAddreesse.DataSource = Adressee.GetAdressee(sWhere, sOrder, "HISTORY");
			dgAddreesse.DataBind();

		}

		private void CreateWithCopyFor(int nDocumentId)
		{
			dgWithCopyFor.DataSource = WithCopyFor.GetWithCopyFor(documentId, "HISTORY");
			dgWithCopyFor.DataBind();
		}

		public string createAttach(int nId)
		{

			return Attached.GetFileAttach(nId, "T");
		}

		private void lnkSend_Mail_Click(object sender, System.EventArgs e)
		{
		
			Response.Redirect("print_alternate.aspx?id=" + txtDocumentId.Text + "&sendertype=" + txtAction.Text + "&to=*");
		}

		private void lnkHeader_Click(object sender, System.EventArgs e)
		{

			if (txtQueryType.Text == "M" || txtAction.Text == "CREATE_DOCUMENT")
				Server.Transfer("CreateMail.aspx?id=" + txtDocumentId.Text + "&action=U" + "&turno=U" + "&mailtype=" + txtMailType.Text + "&filter=" + txtAreaId.Text + "," + txtStatus.Text  + "," + txtElaborationDateFrom.Text + "," + txtElaborationDateTo.Text);
			else
				Server.Transfer("document_display.aspx?id=" + txtDocumentId.Text + "&action=" + txtAction.Text + "&turno=U" + "&mailtype=" + txtMailType.Text + "&querytype=" + txtQueryType.Text + "&filter=" + txtAreaId.Text + "," + txtStatus.Text  + "," + txtElaborationDateFrom.Text + "," + txtElaborationDateTo.Text); 
		}

		private void lnkStatus_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("edit_status_mail.aspx?id=" + txtDocumentId.Text);
		}

		public void dgAddreesse_Edit(Object sender, DataGridCommandEventArgs e)
		{
			// Set the current item to edit mode
			Server.Transfer("print_alternate.aspx?id=" + dgAddreesse.DataKeys[e.Item.ItemIndex] + "&sendertype=" + txtAction.Text + "&to=" + ((Label)e.Item.FindControl("lblDestinatarioId")).Text);
		}

		public void Item_Created(Object sender, DataGridItemEventArgs e)
		{
			// Set the current item to edit mode
			e.Item.Cells[7].Visible = GetDisplayColumnStatus();
			e.Item.Cells[8].Visible = GetDisplayColumnStatus();
		}

		protected Boolean GetVerifyStatus(string sValue) 
		{
			if (sValue == "1") {
				return true;
			} else {
				return false;
			}
		}

		protected Boolean GetSeguimientoStatus(string sValue) 
		{
			if (sValue == "1")
				return true;
			else
				return false;
		}


		protected Boolean GetDisplayColumnStatus() 
		{
			return Document_Alternate.GetDisplayColumnStatus(Session["uid"].ToString());
		}

		public void save_estatus_turnado(Object sender, System.EventArgs e)
		{
			CheckBox chkSelected = new CheckBox(); 
			string sDocumentTurnarID = String.Empty;
			foreach (DataGridItem dgItem in dgAddreesse.Items) 
			{
				chkSelected = (CheckBox)dgItem.FindControl("chkVerify");
				if (chkSelected.Checked)
				{
					sDocumentTurnarID =	((Label)dgItem.FindControl("lblDocumentoTurnarID")).Text;
					Document_Alternate.UpdateEstatusVerifica(sDocumentTurnarID, "1");
				}else{
					sDocumentTurnarID =	((Label)dgItem.FindControl("lblDocumentoTurnarID")).Text;
					Document_Alternate.UpdateEstatusVerifica(sDocumentTurnarID, "0");
				}
			}
			
		}

		public void save_estatus_seguimiento(Object sender, System.EventArgs e)
		{
			CheckBox chkSelected = new CheckBox(); 
			string sDocumentTurnarID = String.Empty;
			foreach (DataGridItem dgItem in dgAddreesse.Items) 
			{
				chkSelected = (CheckBox)dgItem.FindControl("chkSeguimiento");
				if (chkSelected.Checked)
				{
					sDocumentTurnarID =	((Label)dgItem.FindControl("lblDocumentoTurnarID")).Text;
					Document_Alternate.UpdateEstatusSeguimiento(sDocumentTurnarID, "1");
				}
				else
				{
					sDocumentTurnarID =	((Label)dgItem.FindControl("lblDocumentoTurnarID")).Text;
					Document_Alternate.UpdateEstatusSeguimiento(sDocumentTurnarID, "0");
				}
			}
			
		}


		private void dgAddreesse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		public int documentId 
		{
			get {return Convert.ToInt32(txtDocumentId.Text);}
			set {txtDocumentId.Text = value.ToString();}
		}
		public string gsAction
		{
			get {return txtAction.Text;}
			set {txtAction.Text = value;}
		}

		public string gsDocumentId
		{
			get {return txtBind.Text;}
			set {txtBind.Text = value;}
		}
		public string gsMailHeader
		{
			get {return txtHeader.Text;}
			set {txtHeader.Text = value;}
		}
		
	}
}
