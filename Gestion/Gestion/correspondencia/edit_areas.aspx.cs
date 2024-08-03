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

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for edit_areas.
	/// </summary>
	public class edit_areas : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblArea;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAreaId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnPuestoId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnTipoEmpleado;
		protected System.Web.UI.HtmlControls.HtmlInputText txtArea;
		protected System.Web.UI.HtmlControls.HtmlInputText txtClaveArea;
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
			//RGRG > this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
		}

		private void LoadData()
		{
			DataSet ds = Areas.GetAreaById(int.Parse(Request.QueryString["id"].ToString()));
			if (ds.Tables[0].Rows.Count > 0 ) {
				txtArea.Value	= ds.Tables[0].Rows[0]["area"].ToString();
				txtClaveArea.Value	= ds.Tables[0].Rows[0]["cve_area"].ToString();
			}
		}

		private void UpdateData()
		{

			if (int.Parse(Request.QueryString["id"].ToString()) == 0 ) {
				Areas.Create(txtClaveArea.Value, txtArea.Value, txtCutDate.Value, "31-12-2049");
			} else {
				Areas.Update(int.Parse(Request.QueryString["id"].ToString()), txtClaveArea.Value, txtArea.Value, txtCutDate.Value);
			}

		}

		private void RemoveData()
		{
			Areas.Remove(int.Parse(Request.QueryString["id"].ToString()), txtCutDate.Value);
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
