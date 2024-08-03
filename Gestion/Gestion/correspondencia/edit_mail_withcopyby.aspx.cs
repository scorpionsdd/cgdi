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
	/// Summary description for insert_mail_withcopyby.
	/// </summary>
	public class insert_mail_withcopyby : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox lstWithCopyFor;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.DataGrid dgWithCopyFor;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtFilter;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtSearch;
	
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
				gnDocumentId = Convert.ToInt32(Request.QueryString["id"]);
				gsAction = Request.QueryString["action"].ToString();
				gsFilter = Request.QueryString["filter"].ToString();

				txtDate.Text = DateTime.Now.ToShortDateString();

				DropBind();
				LstBind();
			}

		}


		// Lista los usuarios seleccionados
		private void LstBind()
		{
			DataSet ds = WithCopyFor.GetWithCopyFor(gnDocumentId);
			dgWithCopyFor.DataSource = ds;
			dgWithCopyFor.DataBind();
			
		}

		// Lista los usuarios disponibles
		private void DropBind()
		{
			DataSet ds = GetDisponibility();
			lstWithCopyFor.DataSource = ds;
			lstWithCopyFor.DataValueField = "id_empleado";
			lstWithCopyFor.DataTextField = "destinatario";
			lstWithCopyFor.DataBind();
		}


		public DataSet GetDisponibility()
		{
			DataSet ds = WithCopyFor.GetWithCopyForDisponibility(gnDocumentId);
			return ds;

		}

		public void dgWithCopyFor_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgWithCopyFor.EditItemIndex = e.Item.ItemIndex;

			// Refresh the grid
			LstBind();
		}

		public void dgWithCopyFor_Update(Object sender, DataGridCommandEventArgs e) 
		{

			int nId = Convert.ToInt32(dgWithCopyFor.DataKeys[(int)e.Item.ItemIndex]);

			int nValue = Convert.ToInt32( ( (DropDownList)e.Item.FindControl("editDestinatario")).SelectedItem.Value );

			WithCopyFor.WithCopyForUpdate(nId, nValue);
			dgWithCopyFor.EditItemIndex = -1;
			
			// Refresh the grid
			DropBind();
			LstBind();
		} 

		public void dgWithCopyFor_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			dgWithCopyFor.EditItemIndex = -1;

			// Refresh the grid
			LstBind();
		} 

		public void dgWithCopyFor_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			int nId = Convert.ToInt32(dgWithCopyFor.DataKeys[(int)e.Item.ItemIndex]);

			WithCopyFor.WithCopyForDelete(nId);

			// Reset the edit mode for the current item
			dgWithCopyFor.EditItemIndex = -1;

			// Refresh the grid
			DropBind();
			LstBind();
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			StringBuilder sSelected = new StringBuilder();

			if (lstWithCopyFor.SelectedIndex > -1)
			{
				for (int i=0; i < lstWithCopyFor.Items.Count; i++)
				{
					if (lstWithCopyFor.Items[i].Selected)
						sSelected.Append(lstWithCopyFor.Items[i].Value + ",");
				}
				sSelected.Remove(sSelected.Length-1,1);
				int nVal = WithCopyFor.WithCopyForInsert(gnDocumentId ,sSelected.ToString());
				DropBind();
				LstBind();
			}

		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string sSearch = txtSearch.Text;
			if (sSearch != "")
			{
				// lower(sof_empleados.nombre) like '" + sSearch + "' and sof_areas.id_area = sof_empleados.id_area  And order by apellidonombre");
				sSearch = "%" + sSearch.ToLower() + "%";
				DataSet ds = Users.GetUsers(" lower(sof_empleados.nombre) like '" + sSearch + "' and sof_empleados.id_area = sof_areas.id_area and sof_empleados.fecha_inicio <= '" + txtDate.Text + "' And sof_empleados.fecha_fin >= '" + txtDate.Text + "' order by apellidonombre");
				lstWithCopyFor.DataSource = ds;
				lstWithCopyFor.DataValueField = "Id_Empleado";
				lstWithCopyFor.DataTextField = "ApellidoNombre";
				lstWithCopyFor.DataBind();
			}
		}

		public int 	gnDocumentId
		{
			get { return Convert.ToInt32(txtDocumentId.Text);}
			set {txtDocumentId.Text = value.ToString();}
		}
		public string	gsAction
		{
			get {return txtAction.Text;}
			set {txtAction.Text = value;}
		}
		public string gsFilter
		{
			get {return txtFilter.Text;}
			set {txtFilter.Text = value;}
		}
	}
}
