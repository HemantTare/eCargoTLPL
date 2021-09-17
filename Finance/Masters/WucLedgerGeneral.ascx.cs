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
/// <summary>
/// Author : Sunil
/// Date Created : 4/10/2008
/// Description : This page is For Add And Edit LedgerGeneral, 
/// Change Hostory :
/// Changed By    Date(DD/MM/YYYY)      Description
/// ================================================
/// </summary>

public partial class Finance_Masters_WucLedger : System.Web.UI.UserControl, ILedgerGeneralView
{


    #region ClassVariables
    private ScriptManager scm_LedgerGeneral;
     LedgerGeneralPresenter objLedgerGeneralPresenter;

    //public int runScriptOnLoad = 0;
    #endregion


    #region ControlsValues


  public string LedgerName
    {
        set { txt_LedgerName.Text = value; }
        get { return txt_LedgerName.Text; }
    }

    private int ReservesLadgerGrpId
    {
        get { return Convert.ToInt32(ddl_Under.SelectedValue.Split(new char[] { 'Ö' })[1]);}
    }

    public string Alias
    {
        set { txt_Alias.Text = value; } 
        get { return txt_Alias.Text; }
    }

    public bool IsMaintainBillByBill
    {
        set {
               chk_BillbyBill.Checked = value;
            }
        get {
            if (ReservesLadgerGrpId == 24 || ReservesLadgerGrpId == 27 || ReservesLadgerGrpId == 25)
            {
                return chk_BillbyBill.Checked;
            }
            else { return false;}
            }
    }

    public int DefaultCreditPeriod
    {
        set { txt_CreditPeriod.Text = value.ToString(); }
        get {
            if (IsMaintainBillByBill)
            {
                return Util.String2Int(txt_CreditPeriod.Text);
            }
            else { return 0;}
            }
    }
    public decimal CreditLimit
    {
        set { txt_CreditLimit.Text = value.ToString(); }
        get {
                if (ReservesLadgerGrpId == 24 || ReservesLadgerGrpId == 27)
                {
                    return Util.String2Decimal(txt_CreditLimit.Text);
                }
                else { return 0.00M;}
            }
    }

    public string TypeOfDutyTax
    {
            set { ddl_Type_of_Duty_Tax.SelectedValue = value; }
        get {

            if (ReservesLadgerGrpId == 25)
            {
                return ddl_Type_of_Duty_Tax.SelectedValue;
            }
            else { return ""; }
            }
    }
    public string LedgerUnderId
    {
        set {ddl_Under.SelectedValue = value; }
        get { return ddl_Under.SelectedValue.Split(new char[] {'Ö'})[0]; }
    }

    public int NatureOfPaymentId
    {
        set { ddl_Nature_Of_Payment.SelectedValue = value.ToString(); }
        get {
            if (TypeOfDutyTax == "TDS")
            {
                return Util.String2Int(ddl_Nature_Of_Payment.SelectedValue);
            }
            else { return 0; }
            }
    }
    public bool IsTDSApplicable
    {
        set { Chk_Is_TDS_Applicable.Checked = value; }
        get {
            if (ReservesLadgerGrpId == 24 || ReservesLadgerGrpId == 27)
            {
                return Chk_Is_TDS_Applicable.Checked;
            }
            else { return false;}
            }
    }

    public int TDSDeducteeTypeId
    {
        set { DDL_Deductee_Type.SelectedValue = value.ToString(); }
        get {
            if (IsTDSApplicable)
            {
                return Util.String2Int(DDL_Deductee_Type.SelectedValue);
            }
            else { return 0;}
            }
    }
    public string TDSDeducteeTypeName
    {
        set { DDL_Deductee_Type.SelectedValue = value.ToString(); }
        get {
            if (IsTDSApplicable)
            {
                return DDL_Deductee_Type.SelectedItem.Text;
            }
            else { return ""; }
            }
    }
    public bool IsLowerDeduction
    {
        set { Chk_Is_Lower_No_Deduction_Applicable.Checked = value; }
        get {
            if (IsTDSApplicable)
            {
                return Chk_Is_Lower_No_Deduction_Applicable.Checked; 
            }
            else { return false;}
            }
    }

