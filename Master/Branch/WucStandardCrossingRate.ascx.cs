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
/// Created On    : 10th October 2008
/// Description   : This is the Page For Master Standard Crossing Rate
/// </summary>
/// 
public partial class Master_Branch_WucStandardCrossingRate : System.Web.UI.UserControl,IStandardCrossingRateView 
{
    #region ClassVariables
    StandardCrossingRatePresenter objStandardCrossingRatePresenter;
    LinkButton lnk_btn_view;
    Label lbl_ToBranchId, lbl_FreightId;
    TextBox txt_Distance, txt_HireRate, txt_Total, txt_Hamali;

    //pankaj
    bool can_view, can_add, can_edit, can_cancel;
    bool can_view1, can_add1, can_edit1, can_cancel1;
    int col_add_edit = 8;
    int col_View = 9;
    //pankaj

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


    public decimal HireRate
    {
        set
        {
            txt_HireRate.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_HireRate.Text != "")
            {
                return Util.String2Decimal(txt_HireRate.Text);
            }
            else
                return 0;
        }

    }
    public decimal Hamali
    {
        set
        {
            txt_Hamali.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_Hamali.Text != "")
                return Util.String2Decimal(txt_Hamali.Text);
            else
                return 0;
        }

    }
    public decimal Total
    {
        set
        {
            txt_Total.Text = Util.Decimal2String(value);
        }
        get
        {
            if (txt_Total.Text != "")
                return Util.String2Decimal(txt_Total.Text);
            else
                return 0;
        }

    }

    public int DistanceInKM
    {
        set
        {
            txt_Distance.Text = Util.Int2String(value);
        }
        get
        {
            if (txt_Distance.Text != "")
                return Util.String2Int(txt_Distance.Text);
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
            ddl_Branch.Items.Insert(0, new ListItem("Select One", "0"));
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
            ddl_Area.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_dg_StandardCrossingRate
    {
        set
        {
            SessionStandardCrossingRateGrid = value;
            dg_StandardCrossingRate.DataSource = value;
            dg_StandardCrossingRate.DataBind();
        }
    }
    public DataTable SessionStandardCrossingRateGrid
    {
        get { return StateManager.GetState<DataTable>("CrossingRate"); }
        set { StateManager.SaveState("CrossingRate", value); }
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
    }
    private void EnabledControl()
    {
        ddl_Branch.Enabled = true;
        ddl_Area.Enabled = true;
    }
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        objStandardCrossingRatePresenter = new StandardCrossingRatePresenter(this, IsPostBack);

        Common objcommon = new Common();

        dg_StandardCrossingRate.Columns[col_View].Visible = false;
        //pankaj
        objcommon.CheckRights(ref can_view, ref can_add, ref can_edit, ref can_cancel);
        objcommon.ForceRights(ref can_view1, ref can_add1, ref can_edit1, ref can_cancel1);
        //pankaj
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Area.SelectedValue) > 0 && Util.String2Int(ddl_Branch.SelectedValue) > 0)
        {
            dg_StandardCrossingRate.CurrentPageIndex = 0;
            objStandardCrossingRatePresenter.FillGrid();
            dg_StandardCrossingRate.Visible = true;
        }
        else
            dg_StandardCrossingRate.Visible = false;
    }

    protected void ddl_Area_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_Area.SelectedValue) > 0 && Util.String2Int(ddl_Branch.SelectedValue) > 0)
        {
            dg_StandardCrossingRate.CurrentPageIndex = 0;
            objStandardCrossingRatePresenter.FillGrid();
            dg_StandardCrossingRate.Visible = true;
        }
        else
            dg_StandardCrossingRate.Visible = false;
    }
   

    protected void dg_StandardCrossingRate_CancelCommand(object source, DataGridCommandEventArgs e)
    {

        EnabledControl();
        dg_StandardCrossingRate.EditItemIndex = -1;
        Bind_dg_StandardCrossingRate = SessionStandardCrossingRateGrid;
    }

    protected void dg_StandardCrossingRate_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DisabledControl();
        dg_StandardCrossingRate.EditItemIndex = e.Item.ItemIndex;
        Bind_dg_StandardCrossingRate = SessionStandardCrossingRateGrid;
    }
    protected void dg_StandardCrossingRate_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_StandardCrossingRate.CurrentPageIndex = e.NewPageIndex;
        Bind_dg_StandardCrossingRate = SessionStandardCrossingRateGrid;
    }
    protected void dg_StandardCrossingRate_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txt_HireRate = (TextBox)e.Item.FindControl("txt_HireRate");
        txt_Hamali = (TextBox)e.Item.FindControl("txt_Hamali");
        txt_Total = (TextBox)e.Item.FindControl("txt_Total");
        txt_Distance = (TextBox)e.Item.FindControl("txt_Distance");
        lbl_ToBranchId = (Label)e.Item.FindControl("lbl_ToBranchId");
        lbl_FreightId = (Label)e.Item.FindControl("lbl_FreightId");
        EnabledControl();
        if (e.CommandName == "Update")
        {
            objStandardCrossingRatePresenter.Save();
            dg_StandardCrossingRate.EditItemIndex = -1;
            objStandardCrossingRatePresenter.FillGrid();
        }
    }
    protected void dg_StandardCrossingRate_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
      
        if (e.Item.ItemIndex != -1)
        {
            lnk_btn_view = (LinkButton)e.Item.FindControl("lnk_btn_view");

            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "HireRate")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Distance_In_Km")) == 0
                && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Hamali")) == 0 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Total")) == 0)
            {
                e.Item.CssClass = "NOTUPDATEDLBL";

                //pankaj
                e.Item.Cells[col_add_edit].Enabled = can_add;

                if (can_add1==false)
                    e.Item.Cells[col_add_edit].Enabled = false;
                //pankaj
            }
            else
            {
                e.Item.CssClass = "UPDATEDLBL";
                //pankaj
                e.Item.Cells[col_add_edit].Enabled = can_edit;

                if (can_edit1 == false)
                    e.Item.Cells[col_add_edit].Enabled = false;
                //pankaj
            }
        }

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            TextBox txt_HireRate = (TextBox)(e.Item.FindControl("txt_HireRate"));
            TextBox txt_Hamali = (TextBox)(e.Item.FindControl("txt_Hamali"));
            TextBox txt_Total = (TextBox)(e.Item.FindControl("txt_Total"));

            txt_HireRate.Attributes.Add("onblur", "return calculate_rate('" + txt_HireRate.ClientID + "','" + txt_Hamali.ClientID + "','" + txt_Total.ClientID + "')");
            txt_Hamali.Attributes.Add("onblur", "return calculate_rate('" + txt_HireRate.ClientID + "','" + txt_Hamali.ClientID + "','" + txt_Total.ClientID + "')");
            txt_Total.Attributes.Add("onblur", "return calculate_rate('" + txt_HireRate.ClientID + "','" + txt_Hamali.ClientID + "','" + txt_Total.ClientID + "')");
        
        }
    }
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        //Response.Redirect("FrmStandardCrossingRateCopy.aspx");
    }
    #endregion
}
