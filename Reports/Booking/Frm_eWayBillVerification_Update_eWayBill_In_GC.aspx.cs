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
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Reports_Booking_Frm_eWayBillVerification_Update_eWayBill_In_GC : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;



    public int GC_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_GC_Id"]);
        }
        set
        {
            ViewState["_GC_Id"] = value;
        }
    }
    public string GC_No
    {
        get
        {
            return Convert.ToString(ViewState["_GC_No"]);
        }
        set
        {
            ViewState["_GC_No"] = value;
            lbl_GCNo.Text = value;

        }
    }

    public string eWayBillNo
    {
        get
        {
            return Convert.ToString(ViewState["_eWayBillNo"]);
        }
        set
        {
            ViewState["_eWayBillNo"] = value;
            lbl_eWayBillNo.Text = value;

        }
    }

    
    public string New_eWayBillNo
    {
        set { txt_New_eWayBillNo.Text = value; }
        get { return txt_New_eWayBillNo.Text; }
    }
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {

            GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"]);
            GC_No = Request.QueryString["GC_No"];
            eWayBillNo = Request.QueryString["eWayBillNo"];

        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();


        }
    }

    #endregion

    #region Other Function


    
    private Message Save()
    {

   
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, GC_Id), 
            objDAL.MakeInParams("@eWayBillNo", SqlDbType.VarChar ,20, eWayBillNo), 
            objDAL.MakeInParams("@New_eWayBillNo",SqlDbType.VarChar,20,New_eWayBillNo ) 
        };

        objDAL.RunProc("dbo.EC_Opr_eWayBill_Update_eWayBillNo_In_GC_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";

            ClearVariables();
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString("Saved Succesfully"));

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (txt_New_eWayBillNo.Text.Trim().Length != 12 && txt_New_eWayBillNo.Text.Trim().Length != 10)
        {
            lblErrors.Text = "Please Enter New eWayBill No.";
            txt_New_eWayBillNo.Focus();
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    public void ClearVariables()
    {
        ds = null;
    }

    #endregion
}
