using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
//using System.Data.OracleClient;
using System.Data.OracleClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Web.Mail;

// Banobras Components
using Gestion.BusinessLogicLayer;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace Gestion.Correspondencia
{
    /// <summary>
    /// Summary description for Mail_Explorer.
    /// </summary>
    public class Mail_Explorer : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList dropShow;
        protected System.Web.UI.WebControls.TextBox txtElaborationDateFrom;

        private const int cnsDocumentoId = 0;
        private const int cnsAsunto = 1;
        private const int cnsResumen = 2;
        private const int cnsUsuarioId = 3;
        private const int cnsVolante = 4;
        private const int cnsTipoRemitente = 5;
        private const int cnsReferencia = 6;
        private const int cnsFechaDocumentoFuente = 7;
        private const int cnsFechaElaboracion = 8;
        private const int cnsDocumentoBisId = 9;
        private const int cnsRemitenteArea = 10;
        private const int cnsRemitenteNombre = 11;
        private const int cnsDestinatarioAreaId = 12;
        private const int cnsDestinatarioArea = 13;
        private const int cnsDestinatarioNombre = 14;
        private const int cnsStatusVolante = 15;

        private const int cnsTipoTurno = 16;
        private const int cnsStatusReceive = 18;
        private const int cnsInstruccion = 19;
        private const int cnsTurnadoNombre = 20;
        private const int cnsTurnadoArea = 21;

        public string gsHeader = "<TR valign=center class='headerRow'> " +
            "<TD><img src='/gestion/images/collapsed.gif' onclick='outline();' alt='Contraer todo'></TD> " +
            "<TD>&nbsp;</TD>" +
            "<TD>Volante</TD>" +
            "<TD>Referencia</TD>" +
            "<TD>Fecha Registro</TD>" +
            "<TD>Fecha Documento</TD>" +
            "<TD>Remitente</TD>" +
            "<TD>Asunto</TD>" +
            "<TD>Resumen</TD>" +
            "<TD>Elaboró</TD>" +
            "<TD>Estatus</TD>" +
            "</TR>";

        public string gsBody = String.Empty;

        public const string cnExpandedDivLine = "<TBODY value=<@LEVEL@>><TR class=applicationTableRowDivisor>" +
            "<TD nowrap colspan=10 valign=top><@SEPARATOR@>" +
            "<img align=absbottom src='/gestion/images/expanded.gif' onclick='outliner();'>" +
            "<@NAME@> (<@COUNTER@>)</TD><TD nowrap colspan=<@COL_SPAN@> align=right></TD></TR>";

        public const string cnCollapsedDivLine = "<TBODY value=<@LEVEL@>><TR class=applicationTableRowDivisor valign=center>" +
            "<TD nowrap colspan=10><@SEPARATOR@>" +
            "<img align=absbottom src='/gestion/images/collapsed.gif' onclick='outliner();'>" +
            "<@NAME@> (<@COUNTER@>)</TD><TD nowrap colspan=<@COL_SPAN@> align=right></TD></TR>";

        public const string cnItemLine0 = "<TR valign=center style='display:inline;' class='<@CLASS@>' >" +
            "<TD><@SEPARATOR@></TD>" +
            "<TD><A href=\"\" onclick=\"return(CallEditor('<@ID@>','<@ACTION@>','<@MAILTYPE@>','<@QUERYTYPE@>','<@FILTER@>')); \"> <img align=absbottom src='/gestion/images/icons_sm/document_sm.gif'> </A></TD>" +
            "<TD><@VOLANTE@></TD>" +
            "<TD nowrap><@REFERENCIA@></TD>" +
            "<TD><@FECHA_REGISTRO@></TD>" +
            "<TD><@FECHA_DOCUMENTO@></TD>" +
            "<TD><@REMITENTE@></TD>" +
            "<TD><@ASUNTO@></TD>" +
            "<TD><@RESUMEN@></TD>" +
            "<TD><@INSTRUCCION@></TD>" +
            "<TD><@DIAS@></TD>" +
            "<TD><@ESTATUS@></TD>" +
            "<TD><@ELABORO@></TD>" +
            "</TR>";

        public const string cnItemLine1 = "<TR valign=center style='display:inline;' class='<@CLASS@>' > " +
            "<TD><@SEPARATOR@></TD>" +
            "<TD><A href=\"\" onclick=\"return(CallEditor('<@ID@>','<@ACTION@>','<@MAILTYPE@>','<@QUERYTYPE@>','<@FILTER@>')); \"> <img align=absbottom src='/gestion/images/icons_sm/document_sm.gif'> </A></TD>" +
            "<TD><@VOLANTE@></TD>" +
            "<TD><@REFERENCIA@></TD>" +
            "<TD><@FECHA_REGISTRO@></TD>" +
            "<TD><@FECHA_DOCUMENTO@></TD>" +
            "<TD><@REMITENTE@></TD>" +
            "<TD><@ASUNTO@></TD>" +
            "<TD><@RESUMEN@></TD>" +
            "<TD><@ELABORO@></TD>" +
            "<TD><@ESTATUS@></TD>" +
            "</TR>";

        public const string cnItemLine2 = "<TR class=applicationTableRowDivisor valign=center style='display:inline;'> " +
            "<TD><@SEPARATOR@></TD>" +
            "<TD><@TIPOTURNO@></TD>" +
            "<TD colspan='5'><@NOMBRE@>-<@AREA@></TD>" +
            "<TD><@INSTRUCCION@></TD>" +
            "<TD><@DIAS@></TD>" +
            "<TD><@ESTATUS@></TD>" +
            "</TR>";

        public const string cnItemLine3 = "<TR class=applicationTableRowDivisor valign=center style='display:inline;'> " +
            "<TD colspan='9'></TD>" +
            "<TD><@ESTATUS@></TD>" +
            "</TR>";

        public const string cnItemLine4 = "<TD><A href=\"\" onclick=\"return(CallEditor('<@ID@>','<@ACTION@>','<@MAILTYPE@>','<@QUERYTYPE@>','<@FILTER@>')); \"> <img align=absbottom src='/gestion/images/icons_sm/document_sm.gif'> </A></TD>";

        private string currAddresseeId = "";
        private int nTotal = 0;
        private int j = 0;

        protected System.Web.UI.WebControls.TextBox txtElaborationDateTo;
        protected System.Web.UI.WebControls.DataGrid grdItems;
        protected System.Web.UI.WebControls.LinkButton lnkExecute;
        protected System.Web.UI.WebControls.RadioButtonList rblOrder;
        protected System.Web.UI.WebControls.DropDownList drpDocumentType;
        protected System.Web.UI.WebControls.DropDownList drpTurnada;
        protected System.Web.UI.WebControls.TextBox txtVolante;
        protected System.Web.UI.WebControls.TextBox txtTextSearch;
        protected System.Web.UI.WebControls.Label errLevel;
        protected System.Web.UI.WebControls.LinkButton lnkExcel;
        protected System.Web.UI.WebControls.CheckBox chkTurnados;
        protected System.Web.UI.WebControls.CheckBox chkCC;
        protected System.Web.UI.WebControls.TextBox txtDate;
        protected System.Web.UI.WebControls.Button btnChangeArea;

        protected System.Web.UI.WebControls.DropDownList dropArea;

        protected System.Web.UI.WebControls.Image ImgCargando;



        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Session.IsNewSession)
            {
                Response.Redirect("/gestion/default.aspx", false);
                return;
            }
            if (!Page.IsPostBack)
            {
                // Put user code to initialize the page here

                string sStatusID = Request["status"];
                txtElaborationDateFrom.Text = Request["edfrom"].ToString();
                txtElaborationDateTo.Text = Request["edto"].ToString();
                txtDate.Text = DateTime.Now.ToShortDateString();

                if (txtElaborationDateFrom.Text == String.Empty)
                    txtElaborationDateFrom.Text = "01/01/" + DateTime.Now.Year.ToString();

                if (txtElaborationDateTo.Text == String.Empty)
                    txtElaborationDateTo.Text = DateTime.Now.ToShortDateString();

                DropAreaBind(Request["areaid"]);

                if (dropShow.Items.FindByValue(sStatusID) != null)
                    dropShow.SelectedItem.Selected = false;

                dropShow.Items.FindByValue(sStatusID).Selected = true;
                DocumentsBindPending(sStatusID, "1");

                DropDocumentType();


            }
        }

        private void SetDefaultValues()
        {
            //RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
        }


        private void DocumentsBindPending(string sStatusSend, string sStatusReceive)
        {
            string sTemp = String.Empty;
            switch (sStatusSend)
            {
                case "1":
                    sTemp = "Pendiente";
                    break;
                case "2":
                    sTemp = "Tramite";
                    break;
                case "3":
                    sTemp = "Concluido";
                    break;
                case "4":
                    sTemp = "Sin Tramite";
                    break;
                case "5":
                    sTemp = "Todos";
                    break;
                default:
                    sTemp = "Invalid selection";
                    break;
            }

            gsBody = dgMailExplorer(sTemp, sStatusReceive);

        }

        public ICollection DocumentsBindPendingJAS()
        {
            string sStatusSend = dropShow.SelectedItem.Value;
            string sStatusInbox = drpTurnada.SelectedItem.Value;
            if (rblOrder.SelectedItem.Text == "Volante")
            {
                if (dropArea.SelectedItem.Selected)
                {
                    dropArea.SelectedItem.Selected = false;
                    dropArea.Items.FindByValue("").Selected = true;
                }
            }
            string sTemp = String.Empty;
            switch (sStatusSend)
            {
                case "1":
                    sTemp = "Pendiente";
                    break;
                case "2":
                    sTemp = "Tramite";
                    break;
                case "3":
                    sTemp = "Concluido";
                    break;
                case "4":
                    sTemp = "Sin Tramite";
                    break;
                case "5":
                    sTemp = "Todos";
                    break;
                default:
                    sTemp = "Invalid selection";
                    break;
            }
            string sAreaId = "*";
            if (dropArea.SelectedIndex > 0)
            {
                sAreaId = dropArea.SelectedItem.Value;
            }
            int nDocumentType = drpDocumentType.SelectedIndex > 0 ? int.Parse(drpDocumentType.SelectedItem.Value) : 0;
            bool bTurnado = chkTurnados.Checked ? true : false;
            bool bCCpara = chkCC.Checked ? true : false;

            ICollection r = Documents.DocumentsPendingsSendJAS(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text, txtElaborationDateTo.Text,
            sTemp, sStatusInbox, sAreaId, rblOrder.SelectedItem.Text, bTurnado, bCCpara, nDocumentType, txtVolante.Text, txtTextSearch.Text, "QRY");
            return r;
        }

        private void DropAreaBind(string sAreaId)
        {
            dropArea.DataSource = Users.GetAreaByDate(txtDate.Text);
            dropArea.DataTextField = "area";
            dropArea.DataValueField = "id_area";
            dropArea.DataBind();
            dropArea.Items.Insert(0, new ListItem("-- Todas Las Areas --", String.Empty));
            if (dropArea.Items.FindByValue(sAreaId) != null)
                dropArea.Items.FindByValue(sAreaId).Selected = true;

        }

        private void DropDocumentType()
        {
            drpDocumentType.DataSource = document_type.GetRecords(int.Parse(Session["uid"].ToString()));
            drpDocumentType.DataTextField = "tipo_documento";
            drpDocumentType.DataValueField = "tipo_documento_id";
            drpDocumentType.DataBind();
            drpDocumentType.Items.Insert(0, new ListItem("-- Todos --", "0"));
            dropArea.SelectedItem.Selected = true;
        }



        #region Web Form Designer generated code
        protected override void OnInit(EventArgs e)
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
            this.lnkExcel.Click += new System.EventHandler(this.lnkExcel_Click);
            this.btnChangeArea.Click += new System.EventHandler(this.btnChangeArea_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        private void lnkExecute_Click(object sender, System.EventArgs e)
        {
            grdItems.DataSource = null;
            grdItems.DataBind();

            this.ImgCargando.Visible = true;

            string sStatusSend = dropShow.SelectedItem.Value;
            string sStatusInbox = drpTurnada.SelectedItem.Value;
            if (rblOrder.SelectedItem.Text == "Volante")
            {
                if (dropArea.SelectedItem.Selected)
                {
                    dropArea.SelectedItem.Selected = false;
                    dropArea.Items.FindByValue("").Selected = true;
                }
            }

            DocumentsBindPending(sStatusSend, sStatusInbox);




        }

        private string dgMailExplorer(string sStatus, string sStatusReceive)
        {
            string sAreaId = "*";
            if (dropArea.SelectedIndex > 0)
            {
                sAreaId = dropArea.SelectedItem.Value;
            }
            DataSet ds;
            string sItemSeparator = Separator(3);
            string sItemSeparator1 = Separator(5);
            int nColSpan = 2;
            StringBuilder sItemSectionVolante = new StringBuilder();
            StringBuilder sItemSection = new StringBuilder();
            StringBuilder sTemp = new StringBuilder();
            StringBuilder sHTML = new StringBuilder();
            string sAreaTmp = String.Empty;
            string sTreeVolante = String.Empty;
            int nItems = 0;
            int nDocumentType = drpDocumentType.SelectedIndex > 0 ? int.Parse(drpDocumentType.SelectedItem.Value) : 0;
            bool lClass = true;
            bool bTurnado = chkTurnados.Checked ? true : false;
            bool bCCpara = chkCC.Checked ? true : false;


            if (rblOrder.SelectedItem.Text == "Area")
            {
                try
                {

                    ds = Documents.DocumentsPendingsSend(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text,
                        txtElaborationDateTo.Text, sStatus, sStatusReceive, sAreaId, txtTextSearch.Text, bTurnado, bCCpara);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)

                            {
                                sAreaId = "*";

                                foreach (DataRow r in ds.Tables[0].Rows)
                                {

                                    if (sAreaId != r["TurnadoClaveArea"].ToString())
                                    {
                                        if (sAreaId != "*")
                                            sItemSection.Replace("<@COUNTER@>", nItems.ToString()).Append("</TBODY>");

                                        sAreaId = r["TurnadoClaveArea"].ToString();
                                        sItemSection.Append(DivisionLine(sAreaId, r["turnadoArea"].ToString(), 1, nColSpan, false));
                                        nItems = 0;
                                    }

                                    TimeSpan DaysDiff = Documents.getDaysPass(Convert.ToInt32(r["documento_id"]), r["StatusVolante"].ToString());


                                    // sTemp = sTemp.Append(cnItemLine1);
                                    sTreeVolante = Documents.getTreeVolante(r["documento_bis_id"].ToString());
                                    sTemp.Append("<tr>");


                                    if (lClass)
                                    {
                                        sTemp.Append("<td>");
                                        sTemp.Replace("<@CLASS@>", "evenRow");
                                        sTemp.Append("</td>");
                                    }

                                    else
                                    {
                                        sTemp.Append("<td>");
                                        sTemp.Replace("<@CLASS@>", "oddRow");
                                        sTemp.Append("</td>");
                                    }

                                    sTemp.Append(cnItemLine4);
                                    //sTemp.Append("</td><td>");
                                    sTemp.Replace("<@ID@>", r["documento_id"].ToString());
                                    //sTemp.Append("</td><td>");
                                    sTemp.Append("<td>");
                                    sTemp.Replace("<@VOLANTE@>", sTreeVolante + " " + r["volante"].ToString());
                                    sTemp = sTemp.Append(r["volante"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@REFERENCIA@>", r["referencia"].ToString());
                                    sTemp = sTemp.Append(r["referencia"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@FECHA_REGISTRO@>", r["fecha_elaboracion"].ToString());
                                    sTemp = sTemp.Append(r["fecha_elaboracion"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@FECHA_DOCUMENTO@>", r["fecha_documento_fuente"].ToString().Substring(0, 10));
                                    sTemp = sTemp.Append(r["fecha_documento_fuente"].ToString().Substring(0, 10));
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@REMITENTE@>", r["remitenteArea"].ToString() + " - " + r["remitenteNombre"].ToString());
                                    sTemp = sTemp.Append(r["remitenteArea"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@ASUNTO@>", r["asunto"].ToString());
                                    sTemp = sTemp.Append(r["asunto"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@RESUMEN@>", r["resumen"].ToString());
                                    sTemp = sTemp.Append(r["resumen"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@ELABORO@>", r["id_empleado"].ToString());
                                    sTemp = sTemp.Append(r["id_empleado"].ToString());
                                    sTemp.Append("</td><td>");
                                    sTemp.Replace("<@ESTATUS@>", r["statusReceive"].ToString());
                                    sTemp = sTemp.Append(r["statusReceive"].ToString());
                                    sTemp.Append("</td><td>");
                                    sAreaTmp = dropArea.SelectedItem.Value != "" ? dropArea.SelectedItem.Value : "*";
                                    sTemp.Replace("<@FILTER@>", sAreaTmp + "," + dropShow.SelectedItem.Value + "," + txtElaborationDateFrom.Text + "," + txtElaborationDateTo.Text);
                                    sTemp.Append("</td>");

                                    if (r["documento_bis_id"].ToString() == "0")
                                    {
                                        sTemp.Append("<td>");
                                        sTemp.Replace("<@QUERYTYPE@>", "M");
                                        sTemp.Append("</td>");
                                    }
                                    else
                                    {
                                        sTemp.Append("<td>");
                                        sTemp.Replace("<@QUERYTYPE@>", "Q");
                                        sTemp.Append("</td>");
                                    }

                                    sTemp.Append("</tr>");

                                    nItems++;
                                    sAreaId = r["turnadoClaveArea"].ToString();


                                    sItemSection.Append(sTemp.ToString() + "\n");

                                    sTemp.Remove(0, sTemp.Length);

                                    lClass = lClass == true ? false : true;


                                }

                                sHTML = sHTML.Append(sItemSection.Replace("<@COUNTER@>", nItems.ToString()) + "</TBODY>");

                            }


                            ds.Dispose();
                        }



                    }
                    else
                    {

                        return sHTML.ToString();



                    }

                }
                catch (OracleException oe)
                {
                    return oe.ToString();

                }
                catch (Exception e)
                {
                    return e.ToString();
                    //Console.WriteLine(e.Message);
                }

            }
            else
            {
                //OracleDataReader r = Documents.DocumentsPendingsSend(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text, txtElaborationDateTo.Text,
                //sStatus, sStatusReceive, sAreaId, rblOrder.SelectedItem.Text, bTurnado, bCCpara, nDocumentType, txtVolante.Text, txtTextSearch.Text, "QRY");


                ICollection r = Documents.DocumentsPendingsSendJAS(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text, txtElaborationDateTo.Text,
                      sStatus, sStatusReceive, sAreaId, rblOrder.SelectedItem.Text, bTurnado, bCCpara, nDocumentType, txtVolante.Text, txtTextSearch.Text, "QRY");


                //Documents.WriteToLog("***Inicia Asigna DataSet: " + DateTime.Now.ToString());
                //grdItems.AllowCustomPaging = true;
                // grdItems.AllowPaging = true;

                grdItems.AutoGenerateColumns = false;
                // grdItems.PageSize = 50;
                grdItems.DataSource = r;
                //Documents.WriteToLog("***Termina Asigna DataSet: " + DateTime.Now.ToString());
                //Documents.WriteToLog("***Inicia DataBind() DataSet: " + DateTime.Now.ToString());

                grdItems.DataBind();

                //Documents.WriteToLog("***Termina DataBind() DataSet: " + DateTime.Now.ToString());

                /*
                sAreaId = "*";
                string sVolanteNumber = "*";
                StringBuilder sTemp1 = new StringBuilder();

                while (r.Read())
                {
                    if (sAreaId != r[cnsDestinatarioAreaId].ToString() )
                    {	
                        if (sAreaId != "*")
                            sItemSectionVolante.Replace("<@COUNTER@>", nItems.ToString()).Append("</TBODY>");

                        sItemSectionVolante.Append(DivisionLine(r[cnsDestinatarioAreaId].ToString(), r[cnsDestinatarioArea].ToString(), 1, nColSpan, false));
                        nItems = 0;
                    }

                    TimeSpan DaysDiff = Documents.getDaysPass(Convert.ToInt32(r[cnsDocumentoId]), r[cnsStatusVolante].ToString());

                    if (sVolanteNumber != r[cnsVolante].ToString())
                    {
                        sTreeVolante = Documents.getTreeVolante(r[cnsDocumentoBisId].ToString());
                        sTemp.Append(cnItemLine1);
                        sTemp.Replace("<@SEPARATOR@>", sItemSeparator);
                        sTemp.Replace("<@REFERENCIA@>", r[cnsReferencia].ToString());
                        sTemp.Replace("<@VOLANTE@>", sTreeVolante + " " + r[cnsVolante].ToString());
                        sTemp.Replace("<@FECHA_REGISTRO@>", r[cnsFechaElaboracion].ToString());
                        sTemp.Replace("<@FECHA_DOCUMENTO@>",r[cnsFechaDocumentoFuente].ToString().Substring(0,10));	
                        sTemp.Replace("<@REFERENCIA@>", r[cnsReferencia].ToString());
                        sTemp.Replace("<@REMITENTE@>", r[cnsRemitenteArea].ToString() + " - " + r["RemitenteNombre"].ToString());
                        sTemp.Replace("<@ASUNTO@>", r[cnsAsunto].ToString());
                        sTemp.Replace("<@RESUMEN@>", r[cnsResumen].ToString());
                        sTemp.Replace("<@ESTATUS@>", r[cnsStatusVolante].ToString());
                        sTemp.Replace("<@URL@>", "/gestion/correspondencia/mail_editor.aspx");
                        sTemp.Replace("<@ID@>", r[cnsDocumentoId].ToString());
                        sTemp.Replace("<@ACTION@>", r[cnsTipoRemitente].ToString());
                        sTemp.Replace("<@MAILTYPE@>", r[cnsTipoRemitente].ToString());
                        sTemp.Replace("<@ELABORO@>", r[cnsUsuarioId].ToString());

                        sAreaTmp = dropArea.SelectedItem.Value != "" ? dropArea.SelectedItem.Value : "*";
                        sTemp.Replace("<@FILTER@>", sAreaTmp + "," + dropShow.SelectedItem.Value + "," + txtElaborationDateFrom.Text + "," + txtElaborationDateTo.Text);

                        if (r[cnsDocumentoBisId].ToString() == "0")
                            sTemp.Replace("<@QUERYTYPE@>", "M");
                        else
                            sTemp.Replace("<@QUERYTYPE@>", "Q");
                    }
                    if ( cbIncludeTurnado.Checked || cbIncludeCC.Checked)
                    {
                        sTemp1.Append(cnItemLine2);
                        sTemp1.Replace("<@SEPARATOR@>", sItemSeparator);
                        sTemp1.Replace("<@TIPOTURNO@>", r[cnsTipoTurno].ToString());
                        sTemp1.Replace("<@NOMBRE@>", r[cnsTurnadoNombre].ToString());
                        sTemp1.Replace("<@AREA@>", r[cnsTurnadoArea].ToString());
                        sTemp1.Replace("<@INSTRUCCION@>", r[cnsInstruccion].ToString());
                        sTemp1.Replace("<@DIAS@>", DaysDiff.Days.ToString() + " Días");
                        sTemp1.Replace("<@ESTATUS@>", r[cnsStatusReceive].ToString() + "\n");
                    }

                    nItems++;
                    sAreaId	= r[cnsDestinatarioAreaId].ToString();
                    sVolanteNumber = r[cnsVolante].ToString();
                    sItemSectionVolante.Append(sTemp.ToString() + "\n" + sTemp1.ToString());

                    sTemp = sTemp.Remove(0, sTemp.Length);
                    if ( cbIncludeTurnado.Checked || cbIncludeCC.Checked)
                    {
                        sTemp1 = sTemp1.Remove(0, sTemp1.Length);
                    }

                }
                sHTML = sHTML.Append(sItemSectionVolante.Replace("<@COUNTER@>",nItems.ToString()) + "</TBODY>");
                */
                //JAS   r.Dispose();
                //JAS   Documents.WriteToLog("***Termina Dispose() OracleDataReader: " + DateTime.Now.ToString());

            }


            return sHTML.ToString();

        }



        private string DivisionLine(string sObjectId, string sObjectName, int nLevel, int nColSpan, Boolean bExpanded)
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
                for (int i = 2; i < nLevel; i++)
                {
                    sTemp += "&nbsp;";
                }
            }
            return sTemp;
        }

        protected void dg_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            DataGridItem row = e.Item;
            if (row.ItemType == ListItemType.Item || row.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label Volante = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblVolante");
                System.Web.UI.WebControls.Label TurnArea = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTurnArea");
                System.Web.UI.WebControls.Label TurnName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTurnName");
                System.Web.UI.WebControls.Label TurnType = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblTurnType");
                System.Web.UI.WebControls.Label AddresseeId = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblAddresseeId");
                System.Web.UI.WebControls.Label Addressee = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblAddressee");
                System.Web.UI.WebControls.Label DocumentBisId = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDocumentBisId");
                System.Web.UI.WebControls.Label DocumentID = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDocumentId");
                System.Web.UI.WebControls.Label ElaborationDate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblElaborationDate");
                System.Web.UI.WebControls.Label CutDate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblCutDate");
                System.Web.UI.WebControls.Label Days = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDays");


                Volante.Text = LinkingVolante(DocumentBisId.Text == "" ? "0" : DocumentBisId.Text) + " " + Volante.Text;
                Days.Text = GetDays(ElaborationDate.Text, CutDate.Text);

                nTotal++;
                if (currAddresseeId != AddresseeId.Text)
                {
                    //add blank row
                    currAddresseeId = AddresseeId.Text;
                    j++;
                    //k = row.ItemIndex + j;
                    //if (bTotal)
                    //nTot = TotalByArea(int.Parse(AddresseeId.Text));	

                    AddRow(row.ItemIndex + j, AddresseeId.Text + " / " + Addressee.Text);

                }
                else
                {
                    if (TurnArea.Text != "")
                        UpdateLabel(Volante, TurnType.Text + " - " + TurnArea + " - " + TurnName);

                }
            }
        }

        private void AddRow(int index, string sAddressee)
        {

            //Create bank row
            Table t = (Table)grdItems.Controls[0];
            TableCell blankCell = new TableCell();
            blankCell.BackColor = Color.Gray;
            blankCell.ColumnSpan = grdItems.Columns.Count;
            blankCell.Controls.Add(new LiteralControl(sAddressee));
            DataGridItem tblRow = new DataGridItem(0, 0, ListItemType.Item);
            blankCell.ID = "Link" + sAddressee;
            tblRow.Cells.Add(blankCell);
            t.Rows.AddAt(index, tblRow);

        }

        private void UpdateLabel(System.Web.UI.WebControls.Label lbl, string sTag)
        {
            lbl.Text = sTag;
        }

        private void AddRowTurn(int index, string sTurn)
        {
            //Create bank row
            Table t = (Table)grdItems.Controls[0];
            TableCell blankCell = new TableCell();
            blankCell.BackColor = Color.Gray;
            blankCell.ColumnSpan = grdItems.Columns.Count;
            blankCell.Controls.Add(new LiteralControl(sTurn));
            DataGridItem tblRow = new DataGridItem(0, 0, ListItemType.Item);
            tblRow.Cells.Add(blankCell);
            t.Rows.AddAt(index, tblRow);
        }


        protected string GetDays(int nID)
        {
            OracleDataReader odr = Document_Alternate.CalculateDaysByDocument(nID);
            DateTime lastDate = DateTime.Now;
            DateTime firstDate = DateTime.Now;

            if (!odr.Read())
                return "0";
            else
            {
                firstDate = odr.GetDateTime(2);
                if (odr["Total"].ToString() == odr["Concluido"].ToString())
                    lastDate = odr.GetDateTime(6);

                TimeSpan daysDiff = lastDate - firstDate;
                odr.Close();
                odr.Dispose();
                return daysDiff.Days.ToString();
            }
        }

        protected string GetDays(string fromDate, string toDate)
        {

            string dToDate = toDate;
            if (toDate.ToString() == "")
                dToDate = fromDate;

            TimeSpan daysDiff = Convert.ToDateTime(dToDate) - Convert.ToDateTime(fromDate);
            return daysDiff.Days.ToString();
        }

        /*
                private int TotalByArea(int nAreaId)
                {
                    return Documents.TotalByArea(nAreaId, dropShow.SelectedItem.Value);
                }
        */

        private string LinkingVolante(string sDocumentBisId)
        {

            return Documents.getTreeVolante(sDocumentBisId);
        }

        private void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void btnConvertToXLS_Click(object sender, System.EventArgs e)
        {


        }

        private void btnSendMail_Click(object sender, System.EventArgs e)
        {

        }

        private void lnkExcel_Click(object sender, System.EventArgs e)
        {
            //string strTitle = "c:\\inetpub\\wwwroot\\gestion\\user_files\\ReportDesigner\\Templates\\" + "BandejadeSalida1.xlt";


            string sStatusSend = dropShow.SelectedItem.Value;
            string sStatusInbox = drpTurnada.SelectedItem.Value;
                      

            string sTemp = String.Empty;
            switch (sStatusSend)
            {
                case "1":
                    sTemp = "Pendiente";
                    break;
                case "2":
                    sTemp = "Tramite";
                    break;
                case "3":
                    sTemp = "Concluido";
                    break;
                case "4":
                    sTemp = "Sin Tramite";
                    break;
                case "5":
                    sTemp = "Todos";
                    break;
                default:
                    sTemp = "Invalid selection";
                    break;
            }

            bool bTurnado = chkTurnados.Checked ? true : false;
            bool bCCpara = chkCC.Checked ? true : false;
            string sAreaId = "*";
            DataSet ds;
            int nDocumentType = drpDocumentType.SelectedIndex > 0 ? int.Parse(drpDocumentType.SelectedItem.Value) : 0;
            OracleDataReader r;
            if (rblOrder.SelectedItem.Text == "Volante")
            {
                       
               r = Documents.FilesPendinsSend(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text, txtElaborationDateTo.Text,
                    sTemp, sStatusInbox, sAreaId, rblOrder.SelectedItem.Text, bTurnado, bCCpara, nDocumentType, txtVolante.Text, txtTextSearch.Text, "XLS");
                              
            }
            else
            {
                                
                if (dropArea.SelectedItem.Selected)
                {
                    sAreaId = dropArea.SelectedItem.Value;
                }
                r = Documents.FilesAreaPendinsSend(Convert.ToInt32(Session["uid"]), txtElaborationDateFrom.Text, txtElaborationDateTo.Text,
                    sTemp, sStatusInbox, sAreaId, rblOrder.SelectedItem.Text, bTurnado, bCCpara, nDocumentType, txtVolante.Text, txtTextSearch.Text, "XLS");

            }

            System.Data.DataTable lDatosSesion = new System.Data.DataTable();
            Gestion.Sources.DataReader_Table drT = new Gestion.Sources.DataReader_Table();
            drT.FillFromReader(lDatosSesion, r);
            string lNombreExcel = "Reporte_" + sTemp;

            this.Session["dtConsultaQuery"] = lDatosSesion;
            string lUrl = string.Format("/gestion/Sources/ExportaExcel.ashx?Source={0}&Name={1}&FechaDel={2}&FechaAl={3}", "dtConsultaQuery", lNombreExcel, txtElaborationDateFrom.Text, txtElaborationDateTo.Text);
            //Response.Redirect(lUrl);
            //USH 20230918
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>DownloadElement('" + lUrl + "');</script>", false);


            //if (dropArea.SelectedIndex > 0)


            ////			ExcelConvert xlsCnv = new ExcelConvert();			
            ////			errLevel.Text = xlsCnv.CreateExcelWorkbook(strTitle, "Agrupación", r, txtElaborationDateFrom.Text, txtElaborationDateTo.Text, Session["uid"].ToString() );
            ////			Response.Redirect("create_report.aspx?filename=" + @errLevel.Text);
            ////			//sfilename= errLevel.Text
            ////			Response.Redirect("error.aspx?errMessage=" + errLevel.Text);



        }

        private void btnChangeArea_Click(object sender, System.EventArgs e)
        {
            DropAreaBind(Request["areaid"]);
        }





    }
}
