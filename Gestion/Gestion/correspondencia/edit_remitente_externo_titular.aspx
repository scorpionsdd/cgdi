<%@ Page language="c#" Codebehind="edit_remitente_externo_titular.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_remitente_externo_titular" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Titulares de las Dependencias</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="remitente_externo_titular" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%">
				<TR>
					<TD class="applicationTitle">Editor de Titulares de
						<%= getDependencia() %>
					</TD>
					<TD class="applicationTitle" noWrap align="right" colSpan="2"><IMG title="Cerrar y regresar a la página principal" onclick="window.close();" src="../Images/close_red.gif"
							align="absMiddle">
					</TD>
				</TR>
				<TR>
					<TD noWrap width="100%" colSpan="4">
						<TABLE class="fullScrollMenu" width="100%" border="1">
							<TR>
								<TD class="header-gray" style="HEIGHT: 28px">Título(Ing, Lic, Etc)</TD>
								<TD style="HEIGHT: 28px" colSpan="2"><asp:textbox id="tbTitulo" runat="server" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<td class="header-gray">Nombre</td>
								<td colSpan="2"><asp:textbox id="tbName" runat="server" Width="412px"></asp:textbox></td>
							</TR>
							<TR>
								<td class="header-gray" style="HEIGHT: 25px">Puesto</td>
								<td style="HEIGHT: 25px" colSpan="2"><asp:textbox id="tbPuesto" runat="server" Width="412"></asp:textbox><asp:button id="btnAdd" runat="server" CssClass="standard-text" Text="Agregar"></asp:button></td>
							</TR>
							<TR>
								<TD colSpan="4"><asp:datagrid id="dgTitular" runat="server" Width="100%" Height="63px" DataKeyField="remitente_externo_titular_id"
										BorderStyle="None" OnDeleteCommand="dgTitular_Delete" OnUpdateCommand="dgTitular_Update" OnEditCommand="dgTitular_Edit"
										CssClass="dataGrid" OnCancelCommand="dgTitular_Cancel" AutoGenerateColumns="False">
										<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
										<EditItemStyle VerticalAlign="Top"></EditItemStyle>
										<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Editar">
												<HeaderStyle HorizontalAlign="Left" Width="12%" CssClass="grid-header" VerticalAlign="Middle"
													Height="12px"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="grid-edit-column"></ItemStyle>
												<ItemTemplate>
													<asp:imagebutton id="EditButton" runat="server" CommandName="Edit" CausesValidation="false" ImageUrl="/gestion/images/icon-pencil.gif"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													<asp:imagebutton id="PryDeleteButton" CommandName="Delete" CausesValidation="False" ImageUrl="/gestion/images/icon-delete.gif"
														Runat="server"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
												</ItemTemplate>
												<EditItemTemplate>
													<asp:imagebutton id="UpdateButton" runat="server" CommandName="Update" CausesValidation="True" ImageUrl="/gestion/images/icon-floppy.gif"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													<asp:imagebutton id="PryCancelButton" runat="server" CommandName="Cancel" CausesValidation="false"
														ImageUrl="/gestion/images/icon-pencil-x.gif"></asp:imagebutton>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Titulo">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "titulo") %>' ID="lblTitulo" />
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox runat="server" id="txtTitulo" Text='<%# DataBinder.Eval(Container.DataItem, "titulo") %>' />
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nombre">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nombre") %>' ID="lblDependencia" />
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox runat="server" id="txtName" Text='<%# DataBinder.Eval(Container.DataItem, "nombre") %>' />
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Puesto">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "puesto") %>' ID="Label1"/>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox runat="server" id="txtPuesto" Text='<%# DataBinder.Eval(Container.DataItem, "puesto") %>' />
												</EditItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
