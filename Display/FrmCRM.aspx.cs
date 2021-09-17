using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.Security;
using Raj.EC.Security;
using ClassLibraryMVP.DataAccess;

public partial class Display_FrmCRM : System.Web.UI.Page
{
    private int _menuHeadId = 6;
    public int UserId;
    private DAL _objDal = new DAL();
    private DataSet _objDS = null;
    DataSet dsInboxCount;
    bool IS_CSO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_null_sessions.Style.Add("display", "none");

            UserId = UserManager.getUserParam().UserId;
            IS_CSO = UserManager.getUserParam().Is_CSA;
            int _menuSystemId = UserManager.getUserParam().SystemId;

            Rights objRights = new Rights();
            objRights.SetRights(UserId, _menuSystemId, 0, 0);

            Menus objMenus = new Menus();
            objMenus.BuildNavigationBar(NBMasters, UserId, _menuHeadId, _menuSystemId);

        }
        dsInboxCount = GetCRMInboxCount();
        
        ComponentArt.Web.UI.NavBarItem objNavBarItem = new ComponentArt.Web.UI.NavBarItem();

        foreach (ComponentArt.Web.UI.NavBarItem item in NBMasters.Items)
        {
            if (item.Text.Trim().ToLower() == "masters")
            {
                item.Visible = IS_CSO;
            }
            else if (item.Text.Trim().ToLower() == "inbox")
            {
                item.Visible = IS_CSO;
            }
        }
      
        foreach (ComponentArt.Web.UI.NavBarItem item in NBMasters.Items)
        {
            if (item.Text.Trim().ToLower() == "inbox")
            {
                objNavBarItem = item;
                break;
            }
        }
        
        if (objNavBarItem != null)
        {
            foreach (ComponentArt.Web.UI.NavBarItem item in objNavBarItem.Items)
            {
                if (item.Text.Trim().ToLower() == "pending for action")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["PendingForAction"].ToString() + ")";
                    item.Visible = !(IS_CSO);
                }
                else if (item.Text.Trim().ToLower() == "action taken by me")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["ActionTakenByMe"].ToString() + ")";
                    item.Visible = !(IS_CSO);
                }
                else if (item.Text.Trim().ToLower() == "action taken by others")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["ActionTakenByOthers"].ToString() + ")";
                    item.Visible = !(IS_CSO);
                }
                else if (item.Text.Trim().ToLower() == "pending for assignment")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["PendingForAssignment"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
                else if (item.Text.Trim().ToLower() == "assigned by me")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["Assigned"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
                else if (item.Text.Trim().ToLower() == "assigned by others")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["AssignedByOther"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
                else if (item.Text.Trim().ToLower() == "in process")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["InProgress"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
                else if (item.Text.Trim().ToLower() == "closed")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["ClosedTickets"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
                else if (item.Text.Trim().ToLower() == "archive")
                {
                    item.Text = item.Text + "(" + dsInboxCount.Tables[0].Rows[0]["Archived"].ToString() + ")";
                    item.Visible = IS_CSO;
                }
            }
        }
    }

    public DataSet GetCRMInboxCount()
    {
        SqlParameter[] sqlpara = {
            _objDal.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode)};

        _objDal.RunProc("EC_CRM_GetInboxCount", sqlpara, ref _objDS);

        return _objDS;
    }

    public void ClearVariables()
    {
        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

}
