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
using ClassLibraryMVP.Security;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 30/12/2008
/// Description   : This Page is For Transport Bill
/// </summary>
/// 
public partial class Finance_Accounting_Vouchers_WucTransportBill : System.Web.UI.UserControl,ITransportBillView
{
    #region ClassVariables
    TransportBillPresenter objTransportBillPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    LinkButton  lbtn_SubTotal, lbtn_OtherCharge;
    CheckBox chk;
    TextBox txt_GCRemarks;
    HiddenField hdn_GcId;
    ClassLibrary.UIControl.DDLSearch ddl_Ledger;
    TextBox txt_Amount; 
    int _dgRowCount;

    string Mode = "0";
    String _flag;
    private int _Document_Allocation_ID = 0;
    private int _Start_No = 0;
    private int _End_No = 0;
    private int _Next_No = 0;
    private string _Padded_Next_No = "";
    private int BindGridCallFrom = 0; 
    #endregion

    #region ControlsValues

    public int BkgDlyBranchId
    {
        get { return Wuc_Region_Area_Branch1.BranchID; }
    }

    public int BkgDlyAreaId
    {
        get { return Wuc_Region_Area_Branch1.AreaID; }
    }

    public int BkgDlyRegionId
    {
        get { return Wuc_Region_Area_Branch1.RegionID; }
    }

    public int IsBookingWise
    {
        get { return Convert.ToInt32(rdl_BkgBranchOrDlyBranchWise.SelectedValue); }
    }

    public int Bill_ForID
    {
        get { return Util.String2Int(rbtn_TransBillFor.SelectedValue); }
        set { rbtn_TransBillFor.SelectedValue = value.ToString(); }
    }

    public DataTable BindBillFor
    {
        set
        {
            rbtn_TransBillFor.DataTextField = "Credit_Memo_For";
            rbtn_TransBillFor.DataValueField = "Credit_Memo_For_ID";
            rbtn_TransBillFor.DataSource = value;
            rbtn_TransBillFor.DataBind();
        }
    }  

    public int BillTypeID
    {
        get { return Util.String2Int(ddl_BillType.SelectedValue); }
        set { ddl_BillType.SelectedValue = Util.Int2String(value); }
    }
    public int ClientID
    {
        get { return Util.String2Int(ddl_Client.SelectedValue); }
    }

    public string  ClientName
    {
        get { return ddl_Client.SelectedText; }
    }

