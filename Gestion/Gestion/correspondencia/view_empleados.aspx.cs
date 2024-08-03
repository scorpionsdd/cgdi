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
	public class view_empleados : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.DataGrid dgFirma;
		protected System.Web.UI.WebControls.DropDownList dropUsers;
		protected System.Web.UI.WebControls.DataGrid grdItems;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCutDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtFilter;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
            Initialize();
			if (Session["rol"] != null) 
			{
				if (Session["rol"].ToString() == "Administrator") 
				{
					if (!IsPostBack) 
					{
						ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Vista de Empleados / Lista Empleados", enuAction.Navegation.GetDescription(), string.Empty, string.Empty, Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString()));
						SetDefaultValues();
					} 
					else 
					{
						ExecuteOperation();
					}	
				}
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
			LoadGrid();
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
				case "GENERATE":
					LoadGrid();
					break;
			}
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
		}

		private void LoadGrid()
		{
			grdItems.DataSource = Users.GetUsersByDate(txtCutDate.Value, txtFilter.Value, Convert.ToString(Session["uid"]), Request.UserHostAddress, Session["sessionId"].ToString(), Session["employeeId"].ToString());
			grdItems.DataBind();
		}


		public void dgFirma_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgFirma.EditItemIndex = e.Item.ItemIndex;
			// Refresh the grid
			//dgBind();
		}


		public void dgFirma_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			dgFirma.EditItemIndex = -1;
			// Refresh the grid
			//dgBind();
		} 

		public void dgFirma_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			firmaUsuario.DeleteRecords(Convert.ToInt32(dgFirma.DataKeys[(int)e.Item.ItemIndex]));
			// Reset the edit mode for the current item
			dgFirma.EditItemIndex = -1;
			// Refresh the grid
			//dgBind();
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


		private void dropUsers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int nVal = firmaUsuario.CreateRecords (int.Parse(dropUsers.SelectedItem.Value), int.Parse(Session["uid"].ToString()));
			//dgBind();
		}

	}
}
