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


public partial class Finance_Reports_Frm_Cheque_Deposited_In_Bank_Details : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
  
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
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "ChequeDepositedDetails";

        if (rdl_DepositedBy.SelectedValue == "1")
        {
            td_Branch1.Style.Add("display", "none");
            td_Branch2.Style.Add("display", "none");
        }
        else
        {
            td_Branch1.Style.Add("display", "block");
            td_Branch2.Style.Add("display", "block");
        }

        if (UserManager.getUserParam().HierarchyCode == "BO")
        {

            ddlBranch.DataTextField = "Branch_Name";
            ddlBranch.DataValueField = "Branch_ID";
            Raj.EC.Common.SetValueToDDLSearch(UserManager.getUserParam().MainName, UserManager.getUserParam().MainId.ToString(), ddlBranch);

            rdl_DepositedBy.Enabled = false;
            ddlBranch.Enabled = false;

        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            LinkButton lnk_VoucherNo;

            lnk_VoucherNo = (LinkButton)e.Item.FindControl("lnk_VoucherNo");

            lnk_VoucherNo.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");

        }
    }

    #endregion

    #region Other Function

    protected void btn_view_Click(object sender, EventArgs e)
    {
        BindGrid("form", e);
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

        string DepositedBy;

        if (rdl_DepositedBy.SelectedValue == "1")
        {
            DepositedBy = "HO";
        }
        else
        {
            DepositedBy = ddlBranch.SelectedText;
        }

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@BranchName", SqlDbType.VarChar , 50,DepositedBy),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("FA_Rpt_Cheque_Deposited_In_Bank_Details", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        lbl_Error.Text = ds.Tables[1].Rows[0][0].ToString();
    
        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (Util.String2Int(ds.Tables[1].Rows[0][0].ToString()) > 0)
        {
            lbl_Error.Text =  ds.Tables[1].Rows[0][0].ToString() + " Record(s) Found";
        }

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

        ds.Tables[0].Columns.Remove("Voucher_Id");

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
