using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;

namespace SAR.Web.Seguridad_bitacoras_accesos
{
    /// <summary>
    /// este es un comentario
    /// </summary>
    public class Handler1 : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// propiedad reusable
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// proceso para exportar 
        /// </summary>
        /// <param name="context">contexto kmadsmk</param>
        public void ProcessRequest(HttpContext context)
        {
            string lSource = context.Request.QueryString["Source"];
            string lName = context.Request.QueryString["Name"];
            string lFechaDel = context.Request.QueryString["FechaDel"];
            string lFechaAl = context.Request.QueryString["FechaAl"];
            DataTable lTabla = HttpContext.Current.Session[lSource] as DataTable;
            //this.ExportExcelFormato(context, lTabla, lName, lFechaDel, lFechaAl);
            this.ExportCvsFormato(context, lTabla, lName, lFechaDel, lFechaAl);
        }

        /// <summary>
        /// Exporta datos de tabla a excel directo desde objeto
        /// </summary>
        /// <param name="pContext">Contexto http</param>
        /// <param name="pTabla">Tabla con datos</param>
        /// <param name="pName">Nombre archivo excel</param>
        private void ExportExcel(HttpContext pContext, System.Data.DataTable pTabla, string pName)
        {

            // requires using System.Data
            //
            DataGrid dos = new DataGrid();
            pContext.Response.ClearContent();
            pContext.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.csv", pName));
            pContext.Response.ContentType = "application/ms-excel";
            pContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
            pContext.Response.Charset = "utf-8";
            pContext.Response.Charset = "";



            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dos.DataSource = pTabla;
            dos.DataBind();
            dos.RenderControl(htw);
            pContext.Response.Write(sw.ToString());
            pContext.Response.End();



        }



