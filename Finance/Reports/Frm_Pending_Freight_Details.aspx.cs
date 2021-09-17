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


public partial class Finance_Reports_Frm_Pending_Freight_Details : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_Freight;

    public DateTime FromDate
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get
        {
            return Wuc_From_To_Datepicker1.SelectedFromDate;
        }
    }

    public DateTime ToDate
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;

        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }
    #endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingFreightDetails";

    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotalFreight;

            lbl_SumTotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalFreight");

            lbl_SumTotalFreight.Text = Total_Freight.ToString();

            if (UserManager.getUserParam().MainId > 0)
            {
                e.Item.Visible = false;
            }
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int GC_ID;
            LinkButton lnk_LRNo;

            lnk_LRNo = (LinkButton)e.Item.FindControl("lnk_LRNo");

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            lnk_LRNo.Attributes.Add("onclick", "return viewwindow_GC('" + GC_ID + "')");
        }
    }



    #endregion

    #region Other Function

    protected void btn_view_Click(object sender, EventArgs e)
    {

        if (Wuc_Region_Area_Branch1.BranchID > 0)
        {
            dg_Grid.CurrentPageIndex =0;
            lbl_Error.Text = "";
            BindGrid("form", e);
        }
        else
        {
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();

            lbl_Error.Text = "Select Atleast One Branch";
        }
    }


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Freight = Util.String2Decimal(dr["TotalFreight"].ToString());
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



        int BranchID = Wuc_Region_Area_Branch1.BranchID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0,BranchID),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_Opr_Pending_Freight_Details", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["LRNo"] = "Total : ";
        dr["PendingFreight"] = Total_Freight;

        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Columns.Remove("GC_ID");

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
