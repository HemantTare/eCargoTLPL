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
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;

using Raj.EC.ControlsView;
using Raj.EC;

public partial class CommonControls_WucVehicleSearch : System.Web.UI.UserControl, IVehicleSearchView
{
    public bool isVehicleFound;
    int Vehicle_MenuItemId = 0;
    string VehicleCotegoryID;
    DateTime _manifestDate;
    bool _retainVehicle;

    public event EventHandler DDLVehicleIndexChange;
    private ScriptManager _scmTruckSearch;


    public bool RetainVehicle
    {
        set { _retainVehicle = value; }
        get { return _retainVehicle; }
    }

    public ScriptManager SetScriptManager
    {
        set { _scmTruckSearch = value; }
    }

    public int VehicleID
    {
        set
        {
            if (value == 0)
            {
                txt_Vehicle_Last_4_Digits.Text = "";
                ddl_Vehicle.Items.Clear();
            }
            if (value > 0)
            {
                BindDDlVehicle(value);
                ddl_Vehicle.SelectedValue = Util.Int2String(value);
            }
        }
        get { return Util.String2Int(ddl_Vehicle.SelectedValue); }
    }

    public string VehicleNumber
    {
        get
        {
            if (VehicleID == -1)
                return "";
            else
                return ddl_Vehicle.SelectedItem.Text;
        }
    }

    public string VehicleCategoryIds
    {
        set { hdn_vehicle_category_ids.Value = value; }
        get { return hdn_vehicle_category_ids.Value; }
    }

    public int VehicleVendorID
    {
        set { hdn_vehicle_vendor_id.Value = value.ToString(); }
        get { return Util.String2Int(hdn_vehicle_vendor_id.Value); }
    }

