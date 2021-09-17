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
using Raj.EC;
using System.Data.SqlClient;
using ClassLibrary;
using ClassLibrary.UIControl;

public partial class Finance_Reports_FrmServiceTaxDetailsNew : System.Web.UI.Page
{
    decimal TotalFreight, TotalTaxAbate, TotalAmtTaxable, TotalServiceTax;
    private DataSet ds;
    Common objcommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(PrepareDTForExportToExcelBillingDetails);
        Wuc_Export_To_Excel2.btn_export_to_excel_click += new EventHandler(PrepareDTForExportToExcelPaidDetails);

        Wuc_Export_To_Excel1.FileName = "BillingDetails";
        Wuc_Export_To_Excel2.FileName = "PaidDetails";

        if (IsPostBack == false)
        {
            Session["ds"] = null;
            objcommon.SetStandardCaptionForGrid(GridBillingDetails);
            objcommon.SetStandardCaptionForGrid(GridPaidetails);
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";

            GridBillingDetails.CurrentPageIndex = 0;
            GridBillingDetails.Visible = true;
            pnlbillingdetails.Visible = true;

            GridPaidetails.CurrentPageIndex = 0;
            GridPaidetails.Visible = true;
            pnlpaiddetails.Visible = true;


            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            GridBillingDetails.Visible = false;
            pnlbillingdetails.Visible = false;

            GridPaidetails.Visible = false;
            pnlpaiddetails.Visible = false;
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
        };
        objDAL.RunProc("[EC_Rpt_Service_Tax_Details_New]", objSqlParam, ref ds);

        Session["ds"] = ds;


        calculate_totals(ds.Tables[0]);
        objcommon.ValidateReportForm(GridBillingDetails, ds.Tables[0], CallFrom, lbl_Error);

        calculate_totals(ds.Tables[1]);
        objcommon.ValidateReportForm(GridPaidetails, ds.Tables[1], CallFrom, lbl_Error);
    }


    private void calculate_totals(DataTable dt)
    {
        TotalFreight = Convert.ToDecimal(dt.Compute("SUM(Total_Freight)", "").ToString());
        TotalTaxAbate = Convert.ToDecimal(dt.Compute("SUM(Tax_Abate)", "").ToString());
        TotalAmtTaxable = Convert.ToDecimal(dt.Compute("SUM(Amt_Taxable)", "").ToString());
        TotalServiceTax = Convert.ToDecimal(dt.Compute("SUM(Service_Tax)", "").ToString());

    }

    protected void GridBillingDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        GridBillingDetails.CurrentPageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)(Session["ds"]);
        calculate_totals(ds.Tables[0]);
        objcommon.ValidateReportForm(GridBillingDetails, ds.Tables[0], "form", lbl_Error);
    }

    protected void GridBillingDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_TotalFreight,
                lbl_TotalTaxAbate, lbl_TotalAmtTaxable,
                lbl_TotalServiceTax;

            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_TotalTaxAbate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTaxAbate");
            lbl_TotalAmtTaxable = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalAmtTaxable");
            lbl_TotalServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalServiceTax");

            lbl_TotalFreight.Text = TotalFreight.ToString();
            lbl_TotalTaxAbate.Text = TotalTaxAbate.ToString();
            lbl_TotalAmtTaxable.Text = TotalAmtTaxable.ToString();
            lbl_TotalServiceTax.Text = TotalServiceTax.ToString();

        }
    }

    protected void GridPaidetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        GridPaidetails.CurrentPageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)(Session["ds"]);
        calculate_totals(ds.Tables[1]);
        objcommon.ValidateReportForm(GridPaidetails, ds.Tables[1], "form", lbl_Error);
    }

    protected void GridPaidetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_TotalFreight,
                lbl_TotalTaxAbate, lbl_TotalAmtTaxable,
                lbl_TotalServiceTax;

            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_TotalTaxAbate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTaxAbate");
            lbl_TotalAmtTaxable = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalAmtTaxable");
            lbl_TotalServiceTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalServiceTax");

            lbl_TotalFreight.Text = TotalFreight.ToString();
            lbl_TotalTaxAbate.Text = TotalTaxAbate.ToString();
            lbl_TotalAmtTaxable.Text = TotalAmtTaxable.ToString();
            lbl_TotalServiceTax.Text = TotalServiceTax.ToString();

        }
    }

    private void PrepareDTForExportToExcelBillingDetails(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)(Session["ds"]);
        DataRow dr;

        if (ds != null)
        {
            calculate_totals(ds.Tables[0]);

            dr = ds.Tables[0].NewRow();
            dr["Booking_Type"] = "Total";
            dr["Total_Freight"] = TotalFreight;
            dr["Tax_Abate"] = TotalTaxAbate;
            dr["Amt_Taxable"] = TotalAmtTaxable;
            dr["Service_Tax"] = TotalServiceTax;

            ds.Tables[0].Rows.Add(dr);
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
        }
        else
        {
            DataTable dt = new DataTable();
            Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
        }

    }

    private void PrepareDTForExportToExcelPaidDetails(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)(Session["ds"]);
        DataRow dr;
        if (ds != null)
        {
            calculate_totals(ds.Tables[1]);

            dr = ds.Tables[1].NewRow();
            dr["Booking_Type"] = "Total";
            dr["Total_Freight"] = TotalFreight;
            dr["Tax_Abate"] = TotalTaxAbate;
            dr["Amt_Taxable"] = TotalAmtTaxable;
            dr["Service_Tax"] = TotalServiceTax;

            ds.Tables[1].Rows.Add(dr);
            Wuc_Export_To_Excel2.SessionExporttoExcel = ds.Tables[1];
        }
        else
        {
            DataTable dt = new DataTable();
            Wuc_Export_To_Excel2.SessionExporttoExcel = dt;
        }
    }
}