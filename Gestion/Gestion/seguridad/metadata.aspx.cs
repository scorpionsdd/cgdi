using Log.Layer.Model.Model.Enumerator;
using Log.Layer.Model.Extension;
using Log.Layer.Business;
using Log.Layer.Model.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
namespace Gestion.gestion.seguridad
{
    public partial class metadata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData() {
            LogSystem item = (LogSystem)Session["LogSystemItem"];
            litHTML.Text = string.Empty;
            litTitle.Text = item.Action;
            litDate.Text = item.DateTimeEvent.ToString("yyyy/MM/ddTHH:mm:ss");
            var metadata= ControlLog.GetInstance().BuildMetadata(item);
            if (metadata.isOk)
            {
                litHTML.Text = metadata.result;
            }
            else
            {
                litHTML.Text = metadata.message;
            }
            
        }
    }
}