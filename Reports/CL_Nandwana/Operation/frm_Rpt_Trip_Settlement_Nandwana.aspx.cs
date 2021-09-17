using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Reports_CL_Nandwana_Operation_frm_Rpt_Trip_Settlement_Nandwana : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    Common objcommon = new Common();

     public int Driver_ID
    {
        get { return Util.String2Int(ddl_Driver.SelectedValue); }
    }

    public int Vehicle_ID
    {
        get { return WucVehicleNo.VehicleID; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "Trip Settlement Register";

    }
    private void BindGrid(object sender, EventArgs e)
    {
        string CallFrom = (string)(sender);

        int Region_Id = WucHierarchyWithID1.RegionID;
        int Area_id = WucHierarchyWithID1.AreaID;
        int Branch_id = WucHierarchyWithID1.BranchID;
        string HierarchyCode = WucHierarchyWithID1.HierarchyCode;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Main_Id = 0;
        if (HierarchyCode == "AO")
        { Main_Id = Area_id; }
        else if (HierarchyCode == "RO")
        { Main_Id = Region_Id; }
        else if (HierarchyCode == "BO")
        { Main_Id = Branch_id; }

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Main_id", SqlDbType.Int,0,Main_Id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Vehicle_Id",SqlDbType.Int , 50, Vehicle_ID  ),
            objDAL.MakeInParams("@Driver_Id", SqlDbType.Int,0,Driver_ID ),
            objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,5,HierarchyCode) };

        objDAL.RunProc("[EC_RPT_Trip_Settlement_Register_Nandwana]", objSqlParam, ref ds);

        int TotalRecords = Util.String2Int(ds.Tables[0].Rows.Count.ToString());
        dg_Grid.VirtualItemCount = TotalRecords;
        SetStandardCaptionForDatatable((DataTable)ds.Tables[0]);
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords.ToString());

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
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

    public void ClearVariables()
    {
        ds = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void PrepareDTForExportToExcel()
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            string[] strCols ={ "[Kms Run]", "[Total Actual Wt]", "[Total Hire]", "[Total Advance]",
                            "[Total Fuel Qty.]","[Total Fuel Cost]","[Trip Expense]",
                            "[Total Trip Cost]","[Driver Closing Balance]"};

            ds.Tables[0].Rows.Add(objcommon.AddTotalsInAutoColumnsExport(ds.Tables[0], strCols));
        }
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    private void SetStandardCaptionForDatatable(DataTable dt)
    {
        foreach (DataColumn dtcol in dt.Columns)
        {
            if (dtcol.Caption.ToLower().Contains("gc_caption"))
            {
                dtcol.Caption = dtcol.Caption.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
                dtcol.ColumnName = dtcol.Caption;
                break;
            }
        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            string[] strCols ={ "[Kms Run]", "[Total Actual Wt]", "[Total Hire]", "[Total Advance]",
                                "[Total Fuel Qty.]","[Total Fuel Cost]","[Trip Expense]",
                                "[Total Trip Cost]","[Driver Closing Balance]"};
            objcommon.ShowTotalsInAutoBindGrid(e, ds.Tables[0], strCols);
        }
    }
}
