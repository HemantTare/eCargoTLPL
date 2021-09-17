using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;

public partial class Reports_Booking_Frm_Freight_Rate_Branch_To_Branch_Report : System.Web.UI.Page
{
    private DataSet ds = new DataSet();
    private DataSet ds1 = new DataSet();
    private DAL objdal = new DAL();
    int Main_Id;
    string Hirearchy_Code;
    decimal Normal_Rate;

    private void GetBool_Value()
    {
        if (Hirearchy_Code != "AD")
        {
            if (rdl_type.SelectedValue == "0")
            {
                Wuc_Region_Area_Branch1.ShowAll = false;
                Wuc_Region_Area_Branch2.ShowAll = true;
            }
            else
            {
                Wuc_Region_Area_Branch2.ShowAll = false;
                Wuc_Region_Area_Branch1.ShowAll = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetBool_Value();

        Main_Id = (int)UserManager.getUserParam().MainId;
        Hirearchy_Code = (string)UserManager.getUserParam().HierarchyCode;

        Wuc_Region_Area_Branch1.SetRegionCaption = "From Region:";
        Wuc_Region_Area_Branch1.SetAreaCaption = "From Area:";
        Wuc_Region_Area_Branch1.SetBranchCaption = "From Branch:";

        Wuc_Region_Area_Branch2.SetRegionCaption = "To Region:";
        Wuc_Region_Area_Branch2.SetAreaCaption = "To Area:";
        Wuc_Region_Area_Branch2.SetBranchCaption = "To Branch:";
               
        if (!IsPostBack)
        {
            GetBool_Value();

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
        
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "Freight Rate Branch To Branch Report";
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
        BindGrid("form", e);
    }

    private DataSet Get_Branch(int Main_Id, string Hierarchy_Code, bool Is_Crossing, bool Is_DropDownClick, string Region_Area, int Id)
    {
        SqlParameter[] SqlPara = {objdal.MakeInParams("@Main_id", SqlDbType.Int, 0, Main_Id), 
                                  objdal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, Hierarchy_Code),
                                  objdal.MakeInParams("@Is_Crossing", SqlDbType.Bit, 0, Is_Crossing),
                                  objdal.MakeInParams("@Is_DropDownClick", SqlDbType.Bit, 0, Is_DropDownClick),
                                //objdal.MakeInParams("@Region_Area", SqlDbType.VarChar, 5, Region_Area),
                                  objdal.MakeInParams("@Id", SqlDbType.Int,4,Id)};

        objdal.RunProc("dbo.EC_RPT_Fill_Branch_On_Login", SqlPara, ref  ds);
        return ds;

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
       
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@From_Region_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.RegionID),
                objDAL.MakeInParams("@From_Area_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.AreaID),
                objDAL.MakeInParams("@From_Branch_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch1.BranchID),

                objDAL.MakeInParams("@To_Region_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch2.RegionID),        
                objDAL.MakeInParams("@To_Area_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch2.AreaID),        
                objDAL.MakeInParams("@To_Branch_Id", SqlDbType.Int, 0,Wuc_Region_Area_Branch2.BranchID),       

                objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
                objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
                objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
                objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
                objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
                objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
                objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
                objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };

        objDAL.RunProc("[EC_RPT_Freight_Rate_Branch_To_Branch_Excel]", objSqlParam, ref ds);
        if (CallFrom == "form_and_pageload") return;
        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        //calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }  

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_total,lbl_Freight;

            lbl_Freight = (Label)e.Item.FindControl("lbl_Freight");
            lbl_total = (Label)e.Item.FindControl("lbl_total");

            //lbl_total.Text = "Total";
            //lbl_Freight.Text = Normal_Rate.ToString();
            //lbl_Demurrage_Days.Text = demurrage_days.ToString();           
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        //Normal_Rate = Util.String2Decimal(dr["VTrans Normal Rate"].ToString());
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        //dr["From Branch"] = "Total";
        //dr["Freight Rate"] = Normal_Rate;

        //ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void rdl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBool_Value();
    }
}