    public string SectionNumber
    {
        set { ddl_Section_Number.SelectedValue = value; }
        get {
            if (IsTDSApplicable)
            {
                return ddl_Section_Number.SelectedValue; 
            }
            else { return ""; }
            }
    }
    public decimal TDSLowerRate
    {
        set { txt_TDS_Lower_Rate.Text = value.ToString(); }
        get {
            if (SectionNumber == "197")
            {
                return Util.String2Decimal(txt_TDS_Lower_Rate.Text);
            }
            else { return 0.00M; }
            }
    }

    public bool IsIgnoreExemptionLimit
    {
        set { Chk_Is_Ignore_TDS_Exemption_Limit.Checked = value; }
        get { 
               return Chk_Is_Ignore_TDS_Exemption_Limit.Checked; 
            }
    }

    public int ServiceTaxCategoryId
    {
        set { ddl_Service_Tax_Category.SelectedValue = value.ToString(); }
        get {
                if (TypeOfDutyTax == "Service Tax")
                {
                    return Util.String2Int(ddl_Service_Tax_Category.SelectedValue);
                }
                else { return 0;}
            }
    }
    public bool IsServiceTaxApplicable
    {
        set { Chk_Is_Service_Tax_Applicable.Checked = value; }
        get {
            if (ReservesLadgerGrpId == 24 || ReservesLadgerGrpId == 27)
            {
                return Chk_Is_Service_Tax_Applicable.Checked;
            }
            else { return false; }
            }
    }

    public bool IsExempted
    {
        set { Chk_Is_Exempted.Checked = value; }
        get {
            if (IsServiceTaxApplicable)
            {
                return Chk_Is_Exempted.Checked;
            }
            else { return false;}
        
            }
    }

    public bool IsFBTApplicable
    {
        set { Chk_Is_FBT_Applicable.Checked = value; }
        get {
            if (ReservesLadgerGrpId == -1)
            {
                return Chk_Is_FBT_Applicable.Checked;
            }
            else { return false;}
            }
    }

    public int FBTCategoryId
    {
        set { DDL_FBT_Category.SelectedValue = value.ToString(); }
        get {
            if (IsFBTApplicable)
            {
                return Util.String2Int(DDL_FBT_Category.SelectedValue);
            }
            else { return 0;}
              
            }
    }


    public string SelectedHierarchy
    {
        set {
            if (value.Trim() != "")
            {
                WucHierarchyWithID1.HierarchyCode = value;
            }
            }
        get {
            if (ReservesLadgerGrpId == 19 || ReservesLadgerGrpId == 28)
            {
                return WucHierarchyWithID1.HierarchyCode;
            }
            else { return "";}
            }
    }

    public string ServiceTaxNo
    {
        set { txt_ServiceTaxNo.Text = value;}
        get {
            if (IsServiceTaxApplicable)
            {
                return txt_ServiceTaxNo.Text.Trim();
            }
            else { return ""; }
            }
    }

    public string ACNo
    {
        set { txt_ACNo.Text = value; }
        get {
            if (ReservesLadgerGrpId == 19 || ReservesLadgerGrpId == 28)
            {
                return txt_ACNo.Text; 
            }
            else { return ""; }
            }
    }

    public string NotificationDetail
    {
        set { txt_Notification_Detail.Text = value; }
        get {
            if (IsExempted)
            {
                return txt_Notification_Detail.Text;
            }
            else { return "";}
            }
    }

    public string Income_Tax_No
    {
        set { txt_Income_Tax_No.Text = value; }
        get { 
              return txt_Income_Tax_No.Text; 
            }
    }

    public string TIN_Sales_Tax_No
    {
        set { txt_TIN_Sales_Tax_No.Text = value; }
        get { return txt_TIN_Sales_Tax_No.Text; }
    }

    public DateTime ServiceTaxRegDate
    {
        set { dtp_ServiceTaxRegDate.SelectedDate = value; }
        get { return dtp_ServiceTaxRegDate.SelectedDate; }
    }

    public DateTime DateOfBankReco
    {
        set { dtp_BankRecoDate.SelectedDate = value; }
        get { return dtp_BankRecoDate.SelectedDate; }
    }

    public int MainId
    {
        set {
                if (value > 0)
                {
                    WucHierarchyWithID1.MainId = value;
                }
           }
        get
        {
            if (ReservesLadgerGrpId == 19 || ReservesLadgerGrpId == 28)
            {
                return  WucHierarchyWithID1.MainId;
            }
            else { return 0; }
        }
    }



