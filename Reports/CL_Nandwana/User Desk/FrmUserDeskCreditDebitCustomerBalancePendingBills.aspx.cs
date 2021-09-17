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



public partial class Reports_CL_Nandwana_UserDesk_FrmUserDeskCreditDebitCustomerBalancePendingBills : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Total_Freight; 
    #endregion

    #region EventClick



    public int Client_Id
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["ClientId"]);
        }
    }

    public int Ledger_Id
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Ledger_Id"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "PendingBills";

        if (IsPostBack == false)
        {

            lbl_ClientName.Text = Request.QueryString["ClientName"];

            Common objcommon = new Common();

            BindGrid("form_and_pageload", e);
            
        }
             
         
    }
 
    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
        }
    }


    

    #endregion

    #region Other Function

 
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Freight = Util.String2Decimal(dr["Total"].ToString());

    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,Client_Id),
            objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int, 0,Ledger_Id)
        };

        objDAL.RunProc("FA_Opr_CreditDebit_Customer_Balance_Bill_Details", objSqlParam, ref ds);
       
        calculate_totals();

        Common objcommon = new Common();
        
        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["Bill_No"] = "Total";
        dr["Balance"] = Total_Freight;

        ds.Tables[0].Rows.Add(dr);

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
