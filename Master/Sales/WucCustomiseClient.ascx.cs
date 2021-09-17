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
using System.Text;
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using Raj.EC.SalesPresenter;
using Raj.EC.SalesView;
using System.Data.SqlClient;
using Raj.EC;

public partial class Master_Sales_WucCustomiseClient : System.Web.UI.UserControl, ICustomiseClientView
{
    CustomiseClientPresenter objCustomiseClientPresenter;
    DataRow DR = null;
    bool isValid = false;
    ClassLibrary.UIControl.DDLSearch ddl_Client;
    LinkButton lnk_ClientView;
    DataTable objDT;
    int menuitemid = 36; // For Regular Client
    //int menuitemid = 24; // For Client Master
    #region ControlsBind

    public int ClientToBeKeptId
    {
        set { ddl_MergeClient.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_MergeClient.SelectedValue); }
    }

    public DataTable BindMergeClientGrid
    {
        set
        {
            dg_ClientDetails.DataSource = value;
            dg_ClientDetails.DataBind();
        }
    }
    public DataTable SessionBindMergeClientGrid
    {
        get { return StateManager.GetState<DataTable>("BindMergeClientGrid"); }
        set 
        { 
            StateManager.SaveState("BindMergeClientGrid", value);
            BindMergeClientGrid = value;
        }
    }
    public String MergeClientXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindMergeClientGrid.Copy());
            _objDs.Tables[0].TableName = "Client_Details";
            return _objDs.GetXml().ToLower();
        }
    }
   
    #endregion
    #region IView
    public bool validateUI()
    {
        errorMessage = "";
        bool _isValid = false;
        TextBox txtClientToKept;
        txtClientToKept = (TextBox)ddl_MergeClient.FindControl("txtBoxddl_MergeClient");

        if (ClientToBeKeptId <= 0)
        {
            errorMessage = "Please Select Client To Be Kept.";
            scm_CustomiseClient.SetFocus(txtClientToKept);
        }
        else if (SessionBindMergeClientGrid.Rows.Count <= 0)
        {
            errorMessage = "Please Add Atleast One Client To Be Merged.";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_MergeClient.DataTextField = "Client_Name";
        ddl_MergeClient.DataValueField = "Client_Id";
        ddl_MergeClient.OtherColumns = "ClientToKeep";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.SalesModel.CustomiseClientModel));

        objCustomiseClientPresenter = new CustomiseClientPresenter(this, IsPostBack);
        if(!IsPostBack)
            MakeDataset();
    }
    protected void ddl_MergeClient_TxtChange(object sender, EventArgs e)
    {
        StringBuilder ViewPath = new StringBuilder(Util.GetBaseURL());

        ViewPath.Append("/");
        ViewPath.Append(Rights.GetObject().GetLinkDetails(menuitemid).ViewUrl);
        ViewPath.Append("&Id=" + Util.EncryptInteger(Util.String2Int(ddl_MergeClient.SelectedValue)));

        lbtn_ViewMergeClient.Attributes.Add("onclick", "return Open_Main_Window('" + ViewPath + "')");
    }

    private void MakeDataset()
    {
        DataSet clientDs = new DataSet();
        DataTable clientDt = new DataTable();
        clientDt.Columns.Add("Client_Id");
        clientDt.Columns.Add("Client_Name");
        Common.SetPrimaryKeys(new string[] { "Client_Id" }, clientDt); 
        clientDs.Tables.Add(clientDt);
        SessionBindMergeClientGrid = clientDs.Tables[0];
    }

    protected void dg_ClientDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Client = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_Client"));
                ddl_Client.DataTextField = "Client_Name";
                ddl_Client.DataValueField = "Client_Id";
            }

            if (e.Item.ItemIndex != -1)
            {
                lnk_ClientView = (LinkButton)(e.Item.FindControl("lbtn_View"));
                StringBuilder ViewPath = new StringBuilder(Util.GetBaseURL());

                ViewPath.Append("/");
                ViewPath.Append(Rights.GetObject().GetLinkDetails(menuitemid).ViewUrl);
                ViewPath.Append("&Id=" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_Id").ToString())));

                lnk_ClientView.Attributes.Add("onclick", "return Open_View_Window('" + ViewPath + "')");
            }
        }
    }
    protected void dg_ClientDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        
        TextBox txtClient;
        ddl_Client = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_Client"));
        //txtClient = (TextBox)ddl_Client.FindControl("txtBoxddl_Client");

        if (e.CommandName == "Add")
        {
            try
            {
                Insert_Dataset(source, e);
                if (isValid == true)
                {
                    BindMergeClientGrid = SessionBindMergeClientGrid;
                    dg_ClientDetails.EditItemIndex = -1;
                    dg_ClientDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Client Name";
                scm_CustomiseClient.SetFocus(ddl_Client);
            }
        }
    }
    protected void dg_ClientDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionBindMergeClientGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionBindMergeClientGrid = objDT;
            dg_ClientDetails.ShowFooter = true;
            BindMergeClientGrid = SessionBindMergeClientGrid;

            if (SessionBindMergeClientGrid.Rows.Count > 0)
            {
                ddl_MergeClient.Enabled = false;
            }
            else
            {
                ddl_MergeClient.Enabled = true;
            }
        }

    }

    private void Insert_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_Client = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_Client"));
        objDT = SessionBindMergeClientGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        if (Allow_To_Add_Update() == true)
        {
            DR["Client_ID"] = ddl_Client.SelectedValue;
            DR["Client_Name"] = ddl_Client.SelectedItem;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionBindMergeClientGrid = objDT;
        }

        if (SessionBindMergeClientGrid.Rows.Count > 0)
        {
            ddl_MergeClient.Enabled = false;
        }
        else
        {
            ddl_MergeClient.Enabled = true;
        }
    }

    private bool Allow_To_Add_Update()
    {
        
        errorMessage = "";
        TextBox txtClient;
        txtClient = (TextBox)ddl_Client.FindControl("txtBoxddl_Client");

        if (Util.String2Int(ddl_Client.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Client To Be Merged.";
            scm_CustomiseClient.SetFocus(txtClient);
        }
        else if (ClientToBeKeptId == Util.String2Int(ddl_Client.SelectedValue))
        {
            errorMessage = "Client To Be Kept Should Not Be Equal To Client To Be Merged.";
            scm_CustomiseClient.SetFocus(txtClient);
        } 
        else
            isValid = true;

        return isValid;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objCustomiseClientPresenter.Save();
    }
}
