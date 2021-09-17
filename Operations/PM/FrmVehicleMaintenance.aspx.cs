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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;

public partial class Operations_PM_FrmVehicleMaintenance : ClassLibraryMVP.UI.Page
{
    int Branch_ID, Vendor_ID;
    string Vendor_Name;
    private DAL objDAL = new DAL();
    private DataSet objDS;
    Common Commonobj = new Common();
    private bool FireDDLVendorLocationTextChange = true;
    int i = 0;

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    private int MenuItemID
    {
        get
        {
            hdn_menuitem_id.Value = Common.GetMenuItemId().ToString();
            return Common.GetMenuItemId();
        }
    }
    public string TransactionNo
    {
        set{lbl_TransactionNo.Text = value;}
    }

    public DateTime TransactionDate
    {
        set{dtp_Transaction_Date.SelectedDate = value;}
        get{return dtp_Transaction_Date.SelectedDate;}
    }
    public int VehicleID
    {
        set{WucVehicleSearch2.VehicleID = value;}
        get{return WucVehicleSearch2.VehicleID;}
    }
    public int Odometer
    {
        set{txt_odometer.Text = Util.Int2String(value);}
        get{return Util.String2Int(txt_odometer.Text);}
    }

    public int ToBeWorkedAT
    {
        set { rblTobeWorkedAt.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(rblTobeWorkedAt.SelectedValue); }
    }

    public int CompanyWorkShop
    {
        set { rblWorkShop.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(rblWorkShop.SelectedValue); }
    }

    public int DDLVendorLocationSelectedValue
    {
        get { return (Util.String2Int(ddl_Vendor.SelectedValue)); }
    }
    public int BranchID
    {
        get
        {
            if (ToBeWorkedAT == 0)
                return (Util.String2Int(ddl_Vendor.SelectedValue));
            else
                return 0;
        }
    }

