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
using ClassLibraryMVP.Security;
using Raj.EC.Security;


public partial class Display_FrmAdmin : System.Web.UI.Page
{


    private int _menuHeadId = 1;
    public int UserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_null_sessions.Style.Add("display", "none");

            UserId = UserManager.getUserParam().UserId;
            int _menuSystemId = UserManager.getUserParam().SystemId;

            Rights objRights = new Rights();
            objRights.SetRights(UserId, _menuSystemId, 0, 0);

            Menus objMenus = new Menus();
            objMenus.BuildNavigationBar(NBAdmin, UserId, _menuHeadId, _menuSystemId);

            if (NBAdmin.Items.Count > 1 && UserManager.getUserParam().HierarchyCode != "AD")
                NBAdmin.Items[1].Visible = false;
        }
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
