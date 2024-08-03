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
using System.Collections.Specialized;


namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for receive_mail.
	/// </summary>
	public class mail_attach_view : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtUserId;
		protected System.Web.UI.WebControls.TextBox txtAnexoId;
		protected System.Web.UI.WebControls.TextBox txtFile;
		protected System.Web.UI.WebControls.TextBox txtMailType;
		protected System.Web.UI.WebControls.DataGrid dgAnexo;

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
				txtUserId.Text	= Session["uid"].ToString();
				txtAnexoId.Text = Request["id"];
				txtMailType.Text = Request["mailtype"].ToString();
			}
			dgAnexoBind();
		}

		private void dgAnexoBind()
		{
			DataSet ds = Attached.GetFileAnexo(gnAnexoId, gsMailType);
			DataRow dr = ds.Tables[0].Rows[0];
			string sUrl = AppSettingsReader.GetValue("URLFilesPath");
			txtFile.Text = sUrl + dr["id_empleado"].ToString() + "/" + dr["archivo"].ToString();

			ds.Dispose();
		}

		public string gsUser
		{
			get {return txtUserId.Text;}
			set {txtUserId.Text = value;}
		}

		public int gnAnexoId
		{
			get {return Convert.ToInt32(txtAnexoId.Text);}
			set {txtAnexoId.Text = value.ToString();}
		}

		public string gsFile
		{
			get {return txtFile.Text;}
			set {txtFile.Text = value;}
		}

		public string gsMailType
		{
			get {return txtMailType.Text;}
			set {txtMailType.Text = value;}
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
