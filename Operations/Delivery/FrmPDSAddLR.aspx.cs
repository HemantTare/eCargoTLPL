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


public partial class Operations_Outward_FrmPDSAddLR : System.Web.UI.Page
{
    Hashtable htSelectedLRs = new Hashtable();
    Hashtable htSelectedDeliveryAreas = new Hashtable();
    string DeliveryAreaIDs = "";
    int deliveryBranchId;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (IsPostBack == false)
        {
            deliveryBranchId = Convert.ToInt32(Request.QueryString["BranchID"]); 
            BindArea();
            BindGrid("form_and_pageload", e);
        }
    }
    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        DataSet ds = new DataSet();

        int grid_currentpageindex = dg_servicelocations.CurrentPageIndex;
        int grid_PageSize = dg_servicelocations.PageSize;
        DateTime start_date = (DateTime)UserManager.getUserParam().StartDate;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = 0;
        int Area_id = 0;
        int Branch_id = deliveryBranchId;
        DateTime As_On_Date = DateTime.Now;
        int Division_Id = UserManager.getUserParam().DivisionId;

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Region_id", SqlDbType.Int, 0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int, 0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int, 0,Branch_id),
            objDAL.MakeInParams("@As_On_Date", SqlDbType.DateTime, 0,As_On_Date),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,Division_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,""),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,0),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,As_On_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,1)
             //objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0,start_date)
        };

        objDAL.RunProc("[EC_RPT_PDS_Delivery_Stock_List_GRD]", objSqlParam, ref ds);
        //if (CallFrom == "form_and_pageload") return;

        //dg_servicelocations.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        //calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_servicelocations, ds.Tables[0], CallFrom, lbl_Error); 
        
    }

    private void BindArea()
    {
        Common objCommon = new Common();

        ddlArea.DataTextField = "DeliveryAreaName";
        ddlArea.DataValueField = "DeliveryAreaID";
        ddlArea.DataSource = objCommon.GetDeliveryArea(deliveryBranchId, true, true, false,0);
        ddlArea.DataBind();
        ddlArea.Visible = false;
        Common.InsertItem(ddlArea);

        for (int i = 0; i < ddlArea.Items.Count; i++)
        {
            htSelectedLRs.Add(ddlArea.Items[i].Value, "");
            htSelectedDeliveryAreas.Add(ddlArea.Items[i].Value, "");
        }

        ViewState["htSelectedLRs"] = htSelectedLRs;
        ViewState["htSelectedDeliveryAreas"] = htSelectedDeliveryAreas;
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrids(3);
        if (IsAnyServiceLocationSelected())
        {
            btnGo_Click(ddlArea, e);
        }
        lblLRAdded.Text = "";
    }

    private bool IsAnyServiceLocationSelected()
    {
        bool IsAnyServiceLocationSelected = false;

        if (ViewState["htSelectedDeliveryAreas"] != null)
        {
            htSelectedDeliveryAreas = (Hashtable)ViewState["htSelectedDeliveryAreas"];
            string selectedServiceLocations = htSelectedDeliveryAreas[ddlArea.SelectedValue].ToString();

            if (selectedServiceLocations != "")
            {
                char[] delimiterChars = { ',' };
                string[] serviceLocatonsArray = selectedServiceLocations.Split(delimiterChars);

                int serviceLocationGridCount = dg_servicelocations.Items.Count;
                CheckBox Chk_Attach;
                HiddenField hdnDeliveryAreaID;

                for (int i = 0; i < serviceLocationGridCount; i++)
                {
                    Chk_Attach = (CheckBox)dg_servicelocations.Items[i].FindControl("Chk_Attach");
                    hdnDeliveryAreaID = (HiddenField)dg_servicelocations.Items[i].FindControl("hdnDeliveryAreaID");

                    for (int j = 0; j < serviceLocatonsArray.Length; j++)
                    {
                        if (hdnDeliveryAreaID.Value == serviceLocatonsArray[j].ToString())
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
        DeliveryAreaIDs = "";
        int serviceLocationGridCount = dg_servicelocations.Items.Count;
        CheckBox Chk_Attach;
        HiddenField hdnDeliveryAreaID;

        for (int i = 0; i < serviceLocationGridCount; i++)
        {
            Chk_Attach = (CheckBox)dg_servicelocations.Items[i].FindControl("Chk_Attach");
            hdnDeliveryAreaID = (HiddenField)dg_servicelocations.Items[i].FindControl("hdnDeliveryAreaID");

            if (Chk_Attach.Checked)
            {
                if (DeliveryAreaIDs == "")
                    DeliveryAreaIDs = hdnDeliveryAreaID.Value;
                else
                    DeliveryAreaIDs = DeliveryAreaIDs + "," + hdnDeliveryAreaID.Value;
            }
        }
        htSelectedDeliveryAreas = (Hashtable)ViewState["htSelectedDeliveryAreas"];
        htSelectedDeliveryAreas[DeliveryAreaIDs] = DeliveryAreaIDs;
        ViewState["htSelectedDeliveryAreas"] = htSelectedDeliveryAreas;

        BindGrids(3);
        CheckSelectedLR();
    }

    private void BindGrids(int callFrom)
    {
        string BranchID, MenifestTypeID;
        int MemoYear ,MemoMonth,MemoDay;

        BranchID = Request.QueryString["BranchID"].ToString();
        
        MemoYear = Util.String2Int(Request.QueryString["MemoYear"].ToString());
        MemoMonth = Util.String2Int(Request.QueryString["MemoMonth"].ToString());
        MemoDay = Util.String2Int(Request.QueryString["MemoDay"].ToString());
        DateTime PDS_Date = new DateTime(MemoYear, MemoMonth, MemoDay);

        DataSet objDS = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, BranchID), 
            objDAL.MakeInParams("@PDS_Date", SqlDbType.DateTime, 0, PDS_Date), 
            objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.VarChar, 2000, DeliveryAreaIDs)};

        objDAL.RunProc("dbo.EC_Opr_PDS_Filter_DlyAreaWise_ReadValues", objSqlParam, ref objDS);

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
