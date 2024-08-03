<%@ Page language="c#" Codebehind="ccpara_mail.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.frmCcParaMail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ccpara_mail</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		   
		   function callCreateTurnar(){
		   
		        var documentoId, turnarId, sUrl;
		        turnarId = '<%= Request["turnadoid"]%>';
		        documentoId = '<%= Request["documentoid"]%>';
		        tipoturno  = '<%= Request["tipoturno"]%>';
		        window.close();

		        sUrl = 'create_turnar.aspx?turnadoid=' + turnarId + '&documentoid=' + documentoId + '&tipoturno=' + tipoturno;
				window.open(sUrl,'_blank');
				return false;
		   }
		   
		   function callDisplay(){

				documentoId = '<%= Request["documentoid"]%>';
				sFile = "/Gestion/Correspondencia/document_display.aspx?id=" + documentoId + "&sender=TURN" ;

				window.open(sFile,'_blank');
				return false;
		   }
		   function viewDocument(){

				alternateId = '<%= Request["turnadoid"] %>';
				documentId = '<%= Request["documentoid"] %>';
				sFile = "/Gestion/Correspondencia/ccpara_reply.aspx?id=" + documentId + "&turnadoid=" + alternateId + "&state=3";
				window.open(sFile,'_blank');
				return false;
		   }

		   function viewAttach(sFile){

				window.open(sFile,'_blank');
				return false;
		   }

		function RefreshWindow(){
		
			window.opener.document.all.lnkExecute.click();
			window.close();
			return false;
		}


		</script>
	</HEAD>
	<body>
		<form id="frmCcParaMail" method="post" runat="server">
			<TABLE class="standardPageTable" id="Table1" cellSpacing="1" cellPadding="1" width="50%"
				border="0">
				<TR>
					<TD class="applicationTitle" noWrap>Recibir Correspondencia&nbsp;</TD>
					<TD align="right" colSpan="3" class="applicationtitle">
						<IMG title="Cerrar y regresar a la página principal" onclick="RefreshWindow();" alt="Cerrar"
							src="../images/close_red.gif" align="absMiddle">
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4" style="HEIGHT: 55px">
						<TABLE class="fullScrollMenu" width="100%" border="1" cellSpacing="1" cellPadding="1">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle" style="HEIGHT: 33px" vAlign="middle">Que desea 
									hacer
								</TD>
							</TR>
							<% if (Request["status"] == "0") { %>
							<TR>
								<TD colSpan="4" class="header-gray" align="center" height="33">
									<a href='change_status_ccpara.aspx?turnadoid=<%= Request["turnadoid"] + "&amp;state=1&amp;documentoid=" + Request["documentoid"]%>'>
										Acuse de Recibo</a>
								</TD>
							</TR>
							<% } %>
							<TR>
								<TD colSpan="4" class="header-gray" align="center" height="33">
									<a href="" onclick="return(viewDocument());">Responder el Volante</a></TD>
							</TR>
							<% if (Request["status"] == "1" || Request["status"] == "0") { %>
							<TR>
								<TD colSpan="4" class="header-gray" align="center" height="33">
									<a href="" onclick="return(callCreateTurnar());">Turnar&nbsp;el Volante</a></TD>
							</TR>
							<% } %>
							<TR>
								<TD class="header-gray" align="center" colSpan="4" height="33">
									<a href="" onclick="return(callDisplay());">Ver el Documento</a></TD>
							</TR>
							<TR>
								<TD colSpan="4" class="header-gray" align="center" height="33">Archivos Adjuntos</TD>
							</TR>
							<%= createAttach(Convert.ToInt32(Request["documentoid"].ToString())) %>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
