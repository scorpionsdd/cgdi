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


namespace Gestion
{
	/// <summary>
	/// Summary description for t.
	/// </summary>
	public class t : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DataGrid dgUsers;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (Request["id_usuario"] != null) 
			{
				if (Request["id_usuario"].ToString() == "Todos") 
				{
					if (!Page.IsPostBack) 
					{
						txtDate.Text = DateTime.Now.ToShortDateString();
						BindGridUsers();
					}					
				}
			}
		}

		private void BindGridUsers()
		{
            string sWhere = "sof_empleados.id_area = sof_areas.id_area(+) " +
            "And (sof_empleados.fecha_inicio <= '" + txtDate.Text + "') And (sof_empleados.fecha_fin >= '" + txtDate.Text + "') " +
            "Order by sof_areas.cve_area, sof_empleados.Categoria DESC";

            DataSet ds = Users.GetUsers(sWhere);
			dgUsers.DataSource = ds;
			dgUsers.DataBind();
		}


		public void  dgUsers_Delete(Object sender, DataGridCommandEventArgs e)
		{
			dgUsers.EditItemIndex = -1;
			string  sUserLogin = (dgUsers.DataKeys[e.Item.ItemIndex]).ToString();
			Response.Redirect("/gestion/default.aspx?id_usuario=" + sUserLogin);
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			if (Request["id_usuario"] != null) 
			{
				if (Request["id_usuario"].ToString() == "780260") 
				{
					BindGridUsers();
				}
			}
		}



	}
}
