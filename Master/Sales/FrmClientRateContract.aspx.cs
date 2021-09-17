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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Sales_FrmClientRateContract : ClassLibraryMVP.UI.Page
{

    Raj.EC.Common ObjCommon = new Raj.EC.Common();

    private DAL objDAL = new DAL();
    private DataSet objDS;
    DataRow dr;

    DataTable objDT;
    DropDownList ddl_FromLocation, ddl_ToLocation;
    TextBox txt_Rate;
    LinkButton lbtn_Add_Commodity;
    bool Allow_To_Save;

    public int KeyId
    {
        set { hdnKeyID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnKeyID.Value); }
    }


    public DataTable Session_FromLocationDdl
    {
        get { return StateManager.GetState<DataTable>("FromLocationDdl"); }
        set { StateManager.SaveState("FromLocationDdl", value); }
    }

    public DataTable Session_ToLocationDdl
    {
        get { return StateManager.GetState<DataTable>("ToLocationDdl"); }
        set { StateManager.SaveState("ToLocationDdl", value); }
    }

    private int ContractForID
    {

        get { return Util.String2Int(ddl_ContractFor.SelectedValue); }
        set
        {
            ddl_ContractFor.SelectedValue = Util.Int2String(value);
        }
    }

    private int ClientGroupID
    {

        get { return Util.String2Int(ddl_ClientGroup.SelectedValue); }
        set
        {
            ddl_ClientGroup.SelectedValue = Util.Int2String(value);
        }
    }

    private DateTime ValidFromDate
    {
        set { dtpValidFrom.SelectedDate = value; }
        get { return dtpValidFrom.SelectedDate; }
    }
    private DateTime ValidUptoDate
    {
        set { dtpValidUpto.SelectedDate = value; }
        get { return dtpValidUpto.SelectedDate; }
    }

    private int RateTypeID
    {
        get { return Util.String2Int(ddl_RateType.SelectedValue); }
        set
        {
            ddl_RateType.SelectedValue = Util.Int2String(value);
        }
    }

    private decimal BiltyCharges
    {
        set { txt_BiltyCharges.Text = Util.Decimal2String(value); }
        get { return txt_BiltyCharges.Text == string.Empty ? 0 : Util.String2Decimal(txt_BiltyCharges.Text); }
    }

    private decimal HamaliPerKg
    {
        set { txt_HamaliPerKg.Text = Util.Decimal2String(value); }
        get { return txt_HamaliPerKg.Text == string.Empty ? 0 : Util.String2Decimal(txt_HamaliPerKg.Text); }
    }

    private decimal FOVPercent
    {
        set { txt_FOVPercent.Text = Util.Decimal2String(value); }
        get { return txt_FOVPercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_FOVPercent.Text); }
    }

    private decimal FOVExemptUpTo
    {
        set { txt_FOVExemptUpTo.Text = Util.Decimal2String(value); }
        get { return txt_FOVExemptUpTo.Text == string.Empty ? 0 : Util.String2Decimal(txt_FOVExemptUpTo.Text); }
    }

    private decimal AOCPercent
    {
        set { txt_AOCPercent.Text = Util.Decimal2String(value); }
        get { return txt_AOCPercent.Text == string.Empty ? 0 : Util.String2Decimal(txt_AOCPercent.Text); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            KeyId = Util.DecryptToInt(Request.QueryString["Id"]);

            fillFromLocation();

            ddlParty.DataTextField = "Client_Name";
            ddlParty.DataValueField = "Client_Id";
            ddlParty.OtherColumns = "2";

            Fill_ClientGroup();

            ReadValues();
        }


        if (ContractForID == 1)
        {
            tr_Client.Attributes.Add("style", "display:block");
            tr_ClientGroup.Attributes.Add("style", "display:none");
        }
        else
        {
            tr_Client.Attributes.Add("style", "display:none");
            tr_ClientGroup.Attributes.Add("style", "display:block");
        }

        if (FOVPercent > 0)
        {
            tr_FOVExemptUpTo.Attributes.Add("style", "display:block");
        }
        else
        {
            tr_FOVExemptUpTo.Attributes.Add("style", "display:none");
        }

    }

    private void fillFromLocation()
    {
        objDAL.RunProc("[dbo].[EF_Mst_FillServiceLocation]",  ref objDS);

        Session_FromLocationDdl = objDS.Tables[0];

        Session_ToLocationDdl = objDS.Tables[0];

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

    public DataTable BindFromLocation
    {
        set { Set_Common_DDL(ddl_FromLocation, "Service_Location_Name", "Service_Location_ID", value, true); }
    }

    public DataTable BindToLocation
    {
        set { Set_Common_DDL(ddl_ToLocation, "Service_Location_Name", "Service_Location_ID", value, true); }
    }

    public DataTable Session_Grid
    {
        get { return StateManager.GetState<DataTable>("Grid"); }
        set { StateManager.SaveState("Grid", value); }
    }

    private void Bind_dg_Commodity()
    {
        dg_Commodity.DataSource = Session_Grid;
        dg_Commodity.DataBind();

    } 

    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@RateContractID", SqlDbType.Int, 0, KeyId)};
        objDAL.RunProc("EC_Master_RateContract_ReadValues", objSqlParam, ref ds);


        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0];

            text = objDR["ClientName"].ToString();
            value = objDR["ClientID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlParty);

            hdn_Is_Regular_Client.Value = objDR["Is_Client_Regular"].ToString();

            ContractForID = Util.String2Int(objDR["RateContractForID"].ToString());
            ClientGroupID = Util.String2Int(objDR["ClientGroupID"].ToString());

            ValidFromDate = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            ValidUptoDate = Convert.ToDateTime(objDR["ValidUpTo"].ToString());

            RateTypeID = Util.String2Int(objDR["RateTypeID"].ToString());

            BiltyCharges = Util.String2Decimal(objDR["BiltyCharges"].ToString());
            HamaliPerKg = Util.String2Decimal(objDR["HamaliPerKg"].ToString());
            AOCPercent = Util.String2Decimal(objDR["AOCPercent"].ToString());
            FOVPercent = Util.String2Decimal(objDR["FOVPercent"].ToString());
            FOVExemptUpTo = Util.String2Decimal(objDR["FOVExemptUpTo"].ToString());

        }

        Session_Grid = ds.Tables[1];

        Bind_dg_Commodity();

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

        DAL objDAL = new DAL();

        string ClientID = ddlParty.SelectedValue;

        if (ContractForID == 2)
        {
            hdn_Is_Regular_Client.Value = "false";
        }


        DataTable DT1 = Session_Grid.Copy();
        DT1.TableName = "Grid_Details";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);
        string DetailsXML = ds.GetXml().ToLower();


        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@RateContractID",SqlDbType.Int,0,KeyId), 
            objDAL.MakeInParams("@RateContractForID",SqlDbType.Int,0,ContractForID),
            objDAL.MakeInParams("@ClientGroupID",SqlDbType.Int,0,ClientGroupID),
            objDAL.MakeInParams("@ClientID",SqlDbType.VarChar ,10,ClientID),
            objDAL.MakeInParams("@Is_Client_Regular",SqlDbType.Bit ,0,Util.String2Bool(hdn_Is_Regular_Client.Value)),
            objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,ValidFromDate),
            objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,ValidUptoDate),
            objDAL.MakeInParams("@RateTypeID",SqlDbType.Int,0,RateTypeID),
            objDAL.MakeInParams("@BiltyCharges",SqlDbType.Decimal,0, BiltyCharges),
            objDAL.MakeInParams("@HamaliPerKg",SqlDbType.Decimal,0, HamaliPerKg),
            objDAL.MakeInParams("@AOCPercent",SqlDbType.Decimal,0, AOCPercent),
            objDAL.MakeInParams("@FOVPercent",SqlDbType.Decimal,0, FOVPercent),
            objDAL.MakeInParams("@FOVExemptUpTo",SqlDbType.Decimal,0, FOVExemptUpTo),
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Master_RateContract_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";

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

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (ContractForID == 1 &&  ddlParty.SelectedValue.Trim()  == "")
        {
            lblErrors.Text = "Please Select Client";
            ddlParty.Focus();
        }
        else if (ContractForID == 2 && ClientGroupID <=0)
        {
            lblErrors.Text = "Please Select Client Group";
            ddl_ClientGroup.Focus();
        }
        else if (ValidFromDate > ValidUptoDate)
        {
            lblErrors.Text = "Valid Upto must be greater than Valid from date";
            dtpValidFrom.Focus();
        }
        else if (Session_Grid.Rows.Count <= 0)
        {
            lblErrors.Text = "Enter Atleast One Record";
            dg_Commodity.Focus();
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    private void Fill_ClientGroup()
    {
        DAL objDAL = new DAL();

        DataSet dsClientGroup = new DataSet();

        objDAL.RunProc("EC_Master_Fill_ClientGroup",  ref dsClientGroup);

        ddl_ClientGroup.DataSource = dsClientGroup;
        ddl_ClientGroup.DataTextField = "Client_Group_Name";
        ddl_ClientGroup.DataValueField = "Client_Group_ID";
        ddl_ClientGroup.DataBind();
    }

    protected void ddlParty_TxtChange(object sender, EventArgs e)
    {


        string ddlParty_Id;
        ddlParty_Id = Convert.ToString(ddlParty.SelectedValue);
        DataSet ds = new DataSet();
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_ID",SqlDbType.VarChar ,15,ddlParty_Id)
        };

        objDAL.RunProc("dbo.EC_Master_RateContract_GetClientDetails", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            hdn_Is_Regular_Client.Value = objDR["Is_Regular_Client"].ToString();
        }

        ddlParty.Focus();
    }

    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_FromLocation = (DropDownList)(e.Item.FindControl("ddl_FromLocation"));
                ddl_ToLocation = (DropDownList)(e.Item.FindControl("ddl_ToLocation"));
                txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));

                lbtn_Add_Commodity = (LinkButton)(e.Item.FindControl("lbtn_Add_Commodity"));

                BindFromLocation  = Session_FromLocationDdl;
                BindToLocation = Session_ToLocationDdl;

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {

                DataRow DR = null;
                DataTable dt = Session_Grid;
                DR = dt.Rows[e.Item.ItemIndex];
                BindFromLocation = Session_FromLocationDdl;
                ddl_FromLocation.SelectedValue = DR["FromLocationID"].ToString();

                BindToLocation= Session_ToLocationDdl;

                ddl_ToLocation.SelectedValue = DR["ToLocationID"].ToString();
                txt_Rate.Text = DR["Rate"].ToString();
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        lblErrors.Text = "";

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;


        Bind_dg_Commodity();
        lblErrors.Text = "";

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_Grid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_Grid.AcceptChanges();
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
            lblErrors.Text = "Duplicate From Location, To Location";
        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_Grid;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];
                _dtColumnPrimaryKey[0] = objDT.Columns["FromLocationID"];
                _dtColumnPrimaryKey[1] = objDT.Columns["ToLocationID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1;
                    dg_Commodity.ShowFooter = true;

                }
            }
            catch (ConstraintException)
            {
                lblErrors.Text = "Duplicate From Location, To Location";
            }
        }
    }


    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        lblErrors.Text = "";


        decimal Rate = txt_Rate.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Rate.Text.Trim());

        if (Util.String2Int(ddl_FromLocation.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select From Location.";
            scm_Comm.SetFocus(ddl_FromLocation);
        }
        else if (Util.String2Int(ddl_ToLocation.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select To Location.";
            scm_Comm.SetFocus(ddl_ToLocation);
        }
        else if (Rate <= 0)
        {
            lblErrors.Text = "Please Enter Freight Rate";
            scm_Comm.SetFocus(txt_Rate);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }

    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    {
        ddl_FromLocation = (DropDownList)(e.Item.FindControl("ddl_FromLocation"));
        ddl_ToLocation = (DropDownList)(e.Item.FindControl("ddl_ToLocation"));
        txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));

        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_Grid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_Grid.Rows[e.Item.ItemIndex];
            }

            dr["FromLocationID"] = ddl_FromLocation.SelectedValue;
            dr["FromLocation"] = Util.String2Int(ddl_FromLocation.SelectedValue) == 0 ? "" : ddl_FromLocation.SelectedItem.Text;
            dr["ToLocationID"] = ddl_ToLocation.SelectedValue;
            dr["ToLocation"] = Util.String2Int(ddl_ToLocation.SelectedValue) == 0 ? "" : ddl_ToLocation.SelectedItem.Text;

            dr["Rate"] = txt_Rate.Text.Trim() == string.Empty ? "0" : txt_Rate.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_Grid.Rows.Add(dr);
            }
        }
    }


}
