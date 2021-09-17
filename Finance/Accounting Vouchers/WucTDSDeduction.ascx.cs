using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;

//Name: Ankit champaneriya
//Date : 24/11/08

public partial class Accounting_Vouchers_wucTDSDeduction : System.Web.UI.UserControl, ITDSDeductionView
{
    #region class variable

    private TDSDeductionPresenter _TDSDeductionPresenter;
    private Common Common_Obj = new Common();
    private String Hierarchy_Code;
    private Int32 User_Id;
    private Int32 Year_Code;
    private Int32 Main_Id;
    private Int32 Voucher_Type_Id;
    bool isValid = false;
    #endregion

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 34;
            //return -1;
        }
    }

    #endregion

    #region InitInterface

    //public int Ledger_ID
    //{
    //    //get it from query string
    //    get
    //    {
    //        return Convert.ToInt32(ddl_Ledger.SelectedValue);
    //        // return Util.DecryptToInt(Request.QueryString["Ledger_ID"]);
    //    }
    //}


    public int TDS_Ledger_Id
    {
        set
        {
            if (keyID > 0)
            {
                ddl_TDS_Ledger.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
            }
            else
            {
                ddl_TDS_Ledger.SelectedValue = Util.Int2String(value);
            }
        }
        get
        {
            return Util.String2Int(ddl_TDS_Ledger.SelectedValue);
        }
    }



    public int Ledger_Group_Id
    {
        set
        {
            if (keyID > 0)
            {
                ddl_Ledger_Group.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
            }
            else
            {
                ddl_Ledger_Group.SelectedValue = Util.Int2String(value);
            }
        }
        get
        {
            return Util.String2Int(ddl_Ledger_Group.SelectedValue);
        }
    }


    public int Vendor_Id
    {
        set
        {
            //ddl_Truck_No.SelectedValue = Util.Int2String(value);
            hdn_Vendor_Id.Value = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(hdn_Vendor_Id.Value);
        }
    }

    public int Ledger_Account_Id
    {
        set
        {
            if (keyID > 0)
            {
                ddl_Ledger_Account.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
            }
            else
            {
                ddl_Ledger_Account.SelectedValue = Util.Int2String(value);
            }

        }
        get
        {
            return Util.String2Int(ddl_Ledger_Account.SelectedValue);
        }
    }


    public String Bill_No
    {
        set
        {
            if (keyID > 0)
            {
                ddl_Name.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
            }

            //ddl_Name.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return ddl_Name.SelectedItem.Text;
        }
    }

    public string TDS_Ledger_Id_View
    {
        set
        {
            ddl_TDS_Ledger.Items.Clear();
            ddl_TDS_Ledger.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
        }
    }

    public string Ledger_Group_Id_View
    {
        set
        {
            ddl_Ledger_Group.Items.Clear();
            ddl_Ledger_Group.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
        }
    }
    public string Ledger_Account_Id_View
    {
        set
        {
            ddl_Ledger_Account.Items.Clear();
            ddl_Ledger_Account.Items.Insert(0, new ListItem(Convert.ToString(value), Convert.ToString(value)));
        }
    }

    public string Reference_No
    {
        set
        {
            txt_Reference_No.Text = value;
        }
        get
        {
            return txt_Reference_No.Text;
        }
    }

    public string Narration
    {
        set
        {
            txt_Narration.Text = value;
        }
        get
        {
            return txt_Narration.Text;
        }
    }



    public string Total_Amount_Paid_Amount
    {
        set
        {
            lbl_Total_Amount_Paid_Amount.Text = value;
        }
        get
        {
            return lbl_Total_Amount_Paid_Amount.Text;
        }
    }


    public string Tax_Percent
    {
        set
        {
            lbl_Tax_Percent.Text = value;
        }
        get
        {
            return lbl_Tax_Percent.Text;
        }
    }

    public string Tax_Amount
    {
        set
        {
            lbl_Tax_Amount.Text = value;
        }
        get
        {
            return lbl_Tax_Amount.Text;
        }
    }

    public string Surcharge_Percent
    {
        set
        {
            lbl_Surcharge_Percent.Text = value;
        }
        get
        {
            return lbl_Surcharge_Percent.Text;
        }
    }

    public string Surcharge_Amount
    {
        set
        {
            lbl_Surcharge_Amount.Text = value;
        }
        get
        {
            return lbl_Surcharge_Amount.Text;
        }
    }

    public string Addtional_Surcharge_Percent
    {
        set
        {
            lbl_Addtional_Surcharge_Percent.Text = value;
        }
        get
        {
            return lbl_Addtional_Surcharge_Percent.Text;
        }
    }

    public string Addtional_Surcharge_Amount
    {
        set
        {
            lbl_Addtional_Surcharge_Amount.Text = value;
        }
        get
        {
            return lbl_Addtional_Surcharge_Amount.Text;
        }
    }


    public string Addl_Ed_Cess_Percent
    {
        set
        {
            lbl_Addl_Ed_Cess_Percent.Text = value;
        }
        get
        {
            return lbl_Addl_Ed_Cess_Percent.Text;
        }
    }


    public string Addl_Ed_Cess_Amount
    {
        set
        {
            lbl_Addl_Ed_Cess_Amount.Text = value;
        }
        get
        {
            return lbl_Addl_Ed_Cess_Amount.Text;
        }
    }


    public string Total_TDS_Amount
    {
        set
        {
            lbl_Total_TDS_Amount.Text = value;
        }
        get
        {
            return lbl_Total_TDS_Amount.Text;
        }
    }


    public string Less_TDS_Deducted_Till_Date_Amount
    {
        set
        {
            lbl_Less_TDS_Deducted_Till_Date_Amount.Text = value;
        }
        get
        {
            return lbl_Less_TDS_Deducted_Till_Date_Amount.Text;
        }
    }

    public string Net_TDS_To_Deduct_Amount
    {
        set
        {
            lbl_Net_TDS_To_Deduct_Amount.Text = value;
        }
        get
        {
            return lbl_Net_TDS_To_Deduct_Amount.Text;
        }
    }

    public string Credit_Days
    {
        set
        {
            lbl_Credit_Days.Text = value;
        }
        get
        {
            return lbl_Credit_Days.Text;
        }
    }

    public string Gross_Amount
    {
        set
        {
            lbl_Gross_Amount.Text = value;
        }
        get
        {
            return lbl_Gross_Amount.Text;
        }
    }

    public string Amount
    {
        set
        {
            lbl_Amount.Text = value;
        }
        get
        {
            return lbl_Amount.Text;
        }
    }

    public string Journal_No
    {
        set
        {
            lbl_Journal_No.Text = value;
        }
        get
        {
            return lbl_Journal_No.Text;
        }
    }


    public DateTime TDSDeduction_Date
    {
        //    set { Picker_Voucher_Date.SelectedDate = value; }
        //    get { return Picker_Voucher_Date.SelectedDate; }


        set { Picker.SelectedDate = value; }
        get { return Picker.SelectedDate; }
    }

    public DataTable BindLedgerGroup
    {
        set
        {
            ddl_Ledger_Group.DataSource = value;
            ddl_Ledger_Group.DataTextField = "Ledger_Group_Name";
            ddl_Ledger_Group.DataValueField = "Ledger_Group_Id";
            ddl_Ledger_Group.DataBind();

            ddl_Ledger_Group.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }


    public DataTable BindName
    {
        set
        {

            ddl_Name.DataSource = value;
            ddl_Name.DataTextField = "Ref_No";
            ddl_Name.DataValueField = "Ref_Id";
            ddl_Name.DataBind();

            ddl_Name.Items.Insert(0, new ListItem("--Select One--", "0"));

        }
    }

    public DataTable BindLedgerAccount
    {
        set
        {
            ddl_Ledger_Account.DataSource = value;
            ddl_Ledger_Account.DataTextField = "Ledger_Name";
            ddl_Ledger_Account.DataValueField = "Ledger_Id";
            ddl_Ledger_Account.DataBind();

            ddl_Ledger_Account.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }

    public DataTable BindTDSLedger
    {
        set
        {
            ddl_TDS_Ledger.DataSource = value;
            ddl_TDS_Ledger.DataTextField = "Ledger_Name";
            ddl_TDS_Ledger.DataValueField = "Ledger_Id";
            ddl_TDS_Ledger.DataBind();

            ddl_TDS_Ledger.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }





    #endregion

    #region Function


    public bool validateUI()
    {
        return true;
    }

    private bool Allow_To_Save()
    {

        errorMessage = "";

        //if (Picker.SelectedDate < Convert.ToDateTime( Param.getUserParam().Start_Date) ||
        //    Picker.SelectedDate > Convert.ToDateTime(Param.getUserParam().End_Date))

        if (Picker.SelectedDate < Convert.ToDateTime(UserManager.getUserParam().StartDate) ||
            Picker.SelectedDate > Convert.ToDateTime(UserManager.getUserParam().EndDate))
        {
            errorMessage = " TDSDeduction Date Should Be Within Current Finiancial Year...";

        }
        //else if (txt_Reference_No.Text == String.Empty)
        //{
        //    errorMessage = "Please Enter Reference No . ";
        //    txt_Reference_No.Focus();
        //}

        else if (Convert.ToInt32(ddl_Ledger_Group.SelectedValue) <= 0)
        {
            errorMessage = " Please Select Ledger Group...";
        }
        else if (Convert.ToInt32(ddl_Ledger_Account.SelectedValue) <= 0)
        {
            errorMessage = " Please Select Ledger Account...";
        }
        else if (Convert.ToInt32(ddl_TDS_Ledger.SelectedValue) <= 0)
        {
            errorMessage = " Please Select TDS Ledger...";
        }

        else if (Convert.ToString(ddl_Name.SelectedValue) == "0")
        {
            errorMessage = " Please Select Name ...";

        }

        else if (Convert.ToDecimal(Amount) == 0)
        {
            errorMessage = " TDS Amount Should Be Greater Than Zero...";
        }
        else if (Convert.ToDecimal(Gross_Amount) < Convert.ToDecimal(Amount))
        {
            errorMessage = " Gross Amount Should Be Greater Than TDS Amount ...";
        }
        else
        {
            isValid = true;
        }

        return isValid;

    }

    #endregion

    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        //Voucher_Type_Id = Convert.ToInt32(  Session["QueryString"]);

        btn_Save.Attributes.Add("onclick", Common_Obj.ClickedOnceScript_For_JS_Validation(Page,btn_Save));

        _TDSDeductionPresenter = new TDSDeductionPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            Hierarchy_Code = Convert.ToString(UserManager.getUserParam().HierarchyCode); //Convert.ToString  (Param.getUserParam().HierarchyCode);
            User_Id = Convert.ToInt32(UserManager.getUserParam().UserId);//Convert.ToInt32(Param.getUserParam().UserID);
            Year_Code = Convert.ToInt32(UserManager.getUserParam().YearCode); //Convert.ToInt32(Param.getUserParam().YearCode);
            Main_Id = Convert.ToInt32(UserManager.getUserParam().MainId); //Convert.ToInt32( Param.getUserParam().MainId);

            //Hierarchy_Code = "HO";
            //User_Id = 53;
            //Year_Code = 08;
            //Main_Id = 1;

            Voucher_Type_Id = 6;

            if (keyID <= 0)
            {
                Vendor_Id = 0;
                _TDSDeductionPresenter.Generate_Voucher_No();
                Picker.Focus();
            }
        }

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                if (Convert.ToInt32(ddl_Ledger_Group.SelectedValue) > 0)
                {
                    ddl_Ledger_Group_SelectedIndexChanged(sender, e);
                }
            }
            if (keyID <= 0)
            {
                Picker.SelectedDate = DateTime.Now;
            }
            else
            {

            }
            if (keyID <= 0)
            {
                txt_Reference_No.Focus();
                btn_save_attributes();
            }

            if (keyID > 0)
            {
                ddl_TDS_Ledger.Enabled = false;
                ddl_Ledger_Account.Enabled = false;
                ddl_Ledger_Group.Enabled = false;
                ddl_Name.Enabled = false;

                txt_Narration.Enabled = false;
                txt_Reference_No.Enabled = false;

                Picker.Enabled = false;
                btn_Save.Visible = false;

            }
            Session["Tds_Deduction_Date"] = Picker.SelectedDate;
        }
        //  btn_Save.Visible = false;

        if (keyID <= 0)
        {
            lbl_TDSDeduction_Heading.Text = "TDS DEDUCTION";
        }
        else
        {
            lbl_TDSDeduction_Heading.Text = "TDS DEDUCTION";
        }
    }
    #endregion

    #region Event Click
    private void btn_save_attributes()
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function'){");
        sbValid.Append("if (Allow_to_Save() == false) { return false; }}");
        sbValid.Append("this.value = 'Wait...';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Save, ""));
        sbValid.Append(";");
        btn_Save.Attributes.Add("onclick", sbValid.ToString());
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        // btn_Save.Enabled = false ;

        //  ddl_TDS_Ledger_SelectedIndexChanged  (sender, e);


        if (Convert.ToDateTime(Session["Tds_Deduction_Date"]) == Picker.SelectedDate)
        {
            if (Allow_To_Save())
            {
                _TDSDeductionPresenter.Save();
                          
                
                //if (Convert.ToBoolean(Param.getUserParam().Is_VT))
                //{
                //    Response.Redirect("~/Display/frm_DataGrid_Finance.aspx?id=527");
                //}
                //else
                //{
                //    Response.Redirect("~/Display/frm_DataGrid_Finance.aspx?id=528");
                //}

                //if (UserManager.getUserParam().DivisionId == 1)
                //{
                //    Response.Redirect("~/Display/frm_DataGrid_Finance.aspx?id=527");
                //}
                //else
                //{
                //    Response.Redirect("~/Display/frm_DataGrid_Finance.aspx?id=528");
                //}
            }
            else
            {
                btn_Save.Enabled = true;
            }
        }
        else
        {
            errorMessage = " Please Refresh The TDS Deduction's Values";
            btn_Save.Visible = true;
            btn_Save.Enabled = true;
        }
    }

    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
        // btn_Save.Enabled = false ;
        Session["Tds_Deduction_Date"] = Picker.SelectedDate;
        ddl_TDS_Ledger_SelectedIndexChanged(sender, e);
    }

    protected void ddl_Ledger_Group_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Tds_Deduction_Date"] = Picker.SelectedDate;
        _TDSDeductionPresenter.Fill_Ledger_Account();

        ddl_Ledger_Account_SelectedIndexChanged(sender, e);
        SM_TDSDeduction.SetFocus(ddl_Ledger_Group);
    }

    protected void ddl_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Tds_Deduction_Date"] = Picker.SelectedDate;

        if (Convert.ToString(ddl_Name.SelectedValue) != "0")
        {
            String[] Pending_Bill_Details = new String[2];

            Pending_Bill_Details = ddl_Name.SelectedValue.Split('Ö');

            Credit_Days = Pending_Bill_Details[1];
            Gross_Amount = Pending_Bill_Details[2];

            Amount = Net_TDS_To_Deduct_Amount;

        }
        else
        {
            Credit_Days = "0";
            Gross_Amount = "0";
            Amount = Net_TDS_To_Deduct_Amount;
        }
        if (Convert.ToDecimal(Net_TDS_To_Deduct_Amount) > 0)
        {
            btn_Save.Enabled = true;
            btn_Save.Visible = true;
        }
        else
        {
            btn_Save.Enabled = false;
            btn_Save.Visible = false;
        }

        SM_TDSDeduction.SetFocus(ddl_Name);
    }

    protected void ddl_Ledger_Account_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Tds_Deduction_Date"] = Picker.SelectedDate;

        _TDSDeductionPresenter.Fill_TDS_Ledger();

        if (ddl_Ledger_Account.Items.Count > 0)
        {
            pnl_TDS_Ledger_For.GroupingText = "TDS Ledger For " + ddl_Ledger_Account.SelectedItem.Text;
            pnl_Pending_Bill_For.GroupingText = "Pending Bill For " + ddl_Ledger_Account.SelectedItem.Text;
        }
        else
        {
            pnl_TDS_Ledger_For.GroupingText = "TDS Ledger For ";
            pnl_Pending_Bill_For.GroupingText = "Pending Bill For ";
        }

        ddl_TDS_Ledger_SelectedIndexChanged(sender, e);
        ddl_Name_SelectedIndexChanged(sender, e);

        if (Convert.ToDecimal(Net_TDS_To_Deduct_Amount) > 0)
        {
            btn_Save.Enabled = true;
            btn_Save.Visible = true;
        }
        else
        {
            btn_Save.Enabled = false;
            btn_Save.Visible = false;
        }

        SM_TDSDeduction.SetFocus(ddl_Ledger_Account);
    }

    protected void ddl_TDS_Ledger_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Tds_Deduction_Date"] = Picker.SelectedDate;
        if (ddl_TDS_Ledger.Items.Count > 0)
        {
            //pnl_TDS_Ledger_For.GroupingText = "TDS Ledger For " + ddl_TDS_Ledger.SelectedItem.Text;
            _TDSDeductionPresenter.Get_TDS_Ledger_Details();
            _TDSDeductionPresenter.Fill_Pending_Bills();
            ddl_Name_SelectedIndexChanged(sender, e);

            if (Convert.ToDecimal(Net_TDS_To_Deduct_Amount) > 0)
            {
                btn_Save.Visible = true;
                btn_Save.Enabled = true;
            }
            else
            {
                btn_Save.Visible = false;

                btn_Save.Enabled = false;
            }
        }
        else
        {
            Total_Amount_Paid_Amount = "0";

            Tax_Percent = "0";
            Tax_Amount = "0";

            Surcharge_Percent = "0";
            Surcharge_Amount = "0";

            Addl_Ed_Cess_Percent = "0";
            Addl_Ed_Cess_Amount = "0";

            Addtional_Surcharge_Percent = "0";
            Addtional_Surcharge_Amount = "0";

            Total_TDS_Amount = "0";
            Less_TDS_Deducted_Till_Date_Amount = "0";
            Net_TDS_To_Deduct_Amount = "0";

            Amount = Net_TDS_To_Deduct_Amount;
            Gross_Amount = "0";
            Credit_Days = "0";

            ddl_Name.Items.Clear();
            ddl_Name.Items.Insert(0, new ListItem("Select One", "0"));

            btn_Save.Visible = false;

        }
        if (Convert.ToDecimal(Net_TDS_To_Deduct_Amount) > 0)
        {
            btn_Save.Enabled = true;
            btn_Save.Visible = true;
        }
        else
        {
            btn_Save.Enabled = false;
            btn_Save.Visible = false;
        }
        SM_TDSDeduction.SetFocus(ddl_TDS_Ledger);

    }

    #endregion
}