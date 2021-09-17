using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;

public partial class Display_DisplayError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string header = StateManager.GetState<String>("Header");
        string description = StateManager.GetState<String>("Description");

        lbl_Heading.Text  = header;
        lbl_Description.Text  = description; 
    }
}
