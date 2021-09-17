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


public partial class Reports_GSTR1_Frm_Rpt_GSTR1_B2C : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_SubTotal1, Total_SubTotal, Total_GST, Total_iGST, Total_CGST, Total_SGST;

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

        Wuc_Export_To_Excel1.FileName = "GSTR1-B2C";

        if (IsPostBack == false)
        {
            Fill_State();
        }
    }

    private void Fill_State()
    {
        DAL objDAL = new DAL();

        DataSet dsState = new DataSet();

        objDAL.RunProc("EC_RPT_GSTR1_Fill_State", ref dsState);

        ddl_State.DataSource = dsState;
        ddl_State.DataTextField = "State_Name";
        ddl_State.DataValueField = "State_ID";
        ddl_State.DataBind();
    }


    protected void dg_Grid1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotalSubTotal1;

            lbl_SumTotalSubTotal1 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalSubTotal1");
            lbl_SumTotalSubTotal1.Text = Total_SubTotal1.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

        }
    }



    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_SumTotalSubTotal, lbl_SumTotalGST, lbl_SumTotaliGST, lbl_SumTotalCGST, lbl_SumTotalSGST;

            lbl_SumTotalSubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalSubTotal");
            lbl_SumTotalGST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalGST");
            lbl_SumTotaliGST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotaliGST");
            lbl_SumTotalCGST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalCGST");
            lbl_SumTotalSGST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SumTotalSGST");

            lbl_SumTotalSubTotal.Text = Total_SubTotal.ToString();
            lbl_SumTotalGST.Text = Total_GST.ToString();
            lbl_SumTotaliGST.Text = Total_iGST.ToString();
            lbl_SumTotalCGST.Text = Total_CGST.ToString();
            lbl_SumTotalSGST.Text = Total_SGST.ToString();

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

        }
    }



    #endregion

    #region Other Function

    protected void btn_view_Click(object sender, EventArgs e)
    {

        dg_Grid.CurrentPageIndex = 0;
        lbl_Error.Text = "";
        BindGrid("form", e);

    }


    private void calculate_totals()
    {
        DataRow dr = ds.Tables[3].Rows[0];
        Total_SubTotal = Util.String2Decimal(dr["TotalSubTotal"].ToString());
        Total_GST = Util.String2Decimal(dr["TotalGST"].ToString());
        Total_iGST = Util.String2Decimal(dr["TotaliGST"].ToString());
        Total_CGST = Util.String2Decimal(dr["TotalCGST"].ToString());
        Total_SGST = Util.String2Decimal(dr["TotalSGST"].ToString());


        Total_SubTotal1 = Util.String2Decimal(dr["TotalSubTotal"].ToString());
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



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_RPT_GSTR1_B2C_StateWise", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[4].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[2], CallFrom, lbl_Error);

        objcommon.ValidateReportForm(dg_Grid1, ds.Tables[0], CallFrom, lbl_Error);



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
        DataRow dr = ds.Tables[2].NewRow();
        dr["LR_No"] = "Total : ";
        dr["SubTotal"] = Total_SubTotal;
        dr["TotalGST"] = Total_GST;
        dr["iGST"] = Total_iGST;
        dr["CGST"] = Total_CGST;
        dr["SGST"] = Total_SGST;

        ds.Tables[2].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[2];

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


    protected void btn_ExportToExcel_Click(object sender, EventArgs e)
    {
        BindGrid("form", e);

        DataRow dr = ds.Tables[0].NewRow();
        dr["SupplyType"] = "Total : ";
        dr["SubTotal"] = Total_SubTotal1;

        ds.Tables[0].Rows.Add(dr);

        Session["ExportToExcel"] = ds.Tables[0].Copy(); 

        Response.Redirect("~/Finance/Utilities/frm_Infra_Grid_Common_ExportToExcel.aspx");
    }


    #endregion


}
