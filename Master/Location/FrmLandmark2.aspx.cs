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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Location_FrmLandmark2 : ClassLibraryMVP.UI.Page
{
    Common objCommon = new Common();

    private string Landmark2ID
    {
        set
        {

            if (value == null)
                hdnKeyID.Value = "0";
            else
                hdnKeyID.Value = value.ToString();
        }
        get
        {
            if (Util.String2Int(hdnKeyID.Value) <= 0)
            {
                return "0";
            }
            return hdnKeyID.Value;
        }
    }

    private string Landmark2
    {
        set
        {
            txtLandmark2.Text = value.ToString();
        }
        get
        {
            return txtLandmark2.Text.Trim();
        }
    }


    private int BranchID
    {
        set
        {
            ViewState["_BranchID"] = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ViewState["_BranchID"]);
        }
    }

    private string DeliveryAreaID
    {
        set
        {

            ddlDeliveryArea.SelectedValue = value;
        }
        get
        {
            return ddlDeliveryArea.SelectedValue;
        }
    }

    private string Landmark1ID
    {
        set
        {

            ddlLandmark1.SelectedValue = value;
        }
        get
        {
            return ddlLandmark1.SelectedValue;
        }
    }

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Landmark2ID  = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

        if (!IsPostBack)
        {
            ReadValues();
        }

        DDLBranch.Focus();

    }


    protected void DDLBranch_TxtChange(object sender, EventArgs e)
    {
        BranchID = Util.String2Int(DDLBranch.SelectedValue);
        if (BranchID > 0)
        {
            FillDeliveryArea();

            ddlLandmark1.Items.Clear();

            if (ddlDeliveryArea.SelectedValue != "" )
            {
                FillLandmark1();
            }
        }
    }

    private void FillDeliveryArea()
    {
        string query = "Select DeliveryAreaID,DeliveryAreaName from ec_master_deliveryarea where IsActive=1 and branchid = " + DDLBranch.SelectedValue + " order by DeliveryAreaName";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDeliveryArea.DataSource = ds;
            ddlDeliveryArea.DataValueField = "DeliveryAreaID";
            ddlDeliveryArea.DataTextField = "DeliveryAreaName";
            ddlDeliveryArea.DataBind();
            ScriptManager.SetFocus(ddlDeliveryArea);
        }
        else
        {
            ddlDeliveryArea.Items.Clear();
        }
    }

    private void FillLandmark1()
    {
        string query = "Select Landmark1ID,Landmark1 from ec_master_Landmark1 where IsActive=1 and DeliveryAreaID = " + ddlDeliveryArea.SelectedValue  + " order by Landmark1";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLandmark1.DataSource = ds;
            ddlLandmark1.DataValueField = "Landmark1ID";
            ddlLandmark1.DataTextField = "Landmark1";
            ddlLandmark1.DataBind();
            ScriptManager.SetFocus(ddlLandmark1);
        }
        else
        {
            ddlLandmark1.Items.Clear();
        }
    }

    protected void ddlDeliveryArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLandmark1();
        ScriptManager.SetFocus(ddlLandmark1);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }

    private bool AllowToSave()
    {
        bool ATS = false;

        if (BranchID <= 0)
        {
            lblErrors.Text = "Please Select Branch";
            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            ScriptManager.SetFocus(txtBranch);
        }
        else if (Util.String2Int(ddlDeliveryArea.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Delivery Area";
            ScriptManager.SetFocus(ddlDeliveryArea); 
        }
        else if (Util.String2Int(ddlLandmark1.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Landmark 1";
            ScriptManager.SetFocus(ddlLandmark1);
        }
        else if (txtLandmark2.Text.Trim() == "" )
        {
            lblErrors.Text = "Please Enter Landmark";
            ScriptManager.SetFocus(txtLandmark2);
        }
        else
            ATS = true;

        return ATS;
    }


    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Landmark2ID", SqlDbType.Int,0, Landmark2ID), 
            objDAL.MakeInParams("@Landmark1ID", SqlDbType.Int,0, Landmark1ID), 
            objDAL.MakeInParams("@Landmark2", SqlDbType.VarChar, 50,Landmark2), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EC_Master_Landmark2_Save]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            ClearVariables();
            ErrorMessage = "Saved SuccessFully";
            string _Msg = "Saved SuccessFully";
           

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

        }
        else if (objMessage.messageID == 2627)
        {
            ErrorMessage = "Duplicate found";
        }
        else
        {
            ErrorMessage = objMessage.message;
        }
        return objMessage;
    }

    public void ClearVariables()
    {
        BranchID = 0;
        Landmark2ID = "0";

    }


    private void ReadValues()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] objSqlParam = {
        objDAL.MakeInParams("@Landmark2ID", SqlDbType.Int,0, Util.String2Int(Landmark2ID))};

        objDAL.RunProc("[dbo].[EC_Master_Landmark2ReadValues]", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            Landmark2 = objDR["Landmark2"].ToString();

            DDLBranch.DataTextField = "Branch_Name";
            DDLBranch.DataValueField = "Branch_ID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["Branch_Name"].ToString(), objDR["Branch_ID"].ToString(), DDLBranch);
            BranchID = Convert.ToInt32(objDR["Branch_ID"].ToString());

            FillDeliveryArea();
            DeliveryAreaID = objDR["DeliveryAreaID"].ToString();

            FillLandmark1();
            Landmark1ID = objDR["Landmark1ID"].ToString();


        }

    }


}
