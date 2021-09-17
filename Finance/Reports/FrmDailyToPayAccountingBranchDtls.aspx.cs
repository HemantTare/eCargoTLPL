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

public partial class Finance_Reports_FrmDailyToPayAccountingBranchDtls : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    public int Region_Id
    {
        get { return Convert.ToInt32(ViewState["_Region_Id"]); }
        set { ViewState["_Region_Id"] = value; }
    }
    public int Area_Id
    {
        get { return Convert.ToInt32(ViewState["_Area_Id"]); }
        set { ViewState["_Area_Id"] = value; }
    }
    public int Branch_ID
    {
        get { return Convert.ToInt32(ViewState["_Branch_ID"]); }
        set { ViewState["_Branch_ID"] = value; }
    }
    public int Dest_Branch_ID
    {
        get { return Convert.ToInt32(ViewState["_Dest_Branch_ID"]); }
        set { ViewState["_Dest_Branch_ID"] = value; }
    }
    public string DirectToPay
    {
        get { return Convert.ToString(ViewState["_DirectToPay"]); }
        set { ViewState["_DirectToPay"] = value; }
    }
    public string AsOnDate
    {
        get { return Convert.ToString(ViewState["_AsOnDate"]); }
        set { ViewState["_AsOnDate"] = value; }
    }
    public string RegionText
    {
        get { return Convert.ToString(ViewState["_RegionText"]); }
        set 
        {
            txtRegion.Text = value;
            ViewState["_RegionText"] = value; 
        }
    }
    public string AreaText
    {
        get { return Convert.ToString(ViewState["_AreaText"]); }
        set 
        {
            txtArea.Text = value;
            ViewState["_AreaText"] = value; 
        }
    }
    public string BranchText
    {
        get { return Convert.ToString(ViewState["_BranchText"]); }
        set 
        {
            txtBranch.Text = value;
            ViewState["_BranchText"] = value; 
        }
    }

    string Crypt,TotalRecords;
   
    int TotalFrt;


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DailyToPayAccountingDlyBranchDtls";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["Region_Id"];
            Region_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Area_Id"];
            Area_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Branch_ID"];
            Branch_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Dest_Branch_ID"];
            Dest_Branch_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["DirectToPay"];
            DirectToPay = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["RegionText"];
            RegionText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["AreaText"];
            AreaText = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["BranchText"];
            BranchText = ClassLibraryMVP.Util.DecryptToString(Crypt);
            
            AsOnDate = Request.QueryString["AsOnDate"];
            txtAsOnDate.Text = Convert.ToDateTime(AsOnDate).ToString("dd MMM yyyy");

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
            objDAL.MakeInParams("@Region_Id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int,0,Area_Id),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int,0,Branch_ID), 
            objDAL.MakeInParams("@DlyBranch_Id", SqlDbType.Int,0,Dest_Branch_ID), 
            objDAL.MakeInParams("@AsOnDate",SqlDbType.DateTime,0,Convert.ToDateTime(AsOnDate).ToString("dd MMM yyyy")) 
             
        };
        objDAL.RunProc("EC_RPT_Daily_To_Pay_Accounting_DlyBranchwise_GRD", objSqlParam, ref ds);


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
        //dr["DiscountAmt"] = DiscountAmt; 

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
        //DiscountAmt = Util.String2Decimal(dr["TotalDiscountAmt"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Tot_Frt;//, lbl_TotDiscount  
             
            lbl_Tot_Frt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Tot_Frt"); 
            //lbl_TotDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotDiscount"); 

            lbl_Tot_Frt.Text = TotalFrt.ToString();
            //lbl_TotDiscount.Text = DiscountAmt.ToString(); 
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
