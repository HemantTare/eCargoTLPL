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

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 26/04/2008
/// Description   : This Page is For Master Driver Insurance Dependent Details
/// and is used in 2nd tab
/// 
/// Author        : Pankaj
/// Created On    : 30 apr 08
/// Description   : updated grid events code
/// </summary>
/// 

public partial class Master_Driver_WucDriverInsuranceDependent : System.Web.UI.UserControl, IDriverInsuranceDependentView
{
    #region ClassVariables
    DriverInsuranceDependentPresenter objDriverInsuranceDependentPresenter;
    TextBox txt_Dependent_Name;
    TextBox txt_Age, txt_MobileNo, txt_StatyingAt;
    DropDownList ddl_Relation;
    bool isValid = false;
    public ScriptManager _Driverinformation;
    #endregion

    #region ValidateUI
    public bool validateUI()
    {
        bool _isValid = false;
        if (InsuranceCompanyID != 0 && InsuranceBranchID  <= 0)
        {
            errorMessage = "Please Select Insurance Branch";// GetLocalResourceObject("Msg_ddl_InsuranceBranch").ToString();
            ddl_Insurance_Branch.Focus();
        }
        else if (InsuranceCompanyID != 0 && PolicyNumber == string.Empty)
        {
            errorMessage = "Please Enter Policy Number";// GetLocalResourceObject("Msg_Txt_PolicyNumber").ToString();
            txt_Policy_Number.Focus();
        }
        else if (InsuranceCompanyID != 0 && InsurancePremium <= 0)
        {
            errorMessage = "Please Enter Insurance Premium Greater Than Zero";// GetLocalResourceObject("Msg_txt_InsurancePremium").ToString();
            txt_Insu_Premium.Focus();
        }
        else if (InsuranceCompanyID != 0 && SumAssured <= 0)
        {
            errorMessage = "Please Enter Sum Assured Value Greater Than Zero";// GetLocalResourceObject("Msg_txt_SumAssured").ToString();
            txt_Sum_assured.Focus();
        }
        else if (InsuranceCompanyID != 0 && NomineeName == string.Empty)
        {
            errorMessage = "Please Enter Nominee Name";// GetLocalResourceObject("Msg_txt_NomineeName").ToString();
            txt_Nominee_Name.Focus();
        }
        else if (InsuranceCompanyID != 0 && NomineeRelationID == 0)
        {
            errorMessage = "Please Select Nominee Relation";// GetLocalResourceObject("Msg_ddl_NomineeRelation").ToString();
            ddl_Nominee_Relation.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }
    #endregion

    #region Iview Implementation

    public string NomineeName
    {
        set { txt_Nominee_Name.Text = value; }
        get { return txt_Nominee_Name.Text; }
    }
    public string PolicyNumber
    {
        set { txt_Policy_Number.Text = value; }
        get { return txt_Policy_Number.Text; }
    }
    public string Driver_Dependent_Details
    {
        get { return SessionDependentDetailsGrid.GetXml(); }
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int InsuranceCompanyID
    {
        set { ddl_Insu_Company.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Insu_Company.SelectedValue); }
    }
    public int InsuranceBranchID
    {
        set { ddl_Insurance_Branch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Insurance_Branch.SelectedValue); }
    }
    public int InsuranceAgentID
    {
        set { ddl_Insurance_Agent.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Insurance_Agent.SelectedValue); }
    }
    public int NomineeRelationID
    {
        set { ddl_Nominee_Relation.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Nominee_Relation.SelectedValue); }
    }
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
            //return 6;
        }
    }

    public decimal InsurancePremium
    {
        set { txt_Insu_Premium.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Insu_Premium.Text); }
    }
    public decimal SumAssured
    {
        set { txt_Sum_assured.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Sum_assured.Text); }
    }

    public DateTime InsuranceExpiryDate
    {
        set { Insurance_Expiry_Date.SelectedDate = value; }
        get { return Insurance_Expiry_Date.SelectedDate; }
    }

    public DataTable BindInsuranceCompany
    {
        set
        {
            ddl_Insu_Company.DataTextField = "Insurance_Company";
            ddl_Insu_Company.DataValueField = "Insurance_Company_ID";
            ddl_Insu_Company.DataSource = value;
            ddl_Insu_Company.DataBind();

            Raj.EC.Common.InsertItem(ddl_Insu_Company);
        }
    }
    public DataTable BindInsuranceAgent
    {
        set
        {
            ddl_Insurance_Agent.DataTextField = "Vendor_Name";
            ddl_Insurance_Agent.DataValueField = "Vendor_ID";
            ddl_Insurance_Agent.DataSource = value;
            ddl_Insurance_Agent.DataBind();
         
            Raj.EC.Common.InsertItem(ddl_Insurance_Agent);
        }
    }
    public DataTable BindNomineeRelation
    {
        set
        {
            ddl_Nominee_Relation.DataTextField = "Family_Relation";
            ddl_Nominee_Relation.DataValueField = "Family_Relation_ID";
            ddl_Nominee_Relation.DataSource = value;
            ddl_Nominee_Relation.DataBind();

            Raj.EC.Common.InsertItem(ddl_Nominee_Relation);
          
        }
    }
    public DataTable SessionDepRelationDropDown
    {
        get { return StateManager.GetState<DataTable>("DepRelationDropDown"); }
        set { StateManager.SaveState("DepRelationDropDown", value); }
    }

    public DataSet BindInsuranceBranch
    {
        set
        {
            ddl_Insurance_Branch.DataTextField = "Branch_Name";
            ddl_Insurance_Branch.DataValueField = "Insurance_Branch_ID";
            ddl_Insurance_Branch.DataSource = value;
            ddl_Insurance_Branch.DataBind();

            Raj.EC.Common.InsertItem(ddl_Insurance_Branch);
          //  UpdatePanel1.Update();
        }
    }
    public DataSet SessionDependentDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("InsuranceDependentGrid"); }
        set { StateManager.SaveState("InsuranceDependentGrid", value); }
    }

    private void BindDependentDetailsGrid()
    {
        Set_Sr_No();
        dg_Dep_Details.DataSource = SessionDependentDetailsGrid;
        dg_Dep_Details.DataBind();
    }
    #endregion

    #region private function and prcoedures

    private DataTable BindDDLRelation
    {
        set
        {
            ddl_Relation.DataTextField = "Family_Relation";
            ddl_Relation.DataValueField = "Family_Relation_ID";
            ddl_Relation.DataSource = value;
            ddl_Relation.DataBind();

            Raj.EC.Common.InsertItem(ddl_Relation);
        }
    }
    
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        objDriverInsuranceDependentPresenter = new DriverInsuranceDependentPresenter(this, IsPostBack);
        if (IsPostBack == false)
        {
            //if (!IsPostBack)
            //{
            //    Common ObjCommon = new Common();
            //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Driver/App_LocalResources/WucDriverInsuranceDependent.ascx.resx");
            //}
            BindDependentDetailsGrid();
           
        }

       String Script = "<script type='text/javascript'>Hide_Control(); </script>";
       System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }
    //objDriverInsuranceDependentPresenter.FillInsuranceBranchOnInsuranceCompanyChange();

    //protected void ddl_Insu_Company_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objDriverInsuranceDependentPresenter.FillInsuranceBranchOnInsuranceCompanyChange();
    //}

    #endregion

    #region Function
    private void Set_Sr_No()
    {
        int Sr_No;
        DataSet DS = SessionDependentDetailsGrid;
        DataRow DR = null;
        for (Sr_No = 0; Sr_No <= DS.Tables[0].Rows.Count - 1; Sr_No++)
        {
            DR = DS.Tables[0].Rows[Sr_No];
            DR["Sr_No"] = Sr_No + 1;
        }
        SessionDependentDetailsGrid = DS;
    }
    #endregion

    #region GridControlsEvents
    protected void dg_Dep_Details_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Dep_Details.EditItemIndex = -1;
        dg_Dep_Details.ShowFooter = true;
        //Set_Sr_No();
        BindDependentDetailsGrid();

    }

    protected void dg_Dep_Details_EditCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        //lbtn_Delete.Enabled = false;
        dg_Dep_Details.EditItemIndex = e.Item.ItemIndex;
        dg_Dep_Details.ShowFooter = false;
        BindDependentDetailsGrid();
    }

    protected void dg_Dep_Details_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindDependentDetailsGrid();
                dg_Dep_Details.EditItemIndex = -1;
                dg_Dep_Details.ShowFooter = true;
            }
        }
    }

    protected void dg_Dep_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                txt_Dependent_Name = (TextBox)(e.Item.FindControl("txt_Dep_Name_Add"));
                txt_Age = (TextBox)(e.Item.FindControl("txt_Age_Add"));
                ddl_Relation = (DropDownList)(e.Item.FindControl("ddl_Relation"));
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                txt_Dependent_Name = (TextBox)(e.Item.FindControl("txt_Dep_Name_Edit"));
                txt_Age = (TextBox)(e.Item.FindControl("txt_Age_Edit"));
                ddl_Relation = (DropDownList)(e.Item.FindControl("ddl_Relation"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindDDLRelation = SessionDepRelationDropDown;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataSet DS = SessionDependentDetailsGrid;
                DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];

                txt_Dependent_Name.Text = DR["Dependent_Name"].ToString();
                txt_Age.Text = DR["Age"].ToString();
                ddl_Relation.SelectedValue = DR["Family_Relation_ID"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataSet DS = SessionDependentDetailsGrid;
        DataRow DR = null;
        if (e.CommandName == "Add" )
        {
            txt_Dependent_Name = (TextBox)(e.Item.FindControl("txt_Dep_Name_Add"));
            txt_Age = (TextBox)(e.Item.FindControl("txt_Age_Add"));
            txt_MobileNo = (TextBox)(e.Item.FindControl("txt_MobileNo_Add"));
            txt_StatyingAt = (TextBox)(e.Item.FindControl("txt_StatyingAt_Add"));

            ddl_Relation = (DropDownList)(e.Item.FindControl("ddl_Relation"));

            DR = DS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update" )
        {
            txt_Dependent_Name = (TextBox)(e.Item.FindControl("txt_Dep_Name_Edit"));
            txt_Age = (TextBox)(e.Item.FindControl("txt_Age_Edit"));
            txt_MobileNo = (TextBox)(e.Item.FindControl("txt_MobileNo_Edit"));
            txt_StatyingAt = (TextBox)(e.Item.FindControl("txt_StatyingAt_Edit"));

            ddl_Relation = (DropDownList)(e.Item.FindControl("ddl_Relation"));

            DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Dependent_Name"] = txt_Dependent_Name.Text;
            DR["Family_Relation_ID"] = ddl_Relation.SelectedValue;
            DR["Family_Relation"] = ddl_Relation.SelectedItem.Text ;
            DR["Age"] = txt_Age.Text;
            DR["MobileNo"] = txt_MobileNo.Text;
            DR["StayingAt"] = txt_StatyingAt.Text;


            if (e.CommandName == "Add") {DS.Tables[0].Rows.Add(DR);}
            SessionDependentDetailsGrid = DS;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (txt_Dependent_Name.Text == string.Empty)
        {
            errorMessage = "Please Enter Dependent Name";
            txt_Dependent_Name.Focus();
        }
        else if (ddl_Relation.SelectedValue == "0" || ddl_Relation.SelectedValue == "")
        {
            errorMessage = "Please Select Relation";
            ddl_Relation.Focus();
        }
        else if (txt_Age.Text == string.Empty)
        {
            errorMessage = "Please Enter Age";
            txt_Age.Focus();
        }
        else if (txt_StatyingAt.Text == string.Empty)
        {
            errorMessage = "Please Enter Staying At";
            txt_StatyingAt.Focus();
        }

        else
            isValid = true;

        return isValid;
    }

    protected void dg_Dep_Details_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (isValid == true)
        {
            dg_Dep_Details.EditItemIndex = -1;
            dg_Dep_Details.ShowFooter = true;

            BindDependentDetailsGrid();
        }
    }

    protected void dg_Dep_Details_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet DS = SessionDependentDetailsGrid;
        DataRow DR = DS.Tables[0].Rows[e.Item.ItemIndex];
        DR.Delete();
        DR.AcceptChanges();
        SessionDependentDetailsGrid = DS;
        BindDependentDetailsGrid();

    }
    #endregion


    protected void ddl_Insu_Company_SelectedIndexChanged1(object sender, EventArgs e)
    {
        objDriverInsuranceDependentPresenter.FillInsuranceBranchOnInsuranceCompanyChange();
    }
}
    