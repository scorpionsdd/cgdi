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
	public class CreateMail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.Button btnCreate;
		protected System.Web.UI.WebControls.DropDownList dropDocumentType;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.TextBox txtReference;
		protected System.Web.UI.WebControls.TextBox txtSummary;
		protected System.Web.UI.WebControls.TextBox txtAreaId;
		protected System.Web.UI.WebControls.RadioButtonList rblRequire;
		protected System.Web.UI.WebControls.TextBox txtAttached;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.DropDownList dropFrom;
		protected System.Web.UI.WebControls.DropDownList dropFromName;
		protected System.Web.UI.WebControls.DropDownList dropTo;
		protected System.Web.UI.WebControls.DropDownList dropToName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvDocumentType;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvDocumentDate;
		protected System.Web.UI.WebControls.CompareValidator cvDocumentDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvFrom;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvSubject;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvFromName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvTo;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvToName;
		protected System.Web.UI.WebControls.DropDownList dropFirma;
		protected System.Web.UI.WebControls.TextBox txtVolante;

		protected System.Web.UI.WebControls.TextBox tbDocumentTypeId;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.TextBox txtDocumentoBisId;
		protected System.Web.UI.WebControls.TextBox txtDocumentoId;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtMailType;
		protected System.Web.UI.WebControls.TextBox txtFrom;
		protected System.Web.UI.WebControls.TextBox txtFromName;
		protected System.Web.UI.WebControls.TextBox txtTipoDocumento;
		protected System.Web.UI.WebControls.Button btnContinue;
		protected System.Web.UI.WebControls.TextBox txtSenderID;
		protected System.Web.UI.WebControls.ImageButton btnClose;
		protected System.Web.UI.WebControls.TextBox txtCutDate;
		protected System.Web.UI.WebControls.Button btnSelCutDate;
		protected System.Web.UI.WebControls.TextBox txtDestinatarioArea;
		protected System.Web.UI.WebControls.TextBox txtDestinatarioName;
		protected System.Web.UI.WebControls.TextBox txtDestinatarioJob;
		protected System.Web.UI.WebControls.TextBox txtRemitenteArea;
		protected System.Web.UI.WebControls.TextBox txtRemitenteName;
		protected System.Web.UI.WebControls.TextBox txtRemitenteJob;
		protected System.Web.UI.WebControls.TextBox txtDocumentType;
		protected System.Web.UI.WebControls.TextBox txtSign;
		protected System.Web.UI.WebControls.RequiredFieldValidator rvSign;
		protected System.Web.UI.WebControls.TextBox txtDestinatarioId;
		protected System.Web.UI.WebControls.TextBox txtDestAreaId;
		protected System.Web.UI.WebControls.TextBox txtRemitenteId;
		protected System.Web.UI.WebControls.TextBox txtRemAreaId;
		protected System.Web.UI.WebControls.TextBox txtSignId;
		protected System.Web.UI.WebControls.TextBox txtTipoDocumentoId;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Label lblTipoSolicitud;
		protected System.Web.UI.WebControls.Label lblTipoAtencion;

		protected System.Web.UI.WebControls.DropDownList ddlTipoSolicitud;
		protected System.Web.UI.WebControls.TextBox txtFecha;
		protected System.Web.UI.WebControls.DropDownList ddlTipoAtencion;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNombreProyecto;
		protected System.Web.UI.WebControls.TextBox txtNombreProyecto;

		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			try
			{
				if (! Page.IsPostBack) 
				{
					txtDocumentoId.Text		= Request.QueryString["id"];
					txtDocumentoBisId.Text	= Request.QueryString["id"];
					txtAction.Text			= Request.QueryString["action"];
					txtMailType.Text		= Request.QueryString["mailtype"];
					txtCutDate.Text         = DateTime.Now.ToShortDateString();

					txtCutDate.Visible = false;
					btnSelCutDate.Visible = false;

					if (txtMailType.Text == "I" || txtMailType.Text == "E")
					{
						txtCutDate.Visible = true;
						btnSelCutDate.Visible = true;
					}	
				
					dropDocumentTypeBind();
					dropTipoSolicitud();
					dropTipoAtencion();

					dropFromBind();
					dropToBind();
					dropFirmaBind();

					dropFrom.Visible	 = true;
					dropFromName.Visible = true;

					txtFrom.Visible		= false;
					txtFromName.Visible = false;
					string sUseCatalog = ExternalSender.getUseCatalog(Session["uid"].ToString());

					if (sUseCatalog == "No" && txtMailType.Text == "E")
					{
						dropFrom.Visible = false;
						dropFromName.Visible = false;
						txtFrom.Visible		= true;
						txtFromName.Visible = true;

						txtRemitenteArea.Visible = false;
						txtRemitenteName.Visible = false;
						txtRemitenteJob.Visible = false;
					}
		
					dropDocumentType.Visible = true;
					txtTipoDocumento.Visible = false;

					rvDocumentType.ControlToValidate = "dropDocumentType";
					rvFrom.ControlToValidate = "dropFrom"; 
					rvFromName.ControlToValidate = "dropFromName"; 
					rvTo.ControlToValidate = "dropTo"; 
					rvToName.ControlToValidate = "dropToName"; 
					rvSign.ControlToValidate = "dropFirma"; 

					if (gsAction == "U")
					{
						rvDocumentType.ControlToValidate = "txtTipoDocumentoId";
						rvFrom.ControlToValidate = "txtRemAreaId"; 
						rvFromName.ControlToValidate = "txtRemitenteId"; 
						rvTo.ControlToValidate = "txtDestAreaId"; 
						rvToName.ControlToValidate = "txtDestinatarioId"; 
						rvSign.ControlToValidate = "txtSignId"; 
					}


					if (document_type.getUseCatalog(Session["uid"].ToString()) == "No")
					{
						dropDocumentType.Visible = false;
						txtDocumentType.Visible = false;
						txtTipoDocumento.Visible = true;
						rvDocumentType.ControlToValidate = "txtTipoDocumento";


						if (txtMailType.Text == "E") 
						{
							rvFrom.ControlToValidate = "txtFrom";
							rvFromName.ControlToValidate = "txtFromName";
						} else {
							rvFrom.ControlToValidate = "txtRemitenteId";
							rvFromName.ControlToValidate = "txtRemAreaId";
						}
					}

					if (gsAction == "C")
					{
						txtRemitenteArea.Visible = false;
						txtRemitenteName.Visible = false;
						txtRemitenteJob.Visible = false;

						txtDestinatarioArea.Visible = false;
						txtDestinatarioName.Visible = false;
						txtDestinatarioJob.Visible = false;

						txtDocumentType.Visible = false;
						txtSign.Visible = false;

						lblMsg.Text = "Agregando Turno: ";
						setDefaultDocument();
						gnDocumentId = 0;
						btnCreate.Text	= "Crear Documento";
						btnContinue.Text = "Crear Documento";
						txtDate.Text	= DateTime.Today.ToShortDateString();
						lblVolante.Text = Documents.getVolanteUpComing(int.Parse(Session["uid"].ToString()));
					}
					else 
					{
						lblMsg.Text = "Modificando Turno: ";
						lblVolante.Text = Documents.getVolanteCurrent(gnDocumentId);
						btnCreate.Text = "Continuar";
						btnContinue.Text = "Continuar";
						setDataDocument();
					}
				}
				else if ( tbDocumentTypeId.Text != "")
				{
					dropDocumentTypeBind();
					dropDocumentType.Items.FindByValue(tbDocumentTypeId.Text).Selected = true;
					tbDocumentTypeId.Text = "";
				}
				else if ( Session["rxID"].ToString() != "")
				{
					dropFromBind();
					if (dropFrom.Items.FindByValue( Session["rxID"].ToString() ) != null)
					{
						dropFrom.Items.FindByValue(Session["rxID"].ToString()).Selected = true;
						dropFromTitularBind(Session["rxID"].ToString());
						if (dropFromName.Items.FindByValue( Session["rxtID"].ToString() ) != null)
						{
							dropFromName.Items.FindByValue(Session["rxtID"].ToString()).Selected = true;
						}

					}
					Session["rxID"] = "";
					Session["rxtID"] = "";
					txtSenderID.Text = "";
				}
			}
			catch (Exception ex)
			{
				Response.Redirect("/gestion/correspondencia/error.aspx?errMessage="+ex.Message+"&errNumber="+ex.Source.ToString());
			}
		}


		private void dropDocumentTypeBind()
		{   
			dropDocumentType.DataSource = document_type.GetRecords(int.Parse(Session["uid"].ToString()) );
			dropDocumentType.DataValueField = "tipo_documento_id";
			dropDocumentType.DataTextField = "tipo_documento";
			dropDocumentType.DataBind();
			dropDocumentType.Items.Insert(0, new ListItem("-- Seleccione Documento --",String.Empty));
		}

		private void dropTipoSolicitud()
		{   
			ddlTipoSolicitud.DataSource = TipoSolicitud.GetTipoSolicitud("V");
			ddlTipoSolicitud.DataValueField = "id_tipo_solicitud";
			ddlTipoSolicitud.DataTextField = "tipo_solicitud";
			ddlTipoSolicitud.DataBind();
			ddlTipoSolicitud.Items.Insert(0, new ListItem("-- Seleccione Solicitud --",String.Empty));
		}

		private void dropTipoAtencion()
		{   
			ddlTipoAtencion.DataSource = TipoAtencion.GetTipoAtencion("V");
			ddlTipoAtencion.DataValueField = "id_tipo_atencion";
			ddlTipoAtencion.DataTextField = "tipo_atencion";
			ddlTipoAtencion.DataBind();
			////ddlTipoAtencion.Items.Insert(0, new ListItem("-- Seleccione Atencion --",String.Empty));
		}

		public void dropFromBind() 
		{
			if (gsMailType == "E" ) 
			{
				dropFrom.DataSource = ExternalSender.GetRecords(int.Parse(Session["uid"].ToString()));
				dropFrom.DataValueField = "remitente_externo_id";
				dropFrom.DataTextField = "dependencia";
				dropFrom.DataBind();
				dropFrom.Items.Insert(0, new ListItem("-- Seleccione Remitente --",String.Empty));
			}
			else
			{
				dropFrom.DataSource = Users.GetArea(txtCutDate.Text);
				dropFrom.DataValueField	= "id_area";
				dropFrom.DataTextField	= "area";
				dropFrom.DataBind();
				dropFrom.Items.Insert(0, new ListItem("-- Seleccione el Area --",String.Empty));
			}
		}

		private void dropToBind() 
		{
			DataSet ds = Users.GetArea(txtCutDate.Text);
			dropTo.DataSource		= ds;
			dropTo.DataValueField	= "id_area";
			dropTo.DataTextField	= "area";
			dropTo.DataBind();
			dropTo.Items.Insert(0, new ListItem("-- Seleccione el Area --",String.Empty));
			ds.Dispose();
		}

		private void dropFromTitularBind(string sAreaId)
		{
			if (gsMailType == "E" ) 
			{
				dropFromName.DataSource = ExternalSenderTitular.GetRecords(int.Parse(sAreaId));
				dropFromName.DataValueField = "remitente_externo_titular_id";
				dropFromName.DataTextField = "nombre";
				dropFromName.DataBind();
				dropFromName.Items.Insert(0, new ListItem("-- Seleccione Titular --",String.Empty));
			}
			else
			{
				dropFromName.DataSource = Users.GetAreaTitular(sAreaId, Session["uid"].ToString(), "R", txtCutDate.Text );
				dropFromName.DataValueField = "id_empleado";
				dropFromName.DataTextField = "nombre";
				dropFromName.DataBind();
				dropFromName.Items.Insert(0, new ListItem("-- Seleccione Titular --",String.Empty));
			}
		
		}

		private void dropToTitularBind(string sAreaId) 
		{
			dropToName.DataSource		= Users.GetAreaTitular(sAreaId,Session["uid"].ToString(), "D", txtCutDate.Text);
			dropToName.DataValueField	= "id_empleado";
			dropToName.DataTextField	= "nombre";
			dropToName.DataBind();
			dropToName.Items.Insert(0, new ListItem("-- Seleccione Titular --",String.Empty));
		}

		private void GetFromTitularJob(int nTitularId) 
		{
			if (gsMailType == "E")
				txtRemitenteJob.Text = ExternalSenderTitular.GetPuesto(nTitularId); 
			else
				txtRemitenteJob.Text = Users.GetJob(nTitularId); 
		}

		private void GetToTitularJob(int nTitularId) 
		{
			txtDestinatarioJob.Text = Users.GetJob(nTitularId); 
		}

		private void setDataDocument()
		{
			DataSet ds = Documents.GetDocumentsRelations(gnDocumentId);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				txtSubject.Text		= dr["asunto"].ToString();
				txtAttached.Text	= dr["anexo"].ToString();
				txtReference.Text	= dr["referencia"].ToString();
				txtSummary.Text     = dr["resumen"].ToString();
				txtDate.Text		= dr["fecha_documento_fuente"].ToString().Substring(0,10);
				txtVolante.Text		= dr["volante"].ToString();


				rblRequire.Items.FindByValue(dr["requiere_tramite"].ToString()).Selected = true;

				gsMailType = dr["tipo_remitente"].ToString();
				dropFrom.Visible	 = true;
				dropFromName.Visible = true;

				txtFrom.Visible		= false;
				txtFromName.Visible = false;

				if (ExternalSender.getUseCatalog(Session["uid"].ToString()) == "No" && txtMailType.Text == "E")
				{
					dropFrom.Visible = false;
					dropFromName.Visible = false;
					txtFrom.Visible		= true;
					txtFromName.Visible = true;

					txtFrom.Text		= ExternalSender.getName(dr["fromAreaId"].ToString());
					txtFromName.Text	= ExternalSenderTitular.getName(dr["fromTitularId"].ToString());

				}

				dropDocumentType.Visible = true;
				txtTipoDocumento.Visible = false;
				if (document_type.getUseCatalog(Session["uid"].ToString()) == "No")
				{
					dropDocumentType.Visible	= false;
					txtTipoDocumento.Visible	= true;
					txtTipoDocumento.Text	= document_type.getTipoDocumento(dr["tipo_documento_id"].ToString());
				}

				txtRemitenteArea.Text = dr["fromClaveArea"].ToString() + " / " + dr["FromArea"].ToString();
				txtRemitenteName.Text = dr["FromName"].ToString();
				txtRemitenteJob.Text = dr["FromJob"].ToString();
				txtDestinatarioArea.Text = dr["toClaveArea"].ToString() + " / " +  dr["toArea"].ToString();
				txtDestinatarioName.Text = dr["ToName"].ToString();
				txtDestinatarioJob.Text = dr["toJob"].ToString();
				txtDocumentType.Text = dr["tipo_documento"].ToString();
				txtSign.Text = dr["SignName"].ToString();

				txtRemitenteId.Text = dr["fromTitularId"].ToString();
				txtRemAreaId.Text = dr["fromAreaId"].ToString();
				txtDestinatarioId.Text = dr["toTitularId"].ToString();
				txtDestAreaId.Text = dr["toAreaId"].ToString();
				txtSignId.Text = dr["id_firma"].ToString();
				txtTipoDocumentoId.Text = dr["tipo_documento_id"].ToString();

//				ddlTipoSolicitud.Items.FindByValue(dr["id_tipo_solicitud"].ToString()).Selected = true;
//				ddlTipoAtencion.Items.FindByValue(dr["id_tipo_atencion"].ToString()).Selected = true;

				lblTipoSolicitud.Text = dr["tipo_solicitud"].ToString();
				lblTipoAtencion.Text = dr["tipo_atencion"].ToString();
				txtFecha.Text = dr["fecha_atencion"].ToString().Length == 0 ? "" : Convert.ToDateTime(dr["fecha_atencion"].ToString()).ToShortDateString();
				txtNombreProyecto.Text = dr["nombre_proyecto"].ToString();

				if (dr["id_tipo_atencion"].ToString() == "1")
				{
					txtFecha.Visible = false;
				}
				this.btnCreate.Visible = true;
				this.btnContinue.Visible = true;

				if (dr["estatus"].ToString() == "Concluido")
				{
					this.btnCreate.Visible = false;
					this.btnContinue.Visible = false;
				}
			}

			ds.Dispose();
		}

		private void setDefaultDocument()
		{
			DataSet ds = firmaUsuario.getRegla( int.Parse(Session["uid"].ToString()) );

			DataRow dr = ds.Tables[0].Rows[0];

			if (gsMailType == "I")
			{

				if (dropFrom.Items.FindByValue( dr["id_remitente_area"].ToString()) != null) {
					dropFrom.Items.FindByValue( dr["id_remitente_area"].ToString() ).Selected = true;
					txtRemitenteArea.Text = dr["RemClaveArea"].ToString() + " / " + dropFrom.SelectedItem.Text;
					txtRemAreaId.Text = dr["id_remitente_area"].ToString();
				}

				dropFromTitularBind(dropFrom.SelectedItem.Value);
				
				if (dropFromName.Items.FindByValue( dr["id_remitente_titular"].ToString()) != null)	{
					dropFromName.Items.FindByValue( dr["id_remitente_titular"].ToString()).Selected = true;
					txtRemitenteName.Text = dropFromName.SelectedItem.Text;
					txtRemitenteId.Text = dr["id_remitente_titular"].ToString();
					txtRemitenteJob.Text  = Users.GetJob( int.Parse(dr["id_remitente_titular"].ToString()));
				}
			}
			
			if (dropTo.Items.FindByValue( dr["id_destinatario_area"].ToString()) != null) {
				dropTo.Items.FindByValue( dr["id_destinatario_area"].ToString()).Selected = true;
				txtDestinatarioArea.Text = dr["DestClaveArea"].ToString() + " / " + dropTo.SelectedItem.Text;
				txtDestAreaId.Text = dr["id_destinatario_area"].ToString();
			}
			
			dropToTitularBind (dropTo.SelectedItem.Value);

			if (dropToName.Items.FindByValue( dr["id_destinatario_titular"].ToString()) != null) {
				dropToName.Items.FindByValue( dr["id_destinatario_titular"].ToString()).Selected = true;
				txtDestinatarioName.Text = dropToName.SelectedItem.Text;
				txtDestinatarioId.Text = dr["id_destinatario_titular"].ToString();
				txtDestinatarioJob.Text = Users.GetJob( int.Parse(dr["id_remitente_titular"].ToString()));
			}

			if (dropFirma.Items.FindByValue( dr["id_firma"].ToString()) != null) {
				dropFirma.Items.FindByValue( dr["id_firma"].ToString()).Selected = true;
				txtSign.Text = dropFirma.SelectedItem.Text;
				txtSignId.Text = dr["id_firma"].ToString();
			}

			ds.Dispose();
	}

		private void dropFirmaBind() 
		{
			dropFirma.DataSource = firmaUsuario.getFirmaArea(int.Parse(Session["uid"].ToString()), txtCutDate.Text);
			dropFirma.DataValueField = "id_empleado";
			dropFirma.DataTextField = "nombre";
			dropFirma.DataBind();
			dropFirma.Items.Insert(0, new ListItem("-- Seleccione Usuario --",String.Empty));
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
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.txtTipoDocumento.TextChanged += new System.EventHandler(this.txtTipoDocumento_TextChanged);
			this.dropDocumentType.SelectedIndexChanged += new System.EventHandler(this.dropDocumentType_SelectedIndexChanged);
			this.ddlTipoAtencion.SelectedIndexChanged += new System.EventHandler(this.DDLTipoAtencionClick);
			this.dropFrom.SelectedIndexChanged += new System.EventHandler(this.dropFrom_SelectedIndexChanged);
			this.txtFrom.TextChanged += new System.EventHandler(this.txtFrom_TextChanged);
			this.btnSelCutDate.Click += new System.EventHandler(this.btnSelCutDate_Click);
			this.dropFromName.SelectedIndexChanged += new System.EventHandler(this.dropFromName_SelectedIndexChanged);
			this.txtFromName.TextChanged += new System.EventHandler(this.txtFromName_TextChanged);
			this.dropTo.SelectedIndexChanged += new System.EventHandler(this.dropTo_SelectedIndexChanged);
			this.dropToName.SelectedIndexChanged += new System.EventHandler(this.dropToName_SelectedIndexChanged);
			this.dropFirma.SelectedIndexChanged += new System.EventHandler(this.dropFirma_SelectedIndexChanged);
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			this.ID = "CreateMail";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			int nDocumentTypeId = 0;
			int nFromAreaId = 0;
			int nFromTitularId = 0;
			int nToAreaId		= 0;
			int nToTitularId	= 0;
			int nFirmaId		= 0;
			int nSolicitudId = 0;
			int nAtencionId = 0;
			string sFechaAtencion = txtFecha.Text;
			string sNombreProyecto = txtNombreProyecto.Text;
			
				DataSet ds = Documents.GetDocumentsRelations(gnDocumentId);
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					nDocumentTypeId =	dr.IsNull("tipo_documento_id") ? 0 : int.Parse(dr["tipo_documento_id"].ToString());
					nFromAreaId =		dr.IsNull("fromAreaId") ? 0 : int.Parse(dr["fromAreaId"].ToString());
					nFromTitularId =	dr.IsNull("fromTitularId") ? 0 : int.Parse(dr["fromTitularId"].ToString());
					nToAreaId =			dr.IsNull("toAreaId") ? 0 :  int.Parse(dr["toAreaId"].ToString());
					nToTitularId =		dr.IsNull("toTitularId") ? 0 : int.Parse(dr["toTitularId"].ToString());
					nFirmaId =			dr.IsNull("id_firma") ? 0 : int.Parse(dr["id_firma"].ToString());
				}

				if (ExternalSender.getUseCatalog(Session["uid"].ToString()) == "No" && txtMailType.Text == "E")	
				{
					nFromAreaId		= ExternalSender.getAreaID(txtFrom.Text, int.Parse(Session["uid"].ToString()) );
					nFromTitularId	= ExternalSenderTitular.getTitularID(nFromAreaId, txtFromName.Text);
				} 
				else 
				{
					nFromAreaId		= dropFrom.SelectedValue != "" ? int.Parse(dropFrom.SelectedItem.Value.ToString()) : nFromAreaId;
					nFromTitularId	= dropFromName.SelectedValue != "" ? int.Parse(dropFromName.SelectedItem.Value.ToString()) : nFromTitularId;
				}

				if (document_type.getUseCatalog(Session["uid"].ToString()) == "No")	
				{
					nDocumentTypeId	= document_type.getID(txtTipoDocumento.Text, int.Parse(Session["uid"].ToString()) );
				}
				else
				{
					nDocumentTypeId =   dropDocumentType.SelectedValue != "" ? int.Parse(dropDocumentType.SelectedItem.Value.ToString()) : nDocumentTypeId;
				}

				nToAreaId		= dropTo.SelectedValue != "" ? int.Parse(dropTo.SelectedItem.Value.ToString()) : nToAreaId;
				nToTitularId	= dropToName.SelectedValue != "" ? int.Parse(dropToName.SelectedItem.Value.ToString()) : nToTitularId;
				nFirmaId		= dropFirma.SelectedValue != "" ?  int.Parse(dropFirma.SelectedItem.Value.ToString()) : nFirmaId;
				
				nSolicitudId	= ddlTipoSolicitud.SelectedValue != "" ? int.Parse(ddlTipoSolicitud.SelectedItem.Value.ToString()) : nSolicitudId;
				nAtencionId		= ddlTipoAtencion.SelectedValue != "" ? int.Parse(ddlTipoAtencion.SelectedItem.Value.ToString()) : nAtencionId;
			
				int nVolante;
				int nDocumentID = gnDocumentId;

				if (gsAction == "C") 
				{
					nVolante = Documents.getNextVolante(int.Parse(Session["uid"].ToString()));

					nDocumentID = Documents.Document_Create(gsMailType, nFromAreaId, nToAreaId, nFromTitularId, nToTitularId, 
						nDocumentTypeId, txtDate.Text, txtReference.Text, txtSubject.Text, txtAttached.Text, 
						txtSummary.Text, "Pendiente", gnDocumentBisId, int.Parse(Session["uid"].ToString()), 
						rblRequire.SelectedItem.Value.ToString(), nVolante, nFirmaId, nSolicitudId, nAtencionId, sFechaAtencion, sNombreProyecto);
				}
				else
				{
					nVolante = int.Parse(txtVolante.Text.ToString());
				
					int nVal = Documents.Document_Update(gnDocumentId, gsMailType, nFromAreaId, nToAreaId, nFromTitularId, nToTitularId, 
						nDocumentTypeId, txtDate.Text, txtReference.Text, txtSubject.Text, txtAttached.Text, 
						txtSummary.Text, "Pendiente", gnDocumentBisId, Documents.getOwnerDocument(gnDocumentId), 
						rblRequire.SelectedItem.Value.ToString(), nFirmaId, nSolicitudId, nAtencionId, sFechaAtencion, sNombreProyecto);
				}

				if (rblRequire.SelectedItem.Value.ToString() == "No")
					Server.Transfer("volante_create.aspx?id=" + gnDocumentId);
				else
					Response.Redirect("/Gestion/Correspondencia/mail_editor.aspx?id=" + nDocumentID + "&action=" + gsAction + "&mailtype=" + gsMailType + "&filter=" + Request["filter"].ToString());
			
		}

		private void dropFrom_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtRemitenteArea.Text = dropFrom.SelectedItem.Text;
			txtRemAreaId.Text = dropFrom.SelectedValue;
			dropFromTitularBind(dropFrom.SelectedItem.Value);
		}

		private void dropTo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtDestinatarioArea.Text = dropTo.SelectedItem.Text;
			txtDestAreaId.Text = dropTo.SelectedValue;
			dropToTitularBind (dropTo.SelectedItem.Value);
		}


		private void dropFromName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (dropFromName.SelectedItem.Value != "")
			{
				txtRemitenteName.Text =  dropFromName.SelectedItem.Text;
				txtRemitenteId.Text = dropFromName.SelectedValue;
				GetFromTitularJob(int.Parse(dropFromName.SelectedItem.Value));
			}
		}

		private void dropToName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (dropToName.SelectedItem.Value != "")
			{
				txtDestinatarioName.Text = dropToName.SelectedItem.Text;
				txtDestinatarioId.Text = dropToName.SelectedValue;
				GetToTitularJob(int.Parse(dropToName.SelectedItem.Value));
			}
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			btnCreate_Click(sender, e);
		}


		protected void SubmitClosed(object sender, ImageClickEventArgs e)
		{
			if (gsAction == "C") 
			{
				if (!Page.IsValid)
				Response.Redirect("/gestion/portal/main.aspx");
			}
			else
			{
				int nDocumentID = gnDocumentId;
				Response.Redirect("/Gestion/Correspondencia/mail_editor.aspx?id=" + nDocumentID + "&action=MODIFY_DOCUMENT" + "&mailtype=" + gsMailType + "&filter=" + Request["filter"].ToString());
			}
				
		}

		private void txtFrom_TextChanged(object sender, System.EventArgs e)
		{
			txtRemitenteArea.Text = txtFrom.Text;
			txtRemAreaId.Text = txtFrom.Text;
		}

		private void txtFromName_TextChanged(object sender, System.EventArgs e)
		{
			txtRemitenteName.Text = txtFromName.Text;
			txtRemitenteId.Text = txtFromName.Text;
		}

		private void btnSelCutDate_Click(object sender, System.EventArgs e)
		{
			dropFromBind();
			dropToBind();
			dropFirmaBind();
		}

		private void txtTipoDocumento_TextChanged(object sender, System.EventArgs e)
		{
			txtDocumentType.Text = txtTipoDocumento.Text;
			txtTipoDocumentoId.Text = txtTipoDocumento.Text;
		}

		private void dropDocumentType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtDocumentType.Text = dropDocumentType.SelectedItem.Text;
			txtTipoDocumentoId.Text = dropDocumentType.SelectedValue;
		}

		private void dropFirma_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtSign.Text = dropFirma.SelectedItem.Text;
			txtSignId.Text = dropFirma.SelectedValue;
		}

		private void DDLTipoAtencionClick(object sender, System.EventArgs e)
		{
			txtFecha.Visible = false;
			if (ddlTipoAtencion.SelectedValue.Equals("1")) 
			{
				txtFecha.Visible = false;
			}
		}

		public int gnDocumentBisId
		{
			get { return Convert.ToInt32(txtDocumentoBisId.Text); }
			set { txtDocumentoBisId.Text = value.ToString();}
		}
		public int gnDocumentId
		{
			get { return Convert.ToInt32(txtDocumentoId.Text); }
			set { txtDocumentoId.Text = value.ToString();}
		}

		public string gsAction
		{
			get { return txtAction.Text; }
			set { txtAction.Text = value;}
		}
		public string gsMailType
		{
			get { return txtMailType.Text; }
			set { txtMailType.Text = value;}
		}


	}
}
