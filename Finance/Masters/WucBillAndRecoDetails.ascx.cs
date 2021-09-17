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
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP.General;
using ClassLibraryMVP;

/// <summary>
/// Author        : Sunil Bhoyar
/// Created On    : 16/11/2008
/// Description   : This is the Form  For Leder Bill Details And Bank Reco Details
/// </summary>


public partial class Finance_Masters_BillAndRecoDetails : System.Web.UI.UserControl, IRecoAndBillDetailsView
{
    #region ClassVariables
    RecoAndBillDetailsPresenter objRecoAndBillDetailsPresenter;
    public DateTime StartDate;
    ComponentArt.Web.UI.Calendar dtp_VoucherDate;
    ComponentArt.Web.UI.Calendar dtp_BillDate;

    TextBox txt_Particular;
    TextBox txt_ChequeNo;
    TextBox txt_Amount;
    DropDownList ddl_dgDrCr;
    TextBox txt_BillName;
    TextBox txt_CreditDays;
    DropDownList ddl_ref_type;
    Label lbl_Row_No;

    #endregion
    #region ControlsValues

    public int RowNo
    {
        get { return hdn_Row_No.Value == "" ? 0 : Convert.ToInt32(hdn_Row_No.Value); }
        set { hdn_Row_No.Value = value.ToString(); }
    }

    public DateTime BillDate
    {
        get { return dtp_BillDate.SelectedDate; }
        set { dtp_BillDate.SelectedDate = value; }
    }

