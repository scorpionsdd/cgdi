<%@ Page language="c#" Codebehind="edit_regla.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_regla" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Vista de Empleados</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/css/worksheet.css">
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
			<TABLE width="100%" height="100%">
				<TR>
					<TD style="HEIGHT: 22px" class="titleRow" noWrap>Editor&nbsp;de Reglas</TD>
					<td style="HEIGHT: 22px" noWrap><A href="javascript:doOperation('Update');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif">Crear
						</A>&nbsp;&nbsp;|&nbsp;&nbsp; <A href="javascript:doOperation('Remove')"><IMG hspace="6" align="absMiddle" src="/gestion/images/icon-delete.gif"></A><A href="javascript:doOperation('Remove')">&nbsp;<SPAN id="spnRemoveBtnText">Eliminar
							</SPAN></A>
					<TD noWrap>Fecha de Vigencia:</TD>
					<td style="HEIGHT: 22px"><INPUT style="WIDTH: 72px; HEIGHT: 20px" id="txtCutDate" maxLength="10" size="6" name="txtCutDate"
							runat="server">
					</td>
					<td style="HEIGHT: 22px"><IMG title="Cerrar y regresar a la página principal" onclick="CloseWindow();" alt="Cerrar"
							align="absMiddle" src="/Gestion/images/close_red.gif">
					</td>
				</TR>
				<TR>
					<td height="100%" vAlign="top" width="100%" colSpan="5" noWrap>
						<table style="WIDTH: 1046px; HEIGHT: 378px" border="0" cellSpacing="0" cellPadding="0"
							width="1046">
							<tr>
								<td style="WIDTH: 160px; HEIGHT: 20px" vAlign="top" noWrap>Nombre:
								</td>
								<td style="HEIGHT: 20px" vAlign="middle" noWrap>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblNombre" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</td>
								<TD style="HEIGHT: 20px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 20px" vAlign="top" noWrap></TD>
							</tr>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 20px" vAlign="top" noWrap>Usuario</TD>
								<TD style="HEIGHT: 20px" vAlign="middle" noWrap><SELECT style="Z-INDEX: 0; WIDTH: 400px" id="cboEmployee" tabIndex="6" name="cboEmployee"
										runat="server"></SELECT></TD>
								<TD style="HEIGHT: 20px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 20px" vAlign="top" noWrap></TD>
							</TR>
							<tr>
								<td style="WIDTH: 160px; HEIGHT: 14px" vAlign="middle" noWrap>Remitente:
								</td>
								<td style="HEIGHT: 14px" vAlign="middle" noWrap><SELECT style="WIDTH: 400px" id="cboRemitenteNombre" tabIndex="6" name="cboRemitenteNombre"
										runat="server"></SELECT><SELECT style="WIDTH: 400px" id="cboRemitenteArea" tabIndex="6" name="cboRemitenteArea"
										runat="server"></SELECT><BR>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblRemitenteNombre" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
									&nbsp; /&nbsp;
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblRemitenteArea" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</td>
								<TD style="HEIGHT: 14px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 14px" vAlign="top" noWrap></TD>
							</tr>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 14px" vAlign="middle" noWrap>Destinatario:</TD>
								<TD style="HEIGHT: 14px" vAlign="middle" noWrap><SELECT style="WIDTH: 400px" id="cboDestinatarioNombre" tabIndex="6" name="cboDestinatarioNombre"
										runat="server"></SELECT><SELECT style="WIDTH: 400px" id="cboDestinatarioArea" tabIndex="6" name="cboDestinatarioArea"
										runat="server"></SELECT><BR>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblDestinatarioNombre" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
									&nbsp; /&nbsp;
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblDestinatarioArea" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</TD>
								<TD style="HEIGHT: 14px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 14px" vAlign="top" noWrap></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 14px" vAlign="middle" noWrap>Firma:</TD>
								<TD style="HEIGHT: 14px" vAlign="middle" noWrap><SELECT style="WIDTH: 400px" id="cboSign" tabIndex="6" name="cboSign" runat="server"></SELECT><BR>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblSign" runat="server" ms_positioning="FlowLayout">Label</DIV>
								</TD>
							</TR>
							<tr>
								<td style="WIDTH: 160px; HEIGHT: 22px" vAlign="middle" noWrap>Folio:
								</td>
								<td style="HEIGHT: 22px" vAlign="middle" noWrap><SELECT style="WIDTH: 400px" id="cboFolioArea" tabIndex="6" name="cboFolioArea" runat="server"></SELECT><br>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblFolio" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</td>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 22px" vAlign="top" noWrap></TD>
							</tr>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 47px" vAlign="middle" noWrap>Los&nbsp;Tipo 
									Documento&nbsp;son por Empleado?
								</TD>
								<td style="HEIGHT: 47px" vAlign="middle" noWrap><br>
									<INPUT id="chkDocumentoTipo" type="checkbox" runat="server" name="chkDocumentoTipo" CHECKED>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<SELECT style="WIDTH: 350px" id="cboTipoDocUser" tabIndex="6" name="cboArea" runat="server">
									</SELECT><SELECT style="WIDTH: 350px" id="cboTipoDocArea" tabIndex="6" name="cboArea" runat="server"></SELECT><BR>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblTipoDocumento" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</td>
							</TR>
							<tr>
								<td style="WIDTH: 160px; HEIGHT: 22px" vAlign="middle" noWrap>Los&nbsp;Remitentes 
									Externos son&nbsp;por Empleado?
								</td>
								<td style="HEIGHT: 18px" vAlign="middle" noWrap>
									<INPUT id="chkRemExtCat" type="checkbox" name="chkRemExtCat" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<SELECT style="WIDTH: 350px" id="cboRemExtUser" tabIndex="6" name="cboRemExtUser" runat="server">
									</SELECT><SELECT style="WIDTH: 350px" id="cboRemExtArea" tabIndex="6" name="cboArea" runat="server"></SELECT><BR>
									<DIV style="WIDTH: 70px; DISPLAY: inline; HEIGHT: 15px" id="lblRemExt" runat="server"
										ms_positioning="FlowLayout">Label</DIV>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 160px; HEIGHT: 35px" vAlign="middle" noWrap>Al imprimir el 
									volante cambia de Pendiente a Trámite?
								</td>
								<td style="HEIGHT: 35px" vAlign="middle" noWrap>
									&nbsp;<INPUT id="chkCambiaStatus" type="checkbox" name="chkCambiaStatus" runat="server">
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Seguimiento Externo?&nbsp;<INPUT id="chkSeguimiento" type="checkbox" name="chkSeguimiento" runat="server">
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;Usa&nbsp;Catálogo 
									Remitente Externo&nbsp;<INPUT id="chkRemitenteExterno" type="checkbox" name="chkRemitenteExterno" runat="server">
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;Usa Catálogo Tipo 
									Documento?&nbsp;<INPUT id="chkTipoDocumento" type="checkbox" name="chkTipoDocumento" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 26px" vAlign="middle" noWrap>Remitente Incluye 
									Operativos?&nbsp;</TD>
								<TD style="HEIGHT: 26px" vAlign="middle" noWrap>&nbsp;<INPUT id="chkRemIncluyeOper" type="checkbox" name="chkRemIncluyeOper" runat="server">&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Al Turnar Despliega 
									Operativos?&nbsp;<INPUT id="chkTurArbol" type="checkbox" name="chkTurArbol" runat="server">
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp; Confirna Concluir, 
									el Volante?&nbsp;<INPUT id="chkConfConcluir" type="checkbox" name="chkConfConcluir" runat="server">&nbsp;&nbsp;</TD>
								<TD style="HEIGHT: 26px" vAlign="top" noWrap></TD>
								<TD style="HEIGHT: 26px" vAlign="top" noWrap></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 160px; HEIGHT: 22px" vAlign="middle" noWrap>Se concluye un 
									volante al acusar de recibo&nbsp;un&nbsp;turno con copia</TD>
								<TD style="HEIGHT: 18px" vAlign="middle" noWrap>
									&nbsp;<INPUT id="chkConcluirAcuseCcpara" type="checkbox" name="chkConcluirAcuseCcpara" runat="server">
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;&nbsp; La 
									Respuesta es en Cascada?&nbsp;<INPUT id="chkResponderCascada" type="checkbox" name="chkResponderCascada" runat="server">&nbsp;</TD>
							</TR>
						</table>
						<INPUT id="hdnFromDate" type="hidden" name="hdnFromDate" runat="server" style="WIDTH: 40px; HEIGHT: 20px"
							size="1"> <INPUT id="hdnToDate" type="hidden" name="hdnToDate" runat="server" style="WIDTH: 8px; HEIGHT: 20px"
							size="1"> <INPUT id="hdnAreaId" type="hidden" name="hdnToDate" runat="server" style="WIDTH: 24px; HEIGHT: 20px"
							size="1"> <INPUT id="hdnPuestoId" type="hidden" name="hdnToDate" runat="server" style="WIDTH: 16px; HEIGHT: 20px"
							size="1"> <INPUT id="hdnTipoEmpleado" type="hidden" name="hdnToDate" runat="server" style="WIDTH: 16px; HEIGHT: 20px"
							size="1"> <INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnRemNameId" size="1" type="hidden"
							name="hdnToDate" runat="server"> <INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnRemAreaId" size="1" type="hidden"
							name="hdnToDate" runat="server"> <INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnDestNameId" size="1" type="hidden"
							name="hdnToDate" runat="server"> <INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnDestAreaId" size="1" type="hidden"
							name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnSignId" size="1" type="hidden"
							name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnFolioId" size="1" type="hidden"
							name="hdnToDate" runat="server"> <INPUT id="hdnEmployeeId" type="hidden" name="hdnEmployeeId" runat="server" style="Z-INDEX: 0; WIDTH: 24px; HEIGHT: 20px"
							size="1"> <INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnTipoDocumentoArea" size="1"
							type="hidden" name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnTipoDocumentoEmpleado" size="1"
							type="hidden" name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="hdnRemitenteExternoId" size="1"
							type="hidden" name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="Hidden11" size="1" type="hidden"
							name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="Hidden12" size="1" type="hidden"
							name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="Hidden13" size="1" type="hidden"
							name="hdnToDate" runat="server"><INPUT style="Z-INDEX: 0; WIDTH: 32px; HEIGHT: 20px" id="Hidden14" size="1" type="hidden"
							name="hdnToDate" runat="server"></td>
					<TD height="100%" vAlign="top" width="100%" noWrap></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
