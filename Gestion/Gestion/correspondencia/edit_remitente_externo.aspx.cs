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
	public class edit_remitente_externo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgExternalSender;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.TextBox tbDependencia;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.TextBox txtSenderID;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
	
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
				string sForm = Request.QueryString["formname"];
				btnClose.Attributes.Add("onClick", "window.opener.setSender('" + sForm + "', document.edit_remitente_externo.txtSenderID.value);");
				//btnAdd.Attributes.Add("onClick","ExecuteForm()");

				dgBind();
			}
		}

		public void dgBind()
		{
			DataSet ds = ExternalSender.GetRecords(int.Parse(Session["uid"].ToString()));
			dgExternalSender.DataSource = ds;
			dgExternalSender.DataBind();

		}

		public void dgExternalSender_Edit(Object sender, DataGridCommandEventArgs e) 
		{
			// Set the current item to edit mode
			dgExternalSender.EditItemIndex = e.Item.ItemIndex;
			// Refresh the grid
			dgBind();
		}

		public void dgExternalSender_ItemBound(Object sender, DataGridItemEventArgs e) 
		{
			//
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{

				// Retrieve the Label control in the current DataListItem.
				/*
				Label DependenciaLabel = (Label)e.Item.FindControl("lblDependencia");

				if (DependenciaLabel.Text == "WEG México S.A. de C.V." )
				{
					e.Item.BackColor = System.Drawing.Color.Yellow;
					e.Item.Font.Bold = true;
				}
				*/
			}
		}

		public void dgExternalSender_Update(Object sender, DataGridCommandEventArgs e) 
		{
			int nId = Convert.ToInt32(dgExternalSender.DataKeys[(int)e.Item.ItemIndex]);

			string  sValue = ((TextBox)e.Item.FindControl("txtDependencia")).Text;
			ExternalSender.UpdateRecords(nId, sValue);
			dgExternalSender.EditItemIndex = -1;
			
			// Refresh the grid
			dgBind();
		} 

		public void dgExternalSender_Cancel(Object sender, DataGridCommandEventArgs e) 
		{
			// Reset the edit mode for the current item
			dgExternalSender.EditItemIndex = -1;
			// Refresh the grid
			dgBind();
		} 

		public void dgExternalSender_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			ExternalSender.DeleteRecords(Convert.ToInt32(dgExternalSender.DataKeys[(int)e.Item.ItemIndex]));
			// Reset the edit mode for the current item
			dgExternalSender.EditItemIndex = -1;
			txtSenderID.Text = "-1";
			Session["rxID"]  = "-1";
			// Refresh the grid
			dgBind();
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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (tbDependencia.Text != "")
			{
				int nID = ExternalSender.CreateRecords(tbDependencia.Text, int.Parse(Session["uid"].ToString()));
				dgExternalSender.EditItemIndex = -1;
				txtSenderID.Text = nID.ToString();
				Session["rxID"] = nID.ToString();
				tbDependencia.Text = "";
				dgBind();
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			
		}

		public string senderID
		{
			get { return txtSenderID.Text; }
			set { txtSenderID.Text = value;}
		}

	}
}
