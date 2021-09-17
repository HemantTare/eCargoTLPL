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
using Raj.EC;
using Raj.EC.ControlsView;
public partial class Finance_Accounting_Vouchers_WucBalanceTruckHire : System.Web.UI.UserControl,IBalanceTruckHireView
{
    BalanceTruckHirePresenter objBalanceTruckHirePresenter;
    MRCashChequePresenter objCashChequePresenter;
    Common ObjCommon = new Common();
    string Mode = "0";


    #region Controls Value

    public string BTHVoucherNo 
    {
        get { return txt_BTHVoucherNo.Text.Trim();}
        set { txt_BTHVoucherNo.Text = value;}
    }
    
    public DateTime BTHVoucherDate
    {
        get { return Wuc_BTHVoucherDate.SelectedDate; }
        set { Wuc_BTHVoucherDate.SelectedDate = value; }
    }
     
    public string ReferenceNo
    {
        get { return txt_ReferenceNo.Text.Trim(); }
        set { txt_ReferenceNo.Text = value; }
    }
    
    public decimal TotalPayableAmount
    {
        get { return Util.String2Decimal(hdn_Total_Payable_Amount.Value); }
        set { 
                txt_TotalPayableAmount.Text = value.ToString();
                hdn_Total_Payable_Amount.Value = value.ToString();
            }
    }

    public string Remark
    {
        get { return txt_Remarks.Text.Trim(); }
        set { txt_Remarks.Text = value; }
    }

    public int BrokerOwnerID 
    {
        get { return Util.String2Int(ddl_OwnerBroker.SelectedValue);} 
    }
    
    public IMRCashChequeDetailsView MRCashChequeDetailsView 
    {
        get {return (IMRCashChequeDetailsView)WucMRCashChequeDetails1;} 
    }
    
    public IVehicleSearchView VehicleSearchView 
    {
        get {return (IVehicleSearchView)WucVehicleSearch1;}
    }

    public IOtherChargeDetailsView OtherChargeDetailsView 
    {
        get {return (IOtherChargeDetailsView)WucOtherChargeDetails1 ;}
    }

    public DataSet Bind_ddlLHPONo 
    {
        set 
        { 
            ddl_LhpoNo.DataSource = value;
            ddl_LhpoNo.DataTextField = "LHPO_No_For_Print";
            ddl_LhpoNo.DataValueField = "LHPO_ID";
            ddl_LhpoNo.DataBind();

            if (keyID <= 0)
                Common.InsertItem(ddl_LhpoNo);
        }
    }
    
    public int LHPONo_ID 
    {
        get {return Util.String2Int(ddl_LhpoNo.SelectedValue) ;}
        set { ddl_LhpoNo.SelectedValue = value.ToString();}
    }

    public decimal Balance_To_Be_Paid
    {
        get { return txt_BalanceToBePaid.Text.Trim() == "0" ? 0 : Util.String2Decimal(txt_BalanceToBePaid.Text); }
        set { txt_BalanceToBePaid.Text = value.ToString(); }
    }
    public DateTime LHPO_Date
    {
        get { return Convert.ToDateTime(hdn_LHPODATE.Value); }
        set { hdn_LHPODATE.Value = value.ToShortDateString(); }
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public bool validateUI()
    {
        bool _isvalid = false;

        //if (Datemanager.IsValidProcessDate("BTH", BTHVoucherDate) == false)
        //{
        //    errorMessage = "Please Enter Valid Date";
        //}
        if (BrokerOwnerID <= 0)
        {
            errorMessage = "Please Select Owner/Broker";
        }
        else if (BTHVoucherDate > DateTime.Now)
        {
            errorMessage = "Please Enter valid BTH Voucher Date";
        }
        else if (VehicleSearchView.VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle";
        }
        else if (LHPONo_ID <= 0)
        {
            errorMessage = "Please Select LHPO NO";
        }
        else if (TotalPayableAmount <= 0)
        {
            errorMessage = "Total Payable Amount Should be Greater Than 0";
        }
        else if (!WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors))
        {
        }
        else if (WucMRCashChequeDetails1.CashAmount >= 20000)
        {
            errorMessage = "Cash Amount Should Be Less Than 20000.";            
        }
        else if (WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
        }
        else
        {
            _isvalid = true;
        }


        if (keyID > 0)
        {
            if (BTHVoucherDate < LHPO_Date)
            {
                errorMessage = "Please Enter BTHVoucherDate Less Than LHPO date";
                _isvalid = false;
            }
        }
        return _isvalid;
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
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1, IsPostBack);
        objBalanceTruckHirePresenter = new BalanceTruckHirePresenter(this, IsPostBack);

