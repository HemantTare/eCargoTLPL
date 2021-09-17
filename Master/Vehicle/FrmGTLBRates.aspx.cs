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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Master_Vehicle_FrmGTLBRates : ClassLibraryMVP.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;

    public decimal ThappiPerTon
    {
        set
        {
            txt_ThappiPerTon.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txt_ThappiPerTon.Text == string.Empty ? 0 : Util.String2Decimal(txt_ThappiPerTon.Text); }
    }

    public decimal LoadingPerTon
    {
        set
        {
            txt_LoadingPerTon.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txt_LoadingPerTon.Text == string.Empty ? 0 : Util.String2Decimal(txt_LoadingPerTon.Text); }
    }


    public decimal WarferPerDay
    {
        set
        {
            txt_WarferPerDay.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txt_WarferPerDay.Text == string.Empty ? 0 : Util.String2Decimal(txt_WarferPerDay.Text); }
    }


    public bool validateUI()
    {
        bool ATS;
        ATS = false;


        if (ThappiPerTon <= 0)
        {
            lblErrors.Text = "Please Enter Thappi Per Ton";
            scm_Comm.SetFocus(txt_ThappiPerTon);
        }
        else if (LoadingPerTon <= 0)
        {
            lblErrors.Text = "Please Enter Loading Per Ton";
            scm_Comm.SetFocus(txt_LoadingPerTon);
        }
        else if (WarferPerDay  <= 0)
        {
            lblErrors.Text = "Please Enter Warfer Per Day";
            scm_Comm.SetFocus(txt_WarferPerDay);
        }

        else
        {
            ATS = true;
        }

        return ATS;
    }


 
    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 

            fillRatesDetails();

            if (Convert.ToInt32(hdnKeyID.Value) > 0)
            {
                td_ApplicableFrom.Disabled = true;
                dtpApplicableFrom.Disable = true;
            }
        }
    }

    private void fillRatesDetails()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@RateID", SqlDbType.Int, 0,Convert.ToInt32(hdnKeyID.Value))};

        objDAL.RunProc("dbo.EC_Mst_GTLBRatesDetails", objSqlParam, ref objDS);
        

        if (objDS.Tables[0].Rows.Count > 0)
        {
            
            DataRow objDR = objDS.Tables[0].Rows[0];

            ThappiPerTon  = Util.String2Decimal(objDR["ThappiPerTon"].ToString());
            LoadingPerTon  =  Util.String2Decimal(objDR["LoadingPerTon"].ToString());
            WarferPerDay =  Util.String2Decimal(objDR["WarferPerDay"].ToString());

            dtpApplicableFrom.SelectedDate = Convert.ToDateTime(objDR["ApplicableFrom"].ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@RateID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@ApplicableFrom",SqlDbType.DateTime,0,dtpApplicableFrom.SelectedDate), 
            objDAL.MakeInParams("@ThappiPerTon",SqlDbType.Decimal ,0,ThappiPerTon), 
            objDAL.MakeInParams("@LoadingPerTon",SqlDbType.Decimal,0,LoadingPerTon), 
            objDAL.MakeInParams("@WarferPerDay",SqlDbType.Decimal,0,WarferPerDay), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_GTLBRates_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }
}
