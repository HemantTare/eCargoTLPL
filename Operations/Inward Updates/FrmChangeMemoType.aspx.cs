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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;



public partial class Operations_Inward_Updates_FrmChangeMemoType : System.Web.UI.Page
{
    #region ClassVariables
    DataSet objDs = new DataSet();
    private DAL objDAL = new DAL();
    DataTable objDT = new DataTable();
    Common objcommon = new Common();

    #endregion

    #region ControlsValue
    private int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }
    private int MemoId
    {
        get { return Util.String2Int(ddl_ManifestNo.SelectedValue); }
        set { ddl_ManifestNo.SelectedValue = Util.Int2String(value); }
    }

    private string MemoTypeID
    {
        get { return hdn_MemoTypeId.Value; }
        set { hdn_MemoTypeId.Value = value; }
    }

    private string MemoPreviousTypeID
    {
        get { return hdn_MemoPreviousTypeId.Value; }
        set { hdn_MemoPreviousTypeId.Value = value; }
    }
    private string MemoFromBranchId
    {

        get { return hdn_MemoFromBranchId.Value; }
        set { hdn_MemoFromBranchId.Value = value; }
    }
    private string MemoToBranchId
    {

        get { return hdn_MemoToBranchId.Value; }
        set { hdn_MemoToBranchId.Value = value; }
    }

    private int NewAUSBranchId
    {
        get
        {
            //if (MemoTypeID == "2")
            //    return Util.String2Int(ddl_MenifestTo.SelectedValue);
            //else
            //    return 0;
            if (MemoTypeID == "1")
                return Util.String2Int(ddl_MenifestTo.SelectedValue);
            else
                return 0;
        }
        set { ddl_MenifestTo.SelectedValue = Util.Int2String(value); }
    }

    private string NewMemoToName
    {
        get
        {
            //if (MemoTypeID == "2")
            //    return ddl_MenifestTo.SelectedItem;
            //else
            //    return txt_MenifestTo.Text;
            if (MemoTypeID == "1")
                return ddl_MenifestTo.SelectedItem;
            else
                return txt_MenifestTo.Text;
        }
        set { txt_MenifestTo.Text = value; }
    }

    private string Reason
    {
        get{return txt_reason.Text;}
    }

    private void SetMenifestToId(string text, string value)
    {
        ddl_MenifestTo.DataTextField = "Branch_Name";
        ddl_MenifestTo.DataValueField = "Branch_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_MenifestTo);
    }

    private DataSet SessionMemoInformationGrid
    {
        set { StateManager.SaveState("MemoInformationGrid", value); }
        get { return StateManager.GetState<DataSet>("MemoInformationGrid"); }
    }


    private string GridXML
    {
        get
        {
            DataSet ds_attchedgc = new DataSet();
            ds_attchedgc.Tables.Add(objcommon.Get_View_Table(SessionMemoInformationGrid.Tables[1], "attached = true").ToTable());

            return ds_attchedgc.GetXml().ToLower();
        }
    }

    #endregion

    #region IView

    private bool validateUI()
    {
        bool _isValid = true;
        return _isValid;
    }

    private string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    private int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    #endregion

    #region ControlsBind

    private DataSet BindMemo
    {
        set
        {
            ddl_ManifestNo.DataSource = value;
            ddl_ManifestNo.DataValueField = "Memo_Id";
            ddl_ManifestNo.DataTextField = "Memo_No_For_Print";
            ddl_ManifestNo.DataBind();
            
        }
    }

    private void BindGrid()
    {
        dg_MemoInformation.DataSource = SessionMemoInformationGrid.Tables[1];
        dg_MemoInformation.DataBind();
    }



    #endregion

    #region OtherMethods

    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        lbl_TotalNoOfGC.Text = "Total No Of " + hdn_GCCaption.Value + ":" ;
        dg_MemoInformation.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + " No";
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        FillMemo();
        FillLabelsAndGridOnMemo();
        visibility();
    }


    private void EmptyTheTextValues()
    {
        txt_MemoDate.Text = "";
        txt_MemoFromBranch.Text = "";
        txt_MemoType.Text = "";
        txt_New_Memo_Type.Text = "";
        txt_memoToBranch.Text = "";
        txt_VehicleCategory.Text = "";
        txt_TotalNoOfGC.Text = "";
        txt_TotalNoOfLoadedArticles.Text = "";
        txt_Total_Loaded_Wt.Text = "";
        hdn_total_attached_gc.Value = "0";
        MemoTypeID = "";
        NewMemoToName = "";
        Common.SetValueToDDLSearch("", "0", ddl_MenifestTo);
        btn_Save.Enabled = false;
        txt_reason.Text = "";
    }
    #endregion

    #region OtherFunction

    private void Fill_Memo_Type()
    {
        Common objcommon = new Common();
        DataSet ds = new DataSet();
        string Query = "";

        ddlMemoType.Visible = true ; 

        Query = "select Memo_Type_Id,Memo_Type  from EC_Master_Memo_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddlMemoType.DataSource = ds;
        ddlMemoType.DataTextField = "Memo_Type";
        ddlMemoType.DataValueField = "Memo_Type_Id";
        ddlMemoType.DataBind();

    }

    private void FillMemo()
    {
        SqlParameter[] objSqlParam ={ 
                                     objDAL.MakeInParams("@vehicle_id", SqlDbType.Int, 0, VehicleID), 
                                     objDAL.MakeInParams("@hierarchy_code", SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                                     objDAL.MakeInParams("@main_id",SqlDbType.Int,0,UserManager.getUserParam().MainId)
                                     };

        objDAL.RunProc("[dbo].[ec_opr_change_memo_type_get_memos]", objSqlParam, ref objDs);
        BindMemo = objDs;
    }

    private void FillLabelsAndGridOnMemo()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@memo_id", SqlDbType.Int, 0, MemoId) };


        objDAL.RunProc("ec_opr_change_memo_type_get_gcs", objSqlParam, ref objDs);

        if (objDs.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDs.Tables[0].Rows[0];
            txt_MemoDate.Text = objDR["memo_date"].ToString();
            txt_MemoFromBranch.Text = objDR["memo_from_branch"].ToString();
            txt_MemoType.Text = objDR["memo_type"].ToString();
            txt_New_Memo_Type.Text = objDR["memo_type"].ToString();
            //txt_New_Memo_Type.Text = objDR["New_Memo_Type"].ToString();
            txt_memoToBranch.Text = objDR["memo_to_branch"].ToString();
            txt_VehicleCategory.Text = objDR["vehicle_category"].ToString();
            txt_TotalNoOfGC.Text = objDR["Total_no_Of_GC"].ToString();
            txt_TotalNoOfLoadedArticles.Text = objDR["Total_Loaded_Articles"].ToString();
            txt_Total_Loaded_Wt.Text = objDR["Total_Loaded_Weight"].ToString();
            MemoTypeID = objDR["memo_type_id"].ToString();

            MemoPreviousTypeID = objDR["memo_type_id"].ToString();
            ddlMemoType.SelectedValue = objDR["memo_type_id"].ToString();
            MemoToBranchId = objDR["memo_to_branch_id"].ToString(); 

            MemoFromBranchId = objDR["memo_branch_id"].ToString();

            visibility();
            btn_Save.Enabled = true;
        }
        else
        {
            EmptyTheTextValues();
        }
        ddl_MenifestTo.OtherColumns = MemoFromBranchId;
        SessionMemoInformationGrid = objDs;
        BindGrid();
    } 

    protected void ddlMemoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MemoTypeID = ddlMemoType.SelectedValue;
        txt_New_Memo_Type.Text = ddlMemoType.SelectedItem.Text;  
        visibility();
    }

    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_Save.Attributes.Add("onclick", objcommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        ddl_MenifestTo.DataTextField = "Branch_Name";
        ddl_MenifestTo.DataValueField = "Branch_ID";


        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);

        if (IsPostBack == false)
        {
            ddlMemoType.Visible = false; 
            SetStandardCaption();
            visibility();
            EmptyTheTextValues();
        }
        
    }

    protected void ddl_ManifestNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Memo_Type();
        FillLabelsAndGridOnMemo();        
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        UpdateDataset();
        if (Allow_To_Save() == true)
        {
            SqlParameter[] objSqlParam ={
                                     objDAL.MakeOutParams("@User_ERROR",SqlDbType.VarChar,4000),
                                     objDAL.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
                                     objDAL.MakeOutParams("@ERROR_DESC",SqlDbType.VarChar,400),
                                     objDAL.MakeInParams("@memo_id", SqlDbType.Int, 0,MemoId), 
                                     objDAL.MakeInParams("@gc_details",SqlDbType.Xml,0,GridXML),
                                     objDAL.MakeInParams("@new_AUS_branch_id",SqlDbType.Int,0,NewAUSBranchId),
                                     objDAL.MakeInParams("@New_memo_to_name",SqlDbType.VarChar,50,NewMemoToName),
                                     objDAL.MakeInParams("@NewMemoTypeID",SqlDbType.Int,0,Convert.ToInt32(ddlMemoType.SelectedValue)),
                                     objDAL.MakeInParams("@updated_by",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                                     objDAL.MakeInParams("@reason",SqlDbType.VarChar,250,Reason)
                                     };

            objDAL.RunProc("[dbo].[ec_opr_change_memo_type_Save]", objSqlParam);

            errorMessage = Convert.ToString(objSqlParam[0].Value);
            int errorcode = Convert.ToInt32(objSqlParam[1].Value);
            string errordesc = Convert.ToString(objSqlParam[2].Value);

            if (errorcode == 0)
            {
                ddl_ManifestNo.SelectedValue = "0";
                FillLabelsAndGridOnMemo();
            }
        }
    }
     
    private void UpdateDataset()
    {
        CheckBox chk;
        int i = 0;
        int Attached_GCs = 0;


        foreach (DataRow dr in SessionMemoInformationGrid.Tables[1].Rows)
        {
            chk = (CheckBox)dg_MemoInformation.Items[i].FindControl("chk_Attach");
            dr["attached"] = chk.Checked;

            if (chk.Checked == true)
                Attached_GCs = Attached_GCs + 1;
            i = i + 1;
        }
        hdn_total_attached_gc.Value = Attached_GCs.ToString();
    }

    private bool Allow_To_Save()
    {
        bool ATS = false;


        if (Util.String2Int(ddl_ManifestNo.SelectedValue)<=0)
        {
            errorMessage = "Please Select Manifest Number";
        }
        else if (MemoTypeID == "2" && NewMemoToName == "")
        {
            errorMessage = "Please Enter Manifest To Branch";
            txt_memoToBranch.Focus();
        }
        else if (MemoTypeID == "1" && NewAUSBranchId <= 0)
        {
            errorMessage = "Please Select Manifest To Branch";
        }
        else if (MemoTypeID == "1" && (MemoPreviousTypeID == MemoTypeID) && (MemoToBranchId == Convert.ToString(NewAUSBranchId)))
        {
            errorMessage = "Please Select Valid Manifest To Branch ";
        }

        //else if (MemoTypeID == "1" && NewMemoToName == "")
        //{
        //    errorMessage = "Please Enter Manifest To Branch";
        //    txt_memoToBranch.Focus();
        //}
        //else if (MemoTypeID == "2" && NewAUSBranchId <= 0)
        //{
        //    errorMessage = "Please Select Manifest To Branch";
        //}
        else if (Util.String2Int(hdn_total_attached_gc.Value) <= 0)
        {
            errorMessage = "Please Select Atleast One " + hdn_GCCaption.Value;
        }
        else if (Reason=="")
        {
            errorMessage = "Please Enter Reason";
            txt_reason.Focus();
        }
        else
            ATS = true;

        return ATS;
    }

    protected void dg_MemoInformation_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "can_edit"));
        }
    }

    private void visibility()
    {
        if (MemoTypeID == "1")
        {
            tr_ddl_memo_to.Style.Add("display", "inline");
            tr_txt_memo_to.Style.Add("display", "none");
        }
        else if (MemoTypeID == "2")
        {
            tr_ddl_memo_to.Style.Add("display", "none");
            tr_txt_memo_to.Style.Add("display", "inline");
        }
        else
        {
            tr_ddl_memo_to.Style.Add("display", "none");
            tr_txt_memo_to.Style.Add("display", "none");
        }
    }


    #endregion


}
