using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;

public partial class Reports_Sales_Billing_Frm_Bill_Register_Nandwana : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();
    Boolean Export = false;

    decimal Total_LR, Sub_Total, Other_Charge, Service_Tax, Octroi_Amount, Total_Bill_Amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "Bill Register";
        Wuc_Region_Area_Branch1.SelectedLocationsOnlyVisibility = true;
        Wuc_Region_Area_Branch1.HoVisibility = true;

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid_Details);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Summary);
        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            if (rbtn_Details_Summary.SelectedValue == "0")
            {
                dg_Grid_Details.Visible = true;
                dg_Grid_Details.CurrentPageIndex = 0;
                dg_Grid_Summary.Visible = false;
                dg_Grid_Summary.CurrentPageIndex = 0;
            }
            else
            {
                dg_Grid_Details.Visible = false;
                dg_Grid_Details.CurrentPageIndex = 0;
                dg_Grid_Summary.Visible = true;
                dg_Grid_Summary.CurrentPageIndex = 0;
            }

            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid_Details.Visible = false;
            dg_Grid_Summary.Visible = false;
        }
    }

    protected void dg_Grid_Details_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Bill_ID, LR_ID;
            Label lbl_Bill_No, lbl_LR_No;

            Bill_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Bill_ID").ToString());
            LR_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "LR_ID").ToString());

            lbl_Bill_No = (Label)e.Item.FindControl("lbl_Bill_No");
            lbl_LR_No = (Label)e.Item.FindControl("lbl_LR_No");

            lbl_Bill_No.Attributes.Add("onclick", "return viewwindow_general('BILL','" + Bill_ID + "')");
            lbl_LR_No.Attributes.Add("onclick", "return viewwindow_general('GC','" + LR_ID + "')");

            if (Export == false)
            {
                lbl_Bill_No.CssClass = "LEVEL2ITEMHOVER";
                lbl_LR_No.CssClass = "LEVEL2ITEMHOVER";

                lbl_Bill_No.Font.Underline = true;
                lbl_LR_No.Font.Underline = true;

                //lbl_Bill_No.Font.Bold = true;
                //lbl_LR_No.Font.Bold = true;

                lbl_Bill_No.ForeColor = Color.Blue;
                lbl_LR_No.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_Total_LR, lbl_Sub_Total, lbl_Other_Charge, 
                  lbl_Service_Tax, lbl_Octroi_Amount, lbl_Total_Bill_Amount;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_Total_LR = (Label)e.Item.FindControl("lbl_Total_LR");
            lbl_Sub_Total = (Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Other_Charge = (Label)e.Item.FindControl("lbl_Other_Charge");
            lbl_Service_Tax = (Label)e.Item.FindControl("lbl_Service_Tax");
            lbl_Octroi_Amount = (Label)e.Item.FindControl("lbl_Octroi_Amount");
            lbl_Total_Bill_Amount = (Label)e.Item.FindControl("lbl_Total_Bill_Amount");

            lbl_Total.Text = "Total";
            lbl_Total_LR.Text = Total_LR.ToString();
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Other_Charge.Text = Other_Charge.ToString();
            lbl_Service_Tax.Text = Service_Tax.ToString();
            lbl_Octroi_Amount.Text = Octroi_Amount.ToString();
            lbl_Total_Bill_Amount.Text = Total_Bill_Amount.ToString();
        }           
    }
    
    protected void dg_Grid_Summary_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Summary.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Summary_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int Bill_ID;
            Label lbl_Bill_No;

            Bill_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Bill_ID").ToString());

            lbl_Bill_No = (Label)e.Item.FindControl("lbl_Bill_No");

            lbl_Bill_No.Attributes.Add("onclick", "return viewwindow_general('BILL','" + Bill_ID + "')");

            if (Export == false)
            {
                lbl_Bill_No.CssClass = "LEVEL2ITEMHOVER";
                lbl_Bill_No.Font.Underline = true;
                //lbl_Bill_No.Font.Bold = true;
                lbl_Bill_No.ForeColor = Color.Blue;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_Total_LR, lbl_Sub_Total, lbl_Other_Charge, 
                  lbl_Service_Tax, lbl_Octroi_Amount, lbl_Total_Bill_Amount;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_Total_LR = (Label)e.Item.FindControl("lbl_Total_LR");
            lbl_Sub_Total = (Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Other_Charge = (Label)e.Item.FindControl("lbl_Other_Charge");
            lbl_Service_Tax = (Label)e.Item.FindControl("lbl_Service_Tax");
            lbl_Octroi_Amount = (Label)e.Item.FindControl("lbl_Octroi_Amount");
            lbl_Total_Bill_Amount = (Label)e.Item.FindControl("lbl_Total_Bill_Amount");

            lbl_Total.Text = "Total";
            lbl_Total_LR.Text = Total_LR.ToString();
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Other_Charge.Text = Other_Charge.ToString();
            lbl_Service_Tax.Text = Service_Tax.ToString();
            lbl_Octroi_Amount.Text = Octroi_Amount.ToString();
            lbl_Total_Bill_Amount.Text = Total_Bill_Amount.ToString();
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex;
        int grid_PageSize;

        if (rbtn_Details_Summary.SelectedValue == "0")
        {
            grid_currentpageindex = dg_Grid_Details.CurrentPageIndex;
            grid_PageSize = dg_Grid_Details.PageSize;
        }
        else
        {
            grid_currentpageindex = dg_Grid_Summary.CurrentPageIndex;
            grid_PageSize = dg_Grid_Summary.PageSize;
        }

        string Hierarchy_Code;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        if (Wuc_Region_Area_Branch1.IsHo == true)
        {
            Hierarchy_Code = "HO";
        }
        else if (Wuc_Region_Area_Branch1.Selected_Region_Only == true)
        {
            Hierarchy_Code = "RO";
        }
        else if (Wuc_Region_Area_Branch1.Selected_Area_Only == true)
        {
            Hierarchy_Code = "AO";
        }
        else
        {
            Hierarchy_Code = "BO";
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,0,Hierarchy_Code),
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),         
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        if (rbtn_Details_Summary.SelectedValue == "0")
        {
            objDAL.RunProc("[EC_RPT_BILL_REGISTER_DETAILS_Nandwana]", objSqlParam, ref ds);
            dg_Grid_Details.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        }
        else
        {
            objDAL.RunProc("[EC_RPT_BILL_REGISTER_SUMMARY_Nandwana]", objSqlParam, ref ds);
            dg_Grid_Summary.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        }

        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();

        if (rbtn_Details_Summary.SelectedValue == "0")
        {
            objcommon.ValidateReportForm(dg_Grid_Details, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);
        }
        else
        {
            objcommon.ValidateReportForm(dg_Grid_Summary, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        Total_LR = Util.String2Decimal(dr["Total LR"].ToString());
        Sub_Total = Util.String2Decimal(dr["Sub Total"].ToString());
        Other_Charge = Util.String2Decimal(dr["Other Charge"].ToString());
        Service_Tax = Util.String2Decimal(dr["Service Tax"].ToString());
        Octroi_Amount = Util.String2Decimal(dr["Octroi Amount"].ToString());
        Total_Bill_Amount = Util.String2Decimal(dr["Total Bill Amount"].ToString());
    }

    private void PrepareDTForExportToExcel()
    {
        if (rbtn_Details_Summary.SelectedValue == "0")
        {
            ds.Tables[0].Columns.Remove("Bill_ID");
            ds.Tables[0].Columns.Remove("LR_ID");
        }
        else
        {
            ds.Tables[0].Columns.Remove("Bill_ID");
        }
        ds.AcceptChanges();

        DataRow dr;
        dr = ds.Tables[0].NewRow();

        dr["Bill No"] = "Total";
        dr["Total LR"] = Total_LR;
        dr["Sub Total"] = Sub_Total;
        dr["Other Charge"] = Other_Charge;
        dr["Service Tax"] = Service_Tax;
        dr["Octroi Amount"] = Octroi_Amount;
        dr["Total Bill Amount"] = Total_Bill_Amount;

        ds.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
