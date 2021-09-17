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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 06/10/2008
/// Description   : This Page is For Master Branch General Details
/// </summary>
/// 

public partial class Master_Branch_WucBranchGeneralDetails : System.Web.UI.UserControl, IBranchGeneralView
{
    #region ClassVariables
    BranchGeneralPresenter objBranchGeneralPresenter;
    private ScriptManager scm_BranchGeneral;
    public EventHandler OnDefaultHubSelect;
    public EventHandler OnDeliveryAtSelect;
    private int _branchID = 0;
    int i = 0;
    DAL obj;
    Common objCommon = new Common();
    #endregion

    #region ControlsValues
    public string BranchCode
    {
        set { Txt_BranchCode.Text = value; }
        get { return Txt_BranchCode.Text; }
    }
    public string BranchName
    {
        set { Txt_BranchName.Text = value; }
        get { return Txt_BranchName.Text; }
    }
    public int Branch_ID
    {
        get { return _branchID; }
        set { _branchID = value; }
    }
    public int BranchTypeID
    {
        set { DDL_BranchType.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(DDL_BranchType.SelectedValue); }
    }
    //public int SetCityID
    //{
    //    set { hdn_area.Value = Util.Int2String(value); }
    //    get { return Util.String2Int(hdn_area.Value); }
    //}
    public string STRegistrationNo
    {
        set { txt_STaxRegistrationNo.Text = value; }
        get { return txt_STaxRegistrationNo.Text; }
    }
    public int AgencyAcountID
    {
        get { return Util.String2Int(DDL_Agency.SelectedValue); }
    }
    public int MemoDestinationID
    {
        get { return Util.String2Int(DDL_MemoDestination.SelectedValue); }
    }
    public int DefaultHubID
    {
        get { return Util.String2Int(DDL_DefaultHub.SelectedValue); }
    }
    public int DeliveryAtID
    {
        get { return Util.String2Int(DDL_DeliveryAt.SelectedValue); }
    }
    public int DeliveryTypeID
    {
        set { ddl_delivery_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_delivery_Type.SelectedValue); }
    }
    public int AreaID
    {
        get { return Util.String2Int(DDL_Area.SelectedValue); }
    }
    public int RegionId
    {
        //set { ddl_Region.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Region.SelectedValue); }
    }
    public string ContactPersonName
    {
        set { Txt_ContactPerson.Text = value; }
        get { return Txt_ContactPerson.Text; }
    }
    public DateTime StartedOn
    {
        set { dtp_StartedOn.SelectedDate = value; }
        get { return dtp_StartedOn.SelectedDate; }
    }
    private Boolean EnableDisableDivision
    {
        set { pnl_Devision.Visible = value; }
    }
    public EventHandler OnCityChanged
    {
        set { WucAddress1.OnCityChanged += value; }
    }
    public void SetMemoDestinationID(string text, string value)
    {
        DDL_MemoDestination.DataTextField = "Branch_Name";
        DDL_MemoDestination.DataValueField = "Branch_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDL_MemoDestination);
    }
    public void SetAgencyAcountID(string text, string value)
    {
        DDL_Agency.DataTextField = "Ledger_Name";
        DDL_Agency.DataValueField = "Ledger_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDL_Agency);
    }
    public void SetAreaID(string text, string value)
    {
        DDL_Area.DataTextField = "Area_Name";
        DDL_Area.DataValueField = "Area_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDL_Area);
    }
    public void SetDefaultHubID(string text, string value)
    {
        DDL_DefaultHub.DataTextField = "Branch_Name";
        DDL_DefaultHub.DataValueField = "Branch_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDL_DefaultHub);
    }
    public void SetDeliveryAtID(string text, string value)
    {
        DDL_DeliveryAt.DataTextField = "Branch_Name";
        DDL_DeliveryAt.DataValueField = "Branch_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDL_DeliveryAt);
    }
    public void SetRegionId(string text, string value)
    {
        ddl_Region.DataTextField = "Region_Name";
        ddl_Region.DataValueField = "Region_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Region);
    }

    #endregion

    #region ControlsBind

    public DataTable BindBranchType
    {
        set
        {
            DDL_BranchType.DataTextField = "Branch_Type";
            DDL_BranchType.DataValueField = "Branch_Type_ID";
            DDL_BranchType.DataSource = value;
            DDL_BranchType.DataBind();
            //Raj.EC.Common.InsertItem(DDL_BranchType);
        }
    }

    public DataTable BindDeliveryType
    {
        set
        {
            ddl_delivery_Type.DataTextField = "Delivery_Type";
            ddl_delivery_Type.DataValueField = "Delivery_Type_Id";
            ddl_delivery_Type.DataSource = value;
            ddl_delivery_Type.DataBind();
            //Raj.EC.Common.InsertItem(DDL_BranchType);
        }
    }

    public DataTable BindDivision
    {
        set
        {
            chk_List_Division.DataTextField = "Division_Name";
            chk_List_Division.DataValueField = "Division_ID";
            chk_List_Division.DataSource = value;
            chk_List_Division.DataBind();
            for (i = 0; i <= value.Rows.Count - 1; i++)
            {
                if (Branch_ID <= 0)
                {
                    chk_List_Division.Items[i].Selected = true;

                }
                else
                {

                    if (Convert.ToBoolean(value.Rows[i]["Att"]) == true)
                    {
                        chk_List_Division.Items[i].Selected = true;
                    }
                }
            }
            SessionDivision = value;
        }
    }

    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }

    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("Division"); }
        set { StateManager.SaveState("Division", value); }
    }
    //public String BranchDivisionXML
    //{
    //    get
    //    {
    //        DataSet _objDs = new DataSet();
    //        //_objDs.Tables.Add(SessionDivision.Copy());

    //        DataTable dt = null;
    //        dt = new DataTable();
    //        dt.Columns.Add("Division_ID");
    //        DataRow dr;
    //        if (UserManager.getUserParam().IsDivisionReq == true)
    //        {
    //            for (i = 0; i <= chk_List_Division.Items.Count - 1; i++)
    //            {
    //                if (chk_List_Division.Items[i].Selected == true)
    //                {
    //                    dr = dt.NewRow();
    //                    dr["Division_ID"] = chk_List_Division.Items[i].Value;
    //                    dt.Rows.Add(dr);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            dr = dt.NewRow();
    //            dr["Division_ID"] = "1";
    //            dt.Rows.Add(dr);
    //        }

    //        _objDs.Tables.Add(dt);

    //        _objDs.Tables[0].TableName = "Division_Details";
    //        return _objDs.GetXml().ToLower();
    //    }
    //}

    public String BranchDivisionXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionDivision.Copy());

            //DataTable dt = null;
            //dt = new DataTable();
            //dt.Columns.Add("Division_ID");
            //DataRow dr;
            //if (UserManager.getUserParam().IsDivisionReq == true)
            //{
            //    for (i = 0; i <= chk_List_Division.Items.Count - 1; i++)
            //    {
            //        if (chk_List_Division.Items[i].Selected == true)
            //        {
            //            dr = dt.NewRow();
            //            dr["Division_ID"] = chk_List_Division.Items[i].Value;
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}
            //else
            //{
            //    dr = dt.NewRow();
            //    dr["Division_ID"] = "1";
            //    dt.Rows.Add(dr);
            //}

            //_objDs.Tables.Add(dt);

            _objDs.Tables[0].TableName = "Division_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    public DataTable MakeDTDivision()
    {
        int cnt;

        DataTable objDT;
        DataRow DR = null;
        objDT = SessionDivision;
        objDT.TableName = "Division_Details";
        objDT.Clear();

        if (UserManager.getUserParam().IsDivisionReq == true)
        {
            for (cnt = 0; cnt < chk_List_Division.Items.Count; cnt++)
            {

                if (chk_List_Division.Items[cnt].Selected == true)
                {
                    DR = objDT.NewRow();
                    DR["Division_ID"] = chk_List_Division.Items[cnt].Value;

                    objDT.Rows.Add(DR);
                }
            }
        }
        else
        {
            DR = objDT.NewRow();
            DR["Division_ID"] = UserManager.getUserParam().DivisionId;
            objDT.Rows.Add(DR);
        }
        SessionDivision = objDT;
        return objDT;
    }
    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_BranchGeneral = value; }
    }

    public bool ValidateDivision()
    {
        int i = 0;
        bool _isValid = false;
        for (int cnt = 0; cnt < chk_List_Division.Items.Count; cnt++)
        {
            if (chk_List_Division.Items[cnt].Selected)
            {
                i++;
                _isValid = true;
                MakeDTDivision();
            }
            else
            {
                //_isValid = false;
            }
        }
        return _isValid;
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        TextBox txt_MemoBranch;
        TextBox txt_Agencyaccount;
        TextBox txt_Area;

        txt_MemoBranch = (TextBox)DDL_MemoDestination.FindControl("txtBoxDDL_MemoDestination");
        txt_Agencyaccount = (TextBox)DDL_Agency.FindControl("txtBoxDDL_Agency");
        txt_Area = (TextBox)DDL_Area.FindControl("txtBoxDDL_Area");

        if (BranchCode == string.Empty)
        {
            errorMessage = "Please Enter Branch Code";
            scm_BranchGeneral.SetFocus(Txt_BranchCode);
        }
        else if (BranchName == string.Empty)
        {
            errorMessage = "Please Enter Branch Name";
            scm_BranchGeneral.SetFocus(Txt_BranchName);
        }
        else if (BranchTypeID <= 0)
        {
            errorMessage = "Please Select Branch Type"; 
            scm_BranchGeneral.SetFocus(DDL_BranchType);
        }
        else if (BranchTypeID == 2 && AgencyAcountID <= 0)
        {
            errorMessage = "Please Select Agency Account";
            scm_BranchGeneral.SetFocus(txt_Agencyaccount);
        }
        else if (ContactPersonName == string.Empty)
        {
            errorMessage = "Please Enter Contact Person";
            scm_BranchGeneral.SetFocus(Txt_ContactPerson);
        }
        else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }
        else if (AreaID <= 0)
        {
            errorMessage = "Please Select Area";
            scm_BranchGeneral.SetFocus(txt_Area);
        }
        else if (UserManager.getUserParam().IsDivisionReq == true && ValidateDivision() == false)
        {
            errorMessage = "Please Select Atleast One Division";
            _isValid = false;
        }
        else if (CheckBranchName() == false)
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 12;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    { objBranchGeneralPresenter = new BranchGeneralPresenter(this, IsPostBack); }

    protected void Page_Load(object sender, EventArgs e)
    {
        Set_All_DDLTextField();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        //objBranchGeneralPresenter = new BranchGeneralPresenter(this, IsPostBack);
        hdn_BranchId.Value = keyID.ToString();
        WucAddress1.VisibleMobileNo = false;
        WucAddress1.VisibleZone = false;
        

        if (!IsPostBack)
        {
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            //hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Branch/App_LocalResources/WucBranchGeneralDetails.ascx.resx");

            if (keyID <= 0)
            {
                SetMemoDestinationID("Same", "0");
                SetDeliveryAtID("Same", "0");
                SetDefaultHubID("Same", "0");
            }
	    if (keyID > 0)
            {
                DDL_Area.OtherColumns = RegionId.ToString();
            }
        }

        if (UserManager.getUserParam().IsDivisionReq == true)
        {
            EnableDisableDivision = true;
            DDL_Area.PostBack = true;
        }
        else
        {
            EnableDisableDivision = false;
            DDL_Area.PostBack = false;
        }

        DDL_MemoDestination.DataTextField = "Branch_Name";
        DDL_MemoDestination.DataValueField = "Branch_Id";
        DDL_MemoDestination.OtherColumns = keyID.ToString();

        DDL_DefaultHub.DataTextField = "Branch_Name";
        DDL_DefaultHub.DataValueField = "Branch_Id";
        DDL_DefaultHub.OtherColumns = keyID.ToString();

        DDL_DeliveryAt.DataTextField = "Branch_Name";
        DDL_DeliveryAt.DataValueField = "Branch_Id";
        DDL_DeliveryAt.OtherColumns = keyID.ToString();

        //Session["cityid"] = WucAddress1.CityId.ToString();

       // DDL_Area.OtherColumns = SetCityID.ToString();

        WucAddress1.SetTDCaptionWidth = "20";
        WucAddress1.SetTDDataWidth = "29";

    }

    //public void HandelsCityChangedEvent(object o, EventArgs e)
    //{
    //    if (IsPostBack)
    //    {
    //        SetAreaID("", "0");
    //        DDL_Area_TextChanged(o, e);
    //        SetCityID = Util.String2Int(WucAddress1.CityId.ToString());
    //        DDL_Area.OtherColumns = SetCityID.ToString();
    //    }
    //    UpdatePanel1.Update();
    //}

    private void Set_All_DDLTextField()
    {
        DDL_MemoDestination.DataTextField = "Branch_Name";
        DDL_MemoDestination.DataValueField = "Branch_Id";

        DDL_Agency.DataTextField = "Ledger_Name";
        DDL_Agency.DataValueField = "Ledger_Id";

        DDL_Area.DataTextField = "Area_Name";
        DDL_Area.DataValueField = "Area_Id";

        DDL_DefaultHub.DataTextField = "Branch_Name";
        DDL_DefaultHub.DataValueField = "Branch_Id";

        DDL_DeliveryAt.DataTextField = "Branch_Name";
        DDL_DeliveryAt.DataValueField = "Branch_Id";

        ddl_Region.DataTextField = "Region_Name";
        ddl_Region.DataValueField = "Region_Id";
    }

    protected void DDL_Area_TextChanged(object sender, EventArgs e)
    {
        //lbl_Errors.Text = WucAddress1.CityName;
        objBranchGeneralPresenter.fillDivisionOnAreaSelection();
        Branch_ID = 0;
        upnl_Division.Update();
    }
    protected void DDL_DefaultHub_TxtChange(object sender, EventArgs e)
    {
        if (OnDefaultHubSelect != null)
        {
            OnDefaultHubSelect((object)DefaultHubID, e);
            //upnl_defaultHub.Update();
        }

    }
    protected void DDL_DeliveryAt_TxtChange(object sender, EventArgs e)
    {
        if (OnDeliveryAtSelect != null)
        {
            OnDeliveryAtSelect((object)DeliveryAtID, e);
            //UpdatePanel1.Update();
        }
    }
    protected void DDL_MemoDestination_TxtChange(object sender, EventArgs e)
    {
        DDL_MemoDestination.DataTextField = "Branch_Name";
        DDL_MemoDestination.DataValueField = "Branch_Id";
        DDL_MemoDestination.OtherColumns = keyID.ToString();

    }
    //protected void ddl_Region_TxtChange(object sender, EventArgs e)
    //{
           
    //}
    private bool CheckBranchName()
    {
        String Query = "select Service_location_Name From EC_Master_Service_Location where Service_location_Name= '" + Txt_BranchName.Text + "' and Is_Branch = 0";
        //DataSet objds = obj.RunQuery(Query);
        DataSet objds = objCommon.EC_Common_Pass_Query(Query);
        if (objds.Tables[0].Rows.Count > 0)
        {
            errorMessage = "Service Location Name Already Exist of this Name.Please Enter Other Branch Name";
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void ddl_Region_TxtChange(object sender, EventArgs e)
    {
        TextBox Txt_Area = (TextBox)DDL_Area.FindControl("txtBoxDDL_Area");
        Txt_Area.Text = "";
        DDL_Area.OtherColumns = RegionId.ToString();
        UpdatePanel1.Update();

    }
}