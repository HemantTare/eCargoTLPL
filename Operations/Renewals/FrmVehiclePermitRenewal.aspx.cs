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

public partial class Operations_Renewals_FrmVehiclePermitRenewal : ClassLibraryMVP.UI.Page
{
    private DAL objDAL = new DAL();
    DropDownList ddl_Permit_State;
    Label lbl_State_ID;
    LinkButton lbtn_Delete;
    DataSet objDS;
    int _Permit_Type_Id;
    bool isValid = false;
    Common objCommon = new Common();
    #region ControlsValues
    public int VehicleID
    {
        set{WucVehicleSearch1.VehicleID = value;}
        get{return WucVehicleSearch1.VehicleID;}
    }

    public DateTime ValidFrom
    {
        set
        {
            Wuc_Valid_From.SelectedDate = value;
            hdn_Valid_From.Value = Convert.ToString(value);
        }
        get{return Wuc_Valid_From.SelectedDate;}
    }
    public DateTime ValidUpTo
    {
        set{Wuc_Valid_Upto.SelectedDate = value;}
        get{return Wuc_Valid_Upto.SelectedDate;}
    }
    public DateTime PermitDate
    {
        set{WucPermitDate.SelectedDate = value;}
        get{return WucPermitDate.SelectedDate;}
    }
    public string Permit_Document_No
    {
        set { txt_Document_No.Text = value; }
        get { return txt_Document_No.Text; }
    }
    public string Renewal_No
    {
        set { lbl_Permit_Renewal_No.Text = value; }
        get { return lbl_Permit_Renewal_No.Text; }
    }
    public string Permit_No
    {
        set { txt_Permit_No.Text = value; }
        get { return txt_Permit_No.Text; }
    }
    public bool Is_Temporary_Permit
    {
        set { hdn_Is_Temporary_Permit.Value = Convert.ToString(value); }
        get { return Util.String2Bool(hdn_Is_Temporary_Permit.Value); }
    }
    public int Permit_Type_ID
    {
        set { ddl_Permit_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Permit_Type.SelectedValue); }
    }
    public DataSet SessionPermitGrid
    {
        get { return StateManager.GetState<DataSet>("PermitGrid"); }
        set { StateManager.SaveState("PermitGrid", value); }
    }
    public DataSet SessionCheckStatePermit
    {
        get { return StateManager.GetState<DataSet>("CheckStatePermit"); }
        set { StateManager.SaveState("CheckStatePermit", value); }
    }
    public DataTable SessionPermitState
    {
        get { return StateManager.GetState<DataTable>("PermitState"); }
        set { StateManager.SaveState("PermitState", value); }
    }
    #endregion
    #region ControlsBind
    public DataTable Bind_ddl_Permit_Type
    {
        set
        {
            ddl_Permit_Type.DataTextField = "Permit_Type";
            ddl_Permit_Type.DataValueField = "Permit_Type_ID";
            ddl_Permit_Type.DataSource = value;
            ddl_Permit_Type.DataBind();
        }
    }
    public DataTable Bind_ddl_Permit_State
    {
        set
        {
            ddl_Permit_State.DataSource = value;
            ddl_Permit_State.DataTextField = "State_Name";
            ddl_Permit_State.DataValueField = "State_ID";
            ddl_Permit_State.DataBind();
            ddl_Permit_State.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }
    public DataSet Bind_dg_Permit_States
    {
        set
        {
            SessionPermitGrid = value;
            SessionPermitGrid.Tables[0].TableName = "Table";
            dg_Permit_States.DataSource = value;
            dg_Permit_States.DataBind();
        }
    }
    #endregion
    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (objCommon.IsValidDate(PermitDate) == false)
        {
            errorMessage = "Please Select Valid Date";
            WucPermitDate.Focus();
        }
        else if (VehicleID == -1)
        {
            errorMessage = "Please Select Vehicle No";
            WucVehicleSearch1.Focus();
        }
        else if (Util.String2Int(ddl_Permit_Type.SelectedValue) == 0)
        {
            errorMessage = "Please Select Permit Type";
            scm_PermitRenewal.SetFocus(ddl_Permit_Type);
        }
        else if (txt_Permit_No.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Permit No";
            scm_PermitRenewal.SetFocus(txt_Permit_No);
        }
        else if (txt_Document_No.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Document No";
            scm_PermitRenewal.SetFocus(txt_Document_No);
        }
        else if (Check_Date() == false)
        {
            _isValid = false;
            return _isValid;
        }
        else if (Check_No_Of_State() == false)
        {
            _isValid = false;
            return _isValid;
        }
        else if (Check_Permit_Renewal_Date_StateWise() == false)
        {
            _isValid = false;
            return _isValid;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 1;
        }
    }

    public void ClearVariables()
    {
        SessionPermitGrid = null;
        SessionCheckStatePermit = null;
        SessionPermitState = null;
    }

     #endregion

    #region OtherProperties

    public int Mode
    {
        get { return Util.DecryptToInt(Request.QueryString["Mode"]); }
    }

    #endregion
    #region OtherMethods
    private bool Check_Date()
    {
        if (ValidFrom > ValidUpTo)
        {
            errorMessage = "Valid From Date Should be Less Then Valid Upto Date";
            Wuc_Valid_From.Focus();
            return false;
        }
        return true;
    }
    private bool Check_No_Of_State()
    {
        if (Permit_Type_ID == 1)
        {
            if (dg_Permit_States.Items.Count < 3)
            {
                errorMessage = "Please Insert At Least Three States For National Permit";
                return false;
            }
        }
        else if (Permit_Type_ID == 2 || Permit_Type_ID == 4)
        {
            if (dg_Permit_States.Items.Count < 1)
            {
                errorMessage = "Please Insert At Least One States For " + ddl_Permit_Type.SelectedItem.Text;
                return false;
            }
        }
        else if (Permit_Type_ID == 3)
        {
            if (dg_Permit_States.Items.Count != 1)
            {
                errorMessage = "Please Insert Only One States For Temporary Permit";
                return false;
            }
        }
        return true;
    }
    private bool Check_Permit_Renewal_Date_StateWise()
    {
        int _i;
        DataSet _ds = new DataSet();
        if (keyID > 0)
        {
            if (ValidFrom < Convert.ToDateTime(hdn_Valid_From.Value))
            {
                errorMessage = "Valid From Date Should be Less Then Valid Upto Date";
                return false;
            }
        }
        else
        {
            if (Permit_Type_ID == 3)
            {
                Is_Temporary_Permit = true;
            }

            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, VehicleID),
                                           objDAL.MakeInParams("@Temporary_Permit_ID", SqlDbType.Bit, 0, Is_Temporary_Permit)};
            objDAL.RunProc("[rstil42].[EF_Trn_PermitRenewal_GetValidFromValidUptoDate]", objSqlParam, ref _ds);

            for (_i = 0; _i < SessionPermitGrid.Tables[0].Rows.Count; _i++)
            {
                DataRow[] dr;
                dr = SessionCheckStatePermit.Tables[0].Select("State_Id=" + Util.String2Int(SessionPermitGrid.Tables[0].Rows[_i]["State_ID"].ToString()));
                if (dr.Length > 0)
                {
                    if (Convert.ToDateTime(_ds.Tables[0].Rows[0]["Permit_Valid_Upto"]) > ValidFrom)
                    {
                        errorMessage = "Please Select Valid From Date Greater Then  '" + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(_ds.Tables[0].Rows[0]["Permit_Valid_Upto"])) + "' For State : '" + SessionPermitGrid.Tables[0].Rows[_i]["State_Name"].ToString() + "' ";
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_Permit_State.SelectedValue) == 0)
        {
            errorMessage = "Please Select Permit State";
            scm_PermitRenewal.SetFocus(ddl_Permit_State);
        }
        else
            isValid = true;

        return isValid;
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = SessionPermitGrid;
        DataRow DR = null;
        ddl_Permit_State = (DropDownList)e.Item.FindControl("ddl_Permit_State");
        lbl_State_ID = (Label)e.Item.FindControl("lbl_State_ID");

        if (e.CommandName == "ADD")
        {
            DR = objDS.Tables[0].NewRow();
        }
        if (e.CommandName == "Update")
        {
            DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["State_ID"] = ddl_Permit_State.SelectedValue;
            DR["State_Name"] = ddl_Permit_State.SelectedItem.Text;
            if (e.CommandName == "ADD")
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionPermitGrid = objDS;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Is_Temporary_Permit = false;
        int _Vehicle_Id;

        if (Request.QueryString["VehicleId"] != null)
        {
            _Vehicle_Id = Util.DecryptToInt(Request.QueryString["VehicleId"].ToString());
            _Permit_Type_Id = Util.DecryptToInt(Request.QueryString["PermitTypeId"].ToString());
            Permit_Type_ID = _Permit_Type_Id; 
            VehicleID = _Vehicle_Id;
            WucVehicleSearch1.SetEnabled = false;
        }

        if (!IsPostBack)
        {
            PermitDate = DateTime.Now;
            ValidFrom = DateTime.Now;
            ValidUpTo = DateTime.Now;
            Get_DropDown();
            Fill_Grid_On_VehicleChanged();
            initValues();
        }

        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.VehicleCategoryIds = "1,2";
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);
        if (!IsPostBack)
        {
            if (keyID < 0)
            {
                VehicleIndexChange(this, e);
                lbl_Permit_Renewal_No.Text = objCommon.Get_Next_Number();
            }
            else
            {
                WucVehicleSearch1.SetEnabled = false;
                ddl_Permit_Type.Enabled = false;
            }
        }
    }

    private void Get_DropDown()
    {
        objDAL.RunProc("rstil42.EF_Trn_PermitRenewal_FillValues", ref objDS);
        Bind_ddl_Permit_Type = objDS.Tables[0];
        SessionPermitState = objDS.Tables[1];
    }

    private void initValues()
    {
        if (keyID > 0)
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Permit_Renewal_ID", SqlDbType.Int, 0, keyID)};
            objDAL.RunProc("rstil42.EF_Trn_PermitRenewal_ReadValues", objSqlParam, ref objDS);

            DataSet objDSGrid = new DataSet();
            objDSGrid.Tables.Add(objDS.Tables[1].Copy());

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[0].Rows[0];

                if (Convert.ToInt32(dr["Max_Permit_Renewal_ID"]) != keyID)
                {
                    if (ClassLibraryMVP.General.Mode.EDIT == Mode)
                    {
                        Common.DisplayErrors(-3700);
                    }
                }

                Renewal_No = dr["Permit_Renewal_No"].ToString();
                PermitDate = Convert.ToDateTime(dr["Permit_Renewal_Date"]);
                VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                Permit_Type_ID = Util.String2Int(dr["Permit_Type_ID"].ToString());
                Permit_No = dr["Permit_No"].ToString();
                Permit_Document_No = dr["Permit_Document_No"].ToString();
                ValidFrom = Convert.ToDateTime(dr["Permit_Valid_From"]);
                ValidUpTo = Convert.ToDateTime(dr["Permit_Valid_Upto"]);

                Bind_dg_Permit_States = objDSGrid;
                SessionCheckStatePermit = FillGrid();
            }
        }
    }
    public DataSet FillGrid()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, VehicleID),
                                           objDAL.MakeInParams("@Temporary_Permit_ID", SqlDbType.Bit, 0, Is_Temporary_Permit)      
                                              };
        objDAL.RunProc("[rstil42].[EF_Trn_PermitRenewal_FillGrid]", objSqlParam, ref objDS);
        return objDS;
    }
    private void VehicleIndexChange(object sender, EventArgs e)
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, VehicleID)};
        objDAL.RunProc("[rstil42].[EF_Trn_PermitRenewal_FillPermitTypeOnVehicleChanged]", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            _Permit_Type_Id = Util.String2Int(objDS.Tables[0].Rows[0]["Permit_Type_ID"].ToString());
        }

        if (_Permit_Type_Id > 0)
        {
            if (_Permit_Type_Id == 3)
            {
                Is_Temporary_Permit = true;
            }
            ddl_Permit_Type.Enabled = false;
        }

        Fill_Grid_On_VehicleChanged();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save_Details();
        }
    }
    public String PremiumRenewalDetailsXML
    {
        get
        {
            //DataSet _objDs = new DataSet();
            //_objDs.Tables.Add(SessionPermitGrid.Copy());
            
            DataView view = new DataView(SessionPermitGrid.Tables[0]);
            DataTable table2 = view.ToTable(false, "State_Id");

            DataSet ds = new DataSet();
            ds.Tables.Add(table2);
            string dsXml = ds.GetXml().ToUpper();

            return dsXml;
        }
    }

    private void Save_Details()
    {
        SqlParameter[] objSqlParam = {
           objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
           objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),   
           objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),                                      
           objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
           objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,UserManager.getUserParam().HierarchyCode),
           objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId ),
           objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
           objDAL.MakeInParams("@Permit_Renewal_ID", SqlDbType.Int, 0, keyID ),
           objDAL.MakeInParams("@Permit_Renewal_Date", SqlDbType.DateTime, 0, PermitDate ),
           objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
           objDAL.MakeInParams("@Permit_No", SqlDbType.NVarChar, 0, Permit_No),
           objDAL.MakeInParams("@Permit_Type_ID", SqlDbType.Int, 0, Permit_Type_ID ),
           objDAL.MakeInParams("@Permit_Valid_From", SqlDbType.DateTime, 0, ValidFrom),
           objDAL.MakeInParams("@Permit_Valid_Upto", SqlDbType.DateTime, 0, ValidUpTo),
           objDAL.MakeInParams("@Permit_Document_No", SqlDbType.NVarChar, 0, Permit_Document_No),
           objDAL.MakeInParams("@Permit_State_XML", SqlDbType.Xml, 0, PremiumRenewalDetailsXML),
           objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId)
           // ,
           //objDAL.MakeInParams("@Is_From_Transaction", SqlDbType.Bit, 0, true)
        };

        objDAL.RunProc("rstil42.EF_Trn_PermitRenewal_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }
    }

    protected void dg_Permit_States_EditCommand(object source, DataGridCommandEventArgs e)
    {
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_Permit_States.EditItemIndex = e.Item.ItemIndex;
        dg_Permit_States.ShowFooter = false;
        Bind_dg_Permit_States = SessionPermitGrid;
    }
    protected void dg_Permit_States_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Permit_State = (DropDownList)e.Item.FindControl("ddl_Permit_State");
                Bind_ddl_Permit_State = SessionPermitState;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Permit_State = (DropDownList)e.Item.FindControl("ddl_Permit_State");
                Bind_ddl_Permit_State = SessionPermitState;
                objDS = SessionPermitGrid;
                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];

                ddl_Permit_State.SelectedValue = DR["State_ID"].ToString();
            }
        }
    }
    protected void dg_Permit_States_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Permit_States.EditItemIndex = -1;
        dg_Permit_States.ShowFooter = true;
        SessionPermitGrid.Tables[0].TableName = "Table";
        Bind_dg_Permit_States = SessionPermitGrid;
        Bind_ddl_Permit_State = SessionPermitState;
    }
    protected void dg_Permit_States_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDS = SessionPermitGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["State_ID"];
            objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_Permit_States.EditItemIndex = -1;
                dg_Permit_States.ShowFooter = true;
                SessionPermitGrid.Tables[0].TableName = "Table";
                Bind_dg_Permit_States = SessionPermitGrid;
                Bind_ddl_Permit_State = SessionPermitState;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            lbl_Errors.Text = "Duplicate State Name";
            scm_PermitRenewal.SetFocus(ddl_Permit_State);
            return;
        }
    }
    protected void ddl_Permit_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Permit_Type_ID > 0)
        {
            if (Permit_Type_ID == 3)
            {
                Is_Temporary_Permit = true;
            }

            Fill_Grid_On_VehicleChanged();
        }
    }
    public void Fill_Grid_On_VehicleChanged()
    {
        SessionCheckStatePermit  = FillGrid();
        Bind_dg_Permit_States = objDS;

    }
    protected void dg_Permit_States_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            objDS = SessionPermitGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["State_ID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    SessionPermitGrid.Tables[0].TableName = "Table";
                    Bind_dg_Permit_States = SessionPermitGrid;
                    Bind_ddl_Permit_State = SessionPermitState;
                    dg_Permit_States.EditItemIndex = -1;
                    dg_Permit_States.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                lbl_Errors.Text = "Dulicate State Name";
                scm_PermitRenewal.SetFocus(ddl_Permit_State);
                return;
            }
        }
    }
    protected void dg_Permit_States_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionPermitGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionPermitGrid = objDS;
            dg_Permit_States.EditItemIndex = -1;
            dg_Permit_States.ShowFooter = true;
            SessionPermitGrid.Tables[0].TableName = "Table";
            Bind_dg_Permit_States = SessionPermitGrid;
            Bind_ddl_Permit_State = SessionPermitState;
        }
    }
}
