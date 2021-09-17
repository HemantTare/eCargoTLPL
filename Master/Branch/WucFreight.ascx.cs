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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 08th October 2008
/// Description   : This is the Page For Master Freight
/// </summary>
/// 


public partial class Master_Branch_WucFreight : System.Web.UI.UserControl,IFreightView 
{
    #region ClassVariables
    FreightPresenter objFreightPresenter;
    LinkButton lnk_btn_view;
    Label lbl_ToCityId, lbl_FreightId;
    TextBox txt_Distance, txt_SpecialRate, txt_NormalRate, txt_FTLRate;
    
    bool can_view, can_add, can_edit, can_cancel;
    bool can_view1, can_add1, can_edit1, can_cancel1;
    int col_add_edit =8;
    int col_View = 9;

    #endregion

    #region ControlsValue


    public int ToCityID
    {
        set
        {
            lbl_ToCityId.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_ToCityId.Text);
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
    

    public decimal FTLRate
    {
        set
        {
            txt_FTLRate.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_FTLRate.Text);
        }

    }
    public decimal NormalRate
    {
        set
        {
            txt_NormalRate.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_NormalRate.Text != "")
                return Util.String2Decimal(txt_NormalRate.Text);
            else
                return 0;
        }

    }
    public decimal SpecialRate
    {
        set
        {
            txt_SpecialRate.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_SpecialRate.Text);
        }

    }

    public decimal DistanceInKM
    {
        set
        {
            txt_Distance.Text = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(txt_Distance.Text);
        }

    }

    public int CityID
    {
        set
        {
            ddl_City.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_City.SelectedValue);
        }

    }
    public int StateID
    {
        set
        {
            ddl_State.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_State.SelectedValue);
        }

    }
    
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_City
    {
        set
        {
            ddl_City.DataSource = value;
            ddl_City.DataTextField = "City_Name";
            ddl_City.DataValueField = "City_ID";
            ddl_City.DataBind();
            ddl_City.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_State
    {
        set
        {
            ddl_State.DataSource = value;
            ddl_State.DataTextField = "State_Name";
            ddl_State.DataValueField = "State_Id";
            ddl_State.DataBind();
            ddl_State.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_dg_Freight
    {
        set
        {
            SessionFreightGrid = value;
            dg_Freight.DataSource = value;
            dg_Freight.DataBind();
        }
    }
    public DataTable SessionFreightGrid
    {
        get { return StateManager.GetState<DataTable>("Freight"); }
        set { StateManager.SaveState("Freight", value); }
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
        ddl_City.Enabled = false;
        ddl_State.Enabled = false;       
    }
    private void EnabledControl()
    {
        ddl_City.Enabled = true;
        ddl_State.Enabled = true;        
    }
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objFreightPresenter = new FreightPresenter(this, IsPostBack);

        Common objcommon = new Common();
        objcommon.CheckRights(ref can_view, ref can_add, ref can_edit, ref can_cancel);
        objcommon.ForceRights(ref can_view1, ref can_add1, ref can_edit1, ref can_cancel1);

    }
    protected void ddl_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_State.SelectedValue) > 0 && Util.String2Int(ddl_City.SelectedValue) > 0)
        {
            dg_Freight.CurrentPageIndex = 0;
            objFreightPresenter.FillGrid();
            dg_Freight.Visible = true ;
        }
        else
            dg_Freight.Visible = false;
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_State.SelectedValue) > 0 && Util.String2Int(ddl_City.SelectedValue) > 0)
        {
            dg_Freight.CurrentPageIndex = 0;
            objFreightPresenter.FillGrid();
            dg_Freight.Visible = true;
        }
        else
            dg_Freight.Visible = false;
    }
    
    protected void dg_Freight_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        EnabledControl();
        dg_Freight.EditItemIndex = -1;
        Bind_dg_Freight = SessionFreightGrid;
    }
    protected void dg_Freight_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DisabledControl();
        dg_Freight.EditItemIndex = e.Item.ItemIndex;
        Bind_dg_Freight = SessionFreightGrid;
    }

    protected void dg_Freight_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Freight.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_Freight = SessionFreightGrid;
    }
    protected void dg_Freight_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txt_FTLRate = (TextBox)e.Item.FindControl("txt_FTLRate");
        txt_Distance = (TextBox)e.Item.FindControl("txt_Distance");
        txt_NormalRate = (TextBox)e.Item.FindControl("txt_NormalRate");
        txt_SpecialRate = (TextBox)e.Item.FindControl("txt_SpecialRate");
        lbl_ToCityId = (Label)e.Item.FindControl("lbl_ToCityId");
        lbl_FreightId = (Label)e.Item.FindControl("lbl_FreightId");
        EnabledControl();
        if (e.CommandName == "Update")
        {
            objFreightPresenter.Save();
            dg_Freight.EditItemIndex = -1;            
            objFreightPresenter.FillGrid();
        }
    }
    protected void dg_Freight_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            lnk_btn_view = (LinkButton)e.Item.FindControl("lnk_btn_view");
            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "FTLRate")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Distance_In_Km")) == 0
                && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "NormalRate")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SpecialRate")) == 0)
            {

                e.Item.CssClass = "NOTUPDATEDLBL";
                lnk_btn_view.Visible = false;

                //Ankit
                e.Item.Cells[col_add_edit].Enabled = can_add;

                if (can_add1 == false)
                    e.Item.Cells[col_add_edit].Enabled = false;

            }
            else
            {
                e.Item.CssClass = "UPDATEDLBL";

                //Ankit
                e.Item.Cells[col_add_edit].Enabled = can_edit;

                if (can_edit1 == false)
                    e.Item.Cells[col_add_edit].Enabled = false;
            }
        }
    }
   
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        //Response.Redirect("FrmFreightCopy.aspx");

        //int MenuItemId = Raj.EC.Common.GetMenuItemId();
        //string Mode = System.Web.HttpContext.Current.Request.QueryString["Mode"];
        //System.Web.HttpContext.Current.Response.Redirect("FrmFreightCopy.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode);
    }
    #endregion
}
