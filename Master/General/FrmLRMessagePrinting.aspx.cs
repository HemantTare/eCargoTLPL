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
using ClassLibraryMVP;
using System.Data.SqlClient;
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_General_FrmGeneralRateDiscount : ClassLibraryMVP.UI.Page
{
    
    bool ATS = false;

    #region members


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //  return 34;
        }
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false; 
       
        if (dtpValidFrom.SelectedDate > dtpValidUpto.SelectedDate)
        {
            lblErrors.Text = "Valid Upto must be greater than Valid from date";
        }
        else if (txtEnglishMessage.Text.Length > 0 && txtHindiMessage.Text.Length > 0)
        {
            lblErrors.Text = "Message Can be Entered Either in English Or in Hindi";
        } 
        else
        {
            ATS = true;
        }

        return ATS;
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 
            if (keyID > 0)
            {
                txtTitle.Enabled = false; 
            }
            ReadValues(); 
        }

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LR_MessageID", SqlDbType.Int, 0, keyID) };
        objDAL.RunProc("EC_Mst_LR_Message_Printing_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            txtTitle.Text = objDR["Title"].ToString(); 
            dtpValidFrom.SelectedDate = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            dtpValidUpto.SelectedDate = Convert.ToDateTime(objDR["ValidUpTo"].ToString());
            txtEnglishMessage.Text = objDR["EnglishMessage"].ToString();
            txtHindiMessage.Text = objDR["HindiMessage"].ToString(); 
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
            objDAL.MakeInParams("@LR_MessageID",SqlDbType.Int,0,keyID), 
            objDAL.MakeInParams("@Title",SqlDbType.VarChar,200,txtTitle.Text), 
            objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,dtpValidFrom.SelectedDate),
            objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,dtpValidUpto.SelectedDate), 
            objDAL.MakeInParams("@EnglishMessage",SqlDbType.VarChar,500,txtEnglishMessage.Text), 
            objDAL.MakeInParams("@HindiMessage",SqlDbType.VarChar,500,txtHindiMessage.Text),  
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("EC_Mst_LR_Message_Printing_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            //lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            string _Msg;
            _Msg = "Saved SuccessFully"; 
            String popupScript = ""; 
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
