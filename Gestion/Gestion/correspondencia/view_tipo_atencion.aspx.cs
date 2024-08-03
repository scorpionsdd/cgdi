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

using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for view_tipo_atencion.
	/// </summary>
	public class view_tipo_atencion : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBoxTipoAtencion;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button CerrarButton;
		protected System.Web.UI.WebControls.DataGrid grdItems;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (Session["rol"] != null) 
			{
				if (!IsPostBack)
				{
					LoadGrid();
				}
			}
		}

		private void LoadGrid()
		{
			this.grdItems.DataSource = TipoAtencion.GetTipoAtencion("V");
			this.grdItems.DataBind();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (TextBoxTipoAtencion.Text != "")
			{
				int gnId = TipoAtencion.Create(TextBoxTipoAtencion.Text);
				grdItems.EditItemIndex = -1;
				TextBoxTipoAtencion.Text = "";
				LoadGrid();
			}
		}

		public void grdItems_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			grdItems.EditItemIndex = e.Item.ItemIndex;
			// Refresh the grid
			LoadGrid();
		}

		public void grdItems_Update(Object sender, DataGridCommandEventArgs e) 
		{
			int nId = Convert.ToInt32(grdItems.DataKeys[(int)e.Item.ItemIndex]);
			string  sValue = ((TextBox)e.Item.FindControl("txtTipoAtencion")).Text;

			TipoAtencion.Update(nId, sValue);
			grdItems.EditItemIndex = -1;
			
			// Refresh the grid
			LoadGrid();
		} 

		public void grdItems_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			grdItems.EditItemIndex = -1;
			// Refresh the grid
			LoadGrid();
		} 

		public void grdItems_Delete(Object sender, DataGridCommandEventArgs e) 
		{
			TipoAtencion.Remove(Convert.ToInt32(grdItems.DataKeys[(int)e.Item.ItemIndex]));
			// Reset the edit mode for the current item
			grdItems.EditItemIndex = -1;
			// Refresh the grid
			
			LoadGrid();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
        #endregion

    }
}
