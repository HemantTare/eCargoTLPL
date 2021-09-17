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
public partial class Master_Location_WucLocalCollectionVoucher : System.Web.UI.UserControl,ILocalCollectionVoucherView 
{
    #region ClassVariables
    LocalCollectionVoucherPresenter objLocalCollectionVoucherPresenter;
    DropDownList ddl_BookingType;
    ClassLibrary.UIControl.DDLSearch ddl_LocalLedger, ddl_DoorLedger;
    LinkButton lbtn_Delete ;    
    DataSet objDS;
    DataSet objDoorDS;
    DataTable objDT;
    DataTable objDT1;
    Label lbl_DivisionID, lbl_SrNo;
    private ScriptManager scm_CompanyParameter;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    int NextSrNo = 0;
    int DoorNextSrNo = 0;
    #endregion

    #region ControlsValue


    public int LocalDivisionId
    {
        set { ddl_LocalDivision.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_LocalDivision.SelectedValue); }

    }

    public int DoorDivisionId
    {
        set { ddl_DoorDivision.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_DoorDivision.SelectedValue); }

    }
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
    #endregion

    #region ControlsBind

    public DataTable Bind_dg_LocalCollectionVoucher
    {
        set
        {
            SessionLocalCollectionVoucherGrid = value;
            dg_LocalCollectionVoucher.DataSource = value;
            dg_LocalCollectionVoucher.DataBind();
        }
    }
    public DataTable Bind_dg_DoorDeliveryExpenseVoucher
    {
        set
        {
            SessionDoorDeliveryExpenseVoucherGrid = value;      
            dg_DoorDeliveryExpenseVoucher.DataSource = value;
            dg_DoorDeliveryExpenseVoucher.DataBind();
        }
    }
    public DataTable BindLocalDivision
    {
        set
        {
            ddl_LocalDivision.DataSource = value;
            ddl_LocalDivision.DataTextField = "Division_Name";
            ddl_LocalDivision.DataValueField = "Division_ID";
            SessionDivision = value;
            ddl_LocalDivision.DataBind();
            ddl_LocalDivision.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindDoorDivision
    {
        set
        {
            ddl_DoorDivision.DataSource = value;
            ddl_DoorDivision.DataTextField = "Division_Name";
            ddl_DoorDivision.DataValueField = "Division_ID";
            SessionDivision = value;
            ddl_DoorDivision.DataBind();
            ddl_DoorDivision.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindBookingType
    {
        set
        {
            ddl_BookingType.DataSource = value;
            ddl_BookingType.DataTextField = "Booking_Type";
            ddl_BookingType.DataValueField = "Booking_Type_Id";
            SessionBookingType = value;
            ddl_BookingType.DataBind();
            ddl_BookingType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable SessionLocalCollectionVoucherGrid
    {
        get { return StateManager.GetState<DataTable>("LocalCollection"); }
        set
        {
            if (value != null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }
                StateManager.SaveState("LocalCollection", value);
            }
        }
    }
    public DataTable SessionDoorDeliveryExpenseVoucherGrid
    {
        get { return StateManager.GetState<DataTable>("DoorDeliveryExpense"); }
        set
        {
            if (value != null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }

                StateManager.SaveState("DoorDeliveryExpense", value);
            }
        }
    }

    public DataTable SessionBookingType
    {
        get { return StateManager.GetState<DataTable>("BookingTypeDropDown"); }
        set { StateManager.SaveState("BookingTypeDropDown", value); }
    }
    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("DivisionDropDown"); }
        set { StateManager.SaveState("DivisionDropDown", value); }
    }
    
    public string LocalCollectionVoucherXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionLocalCollectionVoucherGrid.Copy());
            _objDs.Tables[0].TableName = "LocalCollectionVoucherGrid";
            return _objDs.GetXml().ToLower();
           // return SessionLocalCollectionVoucherGrid.GetXml().ToLower();
        }
    }
    public string DoorDeliveryExpenseVoucherXML
    {
        get
        {
            DataSet _objDs1 = new DataSet();
            _objDs1.Tables.Add(SessionDoorDeliveryExpenseVoucherGrid.Copy());
            _objDs1.Tables[0].TableName = "LocalCollectionVoucherDoorExpGrid";
            return _objDs1.GetXml();
            //return SessionDoorDeliveryExpenseVoucherGrid.GetXml();
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = true;

        //string TotalAdvance = "";
        //isValid = true;
        //if (SessionATHDetailsGrid.Tables[0].Rows.Count > 0)
        //{
        //    TotalAdvance = SessionATHDetailsGrid.Tables[0].Compute("SUM(Advance_Amount)", string.Empty).ToString();
        //    // hdn_ToalAdvanceGrid.Value = TotalAdvance;
        //}
        //if (TotalAdvance != "")
        //{
        //    if (Convert.ToDouble(TotalAdvance) > Convert.ToDouble(hdn_TotalAdvance.Value))
        //    {
        //        errorMessage = GetLocalResourceObject("Msg_AdvanceAmount").ToString();
        //        isValid = false;
        //    }
        //}
        return isValid;

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
            //return -1;
        }
    }

    #endregion


    //#region OtherProperties
    //public ScriptManager SetScriptManager
    //{
    //    set { scm_LHPOAlertsBranches = value; }
    //}
    //#endregion


    #region OtherMethods
    public ScriptManager SetScriptManager
    {
        set { scm_CompanyParameter = value; }
    }

    //Door Methods
    private void Insert_Update_Door_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDT = SessionDoorDeliveryExpenseVoucherGrid;
        DataRow DR = null;
        ddl_DoorLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_DoorLedger");
        ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
        lbl_DivisionID = (Label)e.Item.FindControl("lbl_DivisionID");
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataRow[] DRArray;
        if (e.CommandName == "ADD")
        {
            DR = objDT.NewRow();
            DoorNextSrNo = AssignDoorSrNo(0);
            //lbl_SrNo.Text = Util.Int2String(DoorNextSrNo);
        }
        if (e.CommandName == "Update")
        {
            // DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            //DR = GetDoorDataRow (e.Item.ItemIndex, objDoorDS);
           // DoorNextSrNo = Util.String2Int(DR["SrNo"].ToString());
            DoorNextSrNo = Util.String2Int(lbl_SrNo.Text);
            DRArray = objDT.Select("SrNo='" + DoorNextSrNo + "'");
            if (DRArray.Length > 0)
            {
                DR = DRArray[0];
            }

        }

        if (Allow_Door_To_Add_Update() == true)
        {
            DR["SrNo"] = DoorNextSrNo;
            DR["Division_ID"] = Util.String2Int(ddl_DoorDivision.SelectedValue);
            DR["Booking_Type_ID"] = Util.String2Int(ddl_BookingType.SelectedValue);
            DR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
            DR["Ledger_Name"] = ddl_DoorLedger.SelectedText;
            DR["Ledger_ID"] = Util.String2Int(ddl_DoorLedger.SelectedValue);
            if (e.CommandName == "ADD")
            {
                objDT.Rows.Add(DR);
            }
            SessionDoorDeliveryExpenseVoucherGrid = objDT;
        }
    }
    private bool Allow_Door_To_Add_Update()
    {
        if (Util.String2Int(ddl_DoorDivision.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Door Division";
            ddl_DoorDivision.Focus();
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
    public void SetDoorLedgerID(string DoorLedger_Name, string DoorLedgerID)
    {
        ddl_DoorLedger.DataTextField = "Ledger_Name";
        ddl_DoorLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(DoorLedger_Name, DoorLedgerID, ddl_DoorLedger);
    }

    public void BindDoorDeliveryExpenseVoucher()
    {
        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionDoorDeliveryExpenseVoucherGrid, "Division_ID= '" + DoorDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_DoorDeliveryExpenseVoucher.DataSource = ds_Temp;
        dg_DoorDeliveryExpenseVoucher.DataBind();
    }
    private DataRow GetDoorDataRow(int ItemIndex, DataSet objDoorDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionDoorDeliveryExpenseVoucherGrid, "Division_ID= '" + DoorDivisionId.ToString() + " '");
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
                drNew = objDoorDS.Tables[0].Rows.Find(o);
            }
        }
        return drNew;
    }
    private int AssignDoorSrNo(int SrNo)
    {
        int DoorNextSrNo = 0;
        if (SrNo == 0 && SessionDoorDeliveryExpenseVoucherGrid.Rows.Count > 0)
        {
            DoorNextSrNo = (int)SessionDoorDeliveryExpenseVoucherGrid.Compute("max(SrNo)", "");
            DoorNextSrNo = DoorNextSrNo + 1;
        }
        else if (SessionDoorDeliveryExpenseVoucherGrid.Rows.Count <= 0)
        {
            DoorNextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            DoorNextSrNo = (int)SessionDoorDeliveryExpenseVoucherGrid.Compute("max(SrNo)", "");
        }
        return DoorNextSrNo;
    }
    private int GetDoorSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionDoorDeliveryExpenseVoucherGrid, "Division_ID= '" + DoorDivisionId.ToString() + " '");
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
        objDT1 = SessionLocalCollectionVoucherGrid;
        DataRow DR = null;
        ddl_LocalLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_LocalLedger");
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
            DR["Division_ID"] = Util.String2Int(ddl_LocalDivision.SelectedValue);
            DR["Booking_Type_ID"] =Util.String2Int(ddl_BookingType.SelectedValue);
            DR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
            DR["Ledger_Name"] = ddl_LocalLedger.SelectedText;
            DR["Ledger_ID"] =Util.String2Int(ddl_LocalLedger.SelectedValue);
            if (e.CommandName == "ADD")
            {
                objDT1.Rows.Add(DR);
            }
            SessionLocalCollectionVoucherGrid= objDT1;
        }
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_LocalDivision.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Local Division";
            ddl_DoorDivision.Focus();
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
    public void SetLocalLedgerID(string LocalLedger_Name, string LocalLedgerID)
    {
        ddl_LocalLedger.DataTextField = "Ledger_Name";
        ddl_LocalLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(LocalLedger_Name, LocalLedgerID, ddl_LocalLedger);
    }
   
    public void BindLocalCollectionVoucher()
    {
        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionLocalCollectionVoucherGrid, "Division_ID= '" + LocalDivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_LocalCollectionVoucher.DataSource = ds_Temp;
        dg_LocalCollectionVoucher.DataBind();
       
    }
    private DataRow GetDataRow(int ItemIndex, DataSet objDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionLocalCollectionVoucherGrid, "Division_ID= '" + LocalDivisionId.ToString() + " '");
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
                object[] o = new object[] {BookingTypeId,DivisionId };               
                drNew = objDS.Tables[0].Rows.Find(o);
            }
        }
        return drNew;
    }
    private int AssignSrNo(int SrNo)
    {
        int NextSrNo = 0;
        if (SrNo == 0 && SessionLocalCollectionVoucherGrid.Rows.Count > 0)
        {
            NextSrNo = (int)SessionLocalCollectionVoucherGrid.Compute("max(SrNo)", "");
            NextSrNo = NextSrNo + 1;
        }
        else if (SessionLocalCollectionVoucherGrid.Rows.Count <= 0)
        {
            NextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            NextSrNo = (int)SessionLocalCollectionVoucherGrid.Compute("max(SrNo)", "");
        }
        return NextSrNo;
    }
    private int GetSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionLocalCollectionVoucherGrid, "Division_ID= '" + LocalDivisionId.ToString() + " '");
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
    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CompanyCallBackFunction.CallBack));

        objLocalCollectionVoucherPresenter = new LocalCollectionVoucherPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            BindLocalCollectionVoucher();
            BindDoorDeliveryExpenseVoucher();
        }
        Upd_Pnl_dg_LocalCollectionVoucher.Update();
        Upd_Pnl_DoorDeliveryExpenseVoucher.Update();
    }
    protected void dg_LocalCollectionVoucher_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_LocalCollectionVoucher.EditItemIndex = -1;
        dg_LocalCollectionVoucher.ShowFooter = true;
        BindLocalCollectionVoucher();
    }
    protected void dg_LocalCollectionVoucher_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        objDT1 = SessionLocalCollectionVoucherGrid;
        DataRow dr = null;
        //dr = GetDataRow(e.Item.ItemIndex, objDS);
        dr = objDT1.Rows[Util.String2Int(lbl_SrNo.Text)];

        if (e.Item.ItemIndex != -1)
        {
            objDT1 = SessionLocalCollectionVoucherGrid;
            objDT1.Rows.Remove(dr);
            objDT1.AcceptChanges();
            SessionLocalCollectionVoucherGrid = objDT1;
            dg_LocalCollectionVoucher.EditItemIndex = -1;
            dg_LocalCollectionVoucher.ShowFooter = true;
            BindLocalCollectionVoucher();
        }
    }
    protected void dg_LocalCollectionVoucher_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        SrNo = GetSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_LocalCollectionVoucher.EditItemIndex = SrNo;
        dg_LocalCollectionVoucher.ShowFooter = false;
        BindLocalCollectionVoucher();
    }
    protected void dg_LocalCollectionVoucher_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            objDT = SessionLocalCollectionVoucherGrid;
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindLocalCollectionVoucher();
                    dg_LocalCollectionVoucher.EditItemIndex = -1;
                    dg_LocalCollectionVoucher.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Booking Type";
                return;
            }
        }
    }
    protected void dg_LocalCollectionVoucher_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_LocalLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_LocalLedger");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                ddl_LocalLedger.DataTextField = "Ledger_Name";
                ddl_LocalLedger.DataValueField = "Ledger_Id";
                ddl_LocalLedger.OtherColumns = "11";
            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindBookingType = SessionBookingType; 
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_LocalLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_LocalLedger");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");

                ddl_LocalLedger.DataTextField = "Ledger_Name";
                ddl_LocalLedger.DataValueField = "Ledger_Id";
                ddl_LocalLedger.OtherColumns = "11";
                objDT = SessionLocalCollectionVoucherGrid;
                
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionLocalCollectionVoucherGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow DR = dt.Rows[0];
                    ddl_BookingType.SelectedValue = DR["Booking_Type_ID"].ToString();
                    SetLocalLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString());
                }              

              
            }
        }
    }
    protected void dg_LocalCollectionVoucher_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            objDT1 = SessionLocalCollectionVoucherGrid;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_LocalCollectionVoucher.EditItemIndex = -1;
                dg_LocalCollectionVoucher.ShowFooter = true;
                BindLocalCollectionVoucher();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Booking Type";
            return;
        }
    }
    protected void dg_DoorDeliveryExpenseVoucher_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_DoorDeliveryExpenseVoucher.EditItemIndex = -1;
        dg_DoorDeliveryExpenseVoucher.ShowFooter = true;
        BindDoorDeliveryExpenseVoucher();
    }
    protected void dg_DoorDeliveryExpenseVoucher_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        objDT = SessionDoorDeliveryExpenseVoucherGrid;
        DataRow dr = null;
        //dr = GetDoorDataRow(e.Item.ItemIndex, objDoorDS);
        dr = objDT.Rows[Util.String2Int(lbl_SrNo.Text)];
        if (e.Item.ItemIndex != -1)
        {
            objDT1 = SessionDoorDeliveryExpenseVoucherGrid;
            objDT1.Rows.Remove(dr);
            objDT1.AcceptChanges();
            SessionDoorDeliveryExpenseVoucherGrid = objDT;
            dg_DoorDeliveryExpenseVoucher.EditItemIndex = -1;
            dg_DoorDeliveryExpenseVoucher.ShowFooter = true;
            BindDoorDeliveryExpenseVoucher();
        }
    }
    protected void dg_DoorDeliveryExpenseVoucher_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        SrNo = GetDoorSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_DoorDeliveryExpenseVoucher.EditItemIndex = SrNo;
        dg_DoorDeliveryExpenseVoucher.ShowFooter = false;
        BindDoorDeliveryExpenseVoucher();
    }
    protected void dg_DoorDeliveryExpenseVoucher_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            objDT = SessionDoorDeliveryExpenseVoucherGrid;
            try
            {
                Insert_Update_Door_Dataset(source, e);
                if (isValid == true)
                {
                    BindDoorDeliveryExpenseVoucher();
                    dg_DoorDeliveryExpenseVoucher.EditItemIndex = -1;
                    dg_DoorDeliveryExpenseVoucher.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Booking Type";
                return;
            }
        }
    }
    protected void dg_DoorDeliveryExpenseVoucher_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_DoorLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_DoorLedger");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                ddl_DoorLedger.DataTextField = "Ledger_Name";
                ddl_DoorLedger.DataValueField = "Ledger_Id";
                ddl_DoorLedger.OtherColumns = "12";
            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindBookingType = SessionBookingType;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_DoorLedger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_DoorLedger");
                ddl_BookingType = (DropDownList)e.Item.FindControl("ddl_BookingType");
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");

                ddl_DoorLedger.DataTextField = "Ledger_Name";
                ddl_DoorLedger.DataValueField = "Ledger_Id";
                ddl_DoorLedger.OtherColumns = "12";
                objDT = SessionDoorDeliveryExpenseVoucherGrid;

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionDoorDeliveryExpenseVoucherGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow DR = dt.Rows[0];
                    ddl_BookingType.SelectedValue = DR["Booking_Type_ID"].ToString();
                    SetDoorLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString());
                }


            }
        }
    }
    protected void dg_DoorDeliveryExpenseVoucher_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            objDT = SessionDoorDeliveryExpenseVoucherGrid;
            Insert_Update_Door_Dataset(source, e);
            if (isValid == true)
            {
                dg_DoorDeliveryExpenseVoucher.EditItemIndex = -1;
                dg_DoorDeliveryExpenseVoucher.ShowFooter = true;
                BindDoorDeliveryExpenseVoucher();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Booking Type";
            return;
        }
    }
   
    protected void ddl_LocalDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLocalCollectionVoucher();
    }
    
    protected void ddl_DoorDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDoorDeliveryExpenseVoucher();
    }
    #endregion
}
