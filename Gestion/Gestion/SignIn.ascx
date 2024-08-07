<%@ Import Namespace="BOComponents" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Log.Layer.Model.Model.Enumerator" %>
<%@ Import Namespace="Log.Layer.Model.Extension" %>
<%@ Import Namespace="Log.Layer.Business" %>
<%@ Import Namespace="Log.Layer.Model.Model" %>
<%@ Import Namespace="BComponents.DataAccessLayer" %>
<%@ Import Namespace="System.Data" %>
<%--<%@ Import Namespace="System.Data.OracleClient" %>--%>
<%@ Import Namespace="System.Configuration" %>
<%@ Control Language="c#" %>

<script runat="server">

	private void register_Click(object sender, System.EventArgs e)
	{
		Response.Redirect("Register.aspx", false);
	}

	private void Btnsignin_Click(object sender, System.EventArgs e)
	{

		// Attempt to Validate User Credentials using UsersDB
		BOComponents.WorkFlow oParticipants = new BOComponents.WorkFlow();
		Session["key"]        =null;
		Session["user_name"]  =null;
		Session["uid"]        =null;
		Session["rol"]        =null;
		Session["sessionId"]  =null;
		Session["employeeId"] = null;
		try
		{
			int nUserID=0;
			try
			{
				nUserID=oParticipants.GetParticipantID((string)Session["sAppServer"], email.Text);
			}
			catch (Exception)
			{
			}

			if (nUserID > 0)
			{
				Session["key"]              = oParticipants.GetParticipantKey((string)Session["sAppServer"], nUserID );
				Session["user_name"]        = oParticipants.GetParticipantName((string)Session["sAppServer"], nUserID);
				Session["uid"]              = nUserID;
				Session["rol"]              = oParticipants.GetParticipantRol((string)Session["sAppServer"], nUserID);
				Session["sessionId"] = Guid.NewGuid().ToString();
				Session["employeeId"] = oParticipants.GetParticipantExpedient((string)Session["sAppServer"], nUserID);

				if (Session["key"].ToString() != "" && Session["key"].ToString() == password.Text)
				{
					//Response.Redirect("/gestion/portal/index.aspx");
					ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Login", enuAction.Access.GetDescription(), string.Empty, string.Empty,Request.UserHostAddress,Session["sessionId"].ToString(),Session["employeeId"].ToString(),null,string.Format("Entro Cuenta {0}",Session["user_name"]),string.Format("Entro Expediente Cuenta {0}",Session["employeeId"])));
					Response.Redirect("/gestion/portal/index.aspx?uid=" + nUserID + "");
				}
				else {
					ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(Convert.ToInt32(Session["uid"]), "Pantalla Login", enuAction.AccessDenied.GetDescription(), string.Empty, string.Empty,Request.UserHostAddress,Session["sessionId"].ToString(),Session["employeeId"].ToString()));
					Message.Text = "<" + "br" + ">El Login a fallado!" + "<" + "br" + ">";
				}
			}
			else
			{
				ControlLog.GetInstance().Create(OracleHelper.ExecuteNonQuery, new LogSystem(nUserID, "Pantalla Login", enuAction.UserNotExist.GetDescription(), string.Empty, string.Empty,Request.UserHostAddress,string.Empty,string.Empty,null,string.Format("Cuenta no encontrada: {0}",email.Text)));
			}
		}
		catch (Exception ex)
		{
			Message.Text = "<" + "br" + ">El Login a fallado!" + "<" + "br" + ">";
		}


	}
</script>
<%--

   The SignIn User Control enables clients to authenticate themselves using
   the ASP.NET Forms based authentication system.

   When a client enters their username/password within the appropriate
   textboxes and clicks the "Login" button, the LoginBtn_Click event
   handler executes on the server and attempts to validate their
   credentials against a SQL database.

   If the password check succeeds, then the LoginBtn_Click event handler
   sets the customers username in an encrypted cookieID and redirects
   back to the portal home page.

   If the password check fails, then an appropriate error message
   is displayed.

--%>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td><span class="header-gray" style="HEIGHT:20px">Clave de Usuario</span></td>
	</tr>
	<tr>
		<td>
			<span class="Normal">Login</span>
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="email"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<asp:TextBox id="email" columns="9" width="185px" cssclass="NormalTextBox" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<span class="Normal">Password:</span>
			<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="password"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<asp:TextBox id="password" columns="9" width="185px" textmode="password" cssclass="NormalTextBox"
				runat="server" />
		</td>
	</tr>
	<tr>
		<td height="37">
			<br>
			<!--<asp:checkbox id="RememberCheckbox" class="Normal" Text="Remember Login" runat="server" />-->
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<asp:Button id="Btnsignin" runat="server" Text="Entrar" OnClick="Btnsignin_Click"></asp:Button>
			<img src="Images/spacer.gif" width="10"> 
			<!--			<asp:Button id="register" runat="server" Text="Salir" CausesValidation="False" OnClick="register_Click"></asp:Button>&nbsp;-->
			<br>
			<br>
			<asp:label id="Message" class="NormalRed" runat="server" />
		</td>
	</tr>
</table>
<br>
