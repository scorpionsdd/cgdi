<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<html>
<title>Expandable Columns</title>
<style>
  hr			{height:2px;color:black;}
  .StdText		{font-family:verdana;font-size:9pt;font-weight:bold;}
  .StdTextBox	{font-family:verdana;font-size:9pt;border:solid 1px black;filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2, OffY=2, Color='gray', Positive='true')}
  .Shadow		{filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2, OffY=2, Color='gray', Positive='true')}
  .Link			{font-weight:bold;}
</style> 


<script runat="server">
public void Page_Load(Object sender, EventArgs e)
{
	if (!IsPostBack)
		ViewState["HasExtraColumns"] = false;
	else
	{
		String tmp = "";
		tmp = Page.Request.Form["__EVENTTARGET"].ToString();
		if (tmp != null )
		{
			tmp = tmp.Substring(0, Math.Min("grid:_ctl1".Length, tmp.Length));
			if (!tmp.StartsWith("grid:_ctl") || tmp=="grid:_ctl1")
				AdjustDynamicColumns();
		}
	}
}


public void OnLoadData(Object sender, EventArgs e)
{
	grid.DataSource = CreateDataSource();
	grid.DataBind();
}


public void OnPostBack(Object sender, EventArgs e)
{
}


public void HandleCommands(Object sender, DataGridCommandEventArgs e)
{
	if (e.CommandName == "ExpandColumns")
	{
		if (Convert.ToBoolean(ViewState["HasExtraColumns"]))
			ViewState["HasExtraColumns"] = false;
		else
		{
			AddExtraColumns();
			ViewState["HasExtraColumns"] = true;
		}
	}

	grid.DataSource = CreateDataSource();
	grid.DataBind();
}


/////////////////////////////////////////////////////////////////////////
// Private Members
// 

protected void AdjustDynamicColumns()
{
	if (Convert.ToBoolean(ViewState["HasExtraColumns"]))
	{
		AddExtraColumns();
		grid.DataSource = CreateDataSource();
		grid.DataBind();	
	}
}

protected DataTable CreateDataSource()
{
	SqlDataAdapter da = new SqlDataAdapter(txtCommand.Text, txtConn.Text);
	DataSet ds = new DataSet();
	da.Fill(ds, "MyTable");
	return ds.Tables["MyTable"];
}

protected void AddExtraColumns()
{
	AppendColumns();
	ButtonColumn bc = (ButtonColumn) grid.Columns[grid.Columns.Count-1];
	bc.Text = "7";
}


protected void AppendColumns()
{
	BoundColumn bc;
	
	bc = new BoundColumn();
	bc.DataField = "country";
	bc.HeaderText = "Country";
	grid.Columns.AddAt(grid.Columns.Count-1, bc);

	bc = new BoundColumn();
	bc.DataField = "title";
	bc.HeaderText = "Position";
	grid.Columns.AddAt(grid.Columns.Count-1, bc);
}

</script>


<body bgcolor="ivory" style="font-family:arial;font-size:9pt">

<!-- ASP.NET topbar -->
<h2>Expandable Columns</h2>
<form runat=server>

  <table>
  <tr>
  <td><asp:label runat="server"  text="Connection String" cssclass="StdText" /></td>
  <td><asp:textbox runat="server"  id="txtConn"
	Enabled="false"
 	cssclass="StdTextBox"
	width="500px"
	text="DATABASE=Northwind;SERVER=localhost;INTEGRATED SECURITY=sspi;" /></td></tr>    

  <tr>
  <td><asp:label runat="server"  text="Command Text" cssclass="StdText"/></td>
  <td><asp:textbox runat="server"  id="txtCommand" 
        Enabled="false"
	width="500px"
 	cssclass="StdTextBox"
	text="SELECT * FROM Employees" /></td></tr></table>    

    <br>
    <asp:linkbutton runat="server" id="btnLoad" text="Go get data..." onclick="OnLoadData" />

    <hr>
    <asp:linkbutton runat="server" text="Post-back..." onclick="OnPostBack" cssclass="link" />

    <hr>
    <asp:label runat="server"  id="lblOutput" />

    <asp:DataGrid id="grid" runat="server" 
		AutoGenerateColumns="false"
		CssClass="Shadow" BackColor="white"
		AllowSorting=true
		CellPadding="2" CellSpacing="2" GridLines="none" 
		BorderStyle="solid" BorderColor="black" BorderWidth="1"
		font-size="x-small" font-names="verdana"
		OnItemCommand="HandleCommands">

		<AlternatingItemStyle BackColor="palegoldenrod" />
		<ItemStyle BackColor="beige" />
		<HeaderStyle ForeColor="white" BackColor="brown" Font-Bold="true" />

		<columns>
			<asp:BoundColumn runat="server" DataField="employeeid" sortexpression="id" HeaderText="ID" />
			<asp:BoundColumn runat="server" DataField="firstname" HeaderText="First Name" />
			<asp:BoundColumn runat="server" DataField="lastname" sortexpression="id"  HeaderText="Last Name" />
			<asp:ButtonColumn runat="server" Text="8" CommandName="ExpandColumns">
				<itemstyle backcolor="lightblue" font-size="12pt" font-bold="true" font-name="webdings" />
					</asp:ButtonColumn>

 		</columns>
     </asp:DataGrid>

</form>

</body>
</html>
