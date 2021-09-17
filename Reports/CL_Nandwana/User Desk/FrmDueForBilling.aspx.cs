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


public partial class Reports_CL_Nandwana_UserDesk_FrmDueForBilling : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal NoOfClient, NoOfLR,TotalFreight; 


    #endregion

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Details);
            BindGrid("form_and_pageload", e);

        }
    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            Label lbl_NoOfLR, lbl_TotalFreight;

            lbl_NoOfLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOfLR");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");

            lbl_NoOfLR.Text = NoOfLR.ToString();
            lbl_TotalFreight.Text = TotalFreight.ToString();
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
            int Client_Id;
            LinkButton lbtn_ClientName;

            Client_Id  = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_Id").ToString());

            lbtn_ClientName = (LinkButton)e.Item.FindControl("lbtn_ClientName");


            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmDueForBillingDetails.aspx?Client_Id=" + Client_Id + "&Client_Name=" + lbtn_ClientName.Text);
            lbtn_ClientName.Attributes.Add("onclick", "return DueForBIllingDetails('" + PathF4 + "')");
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

       

        objDAL.RunProc("COM_UserDesk_Get_Due_For_Billing", ref ds);

        calculate_totals();

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error);

        ClearVariables();
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        NoOfClient = Util.String2Decimal(dr["NoOfClient"].ToString());
        NoOfLR = Util.String2Decimal(dr["NoOfLR"].ToString());
        TotalFreight = Util.String2Decimal(dr["TotalFreight"].ToString());

    }

    public void ClearVariables()
    {
        ds = null;
    }
}
