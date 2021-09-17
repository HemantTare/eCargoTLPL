using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC;
using ClassLibraryMVP.General;
using ClassLibraryMVP;

public partial class TrackNTrace_frmTNTBillingOtherChargeDetails : System.Web.UI.Page
{
    DataSet objDS = new DataSet();
    Common objCom = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        int Bill_Id, GC_Id;

        Bill_Id = Util.DecryptToInt(Request.QueryString["Bill_Id"].ToString());
        GC_Id = Util.DecryptToInt(Request.QueryString["GC_ID"].ToString());

        if (!IsPostBack)
        {
            string Query = "select GC_Other_Charge_Head,Description,Amount from dbo.FA_Opr_Bill_Other_Charges_Details billdet " +
                "inner join dbo.EC_Master_GC_Other_Charge_Head mst on billdet.GC_Other_Charge_Head_ID = mst.GC_Other_Charge_Head_ID " +
                "where bill_id = " + Bill_Id + " and GC_Id = " + GC_Id;

            objDS = objCom.EC_Common_Pass_Query(Query);
            dg_TransportOtherCharges.DataSource = objDS;
            dg_TransportOtherCharges.DataBind();
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
