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
using Log.Layer.Business;
using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Model;
using Log.Layer.Model.Extension;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for edit_areas.
	/// </summary>
	public class edit_roles: System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnPuestoId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlInputText txtRol;
		protected System.Web.UI.HtmlControls.HtmlInputText txtEmpleado;
		protected System.Web.UI.HtmlControls.HtmlSelect cboEmployee;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCutDate;
	
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
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Lista de Roles / Edicion de Roles", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                SetDefaultValues();
			} 
			else 
			{
				ExecuteOperation();
			}	
		}

		private void Initialize() 
		{
			txtCutDate.Value = DateTime.Now.ToShortDateString();
		}

		private void SetDefaultValues() 
		{
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
			LoadData();
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
			DataSet ds = Roles.GetRolesByID(int.Parse(Request.QueryString["id"].ToString()), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			if (ds.Tables[0].Rows.Count > 0 ) {
				txtRol.Value	= ds.Tables[0].Rows[0]["rol"].ToString();
				txtEmpleado.Value	= ds.Tables[0].Rows[0]["nombre"].ToString();
			}
		}

		private void UpdateData()
		{

			if (int.Parse(Request.QueryString["id"].ToString()) == 0 ) {
				Roles.Create(cboEmployee.Value, txtRol.Value, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			} else {
				Roles.Update(int.Parse(Request.QueryString["id"].ToString()), cboEmployee.Value, txtRol.Value, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			}

		}

		private void RemoveData()
		{
			Roles.Remove(int.Parse(Request.QueryString["id"].ToString()), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
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
