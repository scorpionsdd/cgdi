<%@ Page language="c#" Codebehind="mail_editor.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.mail_editor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>mail_editor</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
           
			function viewAttach(sFile) {
				window.open(sFile, '_blank');
				return false;
			}

			function CloseWindow(){
				window.close();
				return false;
		   }

		</script>
	</HEAD>
	<body>
		<form id="mail_editor" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%">
				<tr>
					<td>&nbsp;</td>
					<td></td>
				</tr>
				<TR>
					<TD class="applicationTitle" style="HEIGHT: 22px" noWrap><P align="left">Editor de 
							Correspondencia</P>
					</TD>
					<% if (gsAction == "CREATE_DOCUMENT" || gsAction == "C") { %>
					<TD class="applicationTitle" style="HEIGHT: 22px" align="right" colSpan="3">
						<P align="right"><IMG title="Cerrar y regresar a la página de captura" onclick="window.location.href = '/gestion/portal/main.aspx'; "
								src="../Images/close_red.gif" align="absMiddle">
						</P>
					</TD>
					<% } else if (gsAction == "ALTERNATE_DOCUMENT1") {%>
					<TD class="applicationTitle" style="HEIGHT: 22px" align="right" colSpan="3"><IMG title="Cerrar y regresar a la página editor" onclick="window.location.href = '/gestion/correspondencia/mail_alternate.aspx'; "
							alt="Cerrar" src="../Images/close_red.gif" align="absMiddle"></TD>
					<% } else if (gsAction == "CURRENT") {%>
					<TD class="applicationTitle" style="HEIGHT: 22px" align="right" colSpan="3"><IMG title="Cerrar y regresar a la página editor" onclick="window.location.href = '/gestion/correspondencia/mail_alternate.aspx'; "
							alt="Cerrar" src="../Images/close_red.gif" align="absMiddle"></TD>
					<% } else if (gsAction == "E" || gsAction == "I") {%>
					<TD class="applicationTitle" style="HEIGHT: 22px" align="right" colSpan="3"><IMG id="clicker" style="CURSOR: hand" onclick="CloseWindow();" src="/gestion/images/close_red.gif">
					</TD>
					<% }%>
					<% if (gsAction == "U" || gsAction == "ALTERNATE_DOCUMENT" || gsAction == "COPIA")  {%>
					<TD class="applicationTitle" style="HEIGHT: 22px" align="right" colSpan="3"><IMG id="clicker" style="CURSOR: hand" onclick="CloseWindow();" src="/gestion/images/close_red.gif">
					</TD>
					<% }%>
				<TR>
					<TD style="HEIGHT: 44px" noWrap colSpan="4">
						<TABLE class="fullScrollMenu">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle" vAlign="middle" noWrap width="315">Correspondencia</TD>
								<TD class="fullScrollMenuTitle" vAlign="middle" noWrap><A class=cmdSubmit href="print_alternate.aspx?id=<%=gsDocumentId%>" target=_blank >Imprimir</A>
									&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;
									<asp:linkbutton id="lnkHeader" runat="server" CssClass="cmdSubmit">Editar Documento</asp:linkbutton>&nbsp;&nbsp; 
									| &nbsp;&nbsp;
								</TD>
							</TR>
						</TABLE>
						<TABLE class="applicationTable">
							<TR>
								<%= gsMailHeader %>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE class="fullScrollMenu">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle" noWrap width="315">Movimientos
								</TD>
								<TD class="fullScrollMenuTitle" noWrap><asp:linkbutton id="btnAddressee" runat="server" ForeColor="White" Text="Destinatarios">Turnar A:</asp:linkbutton>&nbsp; 
									&nbsp; | &nbsp;
									<asp:linkbutton id="btnWitCopyBy" runat="server" ForeColor="White" Text="Destinatarios">
									Con copia para</asp:linkbutton>&nbsp; &nbsp; | &nbsp;
									<asp:linkbutton id="btnAttach" runat="server" ForeColor="White" Text="Anexos">Adjuntar</asp:linkbutton>&nbsp;&nbsp; 
									&nbsp;|&nbsp;&nbsp;&nbsp; &nbsp; <A href="edit_status_mail.aspx?id=<%=documentId%>" target=_blank >
										Actualiza Estatus</A></TD>
							</TR>
						</TABLE>
						<TABLE class="applicationTable" width="100%">
							<TR class="applicationTableHeader">
								<TD class="applicationTitle" style="WIDTH: 320px" noWrap><b>Turnado A:</b></TD>
							</TR>
							<TR>
								<%-- Turnados --%>
								<td vAlign="top" noWrap width="100%" colSpan="4"><asp:datagrid id="dgAddreesse" runat="server" CssClass="dataGrid" OnItemCreated="Item_Created"
										OnEditCommand="dgAddreesse_Edit" DataKeyField="documento_id" Width="100%" AutoGenerateColumns="False">
										<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
										<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
										<EditItemStyle VerticalAlign="Top"></EditItemStyle>
										<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Imprimir">
												<ItemTemplate>
													<asp:HyperLink id="hyperlink1" ImageUrl="/gestion/images/icons_sm/printer.jpg" NavigateUrl='<%# "print_alternate.aspx?" + DataBinder.Eval(Container.DataItem, "paraKeys") %>' Text="Imprime el volante para el destinatario" Target="_new" runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Respuesta">
												<ItemTemplate>
													<asp:HyperLink id="Hyperlink2" ImageUrl="/gestion/images/icons_sm/document_sm.gif" NavigateUrl='<%# "alternate_reply.aspx?id=" + DataBinder.Eval(Container.DataItem, "documento_turnar_id") + "&state=3" %> ' Text="Respuesta del destinatario" Target="_new" runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="ParaKeys">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParaKeys") %>' ID="lblKeys"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nombre">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "destinatario") %>' ID="lblName"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Area">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "area") %>' ID="lblArea"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Instrucci&#243;n">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "instruccion") %>' ID="lblInstruccion"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Estatus">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status") %>' ID="lblStatus"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Con">
												<ItemTemplate>
													<asp:CheckBox runat="server" ID="chkVerify" OnCheckedChanged="save_estatus_turnado" AutoPostBack="True" Checked='<%# GetVerifyStatus( DataBinder.Eval(Container.DataItem, "estatus_verifica").ToString() ) %>'/>
													<asp:Label ID="lblVerify" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "estatus_verifica") %>' />
													<asp:Label ID="lblDocumentoTurnarID" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "documento_turnar_id") %>' />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Seg">
												<ItemTemplate>
													<asp:CheckBox runat="server" ID="chkSeguimiento" OnCheckedChanged="save_estatus_seguimiento" AutoPostBack="True" Checked='<%# GetSeguimientoStatus( DataBinder.Eval(Container.DataItem, "estatus_seguimiento").ToString() ) %>' />
													<asp:Label ID="lblSeguimiento" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "estatus_verifica") %>' />
													<asp:Label ID="lblDocumentoTurnarIDD" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "documento_turnar_id") %>' />
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</TR>
							<TR class="applicationTableHeader">
								<TD class="applicationTitle" noWrap><b>Con Copia A:</b></TD>
							</TR>
							<TR>
								<TD vAlign="top" noWrap width="100%" colSpan="4"><asp:datagrid id="dgWithCopyFor" runat="server" CssClass="dataGrid" Width="100%" AutoGenerateColumns="False">
										<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
										<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
										<EditItemStyle VerticalAlign="Top"></EditItemStyle>
										<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
										<Columns>
											<asp:HyperLinkColumn Text="Respuesta" Target="_blank" DataNavigateUrlField="ccpara_id" DataNavigateUrlFormatString="ccpara_reply.aspx?turnadoid={0}&state=3"></asp:HyperLinkColumn>
											<asp:TemplateColumn HeaderText="Respuesta">
												<ItemTemplate>
													<asp:HyperLink id="Hyperlink3" ImageUrl="/gestion/images/icons_sm/document_sm.gif" NavigateUrl='<%# "ccpara_reply.aspx?turnadoid=" + DataBinder.Eval(Container.DataItem, "ccpara_id") + "&state=3" %> ' Text="Respuesta del destinatario" Target="_new" runat="server" />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nombre">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "destinatario") %>' ID="Label2"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Area">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "area") %>' ID="Label3"/>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Estatus">
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status") %>' ID="lblCCparaStatus"/>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
							<TR class="applicationTableHeader">
								<TD class="applicationTitle" noWrap><b>Archivos Adjuntos</b></TD>
							</TR>
							<%= createAttach(Convert.ToInt32(Request["id"].ToString())) %>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<asp:textbox id="txtDocumentId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtAction" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtBind" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtMailType" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtQueryType" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtHeader" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtAreaId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtStatus" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtElaborationDateFrom" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtElaborationDateTo" runat="server" Visible="False"></asp:textbox></form>
	</body>
</HTML>
