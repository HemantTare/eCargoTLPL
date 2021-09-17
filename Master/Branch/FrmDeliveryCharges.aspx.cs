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

public partial class Master_Branch_FrmDeliveryCharges : System.Web.UI.Page
{
    Common objCommon = new Common();

    private string DeliveryChargeID
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

    private int BranchID
    {
        //get
        //{
        //    return DDLBranch.SelectedValue;

        //}
        //set
        //{
        //    ViewState["_BranchID"] = value;
        //}

        //set { DDLBranch.SelectedValue = Util.Int2String(value); }
        //get { return Util.String2Int(DDLBranch.SelectedValue); }

        set
        {
            ViewState["_BranchID"] = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ViewState["_BranchID"]);
        }
    }
    private int FromBranchID
    {
        //get
        //{
        //    return DDLFromBranch.SelectedValue;
        //}

        //set { DDLFromBranch.SelectedValue = Util.Int2String(value); }
        //get { return Util.String2Int(DDLFromBranch.SelectedValue); }
        
        set 
        {
            ViewState["_FromBranchID"] = Util.Int2String(value); 
        }
        get 
        {
            return Convert.ToInt32(ViewState["_FromBranchID"]); 
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
    private string FromDeliveryAreaID
    {
        set
        {

            FromddlDeliveryArea.SelectedValue = value;
        }
        get
        {
            return FromddlDeliveryArea.SelectedValue;
        }
    }
    private DateTime ApplicableFrom
    {
        set
        {
            dtpApplicableFromDate.SelectedDate = value;
        }
        get
        {
            return dtpApplicableFromDate.SelectedDate;
        }
    }
    private DateTime FromApplicableFrom
    {
        set
        {
            //FromdtpApplicableFromDate.SelectedDate = value;
            ddlApplicableFrom.SelectedItem.Text = Convert.ToString(value);
        }
        get
        {
            //return FromdtpApplicableFromDate.SelectedDate;
            return Convert.ToDateTime(ddlApplicableFrom.SelectedItem.Text);
        }
    }

    
    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }
    private string SizeRateXML
    {
        get
        {
            string XML = "<newdataset>";

            for (int i = 0; i <= dgGrid.Items.Count - 1; i++)
            {
                HiddenField hdnSizeID = (HiddenField)(dgGrid.Items[i].FindControl("hdnSizeID"));
                TextBox txtRatePerArticle = (TextBox)(dgGrid.Items[i].FindControl("txtRatePerArticle"));

                XML = XML + "<sizeratedetails>";
                XML = XML + "<sizeid>" + hdnSizeID.Value + "</sizeid>";
                XML = XML + "<rateperarticle>" + txtRatePerArticle.Text + "</rateperarticle>";
                XML = XML + "</sizeratedetails>";
            }

            XML = XML + "</newdataset>";

            return XML;
        }
    }
    public DataTable SessionDlyChrgCopyFrom
    {
        get { return StateManager.GetState<DataTable>("DlyChrgCopyFrom"); }
        set
        {
            StateManager.SaveState("DlyChrgCopyFrom", value);
            if (StateManager.Exist("DlyChrgCopyFrom"))
            {
                FillGrid();

            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            DeliveryChargeID = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            initValues();
            ReadValues();
            VisibleControls(false);
            //pnl_Copy.Visible = false;
            //FillGrid();
            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            ScriptManager.SetFocus(txtBranch);
        }

    }

    private void initValues()
    {
        DataSet ds = new DataSet();

        //DataTable dt = new DataTable();
        //dt.Columns.Add("DeliveryChargeID");
        //dt.Columns.Add("BranchID");
        //dt.Columns.Add("DeliveryAreaID");
        //dt.Columns.Add("ApplicableFrom");

        //DataRow dr;
        //dr = dt.NewRow();
        //dt.Rows.Add(dr);

        string query = "select DeliveryChargeID, BranchID as Branch_ID,'' Branch_Name, DeliveryAreaID,ApplicableFrom from Ec_Master_DeliveryCharge where 1=2";
        ds = objCommon.EC_Common_Pass_Query(query);

        SessionDlyChrgCopyFrom = ds.Tables[0]; 
    }
    private void ReadValues()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DeliveryChargeID", SqlDbType.Int, 0, DeliveryChargeID) };
        objDAL.RunProc("dbo.EC_master_DeliveryChargeReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            DDLBranch.DataTextField = "Branch_Name";
            DDLBranch.DataValueField = "Branch_ID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["Branch_Name"].ToString(), objDR["Branch_ID"].ToString(), DDLBranch);
            BranchID = Convert.ToInt32(objDR["Branch_ID"].ToString());
            FillDeliveryArea();
            DeliveryAreaID = objDR["DeliveryAreaID"].ToString();

            ApplicableFrom = Convert.ToDateTime(objDR["ApplicableFrom"].ToString());
        }
    }


    protected void DDLBranch_TxtChange(object sender, EventArgs e)
    {
        BranchID = Util.String2Int(DDLBranch.SelectedValue);
        FillDeliveryArea();
    }
    protected void DDLFromBranch_TxtChange(object sender, EventArgs e)
    {
        FromBranchID = Util.String2Int(DDLFromBranch.SelectedValue);
        FillFromDeliveryArea();
        FillFromBranchDate();
        ScriptManager.SetFocus(FromddlDeliveryArea);
    }

    private void FillDeliveryArea()
    {
        string query = "Select DeliveryAreaID,DeliveryAreaName from ec_master_deliveryarea where branchid = " + DDLBranch.SelectedValue + " order by DeliveryAreaName";
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

    private void FillFromDeliveryArea()
    {
        string query = "Select DeliveryAreaID,DeliveryAreaName from ec_master_deliveryarea where branchid = " + DDLFromBranch.SelectedValue + " order by DeliveryAreaName";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            FromddlDeliveryArea.DataSource = ds;
            FromddlDeliveryArea.DataValueField = "DeliveryAreaID";
            FromddlDeliveryArea.DataTextField = "DeliveryAreaName";
            FromddlDeliveryArea.DataBind();
            ScriptManager.SetFocus(FromddlDeliveryArea);
        }
        else
        {
            FromddlDeliveryArea.Items.Clear();
        }
    }
    private void FillFromBranchDate()
    {
        if (FromDeliveryAreaID.Length > 0)
        {
            string query = "Select DeliveryChargeID,dbo.DateOnlyDisplay(ApplicableFrom) as ApplicableFrom from Ec_Master_DeliveryCharge where DeliveryAreaID = " + FromDeliveryAreaID + " order by ApplicableFrom desc";
            DataSet ds = new DataSet();
            ds = objCommon.EC_Common_Pass_Query(query);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlApplicableFrom.DataSource = ds;
                ddlApplicableFrom.DataValueField = "DeliveryChargeID";
                ddlApplicableFrom.DataTextField = "ApplicableFrom";
                ddlApplicableFrom.DataBind();
                ScriptManager.SetFocus(ddlApplicableFrom);
            }
            else
            {
                ddlApplicableFrom.Items.Clear();
            }
        }
        else
        {
            ddlApplicableFrom.Items.Clear(); 
        }
    }

    private void FillGrid()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DeliveryChargeID", SqlDbType.Int, 0, DeliveryChargeID) };
        objDAL.RunProc("dbo.EC_Master_DeliveryChargesFillGrid", objSqlParam, ref ds);

        dgGrid.DataSource = ds;
        dgGrid.DataBind();
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

        //if (Util.String2Int(DDLBranch.SelectedValue) <= 0)
        //else if (Util.String2Int(ddlDeliveryArea.SelectedValue) <= 0)

        if (BranchID <= 0)
        {
            lblErrors.Text = "Please Select Branch";
            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            ScriptManager.SetFocus(txtBranch);
        }
        else if (Util.String2Int(ddlDeliveryArea.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Delivery Area";
            ddlDeliveryArea.Focus();
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
            objDAL.MakeOutParams("@KeyID", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@DeliveryChargeID", SqlDbType.Int,0, DeliveryChargeID), 
            objDAL.MakeInParams("@BranchID",SqlDbType.Int,0, BranchID),
            objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int,0, DeliveryAreaID), 
            objDAL.MakeInParams("@ApplicableFrom", SqlDbType.DateTime, 0,ApplicableFrom), 
            objDAL.MakeInParams("@SizeRateXML", SqlDbType.Xml, 0,SizeRateXML), 
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[Ec_Master_DeliveryChargeSave]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            ClearVariables();
            ErrorMessage = "Saved SuccessFully";
            string _Msg = "Saved SuccessFully";
            DeliveryChargeID = Convert.ToString(objSqlParam[2].Value);
           
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

        }
        else if (objMessage.messageID == 2627)
        {
            ErrorMessage = "Duplicate combination found for fields Branch,delivery Area,ApplicableFrom.";
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
        FromBranchID = 0;
        DeliveryChargeID = "0";
        dgGrid.DataSource = null;
        dgGrid.DataBind();
    
    }


    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        //pnl_Copy.Visible = true;
        VisibleControls(true);
        TextBox txtBranch = (TextBox)DDLFromBranch.FindControl("txtBoxDDLFromBranch");
        ScriptManager.SetFocus(txtBranch);
 
    }
    protected void btn_fillCopied_Click(object sender, EventArgs e)
    {
        if (ddlApplicableFrom.SelectedIndex == -1)
        {
            ErrorMessage = "Applicable From Date, Does Not Exist for Selected From Delivery Area"; 
            return; 
        }

        VisibleControls(false);
        //pnl_Copy.Visible = false;
        //DataTable dt = new DataTable();

        //dt  = SessionDlyChrgCopyFrom;
        //DlyChargeID = Convert.ToInt32(dt.Rows[0]["DeliveryChargeID"]);
        //BranchID = Convert.ToInt32(dt.Rows[0]["Branch_ID"]);
        //Branch_Name = Convert.ToString(dt.Rows[0]["Branch_Name"]);
        //DlyAreaID = Convert.ToInt32(dt.Rows[0]["DeliveryAreaID"]);
        //ApplFrom = Convert.ToDateTime(dt.Rows[0]["ApplicableFrom"]);

        int DlyChargeID, intBranchID, DlyAreaID;
        string Branch_Name;
        DateTime ApplFrom;


        DlyChargeID = Convert.ToInt32(DeliveryChargeID);
        intBranchID = Convert.ToInt32(FromBranchID); //Convert.ToInt32(DDLFromBranch.SelectedValue); 
        DlyAreaID = Convert.ToInt32(FromDeliveryAreaID);
        ApplFrom = Convert.ToDateTime(FromApplicableFrom);

        FillGridFromCopiedIds(DlyChargeID, intBranchID, DlyAreaID, ApplFrom);
       
    }

    public void VisibleControls(Boolean State)
    {
            lblFromBranch.Visible = State;
            DDLFromBranch.Visible = State;
            lblFromDeliveryArea.Visible = State;
            FromddlDeliveryArea.Visible = State;
            lblApplicableFrom.Visible = State;
            //FromdtpApplicableFromDate.Visible = State;
            ddlApplicableFrom.Visible = State;
            btn_fillCopied.Visible = State;

            if (Convert.ToInt32(DeliveryChargeID) > 0)
            {
                btn_Copy.Visible = false;
            }
            else
            {
                btn_Copy.Visible = true;
            }
             
    }


    private void FillGridFromCopiedIds(int DeliveryChargeID, int intBranchID, int DeliveryAreaID, DateTime ApplicableFrom)
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, intBranchID),
                    objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0, DeliveryAreaID),
                    objDAL.MakeInParams("@ApplicableFrom", SqlDbType.DateTime, 0, ApplicableFrom),
        };
        objDAL.RunProc("EC_Master_Copy_DeliveryCharges", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            dgGrid.DataSource = ds;
            dgGrid.DataBind();
        }
        else
        {
            //dgGrid.DataSource = null;
            //dgGrid.DataBind();
            ErrorMessage = "No Records Found For Selected Copy From Branch ";
            VisibleControls(true);
        }
    }

    protected void ddlDeliveryArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.SetFocus(dtpApplicableFromDate);
    }
    protected void FromddlDeliveryArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.SetFocus(FromdtpApplicableFromDate);
        FillFromBranchDate();
        ScriptManager.SetFocus(ddlApplicableFrom);
    }
}
