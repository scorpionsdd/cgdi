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
	public class edit_remitente_externo_titular : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.DataGrid dgTitular;
		protected System.Web.UI.WebControls.TextBox tbName;
		protected System.Web.UI.WebControls.TextBox tbPuesto;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.TextBox tbTitulo;

		public edit_remitente_externo sourcepage;
		private int nId = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			InitializeValues();
			if (! Page.IsPostBack)
			{
				dgBind();
			}
		}

		private void InitializeValues()
		{
			if (Request.QueryString["id"] == null)
				nId = int.Parse(Session["rxID"].ToString());
			else
				nId = int.Parse(Request.QueryString["id"].ToString());

		}
		private void dgBind()

		{

			DataSet ds = ExternalSenderTitular.GetRecords( nId );

			dgTitular.DataSource = ds;
			dgTitular.DataBind();
		}


		public void dgTitular_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgTitular.EditItemIndex = e.Item.ItemIndex;

			// Refresh the grid
			dgBind();
		}

		public void dgTitular_Update(Object sender, DataGridCommandEventArgs e) 
		{
			int nId = Convert.ToInt32(dgTitular.DataKeys[(int)e.Item.ItemIndex]);

			string  sName	= ((TextBox)e.Item.FindControl("txtName")).Text;
			string  sPuesto	= ((TextBox)e.Item.FindControl("txtPuesto")).Text;
			string  sTitulo	= ((TextBox)e.Item.FindControl("txtTitulo")).Text;

			ExternalSenderTitular.UpdateRecords(nId, sName, sPuesto, sTitulo );
			dgTitular.EditItemIndex = -1;
			
			// Refresh the grid
			dgBind();
		} 

		public void dgTitular_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			dgTitular.EditItemIndex = -1;
			// Refresh the grid
			dgBind();
		} 

		public void dgTitular_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			ExternalSenderTitular.DeleteRecords(Convert.ToInt32(dgTitular.DataKeys[(int)e.Item.ItemIndex]));
			// Reset the edit mode for the current item
			dgTitular.EditItemIndex = -1;

			// Refresh the grid
			//Session["rxtID"] = "-1";
			dgBind();
		}

		public string getDependencia()
		{
			string sDependencia = ExternalSender.GetDependencia(nId);
			return sDependencia;
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if ( tbName.Text != "")
			{
				int nID = ExternalSenderTitular.CreateRecords(nId, tbName.Text, tbPuesto.Text, tbTitulo.Text);
				dgTitular.EditItemIndex = -1;
				tbName.Text = "";
				tbPuesto.Text = "";
				tbTitulo.Text = "";
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
			this.dgTitular.SelectedIndexChanged += new System.EventHandler(this.dgTitular_SelectedIndexChanged);
			this.ID = "tente_externo_titular";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgTitular_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
