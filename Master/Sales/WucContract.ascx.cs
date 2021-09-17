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
/// Author        : Aashish Lad
/// Created On    : 16th October 2008
/// Description   : This is the Page For Master Contract Tab
/// This is Main User Control in Which Other 4 User Control of Contract is kept
/// </summary>
/// 

public partial class Master_Sales_WucContract : System.Web.UI.UserControl,IContractView 
{
    #region ClassVariables
    ContractPresenter objContractPresenter;
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

    public IContractGeneralView ContractGeneralView
    {
        get { return (IContractGeneralView)WucContractGeneral1; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public IContractTermsView ContractTermsView
    {
        get { return (IContractTermsView)WucContractTerms1; }
    }

    public IContractFreightDetailsView ContractFreightDetailsView
    {
        get { return (IContractFreightDetailsView)WucContractFreightDetails1; }
    }
    public IAttachmentsView AttachmentsView
    {
        get { return (IAttachmentsView)WucAttachments1; }
    }
    //Added : Anita On : 18 Feb 09
    public void ClearVariables()
    {
        WucContractTerms1.SessionContractTermsGrid = null;
        WucContractTerms1.SessionTermsHead = null;

        WucContractFreightDetails1.SessionFreightDetailsGrid = null;
        WucContractFreightDetails1.SessionOtherChargesFreightRateDetails = null;
        WucContractFreightDetails1.SessionOtherChargesFreightRate = null;

        WucAttachments1.SessionAttachmentsGrid = null;        
    }

    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit : 21-02-09
    {
        ClearVariables();
    }

    #endregion

    #region Validation
    public bool validateUI()
    {
        bool IsValid = true;

        if (WucContractGeneral1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 0;
            TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            IsValid = false;
        }
        else if (WucContractTerms1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 1;
            TabStrip1.SelectedTab = TabStrip1.Tabs[1];
            IsValid = false;
        }
        else if (WucContractFreightDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 2;
            TabStrip1.SelectedTab = TabStrip1.Tabs[2];
            IsValid = false;
        }
        else if (WucAttachments1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 3;
            TabStrip1.SelectedTab = TabStrip1.Tabs[2];
            IsValid = false;
        }
        else
        {
            IsValid = true;
        }

        return IsValid;
    }
    #endregion

    #region ControlsEvent
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
        objContractPresenter = new ContractPresenter(this, IsPostBack);
        
        
        WucContractGeneral1.SetScriptManager = scm_Contract;
        WucContractTerms1.SetScriptManager = scm_Contract;
        WucContractFreightDetails1.SetScriptManager = scm_Contract;
        WucAttachments1.SetScriptManager = scm_Contract;
        btn_null_sessions.Visible = false;
        WucAttachments1.AttachmentFormId = 1001;//pankaj 23 oct

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

    }      
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objContractPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objContractPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    #endregion
}
