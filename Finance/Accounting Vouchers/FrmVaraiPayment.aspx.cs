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

public partial class Finance_Accounting_Vouchers_FrmVaraiPayment : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    string Mode = "0";
    private DataSet objDS;
    DataTable objDT = new DataTable();
    private DAL objDAL = new DAL();
    string _flag;
    DataRow dr;
    bool Allow_To_Save_Grid;

    DropDownList ddl_Packing;
    TextBox txt_VehicleNo, txt_LRNo, txt_Qty, txt_Rate, txt_Amount;


    ListBox lst_VehicleNo;
    HiddenField hdn_VehicleId;


    public int KeyId
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }


    public String VoucherNo
    {
        set { lbl_VoucherNoValue.Text = value; }
        get { return lbl_VoucherNoValue.Text; }
    }

    private DateTime VoucherDate
    {
        set { dtpVoucherDate.SelectedDate = value; }
        get { return dtpVoucherDate.SelectedDate; }
    }



    private string PayTypeID
    {
        set
        {

            ddlPayType.SelectedValue = value;
        }
        get
        {
            return ddlPayType.SelectedValue;
        }
    }


    public DataTable Session_PackingDdl
    {
        get { return StateManager.GetState<DataTable>("PackingDdl"); }
        set { StateManager.SaveState("PackingDdl", value); }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }


    private decimal Total
    {
        set
        {
            lbl_Total.Text = Util.Decimal2String(value);
            hdn_Total.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total.Value); }
    }

    public string errorMessage
    {
        set { lblErrors.Text = value; }
    }

    private string Bank_Ledger_ID
    {
        set
        {

            ddl_BankLedger.SelectedValue = value;
        }
        get
        {
            return ddl_BankLedger.SelectedValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        errorMessage = "";

        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));
        btn_Close.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Close, btn_Save_Exit));



        if (!IsPostBack)
        {
            FillBankLedger(UserManager.getUserParam().MainId);

            FillPayType();

            FillPacking();

            if (KeyId <= 0)
            {
                Next_Voucher_Number();


            }
            else
            {
                ReadValues();
            }

            FillVarnarPackingDetails();

            fillGridDetails();

            SetLinks();
        }

        lstVarnar.Style.Add("visibility", "hidden");


    }

    public void BindBankLedger(DataTable dt)
    {
        ddl_BankLedger.DataTextField = "Ledger_Name";
        ddl_BankLedger.DataValueField = "Ledger_ID";
        ddl_BankLedger.DataSource = dt;
        ddl_BankLedger.DataBind();
    }

    private void FillBankLedger(int ClientId)
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
        objDAL.RunProc("FA_Mst_Fill_Bank_Ledger_For_Branch", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            BindBankLedger(ds.Tables[0]);
        }
    }

    private void FillPacking()
    {

        objDAL.RunProc("FA_Opr_Fill_Packing_Type", ref objDS);

        Session_PackingDdl = objDS.Tables[0];

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

    public DataTable BindPacking
    {
        set { Set_Common_DDL(ddl_Packing, "Packing_Type", "Packing_Id", value, true); }
    }


    private void fillGridDetails()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0, KeyId) };

        objDAL.RunProc("FA_Opr_VaraiPayment_Details", objSqlParam, ref objDS);

        Session_Grid = objDS.Tables[0];

        Bind_dg_Commodity();
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

    private void FillPayType()
    {
        string query = "Select Pay_Type_ID,Pay_Type from EC_Master_Varai_Pay_Mode where Is_Active=1 Order by Pay_Type_ID";
        DataSet ds = new DataSet();
        ds = objComm.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlPayType.DataSource = ds;
            ddlPayType.DataValueField = "Pay_Type_ID";
            ddlPayType.DataTextField = "Pay_Type";
            ddlPayType.DataBind();
        }
    }



    private void Next_Voucher_Number()
    {
        VoucherNo = objComm.Get_Next_Number();
    }







    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {

        if (Allow_To_Save())
        {
            btn_Save_Exit.Enabled = false;

            _flag = "SaveAndExit";

            Save();
        }
    }


    public Message Save()
    {

        DataTable DT1 = Session_Grid.Copy();
        DT1.TableName = "Grid_Details";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string DetailsXML = ds.GetXml().ToLower();


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar ,5,UserManager.getUserParam().HierarchyCode),
                objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,1),
                objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
                objDAL.MakeInParams("@Voucher_Id",SqlDbType.Int,0,KeyId),
                objDAL.MakeInParams("@Varnar_ID",SqlDbType.Int,0,Util.String2Int(hdn_Varnarld.Value)),
                objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime ,0,dtpVoucherDate.SelectedDate),
                objDAL.MakeInParams("@Pay_Type_ID",SqlDbType.Int,0,Util.String2Int(ddlPayType.SelectedValue)),
                objDAL.MakeInParams("@Beneficiary_Name",SqlDbType.VarChar ,100,hdn_Beneficiary.Value),
                objDAL.MakeInParams("@Beneficiary_Mobile",SqlDbType.VarChar ,10,hdn_BeneficiaryMobile.Value),
                objDAL.MakeInParams("@Bank_Ref_No",SqlDbType.VarChar ,50,""),
                objDAL.MakeInParams("@Total_Amount",SqlDbType.Decimal  ,0,Util.String2Decimal(hdn_Total.Value)),
                objDAL.MakeInParams("@PaidThrough",SqlDbType.Int,0,Util.String2Int (rdl_PayThrough.SelectedValue)),
                objDAL.MakeInParams("@PaidThrough_BankLedgerId",SqlDbType.Int,0,Bank_Ledger_ID),
                objDAL.MakeInParams("@Narration",SqlDbType.VarChar ,500,txt_Remarks.Text),
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML),
                objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.FA_Opr_Varai_Payment_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
            string _Msg;
            _Msg = "Saved SuccessFully";
            lblErrors.Text = _Msg;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    private bool Allow_To_Save()
    {
        bool ATS = false;

        if (dtpVoucherDate.SelectedDate > UserManager.getUserParam().TodaysDate)
        {
            lblErrors.Text = "Booking Date Should Be Less Than Or Equal To Todays Date.";
            ScriptManager.SetFocus(dtpVoucherDate);
        }
        else if (dtpVoucherDate.SelectedDate < UserManager.getUserParam().StartDate || dtpVoucherDate.SelectedDate > UserManager.getUserParam().EndDate)
        {
            lblErrors.Text = "Voucher Date should be in current Financial Date";
            ScriptManager.SetFocus(dtpVoucherDate);
        }

        else if (txtVarnar.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please Select Varnar";
            ScriptManager.SetFocus(txtVarnar);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Payment By";
            ScriptManager.SetFocus(ddlPayType);
        }
        else if (Util.String2Int(ddlPayType.SelectedValue) == 1 && (hdn_AccountNo.Value.Trim() == string.Empty || hdn_IFSCCode.Value.Trim() == string.Empty || hdn_BankName.Value.Trim() == string.Empty))
        {
            lblErrors.Text = "Please Select Payment By";
            ScriptManager.SetFocus(ddlPayType);
        }

        else if (Util.String2Decimal(hdn_Total.Value) <= 0)
        {
            lblErrors.Text = "Total Cannot Be Zero";
            ScriptManager.SetFocus(dg_Commodity);
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }



    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Packing = (DropDownList)(e.Item.FindControl("ddl_Packing"));

                txt_LRNo = (TextBox)(e.Item.FindControl("txt_LRNo"));
                txt_Qty = (TextBox)(e.Item.FindControl("txt_Qty"));
                txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));


                BindPacking = Session_PackingDdl;

                txt_Qty.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_QtyRate('" + txt_Qty.ClientID + "','" + txt_Rate.ClientID
                + "','" + txt_Amount.ClientID + "');");

                txt_Rate.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_QtyRate('" + txt_Qty.ClientID + "','" + txt_Rate.ClientID
                + "','" + txt_Amount.ClientID + "');");


                txt_VehicleNo = (TextBox)(e.Item.FindControl("txt_VehicleNo"));
                hdn_VehicleId = (HiddenField)(e.Item.FindControl("hdn_VehicleId"));
                lst_VehicleNo = (ListBox)(e.Item.FindControl("lst_VehicleNo"));

                txt_VehicleNo.Attributes.Add("onkeyup", "Search_txtSearch(event," + txt_VehicleNo.ClientID + ",'" + lst_VehicleNo.ClientID + "','Vehicle',3)");
                txt_VehicleNo.Attributes.Add("onfocus", "On_Focus(" + txt_VehicleNo.ClientID + "," + lst_VehicleNo.ClientID + ")");
                txt_VehicleNo.Attributes.Add("onkeydown", "return on_keydown(event," + txt_VehicleNo.ClientID + "," + lst_VehicleNo.ClientID + ")");
                lst_VehicleNo.Attributes.Add("onfocus", "listboxonfocus(" + txt_VehicleNo.ClientID + ")");
                txt_VehicleNo.Attributes.Add("onblur", "On_txtLostFocus('" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "','" + hdn_VehicleId.ClientID + "');" + "txtbox_onlostfocus(this);");

                lst_VehicleNo.Style.Add("visibility", "hidden");

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {

                DataRow DR = null;
                DataTable dt = Session_Grid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];
                BindPacking = Session_PackingDdl;
                ddl_Packing.SelectedValue = DR["Packing_ID"].ToString();

                hdn_VehicleId.Value = DR["Vehicle_Id"].ToString();
                txt_VehicleNo.Text = DR["Vehicle_No"].ToString();
                txt_LRNo.Text = DR["LRNo"].ToString();
                txt_Qty.Text = DR["Qty"].ToString();
                txt_Rate.Text = DR["Rate"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();

                txt_Qty.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_QtyRate('" + txt_Qty.ClientID + "','" + txt_Rate.ClientID
                + "','" + txt_Amount.ClientID + "');");

                txt_Rate.Attributes.Add("onblur", "txtbox_onlostfocus(this); Onblur_QtyRate('" + txt_Qty.ClientID + "','" + txt_Rate.ClientID
                + "','" + txt_Amount.ClientID + "');");


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


            if (Session_Grid.Compute("Sum(Amount)", "").ToString() != "")
            {
                Total = Util.String2Decimal(Session_Grid.Compute("Sum(Amount)", "").ToString());
            }
            else
            {
                Total = 0;
            }
        }

    }
    protected void dg_Commodity_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Commodity_Dataset(source, e);
            if (Allow_To_Save_Grid == true)
            {
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;

                Bind_dg_Commodity();
            }
        }
        catch (ConstraintException)
        {
            lblErrors.Text = "Duplicate Vehicle, LRNo & Packing Type";
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
                _dtColumnPrimaryKey = new DataColumn[3];
                _dtColumnPrimaryKey[0] = objDT.Columns["Vehicle_Id"];
                _dtColumnPrimaryKey[1] = objDT.Columns["LRNo"];
                _dtColumnPrimaryKey[2] = objDT.Columns["Packing_Type"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save_Grid == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1;
                    dg_Commodity.ShowFooter = true;

                }
                //TotalRate = TotalRate;
            }
            catch (ConstraintException)
            {
                lblErrors.Text = "Duplicate VehicleNo, LRNo & Packing Type";
            }
        }
    }


    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save_Grid = false;
        lblErrors.Text = "";


        decimal Qty = txt_Qty.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Qty.Text.Trim());

        decimal Rate = txt_Rate.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Rate.Text.Trim());

        decimal Amount = txt_Amount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text.Trim());

        if (txt_VehicleNo.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Vehicle No.";
            ScriptManager.SetFocus(txt_VehicleNo);
        }
        else if (Util.String2Int(hdn_VehicleId.Value) <= 0)
        {
            lblErrors.Text = "Please Enter Vehicle No.";
            ScriptManager.SetFocus(txt_VehicleNo);
        }

        else if (txt_LRNo.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter LR No.";
            ScriptManager.SetFocus(txt_LRNo);
        }
        else if (Qty > 0 && Util.String2Int(ddl_Packing.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Packing.";
            ScriptManager.SetFocus(ddl_Packing);
        }
        else if (Qty > 0 && Amount <= 0)
        {
            lblErrors.Text = "Please Enter Amount";
            ScriptManager.SetFocus(txt_Amount);
        }
        else
        {
            Allow_To_Save_Grid = true;
        }

        return Allow_To_Save_Grid;
    }

    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    {
        hdn_VehicleId = (HiddenField)(e.Item.FindControl("hdn_VehicleId"));
        txt_VehicleNo = (TextBox)(e.Item.FindControl("txt_VehicleNo"));
        txt_LRNo = (TextBox)(e.Item.FindControl("txt_LRNo"));
        txt_Qty = (TextBox)(e.Item.FindControl("txt_Qty"));
        ddl_Packing = (DropDownList)(e.Item.FindControl("ddl_Packing"));
        txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));


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


            dr["Vehicle_Id"] = Util.String2Int(hdn_VehicleId.Value);
            dr["Vehicle_No"] = txt_VehicleNo.Text;
            dr["LRNo"] = txt_LRNo.Text;
            dr["Qty"] = txt_Qty.Text.Trim() == string.Empty ? "0" : txt_Qty.Text.Trim();
            dr["Packing_Id"] = ddl_Packing.SelectedValue;
            dr["Packing_Type"] = Util.String2Int(ddl_Packing.SelectedValue) == 0 ? "" : ddl_Packing.SelectedItem.Text;
            dr["Rate"] = txt_Rate.Text.Trim() == string.Empty ? "0" : txt_Rate.Text.Trim();
            dr["Amount"] = txt_Amount.Text.Trim() == string.Empty ? "0" : txt_Amount.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_Grid.Rows.Add(dr);
            }

            Total = Util.String2Decimal(Session_Grid.Compute("Sum(Amount)", "").ToString());

        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddlPayType.SelectedValue) == 1)
        {
            tr_BeneficiaryMobile.Visible = false;
            tr_AccountNo.Visible = true;
            tr_Blank2.Visible = true;
            tr_IFSCCode.Visible = true;
            tr_Blank2.Visible = true;
            tr_BeneficiaryBank.Visible = true;
            tr_Blank3.Visible = true;
 

        }
        else
        {
            tr_BeneficiaryMobile.Visible = true;
            tr_AccountNo.Visible = false;
            tr_Blank2.Visible = false;
            tr_IFSCCode.Visible = false;
            tr_Blank2.Visible = false;
            tr_BeneficiaryBank.Visible = false;
            tr_Blank3.Visible = false;
        }
    }


    private void FillVarnarPackingDetails()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Varnar_ID", SqlDbType.Int, 0, Convert.ToInt32(hdn_Varnarld.Value)) };

        objDAL.RunProc("EC_Mst_Varnar_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            ddlPayType.SelectedValue = objDR["Pref_Pay_Type_ID"].ToString();

            if (Util.String2Int(ddlPayType.SelectedValue) == 1)
            {
                tr_BeneficiaryMobile.Visible = false;
                tr_AccountNo.Visible = true;
                tr_Blank2.Visible = true;
                tr_IFSCCode.Visible = true;
                tr_Blank2.Visible = true;
                tr_BeneficiaryBank.Visible = true;
                tr_Blank3.Visible = true;


            }
            else
            {
                tr_BeneficiaryMobile.Visible = true;
                tr_AccountNo.Visible = false;
                tr_Blank2.Visible = false;
                tr_IFSCCode.Visible = false;
                tr_Blank2.Visible = false;
                tr_BeneficiaryBank.Visible = false;
                tr_Blank3.Visible = false;
            }

        }

        Session_PackingGrid = objDS.Tables[1];

        Bind_dg_Grid();

    }

    private void Bind_dg_Grid()
    {
        dg_Grid.DataSource = Session_PackingGrid;
        dg_Grid.DataBind();

    }

    public DataTable Session_PackingGrid
    {
        get { return StateManager.GetState<DataTable>("PackingGrid"); }
        set { StateManager.SaveState("PackingGrid", value); }
    }

    protected void btn_hidden_Click(object sender, EventArgs e)
    {
        FillVarnarPackingDetails();
        SetLinks();
    }


    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0, KeyId)};

        objDAL.RunProc("FA_Opr_Varai_Payment_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lbl_VoucherNoValue.Text = objDR["Voucher_No"].ToString();
            VoucherDate  = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
            hdn_Varnarld.Value = objDR["Varnar_Id"].ToString();
            txtVarnar.Text = objDR["Varnar_Name"].ToString();
            ddlPayType.SelectedValue = objDR["Pay_Type_ID"].ToString();
            lbl_Total.Text = objDR["Total_Amount"].ToString();
            hdn_Total.Value = objDR["Total_Amount"].ToString();
            txt_Remarks.Text = objDR["Narration"].ToString();

            rdl_PayThrough.SelectedValue = objDR["PaidThrough"].ToString();

            Bank_Ledger_ID = objDR["PaidThrough_BankLedgerId"].ToString();

            lbl_MobileNo.Text = objDR["Mobile_No"].ToString();
            hdn_MobileNo.Value = objDR["Mobile_No"].ToString();
            lbl_AccountNo.Text = objDR["Account_No"].ToString();
            hdn_AccountNo.Value = objDR["Account_No"].ToString();
            lbl_IFSCCode.Text =  objDR["IFSC_Code"].ToString();
            hdn_IFSCCode.Value = objDR["IFSC_Code"].ToString();
            lbl_BankName.Text = objDR["Bank_Name"].ToString();
            hdn_BankName.Value = objDR["Bank_Name"].ToString();

            lbl_Beneficiary.Text = objDR["Beneficiary_Name"].ToString();
            hdn_Beneficiary.Value = objDR["Beneficiary_Name"].ToString();

            lbl_BeneficiaryMobile.Text = objDR["Beneficiary_Mobile"].ToString();
            hdn_BeneficiaryMobile.Value = objDR["Beneficiary_Mobile"].ToString();

            if (rdl_PayThrough.SelectedValue == "1")
            {
                tr_Bank.Style.Add("display", "block");
            }
            else
            {
                tr_Bank.Style.Add("display", "none");
            }



        }


        //Session_PackingGrid = objDS.Tables[1];

        //Bind_dg_Grid();

    }


    private void SetLinks()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        fRights = uObj.getForm_Rights(356);
        bool can_add = fRights.canAdd();
        bool can_edit = fRights.canEdit();

        lbtn_AddVarnar.Visible = false;
        
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdn_Varnar_Path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(356).AddUrl + "&Call_From=VaraiPayment";

            lbtn_AddVarnar.Visible = true;
        }
        else
        {
            hdn_Varnar_Path.Value = "";
        }

        lbtn_EditVarnar.Visible = false;
        if (can_edit == true)
        {
            hdn_EncreptedVarnarId.Value = Util.EncryptInteger(Util.String2Int(hdn_Varnarld.Value));

            StateManager.SaveState("QueryString", "2");
            hdn_Varnar_Path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(356).EditUrl + "&Id=" + hdn_EncreptedVarnarId.Value  + "&Call_From=VaraiPayment";

            lbtn_EditVarnar.Visible = true;
        }
        else
        {
            hdn_Varnar_Path.Value = "";
        }

    }
}
