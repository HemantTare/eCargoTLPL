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
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using Raj.EC.ControlsView;

/// <summary>
/// author Shiv Kumar mishra
/// created on 7th oct 08
/// this is main user control in which other 4 user control of Branch are kept
/// </summary>
/// 
public partial class Master_Branch_WucBranch : System.Web.UI.UserControl,IBranchView
{
    #region ClassVariables
    BranchPresenter objBranchPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    string _flag = "";
    string Mode = "0";
    #endregion

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get { return 523; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public IBranchGeneralView BranchGeneralView
    {
        get { return (IBranchGeneralView)WucBranchGeneralDetails1; }
    }

    public IBranchDeptServiceView BranchDeptServiceView
    {
        get { return (IBranchDeptServiceView)WucBranchDeptServices1; }
    }

    public IBranchParametersView BranchParametersView
    {
        get { return (IBranchParametersView)WucBranchParameters1; }
    }

    public IBranchRequiredFormsView BranchRequiredFormsView
    {
        get { return (IBranchRequiredFormsView)WucBranchRequiredForms1; }
    }
       
    #endregion

    #region Validation
    public bool validateUI()
    {
        bool IsValid = true;

        if (WucBranchGeneralDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 0;
            TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            IsValid = false;
        }
        else if (WucBranchDeptServices1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 1;
            TabStrip1.SelectedTab = TabStrip1.Tabs[1];
            IsValid = false;
        }
        else if (WucBranchParameters1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 2;
            TabStrip1.SelectedTab = TabStrip1.Tabs[2];
            IsValid = false;
        }
        else if (WucBranchRequiredForms1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 3;
            TabStrip1.SelectedTab = TabStrip1.Tabs[3];
            IsValid = false;
        }
        else
        {
            IsValid = true;
        }

        return IsValid;
    }
    #endregion

    #region Page Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        objBranchPresenter = new BranchPresenter(this, IsPostBack);

        WucBranchGeneralDetails1.OnCityChanged = new EventHandler(WucBranchRequiredForms1.HandelsCityChangedEvent);
        //WucBranchGeneralDetails1.OnCityChanged = new EventHandler(WucBranchGeneralDetails1.HandelsCityChangedEvent);

        WucBranchGeneralDetails1.OnDefaultHubSelect += new EventHandler(WucBranchDeptServices1.HandelsDefaultHubChangedEvent);
        WucBranchGeneralDetails1.OnDeliveryAtSelect += new EventHandler(WucBranchDeptServices1.HandelsDeliveryAtChangedEvent);

        WucBranchGeneralDetails1.SetScriptManager = scm_Branch;
        WucBranchDeptServices1.SetScriptManager = scm_Branch;
        WucBranchParameters1.SetScriptManager = scm_Branch;
        WucBranchRequiredForms1.SetScriptManager = scm_Branch;

        WucBranchDeptServices1.DeliveryAtID = WucBranchGeneralDetails1.DeliveryAtID;
        WucBranchDeptServices1.DeliveryHubID = WucBranchGeneralDetails1.DefaultHubID;

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

    }    

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objBranchPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objBranchPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    

  #endregion
   
}
