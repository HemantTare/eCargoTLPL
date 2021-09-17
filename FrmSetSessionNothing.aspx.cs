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

public partial class FrmSetSessionNothing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_null_user_Click(object sender, EventArgs e)
    {
        UserManager Um = new UserManager();
        Um.SetUserSessiontoNothing();
    }
    protected void btn_null_company_Click(object sender, EventArgs e)
    {
        CompanyManager Cm = new CompanyManager();
        Cm.SetCompanySessiontoNothing();
    }
}
