using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC.ControlsView;
/// <summary>
/// Name : Ankit champaneriya
/// Date : 25-10-08
/// Description : Excess Details tab
/// </summary>
/// 
public partial class Operations_Inward_WucAUSExcessDetails : System.Web.UI.UserControl, IAUSExcessDetailsView
{
    #region ClassVariables



    public event EventHandler TotalExcessArticlesChange;

    AUSExcessDetailsPresenter objAUSExcessDetailsPresenter;

    DropDownList ddl_PackingType;
    DropDownList ddl_Commodity;
    DropDownList ddl_Item;

    TextBox txt_GCNo;
    TextBox txt_ExcessArticles;
    TextBox txt_Marking;
    TextBox txt_Remarks;

    DataTable objDT;
    DataRow DR = null;
    bool isValid = false;
    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set
        { //scm_ExcessDetails = value; 
        }
    }

    public void CalculateTotal(DataTable dt)
    {
        int i = dt.Rows.Count;
        int Total = 0;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            Total = Total + Util.String2Int(dt.Rows[i]["Excess_Articles"].ToString());
        }
        TotalExcessArticles = Total;
    }

    private void SetStandardCaption()
    {
        const int GCCaption = 0;
        dg_ExcessDetails.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";

    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        ddl_PackingType = (DropDownList)(e.Item.FindControl("ddl_PackingType"));
        ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
        ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
        txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
        txt_Marking = (TextBox)(e.Item.FindControl("txt_Marking"));
        txt_ExcessArticles = (TextBox)(e.Item.FindControl("txt_ExcessArticles"));
        txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

        objDT = SessionBindExcessGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Packing_Type_ID"] = ddl_PackingType.SelectedValue;
            DR["Packing_Type"] = ddl_PackingType.SelectedItem;
            DR["Commodity_ID"] = ddl_Commodity.SelectedValue;
            DR["Commodity_Name"] = ddl_Commodity.SelectedItem;
            DR["Item_ID"] = ddl_Item.SelectedValue;
            DR["Item_Name"] = ddl_Item.SelectedItem;
            DR["GC_No"] = txt_GCNo.Text;
            DR["Marking_On_Package"] = txt_Marking.Text;
            DR["Excess_Articles"] = txt_ExcessArticles.Text;
            DR["Remarks"] = txt_Remarks.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionBindExcessGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";
        if (txt_ExcessArticles.Text.Trim() == string.Empty || Util.String2Int(txt_ExcessArticles.Text) <= 0)
        {
            errorMessage = "Please Enter ExcessArticle";// GetLocalResourceObject("Msg_txt_ExcessArticles").ToString();
            //scm_ExcessDetails.SetFocus(txt_ExcessArticles);
        }
        else if (txt_Marking.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Marking Details";// GetLocalResourceObject("Msg_txt_Marking").ToString();
            //scm_ExcessDetails.SetFocus(txt_Marking);
        }

        else if (Util.String2Int(ddl_PackingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Packing Type";// GetLocalResourceObject("Msg_ddl_PackingType").ToString();
            //scm_ExcessDetails.SetFocus(ddl_PackingType);
        }
        else if (Util.String2Int(ddl_Commodity.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Commodity";// GetLocalResourceObject("Msg_ddl_Commodity").ToString();
            //scm_ExcessDetails.SetFocus(ddl_Commodity);
        }
        else if (Util.String2Int(ddl_Item.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Item";// GetLocalResourceObject("Msg_ddl_Item").ToString();
            //scm_ExcessDetails.SetFocus(ddl_Item);
        }

        else if (txt_Remarks.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Remarks";// GetLocalResourceObject("Msg_txt_Remarks").ToString();
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    #endregion

    #region InitInterface




    public int TotalExcessArticles
    {
        set
        {
            lbl_TotalExcessArticlesValue.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_TotalExcessArticlesValue.Text);
        }
    }
    public int CommodityId
    {
        set
        {
            ddl_Commodity.SelectedValue = Util.Int2String(value);


        }
        get
        {
            return Util.String2Int(ddl_Commodity.SelectedValue);
        }
    }

    public String Excess_Details_Xml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindExcessGrid.Copy());
            _objDs.Tables[0].TableName = "excess_details";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region ControlsBind

    public DataTable BindPackingType
    {
        set
        {
            ddl_PackingType.DataTextField = "Packing_Type";
            ddl_PackingType.DataValueField = "Packing_Id";
            ddl_PackingType.DataSource = value;
            ddl_PackingType.DataBind();
            ddl_PackingType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindCommodity
    {
        set
        {
            ddl_Commodity.DataTextField = "Commodity_Name";
            ddl_Commodity.DataValueField = "Commodity_Id";
            ddl_Commodity.DataSource = value;
            ddl_Commodity.DataBind();
            ddl_Commodity.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindItem
    {
        set
        {

            ddl_Item.DataTextField = "Item_Name";
            ddl_Item.DataValueField = "Item_Id";
            ddl_Item.DataSource = value;
            ddl_Item.DataBind();
            ddl_Item.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindExcessDetailsGrid
    {
        set
        {
            dg_ExcessDetails.DataSource = value;
            dg_ExcessDetails.DataBind();
            //SetStandardCaption();
            CalculateTotal(value);


            EventArgs e = null;

            if (Raj.EC.Common.GetMenuItemId() != 112)
                TotalExcessArticlesChange((object)TotalExcessArticles, e);
        }
    }

    public DataTable SessionBindPackingTypeDropdown
    {
        get { return StateManager.GetState<DataTable>("BindPackageTypeDropdown"); }
        set { StateManager.SaveState("BindPackageTypeDropdown", value); }
    }

    public DataTable SessionBindCommodityDropdown
    {
        get { return StateManager.GetState<DataTable>("BindCommodityDropdown"); }
        set { StateManager.SaveState("BindCommodityDropdown", value); }
    }

    public DataTable SessionBindItemDropdown
    {
        get { return StateManager.GetState<DataTable>("BindItemDropdown"); }
        set { StateManager.SaveState("BindItemDropdown", value); }
    }

    public DataTable SessionBindExcessGrid
    {
        get { return StateManager.GetState<DataTable>("BindExcessDetailsGrid"); }
        set { StateManager.SaveState("BindExcessDetailsGrid", value); }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (SessionBindExcessGrid.Rows.Count > 0 && TotalExcessArticles <= 0)
        {
            errorMessage = " Excess GC Articles Must Be Greater Than Zero. ";
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
        get
        {
            // return 1;
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();

        objAUSExcessDetailsPresenter = new AUSExcessDetailsPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            BindExcessDetailsGrid = SessionBindExcessGrid;
            //SetStandardCaption();
        }
        //upd_pnl_dg_AUSExcessDetails.Update();
    }

    #endregion

    #region GRID EVENT
    protected void dg_ExcessDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_PackingType = (DropDownList)(e.Item.FindControl("ddl_PackingType"));
                ddl_Commodity = (DropDownList)(e.Item.FindControl("ddl_Commodity"));
                ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
                txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
                txt_Marking = (TextBox)(e.Item.FindControl("txt_Marking"));
                txt_ExcessArticles = (TextBox)(e.Item.FindControl("txt_ExcessArticles"));
                txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindPackingType = SessionBindPackingTypeDropdown;
                BindCommodity = SessionBindCommodityDropdown;
                BindItem = SessionBindItemDropdown;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionBindExcessGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                ddl_PackingType.SelectedValue = DR["Packing_Type_ID"].ToString();
                ddl_Commodity.SelectedValue = DR["Commodity_ID"].ToString();
                objAUSExcessDetailsPresenter.FillItemOnCommodityChanged();
                BindItem = SessionBindItemDropdown;
                ddl_Item.SelectedValue = DR["Item_ID"].ToString();
                txt_GCNo.Text = DR["GC_No"].ToString();
                txt_Marking.Text = DR["Marking_On_Package"].ToString();
                txt_ExcessArticles.Text = DR["Excess_Articles"].ToString();
                txt_Remarks.Text = DR["Remarks"].ToString();
            }
        }
    }

    protected void dg_ExcessDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionBindExcessGrid;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindExcessDetailsGrid = SessionBindExcessGrid;
                    dg_ExcessDetails.EditItemIndex = -1;
                    dg_ExcessDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Packing Type";// GetLocalResourceObject("Msg_Duplicate").ToString();
            }
        }
    }

    protected void dg_ExcessDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ExcessDetails.EditItemIndex = e.Item.ItemIndex;
        dg_ExcessDetails.ShowFooter = false;
        BindExcessDetailsGrid = SessionBindExcessGrid;
    }

    protected void dg_ExcessDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionBindExcessGrid;
            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_ExcessDetails.EditItemIndex = -1;
                dg_ExcessDetails.ShowFooter = true;

                BindExcessDetailsGrid = SessionBindExcessGrid;
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Packing Type"; //GetLocalResourceObject("Msg_Duplicate").ToString();
        }
    }

    protected void dg_ExcessDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ExcessDetails.EditItemIndex = -1;
        dg_ExcessDetails.ShowFooter = true;
        BindExcessDetailsGrid = SessionBindExcessGrid;
    }

    protected void dg_ExcessDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionBindExcessGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionBindExcessGrid = objDT;
            dg_ExcessDetails.EditItemIndex = -1;
            dg_ExcessDetails.ShowFooter = true;
            BindExcessDetailsGrid = SessionBindExcessGrid;
        }
    }

    protected void dg_ExcessDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    protected void ddl_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Commodity = (DropDownList)sender;
        DataGridItem _item = (DataGridItem)ddl_Commodity.Parent.Parent;
        ddl_Item = (DropDownList)_item.FindControl("ddl_Item");
        objAUSExcessDetailsPresenter.FillItemOnCommodityChanged();
        BindItem = SessionBindItemDropdown;
    }

}