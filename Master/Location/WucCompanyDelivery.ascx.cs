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

public partial class Master_Location_WucCompanyDelivery : System.Web.UI.UserControl,ICompanyDeliveryView
{
    #region ClassVariable
    CompanyDeliveryPresenter objCompanyDeliveryPresenter;
    private ScriptManager scm_CompanyParameter;
    ClassLibrary.UIControl.DDLSearch ddl_DeliveryIncomeLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_ServiceTaxLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_OctroiReceivableLedgerName;
    private int _Index;
    DropDownList ddl_BookingType;
    DataSet objDS;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    Label lbl_SrNo;
    int NextSrNo = 0;
    DataTable objDT;
    bool isValid = false;
    LinkButton lbtn_Delete;    


    #endregion

    #region ControlsValues
    public int DivisionId
    {
        set { ddl_Division.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Division.SelectedValue); }
    }

    public DataTable SessionCompanyDeliveryGrid
    {
        get { return StateManager.GetState<DataTable>("CompanyDeliveryGrid"); }
        set
        {
            if (value != null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }

                StateManager.SaveState("CompanyDeliveryGrid", value);
            }
        }
    }
    public int DeliveryIncomeLedgerId
    {
        get { return ddl_DeliveryIncomeLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_DeliveryIncomeLedgerName.SelectedValue); }
    }
    
    public int ServiceTaxLedgerNameId
    {
        get { return ddl_ServiceTaxLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_ServiceTaxLedgerName.SelectedValue); }
    }
    public int OctroiReceivableLedgerId
    {
        get { return ddl_OctroiReceivableLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_OctroiReceivableLedgerName.SelectedValue); }
    }

    public string CompanyDeliveryDetails
    {

        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionCompanyDeliveryGrid.Copy());
            _objDs.Tables[0].TableName = "CompanyDeliveryGrid";
            return _objDs.GetXml();
           // return SessionCompanyDeliveryGrid.GetXml();
        }
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

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;



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
            //return -1;
        }
    }


     #endregion

    #region ControlsBind

    public DataTable BindDeliveryIncomeLedger
    {
        set
        {
            DropDownList ddl_DeliveryIncomeLedgerName = (DropDownList)(dg_CompanyDelivery.Controls[1].Controls[_Index].FindControl("ddl_DeliveryIncomeLedgerName"));
            ddl_DeliveryIncomeLedgerName.DataTextField = "Delivery_Income_Ledger_Name";
            ddl_DeliveryIncomeLedgerName.DataValueField = "Delivery_Income_Ledger_Id";
            SessionDeliveryIncomeDropDown = value;
            ddl_DeliveryIncomeLedgerName.DataSource = value;
            ddl_DeliveryIncomeLedgerName.DataBind();





        }

    }

    public DataTable SessionDeliveryIncomeDropDown
    {
        get { return StateManager.GetState<DataTable>("DeliveryIncomeDropDown"); }
        set { StateManager.SaveState("DeliveryIncomeDropDown", value); }
    }
    public DataTable BindOctroiReceivableLedger
    {
        set
        {
            DropDownList ddl_OctroiReceivableLedgerName = (DropDownList)(dg_CompanyDelivery.Controls[3].Controls[_Index].FindControl("ddl_OctroiReceivableLedgerName"));
            ddl_OctroiReceivableLedgerName.DataTextField = "Octroi_Receivable_Ledger_Name";
            ddl_OctroiReceivableLedgerName.DataValueField = "Octroi_Receivable_Ledger_Id";
            SessionOctroiReceivableDropDown = value;
            ddl_OctroiReceivableLedgerName.DataSource = value;
            ddl_OctroiReceivableLedgerName.DataBind();




        }

    }

    public DataTable SessionOctroiReceivableDropDown
    {
        get { return StateManager.GetState<DataTable>("OctroiReceivableDropDown"); }
        set { StateManager.SaveState("OctroiReceivableDropDown", value); }
    }

    public DataTable BindSessionTaxLedger
    {
        set
        {
            DropDownList ddl_ServiceTaxLedgerName = (DropDownList)(dg_CompanyDelivery.Controls[2].Controls[_Index].FindControl("ddl_ServiceTaxLedgerName"));

            ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
            ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_Id";
            SessionServiceTaxLedgerDropDown = value;
            ddl_ServiceTaxLedgerName.DataSource = value;
            ddl_ServiceTaxLedgerName.DataBind();




        }

    }

    public DataTable SessionServiceTaxLedgerDropDown
    {
        get { return StateManager.GetState<DataTable>("SessionTaxLedgerDropDown"); }
        set { StateManager.SaveState("SessionTaxLedgerDropDown", value); }
    }


    public DataTable BindDivision
    {
        set
        {
            ddl_Division.DataSource = value;
            ddl_Division.DataTextField = "Division_Name";
            ddl_Division.DataValueField = "Division_ID";
            SessionDivision = value;
            ddl_Division.DataBind();
            ddl_Division.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("DivisionDropDown"); }
        set { StateManager.SaveState("DivisionDropDown", value); }
    }

    public DataTable BindBookingType
    {
        set
        {
            ddl_BookingType.DataSource = value;
            ddl_BookingType.DataTextField = "Booking_Type";
            ddl_BookingType.DataValueField = "Booking_Type_ID";
            SessionBookingType = value;

            ddl_BookingType.DataBind();
            ddl_BookingType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionBookingType
    {
        get { return StateManager.GetState<DataTable>("BookingTypeDropDown"); }
        set { StateManager.SaveState("BookingTypeDropDown", value); }
    }
   
    public DataTable BindCompanyDeliveryGrid
    {
        set
        {

            SessionCompanyDeliveryGrid = value;
            dg_CompanyDelivery.DataSource = value;
            dg_CompanyDelivery.DataBind();
           

        }
    }

    #endregion

    #region OtherMethods

    public void BindGrid()
    {


        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();

        dv = ObjCommon.Get_View_Table(SessionCompanyDeliveryGrid, "Division_ID= '" + DivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_CompanyDelivery.DataSource = ds_Temp;
        dg_CompanyDelivery.DataBind();


    }


    private void insertUpdateDataset(DataGridCommandEventArgs e)
    {
        objDT = SessionCompanyDeliveryGrid;
        DataRow objDR = null;
        ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));
        ddl_DeliveryIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_DeliveryIncomeLedgerName"));
        ddl_OctroiReceivableLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_OctroiReceivableLedgerName"));
        ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ServiceTaxLedgerName"));
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        DataRow[] DRArray;



        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
            NextSrNo = AssignSrNo(0);
            //lbl_SrNo.Text = Util.Int2String(NextSrNo);


        }
        else if (e.CommandName == "Update")
        {
            NextSrNo = Util.String2Int(lbl_SrNo.Text);
            DRArray = objDT.Select("SrNo='" + NextSrNo + "'");
            if (DRArray.Length > 0)
            {
                objDR = DRArray[0];
            }

            //NextSrNo = Util.String2Int(objDR["SrNo"].ToString());

        }


        if (validateGrid() == true)
        {

        objDR["SrNo"] = NextSrNo;
        objDR["Booking_Type_ID"] = ddl_BookingType.SelectedValue;
        objDR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
        objDR["Division_ID"] = Util.String2Int(ddl_Division.SelectedValue);
        objDR["Delivery_Income_Ledger_ID"] = Convert.ToInt32(ddl_DeliveryIncomeLedgerName.SelectedValue);
        objDR["Delivery_Income_Ledger_Name"] = ddl_DeliveryIncomeLedgerName.SelectedText;
        objDR["Octroi_Receivable_Ledger_ID"] = Convert.ToInt32(ddl_OctroiReceivableLedgerName.SelectedValue);
        objDR["Octroi_Receivable_Ledger_Name"] = ddl_OctroiReceivableLedgerName.SelectedText;
        objDR["Service_Tax_Ledger_ID"] = Convert.ToInt32(ddl_ServiceTaxLedgerName.SelectedValue);
        objDR["Service_Tax_Ledger_Name"] = ddl_ServiceTaxLedgerName.SelectedText;



        if (e.CommandName == "Add")
        {

            objDT.Rows.Add(objDR);
        }

        else if (e.CommandName == "Update")
        {
            dg_CompanyDelivery.EditItemIndex = -1;
            dg_CompanyDelivery.ShowFooter = true;

        }
        BindCompanyDeliveryGrid = objDT;
    }

    }

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyParameter = value; }
    }

    public bool validateGrid()
    {
        if (Util.String2Int(ddl_Division.SelectedValue) <= 0)
        {
            errorMessage = "Please Select  Division";
            ddl_Division.Focus();
        }
        else if (Util.String2Int(ddl_BookingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Booking Type";
            ddl_BookingType.Focus();
        }
        else if (DeliveryIncomeLedgerId <= 0)
        {
            errorMessage = "Please Select Delivery Income Leger";
            ddl_DeliveryIncomeLedgerName.Focus();
        }
        else if (ServiceTaxLedgerNameId <= 0)
        {
            errorMessage = "Please Select Service Tax Ledger";
            ddl_ServiceTaxLedgerName.Focus();
        }
        else if (OctroiReceivableLedgerId <= 0)
        {
            errorMessage = "Please Select Service Tax Ledger";
            ddl_OctroiReceivableLedgerName.Focus();
        }
        else
            isValid = true;

        return isValid;
    }
    private DataRow GetDataRow(int ItemIndex, DataSet objDS)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionCompanyDeliveryGrid, "Division_ID= '" + DivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);
        DataRow dr = null;
        DataRow drNew = null;
        int BookingTypeId, DivisionTypeId;
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                BookingTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Booking_Type_ID"].ToString());
                DivisionTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Division_ID"].ToString());
                object[] o = new object[] { BookingTypeId, DivisionId };
                drNew = objDS.Tables[0].Rows.Find(o);
            }
        }
        return drNew;
    }
    private int AssignSrNo(int SrNo)
    {
        int NextSrNo = 0;
        if (SrNo == 0 && SessionCompanyDeliveryGrid.Rows.Count > 0)
        {
            NextSrNo = (int)SessionCompanyDeliveryGrid.Compute("max(SrNo)", "");
            NextSrNo = NextSrNo + 1;
        }
        else if (SessionCompanyDeliveryGrid.Rows.Count <= 0)
        {
            NextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            NextSrNo = (int)SessionCompanyDeliveryGrid.Compute("max(SrNo)", "");
        }
        return NextSrNo;
    }
    private int GetSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionCompanyDeliveryGrid, "Division_ID= '" + DivisionId.ToString() + " '");
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

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CompanyCallBackFunction.CallBack));
        objCompanyDeliveryPresenter = new CompanyDeliveryPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            BindGrid();
        }
        Upd_pnl_CompanyDelivery.Update();

    }

    protected void ddl_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
          BindGrid();
    }

    #endregion

    #region GridEvents
    protected void dg_CompanyDelivery_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {

                ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));

                ddl_DeliveryIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_DeliveryIncomeLedgerName"));
                ddl_DeliveryIncomeLedgerName.DataValueField = "Delivery_Income_Ledger_ID";
                ddl_DeliveryIncomeLedgerName.DataTextField = "Delivery_Income_Ledger_Name";
                ddl_DeliveryIncomeLedgerName.OtherColumns = "4";

                

                ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ServiceTaxLedgerName"));
                ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_ID";
                ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
                ddl_ServiceTaxLedgerName.OtherColumns = "5";

                ddl_OctroiReceivableLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_OctroiReceivableLedgerName"));
                ddl_OctroiReceivableLedgerName.DataValueField = "Octroi_Receivable_Ledger_ID";
                ddl_OctroiReceivableLedgerName.DataTextField = "Octroi_Receivable_Ledger_Name";
                ddl_OctroiReceivableLedgerName.OtherColumns = "6";

                //ddl_TyreNo.OtherColumns = KeyId.ToString();
                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");               
                BindBookingType = SessionBookingType;

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_BookingType.SelectedValue = SessionBookingType.Rows[e.Item.ItemIndex]["Booking_Type_ID"].ToString();
                ddl_DeliveryIncomeLedgerName.OtherColumns = "4";
                ddl_ServiceTaxLedgerName.OtherColumns = "5";
                ddl_OctroiReceivableLedgerName.OtherColumns = "6";
                objDT = SessionCompanyDeliveryGrid;

                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionCompanyDeliveryGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow objDR = dt.Rows[0];
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Delivery_Income_Ledger_Name"].ToString(), objDR["Delivery_Income_Ledger_ID"].ToString(), ddl_DeliveryIncomeLedgerName);
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Service_Tax_Ledger_Name"].ToString(), objDR["Service_Tax_Ledger_ID"].ToString(), ddl_ServiceTaxLedgerName);
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Octroi_Receivable_Ledger_Name"].ToString(), objDR["Octroi_Receivable_Ledger_ID"].ToString(), ddl_OctroiReceivableLedgerName);

                }




            }


        }
    }
    protected void dg_CompanyDelivery_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
          {
            
                 errorMessage = "";
                 objDT = SessionCompanyDeliveryGrid;
            try
            {
               

                insertUpdateDataset(e);
                if (isValid == true)
                {

                    BindGrid();
                    dg_CompanyDelivery.EditItemIndex = -1;
                    dg_CompanyDelivery.ShowFooter = true;

                }
            }
                 catch (ConstraintException)
                 {
                     errorMessage = "Duplicate Booking Type";
                     return;
                 }
        }
    }
    protected void dg_CompanyDelivery_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        SrNo = GetSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_CompanyDelivery.EditItemIndex = SrNo;
        dg_CompanyDelivery.ShowFooter = false;
        BindGrid();
        


    }
    protected void dg_CompanyDelivery_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_CompanyDelivery.EditItemIndex = -1;
        dg_CompanyDelivery.ShowFooter = true;
        BindGrid();

    }

    protected void dg_CompanyDelivery_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        objDT = SessionCompanyDeliveryGrid;
        DataRow dr = null;
        //dr = GetDataRow(e.Item.ItemIndex, objDS);
        SrNo = Util.String2Int(lbl_SrNo.Text);
        //DataRow[] DRArray;

        //DRArray = objDT.Select("SrNo='" + SrNo + "'");
        //if (DRArray.Length > 0)
        //{
        //    dr = DRArray[0];
        //}
        dr = objDT.Rows[Util.String2Int(lbl_SrNo.Text)]; 
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionCompanyDeliveryGrid;
            objDT.Rows.Remove(dr);
            objDT.AcceptChanges();
            SessionCompanyDeliveryGrid = objDT;
            dg_CompanyDelivery.EditItemIndex = -1;
            dg_CompanyDelivery.ShowFooter = true;
            BindGrid();

        }
    }
    #endregion

   
}