        /// <summary>
        /// Exporta datos de tabla a excel con formato (encabezado - datos)
        /// </summary>
        /// <param name="pContext">Contexto http</param>
        /// <param name="pTabla">Tabla con datos</param>
        /// <param name="pName">Nombre archivo excel</param>
        /// <param name="pFechaDel">Fecha elaboración del </param>
        /// /// <param name="pFechaAl">Fecha elaboración al </param>
        private void ExportExcelFormato(HttpContext pContext, DataTable pTabla, string pName, string pFechaDel, string pFechaAl)
        {
            pContext.Response.Clear();
            pContext.Response.ClearContent();
            pContext.Response.ClearHeaders();
            pContext.Response.Buffer = true;
            pContext.Response.ContentType = "application/ms-excel";
            pContext.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            pContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", pName));

            pContext.Response.Charset = "utf-8";
            pContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252");
            //sets font
            pContext.Response.Write("<font style='font: 8pt Arial;'>");
            pContext.Response.Write("<BR />");
            pContext.Response.Write("<img src='http://bnodcgd-b/gestion/images/BanobrasExcel.png'/>");  //las imagenes deben estar al tamaño deseado, no respeta el style.

            ////string periodo
            string lPeriodo = string.Format("   Del:    {0}      Al:    {1}", pFechaDel, pFechaAl);

            //         //tabla con encabezado
            //         //
            pContext.Response.Write("<table>");
            pContext.Response.Write("<tr style='text-align:center'>");
            pContext.Response.Write("<td colspan='3'>&nbsp;</td>");
            pContext.Response.Write("<td colspan='3' style='font: bold 18pt Arial;'>Relaci&oacute;n de correspondencia enviada</td>");
            pContext.Response.Write("<td colspan='2'>&nbsp;</td>");
            pContext.Response.Write("</tr>");
            pContext.Response.Write("<tr>");
            pContext.Response.Write("<td colspan='8'>&nbsp;</td>");
            pContext.Response.Write("</tr>");
            pContext.Response.Write("<tr style='height:80px' valign='top'>");
            pContext.Response.Write("<td colspan='4'>&nbsp;</td>");
            pContext.Response.Write("<td colspan='2' style='border: 1px solid black; text-align: left'><font style='font:bold 9pt Arial;'>Periodo:</font> <font style='font:9pt Arial;'>" + lPeriodo + "</font></td>");
            pContext.Response.Write("<td colspan='2'>&nbsp;</td>");
            pContext.Response.Write("</tr>");
            pContext.Response.Write("<tr style='height:30px'><td colspan='8'>&nbsp;</td></tr>");
            pContext.Response.Write("<tr valign='top' style='font:bold 8pt Arial; text-align:center; '>");
            pContext.Response.Write("<td rowspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:60px;'>Volante</td>");
            pContext.Response.Write("<td colspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:180px; '>Fecha</td>");
            pContext.Response.Write("<td rowspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:220px;'>Destinatario</td>");
            pContext.Response.Write("<td rowspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:220px;'>Remitente</td>");
            pContext.Response.Write("<td rowspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:220px;'>Asunto</td>");
            pContext.Response.Write("<td colspan='2' style='border: 1px solid black; background-color:#e6e6e6; width:300px;'>Observaciones</td>");
            pContext.Response.Write("</tr>");
            pContext.Response.Write("<tr  style='font: bold 8pt Arial; text-align:center; '>");
            pContext.Response.Write("<td style='border: 1px solid black; background-color:#e6e6e6; '>Documento</td>");
            pContext.Response.Write("<td style='border: 1px solid black; background-color:#e6e6e6; '>Elaboraci&oacute;n</td>");
            pContext.Response.Write("<td style='border: 1px solid black; background-color:#e6e6e6; '>Turnado A:</td>");
            pContext.Response.Write("<td style='border: 1px solid black; background-color:#e6e6e6; '>Desahogo</td>");
            pContext.Response.Write("<td   rowspan=='2' style='border: 1px solid black; background-color:#e6e6e6; '>&Aacute;rea a la que se turna</td>");
            pContext.Response.Write("<td   rowspan=='2' style='border: 1px solid black; background-color:#e6e6e6; '>Clave de &aacute;rea a quien se turna</td>");
            pContext.Response.Write("<td   rowspan=='2' style='border: 1px solid black; background-color:#e6e6e6; '>Fecha de turno</td>");
            pContext.Response.Write("<td   rowspan=='2' style='border: 1px solid black; background-color:#e6e6e6; '>Status</td>");
            pContext.Response.Write("</tr>");
            pContext.Response.Write("</table>");

            //se ingresan datos de pTabla
            pContext.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font: 8pt Arial; background:white;'>");

            //obtienes numero de columnas
            int columnscount = pTabla.Columns.Count;



            //recorres tabla para obtener datos por columna
            foreach (DataRow row in pTabla.Rows)
            {
                pContext.Response.Write("<tr valign='top'>");
                pContext.Response.Write("<td>" + row["VOLANTE"].ToString() + "</td>");
                pContext.Response.Write("<td>" + Convert.ToDateTime(row["FECHA_DOCUMENTO_FUENTE"].ToString()).ToShortDateString() + "</td>");
                pContext.Response.Write("<td>" + Convert.ToDateTime(row["FECHA_ELABORACION"].ToString()).ToShortDateString() + "</td>");
                pContext.Response.Write("<td>" + row["DESTINATARIONOMBRE"].ToString() + "<br />" + row["DESTINATARIOAREA"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["REMITENTENOMBRE"].ToString() + "<br />" + row["REMITENTEAREA"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["RESUMEN"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["TURNADOAREA"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["OBSERVACION"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["DESTINATARIOAREA"].ToString() + "</td>");
                pContext.Response.Write("<td>" + row["DESTINATARIOAREAID"].ToString() + "</td>");
                pContext.Response.Write("<td>" + Convert.ToDateTime(row["FECHA_CORTE"].ToString()).ToShortDateString() + "</td>");
                pContext.Response.Write("<td>" + row["STATUSVOLANTE"].ToString() + "</td>");
                pContext.Response.Write("</tr>");
            }
            pContext.Response.Write("</Table>");
            pContext.Response.Write("</font>");
            pContext.Response.Flush();
            pContext.Response.End();




        }

        private void ExportCvsFormato(HttpContext pContext, DataTable pTabla, string pName, string pFechaDel, string pFechaAl)
        {

            //DataTable table = new DataTable();
            //columns  

            //table.Columns.Add("VOLANTE", typeof(string));
            //table.Columns.Add("FECHA_DOCUMENTO_FUENTE", typeof(string));
            //table.Columns.Add("FECHA_ELABORACION", typeof(string));
            //table.Columns.Add("DESTINATARIONOMBRE", typeof(string));
            //table.Columns.Add("REMITENTENOMBRE", typeof(string));
            //table.Columns.Add("RESUMEN", typeof(string));
            //table.Columns.Add("TURNADOAREA", typeof(string));
            //table.Columns.Add("OBSERVACION", typeof(string));
            //table.Columns.Add("DESTINATARIOAREA", typeof(string));
            //table.Columns.Add("DESTINATARIOAREAID", typeof(string));
            //table.Columns.Add("FECHA_CORTE", typeof(string));
            //table.Columns.Add("STATUSVOLANTE", typeof(string));

            List<ReporteExportarExcel> lista = new List<ReporteExportarExcel>();


            foreach (DataRow row in pTabla.Rows)
            {
                ReporteExportarExcel re = new ReporteExportarExcel();

                re.VOLANTE = row["VOLANTE"].ToString();
                re.FECHA_DOCUMENTO = Convert.ToDateTime(row["FECHA_DOCUMENTO_FUENTE"].ToString()).ToShortDateString();
                re.FECHA_ELABORACION = Convert.ToDateTime(row["FECHA_ELABORACION"].ToString()).ToShortDateString();
                re.DESTINATARIO = row["DESTINATARIONOMBRE"].ToString() + " " + row["DESTINATARIOAREA"].ToString();
                re.REMITENTE = row["REMITENTENOMBRE"].ToString() + " " + row["REMITENTEAREA"].ToString();
                re.ASUNTO = row["RESUMEN"].ToString();
                re.AREA_TURNADA = row["TURNADOAREA"].ToString();
                re.OBSERVACION = row["OBSERVACION"].ToString();
                re.STATUS = row["STATUSVOLANTE"].ToString();
                re.CLAVE_AREA = row["NOMBRERECIBE"].ToString();
                re.TURNADO_AREA = row["AREARECIBE"].ToString();
                re.STATUS_TURNADO = row["ESTADO"].ToString();
                re.FECHA_TURNO = row["FCHSTATUSTURNO"].ToString();
                lista.Add(re);
               
            }

            //data  

            StringBuilder sb = new StringBuilder();
            string lPeriodo = string.Format(";;;;periodo   del:    {0}      al:    {1}", pFechaDel, pFechaAl);
            var first = ";;;;Relación de correspondencia enviada \n";
            var second = lPeriodo;
         
            var newLine = string.Format("{0}{1}\n", first, second);
            sb.AppendLine(newLine);

            //string columnas = string.Empty;
            //for (int i = 0; i < table.Columns.Count; i++)
            //{

            //    sb.Append(table.Columns[i].ColumnName);
            //    sb.Append(i == table.Columns.Count - 1 ? "\n" : ";");
            //}
            sb.Append(";Fecha;;;;;Observaciones\n");
            sb.Append("Volante;Documento;Elaboración;Destinatario;Remitente;Asunto;Turnado A:;Desahogo;Status del Volante DGAF;Nombre de la persona a la que se turna;Área a la que se turna;Status del turno;Fecha de status del turno\n");
            

            foreach (ReporteExportarExcel ree in lista)
            {
                sb.Append($"{ree.VOLANTE};{ree.FECHA_DOCUMENTO};{ree.FECHA_ELABORACION};{ree.DESTINATARIO};{ree.REMITENTE};{ree.ASUNTO};{ree.AREA_TURNADA};{ree.OBSERVACION};{ree.STATUS};{ree.CLAVE_AREA};{ree.TURNADO_AREA};{ree.STATUS_TURNADO};{ree.FECHA_TURNO}\n");
                
            }
          
            var bytes = Encoding.GetEncoding("utf-8").GetBytes(sb.ToString());
            MemoryStream stream = new MemoryStream(bytes);

            StreamReader reader = new StreamReader(stream);


            pContext.Response.Clear();
            pContext.Response.ClearContent();
            pContext.Response.ClearHeaders();
            pContext.Response.Buffer = true;
            pContext.Response.ContentType = "application/csv";
            pContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.csv", pName));

            pContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
            pContext.Response.Charset = "utf-8";


            pContext.Response.Write(reader.ReadToEnd());
            pContext.Response.Flush();
            pContext.Response.End();


        }



        /// <summary>
        /// metodo original
        /// </summary>
        /// <param name="pContext">contexto http</param>
        /// <param name="pTabla">tabla datos</param>
        /// <param name="pName">nombre archivo</param>
        private void Original(HttpContext pContext, DataTable pTabla, string pName)
        {
            pContext.Response.Clear();
            pContext.Response.ClearContent();
            pContext.Response.ClearHeaders();
            pContext.Response.Buffer = true;
            pContext.Response.ContentType = "application/ms-excel";
            pContext.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            pContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", pName));
            pContext.Response.Charset = "utf-8";
            pContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            pContext.Response.Write("<font style='font: 8pt Arial;'>");
            pContext.Response.Write("<BR />");

            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            pContext.Response.Write("<Table border='1' bgColor='#ffffff' " +
                "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                "style='font-size:10.0pt; font-family:Calibri; background:white;'>");

            //am getting my grid's column headers
            int columnscount = pTabla.Columns.Count;

            pContext.Response.Write("<TR>");
            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                pContext.Response.Write("<Td style='font: bold; font-size:11.0pt; font-family:Calibri; background-color:#e6e6e6; text-align:center;'>");
                //Get column headers  and make it as bold in excel columns
                pContext.Response.Write("<B>");
                pContext.Response.Write(pTabla.Columns[j].ColumnName);
                pContext.Response.Write("</B>");
                pContext.Response.Write("</Td>");
            }
            pContext.Response.Write("</TR>");

            foreach (DataRow row in pTabla.Rows)
            {//write in new row
                pContext.Response.Write("<TR>");
                for (int i = 0; i < pTabla.Columns.Count; i++)
                {
                    pContext.Response.Write("<Td>");
                    pContext.Response.Write(row[i].ToString());
                    pContext.Response.Write("</Td>");
                }

                pContext.Response.Write("</TR>");
            }
            pContext.Response.Write("</Table>");
        }
    }

    public class ReporteExportarExcel
    {
        public string VOLANTE { get; set; }
        public string FECHA_DOCUMENTO { get; set; }
        public string FECHA_ELABORACION { get; set; }
        public string DESTINATARIO { get; set; }
        public string REMITENTE { get; set; }
        public string ASUNTO { get; set; }
        public string TURNADO_AREA { get; set; }
        public string STATUS_TURNADO { get; set; }
        public string OBSERVACION { get; set; }
        public string AREA_TURNADA { get; set; }
        public string CLAVE_AREA { get; set; }
        public string FECHA_TURNO { get; set; }
        public string STATUS { get; set; }
    }

}