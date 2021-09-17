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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Text.RegularExpressions;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 13/10/2008
/// Description   : This Page is For Master Client Billing Details
/// </summary>
/// 

public partial class Master_Sales_WucClientBillingDetails : System.Web.UI.UserControl,IClientBillingView
{
    #region ClassVariables
    ClientBillingPresenter objClientBillingPresenter;
    private ScriptManager scm_ClientBilling;
    int i = 0;
    ClassLibrary.UIControl.DDLSearch ddl_BillingBranch, ddl_City;
    TextBox txt_ContactPerson, txt_BillingName;
    TextBox txt_Address, txt_ContactNo, txt_Email;
    DataTable objDT;
    DataRow DR = null;
    bool isValid = false;
    #endregion

    #region ControlsValues

   
    public bool Is_To_Pay
    {
        get { return cbl_BookingPaymentMode.Items[0].Selected; }
        set { cbl_BookingPaymentMode.Items[0].Selected = Convert.ToBoolean(value); }
    }

    public bool Is_Paid
    {
        get 
        {
            return false;
            //return cbl_BookingPaymentMode.Items[1].Selected; 
        }
        set { cbl_BookingPaymentMode.Items[1].Selected = Convert.ToBoolean(value); }
    }
    public bool Is_To_Be_Billed
    {
        get 
        {
            return false;
            //return cbl_BookingPaymentMode.Items[2].Selected; 
        }
        set { cbl_BookingPaymentMode.Items[2].Selected = Convert.ToBoolean(value); }
    }
    public bool Is_FOC
    {
        get 
        {
            return false;
            //return cbl_BookingPaymentMode.Items[3].Selected; 
        }
        set { cbl_BookingPaymentMode.Items[3].Selected = Convert.ToBoolean(value); }
    }

