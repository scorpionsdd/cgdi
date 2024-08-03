<%@ Page CodeBehind="header.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Gestion.Portal.header" %>
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
			
			function openIntranet(){

				self.close();
				window.close();
				window.open('http://intranet');
				return false;
			}

		</script>
	</HEAD>
	<BODY text="navy" bottomMargin="0" bgColor="#cccccc" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="FlowLayout">
		<div>
			<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" align="center" bgColor="white"
				border="1">
				<tr>
					<td>
						<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="780" border="0">
							<tr>
								<td width="780" colSpan="3">
									<table cellSpacing="0" cellPadding="0" width="780" border="0">
										<tr width="780">
											<td width="195" background="/gestion/portal/images/banobras.gif" colSpan="4" height="32"
												rowSpan="2"><asp:image id="Image5" border="0" ImageUrl="/gestion/portal/Images/banobras.gif" runat="server"></asp:image></td>
											<TD bgColor="#00c0c0" height="32"><asp:image id="Image1" border="0" ImageUrl="/gestion/portal/Images/transp.gif" runat="server"
													Height="15" alt="" Width="8"></asp:image><span><asp:label id="Label1" runat="server" Font-Bold="True" CssClass="pnlBanco">Banco Nacional de Obras y Servicios Públicos S.N.C.</asp:label></span></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<TD width="65" bgColor="#00c0c0" height="32"></TD>
											<td align="center" width="65" background="/gestion/portal/Images/aguila.gif" bgColor="#00a09c"
												height="32" rowSpan="2"></td>
										</tr>
										<tr width="780">
											<td class="header" style="HEIGHT: 33px" align="left" width="520" bgColor="#d6d3ce" colSpan="7">
												<DIV class="pnlNombre" style="POSITION: relative" ms_positioning="GridLayout"><span class="normaltext1" id="participantName" style="FONT-WEIGHT: bold">
														<%=Session["user_name"]%>
														&nbsp;(<%=Session["uid"]%>
														)</span>
												</DIV>
												<A class="header" href="http://www.banobras.gob.mx"></A>
											</td>
										</tr>
										<tr width="780">
											<td style="HEIGHT: 32px" vAlign="middle" align="left" bgColor="#aaaaae" colSpan="4"><asp:image id="Image4" ImageUrl="/gestion/portal/images/1px.gif" runat="server" Width="8px"></asp:image></td>
											<td class="header" style="HEIGHT: 33px" align="right" width="130" background="/gestion/portal/images/mexico.gif"
												bgColor="#00a09c"></td>
											<td style="HEIGHT: 32px" align="right" bgColor="#6e8088" colSpan="6">
											<a id="lnkInicio" class="header2" onclick="window.parent.close()">Salir</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td width="65" background="/gestion/portal/Images/hscp.gif" height="32"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</BODY>
</HTML>
