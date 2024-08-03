<%@ Page language="c#" Codebehind="view_tipo_atencion.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.view_tipo_atencion" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Tipos de Solicitud</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/css/worksheet.css">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
		<script language="javascript">
		
			var bSended = false;
			var oViewer = null;
			
			function  doOperation(operationTag) {
			    document.getElementById("frmEditor").hdnOperationTag.value = operationTag;
				switch (operationTag) 
				{
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
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=660px";
							var sURL = "edit_tipo_solicitud.aspx?id=" + arguments[1];
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
            
            function closeForm()
			{
				var sUrl = '/gestion/portal/main.aspx';
				window.location.href = sUrl;
			}

           
		</script>
	</HEAD>
	<body oncontextmenu="return true;" MS_POSITIONING="GridLayout">
		<form id="frmEditor" method="post" runat="server">
			<TABLE width="100%" height="100%">
				<TR>
					<TD style="HEIGHT: 27px" class="titleRow" noWrap>
						<P>Tipos de Atención</P>
						<P>&nbsp;</P>
					</TD>
					<td style="HEIGHT: 27px" noWrap><asp:textbox id="TextBoxTipoAtencion" runat="server" Width="484px"></asp:textbox>&nbsp;
						<asp:button id="btnAdd" runat="server" CssClass="cmdSubmit" Text="Agregar"></asp:button></td>
					<td style="HEIGHT: 27px" align="right"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm();" alt="Cerrar"
							align="absMiddle" src="/Gestion/images/close_red.gif">
				</TR>
				<TR>
					<td height="100%" width="100%" colSpan="8" noWrap>
						<table cellSpacing="0" cellPadding="0" width="100%" height="100%">
							<tr>
								<td vAlign="top" width="100%">
									<div style="WIDTH: 100%; HEIGHT: 100%; OVERFLOW: auto">
										<asp:datagrid id="grdItems" tabIndex="-1" runat="server" Width="100%" Height="39px" CssClass="dataGrid"
											DataKeyField="id_tipo_atencion" BorderStyle="None" OnDeleteCommand="grdItems_Delete" OnUpdateCommand="grdItems_Update"
											OnEditCommand="grdItems_Edit" OnCancelCommand="grdItems_Cancel" AutoGenerateColumns="False">
											<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
											<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
											<EditItemStyle VerticalAlign="Top"></EditItemStyle>
											<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Editar">
													<HeaderStyle HorizontalAlign="Left" Width="8%" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="grid-edit-column"></ItemStyle>
													<ItemTemplate>
														<asp:imagebutton id="EditImageButton" runat="server" CommandName="Edit" CausesValidation="false"
															ImageUrl="/gestion/images/icon-pencil.gif" BorderStyle="None"></asp:imagebutton><IMG src="images/spacer.gif" width="1">
														<asp:imagebutton id="DeleteImageButton" CommandName="Delete" CausesValidation="False" ImageUrl="/gestion/images/icon-delete.gif"
															Runat="server" BorderStyle="None"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													</ItemTemplate>
													<EditItemTemplate>
														<asp:imagebutton id="UpdateImageButton" runat="server" CommandName="Update" CausesValidation="True"
															ImageUrl="/gestion/images/icon-floppy.gif" BorderStyle="None"></asp:imagebutton><IMG src="images/spacer.gif" width="1">
														<asp:imagebutton id="CancelImageButton" runat="server" CommandName="Cancel" CausesValidation="false"
															ImageUrl="/gestion/images/icon-pencil-x.gif" BorderStyle="None"></asp:imagebutton>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tipo Atención">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_atencion") %>' ID="lblTipoAtencion"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox runat="server" id="txtTipoAtencion" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_atencion") %>' />
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
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
