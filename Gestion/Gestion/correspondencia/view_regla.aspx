<%@ Page language="c#" Codebehind="view_regla.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.view_regla" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Vista de Reglas</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
		<script language="javascript">
		
			var bSended = false;
			var oViewer = null;
			
			function doOperation(operationTag) {
			    document.getElementById("frmEditor").hdnOperationTag.value = operationTag;

				switch (operationTag) {
					case "Generate":
						if (!bSended) {
							if (validate()) {
								bSended = true;
								document.body.style.cursor='wait';
								document.getElementById("frmEditor").submit();
							}
						}
						break;
					case "Detail":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "edit_regla.aspx?id=" + arguments[1];
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "SetSearcher":
						setSearcher(true);
						break;
					default:
						alert("La operación solicitada todavía no ha sido definida en el programa.");
						break;
				}
				return;				
			}
			
			function validate() {
				return true;
			}
			
            function CloseWindow()
            {
                self.close();
            }
           
        </script>
	</HEAD>
	<body oncontextmenu="return true;" MS_POSITIONING="GridLayout">
		<form id="frmEditor" method="post" runat="server">
			<TABLE height="100%" width="100%">
				<TR>
					<TD class="titleRow" style="HEIGHT: 27px" noWrap>Lista de Reglas</TD>
					<td style="HEIGHT: 27px" noWrap>Fecha de Vigencia:
					</td>
					<td style="HEIGHT: 27px" noWrap><INPUT id="txtCutDate" style="WIDTH: 144px; HEIGHT: 19px" size="18" name="txtCutDate" runat="server"></td>
					<td style="HEIGHT: 27px" noWrap>Filtrar por:
					</td>
					<td style="HEIGHT: 27px" noWrap><INPUT id="txtFilter" style="WIDTH: 144px; HEIGHT: 19px" size="18" name="txtFilter" runat="server"></td>
					<td style="HEIGHT: 27px" noWrap><A href="javascript:doOperation('Generate');"><IMG hspace="6" src="/gestion/images/detail.gif" align="absMiddle">Crear
						</A>
					</td>
					<td style="HEIGHT: 27px" noWrap><A href="javascript:doOperation('Detail',0);"><IMG style="WIDTH: 16px; HEIGHT: 12px" height="12" hspace="6" src="/gestion/images/workflow/manny.gif"
								width="16" align="absMiddle">Agregar </A>
					</td>
					<td style="HEIGHT: 27px" align="right"><IMG title="Cerrar y regresar a la página principal" onclick="window.close();"
							alt="Cerrar" src="/Gestion/images/close_red.gif" align="absMiddle">
					</td>
				</TR>
				<TR>
					<td noWrap width="100%" colSpan="8" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td vAlign="top" width="100%">
									<div style="WIDTH: 100%; HEIGHT: 100%; OVERFLOW: auto">
										<asp:datagrid id="grdItems" tabIndex="-1" runat="server" AutoGenerateColumns="False" Width="100%"
											AllowSorting="True" CssClass="dataGrid" ShowFooter="True">
											<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
											<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
											<EditItemStyle VerticalAlign="Top"></EditItemStyle>
											<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="regla_id" DataFormatString="&lt;img class=imageButton src='/gestion/images/icons_sm/document_sm.gif'; onclick=&quot;return doOperation('Detail', {0})&quot;&gt;"></asp:BoundColumn>
												<asp:BoundColumn DataField="UsuarioNombre" SortExpression="UsuarioNombre ASC" HeaderText="Empleado">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="destinatarioNombre" SortExpression="destinatarioNombre ASC" HeaderText="Destinatario">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="destinatarioClaveArea" SortExpression="destinatarioClaveArea ASC" HeaderText="Clave">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="destinatarioArea" SortExpression="destinatarioArea ASC" HeaderText="Area">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RemitenteNombre" SortExpression="RemitenteNombre ASC" HeaderText="Remitente">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RemitenteClaveArea" SortExpression="RemitenteClaveArea ASC" HeaderText="Clave">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RemitenteArea" SortExpression="RemitenteArea ASC" HeaderText="Area">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="signNombre" SortExpression="signNombre ASC" HeaderText="Firma">
													<HeaderStyle Wrap="False"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle Visible="False" Wrap="False"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