    private bool Is_SeriesNo_Required
    {
        get { return Util.String2Bool(hdn_Is_Series_Required.Value); }
    }
    public DateTime BillDate
    {
        set { dtp_BillDate.SelectedDate = value; }
        get { return dtp_BillDate.SelectedDate; }
    }
    public int Total_No_Of_GC
    {
        set 
        {
            hdn_totalgc.Value = Util.Int2String(value);
            lbl_totalgc.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_totalgc.Value); }
    }
    public string BillNo
    {
        set { lbl_TransBillNo.Text = value; }
        get { return lbl_TransBillNo.Text; }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public decimal Less_Amount
    {
        set { txt_Less_Amt.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Less_Amt.Text); }
    }
    public string ReferenceNo
    {
        set { txt_Ref_No.Text = value; }
        get { return txt_Ref_No.Text; }
    }
    public string TotalSubTotal
    {
        set
        {
            hdn_totalsubtot.Value = value;
            lbl_totalsubtot.Text = value;
        }
    }
    public string TotalLRSerTax
    {
        set
        {
            hdn_totalLRSerTax.Value = value;
            lbl_totalLRSerTax.Text = value;
        }
    }
    public string TotalRound_Off
    {
        set
        {
            hdn_totalRound_Off.Value = value;
            lbl_totalRound_Off.Text = value;
        }
    }
    public string TotalLRTotal
    {
        set
        {
            hdn_totalLRTotal.Value = value;
            lbl_totalLRTotal.Text = value;
        }
    } 
    public string TotalOtherCharge
    {
        set
        {
            hdn_totalothercharge.Value = value;
            lbl_totalothercharge.Text = value;
        }
    }
    public string TotalOctroiFormCharge
    {
        set
        {
            hdn_TotalOctroiFormCharge.Value = value;
            lbl_TotalOctroiFormCharge.Text = value;
        }
    }
    public string TotalOctroiServiceCharge
    {
        set
        {
            hdn_TotalOctroiServiceCharge.Value = value;
            lbl_TotalOctroiServiceCharge.Text = value;
        }
    }
    public string TotalServiceTax
    {
        set
        {
            hdn_totalservicetax.Value = value;
            lbl_totalservicetax.Text = value;
        }
    }
    public string TotalOctAmount
    {
        set
        {
            hdn_totaloctamt.Value = value;
            lbl_totaloctamt.Text = value;
        }
    }
    public string TotalGCAmount
    {
        set
        {
            hdn_totalgcamount.Value = value;
            lbl_totalgcamount.Text = value;
        }
        get
        {
            return lbl_totalgcamount.Text ;
        }
    }
    public String BillingName
    {
        set{txt_BillingName.Text = value; }
        get{return txt_BillingName.Text.Trim();}
    }
    public String ContactPerson
    {
        set{ txt_ContactPerson.Text = value; }
        get{return txt_ContactPerson.Text.Trim();}
    }
    public String BillingAddress
    {
        set{txt_BillingAddress.Text = value; }
        get{ return txt_BillingAddress.Text.Trim(); }
    }
    public String ContactNo
    {
        set{ txt_ContactNo.Text = value; }
        get {return txt_ContactNo.Text.Trim();}
    }
    public String Email
    {
        set { txt_Email.Text = value; }
        get { return txt_Email.Text.Trim(); }
    }
    private string Start_End_No
    {
        get { return lbl_Start_End_No.Text; }
        set { lbl_Start_End_No.Text = value; }
    }
    public int Document_Allocation_ID
    {
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
    }
    public int Start_No
    {
        set { hdn_Start_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Start_No.Value); }
    }
    public int End_No
    {
        set { hdn_End_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_End_No.Value); }
    }
    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get 
        {
            if (Is_SeriesNo_Required == true)
            {
                return Util.String2Int(BillNo);
            }
            else
            {
                return Util.String2Int(hdn_Next_No.Value);
            }
        }
    }
    public string Padded_Next_No
    {
        set { hdn_Padded_Next_No.Value = value; }
        get { return hdn_Padded_Next_No.Value; }
    }

    public void SetClientId(string text, string value)
    {
        ddl_Client.DataTextField = "Client_Name";
        ddl_Client.DataValueField = "Client_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Client);
    }
 
    public decimal TotalAmount
    {
        set { 
            lbl_TotalAmount.Text = value.ToString();
            hdn_TotalAmount.Value = value.ToString(); 
        }
        get
        {
            return convertToDecimal(SessionLedgerDT.Compute("Sum(Amount)", ""));
        }
    }
    public decimal Total_Additional_Charges
    {
        set {
             lbl_Total_Additional_Charges.Text = value.ToString();
             hdn_Total_Additional_Charges.Value = value.ToString();    
        }
        get
        {
            return convertToDecimal(hdn_Total_Additional_Charges.Value);
        }
    }

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return "Dr"; }
        else { return "Cr"; }
    }

    public string convertToAbs(object value)
    {
        return Convert.ToString(Math.Abs(Convert.ToDecimal(value)));
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    public int convertToInt(object value)
    {
        if (value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToInt32(value); }
    }
    #endregion

    #region Other Method
    public void Set_Hidden_and_LABEL_To_Zero()
    {
        lbl_totalgc.Text = "0";
        lbl_totalsubtot.Text = "0";
        lbl_totalLRSerTax.Text = "0";
        lbl_totalRound_Off.Text = "0";
        lbl_totalLRTotal.Text = "0";
        lbl_totalothercharge.Text = "0";
        lbl_TotalOctroiFormCharge.Text = "0";
        lbl_TotalOctroiServiceCharge.Text = "0";
        lbl_totalservicetax.Text = "0";
        lbl_totaloctamt.Text = "0";
        lbl_totalgcamount.Text = "0";

        lbl_Total_Additional_Charges.Text = "0"; 

        hdn_totalgc.Value = "0";
        hdn_totalsubtot.Value = "0";
        hdn_totalLRSerTax.Value = "0";
        hdn_totalRound_Off.Value = "0";
        hdn_totalLRTotal.Value = "0";
        hdn_totalothercharge.Value = "0";
        hdn_TotalOctroiFormCharge.Value = "0";
        hdn_TotalOctroiServiceCharge.Value = "0";
        hdn_totalservicetax.Value = "0";
        hdn_totaloctamt.Value = "0";
        hdn_totalgcamount.Value = "0";

        hdn_Total_Additional_Charges.Value = "0";

        hdn_Is_Series_Required.Value = objComm.Get_Values_Where("Ec_master_company_Parameters", "Is_Transport_Bill_Series_Required", "", "Is_Transport_Bill_Series_Required", false).Tables[0].Rows[0]["Is_Transport_Bill_Series_Required"].ToString();
        hdn_Max_Length.Value = objComm.Get_Values_Where("ec_master_company_gc_parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString();
    }

    public void Set_Label_To_Hiddenvalue()
    {
        lbl_totalgc.Text = hdn_totalgc.Value;
        lbl_totalsubtot.Text = hdn_totalsubtot.Value;

        lbl_totalLRSerTax.Text = hdn_totalLRSerTax.Value;
        lbl_totalRound_Off.Text = hdn_totalRound_Off.Value;
        lbl_totalLRTotal.Text = hdn_totalLRTotal.Value; 

        lbl_totalothercharge.Text = hdn_totalothercharge.Value;
        lbl_totalservicetax.Text = hdn_totalservicetax.Value;
        lbl_totaloctamt.Text = hdn_totaloctamt.Value;
        lbl_totalgcamount.Text = hdn_totalgcamount.Value;
    }
    private void Calculate_Total()
    {
        Set_Hidden_and_LABEL_To_Zero();
        Total_No_Of_GC = 0;

        if (StateManager.IsValidSession("BindBillGrid"))
        {
            if (SessionBillGrid.Rows.Count > 0)
            {
                DataView dv = new DataView(SessionBillGrid, "Att = true", "Att", DataViewRowState.CurrentRows);
                DataTable dt = dv.ToTable();

                Total_No_Of_GC = dt.Rows.Count;

                TotalLRSerTax = SessionBillGrid.Compute("sum(GCService_Tax_Amount)", "Att = true").ToString();
                TotalRound_Off = SessionBillGrid.Compute("sum(Round_Off)", "Att = true").ToString();
                TotalLRTotal = SessionBillGrid.Compute("sum(Total_GC_Amount)", "Att = true").ToString();

                TotalSubTotal = SessionBillGrid.Compute("sum(Actual_Sub_Total)", "Att = true").ToString();
                TotalOtherCharge = SessionBillGrid.Compute("sum(FA_Other_Charges)", "Att = true").ToString();
                TotalOctroiFormCharge = SessionBillGrid.Compute("sum(Octroi_Form_Charges)", "Att = true").ToString();
                TotalOctroiServiceCharge = SessionBillGrid.Compute("sum(Octroi_Service_Charges)", "Att = true").ToString();
                TotalServiceTax = SessionBillGrid.Compute("sum(Service_Tax_Amount)", "Att = true").ToString();
                TotalOctAmount = SessionBillGrid.Compute("sum(Oct_Amount)", "Att = true").ToString();
                TotalGCAmount = SessionBillGrid.Compute("sum(Bill_GC_Amt)", "Att = true").ToString();
                decimal tempTotalGCAmount = Util.String2Decimal(TotalGCAmount);
                if (tempTotalGCAmount <= 0)
                {
                    tempTotalGCAmount = 0;
                }

                Total_Additional_Charges = tempTotalGCAmount + TotalAmount;
                //Total_Additional_Charges = 0 + TotalAmount;
            }
            else
            {
                TotalLRSerTax = "0";
                TotalRound_Off = "0";
                TotalLRTotal = "0";
                TotalSubTotal = "0";
                TotalOtherCharge = "0";
                TotalOctroiFormCharge = "0";
                TotalOctroiServiceCharge = "0";
                TotalServiceTax = "0";
                TotalOctAmount = "0";
                TotalGCAmount = "0";
            }
        }
    }

    private void calculate_griddetails()
    {
        Total_No_Of_GC = 0;
        int i;

        if (dg_Bill.Items.Count > 0)
        {
            for (i = 0; i <= dg_Bill.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Bill.Items[i].FindControl("Chk_Attach");
                txt_GCRemarks = (TextBox)dg_Bill.Items[i].FindControl("txt_GCRemarks");

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                }
                SessionBillGrid.Rows[i]["Att"] = chk.Checked;
                SessionBillGrid.Rows[i]["GC_Remarks"] = txt_GCRemarks.Text.Trim();
            }
        }
    }

    private bool GridValidation()
    {
        bool ATS = true;
        Label lbl_GCNo,lbl_GCDate;
        int i = 0;

        if (dg_Bill.Items.Count > 0)
        {
            for (i = 0; i <= dg_Bill.Items.Count - 1; i++)
            {
                lbl_GCNo = (Label)dg_Bill.Items[i].FindControl("lbl_GCNo");
                lbl_GCDate = (Label)dg_Bill.Items[i].FindControl("lbl_GCDate");
                chk = (CheckBox)dg_Bill.Items[i].FindControl("Chk_Attach");

                if (chk.Checked == true && BillDate < Convert.ToDateTime(lbl_GCDate.Text))
                {
                    errorMessage = "Bill Date can't be less than " + CompanyManager.getCompanyParam().GcCaption + " Date For " + CompanyManager.getCompanyParam().GcCaption + " : " + lbl_GCNo.Text;
                    ATS = false;
                    break;
                }
            }
        }

        return ATS;
    }

    public void ClearVariables()
    {
        SessionBillGrid = null;
        SessionBillOtherChargeGrid = null;
    }
    #endregion

    #region ControlsBind

    public DataTable BindBillType
    {
        set
        {
            ddl_BillType.DataTextField = "Bill_Type";
            ddl_BillType.DataValueField = "Bill_Type_Id";
            ddl_BillType.DataSource = value;
            ddl_BillType.DataBind();
        }
    }

    public void BindBillGrid()
    {
        calculate_service_tax_and_total_amount();
        dg_Bill.DataSource = SessionBillGrid;
        dg_Bill.DataBind();
        Calculate_Total();
        HideGridCol();
    }

    private void calculate_service_tax_and_total_amount()
    {
        decimal Service_Tax_Percent = 0, Bill_Other_Charges = 0;
        decimal Octroi_Form_Charge = 0, Octroi_Service_Charge = 0;
        decimal Actual_Taxable_Amount = 0, GC_Actual_Taxable_Amount = 0;
        decimal Taxable_Amount = 0, GC_Taxable_Amount = 0;
        decimal Service_Tax_Amount = 0, GC_Service_Tax_Amount = 0;
        decimal Temp_Service_Tax_Amount = 0, Temp_GC_Service_Tax_Amount = 0;
        decimal Bill_GC_Amt = 0, Total_GC_Amount = 0;
        decimal Octroi_Amount = 0, Sub_Total = 0;
        decimal AbatePercentage = 0;

        decimal GCService_Tax_Amount = 0; //LR ST
        bool Is_ST_Abatment = false;
        int service_type_id;

        int booking_type_id = 0;
        TextBox txt_Octroi_Form_Charge,txt_Octroi_Service_Charge;

        if (SessionBillGrid.Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow dr in SessionBillGrid.Rows)
            {
                if (BindGridCallFrom == 3)
                {
                    CheckBox chk_Att = (CheckBox)dg_Bill.Items[i].FindControl("Chk_Attach");
                    txt_Octroi_Form_Charge = (TextBox)dg_Bill.Items[i].FindControl("txt_Octroi_Form_Charges");
                    txt_Octroi_Service_Charge = (TextBox)dg_Bill.Items[i].FindControl("txt_Octroi_Service_Charges");

                    SessionBillGrid.Rows[i]["Att"] = chk_Att.Checked;

                    if (Bill_ForID == 2 || Bill_ForID == 3)
                    {
                        dr["Octroi_Form_Charges"] = Util.String2Decimal(txt_Octroi_Form_Charge.Text) == -1 ? 0 : Convert.ToDecimal(txt_Octroi_Form_Charge.Text);
                        dr["Octroi_Service_Charges"] = Util.String2Decimal(txt_Octroi_Service_Charge.Text) == -1 ? 0 : Convert.ToDecimal(txt_Octroi_Service_Charge.Text);
                    }
                }

                Octroi_Form_Charge = Util.String2Decimal(dr["Octroi_Form_Charges"].ToString());
                Octroi_Service_Charge = Util.String2Decimal(dr["Octroi_Service_Charges"].ToString());
                Service_Tax_Percent = Util.String2Decimal(dr["Service_Tax_Percent"].ToString());
                Bill_Other_Charges = Util.String2Decimal(dr["FA_Other_Charges"].ToString());
                Sub_Total = Util.String2Decimal(dr["Actual_Sub_Total"].ToString());
                Octroi_Amount = Util.String2Decimal(dr["Oct_Amount"].ToString());
                service_type_id = Util.String2Int(dr["Service_Type_Id"].ToString());
                Is_ST_Abatment = Util.String2Bool(dr["Is_ST_Abatment"].ToString());
                Total_GC_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
                AbatePercentage = Util.String2Decimal(dr["AbatePercentage"].ToString());

                GCService_Tax_Amount = Util.String2Decimal(dr["GCService_Tax_Amount"].ToString());

                if (Bill_ForID == 1) // Frieght
                {

                    if (GCService_Tax_Amount > 0)
                    {
                        Actual_Taxable_Amount = Bill_Other_Charges;
                        GC_Actual_Taxable_Amount = Bill_Other_Charges;
                    }
                    else
                    {
                        Actual_Taxable_Amount = Bill_Other_Charges + Sub_Total;
                        GC_Actual_Taxable_Amount = Bill_Other_Charges + Sub_Total;
                    }

                    
                    Octroi_Service_Charge = 0;
                    Octroi_Form_Charge = 0;
                    Octroi_Amount = 0;
                }
                else if (Bill_ForID == 2) //Octroi
                {
                    Actual_Taxable_Amount = Octroi_Service_Charge + Octroi_Form_Charge;
                    GC_Actual_Taxable_Amount = 0;
                    Sub_Total = 0;
                    Bill_Other_Charges = 0;
                }
                else
                {
                    if (GCService_Tax_Amount > 0)
                    {
                        Actual_Taxable_Amount = Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge;
                        GC_Actual_Taxable_Amount = Bill_Other_Charges;
                    }
                    else
                    {
                        Actual_Taxable_Amount = Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge + Sub_Total;
                        GC_Actual_Taxable_Amount = Bill_Other_Charges + Sub_Total;

                    }
                }


                Taxable_Amount = Actual_Taxable_Amount * AbatePercentage;
                GC_Taxable_Amount = GC_Actual_Taxable_Amount * AbatePercentage;
                //Taxable_Amount = Actual_Taxable_Amount * Util.String2Decimal("0.25");
                //GC_Taxable_Amount = GC_Actual_Taxable_Amount * Util.String2Decimal("0.25");

                if (service_type_id == 2 && Is_ST_Abatment == false)
                {
                    Taxable_Amount = Actual_Taxable_Amount;
                    GC_Taxable_Amount = GC_Actual_Taxable_Amount;
                }

                //Service_Tax_Amount = Taxable_Amount * Service_Tax_Percent / 100;
                //GC_Service_Tax_Amount = GC_Taxable_Amount * Service_Tax_Percent / 100;

                //Service_Tax_Amount = Math.Round(Service_Tax_Amount, MidpointRounding.AwayFromZero);
                //GC_Service_Tax_Amount = Math.Round(GC_Service_Tax_Amount, MidpointRounding.AwayFromZero);

                Temp_Service_Tax_Amount = Taxable_Amount * Service_Tax_Percent / 100;
                Temp_GC_Service_Tax_Amount = GC_Taxable_Amount * Service_Tax_Percent / 100;

                Temp_Service_Tax_Amount = Math.Round(Temp_Service_Tax_Amount, MidpointRounding.AwayFromZero);
                Temp_GC_Service_Tax_Amount = Math.Round(Temp_GC_Service_Tax_Amount, MidpointRounding.AwayFromZero);

                booking_type_id = Util.String2Int(dr["booking_type_id"].ToString());
                //////////////////////////////////////////////////////////////////////////////////////
                if (Bill_ForID == 1) // Frieght
                {
                    //Sundry
                    if (booking_type_id == 1 && (Bill_Other_Charges + Sub_Total) < 750)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id == 1 && (Bill_Other_Charges + Sub_Total) < 750)
                    { Temp_GC_Service_Tax_Amount = 0; }

                    //FTL
                    if (booking_type_id != 1 && (Bill_Other_Charges + Sub_Total) < 1500)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id != 1 && (Bill_Other_Charges + Sub_Total) < 1500)
                    { Temp_GC_Service_Tax_Amount = 0; }
                }
                else if (Bill_ForID == 2) //Octroi
                {
                    //Sundry
                    if (booking_type_id == 1 && (Octroi_Service_Charge + Octroi_Form_Charge) < 750)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id == 1 && (Octroi_Service_Charge + Octroi_Form_Charge) < 750)
                    { Temp_GC_Service_Tax_Amount = 0; }

                    //FTL
                    if (booking_type_id != 1 && (Octroi_Service_Charge + Octroi_Form_Charge) < 1500)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id != 1 && (Octroi_Service_Charge + Octroi_Form_Charge) < 1500)
                    { Temp_GC_Service_Tax_Amount = 0; }


                }
                else
                {
                    //Sundry
                    if (booking_type_id == 1 && (Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge + Sub_Total) < 750)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id == 1 && (Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge + Sub_Total) < 750)
                    { Temp_GC_Service_Tax_Amount = 0; }

                    //FTL
                    if (booking_type_id != 1 && (Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge + Sub_Total) < 1500)
                    { Temp_Service_Tax_Amount = 0; }

                    if (booking_type_id != 1 && (Bill_Other_Charges + Octroi_Form_Charge + Octroi_Service_Charge + Sub_Total) < 1500)
                    { Temp_GC_Service_Tax_Amount = 0; }

                }
                //////////////////////////////////////////////////////////////////////////////////////
                //if (Util.String2Bool(dr["Is_Service_Tax_Applicable_GC"].ToString()) == false)
                //{
                //    Service_Tax_Amount = 0;
                //    GC_Service_Tax_Amount = 0;
                //}
                if (Util.String2Bool(dr["Is_Service_Tax_Applicable_GC"].ToString()) == false)
                {
                    Temp_Service_Tax_Amount = 0;
                    Temp_GC_Service_Tax_Amount = 0;
                }
                if (service_type_id == 2 && Is_ST_Abatment == false)
                {
                    Temp_Service_Tax_Amount = Taxable_Amount * Service_Tax_Percent / 100;;
                    Temp_GC_Service_Tax_Amount = GC_Taxable_Amount * Service_Tax_Percent / 100; ;
                }
                Service_Tax_Amount = Temp_Service_Tax_Amount;
                GC_Service_Tax_Amount = Temp_GC_Service_Tax_Amount;

                dr["Service_Tax_Amount"] = Service_Tax_Amount;
                //dr["GCService_Tax_Amount"] = GC_Service_Tax_Amount;

                //Bill_GC_Amt = Bill_Other_Charges + Octroi_Service_Charge + Octroi_Form_Charge +
                //                Service_Tax_Amount + Octroi_Amount;
                Bill_GC_Amt = Total_GC_Amount + Bill_Other_Charges + Octroi_Service_Charge + Octroi_Form_Charge +
                                GC_Service_Tax_Amount + Octroi_Amount;

                //Total_GC_Amount = Sub_Total + Bill_Other_Charges + GC_Service_Tax_Amount;
                //Total_GC_Amount = Sub_Total; 

                Bill_GC_Amt = Math.Round(Bill_GC_Amt, MidpointRounding.AwayFromZero);
                //Total_GC_Amount = Math.Round(Total_GC_Amount, MidpointRounding.AwayFromZero);

                dr["Bill_GC_Amt"] = Bill_GC_Amt;
                //dr["Total_GC_Amount"] = Total_GC_Amount;

                i = i + 1;
            }
        }
    }

    public DataTable SessionBillGrid
    {
        get { return StateManager.GetState<DataTable>("BindBillGrid"); }
        set
        {
            StateManager.SaveState("BindBillGrid", value);
            if (StateManager.Exist("BindBillGrid"))
            {
                BindBillGrid();
            }
        }
    }

    public DataTable SessionBillOtherChargeGrid
    {
        get { return StateManager.GetState<DataTable>("SessionBillOtherChargeGrid"); }
        set {StateManager.SaveState("SessionBillOtherChargeGrid", value);}
    } 

    public String BillDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            DataView view = objComm.Get_View_Table(SessionBillGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "BillGridDetails";
            return _objDs.GetXml().ToLower();
        }
    }

    public String BillOtherChargeGridXML
    {
        get
        {
            DataSet _objDs1 = new DataSet();
            _objDs1.Tables.Add(SessionBillOtherChargeGrid.Copy());

            _objDs1.Tables[0].TableName = "BillOtherChargeGrid";
            return _objDs1.GetXml().ToLower();
        }
    }
    public String LedgerDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionLedgerDT.Copy());

            _objDs.Tables[0].TableName = "LedgerDetails";
            return _objDs.GetXml().ToLower();
        }
    }


    public DataTable SessionLedgerDT
    {
        set { StateManager.SaveState("Ledger_DT", value); }
        get { return StateManager.GetState<DataTable>("Ledger_DT"); }
    }
    public DataTable Bind_dg_Voucher
    {
        set
        {
            SessionLedgerDT = value;

            _dgRowCount = value.Rows.Count;

            dg_Voucher.DataSource = value;
            dg_Voucher.DataBind(); 
            TotalAmount = TotalAmount;
            if (TotalGCAmount =="")
            {
                TotalGCAmount ="0";
            }

            Total_Additional_Charges = Convert.ToDecimal(TotalGCAmount) + TotalAmount;
        }
    }

    private void SetLedgerId(string value, string text)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id"; 
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    } 

    public int LedgerId
    {
        get { return !IsPostBack == true ? -1 : Util.String2Int(ddl_Ledger.SelectedValue); }
    }


    public decimal Amount
    {
        set { txt_Amount.Text = value.ToString(); }
        get { return convertToDecimal(txt_Amount.Text); }
    }
    #endregion

    #region Voucher_GridEvents
    protected void dg_Voucher_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            InsertUpdateVoucher(e);
        }

        else if (e.CommandName == "Edit")
        {
            dg_Voucher.EditItemIndex = e.Item.ItemIndex;
            Bind_dg_Voucher = SessionLedgerDT;
            dg_Voucher.ShowFooter = false; 
        }

        else if (e.CommandName == "Cancel")
        {
            dg_Voucher.EditItemIndex = -1;
            Bind_dg_Voucher = SessionLedgerDT;
            dg_Voucher.ShowFooter = true;
        }

        else if (e.CommandName == "Delete")
        {

            string ledger_id = SessionLedgerDT.Rows[e.Item.ItemIndex]["Ledger_Id"].ToString(); 
            SessionLedgerDT.Rows[e.Item.ItemIndex].Delete();

            SessionLedgerDT.AcceptChanges();
            Bind_dg_Voucher = SessionLedgerDT;
        }
    }



    protected void dg_Voucher_ItemDataBound(object source, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string LedgerName = SessionLedgerDT.Rows[e.Item.ItemIndex]["Ledger_Name"].ToString();
            int LedgerID = convertToInt(SessionLedgerDT.Rows[e.Item.ItemIndex]["Ledger_Id"]);


        }
        if (e.Item.ItemType == ListItemType.Header)
        { 
              
        }
        //if (!IsPostBack)
        //{ SetLedgerId("", ""); }

        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            findControls(e.Item);

            if (_dgRowCount == 0)
            {
                 
                
            }


            if (e.Item.ItemType == ListItemType.Footer)
            {
                 
            }


            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Ledger.DataTextField = "Ledger_Name";
                ddl_Ledger.DataValueField = "Ledger_Id";
                SetLedgerId(SessionLedgerDT.Rows[e.Item.ItemIndex]["Ledger_Id"].ToString(), SessionLedgerDT.Rows[e.Item.ItemIndex]["Ledger_Name"].ToString());

                LinkButton lnk_Delete = (LinkButton)e.Item.FindControl("lnk_Delete");
                lnk_Delete.Visible = false;

                 
            }

        }
    }

    #endregion

    private void findControls(DataGridItem item)
    {
       
        ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_Ledger");
        txt_Amount = (TextBox)item.FindControl("txt_Amount"); 
    }

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        TextBox Txt_Client;

        errorMessage = "";

        Txt_Client = (TextBox)ddl_Client.FindControl("txtBoxddl_Client");

        if (BillTypeID <= 0)
        {
            errorMessage = "Please Select Bill Type";
            ddl_BillType.Focus();
        }
        else if (Datemanager.IsValidProcessDate("FIN_TRANSBILL", BillDate) == false)
        {
            errorMessage = "Please Select Valid Bill Date";
        }
        else if (keyID <= 0 && Is_SeriesNo_Required == true && (Util.String2Int(BillNo) < Start_No || Util.String2Int(BillNo) > End_No))
        {
            errorMessage = "Bill No. Should be Between " + Start_No + " and " + End_No;
            lbl_TransBillNo.Focus();
        }
        else if (ClientID <= 0)
        {
            errorMessage = "Please Select Client";
            Txt_Client.Focus();
        }
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (Util.String2Decimal(hdn_totalgcamount.Value) <= 0)
        {
            errorMessage = "Total "+ CompanyManager.getCompanyParam().GcCaption  +" Amount should be greater than Zero";
        }
        else if (GridValidation() == false)
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        if (_isValid == true)
        {
            calculate_griddetails();
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public string Flag
    {
        get { return _flag; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Set_Standard_Caption();

        Mode =  Request.QueryString["Mode"].ToString();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ddl_Client.DataTextField = "Client_Name";
        ddl_Client.DataValueField = "Client_Id";

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save));

        Wuc_Region_Area_Branch1.ShowAll = true;
        rbtn_TransBillFor.Visible  = false;

        //Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
        //Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnBkgDlyBranchSelectionEvent);

        if (!IsPostBack)
        {
            Set_Hidden_and_LABEL_To_Zero();
        }
        else 
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnBkgDlyBranchSelectionEvent);
        }

        objTransportBillPresenter = new TransportBillPresenter(this, IsPostBack);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                BillTypeID = 1; // for outward Bill
                //Bill_ForID = 3;
                Bill_ForID = 1;

                if (Is_SeriesNo_Required)
                {
                    lbl_Start_End_No.Visible = true;
                    lbl_TransBillNo.ReadOnly = false;
                    lbl_TransBillNo.MaxLength = Util.String2Int(hdn_Max_Length.Value);
                    lbl_TransBillNo.Attributes.Add("onkeypress", "return Only_Integers(" + lbl_TransBillNo.ClientID + ",event)");
                    
                    Get_Next_Series_No();
                    lbl_TransBillNo.CssClass = "TEXTBOX";
                }
                else
                {
                    Next_Bill_Number();
                    lbl_Start_End_No.Visible = false;

                    if (CompanyManager.getCompanyParam().ClientCode.ToLower() != "reach")
                    {
                        lbl_TransBillNo.ReadOnly = true;
                        lbl_TransBillNo.CssClass = "TEXTBOXASLABEL";
                    }
                }

                int _Client_Id;
                string Crypt,_Client_Name;

                Crypt = System.Web.HttpContext.Current.Request.QueryString["Client_Id"];

                if (Crypt != null)
                {
                    _Client_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                    Crypt = System.Web.HttpContext.Current.Request.QueryString["Client_Name"];
                    _Client_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);


                    if (_Client_Id > 0)
                    {
                        Raj.EC.Common.SetValueToDDLSearch(_Client_Name, Util.Int2String(_Client_Id), ddl_Client);
                        objTransportBillPresenter.FillGCDetails(1);
                        UpdatePanel1.Update();
                        upd_ClientBillingDetails.Update();
                        HideGridCol();
                    }

                }
                ddl_Client.FindControl("txtBoxddl_Client").Focus();
            }
            else
            {
                ddl_Client.Enabled = false;
                lbl_TransBillNo.ReadOnly = true;
            }

            

        }

        if (keyID > 0)
        {
            rbtn_TransBillFor.Enabled = false;
            Wuc_Region_Area_Branch1.Visible = false;
            rdl_BkgBranchOrDlyBranchWise.Visible = false;
        }

        Set_Label_To_Hiddenvalue();
        String Script = "<script type='text/javascript'>HideLabel();</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);

        
    }

    private void InsertUpdateVoucher(DataGridCommandEventArgs e)
    {
        findControls(e.Item);
        if (ValidateVoucherValues() == false)
        { return; }

        DataTable objDT = SessionLedgerDT;
        DataRow objDR = null;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
        }
        else
        {
            objDR = objDT.Rows[e.Item.ItemIndex];
        }

        try
        {
            objDR["Ledger_Id"] = LedgerId;
            objDR["Ledger_Name"] = ddl_Ledger.SelectedItem;
            objDR["Amount"] = Amount; 

             
            

            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(objDR); 
            }

            if (e.CommandName == "Update")
            {
                if (Convert.ToInt32(objDT.Rows[e.Item.ItemIndex]["Ledger_Id"]) != LedgerId)
                {
                    
                }

                dg_Voucher.EditItemIndex = -1;
                dg_Voucher.ShowFooter = true;
            }
            objDT.AcceptChanges();
            Bind_dg_Voucher = objDT;
        }
        catch (ConstraintException)
        {

            if (e.CommandName == "Edit")
            {
                objDR.RejectChanges();
            }
            errorMessage = "Duplicate Particulers";
        }
    }

    private bool ValidateVoucherValues()
    {
        bool _isValid = false;
        if (LedgerId <= 0)
        {
            errorMessage = "Please Enter Particulers";
        } 
        else if (Amount == 0)
        {
            errorMessage = "Please Enter Amount Amount";
        }
        
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    private void ff()
    {
        ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)dg_Voucher.FindControl("ddl_Ledger");
    }
    public void Get_Next_Series_No()
    {
        objComm.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 7);

        Document_Allocation_ID = _Document_Allocation_ID;
        Start_No = _Start_No;
        End_No = _End_No;
        Next_No = _Next_No;

        if (_Next_No <= 0)
        {
            Raj.EC.Common.DisplayErrors(1013);
        }

        _Padded_Next_No = _Next_No.ToString("0000000");
        Padded_Next_No = _Padded_Next_No;
        BillNo = Padded_Next_No;

        Start_End_No = "(" + Start_No + " - " + End_No + ")";
    }

    private void Next_Bill_Number()
    {       
       BillNo = objComm.Get_Next_Number();
    }

    private void Set_Standard_Caption()
    {
        string Caption = CompanyManager.getCompanyParam().GcCaption;

        Lbl_TotalGC_Text.Text = "Total  " + Caption + " :";
        dg_Bill.Columns[1].HeaderText = Caption + "  No";
        dg_Bill.Columns[11].HeaderText = Caption + "  Sub Total";
        UpdatePanel1.Update();
    } 

    protected void ddl_Client_TxtChange(object sender, EventArgs e)
    {
        objTransportBillPresenter.FillGCDetails(1);
        UpdatePanel1.Update();
        upd_ClientBillingDetails.Update();
        HideGridCol();
    }

    protected void dg_Bill_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string FreightUrl, OChargeUrl;


        if (e.Item.ItemIndex != -1)
        {
            int booking_type_id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "booking_type_id").ToString());
            decimal Actual_Sub_Total = Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "Actual_Sub_Total").ToString());

            chk = (CheckBox)e.Item.FindControl("Chk_Attach");
            lbtn_SubTotal = (LinkButton)e.Item.FindControl("lbtn_SubTotal");
            lbtn_OtherCharge = (LinkButton)e.Item.FindControl("lbtn_OtherCharge");
            txt_GCRemarks = (TextBox)e.Item.FindControl("txt_GCRemarks");

            hdn_GcId = (HiddenField)e.Item.FindControl("hdn_GcId");

            int GCId = Util.String2Int(hdn_GcId.Value);

            FreightUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Finance/Accounting Vouchers/FrmTransportBillFreight.aspx?ItemIndex=" +
                         ClassLibraryMVP.Util.EncryptInteger(e.Item.ItemIndex) + "&Mode=" + Mode + "&Menu_Item_Id=MQA0ADMA";

            OChargeUrl = ClassLibraryMVP.Util.GetBaseURL() + 
                         "/Finance/Accounting Vouchers/FrmTransportBillOtherCharge.aspx?ItemIndex=" + 
                         ClassLibraryMVP.Util.EncryptInteger(e.Item.ItemIndex) + 
                         "&GCID=" + ClassLibraryMVP.Util.EncryptInteger(GCId) +
                         "&Mode=" + Mode + "&IsFromGC=false&Menu_Item_Id=MQA0ADMA";

            if (Convert.ToBoolean(SessionBillGrid.Rows[e.Item.ItemIndex]["CanEdit"]) == true)
            {
                lbtn_SubTotal.Attributes.Add("onclick", "return Show_GC_SubTotal('" + FreightUrl + "','" + chk.ClientID + "');");
                //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                //{
                    lbtn_OtherCharge.Attributes.Add("onclick", "return Show_GC_OtherCharge('" + OChargeUrl + "','" + chk.ClientID + "');");
                //}
                //chk.Attributes.Add("onblur", "return setFocusonSubTotal('" + lbtn_SubTotal.ClientID + "');");
                lbtn_SubTotal.Attributes.Add("onblur", "return setFocusonOtherCharge('" + lbtn_OtherCharge.ClientID + "');"); 

            }

            lbtn_SubTotal.ToolTip = SessionBillGrid.Rows[e.Item.ItemIndex]["Bill_Ratio"].ToString() + " % of " +
                                    SessionBillGrid.Rows[e.Item.ItemIndex]["Actual_Sub_Total"].ToString();

            if (SessionBillGrid.Rows[e.Item.ItemIndex]["Is_Multiple_Billing"].ToString() == "True")
            {
                e.Item.Cells[11].Enabled = false;                
            }
            else
            {
                 e.Item.Cells[11].Enabled = true ;
            }

            if (Convert.ToBoolean(SessionBillGrid.Rows[e.Item.ItemIndex]["CanEdit"]) == false)
            {
                chk.Enabled = false;
                lbtn_SubTotal.Enabled = false;
                lbtn_OtherCharge.Enabled = false;
                txt_GCRemarks.Enabled = false;
            }

            if (DataBinder.Eval(e.Item.DataItem, "Consignor_Name").ToString().Substring(0,3) != ClientName.Substring(0,3) && DataBinder.Eval(e.Item.DataItem, "Consignee_Name").ToString().Substring(0,4) != ClientName.Substring(0,4))
            {
                e.Item.BackColor = System.Drawing.Color.Yellow;
            }

        }
    }

    protected void btn_update_grid_Click(object sender, EventArgs e)
    {
        BindBillGrid();
    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        //calculate_griddetails();
        objTransportBillPresenter.Save();
    }

    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        //calculate_griddetails();
        objTransportBillPresenter.Save();
    }

    protected void rbtn_TransBillFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTransportBillPresenter.FillGCDetails(2);
        UpdatePanel1.Update();
        Set_Hidden_and_LABEL_To_Zero();
        UpdatePanel3.Update();
        UpdatePanel2.Update();
    }

    private void OnBkgDlyBranchSelectionEvent(object sender, EventArgs e)
    {
        objTransportBillPresenter.FillGCDetails(2);
        UpdatePanel1.Update();
        Set_Hidden_and_LABEL_To_Zero();
        UpdatePanel3.Update();
        UpdatePanel2.Update();
    }

    protected void rdl_BkgBranchOrDlyBranchWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTransportBillPresenter.FillGCDetails(2);
        UpdatePanel1.Update();
        Set_Hidden_and_LABEL_To_Zero();
        UpdatePanel3.Update();
        UpdatePanel2.Update();
    }


    protected void txt_Octroi_Form_Charges_TextChanged(object sender, EventArgs e)
    {
        Set_Hidden_and_LABEL_To_Zero();
        BindGridCallFrom = 3;
        BindBillGrid();

        TextBox txt_Octroi_Form_Charges = (TextBox)sender;
        DataGridItem dgBill = (DataGridItem)(txt_Octroi_Form_Charges.Parent.Parent);

        if (dgBill.ItemType == ListItemType.Item || dgBill.ItemType == ListItemType.AlternatingItem)
        {
            TextBox txt_Octroi_Service_Charge = (TextBox)(dgBill.FindControl("txt_Octroi_Service_Charges"));
            txt_Octroi_Service_Charge.Focus();
        }
    }
    protected void txt_Octroi_Service_Charges_TextChanged(object sender, EventArgs e)
    {
        Set_Hidden_and_LABEL_To_Zero();
        BindGridCallFrom = 3;
        BindBillGrid();
    }

    public void HideGridCol()
    {
        dg_Bill.Columns[5].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Dly Branch
        dg_Bill.Columns[5].ItemStyle.CssClass = "HIDEGRIDCOL";
        dg_Bill.Columns[5].FooterStyle.CssClass = "HIDEGRIDCOL";

        dg_Bill.Columns[6].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Dly Date
        dg_Bill.Columns[6].ItemStyle.CssClass = "HIDEGRIDCOL";
        dg_Bill.Columns[6].FooterStyle.CssClass = "HIDEGRIDCOL";

        dg_Bill.Columns[8].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Dly Type
        dg_Bill.Columns[8].ItemStyle.CssClass = "HIDEGRIDCOL";
        dg_Bill.Columns[8].FooterStyle.CssClass = "HIDEGRIDCOL";


        if (Bill_ForID == 1)
        {

            //dg_Bill.Columns[13].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Form Charge
            //dg_Bill.Columns[13].ItemStyle.CssClass = "HIDEGRIDCOL";
            //dg_Bill.Columns[13].FooterStyle.CssClass = "HIDEGRIDCOL";

            //dg_Bill.Columns[14].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Service Charge
            //dg_Bill.Columns[14].ItemStyle.CssClass = "HIDEGRIDCOL";
            //dg_Bill.Columns[14].FooterStyle.CssClass = "HIDEGRIDCOL";

            //dg_Bill.Columns[16].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Charge
            //dg_Bill.Columns[16].ItemStyle.CssClass = "HIDEGRIDCOL";
            //dg_Bill.Columns[16].FooterStyle.CssClass = "HIDEGRIDCOL";

            dg_Bill.Columns[16].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Service Charge
            dg_Bill.Columns[16].ItemStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[16].FooterStyle.CssClass = "HIDEGRIDCOL";

            dg_Bill.Columns[17].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Charge
            dg_Bill.Columns[17].ItemStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[17].FooterStyle.CssClass = "HIDEGRIDCOL";

            dg_Bill.Columns[19].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Octroi Form Charge
            dg_Bill.Columns[19].ItemStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[19].FooterStyle.CssClass = "HIDEGRIDCOL";

        }
        else
        {

            //dg_Bill.Columns[13].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[13].ItemStyle.CssClass = "";
            //dg_Bill.Columns[13].FooterStyle.CssClass = "";

            //dg_Bill.Columns[14].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[14].ItemStyle.CssClass = "";
            //dg_Bill.Columns[14].FooterStyle.CssClass = "";

            //dg_Bill.Columns[16].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[16].ItemStyle.CssClass = "";
            //dg_Bill.Columns[16].FooterStyle.CssClass = "";

            dg_Bill.Columns[16].HeaderStyle.CssClass = "";
            dg_Bill.Columns[16].ItemStyle.CssClass = "";
            dg_Bill.Columns[16].FooterStyle.CssClass = "";

            dg_Bill.Columns[17].HeaderStyle.CssClass = "";
            dg_Bill.Columns[17].ItemStyle.CssClass = "";
            dg_Bill.Columns[17].FooterStyle.CssClass = "";

            dg_Bill.Columns[19].HeaderStyle.CssClass = "";
            dg_Bill.Columns[19].ItemStyle.CssClass = "";
            dg_Bill.Columns[19].FooterStyle.CssClass = "";
        }


        if (Bill_ForID == 2)
        {
            //dg_Bill.Columns[11].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Sub Total
            //dg_Bill.Columns[11].ItemStyle.CssClass = "HIDEGRIDCOL";
            //dg_Bill.Columns[11].FooterStyle.CssClass = "HIDEGRIDCOL";

            //dg_Bill.Columns[12].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Other Charge
            //dg_Bill.Columns[12].ItemStyle.CssClass = "HIDEGRIDCOL";
            //dg_Bill.Columns[12].FooterStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[10].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Sub Total
            dg_Bill.Columns[10].ItemStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[10].FooterStyle.CssClass = "HIDEGRIDCOL";

            dg_Bill.Columns[11].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Other Charge
            dg_Bill.Columns[11].ItemStyle.CssClass = "HIDEGRIDCOL";
            dg_Bill.Columns[11].FooterStyle.CssClass = "HIDEGRIDCOL";
        }
        else
        {
            //dg_Bill.Columns[11].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[11].ItemStyle.CssClass = "";
            //dg_Bill.Columns[11].FooterStyle.CssClass = "";

            //dg_Bill.Columns[12].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[12].ItemStyle.CssClass = "";
            //dg_Bill.Columns[12].FooterStyle.CssClass = "";

            dg_Bill.Columns[10].HeaderStyle.CssClass = "";
            dg_Bill.Columns[10].ItemStyle.CssClass = "";
            dg_Bill.Columns[10].FooterStyle.CssClass = "";

            dg_Bill.Columns[11].HeaderStyle.CssClass = "";
            dg_Bill.Columns[11].ItemStyle.CssClass = "";
            dg_Bill.Columns[11].FooterStyle.CssClass = "";
        }

        if (Bill_ForID == 3)
        {
            //dg_Bill.Columns[11].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[11].ItemStyle.CssClass = "";
            //dg_Bill.Columns[11].FooterStyle.CssClass = "";

            //dg_Bill.Columns[12].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[12].ItemStyle.CssClass = "";
            //dg_Bill.Columns[12].FooterStyle.CssClass = "";

            dg_Bill.Columns[10].HeaderStyle.CssClass = "";
            dg_Bill.Columns[10].ItemStyle.CssClass = "";
            dg_Bill.Columns[10].FooterStyle.CssClass = "";

            dg_Bill.Columns[11].HeaderStyle.CssClass = "";
            dg_Bill.Columns[11].ItemStyle.CssClass = "";
            dg_Bill.Columns[11].FooterStyle.CssClass = "";

            //dg_Bill.Columns[13].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[13].ItemStyle.CssClass = "";
            //dg_Bill.Columns[13].FooterStyle.CssClass = "";

            //dg_Bill.Columns[14].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[14].ItemStyle.CssClass = "";
            //dg_Bill.Columns[14].FooterStyle.CssClass = "";

            //dg_Bill.Columns[16].HeaderStyle.CssClass = "";
            //dg_Bill.Columns[16].ItemStyle.CssClass = "";
            //dg_Bill.Columns[16].FooterStyle.CssClass = "";

            dg_Bill.Columns[16].HeaderStyle.CssClass = "";
            dg_Bill.Columns[16].ItemStyle.CssClass = "";
            dg_Bill.Columns[16].FooterStyle.CssClass = "";

            dg_Bill.Columns[17].HeaderStyle.CssClass = "";
            dg_Bill.Columns[17].ItemStyle.CssClass = "";
            dg_Bill.Columns[17].FooterStyle.CssClass = "";

            dg_Bill.Columns[19].HeaderStyle.CssClass = "";
            dg_Bill.Columns[19].ItemStyle.CssClass = "";
            dg_Bill.Columns[19].FooterStyle.CssClass = "";
        }

        //dg_Bill.Columns[18].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Remarks
        //dg_Bill.Columns[18].ItemStyle.CssClass = "HIDEGRIDCOL";
        //dg_Bill.Columns[18].FooterStyle.CssClass = "HIDEGRIDCOL";

        dg_Bill.Columns[21].HeaderStyle.CssClass = "HIDEGRIDCOL";  // Remarks
        dg_Bill.Columns[21].ItemStyle.CssClass = "HIDEGRIDCOL";
        dg_Bill.Columns[21].FooterStyle.CssClass = "HIDEGRIDCOL";


    }

    private void EnableDisableControls(bool value)
    { 
        
    
    }
}