    public DataTable bind_ddl_Under
    {
        set
        {
            ddl_Under.DataTextField = "Ledger_Group_Name";
            ddl_Under.DataValueField = "Ledger_Group_ID";
            ddl_Under.DataSource = value;
            ddl_Under.DataBind();
            ddl_Under.Items.Insert(0, new ListItem("---Select One---", "0Ö0"));
        }
    }

    public DataTable bind_ddl_NatureOfPayment
    {
        set
        {
            ddl_Nature_Of_Payment.DataTextField = "TDS_Nature_of_Payment_Name";
            ddl_Nature_Of_Payment.DataValueField = "TDS_Nature_of_Payment_Id";
            ddl_Nature_Of_Payment.DataSource = value;
            ddl_Nature_Of_Payment.DataBind();
            ddl_Nature_Of_Payment.Items.Insert(0,new ListItem("---Select One---", "0"));
        }
    }

    public DataTable bind_ddl_ServiceTaxCategory
    {
        set
        {
            ddl_Service_Tax_Category.DataTextField = "Service_Tax_Category_Name";
            ddl_Service_Tax_Category.DataValueField = "Service_Tax_Category_Id";
            ddl_Service_Tax_Category.DataSource = value;
            ddl_Service_Tax_Category.DataBind();
        }
    }

    public DataTable bind_ddl_FBTCategory
    {
        set
        {
            DDL_FBT_Category.DataTextField = "FBT_Category_Name";
            DDL_FBT_Category.DataValueField = "FBT_Category_ID";
            DDL_FBT_Category.DataSource = value;
            DDL_FBT_Category.DataBind();
        }
    }

    public DataTable bind_ddl_Deductee_Type
    {
        set
        {
            DDL_Deductee_Type.DataTextField = "TDS_Deductee_Type_Name";
            DDL_Deductee_Type.DataValueField = "TDS_Deductee_Type_Id";
            DDL_Deductee_Type.DataSource = value;
            DDL_Deductee_Type.DataBind();
            DDL_Deductee_Type.Items.Insert(0,new ListItem("---Select One---", "0"));
        }
    }

    public bool EnableControls
    {
        set
        {
           ClassLibraryMVP.UI.Page page = new ClassLibraryMVP.UI.Page();
           //page.DisableControls(this.Controls);
           
            if (value == true)
            {
                txt_LedgerName.Enabled = value;
                ddl_Under.Enabled = value;
                txt_Alias.Enabled = value;
            }
        }
    }

    public bool EnableControlsOnTrans
    {
        set
        {
            chk_BillbyBill.Enabled = value;
        }
    }

    #endregion

    

