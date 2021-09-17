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
/// Created On    : 06th October 2008
/// Description   : This is the Page For Master Transit Days 
/// </summary>
/// 


public partial class Master_Branch_WucTransitDays : System.Web.UI.UserControl,ITransitDaysView 
{
    #region ClassVariables
    TransitDaysPresenter objTransitDaysPresenter;
    LinkButton lnk_btn_view;
    Label lbl_ToCityId, lbl_transit_day, lbl_distance, lbl_TransitDaysId;
    TextBox txt_transit_day, txt_distance;
    
    bool can_view, can_add, can_edit, can_cancel;
    bool can_view1, can_add1, can_edit1, can_cancel1;
    int col_add_edit = 6;
    int col_View = 7;


    #endregion

    #region ControlsValue

    public int TransitDaysID
    {
        set
        {
            lbl_TransitDaysId.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_TransitDaysId.Text);
        }

    }

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

    public int TransitDays
    {
        set
        {
            txt_transit_day.Text = Util.Int2String(value);
        }
        get
        {
            if (txt_transit_day.Text != "")
                return Util.String2Int(txt_transit_day.Text);
            else
                return 0;
        }

    }

    public int DistanceInKM
    {
        set
        {
            txt_distance.Text = Util.Int2String(value);
        }
        get
        {
            if (txt_distance.Text != "")
                return Util.String2Int(txt_distance.Text);
            else
                return 0;
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
    public int VehicleID
    {
        set
        {
            ddl_VehicleType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_VehicleType.SelectedValue);
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
    public DataTable Bind_ddl_Vehicle
    {
        set
        {
            ddl_VehicleType.DataSource = value;
            ddl_VehicleType.DataTextField = "Vehicle_Type";
            ddl_VehicleType.DataValueField = "Vehicle_Type_Id";
            ddl_VehicleType.DataBind();
            ddl_VehicleType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_dg_TransitDays
    {
        set
        {
            SessionTransitDaysGrid = value;          
            dg_TransitDays.DataSource = value;           
            dg_TransitDays.DataBind();
        }
    }
    public DataTable SessionTransitDaysGrid
    {
        get { return StateManager.GetState<DataTable>("TransitDays"); }
        set { StateManager.SaveState("TransitDays", value); }
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
        ddl_VehicleType.Enabled = false;
    }
    private void EnabledControl()
    {
        ddl_City.Enabled = true;
        ddl_State.Enabled = true;
        ddl_VehicleType.Enabled = true;
    }
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objTransitDaysPresenter = new TransitDaysPresenter(this, IsPostBack);

        Common objcommon = new Common();

        objcommon.CheckRights(ref can_view, ref can_add, ref can_edit, ref can_cancel);
        objcommon.ForceRights(ref can_view1, ref can_add1, ref can_edit1, ref can_cancel1);
    }
    
    protected void ddl_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_State.SelectedValue) > 0 && Util.String2Int(ddl_City.SelectedValue) > 0 && Util.String2Int(ddl_VehicleType.SelectedValue) > 0)
        {
            dg_TransitDays.CurrentPageIndex = 0;
            objTransitDaysPresenter.FillGrid();
            dg_TransitDays.Visible = true;
        }
        else
            dg_TransitDays.Visible = false;
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_State.SelectedValue) > 0 && Util.String2Int(ddl_City.SelectedValue) > 0 && Util.String2Int(ddl_VehicleType.SelectedValue) > 0)
        {
            dg_TransitDays.CurrentPageIndex = 0;
            objTransitDaysPresenter.FillGrid();
            dg_TransitDays.Visible = true;
        }
        else
            dg_TransitDays.Visible = false;
    }

    protected void ddl_VehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_State.SelectedValue) > 0 && Util.String2Int(ddl_City.SelectedValue) > 0 && Util.String2Int(ddl_VehicleType.SelectedValue) > 0)
        {
            dg_TransitDays.CurrentPageIndex = 0;
            objTransitDaysPresenter.FillGrid();
            dg_TransitDays.Visible = true;
        }
        else
            dg_TransitDays.Visible = false;

        
    }
   
    protected void dg_TransitDays_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_TransitDays.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_TransitDays = SessionTransitDaysGrid;
    }
    protected void dg_TransitDays_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DisabledControl();
        dg_TransitDays.EditItemIndex = e.Item.ItemIndex;
        Bind_dg_TransitDays = SessionTransitDaysGrid;
    }
    protected void dg_TransitDays_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        EnabledControl();
        dg_TransitDays.EditItemIndex = -1;
        Bind_dg_TransitDays = SessionTransitDaysGrid;
    }
    protected void dg_TransitDays_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            lnk_btn_view = (LinkButton)e.Item.FindControl("lnk_btn_view");
            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Transit_Days")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Distance_In_Km")) == 0)
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

    protected void dg_TransitDays_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txt_transit_day=(TextBox)e.Item.FindControl("txt_transit_day");
        txt_distance=(TextBox)e.Item.FindControl("txt_distance");
        lbl_ToCityId = (Label)e.Item.FindControl("lbl_ToCityId");          
        lbl_transit_day = (Label)e.Item.FindControl("lbl_transit_day");          
        lbl_distance = (Label)e.Item.FindControl("lbl_distance");
        lbl_TransitDaysId = (Label)e.Item.FindControl("lbl_TransitDaysId");
        EnabledControl();
        if (e.CommandName == "Update")
        {         
            objTransitDaysPresenter.Save();
            dg_TransitDays.EditItemIndex = -1;
            objTransitDaysPresenter.FillGrid();
        }
    }
   
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        //Response.Redirect("FrmTransitDaysStateToState.aspx");
    }
    #endregion
}
