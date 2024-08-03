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
	/// Summary description for receive_mail.
	/// </summary>
	public class change_status_ccpara : System.Web.UI.Page
	{
		
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
				ReplyBind(int.Parse(Request.QueryString["documentoid"]), int.Parse(Request.QueryString["turnadoid"]), Request.QueryString["state"], Request.QueryString["datereply"], Request.QueryString["reply"]);
			}
		}

		private void ReplyBind(int nId, int nCcparaId, string sStatus, string sDateReply, string sReply )
		{

			string sDate = DateTime.Now.ToShortDateString();
			string sTmpStatus = sStatus;
			string sTmpReply  = sReply;
			string sTmpDateReply  = sDateReply;

			if ( participants.GetConcluirAcuseCcpara(Session["uid"].ToString()) == "Si" )
			{
				sTmpStatus	  = "3";
				sTmpReply	  = "Acuse de Recibo";
				sTmpDateReply = DateTime.Now.ToShortDateString();
			}

			if (Document_Alternate.Estatus_Ccpara_Verify(nId, nCcparaId, sTmpStatus) == 0)
				Document_Alternate.Estatus_Ccpara_Create(nId, nCcparaId, sTmpStatus, sTmpDateReply, sTmpReply);
			else
				Document_Alternate.Estatus_Ccpara_Update(nId, nCcparaId, sTmpStatus, sTmpDateReply, sTmpReply);

			Document_Alternate.Document_Ccpara_Update(nCcparaId, sTmpStatus);

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
