using System;
using System.Data;
using System.Configuration;
//using System.Diagnostics;
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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 13th October 2008
/// Description   : This is the Page For Master Contract General Tab
/// </summary>


public partial class Master_Sales_WucContractGeneral : System.Web.UI.UserControl,IContractGeneralView 
{
    #region ClassVariables
    ContractGeneralPresenter objContractGeneralPresenter;
    PageControls pc = new PageControls();  //added ankit
    private ScriptManager scm_ContractGeneral;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion
    
    //Added :Anita On: 05 Feb 09
    #region OtherMethod
    private bool CheckDuplicateContractName()
    {
        bool DuplicateContractName = objContractGeneralPresenter.IsDuplicateContractName();
        return DuplicateContractName;
    }
    #endregion    

    #region ControlsValue
    
    public string ContractNo
    {
        set{lbl_ContractNoValue.Text = value;}
        get{return lbl_ContractNoValue.Text; }
    }
    //Added :Anita On: 05 Feb 09
    public string ContractName
    {
        set { txt_ContractName.Text = value; }
        get { return txt_ContractName.Text; }
    }
    public string ClientPONo
    {
        set{txt_ClientPONo.Text = value;}
        get{return txt_ClientPONo.Text;}
    }
    public string Remark
    {
        set{txt_Remark.Text = value;}
        get{return txt_Remark.Text; }
    }
    public int BranchID
    {      
        get{return Util.String2Int(ddl_Branch.SelectedValue);}
    }       
    public int ClientNameID
    {        
        get{return Util.String2Int(ddl_ClientName.SelectedValue);}
    }
    public int BillingClientID
    {
        get { return Util.String2Int(ddl_BillingClientName.SelectedValue); }
    }
    public int BillingBranchID
    {
        get { return WucHierarchywithID.MainId; }
        set { WucHierarchywithID.MainId = value; }
    }

    public string BillingHierarchy
    {
        get { return WucHierarchywithID.HierarchyCode; }
        set { WucHierarchywithID.HierarchyCode = value; }
    }

