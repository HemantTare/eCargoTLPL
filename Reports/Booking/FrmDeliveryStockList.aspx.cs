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

//Author : Harshal Sapre
//Desc   : Delivery Stock List Report
//Date   : 14-01-09

public partial class Reports_Booking_FrmDeliveryStockList : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_Gc, TotalFreight;
    decimal Basic_Freight, Total_Freight, Invoice_Value;
    string ToPayTotal, PaidTotal, TBBTotal;
    //bool Is_InwardDateWise;
    #endregion

    public bool Is_InwardDateWise
    { 
        get 
        {
            return Convert.ToBoolean(ViewState["_Is_InwardDateWise"]);
        }
        set
        {
            ViewState["_Is_InwardDateWise"] = value; 
        }  
    }

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DeliveryStockList";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            rbtn_SummDetails.SelectedValue = "Summary";

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
            if (UserManager.getUserParam().HierarchyCode == "BO")
            {
                WucDatePicker1.Disable = true;
            }
            SummDetails();
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        ////rbtn_SummDetails.SelectedValue = "Summary"; 
        if ((objDateCommon.Vaildate_Date(WucDatePicker1.SelectedDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            //dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            if (rbtn_SummDetails.SelectedValue.Length > 0)
            {
                BindGrid("form", e);
            }
        }
        else
        {
            lbl_Error.Text = msg;
            //dg_Grid.Visible = false;
        }
        SummDetails();
    }
    
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
        SummDetails();
    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
        SummDetails();
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_ChargedWeight, lbl_ActualWeight, lbl_Articles, lbl_Total_Gc;
            Label lbl_BasicFreight, lbl_TotalFreight, lbl_InvoiceValue;

            lbl_ChargedWeight = (Label)e.Item.FindControl("lbl_ChargedWeight");
            lbl_ActualWeight = (Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (Label)e.Item.FindControl("lbl_Articles");
            lbl_BasicFreight = (Label)e.Item.FindControl("lbl_BasicFreight");
            lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_InvoiceValue = (Label)e.Item.FindControl("lbl_InvoiceValue");
            lbl_Total_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Gc");

            lbl_Total_Gc.Text = Total_Gc.ToString();
            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_BasicFreight.Text = Basic_Freight.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
            lbl_InvoiceValue.Text = Invoice_Value.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID ;
            LinkButton lnk_GC_No ;

            if ((e.Item.Cells[8].Text) == "NEW")
            {
                e.Item.Cells[8].ForeColor = System.Drawing.Color.Red;
                e.Item.Cells[8].Font.Bold = true;
            }
            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
          
        }
    }

    protected void dg_GridTotal_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
        {
            Label lbl_ToPayTotal, lbl_PaidTotal, lbl_TBBTotal;

            lbl_ToPayTotal = (Label)e.Item.FindControl("lbl_ToPayTotal");
            lbl_PaidTotal = (Label)e.Item.FindControl("lbl_PaidTotal");
            lbl_TBBTotal = (Label)e.Item.FindControl("lbl_TBBTotal");

            lbl_ToPayTotal.Text = ToPayTotal.ToString();
            lbl_PaidTotal.Text = PaidTotal.ToString();
            lbl_TBBTotal.Text = TBBTotal.ToString();
        }
    }


    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumArticles, lbl_SumTotal_Gc, lbl_SumTotalFreight;

            lbl_SumArticles = (Label)e.Item.FindControl("lbl_SumArticles");
            lbl_SumTotal_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotal_Gc");
            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");
            
            lbl_SumArticles.Text = Articles.ToString();
            lbl_SumTotal_Gc.Text = Total_Gc.ToString();
            lbl_SumTotalFreight.Text = Total_Freight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int   DeliveryAreaID;
            LinkButton lnk_GC_No, lbtn_DlyArea;

            
            DeliveryAreaID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DeliveryAreaID").ToString());
            
            lbtn_DlyArea = (LinkButton)e.Item.FindControl("lbtn_DlyArea");

            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_id = Wuc_Region_Area_Branch1.BranchID;

            string RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
            string AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
            string BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;

            DateTime As_On_Date = WucDatePicker1.SelectedDate;
            int Division_Id = WucDivisions1.Division_ID;

            //lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");


            StringBuilder PathDlyStk = new StringBuilder(Util.GetBaseURL());
            PathDlyStk.Append("/");
            PathDlyStk.Append("Reports/Booking/FrmDeliveryAreaDetails.aspx?Region_Id=" + ClassLibraryMVP.Util.EncryptInteger(Region_Id)
                + "&Area_Id=" + ClassLibraryMVP.Util.EncryptInteger(Area_id)
                + "&Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_id)
                + "&As_On_Date=" + As_On_Date.ToString()
                + "&Division_Id=" + ClassLibraryMVP.Util.EncryptInteger(Division_Id)
                + "&DeliveryAreaID=" + ClassLibraryMVP.Util.EncryptInteger(DeliveryAreaID)
                + "&RegionText=" + ClassLibraryMVP.Util.EncryptString(RegionText)
                + "&AreaText=" + ClassLibraryMVP.Util.EncryptString(AreaText)
                + "&BranchText=" + ClassLibraryMVP.Util.EncryptString(BranchText)
                + "&DlyArea=" + ClassLibraryMVP.Util.EncryptString(lbtn_DlyArea.Text));

            lbtn_DlyArea.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + PathDlyStk + "')");
        }
    }
    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Gc = Util.String2Decimal(dr["Total_GC"].ToString());
        ChargedWeight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Total_Actual_Wt"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Total_Invoice_Value"].ToString());
        if (ds.Tables[3].Rows.Count > 0)
        {
            DataRow dr3 = ds.Tables[3].Rows[0];
            ToPayTotal = dr3["ToPay Total"].ToString();
            PaidTotal = dr3["Paid Total"].ToString();
            TBBTotal = dr3["TBB Total"].ToString();
        }
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
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime As_On_Date = WucDatePicker1.SelectedDate;
        int Division_Id = WucDivisions1.Division_ID;

        

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@As_On_Date", SqlDbType.DateTime, 0,As_On_Date),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit),
            objDAL.MakeInParams("@InwardDateWise",SqlDbType.Bit,0,Is_InwardDateWise)
             //objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0,start_date)
        };

        objDAL.RunProc("[EC_RPT_Delivery_Stock_List_GRD]", objSqlParam, ref ds);
        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
        objcommon.ValidateReportForm(dg_GridTotal, ds.Tables[3], CallFrom, lbl_Error);
        objcommon.ValidateReportForm(dg_Details, ds.Tables[4], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["gc_caption Date"] = "Total";
        dr["gc_caption No"] = Total_Gc;
        dr["Charged Weight"] = ChargedWeight;
        dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["Total Freight"] = Total_Freight;
        dr["Invoice Value"] = Invoice_Value;

        ds.Tables[0].Rows.Add(dr);

        DataRow dr1 = ds.Tables[0].NewRow();
        dr1["gc_caption Date"] = "Payment Total";
        dr1["Cnee Name"] = ToPayTotal;
        dr1["Cnr Name"] = PaidTotal;
        dr1["Pay Mode"] = TBBTotal; 
        ds.Tables[0].Rows.Add(dr1);

        if (rbtn_SummDetails.SelectedValue == "Summary")
        {
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[4];
        }
        else
        {
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
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
    #endregion

    protected void rbtn_SummDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        dg_Grid.DataSource = null;
        dg_Grid.DataBind();

        dg_GridTotal.DataSource = null;
        dg_GridTotal.DataBind();

        dg_Details.DataSource = null;
        dg_Details.DataBind();

        if (rbtn_SummDetails.SelectedValue == "Summary")
        {
            Is_InwardDateWise = false;
            dg_Details.CurrentPageIndex = 0;
            //BindGrid("form", e);
        }
        else if (rbtn_SummDetails.SelectedValue == "Details")
        {
            Is_InwardDateWise = false;
            dg_Grid.CurrentPageIndex = 0;
            //BindGrid("form", e);
        }
        else if (rbtn_SummDetails.SelectedValue == "Inward Date")
        {
            Is_InwardDateWise = true;
            dg_Grid.CurrentPageIndex = 0;
            //BindGrid("form", e);
        } 

        SummDetails();
    }

    private void SummDetails()
    {
        if (rbtn_SummDetails.SelectedValue == "Summary")
        {
            dg_Grid.Visible = false;
            dg_GridTotal.Visible = false;
            dg_Details.Visible = true;  
        }
        else if (rbtn_SummDetails.SelectedValue == "Details")
        {
            dg_Grid.Visible = true;
            dg_GridTotal.Visible = true;
            dg_Details.Visible = false;
        }
        else if (rbtn_SummDetails.SelectedValue == "Inward Date")
        {
            dg_Grid.Visible = true;
            dg_GridTotal.Visible = true;
            dg_Details.Visible = false;
        }

    
    }
}
