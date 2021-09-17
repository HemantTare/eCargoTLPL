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


public partial class Operations_Delivery_FrmDDCTempoFrgtDetails : System.Web.UI.Page
{ 
    public int Document_ID 
    {
        get { return Convert.ToInt32(ViewState["_Document_ID"]); }
        set { ViewState["_Document_ID"] = Util.Int2String(value); }
    }
    public string Document_Type
    {
        get { return Convert.ToString(ViewState["_Document_Type"]); }
        set { ViewState["_Document_Type"] = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (IsPostBack == false)
        {
            Document_Type = Request.QueryString["Document_Type"]; 
            Document_ID = Util.DecryptToInt(Request.QueryString["Document_ID"]);  

            if (Document_Type == "DDC")
            {
                updpnl_DDCTempoFrgt.Visible = true;
                updpnl_PDSTempoFrgt.Visible = false;
                BindGridDDC(Document_ID);
                lbl_heading.Text = "DDC Tempo Freight Details";
            }
            else if (Document_Type == "PDS")
            {
                updpnl_DDCTempoFrgt.Visible = false;
                updpnl_PDSTempoFrgt.Visible = true;
                BindGridPDS(Document_ID); 
                lbl_heading.Text = "PDS Tempo Freight Details";
            }
        }
    }

    private void BindGridDDC(int Document_ID)
    { 
        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, Document_ID)}; 
        objDAL.RunProc("dbo.EC_Opr_Get_DDC_Tempo_Freight_Details", objSqlParam, ref objDS);

        dg_DDCTempoFrgt.DataSource = objDS.Tables[0];
        dg_DDCTempoFrgt.DataBind();
    }
    private void BindGridPDS(int Document_ID)
    {
        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@PDS_Id", SqlDbType.Int, 0, Document_ID) };
        objDAL.RunProc("dbo.EC_Opr_Get_PDS_Tempo_Freight_Details", objSqlParam, ref objDS);

        dg_PDSTempoFrgt.DataSource = objDS.Tables[0];
        dg_PDSTempoFrgt.DataBind();
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
