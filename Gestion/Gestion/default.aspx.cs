using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCGD
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["id_usuario"] != null)
                {
                    try
                    {
                        BOComponents.WorkFlow oParticipants = new BOComponents.WorkFlow();
                        int nUserId = oParticipants.GetParticipantID((string)Session["sAppServer"], Request["id_usuario"].ToString());
                        //int nUserId = oParticipants.GetParticipantID(ConnectionS, Request["id_usuario"].ToString());

                        if (nUserId > 0)
                        {
                            Session["key"] = oParticipants.GetParticipantKey((string)Session["sAppServer"], nUserId);
                            Session["user_name"] = oParticipants.GetParticipantName((string)Session["sAppServer"], nUserId);
                            Session["uid"] = nUserId;
                            Session["rol"] = oParticipants.GetParticipantRol((string)Session["sAppServer"], nUserId);
                            oParticipants.Dispose();
                            Response.Redirect("/gestion/portal/index.aspx", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        var errMessage = ex.Message;
                        if (ex.HResult == -2146233033)
                            errMessage = "Usuario no valido, favor de rectificarlo.";
                        Response.Redirect("/gestion/correspondencia/error.aspx?errMessage=" + errMessage + "&errNumber=" + ex.HResult);
                    }
                }
            }
        }
    }
}