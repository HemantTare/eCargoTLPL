using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_Sales_Billing_Frm_Booking_Summary : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

   
    string Column_Name, Proc_Name;

    decimal Sundry_No_OF_GC, Sundry_Charged_Weight, Sundry_ToPay_Freight, Sundry_Paid_Freight,
            Sundry_TBB_Freight, Sundry_STB_Freight, Sundry_FOC_Freight, Sundry_Total_Freight,

            FTL_No_OF_GC, FTL_Charged_Weight, FTL_ToPay_Freight, FTL_Paid_Freight,
            FTL_TBB_Freight, FTL_STB_Freight, FTL_FOC_Freight, FTL_Total_Freight,

            ODC_No_OF_GC, ODC_Charged_Weight, ODC_ToPay_Freight, ODC_Paid_Freight,
            ODC_TBB_Freight, ODC_STB_Freight, ODC_FOC_Freight, ODC_Total_Freight,

            Super_ODC_No_OF_GC, Super_ODC_Charged_Weight, Super_ODC_ToPay_Freight, Super_ODC_Paid_Freight,
            Super_ODC_TBB_Freight, Super_ODC_STB_Freight, Super_ODC_FOC_Freight, Super_ODC_Total_Freight,

            Grand_No_OF_GC, Grand_Charged_Weight, Grand_ToPay_Freight, Grand_Paid_Freight,
            Grand_TBB_Freight, Grand_STB_Freight, Grand_FOC_Freight, Grand_Total_Freight;  
    

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "BookingSummary";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header) 
        {
            Label lbl_Column_Name;
            lbl_Column_Name = (Label)e.Item.FindControl("lbl_Column_Name");
            lbl_Column_Name.Text = Func_Column_Name();
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total,

            lbl_Sundry_NO_OF_GC, lbl_Sundry_Charged_Weight, lbl_Sundry_ToPay_Freight,lbl_Sundry_Paid_Freight,
            lbl_Sundry_TBB_Freight, lbl_Sundry_STB_Freight, lbl_Sundry_FOC_Freight, lbl_Sundry_Total_Freight,

            lbl_FTL_NO_OF_GC, lbl_FTL_Charged_Weight, lbl_FTL_ToPay_Freight,lbl_FTL_Paid_Freight,
            lbl_FTL_TBB_Freight, lbl_FTL_STB_Freight, lbL_FTL_FOC_Freight, lbl_FTL_Total_Freight,

            lbl_ODC_NO_OF_GC, lbl_ODC_Charged_Weight, lbl_ODC_ToPay_Freight,lbl_ODC_Paid_Freight,
            lbl_ODC_TBB_Freight, lbl_ODC_STB_Freight, lbl_ODC_FOC_Freight, lbl_ODC_Total_Freight,

            lbl_Super_ODC_NO_OF_GC, lbl_Super_ODC_Charged_Weight, lbl_Super_ODC_ToPay_Freight, lbl_Super_ODC_Paid_Freight,
            lbl_Super_ODC_TBB_Freight, lbl_Super_ODC_STB_Freight, lbl_Super_ODC_FOC_Freight, lbl_Super_ODC_Total_Freight,

            lbl_Grand_NO_OF_GC, lbl_Grand_Charged_Weight, lbl_Grand_ToPay_Freight, lbl_Grand_Paid_Freight,
            lbl_Grand_TBB_Freight, lbl_Grand_STB_Freight, lbl_Grand_FOC_Freight, lbl_Grand_Total_Freight;


            lbl_Total = (Label)e.Item.FindControl("lbl_Total");

            lbl_Sundry_NO_OF_GC = (Label)e.Item.FindControl("lbl_Sundry_NO_OF_GC"); 
            lbl_Sundry_Charged_Weight = (Label)e.Item.FindControl("lbl_Sundry_Charged_Weight");
            lbl_Sundry_ToPay_Freight = (Label)e.Item.FindControl("lbl_Sundry_ToPay_Freight");
            lbl_Sundry_Paid_Freight = (Label)e.Item.FindControl("lbl_Sundry_Paid_Freight");
            lbl_Sundry_TBB_Freight = (Label)e.Item.FindControl("lbl_Sundry_TBB_Freight");
            lbl_Sundry_STB_Freight = (Label)e.Item.FindControl("lbl_Sundry_STB_Freight"); 
            lbl_Sundry_FOC_Freight = (Label)e.Item.FindControl("lbl_Sundry_FOC_Freight"); 
            lbl_Sundry_Total_Freight = (Label)e.Item.FindControl("lbl_Sundry_Total_Freight");

            lbl_FTL_NO_OF_GC = (Label)e.Item.FindControl("lbl_FTL_NO_OF_GC");
            lbl_FTL_Charged_Weight = (Label)e.Item.FindControl("lbl_FTL_Charged_Weight");
            lbl_FTL_ToPay_Freight = (Label)e.Item.FindControl("lbl_FTL_ToPay_Freight");
            lbl_FTL_Paid_Freight = (Label)e.Item.FindControl("lbl_FTL_Paid_Freight");
            lbl_FTL_TBB_Freight = (Label)e.Item.FindControl("lbl_FTL_TBB_Freight");
            lbl_FTL_STB_Freight = (Label)e.Item.FindControl("lbl_FTL_STB_Freight");
            lbL_FTL_FOC_Freight = (Label)e.Item.FindControl("lbL_FTL_FOC_Freight");
            lbl_FTL_Total_Freight = (Label)e.Item.FindControl("lbl_FTL_Total_Freight");

            lbl_ODC_NO_OF_GC = (Label)e.Item.FindControl("lbl_ODC_NO_OF_GC");
            lbl_ODC_Charged_Weight = (Label)e.Item.FindControl("lbl_ODC_Charged_Weight");
            lbl_ODC_ToPay_Freight = (Label)e.Item.FindControl("lbl_ODC_ToPay_Freight");
            lbl_ODC_Paid_Freight = (Label)e.Item.FindControl("lbl_ODC_Paid_Freight");
            lbl_ODC_TBB_Freight = (Label)e.Item.FindControl("lbl_ODC_TBB_Freight");
            lbl_ODC_STB_Freight = (Label)e.Item.FindControl("lbl_ODC_STB_Freight");
            lbl_ODC_FOC_Freight = (Label)e.Item.FindControl("lbl_ODC_FOC_Freight");
            lbl_ODC_Total_Freight = (Label)e.Item.FindControl("lbl_ODC_Total_Freight");

            lbl_Super_ODC_NO_OF_GC = (Label)e.Item.FindControl("lbl_Super_ODC_NO_OF_GC");
            lbl_Super_ODC_Charged_Weight = (Label)e.Item.FindControl("lbl_Super_ODC_Charged_Weight");
            lbl_Super_ODC_ToPay_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_ToPay_Freight");
            lbl_Super_ODC_Paid_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_Paid_Freight");
            lbl_Super_ODC_TBB_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_TBB_Freight");
            lbl_Super_ODC_STB_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_STB_Freight");
            lbl_Super_ODC_FOC_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_FOC_Freight");
            lbl_Super_ODC_Total_Freight = (Label)e.Item.FindControl("lbl_Super_ODC_Total_Freight");

            lbl_Grand_NO_OF_GC = (Label)e.Item.FindControl("lbl_Grand_NO_OF_GC");
            lbl_Grand_Charged_Weight = (Label)e.Item.FindControl("lbl_Grand_Charged_Weight");
            lbl_Grand_ToPay_Freight = (Label)e.Item.FindControl("lbl_Grand_ToPay_Freight");
            lbl_Grand_Paid_Freight = (Label)e.Item.FindControl("lbl_Grand_Paid_Freight");
            lbl_Grand_TBB_Freight = (Label)e.Item.FindControl("lbl_Grand_TBB_Freight");
            lbl_Grand_STB_Freight = (Label)e.Item.FindControl("lbl_Grand_STB_Freight");
            lbl_Grand_FOC_Freight = (Label)e.Item.FindControl("lbl_Grand_FOC_Freight");
            lbl_Grand_Total_Freight = (Label)e.Item.FindControl("lbl_Grand_Total_Freight");


            lbl_Sundry_NO_OF_GC.Text = Sundry_No_OF_GC.ToString(); 
            lbl_Sundry_Charged_Weight.Text = Sundry_Charged_Weight.ToString();
            lbl_Sundry_ToPay_Freight.Text = Sundry_ToPay_Freight.ToString();
            lbl_Sundry_Paid_Freight.Text = Sundry_Paid_Freight.ToString();
            lbl_Sundry_TBB_Freight.Text = Sundry_TBB_Freight.ToString(); 
            lbl_Sundry_STB_Freight.Text = Sundry_STB_Freight.ToString();
            lbl_Sundry_FOC_Freight.Text = Sundry_FOC_Freight.ToString(); 
            lbl_Sundry_Total_Freight.Text = Sundry_Total_Freight.ToString();

            lbl_FTL_NO_OF_GC.Text = FTL_No_OF_GC.ToString(); 
            lbl_FTL_Charged_Weight.Text = FTL_Charged_Weight.ToString();
            lbl_FTL_ToPay_Freight.Text = FTL_ToPay_Freight.ToString();
            lbl_FTL_Paid_Freight.Text = FTL_Paid_Freight.ToString();
            lbl_FTL_TBB_Freight.Text = FTL_TBB_Freight.ToString();
            lbl_FTL_STB_Freight.Text = FTL_STB_Freight.ToString();
            lbL_FTL_FOC_Freight.Text = FTL_FOC_Freight.ToString();
            lbl_FTL_Total_Freight.Text = FTL_Total_Freight.ToString();

            lbl_ODC_NO_OF_GC.Text = ODC_No_OF_GC.ToString();
            lbl_ODC_Charged_Weight.Text = ODC_Charged_Weight.ToString();
            lbl_ODC_ToPay_Freight.Text = ODC_ToPay_Freight.ToString();
            lbl_ODC_Paid_Freight.Text = ODC_Paid_Freight.ToString();
            lbl_ODC_TBB_Freight.Text = ODC_TBB_Freight.ToString();
            lbl_ODC_STB_Freight.Text = ODC_STB_Freight.ToString();
            lbl_ODC_FOC_Freight.Text = ODC_FOC_Freight.ToString();
            lbl_ODC_Total_Freight.Text = ODC_Total_Freight.ToString();

            lbl_Super_ODC_NO_OF_GC.Text = Super_ODC_No_OF_GC.ToString();
            lbl_Super_ODC_Charged_Weight.Text = Super_ODC_Charged_Weight.ToString();
            lbl_Super_ODC_ToPay_Freight.Text = Super_ODC_ToPay_Freight.ToString();
            lbl_Super_ODC_Paid_Freight.Text = Super_ODC_Paid_Freight.ToString();
            lbl_Super_ODC_TBB_Freight.Text = Super_ODC_TBB_Freight.ToString();
            lbl_Super_ODC_STB_Freight.Text = Super_ODC_STB_Freight.ToString();
            lbl_Super_ODC_FOC_Freight.Text = Super_ODC_FOC_Freight.ToString();
            lbl_Super_ODC_Total_Freight.Text = Super_ODC_Total_Freight.ToString();

            lbl_Grand_NO_OF_GC.Text = Grand_No_OF_GC.ToString();
            lbl_Grand_Charged_Weight.Text = Grand_Charged_Weight.ToString();
            lbl_Grand_ToPay_Freight.Text = Grand_ToPay_Freight.ToString();
            lbl_Grand_Paid_Freight.Text = Grand_Paid_Freight.ToString();
            lbl_Grand_TBB_Freight.Text = Grand_TBB_Freight.ToString();
            lbl_Grand_STB_Freight.Text = Grand_STB_Freight.ToString();
            lbl_Grand_FOC_Freight.Text = Grand_FOC_Freight.ToString();
            lbl_Grand_Total_Freight.Text = Grand_Total_Freight.ToString();
            lbl_Total.Text = "Total";
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Sundry_No_OF_GC = Util.String2Int(dr["Sundry No OF GC"].ToString());
        Sundry_Charged_Weight = Util.String2Decimal(dr["Sundry Charged Weight"].ToString());
        Sundry_ToPay_Freight = Util.String2Decimal (dr["Sundry ToPay Freight"].ToString());
        Sundry_Paid_Freight = Util.String2Decimal(dr["Sundry Paid Freight"].ToString());
        Sundry_TBB_Freight = Util.String2Decimal(dr["Sundry TBB Freight"].ToString());
        Sundry_STB_Freight = Util.String2Int(dr["Sundry STB Freight"].ToString());
        Sundry_FOC_Freight = Util.String2Int(dr["Sundry FOC Freight"].ToString());
        Sundry_Total_Freight = Util.String2Decimal(dr["Sundry Total Freight"].ToString());

        FTL_No_OF_GC = Util.String2Int(dr["FTL No OF GC"].ToString());
        FTL_Charged_Weight = Util.String2Int(dr["FTL Charged Weight"].ToString());
        FTL_ToPay_Freight = Util.String2Int(dr["FTL ToPay Freight"].ToString());
        FTL_Paid_Freight = Util.String2Int(dr["FTL Paid Freight"].ToString());
        FTL_TBB_Freight = Util.String2Int(dr["FTL TBB Freight"].ToString());
        FTL_STB_Freight = Util.String2Int(dr["FTL STB Freight"].ToString());
        FTL_FOC_Freight = Util.String2Int(dr["FTL FOC Freight"].ToString());
        FTL_Total_Freight = Util.String2Int(dr["FTL Total Freight"].ToString());

        ODC_No_OF_GC = Util.String2Int(dr["ODC No OF GC"].ToString());
        ODC_Charged_Weight = Util.String2Int(dr["ODC Charged Weight"].ToString());
        ODC_ToPay_Freight = Util.String2Int(dr["ODC ToPay Freight"].ToString());
        ODC_Paid_Freight = Util.String2Int(dr["ODC Paid Freight"].ToString());
        ODC_TBB_Freight = Util.String2Int(dr["ODC TBB Freight"].ToString()); 
        ODC_STB_Freight = Util.String2Int(dr["ODC STB Freight"].ToString());
        ODC_FOC_Freight = Util.String2Int(dr["ODC FOC Freight"].ToString()); 
        ODC_Total_Freight = Util.String2Int(dr["ODC Total Freight"].ToString());

        Super_ODC_No_OF_GC = Util.String2Int(dr["Super ODC No OF GC"].ToString());
        Super_ODC_Charged_Weight = Util.String2Int(dr["Super ODC Charged Weight"].ToString());
        Super_ODC_ToPay_Freight = Util.String2Int(dr["Super ODC ToPay Freight"].ToString()); 
        Super_ODC_Paid_Freight = Util.String2Int(dr["Super ODC Paid Freight"].ToString());
        Super_ODC_TBB_Freight = Util.String2Int(dr["Super ODC TBB Freight"].ToString()); 
        Super_ODC_STB_Freight = Util.String2Int(dr["Super ODC STB Freight"].ToString());
        Super_ODC_FOC_Freight = Util.String2Int(dr["Super ODC FOC Freight"].ToString()); 
        Super_ODC_Total_Freight = Util.String2Int(dr["Super ODC Total Freight"].ToString());

        Grand_No_OF_GC = Util.String2Int(dr["Total No OF GC"].ToString());
        Grand_Charged_Weight = Util.String2Int(dr["Total Charged Weight"].ToString());
        Grand_ToPay_Freight = Util.String2Decimal(dr["Total ToPay Freight"].ToString());
        Grand_Paid_Freight = Util.String2Decimal(dr["Total Paid Freight"].ToString());
        Grand_TBB_Freight = Util.String2Int(dr["Total TBB Freight"].ToString());
        Grand_STB_Freight = Util.String2Int(dr["Total STB Freight"].ToString());
        Grand_FOC_Freight = Util.String2Int(dr["Total FOC Freight"].ToString());
        Grand_Total_Freight = Util.String2Decimal(dr["Total Freight"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };
        if (Util.String2Int(ddl_Report_Type.SelectedValue) != 0)
        {
             objDAL.RunProc(Func_Proc_Name(), objSqlParam, ref ds);

             dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

             calculate_totals();
             Common objcommon = new Common();
             objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

             if (CallFrom == "exporttoexcelusercontrol")
             {
                 PrepareDTForExportToExcel();
             }        
            
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr;
            //    calculate_totals();
            //    dr = ds.Tables[0].NewRow();
            //    dr["Column Name"] = "Total";

            //    dr["Sundry No OF GC"] = Sundry_No_OF_GC;
            //    dr["Sundry Charged Weight"] = Sundry_Charged_Weight;
            //    dr["Sundry ToPay Freight"] = Sundry_ToPay_Freight;
            //    dr["Sundry Paid Freight"] = Sundry_Paid_Freight;
            //    dr["Sundry TBB Freight"] = Sundry_TBB_Freight;
            //    dr["Sundry STB Freight"] = Sundry_STB_Freight;
            //    dr["Sundry FOC Freight"] = Sundry_FOC_Freight;
            //    dr["Sundry Total Freight"] = Sundry_Total_Freight;

            //    dr["FTL No OF GC"] = FTL_No_OF_GC;
            //    dr["FTL Charged Weight"] = FTL_Charged_Weight;
            //    dr["FTL ToPay Freight"] = FTL_ToPay_Freight;
            //    dr["FTL Paid Freight"] = FTL_Paid_Freight;
            //    dr["FTL TBB Freight"] = FTL_TBB_Freight;
            //    dr["FTL STB Freight"] = FTL_STB_Freight;
            //    dr["FTL FOC Freight"] = FTL_FOC_Freight;
            //    dr["FTL Total Freight"] = FTL_Total_Freight;

            //    dr["ODC No OF GC"] = ODC_No_OF_GC;
            //    dr["ODC Charged Weight"] = ODC_Charged_Weight;
            //    dr["ODC ToPay Freight"] = ODC_ToPay_Freight;
            //    dr["ODC Paid Freight"] = ODC_Paid_Freight;
            //    dr["ODC TBB Freight"] = ODC_TBB_Freight;
            //    dr["ODC STB Freight"] = ODC_STB_Freight;
            //    dr["ODC FOC Freight"] = ODC_FOC_Freight;
            //    dr["ODC Total Freight"] = ODC_Total_Freight;

            //    dr["Super ODC No OF GC"] = Super_ODC_No_OF_GC;
            //    dr["Super ODC Charged Weight"] = Super_ODC_Charged_Weight;
            //    dr["Super ODC ToPay Freight"] = Super_ODC_ToPay_Freight;
            //    dr["Super ODC Paid Freight"] = Super_ODC_Paid_Freight;
            //    dr["Super ODC TBB Freight"] = Super_ODC_TBB_Freight;
            //    dr["Super ODC STB Freight"] = Super_ODC_STB_Freight;
            //    dr["Super ODC FOC Freight"] = Super_ODC_FOC_Freight;
            //    dr["Super ODC Total Freight"] = Super_ODC_Total_Freight;

            //    dr["Total No OF GC"] = Grand_No_OF_GC;
            //    dr["Total Charged Weight"] = Grand_Charged_Weight;
            //    dr["Total ToPay Freight"] = Grand_ToPay_Freight;
            //    dr["Total Paid Freight"] = Grand_Paid_Freight;
            //    dr["Total TBB Freight"] = Grand_TBB_Freight;
            //    dr["Total STB Freight"] = Grand_STB_Freight;
            //    dr["Total FOC Freight"] = Grand_FOC_Freight;
            //    dr["Total Freight"] = Grand_Total_Freight;

            //    ds.Tables[0].Rows.Add(dr);
            //    DataSet ds_Excel = new DataSet();
            //    ds_Excel = ds.Copy();
            //    ds_Excel.Tables[0].Columns["Column Name"].ColumnName = Func_Column_Name();

            //    Wuc_Export_To_Excel1.SessionExporttoExcel = ds_Excel;
            //    ds.Tables[0].Rows.RemoveAt(ds.Tables[0].Rows.Count - 1);
            //    dg_Grid.DataSource = ds;
            //    dg_Grid.DataBind();
            //    SessionBookingSummary = ds_Excel;
            //    dg_Grid.Visible = true;
            //    lbl_Error.Text = "";
            //}
            //else
            //{
            //    dg_Grid.Visible = false;
            //    lbl_Error.Text = "Record Not Found!";
            //}
        }
        else
        {
            dg_Grid.Visible = false;
            lbl_Error.Text = "Select Report Type";
        }
    }

    private string Func_Proc_Name()
    {
        Proc_Name = "";
        
        if (Util.String2Int(ddl_Report_Type.SelectedValue) == 1)
        {
            Proc_Name = "EC_RPT_Booking_Summary_BookingBranchWise";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 2)
        {
            Proc_Name = "EC_RPT_Booking_Summary_BookingDateWise";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 3)
        {
            Proc_Name = "EC_RPT_Booking_Summary_DeliveryAreaWise";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 4)
        {
            Proc_Name = "EC_RPT_Booking_Summary_ClientWise";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 5)
        {
            Proc_Name = "EC_RPT_Booking_Summary_CommodityWise";
        }
        return Proc_Name;
    }

    private string Func_Column_Name()
    {
       
        Column_Name = "";

        if (Util.String2Int(ddl_Report_Type.SelectedValue) == 1)
        {
            Column_Name = "Booking Branch";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 2)
        {
            Column_Name = "Booking Date";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 3)
        {
            Column_Name = "Delivery Area";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 4)
        {
            Column_Name = "Client Name";
        }
        else if (Util.String2Int(ddl_Report_Type.SelectedValue) == 5)
        {
            Column_Name = "Commodity Name";
        }
        return Column_Name;
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
        }
    }
    private void PrepareDTForExportToExcel()
    {
        DataRow dr;
        dr = ds.Tables[0].NewRow();
        Func_Column_Name();
        dr["Column Name"] = "Total";

        dr["Sundry No OF GC"] = Sundry_No_OF_GC;
        dr["Sundry Charged Weight"] = Sundry_Charged_Weight;
        dr["Sundry ToPay Freight"] = Sundry_ToPay_Freight;
        dr["Sundry Paid Freight"] = Sundry_Paid_Freight;
        dr["Sundry TBB Freight"] = Sundry_TBB_Freight;
        dr["Sundry STB Freight"] = Sundry_STB_Freight;
        dr["Sundry FOC Freight"] = Sundry_FOC_Freight;
        dr["Sundry Total Freight"] = Sundry_Total_Freight;

        dr["FTL No OF GC"] = FTL_No_OF_GC;
        dr["FTL Charged Weight"] = FTL_Charged_Weight;
        dr["FTL ToPay Freight"] = FTL_ToPay_Freight;
        dr["FTL Paid Freight"] = FTL_Paid_Freight;
        dr["FTL TBB Freight"] = FTL_TBB_Freight;
        dr["FTL STB Freight"] = FTL_STB_Freight;
        dr["FTL FOC Freight"] = FTL_FOC_Freight;
        dr["FTL Total Freight"] = FTL_Total_Freight;

        dr["ODC No OF GC"] = ODC_No_OF_GC;
        dr["ODC Charged Weight"] = ODC_Charged_Weight;
        dr["ODC ToPay Freight"] = ODC_ToPay_Freight;
        dr["ODC Paid Freight"] = ODC_Paid_Freight;
        dr["ODC TBB Freight"] = ODC_TBB_Freight;
        dr["ODC STB Freight"] = ODC_STB_Freight;
        dr["ODC FOC Freight"] = ODC_FOC_Freight;
        dr["ODC Total Freight"] = ODC_Total_Freight;

        dr["Super ODC No OF GC"] = Super_ODC_No_OF_GC;
        dr["Super ODC Charged Weight"] = Super_ODC_Charged_Weight;
        dr["Super ODC ToPay Freight"] = Super_ODC_ToPay_Freight;
        dr["Super ODC Paid Freight"] = Super_ODC_Paid_Freight;
        dr["Super ODC TBB Freight"] = Super_ODC_TBB_Freight;
        dr["Super ODC STB Freight"] = Super_ODC_STB_Freight;
        dr["Super ODC FOC Freight"] = Super_ODC_FOC_Freight;
        dr["Super ODC Total Freight"] = Super_ODC_Total_Freight;

        dr["Total No OF GC"] = Grand_No_OF_GC;
        dr["Total Charged Weight"] = Grand_Charged_Weight;
        dr["Total ToPay Freight"] = Grand_ToPay_Freight;
        dr["Total Paid Freight"] = Grand_Paid_Freight;
        dr["Total TBB Freight"] = Grand_TBB_Freight;
        dr["Total STB Freight"] = Grand_STB_Freight;
        dr["Total FOC Freight"] = Grand_FOC_Freight;
        dr["Total Freight"] = Grand_Total_Freight;

        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
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
}
