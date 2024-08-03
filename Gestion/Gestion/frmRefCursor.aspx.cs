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
//using System.Data.OracleClient;
using System.Data.OracleClient;

namespace Gestion
{
	/// <summary>
	/// Summary description for WebForm2.
	/// </summary>
	public class WebForm2 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.DataGrid dgUsers1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			OracleConnection conn = new OracleConnection("Data Source=cgestion;User ID=cgestion;Password=cgestion");
			conn.Open();
			OracleCommand cmd = conn.CreateCommand();
			cmd.CommandText = "GestionQuery.GetDocumentsByVolante";
			cmd.CommandType = CommandType.StoredProcedure;
            
			cmd.Parameters.Add("sUsersId", null);
			cmd.Parameters.Add("sAreaId", null);
			cmd.Parameters.Add("sStatusVolante", null);
			cmd.Parameters.Add("sDocumentType", null);
			cmd.Parameters.Add("sTextSearch", null);
			cmd.Parameters.Add("sFromDate", null);
			cmd.Parameters.Add("sToDate", null);
			cmd.Parameters.Add("cur_Documents", null);

			cmd.Parameters[0].Value = "10460";
			cmd.Parameters[1].Value = "423";
			cmd.Parameters[2].Value =  "'Tramite'";
			cmd.Parameters[3].Value = "'%'";
			cmd.Parameters[4].Value = "'%'";
			cmd.Parameters[5].Value = "01/01/2007";
			cmd.Parameters[6].Value = "30/10/2007";
			cmd.Parameters[7].Direction = ParameterDirection.Output;


			DataSet ds = new DataSet();
			OracleDataAdapter adapter = new OracleDataAdapter(cmd);
			adapter.Fill(ds);

			dgUsers1.DataSource = ds;
			dgUsers1.DataBind();

			conn.Close();
			

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			TextBox1.Text = TextBox1.Text.Replace("/","-");
		}
	}
}
