<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SCGD._default" %>
<%@ Register TagPrefix="uc1" TagName="SignIn" Src="SignIn.ascx" %>
<%@ Register TagPrefix="TimeTrack" TagName="Banner" Src="Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title>Control de Gestión</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="styles.css" type="text/css" rel="stylesheet">
</head>
<body>
  <form id="Reports" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td valign="top" background="images/bars.gif" height="46">
                    <TimeTrack:Banner ID="Banner1" runat="server"></TimeTrack:Banner>
                </td>
            </tr>
            <tr>
                <td class="tab-active" valign="top" height="15">
                    <img height="15" src="images/spacer.gif" width="15"></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr width="100%">
                <td width="8">
                    <img height="8" src="images/spacer.gif" width="8"></td>
                <td valign="top">
                    <table class="admin-tan-border" height="530" cellspacing="0" cellpadding="0" width="100%"
                        border="0">
                        <tr>
                            <td>
                                <img height="16" src="images/spacer.gif"></td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="images/spacer.gif" width="16"></td>
                            <td>
                                <uc1:SignIn ID="SignIn1" runat="server"></uc1:SignIn>
                            </td>
                        </tr>
                        <tr valign="bottom">
                            <td width="14">&nbsp;</td>
                            <td>
                                <img src="images/spacer.gif" width="8"></td>
                        </tr>
                    </table>
                </td>
                <td width="11">
                    <img height="11" src="images/spacer.gif" width="11"></td>
            </tr>
            <tr>
                <td valign="top" colspan="5" height="15">
                    <img height="15" src="images/spacer.gif" width="15"></td>
            </tr>
        </table>
    </form>
</body>
</html>
