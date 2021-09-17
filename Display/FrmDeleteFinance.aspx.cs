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
using ClassLibrary;
using Raj.EC.Security;
using ClassLibraryMVP.Security;
using Raj.EC;

public partial class Display_FrmDeleteFinance : System.Web.UI.Page
{

    private int _ID = 0;
    private int _Menu_Item_ID = 0;
    string linkName, LinkBtnName, No = "";
    int Error_Id = 0;
    string Error_Desc = "";
    Cancel objCancel = new Cancel();


    protected void Page_Load(object sender, EventArgs e)
    {

        _Menu_Item_ID = Raj.EC.Common.GetMenuItemId();
        linkName = Util.DecryptToString(Request.QueryString["LinkName"]);
        LinkBtnName = Util.DecryptToString(Request.QueryString["LinkBtnText"]);
        _ID = Util.DecryptToInt(Request.QueryString["Id"]);
        No = Util.DecryptToString(Request.QueryString["No"]);


        if (!IsPostBack)
        {
            lblHeader.Text = LinkBtnName.ToUpper() + " " + linkName.ToUpper() + " " + "  No :" + No;
            btn_Cancel.Text = LinkBtnName;
        }

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        objCancel.EC_Opr_Cancel_Finance(_ID, _Menu_Item_ID, txt_Reason.Text.Trim(), UserManager.getUserParam().UserId, ref Error_Id, ref Error_Desc);
        string CloseScript = "<script language='javascript'> alert('Record Cancelled SuccessFully');window.opener.location.reload();window.close();</script>";
        ScriptManager.RegisterStartupScript(up_Btn, typeof(String), "CloseScript", CloseScript, false);
    }
}
