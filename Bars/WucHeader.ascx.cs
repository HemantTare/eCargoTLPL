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
using Raj.EC.Security;
using ClassLibraryMVP; 


public partial class Bars_WucHeader : System.Web.UI.UserControl
{

    private DataSet objDS;
    Menus objMenus = new Menus();
    Raj.EC.Common objComm = new Raj.EC.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_companyName.Text = CompanyManager.getCompanyParam().CompanyName;

            objDS = objMenus.CheckMenuHeadRights(UserManager.getUserParam().UserId);
            foreach (DataRow Dr in objDS.Tables[0].Rows)
            {
                if (Convert.ToInt32(Dr["MenuHeadId"]) == 1)
                {
                    lnk_Btn_Admin.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgAdmin.Visible = lnk_Btn_Admin.Visible;
                }
                else if (Convert.ToInt32(Dr["MenuHeadId"]) == 2)
                {
                    Lnk_Btn_Masters.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgMasters.Visible = Lnk_Btn_Masters.Visible;
                }
                else if (Convert.ToInt32(Dr["MenuHeadId"]) == 3)
                {
                    Lnk_Btn_Opr.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgOpr.Visible = Lnk_Btn_Opr.Visible;
                }
                else if (Convert.ToInt32(Dr["MenuHeadId"]) == 4)
                {
                    Lnk_Btn_Finance.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgFinance.Visible = Lnk_Btn_Finance.Visible;
                }
                else if (Convert.ToInt32(Dr["MenuHeadId"]) == 5)
                {
                    Lnk_Btn_Reports.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgReports.Visible = Lnk_Btn_Reports.Visible;
                }
                else if (Convert.ToInt32(Dr["MenuHeadId"]) == 6)
                {
                    Lnk_Btn_CRM.Visible = Convert.ToBoolean(Dr["Show"]);
                    ImgCRM.Visible = Lnk_Btn_CRM.Visible;
                }
            }
            SetUserDetails();

             String url;
             url = objComm.getBaseURL() + "/TrackNTrace/FrmMainTrackNTrace.aspx";
             lnk_track_and_trace.Attributes.Add("onclick", "return newwindow1('" + url + "')");

        }
        if (Session["MenuHeadId"] != null)
        {
            if (StateManager.GetState<int>("MenuHeadId") == 1)
            {
                lnk_Btn_Admin.CssClass = "SELECTEDLINK";
            }
            else if (StateManager.GetState<int>("MenuHeadId") == 2)
            {
                Lnk_Btn_Masters.CssClass = "SELECTEDLINK";
            }
            else if (StateManager.GetState<int>("MenuHeadId") == 3)
            {
                Lnk_Btn_Opr.CssClass = "SELECTEDLINK";
            }
            else if (StateManager.GetState<int>("MenuHeadId") == 4)
            {
                Lnk_Btn_Finance.CssClass = "SELECTEDLINK";
            }
            else if (StateManager.GetState<int>("MenuHeadId") == 5)
            {
                Lnk_Btn_Reports.CssClass = "SELECTEDLINK";
            }
        }
        else
        {
            Lnk_Btn_User_Desk.CssClass = "SELECTEDLINK";
        }
    }

    private void SetUserDetails()
    {
        if (UserManager.getUserParam().HierarchyCode == "AD")
        {
            lbl_login_details.Text = "ADMIN LOGIN | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "HO")
        {
            lbl_login_details.Text = "HO LOGIN | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "RO")
        {
            lbl_login_details.Text = "REGION:" + UserManager.getUserParam().MainName.ToUpper() + " | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "AO")
        {
            lbl_login_details.Text = "AREA:" + UserManager.getUserParam().MainName.ToUpper() + " | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "BO")
        {
            lbl_login_details.Text = "BRANCH:" + UserManager.getUserParam().MainName.ToUpper() + " | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "NE")
        {
            lbl_login_details.Text = "USER:" + UserManager.getUserParam().UserName.ToUpper();
        }
        else if (UserManager.getUserParam().HierarchyCode == "CL")
        {
            lbl_login_details.Text = "CLIENT LOGIN | USER:" + UserManager.getUserParam().UserName.ToUpper();
        }

        lbl_FY.Text = "FINANCIAL YEAR : " +
            UserManager.getUserParam().StartDate.ToString("dd MMM yyyy").ToUpper() + " - " +
            UserManager.getUserParam().EndDate.ToString("dd MMM yyyy").ToUpper();

        if (CompanyManager.getCompanyParam().IsActivateDivision)
            lbl_division.Text = lbl_division.Text + " : " + UserManager.getUserParam().DivisionName;
        else
            lbl_division.Visible = false;
    }
    protected void lnk_Btn_Admin_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 1);
        Response.Redirect("~/Display/FrmAdmin.aspx");
    }
    protected void Lnk_Btn_Masters_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 2);
        Response.Redirect("~/Display/FrmMasters.aspx");
    }
    protected void Lnk_Btn_User_Desk_Click(object sender, EventArgs e)
    {
        Session["MenuHeadId"] = null;
        Response.Redirect("~/Display/FrmDisplay.aspx");
    }
    protected void Lnk_Btn_Opr_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 3);
        Response.Redirect("~/Display/FrmOperations.aspx");
    }
    protected void Lnk_Btn_Finance_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 4);
        Response.Redirect("~/Display/FrmFinance.aspx");
    }
    protected void Lnk_Btn_Reports_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 5);
        Response.Redirect("~/Display/FrmReports.aspx");
    }
    protected void Lnk_Btn_CRM_Click(object sender, EventArgs e)
    {
        StateManager.SaveState("MenuHeadId", 6);
        Response.Redirect("~/Display/FrmCRM.aspx");
    }
    protected void Lnk_Btn_Fleet_Click(object sender, EventArgs e)
    {
        //Session["MenuHeadId"] = null;

          string urlString;
        urlString = "http://" + System.Web.HttpContext.Current.Request.Url.Host;

        if (!System.Web.HttpContext.Current.Request.Url.IsDefaultPort)
        {
            urlString = urlString + ":" + System.Web.HttpContext.Current.Request.Url.Port;
        }

        string url = urlString + "/eFleet/frmlogin.aspx?IsFromCargo=ZgByAG8AbQBFAEMA&Uid=" + Util.EncryptString(Session["UID"].ToString()) + "&Pwd=" + Util.EncryptString(Session["PWD"].ToString());

        string popupScript = "<script language='javascript'>openwindowFleet('" + url + "');</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "FleetPopup", popupScript.ToString(), false);
   }
    protected void Lnk_Btn_Logout_Click(object sender, EventArgs e)
    {
        objDS = objComm.GetPendingMemo();
        if (objDS.Tables[0].Rows.Count > 0)
        {
            String url;
            url = objComm.getBaseURL() + "/Reports/User Desk/frmPendingMemo.aspx";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "display", "onlogoutclick('" + url + "');", true);
        }
        else
            Response.Redirect("../Logout.aspx");
    }
}