    public int Days
    {
        set { txt_Days.Text = Util.Int2String(value); }
        get { return txt_Days.Text == string.Empty ? 0 : Util.String2Int(txt_Days.Text); }
    }
    public decimal POMaxLimit
    {
        set { txt_POMaxLimit.Text = Util.Decimal2String(value); }
        get { return txt_POMaxLimit.Text == string.Empty ? 0 : Util.String2Decimal(txt_POMaxLimit.Text); }
    }
    public decimal Weight
    {
        set { txt_Weight.Text = Util.Decimal2String(value); }
        get { return txt_Weight.Text == string.Empty ? 0 : Util.String2Decimal(txt_Weight.Text); }
    }
    public decimal Freight
    {
        set { txt_Freight.Text = Util.Decimal2String(value); }
        get { return txt_Freight.Text == string.Empty ? 0 : Util.String2Decimal(txt_Freight.Text); }
    }
    public decimal Amount
    {
        set { txt_Amount.Text = Util.Decimal2String(value); }
        get { return txt_Amount.Text == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text); } //added : condition added by Ankit
    }
    public DateTime ContractDate
    {
        set{WucContractDate.SelectedDate = value;}
        get{return WucContractDate.SelectedDate;}
    }
    public DateTime PODate
    {
        set{WucPODate.SelectedDate = value;}
        get{return WucPODate.SelectedDate;}
    }
    public DateTime ValidFromDate
    {
        set{WucValidFrom.SelectedDate = value;}
        get{ return WucValidFrom.SelectedDate;}
    }
    public DateTime ValidUptoDate
    {
        set{WucValidUpto.SelectedDate = value;}
        get{return WucValidUpto.SelectedDate;}
    }
    public int GCRiskId
    {
        set{ddl_GCRisk.SelectedValue = Util.Int2String(value);}
        get{return Convert.ToInt32(ddl_GCRisk.SelectedValue);}
    }
    public int ConsignmentTypeId
    {
        set { ddl_ConsignmentType.SelectedValue = Util.Int2String(value); }
        get { return Convert.ToInt32(ddl_ConsignmentType.SelectedValue); }
    }
    #endregion
        
    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        TextBox Txt_BB, Txt_BP, Txt_Branch, Txt_Client;

        Txt_Client = (TextBox)ddl_ClientName.FindControl("txtBoxddl_ClientName");
        Txt_Branch = (TextBox)ddl_Branch.FindControl("txtBoxddl_Branch");
        //Txt_BB = (TextBox)ddl_BillingBranchName.FindControl("txtBoxddl_BillingBranchName");
        Txt_BP = (TextBox)ddl_BillingClientName.FindControl("txtBoxddl_BillingClientName");

        //Added :Anita On: 05 Feb 09
        if (txt_ContractName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please enter Contract Name.";
            scm_ContractGeneral.SetFocus(txt_ContractName);
        }
        else if(CheckDuplicateContractName())
        {
            errorMessage="Duplicate Contract Name.";
        }
        ////-------------------------------------------
        else if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Branch";// GetLocalResourceObject("Msg_Branch").ToString();
            scm_ContractGeneral.SetFocus(Txt_Branch);
        }   
        else if (Util.String2Int(ddl_ClientName.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Client Name";// GetLocalResourceObject("Msg_ClientName").ToString();
            scm_ContractGeneral.SetFocus(Txt_Client);
        }             
        else if (txt_POMaxLimit.Text == string.Empty && pc.Control_Is_Mandatory(txt_POMaxLimit) == true)
        {
            errorMessage = "Please Enter PO Max Limit";// GetLocalResourceObject("Msg_POMaxLimit").ToString();
            scm_ContractGeneral.SetFocus(txt_POMaxLimit);
        }
        else if (ContractDate > DateTime.Now)
        {
            errorMessage = "Contract Date Should be Less then Current date";// GetLocalResourceObject("Msg_ContractDate1").ToString();
        }
        else if (PODate > DateTime.Now)
        {
            errorMessage = "PO Date Should be Less then Current date";// GetLocalResourceObject("Msg_PODate").ToString();
        }
        //else if (PODate < ContractDate)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_PODate1").ToString();
        //}
        else if (PODate > ValidFromDate)
        {
            errorMessage = " PO Date Cannot Be Greater Than  Contract Commencement Date (Valid From)";
        }
        else if (ValidFromDate < ContractDate)
        {
            errorMessage = "Valid From Date Should be Greater then or Equal to Contract Date";// GetLocalResourceObject("Msg_ValidFromDate").ToString();
        }
        else if (ValidFromDate > ValidUptoDate)
        {
            errorMessage = "Valid From Date Should be Less then Valid Upto Date";// GetLocalResourceObject("Msg_ValidFrom").ToString();
        }
        else if (Util.String2Int(ddl_BillingClientName.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Billing Party";// GetLocalResourceObject("Msg_BillingClientName").ToString();
            scm_ContractGeneral.SetFocus(Txt_BP);
        }
        //else if (Util.String2Int(ddl_BillingBranchName.SelectedValue) <= 0)
        //{
        //    errorMessage = "Please Select Billing Branch";// GetLocalResourceObject("Msg_BillingBranchName").ToString();
        //    scm_ContractGeneral.SetFocus(Txt_BB);
        //}
        else if (WucHierarchywithID.HierarchyCode == "0") 
        {
            errorMessage = "Please Select Billing Hierarchy";
        }
        else if(WucHierarchywithID.HierarchyCode != "0" && WucHierarchywithID.HierarchyCode != "HO" && WucHierarchywithID.MainId <= 0)
        {
            if (WucHierarchywithID.HierarchyCode == "BO")
            {
                errorMessage = "Please Select Billing Branch";
            }

            if (WucHierarchywithID.HierarchyCode == "AO")
            {
                errorMessage = "Please Select Billing Area";
            }

            if (WucHierarchywithID.HierarchyCode == "RO")
            {
                errorMessage = "Please Select Billing Region";
            }
        }
        else if (txt_Weight.Text == string.Empty && pc.Control_Is_Mandatory(txt_Weight) == true)
        {
            errorMessage = "Please Enter Weight";// GetLocalResourceObject("Msg_Weight").ToString();
            scm_ContractGeneral.SetFocus(txt_Weight);
        }
        else if (txt_Freight.Text == string.Empty && pc.Control_Is_Mandatory(txt_Freight) == true)
        {
            errorMessage = "Please Enter Freight";// GetLocalResourceObject("Msg_Freight").ToString();
            scm_ContractGeneral.SetFocus(txt_Freight);
        }
        else if (txt_Days.Text == string.Empty && pc.Control_Is_Mandatory(txt_Days) == true)
        {
            errorMessage = "Please Enter Days";// GetLocalResourceObject("Msg_Days").ToString();
            scm_ContractGeneral.SetFocus(txt_Days);
        }
        else if (txt_Amount.Text == string.Empty && pc.Control_Is_Mandatory(txt_Amount) == true)
        {
            errorMessage = "Please Enter Amount";// GetLocalResourceObject("Msg_Amount").ToString();
            scm_ContractGeneral.SetFocus(txt_Amount);
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    public string errorMessage
    {
        set{lbl_Errors.Text = value;}
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
    public DataTable BindGCRiskType
    {
        set
        {
            ddl_GCRisk.DataSource = value;
            ddl_GCRisk.DataTextField = "Risk_Type";
            ddl_GCRisk.DataValueField = "Risk_Type_ID";
            ddl_GCRisk.DataBind();
        }
    }
    public DataTable BindConsignmentType
    {
        set
        {
            ddl_ConsignmentType.DataSource = value;
            ddl_ConsignmentType.DataTextField = "consignment_Type";
            ddl_ConsignmentType.DataValueField = "Consignment_Type_Id";
            ddl_ConsignmentType.DataBind();
        }
    }
    #endregion 

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_ContractGeneral = value; }
    }
    #endregion

    #region OtherMethods
    public void SetBranchID(string Branch_Name, string BranchID)
    {
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        Raj.EC.Common.SetValueToDDLSearch(Branch_Name, BranchID, ddl_Branch);
    }

    public void SetClientID(string Client_Name, string ClientID)
    {
        ddl_ClientName.DataTextField = "Client_Name";
        ddl_ClientName.DataValueField = "Client_Id";
        ddl_ClientName.OtherColumns = BranchID.ToString();
        Raj.EC.Common.SetValueToDDLSearch(Client_Name, ClientID, ddl_ClientName);
    }
    public void SetBillingClientID(string BillingClientName, string BillingClientID)
    {
        ddl_BillingClientName.DataTextField = "Client_Name";
        ddl_BillingClientName.DataValueField = "Client_Id";
        Raj.EC.Common.SetValueToDDLSearch(BillingClientName, BillingClientID, ddl_BillingClientName);
    }
    //public void SetBillingBranchID(string BillingBranchName, string BillingBranchId)
    //{
    //    ddl_BillingBranchName.DataTextField = "Branch_Name";
    //    ddl_BillingBranchName.DataValueField = "Branch_Id";
    //    Raj.EC.Common.SetValueToDDLSearch(BillingBranchName, BillingBranchId, ddl_BillingBranchName);
    //}
    private void SetStandardCaption()
    {
        lbl_GCRisk.Text = CompanyManager.getCompanyParam().GcCaption + " Risk ";
    }

    #endregion

    #region ControlsEvent
    //void Page_Error(object sender, EventArgs e)
    //{
    //    Response.Write("Error:\n");
    //    Exception e = Server.GetLastError();

    //    EventLog.WriteEntry("Test Web",
    //    "MESSAGE: " + e.Message +
    //    "\nSOURCE: " + e.Source +
    //    "\nFORM: " + Request.Form.ToString() +
    //    "\nQUERYSTRING: " + Request.QueryString.ToString() +
    //    "\nTARGETSITE: " + e.TargetSite +
    //    "\nSTACKTRACE: " + e.StackTrace,
    //    EventLogEntryType.Error);

    //    Trace.Write("Message", e.Message);
    //    Trace.Write("Source", e.Source);
    //    Trace.Write("Stack Trace", e.StackTrace);
    //    Response.Write("Sorry, an error was encountered.");
    //    Context.ClearError();
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);  //added : Ankit 
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        ddl_ClientName.DataTextField = "Client_Name";
        ddl_ClientName.DataValueField = "Client_Id";

        //ddl_BillingBranchName.DataTextField = "Branch_Name";
        //ddl_BillingBranchName.DataValueField = "Branch_Id";
        ddl_BillingClientName.DataTextField = "Client_Name";
        ddl_BillingClientName.DataValueField = "Client_Id";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        DataSet ds = new DataSet();
        ds = ObjCommon.Get_Values_Where("ec_master_company_gc_parameter", "Is_Multiple_Location_Billing_Allowed", "", "Is_Multiple_Location_Billing_Allowed", false);

        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Is_Multiple_Location_Billing_Allowed"]) == true)
        {
            WucHierarchywithID.Allow_All_Hierarchy = true;
        }
        else
        {
            WucHierarchywithID.HierarchyCode = "BO";
        }

        WucHierarchywithID.Set_Hierarchy_Caption = "Billing Hierarchy :";
        WucHierarchywithID.DDLHierarchyChange += new EventHandler(SetHierarchyCaption);

        objContractGeneralPresenter = new ContractGeneralPresenter(this, IsPostBack);
        
        if (!IsPostBack)
        {
            //hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Sales/App_LocalResources/WucContractGeneral.ascx.resx");           

            if (keyID <= 0)
            {
               ContractNo = ObjCommon.Get_Next_Number();
            }
        }
        SetStandardCaption();
        ddl_ClientName.OtherColumns = BranchID.ToString();

    }


    protected void ddl_Branch_TxtChange(object sender, EventArgs e)
    {
        TextBox txt_client = (TextBox)ddl_ClientName.FindControl("txtBoxddl_ClientName");
        txt_client.Text = "";
        ddl_ClientName.OtherColumns = BranchID.ToString();

        if (Util.String2Int(ddl_Branch.SelectedValue) > 0)
        {
            //SetBillingBranchID(ddl_Branch.SelectedText, ddl_Branch.SelectedValue);
            WucHierarchywithID.HierarchyCode = "BO";
            WucHierarchywithID.MainId = BranchID;
        }
        else
        {
            WucHierarchywithID.HierarchyCode = "0";
        }

        WucHierarchywithID.Set_Default_Values(sender, e);
        SetHierarchyCaption(sender,e);
        scm_ContractGeneral.SetFocus(txt_client);
    }


    protected void ddl_ClientName_TxtChange(object sender, EventArgs e)
    {
        if (Util.String2Int( ddl_ClientName.SelectedValue  ) > 0)
        {
            SetBillingClientID(ddl_ClientName.SelectedText, ddl_ClientName.SelectedValue);
        }
        else
        {
            SetBillingClientID("", "0");
        }

        //scm_ContractGeneral.SetFocus(ddl_ClientName);
    }


    #endregion

    public void SetHierarchyCaption(object sender,EventArgs e)
    {
        if (WucHierarchywithID.HierarchyCode == "BO")
        {
            WucHierarchywithID.Set_Location_Caption = "Billing Branch :";
        }
        else if (WucHierarchywithID.HierarchyCode == "AO")
        {
            WucHierarchywithID.Set_Location_Caption = "Billing Area :";
        }
        else if (WucHierarchywithID.HierarchyCode == "RO")
        {
            WucHierarchywithID.Set_Location_Caption = "Billing Region :";
        }
    
    }
}
