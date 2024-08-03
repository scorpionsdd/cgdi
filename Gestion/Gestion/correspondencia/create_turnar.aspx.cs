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
	public class create_turnar: System.Web.UI.Page
	{
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
		protected System.Web.UI.WebControls.Button btnTurnar;
		protected System.Web.UI.WebControls.Label lblStepRequire;
		protected System.Web.UI.WebControls.DropDownList ddlSignature;
		protected System.Web.UI.WebControls.TextBox txtDocumentTurnarId;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtFromAreaId;
		protected System.Web.UI.WebControls.TextBox txtToAreaId;
		protected System.Web.UI.WebControls.TextBox txtFromTitularId;
		protected System.Web.UI.WebControls.TextBox txtToTitularId;
		protected System.Web.UI.WebControls.TextBox txtTipoRemitente;
		protected System.Web.UI.WebControls.TextBox txtTipoDocumentoId;
		protected System.Web.UI.WebControls.Label ddlTipoSolicitud;
		protected System.Web.UI.WebControls.Label ddlTipoAtencion;
		protected System.Web.UI.WebControls.TextBox txtDocumentBisId;
		protected System.Web.UI.WebControls.TextBox txtFechaAtencion;
		protected System.Web.UI.WebControls.Label lblTipoSolicitud;
		protected System.Web.UI.WebControls.Label lblTipoAtencion;
		protected System.Web.UI.WebControls.TextBox txtTipoSolicitudId;
		protected System.Web.UI.WebControls.TextBox txtTipoAtencionId;
		protected System.Web.UI.WebControls.TextBox txtFecha;
		protected System.Web.UI.WebControls.TextBox txtNombreProyecto;

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
				txtDocumentTurnarId.Text	= Request.QueryString["turnadoid"];
				txtDocumentId.Text			= Request.QueryString["documentoid"];
				txtDocumentBisId.Text		= Request.QueryString["documentoid"];

				ddlSignatureBind(int.Parse(Session["uid"].ToString()) );
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
			this.btnTurnar.Click += new System.EventHandler(this.btnTurnar_Click);
			this.ID = "CreateMail";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void ddlSignatureBind(int nUserId)
		{
			ddlSignature.DataSource = firmaUsuario.getFirmaArea(nUserId, DateTime.Now.ToShortDateString());
			ddlSignature.DataValueField = "id_empleado";
			ddlSignature.DataTextField = "nombre";
			ddlSignature.DataBind();
			DataSet ds = firmaUsuario.getRegla(int.Parse(Session["uid"].ToString()));

			if ( ds.Tables[0].Rows.Count > 0)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				ddlSignature.Items.FindByValue(dr["id_firma"].ToString()).Selected = true;
			}
			else{
				string sArea = firmaUsuario.isBoss(int.Parse(Session["uid"].ToString()));
				if (sArea != "0")
				{
					ddlSignature.Items.FindByValue(Session["uid"].ToString()).Selected = true;
				}
				else{
					ddlSignature.Items.Insert(0, new ListItem("-- Seleccione Usuario --",String.Empty));
				}
			}

	}
		private void Document() 
		{
			
			DataSet ds = Documents.GetDocumentsRelations(gnDocumentId);
			DataSet dsTurnar;
			dsTurnar = Documents.GetDocumentTurnar(gnDocumentTurnarId, Request["tipoturno"]);

			DataRow r;
			DataRow rReception;
		
			rReception = dsTurnar.Tables[0].Rows[0];

			if (ds.Tables[0].Rows.Count > 0 ) {
				r = ds.Tables[0].Rows[0];

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

				txtFromAreaId.Text		= r["toAreaId"].ToString();
				txtFromTitularId.Text	= r["toTitularId"].ToString();
			
				txtToAreaId.Text		= rReception["id_destinatario_area"].ToString();
				txtToTitularId.Text		= rReception["id_destinatario"].ToString();
				txtTipoRemitente.Text	= r["tipo_remitente"].ToString();
				txtTipoDocumentoId.Text = r["tipo_documento_id"].ToString();

				txtNombreProyecto.Text = r["nombre_proyecto"].ToString();
				lblTipoSolicitud.Text = r["tipo_solicitud"].ToString();
				lblTipoAtencion.Text  = r["tipo_atencion"].ToString();
				
				txtFechaAtencion.Text = "";

				if (! DBNull.Value.Equals(r["fecha_atencion"])) 
				{
					txtFechaAtencion.Text = (string) r["fecha_atencion"];
				}

				txtTipoSolicitudId.Text = r["id_tipo_solicitud"].ToString();
				txtTipoAtencionId.Text  = r["id_tipo_atencion"].ToString();

			}
			ds.Dispose();
		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("mail_alternate.aspx");
		}

		private void btnTurnar_Click(object sender, System.EventArgs e)
		{
			int nDocumentTypeId = int.Parse( txtTipoDocumentoId.Text );

			int nFromAreaId		= int.Parse( txtFromAreaId.Text );
			int nFromTitularId	= int.Parse( txtFromTitularId.Text );
			
			int nToAreaId		= int.Parse( txtToAreaId.Text );
			int nToTitularId	= int.Parse( txtToTitularId.Text );
			string sSenderType	= "I";
			
			int nFirmaId		= int.Parse( ddlSignature.SelectedItem.Value.ToString());
			int nVolante;
			int nDocumentId = gnDocumentId;

			int nSolicitudId = int.Parse(txtTipoSolicitudId.Text);
			int nAtencionId = int.Parse(txtTipoAtencionId.Text);

			string sFechaAtencion = txtFechaAtencion.Text;
			string sNombreProyecto = txtNombreProyecto.Text;

			nVolante = Documents.getNextVolante(int.Parse(Session["uid"].ToString()));

			nDocumentId	= Documents.Document_Create(sSenderType, nFromAreaId, nToAreaId, nFromTitularId,  nToTitularId, nDocumentTypeId, 
													lblDocumentDate.Text, lblReference.Text, lblSubject.Text, lblAnexo.Text, 
													lblSummary.Text, "Pendiente", gnDocumentBisId, int.Parse(Session["uid"].ToString()), 
													lblStepRequire.Text, nVolante, nFirmaId, nSolicitudId, nAtencionId, sFechaAtencion, sNombreProyecto);

			if (Request["tipoturno"] == "T")
			{

				Document_Alternate.Document_Alternate_Update(gnDocumentTurnarId, "2");
				Document_Alternate.Alternate_Bis(gnDocumentTurnarId, nDocumentId);
				Document_Alternate.Estatus_Turnar_Create(gnDocumentTurnarId, DateTime.Now.ToShortDateString(), "", "2");
				Response.Redirect("/Gestion/Correspondencia/mail_editor.aspx?id=" + nDocumentId + "&action=ALTERNATE_DOCUMENT" + "&mailtype=R" + "&filter=Todos,1,000000,000000");
			}
			else{
				Document_Alternate.Document_Ccpara_Update(gnDocumentTurnarId,"2");
				Document_Alternate.Ccpara_Bis(gnDocumentTurnarId, nDocumentId);
				Response.Redirect("/Gestion/Correspondencia/mail_editor.aspx?id=" + nDocumentId + "&action=COPIA" + "&mailtype=R" + "&filter=Todos,1,000000,000000");
			}
			
			
		}

		public int gnDocumentBisId
		{
			get { return Convert.ToInt32(txtDocumentBisId.Text); }
			set { txtDocumentBisId.Text = value.ToString();}
		}
		public int gnDocumentId
		{
			get { return Convert.ToInt32(txtDocumentId.Text); }
			set { txtDocumentId.Text = value.ToString();}
		}

		public int gnDocumentTurnarId
		{
			get { return Convert.ToInt32(txtDocumentTurnarId.Text); }
			set { txtDocumentTurnarId.Text = value.ToString();}
		}


	}
}
