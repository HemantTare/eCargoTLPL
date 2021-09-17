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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 13th October 2008
/// Description   : This is the Page For Master Contract Terms Tab
/// </summary>


public partial class Master_Sales_WucContractTerms : System.Web.UI.UserControl,IContractTermsView 
{
    #region ClassVariables
    ContractTermsPresenter objContractTermsPresenter;
    DropDownList ddl_TermsHead;
    TextBox txt_Description;
    LinkButton lbtn_Delete;
    Label lbl_Terms_ID;
    DataSet objDS;
    private ScriptManager scm_ContractTerms;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValue

    public int TermsID
    {
        set
        {
            ddl_TermsHead.SelectedValue  = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_TermsHead.SelectedValue);
        }

    }
    public string TermsDescription
    {
        set
        {
            txt_Description.Text = value;
        }
        get
        {
            return txt_Description.Text;
        }

    }
    #endregion

    #region ControlsBind

    public DataSet Bind_dg_ContractTerms
    {
        set
        {
            SessionContractTermsGrid = value;
            dg_ContractTerms.DataSource = value;
            dg_ContractTerms.DataBind();
        }
    }

    public DataTable Bind_ddl_TermsHead
    {
        set
        {
            
            ddl_TermsHead.DataSource = value;
            ddl_TermsHead.DataTextField = "Term_Head";
            ddl_TermsHead.DataValueField = "Term_ID";
            ddl_TermsHead.DataBind();
            ddl_TermsHead.Items.Insert(0, new ListItem("--Select One--", "0"));
        }

    }
    public DataTable SessionTermsHead
    {
        get { return StateManager.GetState<DataTable>("TermsHead"); }
        set { StateManager.SaveState("TermsHead", value); }
    }
    public DataSet SessionContractTermsGrid
    {
        get { return StateManager.GetState<DataSet>("ContractTerms"); }
        set { StateManager.SaveState("ContractTerms", value); }
    }
    public string ContractTermsXML
    {        
        get
        {
            SessionContractTermsGrid.Tables[0].TableName = "TermsGrid";
            return SessionContractTermsGrid.GetXml().ToLower();
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


    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_ContractTerms = value; }
    }
    #endregion


    #region OtherMethods
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = SessionContractTermsGrid;
        DataRow DR = null;
        ddl_TermsHead = (DropDownList)e.Item.FindControl("ddl_TermsHead");
        txt_Description = (TextBox)e.Item.FindControl("txt_Description");

        lbl_Terms_ID = (Label)e.Item.FindControl("lbl_Terms_ID");

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

            DR["Term_ID"] = ddl_TermsHead.SelectedValue;
            DR["Term_Head"] = ddl_TermsHead.SelectedItem.Text;
            DR["Description"] = txt_Description.Text;
            if (e.CommandName == "ADD")
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionContractTermsGrid = objDS;
        }
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_TermsHead.SelectedValue) == 0)
        {
 
            errorMessage = "Please Select Terms Head";            
        }
        else
            isValid = true;

        return isValid;
    }
    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objContractTermsPresenter = new ContractTermsPresenter(this, IsPostBack);
        //hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Sales/App_LocalResources/WucClientGeneralDetails.ascx.resx");
    }
 
    protected void dg_ContractTerms_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ContractTerms.EditItemIndex = -1;
        dg_ContractTerms.ShowFooter = true;        
        Bind_dg_ContractTerms = SessionContractTermsGrid;
        Bind_ddl_TermsHead = SessionTermsHead;
    }

    protected void dg_ContractTerms_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionContractTermsGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionContractTermsGrid = objDS;
            dg_ContractTerms.EditItemIndex = -1;
            dg_ContractTerms.ShowFooter = true;            
            Bind_dg_ContractTerms = SessionContractTermsGrid;
            Bind_ddl_TermsHead = SessionTermsHead;
        }

    }
    protected void dg_ContractTerms_EditCommand(object source, DataGridCommandEventArgs e)
    {
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_ContractTerms.EditItemIndex = e.Item.ItemIndex;
        dg_ContractTerms.ShowFooter = false;
        Bind_dg_ContractTerms = SessionContractTermsGrid;
    }
    
    protected void dg_ContractTerms_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDS = SessionContractTermsGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["Term_ID"];
            objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_ContractTerms.EditItemIndex = -1;
                dg_ContractTerms.ShowFooter = true;                
                Bind_dg_ContractTerms = SessionContractTermsGrid;
                Bind_ddl_TermsHead = SessionTermsHead;
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Terms";          
            return;
        }

    }

    protected void dg_ContractTerms_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_TermsHead = (DropDownList)e.Item.FindControl("ddl_TermsHead");
                Bind_ddl_TermsHead = SessionTermsHead;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_TermsHead = (DropDownList)e.Item.FindControl("ddl_TermsHead");
                Bind_ddl_TermsHead = SessionTermsHead;
                objDS = SessionContractTermsGrid;
                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];

                ddl_TermsHead.SelectedValue = DR["Term_ID"].ToString();
            }
        }
    }

    protected void dg_ContractTerms_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            objDS = SessionContractTermsGrid;
            try
            {
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["Term_ID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {                    
                    Bind_dg_ContractTerms = SessionContractTermsGrid;
                    Bind_ddl_TermsHead = SessionTermsHead;
                    dg_ContractTerms.EditItemIndex = -1;
                    dg_ContractTerms.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Terms";                
                return;
            }
        }

    }

    protected void ddl_TermsHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_TermsHead = (DropDownList)sender;
        DataGridItem _item = (DataGridItem)ddl_TermsHead.Parent.Parent;
        txt_Description = (TextBox)_item.FindControl("txt_Description");


        if (TermsID > 0)
        {
            objContractTermsPresenter.GetTermDescraption();
            txt_Description.Enabled = true;
        }
        else
        {
            txt_Description.Text = "";
            txt_Description.Enabled = false;
        }
    }
    #endregion
}
