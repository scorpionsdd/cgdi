<%@ Page language="c#" Codebehind="mail_attach_view.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.mail_attach_view" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Anexos</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/styles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			
			function window_onload() {

				var gsFile = document.all.txtFile.value;
				window.close();
				window.open(gsFile, 'dummy');
				return false;
			}

		</script>
	</HEAD>
	<body onload="window_onload();">
		<form id="mail_attach_view" method="post" runat="server">
			&nbsp;
			<table>
				<tr>
					<td>Procesando
					</td>
				</tr>
			</table>
			<asp:TextBox id="txtUserId" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtAnexoId" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtFile" runat="server" Visible="True"></asp:TextBox>
			<asp:TextBox id="txtMailType" runat="server" Visible="True"></asp:TextBox></form>
	</body>
</HTML>
