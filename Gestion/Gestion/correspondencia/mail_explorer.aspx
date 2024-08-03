<%@ Page language="c#" Codebehind="mail_explorer.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.Mail_Explorer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<script runat="server">
	private void Grid_Change(Object sender, DataGridPageChangedEventArgs e)
	{
		// For the DataGrid control to navigate to the correct page when
		// paging is allowed, the CurrentPageIndex property must be updated
		// programmatically. This process is usually accomplished in the
		// event-handling method for the PageIndexChanged event.

		// Set CurrentPageIndex to the page the user clicked.
		grdItems.CurrentPageIndex = e.NewPageIndex;
		// Rebind the data to refresh the DataGrid control. 
		grdItems.DataSource = DocumentsBindPendingJAS();
		grdItems.DataBind();
	}
</script>


<HTML>
	<HEAD>
		<title>Explorador de Correo Enviado</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/resources/applications.css">
		<script language="javascript" type="text/javascript" src="/gestion/general.js"></script>
		<LINK rel="stylesheet" type="text/css" href="/gestion/css/worksheet.css">
		
		<script language="javascript">

                function closeForm() {
                    var sUrl = '/gestion/portal/main.aspx';
                    window.location.href = sUrl;
                }
		</script>
	
		<script language="javascript">
		<!--			
			var bSended = false;
			var oViewer = null;
			
			function doOperation(operationTag) {
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
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=660px";
							var sURL = "/gestion/correspondencia/mail_editor.aspx?id=" + arguments[2];
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "ViewExcel":
						var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=460px";
						var sURL = arguments[1]
						
						if (oViewer == null || oViewer.closed) {
							oViewer = window.open(sURL, "_blank", sOptions);
						} else {
							oViewer.focus();
							oViewer.navigate(sURL);
						}
						return false;
					
					case "ViewConditions":
						var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=460px";
						var sURL = "transactions_viewer.aspx?productId=" + arguments[1] + "&organizationId=" + arguments[2] + "&conditionId=" + arguments[3];
						
						sURL += "&fromDate=" + document.getElementById("frmEditor").txtFromDate.value + "&toDate=" + document.getElementById("frmEditor").txtToDate.value;
						if (oViewer == null || oViewer.closed) {
							oViewer = window.open(sURL, "_blank", sOptions);
						} else {
							oViewer.focus();
							oViewer.navigate(sURL);
						}
						break;

					default:
						alert("La operación solicitada todavía no ha sido definida en el programa.");
						break;
				}
				return;				
			}
			
			function validate() {
				var oForm = document.getElementById("frmEditor");
				if (oForm.cboGroup1.value == "") {
					alert("Requiero se seleccione alguna de las formas de agrupación.");
					oForm.cboGroup1.focus();
					return false;
				}
				if (oForm.cboGroup1.value != "" && oForm.cboGroup1.value == oForm.cboGroup2.value) {
					alert("Las agrupaciones uno y dos son la misma.");
					oForm.cboGroup2.focus();
					return false;
				}
				if (oForm.cboGroup1.value != "" && oForm.cboGroup1.value == oForm.cboGroup3.value) {
					alert("Las agrupaciones uno y tres son la misma.");
					oForm.cboGroup3.focus();
					return false;
				}
				if (oForm.cboGroup1.value != "" && oForm.cboGroup1.value == oForm.cboGroup4.value) {
					alert("Las agrupaciones uno y cuatro son la misma.");
					oForm.cboGroup4.focus();
					return false;
				}				
				if (oForm.cboGroup2.value != "" && oForm.cboGroup2.value == oForm.cboGroup3.value) {
					alert("Las agrupaciones dos y tres son la misma.");
					oForm.cboGroup3.focus();
					return false;
				}
				if (oForm.cboGroup2.value != "" && oForm.cboGroup2.value == oForm.cboGroup4.value) {
					alert("Las agrupaciones dos y cuatro son la misma.");
					oForm.cboGroup4.focus();
					return false;
				}
				if (oForm.cboGroup3.value != "" && oForm.cboGroup3.value == oForm.cboGroup4.value) {
					alert("Las agrupaciones tres y cuatro son la misma.");
					oForm.cboGroup4.focus();
					return false;
				}					
				if (oForm.cboGroup3.value == "" && oForm.cboGroup4.value != "") {
					oForm.cboGroup3.value = oForm.cboGroup4.value;
					oForm.cboGroup4.value = "";
				}
				if (oForm.cboGroup2.value == "" && oForm.cboGroup3.value != "") {
					oForm.cboGroup2.value = oForm.cboGroup3.value;
					oForm.cboGroup3.value = "";
				}
				return true;
			}
           
			//}
		//-->
			////USH 20230918
            function DownloadElement(url) {

                var file_path = url;
                var a = document.createElement('A');
                a.href = file_path;
                a.download = 'descarga';
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            };
        </script>

		
	</HEAD>
	<body onload="changeFrames();">
		<form id="frmEditor" method="post" runat="server">
			<asp:Image id="ImgCargando" style="display:none;margin: auto; " runat="server" Width="60" Height="60" src="../images/loading.gif" alt=""/>
			<TABLE id="Table1" class="standardPageTable" border="1" cellSpacing="0" cellPadding="0"
				width="100%">
				<TR>
					<TD class="applicationTitle" noWrap>Explorador de Correspondencia&nbsp;Enviada</TD>
					<TD class="applicationTitle" noWrap align="left"><IMG title="Cerrar y regresar a la página principal"  onclick="closeForm();"
							align="absMiddle" src="../Images/close_red.gif">&nbsp;&nbsp;
						<asp:label id="errLevel" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="3" noWrap>
						<TABLE class="fullScrollMenu" border="1" width="100%">
							<TR class="fullScrollMenuHeader">
								<TD style="HEIGHT: 29px" class="fullScrollMenuTitle" noWrap>Consulta de 
									Correspondencia</TD>
								<TD style="HEIGHT: 29px" class="fullScrollMenuTitle" colSpan="4" align="left"><asp:linkbutton id="lnkExecute" runat="server" Height="16px" OnClientClick="document.getElementById('ImgCargando').style.display = 'block';"  BorderStyle="None" CssClass="cmdSubmit">Ejecutar Consulta</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:linkbutton  OnClientClick="document.getElementById('ImgCargando').style.display = 'block';" id="lnkExcel" runat="server" Height="16px" BorderStyle="None" CssClass="cmdSubmit">Generar CSV</asp:linkbutton>
								</TD>
							</TR>
							<TR>
								<TD class="header-gray" vAlign="top" noWrap>Estatus Enviada</TD>
								<TD class="header-gray" vAlign="top" colSpan="2" noWrap><asp:dropdownlist id="dropShow" runat="server" CssClass="standard-text">
										<asp:ListItem Selected="True" Value="1">Pendientes por Turnar</asp:ListItem>
										<asp:ListItem Value="2">Turnados en Tr&#225;mite</asp:ListItem>
										<asp:ListItem Value="3">Turnados Concluidos</asp:ListItem>
										<asp:ListItem Value="4">Documentos Sin Tr&#225;mite</asp:ListItem>
										<asp:ListItem Value="5">Todos</asp:ListItem>
									</asp:dropdownlist>&nbsp;
									<asp:checkbox id="chkCC" runat="server" Checked="True" Text="Con Copia"></asp:checkbox><asp:checkbox id="chkTurnados" runat="server" Height="1px" Checked="True" Text="Turnados"></asp:checkbox></TD>
								<TD class="header-gray" vAlign="middle" noWrap>Estatus Turnada</TD>
								<TD class="header-gray" vAlign="middle" noWrap><asp:dropdownlist id="drpTurnada" runat="server" CssClass="standard-text">
										<asp:ListItem Value="1" Selected="True">Pendientes</asp:ListItem>
										<asp:ListItem Value="2">Concluidos</asp:ListItem>
										<asp:ListItem Value="4">Seguimiento</asp:ListItem>
										<asp:ListItem Value="3">Todos</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<tr style="DISPLAY: none">
								<TD style="HEIGHT: 46px" class="header-gray" vAlign="middle" noWrap>No. de Volante</TD>
								<TD style="HEIGHT: 46px" class="header-gray" vAlign="top" colSpan="2" noWrap><asp:textbox id="txtVolante" runat="server" MaxLength="6"></asp:textbox></TD>
								<TD style="HEIGHT: 46px" class="header-gray" vAlign="middle" colSpan="2" noWrap></TD>
							</tr>
							<TR>
								<TD class="header-gray" vAlign="middle" noWrap>Filtrar por:</TD>
								<TD class="standard-text" vAlign="top" colSpan="2"><asp:radiobuttonlist id="rblOrder" runat="server" Height="44px" Width="160px" RepeatColumns="2" TextAlign="Left">
										<asp:ListItem Value="Volante" Selected="True">Volante</asp:ListItem>
										<asp:ListItem Value="Area">Area</asp:ListItem>
									</asp:radiobuttonlist></TD>
								<TD class="header-gray" vAlign="middle" noWrap>Tipo Documento</TD>
								<td class="header-gray" vAlign="middle" noWrap><asp:dropdownlist id="drpDocumentType" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD style="HEIGHT: 48px" class="header-gray" vAlign="middle">Fecha de Elaboración</TD>
								<TD style="HEIGHT: 48px" class="header-gray" colSpan="2" noWrap>Del Día:
									<asp:textbox id="txtElaborationDateFrom" runat="server" CssClass="standard-text" Width="71px"></asp:textbox>&nbsp;&nbsp;
								</TD>
								<TD style="HEIGHT: 48px" class="header-gray" noWrap>Al Día:</TD>
								<TD style="HEIGHT: 48px" class="header-gray" noWrap><asp:textbox id="txtElaborationDateTo" runat="server" CssClass="standard-text" Width="71px"></asp:textbox>día 
									/ mes / año</TD>
							</TR>
							<TR>
								<TD class="header-gray" vAlign="middle" noWrap>Area</TD>
								<TD colSpan="4" noWrap><asp:dropdownlist id="dropArea" runat="server" Width="50%"></asp:dropdownlist><asp:textbox id="txtDate" runat="server" Width="64px"></asp:textbox><asp:button id="btnChangeArea" runat="server" Text="Ejecuta" Width="48px"></asp:button>Con 
									algún Texto
									<asp:textbox id="txtTextSearch" runat="server" Width="192px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD width="100%" colSpan="5" noWrap>
                        <% if (rblOrder.SelectedItem.Text == "Area") { %>
						<TABLE class="dataGrid" width="100%">
							<THEAD>
                                <%=gsHeader%>
							</THEAD>
                            <% if (gsBody.Length != 0 )
								{ %>							<%=gsBody%>							<% }else  { %>
							<TBODY>
								<TR>
									<TD colSpan="4"><b>No encontré ningún correo con el criterio de búsqueda proporcionado.</b></TD>
								</TR>
							</TBODY>
                            <% } %>
						</TABLE>
                        <% } else { %>

						<asp:datagrid id="grdItems" tabIndex="-1" runat="server" CssClass="dataGrid" Width="100%" AutoGenerateColumns="False"
							ShowFooter="False" AllowSorting="True" AllowPaging="False"  OnPageIndexChanged="Grid_Change"
							OnItemDataBound="dg_ItemDataBound" Height="16px">
							<FooterStyle Wrap="False" HorizontalAlign="Left" CssClass="totalsRow"></FooterStyle>
							<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
							<EditItemStyle VerticalAlign="Top"></EditItemStyle>
							<AlternatingItemStyle CssClass="evenRow" VerticalAlign="Top"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Left" CssClass="oddRow" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="headerRow"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:HyperLink Runat="server" Target=_blank ID="Hyperlink1" NavigateUrl='<%# "/gestion/correspondencia/mail_editor.aspx?id=" + DataBinder.Eval(Container.DataItem, "documento_id") +
													"&mailtype=" + DataBinder.Eval(Container.DataItem, "Tipo_Remitente") + "&action=" + DataBinder.Eval(Container.DataItem, "Tipo_Remitente") + "&querytype=M" + "&filter=" + dropArea.SelectedItem.Value + "," + dropShow.SelectedItem.Value + "," + txtElaborationDateFrom.Text + "," + txtElaborationDateTo.Text %>' ImageUrl="/gestion/images/icons_sm/document_sm.gif"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Volante">
									<ItemTemplate>
										<asp:Label id="lblVolante" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Volante")%>' Visible="True">
										</asp:Label>
										<asp:Label id="lblAddresseeId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Destinatario_Cve_Area")%>' Visible=False/>
										<asp:Label id="lblAddressee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DestinatarioArea")%>' Visible=False/>
										<asp:Label id="lblDocumentBisId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Documento_Bis_Id")%>' Visible=False/>
										<asp:Label id="lblTurnArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TurnadoArea")%>' Visible=False/>
										<asp:Label id="lblDocumentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "documento_id")%>' Visible=False/>
										<asp:Label id="lblCutDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fecha_corte")%>' Visible=False/>
										<asp:Label id="lblElaborationDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Fecha_Elaboracion")%>' Visible=False/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Referencia">
									<ItemTemplate>
										<asp:Label id="lblReference" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Referencia").ToString()%>'/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Fecha_Elaboracion" HeaderText="Fecha Registro"></asp:BoundColumn>
								<asp:BoundColumn DataField="Fecha_Documento_Fuente" HeaderText="Fecha Documento" DataFormatString="{0:d}"></asp:BoundColumn>
								<asp:BoundColumn DataField="RemitenteArea" HeaderText="Remitente"></asp:BoundColumn>
								<asp:BoundColumn DataField="Asunto" HeaderText="Asunto"></asp:BoundColumn>
								<asp:BoundColumn DataField="Resumen" HeaderText="Resumen"></asp:BoundColumn>
								<asp:BoundColumn DataField="StatusVolante" HeaderText="Estatus"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Días">
									<ItemTemplate>
										<asp:Label id="lblDays" runat="server" Text='' />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
                        <% } %>
					</TD>
				</TR>
			</TABLE>
			
		
 

		</form>
	</body>
</HTML>
