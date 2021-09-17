using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_CL_Nandwana_User_Desk_Frm_LRStatus : System.Web.UI.Page
{
    private DataSet ds;
    decimal Articles, Actual_Weight;            
    Common objcommon = new Raj.EC.Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "LR Status Report";
        if (!IsPostBack)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
         DateCommon objDateCommon = new DateCommon();
        string msg = "";
        Session["LRStatus"] = null;
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
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label   lbl_Articles,  lbl_ActualWeight;
            lbl_Articles = (Label)e.Item.FindControl("lbl_Articles");            
            lbl_ActualWeight = (Label)e.Item.FindControl("lbl_ActualWeight");
                     
            lbl_Articles.Text = Articles.ToString();           
            lbl_ActualWeight.Text = Actual_Weight.ToString();      
        }
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;        
        BindGrid("form", e);
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
        if (Session["LRStatus"] == null)
        {
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_id = Wuc_Region_Area_Branch1.BranchID;
            DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
            DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
            int BookingTypeId = Wuc_GC_Parameters1.Booking_Type_ID;
            int DeliveryTypeId = Wuc_GC_Parameters1.Delivery_Type_ID;
            int PaymentTypeId = Wuc_GC_Parameters1.Payment_Type_ID;

            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
                objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
                objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
                objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
                objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int,0,BookingTypeId),
                objDAL.MakeInParams("@Delivery_Type_Id", SqlDbType.Int,0,DeliveryTypeId),
                objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,PaymentTypeId),
                objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,WucDivisions1.Division_ID),
                objDAL.MakeInParams("@LR_No",SqlDbType.VarChar,20,Txt_LR_No.Text.Trim()),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
            };
        
            objDAL.RunProc("EC_RPT_LR_Status_Nandwana", objSqlParam, ref ds);
            Session["LRStatus"] = ds;
        }
        else
        {
            ds = (DataSet)Session["LRStatus"];
        }
        if (CallFrom == "form_and_pageload")
        {
            return;
        }

         string TotalRecords = ds.Tables[0].Rows.Count.ToString();
         Articles = Util.String2Decimal(ds.Tables[0].Compute("Sum(Total_Articles)", "").ToString());
         Actual_Weight = Util.String2Decimal(ds.Tables[0].Compute("Sum(Total_Actual_Weight)", "").ToString());


         objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
       
        dr["Total Articles"] = Articles;     
        dr["Total Weight"] = Actual_Weight;         
        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    public void ClearVariables()
    {
        ds = null;
    }
}
