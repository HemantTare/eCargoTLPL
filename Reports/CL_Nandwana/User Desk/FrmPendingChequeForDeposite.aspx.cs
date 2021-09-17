using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_CL_Nandwana_UserDesk_FrmPendingChequeForDeposite : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal NoOfCheques;
    private int Branch_Id, Area_Id, Region_Id;
    Raj.EC.Common objComm = new Raj.EC.Common();

    DropDownList ddl_BankLedger;

    DataRow dr;

    DataTable objDT = new DataTable();

    private DAL objDAL = new DAL();

    int ItemSelected;

    #endregion


    public DataTable SessionBindGrid
    {
        get { return StateManager.GetState<DataTable>("BindGrid"); }
        set { StateManager.SaveState("BindGrid", value); }
    }

    public String DetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindGrid, "");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "Grid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    public DataTable Session_BankLedgerDdl
    {
        get { return StateManager.GetState<DataTable>("BankLedgerDdl"); }
        set { StateManager.SaveState("BankLedgerDdl", value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Branch_Id = Convert.ToInt32(Request.QueryString["Branch_ID"].ToString());
        Area_Id = Convert.ToInt32(Request.QueryString["Area_ID"].ToString());
        Region_Id = Convert.ToInt32(Request.QueryString["Region_ID"].ToString());

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        if (IsPostBack == false)
        {
            fillBankLedger();

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);

        }

    }

    protected void dg_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        LinkButton lbtn_ChequeNo, lbtn_Time, lbtn_Amount;

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());


            CheckBox chk_SentToHO = (CheckBox)e.Item.FindControl("chk_SentToHO");
            CheckBox chk_Deposited = (CheckBox)e.Item.FindControl("chk_Deposited");
            CheckBox chk_RecevdToHO = (CheckBox)e.Item.FindControl("chk_RecevdToHO");

            bool IsSentToHO, IsReceivedATHO;

            IsSentToHO = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "IsSentToHO").ToString());
            IsReceivedATHO = Util.String2Bool(DataBinder.Eval(e.Item.DataItem, "IsReceivedATHO").ToString());

            ddl_BankLedger = (DropDownList)(e.Item.FindControl("ddl_BankLedger"));
            BindBankLedger = Session_BankLedgerDdl;

            if (UserManager.getUserParam().HierarchyCode == "BO")
            {
                chk_SentToHO.Attributes.Add("onclick", "checkuncheck('" + chk_SentToHO.ClientID + "','" + chk_Deposited.ClientID + "','" + ddl_BankLedger.ClientID + "');" + "checkuncheckDeposited('" + chk_Deposited.ClientID + "','" + ddl_BankLedger.ClientID + "');");
                chk_Deposited.Attributes.Add("onclick", "checkuncheck('" + chk_Deposited.ClientID + "','" + chk_SentToHO.ClientID + "','" + ddl_BankLedger.ClientID + "');" + "checkuncheckDeposited('" + chk_Deposited.ClientID + "','" + ddl_BankLedger.ClientID + "');");

                chk_RecevdToHO.Enabled = false;

                if (IsSentToHO == true)
                {
                    chk_Deposited.Enabled = false;
                    chk_SentToHO.Enabled = false; 
                }
            }
            else if (UserManager.getUserParam().HierarchyCode == "HO" || UserManager.getUserParam().HierarchyCode == "AD")
            {
                chk_Deposited.Attributes.Add("onclick", "checkuncheckDeposited('" + chk_Deposited.ClientID + "','" + ddl_BankLedger.ClientID + "');");
            }

           
            if (UserManager.getUserParam().HierarchyCode != "BO" && IsSentToHO == false)
            {
                chk_Deposited.Enabled = false;
                chk_RecevdToHO.Enabled = false;
            }

            if (UserManager.getUserParam().HierarchyCode != "BO" && IsReceivedATHO == false )
            {
                chk_Deposited.Enabled = false;
            }

            if (UserManager.getUserParam().HierarchyCode != "BO")
            {
                chk_SentToHO.Enabled = false;

                if (IsReceivedATHO == true)
                {
                    chk_RecevdToHO.Enabled = false;
                }

            }

            lbtn_ChequeNo = (LinkButton)(e.Item.FindControl("lbtn_ChequeNo"));

            lbtn_Amount = (LinkButton)(e.Item.FindControl("lbtn_Amount"));

            lbtn_ChequeNo.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");

            lbtn_Amount.Attributes.Add("onclick", "return openVoucherWindow('" + Util.EncryptInteger(Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Voucher_Id").ToString())) + "')");


            lbtn_Time = (LinkButton)(e.Item.FindControl("lbtn_Time"));

            lbtn_Time.Attributes.Add("onclick", "return openDetailsWindow('" + DataBinder.Eval(e.Item.DataItem, "VoucherCreatedBy").ToString() + "','"
                + DataBinder.Eval(e.Item.DataItem, "ChqSentToHOOn").ToString() + "','"
                + DataBinder.Eval(e.Item.DataItem, "SentTime").ToString() + "','"
                + DataBinder.Eval(e.Item.DataItem, "ChqSentToHOBy").ToString() + "','"
                + DataBinder.Eval(e.Item.DataItem, "ChqRecdAtHOOn").ToString() + "','"
                + DataBinder.Eval(e.Item.DataItem, "RecdTime").ToString() + "','" 
                + DataBinder.Eval(e.Item.DataItem, "ChqRecdAtHOBy").ToString()  + "')");


            string IsDueForDeposite = DataBinder.Eval(e.Item.DataItem, "IsDueForDeposite").ToString();

            if (IsDueForDeposite == "True")
            {
                e.Item.BackColor = System.Drawing.Color.Gold;
                ddl_BankLedger.BackColor = System.Drawing.Color.Gold;
            }
            else
            {
                chk_Deposited.Enabled = false;
                ddl_BankLedger.BackColor = System.Drawing.Color.White;
            }

            DataRow DR = null;
            DataTable dt = SessionBindGrid;//for grid topic
            DR = dt.Rows[e.Item.ItemIndex];
            ddl_BankLedger.SelectedValue = DR["DepositedIn"].ToString();


        }

    }

    protected void dg_Details_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }


    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Details.CurrentPageIndex;
        int grid_PageSize = dg_Details.PageSize;


        SqlParameter[] objSqlParam = {
            objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,Branch_Id),
            objDAL.MakeInParams("@Area_Id",SqlDbType.Int,0,Area_Id),
            objDAL.MakeInParams("@Region_Id",SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
        objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)};

        objDAL.RunProc("COM_UserDesk_Get_Pending_Cheques_For_Deposite", objSqlParam, ref ds);

        dg_Details.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();


        SessionBindGrid = ds.Tables[0];

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Details, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (Branch_Id > 0)
        {
            dg_Details.Columns[0].Visible = false;
        }

        if (Util.String2Int(TotalRecords) <= 0)
        {
            btn_Save.Visible = false;
        }

        ClearVariables();
    }


    private void calculate_griddetails()
    {
        ItemSelected = 0;

        CheckBox chk_SentToHO, chk_Deposited, chk_RecevdToHO;

        int i;
        if (dg_Details.Items.Count > 0)
        {
            objDT = SessionBindGrid;

            for (i = 0; i <= dg_Details.Items.Count - 1; i++)
            {
                chk_SentToHO = (CheckBox)dg_Details.Items[i].FindControl("chk_SentToHO");
                chk_Deposited = (CheckBox)dg_Details.Items[i].FindControl("chk_Deposited");
                chk_RecevdToHO = (CheckBox)dg_Details.Items[i].FindControl("chk_RecevdToHO");

                objDT.Rows[i]["IsSentToHo"] = chk_SentToHO.Checked;
                objDT.Rows[i]["IsDeposited"] = chk_Deposited.Checked;
                objDT.Rows[i]["IsReceivedATHO"] = chk_RecevdToHO.Checked;


                if (chk_SentToHO.Checked == true && chk_SentToHO.Enabled == true)
                {
                    ItemSelected = ItemSelected + 1;
                }

                if (chk_Deposited.Checked == true && chk_Deposited.Enabled == true)
                {
                    ItemSelected = ItemSelected + 1;
                }

                if (chk_RecevdToHO.Checked == true && chk_RecevdToHO.Enabled == true)
                {
                    ItemSelected = ItemSelected + 1;
                }

            }

            SessionBindGrid = objDT;
        }
    }

    public bool validateUI()
    {
        bool _isValid = false;

        if (ItemSelected <= 0)
            lbl_Error.Text = "Please Select Atleast One Record";
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        calculate_griddetails();
        if (validateUI())
        {
            btn_Save.Enabled = false;

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@DepositedByHierarchyCode", SqlDbType.VarChar,5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@DepositedByMainId", SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@DepositedByUserID", SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,DetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_Pending_Cheque_Deposite_Save", objSqlParam);

            if (Convert.ToInt32(objSqlParam[0].Value) == 0)
            {
                ClearVariables();
                SessionBindGrid = null;
                Session_BankLedgerDdl = null;
                Response.Write("<script language='javascript'>{self.close()}</script>");
            }
        }
    }


    public void ClearVariables()
    {
        ds = null;
    }

    private void fillBankLedger()
    {
        objDAL.RunProc("[dbo].[FA_Fill_Bank_Ledger]", ref ds);
        Session_BankLedgerDdl = ds.Tables[0];

    }

    public DataTable BindBankLedger
    {
        set { Set_Common_DDL(ddl_BankLedger, "Ledger_Name", "Ledger_ID", value, true); }
    }

    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        //if (Is_ZeroInex)
        //    DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    
    protected void ddl_BankLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        int  i;
    
        DataTable DT = SessionBindGrid;
        DT.TableName = "grid";
        DataTable DT1 = DT.Copy();

        for (i = 0; i <= dg_Details.Items.Count - 1; i++)
        {
            ddl_BankLedger = (DropDownList)dg_Details.Items[i].FindControl("ddl_BankLedger");

            DT1.Rows[i]["DepositedIn"] = ddl_BankLedger.SelectedValue;

        }

        SessionBindGrid = DT1.Copy(); 
    }

}
