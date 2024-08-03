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

namespace Gestion
{
	/// <summary>
	/// Summary description for admin.
	/// </summary>

	public class admin : System.Web.UI.Page
	{
		
		public string administrator = String.Empty;
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			administrator = "NULL";
			Initialize();

			if (!IsPostBack) {
				if (Session["rol"].ToString() == "Administrator") 
				{
					SetDefaultValues();
				}
			} 

		}

		private void Initialize() 
		{
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "NULL");
            ClientScript.RegisterHiddenField("hdnOperationTag", "NULL");
			//session["administrator"] = "Administrator";
			Session["rol"] = "Administrator";
		}

		private void SetDefaultValues() 
		{
			//RGRG> this.RegisterHiddenField("hdnOperationTag", "GENERATE");
            ClientScript.RegisterHiddenField("hdnOperationTag", "GENERATE");
			administrator = "GENERATE";
			
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
