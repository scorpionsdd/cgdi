<%@ Page language="c#" Codebehind="statistics.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.Statistic" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Explorador de Correspondencia Externa</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--			
			var bSended = false;
			var oViewer = null;
			
			function doOperation(operationTag) {
				switch (operationTag) {
				    case "Generate":
				        if (!bSended) {
							if (validate()) {
								bSended = true;
								document.body.style.cursor = 'wait';
								document.getElementById("frmEditor").submit();
							}
						}
						
						break;
					case "Detail":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=660px";
							var sURL = "mail_editor.aspx?id=" + arguments[1];
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "ViewConditions":
						var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=460px";
						var sURL = "transactions_viewer.aspx?productId=" + arguments[1] + "&organizationId=" + arguments[2] + "&conditionId=" + arguments[3];
						
						sURL += "&fromDate=" + document.getElementById("frmEditor").txtFromDate.value + "&toDate=" +
                            document.getElementById("frmEditor").txtToDate.value;

						if (oViewer == null || oViewer.closed) {
							oViewer = window.open(sURL, "_blank", sOptions);
						} else {
							oViewer.focus();
							oViewer.navigate(sURL);
						}
						break;
					default:
						alert("La operación solicitada todavía no ha sido definida en el programa.");
						break;
				}
				return;				
			}
			
			function validate() {
			    var oForm = document.getElementById("frmEditor");
				if (oForm.cboSender.value == "") {
					alert("Requiero seleccione una Area");
					oForm.cboSender.focus();
					return false;
				}
				return true;
			}
			
		//-->
		</script>
	</HEAD>
	<BODY>
		<form id="frmEditor" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td noWrap width="100%">
						<table style="WIDTH: 100%" cellSpacing="2" cellPadding="2">
							<TR>
								<TD style="BORDER-BOTTOM: #669999 1px solid" noWrap width="100%" colSpan="5">
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td style="FONT-FAMILY: Tahoma; FONT-SIZE: 10pt" vAlign="top" noWrap>&nbsp;
												<asp:label id="lblTitle" runat="server">Label</asp:label></td>
											<td style="FONT-FAMILY: Tahoma; FONT-SIZE: 10pt" width="100%"><b></b></td>
										</tr>
									</table>
								</TD>
								<TD style="BORDER-BOTTOM: #669999 1px solid" noWrap width="100%"></TD>
							</TR>
							<TR height="10">
								<TD noWrap>Area&nbsp;que Envía&nbsp;:</TD>
								<TD style="WIDTH: 162px; HEIGHT: 17px" vAlign="top" noWrap align="left">&nbsp;
									<SELECT id="cboSender" style="WIDTH: 320px" name="cboSender" runat="server">
									</SELECT>
								</TD>
								<td style="HEIGHT: 17px" noWrap>&nbsp; Enviada-Recibida
								</td>
								<TD style="HEIGHT: 17px" noWrap align="left">&nbsp;
									<asp:RadioButtonList ID="RBLTipoCorres" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
										<asp:ListItem Text="Recibida" Value="R"></asp:ListItem>
										<asp:ListItem Text="Enviada" Value="E"></asp:ListItem>
									</asp:RadioButtonList>
								</TD>
								<td style="HEIGHT: 17px" noWrap align="left" width="100%"><A href="javascript:doOperation('Generate');"><IMG hspace="6" src="/gestion/images/detail.gif" align="absMiddle">Generar</A>
								</td>
								<TD style="HEIGHT: 17px" noWrap align="left" width="100%"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 72px; HEIGHT: 23px" noWrap>Fecha Desde:</TD>
								<TD style="HEIGHT: 23px" vAlign="top" noWrap><INPUT class="Input" id="txtDateFrom" style="WIDTH: 192px" maxLength="10" size="26" name="txtDateFrom"
										runat="server">
								</TD>
								<TD style="HEIGHT: 23px" vAlign="baseline" noWrap>Fecha Hasta:&nbsp;</TD>
								<TD style="HEIGHT: 23px" noWrap><INPUT class="Input" id="txtDateTo" style="WIDTH: 192px" maxLength="10" name="txtDateTo"
										runat="server">
								</TD>
								<TD style="HEIGHT: 23px" noWrap><IMG title="Cerrar y regresar a la página principal" onclick="window.location.href = '/gestion/portal/main.aspx'; "
										src="../Images/close_red.gif" align="absMiddle"></TD>
								<TD style="HEIGHT: 23px" noWrap></TD>
							</TR>
							<TR>
								<td style="WIDTH: 72px" noWrap>&nbsp;
								</td>
								<td vAlign="top" noWrap>&nbsp;
								</td>
								<td vAlign="baseline" noWrap>&nbsp;
								</td>
								<TD noWrap>&nbsp;
								</TD>
								<td noWrap></td>
								<TD noWrap></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td noWrap width="100%" height="100%">
						<table style="WIDTH: 100%" height="100%" cellSpacing="0" cellPadding="0">
							<tr>
								<td vAlign="top" width="100%" colSpan="2">
									<div style="WIDTH:100%; HEIGHT:100%; OVERFLOW:auto"><asp:datagrid id="grdItems" tabIndex="-1" ShowHeader="True" runat="server" AutoGenerateColumns="False"
											AllowPaging="False" AllowSorting="True" ShowFooter="True" Width="100%" CssClass="dataGrid">
											<FooterStyle Wrap="False" HorizontalAlign="Right" CssClass="totalsRow"></FooterStyle>
											<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
											<EditItemStyle VerticalAlign="Top"></EditItemStyle>
											<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Right" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle Wrap="False" HorizontalAlign="Right" CssClass="headerRow"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="fromClaveArea" SortExpression="fromClaveArea ASC" HeaderText="Clave">
													<HeaderStyle Wrap="False" HorizontalAlign="Left"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Area" SortExpression="Area ASC" HeaderText="Area">
													<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="65%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Pendiente" SortExpression="Pendiente ASC" HeaderText="Pendiente" DataFormatString="{0:N0}">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Tramite" SortExpression="Tramite ASC" HeaderText="Tramite" DataFormatString="{0:N0}">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Concluido" SortExpression="Concluido ASC" HeaderText="Concluido" DataFormatString="{0:N0}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Total" SortExpression="Total ASC" HeaderText="Total" DataFormatString="{0:N0}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle Visible="False" Wrap="False"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
