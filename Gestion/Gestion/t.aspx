<%@ Page language="c#" Codebehind="t.aspx.cs" AutoEventWireup="false" Inherits="Gestion.t" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>t</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="t" method="post" runat="server">
			Fecha de la Consulta:
			<asp:TextBox id="txtDate" runat="server"></asp:TextBox>
			<asp:Button id="btnQuery" runat="server" Text="Consulta"></asp:Button>
			<asp:DataGrid id="dgUsers" runat="server" Width="832px" AutoGenerateColumns="False" OnDeleteCommand="dgUsers_Delete"
				DataKeyField="login">
				<Columns>
					<asp:BoundColumn DataField="login" HeaderText="Login"></asp:BoundColumn>
					<asp:BoundColumn DataField="id_empleado" HeaderText="Id"></asp:BoundColumn>
					<asp:BoundColumn DataField="clave_empleado" HeaderText="Clave"></asp:BoundColumn>
					<asp:ButtonColumn DataTextField="nombre" HeaderText="Nombre" CommandName="Delete"></asp:ButtonColumn>
					<asp:BoundColumn DataField="clave_Area" HeaderText="Clave Area"></asp:BoundColumn>
					<asp:BoundColumn DataField="Area" HeaderText="Area"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