    public int VendorID
    {
        get
        {
            if (ToBeWorkedAT == 1)
                return (Util.String2Int(ddl_Vendor.SelectedValue));
            else
                return 0;
        }
    }
    private int RepairServiceCategoryID
    {
        set { ddl_Repair_Service_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Repair_Service_Category.SelectedValue); }
    }

    private string RepairServiceCategory
    {
        get { return ddl_Repair_Service_Category.SelectedItem.Text; }
    }

    private int RepairServiceID
    {
        set { ddl_Repair_Service.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Repair_Service.SelectedValue); }
    }

    private string RepairService
    {
        get { return ddl_Repair_Service.SelectedItem.Text; }
    }

    private int ServiceAgainstID
    {
        set { ddl_Service_Against.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Service_Against.SelectedValue); }
    }

    private string ServiceAgainst
    {
        get { return ddl_Service_Against.SelectedItem.Text; }
    }
    private decimal LabourCost
    {
        set { txt_LabourCost.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_LabourCost.Text); }
    }
    private decimal LabourDiscountAmount
    {
        set{txt_LabourDiscount.Text = Util.Decimal2String(value);}
        get { return Util.String2Decimal(txt_LabourDiscount.Text); }
    }
    private decimal TotalLabourAmount
    {
        set{
            txt_TotalLabourCast.Text = Util.Decimal2String(value);
            hdn_TotalLabourCast.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalLabourCast.Value); }
    }
    private decimal PartCost
    {
        set { txt_PartCost.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_PartCost.Text); }
    }
    private decimal PartDiscountAmount
    {
        set { txt_PartDiscount.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_PartDiscount.Text); }
    }
    private decimal TotalPartAmount
    {
        set { 
            txt_TotalPartCast.Text = Util.Decimal2String(value);
            hdn_TotalPartCast.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalPartCast.Value); }
    }
    private decimal TotalAmount
    {
        set { 
            txt_TotalCast.Text = Util.Decimal2String(value);
            hdn_TotalCast.Value = Util.Decimal2String(value); 
        }
        get { return Util.String2Decimal(hdn_TotalCast.Value); }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    private string InvoiceNo
    {
        set { txt_InvoiceNo.Text = value; }
        get { return txt_InvoiceNo.Text; }
    }

    public DataTable BindDDLRepairServiceCategory
    {
        set
        {
            ddl_Repair_Service_Category.DataSource = value;
            ddl_Repair_Service_Category.DataValueField = "Service_Category_ID";
            ddl_Repair_Service_Category.DataTextField = "Service_Category";
            ddl_Repair_Service_Category.DataBind();
            ddl_Repair_Service_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionRepairService
    {
        get { return StateManager.GetState<DataTable>("RepairService"); }
        set { StateManager.SaveState("RepairService", value); }
    }

    private void BindDDLRepairService()
    {
        string FE = "Service_Category_ID = " + Util.String2Int(ddl_Repair_Service_Category.SelectedValue);

        ddl_Repair_Service.DataSource = Commonobj.Get_View_Table(SessionRepairService, FE);
        ddl_Repair_Service.DataValueField = "Service_ID";
        ddl_Repair_Service.DataTextField = "Service_Name";
        ddl_Repair_Service.DataBind();
        ddl_Repair_Service.Items.Insert(0, new ListItem("Select One", "0"));
    }

    public DataTable BindDDLServiceAgainst
    {
        set
        {
            ddl_Service_Against.DataSource = value;
            ddl_Service_Against.DataValueField = "service_Against_id";
            ddl_Service_Against.DataTextField = "service_Against";
            ddl_Service_Against.DataBind();
            ddl_Service_Against.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }




    public DataTable BindServiceTask
    {
        set
        {
            chk_List_ServiceTask.DataTextField = "ServiceTask_Name";
            chk_List_ServiceTask.DataValueField = "ServiceTask_ID";
            chk_List_ServiceTask.DataSource = value;
            chk_List_ServiceTask.DataBind();
            for (i = 0; i <= value.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(value.Rows[i]["Att"]) == true)
                {
                    chk_List_ServiceTask.Items[i].Selected = true;
                }
            }
            SessionServiceTask = value;
            upnl_chk_List_ServiceTask.Update();
        }
    }
    public DataTable SessionServiceTask
    {
        get { return StateManager.GetState<DataTable>("ServiceTask"); }
        set { StateManager.SaveState("ServiceTask", value); }
    }
    public String BranchServiceTaskXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataTable dt = null;
            dt = new DataTable();
            dt.Columns.Add("ServiceTask_ID");
            DataRow dr;
            for (i = 0; i <= chk_List_ServiceTask.Items.Count - 1; i++)
            {
                if (chk_List_ServiceTask.Items[i].Selected == true)
                {
                    dr = dt.NewRow();
                    dr["ServiceTask_ID"] = chk_List_ServiceTask.Items[i].Value;
                    dt.Rows.Add(dr);
                }
            }

            _objDs.Tables.Add(dt);

            _objDs.Tables[0].TableName = "ServiceTask_Details";
            return _objDs.GetXml().ToLower();
        }
    }


    private void FillValues()
    {
        objDAL.RunProc("[rstil7].[EF_Trn_WorkOrder_Labour_Externel_Service_FillValues]", ref objDS);
        if (objDS.Tables.Count > 0)
        {
            BindDDLRepairServiceCategory = objDS.Tables[0];
            SessionRepairService = objDS.Tables[1];
            BindDDLServiceAgainst = objDS.Tables[2];
        }
    }

    private void FillServiceTask()
    {

        SqlParameter[] objSqlParam = {
        objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0,keyID),
        objDAL.MakeInParams("@Service_ID", SqlDbType.Int, 0,RepairServiceID)};

        objDAL.RunProc("[dbo].[EF_Opr_Fill_Vehicle_Maintainance_ServiceTask]",objSqlParam, ref objDS);

        if (objDS.Tables.Count > 0)
        {
            BindServiceTask = objDS.Tables[0];
        }
    }


    public bool validateUI()
    {
        bool _isValid = false;

        if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Enter Vehicle No.";
            WucVehicleSearch2.Focus();
        }
        else if (Odometer <= 0)
        {
            lbl_Errors.Text = "Enter Current Odometer";
            txt_odometer.Focus();
        }
        else if (Util.String2Int(rblWorkShop.SelectedValue) < 0 )
        {
            lbl_Errors.Text = "Select Worked AT";
            rblWorkShop.Focus();
        }
        else if (Util.String2Int(rblTobeWorkedAt.SelectedValue) == 0 && BranchID <= 0)
        {
            lbl_Errors.Text = "Select Worked AT Branch";
            ddl_Vendor.Focus();
        }
        else if (Util.String2Int(rblTobeWorkedAt.SelectedValue) == 1 && VendorID  <= 0)
        {
            lbl_Errors.Text = "Select Worked AT Vendor";
            ddl_Vendor.Focus();
        }
        else if (Util.String2Int(ddl_Service_Against.SelectedValue) <= 0)
        {
            lbl_Errors.Text = "Select Service Against";
            ddl_Service_Against.Focus();
        }
        else if (Util.String2Int(ddl_Repair_Service.SelectedValue) <= 0)
        {
            lbl_Errors.Text = "Select Repair Service";
            ddl_Repair_Service.Focus();
        }
        else
        {
            _isValid = true;
            lbl_Errors.Text = "";
        }

        return _isValid;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (MenuItemID != 10067)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
            WucVehicleSearch2.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

            WucVehicleSearch2.SetAutoPostBack = true;
            WucVehicleSearch2.VehicleCategoryIds = "1,2";

            changesForDDlSearch();
            InitialSettings();
        }

        if (IsPostBack == false && keyID <= 0 && Request.QueryString["VehicleID"] != null)
        {

            VehicleID = Util.DecryptToInt(Request.QueryString["VehicleID"].ToString());
            Branch_ID = Util.DecryptToInt(Request.QueryString["BranchID"].ToString());
            Vendor_ID = Util.DecryptToInt(Request.QueryString["VendorID"].ToString());
            Vendor_Name = Util.DecryptToString(Request.QueryString["VendorName"].ToString());

            CompanyWorkShop = Util.DecryptToInt(Request.QueryString["CompanyWorkShop"].ToString());

            RepairServiceCategoryID = Util.DecryptToInt(Request.QueryString["ServiceCategoryId"].ToString());

            ddl_Repair_Service_Category_SelectedIndexChanged(this, e);

            RepairServiceID = Util.DecryptToInt(Request.QueryString["ServiceId"].ToString());

            FillServiceTask();

            SetVendorID(Vendor_Name, Util.Int2String(Vendor_ID));

            ddl_Repair_Service_Category.Enabled = false;
            ddl_Repair_Service.Enabled = false;

            WucVehicleSearch2.SetEnabled = false;
            VehicleIndexChange(sender, e);

            if (Branch_ID > 0 && Vendor_ID <= 0)
            {
                ToBeWorkedAT = 0;
            }
            else if (Vendor_ID > 0 && Branch_ID <= 0)
            {
                ToBeWorkedAT = 1;
            }

            changesForDDlSearch();
            InitialSettings();
        }
        else
        {
            if (IsPostBack == false)
            {
                initValues();
            }
        }
    }

    private void VehicleIndexChange(object o, EventArgs e)
    {
        Odometer = Util.String2Int(WucVehicleSearch2.GetVehicleParameter("Current_Odometer"));
    }
    private void InitialSettings()
    {
        if (IsPostBack == false)
        {
            FillValues();
            BindDDLRepairService();
            if (keyID <= 0)
            {
               lbl_TransactionNo.Text = Commonobj.Get_Next_Number();
            }
            else
            {
                WucVehicleSearch2.SetEnabled = false;
            }
        }
    }
    private void changesForDDlSearch()
    {
        if (ToBeWorkedAT == 0)
        {
            lbl_VendorLocation_Caption.Text = "Location:";
            ddl_Vendor.CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetWorkOrderBranchName";
        }
        else if (ToBeWorkedAT == 1)
        {
            lbl_VendorLocation_Caption.Text = "Vendor:";
            ddl_Vendor.OtherColumns = "11";
            ddl_Vendor.CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetSearchVendor";
        }
    }

    public void SetVendorID(string text, string value)
    {
        if (ToBeWorkedAT == 0)
        {
            ddl_Vendor.DataTextField = "Branch_Name";
            ddl_Vendor.DataValueField = "Branch_Id";
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Vendor);
        }
        else if (ToBeWorkedAT == 1)
        {
            ddl_Vendor.DataTextField = "Vendor_Name";
            ddl_Vendor.DataValueField = "Vendor_ID";
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Vendor);
        }
    }
    public void initValues()
    {
        if (keyID > 0)
        {
            SqlParameter[] sqlPara ={ objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, keyID) };
            objDAL.RunProc("[dbo].[EF_Opr_Vehicle_Maintainance_ReadValues]", sqlPara, ref objDS);
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                TransactionNo = objDR["Transaction_No"].ToString();
                TransactionDate = Convert.ToDateTime(objDR["Transaction_Date"].ToString());

                Odometer = Util.String2Int(objDR["Odometer"].ToString());
                Remarks = objDR["Remarks"].ToString();
                ToBeWorkedAT = Util.String2Int(objDR["WorkedAt"].ToString());
                CompanyWorkShop = Util.String2Int(objDR["WorkedAtCompanyWorkShop"].ToString());

                if (Util.String2Int(objDR["Work_By_Branch_Id"].ToString()) > 0)
                {
                    ToBeWorkedAT = 0;
                    SetVendorID(objDR["Branch_Name"].ToString(), objDR["Work_By_Branch_Id"].ToString());
                }
                else if (Util.String2Int(objDR["Work_By_Vendor_Id"].ToString()) > 0)
                {
                    ToBeWorkedAT = 1;
                    SetVendorID(objDR["Vendor_Name"].ToString(), objDR["Work_By_Vendor_Id"].ToString());
                }


                RepairServiceCategoryID = Util.String2Int(objDR["Service_Category_ID"].ToString());
                BindDDLRepairService();
                RepairServiceID = Util.String2Int(objDR["Service_ID"].ToString());
                ServiceAgainstID = Util.String2Int(objDR["Service_Against_ID"].ToString());

                LabourCost = Util.String2Decimal(objDR["Labour_Cost"].ToString());
                LabourDiscountAmount = Util.String2Decimal(objDR["Labour_Discount"].ToString());
                TotalLabourAmount = Util.String2Decimal(objDR["Total_Labour_Cost"].ToString());
                PartCost = Util.String2Decimal(objDR["Parts_Cost"].ToString());
                PartDiscountAmount = Util.String2Decimal(objDR["Parts_Discount"].ToString());
                TotalPartAmount = Util.String2Decimal(objDR["Total_Parts_Cost"].ToString());
                TotalAmount = Util.String2Decimal(objDR["Total_Cost"].ToString());

                InvoiceNo = objDR["InvoiceNo"].ToString();

                changesForDDlSearch();

                FillServiceTask();

                if (ServiceAgainstID == 4)
                {
                    txt_LabourCost.Enabled = false;
                    txt_PartCost.Enabled = false;
                }
                else
                {
                    txt_LabourCost.Enabled = true;
                    txt_PartCost.Enabled = true;
                }
            }
        }
    }

    protected void rblTobeWorkedAt_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)ddl_Vendor.Controls[0];
        txt.Text = "";
        changesForDDlSearch();
        FireDDLVendorLocationTextChange = false;
    }
    protected void ddl_Vendor_OnTxtChange(object sender, EventArgs e)
    {
        if (FireDDLVendorLocationTextChange == true)
        {
            changesForDDlSearch();
        }
    }
    protected void ddl_Repair_Service_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDDLRepairService();
        FillServiceTask();
    }

    protected void ddl_Repair_Service_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillServiceTask();
    }

    protected void ddl_Service_Against_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_Service_Against.SelectedValue == "4")
        {
            txt_LabourCost.Text = "0";
            txt_PartCost.Text="0";

            txt_TotalLabourCast.Text = "0";
            txt_TotalCast.Text = "0";

            hdn_TotalLabourCast.Value = "0";
            hdn_TotalPartCast.Value = "0";
            hdn_TotalCast.Value = "0";

            txt_LabourCost.Enabled = false;
            txt_PartCost.Enabled = false;
        }
        else
        {
            txt_LabourCost.Enabled = true;
            txt_PartCost.Enabled = true;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save_Details();
        }
    }
    private void Save_Details()
    {
        //Message objMessage = new Message();
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Common.GetMenuItemId()),

            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0,0),

            objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0,keyID),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime, 0,TransactionDate),

            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,VehicleID),
            objDAL.MakeInParams("@Odometer", SqlDbType.Int, 0,Odometer),
            objDAL.MakeInParams("@Work_At", SqlDbType.Int, 0,ToBeWorkedAT),
            objDAL.MakeInParams("@Work_By_Branch_ID", SqlDbType.Int, 0,BranchID),
            objDAL.MakeInParams("@Work_By_Vendor_ID", SqlDbType.Int, 0,VendorID),
            objDAL.MakeInParams("@CompanyWorkShop", SqlDbType.Int, 0,CompanyWorkShop),

            objDAL.MakeInParams("@ServiceCategory_ID", SqlDbType.Int, 0,RepairServiceCategoryID),
            objDAL.MakeInParams("@Service_ID", SqlDbType.Int, 0,RepairServiceID),
            objDAL.MakeInParams("@Service_Against_ID", SqlDbType.Int, 0,ServiceAgainstID),

            objDAL.MakeInParams("@Labour_Cost", SqlDbType.Decimal, 0,LabourCost),
            objDAL.MakeInParams("@Part_Cost", SqlDbType.Decimal, 0,PartCost),
            objDAL.MakeInParams("@Labour_Discount", SqlDbType.Decimal, 0,LabourDiscountAmount),
            objDAL.MakeInParams("@Part_Discount", SqlDbType.Decimal, 0,PartDiscountAmount),
            objDAL.MakeInParams("@Total_Labour_Cost", SqlDbType.Decimal, 0,TotalLabourAmount),
            objDAL.MakeInParams("@Total_Part_Cost", SqlDbType.Decimal, 0,TotalPartAmount),
            objDAL.MakeInParams("@Total_Cost", SqlDbType.Decimal, 0,TotalAmount),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,Remarks),
            objDAL.MakeInParams("@Document_Status_ID", SqlDbType.Int, 0,0),
            objDAL.MakeInParams("@ServiceTask_Details", SqlDbType.Xml , 0,BranchServiceTaskXML),
            objDAL.MakeInParams("@InvoiceNo", SqlDbType.VarChar, 20,InvoiceNo),
            objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0,UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EF_Opr_Vehicle_Maintainance_Save", objSqlParam);

        //objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        //objMessage.message = Convert.ToString(objSqlParam[1].Value);
        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }
    }
}
