<%@ Page language="c#" Codebehind="edit_puestos.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_puestos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Vista de Puestos</title>
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
					<TD class="titleRow" style="HEIGHT: 23px" noWrap>Edición&nbsp;de Areas</TD>
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
							<tr>
								<td style="WIDTH: 113px; HEIGHT: 12px" vAlign="top" noWrap>Clave del puesto:
								</td>
								<td style="HEIGHT: 12px" vAlign="top" noWrap><INPUT id="txtClaveArea" style="WIDTH: 72px; HEIGHT: 20px" size="6" name="txtClaveArea"
										runat="server" maxLength="6">
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 113px; HEIGHT: 22px" vAlign="top" noWrap>Puesto:
								</td>
								<td vAlign="top" noWrap>
									<INPUT style="WIDTH: 392px; HEIGHT: 20px" id="txtArea" size="60" name="txtArea" runat="server"
										maxLength="250"> <INPUT style="Z-INDEX: 0; WIDTH: 72px; HEIGHT: 20px" id="txtCutDate" size="6" name="txtCutDate"
										runat="server"><br>
									<DIV id="lblArea" style="WIDTH: 64px; DISPLAY: inline; HEIGHT: 19px" runat="server" ms_positioning="FlowLayout"></DIV>
								</td>
							</tr>
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
