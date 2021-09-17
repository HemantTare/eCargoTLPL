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

/// <summary>
/// Common Link which will be used in Common Grid Displayed
/// </summary>

public partial class CommonControls_WucLinkName : System.Web.UI.UserControl
{

    public string text
    {
        get { return lbl_Link_Name.Text; }
        set { lbl_Link_Name.Text = value; }

    }
}
