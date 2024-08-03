<%@ Page CodeBehind="headerexpanded.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Gestion.Portal.headerExpanded" %>

<HTML>
	<HEAD>
		<title>Banobras - Intranet corporativa</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/BanStyles.css" type="text/css" rel="stylesheet">
		<base target="main">
		<script language="javascript">
			var popUp;
			function openHelp(){

				popUp = window.open('/Gestion/help_learning/help.pdf', 'Ayuda', 'width=800,height=500,left=200,top=250,scrollbars=yes');
				return false;
			}
			
		</script>
	</HEAD>
	<BODY text="navy" bottomMargin="0" bgColor="#cccccc" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="FlowLayout">
		<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="100%" align="center"
			bgColor="white" border="1">
			<tr>
				<td>
					<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td width="100%">
								<table cellSpacing="0" cellPadding="0" width="100%" border="0">
									<tr>
										<td width="195" background="/gestion/portal/images/banobras.gif" height="32" rowSpan="2"><asp:image id="Image5" ImageUrl="/gestion/portal/Images/banobras.gif" runat="server"></asp:image></td>
										<td bgColor="#00c0c0" height="32"><asp:image id="Image1" ImageUrl="/gestion/portal/Images/transp.gif" runat="server" Height="15"
												Width="8"></asp:image>
												<asp:label id="Label1" runat="server" Font-Bold="True" CssClass="pnlBanco">Banco Nacional de Obras y Servicios Públicos S.N.C.</asp:label></td>
										<TD bgColor="#00c0c0" height="32">&nbsp;</TD>
										
										<td align="center" width="65" background="/gestion/portal/images/aguila.gif" bgColor="#00a09c"
											height="32" rowSpan="2"></td>
									</tr>
									<tr>
										<td class="header" style="HEIGHT: 33px" align="left" bgColor="#d6d3ce" colspan="2"><span class="normaltext1" id="participantName" style="FONT-WEIGHT: bold">
												<%=Session["user_name"]%>
												&nbsp;(<%=Session["uid"]%>
												)</span> <A class="header" href="http://www.banobras.gob.mx"</A>
										</td>
									</tr>
									<tr>
										<td vAlign="middle" align="left" width="195" bgColor="#aaaaae"><asp:image id="Image4" ImageUrl="/gestion/portal/images/1px.gif" runat="server" Width="8px"></asp:image></td>
										<td class="header" style="HEIGHT: 33px" align="left" bgColor="#6e8088" valign="middle"><asp:image id="Image2" ImageUrl="/gestion/portal/images/mexico.gif" runat="server" Height="32"
												Width="130px"></asp:image>
										<TD bgColor="#6e8088" height="32" align="right">
											<a id="lnkInicio" class="header2" onclick="parent.frames[0].location.href = '/gestion/portal/header.aspx'; parent.frames[1].location.href = '/gestion/portal/main.aspx';">Inicio</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										</TD>
										
										<td width="65" background="/gestion/portal/Images/hscp.gif" height="32"></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</BODY>
</HTML>
