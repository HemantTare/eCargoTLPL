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

public partial class Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_UndeliverReason : System.Web.UI.Page
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

    public int Reason_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_Reason_Id"]);
        }
        set
        {
            ViewState["_Reason_Id"] = value;
            ddl_Reason.SelectedValue = Convert.ToString(value);
        }
    }

#endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {

            Fill_Reason();

            GC_Id = Convert.ToInt32(Request.QueryString["GC_Id"]);
            GC_No  = Request.QueryString["GC_No"];
            Reason_Id = Convert.ToInt32(Request.QueryString["Reason_Id"]);

            
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();

            ClearVariables();
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString("Saved Succesfully"));
            
        }
    }

    #endregion

    #region Other Function


    private void Fill_Reason()
    {
        DAL objDAL = new DAL();

        objDAL.RunProc("App_Opr_DDC_FillReturnReasons", ref ds);


        ddl_Reason.DataSource = ds;
        ddl_Reason.DataTextField = "Reason";
        ddl_Reason.DataValueField = "Reason_Id";
        ddl_Reason.DataBind();

        ddl_Reason.Items.Insert(0, new ListItem("Select Reason", "0"));
    }

    private Message Save()
    {

        int Reason_Id = 0;

        Reason_Id = Util.String2Int(ddl_Reason.SelectedValue);

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0, GC_Id), 
            objDAL.MakeInParams("@Reason_Id",SqlDbType.Int,0,Reason_Id), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Opr_Dly_BranchWise_Pending_Stock_UndeliverReason_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
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

        if (Util.String2Int(ddl_Reason.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Valid Reason";
            ddl_Reason.Focus();
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
