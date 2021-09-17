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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Master_Branch_FrmBranchBharaiRates : ClassLibraryMVP.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList ddl_Size, ddl_Item; 
    TextBox txt_RatePerArticle;
    DataRow dr;
    LinkButton lbtn_Add_Commodity;
    bool Allow_To_Save;
    DataTable objDT;
    int _branch_id;

    public bool validateUI()
    {
        bool ATS;
        ATS = false;
 

        if (Session_BharaiGrid.Rows.Count <= 0)
        {
            lblErrors.Text = "Please Select Item, Respective Size And Rate";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    public int Branch_Id
    {
        get { return _branch_id; }
        set { _branch_id = value; }
    }

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
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();
        Set_LabelText(); 
         
    } 
    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
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
 
    public DataTable BindItem
    {
        set { Set_Common_DDL(ddl_Item, "Item_Name", "ItemID", value, true); }
    }
    public DataTable BindSize
    {
        set { Set_Common_DDL(ddl_Size, "SizeName", "SizeID", value, true); }
    }

    public DataTable Session_BharaiGrid
    {
        get { return StateManager.GetState<DataTable>("BharaiGrid"); }
        set { StateManager.SaveState("BharaiGrid", value); }
    } 

    public DataTable Session_ItemDdl
    {
        get { return StateManager.GetState<DataTable>("ItemDdl"); }
        set { StateManager.SaveState("ItemDdl", value); }
    }

    public DataTable Session_SizeDdl
    {
        get { return StateManager.GetState<DataTable>("SizeDdl"); }
        set { StateManager.SaveState("SizeDdl", value); }
    }

    public decimal TotalRate
    {
        set
        {
            lbl_TotalRate.Text = value.ToString();
            hdn_TotalRate.Value = value.ToString();
        }
        get
        {  
            if (Session_BharaiGrid.Rows.Count > 0)

            { return Convert.ToDecimal(Session_BharaiGrid.Compute("Sum(RatePerArticle)", "")); }
            else
            { return hdn_TotalRate.Value == string.Empty ? 0 : Util.String2Decimal(hdn_TotalRate.Value); }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 

            ddlBranch.DataTextField = "Branch_Name";
            ddlBranch.DataValueField = "Branch_Id";

            fillBharaiRatesValues();
            fillBharaiRatesDetails();
            if (Convert.ToInt32(hdnKeyID.Value) > 0)
            {
                td_ApplicableFrom.Disabled = true;
                dtpApplicableFrom.Disable = true;
                ddlBranch.Enabled = false;  
            }
        }
    }

    private void fillBharaiRatesValues()
    {  
        objDAL.RunProc("EC_Mst_BharaiRates_FillValues",  ref objDS);
        Session_ItemDdl = objDS.Tables[0];  
        Session_SizeDdl = objDS.Tables[1];
    }
    private void fillBharaiRatesDetails()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@BharaiRateID", SqlDbType.Int, 0,Convert.ToInt32(hdnKeyID.Value))};

        objDAL.RunProc("EC_Mst_BharaiRatesDetails", objSqlParam, ref objDS);
        
        Session_BharaiGrid = objDS.Tables[0];

        if (objDS.Tables[1].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = objDS.Tables[1].Rows[0];

            text = objDR["Branch_Name"].ToString();
            value = objDR["Branch_ID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlBranch);
            dtpApplicableFrom.SelectedDate = Convert.ToDateTime(objDR["ApplicableFrom"].ToString());
        }
        Bind_dg_Commodity();
    }

    private void Set_LabelText()
    { 
        hdn_TotalRate.Value = Session_BharaiGrid.Compute("SUM(RatePerArticle)", "").ToString();  
        hdn_TotalRate.Value = hdn_TotalRate.Value == string.Empty ? "0" : hdn_TotalRate.Value; 
        
        lbl_TotalRate.Text = hdn_TotalRate.Value; 
    }
    protected void ddlBranch_TxtChange(object sender, EventArgs e)
    { 
        Branch_Id = Convert.ToInt32(ddlBranch.SelectedValue);
    }
    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
                ddl_Size = (DropDownList)(e.Item.FindControl("ddl_Size")); 
                txt_RatePerArticle = (TextBox)(e.Item.FindControl("txt_RatePerArticle"));
               
                lbtn_Add_Commodity = (LinkButton)(e.Item.FindControl("lbtn_Add_Commodity"));

                BindSize = Session_SizeDdl;
                ddl_Size.SelectedValue = "2";
                
                BindItem = Session_ItemDdl;  
                
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                //dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
                fillBharaiRatesValues();
                DataRow DR = null;
                DataTable dt = Session_BharaiGrid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                ddl_Size.SelectedValue = DR["SizeID"].ToString(); 
                ddl_Item.SelectedValue = DR["ItemID"].ToString(); 
                txt_RatePerArticle.Text = DR["RatePerArticle"].ToString();
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;
         

        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_BharaiGrid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;
            Bind_dg_Commodity();
        } 

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
                 
                Bind_dg_Commodity(); 
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate Packing Type,Item And Size";
            scm_Comm.SetFocus(ddl_Size);
        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_BharaiGrid;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];
                _dtColumnPrimaryKey[0] = objDT.Columns["ItemID"];
                _dtColumnPrimaryKey[1] = objDT.Columns["SizeID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1; 
                    dg_Commodity.ShowFooter = true;
                  
                }
                //TotalRate = TotalRate;
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate Packing Type,Item And Size";
                scm_Comm.SetFocus(ddl_Size);
            }
        }
    }
    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    { 
        ddl_Item = (DropDownList)(e.Item.FindControl("ddl_Item"));
        ddl_Size = (DropDownList)(e.Item.FindControl("ddl_Size")); 
        txt_RatePerArticle = (TextBox)(e.Item.FindControl("txt_RatePerArticle"));
      
        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_BharaiGrid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            }

            dr["ItemID"] = ddl_Item.SelectedValue;
            dr["Item_Name"] = Util.String2Int(ddl_Item.SelectedValue) == 0 ? "" : ddl_Item.SelectedItem.Text;
            dr["SizeId"] = ddl_Size.SelectedValue;
            dr["SizeName"] = ddl_Size.SelectedItem.Text; 
            dr["RatePerArticle"] = txt_RatePerArticle.Text.Trim() == string.Empty ? "0" : txt_RatePerArticle.Text.Trim();  

            if (e.CommandName == "Add")
            {
                Session_BharaiGrid.Rows.Add(dr);
            }
        }
    }
 

    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        ErrorMsg = "";
        int Articles, PackingTypeId;
 
        decimal RatePerArticle = txt_RatePerArticle.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_RatePerArticle.Text.Trim());

        if (Util.String2Int(ddl_Item.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Item.";
            scm_Comm.SetFocus(ddl_Item);
        }
        else if (Util.String2Int(ddl_Size.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Size.";
            scm_Comm.SetFocus(ddl_Size);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }
     

    protected void btn_Item_Click(object sender, EventArgs e)
    {
        dg_Commodity.ShowFooter = true;
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();
    }

    protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsComm;
        ddl_Item = (DropDownList)sender;
        DataGridItem dg_Commodity = (DataGridItem)ddl_Item.Parent.Parent;

        ddl_Item = (DropDownList)(dg_Commodity.FindControl("ddl_Item"));

        //dsComm = fillCommodityValues(Util.String2Int(ddl_Item.SelectedValue));
        scm_Comm.SetFocus(ddl_Item);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        //DataTable DT = (DataTable)(Session["Session_BharaiGrid"]);

        DataTable DT1 = Session_BharaiGrid.Copy();
        DT1.TableName = "bharaigrid";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string bharaiDetailsXML = ds.GetXml().ToLower();

 
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@BharaiRateID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)), 
            objDAL.MakeInParams("@BranchID",SqlDbType.Int,0,Util.String2Int(ddlBranch.SelectedValue)),  
            objDAL.MakeInParams("@ApplicableFrom",SqlDbType.DateTime,0,dtpApplicableFrom.SelectedDate), 
            objDAL.MakeInParams("@BharaiDetailsXML",SqlDbType.Xml,0,bharaiDetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Mst_BharaiRates_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }
}
