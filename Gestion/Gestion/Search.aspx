<%@ Page Language="C#" Debug="False" Strict="True" Explicit="True" Buffer="True"%>
<%@ Import Namespace="System" %>

<html>
<head>
<title>Highlighting Multiple Search Keywords in .NET</title>
</head>

<style type="text/css">
.highlight {text-decoration:none; font-weight:bold; color:black; background:yellow;}
</style>

<body bgcolor="#FFFFFF" topmargin="0" marginheight="0" onLoad="document.forms[0].keywords.focus();">

<script language="C#" runat="server">

void Page_Load(Object Source, EventArgs E) 
{

LabelTxt.Text = "This sample text is used to demonstrate multiple keyword highlighting in .NET. Simply type in any word or words found within this sample text and hit the submit button to highlight the searched words. Also, it doesn't matter how many you enter."; 

}

public string Highlight(string Search_Str, string InputTxt) 
{ 

// Setup the regular expression and add the Or operator.
Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);

// Highlight keywords by calling the delegate each time a keyword is found.
return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords)); 

// Set the RegExp to null.
RegExp = null;

}

public string ReplaceKeyWords(Match m) 
{

return "<span class=highlight>" + m.Value + "</span>"; 

}

</script>

<H3>Highlighting Multiple Search Keywords in .NET</H3><BR>

<form runat="server" method="post" ID="Form1">

<asp:TextBox id="keywords" runat="server"/>
<asp:Button id="button" Text="Submit" runat="server" /><br><br>
<asp:Label id="LabelTxt" runat="server"/>

</form>

<%= Highlight(keywords.Text, LabelTxt.Text)%>

</body>
</html>

