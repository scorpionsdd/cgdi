<%@ Page CodeBehind="main.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Gestion.Portal.main" %>
<HTML>
	<HEAD>
		<title>Banobras - Intranet corporativa</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/main_page.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function callCalendario(){
			window.open("/Gestion/Utilidades/Calendario.html", null, "left=300,top=200,height=200,width=280,location=0,resizable=0");
			return false;
		}
		function callCalculadora(){
			window.open("/Gestion/Utilidades/calculadora.html", null, "left=300,top=200,height=200,width=300,location=0,resizable=0");
			return false;
		}
		</script>
	</HEAD>
	<body leftMargin="1" MS_POSITIONING="FlowLayout">
		<!--BEGIN_TABLA_PRINCIPAL-->
		<table cellSpacing="0" cellPadding="0" width="768" align="center" border="1">
			<tr>
				<!--BEGIN_COLUMNA_2-->
				<td vAlign="top" bgColor="white" rowSpan="3">
					<table cellSpacing="3" cellPadding="0" border="0">
						<!--BEGIN_DOCUMENTOS-->
						<TR>
							<td>
								<table cellSpacing="3" cellPadding="3" border="0">
									<TR>
										<td>
											<table cellSpacing="0" cellPadding="0" border="0">
												<tr>
													<td>
														<table cellSpacing="0" cellPadding="0" border="0">
															<tr>
																<td align="center"><A class="newsTitle">Sistema de Control de Gestión Documental</A>
																</td>
															</tr>
															<tr>
																<td>&nbsp;</td>
															</tr>
															<tr>
																<td>
																	<TABLE class="pollTable" cellSpacing="0" cellPadding="9" width="780" border="0">
																		<%=gsTasksQuickBar%>
																	</TABLE>
																</td>
															</tr>
															<tr>
																<td class="newsAbstract">
																	<P align="center"></P>
																	<P><br>
																		<br>
																		<i>El equipo de desarrollo de</i> <b>La Subdirección de Sistemas de Información</b>
																		<br>
																		<B><SPAN style="FONT-FAMILY: 'Tahoma','sans-serif'; COLOR: black; FONT-SIZE: 9pt; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: ES-MX; mso-fareast-language: ES-MX; mso-bidi-language: AR-SA">
																				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
																				Subgerencia de Sistemas Fiduciarios</SPAN></B></P>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
									</TR>
								</table>
							</td>
						</TR>
						<!--END_DOCUMENTOS-->
						<TR height="20">
							<td>
								<!--BEGIN_BANNER_SERVICIOS-->
								<table class="copyright" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>Desarrollado&nbsp;por el Banco Nacional de Obras y Servicios Públicos, S.N.C.</TD>
									</TR>
									<TR>
										<TD><A href="http://www.banobras.gob.mx/" target="_blank">Banobras © México 2004. Banco 
												Nacional del Obras, S.C. Todos los derechos reservados.</A></TD>
									</TR>
								</table>
								<!--END_TABLA_SERVICIOS--></td>
						</TR>
					</table>
				</td>
				<!--END_COLUMNA_2--></tr>
		</table>
	</body>
</HTML>
