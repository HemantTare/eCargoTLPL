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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using Raj.EC;


public partial class Operations_Delivery_FrmClosingStkRptDetails : System.Web.UI.Page
{
    public int SrNo
    {
        get { return Convert.ToInt32(ViewState["_SrNo"]); }
        set { ViewState["_SrNo"] = Util.Int2String(value); }
    }
    public int Branch_ID 
    {
        get { return Convert.ToInt32(ViewState["_Branch_ID"]); }
        set { ViewState["_Branch_ID"] = Util.Int2String(value); }
    }
    public string AsOnDate
    {
        get { return Convert.ToString(ViewState["_AsOnDate"]); }
        set { ViewState["_AsOnDate"] = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (IsPostBack == false)
        {
            SrNo = Util.DecryptToInt(Request.QueryString["SrNo"]);
            Branch_ID = Util.DecryptToInt(Request.QueryString["Branch_ID"]);
            AsOnDate = Request.QueryString["AsOnDate"]; 
             
            lbl_heading.Text = "Closing Stock Report Details";
            BindGrid();
        }
    }

    private void BindGrid()
    { 
        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@SrNo", SqlDbType.Int, 0, SrNo),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, Branch_ID),
            objDAL.MakeInParams("@As_on_Date", SqlDbType.DateTime, 0, Convert.ToDateTime(AsOnDate))};
        objDAL.RunProc("dbo.EC_RPT_ClosingStock_Details_Report", objSqlParam, ref objDS);

        dg_CloseStkDtls.DataSource = objDS.Tables[0];
        dg_CloseStkDtls.DataBind();
    } 

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
