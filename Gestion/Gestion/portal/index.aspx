<%@ Page CodeBehind="index.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Gestion.Portal.index" %>

<html>
<head>
    <title>Banobras - Control de Gestion Documental</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
</head>
<script type="text/javascript" language="javascript">

    var iStart = 0;
    var initial;
    var iMinute = "<%= Session.Timeout %>"; //Obtengo el tiempo de session permitida
    function showTimer() {
        iStart = 60;
        iMinute -= 1
        lessMinutes();
    }
    function lessMinutes() {
        //Busco mi elemento que uso para mostrar los minutos que le quedan (minutos y segundos)
        obj = document.getElementById('TimeLeft');
        if (iStart == 0) {
            iStart = 60
            iMinute -= 1;
        }
        iStart = iStart - 1;

        //Si minuto y segundo = 0 ya expiró la sesion
        if (iMinute == 0 && iStart == 0) {

            let isBoss = confirm("Su sesion ha expirado, sera redireccionado a la página principal");
            //alert(isBoss);
            if (isBoss == true) {
                window.location.href = 'http://intranet';
            }
            else {


                const urlParams = new URLSearchParams(window.location.search);
                const myParam = urlParams.get('uid');
                var dataString = "{'uid': '" + myParam + "'}";

                $.ajax({
                    type: "POST",
                    url: "index.aspx/SessionTimeout",
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        // Replace the div's content with the page method's return.

                        iStart = 0;
                        initial;
                        iMinute = "<%= Session.Timeout %>"; //Obtengo el tiempo de session permitida
                        window.clearTimeout(initial);
                        lessMinutes();


                    }
                });
            }
        }

        if (iStart < 10)
            obj.innerText = iMinute.toString() + ':0' + iStart.toString();
        else
            obj.innerText = iMinute.toString() + ':' + iStart.toString();

        //actualizo mi método cada segundo 
        initial = window.setTimeout("lessMinutes();", 1000)
    }

</script>
<%--	<frameset rows="100,*" cols="*" frameborder="NO" border="0" framespacing="0">
		<frame src="header.aspx" name="header" marginwidth="0" marginheight="0" frameborder="NO"
			scrolling="no">
		<frame src="main.aspx" name="main" scrolling="yes" marginwidth="0" marginheight="0" frameborder="NO" >
	</frameset>--%>
<body onload="InitializeTimer()" style="overflow: hidden">
    <span id="TimeLeft" hidden></span>


    <iframe frameborder="NO" border="0" scrolling="no" framespacing="0" width="100%"
        src="<% SetFrames();%>/gestion/portal/header.aspx"></iframe>

    <iframe frameborder="NO" border="0" scrolling="yes" framespacing="0" width="100%" height="78%"
        src="<% SetFrames();%>/gestion/portal/main.aspx"></iframe>
    <script type="text/javascript" language="javascript">
        showTimer();
    </script>
</body>
</html>
