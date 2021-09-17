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
using System.Text;
using System.Net;
using System.IO;


public partial class Operations_Booking_FrmPartyInvoiceWeightUpdateNewConsigneeDetails : System.Web.UI.Page
{
    Common CommonObj = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;

    private int BranchID, BillingClientID;
    private string HierarchyCode;
    private DateTime FromDate, ToDate;

    private DataSet ds_NewClient_details = null;

    private string ErrorMsg
    {
        set { lbl_errors.Text = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        BranchID = Convert.ToInt32(Request.QueryString["BranchID"].ToString());
        BillingClientID = Convert.ToInt32(Request.QueryString["BillingClientID"].ToString());
        HierarchyCode = Request.QueryString["HierarchyCode"].ToString();
        FromDate = Convert.ToDateTime(Request.QueryString["FromDate"].ToString());
        ToDate = Convert.ToDateTime(Request.QueryString["ToDate"].ToString());

        if (!IsPostBack)
        {
            Session.Remove("ds_NewClient_details");

            BindGrid("form_and_pageload", e);
        }

    }



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);


        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@HierarchyCode", SqlDbType.Char, 3, HierarchyCode),
        objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
        objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
        objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, BranchID),
        objDAL.MakeInParams("@ClientID", SqlDbType.Int, 0, BillingClientID)};

        objDAL.RunProc("EC_Opr_Fill_LR_For_Weight_Update_New", objSqlParam, ref objDS);

        if (objDS.Tables[2].Rows.Count > 0)
        {
            Session["ds_NewClient_details"] = objDS.Tables[2].DataSet;

            ds_NewClient_details = (DataSet)Session["ds_NewClient_details"];

            datagrid1.DataSource = objDS.Tables[2];
            datagrid1.DataBind();
        }
    }

    protected void datagrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemIndex != -1)
        {

            CheckBox chk_verified = (CheckBox)e.Item.FindControl("Chk_Verified");

            string Chk_Verified = ds_NewClient_details.Tables[2].Rows[e.Item.ItemIndex]["IsVerified"].ToString();

            if (Chk_Verified == "True")
            {
                chk_verified.Enabled = false;

                e.Item.BackColor = System.Drawing.Color.Lime;
            }
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            int Consignee_Client_ID, DlyAreaID;

            LinkButton lnk_Consignee_Name, lnk_DlyArea;

            Boolean Is_Consignee_Regular_Client;

            lnk_Consignee_Name = (LinkButton)e.Item.FindControl("lnk_Consignee_Name");
            lnk_DlyArea = (LinkButton)e.Item.FindControl("lnk_DlyArea");

            Consignee_Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Consignee_Client_Id").ToString());
            DlyAreaID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "DlyAreaID").ToString());
            Is_Consignee_Regular_Client = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "Is_Consignee_Regular_Client").ToString());

            lnk_Consignee_Name.Attributes.Add("onclick", "return viewwindow_ClientConsignee('" + ClassLibraryMVP.Util.EncryptInteger(Consignee_Client_ID) + "','" + Is_Consignee_Regular_Client + "')");

            lnk_DlyArea.Attributes.Add("onclick", "return viewwindow_DeliveryArea('" + ClassLibraryMVP.Util.EncryptInteger(DlyAreaID) + "')");

        }

    }

    private void Update_ds_NewClient_details()
    {
        ds_NewClient_details = (DataSet)Session["ds_NewClient_details"];
        int ds_row = 0;

        int i = 0;
        CheckBox Chk_Verified = default(CheckBox);

        for (i = 0; i <= datagrid1.Items.Count - 1; i++)
        {
            ds_row = (datagrid1.PageSize * datagrid1.CurrentPageIndex) + i;
            Chk_Verified = (CheckBox)datagrid1.Items[i].Cells[8].FindControl("Chk_Verified");
            ds_NewClient_details.Tables[2].Rows[ds_row]["IsVerified"] = Convert.ToBoolean(Chk_Verified.Checked);
        }

        Session["ds_NewClient_details"] = ds_NewClient_details;
    }

    protected void Btn_Save_Click(object sender, System.EventArgs e)
    {
        Update_ds_NewClient_details();

        if (Allow_To_Save() == true)
        {
            Save();
            ClearVariables();
            Response.Redirect("~/Display/CloseForm.aspx");
        }
    }

    private bool Allow_To_Save()
    {
        bool functionReturnValue = false;
        ds_NewClient_details = (DataSet)Session["ds_NewClient_details"];

        if (ds_NewClient_details != null)
        {
            DataView view = default(DataView);
            view = CommonObj.Get_View_Table(ds_NewClient_details.Tables[2], "IsVerified = 1 and IsVerifiedPrevious=0");
            if (view.Count > 0)
            {
                lbl_errors.Visible = false;
                functionReturnValue = true;
            }
            else
            {
                lbl_errors.Visible = true;
                lbl_errors.Text = "No Record Selected";
            }
        }
        else
        {
            lbl_errors.Visible = true;
            lbl_errors.Text = "No Record Selected";
        }
        return functionReturnValue;
    }

    private void Save()
    {
        int Verified_By = UserManager.getUserParam().UserId;
        string xml = null;


        ds_NewClient_details = (DataSet)Session["ds_NewClient_details"];

        xml = ds_NewClient_details.GetXml();

        NewClientVerificationSave(xml, Verified_By);
    }

    private void NewClientVerificationSave(string xml, int Verified_By)
    {
        DAL objDAL = new DAL();
        SqlParameter[] sqlpar = {
            objDAL.MakeInParams("@xml", SqlDbType.Xml, 0, xml),
            objDAL.MakeInParams("@Verified_By", SqlDbType.Int, 0, Verified_By)
            };

        objDAL.RunProc("EC_NewClientVerification_Save", sqlpar);
    }

    private void ClearVariables()
    {
        objDS = null;
        Session.Remove("ds_NewClient_details");
        ds_NewClient_details = null;
    }
}


