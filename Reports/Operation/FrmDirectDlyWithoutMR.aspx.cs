using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryMVP.Security;
using ClassLibraryMVP;

//Author : Sushant K
//Desc   : Reports_Operation_FrmDirectDlyWithoutMR    
//Date   : 03-01-09

public partial class Reports_Operation_FrmDirectDlyWithoutMR : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    int ds_index, i;
    decimal Articles, Sub_Total, Basic_Freight, STax_Amt, Total_Freight, GC_Amount, Discount;
    int NoOf_GC;
    string Mode = "0";
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DirectDlyWithoutMR";
        Wuc_Region_Area_Branch1.SetRegionCaption = "Region";
        Wuc_Region_Area_Branch1.SetAreaCaption = "Area";
        Wuc_Region_Area_Branch1.SetBranchCaption = "Branch";

        if (IsPostBack == false)
        { 

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            //BindGrid("form_and_pageload", e); 
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = Convert.ToDateTime("01 Apr 2019");

            if (UserManager.getUserParam().EndDate  <= DateTime.Now)
            {
                Wuc_From_To_Datepicker1.SelectedToDate = UserManager.getUserParam().EndDate;
            }

            BindGrid("form", e);
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
        //    lbl_Error.Text = "";
        //    dg_Grid.Visible = true;
        //    dg_Grid.CurrentPageIndex = 0;
        //    BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}

        lbl_Error.Text = "";
        dg_Grid.Visible = true;
        dg_Grid.CurrentPageIndex = 0;
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

        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Region_id = Wuc_Region_Area_Branch1.RegionID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        
            SqlParameter[] objSqlParam ={
            objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@AreaID", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@RegionID", SqlDbType.Int,0,Region_id),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Todate ", SqlDbType.DateTime,0,To_date) 
        };

            objDAL.RunProc("EC_RPT_DirectDlyWithoutMR_GRD", objSqlParam, ref ds);

            if (CallFrom == "form_and_pageload") return;

            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[0].Rows[0][0].ToString();
            //calculate_totals();

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[1], CallFrom, lbl_Error, TotalRecords);

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            } 
    }

    private void PrepareDTForExportToExcel()
    {

        DataTable dt = new DataTable();
        dt = ds.Tables[1].Copy();

        dt.Columns.Remove("Memo_Id");
        dt.Columns.Remove("LHPO_Id");
        dt.Columns.Remove("GC_Id");
        dt.Columns.Remove("Payment_Type_Id");
        dt.Columns.Remove("From_Service_Location_ID");
        dt.Columns.Remove("To_Service_Location_ID");
        dt.Columns.Remove("Memo_Branch_Id");

        dt.Columns.Remove("Vehicle_ID");
        dt.Columns.Remove("Memo_Date");
        dt.Columns.Remove("MemoFromBranch_Name");
        dt.Columns.Remove("MemoTo_Name");
        dt.Columns.Remove("LHPO_No_For_Print");

        dt.Columns["GC_Date"].ColumnName = "LRDate";
        dt.Columns["GC_No_For_Print"].ColumnName = "LRNo";
        dt.Columns["Memo_No_For_Print"].ColumnName = "InvNo";
        dt.Columns["FromServiceLocation"].ColumnName = "From";
        dt.Columns["ToServiceLocation"].ColumnName = "To";
        dt.Columns["Payment_Type"].ColumnName = "PayType";
        dt.Columns["Total_Articles"].ColumnName = "Qty";
        dt.Columns["Total_GC_Amount"].ColumnName = "Frt";


        Wuc_Export_To_Excel1.SessionExporttoExcel = dt;
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        //DataRow dr = ds.Tables[1].Rows[0];
        //NoOf_GC = Util.String2Int(dr["Total_NoOf_GC"].ToString()); 
        //Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        //Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        //Sub_Total = Util.String2Decimal(dr["Total_Sub_Total"].ToString());
        //STax_Amt = Util.String2Decimal(dr["Total_STax"].ToString());
        //GC_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
        //Discount = Util.String2Decimal(dr["Total_Discount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
      
        if ((e.Item.Cells[8].Text) == "Total :")
        {
            e.Item.BackColor = System.Drawing.Color.Green;
            e.Item.ForeColor = System.Drawing.Color.WhiteSmoke;
            e.Item.Font.Bold = true;
        }
        if (e.Item.ItemIndex != -1)
        {

            LinkButton lbtn_GC_No_For_Print;
            HiddenField hdn_Memo_Id,hdn_LHPO_Id, hdn_GC_Id,hdn_Payment_Type_Id;
            HiddenField hdn_From_Service_Location_ID, hdn_To_Service_Location_ID,hdn_FromServiceLocation, hdn_ToServiceLocation ;
            HiddenField hdn_Vehicle_ID,hdn_Vehicle_No, hdn_GC_Date;
            HiddenField hdn_Memo_Date, hdn_Memo_No_For_Print, hdn_Memo_Branch_Id;
            HiddenField hdn_MemoFromBranch_Name, hdn_MemoTo_Name, hdn_LHPO_NO_For_Print, hdn_Total_Articles;
            HiddenField hdn_Total_GC_Amount, hdn_Payment_Type;  

            lbtn_GC_No_For_Print = (LinkButton)e.Item.FindControl("lbtn_GC_No_For_Print");

            ds_index = e.Item.ItemIndex;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lbtn_GC_No_For_Print = (LinkButton)e.Item.FindControl("lbtn_GC_No_For_Print");
                hdn_LHPO_Id = (HiddenField)e.Item.FindControl("hdn_LHPO_Id");
                hdn_Memo_Id = (HiddenField)e.Item.FindControl("hdn_Memo_Id");
                hdn_GC_Id = (HiddenField)e.Item.FindControl("hdn_GC_Id");
                hdn_Payment_Type_Id = (HiddenField)e.Item.FindControl("hdn_Payment_Type_Id");
                hdn_From_Service_Location_ID = (HiddenField)e.Item.FindControl("hdn_From_Service_Location_ID");
                hdn_To_Service_Location_ID = (HiddenField)e.Item.FindControl("hdn_To_Service_Location_ID");
                hdn_FromServiceLocation = (HiddenField)e.Item.FindControl("hdn_FromServiceLocation");
                hdn_ToServiceLocation = (HiddenField)e.Item.FindControl("hdn_ToServiceLocation");
                hdn_Vehicle_ID = (HiddenField)e.Item.FindControl("hdn_Vehicle_ID");
                hdn_Vehicle_No = (HiddenField)e.Item.FindControl("hdn_Vehicle_No");
                hdn_GC_Date = (HiddenField)e.Item.FindControl("hdn_GC_Date");
                hdn_Memo_Date = (HiddenField)e.Item.FindControl("hdn_Memo_Date");
                hdn_Memo_No_For_Print = (HiddenField)e.Item.FindControl("hdn_Memo_No_For_Print");
                hdn_Memo_Branch_Id = (HiddenField)e.Item.FindControl("hdn_Memo_Branch_Id");
                hdn_MemoFromBranch_Name = (HiddenField)e.Item.FindControl("hdn_MemoFromBranch_Name");
                hdn_MemoTo_Name = (HiddenField)e.Item.FindControl("hdn_MemoTo_Name");
                hdn_LHPO_NO_For_Print = (HiddenField)e.Item.FindControl("hdn_LHPO_NO_For_Print");
                hdn_Total_Articles = (HiddenField)e.Item.FindControl("hdn_Total_Articles");
                hdn_Total_GC_Amount = (HiddenField)e.Item.FindControl("hdn_Total_GC_Amount");
                hdn_Payment_Type = (HiddenField)e.Item.FindControl("hdn_Payment_Type");

                if (hdn_Payment_Type_Id.Value == "1")
                {

                    int BranchID = Wuc_Region_Area_Branch1.BranchID;
                    string BranchName = Wuc_Region_Area_Branch1.SelectedBranchText;

                    StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                    Path.Append("/");
                    Path.Append("Reports/Operation/frmDDlyWOMRDtls.aspx?ds_index=" + ds_index + "&BranchID=" + BranchID + "&BranchName=" + BranchName + "&_menuItemid=5237" + "&Mode=1");


                    if ((Rights.GetObject().getForm_Rights(5237).canAdd()) == true)
                    {
                        lbtn_GC_No_For_Print.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "',"
                            + lbtn_GC_No_For_Print.ClientID + ","
                            + hdn_GC_Id.ClientID + ","
                            + hdn_LHPO_Id.ClientID + ","
                            + hdn_Memo_Id.ClientID + ","
                            + hdn_Payment_Type_Id.ClientID + ","
                            + hdn_From_Service_Location_ID.ClientID + ","
                            + hdn_To_Service_Location_ID.ClientID + ","
                            + hdn_FromServiceLocation.ClientID + ","
                            + hdn_ToServiceLocation.ClientID + ","
                            + hdn_Vehicle_ID.ClientID + ","
                            + hdn_Vehicle_No.ClientID + ","
                            + hdn_GC_Date.ClientID + ","
                            + hdn_Memo_Date.ClientID + ","
                            + hdn_Memo_No_For_Print.ClientID + ","
                            + hdn_Memo_Branch_Id.ClientID + ","
                            + hdn_MemoFromBranch_Name.ClientID + ","
                            + hdn_MemoTo_Name.ClientID + ","
                            + hdn_LHPO_NO_For_Print.ClientID + ","
                            + hdn_Total_Articles.ClientID + ","
                            + hdn_Total_GC_Amount.ClientID + ","
                            + hdn_Payment_Type.ClientID + ")");

                    }
                    else
                    {
                        StringBuilder PathGCID = new StringBuilder(Util.GetBaseURL());
                        PathGCID.Append("/");
                        PathGCID.Append("Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=" + ClassLibraryMVP.Util.EncryptInteger(Util.String2Int(hdn_GC_Id.Value)));

                        lbtn_GC_No_For_Print.Attributes.Add("onclick", "return viewwindow_general('" + PathGCID + "')");

                    }
                }
                else 
                {
                    lbtn_GC_No_For_Print.Attributes.Remove("href");
                    lbtn_GC_No_For_Print.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                    lbtn_GC_No_For_Print.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                    if (lbtn_GC_No_For_Print.Enabled != false)
                    {
                        lbtn_GC_No_For_Print.Enabled = false;
                    }

                    if (lbtn_GC_No_For_Print.OnClientClick != null)
                    {
                        lbtn_GC_No_For_Print.OnClientClick = null;
                    } 
                }
 
            } 
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
