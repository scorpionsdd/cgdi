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
	public class edit_regla : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.DataGrid dgFirma;
		protected System.Web.UI.WebControls.DropDownList dropUsers;
		protected System.Web.UI.HtmlControls.HtmlInputText Text3;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnPuestoId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlSelect cboFolioArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboRemitenteNombre;
		protected System.Web.UI.HtmlControls.HtmlSelect cboRemitenteArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboDestinatarioNombre;
		protected System.Web.UI.HtmlControls.HtmlSelect cboDestinatarioArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboSign;
		protected System.Web.UI.HtmlControls.HtmlSelect cboTipoDocArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboTipoDocUser;
		protected System.Web.UI.HtmlControls.HtmlSelect cboRemExtArea;
		protected System.Web.UI.HtmlControls.HtmlSelect cboRemExtUser;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnToDate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblNombre;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblRemitenteNombre;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblRemitenteArea;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblDestinatarioNombre;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblSign;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblFolio;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblTipoDocumento;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblRemExt;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblDestinatarioArea;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCutDate;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkDocumentoTipo;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkCambiaStatus;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkResponderCascada;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkSeguimiento;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkTipoDocumento;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkRemIncluyeOper;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkTurArbol;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkConcluirAcuseCcpara;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkConfConcluir;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkRemExtCat;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkRemitenteExterno;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnRemNameId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnRemAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnDestNameId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnDestAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSignId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFolioId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden11;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden12;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden13;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden14;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnEmployeeId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoDocumentoArea;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoDocumentoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnRemitenteExternoId;

		protected System.Web.UI.HtmlControls.HtmlSelect cboEmployee;
		
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
                ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Vista de Reglas / Editor de Regla", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
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
			LoadRemitente();
			LoadAreaRemitente();
			LoadDestinatario();
			LoadAreaDestinatario();
			LoadSign();
			LoadFolio();
			LoadRemExtArea();
			LoadRemExtUser();
			LoadTipoDocArea();
			LoadTipoDocUser();
			LoadEmployee();
			LoadData();
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
			DataSet ds = Regla.GetRegla( int.Parse(Request.QueryString["id"].ToString()), Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			if (ds.Tables[0].Rows.Count > 0 ) {
				
				lblNombre.InnerText = ds.Tables[0].Rows[0]["usuarioNombre"].ToString();
				lblRemitenteNombre.InnerText	= ds.Tables[0].Rows[0]["remitenteNombre"].ToString();
				lblRemitenteArea.InnerText		= ds.Tables[0].Rows[0]["remitenteClaveArea"].ToString() + "  " + ds.Tables[0].Rows[0]["remitenteArea"].ToString();
				
				lblDestinatarioNombre.InnerText	= ds.Tables[0].Rows[0]["destinatarioNombre"].ToString();
				lblDestinatarioArea.InnerText	= ds.Tables[0].Rows[0]["destinatarioClaveArea"].ToString() + "  " + ds.Tables[0].Rows[0]["destinatarioArea"].ToString();
				
				lblSign.InnerText = ds.Tables[0].Rows[0]["signNombre"].ToString();
				lblFolio.InnerText = ds.Tables[0].Rows[0]["folioClaveArea"].ToString() + " / " + ds.Tables[0].Rows[0]["folioArea"].ToString();
				
				hdnEmployeeId.Value =  ds.Tables[0].Rows[0]["ID_EMPLEADO"].ToString();
				hdnRemAreaId.Value  =  ds.Tables[0].Rows[0]["ID_REMITENTE_AREA"].ToString();
				hdnRemNameId.Value  =  ds.Tables[0].Rows[0]["ID_REMITENTE_TITULAR"].ToString();
				hdnDestAreaId.Value =  ds.Tables[0].Rows[0]["ID_DESTINATARIO_AREA"].ToString();
				hdnDestNameId.Value =  ds.Tables[0].Rows[0]["ID_DESTINATARIO_TITULAR"].ToString();
				hdnSignId.Value     =  ds.Tables[0].Rows[0]["ID_FIRMA"].ToString();
				hdnFolioId.Value    =  ds.Tables[0].Rows[0]["ID_FOLIO"].ToString();
				hdnTipoDocumentoArea.Value = ds.Tables[0].Rows[0]["ID_TIPO_DOCUMENTO_AREA"].ToString();
				hdnTipoDocumentoEmpleado.Value = ds.Tables[0].Rows[0]["ID_TIPO_DOCUMENTO_EMPLEADO"].ToString();
				hdnRemitenteExternoId.Value = ds.Tables[0].Rows[0]["ID_REMITENTE_EXTERNO"].ToString();
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["documento_tipo"]) )
					chkDocumentoTipo.Checked = ds.Tables[0].Rows[0]["documento_tipo"].ToString() == "Emp" ? true : false;

				if (chkDocumentoTipo.Checked == true)
					lblTipoDocumento.InnerText = ds.Tables[0].Rows[0]["TIPO_DOC_EMP"].ToString();
				else
					lblTipoDocumento.InnerText = ds.Tables[0].Rows[0]["TIPO_DOC_AREA"].ToString();

				lblRemExt.InnerText = ds.Tables[0].Rows[0]["REM_EXT"].ToString();


				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["responder_cascada"]) )
					chkResponderCascada.Checked = ds.Tables[0].Rows[0]["responder_cascada"].ToString() == "Si" ? true : false;

				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["cambia_estatus"]) )
					chkCambiaStatus.Checked = ds.Tables[0].Rows[0]["cambia_estatus"].ToString() == "Si" ? true : false;
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["REMITENTE_EXT_CATALOGO"]) )
					chkRemExtCat.Checked = ds.Tables[0].Rows[0]["REMITENTE_EXT_CATALOGO"].ToString() == "Si" ? true : false;

				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["tipo_documento_catalogo"]) )
					chkTipoDocumento.Checked = ds.Tables[0].Rows[0]["tipo_documento_catalogo"].ToString() == "Si" ? true : false;

				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["rem_incluye_oper"]) )
					chkRemIncluyeOper.Checked = ds.Tables[0].Rows[0]["rem_incluye_oper"].ToString() == "Si" ? true : false;

				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["tur_arbol"]) )
					chkTurArbol.Checked = ds.Tables[0].Rows[0]["tur_arbol"].ToString() == "Si" ? true : false;
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["confirma_concluir"]) )
					chkConfConcluir.Checked = ds.Tables[0].Rows[0]["confirma_concluir"].ToString() == "Si" ? true : false;
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["concluir_acuse_ccpara"]) )
					chkConcluirAcuseCcpara.Checked = ds.Tables[0].Rows[0]["concluir_acuse_ccpara"].ToString() == "Si" ? true : false;
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["REMITENTE_EXTERNO"]) )
					chkRemitenteExterno.Checked = ds.Tables[0].Rows[0]["REMITENTE_EXTERNO"].ToString() == "Si" ? true : false;
				
				if (! Convert.IsDBNull(ds.Tables[0].Rows[0]["SEGUIMIENTO"]) )
					chkSeguimiento.Checked = ds.Tables[0].Rows[0]["SEGUIMIENTO"].ToString() == "Si" ? true : false;

			}
		}

	
		private void LoadEmployee()
		{
			cboEmployee.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboEmployee.DataValueField = "id_empleado";
			cboEmployee.DataTextField = "nombre";
			cboEmployee.DataBind();
			cboEmployee.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboEmployee.Value = "0";
		}

		private void LoadRemitente()
		{
			cboRemitenteNombre.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboRemitenteNombre.DataValueField = "id_empleado";
			cboRemitenteNombre.DataTextField = "nombre";
			cboRemitenteNombre.DataBind();
			cboRemitenteNombre.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboRemitenteNombre.Value = "0";
		}

		private void LoadAreaRemitente()
		{
			cboRemitenteArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboRemitenteArea.DataValueField = "id_area";
			cboRemitenteArea.DataTextField = "area";
			cboRemitenteArea.DataBind();
			cboRemitenteArea.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboRemitenteArea.Value = "0";
		}

		private void LoadDestinatario()
		{
			cboDestinatarioNombre.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboDestinatarioNombre.DataValueField = "id_empleado";
			cboDestinatarioNombre.DataTextField = "nombre";
			cboDestinatarioNombre.DataBind();
			cboDestinatarioNombre.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboDestinatarioNombre.Value = "0";
		}

		private void LoadAreaDestinatario()
		{
			cboDestinatarioArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboDestinatarioArea.DataValueField = "id_area";
			cboDestinatarioArea.DataTextField = "area";
			cboDestinatarioArea.DataBind();
			cboDestinatarioArea.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboDestinatarioArea.Value = "0";
		}

		private void LoadSign()
		{
			cboSign.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboSign.DataValueField = "id_empleado";
			cboSign.DataTextField = "nombre";
			cboSign.DataBind();
			cboSign.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboSign.Value = "0";
		}


		private void LoadFolio()
		{
			cboFolioArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboFolioArea.DataValueField = "id_area";
			cboFolioArea.DataTextField = "area";
			cboFolioArea.DataBind();
			cboFolioArea.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboFolioArea.Value = "0";
		}

		private void LoadRemExtArea()
		{
			cboRemExtArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboRemExtArea.DataValueField = "id_area";
			cboRemExtArea.DataTextField = "area";
			cboRemExtArea.DataBind();
			cboRemExtArea.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboRemExtArea.Value = "0";
		}

		
		private void LoadRemExtUser()
		{
			cboRemExtUser.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboRemExtUser.DataValueField = "id_empleado";
			cboRemExtUser.DataTextField = "nombre";
			cboRemExtUser.DataBind();
			cboRemExtUser.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboRemExtUser.Value = "0";
		}

		private void LoadTipoDocArea()
		{
			cboTipoDocArea.DataSource = Users.GetAreaByDate(txtCutDate.Value);
			cboTipoDocArea.DataValueField = "id_area";
			cboTipoDocArea.DataTextField = "area";
			cboTipoDocArea.DataBind();
			cboTipoDocArea.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboTipoDocArea.Value = "0";
		}

		
		private void LoadTipoDocUser()
		{
			cboTipoDocUser.DataSource = Users.GetUsersCurrent( txtCutDate.Value);
			cboTipoDocUser.DataValueField = "id_empleado";
			cboTipoDocUser.DataTextField = "nombre";
			cboTipoDocUser.DataBind();
			cboTipoDocUser.Items.Insert(0, new ListItem("» Seleccione","0"));
			cboTipoDocUser.Value = "0";
		}


		private void UpdateData()
		{

			int ID_EMPLEADO;
			int ID_REMITENTE_AREA;
			int ID_REMITENTE_TITULAR;
			int ID_DESTINATARIO_AREA;
			int ID_DESTINATARIO_TITULAR;
			int ID_FIRMA;
			int ID_FOLIO;
			int ID_TIPO_DOCUMENTO_EMPLEADO;
			int ID_TIPO_DOCUMENTO_AREA;
			int ID_REMITENTE_EXTERNO;

			int id = int.Parse(Request.QueryString["id"].ToString());
			if (id == 0) {
				hdnEmployeeId.Value = "0";
				hdnRemAreaId.Value = "0";
				hdnRemNameId.Value = "0";
				hdnDestAreaId.Value = "0";
				hdnDestNameId.Value = "0";
				hdnSignId.Value = "0";
				hdnFolioId.Value = "0";
				hdnTipoDocumentoEmpleado.Value = "0";
				hdnTipoDocumentoArea.Value = "0";
				hdnRemitenteExternoId.Value = "0";
			}
			
			ID_EMPLEADO = int.Parse(cboEmployee.Value);
			if (cboEmployee.Value == "0")
				ID_EMPLEADO = int.Parse(hdnEmployeeId.Value);
			
			ID_REMITENTE_AREA = int.Parse(cboRemitenteArea.Value);
			if (cboRemitenteArea.Value == "0")
				ID_REMITENTE_AREA = hdnRemAreaId.Value.Length == 0 ? 0 : int.Parse(hdnRemAreaId.Value);
			
			ID_REMITENTE_TITULAR = int.Parse(cboRemitenteNombre.Value);
			if (cboRemitenteNombre.Value == "0")
				ID_REMITENTE_TITULAR  = hdnRemNameId.Value.Length == 0 ? 0 : int.Parse(hdnRemNameId.Value);

			ID_DESTINATARIO_AREA = int.Parse(cboDestinatarioArea.Value);
			if (cboDestinatarioArea.Value == "0")
				ID_DESTINATARIO_AREA  = hdnDestAreaId.Value.Length == 0 ? 0 : int.Parse(hdnDestAreaId.Value);

			ID_DESTINATARIO_TITULAR = int.Parse(cboDestinatarioNombre.Value);
			if (cboDestinatarioNombre.Value == "0")
				ID_DESTINATARIO_TITULAR = hdnDestNameId.Value.Length == 0 ? 0 : int.Parse(hdnDestNameId.Value);

			ID_FIRMA = int.Parse(cboSign.Value);
			if (cboSign.Value == "0")
				ID_FIRMA = hdnSignId.Value.Length == 0 ? 0 : int.Parse(hdnSignId.Value);

			ID_FOLIO = int.Parse(cboFolioArea.Value);
			if (cboFolioArea.Value == "0")
				ID_FOLIO = hdnFolioId.Value.Length == 0 ? 0 :  int.Parse(hdnFolioId.Value);

			ID_TIPO_DOCUMENTO_EMPLEADO = int.Parse(cboTipoDocUser.Value);
			if (cboTipoDocUser.Value == "0")
				ID_TIPO_DOCUMENTO_EMPLEADO = hdnTipoDocumentoEmpleado.Value.Length == 0 ? 0 :  int.Parse(hdnTipoDocumentoEmpleado.Value);

			ID_TIPO_DOCUMENTO_AREA = int.Parse(cboTipoDocArea.Value);
			if (cboTipoDocArea.Value == "0")
				ID_TIPO_DOCUMENTO_AREA = hdnTipoDocumentoArea.Value.Length == 0 ? 0 : int.Parse(hdnTipoDocumentoArea.Value);
			
			ID_REMITENTE_EXTERNO = int.Parse(cboRemExtUser.Value);
			if (cboRemExtUser.Value == "0")
				ID_REMITENTE_EXTERNO = hdnRemitenteExternoId.Value.Length == 0 ? 0 : int.Parse(hdnRemitenteExternoId.Value);


			string CAMBIA_ESTATUS = chkCambiaStatus.Checked == true ? "Si" : "No";
			string SEGUIMIENTO = chkSeguimiento.Checked == true ? "Si" : "No";;
			string TIPO_DOCUMENTO = " ";
			string REMITENTE_EXTERNO = chkRemitenteExterno.Checked == true ? "Si" : "No";
			string TIPO_USUARIO = "S";
			string REMITENTE_EXT_CATALOGO = chkRemExtCat.Checked == true ? "Si" : "No";
			string TIPO_DOCUMENTO_CATALOGO = chkTipoDocumento.Checked == true ? "Si" : "No";
			string REM_INCLUYE_OPER = chkRemIncluyeOper.Checked == true ? "Si" : "No";
			string TUR_ARBOL = chkTurArbol.Checked == true ? "Si" : "No";
			string CONFIRMA_CONCLUIR = chkConfConcluir.Checked == true ? "Si" : "No";
			string CONCLUIR_ACUSE_CCPARA = chkConcluirAcuseCcpara.Checked == true ? "Si" : "No";
			string RESPONDER_CASCADA = chkResponderCascada.Checked == true ? "Si" : "No";

			string DOCUMENTO_TIPO = chkDocumentoTipo.Checked == true ? "Emp" : "Area";
			string ELIMINADO = "0";
			string FECHA_INICIO = txtCutDate.Value;
			string FECHA_FIN = "31/12/2049";
			
			if (id == 0 )
			{
				Regla.Create(
					CAMBIA_ESTATUS
					,SEGUIMIENTO
					,TIPO_DOCUMENTO
					,REMITENTE_EXTERNO
					,TIPO_USUARIO
					,REMITENTE_EXT_CATALOGO
					,TIPO_DOCUMENTO_CATALOGO
					,REM_INCLUYE_OPER
					,TUR_ARBOL
					,CONFIRMA_CONCLUIR
					,CONCLUIR_ACUSE_CCPARA
					,RESPONDER_CASCADA
					,ID_EMPLEADO
					,ID_REMITENTE_AREA
					,ID_REMITENTE_TITULAR
					,ID_DESTINATARIO_AREA
					,ID_DESTINATARIO_TITULAR
					,ID_FIRMA
					,ID_FOLIO
					,ID_TIPO_DOCUMENTO_EMPLEADO
					,ID_TIPO_DOCUMENTO_AREA
					,ID_REMITENTE_EXTERNO
					,DOCUMENTO_TIPO
					,ELIMINADO
					,FECHA_INICIO
					,FECHA_FIN
                    , Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()
                    );

			}
			else
			{
				Regla.Update(id
					,CAMBIA_ESTATUS
					,SEGUIMIENTO
					,TIPO_DOCUMENTO
					,REMITENTE_EXTERNO
					,TIPO_USUARIO
					,REMITENTE_EXT_CATALOGO
					,TIPO_DOCUMENTO_CATALOGO
					,REM_INCLUYE_OPER
					,TUR_ARBOL
					,CONFIRMA_CONCLUIR
					,CONCLUIR_ACUSE_CCPARA
					,RESPONDER_CASCADA
					,ID_EMPLEADO
					,ID_REMITENTE_AREA
					,ID_REMITENTE_TITULAR
					,ID_DESTINATARIO_AREA
					,ID_DESTINATARIO_TITULAR
					,ID_FIRMA
					,ID_FOLIO
					,ID_TIPO_DOCUMENTO_EMPLEADO
					,ID_TIPO_DOCUMENTO_AREA
					,ID_REMITENTE_EXTERNO
					,DOCUMENTO_TIPO
					,ELIMINADO
					,FECHA_INICIO
					,FECHA_FIN
                    , Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			}

		}

		private void RemoveData()
		{
			Regla.Remove(int.Parse(Request.QueryString["id"].ToString()), txtCutDate.Value, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
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
			this.chkDocumentoTipo.ServerChange += new System.EventHandler(this.chkDocumentoTipo_ServerChange);
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void chkDocumentoTipo_ServerChange(object sender, System.EventArgs e)
		{
		
		}

	}
}
