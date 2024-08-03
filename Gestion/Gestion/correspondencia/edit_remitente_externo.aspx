<%@ Page language="c#" Codebehind="edit_remitente_externo.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_remitente_externo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Editor de Remitentes</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">

			function ExecuteForm()
			{
				var sUrl = "/gestion/correspondencia/edit_remitente_externo_titular.aspx";
				window.open(sUrl);
			}

		</script>
	</HEAD>
	<body>
		<form id="edit_remitente_externo" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%" border="0">
				<TBODY>
					<TR>
						<TD class="applicationTitle" style="HEIGHT: 16px" noWrap>Editor de Remitentes 
							Externos</TD>
						<% if ( Request["callType"] == "in") { %>
						<TD class="applicationTitle" noWrap align="right" colSpan="2"><asp:button id="btnClose" runat="server" CssClass="cmdSubmit" Width="60px" Text="Cerrar"></asp:button></TD>
						<% } else { %>
						<TD class="applicationTitle" align="right" colSpan="2"><IMG title="Cerrar y regresar a la página principal" onclick="window.location.href = '/gestion/portal/main.aspx'; "
								alt="Cerrar" src="../Images/close_red.gif" align="absMiddle"></TD>
						<%}%>
					</TR>
					<TR>
						<TD noWrap colSpan="4">
							<TABLE class="fullScrollMenu" width="100%" border="1">
								<TR>
									<TD class="header-gray" style="HEIGHT: 25px">Dependencia</TD>
									<TD style="HEIGHT: 25px" colSpan="2"><asp:textbox id="tbDependencia" runat="server" Width="406px"></asp:textbox>&nbsp;&nbsp;
										<asp:button id="btnAdd" runat="server" CssClass="cmdSubmit" Text="Agregar"></asp:button></TD>
								</TR>
								<TR>
									<TD noWrap width="100%" colSpan="4"><asp:datagrid id="dgExternalSender" runat="server" CssClass="dataGrid" Width="264px" OnItemDataBound="dgExternalSender_ItemBound"
											Height="140px" DataKeyField="remitente_externo_id" BorderStyle="None" OnDeleteCommand="dgExternalSender_Delete" OnUpdateCommand="dgExternalSender_Update"
											OnEditCommand="dgExternalSender_Edit" OnCancelCommand="dgExternalSender_Cancel" AutoGenerateColumns="False">
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
												<asp:TemplateColumn HeaderText="Titulares">
													<ItemTemplate>
														<asp:HyperLink id="hyperlink1" ImageUrl="/gestion/images/icons_sm/document_sm.gif" NavigateUrl='<%# "edit_remitente_externo_titular.aspx?id=" + DataBinder.Eval(Container.DataItem, "remitente_externo_id") %>' Text="Despliega los titulares" Target="_new" runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Dependencia">
													<ItemStyle Wrap="False" Width="80%" CssClass="grid-edit-column"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dependencia") %>' ID="lblDependencia"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox runat="server" id="txtDependencia" Text='<%# DataBinder.Eval(Container.DataItem, "dependencia") %>' />
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<asp:textbox id="txtSenderID" runat="server" Width="1px"></asp:textbox>
		</form>
		</TBODY></TABLE>
	</body>
</HTML>
