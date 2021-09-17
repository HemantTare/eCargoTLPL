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

public partial class Finance_Accounting_Vouchers_FrmVoucherBank : ClassLibraryMVP.UI.Page//System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.DecryptToInt(Request.QueryString["Mode"]) == 4)
        {
            DisableControls(WucVoucherBank1.Controls);
           
        }
    }
}
