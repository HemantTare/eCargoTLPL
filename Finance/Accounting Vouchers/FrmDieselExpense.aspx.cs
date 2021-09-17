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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Finance_Accounting_Vouchers_FrmDieselExpense : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    string Mode = "0";
    private DataSet objDS;
    private DAL objDAL = new DAL();
    string _flag;

    public int KeyId
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }


    public String VoucherNo
    {
        set { lbl_VoucherNoValue.Text = value; }
        get { return lbl_VoucherNoValue.Text; }
    }

    private DateTime VoucherDate
    {
        set { dtpVoucherDate.SelectedDate = value; }
        get { return dtpVoucherDate.SelectedDate; }
    }

    public string errorMessage
    {
        set { lblErrors.Text = value; }
    }

    public int VehicleID
    {
        get { return DDLVehicle.VehicleID; }
        set
        {
            DDLVehicle.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public string VehicleCategoryIds
    {
        get { return DDLVehicle.VehicleCategoryIds; }
        set
        {
            DDLVehicle.VehicleCategoryIds = value;
            hdn_VehicleCategoryIds.Value = value;
        }
    }

    public string Pump
    {
        get { return txt_Pump.Text; }
        set
        {
            txt_Pump.Text = value;
        }
    }

    public int CurrentKm
    {
        get { return Util.String2Int(txt_CurrentKm.Text) ; }
        set
        {
            txt_CurrentKm.Text = Util.Int2String(value);
            hdn_CurrentKm.Value = Util.Int2String(value);
        }
    }

    public int PreviousKm
    {
        get { return Util.String2Int(lbl_PreviousKm.Text); }
        set
        {
            lbl_PreviousKm.Text = Util.Int2String(value);
        }
    }

    public string  Bill_No
    {
        get { return txt_BillNo.Text; }
        set
        {
            txt_BillNo.Text = value;
        }
    }

    public int DiffKm
    {
        get { return Util.String2Int(lbl_KMDiff.Text); }
        set
        {
            lbl_KMDiff.Text = Util.Int2String(value);
            hdn_KMDiff.Value = Util.Int2String(value);
        }
    }

    public decimal  DieselQty
    {
        get { return Util.String2Decimal(txt_DieselQty.Text); }
        set
        {
            txt_DieselQty.Text = Util.Decimal2String(value);
            hdn_DieselQty.Value = Util.Decimal2String(value);
        }
    }

    public decimal Amount
    {
        get { return Util.String2Decimal(txt_Amount.Text); }
        set
        {
            txt_Amount.Text = Util.Decimal2String(value);
        }
    }


    public decimal Average
    {
        get { return Util.String2Decimal(lbl_Average.Text); }
        set
        {
            lbl_Average.Text = Util.Decimal2String(value);
            hdn_Average.Value = Util.Decimal2String(value);
        }
    }

    public string Remarks
    {
        get { return txt_Remarks.Text; }
        set
        {
            txt_Remarks.Text = value;
        }
    }

    public string PaidTo
    {
        get { return txt_PaidTo.Text; }
        set
        {
            txt_PaidTo.Text = value;
        }
    }

    public string PaidToMobile
    {
        get { return txt_PaidToMobile.Text; }
        set
        {
            txt_PaidToMobile.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        errorMessage = "";
        VehicleCategoryIds = "";

        DDLVehicle.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);

        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));
        btn_Close.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Close, btn_Save_Exit));

        if (!IsPostBack)
        {

            if (KeyId <= 0)
            {
                Next_Voucher_Number();

                lbl_PaidBy.Text = UserManager.getUserParam().FirstName + " " + UserManager.getUserParam().LastName;
            }
            else
            {
                ReadValues();
            }

        }

        ScriptManager.SetFocus(DDLVehicle);         
    }


    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCategoryIds = DDLVehicle.GetVehicleParameter("Vehicle_Category_ID");
        hdn_VehicleCategoryIds.Value = VehicleCategoryIds;

        SetVehicleInfoOnVehicleChanged();
    }
    public void SetVehicleInfoOnVehicleChanged()
    {
        if (VehicleID > 0)
        {
            hdn_VehicleID.Value = VehicleID.ToString();
            objDS = GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                lbl_PreviousKm.Text = objDR["PreviousKM"].ToString();

                if (Util.String2Int(lbl_PreviousKm.Text) > 0 && Util.String2Int(txt_CurrentKm.Text) > 0)
                {
                    lbl_KMDiff.Text = Util.Int2String(Util.String2Int(txt_CurrentKm.Text) - Util.String2Int(lbl_PreviousKm.Text));

                    if (Util.String2Decimal(txt_DieselQty.Text) > 0 && Util.String2Decimal(txt_DieselQty.Text) > 0)
                    {
                        hdn_Average.Value  = Util.Decimal2String(Math.Round(Util.String2Decimal(lbl_KMDiff.Text) / Util.String2Decimal(txt_DieselQty.Text),2));
                        lbl_Average.Text  = hdn_Average.Value ;

                    }
                    else
                    {
                        hdn_Average.Value = "0";
                        lbl_Average.Text  = hdn_Average.Value;
                    }
                
                }
                else
                {
                    lbl_KMDiff.Text = "0";
                    hdn_KMDiff.Value = "0";

                    lbl_Average.Text = "0";
                    hdn_Average.Value = "0";
                }
            }
            else
            {
                lbl_PreviousKm.Text = "0";

                lbl_KMDiff.Text = "0";
                hdn_KMDiff.Value = "0";

                lbl_Average.Text = "0";
                hdn_Average.Value = "0";
            }

            ScriptManager.SetFocus(txt_Pump);

        }
        else
        {
            lbl_PreviousKm.Text = "0";

            lbl_KMDiff.Text = "0";
            hdn_KMDiff.Value = "0";

            lbl_Average.Text = "0";
            hdn_Average.Value = "0";
        }
    }
    public DataSet GetVehicleInformationOnVehicleChanged()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
            objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, KeyId),
            objDAL.MakeInParams("@Date", SqlDbType.DateTime , 0, VoucherDate)
        };
        objDAL.RunProc("FA_Opr_Diesel_Expense_Get_Vehicle_Previous_KM", objSqlParam, ref objDS);
        return objDS;
    }


    private void Next_Voucher_Number()
    {
        VoucherNo = objComm.Get_Next_Number();
    }







    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {

        if (Allow_To_Save())
        {
            btn_Save_Exit.Enabled = false;

            _flag = "SaveAndExit";

            Save();
        }
    }

    public Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar ,5,UserManager.getUserParam().HierarchyCode),
                objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
                objDAL.MakeInParams("@Transaction_ID",SqlDbType.Int,0,KeyId),
                objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleID),
                objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime ,0,VoucherDate),
                objDAL.MakeInParams("@Pump_Name",SqlDbType.VarChar ,100,Pump),
                objDAL.MakeInParams("@Bill_No",SqlDbType.VarChar ,20,Bill_No),
                objDAL.MakeInParams("@Current_Km",SqlDbType.Int  ,0,CurrentKm),
                objDAL.MakeInParams("@Previous_Km",SqlDbType.Int  ,0,PreviousKm),
                objDAL.MakeInParams("@Km_Diff",SqlDbType.Int  ,0,Util.String2Decimal(hdn_KMDiff.Value)),
                objDAL.MakeInParams("@Diesel_Qty",SqlDbType.Decimal  ,0,DieselQty),
                objDAL.MakeInParams("@Amount",SqlDbType.Decimal  ,0,Amount),
                objDAL.MakeInParams("@Average",SqlDbType.Decimal  ,0,Util.String2Decimal(hdn_Average.Value)),
                objDAL.MakeInParams("@Paid_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                objDAL.MakeInParams("@Paid_To",SqlDbType.VarChar ,100,PaidTo),
                objDAL.MakeInParams("@Paid_To_Mobile",SqlDbType.VarChar ,20,PaidToMobile),
                objDAL.MakeInParams("@Remark",SqlDbType.VarChar ,1000,txt_Remarks.Text),
                objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("FA_Opr_Diesel_Expense_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
            string _Msg;
            _Msg = "Saved SuccessFully";
            lblErrors.Text = _Msg;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, KeyId) };

        objDAL.RunProc("FA_Opr_Diesel_Expense_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            VoucherNo = objDR["Transaction_No"].ToString();
            VoucherDate = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
            hdn_VehicleID .Value = objDR["Vehicle_ID"].ToString();
            DDLVehicle.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
            Pump = objDR["Pump_Name"].ToString();
            Bill_No = objDR["Bill_No"].ToString();

            CurrentKm = Util.String2Int (objDR["Current_Km"].ToString());
            PreviousKm = Util.String2Int(objDR["Previous_Km"].ToString());

            DiffKm = Util.String2Int(objDR["Km_Diff"].ToString());
            hdn_KMDiff.Value  = objDR["Km_Diff"].ToString();

            DieselQty = Util.String2Decimal(objDR["Diesel_Qty"].ToString());

            Amount = Util.String2Decimal(objDR["Amount"].ToString());

            Average  = Util.String2Decimal(objDR["Average"].ToString());
            hdn_Average.Value  = objDR["Average"].ToString();

            lbl_PaidBy.Text = objDR["Paid_By"].ToString();

            PaidTo = objDR["Paid_To"].ToString();
            PaidToMobile  = objDR["Paid_To_Mobile"].ToString();

            Remarks = objDR["Remark"].ToString();

        }
    }

    private bool Allow_To_Save()
    {
        bool ATS = false;

        if (dtpVoucherDate.SelectedDate > UserManager.getUserParam().TodaysDate)
        {
            lblErrors.Text = "Booking Date Should Be Less Than Or Equal To Todays Date.";
            ScriptManager.SetFocus(dtpVoucherDate);
        }
        else if (dtpVoucherDate.SelectedDate < UserManager.getUserParam().StartDate || dtpVoucherDate.SelectedDate > UserManager.getUserParam().EndDate)
        {
            lblErrors.Text = "Voucher Date should be in current Financial Date";
            ScriptManager.SetFocus(dtpVoucherDate);
        }
        else if (VehicleID <= 0)
        {
            lblErrors.Text = "Select Vehicle No.";
            ScriptManager.SetFocus(DDLVehicle);
        }
        else if (CurrentKm  <= 0)
        {
            lblErrors.Text = "Enter Current KM";
            ScriptManager.SetFocus(txt_CurrentKm);
        }
        else if (Pump.Trim() == "")
        {
            lblErrors.Text = "Enter Pump Name";
            ScriptManager.SetFocus(txt_Pump);
        }
        else if (DieselQty  <= 0)
        {
            lblErrors.Text = "Enter Diesel Qty";
            ScriptManager.SetFocus(txt_DieselQty);
        }
        else if (Amount  <= 0)
        {
            lblErrors.Text = "Enter Amount";
            ScriptManager.SetFocus(txt_Amount);
        }

        else if (PreviousKm > CurrentKm)
        {
            lblErrors.Text = "Previous KM Reading Can Not Be Greater Than Current KM Reading";
            ScriptManager.SetFocus(txt_CurrentKm);

        }
        else if (PaidTo.Trim() == "")
        {
            lblErrors.Text = "Enter Paid To Whom";
            ScriptManager.SetFocus(txt_PaidTo);
        }
        else if (PaidToMobile.Trim().Length  != 10)
        {
            lblErrors.Text = "Enter Proper Mobile No Of Paid To Whom";
            ScriptManager.SetFocus(txt_PaidToMobile);
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }




}
