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
	/// Summary description for insert_mail_addressee.
	/// </summary>
	public class edit_empleados : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.DataGrid dgFirma;
		protected System.Web.UI.WebControls.DropDownList dropUsers;
		protected System.Web.UI.HtmlControls.HtmlInputText txtExpediente;
		protected System.Web.UI.HtmlControls.HtmlInputText Text3;
		protected System.Web.UI.HtmlControls.HtmlInputText txtName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtLastName;
		protected System.Web.UI.HtmlControls.HtmlSelect cboArea;
		protected System.Web.UI.HtmlControls.HtmlInputText txtLogin;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPassword;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblPuesto;
		protected System.Web.UI.HtmlControls.HtmlSelect cboPuesto;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCategoria;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblArea;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFromDate;
		protected System.Web.UI.HtmlControls.HtmlSelect cboTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnPuestoId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCutDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnToDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
            Initialize();
			if (!IsPostBack) {
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Vista de Empleados / Edicion Empleado", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
                SetDefaultValues();
			} else {
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
			LoadAreas();
			LoadPuestos();
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
			DataSet ds = Users.GetUsersById(int.Parse(Request.QueryString["id"].ToString()), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			if (ds.Tables[0].Rows.Count > 0 ) {
				txtExpediente.Value	= ds.Tables[0].Rows[0]["clave_empleado"].ToString();
				txtName.Value		= ds.Tables[0].Rows[0]["nombre"].ToString();
				txtLastName.Value	= ds.Tables[0].Rows[0]["apellidoNombre"].ToString();
				lblPuesto.InnerText	= ds.Tables[0].Rows[0]["clave_puesto"].ToString() + " - " +ds.Tables[0].Rows[0]["puesto"].ToString();
				lblArea.InnerText	= ds.Tables[0].Rows[0]["cve_area"].ToString() + " - " + ds.Tables[0].Rows[0]["area"].ToString();				
				txtLogin.Value		= ds.Tables[0].Rows[0]["login"].ToString();
				txtPassword.Value	= ds.Tables[0].Rows[0]["password"].ToString();
				txtCategoria.Value	= ds.Tables[0].Rows[0]["categoria"].ToString();
				lblTipoEmpleado.InnerText = ds.Tables[0].Rows[0]["tipo_usuario"].ToString();
				hdnFromDate.Value = ds.Tables[0].Rows[0]["fecha_inicio"].ToString();
				hdnToDate.Value = ds.Tables[0].Rows[0]["fecha_fin"].ToString();
				hdnAreaId.Value   = ds.Tables[0].Rows[0]["id_area"].ToString();
				hdnPuestoId.Value = ds.Tables[0].Rows[0]["id_puesto"].ToString();
				hdnTipoEmpleado.Value = ds.Tables[0].Rows[0]["tipo_usuario"].ToString();
				cboTipoEmpleado.Value = hdnTipoEmpleado.Value;
			}
		}

		private void LoadAreas()
		{
			cboArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboArea.DataValueField = "id_area";
			cboArea.DataTextField = "area";
			cboArea.DataBind();
			cboArea.Items.Insert(0, new ListItem("» Seleccione",""));
			cboArea.Value = hdnAreaId.Value;
		}

		private void LoadPuestos()
		{
			cboPuesto.DataSource = Users.GetPuesto();
			cboPuesto.DataValueField = "puesto_id";
			cboPuesto.DataTextField = "descripcion";
			cboPuesto.DataBind();
			cboPuesto.Items.Insert(0, new ListItem("» Seleccione",""));
			cboPuesto.Value = hdnPuestoId.Value;
		}

		private void UpdateData()
		{
			int nAreaId   = 0;
			int nPuestoId = 0;
			string sTipoEmpleado = string.Empty;
			
			string cveArea = GetClaveArea(int.Parse(cboArea.Value));
			string cvePuesto = GetClavePuesto(int.Parse(cboPuesto.Value));

			if (int.Parse(Request.QueryString["id"].ToString()) == 0 )
			{
				nAreaId   = int.Parse(cboArea.Value);
				nPuestoId = int.Parse(cboPuesto.Value);
				sTipoEmpleado = cboTipoEmpleado.Value;

				Users.Create(txtExpediente.Value, txtName.Value, txtLastName.Value, txtCategoria.Value, sTipoEmpleado, nAreaId, cveArea, 
					nPuestoId, cvePuesto, txtLogin.Value, txtPassword.Value, DateTime.Now.ToShortDateString(), "31-12-2049", Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			}
			else
			{

				nAreaId   = int.Parse(hdnAreaId.Value );
				nPuestoId = int.Parse(hdnPuestoId.Value );
				sTipoEmpleado = hdnTipoEmpleado.Value;

				if (cboArea.Value != "")
					nAreaId = int.Parse(cboArea.Value);
				if (cboPuesto.Value != "")
					nPuestoId = int.Parse(cboPuesto.Value);
				if (cboTipoEmpleado.Value != "")
					sTipoEmpleado = cboTipoEmpleado.Value;
	
				Users.Update(int.Parse(Request.QueryString["id"].ToString()), txtName.Value, 
					txtLastName.Value, nAreaId, cveArea, cvePuesto, txtCategoria.Value, sTipoEmpleado, DateTime.Now.ToShortDateString(), "31-12-2049", 
					txtLogin.Value, txtPassword.Value, txtExpediente.Value, nPuestoId, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			}

		}

		private string GetClaveArea(int areaId)
		{
			return Areas.GetClaveArea(areaId);
		}

		private string GetClavePuesto(int puestoId)
		{
			return Areas.GetClavePuesto(puestoId);
		}

		private void RemoveData()
		{
			Users.Remove(int.Parse(Request.QueryString["id"].ToString()), "1", DateTime.Now.ToShortDateString(), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
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
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
