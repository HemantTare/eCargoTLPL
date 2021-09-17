using System;

using System.Text;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using Raj.EC;
using Raj.EC.ControlsView;
using System.Web.UI.WebControls;

/// <summary>
/// Name : Ankit champaneriya
/// DATE : 21-10-2008
/// Description : Filteration Page
/// </summary>

public partial class Finance_Reports_WucFilteration : System.Web.UI.UserControl, IFilterationView
{
    #region ClassVariables

    //int MenuItemId;
    Boolean IsReceivable;
    string queryStr = "";
    FilterationPresenter objFilterationPresenter;

    #endregion

    #region ControlsValue

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    public int MenuItemCode
    {
        get { return Common.GetMenuItemCode(); }
    }

    public DataSet Session_VoucherType
    {
        set { StateManager.SaveState("VoucherId", value); }
        get { return StateManager.GetState<DataSet>("VoucherId"); }
    }

    public DataSet Voucher_Type
    {
        get
        {
            DataTable _obj_DataTable = new DataTable("Voucher_Type");
            _obj_DataTable.Columns.Add("VoucherType_Id");
            foreach (ListItem _obj_Item in chklst_Voucher_Type.Items)
            {
                DataRow Dr = _obj_DataTable.NewRow();
                if (_obj_Item.Selected == true)
                {
                    Dr["VoucherType_Id"] = _obj_Item.Value;
                    _obj_DataTable.Rows.Add(Dr);
                }
            }
            DataSet ds_VoucherTypeID = new DataSet();
            ds_VoucherTypeID.Tables.Add(_obj_DataTable);

            return ds_VoucherTypeID;
        }
    }

    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        //MenuItemId = Common.GetMenuItemId();
        Session["StartDate"] = null;
        Session["EndDate"] = null;

        //if (MenuItemId == 79 || MenuItemId == 81 || MenuItemId == 92 || MenuItemId == 93 || MenuItemId == 103 || MenuItemId == 104)
        // {
        //     lbl_ledgerGroupName.Visible = true;
        //     ddl_LedgerGroupName.Visible = true;
        //     objFilterationPresenter = new FilterationPresenter(this, IsPostBack);
        // }
        // else
        // {
        //     lbl_ledgerGroupName.Visible = false;
        //     ddl_LedgerGroupName.Visible = false;
        // }

        if (MenuItemCode == 79 || MenuItemCode == 81 || MenuItemCode == 92 || MenuItemCode == 93 || MenuItemCode == 103 || MenuItemCode == 104 || MenuItemCode == 241 || MenuItemCode == 245)
        {
            lbl_ledgerGroupName.Visible = true;
            ddl_LedgerGroupName.Visible = true;
            objFilterationPresenter = new FilterationPresenter(this, IsPostBack);

            if (MenuItemCode == 241 || MenuItemCode == 245)
            {
                ddl_LedgerGroupName.Enabled = false;
                ddl_LedgerGroupName.SelectedValue = "24";
            }
        }
        else
        {
            lbl_ledgerGroupName.Visible = false;
            ddl_LedgerGroupName.Visible = false;
            //lbl_ledgerGroupName.Enabled = false;
            //ddl_LedgerGroupName.Enabled = false;
            //ddl_LedgerGroupName.Attributes.Add("visibility", "false");
            //lbl_ledgerGroupName.Attributes.Add("visibility", "false");
        }

        //TRIAL BALANCE
        if (MenuItemCode == 47)
        {
            lbl_Select.Visible = true;
            rdl_Is_Group_Ledger.Visible = true;
        }
        else if (MenuItemCode == 247)
        {
            lbl_Select_1.Visible = true;  
            rdl_Is_Receipt_Payment.Visible = true;
        }
        else
        {
            lbl_Select.Visible = false;
            lbl_Select_1.Visible = false;  
            rdl_Is_Group_Ledger.Visible = false;
            rdl_Is_Receipt_Payment.Visible = false;
        }


