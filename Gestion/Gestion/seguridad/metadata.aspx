<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="metadata.aspx.cs" Inherits="Gestion.gestion.seguridad.metadata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle</title>
    <style>
        body {
            font-family:Arial;
        }
        .item-value-old {
            color:red;
        }
        .item-value-new {
            color:darkgreen;
        }
        .item-value-split {
            font-weight:bold;
        }
        tr:nth-child(odd) {
            background-color: silver;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <strong>
            <asp:Literal ID="litTitle" runat="server"></asp:Literal>
        </strong>
        <h4>
            <asp:Literal ID="litDate" runat="server"></asp:Literal>
        </h4>
        <hr />
        <div>
            <asp:Literal ID="litHTML" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
