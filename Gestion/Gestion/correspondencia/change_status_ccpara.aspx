<%@ Page language="c#" Codebehind="change_status_ccpara.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.change_status_ccpara" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Correspondencia con Copia</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function Alternate_Mail()
		{
			window.close();
			window.opener.document.all.lnkExecute.click();
			return false;
			
		}
		</script>
	</HEAD>
	<body onload="Alternate_Mail();">
		<form id="change_status_ccpara" method="post" runat="server">
			<TABLE class="standardPageTable" id="Table1" cellSpacing="1" cellPadding="1" width="100%"
				border="0">
				<TR>
					<TD class="applicationTitle" noWrap>Editor de Estatus&nbsp;</TD>
					<TD align="right" colSpan="3" class="applicationTitle"><IMG title="Cerrar y regresar a la página principal" onclick="window.location.href = '/gestion/portal/main.aspx'; "
							src="/Gestion/images/close_red.gif" align="absMiddle">
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE class="applicationTable" width="100%" border="1">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle" style="HEIGHT: 33px" vAlign="middle">Modifica 
									Estado Documentos&nbsp;Recibidos
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
