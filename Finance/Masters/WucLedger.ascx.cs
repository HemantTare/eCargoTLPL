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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC.ControlsView;
/// <summary>
/// Author : Sunil
/// Date Created : 4/10/2008
/// Description : This page is For Add And Edit Ledger, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class Finance_Masters_WucLadger : System.Web.UI.UserControl, ILedgerView
{
    #region ClassVariables
     LedgerPresenter objLedgerPresenter;
    #endregion


    #region ControlsValues

    public ILedgerGeneralView getLedgerGeneralView
    {
        get
        {
            return (ILedgerGeneralView)WucLedgerGeneral1;
        }
    }

    public IDivisionView getDivisionView
    {
        get
        {
            return (IDivisionView)WucLedgerDivision1;
        }
    }

    public IAddressView getAddressView
    {
        get
        {
            return (IAddressView)WucAddress1;
        }
    }


    public string Name
    {
        set { txt_Name.Text = value; }
        get { return txt_Name.Text.Trim(); }
    }
    public string ContactPerson
    {
        set { txt_ContactPerson.Text = value; }
        get { return txt_ContactPerson.Text.Trim(); }
    }
    public string Note
    {
        set { txt_Note.Text = value; }
        get { return txt_Note.Text.Trim(); }
    }

    #endregion

    

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (WucLedgerGeneral1.validateUI()==false)
        {
            MP_Ledger.SelectedIndex = 0;
            TB_Ledger.SelectedTab = TB_Ledger.Tabs[0];
            _isValid = false;
            
        }
        else if (WucLedgerDivision1.GetSlectedIds().Rows.Count == 0)
        {
            errorMessage = "Please Select Atleast One " + GetGlobalResourceObject("FA_Mst", "Mst_Ledger_DivisionTab").ToString(); ;
        }
        else { _isValid = true; }
         return _isValid;
    }

    public string errorMessage
    {
        set
        {
           lbl_Errors.Text = value;
        }
    }

    public int keyID
    {
        get
        {
           return Util.DecryptToInt(Request.QueryString["Id"]); 
           //return Util.String2Int(Request.QueryString["Id"]); 
           // return Raj.CRM.Common.Null2Int(Request.QueryString["Id"]);

           //return 50909;
        }
    }

    #endregion


    #region OtherProperties
     
    #endregion


    #region OtherMethods

    #endregion
 

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //Param.ValidateSession();


        Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));    

         objLedgerPresenter = new LedgerPresenter(this, IsPostBack);

         if (!IsPostBack)
         {
             WucAddress1.VisibleMobileNo = false;
             WucAddress1.VisiblePhoneNo2 = false;
             WucAddress1.VisibleStdCode = false;
             TB_Ledger.Tabs[2].Visible = UserManager.getUserParam().IsDivisionReq;
             TB_Ledger.Tabs[2].Text = GetGlobalResourceObject("FA_Mst", "Mst_Ledger_DivisionTab").ToString();
             
         }
         WucLedgerGeneral1.SetScriptManager = scm_Ledger; 
         
 
    }

    #endregion
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (validateUI())
       objLedgerPresenter.Save();
    }
    
}
 














 


   
 
