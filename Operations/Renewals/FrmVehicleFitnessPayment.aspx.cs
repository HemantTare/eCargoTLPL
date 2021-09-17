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

public partial class Operations_Renewals_FrmVehicleFitnessPayment : ClassLibraryMVP.UI.Page
{

    private DAL objDAL = new DAL();
    private DataSet objDS;
    Common objCommon = new Common();
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

    public DateTime FitnessDate
    {
        set
        {
            Wuc_FitnessDate.SelectedDate = value;
        }
        get
        {
            return Wuc_FitnessDate.SelectedDate;
        }
    }
    public DateTime ValidUpTo
    {
        set
        {
            Wuc_Valid_UpTo.SelectedDate = value;
        }
        get
        {
            return Wuc_Valid_UpTo.SelectedDate;
        }
    }
    public DateTime IssueDate
    {
        set
        {
            Wuc_Issue_Date.SelectedDate = value;
        }
        get
        {
            return Wuc_Issue_Date.SelectedDate;
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

    public string FitnessCertificateNo
    {
        set { txt_Fitness_Certificate_No.Text = value; }
        get { return txt_Fitness_Certificate_No.Text; }
    }
    public decimal Amount
    {
        set { txt_Amount.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Amount.Text); }
    }
    public string ChequeNo
    {
        set { txt_Cheque_No.Text = value; }
        get { return txt_Cheque_No.Text; }
    }
    public string Fitness_No
    {
        set { lbl_Fitness_No.Text = value; }
        get { return lbl_Fitness_No.Text; }
    }
    public int RTO_ID
    {
        set { ddl_RTO.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_RTO.SelectedValue); }
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
    public int Renewal_ID
    {
        set { hdn_Renewal_ID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Renewal_ID.Value); }
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
    public DataTable Bind_ddl_RTO
    {
        set
        {
            ddl_RTO.DataTextField = "RTO_City";
            ddl_RTO.DataValueField = "RTO_ID";
            ddl_RTO.DataSource = value;
            ddl_RTO.DataBind();
            ddl_RTO.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion
    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (objCommon.IsValidDate(FitnessDate) == false)
        {
            errorMessage = "Please Enter Valid Date";
            Wuc_FitnessDate.Focus();
        }
        else if (VehicleID == -1)
        {
            errorMessage = "Please Enter Vehicle";
            WucVehicleSearch1.Focus();
        }
        else if (txt_Fitness_Certificate_No.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Certificate No";
            txt_Fitness_Certificate_No.Focus();
        }
        else if (Util.String2Int(ddl_RTO.SelectedValue) == 0)
        {
            errorMessage = "Please Select RTO";
            ddl_RTO.Focus();
        }
        else if (txt_Amount.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Amount";
            txt_Amount.Focus();
        }
        else if (Check_Date() == false)
        {
            _isValid = false;
            return _isValid;

        }
        else if (Check_Fitness_Payment_Date() == false)
        {
            _isValid = false;
            return _isValid;

        }
        else if (Check_Is_Cheque() == false)
        {
            if (txt_Cheque_No.Text.Trim() == string.Empty)
            {
                errorMessage = "Please Enetr Cheque No";
                txt_Cheque_No.Focus();
                _isValid = false;
                return _isValid;
            }
            else if (Util.String2Int(ddl_Bank_Name.SelectedValue) == 0)
            {
                errorMessage = "Please Select Bank Name";
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
        }
    }

    #endregion

    public int Mode
    {
        get { return Util.DecryptToInt(Request.QueryString["Mode"]); }
    }
    #region OtherMethods
    private bool Check_Date()
    {
        if (IssueDate > ValidUpTo)
        {
            errorMessage = "Issue date can not be greater than upto date";
            Wuc_Issue_Date.Focus();
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

    private bool Check_Fitness_Payment_Date()
    {
        SqlParameter[] objSqlParam = {
                                       objDAL.MakeInParams("@Fitness_Renewal_ID", SqlDbType.Int, 0, keyID),
                                       objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, VehicleID),
                                       objDAL.MakeInParams("@Fitness_IssueDate", SqlDbType.DateTime, 0, IssueDate),
                                       objDAL.MakeInParams("@Fitness_ValidUpTo", SqlDbType.DateTime, 0, ValidUpTo) 
                                          };
        objDAL.RunProc("[rstil42].[EF_Trn_FitnessPayment_GetValidFromValidUptoDate]", objSqlParam, ref objDS);

        if (Util.String2Bool(objDS.Tables[0].Rows[0]["Is_Valid_Date"].ToString()) == false)
        {
            errorMessage = "Please Enter Issue Date Greater  Than  '" + objDS.Tables[0].Rows[0]["To_Date"].ToString() + "' ";
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
        int _Vehicle_Id = 1;

        if (Request.QueryString["VehicleId"] != null)
        {
            _Vehicle_Id = Util.DecryptToInt(Request.QueryString["VehicleId"].ToString());
            VehicleID = _Vehicle_Id;
        }
        //objVehicleFitnessPaymentPresenter = new VehicleFitnessPaymentPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            FitnessDate = DateTime.Now;
            IssueDate = DateTime.Now;
            ValidUpTo = DateTime.Now;
            ChequeDate = DateTime.Now;
            Get_All_DropDown();
            initValues();
        }
        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.VehicleCategoryIds = "1,2";
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);
        if (keyID < 0)
        {
            lbl_Fitness_No.Text = objCommon.Get_Next_Number();
        }
        else
        {
            WucVehicleSearch1.SetEnabled = false;
        }
    }
    private void VehicleIndexChange(object sender, EventArgs e)
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, VehicleID)};
        objDAL.RunProc("[rstil42].[EF_Trn_FitnessPayment_FillRTOCityOnVehicleChanged]", objSqlParam, ref objDS);
        if (objDS.Tables[0].Rows.Count > 0)
        {
            RTO_ID = Util.String2Int(objDS.Tables[0].Rows[0]["Fitness_RTO_City_ID"].ToString());
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save_Details();
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
               objDAL.MakeInParams("@Fitness_Renewal_ID", SqlDbType.Int, 0, keyID ),                                               
               objDAL.MakeInParams("@Fitness_Renewal_Date", SqlDbType.DateTime, 0, FitnessDate ),
               objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
               objDAL.MakeInParams("@Fitness_Certificate_No", SqlDbType.NVarChar,50, FitnessCertificateNo),
               objDAL.MakeInParams("@Fitness_RTO_City_ID", SqlDbType.Int, 0, RTO_ID),
               objDAL.MakeInParams("@Fitness_Issue_Date", SqlDbType.DateTime, 0, IssueDate),
               objDAL.MakeInParams("@Fitness_Valid_Upto", SqlDbType.DateTime, 0, ValidUpTo),
               objDAL.MakeInParams("@Fitness_Amount", SqlDbType.Decimal, 0, Amount),
               objDAL.MakeInParams("@Is_Cheque", SqlDbType.Bit, 0, Is_Cheque),
               objDAL.MakeInParams("@Cheque_No", SqlDbType.NVarChar, 0, ChequeNo),                                                
               objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime, 0, ChequeDate),
               objDAL.MakeInParams("@Bank_ID", SqlDbType.Int, 0, Bank_ID), 
               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId)
            //,
            //   objDAL.MakeInParams("@Is_From_Transaction", SqlDbType.Bit, 0, true)
            //,
            //   objDAL.MakeInParams("@VoucherDetailsXML", SqlDbType.Xml, 0, voucherXml),
            //   objDAL.MakeInParams("@VoucherBillByBillXML", SqlDbType.Xml, 0,billByBillXml)
         };

        objDAL.RunProc("rstil42.EF_Trn_FitnessPayment_Save", objSqlParam);

        //objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        //objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }
    }
    private void Get_All_DropDown()
    {
        objDAL.RunProc("rstil42.EF_Trn_FitnessPayment_FillValues", ref objDS);
        if (objDS.Tables[0].Rows.Count > 0)
        {
            Bind_ddl_RTO = objDS.Tables[0];
            Bind_ddl_Bank_Name = objDS.Tables[1];
        }
    }
    //private void AutoVoucherDetails(ref string VoucherXml, ref string BillByBillXml)
    //{
    //    AutoVoucher objAV = new AutoVoucher();

    //    DataSet ds = objDAL.RunQuery("Select Ledger_Id from dbo.EF_Master_Vehicle Where Vehicle_ID = " + objIVehicleFitnessPaymentView.VehicleID);
    //    int ledgerID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

    //    double assValue = Convert.ToDouble(Amount);

    //    if (Is_Cheque) // if bank is selected then credit to bank else credit to cash
    //    {
    //        ds = objDAL.RunQuery("Select Ledger_Id from dbo.EC_Master_Bank Where Bank_ID = " + objIVehicleFitnessPaymentView.Bank_ID);
    //        int Bank_Ledger_ID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
    //        objAV.OtherVoucherDetails("Bank", Bank_Ledger_ID, Convert.ToDecimal(assValue), true);
    //    }
    //    else
    //        objAV.OtherVoucherDetails("Cash", "Cash_Ledger_ID", Convert.ToDecimal(assValue), true);

    //    VoucherXml = objAV.PrepareVoucherDetailsXML(ledgerID, false, assValue, 0, ref BillByBillXml);
    //}
    private void initValues()
    {
        if (keyID > 0)
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Fitness_Renewal_ID", SqlDbType.Int, 0, keyID) 
                                         };
            objDAL.RunProc("rstil42.EF_Trn_FitnessPayment_ReadValues", objSqlParam, ref objDS);

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[0].Rows[0];

                if (Convert.ToInt32(dr["Max_Fitness_Renewal_ID"]) != keyID)
                {
                    if (ClassLibraryMVP.General.Mode.EDIT == Mode)
                    {
                        Common.DisplayErrors(-3600);
                    }
                }

                Fitness_No = dr["Fitness_Renewal_No"].ToString();
                FitnessDate = Convert.ToDateTime(dr["Fitness_Renewal_Date"]);
                VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                FitnessCertificateNo = dr["Fitness_Certificate_No"].ToString();
                RTO_ID = Util.String2Int(dr["Fitness_RTO_City_ID"].ToString());
                IssueDate = Convert.ToDateTime(dr["Fitness_Issue_Date"]);
                ValidUpTo = Convert.ToDateTime(dr["Fitness_Valid_Upto"]);
                Amount = Util.String2Decimal(dr["Fitness_Amount"].ToString());
                Is_Cheque = Convert.ToBoolean(dr["Is_Cheque"]);
                ChequeNo = dr["Cheque_No"].ToString();
                ChequeDate = Convert.ToDateTime(dr["Cheque_Date"]);
                Bank_ID = Util.String2Int(dr["Bank_ID"].ToString());
            }
        }
    }
}
