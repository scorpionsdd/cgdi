<%@ Page language="c#" Codebehind="admin.aspx.cs" AutoEventWireup="false" Inherits="Gestion.admin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>admin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/css/worksheet.css">
        <script type="text/javascript" src="scripts/script.js"></script>
		<script type="text/javascript">
			var bSended = false;
			var oViewer = null;
			function doOperation(operationTag) {
			    document.getElementById("frmEditor").hdnOperationTag.value = operationTag;

			    switch (operationTag) {
					case "Participants":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_empleados.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "Rules":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_regla.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "MappingReceived":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_mapeo_recibidos.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "MappingSend":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_mapeo_enviados.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;

					case "Areas":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_areas.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
						
					case "Puestos":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_puestos.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "Roles":
						if (!bSended) {
							var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
							var sURL = "Correspondencia/view_roles.aspx";
							if (oViewer == null || oViewer.closed) {
								oViewer = window.open(sURL, "_blank", sOptions);
							} else {
								oViewer.focus();
								oViewer.navigate(sURL);
							}
						}
						break;
					case "Bitacora":
						if (!bSended) {
                            var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
                            var sURL = "seguridad/bitacora.aspx";
                            if (oViewer == null || oViewer.closed) {
                                oViewer = window.open(sURL, "_blank", sOptions);
                            } else {
                                oViewer.focus();
                                oViewer.navigate(sURL);
                            }
                        }
                        break;
				}
			}
			
            function CloseWindow()
            {
                self.close();
            }
           
        </script>
	</HEAD>
	<body>
		<form id="frmEditor" method="post" runat="server">
			<% if (administrator != "NULL") { %>
			<TABLE style="WIDTH: 504px; HEIGHT: 77px" border="1" cellSpacing="1" cellPadding="1" width="504">
				<TR>
					<TD style="HEIGHT: 31px">
						<H5>Administración Control de Gestión Documental</H5>
					</TD>
				</TR>
				<tr>
					<td>
                        <A href="javascript:doOperation('Puestos');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Puestos
						</A>
					</>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('Areas');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Areas
						</A>
					</td>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('Participants');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Empleados
						</A>
					</td>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('Rules');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Reglas
						</A>
					</td>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('MappingReceived');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Mapeo 
							Recibidos </A>
					</td>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('MappingSend');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Mapeo 
							Enviados </A>
					</td>
				</tr>
				<tr>
					<td><A href="javascript:doOperation('Roles');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Roles
						</A>
					</td>
				</tr>
							<tr>
				<td><A href="javascript:doOperation('Bitacora');"><IMG hspace="6" align="absMiddle" src="/gestion/images/detail.gif" width="14" height="14">Bitacora
					</A>
				</td>
			</tr>
			</TABLE>
			<% } %>
		</form>
	</body>
</HTML>