        WucMRCashChequeDetails1.Scmcheque = scm1;
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        WucOtherChargeDetails1.OtherDetailsGrid += new EventHandler(Calculate_Total_Payable_Amount);

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                WucVehicleSearch1.VehicleVendorID = 0;
                BTHVoucherNo = ObjCommon.Get_Next_Number();
            }
            else
            {
                ddl_OwnerBroker.Enabled = false;
                WucVehicleSearch1.Enable_Disable(false);
                ddl_LhpoNo.Enabled = false;
                Wuc_BTHVoucherDate.AutoPostBackOnSelectionChanged = false;
            }
        }
        SetStandardCaption();

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }

    private void SetStandardCaption()
    {
        lbl_LhpoNo.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No. :";
        lbl_LhpoDate.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Date :";
        lbl_LhpoBranch.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Branch :";
    }

    public void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_OwnerBroker.SelectedValue) <= 0 || ddl_OwnerBroker.SelectedValue == "")
        {
            errorMessage = "Please Select Owner/Broker First";
            VehicleSearchView.VehicleID = 0;
            ddl_LhpoNo.Items.Clear();
            Clear_Fields();
        }
        else
        {
            LHPO_DDl_Fill();
            Clear_Fields();
        }
    }
    protected void ddl_LhpoNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objBalanceTruckHirePresenter.Set_Lhpo_Detail();
        Fill_LHPO_Detail(ds);
        Calculate_Total_Payable_Amount(sender,e);
    }

    public void Calculate_Total_Payable_Amount(object sender,EventArgs e)
    {
        decimal total_amount = 0;
        
        total_amount = Balance_To_Be_Paid + OtherChargeDetailsView.TotalAmount;
        TotalPayableAmount = total_amount <= 0 ? 0 : total_amount;
     
        MRCashChequeDetailsView.CashAmount = 0;
        MRCashChequeDetailsView.ChequeAmount = 0;
        MRCashChequeDetailsView.Total_ChequeAmount = 0;

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);    
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objBalanceTruckHirePresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    public void LHPO_DDl_Fill()
    {
        if (WucVehicleSearch1.VehicleID > 0)
        {
            string ID = WucVehicleSearch1.GetVehicleParameter("Vehicle_Category_ID");

            if (ID == "1")
                txt_VehicleCat.Text = "Own";
            else if (ID == "2")
                txt_VehicleCat.Text = "Managed";
            else if (ID == "3")
                txt_VehicleCat.Text = "Attached";
            else if (ID == "5")
                txt_VehicleCat.Text = "Market";
        }
        else
            txt_VehicleCat.Text = "";

        if (objBalanceTruckHirePresenter != null)
        {
            objBalanceTruckHirePresenter.Fill_LHPONoDDL();
            Clear_Fields();
        }    
    }

    public void Fill_LHPO_Detail(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow DR = ds.Tables[0].Rows[0];
            txt_ManualRefNo.Text = DR["Manual_Ref_No"].ToString();
            txt_Driver1.Text = DR["Driver_Name"].ToString();
            txt_LHPOBranch.Text = DR["Branch_Name"].ToString();
            txt_FromBranch.Text = DR["From_Service_Location_Name"].ToString();
            txt_ToLocation.Text = DR["To_Service_Location_Name"].ToString();
            txt_HireAmount.Text = DR["Truck_Hire_Charge"].ToString();
            txt_TDS.Text = DR["TDS_Amount"].ToString();
            txt_Advancepayable.Text = DR["Total_Advance_To_Be_Paid"].ToString();
            txt_BalanceToBePaid.Text = DR["Balance_Payble_Amount"].ToString();
            txt_LhpoDate.Text = Convert.ToDateTime(DR["LHPO_Date"]).ToShortDateString();
        }
        else
        {
            Clear_Fields();
        }   
    }

    public void SetOwnerID(string Name, string ID)
    {
        ddl_OwnerBroker.DataTextField = "Vendor_Name";
        ddl_OwnerBroker.DataValueField = "Vendor_ID";
        Raj.EC.Common.SetValueToDDLSearch(Name, ID, ddl_OwnerBroker);
    }
    protected void Wuc_BTHVoucherDate1_SelectionChanged(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            objBalanceTruckHirePresenter.Fill_LHPONoDDL();
            Clear_Fields();
        }
    }
    public void ClearVariables()
    {
        MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        MRCashChequeDetailsView.Session_ddl_DepositIn = null;
        OtherChargeDetailsView.Session_OtherDetailsGrid = null;
    }
    public void Clear_Fields()
    {
        txt_ManualRefNo.Text = "";
        txt_Driver1.Text = "";
        txt_LhpoDate.Text = "";
        txt_LHPOBranch.Text = "";
        txt_FromBranch.Text = "";
        txt_ToLocation.Text = "";
        txt_HireAmount.Text = "";
        txt_TDS.Text = "";
        txt_Advancepayable.Text = "";
        txt_BalanceToBePaid.Text = "";

        Object sender = null;
        EventArgs e = null;
        Calculate_Total_Payable_Amount(sender, e);

        MRCashChequeDetailsView.CashAmount = 0;
        MRCashChequeDetailsView.ChequeAmount = 0;
        MRCashChequeDetailsView.Total_ChequeAmount = 0;

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);    
    }
    protected void ddl_OwnerBroker_TxtChange(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleVendorID = Util.String2Int(ddl_OwnerBroker.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_OwnerBroker.SelectedValue);
        VehicleSearchView.VehicleID = 0;
        ddl_LhpoNo.Items.Clear();
        txt_VehicleCat.Text = "";
        OnDDLVehicleSelection(sender, e);
    }
}
