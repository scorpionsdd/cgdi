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
using System.Configuration;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for mail_editor.
	/// </summary>
	public class print_alternate : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.HyperLink btnSend;
		protected System.Web.UI.WebControls.Label lblSystem;
		protected System.Web.UI.WebControls.Label lblAutor;
		protected System.Web.UI.WebControls.Label lblVolante;
		protected System.Web.UI.WebControls.Label lblReference;
		protected System.Web.UI.WebControls.Label lblDocumentDate;
		protected System.Web.UI.WebControls.Label lblToArea;
		protected System.Web.UI.WebControls.Label lblToName;
		protected System.Web.UI.WebControls.Label lblFromArea;
		protected System.Web.UI.WebControls.Label lblFromName;
		protected System.Web.UI.WebControls.Label lblDocumentType;
		protected System.Web.UI.WebControls.Label lblSubject;
		protected System.Web.UI.WebControls.Label lblSummary;
		protected System.Web.UI.WebControls.Label lblAttached;
		protected System.Web.UI.WebControls.Label lblElaborationDate;

		protected System.Web.UI.WebControls.Label lblTipoSolicitud;
		protected System.Web.UI.WebControls.Label lblTipoAtencion;
		protected System.Web.UI.WebControls.Label lblNombreProyecto;
		
		protected string sTurnar = string.Empty;
		protected string sCCpara = string.Empty;
		protected string sFirma	= string.Empty;

		protected System.Web.UI.WebControls.Label lblArea;
		protected System.Web.UI.WebControls.Image Image1;

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
				if (firmaUsuario.getRuleStatusChange( int.Parse(Session["uid"].ToString())) == "Si" )
				{
					if (Request.QueryString["to"].ToString() == "*")
					{
						// Update Sof_Documento
						Documents.saveResponse(int.Parse(Request.QueryString["id"].ToString())," ", "Tramite", DateTime.Now.ToShortDateString());
						// Update Sof_Documento_Turnar
						Documents.PutStatus(int.Parse(Request.QueryString["id"].ToString()), "Tramite", DateTime.Now.ToShortDateString());
						// Update Sof_Estatus_Turnar
					}
				}

				BindVolante();
			}
		}

		private void BindVolante()
		{

			sFirma = "<BR>Respuesta: <@RESPONSE_DATE@><TABLE id='txtResponse' class='tan-border' cellSpacing='5' cellPadding='0' width='100%' border='0'><tbody>";
			string sHtmlEnd	= "</tbody></TABLE>";

			lblSystem.Text		= "Control de Gestión";
			DataSet dsDocument	= Documents.GetDocumentsRelations(int.Parse(Request.QueryString["id"].ToString()));
			DataRow r			= dsDocument.Tables[0].Rows[0];
			string sCreator		= Documents.AutorUser(int.Parse(Request.QueryString["id"].ToString()));

			string sFirmaName	= firmaUsuario.getFirma(int.Parse( r["id_firma"].ToString()));
			string sSofPuesto = r["toJob"].ToString().TrimEnd();

			//lblVolante.Text	=	r["toAreaId"].ToString()+ "-" + Documents.getTreeVolante( r["documento_bis_id"].ToString() ) + r["volante"].ToString();
			lblVolante.Text	=	r["toClaveArea"].ToString()+ "-" + Documents.getTreeVolante( r["documento_bis_id"].ToString() ) + r["volante"].ToString();

			string sEncarTo = r["TipoUsuarioTO"].ToString() == "E" ? "ENCARGADO DE LA " : String.Empty;
			string sEncarFrom  = r["TipoUsuarioFROM"].ToString() == "E" ? "ENCARGADO DE LA " : String.Empty;

			lblElaborationDate.Text	=	r["fecha_elaboracion"].ToString();
			lblAutor.Text			=	sCreator;
			lblReference.Text		=	r["referencia"].ToString();
			lblDocumentDate.Text	=	r["fecha_documento_fuente"].ToString().Substring(0,10);
			lblElaborationDate.Text = 	r["fecha_elaboracion"].ToString();

			lblFromArea.Text		=   sEncarFrom + r["fromArea"].ToString();
			lblFromName.Text		=	r["fromName"].ToString();

			lblToArea.Text			=   sEncarTo + r["toArea"].ToString();
			lblToName.Text			=	r["toName"].ToString();
			
			if (r["toName"].ToString().IndexOf("Kessel",0) !=  -1)
					lblToName.Text = "";
			else
				lblToName.Text			=	r["toName"].ToString();

			lblDocumentType.Text	=	r["tipo_documento"].ToString();
			lblSubject.Text			=	r["asunto"].ToString();
			lblAttached.Text		=	r["anexo"].ToString();
			lblSummary.Text			=	r["resumen"].ToString();
			lblArea.Text			=	r["toArea"].ToString();

			lblTipoSolicitud.Text	=   r["tipo_solicitud"].ToString();
			lblTipoAtencion.Text	=   r["tipo_atencion"].ToString();
			lblNombreProyecto.Text =	r["nombre_proyecto"].ToString();
		
			DataSet dsTurnar = Adressee.GetTurnados(int.Parse(Request.QueryString["id"].ToString()), Request.QueryString["to"].ToString());

			string sEncargoTurnar = String.Empty;
			if (dsTurnar.Tables[0].Rows.Count > 0)
			{
				sTurnar	=	"<BR>Turnar:<TABLE id='alternate' class='tan-border' cellSpacing='5' cellPadding='0' width='100%' border='0'><tbody>" +
							"<TR class='HeaderTR'><TD width='30%'>Nombre</TD><TD>Area</TD><TD>Instrucción</TD><TD>Trámite</TD></TR>";

				foreach (DataRow dr in dsTurnar.Tables[0].Rows)
				{
					sEncargoTurnar = dr["tipo_usuario"].ToString() == "E" ? "ENCARGADO DE LA " : String.Empty;

					sTurnar += "<tr><td>" + dr["nombre"].ToString() + "</td>";
					sTurnar += "<td>" + sEncargoTurnar + dr["area"].ToString() + "</td>" ;
					sTurnar += "<td class='report-text1'>" + dr["instruccion"].ToString() + "</td>" ;
					sTurnar += "<td>" + dr["tipo_tramite"].ToString() + "</td></tr>";
				}
				sTurnar += sHtmlEnd;
			}

			dsTurnar.Dispose();

			DataSet dsCCPara = Documents.GetWithCopyFor(int.Parse(Request.QueryString["id"].ToString()));
			
			if (dsCCPara.Tables[0].Rows.Count > 0 )
			{
				sCCpara =	"<BR>Ccp:<TABLE id='ccpara' class='tan-border' cellSpacing='5' cellPadding='0' width='100%' border='0'><tbody>" +
							"<TR class='HeaderTR'><TD noWrap>Nombre</TD><TD>&nbsp;</TD><TD>Area</TD></TR>";

				foreach (DataRow dr in dsCCPara.Tables[0].Rows)
				{
					sCCpara += "<TR><TD noWrap>" + dr["nombre"].ToString() + "</TD>";
					sCCpara += "<TD>&nbsp;</TD>";
					sCCpara += "<TD>" + dr["area"].ToString() + "</TD></TR>";
				}
				sCCpara += sHtmlEnd;
			}
			dsCCPara.Dispose();

			if (Request.QueryString["to"].ToString() == "*")
			{
				DataSet dsResponse = Documents.getResponseDS(int.Parse(Request.QueryString["id"].ToString()));
				
				//dsResponse.Tables[0].Rows[0]["observacion"].ToString() == "" 
				if ( dsResponse.Tables[0].Rows.Count < 0 )
				{
					sFirma = sFirma.Replace("<@RESPONSE_DATE@>","  /   /   ");
					for (int i=1; i<7; i++)
					{
						sFirma += "<TR><TD>&nbsp;</TD></TR>";
					}
				}
				else 
				{

					foreach (DataRow drResponse in dsResponse.Tables[0].Rows)
					{
						sFirma = sFirma.Replace("<@RESPONSE_DATE@>",drResponse["fecha_desde"].ToString().Substring(0,10));
						sFirma += "<TR><TD>" + drResponse["observacion"].ToString() + "</TD></TR>";
					}
					sFirma = sFirma.Replace("<@RESPONSE_DATE@>", "  /   /   ");
				}
			}
			else	
			{
				DataSet dsResponse = Document_Alternate.getResponse(int.Parse(Request["turnarid"].ToString()));

				if ( dsResponse.Tables[0].Rows.Count == 0 )
				{
					sFirma = sFirma.Replace("<@RESPONSE_DATE@>","  /   /   ");
					for (int i=1; i<7; i++)
					{
						sFirma += "<TR><TD>&nbsp;</TD></TR>";
					}
				}
				else
				{
					foreach (DataRow drResponse in dsResponse.Tables[0].Rows)
					{
						sFirma = sFirma.Replace("<@RESPONSE_DATE@>",drResponse["fecha_desde"].ToString().Substring(0,10));
						sFirma += "<TR><TD>" + drResponse["observacion"].ToString() + "</TD></TR>";
					}
					sFirma = sFirma.Replace("<@RESPONSE_DATE@>", "  /   /   ");
				}
			
			
			}


			string sRaya = "_________________________________";
			sFirma += sHtmlEnd;
			if (r["toName"].ToString().IndexOf("Kessel", 0) != -1)
			{
				sFirma += "<BR><B><Br><BR><BR><BR><TABLE id='tblFirma' border='0' width='100%'><TR><TD align='center'>" + sRaya + "</TD></TR>";
				sFirma += "<TR><TD class='applicationTitle' align='center'>" + "Secretaría Particular" + "</TD></TR>";
				sFirma += "<TR><TD class='applicationTitle' align='center'>" + "Dirección General" + "</TD></TR></TABLE>";


			}
			else
			{
				sFirma += "<BR><B><Br><BR><BR><BR><TABLE id='tblFirma' border='0' width='100%'><TR><TD align='center'>" + sRaya + "</TD></TR>";

				sFirma += "<TR><TD class='applicationTitle' align='center'>" + sFirmaName  + " <br/> " + sSofPuesto + "</TD></TR></TABLE>";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}
}
