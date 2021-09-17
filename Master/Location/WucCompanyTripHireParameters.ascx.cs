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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;


public partial class Master_Location_WucCompanyTripHireParameters : System.Web.UI.UserControl,ICompanyTripHireParametersView 
{
    #region ClassVariable
    CompanyTripHireParametersPresenter objTripHireParametersPresenter;
    private ScriptManager scm_CompanyParameter;
    ClassLibrary.UIControl.DDLSearch ddl_TruckHireExpenseAmountLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_TDSLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_LoadingChargesName;
    ClassLibrary.UIControl.DDLSearch ddl_FuelExpenseLedgerName;    
    DropDownList ddl_BookingType;
    DataSet objDS;
    DataTable objDT;
    DataTable objDT1;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    LinkButton lbtn_Delete;    
    DataSet objTripHireDS;
    Label lbl_DivisionID, lbl_SrNo;
    bool isValid = false;
    int NextSrNo = 0;
    int TripHireNextSrNo = 0;
    #endregion

    #region ControlsValue
    public int SrNo
    {
        set
        {
            lbl_SrNo.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_SrNo.Text);
        }

    }
    public int TripHireDivisionId
    {
        set {ddl_TripHireDivision.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_TripHireDivision.SelectedValue); }
    }

    public int ATHDivisionId
    {
        set { ddl_ATHDivision.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ATHDivision.SelectedValue); }
    }
    public int LHPONatureOfPaymentId
    {
        set { ddl_LHPONatureOfPayment.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_LHPONatureOfPayment.SelectedValue); }
    }
    public DataTable SessionTripHireParametersGrid
    {
        get { return StateManager.GetState <DataTable>("TripHireParametersGrid"); }
        set
        {
            if (value != null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }

                StateManager.SaveState("TripHireParametersGrid", value);
            }
        }
    }
    public DataTable SessionATHGrid
    {
        get { return StateManager.GetState<DataTable>("ATHGrid"); }
        set
        {
            if (value != null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }

                StateManager.SaveState("ATHGrid", value);
            }
        }
    }
    public int TripExpenseLedgerId
    {
        
        get 
        {
            if (ddl_TripExpenseLedger.SelectedValue == "")
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_TripExpenseLedger.SelectedValue);
            }
        }

    }

    public bool IsTreatAdvanceForOwnTruckAsExpense
    {
        set { Chk_IsTreatAdvanceForOwnTruckAsExpense.Checked=value; }       
        get { return Chk_IsTreatAdvanceForOwnTruckAsExpense.Checked; }

    }
    //public bool IsCheckBox
    //{
    //    set { Chk_Box.Checked = 1; }
    //    get { return Chk_Box.Checked; }
    //}
    public DataTable SessionBookingType
    {
        get { return StateManager.GetState<DataTable>("BookingTypeDropDown"); }
        set { StateManager.SaveState("BookingTypeDropDown", value); }
    }
    public int TruckHireExpenseAmountLedgerId
    {
        get { return ddl_TruckHireExpenseAmountLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_TruckHireExpenseAmountLedgerName.SelectedValue); }
    }
    public int TDSLedgerId
    {
        get { return ddl_TDSLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_TDSLedgerName.SelectedValue); }
    }
    public int LoadingChargesLedgerId
    {
        get { return ddl_LoadingChargesName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_LoadingChargesName.SelectedValue); }
    }

    public string TripHireParametersDetailsXML
    {

        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionTripHireParametersGrid.Copy());
            _objDs.Tables[0].TableName = "TripHireParametersGrid";
            return _objDs.GetXml();
            //return SessionTripHireParametersGrid.GetXml();
        }
    }
    public string ATHDetailsXML
    {

        get
        {

            DataSet _objDs1 = new DataSet();
            _objDs1.Tables.Add(SessionATHGrid.Copy());
            _objDs1.Tables[0].TableName = "ATHParametersGrid";
            return _objDs1.GetXml();
           // return SessionATHGrid.GetXml();
        }
    }



    #endregion 

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
      
        if (Chk_IsTreatAdvanceForOwnTruckAsExpense.Checked == true  && TripExpenseLedgerId < 0 )
        {
           
                errorMessage = "Please Select Expense Ledger Name";
                ddl_TripExpenseLedger.Focus();              
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
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
         get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //set { keyID = value; }
        //get { return keyID; }

    }


    #endregion

    #region ControlsBind

    public DataTable BindTripHireGrid
    {
        set
        {
            SessionTripHireParametersGrid = value;
            dg_TripHireParameters.DataSource=value;
            dg_TripHireParameters.DataBind();
            
        }
    }
    public DataTable BindATHGrid
    {
        set
        {
            SessionATHGrid = value;
            dg_ATH.DataSource = value;
            dg_ATH.DataBind();
           
        }
    }
    public DataTable BindTripHireDivision
    {
        set
        {
          
            ddl_TripHireDivision.DataSource=value;
            ddl_TripHireDivision.DataTextField="Division_Name";
            ddl_TripHireDivision.DataValueField="Division_ID";
            SessionDivision = value;
            ddl_TripHireDivision.DataBind();
            ddl_TripHireDivision.Items.Insert(0, new ListItem("Select One", "0"));

           
        }
    }
    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("DivisionDropDown"); }
        set { StateManager.SaveState("DivisionDropDown", value); }
    }
    public DataTable BindLHPONatureOfPayment
    {
        set
        {

            
            ddl_LHPONatureOfPayment.DataSource = value;
            ddl_LHPONatureOfPayment.DataTextField = "TDS_Nature_Of_Payment_Name";
            ddl_LHPONatureOfPayment.DataValueField = "TDS_Nature_Of_Payment_Id";
            SessionLHPONatureOfPayment = value;
            ddl_LHPONatureOfPayment.DataBind();
            ddl_LHPONatureOfPayment.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }
    public DataTable SessionLHPONatureOfPayment
    {
        get { return StateManager.GetState<DataTable>("LHPONatureOfPayment"); }
        set { StateManager.SaveState("LHPONatureOfPayment", value); }
    }

    public DataTable BindATHDivision
    {
        set
        {
          
            ddl_ATHDivision.DataSource=value;
            ddl_ATHDivision.DataTextField="Division_Name";
            ddl_ATHDivision.DataValueField="Division_ID";
            SessionDivision = value;
            ddl_ATHDivision.DataBind();
            ddl_ATHDivision.Items.Insert(0, new ListItem("Select One", "0"));

           
        }
    }
    //public DataTable SessionATHDivision
    //{
    //    get { return StateManager.GetState<DataTable>("ATHDivision"); }
    //    set { StateManager.SaveState("ATHDivision", value); }
    //}
    public DataTable BindBookingType
    {
        set
        {
            ddl_BookingType.DataSource = value;
            ddl_BookingType.DataTextField = "Booking_Type";
            ddl_BookingType.DataValueField = "Booking_Type_Id";
            ddl_BookingType.DataBind();
            ddl_BookingType.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }    

    #endregion
    #region OtherMethods

    public void SetExpenseLedgerID(string Ledger_Name, string Ledger_Id)
    {
        ddl_TripExpenseLedger.DataTextField = "Ledger_Name";
        ddl_TripExpenseLedger.DataValueField = "Ledger_Id";

        Raj.EC.Common.SetValueToDDLSearch(Ledger_Name, Ledger_Id, ddl_TripExpenseLedger);
    }
    //TripHire Methods
    private void Insert_Update_TripHire_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        DataTable objDT = SessionTripHireParametersGrid;        
        DataRow DR = null;        
        ddl_TruckHireExpenseAmountLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TruckHireExpenseAmountLedgerName"));
        ddl_TDSLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TDSLedgerName"));
        ddl_LoadingChargesName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_LoadingChargesName"));

        ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
        lbl_DivisionID = (Label)e.Item.FindControl("lbl_DivisionID");
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataRow [] DRArray;

        if (e.CommandName == "ADD")
        {
            DR = objDT.NewRow();
            TripHireNextSrNo = AssignTripHireSrNo(0);
            //lbl_SrNo.Text = Util.Int2String(TripHireNextSrNo);

          }
        if (e.CommandName == "Update")
        {
            // DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            //DR = GetTripHireDataRow(e.Item.ItemIndex, objTripHireDS);
            TripHireNextSrNo = Util.String2Int(lbl_SrNo.Text);
            DRArray=  objDT.Select("SrNo='"+TripHireNextSrNo+"'");
            if (DRArray.Length > 0)
            {
                DR = DRArray[0];
            }
            //TripHireNextSrNo = Util.String2Int(DR["SrNo"].ToString());
        }
                 
        if (Allow_TripHire_To_Add_Update()  == true)
        {
            DR["SrNo"] = TripHireNextSrNo;
            DR["Division_ID"] = Util.String2Int(ddl_TripHireDivision.SelectedValue);
            DR["Booking_Type_ID"] = Util.String2Int(ddl_BookingType.SelectedValue);
            DR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
            DR["Truck_Hire_Expense_Ledger_Name"] = ddl_TruckHireExpenseAmountLedgerName.SelectedText;
            DR["Truck_Hire_Expense_Ledger_ID"] = Util.String2Int(ddl_TruckHireExpenseAmountLedgerName.SelectedValue);
            DR["TDS_Ledger_Name"] = ddl_TDSLedgerName.SelectedText;
            DR["TDS_Ledger_ID"] = Util.String2Int(ddl_TDSLedgerName.SelectedValue);
            DR["Loading_Charges_Ledger_Name"] = ddl_LoadingChargesName.SelectedText;
            DR["Loading_Charges_Ledger_ID"] = Util.String2Int(ddl_LoadingChargesName.SelectedValue);
            if (e.CommandName == "ADD")
            {
                objDT.Rows.Add(DR);
            }
            SessionTripHireParametersGrid = objDT;
        }
    }
    private bool Allow_TripHire_To_Add_Update()
    {
        if (Util.String2Int(ddl_TripHireDivision.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Trip Hire Division";
            ddl_TripHireDivision.Focus();
        }
        else if (Util.String2Int(ddl_BookingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Booking Type";
            ddl_BookingType.Focus();
        }
        else if (TruckHireExpenseAmountLedgerId <= 0)
        {
            errorMessage = "Please Select Truck Hire Expense Account";
            ddl_TruckHireExpenseAmountLedgerName.Focus();
        }
        else if (TDSLedgerId <= 0)
        {
            errorMessage = "Please Select TDS Ledger";
            ddl_TDSLedgerName.Focus();
        }
        else if (LoadingChargesLedgerId <= 0)
        {
            errorMessage = "Please Select Loading Charges";
            ddl_LoadingChargesName.Focus();
        }
        else
            isValid = true;

        return isValid;
    }
   
    public void SetTruckHireExpenseAmountLedgerID(string TruckHireExpenseAmountLedger_Name, string TruckHireExpenseAmountLedgerID)
    {
        ddl_TruckHireExpenseAmountLedgerName.DataTextField = "Truck_Hire_Expense_Ledger_Name";
        ddl_TruckHireExpenseAmountLedgerName.DataValueField = "Truck_Hire_Expense_Ledger_ID";
        Raj.EC.Common.SetValueToDDLSearch(TruckHireExpenseAmountLedger_Name, TruckHireExpenseAmountLedgerID, ddl_TruckHireExpenseAmountLedgerName);
    }
    public void SetTDSLedgerID(string TDSLedger_Name, string TDSLedgerID)
    {
        ddl_TDSLedgerName.DataTextField = "TDS_Ledger_Name";
        ddl_TDSLedgerName.DataValueField = "TDS_Ledger_ID";
        Raj.EC.Common.SetValueToDDLSearch(TDSLedger_Name, TDSLedgerID, ddl_TDSLedgerName);
    }
    public void SetLoadingLedgerID(string LoadingLedger_Name, string LoadingLedgerID)
    {
        ddl_LoadingChargesName.DataTextField = "Loading_Charges_Ledger_Name";
        ddl_LoadingChargesName.DataValueField = "Loading_Charges_Ledger_ID";
        Raj.EC.Common.SetValueToDDLSearch(LoadingLedger_Name, LoadingLedgerID, ddl_LoadingChargesName);
    }

    public void BindTripHireGridDetails()
    {
        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionTripHireParametersGrid, "Division_ID= '" + TripHireDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_TripHireParameters.DataSource = ds_Temp;
        dg_TripHireParameters.DataBind();
    }
    private DataRow GetTripHireDataRow(int ItemIndex, DataSet objTripHireDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionTripHireParametersGrid, "Division_ID= '" + TripHireDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);
        DataRow dr = null;
        DataRow drNew = null;
        int BookingTypeId, DivisionId;
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                BookingTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Booking_Type_ID"].ToString());
                DivisionId = Util.String2Int(ds.Tables[0].Rows[i]["Division_ID"].ToString());
                object[] o = new object[] { BookingTypeId, DivisionId };
                drNew = objTripHireDS.Tables[0].Rows.Find(o);
            }
        }
        return drNew;
    }
    private int AssignTripHireSrNo(int SrNo)
    {
        int TripHireNextSrNo = 0;
        if (SrNo == 0 && SessionTripHireParametersGrid.Rows.Count > 0)
        {
            TripHireNextSrNo = (int)SessionTripHireParametersGrid.Compute("max(SrNo)", "");
            TripHireNextSrNo = TripHireNextSrNo + 1;
        }
        else if (SessionTripHireParametersGrid.Rows.Count <= 0)
        {
            TripHireNextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            TripHireNextSrNo = (int)SessionTripHireParametersGrid.Compute("max(SrNo)", "");
        }
        return TripHireNextSrNo;
    }
    private int GetTripHireSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionTripHireParametersGrid, "Division_ID= '" + TripHireDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                SrNoForEditDeleteCancel = ds.Tables[0].Rows.IndexOf(ds.Tables[0].Rows[i]);
            }
        }
        return SrNoForEditDeleteCancel;
    }
    // End Door Methods

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataTable objDT1 = SessionATHGrid;
        DataRow DR = null;
        ddl_FuelExpenseLedgerName = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_FuelExpenseLedgerName");
        ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
        lbl_DivisionID = (Label)e.Item.FindControl("lbl_DivisionID");
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataRow[] DRArray1;
        if (e.CommandName == "ADD")
        {
            DR = objDT1.NewRow();
            NextSrNo = AssignSrNo(0);
            //lbl_SrNo.Text = Util.Int2String(NextSrNo);


        }
        if (e.CommandName == "Update")
        {
            // DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            //DR = GetDataRow(e.Item.ItemIndex, objDS);
            //DR = objDS.Tables[0].Rows[Util.String2Int(lbl_SrNo.Text)];
            NextSrNo = Util.String2Int(lbl_SrNo.Text);
            DRArray1 = objDT1.Select("SrNo='" + NextSrNo + "'");
            if (DRArray1.Length > 0)
            {
                DR = DRArray1[0];
            }
           // NextSrNo = Util.String2Int(DR["SrNo"].ToString());
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["SrNo"] = NextSrNo;
            DR["Division_ID"] = Util.String2Int(ddl_ATHDivision.SelectedValue);
            DR["Booking_Type_ID"] = Util.String2Int(ddl_BookingType.SelectedValue);
            DR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
            DR["Ledger_Name"] = ddl_FuelExpenseLedgerName.SelectedText;
            DR["Ledger_ID"] = Util.String2Int(ddl_FuelExpenseLedgerName.SelectedValue);
            if (e.CommandName == "ADD")
            {
                objDT1.Rows.Add(DR);
            }
            SessionATHGrid = objDT1;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_ATHDivision.SelectedValue) <= 0)
        {
            errorMessage = "Please Select ATH Division";
            ddl_ATHDivision.Focus();
        }
        else if (Util.String2Int(ddl_BookingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Booking Type";
            ddl_BookingType.Focus();
        }
        else
            isValid = true;

        return isValid;
    }
    public void SetFuelExpenseLedgerID(string FuelExpenseLedger_Name, string FuelExpenseLedgerID)
    {
        ddl_FuelExpenseLedgerName.DataTextField = "Ledger_Name";
        ddl_FuelExpenseLedgerName.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(FuelExpenseLedger_Name, FuelExpenseLedgerID, ddl_FuelExpenseLedgerName);
    }

    public void BindATHGridDetails()
    {
        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionATHGrid, "Division_ID= '" + ATHDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_ATH.DataSource = ds_Temp;
        dg_ATH.DataBind();

    }
    private DataRow GetDataRow(int ItemIndex, DataSet objDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionATHGrid, "Division_ID= '" + ATHDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);
        DataRow dr = null;
        DataRow drNew = null;
        int BookingTypeId, DivisionId;
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                BookingTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Booking_Type_ID"].ToString());
                DivisionId = Util.String2Int(ds.Tables[0].Rows[i]["Division_ID"].ToString());
                object[] o = new object[] { BookingTypeId, DivisionId };
                drNew = objDS.Tables[0].Rows.Find(o);
            }
        }
        return drNew;
    }
    private int AssignSrNo(int SrNo)
    {
        int NextSrNo = 0;
        if (SrNo == 0 && SessionATHGrid.Rows.Count > 0)
        {
            NextSrNo = (int)SessionATHGrid.Compute("max(SrNo)", "");
            NextSrNo = NextSrNo + 1;
        }
        else if (SessionATHGrid.Rows.Count <= 0)
        {
            NextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            NextSrNo = (int)SessionATHGrid.Compute("max(SrNo)", "");
        }
        return NextSrNo;
    }
    private int GetSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionATHGrid, "Division_ID= '" + ATHDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                SrNoForEditDeleteCancel = ds.Tables[0].Rows.IndexOf(ds.Tables[0].Rows[i]);
            }
        }
        return SrNoForEditDeleteCancel;
    }

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyParameter = value; }
    }  

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ddl_TripExpenseLedger.DataTextField = "Ledger_Name";
        ddl_TripExpenseLedger.DataValueField = "Ledger_Id";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CompanyCallBackFunction.CallBack));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        //if (Chk_IsTreatAdvanceForOwnTruckAsExpense.Checked == true)
        //{
        //    tr_ExpenseLedger.Visible = true;
            

        //}
        //else
        //{
        //    tr_ExpenseLedger.Visible = false;
            

        //}
        
        objTripHireParametersPresenter = new CompanyTripHireParametersPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            BindTripHireGridDetails();
            BindATHGridDetails();
        }
        Upd_Pnl_dg_TripHireParameters.Update();
        Upd_Pnl_ATH.Update();
    }
    protected void ddl_ATHDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindATHGridDetails();
    }
    protected void ddl_TripHireDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTripHireGridDetails();
    }

    //protected void Chk_IsTreatAdvanceForOwnTruckAsExpense_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (Chk_IsTreatAdvanceForOwnTruckAsExpense.Checked == true)
    //    {
    //        tr_ExpenseLedger.Visible = true;

    //    }
    //    else
    //    {
    //        tr_ExpenseLedger.Visible = false;
    //    }

    //}

    #region TripHireGridEvents
    protected void dg_TripHireParameters_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {

                ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));

                ddl_TruckHireExpenseAmountLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TruckHireExpenseAmountLedgerName"));
                ddl_TruckHireExpenseAmountLedgerName.DataValueField = "Truck_Hire_Expense_Ledger_ID";
                ddl_TruckHireExpenseAmountLedgerName.DataTextField = "Truck_Hire_Expense_Ledger_Name";
                ddl_TruckHireExpenseAmountLedgerName.OtherColumns = "7";

                ddl_TDSLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TDSLedgerName"));
                ddl_TDSLedgerName.DataValueField = "TDS_Ledger_ID";
                ddl_TDSLedgerName.DataTextField = "TDS_Ledger_Name";
                ddl_TDSLedgerName.OtherColumns = "8";

                ddl_LoadingChargesName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_LoadingChargesName"));
                ddl_LoadingChargesName.DataValueField = "Loading_Charges_Ledger_ID";
                ddl_LoadingChargesName.DataTextField = "Loading_Charges_Ledger_Name";
                ddl_LoadingChargesName.OtherColumns = "9";
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
                

            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindBookingType = SessionBookingType;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));

                ddl_TruckHireExpenseAmountLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TruckHireExpenseAmountLedgerName"));
                ddl_TruckHireExpenseAmountLedgerName.DataValueField = "Truck_Hire_Expense_Ledger_ID";
                ddl_TruckHireExpenseAmountLedgerName.DataTextField = "Truck_Hire_Expense_Ledger_Name";
                ddl_TruckHireExpenseAmountLedgerName.OtherColumns = "7";

                ddl_TDSLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_TDSLedgerName"));
                ddl_TDSLedgerName.DataValueField = "TDS_Ledger_ID";
                ddl_TDSLedgerName.DataTextField = "TDS_Ledger_Name";
                ddl_TDSLedgerName.OtherColumns = "8";

                ddl_LoadingChargesName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_LoadingChargesName"));
                ddl_LoadingChargesName.DataValueField = "Loading_Charges_Ledger_ID";
                ddl_LoadingChargesName.DataTextField = "Loading_Charges_Ledger_Name";
                ddl_LoadingChargesName.OtherColumns = "9";
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
                DataTable objDT= SessionTripHireParametersGrid;
                 

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionTripHireParametersGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow DR = dt.Rows[0];
                    ddl_BookingType.SelectedValue = DR["Booking_Type_ID"].ToString();
                    SetLoadingLedgerID(DR["Loading_Charges_Ledger_Name"].ToString(), DR["Loading_Charges_Ledger_ID"].ToString());
                    SetTDSLedgerID(DR["TDS_Ledger_Name"].ToString(), DR["TDS_Ledger_ID"].ToString());
                    SetTruckHireExpenseAmountLedgerID(DR["Truck_Hire_Expense_Ledger_Name"].ToString(), DR["Truck_Hire_Expense_Ledger_ID"].ToString());
                }                             

            }


        }
    }
    protected void dg_TripHireParameters_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
           DataTable objDT = SessionTripHireParametersGrid;
            Insert_Update_TripHire_Dataset(source, e);
            if (isValid == true)
            {
                dg_TripHireParameters.EditItemIndex = -1;
                dg_TripHireParameters.ShowFooter = true;
                BindTripHireGridDetails();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Booking Type";
            return;
        }
    }
    protected void dg_TripHireParameters_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
          DataTable objDT = SessionTripHireParametersGrid;
            try
            {
                Insert_Update_TripHire_Dataset(source, e);
                if (isValid == true)
                {
                    BindTripHireGridDetails();
                    dg_TripHireParameters.EditItemIndex = -1;
                    dg_TripHireParameters.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Booking Type";
                return;
            }
        }

    }
    protected void dg_TripHireParameters_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        SrNo = GetTripHireSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_TripHireParameters.EditItemIndex = SrNo;
        dg_TripHireParameters.ShowFooter = false;
        BindTripHireGridDetails();

    }
    protected void dg_TripHireParameters_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_TripHireParameters.EditItemIndex = -1;
        dg_TripHireParameters.ShowFooter = true;
        BindTripHireGridDetails();

    }

    protected void dg_TripHireParameters_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataTable objDT = SessionTripHireParametersGrid;
        DataRow dr = null;
        //dr = GetTripHireDataRow(e.Item.ItemIndex, objTripHireDS);
        //dr = objDS.Tables[0].Rows[Util.String2Int(lbl_SrNo.Text)];
        SrNo = Util.String2Int(lbl_SrNo.Text);
        dr = objDT.Rows[Util.String2Int(lbl_SrNo.Text)]; 
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionTripHireParametersGrid;
            objDT.Rows.Remove(dr);
            objDT.AcceptChanges();
            SessionTripHireParametersGrid = objDT;
            dg_TripHireParameters.EditItemIndex = -1;
            dg_TripHireParameters.ShowFooter = true;
            BindTripHireGridDetails();
        }
    }
    #endregion
    #region ATH GRIDEvents

    protected void dg_ATH_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_ATH.EditItemIndex = -1;
        dg_ATH.ShowFooter = true;
        BindATHGridDetails();
    }
    protected void dg_ATH_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataTable objDT1 = SessionATHGrid;
        DataRow dr = null;
        //dr = GetDataRow(e.Item.ItemIndex, objDS);
        //dr = objDS.Tables[0].Rows[Util.String2Int(lbl_SrNo.Text)];
        SrNo = Util.String2Int(lbl_SrNo.Text);
        dr = objDT1.Rows[Util.String2Int(lbl_SrNo.Text)]; 
        if (e.Item.ItemIndex != -1)
        {
            objDT1 = SessionATHGrid;
            objDT1.Rows.Remove(dr);
            objDT1.AcceptChanges();
            SessionATHGrid = objDT1;
            dg_ATH.EditItemIndex = -1;
            dg_ATH.ShowFooter = true;
            BindATHGridDetails();
        }
    }
    protected void dg_ATH_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        SrNo = GetSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_ATH.EditItemIndex = SrNo;
        dg_ATH.ShowFooter = false;
        BindATHGridDetails();
    }
    protected void dg_ATH_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            DataTable objDT1 = SessionATHGrid;
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindATHGridDetails();
                    dg_ATH.EditItemIndex = -1;
                    dg_ATH.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Booking Type";
                return;
            }
        }
    }
    protected void dg_ATH_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_FuelExpenseLedgerName = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_FuelExpenseLedgerName");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                ddl_FuelExpenseLedgerName.DataTextField = "Ledger_Name";
                ddl_FuelExpenseLedgerName.DataValueField = "Ledger_Id";
                ddl_FuelExpenseLedgerName.OtherColumns = "10";
            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindBookingType = SessionBookingType;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_FuelExpenseLedgerName = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_FuelExpenseLedgerName");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");

                ddl_FuelExpenseLedgerName.DataTextField = "Ledger_Name";
                ddl_FuelExpenseLedgerName.DataValueField = "Ledger_Id";
                ddl_FuelExpenseLedgerName.OtherColumns = "10";
                objDT1 = SessionATHGrid;

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionATHGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow DR = dt.Rows[0];
                    ddl_BookingType.SelectedValue = DR["Booking_Type_ID"].ToString();
                    SetFuelExpenseLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString());
                }


            }
        }
    }
    protected void dg_ATH_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
           
            DataTable objDT1= SessionATHGrid;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_ATH.EditItemIndex = -1;
                dg_ATH.ShowFooter = true;
                BindATHGridDetails();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Booking Type";
            return;
        }
    }
    #endregion

    
   
}
