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
	public class edit_mail_addressee : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.DataGrid dgAddressee;
		protected System.Web.UI.WebControls.ListBox lstAddressee;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.TextBox txtSearch;
		protected System.Web.UI.WebControls.Button btnSearch;

		public DataSet dsTipoTramite;
		public DataSet dsArea;
		
		protected System.Web.UI.WebControls.DropDownList dropArea;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtDocumentId;
		protected System.Web.UI.WebControls.TextBox txtClose;
		protected System.Web.UI.WebControls.TextBox txtFilter;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.Button btnAreas;
		protected System.Web.UI.WebControls.TextBox txtInstruction;

	
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
				gsAction = Request.QueryString["action"];
				gsFilter = Request.QueryString["filter"];
				txtDate.Text = DateTime.Now.ToShortDateString();

				dropAreaBind();
				lstSelectBind();
				lstSelectedBind();
				TipoTramiteBind();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dropArea.SelectedIndexChanged += new System.EventHandler(this.dropArea_SelectedIndexChanged);
			this.btnAreas.Click += new System.EventHandler(this.btnAreas_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.dgAddressee.SelectedIndexChanged += new System.EventHandler(this.dgAddressee_SelectedIndexChanged);
			this.ID = "edit_mail_addressee";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			StringBuilder sSelected = new StringBuilder();

			if (lstAddressee.SelectedIndex > -1)
			{
				for (int i=0; i < lstAddressee.Items.Count; i++)
				{
					if (lstAddressee.Items[i].Selected)
						sSelected.Append(lstAddressee.Items[i].Value + ",");
				}
				sSelected.Remove(sSelected.Length-1,1);
				int nVal = Adressee.AdresseeInsert(gnDocumentId, sSelected.ToString(), txtInstruction.Text);
				lstSelectBind();
				lstSelectedBind();

			}
		}

		private void lstSelectedBind()
		{
			dgAddressee.DataSource = Adressee.GetAdresseeByDocument(gnDocumentId);
			dgAddressee.DataBind();
		}

		private void lstSelectBind()
		{
			string sArea = dropArea.SelectedItem.Value;
			lstAddressee.DataSource = GetAdresseeDisponibility(sArea, Session["uid"].ToString(), txtDate.Text);
			lstAddressee.DataValueField = "id_empleado";
			lstAddressee.DataTextField	= "destinatario";
			lstAddressee.DataBind();
		}

		public DataSet GetAdresseeDisponibility(string sAreaId, string sUserID, string sDate)
		{
			string sTmp = Users.GetAreaKey(int.Parse(sAreaId) ).TrimEnd('0');

			string	sWhere = " id_empleado not in (select id_destinatario from sof_documento_turnar Where documento_id = " + gnDocumentId + " and eliminado = '0')" +
				" And sof_areas.id_area = sof_empleados.id_area " +
				" And sof_empleados.fecha_inicio <= '" + sDate + "' And sof_empleados.fecha_fin >= '" + sDate +"' ";

			if ( participants.getTurnarArbol(sUserID) == "Si" )
			{
				if (sTmp.Length == 1)
					sTmp += "0";

				sWhere += " and sof_areas.cve_area like '" + sTmp + "%' and sof_empleados.categoria > '12C' and sof_empleados.eliminado = '0'";
			}
			else
			{
				sTmp += sTmp.Length == 1 ? "0%" : "%";
				sWhere += " and sof_areas.cve_area like '" + sTmp + "' and sof_empleados.categoria > '12C' and sof_empleados.eliminado = '0'";
			}

			DataSet ds = Adressee.GetAdresseeDisponibility(sWhere, "apellidonombre");
			return ds;
		}

		public int GetTipoTramiteIndex(string sTipoTramiteId)
		{
			int i=0;
			foreach(DataRow dr in dsTipoTramite.Tables[0].Rows)
			{
				if (dr["tipo_tramite_id"].ToString() != sTipoTramiteId)
					i++;
				else
					break;
			}
			return i;
		}

		public int GetAreaIndex(string sAreaId)
		{
			int i=0;
			foreach(DataRow dr in dsArea.Tables[0].Rows)
			{
				if (dr["id_area"].ToString() != sAreaId)
					i++;
				else
					break;
			}
			return i;
		}


		public void dgAddressee_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgAddressee.EditItemIndex = e.Item.ItemIndex;
			EnableDisable (false);
			// Refresh the grid
			lstSelectedBind();

		}

		public void dgAddressee_Update(Object sender, DataGridCommandEventArgs e) 
		{

			int nId = Convert.ToInt32(dgAddressee.DataKeys[(int)e.Item.ItemIndex]);
			
			string  sAreaId = ((DropDownList)e.Item.FindControl("editArea")).SelectedItem.Value;
			string  sInstruccion = ((TextBox)e.Item.FindControl("txtInstruccion")).Text;
			int nTipoTramiteId = Convert.ToInt32( ( (DropDownList)e.Item.FindControl("editTipoTramite")).SelectedItem.Value );

			Adressee.AdresseeUpdate(nId, sAreaId, sInstruccion, nTipoTramiteId);
			dgAddressee.EditItemIndex = -1;

			EnableDisable (true);
			
			// Refresh the grid
			lstSelectBind();
			lstSelectedBind();
		} 

		public void dgAddressee_Cancel(Object sender, DataGridCommandEventArgs e)
		{
			// Reset the edit mode for the current item
			dgAddressee.EditItemIndex = -1;
			EnableDisable (true);

			// Refresh the grid
			lstSelectedBind();
		} 

		public void dgAddressee_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			int nId = Convert.ToInt32(dgAddressee.DataKeys[(int)e.Item.ItemIndex]);
			Adressee.AdresseeDelete(nId);
			
			// Reset the edit mode for the current item
			dgAddressee.EditItemIndex = -1;

			// Refresh the grid
			lstSelectBind();
			lstSelectedBind();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string sSearch = txtSearch.Text;
			if (sSearch != "")
			{
				sSearch = sSearch.Replace("*","").ToLower();
				sSearch = "%" + sSearch + "%";
				lstAddressee.DataSource = Users.GetUsers(" lower(sof_empleados.nombre) like '" + sSearch + "' and sof_empleados.id_area = sof_areas.id_area and sof_empleados.fecha_inicio <= '" + txtDate.Text + "' And sof_empleados.fecha_fin >= '" + txtDate.Text + "' order by apellidonombre");
				lstAddressee.DataValueField = "id_empleado";
				lstAddressee.DataTextField = "apellidonombre";
				lstAddressee.DataBind();
			}
		} 

		public DataSet TipoTramiteBind()
		{
			DataSet ds = TipoTramite.GetRecords();
			dsTipoTramite = ds;
			return ds;
		}

		private void dropAreaBind()
		{
			string sDestinatarioId =  Users.GetAreaParameter(int.Parse(Session["uid"].ToString()));

			dropArea.DataSource = Users.GetAreaByDate(txtDate.Text);
			dropArea.DataValueField = "id_area";
			dropArea.DataTextField = "area";
			dropArea.DataBind();

			if (dropArea.Items.FindByValue(sDestinatarioId) != null)
				dropArea.Items.FindByValue( sDestinatarioId ).Selected = true;
		}

		public DataSet editAreaBind()
		{
			dsArea = Users.GetArea( DateTime.Now.ToShortDateString());
			return dsArea;
		}

		private void dropArea_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lstSelectBind();		
		}

		private void EnableDisable(bool bEd)
		{
			txtSearch.Enabled = bEd;
			dropArea.Enabled = bEd;
			btnSearch.Enabled = bEd;
			lstAddressee.Enabled = bEd;
			btnSave.Enabled = bEd;
		}

		private void dgAddressee_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnAreas_Click(object sender, System.EventArgs e)
		{
			dropAreaBind();
			lstSelectBind();
		}

		public string gsAction
		{
			get {return txtAction.Text;}
			set { txtAction.Text = value;}
		}
		
		public int gnDocumentId
		{
			get {return Convert.ToInt32(txtDocumentId.Text);}
			set { txtDocumentId.Text = value.ToString();}
		}

		public string gsClosePage
		{
			get {return txtClose.Text;}
			set { txtClose.Text = value;}
		}

		public string gsFilter
		{
			get {return txtFilter.Text;}
			set { txtFilter.Text = value;}
		}

	}
}