    public int BillingCycle
    {
        set { ddl_BillingCycle.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BillingCycle.SelectedValue); }
    }

    public int BillingDays
    {
        set { ddl_Day.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Day.SelectedValue); }
    }
    public int BillingCycleDay1
    {
        set { ddl_BillingCycleDay1.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BillingCycleDay1.SelectedValue); }
    }
    public int BillingCycleDay2
    {
        set { ddl_BillingCycleDay2.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BillingCycleDay2.SelectedValue); }
    }
    #endregion
   
    #region ControlsBind
    public DataTable BindBillingGrid
    {
        set
        {
            dg_BillingDetails.DataSource = value;
            dg_BillingDetails.DataBind();
        }
    }

    public DataTable BindPaymentMode
    {
        set
        {
            cbl_BookingPaymentMode.DataTextField = "Payment_Type";
            cbl_BookingPaymentMode.DataValueField = "Payment_Type_Id";
            cbl_BookingPaymentMode.DataSource = value;
            cbl_BookingPaymentMode.DataBind();
            for (i = 0; i <= value.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(value.Rows[i]["Att"]) == true)
                {
                    cbl_BookingPaymentMode.Items[i].Selected = true;
                }
            }
        }
    }
    public DataTable BindBillingCycle
    {
        set
        {
            ddl_BillingCycle.DataTextField = "BillingCycle";
            ddl_BillingCycle.DataValueField = "BillingCycleID";
            ddl_BillingCycle.DataSource = value;
            ddl_BillingCycle.DataBind();
        }
    }

    public DataTable BindBillingDays
    {
        set
        {
            ddl_Day.DataTextField = "DayName";
            ddl_Day.DataValueField = "DayNo";
            ddl_Day.DataSource = value;
            ddl_Day.DataBind();
        }
    }

    public void SetBranchID(string Text, string Value)
    {
        ddl_BillingBranch.DataTextField = "Branch_Name";
        ddl_BillingBranch.DataValueField = "Branch_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, ddl_BillingBranch);
    }
    public void SetCityID(string Text, string Value)
    {
        ddl_City.DataTextField = "City_Name";
        ddl_City.DataValueField = "City_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, ddl_City);
    }
    public void SetBillingCycleDetails()
    {
        if (BillingCycle == 1)
        {
            BillingCycleDay1 = 1;
            BillingCycleDay2 = 1;
        }
        else if (BillingCycle == 2)
        {
            BillingDays = 1;
        }
        else if (BillingCycle == 3)
        {
            BillingDays = 1;
            BillingCycleDay2 = 1;
        }
    }
    public DataTable SessionBindBillingGrid
    {
        get { return StateManager.GetState<DataTable>("BindBillingGrid"); }
        set { StateManager.SaveState("BindBillingGrid", value); }
    }
       
    public String BillingDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindBillingGrid.Copy());
            _objDs.Tables[0].TableName = "Billing_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    public String PaymentModeXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataTable dt = null;
            dt = new DataTable();
            dt.Columns.Add("Payment_Type_Id");
            DataRow dr;
            for (i = 0; i <= cbl_BookingPaymentMode.Items.Count - 1; i++)
            {
                if (cbl_BookingPaymentMode.Items[i].Selected == true)
                {
                    dr = dt.NewRow();
                    dr["Payment_Type_Id"] = cbl_BookingPaymentMode.Items[i].Value;
                    dt.Rows.Add(dr);
                }
            }

            _objDs.Tables.Add(dt);

            _objDs.Tables[0].TableName = "Payment_Mode";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_ClientBilling = value; }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
        SetBillingCycleDetails();

        if(BillingCycle==2 && BillingCycleDay2<= BillingCycleDay1 )
        {
            _isValid = false;
            errorMessage = "Billing Day 1 Can Not Be Greater Than Billing Day 2";
            scm_ClientBilling.SetFocus(ddl_BillingCycleDay1);
        }
        else if (BillingCycle == 2 && (BillingCycleDay2 - BillingCycleDay1) < 15 )
        {
            _isValid = false;
            errorMessage = "Billing Day 1 And Billing Day 2 Must Have 15 Days Gap";
            scm_ClientBilling.SetFocus(ddl_BillingCycleDay1);
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
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objClientBillingPresenter = new ClientBillingPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            BindBillingGrid = SessionBindBillingGrid;
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "BillingCycleVisibility", "SetBillingCycleVisibility();", true);
    }
    protected void dg_BillingDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_BillingBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingBranch"));
                ddl_City = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_City"));
                txt_ContactPerson = (TextBox)(e.Item.FindControl("txt_ContactPerson"));
                txt_BillingName = (TextBox)(e.Item.FindControl("txt_BillingName"));
                txt_Address = (TextBox)(e.Item.FindControl("txt_Address"));
                txt_ContactNo = (TextBox)(e.Item.FindControl("txt_ContactNo"));
                txt_Email = (TextBox)(e.Item.FindControl("txt_Email"));

                ddl_BillingBranch.DataTextField = "Branch_Name";
                ddl_BillingBranch.DataValueField = "Branch_ID";
                ddl_City.DataTextField = "City_Name";
                ddl_City.DataValueField = "City_ID";                
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionBindBillingGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                SetBranchID(DR["Branch_Name"].ToString(), DR["Branch_ID"].ToString());
                SetCityID(DR["City_Name"].ToString(), DR["City_ID"].ToString());
                txt_ContactPerson.Text = DR["Contact_Person"].ToString();
                txt_BillingName.Text = DR["Billing_Name"].ToString();
                txt_Address.Text = DR["Billing_Address"].ToString();
                txt_ContactNo.Text = DR["Contact_No"].ToString();
                txt_Email.Text = DR["email"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        ddl_BillingBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingBranch"));
        ddl_City = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_City"));
        txt_ContactPerson = (TextBox)(e.Item.FindControl("txt_ContactPerson"));
        txt_BillingName = (TextBox)(e.Item.FindControl("txt_BillingName"));
        txt_Address = (TextBox)(e.Item.FindControl("txt_Address"));
        txt_ContactNo = (TextBox)(e.Item.FindControl("txt_ContactNo"));
        txt_Email = (TextBox)(e.Item.FindControl("txt_Email"));

        objDT = SessionBindBillingGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Branch_ID"] = ddl_BillingBranch.SelectedValue;
            DR["Branch_Name"] = ddl_BillingBranch.SelectedItem;
            DR["City_ID"] = ddl_City.SelectedValue;
            DR["City_Name"] = ddl_City.SelectedItem;
            DR["Contact_Person"] = txt_ContactPerson.Text;
            DR["Billing_Name"] = txt_BillingName.Text;
            DR["Billing_Address"] = txt_Address.Text;
            DR["Contact_No"] = txt_ContactNo.Text;
            DR["email"] = txt_Email.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionBindBillingGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {

        lbl_Errors.Text = "";

        if (Util.String2Int(ddl_BillingBranch.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Billing Branch";// GetLocalResourceObject("Msg_ddl_BillingBranch").ToString();
            scm_ClientBilling.SetFocus(ddl_BillingBranch);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower()!= "nandwana" && txt_ContactPerson.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Contact Person";// GetLocalResourceObject("Msg_txt_ContactPerson").ToString();
            scm_ClientBilling.SetFocus(txt_ContactPerson);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower()!= "nandwana" && txt_BillingName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Billing Name";// GetLocalResourceObject("Msg_txt_BillingName").ToString();
            scm_ClientBilling.SetFocus(txt_BillingName);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel" && txt_Address.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Billing Address";// GetLocalResourceObject("Msg_txt_Address").ToString();
            scm_ClientBilling.SetFocus(txt_Address);
        }
        else if (Util.String2Int(ddl_City.SelectedValue) <= 0)
        {
            errorMessage = "Please Select City";// GetLocalResourceObject("Msg_ddl_City").ToString();
            scm_ClientBilling.SetFocus(ddl_City);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel" && txt_ContactNo.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Contact No";// GetLocalResourceObject("Msg_txt_ContactNo").ToString();
            scm_ClientBilling.SetFocus(txt_ContactNo);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel" && txt_Email.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Email";// GetLocalResourceObject("Msg_txt_Email").ToString();
            scm_ClientBilling.SetFocus(txt_Email);
        }
        else if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "excel" && !Check_Email(txt_Email))
        {
            errorMessage = "Please Enter Valid EmailId";// GetLocalResourceObject("Msg_Email_Valid").ToString();
            scm_ClientBilling.SetFocus(txt_Email);
        }
        else
        {

            isValid = true;
        }

        return isValid;
    }


    protected void dg_BillingDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionBindBillingGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["Branch_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindBillingGrid = SessionBindBillingGrid;
                    dg_BillingDetails.EditItemIndex = -1;
                    dg_BillingDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Billing Branch";// GetLocalResourceObject("Msg_Duplicate").ToString();
                scm_ClientBilling.SetFocus(ddl_BillingBranch);
            }
        }
    }
    protected void dg_BillingDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_BillingDetails.EditItemIndex = e.Item.ItemIndex;
        dg_BillingDetails.ShowFooter = false;
        BindBillingGrid = SessionBindBillingGrid;
    }
    protected void dg_BillingDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionBindBillingGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["Branch_ID"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_BillingDetails.EditItemIndex = -1;
                dg_BillingDetails.ShowFooter = true;

                BindBillingGrid = SessionBindBillingGrid;
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Billing Branch";// GetLocalResourceObject("Msg_Duplicate").ToString();
            scm_ClientBilling.SetFocus(ddl_BillingBranch);
        }
    }
    protected void dg_BillingDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_BillingDetails.EditItemIndex = -1;
        dg_BillingDetails.ShowFooter = true;
        BindBillingGrid = SessionBindBillingGrid;
    }
    protected void dg_BillingDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionBindBillingGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionBindBillingGrid = objDT;
            dg_BillingDetails.EditItemIndex = -1;
            dg_BillingDetails.ShowFooter = true;
            BindBillingGrid = SessionBindBillingGrid;
        }
    }

    private bool Check_Email(TextBox txt_Email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
            @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
            @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match = Regex.Match(txt_Email.Text.Trim(), pattern, RegexOptions.IgnoreCase);
                        
            if (match.Success)
                return true;
            else
                return false;
        }
}
