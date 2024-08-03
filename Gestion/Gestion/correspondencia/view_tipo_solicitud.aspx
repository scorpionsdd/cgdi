<%@ Page language="c#" Codebehind="view_tipo_solicitud.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.view_tipo_solicitud" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Tipos de Solicitud</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
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
			<TABLE height="100%" width="100%">
				<TR>
					<TD class="titleRow" style="HEIGHT: 27px" noWrap>Tipos de Solicitud</TD>
					<td style="HEIGHT: 27px" noWrap><asp:textbox id="TextBoxTipoSolicitud" runat="server" Width="484px"></asp:textbox>
						&nbsp;
						<asp:button id="btnAdd" runat="server" Text="Agregar" CssClass="cmdSubmit"></asp:button>
					</td>
					<td style="HEIGHT: 27px" align="right"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm();" alt="Cerrar"
							src="/Gestion/images/close_red.gif" align="absMiddle">
					</td>
				</TR>
				<TR>
					<td noWrap width="100%" colSpan="8" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td vAlign="top" width="100%">
									<div style="WIDTH: 100%; HEIGHT: 100%; OVERFLOW: auto">
										<asp:datagrid id="grdItems" tabIndex="-1" runat="server" Width="100%" Height="39px" CssClass="dataGrid"
											DataKeyField="id_tipo_solicitud" BorderStyle="None" OnDeleteCommand="dgTipoSolicitud_Delete"
											OnItemDataBound="dgTipoSolicitud_ItemBound" OnUpdateCommand="dgTipoSolicitud_Update" OnEditCommand="dgTipoSolicitud_Edit"
											OnCancelCommand="dgTipoSolicitud_Cancel" AutoGenerateColumns="False">
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
														<asp:imagebutton id="EditButton" runat="server" CommandName="Edit" CausesValidation="false" ImageUrl="/gestion/images/icon-pencil.gif"></asp:imagebutton><IMG src="images/spacer.gif" width="1">
														<asp:imagebutton id="PryDeleteButton" CommandName="Delete" CausesValidation="False" ImageUrl="/gestion/images/icon-delete.gif"
															Runat="server"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													</ItemTemplate>
													<EditItemTemplate>
														<asp:imagebutton id="UpdateButton" runat="server" CommandName="Update" CausesValidation="True" ImageUrl="/gestion/images/icon-floppy.gif"></asp:imagebutton><IMG src="images/spacer.gif" width="1">
														<asp:imagebutton id="PryCancelButton" runat="server" CommandName="Cancel" CausesValidation="false"
															ImageUrl="/gestion/images/icon-pencil-x.gif"></asp:imagebutton>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tipo Solicitud">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_solicitud") %>' ID="lblTipoSolicitud"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox runat="server" id="txtTipoSolicitud" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_solicitud") %>' />
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
