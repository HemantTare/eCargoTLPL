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

public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_Details : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Total_Gc, TotalFreight;
    decimal Basic_Freight, Total_Freight, Invoice_Value;
    string ToPayTotal, PaidTotal, TBBTotal;
   
   
    public int Delivery_branch_Id  
    {
        get
        {
            return Convert.ToInt32(ViewState["_Delivery_branch_Id"]);
        }
        set
        {
            ViewState["_Delivery_branch_Id"] = value;
        }
    }
    public string DlyBranch  
    {
        get
        {
            return Convert.ToString(ViewState["_DlyBranch"]);
        }
        set
        {
            ViewState["_DlyBranch"] = value;
            txtlblDlyBranch.Text = value;

        }
    }

    public int DeliveryArea_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_DeliveryArea_Id"]);
        }
        set
        {
            ViewState["_DeliveryArea_Id"] = value;
        }
    }

    public int  IsOld
    {
        get
        {
            return Convert.ToInt32(ViewState["_IsOld"]);
        }
        set
        {
            ViewState["_IsOld"] = value;
        }
    }

    public Boolean IsDetailed
    {
        get
        {
            return Convert.ToBoolean(ViewState["_IsDetailed"]);
        }
        set
        {
            ViewState["_IsDetailed"] = value;
        }
    }

    public int AllLR
    {
        get
        {
            return Convert.ToInt32(ViewState["_AllLR"]);
        }
        set
        {
            ViewState["_AllLR"] = value;
        }
    }


#endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DeliveryStockList_Summary";

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Delivery_branch_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Delivery_branch_Id"]);
            DlyBranch = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["DlyBranch"]);

            DeliveryArea_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["DeliveryArea_Id"]);

            IsOld = Convert.ToInt32(Request.QueryString["IsOld"]);
            IsDetailed = Convert.ToBoolean(Request.QueryString["IsDetailed"]);

            AllLR = Convert.ToInt32(Request.QueryString["AllLR"]);


            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            BindGrid("form_and_pageload", e);

            
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
         
        
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
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
            Label lbl_Articles, lbl_Total_Gc;
            Label lbl_TotalFreight;

            lbl_Articles = (Label)e.Item.FindControl("lbl_Articles");
            lbl_TotalFreight = (Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_Total_Gc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Gc");

            lbl_Total_Gc.Text = Total_Gc.ToString();
            
            lbl_Articles.Text = Articles.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID, Consignee_Id, Reason_Id,ReasonCount;
            LinkButton lnk_GC_No, lnk_Consignee, lnk_Undeliver_Reason, lnk_ReasonCount;
            bool Is_Consignee_Regular;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            Consignee_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Id").ToString());
            Is_Consignee_Regular = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular").ToString());

            Reason_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Reason_ID").ToString());

            ReasonCount = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "ReasonCount").ToString());


            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_Consignee = (LinkButton)e.Item.FindControl("lnk_Consignee");

            lnk_Undeliver_Reason = (LinkButton)e.Item.FindControl("lnk_Undeliver_Reason");
            lnk_ReasonCount = (LinkButton)e.Item.FindControl("lnk_ReasonCount");

            StringBuilder PathGCID = new StringBuilder(Util.GetBaseURL());
            PathGCID.Append("/");
            PathGCID.Append("Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=" + ClassLibraryMVP.Util.EncryptInteger(GC_ID));

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + PathGCID + "')");


            lnk_Consignee.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Id) + "','" + Is_Consignee_Regular + "')");

            lnk_Undeliver_Reason.Attributes.Add("onclick", "return viewwindow_UndeliverReason('" + GC_ID + "','" + lnk_GC_No.Text + "','" + Reason_Id + "')");


            if (ReasonCount > 0)
            {
                lnk_ReasonCount.Attributes.Add("onclick", "return viewwindow_ReasonCount('" + GC_ID +"','" + lnk_GC_No.Text + "')");
            }
            else
            {
                lnk_ReasonCount.Visible = false; 
            }

            string Chk_AddressUpdated = DataBinder.Eval(e.Item.DataItem, "IsAddressUpdated").ToString();

            if (Chk_AddressUpdated == "Yes")
            {
                e.Item.BackColor = System.Drawing.Color.Lime;
            }

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


        int Branch_id = UserManager.getUserParam().MainId; 
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Delivery_branch_Id),
            objDAL.MakeInParams("@DeliveryArea_Id", SqlDbType.Int, 0,DeliveryArea_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),  
            objDAL.MakeInParams("@IsDetailed",SqlDbType.Bit,0,IsDetailed),
            objDAL.MakeInParams("@IsOld",SqlDbType.Int,0,IsOld),
            objDAL.MakeInParams("@AllLR",SqlDbType.Int,0,AllLR)
        };

        if (IsOld == 3 || IsOld == 7 || IsOld == 11)
        {
            objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock_More_Than_3_Months", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("EC_Opr_Dly_BranchWise_Pending_Stock", objSqlParam, ref ds);
        }

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

        dg_Grid.Columns[9].Visible = false;

        if (IsOld == 0)
        {
            dg_Grid.Columns[10].Visible = false;
            dg_Grid.Columns[11].Visible = false;
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

        //DataRow dr1 = ds.Tables[0].NewRow();
        //dr1["gc_caption Date"] = "Total";
        //dr1["Cnee Name"] = ToPayTotal;
        //dr1["Cnr Name"] = PaidTotal;
        //dr1["Pay Mode"] = TBBTotal; 
        //ds.Tables[0].Rows.Add(dr1);


        ds.Tables[0].Columns.Remove("SrNo");
        ds.Tables[0].Columns.Remove("lr_caption Date");
        ds.Tables[0].Columns.Remove("GC_ID");
        ds.Tables[0].Columns.Remove("DeliveryAreaID");
        ds.Tables[0].Columns.Remove("Delivery_branch_Id");
        ds.Tables[0].Columns.Remove("Charged Weight");
        ds.Tables[0].Columns.Remove("Actual Weight");
        ds.Tables[0].Columns.Remove("Basic Freight");
        ds.Tables[0].Columns.Remove("Invoice Value");

        ds.Tables[0].Columns.Remove("Consignee_Id");
        ds.Tables[0].Columns.Remove("Is_Consignee_Regular");

        ds.Tables[0].Columns.Remove("Reason_Id");
        ds.Tables[0].Columns.Remove("Reason"); 

        if (IsOld == 0)
        {
            ds.Tables[0].Columns.Remove("InStockReason");
            ds.Tables[0].Columns.Remove("ReasonCount"); 
        }

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
