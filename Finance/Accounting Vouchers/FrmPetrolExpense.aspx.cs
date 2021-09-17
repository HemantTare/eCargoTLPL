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

public partial class Finance_Accounting_Vouchers_FrmPetrolExpense : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    string Mode = "0";
    private DataSet objDS;
    private DAL objDAL = new DAL();
    string _flag;

    DataTable objDT = new DataTable();
    DataRow dr;
    bool Allow_To_Save_Grid;

    TextBox txt_FromLoc, txt_ToLoc, txt_Amount;


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

    public string errorMessage
    {
        set { lblErrors.Text = value; }
    }


    public string Name
    {
        get { return txt_Name.Text; }
        set
        {
            txt_Name.Text = value;
        }
    }

    public string VehicleNo
    {
        get { return txt_VehicleNo.Text; }
        set
        {
            txt_VehicleNo.Text = value;
        }
    }

    public string Reason
    {
        get { return txt_Reason.Text; }
        set
        {
            txt_Reason.Text = value;
        }
    }

    public decimal Total
    {
        get { return Util.String2Decimal(lbl_TotalAmount.Text); }
        set
        {
            lbl_TotalAmount.Text = Util.Decimal2String(value);
        }
    }



    public string Remarks
    {
        get { return txt_Remarks.Text; }
        set
        {
            txt_Remarks.Text = value;
        }
    }

    public string PaidTo
    {
        get { return txt_PaidTo.Text; }
        set
        {
            txt_PaidTo.Text = value;
        }
    }

    public string PaidToMobile
    {
        get { return txt_PaidToMobile.Text; }
        set
        {
            txt_PaidToMobile.Text = value;
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

            if (KeyId <= 0)
            {
                Next_Voucher_Number();

                lbl_PaidBy.Text = UserManager.getUserParam().FirstName + " " + UserManager.getUserParam().LastName;
            }
            else
            {
                ReadValues();
            }

            fillGridDetails();
        }

        ScriptManager.SetFocus(txt_Name);
    }



    private void fillGridDetails()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, KeyId) };

        objDAL.RunProc("FA_Opr_Petrol_Expense_Details_Read", objSqlParam, ref objDS);

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



    private void Next_Voucher_Number()
    {
        VoucherNo = objComm.Get_Next_Number();
    }


    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, KeyId) };

        objDAL.RunProc("FA_Opr_Petrol_Expense_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lbl_VoucherNoValue.Text = objDR["Transaction_No"].ToString();
            VoucherDate = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
            Name = objDR["Name"].ToString();
            VehicleNo = objDR["VehicleNo"].ToString();
            Reason  = objDR["reason"].ToString();
            Total  = Util.String2Decimal(objDR["Total_Amount"].ToString());
            Remarks  = objDR["Remark"].ToString();
            PaidTo = objDR["Paid_To"].ToString();
            PaidToMobile = objDR["Paid_To_Mobile"].ToString();
            lbl_PaidBy.Text  = objDR["Paid_By"].ToString();
        }

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
                objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
                objDAL.MakeInParams("@Transaction_Id",SqlDbType.Int,0,KeyId),
                objDAL.MakeInParams("@Voucher_Date",SqlDbType.DateTime ,0,dtpVoucherDate.SelectedDate),
                objDAL.MakeInParams("@Name",SqlDbType.VarChar ,100,Name),
                objDAL.MakeInParams("@VehicleNo",SqlDbType.VarChar ,20,VehicleNo),
                objDAL.MakeInParams("@Reason",SqlDbType.VarChar ,100,Reason),
                objDAL.MakeInParams("@Total_Amount",SqlDbType.Decimal  ,0,Total),
                objDAL.MakeInParams("@Paid_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                objDAL.MakeInParams("@Paid_To",SqlDbType.VarChar ,100,PaidTo),
                objDAL.MakeInParams("@Paid_To_Mobile",SqlDbType.VarChar ,20,PaidToMobile),
                objDAL.MakeInParams("@Remark",SqlDbType.VarChar ,1000,txt_Remarks.Text),
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML),
                objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.FA_Opr_Petrol_Expense_Save", objSqlParam);

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
        else if (Name.Trim() == "")
        {
            lblErrors.Text = "Enter Name";
            ScriptManager.SetFocus(txt_Name);
        }
        else if (Reason.Trim() == "")
        {
            lblErrors.Text = "Enter Proper Reason";
            ScriptManager.SetFocus(txt_Reason);
        }
        else if (Total <= 0)
        {
            lblErrors.Text = "Total Amount Must Be Greater Than Zero";
            ScriptManager.SetFocus(txt_Name);
        }

        else if (VehicleNo.Trim() == "")
        {
            lblErrors.Text = "Enter Vehicle No.";
            ScriptManager.SetFocus(txt_VehicleNo);
        }
        else if (PaidToMobile.Trim().Length != 10)
        {
            lblErrors.Text = "Enter Proper Mobile No Of Paid To Whom";
            ScriptManager.SetFocus(txt_PaidToMobile);
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
                txt_FromLoc = (TextBox)(e.Item.FindControl("txt_FromLoc"));
                txt_ToLoc = (TextBox)(e.Item.FindControl("txt_ToLoc"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                ScriptManager.SetFocus(txt_FromLoc);

                DataRow DR = null;
                DataTable dt = Session_Grid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                txt_FromLoc.Text = DR["From_Location"].ToString();
                txt_ToLoc.Text = DR["To_Location"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();


            }


        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        lblErrors.Text = "";

        ScriptManager.SetFocus(txt_FromLoc);

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;


        Bind_dg_Commodity();
        lblErrors.Text = "";

        ScriptManager.SetFocus(txt_FromLoc);

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

        ScriptManager.SetFocus(txt_FromLoc);

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

                ScriptManager.SetFocus(txt_FromLoc);

            }
        }
        catch (ConstraintException)
        {
            lblErrors.Text = "Duplicate From & To Location";
            ScriptManager.SetFocus(txt_FromLoc);

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
                _dtColumnPrimaryKey[0] = objDT.Columns["From_Location"];
                _dtColumnPrimaryKey[1] = objDT.Columns["To_Location"];
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
                lblErrors.Text = "Duplicate From & To Location";
                ScriptManager.SetFocus(txt_FromLoc);

            }
        }
    }


    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save_Grid = false;
        lblErrors.Text = "";



        decimal Amount = txt_Amount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text.Trim());

        if (txt_FromLoc.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter From Location";
            ScriptManager.SetFocus(txt_FromLoc);
        }
        else if (txt_ToLoc.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter To Location";
            ScriptManager.SetFocus(txt_ToLoc);
        }

        else if (Amount <= 0)
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
        txt_FromLoc = (TextBox)(e.Item.FindControl("txt_FromLoc"));
        txt_ToLoc = (TextBox)(e.Item.FindControl("txt_ToLoc"));
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


            dr["From_Location"] = txt_FromLoc.Text;
            dr["To_Location"] = txt_ToLoc.Text;
            dr["Amount"] = txt_Amount.Text.Trim() == string.Empty ? "0" : txt_Amount.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_Grid.Rows.Add(dr);
            }

            Total = Util.String2Decimal(Session_Grid.Compute("Sum(Amount)", "").ToString());

        }
    }



}
