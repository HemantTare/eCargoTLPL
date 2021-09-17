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
/// created on 14th oct 08
/// this is main user control in which other 3 user control of Client are kept
/// </summary>
/// 

public partial class Master_Sales_WucClient : System.Web.UI.UserControl,IClientView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    ClientPresenter objClientPresenter;
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

    public IClientGeneralView ClientGeneralView
    {
        get { return (IClientGeneralView)WucClientGeneralDetails1; }
    }

    public IClientFinanceView ClientFinanceView
    {
        get { return (IClientFinanceView)WucClientFinanceDetails1; }
    }

    public IClientBillingView ClientBillingView
    {
        get { return (IClientBillingView)WucClientBillingDetails1; }
    }
    //Added : Anita On : 18 Feb 09
    public void ClearVariables()
    {
        WucClientBillingDetails1.SessionBindBillingGrid = null;        
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

        if (WucClientGeneralDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 0;
            TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            IsValid = false;
        }

        else if (WucClientFinanceDetails1.IsServiceTaxPayByClient == true && WucClientGeneralDetails1.CSTTINNo.Trim().Length != 15 )
        {
            MultiPage1.SelectedIndex = 0;
            TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            WucClientGeneralDetails1.errorMessage = "Please Enter 15 Digits GST No";
            IsValid = false;
        }
        else if (WucClientFinanceDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 1;
            TabStrip1.SelectedTab = TabStrip1.Tabs[1];
            IsValid = false;
        }
        else if (WucClientBillingDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 2;
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
        objClientPresenter = new ClientPresenter(this, IsPostBack);
        if (IsPostBack == false)
            pc.AddAttributes(this.Controls);       
        WucClientGeneralDetails1.SetScriptManager = scm_Client;
        WucClientFinanceDetails1.SetScriptManager = scm_Client;
        WucClientBillingDetails1.SetScriptManager = scm_Client;
        WucClientGeneralDetails1.OnCopyFromClient += new EventHandler(WucClientFinanceDetails1.HandelsServiceTax);

        WucClientFinanceDetails1.ClientGroupId = WucClientGeneralDetails1.ClientGroupID;
        WucClientGeneralDetails1.IsServiceTaxPay = WucClientFinanceDetails1.IsServiceTaxPayByClient;
        btn_null_sessions.Visible = false;

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());


        if (keyID > 0)
        {
            CheckBox chkSMSAlert = (CheckBox)WucClientGeneralDetails1.FindControl("WucAddress1$chk_SMS_Alert");
            CheckBox chkeMailAlert = (CheckBox)WucClientGeneralDetails1.FindControl("WucAddress1$chk_eMail_Alert");

            DropDownList ddl_ClientCategory = (DropDownList)WucClientGeneralDetails1.FindControl("ddl_ClientCategory");
            TextBox txt_CstNo = (TextBox)WucClientGeneralDetails1.FindControl("txt_CSTTINNo");
            DropDownList ddl_dly_type = (DropDownList)WucClientGeneralDetails1.FindControl("ddl_dly_type");
            CheckBox chk_Is_Casual_Taxable = (CheckBox)WucClientGeneralDetails1.FindControl("chk_Is_Casual_Taxable");
            CheckBoxList chk_Is_ToPay_Allowed = (CheckBoxList)WucClientBillingDetails1.FindControl("cbl_BookingPaymentMode");
            TextBox txt_Remarks = (TextBox)WucClientGeneralDetails1.FindControl("txt_Remarks");
            CheckBox Chk_IsServiceTaxPayable = (CheckBox)WucClientFinanceDetails1.FindControl("chk_ServiceTaxPayableByClient");

            if (UserManager.getUserParam().ProfileId != 20 && UserManager.getUserParam().ProfileId != 0)
            {
                ddl_ClientCategory.Enabled = false;
                //txt_CstNo.Enabled = false;
                ddl_dly_type.Enabled = false;
                chk_Is_Casual_Taxable.Enabled = false;
                chk_Is_ToPay_Allowed.Enabled = false;
                txt_Remarks.Enabled = false;
                //Chk_IsServiceTaxPayable.Enabled = false;
                chkSMSAlert.Enabled = false;
                chkeMailAlert.Enabled = false;
            }
            else
            {
                ddl_ClientCategory.Enabled = true;
                //txt_CstNo.Enabled = true;
                ddl_dly_type.Enabled = true;
                chk_Is_Casual_Taxable.Enabled = true;
                chk_Is_ToPay_Allowed.Enabled = true;
                txt_Remarks.Enabled = true;
                //Chk_IsServiceTaxPayable.Enabled = true;
                chkSMSAlert.Enabled = true;
                chkeMailAlert.Enabled = true;
            }
        }


    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objClientPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objClientPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
