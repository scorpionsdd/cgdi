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
using System.IO;

using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for insert_mail_attach.
	/// </summary>
	public class insert_mail_attach : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.DataGrid dgAttach;
		protected System.Web.UI.HtmlControls.HtmlInputFile UploadedFile;
		protected System.Web.UI.WebControls.Label userMessage;
		protected System.Web.UI.WebControls.TextBox txtAction;
		protected System.Web.UI.WebControls.TextBox txtFilter;
		protected System.Web.UI.WebControls.TextBox txtDocumentID;
		protected System.Web.UI.WebControls.TextBox txtMailType;
		//protected ProgressBar.UploadProgress UploadProgressBar1;


		//const string cnsFilePath = @"C:\inetpub\wwwroot\gestion\user_files\";
		protected System.Web.UI.WebControls.Panel ObjPanel2;

		//private  ProgressBar.FileUpload ObjFileUpload;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			gnDocumentId	= Convert.ToInt32(Request.QueryString["id"]);
			gsAction		= Request.QueryString["action"].ToString();
			gsFilter		= Request.QueryString["filter"].ToString();
			gsMailType		= Request.QueryString["mailtype"].ToString();

			if (! Page.IsPostBack)
				AttachBind();
		}

		private void AttachBind()		{
			DataSet ds = Attached.GetAttach(gnDocumentId, "T");
			dgAttach.DataSource = ds;
			dgAttach.DataBind();
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			if (UploadedFile.ToString() != "")
			{
				HttpFileCollection allFiles = Request.Files;
				HttpPostedFile uploadedFile = allFiles["UploadedFile"];

				FileInfo uploadedFileInfo = new FileInfo(uploadedFile.FileName);
				string fd = AppSettingsReader.GetValue("GeneratedFilesPath") + Users.GetUserKey( Documents.getOwnerDocument(gnDocumentId) );

				if ( ! Directory.Exists(fd) )
					Directory.CreateDirectory(fd);

				string fn = fd + "\\" + uploadedFileInfo.Name;
				uploadedFile.SaveAs(fn);
				
				string sServer = uploadedFileInfo.DirectoryName;
				
				int nVal = Attached.AttachedInsert(gnDocumentId, uploadedFileInfo.Name, sServer, gsMailType);
				AttachBind();

				userMessage.Text = "El ultimo archivo guardado es:  " + uploadedFileInfo.Name;
			}
		}

		public void dgAttach_Delete(Object sender, DataGridCommandEventArgs e) 
		{

			int nId = Convert.ToInt32(dgAttach.DataKeys[(int)e.Item.ItemIndex]);

//			int nValue = Convert.ToInt32( ( (DropDownList)e.Item.FindControl("addressee")).SelectedItem.Value );

			string sFile = 	( (Label)e.Item.FindControl("lblFile")).Text;

			Attached.AttachedDelete(nId);

			FileInfo uploadedFileInfo = new FileInfo(AppSettingsReader.GetValue("GeneratedFilesPath") + Users.GetUserKey(Documents.getOwnerDocument(gnDocumentId)) + "/" + sFile);
			if (uploadedFileInfo.Exists == true)
			{
				uploadedFileInfo.Delete();
			}
			
			// Reset the edit mode for the current item
			dgAttach.EditItemIndex = -1;

			// Refresh the grid
			AttachBind();
		} 

		public string gsAction
		{
			get {return txtAction.Text;}
			set { txtAction.Text = value;}
		}
		
		public int gnDocumentId
		{
			get {return Convert.ToInt32(txtDocumentID.Text);}
			set { txtDocumentID.Text = value.ToString();}
		}

		public string gsFilter
		{
			get {return txtFilter.Text;}
			set { txtFilter.Text = value;}
		}

		public string gsMailType
		{
			get {return txtMailType.Text;}
			set { txtMailType.Text = value;}
		}

	}
}
