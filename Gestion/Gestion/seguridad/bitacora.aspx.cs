using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Extension;
using Log.Layer.Business;
using Log.Layer.Model.Model;
using BComponents.DataAccessLayer;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

namespace Gestion.gestion.seguridad
{
    public partial class bitacora : System.Web.UI.Page
    {
        public bool _offLine;
        public string _role;
        List<Log.Layer.Model.Model.Enumerator.enuAction> _action = Enum.GetValues(typeof(Log.Layer.Model.Model.Enumerator.enuAction)).Cast<Log.Layer.Model.Model.Enumerator.enuAction>().ToList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Bitacora", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                ViewState["SortExpression"] = "LogId";
                ViewState["SortDirection"] = "DESC";
                _role = Session["rol"].ToString();
                Get();
            }
        }
        private void Get() {
            UserGet();
            ModuleGet();
            ActionGet();
            TableGet();
            btnExportar.Visible = false;
        }
        private void UserGet() {
            List<Item> result = new List<Item>();
            if (!_offLine)
            {
                string sql = "SELECT CAST(ID_EMPLEADO AS varchar(20)) \"value\",NOMBRE \"text\", '' label FROM CGESTION.SOF_EMPLEADOS ORDER BY NOMBRE";
                OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sql);
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
                        , new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Bitacora", enuAction.Retrieve.GetDescription(), "SOF_EMPLEADOS", sql, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                if (dr != null)
                {
                    result = dr.DataReaderMapToList<Item>();
                    if (result != null)
                    {
                        if (result.Any())
                        {
                            cboUsuario.DataSource = result;
                            cboUsuario.DataValueField = "value";
                            cboUsuario.DataTextField = "text";
                            cboUsuario.DataBind();
                            cboUsuario.Items.Insert(0, "--Todos--");
                        }
                    }
                }
            }
            else
            {
                var list = Enumerable.Range(1, 10).ToList();
                if (list != null)
                {
                    if (list.Any())
                    {
                        list.ForEach(x => {
                            Item item = new Item();
                            item.text = string.Format("Usuario {0}", x);
                            item.value = x.ToString();
                            result.Add(item);
                        });
                    }
                }
                cboUsuario.DataSource = result;
                cboUsuario.DataValueField = "value";
                cboUsuario.DataTextField = "text";
                cboUsuario.DataBind();
                cboUsuario.Items.Insert(0, "--Todos--");
            }


        }
        private void ModuleGet()
        {
            //List<Item> result = new List<Item>();
            //if (!_offLine)
            //{
            //    string sql = "SELECT CAST(APPLICATIONID  AS varchar(20))   \"value\",APPNAME  \"text\" FROM CGESTION.MHAPPLICATIONS ORDER BY APPNAME";
            //    OracleDataReader dr = OracleHelper.ExecuteReader(ConfigurationManager.AppSettings["ConnectionString"], CommandType.Text, sql);
            //    ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
            //        , new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Bitacora", enuAction.Retrieve.GetDescription(), "MHAPPLICATIONS", sql, Request.UserHostAddress));
            //    if (dr != null)
            //    {
            //        result = dr.DataReaderMapToList<Item>();
            //        if (result != null)
            //        {
            //            if (result.Any())
            //            {
            //                cboPantalla.DataSource = result;
            //                cboPantalla.DataValueField = "value";
            //                cboPantalla.DataTextField = "text";
            //                cboPantalla.DataBind();
            //                cboPantalla.Items.Insert(0, "--Todos--");
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    var list = Enumerable.Range(1, 10).ToList();
            //    if (list != null)
            //    {
            //        if (list.Any())
            //        {
            //            list.ForEach(x => {
            //                Item item = new Item();
            //                item.text = string.Format("Pantalla {0}", x);
            //                item.value = x.ToString();
            //                result.Add(item);
            //            });
            //        }
            //    }
            //    cboPantalla.DataSource = result;
            //    cboPantalla.DataValueField = "value";
            //    cboPantalla.DataTextField = "text";
            //    cboPantalla.DataBind();
            //    cboPantalla.Items.Insert(0, "--Todos--");
            //}

        }
        private void ActionGet()
        {
            List<Item> result=new List<Item> ();
            var list = _action.Select(x=>x.GetDescription()).ToList();
            int i = 0;
            if (list != null)
            {
                if (list.Any())
                {
                    list.ForEach(x => {
                        Item item = new Item();
                        i++;
                        item.text = x;
                        item.value = i.ToString();
                        result.Add(item);
                    });
                }
            }
            cboAccion.DataSource = result;            
            cboAccion.DataValueField = "value";
            cboAccion.DataTextField = "text";
            cboAccion.DataBind();
            cboAccion.Items.Insert(0, "--Todos--");
        }
        private void TableGet()
        {
            //List<Item> result = new List<Item>();
            //var list = Enumerable.Range(1,10).ToList();
            //if (list != null)
            //{
            //    if (list.Any())
            //    {
            //        list.ForEach(x => {
            //            Item item = new Item();
            //            item.text = string.Format("Tabla {0}", x);
            //            item.value = x.ToString();
            //            result.Add(item);
            //        });
            //    }
            //}
            //cboTabla.DataSource = result;
            //cboTabla.DataValueField = "value";
            //cboTabla.DataTextField = "text";
            //cboTabla.DataBind();
            //cboTabla.Items.Insert(0, "--Todos--");
        }
        private void DataGet()
        {
            
            var list = Enumerable.Range(1, 10).ToList();
            if (list != null)
            {
                if (list.Any())
                {
                    //var result=list.Select(x => new {
                    //    LogId=x,
                    //    UserId=string.Format("Usuario {0}",x),
                    //    DateTimeEvent =DateTime.Now.ToShortDateString(),
                    //    Module = string.Format("Pantalla {0}", x),
                    //    Action = string.Format("Accion {0}", x),
                    //    IsDB=true,
                    //    Table = string.Format("Tabla {0}", x),
                    //    Sentence = string.Format("Sentencia {0}", x),
                    //}).ToList<object>();
                    var filter = GetDataFilter();
                    if (filter == null) {
                        return;
                    }
                    var result=ControlLog.GetInstance().Get(OracleHelper.ExecuteReader, filter);
                    var dt = result.result;                    
                    ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery
                        , new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Bitacora", enuAction.Retrieve.GetDescription(), "LOGSYSTEM", result.message, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                    ViewState["dt"] = dt;
                    gvResultado.DataSource = dt;
                    gvResultado.DataBind();
                    btnExportar.Visible = dt!=null && dt.Rows.Count>0;
                    HideColumn();
                }
            }
        }
        private LogSystemFilter GetDataFilter() {
            LogSystemFilter result=new LogSystemFilter();
            if (cboUsuario.SelectedIndex > 0) result.UserId = Convert.ToInt32(cboUsuario.SelectedValue);
            if(!string.IsNullOrEmpty(txtPantalla.Text))result.Module = txtPantalla.Text;
            if (cboAccion.SelectedIndex > 0) result.Action = cboAccion.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtTabla.Text)) result.Table= txtTabla.Text;
            try
            {
                if (!string.IsNullOrEmpty(txtDesde.Text)) result.DateTimeEventFrom = Convert.ToDateTime(txtDesde.Text);
                if (!string.IsNullOrEmpty(txtHasta.Text)) result.DateTimeEventTo= Convert.ToDateTime(txtHasta.Text);
                if (result.DateTimeEventFrom.HasValue && result.DateTimeEventTo.HasValue)
                {
                    if (result.DateTimeEventFrom.Value > result.DateTimeEventTo.Value)
                    {
                        result = null;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "openModal(\"Validacion\",\"La Fecha Desde debe de ser menor a Fecha Hasta\");", true);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "openModal(\"Validacion\",\"La Fecha Desde y Fecha Hasta deben de tener el formato dd/mm/aaaa\");", true);
            }
            
            return result;
        }
        private void DataExport() {
            DataGrid dos = new DataGrid();
            
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xls", Guid.NewGuid()));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Charset = "";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            var dt = ViewState["dt"] as DataTable;
            var list = Enumerable.Range(1, 10).ToList();
            if (dt != null && dt.Rows.Count>0)
            {
                dos.DataSource = dt;
            }
            dos.DataBind();
            dos.RenderControl(htw);
            ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Bitacora", enuAction.Download.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
            HttpContext.Current.Response.Write(sw.ToString());
            this.Context.Response.End();
        }        
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Requerido para evitar el error de verificación de controles ASP.NET
        }
        protected void bntLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            cboUsuario.SelectedIndex = 0;
            //cboPantalla.SelectedIndex = 0;
            txtPantalla.Text = string.Empty;
            cboAccion.SelectedIndex = 0;
            //cboTabla.SelectedIndex = 0;
            txtTabla.Text = string.Empty;
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            DataGet();
        }
        protected void btnExportar_Click(object sender, ImageClickEventArgs e)
        {
            if (gvResultado.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "openModal(\"Validacion\",\"Realice una busqueda\");", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "run();", true);
            }
        }
        protected void btnExportar2_Click(object sender, EventArgs e)
        {
            DataExport();
        }
        protected void gvResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["LogSystemItem"] = null;
            if (e.CommandName == "Select")
            {
                string value = e.CommandArgument.ToString().Replace("\n",string.Empty).Replace("\"", "$$");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "openModal(\"Sentencia Ejecutada\",\"" + value+ "\");", true);
            }
            else if (e.CommandName=="Detail")
            {
                LogSystem logSystem = new LogSystem(); 
                string value = e.CommandArgument.ToString();
                var dt = ViewState["dt"] as DataTable;
                var rows=dt.Select(" LOGID=" + value);
                if (rows!=null && rows.Count()>0)
                {
                    foreach (var item in rows)
                    {
                        logSystem.LogId = Convert.ToInt32(item[0]);
                        logSystem.Action= item[5].ToString();
                        logSystem.DateTimeEvent= Convert.ToDateTime(item[3]);
                        logSystem.Metadata= item[10].ToString();
                    }
                    Session["LogSystemItem"] = logSystem;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "doOperation();", true);
                    
                }
            }
        }
        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                string isBD = e.Row.Cells[5].Text;
                bool isJson = !string.IsNullOrEmpty(((System.Data.DataRowView)e.Row.DataItem).Row.ItemArray[10].ToString());
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("btnSelect");
                ImageButton btnDetail = (ImageButton)e.Row.FindControl("btnDetail");
                btnSelect.Visible = isBD == "1";
                btnDetail.Visible = isJson;
               
            }
        }
        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            DataGet();
        }
        protected void gvResultado_Sorting(object sender, GridViewSortEventArgs e)
        {
            var dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvResultado.DataSource = dv;
                gvResultado.DataBind();
                HideColumn();
            }
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        private void HideColumn() {
            bool isAdministrator = _role == "Administrator";
            if (!isAdministrator)
            {
                List<int> column = new List<int> { 5, 6, 9 };
                column.ForEach(x => {
                    gvResultado.Columns[x].Visible = false;
                });
            }
        }

        
    }
}