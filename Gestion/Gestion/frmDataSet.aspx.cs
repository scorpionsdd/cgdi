using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using System.Data.OracleClient;
using System.Data.OracleClient;

namespace Gestion
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.Label lblResult1;
		protected System.Web.UI.WebControls.DataGrid dgUsers;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session.IsNewSession)
			{
				Response.Redirect("/gestion/default.aspx", false);
				return;
			}
			if (! Page.IsPostBack)
			{
				int num1 = Math.Abs(lblResult.Text.GetHashCode());
				string value = "0000000000" + num1.ToString("D");
				string value1 = value.Substring(value.Length - 10);

				char[] chrArray1 = lblResult.Text.ToCharArray();
				int value2 = chrArray1[0] * '\x0003';
				int s = Convert.ToChar(value2);

				string publicKey = Session.SessionID;
				int i=0;
				int j = i % publicKey.Length;

				char ff = publicKey[0];
				int xx = ff % 'ÿ';
				int h = publicKey[(i % publicKey.Length)] % 'ÿ';
				

				string strNumber = h.ToString();
				char chrNumber = System.Convert.ToChar(strNumber[0]);


				// Create an ASCII encoding.
				Encoding ascii = Encoding.ASCII;
        
				// A Unicode string with two characters outside the ASCII code range.
				String unicodeString =
					"This unicode string contains two characters " +
					"with codes outside the ASCII code range, " +
					"Pi (\u03a0) and Sigma (\u03a3).";
				Console.WriteLine("Original string:");
				Console.WriteLine(unicodeString);

				// Save the positions of the special characters for later reference.
				int indexOfPi = unicodeString.IndexOf('\u03a0');
				int indexOfSigma = unicodeString.IndexOf('\u03a3');

				// Encode the string.
				Byte[] encodedBytes = ascii.GetBytes(unicodeString);
				Console.WriteLine();
				Console.WriteLine("Encoded bytes:");
				foreach (Byte b in encodedBytes) 
				{
					Console.Write("[{0}]", b);
				}
				Console.WriteLine();
        
				// Notice that the special characters have been replaced with
				// the value 63, which is the ASCII character code for '?'.
				Console.WriteLine();
				Console.WriteLine(
					"Value at position of Pi character: {0}",
					encodedBytes[indexOfPi]
					);
				Console.WriteLine(
					"Value at position of Sigma character: {0}",
					encodedBytes[indexOfSigma]
					);

				// Decode bytes back to a string.
				// Notice missing the Pi and Sigma characters.
				String decodedString = ascii.GetString(encodedBytes);
				Console.WriteLine();
				Console.WriteLine("Decoded bytes:");
				Console.WriteLine(decodedString);


/*
  				string sServer ="Data Source=isicofin.world;User ID=cgestion;Password=cgestion";
				OracleConnection ocnn = new OracleConnection(sServer);
				OracleDataAdapter oda = new OracleDataAdapter();
				oda.SelectCommand = new OracleCommand("select nombre,puesto from int_v_usuarios",ocnn);
				DataSet ds = new DataSet();
				ocnn.Open();

				oda.Fill(ds,"usuarios");
				

				dgUsers.DataSource = ds;
				dgUsers.DataBind();

				ocnn.Close();
*/


				string sServer ="Data Source=isicofin.world;User ID=cgestion;Password=cgestion";
				string sSql = " select * from mhapplications " +
					" where applicationid = 1";

				OracleConnection ocnn = new OracleConnection(sServer);
				OracleCommand cmd = new OracleCommand(sSql,ocnn);
				ocnn.Open();
				OracleDataReader odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				
				while(odr.Read()) 
				{
					lblResult.Text = odr["APPPATH"].ToString();
				}

				odr.Close();
				ocnn.Close();

				
			}

			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
