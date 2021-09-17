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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 5th December 2008
/// Description   : This is the Page For LHPO Forms Second Tab takes LHPO Attached Branches
/// </summary>
public partial class Operations_Outward_WucLHPOAttachedBranch : System.Web.UI.UserControl,ILHPOAttachedBranchView 
{
   
    #region ClassVariables
    LHPOAttachedBranchPresenter objLHPOAttachedBranchPresenter;
    ClassLibrary.UIControl.DDLSearch ddl_Branch;
    ClassLibrary.UIControl.DDLSearch ddl_FromLocation;
    LinkButton lbtn_Delete;    
    DataSet objDS;    
    Label lbl_BranchID;
    private ScriptManager scm_LHPOAttachedBranches;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion
    #region ControlsValue

    public int BranchID
    {
        get
        {
            return Util.String2Int(ddl_Branch.SelectedValue);
        }
    }
    public int MenuItemId
    {
        get { return Raj.EC.Common.GetMenuItemId(); }
    }

    #endregion
    #region ControlsBind
    public DataSet Bind_dg_AttachedLHPOBranches
    {
        set
        {     
            dg_AttachedLHPOBranches.DataSource = value;
            dg_AttachedLHPOBranches.DataBind();
            if (value.Tables[0].Rows.Count > 0)
            {
                Hdn_AttBranch.Value = value.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Hdn_AttBranch.Value = "0";
            }
        }
    }
    public string AttachedLHPOBranchesXML
    {
        get
        {
            return SessionAttachedLHPOBranchesGrid.GetXml().ToLower();
        }
    }
    public DataSet SessionAttachedLHPOBranchesGrid
    {
        get { return StateManager.GetState<DataSet>("AttachedBranches"); }
        set {StateManager.SaveState("AttachedBranches", value);}
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
    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_LHPOAttachedBranches = value; }
    }
    #endregion
    #region OtherMethods

    private void DisableControlForRectification()
    {
        dg_AttachedLHPOBranches.Enabled = false;
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = SessionAttachedLHPOBranchesGrid;
        DataRow DR = null;
        ddl_Branch = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_Branch");
        
        
        //WucLHPO1_WucLHPOAttachedBranch1_dg_AttachedLHPOBranches_ctl02_ddl_Branch_txtBoxddl_Branch
        lbl_BranchID = (Label)e.Item.FindControl("lbl_BranchID");

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

            DR["BranchID"] = ddl_Branch.SelectedValue;
            DR["BranchName"] = ddl_Branch.SelectedText;
         //   DR["Description"] = txt_Description.Text;
            if (e.CommandName == "ADD")
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionAttachedLHPOBranchesGrid = objDS;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Branch";
          
        }
        else
            isValid = true;

        return isValid;
    }
    public void BindGridAttachedBranches()
    {
        dg_AttachedLHPOBranches.DataSource = SessionAttachedLHPOBranchesGrid;
        dg_AttachedLHPOBranches.DataBind();

        if (SessionAttachedLHPOBranchesGrid.Tables[0].Rows.Count > 0)
        {
            Hdn_AttBranch.Value = SessionAttachedLHPOBranchesGrid.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Hdn_AttBranch.Value = "0";
        }
    }
    public void SetBranchID(string Branch_Name, string BranchID)
    {
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        Raj.EC.Common.SetValueToDDLSearch(Branch_Name, BranchID, ddl_Branch);
    }
    private void CheckLHPOTypeID()
    {
        int LHPOTypeID;
        dg_AttachedLHPOBranches.Enabled = true;        
        if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null)
        {
            LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
            if (LHPOTypeID == 2)
            {
                dg_AttachedLHPOBranches.Enabled = false;
            }
            else
            {
                dg_AttachedLHPOBranches.Enabled = true;             
            }
        }
    }    
    private void SetStandardCaption()
    {       
        pnl_dg_AttachedLHPOBranches.GroupingText = "Attached   " + CompanyManager.getCompanyParam().LHPOCaption + "   Branches";        
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {      
        objLHPOAttachedBranchPresenter = new LHPOAttachedBranchPresenter(this, IsPostBack);

        
        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                CheckLHPOTypeID();         
            }
        }
        SetStandardCaption();
        if (MenuItemId == 198)
        {
            DisableControlForRectification();
        }

        
    }
    public void FillGrid(object o, EventArgs e)
    {

        objLHPOAttachedBranchPresenter.FillGrid();
        Upd_Pnl_dg_AttachedLHPOBranches.Update();      
        CheckLHPOTypeID();
    }
    protected void dg_AttachedLHPOBranches_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_AttachedLHPOBranches.EditItemIndex = -1;
        dg_AttachedLHPOBranches.ShowFooter = true;
        BindGridAttachedBranches();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_AttachedLHPOBranches_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionAttachedLHPOBranchesGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionAttachedLHPOBranchesGrid = objDS;
            dg_AttachedLHPOBranches.EditItemIndex = -1;
            dg_AttachedLHPOBranches.ShowFooter = true;
            BindGridAttachedBranches();
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_AttachedLHPOBranches_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_AttachedLHPOBranches.EditItemIndex = e.Item.ItemIndex;
        dg_AttachedLHPOBranches.ShowFooter = false;
        BindGridAttachedBranches();
    }
    protected void dg_AttachedLHPOBranches_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            objDS = SessionAttachedLHPOBranchesGrid;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_AttachedLHPOBranches.EditItemIndex = -1;
                dg_AttachedLHPOBranches.ShowFooter = true;
                BindGridAttachedBranches();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Branch";
            return;
        }
    }
    protected void dg_AttachedLHPOBranches_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Branch = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_Branch");

            }          
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Branch = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_Branch");
                objDS = SessionAttachedLHPOBranchesGrid;

                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
                               
                SetBranchID(DR["BranchName"].ToString(), DR["BranchID"].ToString());
            }           
        }
    }
    protected void dg_AttachedLHPOBranches_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            objDS = SessionAttachedLHPOBranchesGrid;
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindGridAttachedBranches();
                    dg_AttachedLHPOBranches.EditItemIndex = -1;
                    dg_AttachedLHPOBranches.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Branch";
                return;
            }
        }
    }
}
