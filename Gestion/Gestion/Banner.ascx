<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System" %>
<%@ Control Language="C#" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script runat="server">
	private void LogOff_Click(object sender, System.EventArgs e)
	{
        Response.Redirect("Default.aspx?id_usuario=");
	}
</script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td width="8" height="27"><IMG height="27" src="images/spacer.gif" width="8"></td>
		<td vAlign="center" align="right" width="76" rowSpan="2">
			<IMG height="40" src="images/banobras.gif" width="76">
		</td>
		<td vAlign="center" align="middle" width="100%" rowSpan="2">
			<IMG height="40" src="images/collage.jpg" width="625">
		</td>
		<td width="100"><IMG height="1" src="images/spacer.gif" width="100"></td>
	</tr>
	<tr>
		<td width="8">&nbsp;</td>
		<td vAlign="bottom">
		<td vAlign="center" align="middle">
			<asp:linkbutton id="LogOff" runat="server" ForeColor="Black" Visible="false" CausesValidation="False" Font-Underline="True" Font-Bold="True" OnClick="LogOff_Click">Salir</asp:linkbutton></td>
		<td vAlign="center" align="middle"><b> <A href="help_learning/help.pdf" target="_blank"><font style="FONT-SIZE: 10px; COLOR: #003300; FONT-FAMILY: Verdana,Arial,Helvetica,sans-serif; TEXT-DECORATION: underline">
						Documentación</font></A></b>
		</td>
	</tr>
</table>
