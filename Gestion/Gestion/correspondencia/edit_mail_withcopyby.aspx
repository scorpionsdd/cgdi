<%@ Page language="c#" Codebehind="edit_mail_withcopyby.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.insert_mail_withcopyby" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Insert Mail Attach</title>
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
		<form id="edit_mail_attach" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%">
				<TR>
					<TD class="applicationTitle" style=" HEIGHT: 20px">Con Copia para</TD>
					<td align="right" nowrap class="applicationTitle"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm();" alt="Cerrar"
							src="/Gestion/Images/close_red.gif" align="absMiddle">
					</td>
				</TR>
				<tr>
					<td colspan="2">
						<TABLE class="fullScrollMenu" cellPadding="1" width="100%">
							<TR>
								<td class="header-gray" style="HEIGHT: 13px" colSpan="4">
									<P align="left">Seleccionar con copia para&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:TextBox id="txtSearch" runat="server" Width="315px"></asp:TextBox>&nbsp;
										<asp:Button id="Button1" runat="server" CssClass="cmdButton" Text="Buscar"></asp:Button></P>
									<p>
										Fecha de corte:&nbsp;
										<asp:TextBox id="txtDate" runat="server" Width="80px"></asp:TextBox>
									</p>
								</td>
							<TR>
								<TD style="HEIGHT: 199px" colSpan="4">
									<P align="left"><asp:listbox id="lstWithCopyFor" runat="server" SelectionMode="Multiple" CssClass="standard-text"
											Height="177px" Width="100%"></asp:listbox></P>
								</TD>
							<TR>
								<TD align="right" colSpan="4" style="HEIGHT: 21px"><asp:button id="btnSave" runat="server" CssClass="standard-text" Text="Aceptar"></asp:button></TD>
							</TR>
							<TR>
								<td class="applicationTitle" style="HEIGHT: 14px" colSpan="4">
									Seleccionados:</td>
							</TR>
							<TR>
								<TD align="left" colSpan="4"><asp:datagrid id="dgWithCopyFor" runat="server" Width="100%" AutoGenerateColumns="False" OnCancelCommand="dgWithCopyFor_Cancel"
										OnEditCommand="dgWithCopyFor_Edit" OnDeleteCommand="dgWithCopyFor_Delete" OnUpdateCommand="dgWithCopyFor_Update" BorderStyle="None"
										DataKeyField="ccpara_id" CssClass="dataGrid">
										<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
										<EditItemStyle VerticalAlign="Top"></EditItemStyle>
										<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Editar">
												<HeaderStyle HorizontalAlign="Left" Width="50px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="grid-edit-column"></ItemStyle>
												<ItemTemplate>
													<asp:imagebutton id="EditButton" runat="server" ImageUrl="/Gestion/images/icon-pencil.gif" CausesValidation="false"
														CommandName="Edit"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													<asp:imagebutton id="PryDeleteButton" ImageUrl="/Gestion/images/icon-delete.gif" CausesValidation="False"
														CommandName="Delete" Runat="server"></asp:imagebutton>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:imagebutton id="UpdateButton" runat="server" ImageUrl="/Gestion/images/icon-floppy.gif" CausesValidation="True"
														CommandName="Update"></asp:imagebutton><IMG src="images/spacer.gif" width="3">
													<asp:imagebutton id="PryCancelButton" runat="server" ImageUrl="/Gestion/images/icon-pencil-x.gif"
														CausesValidation="false" CommandName="Cancel"></asp:imagebutton>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Con copia para">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "destinatario") %>' ID="lblWithCopyFor"/>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:dropdownlist runat="server" id=editDestinatario DataSource='<% # GetDisponibility() %>' DataTextField='destinatario' DataValueField='id_empleado' >
													</asp:dropdownlist>
												</EditItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<asp:TextBox id="txtDocumentId" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtAction" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtFilter" runat="server" Visible="False"></asp:TextBox>
		</form>
	</body>
</HTML>
