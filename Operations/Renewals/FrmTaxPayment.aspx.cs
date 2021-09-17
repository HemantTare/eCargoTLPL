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

public partial class Operations_Renewals_FrmTaxPayment : ClassLibraryMVP.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    Common objCommon = new Common();
    string[] _temporaryPermit_Array = new string[2];
    int _State_ID;

    #region ControlsValues
    public int VehicleID
    {
        set
        {
            WucVehicleSearch1.VehicleID = value;
        }
        get
        {
            return WucVehicleSearch1.VehicleID;
        }
    }

    public DateTime TaxDetailsDate
    {
        set
        {
            Wuc_Tax_Details_Date.SelectedDate = value;
        }
        get
        {
            return Wuc_Tax_Details_Date.SelectedDate;
        }
    }
    public DateTime ValidUpTo
    {
        set
        {
            Wuc_Valid_Upto.SelectedDate = value;
        }
        get
        {
            return Wuc_Valid_Upto.SelectedDate;
        }
    }
    public DateTime ValidFrom
    {
        set
        {
            Wuc_Valid_From.SelectedDate = value;
        }
        get
        {
            return Wuc_Valid_From.SelectedDate;
        }
    }
    public DateTime ChequeDate
    {
        set
        {
            Wuc_Cheque_Date.SelectedDate = value;
        }
        get
        {
            return Wuc_Cheque_Date.SelectedDate;
        }
    }

    public string Receipt_No
    {
        set { txt_Receipt_No.Text = value; }
        get { return txt_Receipt_No.Text; }
    }
    public decimal Tax_Amount
    {
        set { txt_Tax_Amount.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Tax_Amount.Text); }
    }
    public string ChequeNo
    {
        set { txt_Cheque_No.Text = value; }
        get { return txt_Cheque_No.Text; }
    }
    public string Tax_Details_No
    {
        set { lbl_Tax_Details_No.Text = value; }
        get { return lbl_Tax_Details_No.Text; }
    }
    public string Permit_Type
    {
        set { lbl_Permit_Type.Text = value; }
        get { return lbl_Permit_Type.Text; }
    }
    public string Permit_No
    {
        set { lbl_Permit_No.Text = value; }
        get { return lbl_Permit_No.Text; }
    }
    public int State_ID
    {
        set
        {
            ddl_State.SelectedValue = Util.Int2String(value);
        }
        get { return Util.String2Int(ddl_State.SelectedValue); }
    }
    public int Permit_Type_Id
    {
        set { hdn_Permit_Type_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Permit_Type_Id.Value); }
    }
    public bool Is_Cheque
    {
        set
        {
            hdn_Is_Cheque.Value = Convert.ToString(value);
            if (hdn_Is_Cheque.Value == "False")
            {
                rdl_Paid_By.SelectedValue = "1";
            }
            else
            {
                rdl_Paid_By.SelectedValue = "2";
            }
        }
        get { return Convert.ToBoolean(hdn_Is_Cheque.Value); }
    }

    public int Bank_ID
    {
        set { ddl_Bank_Name.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Bank_Name.SelectedValue); }
    }

    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_Bank_Name
    {
        set
        {
            ddl_Bank_Name.DataTextField = "Bank_Name";
            ddl_Bank_Name.DataValueField = "Bank_ID";
            ddl_Bank_Name.DataSource = value;
            ddl_Bank_Name.DataBind();
            ddl_Bank_Name.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_State
    {
        set
        {
            ddl_State.Items.Clear();
            ddl_State.DataTextField = "State_Name";
            ddl_State.DataValueField = "State_Id";
            ddl_State.DataSource = value;
            ddl_State.DataBind();
            ddl_State.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (objCommon.IsValidDate(TaxDetailsDate) == false)
        {
            lbl_Errors.Text = "Please Enter Valid Date";
            Wuc_Tax_Details_Date.Focus();
        }
        else if (VehicleID == -1)
        {
            lbl_Errors.Text = "Please Enter Vehicle No";
            WucVehicleSearch1.Focus();
        }
        else if (Util.String2Int(ddl_State.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select State";
            ddl_State.Focus();
        }
        else if (txt_Tax_Amount.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Tax Amount";
            txt_Tax_Amount.Focus();
        }
        else if (txt_Receipt_No.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Tax Receipt No";
            txt_Receipt_No.Focus();
        }
        else if (Check_Date() == false)
        {
            _isValid = false;
            return _isValid;

        }
        else if (Check_Tax_Payment_Date() == false)
        {
            _isValid = false;
            return _isValid;

        }
        else if (Check_Is_Cheque() == false)
        {
            if (txt_Cheque_No.Text.Trim() == string.Empty)
            {
                lbl_Errors.Text = "Please Enter Cheque No";
                txt_Cheque_No.Focus();
                _isValid = false;
                return _isValid;
            }
            else if (Util.String2Int(ddl_Bank_Name.SelectedValue) == 0)
            {
                lbl_Errors.Text = "Please Select Bank";
                ddl_Bank_Name.Focus();
                _isValid = false;
                return _isValid;
            }
            _isValid = true;
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
            lbl_Errors.Text = "Valid From Date Should be Less Then Valid Upto Date";
            Wuc_Valid_From.Focus();
            return false;
        }
        return true;
    }
    private bool Check_Is_Cheque()
    {
        if (rdl_Paid_By.SelectedValue == "2")
        {
            Is_Cheque = true;
            return false;
        }
        else
        {
            Is_Cheque = false;
            txt_Cheque_No.Text = "";
            ddl_Bank_Name.SelectedValue = "0";
            return true;
        }
    }

    private bool Check_Tax_Payment_Date()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Permit_Renewal_Tax_ID", SqlDbType.Int, 0, keyID ),
           objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0, State_ID),
           objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, VehicleID),
           objDAL.MakeInParams("@Payment_ValidFrom", SqlDbType.DateTime, 0, ValidFrom),
           objDAL.MakeInParams("@Payment_ValidUpTo", SqlDbType.DateTime, 0, ValidUpTo)};
        objDAL.RunProc("[rstil42].[EF_Trn_TaxPayment_GetValidFromValidUptoDate]", objSqlParam, ref objDS);

        if (Util.String2Bool(objDS.Tables[0].Rows[0]["Is_Valid_Date"].ToString()) == false)
        {
            lbl_Errors.Text = "Please Enter Date Between '" + objDS.Tables[0].Rows[0]["From_Date"].ToString() + "' and '" + objDS.Tables[0].Rows[0]["To_Date"].ToString() + "'";
            return false;
        }
        else
        {
            return true;
        }

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        int _Vehicle_Id;
        if (Request.QueryString["VehicleId"] != null)
        {
            _Vehicle_Id = Util.DecryptToInt(Request.QueryString["VehicleId"].ToString());
            _State_ID = Util.DecryptToInt(Request.QueryString["StateId"].ToString());
            VehicleID = _Vehicle_Id;
            WucVehicleSearch1.SetEnabled = false;
        }
        //objTaxPaymentPresenter = new TaxPaymentPresenter(this, IsPostBack);
        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.VehicleCategoryIds = "1,2";
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);
        if (!IsPostBack)
        {
            TaxDetailsDate = DateTime.Now;
            ValidFrom = DateTime.Now;
            ValidUpTo = DateTime.Now;
            ChequeDate = DateTime.Now;
            Get_DropDown();
            initValues();

            if (keyID < 0)
            {
                VehicleIndexChange(this, e);
                lbl_Tax_Details_No.Text = objCommon.Get_Next_Number();
                ddl_State.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                WucVehicleSearch1.SetEnabled = false;
            }
        }
    }
    private void Get_DropDown()
    {
        objDAL.RunProc("rstil42.EF_Trn_TaxPayment_FillValues", ref objDS);
        Bind_ddl_Bank_Name = objDS.Tables[0];
    }
    public void Get_State_On_VehicleChanged()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, VehicleID) };
        objDAL.RunProc("[rstil42].[EF_Trn_TaxPayment_FillStateOnVehicleChanged]", objSqlParam, ref objDS);

        Bind_ddl_State = objDS.Tables[0];
    }
    private void VehicleIndexChange(object sender, EventArgs e)
    {
        lbl_Permit_No.Text = "";
        lbl_Permit_Type.Text = "";
        if (VehicleID > 0)
        {
            Get_State_On_VehicleChanged();
        }
        if (_State_ID > 0)
        {
            State_ID = _State_ID;
            Set_PermitNo_PermitType_On_StateChanged();
            ddl_State.Enabled = false;
        }
    }
    public void Set_PermitNo_PermitType_On_StateChanged()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0,Util.String2Int(ddl_State.SelectedValue)),
                objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, VehicleID)};
        objDAL.RunProc("[rstil42].[EF_Trn_TaxPayment_FillPermitNoPermitTypeOnStateChanged]", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            Permit_No = objDS.Tables[0].Rows[0]["Permit_No"].ToString();
            Permit_Type = objDS.Tables[0].Rows[0]["Permit_Type"].ToString();
            Permit_Type_Id = Util.String2Int(objDS.Tables[0].Rows[0]["Permit_Type_Id"].ToString());
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save_Details();
        }  
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_PermitNo_PermitType_On_StateChanged();
    }
    private void initValues()
    {
        if (keyID > 0)
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@Permit_Renewal_Tax_ID", SqlDbType.Int, 0, keyID)};
            objDAL.RunProc("rstil42.EF_Trn_TaxPayment_ReadValues", objSqlParam, ref objDS);

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[0].Rows[0];
                DataRow dr1 = objDS.Tables[1].Rows[0];

                if (Convert.ToInt32(dr["Max_Permit_Renewal_Tax_ID"]) != keyID)
                {
                    if (ClassLibraryMVP.General.Mode.EDIT == Mode)
                    {
                        Common.DisplayErrors(-4100);
                    }
                }

                Tax_Details_No = dr["Permit_Renewal_Tax_No"].ToString();
                TaxDetailsDate = Convert.ToDateTime(dr["Permit_Renewal_Tax_Date"]);
                VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                Get_State_On_VehicleChanged();
                State_ID = Util.String2Int(dr["State_Id"].ToString());
                Tax_Amount = Util.String2Decimal(dr["Tax_Amount"].ToString());

                Receipt_No = dr["Receipt_No"].ToString();
                ValidFrom = Convert.ToDateTime(dr["Valid_From"]);
                ValidUpTo = Convert.ToDateTime(dr["Valid_Upto"]);
                Is_Cheque = Convert.ToBoolean(dr["Is_Cheque"]);
                ChequeNo = dr["Cheque_No"].ToString();
                ChequeDate = Convert.ToDateTime(dr["Cheque_Date"]);
                Bank_ID = Util.String2Int(dr["Bank_ID"].ToString());

                Permit_Type_Id = Util.String2Int(dr1["Permit_Type_ID"].ToString());
                Permit_Type = dr1["Permit_Type"].ToString();
                Permit_No = dr1["Permit_No"].ToString();
            }
        }
    }
    private void Save_Details()
    {
        //Message objMessage = new Message();

        //Prepares a voucherdetails and bill by bill xml
        //for creating an auto voucher .
        //string voucherXml = "", billByBillXml = "";
        //AutoVoucherDetails(ref voucherXml, ref billByBillXml);

        SqlParameter[] objSqlParam = {                                             
                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),   
                               objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Common.GetMenuItemId()),                                      
                               objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                               objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,UserManager.getUserParam().HierarchyCode),
                               objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId ),                                                                                    
                               objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),                                  
                               objDAL.MakeInParams("@Permit_Renewal_Tax_ID", SqlDbType.Int, 0, keyID ),                                               
                               objDAL.MakeInParams("@Permit_Renewal_Tax_Date", SqlDbType.DateTime, 0, TaxDetailsDate ),
                               objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),                                               
                               objDAL.MakeInParams("@Permit_Type_ID", SqlDbType.Int, 0, Permit_Type_Id),  
                               objDAL.MakeInParams("@State_ID", SqlDbType.Int, 0, State_ID),   
                               objDAL.MakeInParams("@Receipt_No", SqlDbType.NVarChar,50, Receipt_No),                                               
                               objDAL.MakeInParams("@Valid_From", SqlDbType.DateTime, 0, ValidFrom),
                               objDAL.MakeInParams("@Valid_Upto", SqlDbType.DateTime, 0, ValidUpTo),
                               objDAL.MakeInParams("@Tax_Amount", SqlDbType.Decimal, 0, Tax_Amount),
                               objDAL.MakeInParams("@Is_Cheque", SqlDbType.Bit, 0, Is_Cheque),
                               objDAL.MakeInParams("@Cheque_No", SqlDbType.NVarChar, 0, ChequeNo),                                                
                               objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime, 0, ChequeDate),
                               objDAL.MakeInParams("@Bank_ID", SqlDbType.Int, 0, Bank_ID), 
                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId)
            //,
            //                   objDAL.MakeInParams("@Is_From_Transaction", SqlDbType.Bit, 0, true)
            //,
            //                   objDAL.MakeInParams("@VoucherDetailsXML ", SqlDbType.Xml, 0, voucherXml),
            //                   objDAL.MakeInParams("@VoucherBillByBillXML ", SqlDbType.Xml, 0, billByBillXml )
                            };

        objDAL.RunProc("rstil42.EF_Trn_TaxPayment_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }
    }
}
