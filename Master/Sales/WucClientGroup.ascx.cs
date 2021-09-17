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
using Raj.EC.SalesPresenter;
using Raj.EC.SalesView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;

public partial class Master_Sales_WucClientGroup : System.Web.UI.UserControl, IClientGroupView
{
    #region ClassVariables
    ClientGroupPresenter objClientGroupPresenter;
    #endregion

    #region ControlsValue

    public string ClientGroupName
    {
        set { txt_ClientGroupName.Text = value; }
        get { return txt_ClientGroupName.Text; }
    }
    public int ParentGroupId
    {
        set { ddl_ParentGroupName.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ParentGroupName.SelectedValue); }
    }

    //public bool LedgerGroupForRadio
    //{
    //    set
    //    {
    //        rbl_Ledgergroup.Items[0].Selected = (!value);
    //        rbl_Ledgergroup.Items[1].Selected = value;
    //        if (keyID > 0 && rbl_Ledgergroup.Items[1].Selected)
    //        {
    //            ddl_LedgerGroup.Enabled = false;
    //        }
    //    }
    //    get
    //    {
    //        if (rbl_Ledgergroup.SelectedValue == "0")
    //            return false;
    //        else
    //            return true;
    //    }
    //}

    public bool LedgerGroupForRadio
    {
        set
        {
            rbl_Ledgergroup.Items[0].Selected = value;
            rbl_Ledgergroup.Items[1].Selected = !value;
            if (keyID > 0 && rbl_Ledgergroup.Items[1].Selected == true)
            {
                ddl_LedgerGroup.Enabled = value;
                EnableRadio = value;
            }
        }
        get
        {
            return rbl_Ledgergroup.Items[0].Selected;
        }
    }

    public bool EnableRadio
    {
        set { rbl_Ledgergroup.Enabled = value; }
        get { return rbl_Ledgergroup.Enabled; }
    }

    public int LedgerGroupId
    {
        set { ddl_LedgerGroup.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_LedgerGroup.SelectedValue); }
    }
    #endregion

    #region ControlsBind
    public DataSet BindParentGroup
    {
        set
        {
            ddl_ParentGroupName.DataSource = value;
            ddl_ParentGroupName.DataTextField = "Client_Group_Name";
            ddl_ParentGroupName.DataValueField = "Client_Group_ID";
            ddl_ParentGroupName.DataBind();
            ddl_ParentGroupName.Items.Insert(0, new ListItem("Primary", "0"));
        }
    }

    public DataSet BindLedgerGroup
    {
        set
        {
            ddl_LedgerGroup.DataSource = value;
            ddl_LedgerGroup.DataTextField = "Ledger_Group_Name";
            ddl_LedgerGroup.DataValueField = "Ledger_Group_Id";
            ddl_LedgerGroup.DataBind();
            ddl_LedgerGroup.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;


        if (txt_ClientGroupName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Client Group Name";
            txt_ClientGroupName.Focus();
        }           
        else if (Util.String2Int(rbl_Ledgergroup.SelectedValue) == 1 && Util.String2Int(ddl_LedgerGroup.SelectedValue) == 0)
        {
            errorMessage = "Please Select Ledger Group Name";
            ddl_LedgerGroup.Focus();
        }
        else if (keyID>0 &&  objClientGroupPresenter.IsClientGroupChanged()==false)
        {
            errorMessage = "Client Group Name Should Not be Changed";
            txt_ClientGroupName.Focus();
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
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    #endregion
    
    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        objClientGroupPresenter = new ClientGroupPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                //objClientGroupPresenter.FillLedgerGroup();
            }
            else
            {
                rbl_Ledgergroup_SelectedIndexChanged(sender, e);
            }
        } 
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objClientGroupPresenter.Save();
    }
    #endregion

    protected void rbl_Ledgergroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_Ledgergroup.Items[1].Selected)
        {
            tr_ledgergrp.Visible = false;
        }
        else
        {
            tr_ledgergrp.Visible = true;
        }
    }
    protected void ddl_ParentGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objClientGroupPresenter.FillLedgerGroup();
    }
}