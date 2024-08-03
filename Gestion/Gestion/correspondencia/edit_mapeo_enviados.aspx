<%@ Page language="c#" Codebehind="edit_mapeo_enviados.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_mapeo_enviados" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Vista de Enviados</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
		<script language="javascript">
		
			var bSended = false;
			var oViewer = null;
			
			function doOperation(operationTag) {
			    document.getElementById("frmEditor").hdnOperationTag.value = operationTag;
					switch (operationTag) {
					case "Update":
						if (validate()) {
							document.body.style.cursor='wait';
							document.getElementById("frmEditor").submit();
						}
						break;
					case "Remove":
						if (validate()) {
							document.body.style.cursor='wait';
							document.getElementById("frmEditor").submit();
						}
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
           
        </script>
	</HEAD>
	<body oncontextmenu="return true;" MS_POSITIONING="GridLayout">
		<form id="frmEditor" method="post" runat="server">
			<TABLE height="100%" width="100%">
				<TR>
					<TD class="titleRow" style="HEIGHT: 23px" noWrap>Edición&nbsp;Correo Enviado</TD>
					<td style="HEIGHT: 23px" noWrap><A href="javascript:doOperation('Update');"><IMG hspace="6" src="/gestion/images/detail.gif" align="absMiddle">Crear
						</A>&nbsp;&nbsp;|&nbsp;&nbsp; <A href="javascript:doOperation('Remove')"><IMG hspace="6" src="/gestion/images/icon-delete.gif" align="absMiddle"></A><A href="javascript:doOperation('Remove')">&nbsp;<SPAN id="spnRemoveBtnText">Eliminar
							</SPAN></A>
					<td style="HEIGHT: 23px" align="right"><IMG title="Cerrar y regresar a la página principal" onclick="CloseWindow();" alt="Cerrar"
							src="/Gestion/images/close_red.gif" align="absMiddle">
					</td>
				</TR>
				<TR>
					<td vAlign="top" noWrap width="100%" colSpan="3" height="100%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap>id del empleado</TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap><INPUT style="Z-INDEX: 0; WIDTH: 72px; HEIGHT: 20px" id="txtEmpleadoId" size="6" name="txtEmpleadoId"
										runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 21px" vAlign="top" noWrap>Nombre</TD>
								<TD style="HEIGHT: 21px" vAlign="top" noWrap><INPUT style="Z-INDEX: 0; WIDTH: 456px; HEIGHT: 20px" id="txtName" size="70" name="txtName"
										runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap>id del Destinatario</TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap><INPUT style="Z-INDEX: 0; WIDTH: 72px; HEIGHT: 20px" id="txtAddrId" size="6" name="txtAddrId"
										runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap>Destinatario</TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap><INPUT style="Z-INDEX: 0; WIDTH: 456px; HEIGHT: 20px" id="txtAddr" size="70" name="txtAddr"
										runat="server"></TD>
							</TR>
							<tr>
								<td style="WIDTH: 113px; HEIGHT: 45px" vAlign="top" noWrap>Area:
								</td>
								<td style="HEIGHT: 45px" vAlign="top" noWrap><SELECT id="cboArea" style="WIDTH: 400px" tabIndex="6" name="cboArea" runat="server">
									</SELECT><INPUT id="txtCutDate" style="WIDTH: 72px; HEIGHT: 20px" size="6" name="txtExpediente"
										runat="server"><br>
									<DIV id="lblArea" style="WIDTH: 64px; DISPLAY: inline; HEIGHT: 19px" runat="server" ms_positioning="FlowLayout"></DIV>
								</td>
							</tr>
							<% if (Request.QueryString["id"].ToString().Length == 0 ) {	%>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap>Usuario</TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap><SELECT style="Z-INDEX: 0; WIDTH: 400px" id="cboEmployee" tabIndex="6" name="cboEmployee"
										runat="server"></SELECT></TD>
							</TR>
							<% } %>
							<TR>
								<TD style="WIDTH: 113px; HEIGHT: 24px" vAlign="top" noWrap>Destinatario:
								</TD>
								<td style="HEIGHT: 24px" vAlign="top" noWrap><SELECT id="cboAddressee" style="WIDTH: 400px" tabIndex="6" name="cboAddressee" runat="server">
									</SELECT><br>
									<DIV id="lblPuesto" style="WIDTH: 64px; DISPLAY: inline; HEIGHT: 19px" runat="server"
										ms_positioning="FlowLayout"></DIV>
								</td>
							</TR>
						</table>
						<INPUT id="hdnFromDate" type="hidden" name="hdnFromDate" runat="server" style="WIDTH: 32px; HEIGHT: 20px"
							size="1"><INPUT id="hdnToDate" type="hidden" name="hdnToDate" runat="server" style="WIDTH: 32px; HEIGHT: 20px"
							size="1"><INPUT id="hdnAreaId" style="WIDTH: 32px; HEIGHT: 20px" type="hidden" size="1" name="hdnToDate"
							runat="server"><INPUT id="hdnPuestoId" style="WIDTH: 32px; HEIGHT: 20px" type="hidden" size="1" name="hdnToDate"
							runat="server"><INPUT id="hdnTipoEmpleado" style="WIDTH: 32px; HEIGHT: 20px" type="hidden" size="1" name="hdnToDate"
							runat="server"></td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
