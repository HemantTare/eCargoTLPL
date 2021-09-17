using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess; 
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_FrmClosingCashTrans : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    //string Mode;
    //decimal Articles, Sub_Total, Basic_Freight, STax_Amt, Total_Freight, GC_Amount, Discount;
    //int NoOf_GC;
    Message objMessage = new Message();

    public int Key
    {
        set { ViewState["_key"] = value; }
        get { return Convert.ToInt32(ViewState["_key"]); }
    }
    public string Mode
    {
        set { ViewState["_mode"] = value; }
        get { return Convert.ToString(ViewState["_mode"]); }
    }
    public decimal OpeningBalance
    {
        set { ViewState["_OpeningBalance"] = value; }
        get { return Convert.ToDecimal(ViewState["_OpeningBalance"]); }
    }
    public decimal OBPaidBooking
    {
        set { ViewState["_OBPaidBooking"] = value; }
        get { return Convert.ToDecimal(ViewState["_OBPaidBooking"]); }
    }
    public decimal OBToPayRecovery
    {
        set { ViewState["_OBToPayRecovery"] = value; }
        get { return Convert.ToDecimal(ViewState["_OBToPayRecovery"]); }
    }
    public decimal OBOtherCashReceipt
    {
        set { ViewState["_OBOtherCashReceipt"] = value; }
        get { return Convert.ToDecimal(ViewState["_OBOtherCashReceipt"]); }
    }
    public decimal OBTotalOpeningBalance
    {
        set { ViewState["_OBTotalOpeningBalance"] = value; }
        get { return Convert.ToDecimal(ViewState["_OBTotalOpeningBalance"]); }
    }
    public decimal Expenses
    {
        set { ViewState["_Expenses"] = value; }
        get { return Convert.ToDecimal(ViewState["_Expenses"]); }
    }
    public decimal ExpTempoFreight
    {
        set { ViewState["_ExpTempoFreight"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpTempoFreight"]); }
    }
    public decimal ExpOther
    {
        set { ViewState["_ExpOther"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpOther"]); }
    }
    public decimal ExpTotal
    {
        set { ViewState["_ExpTotal"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpTotal"]); }
    }
    public decimal ExpCashPaidToOthers
    {
        set { ViewState["_ExpCashPaidToOthers"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpCashPaidToOthers"]); }
    }
    public decimal ExpIntraBranch
    {
        set { ViewState["_ExpIntraBranch"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpIntraBranch"]); }
    }
    public decimal ExpTransfer
    {
        set { ViewState["_ExpTransfer"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpTransfer"]); }
    }
    public decimal ExpDepositInBank
    {
        set { ViewState["_ExpDepositInBank"] = value; }
        get { return Convert.ToDecimal(ViewState["_ExpDepositInBank"]); }
    }
    public decimal ClosingBalance
    {
        set 
        { 
            ViewState["_ClosingBalance"] = value;
            hdfn_ClosingBalance.Value = value.ToString();
        }
        get { return Convert.ToDecimal(ViewState["_ClosingBalance"]); }
    }
    public string Session_ClosingStock_Details_Xml
    {
        get { return Session_ClosingStock_Details.GetXml().ToLower(); }
    } 
    public DataSet Session_ClosingStock_Details
    {
        get { return StateManager.GetState<DataSet>("ClosingStock_Details"); }
        set { StateManager.SaveState("ClosingStock_Details", value); }
    }

    #endregion

    #region EventClick

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        if (Mode == "4")
        {
            Key = Util.DecryptToInt(Request.QueryString["Id"].ToString());

            btn_null_sessions.Visible = true;
            btn_null_sessions.Enabled = true;
            btn_Save.Visible = false;
            btn_Save.Enabled = false;
            btn_view.Visible = false; 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Dtp_AsOnDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
            Upd_ClosingCashAmountDetails.Visible = false;
            if (Mode == "4")
            {
                Key = Util.DecryptToInt(Request.QueryString["Id"].ToString());
                BindGrid("form", e);
            }
        }
    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {  
        string msg = "";
        int Branch_id = UserManager.getUserParam().MainId;
        if (Branch_id == 0)
        {

            msg = "Unauthorized Login";
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;  
        }
        else 
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
           int Branch_id;
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;

         
            Branch_id = UserManager.getUserParam().MainId;
        
        SqlParameter[] objSqlParam ={
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@CloseCashAmount_ID", SqlDbType.Int,0,Key), 
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@Date", SqlDbType.DateTime,0,AsOnDate),  
            objDAL.MakeInParams("@Mode", SqlDbType.Int,0,Convert.ToInt32(Mode))  
        };

            objDAL.RunProc("FA_Opr_LoginDailyClosingCashReport", objSqlParam, ref ds); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                btn_Save.Visible = true;

                Common objcommon = new Common();
                objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

                
                ClearVariables();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Upd_ClosingCashAmountDetails.Visible = true;
                    Upd_ClosingCashBal.Visible = true;
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    //dg_GridClosingCashAmountDetails.DataSource = ds.Tables[1];
                    //dg_GridClosingCashAmountDetails.DataBind();
                    OpeningBalance = Convert.ToDecimal(ds.Tables[1].Rows[0]["OpeningBal"].ToString());
                    OBPaidBooking = Convert.ToDecimal(ds.Tables[1].Rows[0]["PaidBooking"].ToString());
                    OBToPayRecovery = Convert.ToDecimal(ds.Tables[1].Rows[0]["ToPayRecovery"].ToString());
                    OBOtherCashReceipt = Convert.ToDecimal(ds.Tables[1].Rows[0]["OtherReceipt"].ToString());
                    OBTotalOpeningBalance = Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalOpenBal"].ToString());
                    Expenses = Convert.ToDecimal(ds.Tables[1].Rows[0]["TempoFrt"].ToString());
                    ExpTempoFreight = Convert.ToDecimal(ds.Tables[1].Rows[0]["TempoFrt"].ToString());
                    ExpOther = Convert.ToDecimal(ds.Tables[1].Rows[0]["OtherExp"].ToString());
                    ExpTotal = Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalExp"].ToString());
                    ExpCashPaidToOthers = Convert.ToDecimal(ds.Tables[1].Rows[0]["PaidToOthers"].ToString());
                    ExpIntraBranch = Convert.ToDecimal(ds.Tables[1].Rows[0]["InterBranchTransfer"].ToString());
                    //ExpTransfer = Convert.ToDecimal(ds.Tables[1].Rows[0]["ExpTransfer"].ToString());
                    ExpDepositInBank = Convert.ToDecimal(ds.Tables[1].Rows[0]["DepositedInBank"].ToString());
                    ClosingBalance = Convert.ToDecimal(ds.Tables[1].Rows[0]["ClosingBal"].ToString());
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                     

                    txtRupeeType1000.Text = ds.Tables[2].Rows[0]["Counts1000"].ToString();
                    lblTotalRupeeType1000.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts1000"]) * 1000).ToString();

                    txtRupeeType500.Text = ds.Tables[2].Rows[0]["Counts500"].ToString();
                    lblTotalRupeeType500.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts500"]) * 500).ToString();

                    txtRupeeType100.Text = ds.Tables[2].Rows[0]["Counts100"].ToString();
                    lblTotalRupeeType100.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts100"]) * 100).ToString();

                    txtRupeeType50.Text = ds.Tables[2].Rows[0]["Counts50"].ToString();
                    lblTotalRupeeType50.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts50"]) * 50).ToString();

                    txtRupeeType20.Text = ds.Tables[2].Rows[0]["Counts20"].ToString();
                    lblTotalRupeeType20.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts20"]) * 20).ToString();

                    txtRupeeType10.Text = ds.Tables[2].Rows[0]["Counts10"].ToString();
                    lblTotalRupeeType10.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts10"]) * 10).ToString();

                    txtRupeeType5.Text = ds.Tables[2].Rows[0]["Counts5"].ToString();
                    lblTotalRupeeType5.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts5"]) * 5).ToString();

                    txtRupeeType2.Text = ds.Tables[2].Rows[0]["Counts2"].ToString();
                    lblTotalRupeeType2.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts2"]) * 2).ToString();

                    txtRupeeType1.Text = ds.Tables[2].Rows[0]["Counts1"].ToString();
                    lblTotalRupeeType1.Text = (Convert.ToInt32(ds.Tables[2].Rows[0]["Counts1"]) * 1).ToString();

                    txtRupeeTypePaisa.Text = ds.Tables[2].Rows[0]["Paisa"].ToString();
                    lblTotalRupeeTypePaisa.Text = (Convert.ToDecimal(ds.Tables[2].Rows[0]["Paisa"]) * 1).ToString();

                    txtRupeeTypeTotal.Text = ds.Tables[2].Rows[0]["Total"].ToString(); 
                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    DataSet dsNew = new DataSet();
                    dsNew.Merge(ds.Tables[3]);
                    Session_ClosingStock_Details = dsNew;

                    dg_GridClosingStock.DataSource = ds.Tables[3];
                    dg_GridClosingStock.DataBind();
                }
            }
            else
            {
               lbl_Error.Text = objMessage.message;
               lbl_Error.Visible = true;
               btn_Save.Visible = false;
               Upd_ClosingCashAmountDetails.Visible = false;
               Upd_ClosingCashBal.Visible = false;
            }
    }

    protected void dg_GridClosingStock_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridClosingStock.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
 
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if ((e.Item.Cells[0].Text) == "Opening Balance : ")
        {
            e.Item.BackColor = System.Drawing.Color.Brown;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[1].Text) == "Paid Booking " || (e.Item.Cells[1].Text) == "To Pay Recovery " || (e.Item.Cells[1].Text) == "Other Cash Receipt ")
        {
            e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            e.Item.ForeColor = System.Drawing.Color.Black;
        }
        if ((e.Item.Cells[1].Text) == "          Total :  ")
        {
            e.Item.BackColor = System.Drawing.Color.Goldenrod;
            e.Item.ForeColor = System.Drawing.Color.DarkRed;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[0].Text) == "Expenses : ")
        {
            e.Item.BackColor = System.Drawing.Color.DarkGreen;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[1].Text) == "Tempo Freight ")
        {
            e.Item.BackColor = System.Drawing.Color.PaleGreen;
            e.Item.ForeColor = System.Drawing.Color.Black;
        }
        if ((e.Item.Cells[1].Text) == "Other ")
        {
            e.Item.BackColor = System.Drawing.Color.PaleGreen;
            e.Item.ForeColor = System.Drawing.Color.Black;
        }
        if ((e.Item.Cells[1].Text) == "          Total : ")
        {
            e.Item.BackColor = System.Drawing.Color.GreenYellow;
            e.Item.ForeColor = System.Drawing.Color.DarkSlateGray;
            e.Item.Font.Bold = true;
        }
        if ((e.Item.Cells[1].Text) == "Cash Paid To Others " || (e.Item.Cells[1].Text) == "Inter Branch Transfer " || (e.Item.Cells[1].Text) == "Deposit In Bank ")
        {
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            e.Item.ForeColor = System.Drawing.Color.Black;
        }
        if ((e.Item.Cells[0].Text) == "Closing Balance : ")
        {
            e.Item.BackColor = System.Drawing.Color.DarkViolet;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }
    }

    public void ClearVariables()
    { 
        txtRupeeType1000.Text = "0";
        lblTotalRupeeType1000.Text = "0"; 
        txtRupeeType500.Text = "0";
        lblTotalRupeeType500.Text = "0"; 
        txtRupeeType100.Text= "0";
        lblTotalRupeeType100.Text = "0"; 
        txtRupeeType50.Text = "0";
        lblTotalRupeeType50.Text = "0";  
        txtRupeeType20.Text = "0";
        lblTotalRupeeType20.Text = "0";  
        txtRupeeType10.Text = "0";
        lblTotalRupeeType10.Text = "0"; 
        txtRupeeType5.Text = "0";
        lblTotalRupeeType5.Text = "0"; 
        txtRupeeType2.Text = "0";
        lblTotalRupeeType2.Text = "0"; 
        txtRupeeType1.Text = "0";
        lblTotalRupeeType1.Text = "0"; 
        txtRupeeTypePaisa.Text = "0";
        lblTotalRupeeTypePaisa.Text = "0"; 
        txtRupeeTypeTotal.Text = "0"; 

    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Branch_id = UserManager.getUserParam().MainId;
        int _userId = UserManager.getUserParam().UserId;
        DateTime AsOnDate = Dtp_AsOnDate.SelectedDate;
        DAL _objDAL = new DAL(); 

        Message objMessage = new Message();

        decimal Total = ((Convert.ToInt32(txtRupeeType1000.Text) * 1000)
                   + (Convert.ToInt32(txtRupeeType500.Text) * 500)
                   + (Convert.ToInt32(txtRupeeType100.Text) * 100)
                   + (Convert.ToInt32(txtRupeeType50.Text) * 50)
                   + (Convert.ToInt32(txtRupeeType20.Text) * 20)
                   + (Convert.ToInt32(txtRupeeType10.Text) * 10)
                   + (Convert.ToInt32(txtRupeeType5.Text) * 5)
                   + (Convert.ToInt32(txtRupeeType2.Text) * 2)
                   + (Convert.ToInt32(txtRupeeType1.Text) * 1));


        //Total = Convert.ToDecimal(txtRupeeTypeTotal.Text);
        Total = Total + (Convert.ToDecimal(txtRupeeTypePaisa.Text));


        SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            _objDAL.MakeOutParams("@Print_Id", SqlDbType.Int, 0), 
            _objDAL.MakeInParams("@CloseCashAmount_ID", SqlDbType.Int, 0,Key),
            _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, Branch_id),            
            _objDAL.MakeInParams("@Closing_Date", SqlDbType.DateTime, 0, AsOnDate),    
            _objDAL.MakeInParams("@OpeningBalance", SqlDbType.Decimal, 0, OpeningBalance), 
            _objDAL.MakeInParams("@OBPaidBooking", SqlDbType.Decimal, 0, OBPaidBooking), 
            _objDAL.MakeInParams("@OBToPayRecovery", SqlDbType.Decimal, 0, OBToPayRecovery), 
            _objDAL.MakeInParams("@OBOtherCashReceipt", SqlDbType.Decimal, 0, OBOtherCashReceipt), 
            _objDAL.MakeInParams("@OBTotalOpeningBalance", SqlDbType.Decimal, 0, OBTotalOpeningBalance), 
            _objDAL.MakeInParams("@Expenses", SqlDbType.Decimal, 0, Expenses), 
            _objDAL.MakeInParams("@ExpTempoFreight", SqlDbType.Decimal, 0, ExpTempoFreight), 
            _objDAL.MakeInParams("@ExpOther", SqlDbType.Decimal, 0, ExpOther), 
            _objDAL.MakeInParams("@ExpTotal", SqlDbType.Decimal, 0, ExpTotal), 
            _objDAL.MakeInParams("@ExpCashPaidToOthers", SqlDbType.Decimal, 0, ExpCashPaidToOthers), 
            _objDAL.MakeInParams("@ExpIntraBranch", SqlDbType.Decimal, 0, ExpIntraBranch),  
            _objDAL.MakeInParams("@ExpDepositInBank", SqlDbType.Decimal, 0, ExpDepositInBank), 
            _objDAL.MakeInParams("@ClosingBalance", SqlDbType.Decimal, 0, ClosingBalance), 
            _objDAL.MakeInParams("@Counts1000", SqlDbType.Int, 0, Util.String2Int(txtRupeeType1000.Text)),      
            _objDAL.MakeInParams("@Counts500", SqlDbType.Int, 0, Util.String2Int(txtRupeeType500.Text)),      
            _objDAL.MakeInParams("@Counts100", SqlDbType.Int, 0, Util.String2Int(txtRupeeType100.Text)),      
            _objDAL.MakeInParams("@Counts50", SqlDbType.Int, 0, Util.String2Int(txtRupeeType50.Text)),      
            _objDAL.MakeInParams("@Counts20", SqlDbType.Int, 0, Util.String2Int(txtRupeeType20.Text)),      
            _objDAL.MakeInParams("@Counts10", SqlDbType.Int, 0, Util.String2Int(txtRupeeType10.Text)),      
            _objDAL.MakeInParams("@Counts5", SqlDbType.Int, 0, Util.String2Int(txtRupeeType5.Text)),      
            _objDAL.MakeInParams("@Counts2", SqlDbType.Int, 0, Util.String2Int(txtRupeeType2.Text)),      
            _objDAL.MakeInParams("@Counts1", SqlDbType.Int, 0, Util.String2Int(txtRupeeType1.Text)),      
            _objDAL.MakeInParams("@Paisa", SqlDbType.Decimal, 0, Util.String2Decimal(txtRupeeTypePaisa.Text)),      
            _objDAL.MakeInParams("@Total", SqlDbType.Decimal, 0, Total),      
            _objDAL.MakeInParams("@ClosingStock_Details_Xml", SqlDbType.Xml, 0, Session_ClosingStock_Details_Xml),      
            _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId)
        };


        _objDAL.RunProc("EC_FA_OPR_ClosingCashAmountDetails_Save", objSqlParam);


        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);
 
        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

        } 
    } 
}