        if (!IsPostBack)
        {
            Dtp_From.SelectedDate = Convert.ToDateTime(UserManager.getUserParam().StartDate);
            //Dtp_To.SelectedDate = Convert.ToDateTime(UserManager.getUserParam().EndDate);
            Dtp_To.SelectedDate = Convert.ToDateTime(System.DateTime.Now.Date);
       
            if (MenuItemCode == 241)
            {
                tableRow.Attributes.Add("style", "display:none");
                tableRow_AsOnDt.Attributes.Add("style", "display:inline");
            }
            else if (MenuItemCode == 245)
            {
                tableRow.Attributes.Add("style", "display:inline");
                tableRow_AsOnDt.Attributes.Add("style", "display:inline");
            }
            else if (MenuItemCode == 246)
            {
                tableRow.Attributes.Add("style", "display:none");
                tableRow_AsOnDt.Attributes.Add("style", "display:inline");
            }
            else if (MenuItemCode == 247)
            {
                tableRow.Attributes.Add("style", "display:inline");
                tableRow_Receipt_Payment.Attributes.Add("style", "display:inline");
                tableRow_AsOnDt.Attributes.Add("style", "display:none");   
            }
            else
            {
                tableRow_AsOnDt.Attributes.Add("style", "display:none");  
            }
             
            if (MenuItemCode == 49)
            {
                fld_VoucherType.Visible = true;
                objFilterationPresenter = new FilterationPresenter(this, IsPostBack);
                objFilterationPresenter.Bind_Voucher();
            }
            else
            {
                fld_VoucherType.Visible = false;
            }

            lbl_Heading.Text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();

            if (Common.IsPopupMenuItem(Common.GetMenuItemCode()))
            {
                PopupOnShowClick();
            }
        }
    }

    protected void btn_Show_Click(object sender, EventArgs e)
    {

        if (validateUI())
        {
            if (MenuItemCode == 49)
            {
                Session_VoucherType = Voucher_Type;
            }

            if (MenuItemCode == 79)
            {
                queryStr = "&IsConsolidated= " + WucHierarchyFiltration1.Is_Consol + "" + "&Hierarchy_Code=" + WucHierarchyFiltration1.HierarchyCode + "" + "&LedgerGroupId= " + ddl_LedgerGroupName.SelectedValue + "" + "&Main_Id=" + WucHierarchyFiltration1.Main_Id + "" + "&StartDate=" + Dtp_From.SelectedDate + "" + "&EndDate=" + Dtp_To.SelectedDate + "&Division_Id=" + WucHierarchyFiltration1.DivisionID + "";
            }
            else if (MenuItemId == 232)
            {
                queryStr = "&IsConsolidated= " + WucHierarchyFiltration1.Is_Consol + "" + "&Hierarchy_Code=" + WucHierarchyFiltration1.HierarchyCode + "" + "&LedgerGroupId=0" + "&Main_Id=" + WucHierarchyFiltration1.Main_Id + "" + "&StartDate=" + Dtp_From.SelectedDate + "" + "&EndDate=" + Dtp_To.SelectedDate + "&IsForCostCentre=1" + "&Division_Id=" + WucHierarchyFiltration1.DivisionID + "";
            }
            //else if (MenuItemId == 47)
            //{
            //    queryStr = "Menu_Item_Id= "+Util.EncryptInteger(MenuItemId)+"&IsConsolidated= " + WucHierarchyFiltration1.Is_Consol + "" + "&Hierarchy_Code=" + WucHierarchyFiltration1.HierarchyCode + "" + "&Main_Id=" + WucHierarchyFiltration1.Main_Id + "" + "&StartDate=" + Util.EncryptString(Dtp_From.SelectedDate.ToString()) + "" + "&EndDate=" + Util.EncryptString(Dtp_To.SelectedDate.ToString()) + "&Division_Id=" + WucHierarchyFiltration1.DivisionID + "";
            //}
            else
            {
                queryStr = "&IsConsolidated= " + WucHierarchyFiltration1.Is_Consol + "" + "&Hierarchy_Code=" + WucHierarchyFiltration1.HierarchyCode + "" + "&Main_Id=" + WucHierarchyFiltration1.Main_Id + "" + "&StartDate=" + Util.EncryptString(Dtp_From.SelectedDate.ToString()) + "" + "&EndDate=" + Util.EncryptString(Dtp_To.SelectedDate.ToString()) + "&Division_Id=" + WucHierarchyFiltration1.DivisionID + "";
            }
            //string viewUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + queryStr;
            //Response.Redirect(viewUrl);
            
            string viewUrl = Util.GetBaseURL();
            viewUrl = viewUrl + "/";

            if (MenuItemCode == 47)
            {
                if (Util.String2Int(rdl_Is_Group_Ledger.SelectedValue) == 1)
                {
                    viewUrl = viewUrl + "Finance/Reports/FrmTrialBalance.aspx?Menu_Item_Id=" + Util.EncryptInteger(MenuItemId) + "&Mode=NAA=" + queryStr;
                    Response.Redirect(viewUrl);
                }
                else
                {
                    viewUrl = viewUrl + "Finance/Reports/FrmTrialBalanceLedger.aspx?Menu_Item_Id=" + Util.EncryptInteger(MenuItemId) + "&Mode=NAA=" + queryStr;
                    Response.Redirect(viewUrl);
                }
            }
            else
            {
                viewUrl = viewUrl + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + queryStr;
                Response.Redirect(viewUrl);
            }
        }
        else
        {
            return;
        }
    }

    private string Replace(string viewUrl)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #endregion

    #region ExtraFunction

    private void PopupOnShowClick()
    {
        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");
        Path.Append(Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl);
        btn_Show.Attributes.Add("onclick", "return Open_Show_Window('" + Path + "')");
    }

    #endregion

    #region ControlsValues

    #endregion

    #region ControlsBind
    public DataSet BindLedgerGroupName
    {
        set
        {
            ddl_LedgerGroupName.DataSource = value;
            ddl_LedgerGroupName.DataValueField = "Ledger_Group_Id";
            ddl_LedgerGroupName.DataTextField = "Ledger_Group_Name";
            ddl_LedgerGroupName.DataBind();

            if (MenuItemCode == 79 || MenuItemCode == 81 || MenuItemCode == 92 || MenuItemCode == 93)
            {
                ddl_LedgerGroupName.Items.Insert(0, new ListItem("All Ledger Groups", "0"));
            }
            
        }
    }


    public DataSet BindVoucherType
    {
        set
        {
            chklst_Voucher_Type.DataSource = value;
            chklst_Voucher_Type.DataTextField = "Voucher_Name";
            chklst_Voucher_Type.DataValueField = "Voucher_Type_Id";
            chklst_Voucher_Type.DataBind();
        }
    }

    #endregion

    #region IView

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
            return 1;
            //return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public bool validateUI()
    {
        bool _isvalid = true;

        if (WucHierarchyFiltration1.validateHierarchyFilteration(lbl_Errors) == false)
        {
            _isvalid = false;
        }

        if (Dtp_From.SelectedDate > Dtp_To.SelectedDate)
        {
            errorMessage = "Please Select (From Date) Less Than (To Date)";
            _isvalid = false;
        }

        return _isvalid;
    }
    #endregion

}
