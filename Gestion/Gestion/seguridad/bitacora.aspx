<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bitacora.aspx.cs" Inherits="Gestion.gestion.seguridad.bitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>Bitacora</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../css/worksheet.css" type="text/css" rel="stylesheet">
    <script src="../script.js"></script>
	<%--<script language="javascript" src="/gestion/scripts/script.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-3.7.1.slim.js" integrity="sha256-UgvvN8vBkgO0luPSUl2s8TIlOSYRoGFAX4jlCIm9Adc=" crossorigin="anonymous"></script>--%>    
    <script type='text/javascript'>
        var oViewer = null;
        //$(document).ready(function () {
        //    $('[id*=myModal]').modal({});
        //});
        function openModal(title, value) {
            //$(".modal-title").text(title);
            //$("#div-sentence").text(value.replaceAll('$$', '"'));
            //$('[id*=myModal]').modal('show');
            alert(title + " \n " + value.replaceAll('$$', '"'));
        }
        function doOperation() {
            var sOptions = "scrollbars=no,fullscreen=no,location=no,resizable=yes,menubar=no,height=540px,width=1100px";
            var sURL = "metadata.aspx";
            
            if (oViewer == null || oViewer.closed) {
                oViewer = window.open(sURL, "_blank", sOptions);
            } else {
                oViewer.focus();
                oViewer.navigate(sURL);
            }
        }
        function closeModal() {
            $('[id*=myModal]').modal('hide');
        }
        function run() {
            //__doPostBack('btnExportar2', 'OnClick');
            var clickButton = document.getElementById("<%= btnExportar2.ClientID %>");
            clickButton.click();
        }
        function validate() {
            return true;
        }
        function CloseWindow() {
            self.close();
        }
    </script>
    <style>
        .form-search {
            display:flex !important;
        }
        .btn {
            height:20px !important;
        }
        .form-control {
               height:20px !important;
        }
    </style>
</head>
<body oncontextmenu="return true;" MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

			<table height="100%" width="100%">
				<tr>
					<td class="titleRow" style="HEIGHT: 27px" noWrap>Bitacora</td>
					<td style="HEIGHT: 27px" noWrap>
                       <div class="row col-md-12">
                            <div class="row col-md-6 form-search" >
                                <div class="row col-md-12" style="width:300px">
                                    <div class="mb-3 row">
                                        <label for="cboUsuario" class="col-sm-2 col-form-label">Usuario</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboUsuario" style="width:100%" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                    <div class="mb-3 row">
                                        <label for="txtPantalla" class="col-sm-2 col-form-label">Pantalla</label>
                                        <div class="col-sm-10">
                                            <%--<asp:DropDownList ID="cboPantalla" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtPantalla" CssClass="form-control" ToolTip="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                    <div class="mb-3 row">
                                        <label for="cboAccion" class="col-sm-2 col-form-label">Accion</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="cboAccion" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                </div>
                                <div class="row col-md-12" style="text-align: center; display: block; margin: 10px;">                            
                                </div>
                            </div>

                            <div class="row col-md-6 form-search">
                                <div class="row col-md-12" style="width:300px">
                                    <div class="mb-3 row">
                                        <label for="txtTabla" class="col-sm-2 col-form-label">Tabla</label>
                                        <div class="col-sm-10">
                                            <%--<asp:DropDownList ID="cboTabla" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtTabla" style="width:100%" CssClass="form-control" ToolTip="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                    <div class="mb-3 row">
                                        <label for="txtDesde" class="col-sm-2 col-form-label">Desde</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtDesde" CssClass="form-control" ToolTip="dd/mm/aaaa" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                    <div class="mb-3 row">
                                        <label for="txtHasta" class="col-sm-2 col-form-label">Hasta</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtHasta" CssClass="form-control" ToolTip="dd/mm/aaaa" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12" style="text-align: center; display: block; margin: 10px;">
                                    <asp:ImageButton ID="bntLimpiar" ImageUrl="../images/workflow/deleted.gif" CssClass="btn btn-danger" runat="server" Text="Limpiar" OnClick="bntLimpiar_Click" />
                                    <asp:ImageButton ID="btnBuscar" ImageUrl="../images/detail.gif" CssClass="btn btn-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                    <asp:ImageButton ImageUrl="../images/download.gif" ID="btnExportar" runat="server" Text="Exportar" CssClass="btn btn-info" OnClick="btnExportar_Click" />
                                </div>
                            </div>
                        </div>
					</td>					
					<td style="HEIGHT: 27px" align="right"><IMG title="Cerrar y regresar a la página principal" onclick="window.close();"
							alt="Cerrar" src="/Gestion/images/close_red.gif" align="absMiddle">
					</td>
				</tr>
				<tr>
					<td noWrap width="100%" colSpan="3" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td vAlign="top" width="100%">
									<div style="WIDTH: 100%; HEIGHT: 400px; OVERFLOW: auto">
										<div class="row col-md-12">
                                            <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="dataGrid"
                                                DataKeyNames="LOGID,SENTENCE"
                                                AllowSorting="True"
                                                OnSorting="gvResultado_Sorting"
                                                OnRowCommand="gvResultado_RowCommand"
                                                OnRowDataBound="gvResultado_RowDataBound">
                                                <HeaderStyle CssClass="headerRow" />
                                                <AlternatingRowStyle CssClass="evenRow"    />
                                                <RowStyle CssClass="oddRow" />
                                                <Columns>
                                                    <asp:BoundField DataField="LOGID" HeaderText="Clave" SortExpression="LOGID"></asp:BoundField>
                                                    <asp:BoundField DataField="USER" HeaderText="Usuario" SortExpression="USERID"></asp:BoundField>
                                                    <asp:BoundField DataField="DATETIMEEVENT" HeaderText="Fecha Evento" SortExpression="DATETIMEEVENT"></asp:BoundField>
                                                    <asp:BoundField DataField="MODULE" HeaderText="Pantalla" SortExpression="MODULE"></asp:BoundField>
                                                    <asp:BoundField DataField="ACTION" HeaderText="Accion" SortExpression="ACTION"></asp:BoundField>
                                                    <asp:BoundField DataField="ISDB" HeaderText="Es BD" SortExpression="ISDB"></asp:BoundField>
                                                    <asp:BoundField DataField="TABLE" HeaderText="Tabla" SortExpression="TABLE"></asp:BoundField>
                                                    <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Detalle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="../images/icons_sm/document_sm.gif" ID="btnDetail" runat="server" Text="Ver" CommandName="Detail" CssClass="btn btn-info" CommandArgument='<%# Eval("LOGID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sentencia">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="../images/icons_sm/document_sm.gif" ID="btnSelect" runat="server" Text="Ver" CommandName="Select" CssClass="btn btn-info" CommandArgument='<%# Eval("SENTENCE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                            
                                                </Columns>
                                            </asp:GridView>
                                        </div>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
                
            </ContentTemplate>              
        </asp:UpdatePanel>
        <asp:Button ID="btnExportar2" runat="server" Text="" OnClick="btnExportar2_Click" Width="1px" Height="1px" BorderColor="White"  BackColor="White" BorderWidth="0px" ForeColor="White" />
        
    </form>




</body>
</html>
