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
using Raj.EC;
using ClassLibraryMVP;

public partial class Operations_Booking_NewGC_FrmNewGCBillingDetails : System.Web.UI.Page
{
    DataRow dr;
    ClassLibrary.UIControl.DDLSearch ddl_BillingParty;
    Controls_WucHierarchyWithID WucHierarchyWithID1;
    TextBox txt_Description, txt_Ratio;
    CheckBox chk_Is_Service_Tax_Applicable;
    bool Allow_To_Save;

    Common objComm = new Common();
    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    #endregion

    #region Controls
   
    public DataTable Session_BillingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("BillingDetailsGrid"); }
        set { StateManager.SaveState("BillingDetailsGrid", value); }
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
            lbl_TotalRatio.Text = value.ToString();
            hdn_TotalRatio.Value = value.ToString();
        }
        get { return hdn_TotalRatio.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalRatio.Value); }
    }
       
    private bool IsMultipleLocationBillingAllow
    {
        get { return Util.String2Bool(hdn_IsMultipleLocationBillingAllowed.Value); }
    }

    public void SetBillingClient(string FromLocation_Name, string FromLocationID)
    {
        ddl_BillingParty.DataTextField = "Billing_Client_Name";
        ddl_BillingParty.DataValueField = "Billing_Client_ID";
        Raj.EC.Common.SetValueToDDLSearch(FromLocation_Name, FromLocationID, ddl_BillingParty);
    }

    #endregion
  
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        hdn_Mode.Value = Request.QueryString["Mode"].ToString();
        hdn_IsMultipleLocationBillingAllowed.Value  = Request.QueryString["IsMultipleLocBillingAllow"].ToString();

        if (!IsPostBack)
        {
            Bind_BillingGrid();

            if (hdn_Mode.Value == "4")
            {
                btn_Exit.Visible = true;
                btn_Exit.Enabled = true;
                dg_Billing.ShowFooter = false;
                dg_Billing.Columns[4].Visible = false;
                dg_Billing.Columns[5].Visible = false;
            }
        }
        SetStandardCaption();
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
    private void updateparentdataset()
    {
        string popupScript = "<script language='javascript'>updateparentdataset(" + hdn_Is_Service_Tax_Applicable.Value + ");</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
    }
    public void Is_service_Tax_Applicable_For_Billing_Party()
    {
        hdn_Is_Service_Tax_Applicable.Value = "0";
        for (int i = 0; i < Session_BillingDetailsGrid.Rows.Count; i++)
        {
            if (Util.String2Bool(Session_BillingDetailsGrid.Rows[i]["Is_Service_Tax_Applicable"].ToString()) == true)
            {
                hdn_Is_Service_Tax_Applicable.Value = "1";
                break;
            }
        }
    }
    private void SetStandardCaption()
    {
        lbl_BillingDetails.Text = CompanyManager.getCompanyParam().GcCaption + " Billing Details";
    }   
    protected void dg_Billing_EditCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Billing.EditItemIndex = e.Item.ItemIndex;
        dg_Billing.ShowFooter = false;
        Bind_BillingGrid();
    }

    protected void dg_Billing_CancelCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dg_Billing.EditItemIndex = -1;
        dg_Billing.ShowFooter = true;
        Bind_BillingGrid();
    }

    protected void dg_Billing_UpdateCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Billing_Details_Dataset(sender, e);
            if (Allow_To_Save == true)
            {
                dg_Billing.EditItemIndex = -1;
                dg_Billing.ShowFooter = true;
                Bind_BillingGrid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Billing Party and Billing Location";
            SM_GCBillingDetails.SetFocus(ddl_BillingParty);
        }
    }

    private void Bind_BillingGrid()
    {
        dg_Billing.DataSource = Session_BillingDetailsGrid;
        dg_Billing.DataBind();
        Is_service_Tax_Applicable_For_Billing_Party();
        if (IsPostBack)
            updateparentdataset();

        TotalRatio = 0;
        Session_TotalRatio = 0;

        if (Session_BillingDetailsGrid.Rows.Count > 0)
        {
            TotalRatio = Util.String2Decimal(Session_BillingDetailsGrid.Compute("sum(bill_Ratio)", "").ToString());
            Session_TotalRatio = TotalRatio;
        }
    }   
    public void dg_Billing_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dr = Session_BillingDetailsGrid.Rows[e.Item.ItemIndex];
        dr.Delete();
        Session_BillingDetailsGrid.AcceptChanges();
        dg_Billing.ShowFooter = true;
        Bind_BillingGrid();
        updateparentdataset();
    }

    protected void dg_Billing_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingParty"));
            WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
            txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
            txt_Ratio = (TextBox)(e.Item.FindControl("txt_Ratio"));
            chk_Is_Service_Tax_Applicable = (CheckBox)(e.Item.FindControl("chk_Is_Service_Tax_Applicable"));

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
            txt_Ratio.Text = dr["Bill_Ratio"].ToString();
            txt_Description.Text = dr["Description"].ToString();
            chk_Is_Service_Tax_Applicable.Checked = Util.String2Bool(dr["Is_Service_Tax_Applicable"].ToString());
        }
    }

    protected void dg_Billing_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                Insert_Update_Billing_Details_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_BillingGrid();
                    dg_Billing.EditItemIndex = -1;
                    dg_Billing.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Billing Party and Billing Location";
                SM_GCBillingDetails.SetFocus(ddl_BillingParty);
            }
        }
    }
    public Boolean Allow_To_Add_Update_Billling_Details()
    {
        Allow_To_Save = false;
        errorMessage = "";

        if (WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {}
        else if (Util.String2Int(ddl_BillingParty.SelectedValue) <= 0)
        {
            errorMessage = " Please Enter Billing Party.";
        }
        else if (Util.String2Decimal(txt_Ratio.Text.Trim()) <= 0)
        {
            errorMessage = "Please Enter Bill Ratio.";
            SM_GCBillingDetails.SetFocus(txt_Ratio);
        }
        else if (Util.String2Decimal(txt_Ratio.Text.Trim()) > 100)
        {
            errorMessage = "Bill Ratio Can't be greater than 100 %";
            SM_GCBillingDetails.SetFocus(txt_Ratio);
        }
        else
        {
            Allow_To_Save = true;
        }
        return Allow_To_Save;
    }

    private void Insert_Update_Billing_Details_Dataset(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BillingParty"));
        WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
        txt_Ratio = (TextBox)(e.Item.FindControl("txt_Ratio"));
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        chk_Is_Service_Tax_Applicable = (CheckBox)(e.Item.FindControl("chk_Is_Service_Tax_Applicable"));

        if (Allow_To_Add_Update_Billling_Details())
        {
            if (e.CommandName == "Add")
                dr = Session_BillingDetailsGrid.NewRow();
            else if (e.CommandName == "Update")
                dr = Session_BillingDetailsGrid.Rows[e.Item.ItemIndex];

            dr["Billing_Hierarchy"] = WucHierarchyWithID1.HierarchyCode;
            dr["Hierarchy_Name"] = WucHierarchyWithID1.GetHierarchyText;

            if (WucHierarchyWithID1.HierarchyCode == "HO")
            {
                dr["Billing_Branch_ID"] = 0;
                dr["Billing_Branch_Name"] = string.Empty;
            }
            else
            {
                dr["Billing_Branch_ID"] = WucHierarchyWithID1.MainId;
                dr["Billing_Branch_Name"] = WucHierarchyWithID1.GetLocationText;
            }

            dr["Billing_Client_ID"] = ddl_BillingParty.SelectedValue;
            dr["Bill_Ratio"] = txt_Ratio.Text.Trim() == string.Empty ? "0" : txt_Ratio.Text.Trim();
            dr["Description"] = txt_Description.Text.Trim();
            dr["Billing_Client_Name"] = ddl_BillingParty.SelectedText.Trim();
            dr["Is_Service_Tax_Applicable"] = chk_Is_Service_Tax_Applicable.Checked;

            if (e.CommandName == "Add")
            {
                Session_BillingDetailsGrid.Rows.Add(dr);
            }
        }
    }
    protected void ddl_BillingParty_Changed(object sender, EventArgs e)
    {
        DataSet dsComm;
        TextBox txt_BillingParty = (TextBox)sender;
        ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)txt_BillingParty.Parent;
        DataGridItem dg_BillingParty = (DataGridItem)ddl_BillingParty.Parent.Parent;

        ddl_BillingParty = (ClassLibrary.UIControl.DDLSearch)(dg_BillingParty.FindControl("ddl_BillingParty"));
        chk_Is_Service_Tax_Applicable = (CheckBox)(dg_BillingParty.FindControl("chk_Is_Service_Tax_Applicable"));

        dsComm = objComm.Get_Values_Where("Ec_master_Client_Vtrans", "Is_Service_Tax_Applicable", "Client_Id =" + ddl_BillingParty.SelectedValue, "Client_Id", true);
        chk_Is_Service_Tax_Applicable.Checked = dsComm.Tables[0].Rows.Count > 0 ? Util.String2Bool(dsComm.Tables[0].Rows[0]["Is_Service_Tax_Applicable"].ToString()) : false;
    }
}
