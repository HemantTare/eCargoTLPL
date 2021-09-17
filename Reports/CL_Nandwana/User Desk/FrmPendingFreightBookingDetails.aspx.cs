using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_UserDesk_FrmPendingFreightBookingDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    Raj.EC.Common objComm = new Raj.EC.Common();
    private DAL objDAL = new DAL();
    DataTable objDT = new DataTable();
    decimal Total_Gc;
    decimal Total_Articles;
    decimal Total_Freight;
    Boolean IsDetailed;
    HiddenField hdn_GCId;
    CheckBox chk;

    public int Booking_branch_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_Booking_branch_Id"]);
        }
        set
        {
            ViewState["_Booking_branch_Id"] = value;
        }
    }
    public string BkgBranch
    {
        get
        {
            return Convert.ToString(ViewState["_BkgBranch"]);
        }
        set
        {
            ViewState["_BkgBranch"] = value;
            txtlblBkgBranch.Text = value;
        }
    }

    public int Consignor_Client_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_Consignor_Client_Id"]);
        }
        set
        {
            ViewState["_Consignor_Client_Id"] = value;
        }
    }
    public string Consignor_Name
    {
        get
        {
            return Convert.ToString(ViewState["_Consignor_Name"]);
        }
        set
        {
            ViewState["_Consignor_Name"] = value;
            txtlblConsignor.Text = value;
        }
    }

    public Boolean Is_Consignor_Regular
    {
        get
        {
            return Convert.ToBoolean(ViewState["_Is_Consignor_Regular"]);
        }
        set
        {
            ViewState["_Is_Consignor_Regular"] = value;
        }
    }

    public DataTable SessionBindGrid
    {
        get { return StateManager.GetState<DataTable>("BindGrid"); }
        set { StateManager.SaveState("BindGrid", value); }
    }

    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "Grid_Details";
            return _objDs.GetXml().ToLower();
        }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "PendingFreightBooking";

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Booking_branch_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Booking_branch_Id"]);
            BkgBranch = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["BkgBranch"]);
            IsDetailed = Convert.ToBoolean(Request.QueryString["IsDetailed"]);
            Consignor_Client_Id = Convert.ToInt32(Request.QueryString["Consignor_Client_Id"]);
            Consignor_Name = Request.QueryString["Consignor_Name"];
            Is_Consignor_Regular = Convert.ToBoolean(Request.QueryString["Is_Consignor_Regular"]);

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
        }
        lbl_TotalReceivable.Text = hdn_TotalReceivable.Value;
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotalFreight, lbl_SumTotalArticles;

            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");
            lbl_SumTotalArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalArticles");

            lbl_SumTotalFreight.Text = Total_Freight.ToString();
            lbl_SumTotalArticles.Text = Total_Articles.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID, Consignor_Id;
            LinkButton lnk_GC_No, lnk_Consignor;
            bool Is_Consignor_Regular;
            TextBox txt_TotalFreight;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            Consignor_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignor_Client_Id").ToString());
            Is_Consignor_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignor_Regular_Client").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");
            txt_TotalFreight = (TextBox)e.Item.FindControl("txt_TotalFreight");

            StringBuilder PathGCID = new StringBuilder(Util.GetBaseURL());
            PathGCID.Append("/");
            PathGCID.Append("Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=" + ClassLibraryMVP.Util.EncryptInteger(GC_ID));
            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + PathGCID + "')");
        }
    }



   
    #endregion

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[3].Rows[0];
        Total_Gc = Util.String2Decimal(dr["TotalLR"].ToString());
        Total_Articles = Util.String2Decimal(dr["TotalArticles"].ToString());
        Total_Freight = Util.String2Decimal(dr["TotalFreight"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
            IsDetailed = Convert.ToBoolean(Request.QueryString["IsDetailed"]);
        }

        int Branch_id = UserManager.getUserParam().MainId; ;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Booking_branch_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@IsDetailed",SqlDbType.Bit,0,IsDetailed),
            objDAL.MakeInParams("@Consignor_Client_Id",SqlDbType.Int,0,Consignor_Client_Id),
            objDAL.MakeInParams("@Is_Consignor_Regular_Client",SqlDbType.Bit,0,Is_Consignor_Regular)
        };

        objDAL.RunProc("EC_Opr_Bkg_BranchWise_Pending_Freight", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[3].Rows[0][0].ToString());

        calculate_totals();

        SessionBindGrid = ds.Tables[2];
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[2], CallFrom, lbl_Error);

        if (UserManager.getUserParam().HierarchyCode == "BO")
        {
            dg_Grid.Columns[0].Visible = true;
            tr_Paymentdetails.Visible = true;
            tr_Paymentdetails2.Visible = true;
            tr_Paymentdetails3.Visible = true;
            tr_Paymentdetails4.Visible = true;
            tr_Paymentdetails5.Visible = true;
            tr_Save.Visible = true;
            btn_Print.Visible = true;
        }
        else
        {
            dg_Grid.Columns[0].Visible = false;
            tr_Paymentdetails.Visible = false;
            tr_Paymentdetails2.Visible = false;
            tr_Paymentdetails3.Visible = false;
            tr_Paymentdetails4.Visible = false;
            tr_Paymentdetails5.Visible = false;
            tr_Save.Visible = false;
            btn_Print.Visible = false;
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[2].NewRow();
        dr["Consignor_Name"] = "Total";
        dr["LRNo"] = Total_Gc;
        dr["TotalFreight"] = Total_Freight;
        dr["TotalArticles"] = Total_Articles;

        ds.Tables[2].Rows.Add(dr);


        ds.Tables[2].Columns.Remove("Att"); 
        ds.Tables[2].Columns.Remove("Consignor_Client_Id");
        ds.Tables[2].Columns.Remove("Is_Consignor_Regular_client");
        ds.Tables[2].Columns.Remove("GC_Id");

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[2];
    }

    public void ClearVariables()
    {
        ds = null;
    }
    private void calculate_griddetails()
    {
        int i;
        if (dg_Grid.Items.Count > 0)
        {
            objDT = SessionBindGrid;

            for (i = 0; i <= dg_Grid.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Grid.Items[i].FindControl("chk_Att");
                objDT.Rows[i]["Att"] = chk.Checked;
            }

            SessionBindGrid = objDT;
        }
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    public bool validateUI()
    {
        bool _isValid = false;

        if (dg_Grid.Items.Count <= 0)
            lbl_Errors.Text = "No LR found.";
        else if (Util.String2Decimal(hdn_TotalReceivable.Value) <= 0)
            lbl_Errors.Text = "Total Receivable value should be greater than zero.";
        else if ((Util.String2Decimal(txt_CashAmount.Text == string.Empty ? "0" : txt_CashAmount.Text) + Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text)) == 0)
        {
            lbl_Errors.Text = "Cash Amount And Cheque Amount Both Should Not Be Zero.";
            txt_CashAmount.Focus();
        }
        else if ((Util.String2Decimal(txt_CashAmount.Text == string.Empty ? "0" : txt_CashAmount.Text) + Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text) + Util.String2Decimal(txt_TDS.Text == string.Empty ? "0" : txt_TDS.Text)) != Util.String2Decimal(hdn_TotalReceivable.Value))
        {
            lbl_Errors.Text = "Cash Amount + Cheque Amount + TDS Amount Should be equal to Total Receivable Amount.";
            txt_CashAmount.Focus();
        }
        else if (Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text) > 0 && Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text) < 50)
        {
            lbl_Errors.Text = "Cheque Value Cannot Be Less Than Rs. 50";
            txt_ChequeAmount.Focus();
        }
        else if (Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text) > 0 && txt_ChequeNo.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Cheque No.";
            txt_ChequeNo.Focus();
        }
        else if (Util.String2Decimal(txt_ChequeAmount.Text == string.Empty ? "0" : txt_ChequeAmount.Text) > 0 && txt_ChequeBank.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Bank Name.";
            txt_ChequeBank.Focus();
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        
        calculate_griddetails();
        if (validateUI())
        {
            
            btn_Save.Enabled = false;

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@ReceivedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@CashAmount", SqlDbType.Decimal, 0,txt_CashAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txt_CashAmount.Text)),
            objDAL.MakeInParams("@ChequeAmount", SqlDbType.Decimal, 0,txt_ChequeAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txt_ChequeAmount.Text)),                
            objDAL.MakeInParams("@ChequeNo", SqlDbType.VarChar,10,txt_ChequeNo.Text),                
            objDAL.MakeInParams("@ChequeDate", SqlDbType.DateTime,0,Dtp_AsOnDate.SelectedDate),
            objDAL.MakeInParams("@ChequeBank", SqlDbType.VarChar, 50,txt_ChequeBank.Text),
            objDAL.MakeInParams("@TDSAmount", SqlDbType.Decimal, 0,txt_TDS.Text == string.Empty ? 0 : Convert.ToDecimal(txt_TDS.Text)),                
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML)
            };

            objDAL.RunProc("dbo.EC_Opr_Pending_Freight_Booking_Save", objSqlParam);

            if (Convert.ToInt32(objSqlParam[0].Value) == 0)
            {
                Response.Write("<script language='javascript'>{self.close()}</script>");
            }
        }
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        calculate_griddetails();

        if (Util.String2Decimal(hdn_TotalReceivable.Value) <= 0)
        {
            lbl_Errors.Text = "No Record Selected For Printing";
        }
        else
        {
            SqlParameter[] objSqlParam = {
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML),
                objDAL.MakeInParams("@GC_IDs", SqlDbType.VarChar, 1000,"")};

            objDAL.RunProc("dbo.EC_Opr_Pending_Freight_Booking_Print", objSqlParam,ref ds);

            String GC_Ids;

            GC_Ids = ds.Tables[1].Rows[0][0].ToString();

            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");

            Path.Append("Reports/CL_Nandwana/User Desk/FrmPendingFreightBookingDetailsViewer.aspx?GC_IDs=" + Util.EncryptString(GC_Ids));

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("Open_Details_Window('");
            sb.Append(Path);
            sb.Append("');");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());


        }
    }
}