    #region IView
    public bool validateUI()
    {
      bool _isValid = false;
      if (LedgerName.Trim() == string.Empty)
      {
         errorMessage = "Please Enter Ledger Name";
         scm_LedgerGeneral.SetFocus(txt_LedgerName);         
          
      }
      else if (Util.String2Int(LedgerUnderId) <=0)
      {
         errorMessage = "Please Select Ledger Group";
         scm_LedgerGeneral.SetFocus(ddl_Under);
      }
      //else if (IsMaintainBillByBill && DefaultCreditPeriod <= 0)
      //{
      //    errorMessage = "Please Enter Default Credit Period";
      //    scm_LedgerGeneral.SetFocus(txt_CreditPeriod);
      //}
      else if (CreditLimit<=0 && (ReservesLadgerGrpId == 24 || ReservesLadgerGrpId == 27))
      {
          scm_LedgerGeneral.SetFocus(txt_CreditLimit);
          errorMessage = "Please Enter Credit Limit";          
          
      }
      else if (ACNo.Trim()=="" && (ReservesLadgerGrpId == 19 || ReservesLadgerGrpId == 28))
      {
          errorMessage = "Please Enter Acount No";
          scm_LedgerGeneral.SetFocus(txt_ACNo);          
      }
      else if (ServiceTaxNo.Trim()=="" && IsServiceTaxApplicable)
      {
          errorMessage = "Please Enter Service Tax No";
          scm_LedgerGeneral.SetFocus(txt_ServiceTaxNo);         
          
      }
      else if (TypeOfDutyTax == "0" && ReservesLadgerGrpId == 25)
      {
          errorMessage = "Please Enter Type Of Duty Tax";
      }
      else if (NatureOfPaymentId<=0 && TypeOfDutyTax == "TDS")
      {
          errorMessage = "Please Enter Nature Of Payment";
      }
      else if (TDSDeducteeTypeId <= 0 && IsTDSApplicable)
      {
          errorMessage = "Please Select TDS Deductee Type";
      }
      else if (SectionNumber == "0" && IsLowerDeduction) //IsTDSApplicable)
      {
          errorMessage = "Please Enter Section Number";
      }
      else if (TDSLowerRate<=0 && SectionNumber == "197")
      {
          errorMessage = "Please Enter TDS Lower Rate";
      }
      else if (SectionNumber == "0" && IsLowerDeduction) //IsTDSApplicable)
      {
          errorMessage = "Please Enter Section Number";
      }
      else if (ServiceTaxCategoryId <= 0 && TypeOfDutyTax == "Service Tax")
      {
          errorMessage = "Please Enter Service Tax Category";
      }
      else if (ServiceTaxCategoryId <= 0 && TypeOfDutyTax == "Service Tax")
      {
          errorMessage = "Please Enter Service Tax Category";
      }
      else if (NotificationDetail.Trim()==""  && IsExempted)
      {
          errorMessage = "Please Enter Notification Detail";
      }
      else if (FBTCategoryId <=0 && IsFBTApplicable)
      {
          errorMessage = "Please Select FBT Category";
      }

      else { _isValid = true; }
      Upd_Ledger.Update();
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

           // return 50909;
        }
    }

    #endregion


    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_LedgerGeneral = value; }
    }
    #endregion


    #region OtherMethods

    #endregion
 

    #region ControlsEvents

    protected void Page_Init(object sender, EventArgs e)
    {
        //Param.ValidateSession();
        objLedgerGeneralPresenter = new LedgerGeneralPresenter(this, IsPostBack);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            regStartupScript();
        }
    }


    private void regStartupScript()
    {
        ClientScriptManager cs = Page.ClientScript;
        string scripts;
        if (!cs.IsStartupScriptRegistered(this.GetType(), "formname"))
        {
            if (keyID > 0)
            {
                scripts = "set_runScriptOnLoad(1);FindControls();HideAllControls();OnUnderChanged();OnBillByBill();OnLowerNoDeductionApplicable();OnSectionNumber();OnServiceTaxApplicable();OnExempted();OnFBTApplicable()";

                if (TDSDeducteeTypeId > 0)
                {
                    scripts = scripts + ";OnDeducteeType();";
                }
                if (NatureOfPaymentId > 0)
                {
                    scripts = scripts + ";OnNatureOfPayment();";
                }
                if (IsTDSApplicable)
                {
                    scripts = scripts + ";OnTDSApplicable();";
                }
                if (TypeOfDutyTax == "TDS" || TypeOfDutyTax == "Service Tax")
                {
                    scripts = scripts + ";OnTypeOfDutyTax();";
                }
                scripts = scripts + ";set_runScriptOnLoad(0);";
            }
            else { scripts = "set_runScriptOnLoad(0);FindControls();HideAllControls();"; }
            cs.RegisterStartupScript(this.GetType(), "formname", scripts, true);
        }
    }

    #endregion
    
    //protected void RBL_Location_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (SelectedHierarchy != "H")
    //    {
    //        objLedgerGeneralPresenter.FillLocation();
    //        SetLocationCaption();

    //        lbl_locationDropDown.Visible = true;
    //        ddl_Location.Visible = true;
    //    }
    //    else 
    //    {
    //        lbl_locationDropDown.Visible = false;
    //        ddl_Location.Visible = false;
    //    }
    //}

    //private void SetLocationCaption()
    //{
    //    if (SelectedHierarchy == "B")
    //    {
    //        lbl_locationDropDown.Text = "Branch Name:";
    //    }
    //    else if (SelectedHierarchy == "A")
    //    {
    //        lbl_locationDropDown.Text = "Area Name:";
    //    }
    //    else if (SelectedHierarchy == "R")
    //    {
    //        lbl_locationDropDown.Text = "Region Name:";
    //    }
    //}
}
 














 


   
 
