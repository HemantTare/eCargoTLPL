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

public partial class Finance_Reports_FrmFrenchMthlyStmentMemoWise : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    public int Memo_ID
    {
        get { return Convert.ToInt32(ViewState["_Memo_ID"]); }
        set { ViewState["_Memo_ID"] = value; }
    } 
    public string MemoNo
    {
        get { return Convert.ToString(ViewState["_MemoNo"]); }
        set { ViewState["_MemoNo"] = value; }
    }
    public string Discount
    {
        get { return Convert.ToString(ViewState["_Discount"]); }
        set { ViewState["_Discount"] = value; }
    }

    string Crypt,TotalRecords, Consignor_Name;
    int TotalFrt;
    decimal DiscountAmt;


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);


        Wuc_Export_To_Excel1.FileName = "FrenchiseeMthlyStmentLRWise";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["Memo_ID"];
            Memo_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);  

            Crypt = Request.QueryString["MemoNo"];
            MemoNo = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["Discount"];
            Discount = ClassLibraryMVP.Util.DecryptToString(Crypt); 

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form", e);
             
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

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,0), 
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,UserManager.getUserParam().StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,UserManager.getUserParam().EndDate), 
            objDAL.MakeInParams("@Memo_ID",SqlDbType.Int,0,Memo_ID) 
             
        };
        objDAL.RunProc("EC_RPT_FrenchiseeMonthlyStatement_GRD", objSqlParam, ref ds); 


        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["Consignee_Name"] = "Total"; 
        dr["TotalFrt"] = TotalFrt; 
        dr["DiscountAmt"] = DiscountAmt; 

        ds.Tables[0].Rows.Add(dr); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        TotalFrt = Util.String2Int(dr["TotalFrt"].ToString());
        DiscountAmt = Util.String2Decimal(dr["TotalDiscountAmt"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Tot_Frt, lbl_TotDiscount;  
             
            lbl_Tot_Frt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Tot_Frt"); 
            lbl_TotDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotDiscount"); 

            lbl_Tot_Frt.Text = TotalFrt.ToString();
            lbl_TotDiscount.Text = DiscountAmt.ToString(); 
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
