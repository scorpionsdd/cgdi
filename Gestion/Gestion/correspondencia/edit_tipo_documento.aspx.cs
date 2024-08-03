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
	/// Summary description for insert_mail_addressee.
	/// </summary>
	public class edit_tipo_documento : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		public static string sCloseWindow;
		public static string form;
		public static string gnId;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.TextBox tbTipoDocumento;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.DataGrid dgTipoDocumento;
		public string sId = String.Empty;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (! Page.IsPostBack)
			{
				string selected = Request.QueryString["selected"];
				string id		= Request.QueryString["id"];
				form			= Request.QueryString["formname"];
				string postBack = Request.QueryString["postBack"];

				btnClose.Attributes.Add("onClick", "window.opener.SetDocumentType('" + form + "', document.DocumentType.tbTipoDocumento.value, document.DocumentType.txtTipoDocumentoId.value);");
				dgBind();
			}
		}

		private void dgBind()
		{
			DataSet ds = document_type.GetRecords(int.Parse(Session["uid"].ToString()));
			dgTipoDocumento.DataSource = ds;
			dgTipoDocumento.DataBind();
		}


		public void dgTipoDocumento_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgTipoDocumento.EditItemIndex = e.Item.ItemIndex;
			// Refresh the grid
			dgBind();
		}

		public void dgTipoDocumento_Update(Object sender, DataGridCommandEventArgs e) 
		{
			int nId = Convert.ToInt32(dgTipoDocumento.DataKeys[(int)e.Item.ItemIndex]);
			string  sValue = ((TextBox)e.Item.FindControl("txtTipoDocumento")).Text;

			document_type.UpdateRecords(nId, sValue);
			dgTipoDocumento.EditItemIndex = -1;
			
			// Refresh the grid
			dgBind();
		} 

		public void dgTipoDocumento_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			dgTipoDocumento.EditItemIndex = -1;
			// Refresh the grid
			dgBind();
		} 

		public void dgTipoDocumento_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			document_type.DeleteRecords(Convert.ToInt32(dgTipoDocumento.DataKeys[(int)e.Item.ItemIndex]));
			// Reset the edit mode for the current item
			dgTipoDocumento.EditItemIndex = -1;
			// Refresh the grid
			
			dgBind();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (tbTipoDocumento.Text != "")
			{
				int gnId = document_type.CreateRecords(tbTipoDocumento.Text, int.Parse(Session["uid"].ToString()));
				sId = gnId.ToString();
				dgTipoDocumento.EditItemIndex = -1;
				tbTipoDocumento.Text = "";
				dgBind();
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
