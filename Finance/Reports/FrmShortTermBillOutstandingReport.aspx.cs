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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Finance_Reports_FrmShortTermBillOutstandingReport : System.Web.UI.Page
{
    #region ClassVariables
    private DataSet ds;
    private DAL objDAL = new DAL();
    Common objcommon = new Common();
    Decimal Total_pending_Amount;
    int MainId;
        
    #endregion

    #region ControlsValue
    public int BranchId
    {
        get { return Util.String2Int(ddl_Branch.SelectedValue); }
        set { ddl_Branch.SelectedValue = Util.Int2String(value);}
    }
    public string convertToDrCr(object value)
    {
        if (Math.Sign(convertToDecimal(value)) == -1)
        { return Convert.ToDecimal(Math.Abs(Convert.ToDecimal(value))).ToString("0")+" " + "Dr"; }
        else { return  Convert.ToDecimal(Math.Abs(Convert.ToDecimal(value))).ToString("0")+" "+ "Cr"; }
    }
    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }
    #endregion

    #region OtherMethods
    private void FillBranch()
    {
        DataSet ds = new DataSet();
        string Query = "";
        if (MainId == 0)
        {
            Query = "select distinct 0 as Branch_Id,'All' as Branch_Name from EC_Master_Branch Union select Branch_Id, Branch_Name from EC_Master_Branch " +
                    "order by Branch_Name";
        //Where Credit_Memo_Branch_Id=" + MainId + " 
        }
        else
        {
            Query = "select Branch_ID, Branch_Name from EC_Master_Branch Where Branch_Id=" + MainId + " order by Branch_Name";

        }
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_Branch.DataSource = ds;
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        ddl_Branch.DataBind();
    }
      #endregion 

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        MainId = UserManager.getUserParam().MainId;
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "Short Term Bill Outstanding Report";


        if (IsPostBack == false)
        {
           
           if (UserManager.getUserParam().HierarchyCode == "BO")
           {
               MainId=MainId;
           }
           else
           {
                MainId = 0;
           }

            FillBranch();
            ddl_Branch.SelectedValue = Util.Int2String(MainId);      
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
        
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
            System.Web.UI.WebControls.Label lbl_PendingAmount;

            lbl_PendingAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_PendingAmount");

            if (Total_pending_Amount > 0)
            {

                lbl_PendingAmount.Text = String.Format(Math.Abs(Total_pending_Amount).ToString(), "0.00") + " " + "Cr";
            }
            else
            {
                lbl_PendingAmount.Text = String.Format(Math.Abs(Total_pending_Amount).ToString(), "0.00") + " " + "Dr";

            }

        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(WucDatePicker1.SelectedDate, ref msg)) == true)
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

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_pending_Amount = Util.String2Decimal(dr["Pending Amount"].ToString());
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
        DateTime AsOnDate = WucDatePicker1.SelectedDate;

        SqlParameter[] objSqlParam ={            
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,BranchId),
            objDAL.MakeInParams("@ClientName",SqlDbType.VarChar,100,txt_ClientName.Text),
            objDAL.MakeInParams("@AsOnDate", SqlDbType.DateTime,0,AsOnDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[dbo].[EC_RPT_ShortTermBillOutstanding_Report]", objSqlParam, ref ds);



        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);
       
        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }
    private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["Pending Amount"] = Total_pending_Amount;      
        ds.Tables[0].Rows.Add(dr);
        ds.Tables[0].Columns.Remove("Ref No");


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
