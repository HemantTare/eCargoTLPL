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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Harshal Sapre
//Desc   : Delivery Stock List Report
//Date   : 14-01-09

public partial class Reports_Booking_FrmDeliveryAreaDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_Gc, TotalFreight;
    decimal Basic_Freight, Total_Freight, Invoice_Value;
    string ToPayTotal, PaidTotal, TBBTotal;
    string Crypt;

    public int RegionID
    {
        get { return Convert.ToInt32(ViewState["_RegionID"]); }
        set { ViewState["_RegionID"] = value; }
    }
    public int AreaID
    {
        get { return Convert.ToInt32(ViewState["_AreaID"]); }
        set { ViewState["_AreaID"] = value; }
    }
    public int BranchID
    {
        get { return Convert.ToInt32(ViewState["_BranchID"]); }
        set { ViewState["_BranchID"] = value; }
    }
    public string As_On_Date
    {
        get { return Convert.ToString(ViewState["_As_On_Date"]); }
        set { ViewState["_As_On_Date"] = value; }
    }
    public int Division_Id
    {
        get { return Convert.ToInt32(ViewState["_Division_Id"]); }
        set { ViewState["_Division_Id"] = value; }
    }
    public int DeliveryAreaID
    {
        get { return Convert.ToInt32(ViewState["_DeliveryAreaID"]); }
        set { ViewState["_DeliveryAreaID"] = value; }
    }
    public string RegionText
    {
        get { return Convert.ToString(ViewState["_RegionText"]); }
        set
        {
            ViewState["_RegionText"] = value; 
        }
    }
    public string AreaText
    {
        get { return Convert.ToString(ViewState["_AreaText"]); }
        set
        {
            ViewState["_AreaText"] = value;
        }
    }
    public string BranchText
    {
        get { return Convert.ToString(ViewState["_BranchText"]); }
        set
        {
            ViewState["_BranchText"] = value;
        }
    }
    public string DlyArea
    {
        get { return Convert.ToString(ViewState["_DlyArea"]); }
        set
        {
            ViewState["_DlyArea"] = value; 
        }
    }
    #endregion


    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = DlyArea + "  DlyAreaWiseDeliveryStockList";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["Region_Id"];
            RegionID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Area_Id"];
            AreaID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Branch_Id"];
            BranchID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            As_On_Date = Request.QueryString["As_On_Date"];

            Crypt = Request.QueryString["Division_Id"];
            Division_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["DeliveryAreaID"];
            DeliveryAreaID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["RegionText"];
            RegionText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["AreaText"];
            AreaText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["BranchText"];
            BranchText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["DlyArea"];
            DlyArea = ClassLibraryMVP.Util.DecryptToString(Crypt); 

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form", e); 
        }
             
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
            int GC_ID, DeliveryAreaID;
            LinkButton lnk_GC_No;//, lbtn_DlyArea

            if ((e.Item.Cells[8].Text) == "NEW")
            {
                e.Item.Cells[8].ForeColor = System.Drawing.Color.Red;
                e.Item.Cells[8].Font.Bold = true;
            }
            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            DeliveryAreaID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DeliveryAreaID").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");
            //lbtn_DlyArea = (LinkButton)e.Item.FindControl("lbtn_DlyArea");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");
            //lbtn_DlyArea.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + ClassLibraryMVP.Util.EncryptInteger(DeliveryAreaID) + "')");
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

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,RegionID),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,AreaID),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,BranchID),
            objDAL.MakeInParams("@As_On_Date", SqlDbType.DateTime, 0,As_On_Date),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0,DeliveryAreaID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 
        };

        objDAL.RunProc("EC_RPT_Delivery_Stock_List_DlyAreaWise", objSqlParam, ref ds);
        //if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error); 

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
    #endregion
 
}
