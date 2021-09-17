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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using Raj.EF.CallBackFunction;
public partial class Operations_Booking_FrmNewGCCommodity : System.Web.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    TextBox txt_NewArticles, txt_Weight, txt_Rate, txt_Amount, txt_NewPacking_Type, txt_ddlItem, txt_Size, txt_ActualWt;
    CheckBox chk_Is_Service_Tax_App_For_Commodity;
    DataRow dr;
    ListBox lst_PackingType, lst_ItemType, lst_Size;
    LinkButton lbtn_Add_Commodity;
    HiddenField hdfn_ToBe, hdfn_ItemRatePerKg, hdnBkgTypeID, hdn_NewPackingTypeId, hdn_ddlItemId, hdn_SizeId, hdfn_BharaiAmt;
    bool Allow_To_Save;

    public int Session_Mode
    {
        get { return StateManager.GetState<int>("SessionMode"); }
        set { StateManager.SaveState("SessionMode", value); }
    }
    public int Session_MenuItem
    {
        get { return StateManager.GetState<int>("SessionMenuItem"); }
        set { StateManager.SaveState("SessionMenuItem", value); }
    }

    private void Bind_dg_Commodity()
    {
        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();
        Set_LabelText();
        Is_service_Tax_Applicable_For_Commodity();
        if (IsPostBack)
            updateparentdataset();
    }
    public bool Is_Item_Required
    {
        set { chk_Is_Item_required.Checked = value; }
        get { return chk_Is_Item_required.Checked; }
    }
    public string Default_Commodity_Weight
    {
        set { hdn_Default_Commodity_Weight.Value = value; }
        get { return hdn_Default_Commodity_Weight.Value; }
    }
    private string ErrorMsg
    {
        set { lbl_CommodityErrorMsg.Text = value; }
    }
    private string ClientCode
    {
        get { return CompanyManager.getCompanyParam().ClientCode.ToLower(); }
    }
    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    public DataTable Session_MultipleCommodityGrid
    {
        get { return StateManager.GetState<DataTable>("MultipleCommodityGrid"); }
        set { StateManager.SaveState("MultipleCommodityGrid", value); }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Operations_Booking_FrmNewGCCommodity));
        lnk_AddCommodity.Style.Add("display", "none");
        lnk_AddItem.Style.Add("display", "none");
        if (!IsPostBack)
        {
            SetDefaultValues();
            Bind_dg_Commodity();
            if (Session_Mode == 4 || Session_MenuItem == 194)
            {
                dg_Commodity.Columns[8].Visible = false;
                dg_Commodity.Columns[9].Visible = false;
                dg_Commodity.ShowFooter = false;
                lnk_AddCommodity.Visible = false;
                lnk_AddItem.Visible = false;
            }
        }
        if (hdn_Bkg_TypeID.Value == "")
        {
            hdn_Bkg_TypeID.Value = "1";

        } 
    }

    private void Set_LabelText()
    {
        hdn_TotalArticles.Value = Session_MultipleCommodityGrid.Compute("SUM(Articles)", "").ToString();
        hdn_TotalWeight.Value = Session_MultipleCommodityGrid.Compute("SUM(Weight)", "").ToString();
        hdn_TotalRate.Value = Session_MultipleCommodityGrid.Compute("SUM(Rate)", "").ToString();
        hdn_TotalAmount.Value = Session_MultipleCommodityGrid.Compute("SUM(Amount)", "").ToString();
        hdn_ItemValueForFOV.Value = Session_MultipleCommodityGrid.Compute("SUM(ItemValue)", "").ToString();
        hdn_TotalBharaiAmt.Value = Session_MultipleCommodityGrid.Compute("SUM(BharaiAmt)", "").ToString();

        hdn_TotalArticles.Value = hdn_TotalArticles.Value == string.Empty ? "0" : hdn_TotalArticles.Value;
        hdn_TotalWeight.Value = hdn_TotalWeight.Value == string.Empty ? "0" : hdn_TotalWeight.Value;
        hdn_TotalRate.Value = hdn_TotalRate.Value == string.Empty ? "0" : hdn_TotalRate.Value;
        hdn_TotalAmount.Value = hdn_TotalAmount.Value == string.Empty ? "0" : hdn_TotalAmount.Value;
        hdn_ItemValueForFOV.Value = hdn_ItemValueForFOV.Value == string.Empty ? "0" : hdn_ItemValueForFOV.Value;
        hdn_TotalBharaiAmt.Value = hdn_TotalBharaiAmt.Value == string.Empty ? "0" : hdn_TotalBharaiAmt.Value;

        if (Session_MultipleCommodityGrid.Rows.Count > 0)
        {
            hdn_FirstCommodityId.Value = Session_MultipleCommodityGrid.Rows[0]["Commodity_ID"].ToString();
            hdn_FirstItemId.Value = Session_MultipleCommodityGrid.Rows[0]["Item_ID"].ToString();
            hdn_FirstPackingTypeId.Value = Session_MultipleCommodityGrid.Rows[0]["Packing_ID"].ToString();
        }
        else
        {
            hdn_FirstCommodityId.Value = "0";
            hdn_FirstItemId.Value = "0";
            hdn_FirstPackingTypeId.Value = "0";
        }
        lbl_TotalArticles.Text = hdn_TotalArticles.Value;
        lbl_TotalWeight.Text = hdn_TotalWeight.Value;
        lbl_TotalRate.Text = hdn_TotalRate.Value;
        lbl_TotalAmount.Text = hdn_TotalAmount.Value;
        //lbl_ItemValueForFOV.Text = hdn_ItemValueForFOV.Value;
    }
    private DataSet fillCommodityValues(int itemid)
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@flag", SqlDbType.Int, 0,1),
                                    objDAL.MakeInParams("@CommodityId", SqlDbType.Int, 0, 0),
                                    objDAL.MakeInParams("@ItemID", SqlDbType.Int, 0, itemid)};

        objDAL.RunProc("EC_Opr_NewGC_Commodity_FillValues", objSqlParam, ref objDS);
        return objDS;
    }
    private void SetDefaultValues()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@flag", SqlDbType.Int,0, 2),
                                    objDAL.MakeInParams("@CommodityId", SqlDbType.Int, 0, 0) };

        objDAL.RunProc("EC_Opr_NewGC_Commodity_FillValues", objSqlParam, ref objDS);

        Is_Item_Required = Util.String2Bool(objDS.Tables[0].Rows[0]["Is_Item_Required"].ToString());
        Default_Commodity_Weight = objDS.Tables[0].Rows[0]["Default_Commodity_Weight"].ToString();
    }
    private void updateparentdataset()
    {
        string popupScript = "<script language='javascript'>updateparentdataset('" + hdn_TotalArticles.Value + "','" + hdn_TotalWeight.Value + "','" + hdn_TotalRate.Value + "','" + hdn_TotalAmount.Value + "','" + hdn_Is_Service_Tax_Applicable_For_Commodity.Value + "','" + hdn_ItemValueForFOV.Value + "','" + hdn_TotalBharaiAmt.Value + "');</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
    }
    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_NewArticles = (TextBox)(e.Item.FindControl("txt_NewArticles"));
                txt_NewPacking_Type = (TextBox)(e.Item.FindControl("txt_NewPacking_Type"));
                hdn_NewPackingTypeId = (HiddenField)(e.Item.FindControl("hdn_NewPackingTypeId"));
                lst_PackingType = (ListBox)(e.Item.FindControl("lst_PackingType"));

                txt_ddlItem = (TextBox)(e.Item.FindControl("txt_ddlItem"));
                hdn_ddlItemId = (HiddenField)(e.Item.FindControl("hdn_ddlItemId"));
                lst_ItemType = (ListBox)(e.Item.FindControl("lst_ItemType"));

                txt_ActualWt = (TextBox)(e.Item.FindControl("txt_ActualWt"));

                txt_Size = (TextBox)(e.Item.FindControl("txt_Size"));
                hdn_SizeId = (HiddenField)(e.Item.FindControl("hdn_SizeId"));
                lst_Size = (ListBox)(e.Item.FindControl("lst_Size"));

                txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));
                txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                hdfn_ToBe = (HiddenField)(e.Item.FindControl("hdfn_ToBe"));
                hdfn_ItemRatePerKg = (HiddenField)(e.Item.FindControl("hdfn_ItemRatePerKg"));                
                lbtn_Add_Commodity = (LinkButton)(e.Item.FindControl("lbtn_Add_Commodity"));
                chk_Is_Service_Tax_App_For_Commodity = (CheckBox)(e.Item.FindControl("chk_Is_ServiceTax_ForCommodity"));
                hdfn_ItemRatePerKg = (HiddenField)(e.Item.FindControl("hdfn_ItemRatePerKg"));
                chk_Is_Service_Tax_App_For_Commodity.Attributes.Add("display", "none");
                hdnBkgTypeID = (HiddenField)(Page.FindControl("hdn_Bkg_TypeID"));

                hdfn_BharaiAmt = (HiddenField)(e.Item.FindControl("hdfn_BharaiAmt"));

                chk_Is_Service_Tax_App_For_Commodity = (CheckBox)(e.Item.FindControl("chk_Is_ServiceTax_ForCommodity"));

                hdn_SizeId.Value = "1";
                txt_Size.Text = "M";
                txt_Weight.Text = Default_Commodity_Weight;

                txt_NewArticles.Attributes.Add("onchange", "CalculateApproxWeightAndAmount('" + txt_NewArticles.ClientID
                    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                    + hdnBkgTypeID.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                //////packing type///
                txt_NewPacking_Type.Attributes.Add("onblur", "On_PackingLostFocus('" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','" + hdn_NewPackingTypeId.ClientID + "')");
                txt_NewPacking_Type.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','PackingType',2)");
                txt_NewPacking_Type.Attributes.Add("onfocus", "On_Focus('" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','PackingType')");
                txt_NewPacking_Type.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "')");

                lst_PackingType.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_PackingType.ClientID + "','" + txt_NewPacking_Type.ClientID + "')");
                
                //////item type///
                txt_ddlItem.Attributes.Add("onblur", "On_ItemLostFocus('" + txt_NewArticles.ClientID
                    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                    + hdnBkgTypeID.ClientID + "','" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                txt_ddlItem.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','Item',2)");
                txt_ddlItem.Attributes.Add("onfocus", "On_Focus('" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','Item')");
                txt_ddlItem.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "')");

                lst_ItemType.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_ItemType.ClientID + "','" + txt_ddlItem.ClientID + "')");

                //////Size ///
                //txt_Size.Attributes.Add("onchange", "CalculateApproxWeightAndAmount('" + txt_NewArticles.ClientID
                //    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                //    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                //    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                //    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                //    + hdnBkgTypeID.ClientID + "')");

                txt_Size.Attributes.Add("onblur", "On_SizeLostFocus('" + txt_NewArticles.ClientID
                    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                    + hdnBkgTypeID.ClientID + "','" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                txt_Size.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','Size',1)");
                txt_Size.Attributes.Add("onfocus", "On_Focus('" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','Size')");
                txt_Size.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_Size.ClientID + "','" + lst_Size.ClientID + "')");

                lst_Size.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_Size.ClientID + "','" + txt_Size.ClientID + "')");

                if (e.Item.ItemType != ListItemType.EditItem)
                {
                    txt_Size.Attributes.Add("onkeyPress", "setFocusonAdd('" + txt_Size.ClientID + "','" + lbtn_Add_Commodity.ClientID + "')");
                }

                lst_PackingType.Style.Add("visibility", "hidden");
                lst_ItemType.Style.Add("visibility", "hidden");
                lst_Size.Style.Add("visibility", "hidden");
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];
                scm_Comm.SetFocus(txt_NewArticles);

                txt_NewPacking_Type.Text = dr["Packing_Type"].ToString();
                hdn_NewPackingTypeId.Value = dr["Packing_ID"].ToString();
                txt_ddlItem.Text = dr["Item_Name"].ToString();
                hdn_ddlItemId.Value = dr["Item_ID"].ToString();
                txt_ActualWt.Text =  dr["ActualWt"].ToString();

                txt_Size.Text = dr["SizeName"].ToString();
                hdn_SizeId.Value = dr["SizeID"].ToString();
                txt_NewArticles.Text = dr["Articles"].ToString();
                txt_Weight.Text = dr["Weight"].ToString();
                txt_Rate.Text = dr["Rate"].ToString();
                txt_Amount.Text = dr["Amount"].ToString();
                hdfn_ToBe.Value = dr["ToBe"].ToString();
                hdfn_ItemRatePerKg.Value = dr["ItemRatePerKg"].ToString(); 
                chk_Is_Service_Tax_App_For_Commodity.Checked = Util.String2Bool(dr["Is_Service_Tax_App_For_Commodity"].ToString());

                hdfn_BharaiAmt.Value = dr["BharaiAmt"].ToString();

                if (hdn_Bkg_TypeID.Value == "2" || UserManager.getUserParam().MainId == 50 || UserManager.getUserParam().MainId == 53 || UserManager.getUserParam().MainId == 78)
                {
                    txt_Weight.Enabled = true;
                    txt_Amount.Enabled = true;
                }
                else
                {
                    txt_Weight.Enabled = false;
                    txt_Amount.Enabled = false;

                }

                lst_PackingType.Style.Add("visibility", "hidden");
                lst_ItemType.Style.Add("visibility", "hidden");
                lst_Size.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        ErrorMsg = ""; 
        scm_Comm.SetFocus(txt_NewArticles);
    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;
        if (Session_MultipleCommodityGrid.Rows.Count == 3)
            dg_Commodity.ShowFooter = false;

        Bind_dg_Commodity();
        ErrorMsg = "";

        scm_Comm.SetFocus(txt_NewArticles);

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_MultipleCommodityGrid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;
            Bind_dg_Commodity();
        }

        scm_Comm.SetFocus(txt_NewArticles);

    }
    protected void dg_Commodity_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Commodity_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;
                if (Session_MultipleCommodityGrid.Rows.Count == 3)
                    dg_Commodity.ShowFooter = false;
                Bind_dg_Commodity();

                string invoicefocusScript = "<script language='javascript'>setfocusoninvoice();</script>";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "invoicefocusScript", invoicefocusScript.ToString(), false);
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate Packing Type,Item And Size";
            scm_Comm.SetFocus(txt_Size);
        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1;

                    if (Session_MultipleCommodityGrid.Rows.Count == 3)
                        dg_Commodity.ShowFooter = false;
                    //Start added on 23-12-13
                    string invoicefocusScript = "<script language='javascript'>setfocusoninvoice();</script>";
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "invoicefocusScript", invoicefocusScript.ToString(), false);
                    //End added on 23-12-13
                }
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate Packing Type,Item And Size";
                scm_Comm.SetFocus(txt_Size);
            }
        }
    }

    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    {
        txt_NewArticles = (TextBox)(e.Item.FindControl("txt_NewArticles"));

        txt_NewPacking_Type = (TextBox)(e.Item.FindControl("txt_NewPacking_Type"));
        hdn_NewPackingTypeId = (HiddenField)(e.Item.FindControl("hdn_NewPackingTypeId"));
        lst_PackingType = (ListBox)(e.Item.FindControl("lst_PackingType"));

        txt_ddlItem = (TextBox)(e.Item.FindControl("txt_ddlItem"));
        hdn_ddlItemId = (HiddenField)(e.Item.FindControl("hdn_ddlItemId"));
        lst_ItemType = (ListBox)(e.Item.FindControl("lst_ItemType"));

        txt_ActualWt = (TextBox)(e.Item.FindControl("txt_ActualWt"));

        txt_Size = (TextBox)(e.Item.FindControl("txt_Size"));
        hdn_SizeId = (HiddenField)(e.Item.FindControl("hdn_SizeId"));
        lst_Size = (ListBox)(e.Item.FindControl("lst_Size"));

        txt_Weight = (TextBox)(e.Item.FindControl("txt_Weight"));
        txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        hdfn_ToBe = (HiddenField)(e.Item.FindControl("hdfn_ToBe"));
        hdfn_ItemRatePerKg = (HiddenField)(e.Item.FindControl("hdfn_ItemRatePerKg"));
        chk_Is_Service_Tax_App_For_Commodity = (CheckBox)(e.Item.FindControl("chk_Is_ServiceTax_ForCommodity"));
        hdnBkgTypeID = (HiddenField)(Page.FindControl("hdn_Bkg_TypeID"));

        hdfn_BharaiAmt = (HiddenField)(e.Item.FindControl("hdfn_BharaiAmt"));

        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_MultipleCommodityGrid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_MultipleCommodityGrid.Rows[e.Item.ItemIndex];
            }

            dr["Articles"] = txt_NewArticles.Text.Trim() == string.Empty ? "0" : txt_NewArticles.Text.Trim();
            dr["Packing_Id"] = hdn_NewPackingTypeId.Value;
            dr["Packing_Type"] = txt_NewPacking_Type.Text.Trim();
            dr["Item_Id"] = hdn_ddlItemId.Value;
            dr["Item_Name"] = Util.String2Int(hdn_ddlItemId.Value) == 0 ? "" : txt_ddlItem.Text;
            dr["ActualWt"] = txt_ActualWt.Text.Trim() == string.Empty ? "0" : txt_ActualWt.Text.Trim();

            dr["SizeId"] = hdn_SizeId.Value;
            dr["SizeName"] = txt_Size.Text;

            dr["Weight"] = txt_Weight.Text.Trim() == string.Empty ? "0" : txt_Weight.Text.Trim();
            dr["Rate"] = txt_Rate.Text.Trim() == string.Empty ? "0" : txt_Rate.Text.Trim();
            dr["Amount"] = txt_Amount.Text.Trim() == string.Empty ? "0" : txt_Amount.Text.Trim();
            dr["ToBe"] = hdfn_ToBe.Value == string.Empty ? "0" : hdfn_ToBe.Value;
            dr["Is_Service_Tax_App_For_Commodity"] = chk_Is_Service_Tax_App_For_Commodity.Checked;
            dr["ItemRatePerKg"] = Convert.ToDecimal(hdfn_ItemRatePerKg.Value);
            dr["ItemValue"] = Convert.ToDecimal(hdfn_ItemRatePerKg.Value) * Convert.ToDecimal(txt_Weight.Text.Trim());

            dr["BharaiAmt"] = hdfn_BharaiAmt.Value == string.Empty ? "0" : hdfn_BharaiAmt.Value;


            if (e.CommandName == "Add")
            {
                Session_MultipleCommodityGrid.Rows.Add(dr);
            }
        }

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_NewArticles.Attributes.Add("onchange", "CalculateApproxWeightAndAmount('" + txt_NewArticles.ClientID + "','" + hdn_ddlItemId.ClientID +
                    "','" + hdn_SizeId.ClientID + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID + "','" + txt_Amount.ClientID +
                    "','" + hdfn_ToBe.ClientID + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID +
                    "','" + hdnBkgTypeID.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                //////packing type///
                txt_NewPacking_Type.Attributes.Add("onblur", "On_PackingLostFocus('" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','" + hdn_NewPackingTypeId.ClientID + "')");
                txt_NewPacking_Type.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','PackingType',2)");
                txt_NewPacking_Type.Attributes.Add("onfocus", "On_Focus('" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "','PackingType')");
                txt_NewPacking_Type.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_NewPacking_Type.ClientID + "','" + lst_PackingType.ClientID + "')");

                lst_PackingType.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_PackingType.ClientID + "','" + txt_NewPacking_Type.ClientID + "')");

                //////item type///
                txt_ddlItem.Attributes.Add("onblur", "On_ItemLostFocus('" + txt_NewArticles.ClientID
                    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                    + hdnBkgTypeID.ClientID + "','" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                txt_ddlItem.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','Item',2)");
                txt_ddlItem.Attributes.Add("onfocus", "On_Focus('" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "','Item')");
                txt_ddlItem.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_ddlItem.ClientID + "','" + lst_ItemType.ClientID + "')");

                lst_ItemType.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_ItemType.ClientID + "','" + txt_ddlItem.ClientID + "')");

                txt_Size.Attributes.Add("onblur", "On_SizeLostFocus('" + txt_NewArticles.ClientID
                    + "','" + hdn_ddlItemId.ClientID + "','" + hdn_SizeId.ClientID
                    + "','" + txt_Weight.ClientID + "','" + txt_Rate.ClientID
                    + "','" + txt_Amount.ClientID + "','" + hdfn_ToBe.ClientID
                    + "','" + chk_Is_Service_Tax_App_For_Commodity.ClientID + "','" + hdfn_ItemRatePerKg.ClientID + "','"
                    + hdnBkgTypeID.ClientID + "','" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','" + hdfn_BharaiAmt.ClientID + "')");

                txt_Size.Attributes.Add("onkeyup", "OnPackingTypeItemsSize(event,'" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','Size',1)");
                txt_Size.Attributes.Add("onfocus", "On_Focus('" + txt_Size.ClientID + "','" + lst_Size.ClientID + "','Size')");
                txt_Size.Attributes.Add("onkeydown", "return on_PackingItemSizekeydown(event,'" + txt_Size.ClientID + "','" + lst_Size.ClientID + "')");

                lst_Size.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_Size.ClientID + "','" + txt_Size.ClientID + "')");
            }
        }
    }

    public void Is_service_Tax_Applicable_For_Commodity()
    {
        hdn_Is_Service_Tax_Applicable_For_Commodity.Value = "0";
        for (int i = 0; i < Session_MultipleCommodityGrid.Rows.Count; i++)
        {
            if (Util.String2Bool(Session_MultipleCommodityGrid.Rows[i]["Is_Service_Tax_App_For_Commodity"].ToString()) == true)
            {
                hdn_Is_Service_Tax_Applicable_For_Commodity.Value = "1";
                break;
            }
        }
    }

    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        ErrorMsg = "";

        int Articles = txt_NewArticles.Text.Trim() == string.Empty ? 0 : Util.String2Int(txt_NewArticles.Text.Trim());
        decimal Weight = txt_Weight.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Weight.Text.Trim());
        decimal Rate = txt_Rate.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Rate.Text.Trim());
        decimal Amount = txt_Amount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text.Trim());

        if (Articles <= 0)
        {
            ErrorMsg = "Please Enter Packages.";
            scm_Comm.SetFocus(txt_NewArticles);
        }
        else if (Util.String2Int(hdn_NewPackingTypeId.Value) <= 0)
        {
            ErrorMsg = "Please Select Packing Type.";
            scm_Comm.SetFocus(txt_NewPacking_Type);
        }
        else if (Util.String2Int(hdn_ddlItemId.Value) <= 0)
        {
            ErrorMsg = "Please Select Item.";
            scm_Comm.SetFocus(txt_ddlItem);
        }
        else if (Util.String2Int(hdn_SizeId.Value) <= 0)
        {
            ErrorMsg = "Please Select Size.";
            scm_Comm.SetFocus(txt_Size);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }
    
    protected void btn_Commodity_Click(object sender, EventArgs e)
    {
        DataRow dr;
        //dr = Session_SizeDdl.NewRow();

        //dr["Size_Id"] = hdn_CommodityId.Value;
        //dr["Size_Name"] = hdn_CommodityName.Value;

        //Session_SizeDdl.Rows.Add(dr);

        dg_Commodity.ShowFooter = true;
        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();
    }

    protected void btn_Item_Click(object sender, EventArgs e)
    {
        dg_Commodity.ShowFooter = true;
        dg_Commodity.DataSource = Session_MultipleCommodityGrid;
        dg_Commodity.DataBind();
    }

    protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsComm;
        //ddl_Item = (DropDownList)sender;
        //DataGridItem dg_Commodity = (DataGridItem)ddl_Item.Parent.Parent;

        //ddl_Item = (DropDownList)(dg_Commodity.FindControl("ddl_Item"));
        //txt_Weight = (TextBox)(dg_Commodity.FindControl("txt_Weight"));
        //txt_Amount = (TextBox)(dg_Commodity.FindControl("txt_Amount"));
        //chk_Is_Service_Tax_App_For_Commodity = (CheckBox)(dg_Commodity.FindControl("chk_Is_ServiceTax_ForCommodity"));
        //hdfn_ItemRatePerKg = (HiddenField)(dg_Commodity.FindControl("hdfn_ItemRatePerKg"));

        //dsComm = fillCommodityValues(Util.String2Int(ddl_Item.SelectedValue));
        //chk_Is_Service_Tax_App_For_Commodity.Checked = dsComm.Tables[1].Rows.Count > 0 ? Util.String2Bool(dsComm.Tables[1].Rows[0]["Is_Service_Tax_Applicable"].ToString()) : false;
        //hdfn_ItemRatePerKg.Value = dsComm.Tables[2].Rows[0]["ItemRatePerKg"].ToString();
        
        //if (hdn_Bkg_TypeID.Value == "1")
        //{
        //    txt_Weight.Enabled = false;
        //    txt_Amount.Enabled = false;
        //}
        //else
        //{
        //    txt_Weight.Enabled = true;
        //    txt_Amount.Enabled = true;
        //}
        //scm_Comm.SetFocus(ddl_Item);
    }
}
