using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

// Banobras-Gestion
using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for frmAlternate.
	/// </summary>
	public class frmAlternate : System.Web.UI.Page
	{
		
		public string gsAction = "&action=MAIL_ALTERNATE";
		public string gsHeader = "<TR valign=center class='headerRow'> " +
								 "<TD><img src='/gestion/images/collapsed.gif' onclick='outline();' alt='Contraer todo'></TD> " +
								 "<TD>&nbsp;</TD> " +
								"<TD>Volante</TD> " +
								 "<TD>Referencia</TD> " + 
								 "<TD>Fecha Registro</TD> " + 
								 "<TD>Fecha Documento</TD> " + 
								 "<TD>Resumen</TD> " +  
								 "<TD>Asunto</TD> " + 
							     "<TD>Instrucción</TD> " +
							     "<TD>Atención</TD> " +
								 "<TD>Estado</TD> " +
								 "<TD>Fecha Desde</TD> " +
								 "<TD>Fecha Hasta</TD> " +
								"<TD>Dias</TD> " +
								 "</TR> ";

		public string gsHeader1 = "<TR class='headerRow' valign=center> " +
			"<TD><img src='/gestion/images/collapsed.gif' onclick='outline();' alt='Contraer todo'></TD> " +
            "<TD>&nbsp;</TD> " +
             "<TD>Volante</TD> " +
			"<TD>Referencia</TD> " + 
			"<TD>Fecha Registro</TD> " + 
			"<TD>Fecha Documento</TD> " + 
			"<TD>Resumen</TD> " +  
			"<TD>Asunto</TD> " + 
			"<TD>Instrucción</TD> " +
		    "<TD>Atención</TD> " +
			"<TD>Estado</TD> " +
			"</TR> ";
		
		public string gsBody = String.Empty;
		
		public const string cnExpandedDivLine =	"<TBODY value=<@LEVEL@>><TR class=applicationTableRowDivisor>" +
												"<TD nowrap colspan=12 valign=top><@SEPARATOR@>" +
												"<img align=absbottom src='/gestion/images/expanded.gif' onclick='outliner();'>" +
												"<@NAME@> (<@COUNTER@>)</TD><TD nowrap colspan=<@COL_SPAN@> align=right></TD></TR>";
		
		public const string cnCollapsedDivLine = "<TBODY value=<@LEVEL@>><TR class=applicationTableRowDivisor valign=center>" +
												 "<TD nowrap colspan=12><@SEPARATOR@>" +
												 "<img align=absbottom src='/gestion/images/collapsed.gif' onclick='outliner();'>" +
												 "<@NAME@> (<@COUNTER@>)</TD><TD nowrap colspan=<@COL_SPAN@> align=right></TD></TR>";

		public const string cnItemLine0 =	"<TR >" +
											"<TD><A href='' onclick='return callOptions(<@DOCUMENTOID@>,<@TURNADOID@>,<@STATUS@>,\"<@ALTERNATE@>\");'> <img align=absbottom src='/gestion/images/icons_sm/document_sm.gif'> </A></TD>" +
                                            "<TD><@SEPARATOR@></TD>" +
                                            "<TD><@VOLANTE@></TD>" + 
											"<TD><@REFERENCIA@></TD>" + 
											"<TD><@FECHA_REGISTRO@></TD>" + 
											"<TD><@FECHA_DOCUMENTO@></TD>" + 
											"<TD><@RESUMEN@></TD>" + 
											"<TD><@ASUNTO@></TD>" + 
											"<TD><@INSTRUCCION@></TD>" +
										    "<TD><@ATENCION@></TD>" +
											"<TD><@ESTADO@></TD>" +
											"<TD><@FECHA_DESDE@></TD>" +
											"<TD><@FECHA_HASTA@></TD>" +
											"<TD><@DIAS@></TD>" +
											"</TR>";
		protected System.Web.UI.WebControls.DropDownList ddlAlternateCopy;
		protected System.Web.UI.WebControls.DropDownList dropShow;
		protected System.Web.UI.WebControls.DropDownList ddlNivelArea;
		protected System.Web.UI.WebControls.LinkButton lnkExecute;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator2;

		public const string cnItemLine1 = "<TR valign=center style='display:inline;' class='<@CLASS@>'>" +
            "<TD><@SEPARATOR@></TD>" +
            "<TD><A href='' onclick='return callOptions(<@DOCUMENTOID@>,<@TURNADOID@>,<@STATUS@>,\"<@ALTERNATE@>\");'> <img align=absbottom src='/gestion/images/icons_sm/document_sm.gif'> </A></TD>" +
            "<TD><@SEPARATOR@></TD>" +
            "<TD><@VOLANTE@></TD>" + 
			"<TD><@REFERENCIA@></TD>" + 
			"<TD><@FECHA_REGISTRO@></TD>" + 
			"<TD><@FECHA_DOCUMENTO@></TD>" + 
			"<TD><@RESUMEN@></TD>" + 
			"<TD><@ASUNTO@></TD>" + 
			"<TD><@INSTRUCCION@></TD>" +
		    "<TD><@ATENCION@></TD>" +
			"<TD><@ESTADO@></TD>" +
            "<TD><@FECHA_DESDE@></TD>" +
            "<TD><@FECHA_HASTA@></TD>" +
            "<TD><@DIAS@></TD>" +
            "</TR>";

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
				int nUser = int.Parse(Session["uid"].ToString());
				string sArea = Users.GetAreaParameter( int.Parse(Session["uid"].ToString()) );
				int nNivel = Users.GetAreaLevel( nUser );
						
				if (ddlNivelArea.Items.FindByValue(nNivel.ToString()) != null)
					ddlNivelArea.Items.FindByValue(nNivel.ToString()).Selected = true;

				if (txtFromDate.Text  == String.Empty)
					txtFromDate.Text = "01/01/" + DateTime.Now.Year.ToString();

				if (txtToDate.Text  == String.Empty)
					txtToDate.Text = DateTime.Now.ToShortDateString();

				gsBody = dgAlternateMailCreate(ddlAlternateCopy.SelectedItem.Value.ToString(), dropShow.SelectedItem.Value.ToString(), nNivel );
			}
		}


		private string dgAlternateMailCreate(string sAlternate, string sStatus, int nNivel )
		{
			DataSet ds = Documents.Documents_Alternate(int.Parse(Session["uid"].ToString()), sAlternate, sStatus, nNivel, txtFromDate.Text, txtToDate.Text);

			string sItemSeparator = Separator(1);
			string sAreaId = "*";
			string sHTML = String.Empty;
			StringBuilder sItemSection = new StringBuilder();
			StringBuilder sTemp = new StringBuilder();
			int nColSpan = 12;
			int nItems = 0;
			string sVolante = String.Empty;
			string sTreeVolante = String.Empty;
			bool lClass = true;

			
			if ( ds.Tables[0].Rows.Count > 0 )
			{
				foreach (DataRow r in ds.Tables[0].Rows)
				{
					sTemp.Remove(0,sTemp.Length);
					if (sAreaId != r["destinatarioAreaId"].ToString() + "-" + r["turnadoAreaId"].ToString() + "-" + r["turnadoNombre"].ToString() )
					{
						if (sAreaId != "*")
						{
							sItemSection.Replace("<@COUNTER@>", nItems.ToString());
							sItemSection.Append("</TBODY>");
						}
					
						sAreaId = r["destinatarioAreaId"].ToString() + "-" + r["turnadoAreaId"].ToString()+ "-" + r["turnadoNombre"].ToString();
						sItemSection.Append(DivisionLine(r["destinatarioClaveArea"].ToString() + "-" + r["turnadoClaveArea"].ToString(), "De: " + r["destinatarioArea"].ToString() + " Para: " + r["turnadoArea"].ToString() + "-" + r["turnadoNombre"].ToString(), 1, nColSpan, false));
						nItems = 0;
					}
				
					
					if (sAlternate == "1")
						sTemp.Append(cnItemLine1);
					else
						sTemp.Append(cnItemLine0);

					sTreeVolante = Documents.getTreeVolante(r["documento_bis_id"].ToString());
					if (lClass)
						sTemp.Replace("<@CLASS@>", "evenRow");
					else
						sTemp.Replace("<@CLASS@>", "oddRow");

					sTemp.Replace("<@SEPARATOR@>", sItemSeparator);
					sTemp.Replace("<@VOLANTE@>", sTreeVolante + r["volante"].ToString());

					if (sAlternate == "0")
					{
						sTemp.Replace("<@TURNADOID@>", r["documento_turnar_id"].ToString());
						sTemp.Replace("<@STATUS@>", r["statusTurnadoId"].ToString());
						sTemp.Replace("<@ALTERNATE@>", "T");
					}
					else
					{
						sTemp.Replace("<@TURNADOID@>", r["ccpara_id"].ToString());
						sTemp.Replace("<@STATUS@>", r["statusCcparaId"].ToString());
						sTemp.Replace("<@ALTERNATE@>", "C");
					}
					sTemp.Replace("<@DOCUMENTOID@>", r["documento_id"].ToString());
					
					sTemp.Replace("<@FECHA_REGISTRO@>", r["fecha_elaboracion"].ToString());
					sTemp.Replace("<@FECHA_DOCUMENTO@>", r["fecha_documento_fuente"].ToString().Substring(0,10));	
					sTemp.Replace("<@REFERENCIA@>", r["referencia"].ToString());
					sTemp.Replace("<@RESUMEN@>", r["resumen"].ToString());
					sTemp.Replace("<@ASUNTO@>", r["asunto"].ToString());
					sTemp.Replace("<@INSTRUCCION@>", r["instruccion"].ToString());
					
					if (sAlternate == "0")
					{
						DateTime dToDate = Document_Alternate.CalculateToDate(int.Parse(r["documento_turnar_id"].ToString()));
						int nDias = 0;
						if ( dToDate.ToShortDateString() == "01/01/2049" )
							dToDate = DateTime.Now;
						
						nDias = Document_Alternate.CalculateHolidayDays(Convert.ToDateTime(r["fecha_desde"].ToString()), dToDate );

						sTemp.Replace("<@FECHA_DESDE@>", r["fecha_desde"].ToString().Substring(0,10));
						sTemp.Replace("<@FECHA_HASTA@>", dToDate.ToShortDateString());
						sTemp.Replace("<@DIAS@>", nDias.ToString());
					}

					sVolante = "*";
					if (sAlternate == "0")
					{
						if (r["statusTurnado"].ToString() == "Returnado")
							sVolante = Documents.getVolanteReturnado(r["documento_id"].ToString(), int.Parse(Session["uid"].ToString()));
					}
					else
					{
						if (r["statusCcpara"].ToString() == "Returnado")
							sVolante = Documents.getVolanteReturnado(r["documento_id"].ToString(), int.Parse(Session["uid"].ToString()));
					}
					if (sAlternate == "0")   // Si es Turnado
					{
						if (sVolante == "*")
							sTemp.Replace("<@ESTADO@>", r["statusTurnado"].ToString());
						else
							sTemp.Replace("<@ESTADO@>", "Vol. No. " + sVolante);
					}
					else  // Si es con Copia
					{
						if (sVolante == "*")
							sTemp.Replace("<@ESTADO@>", r["statusCcpara"].ToString());
						else
							sTemp.Replace("<@ESTADO@>", "Vol. No. " + sVolante);
					}

					
					if (sAlternate == "0")
					{
						if (r["statusTurnadoId"].ToString() == "0")
							sTemp.Replace("<@ESTADOID@>", "1");
						else
							sTemp.Replace("<@ESTADOID@>", "9");
					}else{
						if (r["statusCcparaId"].ToString() == "0")
							sTemp.Replace("<@ESTADOID@>", "1");
						else
							sTemp.Replace("<@ESTADOID@>", "9");
					}
					
					sTemp.Replace("<@ATENCION@>", r["tipoAtencion"].ToString());
					nItems++;
				
					sAreaId = r["destinatarioAreaId"].ToString() + "-" + r["turnadoAreaId"].ToString() + "-" + r["turnadoNombre"].ToString();
					sItemSection.Append(sTemp.ToString());
					lClass = lClass == true ? false : true;
				}
				sHTML = sItemSection.Replace("<@COUNTER@>",nItems.ToString()) + "</TBODY>";
			}
			ds.Dispose();
			return sHTML;
			
		}

		private string DivisionLine(string sObjectId , string sObjectName, int nLevel, int nColSpan, Boolean bExpanded)
		{
			string sTemp;
			sTemp = bExpanded ? cnExpandedDivLine : cnCollapsedDivLine;
			sTemp = sTemp.Replace("<@LEVEL@>", nLevel.ToString());
			sTemp = sTemp.Replace("<@SEPARATOR@>", Separator(nLevel));
			sTemp = sTemp.Replace("<@NAME@>", sObjectId + " / " + sObjectName);
			sTemp = sTemp.Replace("<@COL_SPAN@>", nColSpan.ToString());
			return sTemp;
		}

		private string Separator(int nLevel)
		{
			string sTemp = "";
			if (nLevel <= 1)
			{
				sTemp = " &nbsp; ";
				for (int i = 2; i<nLevel; i++)
				{
					sTemp += "&nbsp;";
				}
			}
			return sTemp;
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
			this.lnkExecute.Click += new System.EventHandler(this.lnkExecute_Click);
			this.ID = "frmAlternate";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void lnkExecute_Click(object sender, System.EventArgs e)
		{
			gsBody = dgAlternateMailCreate(ddlAlternateCopy.SelectedItem.Value, dropShow.SelectedItem.Value, int.Parse(ddlNivelArea.SelectedItem.Value));
		}

	}
}
