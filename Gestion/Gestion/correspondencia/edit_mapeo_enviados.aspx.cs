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
using BComponents.DataAccessLayer;
using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Extension;
using Log.Layer.Business;
using Log.Layer.Model.Model;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for edit_mapeo_enviados.
	/// </summary>
	public class edit_mapeo_enviados : System.Web.UI.Page
	{

		protected System.Web.UI.HtmlControls.HtmlSelect cboArea;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCutDate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboAddressee;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblPuesto;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnPuestoId;
		protected System.Web.UI.HtmlControls.HtmlInputText txtEmpleadoId;
		protected System.Web.UI.HtmlControls.HtmlInputText txtName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtAddrId;
		protected System.Web.UI.HtmlControls.HtmlInputText txtAddr;
		protected System.Web.UI.HtmlControls.HtmlSelect Select1;
		protected System.Web.UI.HtmlControls.HtmlSelect cboEmployer;
		protected System.Web.UI.HtmlControls.HtmlSelect cboEmployee;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoEmpleado;
	

		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			Initialize();
			if (!IsPostBack) 
			{
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Vista de Enviados / Edición Correo Enviado", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                SetDefaultValues();
			}
			else 
			{
				ExecuteOperation();
			}	
		}


		private void Initialize() 
		{
		}

		private void SetDefaultValues() 
		{
			txtCutDate.Value = DateTime.Now.ToShortDateString();
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
			LoadData();
			//LoadAreas();
			LoadAddr();
			LoadEmployee();
		}

		private void ExecuteOperation() 
		{
			string operation = Request.Form["hdnOperationTag"];
      
			if (operation == null || operation.Length == 0) 
			{
				return;
			}
			switch (operation.ToUpper()) 
			{
				case "UPDATE":
					UpdateData();
					break;
				case "REMOVE":
					RemoveData();
					break;

			}
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
		}

		private void LoadData()
		{
			DataSet ds = MapeoRegla.GetMapeoReglaRowId(Request.QueryString["id"].ToString(), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			if (ds.Tables[0].Rows.Count > 0 ) 
			{
				txtEmpleadoId.Value	= ds.Tables[0].Rows[0]["id_empleado"].ToString();
				txtName.Value		= ds.Tables[0].Rows[0]["nombre"].ToString();
				
				txtAddrId.Value  = ds.Tables[0].Rows[0]["id_empleado_bis"].ToString();
				txtAddr.Value  = ds.Tables[0].Rows[0]["nombreAdd"].ToString();
			}
		}

		private void LoadAreas()
		{
			cboArea.DataSource = Areas.GetArea();
			cboArea.DataValueField = "id_area";
			cboArea.DataTextField = "area";
			cboArea.DataBind();
			cboArea.Items.Insert(0, new ListItem("» Seleccione",""));
		}

		private void LoadAddr()
		{
			string sWhere = "sof_empleados.id_area = sof_areas.id_area(+) " +
				"And (sof_empleados.fecha_inicio <= '" + txtCutDate.Value + "') And  (sof_empleados.fecha_fin >= '" + txtCutDate.Value + "') " +
				"Order by sof_empleados.apellidonombre ASC";

			DataSet ds = Users.GetUsers(sWhere);
			cboAddressee.DataSource = ds;
			cboAddressee.DataValueField = "id_empleado";
			cboAddressee.DataTextField = "apearea";
			cboAddressee.DataBind();
			cboAddressee.Items.Insert(0, new ListItem("» Seleccione",""));
		}

		private void LoadEmployee()
		{
			string sWhere = "sof_empleados.id_area = sof_areas.id_area(+) " +
				"And (sof_empleados.fecha_inicio <= '" + txtCutDate.Value + "') " +
				"And  (sof_empleados.fecha_fin >= '" + txtCutDate.Value + "') " +
				"Order By sof_empleados.apellidonombre ASC";

			DataSet ds = Users.GetUsers(sWhere);
			cboEmployee.DataSource = ds;
			cboEmployee.DataValueField = "id_empleado";
			cboEmployee.DataTextField = "apearea";
			cboEmployee.DataBind();
			cboEmployee.Items.Insert(0, new ListItem("» Seleccione",""));
		}


		private void RemoveData()
		{
			MapeoRegla.Remove(Request.QueryString["id"].ToString(), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
		}


		private void UpdateData()
		{
			int nEmployeeBisId = int.Parse(cboAddressee.Value);
			int nEmployeeId = 0;

			if (Request.QueryString["id"].ToString().Length == 0 ) 
			{
				nEmployeeId = int.Parse(cboEmployee.Value);
				MapeoRegla.Create(nEmployeeId, nEmployeeBisId, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			} 
			else 
			{
                //MapeoRecibidos.Update JAMN ANTES
                MapeoRecibidos.Update(Request.QueryString["id"].ToString(), nEmployeeId, nEmployeeBisId, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
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
