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
using System.Data.OracleClient;
//using System.Data.OracleClient;
using System.Text;
using Gestion.BusinessLogicLayer;

namespace Gestion.Correspondencia
{
	/// <summary>
	/// Summary description for CreateMail.
	/// </summary>
	public class External_Explorer : System.Web.UI.Page
	{
		protected DataGrid grdItems;
		protected HtmlInputText txtFilter;
		protected HtmlInputCheckBox chkAllKeywords;
		protected HtmlSelect cboStatus;
		protected HtmlInputText txtDateFrom;
		protected HtmlInputText txtDateTo;
		protected HtmlSelect cboArea;

		protected string sHtmlSender = string.Empty;
		protected HtmlSelect cboSender;

		private string currRemitenteId;
		protected Label lblTitle;
		private int j = 0;


		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (! Page.IsPostBack)
				SetDefaultValues(Request.QueryString["sendertype"]);
			else
				LoadGrid();
			
		}

		private void SetDefaultValues(string senderType) 
		{
			if (senderType == "E") 
			{
				lblTitle.Text = "Remitentes Externos | Consultas " ;
				OracleDataReader dr = ExternalSender.GetRecordsDr(int.Parse(Session["uid"].ToString()));
				cboSender.DataSource = dr;
				cboSender.DataTextField = "dependencia";
				cboSender.DataValueField = "remitente_externo_id";
				cboSender.DataBind();
				cboSender.Items.Insert(0, new ListItem("( Todos )",String.Empty));
			} 
			else 
			{
				lblTitle.Text = "Remitentes Internos | Consultas " ;
				cboSender.DataSource = Users.GetArea( int.Parse(Session["uid"].ToString()));
				cboSender.DataTextField = "area";
				cboSender.DataValueField = "area_id";
				cboSender.DataBind();
				cboSender.Items.Insert(0, new ListItem("( Todos )",String.Empty));
			}
			if (txtDateFrom.Value.Trim()  == String.Empty)
				txtDateFrom.Value = "01/01/" + DateTime.Now.Year.ToString();

			if (txtDateTo.Value.Trim()  == String.Empty)
				txtDateTo.Value = DateTime.Now.ToShortDateString();

			Execute();
		}

		private void LoadGrid()
		{
			Execute();
		}

		private void Execute()
		{
			grdItems.DataSource = Documents.GetDocuments(int.Parse(Session["uid"].ToString()), cboSender.Value.ToString(), cboStatus.Value.ToString(), Request.QueryString["sendertype"], txtDateFrom.Value.Trim(), txtDateTo.Value.Trim());
			grdItems.DataBind();

		}

		protected void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataGridItem row = e.Item;

			if (row.ItemType == ListItemType.Item || row.ItemType == ListItemType.AlternatingItem)
			{
				Label remitenteAreaId = (Label)e.Item.FindControl("lblRemitente_Area_Id");
				Label remitenteName = (Label)e.Item.FindControl("lblRemitenteArea");

				if (currRemitenteId != remitenteAreaId.Text)
				{ 
					//add blank row
					currRemitenteId = remitenteAreaId.Text;
					j++;
					AddRow(row.ItemIndex + j, remitenteAreaId.Text + " / " + remitenteName.Text);
				}
			}
		}

		private void AddRow(int index, string sTag)
		{
			//Create bank row
			Table t = (Table)grdItems.Controls[0];
			TableCell blankCell = new TableCell();
			blankCell.BackColor = Color.Tan;
			blankCell.ColumnSpan = grdItems.Columns.Count;
			blankCell.Controls.Add(new LiteralControl(sTag));
			DataGridItem tblRow = new DataGridItem(0,0,ListItemType.Item); 
			tblRow.Cells.Add(blankCell);
			t.Rows.AddAt(index,tblRow);
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
			this.ID = "Explorer_External";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
