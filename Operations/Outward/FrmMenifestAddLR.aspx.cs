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
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using Raj.EC;


public partial class Operations_Outward_FrmMenifestAddLR : System.Web.UI.Page
{
    Hashtable htSelectedLRs = new Hashtable();
    Hashtable htSelectedServiceLocs = new Hashtable();
    string serviceLocationIds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (IsPostBack == false)
        {
            BindArea();
        }
    }

    private void BindArea()
    {
        Common objCommon = new Common();

        ddlArea.DataTextField = "Area_Name";
        ddlArea.DataValueField = "Area_ID";
        ddlArea.DataSource = objCommon.EC_Common_Pass_Query("SELECT Area_ID,Area_Name FROM EC_Master_Area");
        ddlArea.DataBind();

        Common.InsertItem(ddlArea);

        for (int i = 0; i < ddlArea.Items.Count; i++)
        {
            htSelectedLRs.Add(ddlArea.Items[i].Value, "");
            htSelectedServiceLocs.Add(ddlArea.Items[i].Value, "");
        }

        ViewState["htSelectedLRs"] = htSelectedLRs;
        ViewState["htSelectedServiceLocs"] = htSelectedServiceLocs;
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrids(2);
        if (IsAnyServiceLocationSelected())
        {
            btnGo_Click(ddlArea, e);
        }
        lblLRAdded.Text = "";
    }

    private bool IsAnyServiceLocationSelected()
    {
        bool IsAnyServiceLocationSelected = false;

        if (ViewState["htSelectedServiceLocs"] != null)
        {
            htSelectedServiceLocs = (Hashtable)ViewState["htSelectedServiceLocs"];
            string selectedServiceLocations = htSelectedServiceLocs[ddlArea.SelectedValue].ToString();

            if (selectedServiceLocations != "")
            {
                char[] delimiterChars = { ',' };
                string[] serviceLocatonsArray = selectedServiceLocations.Split(delimiterChars);

                int serviceLocationGridCount = dg_servicelocations.Items.Count;
                CheckBox Chk_Attach;
                HiddenField hdnServiceLocationID;

                for (int i = 0; i < serviceLocationGridCount; i++)
                {
                    Chk_Attach = (CheckBox)dg_servicelocations.Items[i].FindControl("Chk_Attach");
                    hdnServiceLocationID = (HiddenField)dg_servicelocations.Items[i].FindControl("hdnServiceLocationID");

                    for (int j = 0; j < serviceLocatonsArray.Length; j++)
                    {
                        if (hdnServiceLocationID.Value == serviceLocatonsArray[j].ToString())
                        {
                            IsAnyServiceLocationSelected = true;
                            Chk_Attach.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        return IsAnyServiceLocationSelected;
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        serviceLocationIds = "";
        int serviceLocationGridCount = dg_servicelocations.Items.Count;
        CheckBox Chk_Attach;
        HiddenField hdnServiceLocationID;

        for (int i = 0; i < serviceLocationGridCount; i++)
        {
            Chk_Attach = (CheckBox)dg_servicelocations.Items[i].FindControl("Chk_Attach");
            hdnServiceLocationID = (HiddenField)dg_servicelocations.Items[i].FindControl("hdnServiceLocationID");

            if (Chk_Attach.Checked)
            {
                if (serviceLocationIds == "")
                    serviceLocationIds = hdnServiceLocationID.Value;
                else
                    serviceLocationIds = serviceLocationIds + "," + hdnServiceLocationID.Value;
            }
        }
        htSelectedServiceLocs = (Hashtable)ViewState["htSelectedServiceLocs"];
        htSelectedServiceLocs[ddlArea.SelectedValue] = serviceLocationIds;
        ViewState["htSelectedServiceLocs"] = htSelectedServiceLocs;

        BindGrids(3);
        CheckSelectedLR();
    }

    private void BindGrids(int callFrom)
    {
        string BranchID, MenifestTypeID;
        int MemoYear ,MemoMonth,MemoDay;

        BranchID = Request.QueryString["BranchID"].ToString();
        MenifestTypeID = Request.QueryString["MemoTypeID"].ToString();
        MemoYear = Util.String2Int(Request.QueryString["MemoYear"].ToString());
        MemoMonth = Util.String2Int(Request.QueryString["MemoMonth"].ToString());
        MemoDay = Util.String2Int(Request.QueryString["MemoDay"].ToString());
        DateTime MenifestDate = new DateTime(MemoYear, MemoMonth, MemoDay);

        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, BranchID),
            objDAL.MakeInParams("@MenifestType_Id", SqlDbType.Int, 0, MenifestTypeID),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, 0) ,
            objDAL.MakeInParams("@Menifest_Id", SqlDbType.Int, 0, 0) ,
            objDAL.MakeInParams("@IsAccTransferReq", SqlDbType.Bit, 0, 0) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, "<parentroot></parentroot>") ,
            objDAL.MakeInParams("@Menifest_Date", SqlDbType.DateTime, 0, MenifestDate),
            objDAL.MakeInParams("@IsFrom_Edit", SqlDbType.Bit, 0, 0),
            objDAL.MakeInParams("@AreaID", SqlDbType.Int, 0, ddlArea.SelectedValue),
            objDAL.MakeInParams("@CallFrom", SqlDbType.TinyInt, 0, callFrom),
            objDAL.MakeInParams("@ServiceLocationIDs", SqlDbType.VarChar, 2000, serviceLocationIds)};

        objDAL.RunProc("dbo.EC_Opr_Menifest_ReadValues", objSqlParam, ref objDS);

        if (callFrom == 2)
        {
            dg_servicelocations.DataSource = objDS.Tables[0];
            dg_servicelocations.DataBind();

            dg_Memo.DataSource = objDS.Tables[1];
            dg_Memo.DataBind();
        }
        if (callFrom == 3)
        {
            dg_Memo.DataSource = objDS.Tables[0];
            dg_Memo.DataBind();
        }
    }

    protected void btnAddLR_Click(object sender, EventArgs e)
    {
        string LRNos = "";
        int memoGridCount = dg_Memo.Items.Count;
        CheckBox Chk_Attach;
        HiddenField hdnGcNo;
        int count = 0;

        for (int i = 0; i < memoGridCount; i++)
        {
            Chk_Attach = (CheckBox)dg_Memo.Items[i].FindControl("Chk_Attach");
            hdnGcNo = (HiddenField)dg_Memo.Items[i].FindControl("hdnGcNo");

            if (Chk_Attach.Checked)
            {
                count = count + 1;
                if (LRNos == "")
                    LRNos = hdnGcNo.Value;
                else
                    LRNos = LRNos + "," + hdnGcNo.Value;
            }
        }

        htSelectedLRs = (Hashtable)ViewState["htSelectedLRs"];
        htSelectedLRs[ddlArea.SelectedValue] = LRNos;
        ViewState["htSelectedLRs"] = htSelectedLRs;
        lblLRAdded.Text = count.ToString() + " LR(s) added";


        htSelectedLRs = (Hashtable)ViewState["htSelectedLRs"];
        string serviceLocationWiseLRs = "";
        lblSelectedLRs.Text = "";
        for (int i = 0; i < ddlArea.Items.Count; i++)
        {
            serviceLocationWiseLRs = htSelectedLRs[ddlArea.Items[i].Value].ToString();

            if (serviceLocationWiseLRs != "")
            {
                lblSelectedLRs.Text = lblSelectedLRs.Text + "," + serviceLocationWiseLRs;
            }
        }

        if (lblSelectedLRs.Text != "")
        {
            lblSelectedLRs.Text = lblSelectedLRs.Text.Substring(1, lblSelectedLRs.Text.Length - 1);
        }
    }

    private void CheckSelectedLR()
    {
        if (ViewState["htSelectedLRs"] != null)
        {
            htSelectedLRs = (Hashtable)ViewState["htSelectedLRs"];

            string selectedLR = htSelectedLRs[ddlArea.SelectedValue].ToString();

            if (selectedLR != "")
            {
                char[] delimiterChars = { ',' };
                string[] selectedLRArray = selectedLR.Split(delimiterChars);

                int memoGridCount = dg_Memo.Items.Count;
                CheckBox Chk_Attach;
                HiddenField hdnGcNo;

                for (int i = 0; i < memoGridCount; i++)
                {
                    Chk_Attach = (CheckBox)dg_Memo.Items[i].FindControl("Chk_Attach");
                    hdnGcNo = (HiddenField)dg_Memo.Items[i].FindControl("hdnGcNo");

                    for (int j = 0; j < selectedLRArray.Length; j++)
                    {
                        if (hdnGcNo.Value == selectedLRArray[j].ToString())
                        {
                            Chk_Attach.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
    }


    protected void btnAddLRToMemo_Click(object sender, EventArgs e)
    {
        btnAddLR_Click(sender, e);

        string script = "<script language='javascript'> " + "UpdateParentWindow();" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "UpdateParentWindow()", script, false);
    }
}
