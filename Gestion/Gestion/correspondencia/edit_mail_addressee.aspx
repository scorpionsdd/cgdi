<%@ Page language="c#" Codebehind="edit_mail_addressee.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_mail_addressee" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>insert_mail_addressee</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
			function closeForm()
			{
				var sUrl = '/gestion/correspondencia/mail_editor.aspx?id=<%= gnDocumentId %>&action=<%= gsAction %>&filter=<%= gsFilter %>';
				window.location.href = sUrl;
			}
		
		</script>
	</HEAD>
	<body>
		<form id="edit_mail_addressee" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tbody>
					<TR>
						<TD class="applicationTitle" style="HEIGHT: 22px" noWrap>Destinatarios</TD>
						<TD class="applicationTitle" style="HEIGHT: 22px" noWrap align="right" colSpan="2"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm();" src="../Images/close_red.gif"
								align="absMiddle">
						</TD>
					<TR>
						<TD noWrap width="100%" colSpan="4">
							<TABLE class="fullScrollMenu" cellSpacing="1" cellPadding="1" width="100%" border="1">
								<tbody>
									<TR>
										<td style="HEIGHT: 28px" vAlign="middle" noWrap align="right" colSpan="4"><asp:textbox id="txtSearch" runat="server" ForeColor="White" BackColor="DimGray" Width="228px"></asp:textbox><asp:button id="btnSearch" runat="server" CssClass="standard-text" Text="Buscar"></asp:button></td>
									</TR>
									<TR>
										<TD class="header-gray" align="left">Instrucción:</TD>
										<TD class="header-gray" style="HEIGHT: 26px" vAlign="top" colSpan="2"><asp:textbox id="txtInstruction" runat="server" Width="435px" TextMode="MultiLine" MaxLength="512"
												Height="35px" tabIndex="1"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="header-gray" HEIGHT:vAlign="top">Area:</TD>
										<TD noWrap><asp:dropdownlist id="dropArea" runat="server" Width="435" AutoPostBack="True" tabIndex="2"></asp:dropdownlist></TD>
										<TD class="header-gray" noWrap>Fecha de Corte</TD>
										<TD noWrap colSpan="2">
											<asp:TextBox id="txtDate" runat="server" Width="72px"></asp:TextBox>
											<asp:Button id="btnAreas" runat="server" Text="Ejecutar"></asp:Button></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 145px" width="100%" colSpan="4"><asp:listbox id="lstAddressee" runat="server" Width="90%" CssClass="stanadard-text" Height="134px"
												SelectionMode="Multiple" tabIndex="3"></asp:listbox><asp:button id="btnSave" runat="server" CssClass="standard-text" Text="Agregar"></asp:button><BR>
										</TD>
									</TR>
									<TR>
										<td class="applicationTitle" style="HEIGHT: 16px" colSpan="4">
											<P>Turnados A:</P>
										</td>
									</TR>
									<TR>
										<TD align="left" width="100%" colSpan="4">
											<TABLE cellPadding="1" width="100%" border="0">
												<tbody>
													<tr>
														<td width="100%"><asp:datagrid id="dgAddressee" runat="server" Width="100%" CssClass="dataGrid" AutoGenerateColumns="False"
																OnCancelCommand="dgAddressee_Cancel" OnEditCommand="dgAddressee_Edit" OnUpdateCommand="dgAddressee_Update"
																OnDeleteCommand="dgAddressee_Delete" BorderStyle="None" DataKeyField="documento_turnar_id">
																<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
																<EditItemStyle VerticalAlign="Top"></EditItemStyle>
																<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
																<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
																<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Editar">
																		<HeaderStyle HorizontalAlign="Left" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center" CssClass="grid-edit-column"></ItemStyle>
																		<ItemTemplate>
																			<asp:imagebutton id="EditButton" runat="server" ImageUrl="/gestion/images/icon-pencil.gif" CausesValidation="false"
																				CommandName="Edit"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
																			<asp:imagebutton id="PryDeleteButton" ImageUrl="/gestion/images/icon-delete.gif" CausesValidation="False"
																				CommandName="Delete" Runat="server"></asp:imagebutton>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<asp:imagebutton id="UpdateButton" runat="server" ImageUrl="/gestion/images/icon-floppy.gif" CausesValidation="True"
																				CommandName="Update"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
																			<asp:imagebutton id="PryCancelButton" runat="server" ImageUrl="/gestion/images/icon-pencil-x.gif"
																				CausesValidation="false" CommandName="Cancel"></asp:imagebutton>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nombre">
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "destinatario") %>' ID="lblDestinatario"/>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Area">
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "area") %>' ID="lblArea" />
																		</ItemTemplate>
																		<EditItemTemplate>
																			<asp:DropDownList Runat=server id="editArea" Font-Size=7 DataSource='<%# editAreaBind() %>' DataTextField='area' DataValueField='id_area' SelectedIndex='<%# GetAreaIndex( DataBinder.Eval(Container.DataItem, "id_area").ToString() ) %>' />
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Instrucci&#243;n">
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "instruccion") %>' ID="lblInstruccion"/>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<asp:TextBox runat="server" TextMode=MultiLine id="txtInstruccion" Text='<%# DataBinder.Eval(Container.DataItem, "instruccion") %>'/>
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Tramite">
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_tramite") %>' ID="lblTipoTramite"/>
																		</ItemTemplate>
																		<EditItemTemplate>
																			<asp:dropdownlist runat="server" id="editTipoTramite" DataSource='<%# TipoTramiteBind() %>' DataTextField='tipo_tramite' DataValueField='tipo_tramite_id' SelectedIndex='<%# GetTipoTramiteIndex( DataBinder.Eval(Container.DataItem, "tipo_tramite_id").ToString() ) %>' />
																		</EditItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid></td>
													</tr>
												</tbody>
											</TABLE>
										</TD>
									</TR>
								</tbody>
							</TABLE>
						</TD>
					</TR>
				</tbody>
			</TABLE>
			<asp:textbox id="txtAction" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtDocumentId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtClose" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtFilter" runat="server" Visible="False"></asp:textbox></form>
	</body>
</HTML>
