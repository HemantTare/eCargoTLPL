using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmDailyBookingStatement : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds, ds1, ds2;

    decimal Income, ServiceTax, RoundOff, TotalLRAmount;
    decimal Total_Income, Total_ServiceTax, Total_RoundOff, Total_LR_Amount;
    int NoOf_GC;

    string Particulars,DailyToPayAccountingUrl;

    DateTime StartDate;
    DateTime EndDate;
    LinkButton lbtn_Particulars;
    Label lbl_Particulars;
    HiddenField hdf_Client_ID, hdf_RowType;

    public int RegionID
    {
        set
        {
            ViewState["_RegionID"] = value;
            hdn_RegionID.Value = value.ToString();
        }
        get { return Convert.ToInt32(ViewState["_RegionID"]); }
    }
    public int AreaID
    {
        set
        {
            ViewState["_AreaID"] = value;
            hdn_AreaID.Value = value.ToString();
        }
        get { return Convert.ToInt32(ViewState["_AreaID"]); }
    }
    public int BranchID
    {
        set
        {
            ViewState["_BranchID"] = value;
            hdn_BranchID.Value = value.ToString();
        }
        get { return Convert.ToInt32(ViewState["_BranchID"]); }
    }
    public string RegionText 
    {
        set 
        { 
            ViewState["_RegionText"] = value;
            hdn_RegionText.Value = value;
        }
        get { return Convert.ToString(ViewState["_RegionText"]); }
    }
    public string AreaText
    {
        set 
        { 
            ViewState["_AreaText"] = value;
            hdn_AreaText.Value = value;
        }
        get { return Convert.ToString(ViewState["_AreaText"]); }
    }
    public string BranchText
    {
        set 
        { 
            ViewState["_BranchText"] = value;
            hdn_BranchText.Value = value; 
        }
        get { return Convert.ToString(ViewState["_BranchText"]); }
    }
   
    public DateTime BookingDate
    {
        set
        {
            hdn_BookingDate.Value = Dtp_BookingDate.SelectedDate.ToString();
        }
        get { return Dtp_BookingDate.SelectedDate; }
    }

    public DateTime BookingDateToDate
    {
        set
        {
            hdn_BookingDateToDate.Value = Dtp_BookingDateToDate.SelectedDate.ToString();
        }
        get { return Dtp_BookingDateToDate.SelectedDate; }
    }

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        StartDate = UserManager.getUserParam().StartDate;
        EndDate = UserManager.getUserParam().EndDate;
        
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid); 
        Wuc_Export_To_Excel1.FileName = "DailyBookingRegister";

        Wuc_Region_Area_Branch1.RegionIndexChange += new EventHandler(AssignHiddenRegion);
        Wuc_Region_Area_Branch1.AreaIndexChange += new EventHandler(AssignHiddenArea);
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(AssignHiddenBranch);
        Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
        Dtp_BookingDate.DateSelectionChanged += new EventHandler(AssignHiddenBookingDate);
        Dtp_BookingDateToDate.DateSelectionChanged += new EventHandler(AssignHiddenBookingDateToDate);

        if (IsPostBack == false)
        { 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_GridPaidToPay);
            objcommon.SetStandardCaptionForGrid(dg_GridTBB);

            //Dtp_BookingDate.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);
            BookingDate = Convert.ToDateTime(System.DateTime.Now.Date);
            BookingDateToDate = Convert.ToDateTime(System.DateTime.Now.Date);

            BindGrid("form_and_pageload", e);
            btn_ToPay.Visible = lbl_Paid.Visible; 
         
        }   
        
        StringBuilder Path = new StringBuilder(Util.GetBaseURL()); 
        Path.Append("/Finance/Reports/FrmDailyToPayAccounting.aspx?CallFrom=DailyBookingStatement");

        btn_ToPay.Attributes.Add("onclick", "return GridTopay('" + Path + "'," + hdn_RegionID.ClientID + "," + hdn_AreaID.ClientID + "," + hdn_BranchID.ClientID + "," + hdn_RegionText.ClientID + "," + hdn_AreaText.ClientID + "," + hdn_BranchText.ClientID + "," + hdn_BookingDate.ClientID + ");");

    }
    private void AssignHiddenBookingDate(object sender, EventArgs e)
    {
        BookingDate = Dtp_BookingDate.SelectedDate;
        
    }
    private void AssignHiddenBookingDateToDate(object sender, EventArgs e)
    {
        BookingDateToDate = Dtp_BookingDateToDate.SelectedDate;

    }
    private void AssignHiddenRegion(object sender, EventArgs e)
    {
        RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
        RegionID = Wuc_Region_Area_Branch1.RegionID;
    }
    private void AssignHiddenArea(object sender, EventArgs e)
    {
        AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
        AreaID = Wuc_Region_Area_Branch1.AreaID;
    }
    private void AssignHiddenBranch(object sender, EventArgs e)
    {
        BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;
        BranchID = Wuc_Region_Area_Branch1.BranchID;
    }
    
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
            lbl_Error.Text = "";
            dg_GridPaidToPay.Visible = true;
            dg_GridPaidToPay.CurrentPageIndex = 0;
            dg_GridTBB.Visible = true;
            dg_GridTBB.CurrentPageIndex = 0;
            BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_GridPaidToPay.Visible = false;
        //}
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_GridPaidToPay.CurrentPageIndex;
        int grid_PageSize = dg_GridPaidToPay.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime Booking_Date = Dtp_BookingDate.SelectedDate;
        DateTime Booking_DateToDate = Dtp_BookingDateToDate.SelectedDate; 
        
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@Booking_Date", SqlDbType.DateTime,0,Booking_Date),
            objDAL.MakeInParams("@Booking_DateToDate", SqlDbType.DateTime,0,Booking_DateToDate),
            objDAL.MakeInParams("@Payment_Type_ID", SqlDbType.Int,0,Convert.ToInt32(ddl_PaymentType.SelectedValue)) 
        };

        objDAL.RunProc("FA_RPT_Daily_Booking_Statement", objSqlParam, ref ds);
         

        if (CallFrom == "form_and_pageload") return;

        //dg_GridPaidToPay.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        //string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        
        calculate_totals();
        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_GridPaidToPay.DataSource = ds.Tables[0];
            dg_GridPaidToPay.DataBind();
            lbl_Paid.Visible = true;
            btn_ToPay.Visible = true;
        }
        else
        {
            dg_GridPaidToPay.DataSource = null;
            dg_GridPaidToPay.DataBind();
            lbl_Paid.Visible = false;
            btn_ToPay.Visible = false;
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            dg_GridTBB.DataSource = ds.Tables[1];
            dg_GridTBB.DataBind();
            lblToPay.Visible = true;
        }
        else
        {
            dg_GridTBB.DataSource = null;
            dg_GridTBB.DataBind();
            lblToPay.Visible = false; 
        }

         
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

        if (Convert.ToInt32(ddl_PaymentType.SelectedValue) == 3)
        {
            lblToPay.Text = "To Be Billed";
            lbl_Paid.Visible = false;
            btn_ToPay.Visible = false;
        }
        else
        {
            lblToPay.Text = "To Pay"; 
            lbl_Paid.Visible = true;
            dg_GridTBB.Visible = false;
            btn_ToPay.Visible = true;
        }

    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow(); 
        dr["Particulars"] = " ";
        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Merge(ds.Tables[1]);

        if (Convert.ToInt32(ddl_PaymentType.SelectedValue) != 3)
        {
            ds.Tables[0].Merge(ds.Tables[2]);
        }

        ds.Tables[0].Columns.Remove("SrNo");
        ds.Tables[0].Columns.Remove("RowType");
        ds.Tables[0].Columns.Remove("Client_ID");

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        
    }

    protected void dg_GridPaidToPay_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridPaidToPay.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[2].Rows[0];
        Particulars = dr["Particulars"].ToString();
        Income = Util.String2Decimal(dr["Income"].ToString());
        ServiceTax = Util.String2Decimal(dr["Service Tax"].ToString());
        RoundOff = Util.String2Decimal(dr["Round Off"].ToString());
        TotalLRAmount = Util.String2Decimal(dr["Total LR Amount"].ToString()); 

    }
    #endregion


    protected void dg_GridPaidToPay_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        string BkgRegPaidTBBViewerUrl;
        if (e.Item.ItemIndex != -1)
        { 
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_Id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_Id = Wuc_Region_Area_Branch1.BranchID;
            DateTime Booking_Date = Dtp_BookingDate.SelectedDate;
            DateTime Booking_DateToDate = Dtp_BookingDateToDate.SelectedDate;

            int Menu_Item_Id = Raj.EC.Common.GetMenuItemId();

            //BkgRegPaidTBBViewerUrl = ClassLibraryMVP.Util.GetBaseURL() +
            //             "/Reports/Direct_Printing/FrmBrchWiseBkgRegPaidTBBViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
            //             "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + "&Branch_id=" + Util.EncryptInteger(Branch_Id) + "&From_Date=" + Booking_Date + "&To_Date=" + Booking_DateToDate;

            BkgRegPaidTBBViewerUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Finance/Reports/FrmDailyBookingStatementPaidPrinOrExport.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
                         "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + "&Branch_id=" + Util.EncryptInteger(Branch_Id) + "&From_Date=" + Booking_Date + "&To_Date=" + Booking_DateToDate;
            

            lbtn_Particulars = (LinkButton)e.Item.FindControl("lbtn_Particulars");
            lbtn_Particulars.Attributes.Add("onclick", "return GridPaidToPay('" + BkgRegPaidTBBViewerUrl + "');");

        }
    }

    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    
    protected void dg_GridTBB_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        string BkgRegPaidTBBViewerUrl;

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Particulars, lbl_Income, lbl_ServiceTax, lbl_RoundOff, lbl_TotalLRAmount;
 
            lbl_Particulars = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Particulars");
            lbl_Income = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Income");
            lbl_ServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ServiceTax");
            lbl_RoundOff = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_RoundOff");
            lbl_TotalLRAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalLRAmount");

            //lbl_Particulars.Text = Particulars.ToString();
            //lbl_Income.Text = Income.ToString();
            //lbl_ServiceTax.Text = ServiceTax.ToString();
            //lbl_RoundOff.Text = RoundOff.ToString();
            //lbl_TotalLRAmount.Text = TotalLRAmount.ToString();

        }

        if (e.Item.ItemIndex != -1)
        {
            lbl_Particulars = (Label)e.Item.FindControl("lbl_Particulars");
            lbtn_Particulars = (LinkButton)e.Item.FindControl("lbtn_Particulars");
            hdf_Client_ID = (HiddenField)e.Item.FindControl("hdf_Client_ID");
            hdf_RowType = (HiddenField)e.Item.FindControl("hdf_RowType");

            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_Id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_Id = Wuc_Region_Area_Branch1.BranchID;
            int Client_ID = Convert.ToInt32(hdf_Client_ID.Value);
            DateTime Booking_Date = Dtp_BookingDate.SelectedDate;
            DateTime Booking_DateToDate = Dtp_BookingDateToDate.SelectedDate;

            if (Convert.ToInt32(ddl_PaymentType.SelectedValue) == 3)
            {

                lbtn_Particulars.Visible = true;
                lbl_Particulars.Visible = false;
                
                BkgRegPaidTBBViewerUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Finance/Reports/FrmDailyBkgStateMClientDtls.aspx?Menu_Item_Id=5223" +
                         "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&Area_id=" + Util.EncryptInteger(Area_Id) + 
                         "&Branch_id=" + Util.EncryptInteger(Branch_Id) + "&From_Date=" + Booking_Date +
                         "&To_Date=" + Booking_DateToDate + "&Client_ID=" + Util.EncryptInteger(Client_ID);

                lbtn_Particulars.Attributes.Add("onclick", "return GridTBB('" + BkgRegPaidTBBViewerUrl + "');");

                if (hdf_RowType.Value  == "GRAND TOTAL")
                {
                    e.Item.Font.Size = 14;
                    e.Item.BackColor = System.Drawing.Color.Maroon;
                    e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
                    e.Item.Font.Bold = true; 
                }

                else if (hdf_RowType.Value == "Client")
                {
                    e.Item.Font.Size = 10;
                    e.Item.BackColor = System.Drawing.Color.Yellow;
                    e.Item.ForeColor = System.Drawing.Color.Navy;
                    e.Item.Font.Bold = true;
                }
                else 
                {
                    e.Item.BackColor = System.Drawing.Color.LightCyan;
                    e.Item.ForeColor = System.Drawing.Color.Black;
                    e.Item.Font.Bold = true;
                }
            }
            else
            {

                lbtn_Particulars.Visible = false;
                lbl_Particulars.Visible = true;
            }
        }
    }
}