    public int Ledger_ID
    {
        set { hdn_Ledger_ID.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Ledger_ID.Value); }
    }

    public int Credit_Limit
    {
        set { hdn_Credit_Limit.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Credit_Limit.Value); }
    }

    public DateTime TransactionDate
    {
        set { hdnTransactionDate.Value = value.ToString(); }
        get { return Convert.ToDateTime(hdnTransactionDate.Value); }
    }

    public DataSet SessionVehicleInformation
    {
        get { return StateManager.GetState<DataSet>("VehicleInformation"); }
        set { StateManager.SaveState("VehicleInformation", value); }
    }

    public string GetVehicleParameter(string ParameterName)
    {
        string ParameterValue = "";
        DataSet _objDS;
        _objDS = SessionVehicleInformation;

        if (_objDS.Tables[0].Rows.Count > 0)
        {
            DataRow[] _objDr = _objDS.Tables[0].Select("Vehicle_ID = " + VehicleID);
            ParameterValue = _objDr[0][ParameterName].ToString();
        }

        return ParameterValue;
    }

    public bool SetAutoPostBack
    {
        set { ddl_Vehicle.AutoPostBack = value; }
    }
    public TextBox txtabc
    {
        get { return txt_Vehicle_Last_4_Digits; }
    }
    public bool SetEnabled
    {
        set
        {
            txt_Vehicle_Last_4_Digits.Enabled = value;
            btn_Vehicle_Search.Enabled = value;
            ddl_Vehicle.Enabled = value;
            lbtn_AddVehicle.Enabled = value;
        }
    }

    public bool Can_Add_Vehicle
    {
        set
        {
            td_lbtn_AddVehicle.Visible = value;
        }
        get
        {
            return td_lbtn_AddVehicle.Visible;
        }
    }

    public bool Can_View_Vehicle
    {
        set
        {
            td_lbtn_viewVehicle.Visible = value;
        }
        get
        {
            return td_lbtn_viewVehicle.Visible;
        }
    }

    private void BindDDlVehicle(int vehicleID)
    {
        DAL _objDAL = new DAL();
        DataSet _objDS = new DataSet();
        SqlParameter[] objSqlParam;

        if (hdnMenuItemID.Value == "51" || hdnMenuItemID.Value == "73") //manifest, trip memo
        {
            objSqlParam = new SqlParameter[] {_objDAL.MakeInParams("@Vehicle_Last_Four_Numbers", SqlDbType.VarChar, 5, txt_Vehicle_Last_4_Digits.Text),
        _objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, vehicleID),
        _objDAL.MakeInParams("@TransactionDate", SqlDbType.DateTime, 0, TransactionDate),
        _objDAL.MakeInParams("@LoginBranchID", SqlDbType.Int, 0, hdn_LoginBranch_Id.Value)};

            _objDAL.RunProc("EC_Opr_DailyVehicleLoadingPlan_Fill_VehicleValues", objSqlParam, ref _objDS);
        }
        else if (hdnMenuItemID.Value == "286" || hdnMenuItemID.Value == "77" || hdnMenuItemID.Value == "82") //PreDoorDeliverySheet
        { 
            objSqlParam = new SqlParameter[] {_objDAL.MakeInParams("@Vehicle_Last_Four_Numbers", SqlDbType.VarChar, 5, txt_Vehicle_Last_4_Digits.Text),
            _objDAL.MakeInParams("@BranchId", SqlDbType.Int, 0, hdn_LoginBranch_Id.Value), 
            _objDAL.MakeInParams("@TransactionDate", SqlDbType.DateTime, 0, TransactionDate)};

            if (hdnMenuItemID.Value == "286")
            { _objDAL.RunProc("EC_Opr_DDCTempoFrgt_Fill_VehicleValues", objSqlParam, ref _objDS); }
            else
            { _objDAL.RunProc("EC_Opr_PreDoorDelivery_Fill_VehicleValues", objSqlParam, ref _objDS); }
        }
        else
        {
            objSqlParam = new SqlParameter[]{ 
        _objDAL.MakeInParams("@Vehicle_Last_Four_Numbers", SqlDbType.VarChar, 5, txt_Vehicle_Last_4_Digits.Text),
        _objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, vehicleID),
        _objDAL.MakeInParams("@Vehicle_Vendor_Id", SqlDbType.Int, 0, VehicleVendorID),
        _objDAL.MakeInParams("@Vehicle_Category_IDs", SqlDbType.VarChar, 15, VehicleCategoryIds)};

            _objDAL.RunProc("rstil7.EF_Mst_Vehicle_Search_Fill_VehicleValues", objSqlParam, ref _objDS);
        }
        //if (_objDS.Tables[0].Rows.Count > 0)
        //{

        ddl_Vehicle.DataSource = _objDS;
        ddl_Vehicle.DataTextField = "Vehicle_No";
        ddl_Vehicle.DataValueField = "Vehicle_ID";
        ddl_Vehicle.DataBind();

        if (VehicleID <= 0)
        {
            ddl_Vehicle.Items.Clear();
            txt_Vehicle_Last_4_Digits.Text = "";
        }

        if (VehicleID <= 0 && _objDS.Tables[0].Rows.Count <= 0)
        {
            String popupScript = "<script language='javascript'>alert('Vehicle does not exist!');</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }

        //}
        SessionVehicleInformation = _objDS;
    }

    protected void btn_Search_Click(object sender, ImageClickEventArgs e)
    {
        if (RetainVehicle)
            BindDDlVehicle(VehicleID);
        else
            BindDDlVehicle(0);

        if (ddl_Vehicle.AutoPostBack == true)
        {
            if (DDLVehicleIndexChange != null)
                DDLVehicleIndexChange(this, e);
        }
        SetLinks();
    }

    public void callVehicleSearch()
    {
        ImageClickEventArgs e = new ImageClickEventArgs(1, 2);
        btn_Search_Click(typeof(object), e);
    }

    protected void ddl_Vehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetLinks();
        //if (ddl_Vehicle.AutoPostBack == true) DDLVehicleIndexChange(this, e);
        if (ddl_Vehicle.AutoPostBack == true)
        {
            if (DDLVehicleIndexChange != null)
                DDLVehicleIndexChange(this, e);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Vehicle_Search.Attributes.Add("onclick", "return isValidTruckNumber('" + txt_Vehicle_Last_4_Digits.ClientID + "')");
        SetLinks();
        if (Request.QueryString["Menu_Item_Id"] != null)
        {
            hdnMenuItemID.Value = Common.GetMenuItemId().ToString();
        }
        hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId);
    }

    public void Enable_Disable(bool value)
    {
        txt_Vehicle_Last_4_Digits.Enabled = value;
        ddl_Vehicle.Enabled = value;
    }

    private void SetLinks()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        //if (IsPostBack == false)
        //{
        fRights = uObj.getForm_Rights(52);
        bool can_add = fRights.canAdd();

        if (can_add == true && Can_Add_Vehicle == true)
        {
            StateManager.SaveState("QueryString", "5");
            hdn_vehicle_path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(52).AddUrl;
        }
        else
        {
            hdn_vehicle_path.Value = "";
        }

        lbtn_AddVehicle.Visible = can_add;
        //}

        if (VehicleID > 0)
        {
            VehicleCotegoryID = GetVehicleParameter("Vehicle_Category_ID");
            StateManager.SaveState("QueryString", VehicleCotegoryID);

            if (VehicleCotegoryID == "1")
                Vehicle_MenuItemId = 85;
            else if (VehicleCotegoryID == "2")
                Vehicle_MenuItemId = 86;
            else if (VehicleCotegoryID == "3")
                Vehicle_MenuItemId = 87;
            else if (VehicleCotegoryID == "5")
                Vehicle_MenuItemId = 52;

            fRights = uObj.getForm_Rights(Vehicle_MenuItemId);
            bool can_view = fRights.canRead();

            if (can_view == true && Can_View_Vehicle == true) //&& Can_View_Vehicle == true
            {
                hdn_encrypted_vehicle_id.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(Vehicle_MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(VehicleID) + "&VehicleId=" + ClassLibraryMVP.Util.EncryptInteger(VehicleID);
                lbtn_VehicleView.Visible = true;
            }
            else
            {
                hdn_encrypted_vehicle_id.Value = "";
                lbtn_VehicleView.Visible = false;
            }
        }
        else
        {
            hdn_encrypted_vehicle_id.Value = "";
            td_lbtn_viewVehicle.Visible = false;
        }
    }
}
