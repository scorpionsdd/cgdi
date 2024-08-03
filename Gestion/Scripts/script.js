var popUp = null; 
var oPostingWindow = null;
			
function OpenDocumentType(idname, postBack)
{
	popUp = window.open('edit_tipo_documento.aspx?formname=' + document.forms[0].name + '&postBack=' + postBack, 'documentType', 
						'width=800,height=500,left=200,top=250,scrollbars=yes');
}


function SetDocumentType(formName, newDocument, newDocumentId)
{
	var id = 'tbDocumentTypeId';
   	eval('var theform = document.' + formName + ';');
   	popUp.close();

	theform.elements[id].value = newDocumentId;
	 
//	theform.elements[id].visible = true;

//	var nItems = theform.elements[id].length;
//	theform.elements[id].options[nItems] = new Option(newDocument, newDocumentId);
//	theform.elements[id].options[nItems].selected = true;

	__doPostBack(id,'');

}


function getSender() {

     var sURL   = '/gestion/correspondencia/edit_remitente_externo.aspx?callType=in' + '&formname=' + document.forms[0].name;
     var sOpt   = 'height=465px,width=670px,resizable=no,scrollbars=no,status=no,location=no';

    if (oPostingWindow == null || oPostingWindow.closed) {
	oPostingWindow = window.open(sURL, '_blank', sOpt);
    } else {
	oPostingWindow.focus();
	oPostingWindow.navigate(sURL);
    }
}


function setSender(sFormName, nSenderID)  {

	var id = 'txtSenderID';
   	eval('var theform = document.' + sFormName + ';');
   	oPostingWindow.close();

	theform.elements[id].value = nSenderID;
	 
	__doPostBack(id,'');
}



function OpenSenderExternal(idname, postBack) {
	popUp = window.open('edit_remitente_externo.aspx?formname=' + document.forms[0].name + '&postBack=' + postBack, 'popupcal', 
						'width=800,height=500,left=200,top=250,scrollbars=yes');
}


function PostSenderExternal(formName) {
	var id = 'dropDependencia';
   	eval('var theform = document.' + formName + ';');
   	popUp.close();
	__doPostBack(id,'');

}


function OpenCalendar(idname, postBack) {
	popUp = window.open('Calendar.aspx?formname=' + document.forms[0].name + 
		'&id=' + idname + '&selected=' + document.forms[0].elements[idname].value + '&postBack=' + postBack, 
		'popupcal', 
		'width=165,height=208,left=200,top=250');
}


function SetDate(formName, id, newDate, postBack) {

	eval('var theform = document.' + formName + ';');
	popUp.close();
	theform.elements[id].value = newDate;
	if (postBack)
		__doPostBack(id,'');
}		
