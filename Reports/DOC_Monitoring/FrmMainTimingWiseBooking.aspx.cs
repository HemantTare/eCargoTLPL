using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_DOC_Monitoring_FrmMainTimingWiseBooking : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds; 
    int TotalGC;
    #endregion

    #region EventClick

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "TimingWiseBooking"; 

        if (IsPostBack == false)
        { 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            
            BindGrid("form_and_pageload", e);     
        }
            Wuc_Region_Area_Branch1.RegionIndexChange += new EventHandler(ClearGrid);
            Wuc_Region_Area_Branch1.AreaIndexChange += new EventHandler(ClearGrid);
            Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(ClearGrid);   

        if (Wuc_Region_Area_Branch1.BranchID <= 0 || Wuc_Region_Area_Branch1.AreaID <= 0 || Wuc_Region_Area_Branch1.RegionID <= 0)
        {
            Wuc_From_To_Datepicker1.Set_To_Date_Visible = "hidden";
            Wuc_From_To_Datepicker1.SelectedToDate = Wuc_From_To_Datepicker1.SelectedFromDate; 
        }
        else
        {
            Wuc_From_To_Datepicker1.Set_To_Date_Visible = "visible";
        }
    }

    private void ClearGrid(object sender, EventArgs e)
    {
        dg_Grid.DataSource = null;
        dg_Grid.DataBind();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Errors.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
            //dg_Grid.Columns[1].Visible = false;
        }
        else
        {
            lbl_Errors.Text = msg;
            dg_Grid.Visible = false;
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

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        if (CallFrom == "form_and_pageload") return;

        if (Region_Id != 0 || Area_id != 0 || Branch_id != 0)
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date) 
        };



            objDAL.RunProc("EC_RPT_BranchWise_Timing_Booking_Register_GRD", objSqlParam, ref ds);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (CallFrom == "form_and_pageload") return;

                    //dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
                    //string TotalRecords = ds.Tables[1].Rows[0][0].ToString();

                    Common objcommon = new Common();
                    objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Errors);

                    //dg_Grid.Columns.RemoveAt(0);

                    if (CallFrom == "exporttoexcelusercontrol")
                    {
                        PrepareDTForExportToExcel();
                    }
                }
            }
            else
            {
                errorMessage = "No Record Found";
                lbl_Errors.Visible = true;
                return;
            }
        }
        else
        {
          errorMessage ="Select Any Single Region or Area or Branch";
          lbl_Errors.Visible = true;
        
        }
    }

    private void PrepareDTForExportToExcel()
    { 

        //ds.Tables[0].Columns.Remove("Sr No.");
        //ds.Tables[0].Columns.Remove("GC_ID"); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

     
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) //  == DataControlRowType.DataRow)
        {
            HyperLink link = new HyperLink();
            //LinkButton link = new LinkButton();
            LinkButton lnk_GC_No = new LinkButton();
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");
            link.Text = lnk_GC_No.Text;
            //link.NavigateUrl = "Navigate somewhere based on data: " + e.Item.DataItem;
           

            //e.Row.Cells[ColumnIndex.Column1].Controls.Add(link);
        
            string DlyToPayRecDateWiseUrl;
         
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_Id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_Id = Wuc_Region_Area_Branch1.BranchID; 
            DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
            DateTime To_Date = Wuc_From_To_Datepicker1.SelectedToDate;
            
            string RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
            string AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
            string BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;
          

            int Menu_Item_Id = Raj.EC.Common.GetMenuItemId(); 

            DlyToPayRecDateWiseUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Reports/DOC_Monitoring/FrmTimingWiseBooking.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
                         "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&RegionText=" + Util.EncryptString(RegionText)
                         + "&Area_Id=" + Util.EncryptInteger(Area_Id) + "&AreaText=" + Util.EncryptString(AreaText)
                         + "&Branch_Id=" + Util.EncryptInteger(Branch_Id) + "&BranchText=" + Util.EncryptString(BranchText)
                         + "&From_Date=" + Convert.ToDateTime(From_Date) + "&To_Date=" + Convert.ToDateTime(To_Date)
                         + "&link_Text=" + Util.EncryptString(link.Text);

            link.NavigateUrl = DlyToPayRecDateWiseUrl + e.Item.DataItem;


            e.Item.Cells[1].Controls.Add(link);
            e.Item.Attributes.Add("onclick", "return DlyToPayRecDate('" + DlyToPayRecDateWiseUrl + "');");

            //link.Attributes.Add("onclick", "return DlyToPayRecDate('" + DlyToPayRecDateWiseUrl + "');");
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
 
}
