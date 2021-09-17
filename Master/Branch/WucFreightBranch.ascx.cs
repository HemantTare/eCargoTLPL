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
using Raj.EC;
using ClassLibraryMVP.Security;
/// <summary>
/// Author        : Aashish Lad
/// Created On    : 11th October 2008
/// Description   : This is the Page For Master Freight Branch
/// </summary>
/// 
public partial class Master_Branch_WucFreightBranch : System.Web.UI.UserControl, IFreightBranchView
{
    #region ClassVariables
    FreightBranchPresenter objFreightBranchPresenter;
    LinkButton lnk_btn_view;
    Label lbl_ToBranchId, lbl_FreightId;
    TextBox txt_FreightRate;
    string queryString;

    bool can_view, can_add, can_edit, can_cancel;
    bool can_view1, can_add1, can_edit1, can_cancel1;
    int col_add_edit = 5;

    #endregion

    #region ControlsValue


    public int ToBranchID
    {
        set
        {
            lbl_ToBranchId.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_ToBranchId.Text);
        }

    }
    public int FreightID
    {
        set
        {
            lbl_FreightId.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_FreightId.Text);
        }

    }

    public decimal FreightRate
    {
        set
        {
            txt_FreightRate.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_FreightRate.Text != "")
                return Util.String2Decimal(txt_FreightRate.Text);
            else
                return 0;
        }

    }


    public int BranchID
    {
        set
        {
            ddl_Branch.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Branch.SelectedValue);
        }

    }
    public int AreaID
    {
        set
        {
            ddl_Area.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Area.SelectedValue);
        }

    }

    public int CommodityID
    {
        set
        {
            ddl_Commodity.SelectedValue = Util.Int2String(value); ;
        }
        get
        {
            return Util.String2Int(ddl_Commodity.SelectedValue);

        }

    }

    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_Branch
    {
        set
        {
            ddl_Branch.DataSource = value;
            ddl_Branch.DataTextField = "Branch_Name";
            ddl_Branch.DataValueField = "Branch_ID";
            ddl_Branch.DataBind();
            Common.InsertItem(ddl_Branch);
        }
    }
    public DataTable Bind_ddl_Area
    {
        set
        {
            ddl_Area.DataSource = value;
            ddl_Area.DataTextField = "Area_Name";
            ddl_Area.DataValueField = "Area_Id";
            ddl_Area.DataBind();
            Common.InsertItem(ddl_Area);
        }
    }

    public DataTable Bind_ddl_Commodity
    {
        set
        {
            ddl_Commodity.DataTextField = "Commodity_Name";
            ddl_Commodity.DataValueField = "Commodity_ID";
            ddl_Commodity.DataSource = value;
            ddl_Commodity.DataBind();
            Common.InsertItem(ddl_Commodity);
        }
    }

    public DataTable Bind_dg_FreightBranch
    {
        set
        {
            SessionFreightBranchGrid = value;
            dg_FreightBranch.DataSource = value;
            dg_FreightBranch.DataBind();
        }
    }
    public DataTable SessionFreightBranchGrid
    {
        get { return StateManager.GetState<DataTable>("FreightBranch"); }
        set { StateManager.SaveState("FreightBranch", value); }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = true;
        //if (txt_Tax_Form.Text.Trim() == string.Empty)
        //{
        //    lbl_Errors.Text = GetLocalResourceObject("MsgTaxForm").ToString();
        //    _isValid = false;
        //}
        //else
        //{
        //    _isValid = true;
        //}

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
            //return Util.DecryptToInt(Request.QueryString["Id"]);
            return -1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods
    private void DisabledControl()
    {
        ddl_Branch.Enabled = false;
        ddl_Area.Enabled = false;
        ddl_Commodity.Enabled = false;
    }
    private void EnabledControl()
    {
        ddl_Branch.Enabled = true;
        ddl_Area.Enabled = true;
        ddl_Commodity.Enabled = true;
    }
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            int MenuItemId = Common.GetMenuItemId();
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);
        }

        queryString = StateManager.GetState<string>("QueryString");
        hdnQueryString.Value = queryString;

        objFreightBranchPresenter = new FreightBranchPresenter(this, IsPostBack);

        if (queryString == "1")
        {
            lbl_Commodity.Visible = false;
            ddl_Commodity.Visible = false;
        }

        Common objcommon = new Common();
        objcommon.CheckRights(ref can_view, ref can_add, ref can_edit, ref can_cancel);
        objcommon.ForceRights(ref can_view1, ref can_add1, ref can_edit1, ref can_cancel1);
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Area.SelectedValue) > 0 && Util.String2Int(ddl_Branch.SelectedValue) > 0
            && ((Util.String2Int(ddl_Commodity.SelectedValue) > 0) || queryString == "1"))
        {
            dg_FreightBranch.CurrentPageIndex = 0;
            objFreightBranchPresenter.FillGrid();
            dg_FreightBranch.Visible = true;
        }
        else
            dg_FreightBranch.Visible = false;
    }
    protected void ddl_Area_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Area.SelectedValue) > 0 && Util.String2Int(ddl_Branch.SelectedValue) > 0
            && ((Util.String2Int(ddl_Commodity.SelectedValue) > 0) || queryString == "1"))
        {
            dg_FreightBranch.CurrentPageIndex = 0;
            objFreightBranchPresenter.FillGrid();
            dg_FreightBranch.Visible = true;
        }
        else
            dg_FreightBranch.Visible = false;
    }

    protected void ddl_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Area.SelectedValue) > 0 && Util.String2Int(ddl_Branch.SelectedValue) > 0
            && ((Util.String2Int(ddl_Commodity.SelectedValue) > 0) || queryString == "1"))
        {
            dg_FreightBranch.CurrentPageIndex = 0;
            objFreightBranchPresenter.FillGrid();
            dg_FreightBranch.Visible = true;
        }
        else
            dg_FreightBranch.Visible = false;
    }

    protected void dg_FreightBranch_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        EnabledControl();
        dg_FreightBranch.EditItemIndex = -1;
        Bind_dg_FreightBranch = SessionFreightBranchGrid;
    }
    protected void dg_FreightBranch_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DisabledControl();
        dg_FreightBranch.EditItemIndex = e.Item.ItemIndex;
        Bind_dg_FreightBranch = SessionFreightBranchGrid;
    }
    protected void dg_FreightBranch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_FreightBranch.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_FreightBranch = SessionFreightBranchGrid;
    }
    protected void dg_FreightBranch_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txt_FreightRate = (TextBox)e.Item.FindControl("txt_FreightRate");
        lbl_ToBranchId = (Label)e.Item.FindControl("lbl_ToBranchId");
        lbl_FreightId = (Label)e.Item.FindControl("lbl_FreightId");

        EnabledControl();
        if (e.CommandName == "Update")
        {
            objFreightBranchPresenter.Save();
            dg_FreightBranch.EditItemIndex = -1;
            objFreightBranchPresenter.FillGrid();
        }
    }
    protected void dg_FreightBranch_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            lnk_btn_view = (LinkButton)e.Item.FindControl("lnk_btn_view");
            if (Util.String2Decimal(DataBinder.Eval(e.Item.DataItem, "FrieghtRate").ToString()) <= 0)
            {

                e.Item.CssClass = "NOTUPDATEDLBL";
                lnk_btn_view.Visible = false;

                e.Item.Cells[col_add_edit].Enabled = can_add;

                if (can_add1 == false)
                    e.Item.Cells[col_add_edit].Enabled = false;
            }
            else
            {
                e.Item.CssClass = "UPDATEDLBL";

                e.Item.Cells[col_add_edit].Enabled = can_edit;

                if (can_edit1 == false)
                    e.Item.Cells[col_add_edit].Enabled = false;
            }
        }
    }
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        //Response.Redirect("FrmFreightBranchCopy.aspx");
    }
    #endregion

}
