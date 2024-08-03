<%@ Page language="c#" Codebehind="edit_tipo_documento.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_tipo_documento" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Tipo de Documento</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
	</HEAD>
	<body>
		<form id="DocumentType" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%">
				<TR>
					<TD class="applicationTitle" style="HEIGHT: 20px" noWrap>Editor de Tipos de 
						Documento</TD>
					<% if (form != null){ %>
					<TD Class="applicationTitle" align="right" colSpan="2">
						<asp:button id="btnClose" runat="server" Text="Cerrar" Width="60px" CssClass="cmdSubmit"></asp:button></TD>
					<% 	}else{ %>
					<TD Class="applicationTitle" align="right" colSpan="2"><IMG title="Cerrar y regresar a la página principal" onclick="window.location.href = '/gestion/portal/main.aspx'; "
							alt="Cerrar" src="../Images/close_red.gif" align="absMiddle"></TD>
					<% } %>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE class="fullScrollMenu" cellPadding="1" border="1" width="100%">
							<TR>
								<TD style="HEIGHT: 25px" class="header-gray">Tipo de Documento</TD>
								<TD style="HEIGHT: 25px" colSpan="2"><asp:textbox id="tbTipoDocumento" runat="server" Width="484px"></asp:textbox>&nbsp;
									<asp:button id="btnAdd" runat="server" Text="Agregar" CssClass="cmdSubmit"></asp:button></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 197px" colSpan="4">
									<asp:datagrid id="dgTipoDocumento" tabIndex="-1" runat="server" Width="100%" Height="39px" CssClass="dataGrid"
										DataKeyField="tipo_documento_id" BorderStyle="None" OnDeleteCommand="dgTipoDocumento_Delete"
										OnUpdateCommand="dgTipoDocumento_Update" OnEditCommand="dgTipoDocumento_Edit" OnCancelCommand="dgTipoDocumento_Cancel"
										AutoGenerateColumns="False">
										<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
										<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
										<EditItemStyle VerticalAlign="Top"></EditItemStyle>
										<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Tipo de Documento">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_documento") %>' ID="lblTipoDocumento"/>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox runat="server" id="txtTipoDocumento" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_documento") %>' />
												</EditItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid>
									<!--<INPUT type="hidden" name="txtTipoDocumentoId" id="txtTipoDocumentoId" value=<%=sId%>>-->
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
