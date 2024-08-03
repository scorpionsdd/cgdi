<%@ Page language="c#" Codebehind="edit_attach_reply.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_attach_replay" %>
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
				var sUrl = '/gestion/correspondencia/alternate_reply.aspx?id=<%= gnTurnarId %>&state=<%= gsState %>&documentoid=<%= gnDocumentId %>';
				window.location.href = sUrl;
			}
		
		</script>
	</HEAD>
	<body>
		<form id="edit_mail_attach" method="post" encType="multipart/form-data" runat="server">
			<TABLE class="standardPageTable" width="100%">
				<TR>
					<TD class="applicationTitle" style="HEIGHT: 20px" noWrap>Adjuntar Archivo</TD>
					<TD class="applicationTitle" align="right" colSpan="3"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm(<%= gsAction %>);" src="../Images/close_red.gif" align=absMiddle >
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE class="fullScrollMenu" cellPadding="1" width="100%">
							<TR>
								<TD class="header-gray" style="WIDTH: 30%">Seleccionar Archivo</TD>
								<TD class="header-gray" colSpan="3">&nbsp;<INPUT id="UploadedFile" style="WIDTH: 497px; HEIGHT: 22px" type="file" size="63" name="UploadedFile"
										runat="server"></TD>
							<TR>
								<TD align="right" colSpan="4"><asp:button id="btnSave" runat="server" CssClass="standard-text" Text="Agregar" Width="66px"></asp:button></TD>
							</TR>
							<TR>
								<td class="applicationTitle" style="HEIGHT: 14px" colSpan="4">Archivos 
									Seleccionados</td>
							</TR>
							<TR>
								<TD align="left" colSpan="4"><asp:datagrid id="dgAttach" runat="server" CssClass="dataGrid" Width="100%" DataKeyField="anexo_id"
										BorderStyle="None" OnDeleteCommand="dgAttach_Delete" AutoGenerateColumns="False">
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
													<asp:imagebutton id="PryDeleteButton" ImageUrl="/Gestion/images/icon-delete.gif" CausesValidation="False"
														CommandName="Delete" Runat="server"></asp:imagebutton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Archivo">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ruta") %>' ID="lblPath"/>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "archivo") %>' ID="lblFile"/>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<P><asp:label id="userMessage" runat="server"></asp:label>
				<asp:textbox id="txtState" runat="server" Visible="False"></asp:textbox></P>
			<P><asp:textbox id="txtAction" runat="server" Visible="False"></asp:textbox>
				<asp:textbox id="txtFilter" runat="server" Visible="False"></asp:textbox>
				<asp:textbox id="txtDocumentID" runat="server" Visible="False"></asp:textbox>
				<asp:textbox id="txtMailType" runat="server" Visible="False"></asp:textbox>
				<asp:textbox id="txtTurnarId" runat="server" Visible="False"></asp:textbox>
			</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
