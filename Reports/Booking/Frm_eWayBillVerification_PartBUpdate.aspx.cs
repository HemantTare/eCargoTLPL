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

public partial class Reports_Booking_Frm_eWayBillVerification_PartBUpdate : System.Web.UI.Page
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


#endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"]);
            GC_No  = Request.QueryString["GC_No"];
            eWayBillNo = Request.QueryString["eWayBillNo"];

            if (Request.QueryString["ValidUpToDate"] != "No")
            {
                dtpValidUpTo.SelectedDate = Convert.ToDateTime(Request.QueryString["ValidUpToDate"]);
            }
            else
            {
                dtpValidUpTo.SelectedDate = DateTime.Today;

            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();

            //ClearVariables();
            //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString("Saved Succesfully"));
            
        }
    }

    #endregion

    #region Other Function


    

    private Message Save()
    {


        //DAL objDAL = new DAL();

        //SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
        //    objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
        //    objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, GC_Id), 
        //    objDAL.MakeInParams("@eWayBillNo", SqlDbType.VarChar ,20, eWayBillNo), 
        //    objDAL.MakeInParams("@eWayBillValidUpTo", SqlDbType.DateTime ,0, dtpValidUpTo.SelectedDate),
        //    objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        //};

        //objDAL.RunProc("dbo.EC_Opr_eWayBill_PartBUpdate_Save", objSqlParam);

        //Message objMessage = new Message();
        //objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        //objMessage.message = Convert.ToString(objSqlParam[1].Value);


        //if (objMessage.messageID == 0)
        //{
        //    lblErrors.Text = "Saved SuccessFully";
        //}
        //else
        //{
        //    lblErrors.Text = objMessage.message;
        //}
        //return objMessage;

        Message objMessage = new Message();
        updateparentdataset();
        return objMessage;
    }

    private void updateparentdataset()
    {

        string _Msg = "Saved SuccessFully";
        String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();updateparentdataset(" + GC_No + ",'" + eWayBillNo + "','" + dtpValidUpTo.SelectedDate.ToString("dd MMM yyyy") + "');</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel, typeof(String), "PopupScript1", popupScript.ToString(), false);

    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        //if (dtpValidUpTo.SelectedDate  <  Convert.ToDateTime(DateTime.Now.ToString("ddMMMyyyy")))
        if (dtpValidUpTo.SelectedDate < DateTime.Today)
        {
            lblErrors.Text = "Please Enter Valid Date. Date Must Be Greater Than Todays Date!";
            dtpValidUpTo.Focus();

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