    public DateTime VoucherDate
    {
        get { return dtp_VoucherDate.SelectedDate; }
        set { dtp_VoucherDate.SelectedDate = value; }
    }
    public string Particulars
    {
        set { txt_Particular.Text = value; }
        get { return txt_Particular.Text; }
    }

    
    private decimal Amount
    {
        set { txt_Amount.Text = Util.Decimal2String(value);}
        get {
            if (dgDrCr == 1)
            {
                return txt_Amount.Text.Trim() == "" ? 0 : Util.String2Decimal(txt_Amount.Text);
            }
            else { return convertToDecimal(txt_Amount.Text) * -1; }
            }
    }

    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value;}
        get { return txt_ChequeNo.Text;}
    }


    private string BillName
    {
        set { txt_BillName.Text = value; }
        get { return txt_BillName.Text; }
    }

    private int Ref_Type_Id
    {
        set { ddl_ref_type.SelectedValue = value.ToString(); }
        get { return Convert.ToInt32(ddl_ref_type.SelectedValue); }
    }

    private string Ref_Type
    {
        get { return ddl_ref_type.SelectedItem.Text; }
    }


    private int CreditDays
    {
        set { txt_CreditDays.Text = value.ToString(); }
        get { return  convertToInt(txt_CreditDays.Text); }
    }

    private int dgDrCr
    {
        set { ddl_dgDrCr.SelectedValue = value.ToString(); }
        get { return Convert.ToInt32(ddl_dgDrCr.SelectedValue);}
    }


    private string Particular
    {
        set { txt_Particular.Text = value; }
        get { return txt_Particular.Text; }
    }


    public int OpeningDrCr
    {
        set { ddl_DrCr.SelectedValue = value.ToString(); }
        get { return Convert.ToInt32(ddl_DrCr.SelectedValue);}
    }

    private int ClearAmountDrCr
    {
        set { ddl_ClearAmountDrCr.SelectedValue = value.ToString(); }
        get { return Convert.ToInt32(ddl_ClearAmountDrCr.SelectedValue); }
    }


    public decimal OpeningBalance
    {
        set {

            if (value>0)
            {OpeningDrCr = 1;}
            else {OpeningDrCr = -1; }

             txt_Opening_Balance.Text = Util.Decimal2String(Math.Abs(value)); 
            }
        get {
               return convertToDecimal(txt_Opening_Balance.Text) * OpeningDrCr; 
            }
    }

    public decimal ClearedAmount
    {
        set {

            if (Math.Sign(value)==-1)
            { ClearAmountDrCr = -1; }
            else { ClearAmountDrCr = 1; }

             txt_ClearAmount.Text = Util.Decimal2String(Math.Abs(value)); 
            }
        get {
            return convertToDecimal(txt_ClearAmount.Text) * ClearAmountDrCr; 
            }  
    }
    
    public string GetBillWiseXML
    {
        get
        {
            string _xml;
            if (OpeningBalance == 0)
            {
                _xml = "<NewDataSet></NewDataSet>";
            }
            else
            {
                DataSet objDS = new DataSet();
                SesssionBillWiseDT.TableName = "BillWise";
                objDS.Tables.Add(SesssionBillWiseDT.Copy());
                objDS.AcceptChanges();
                return objDS.GetXml();
            }
            return _xml;
        }
    }

    public string GetBankRecoXML
    {
        get
        {
            string _xml;
            if (OpeningBalance == 0 || (OpeningBalance==ClearedAmount))
            {
                _xml = "<NewDataSet></NewDataSet>";
            }
            else 
            {
                DataSet objDS = new DataSet();
                SesssionBankRecoDT.TableName = "BankReco";
                objDS.Tables.Add(SesssionBankRecoDT.Copy());
                objDS.AcceptChanges();
                _xml = objDS.GetXml();
            }

            return _xml;
        }
    }

    public string convertToDrCr(object value)
    {
        if (Math.Sign(Convert.ToDecimal(value)) == -1)
        { return "Dr"; }
        else{return "Cr";}
    }

    public string convertToAbs(object value)
    {
        return Convert.ToString(Math.Abs(Convert.ToDecimal(value)));
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0;}
        else { return Convert.ToDecimal(value); }
    }

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }

    

    #endregion

    #region ControlsBind



    public DataTable SesssionBillWiseDT
    {
        set { StateManager.SaveState("BillWise_DT", value); }
        get { return StateManager.GetState<DataTable>("BillWise_DT"); }
    }


    public DataTable SesssionBankRecoDT
    {
        set { StateManager.SaveState("BankReco_DT", value); }
        get { return StateManager.GetState<DataTable>("BankReco_DT"); }
    }

    

    public DataTable Bind_dg_BillWise
    {
        set
        {
            SesssionBillWiseDT = value;
            CalculateRowNo();
            dg_BillWise.DataSource = value;
            dg_BillWise.DataBind();
            SetTotalLable = TotalBillWise;
        }
    }

    public DataTable Bind_dg_BankReco
    {
        set
        {
            SesssionBankRecoDT = value;
            CalculateRowNoBankReco();
            dg_BankReco.DataSource = value;
            dg_BankReco.DataBind();
            SetTotalLable = TotalBankReco + ClearedAmount;
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        
        if (IsBankReco && OpeningBalance != 0 && OpeningBalance != (TotalBankReco + ClearedAmount))
        {
            errorMessage = "Total Bank Reconcillation Amount Not Match To Opening Balance Amount";
        }
        else if (IsBillWise && OpeningBalance != 0 && OpeningBalance != TotalBillWise)
        {
            errorMessage = "Total Bill Wise Amount Not Match To Opening Balance Amount";
        }
        else 
        {
            _isValid = true;
        }
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
           // return 70;
        }
    }

    #endregion


    #region OtherProperties

    public  bool VisibleBillWise 
    {
        set {tbl_BillWise.Visible=value ;}
    }

    public bool VisibleBankReco
    {
        set { tbl_BankReco.Visible = value; }
    }

    public bool IsBankReco 
    {
        set { ViewState["IsBankReco"]=value; }
        get { return Convert.ToBoolean(ViewState["IsBankReco"]);}
    }
    public bool IsBillWise
    {
        set { ViewState["IsBillWise"] = value; }
        get { return Convert.ToBoolean(ViewState["IsBillWise"]); }
    }

    #endregion


    #region OtherMethods


    private void InsertUpdateBillWise(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (ValidateBillWiseValues() == false)
        { return; }

        DataTable objDT = SesssionBillWiseDT;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[RowNo];
        }

        try
        {
            objDR["Bill_Date"] = BillDate;
            objDR["Ref_No"] = BillName;
            objDR["Credit_Days"] = CreditDays;
            objDR["Amount"] = Amount;
            objDR["Ref_Type_ID"] = Ref_Type_Id;
            objDR["Ref_Type"] = Ref_Type;
            
            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_BillWise.EditItemIndex = -1;
                dg_BillWise.ShowFooter = true;
            }
            Bind_dg_BillWise = objDT;
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Bill Name";
        }
    }

    private bool ValidateBillWiseValues()
    {
        bool _isValid = false;
        if (BillDate >= UserManager.getUserParam().StartDate)
        {
            errorMessage = "Bill Date Should Be Less Than " + UserManager.getUserParam().StartDate.ToString("dd/MM/yyyy");
        }
        else if (BillName.Trim() ==string.Empty)
        {
            errorMessage = "Please Enter Bill Name";
        }
        //else if (CreditDays <=0)
        //{
        //    errorMessage = "Please Enter Credit Days";
        //}
        else if (Amount == 0)
        {
            errorMessage = "Please Enter Amount";
        }
        else
        {
            _isValid = true;
        }
         return _isValid;
    }


    public decimal TotalBillWise
    {
        get { return convertToDecimal(SesssionBillWiseDT.Compute("Sum(Amount)", "")); }
    }


    public decimal SetTotalLable
    {
        set { lbl_Total.Text = Math.Abs(value) +" "+ convertToDrCr(value); }
    }


    private void InsertUpdateBankReco(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (ValidateBankRecoValues() == false)
        { return; }

        DataTable objDT = SesssionBankRecoDT;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[RowNo];
        }

        try
        {
            objDR["Voucher_Date"] = VoucherDate.ToString("dd/MM/yyyy");
            objDR["Particulars"] = Particulars;
            objDR["Cheque_No"] = ChequeNo;
            objDR["Amount"] = Amount;

            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR);
            }

            if (e.CommandName == "Update")
            {
                dg_BankReco.EditItemIndex = -1;
                dg_BankReco.ShowFooter = true;
            }

            Bind_dg_BankReco = objDT;
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
        }
    }

    private bool ValidateBankRecoValues()
    {
        bool _isValid = false;
        if (VoucherDate >= UserManager.getUserParam().StartDate)
        {
            errorMessage = "Voucher Date Should Be Less Than " + UserManager.getUserParam().StartDate.ToString("dd/MM/yyyy");
        }
       else if (ChequeNo.Trim()==string.Empty)
        {
            errorMessage = "Please Enter Cheque No";
        }
        else if (Amount == 0)
        {
            errorMessage = "Please Enter Amount";
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    public decimal TotalBankReco
    {
        get { return convertToDecimal(SesssionBankRecoDT.Compute("Sum(Amount)", "")); }
    }



    #endregion


    #region  ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        Raj.EC.Common ObjCommon = new Raj.EC.Common();
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        objRecoAndBillDetailsPresenter = new RecoAndBillDetailsPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (IsBankReco == false && IsBillWise == false)
            { tr_Total.Visible = false; }
        }
    }

    #endregion


    #region BankReco_GridEvents
    protected void dg_BankReco_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateBankReco(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_BankReco.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_BankReco = SesssionBankRecoDT;
            dg_BankReco.ShowFooter = false;
        }

        else if (e.CommandName == "Cancel")
        {
            dg_BankReco.EditItemIndex = -1;
            Bind_dg_BankReco = SesssionBankRecoDT;
            dg_BankReco.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {
            lbl_Row_No = (Label)e.Item.FindControl("lbl_Row_No");
            RowNo = Convert.ToInt32(lbl_Row_No.Text);
            SesssionBankRecoDT.Rows[e.Item.ItemIndex].Delete();
            SesssionBankRecoDT.AcceptChanges();
            dg_BankReco.CurrentPageIndex = 0;
            Bind_dg_BankReco = SesssionBankRecoDT;
        }
    }

    protected void dg_BankReco_ItemDataBound(object source, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            dtp_VoucherDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_VoucherDate");
            lbl_Row_No = (Label)e.Item.FindControl("lbl_Row_No");
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                RowNo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "RowNo"));
                dtp_VoucherDate.MaxDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1, 0, 0, 0));
                dtp_VoucherDate.SelectedDate = Convert.ToDateTime(SesssionBankRecoDT.Rows[RowNo]["Voucher_Date"]);
                lbl_Row_No.Text = Convert.ToString(SesssionBankRecoDT.Rows[RowNo]["rowno"]);
            }
            else
            {
                dtp_VoucherDate.MaxDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1,0,0,0));
                dtp_VoucherDate.SelectedDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1, 0, 0, 0));
            }
        }
    }

    #endregion




    #region BillWise_GridEvents
    protected void dg_BillWise_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateBillWise(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_BillWise.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_BillWise = SesssionBillWiseDT;
            dg_BillWise.ShowFooter = false;
        }

        else if (e.CommandName == "Cancel")
        {
            dg_BillWise.EditItemIndex = -1;
            Bind_dg_BillWise = SesssionBillWiseDT;
            dg_BillWise.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
            {
                lbl_Row_No = (Label)e.Item.FindControl("lbl_Row_No");
                RowNo = Convert.ToInt32(lbl_Row_No.Text);
                SesssionBillWiseDT.Rows[RowNo].Delete();
                SesssionBillWiseDT.AcceptChanges();
                dg_BillWise.CurrentPageIndex = 0;
                Bind_dg_BillWise = SesssionBillWiseDT;
            }
    }

    protected void dg_BillWise_ItemDataBound(object source, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            dtp_BillDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_BillDate");
            ddl_ref_type = (DropDownList)e.Item.FindControl("ddl_RefType");
            lbl_Row_No = (Label)e.Item.FindControl("lbl_Row_No");
            
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                RowNo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "RowNo"));
                dtp_BillDate.MaxDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1, 0, 0, 0));
                dtp_BillDate.SelectedDate = Convert.ToDateTime(SesssionBillWiseDT.Rows[RowNo]["Bill_Date"]);
                Ref_Type_Id = Convert.ToInt32(SesssionBillWiseDT.Rows[RowNo]["Ref_Type_ID"]);
                lbl_Row_No.Text = Convert.ToString(SesssionBillWiseDT.Rows[RowNo]["rowno"]);
            }
            else 
            {
                dtp_BillDate.MaxDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1, 0, 0, 0));
                dtp_BillDate.SelectedDate = UserManager.getUserParam().StartDate.Subtract(new TimeSpan(1, 0, 0, 0));
            }
        }
    }

    #endregion



    private void findControls(DataGridItem item)
    {
        ddl_dgDrCr = (DropDownList)item.FindControl("ddl_DrCr");
        dtp_VoucherDate = (ComponentArt.Web.UI.Calendar)item.FindControl("dtp_VoucherDate");
        dtp_BillDate = (ComponentArt.Web.UI.Calendar)item.FindControl("dtp_BillDate");
        txt_Particular = (TextBox)item.FindControl("txt_Particular");
        txt_ChequeNo = (TextBox)item.FindControl("txt_ChequeNo");
        txt_Amount = (TextBox)item.FindControl("txt_Amount");
        txt_BillName = (TextBox)item.FindControl("txt_BillName");
        txt_CreditDays = (TextBox)item.FindControl("txt_CreditDays");
        ddl_ref_type = (DropDownList)item.FindControl("ddl_RefType");
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (IsBankReco)
        {SetTotalLable = TotalBankReco;}
        else if (IsBillWise)
        { SetTotalLable = TotalBillWise +ClearedAmount;}

        objRecoAndBillDetailsPresenter.Save();
    }
    protected void dg_BillWise_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_BillWise.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_BillWise = SesssionBillWiseDT;
    }
    protected void dg_BankReco_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_BankReco.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_BankReco = SesssionBankRecoDT;
    }

    private void CalculateRowNo()
    {
        int i = 0;

        foreach (DataRow dr in SesssionBillWiseDT.Rows)
        {
            dr["RowNo"] = i;
            i++;
        }

        SesssionBillWiseDT.AcceptChanges();
    }

    private void CalculateRowNoBankReco()
    {
        int i = 0;

        foreach (DataRow dr in SesssionBankRecoDT.Rows)
        {
            dr["RowNo"] = i;
            i++;
        }

        SesssionBankRecoDT.AcceptChanges();
    }
}














 


   
 

