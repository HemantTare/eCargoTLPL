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



public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBill_Pending_Vehicle_Update_UnVerified_eWayBills_PDS : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);
            
        }
             
         
    }
 
    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
         
    }

     
    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID;
            LinkButton lnk_GC_No;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());

            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(GC_ID) + "')");

            
        }
    }
    #endregion

    #region Other Function

 
   
    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int PDS_Id;


        string Crypt = "";

        Crypt = System.Web.HttpContext.Current.Request.QueryString["PDS_Id"];
        PDS_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);
        
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0,PDS_Id)};

        objDAL.RunProc("EC_Opr_eWayBill_Pending_Vehicle_Update_UnVerified_eWayBills_PDS", objSqlParam, ref ds);
       
        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        Common objcommon = new Common();
        
        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

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
