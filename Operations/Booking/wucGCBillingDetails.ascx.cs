
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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

using ClassLibraryMVP;
using Raj.EC;
public partial class Operations_Booking_wucGCBillingDetails : System.Web.UI.UserControl 
{    
    
    DataRow dr;
    CheckBox chk;
    DataTable objdata_BillingDetails;
    
    ClassLibrary.UIControl.DDLSearch ddl_BillingParty;
    //ClassLibrary.UIControl.DDLSearch ddl_BillingBranch ;
    Controls_WucHierarchyWithID WucHierarchyWithID1;
    private DAL objDAL = new DAL();

    TextBox ddl_BillingParty_txtBoxddl_BillingParty, txt_Bill_SrNo, txt_Description, txt_Ratio, txt_Amount;    
    Int32  Ledger_Id;
    Decimal Credit_Limit, Closing_Balance;
    Boolean Allow_To_Save;
    public Common CommonObj = new Common();

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    #endregion 

    #region Controls 
   
    public string BillingDetailsErrorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public Boolean Session_Is_Validate_Credit_Limit
    {
        get { return StateManager.GetState<Boolean>("Is_Validate_Credit_Limit"); }
        set { StateManager.SaveState("Is_Validate_Credit_Limit", value); }
    }

    public DataTable Session_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("BillingDetailsGrid"); }
        set { StateManager.SaveState("BillingDetailsGrid", value); }
    }

    public DataTable Session_Main_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("Main_BillingDetailsGrid"); }
        set { StateManager.SaveState("Main_BillingDetailsGrid", value); }
    }

    public Decimal Session_TotalRatio
    {
        get { return StateManager.GetState<Decimal>("Session_TotalRatio"); }
        set { StateManager.SaveState("Session_TotalRatio", value); }
    }

    public Decimal TotalRatio
    {
        set
        {
            lbl_TotalRatio.Text = Util.Decimal2String(value);
            hdn_TotalRatio.Value = Util.Decimal2String(value);
        }
        get{return ValueOfHdn_Decimal(hdn_TotalRatio);}
    }

    public Decimal ValueOfHdn_Decimal(HiddenField H)
    {
        return Util.String2Decimal(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }

    private Boolean IsMultipleLocationBillingAllow
    {
        get { return Util.String2Bool(hdn_IsMultipleLocationBillingAllowed.Value); }
    }

    public void SetBillingClient(string FromLocation_Name, string FromLocationID)
    {
        ddl_BillingParty.DataTextField = "Billing_Client_Name";
        ddl_BillingParty.DataValueField = "Billing_Client_ID";
        Raj.EC.Common.SetValueToDDLSearch(FromLocation_Name, FromLocationID, ddl_BillingParty);
    }
    
    //public void SetBilllingBranch(string FromLocation_Name, string FromLocationID)
    //{
    //    ddl_BillingBranch.DataTextField = "Billing_Branch_Name";
    //    ddl_BillingBranch.DataValueField = "Billing_Branch_Id";
    //    Raj.EC.Common.SetValueToDDLSearch(FromLocation_Name, FromLocationID, ddl_BillingBranch  );
    //}
    
    #endregion
    #region Function
    
    public bool validateUI()
    {
        bool isValid = false;
        isValid = false;

        errorMessage = "";

        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            if ( Session_BillingDetailsGrid.Rows.Count<=0)
            {
                errorMessage = "Please Enter Billing Details.";
            }
            else if ( TotalRatio < 100 || TotalRatio > 100 )
            {
                errorMessage = "Total Ratio Should Be 100 %";
            }
            else
            {
                isValid = true;
            }
        }
        else
        {
            errorMessage = "Please Enter Billing Details.";
            isValid = false;
        }        

        return isValid;
    }

     #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        hdn_Mode.Value = Request.QueryString["Mode"].ToString();
        hdn_IsMultipleLocationBillingAllowed.Value = Request.QueryString["IsMultipleLocBillingAllow"].ToString();

        if (!IsPostBack)
        {
            Bind_BillingGrid();
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();

            sbValid.Append("if (Allow_To_Exit() == false) { return false; }");
            sbValid.Append(Page.ClientScript.GetPostBackEventReference(btn_Exit, ""));
            sbValid.Append(";");
            btn_Exit.Attributes.Add("onclick", sbValid.ToString());

            if (hdn_Mode.Value == "4")
            {
                Btn_Save.Visible = false;
                btn_Exit.Visible = true;
                btn_Exit.Enabled = true;
                dg_Billing.ShowFooter = false;
                dg_Billing.Columns[7].Visible = false;
                dg_Billing.Columns[8].Visible = false;
            }
        }

        SetStandardCaption();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_BillingDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "billing_details";           
            
            Session_Main_BillingDetailsGrid = _objDs.Tables[0];

            string _Msg;
            _Msg = "Saved SuccessFully";

            String popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_ServiceTaxDetails();</script>";

            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Session_BillingDetailsGrid = Session_Main_BillingDetailsGrid;
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }

    private void SetStandardCaption()
    {
        lbl_BillingDetails.Text = CompanyManager.getCompanyParam().GcCaption + " Billing Details";
    }

    // ' Holds the direction to be sorted.
    private string sortDir_BillingDetails_R
    {
        get
        {
            return Convert.ToString(ViewState["sortDir_BillingDetails"]);
        }
        set
        {
            ViewState["sortDir_BillingDetails"] = value;
        }
    }

    protected void dg_Billing_EditCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Billing.EditItemIndex = e.Item.ItemIndex;
        dg_Billing.ShowFooter = false;
        Bind_BillingGrid  ();
    }

    protected void dg_Billing_CancelCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Billing.EditItemIndex = -1;
        dg_Billing.ShowFooter = true;
        Bind_BillingGrid();
    }

    protected void dg_Billing_UpdateCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        Insert_Update_Billing_Details_Dataset(sender, e);
        if (Allow_To_Save == true)
        {
            dg_Billing.EditItemIndex = -1;
            dg_Billing.ShowFooter = true;

            Bind_BillingGrid   ();
        }
    }

    private void Bind_BillingGrid()
    {
        Set_Billing_Details_SrNo();
        dg_Billing.DataSource = Session_BillingDetailsGrid;
        dg_Billing.DataBind();

        TotalRatio = 0;
        Session_TotalRatio = 0;

        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            if (Session_BillingDetailsGrid.Rows.Count > 0)
            {
                TotalRatio = Util.String2Decimal( Session_BillingDetailsGrid.Compute("sum(bill_Ratio)", "").ToString());
                Session_TotalRatio  = Util.String2Decimal( Session_BillingDetailsGrid.Compute("sum(bill_Ratio)", "").ToString());
            }
        }
        else
        {
            TotalRatio = 0;
            Session_TotalRatio = 0;
        }
    }

    public void Set_Billing_Details_SrNo()
    {
        int i = 0;

        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            for (i = 0; i <= Session_BillingDetailsGrid.Rows.Count - 1; i++)
            {
                Session_BillingDetailsGrid.Rows[i]["Sr_No"] = i + 1;
                Session_BillingDetailsGrid.AcceptChanges();
            }
        }
    }

    public void dg_Billing_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            dr = Session_BillingDetailsGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_BillingDetailsGrid.AcceptChanges();
        }
        dg_Billing.ShowFooter = true;
        Bind_BillingGrid();
    }

    protected void dg_Billing_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingParty"));
            WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
            txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
            txt_Bill_SrNo = (TextBox)(e.Item.FindControl("txt_Bill_SrNo"));

            if (IsMultipleLocationBillingAllow == true)
            {
                WucHierarchyWithID1.Allow_All_Hierarchy = true;
                WucHierarchyWithID1.Set_Default_Values(sender, e);
            }
            else
            {
                WucHierarchyWithID1.HierarchyCode = "BO";
                WucHierarchyWithID1.Set_Default_Values(sender, e);
            }
            WucHierarchyWithID1.Set_TD_Caption_Visible = false;
            WucHierarchyWithID1.Set_hierarchy_Width();
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            dr = Session_BillingDetailsGrid.Rows[e.Item.ItemIndex];

            SetBillingClient(dr["Billing_Client_Name"].ToString(), dr["Billing_Client_ID"].ToString());
            WucHierarchyWithID1.HierarchyCode = dr["Billing_Hierarchy"].ToString().ToUpper();
            WucHierarchyWithID1.MainId = Util.String2Int(dr["Billing_Branch_Id"].ToString());
            txt_Description.Text = dr["Description"].ToString();
        }
    }

    protected void dg_Billing_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {            
            Insert_Update_Billing_Details_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                Bind_BillingGrid();
                dg_Billing.EditItemIndex = -1;
                dg_Billing.ShowFooter = true;
            }
        }
    }

    public Boolean Allow_To_Add_Update_Billling_Details(int Sr_No)
    {
        Allow_To_Save = false;

        BillingDetailsErrorMessage = "";

        if (WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {
            //BillingDetailsErrorMessage = " Please Enter Billing Branch.";
        }
        else if (Util.String2Int(ddl_BillingParty.SelectedValue) <= 0)
        {
            BillingDetailsErrorMessage = " Please Enter Billing Party.";
        }         
        else if (Util.String2Decimal(txt_Ratio.Text.Trim()) <= 0)
        {
            BillingDetailsErrorMessage = " Please Enter Bill Ratio greater than Zero";
        }
        else if (Util.String2Decimal(txt_Ratio.Text.Trim()) > 100)
        {
            BillingDetailsErrorMessage = " Invalid Bill Ratio.";
        }
        else
        {
            Allow_To_Save = true;
        }

        DataView dv;

        if (Allow_To_Save)
        {
            string FQ = " Billing_Client_ID =" + ddl_BillingParty.SelectedValue.ToString() + " And Billing_Branch_ID =" +
                       WucHierarchyWithID1.MainId.ToString() + " And Billing_Hierarchy ='" + WucHierarchyWithID1.HierarchyCode + "' And Sr_No <>" + Sr_No;

            dv = CommonObj.Get_View_Table(Session_BillingDetailsGrid, FQ);

            if (dv.Count > 0)
            {
                BillingDetailsErrorMessage = "Duplicate Billing Party and Billing Location";
                Allow_To_Save = false;
            }
        }

        return Allow_To_Save;
    }

    private void Insert_Update_Billing_Details_Dataset(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objdata_BillingDetails = Session_BillingDetailsGrid;

        ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingParty"));
        WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
        //ddl_BillingBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingBranch"));
        txt_Ratio = (TextBox)(e.Item.FindControl("txt_Ratio"));
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        
        txt_Bill_SrNo = (TextBox)(e.Item.FindControl("txt_Bill_SrNo"));

        if (Allow_To_Add_Update_Billling_Details(e.Item.ItemIndex + 1))
        {
            if (e.CommandName == "Add")
            {
                dr = Session_BillingDetailsGrid.NewRow();
                dr["GC_Billing_Id"] = "0";
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_BillingDetailsGrid.Rows[e.Item.ItemIndex];
            }

            dr["Sr_No"] = e.Item.ItemIndex;//'"Sr_No";

            dr["Billing_Hierarchy"] = WucHierarchyWithID1.HierarchyCode;
            dr["Hierarchy_Name"] = WucHierarchyWithID1.GetHierarchyText;

            if (WucHierarchyWithID1.HierarchyCode == "HO")
            {
                dr["Billing_Branch_ID"] = 0;
                dr["Billing_Branch_Name"] = "";
            }
            else
            {
                dr["Billing_Branch_ID"] = WucHierarchyWithID1.MainId;
                dr["Billing_Branch_Name"] = WucHierarchyWithID1.GetLocationText;
            }

            dr["Billing_Client_ID"] = ddl_BillingParty.SelectedValue;
            //dr["Billing_Branch_ID"] = ddl_BillingBranch.SelectedValue;
            dr["Bill_Ratio"] = txt_Ratio.Text.Trim() == string.Empty ? "0" : txt_Ratio.Text.Trim();
            dr["Description"] = txt_Description.Text.Trim();

            dr["Billing_Client_Name"] = ddl_BillingParty.SelectedText.Trim();
            //dr["Billing_Branch_Name"] = ddl_BillingBranch.SelectedText.Trim();
                        
            Get_Billing_Party_Credit_Details(Util.String2Int(ddl_BillingParty.SelectedValue));

            if (Session_Is_Validate_Credit_Limit == true)
            {
                dr["Credit_Limit"] = Credit_Limit;
                dr["Closing_Balance"] = Closing_Balance;
            }
            else
            {
                dr["Credit_Limit"] = 0;
                dr["Closing_Balance"] = 0;
            }

            dr["Ledger_Id"] = Ledger_Id;

            if (e.CommandName == "Add")
            {
                objdata_BillingDetails.Rows.Add(dr);
            }
            Session_BillingDetailsGrid.AcceptChanges();
        }
        Session_BillingDetailsGrid = objdata_BillingDetails;
    }

    private void Calculate_BillingDetailsTotal()
    {
        TotalRatio = 0;        

        if (StateManager.IsValidSession("BillingDetailsGrid"))
        {
            if (Session_BillingDetailsGrid.Rows.Count > 0)
            {
                TotalRatio = Util.String2Int(Session_BillingDetailsGrid.Compute("sum(Ratio)", "").ToString());
            }
            else
            {
                TotalRatio = 0;
            }
        }
    }

    private void Get_BillingPartyDetails(object sender, EventArgs e)
    {
        ddl_BillingParty_txtBoxddl_BillingParty = (TextBox)sender;
        DataGridItem dg_Billing1 = (DataGridItem)ddl_BillingParty_txtBoxddl_BillingParty.Parent.Parent.Parent;
       
        ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(dg_Billing1.FindControl("ddl_BillingParty"));

        int BillingPartyId;
        BillingPartyId = 0;

        if (Util.String2Int( ddl_BillingParty.SelectedValue) > 0 )
        {
            BillingPartyId = Util.String2Int(ddl_BillingParty.SelectedValue);
        }
        else
        {
            BillingPartyId = 0;            
        }

        Get_Billing_Party_Credit_Details(BillingPartyId);
    }

    public void Get_Billing_Party_Credit_Details(Int32 BillingPartyId)
    {
        DataSet ds_BillingPartyDetails = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Billing_Party_Id", SqlDbType.Int, 0, BillingPartyId) };

        objDAL.RunProc("Ec_Opr_GC_Get_Billing_Party_Details", objSqlParam, ref ds_BillingPartyDetails);

        if (ds_BillingPartyDetails.Tables[0].Rows.Count > 0)
        {
            Credit_Limit = Util.String2Decimal(ds_BillingPartyDetails.Tables[0].Rows[0]["Credit_Limit"].ToString());
            Closing_Balance = Util.String2Decimal(ds_BillingPartyDetails.Tables[0].Rows[0]["Closing_Balance"].ToString());
            Ledger_Id = Util.String2Int (ds_BillingPartyDetails.Tables[0].Rows[0]["Ledger_Id"].ToString());
        }
        else
        {
            Credit_Limit = 0;
            Closing_Balance = 0;
            Ledger_Id = 0;
        }
    }

    protected void ddl_BillingParty_TxtChange(object sender, EventArgs e)
    {        
        Get_BillingPartyDetails(sender, e);
    }
}
