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

public partial class Master_Branch_FrmVarnarMaster : ClassLibraryMVP.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DataRow dr;
    bool Allow_To_Save;
    DataTable objDT;

    Common objCommon = new Common();



    private string PayTypeID
    {
        set
        {

            ddlPayType.SelectedValue = value;
        }
        get
        {
            return ddlPayType.SelectedValue;
        }
    }

    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }


    public DataTable Session_PackingGrid
    {
        get { return StateManager.GetState<DataTable>("PackingGrid"); }
        set { StateManager.SaveState("PackingGrid", value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            FillPayType();

            ReadValues();

            txt_Name.Focus();
        }
    }

    private void FillPayType()
    {
        string query = "Select Pay_Type_ID,Pay_Type from EC_Master_Varai_Pay_Mode where Is_Active=1 Order by Pay_Type_ID";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlPayType.DataSource = ds;
            ddlPayType.DataValueField = "Pay_Type_ID";
            ddlPayType.DataTextField = "Pay_Type";
            ddlPayType.DataBind();
        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddlPayType.SelectedValue) == 1)
        {
            tr_AccNo.Visible = true;
            tr_Blank1.Visible = true;
            tr_IFSCCode.Visible = true;
            tr_Blank2.Visible = true;
            tr_BankName.Visible = true;
            tr_BenificiaryMobile.Visible = false;
        }
        else
        {
            tr_AccNo.Visible = false;
            tr_Blank1.Visible = false;
            tr_IFSCCode.Visible = false;
            tr_Blank2.Visible = false;
            tr_BankName.Visible = false;
            tr_BenificiaryMobile.Visible = true;
        }
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;


        if (txt_Name.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Name";
            scm_Comm.SetFocus(txt_Name);
        }
        else if (txt_MobileNo.Text.Trim().Length < 10)
        {
            lblErrors.Text = "Please Enter Proper Mobile No.";
            scm_Comm.SetFocus(txt_MobileNo);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) == 1 && txt_AccNo.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Account No.";
            scm_Comm.SetFocus(txt_AccNo);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) == 1 && txt_IFSCCode.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter IFSC Code";
            scm_Comm.SetFocus(txt_IFSCCode);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) == 1 && txt_BankName.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Bank Name";
            scm_Comm.SetFocus(txt_BankName);
        }
        else if (txt_Beneficiary.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Beneficiary Name";
            scm_Comm.SetFocus(txt_Beneficiary);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) != 1 && txt_BeneficiaryMobile.Text.Trim().Length < 10)
        {
            lblErrors.Text = "Please Enter Beneficiary Mobile No.";
            scm_Comm.SetFocus(txt_BeneficiaryMobile);
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    protected void btnCopyName_Click(object sender, EventArgs e)
    {

        txt_Beneficiary.Text = txt_Name.Text;

    }

    protected void btnCopyMobileNo_Click(object sender, EventArgs e)
    {

        txt_BeneficiaryMobile.Text = txt_MobileNo.Text;

    }

    private Message Save()
    {

        if (Util.String2Int(ddlPayType.SelectedValue) != 1)
        {
            txt_AccNo.Text = "";
            txt_IFSCCode.Text = "";
            txt_BankName.Text = "";
        }


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@Varnar_ID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@Varnar_Name",SqlDbType.VarChar ,100,txt_Name.Text),  
            objDAL.MakeInParams("@Mobile_No",SqlDbType.VarChar ,10,txt_MobileNo.Text),
            objDAL.MakeInParams("@Pref_Pay_Type_Id",SqlDbType.Int  ,0,Util.String2Int(ddlPayType.SelectedValue)), 
            objDAL.MakeInParams("@Account_No",SqlDbType.VarChar ,30,txt_AccNo.Text),
            objDAL.MakeInParams("@IFSC_Code",SqlDbType.VarChar ,20,txt_IFSCCode.Text),
            objDAL.MakeInParams("@Bank_Name",SqlDbType.VarChar ,100,txt_BankName.Text),
            objDAL.MakeInParams("@Beneficiary_Name",SqlDbType.VarChar ,100,txt_Beneficiary.Text),
            objDAL.MakeInParams("@Beneficiary_Mobile",SqlDbType.VarChar ,10,txt_BeneficiaryMobile.Text),
            objDAL.MakeInParams("@PackingDetailsXML",SqlDbType.Xml,0,RateXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_Varnar_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);


            String Call_From;

            Call_From = Request.QueryString["Call_From"];

            if (Call_From == "VaraiPayment")
            {
                updateparentdata();
                //popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_Item_Details();</script>";
            }
            else
            {

                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            }
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }

    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Varnar_ID", SqlDbType.Int, 0, Convert.ToInt32(hdnKeyID.Value)) };

        objDAL.RunProc("EC_Mst_Varnar_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            txt_Name.Text = objDR["Varnar_Name"].ToString();
            txt_MobileNo.Text = objDR["Mobile_No"].ToString();
            ddlPayType.SelectedValue = objDR["Pref_Pay_Type_ID"].ToString();
            txt_AccNo.Text = objDR["Account_No"].ToString();
            txt_IFSCCode.Text = objDR["IFSC_Code"].ToString();
            txt_BankName.Text = objDR["Bank_Name"].ToString();
            txt_Beneficiary.Text = objDR["Beneficiary_Name"].ToString();
            txt_BeneficiaryMobile.Text = objDR["Beneficiary_Mobile"].ToString();

        }


        if (Util.String2Int(ddlPayType.SelectedValue) == 1)
        {
            tr_AccNo.Visible = true;
            tr_Blank1.Visible = true;
            tr_IFSCCode.Visible = true;
            tr_Blank2.Visible = true;
            tr_BankName.Visible = true;
            tr_BenificiaryMobile.Visible = false;
        }
        else
        {
            tr_AccNo.Visible = false;
            tr_Blank1.Visible = false;
            tr_IFSCCode.Visible = false;
            tr_Blank2.Visible = false;
            tr_BankName.Visible = false;
            tr_BenificiaryMobile.Visible = true;
        }

        Session_PackingGrid = objDS.Tables[1];

        Bind_dg_Grid();

    }

    private void Bind_dg_Grid()
    {
        dg_Grid.DataSource = Session_PackingGrid;
        dg_Grid.DataBind();

    }

    private string RateXML
    {
        get
        {
            string XML = "<newdataset>";

            for (int i = 0; i <= dg_Grid.Items.Count - 1; i++)
            {

                HiddenField hdn_Packing_Id = (HiddenField)(dg_Grid.Items[i].FindControl("hdn_Packing_Id"));
                TextBox txtRate = (TextBox)(dg_Grid.Items[i].FindControl("txtRate"));

                XML = XML + "<ratedetails>";
                XML = XML + "<packingtypeid>" + hdn_Packing_Id.Value + "</packingtypeid>";
                XML = XML + "<rate>" + txtRate.Text + "</rate>";
                XML = XML + "</ratedetails>";
            }

            XML = XML + "</newdataset>";

            return XML;
        }
    }

    private void updateparentdata()
    {
        String popupScript = "";
        string _Msg = "Saved SuccessFully";
        //        String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();updateparentdata('" + txt_Name.Text + "','" + txt_MobileNo.Text   + "');</script>";
        popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();updateparentdata('" + txt_Name.Text + "','" + txt_MobileNo.Text + "','" + txt_Beneficiary.Text + "','" + txt_BeneficiaryMobile.Text + "','" + txt_AccNo.Text + "','" + txt_IFSCCode.Text + "','" + txt_BankName.Text + "');</script>";

        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel, typeof(String), "PopupScript1", popupScript.ToString(), false);

    }
}
