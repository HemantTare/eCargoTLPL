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
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using System.Data.SqlClient;
using Raj.EC;
using Raj.EC.ControlsView;
using System.Text;


public partial class Finance_Reports_WucDailyCashBook : System.Web.UI.UserControl,IDailyCashBookView
{
    #region ClassVariables
    DailyCashBookPresenter objDailyCashBookPresenter;

    public int Ledger_ID;
    private Boolean IsConsol;
    private String HierarchyCode;
    private int MainId;
    Label lbl_LedgerName, lbl_LedgerAmount;
    string queryStr = "";

    string Crypt;

    #endregion
                            
    #region ControlsValue
    public int Ledger_Id
    {
        get { return Ledger_ID; }
    }

    public DateTime StartDate
    {
        get { return WucGridSearch1.StartDate; }
        set { WucGridSearch1.StartDate = value; }
    }
    public DateTime EndDate
    {
        get { return WucGridSearch1.EndDate; }
        set { WucGridSearch1.EndDate = value; }
    }

    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }

    public String Hierarchy_Code
    {
        get { return HierarchyCode; }
        set { HierarchyCode = value; }
    }

    public int Main_Id
    {
        get { return MainId; }
        set { MainId = value; }
    }

    public string Closing_Balance_Credit
    {
        set
        {
            if (value == "0.000")
            {
                lbl_Closing_Balance_Credit.Text = "";

            }
            else
            {
                lbl_Closing_Balance_Debit.Visible = false;
                lbl_Closing_Balance_Credit.Visible = true;
                lbl_Closing_Balance_Credit.Text = value + " Cr";
            }
        }
    }
    public string Closing_Balance_Debit
    {
        set
        {
            if (value == "0.000")
            {
                lbl_Closing_Balance_Debit.Text = "";
            }
            else
            {
                lbl_Closing_Balance_Credit.Visible = false;
                lbl_Closing_Balance_Debit.Visible = true;
                lbl_Closing_Balance_Debit.Text = value + " Dr";
            }
        }
    }

    public string Opening_Balance_Credit
    {
        set
        {
            if (value == "0.000")
            {
                lbl_opening_Balance_Credit.Text = "";

            }
            else
            {
                lbl_opening_Balance_Debit.Visible = false;
                lbl_Closing_Balance_Credit.Visible = true;
                lbl_opening_Balance_Credit.Text = value + " Cr";
            }

        }
    }
    public string Opening_Balance_Debit
    {
        set
        {
            if (value == "0.000")
            {
                lbl_opening_Balance_Debit.Text = "";

            }
            else
            {
                lbl_opening_Balance_Credit.Visible = false;
                lbl_Closing_Balance_Debit.Visible = true;
                lbl_opening_Balance_Debit.Text = value + "Dr";
            }
        }
    }
    public decimal Current_Total_Credit
    {
        set
        {
            //if (value > 0)
            //{
                lbl_Current_Total_Credit.Text = Math.Abs(value).ToString() + " Cr";
            //}
            //else
            //{
            //    lbl_Current_Total_Credit.Text = "";
            //}
        }
    }

    public decimal Current_Total_Debit
    {
        set
        {
            //if (value < 0)
            //{
                lbl_Current_Total_Debit.Text = Math.Abs(value).ToString() + " Dr";
            //}
            //else
            //{
            //    lbl_Current_Total_Debit.Text = "";                
            //}
        }
    }
    public DataSet LV_DS
    {
        get { return StateManager.GetState<DataSet>("LV_DS"); }
        set { StateManager.SaveState("LV_DS", value); }
    }

    public DataSet SessionDailyCashBook
    {
        get { return StateManager.GetState<DataSet>("Ledger_Voucher"); }
        set { StateManager.SaveState("Ledger_Voucher", value); }
    }


   
    #endregion

    #region ControlsBind
    public DataSet BindDailyCashBookGrid
    {
        set
        {
            dgLedgerVoucher.DataSource = value;
            Session["DS"] = value;            
            dgLedgerVoucher.DataBind();


        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {

        return true;

    }

    public string errorMessage 
    { 
        set { lbl_Error.Text = value; } 
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    private bool Is_Expand
    {
        get
        {
            return ViewState["Is_Expand"] != null ? Convert.ToBoolean(ViewState["Is_Expand"]) : false;
        }
        set { ViewState["Is_Expand"] = value; }
    }

     

    #endregion
    #region OtherMethods
    private string _makePath(int flag)
    {

        Common _objCommon = new Common();
        StringBuilder _path = new StringBuilder(_objCommon.getBaseURL());
        if (flag == 1)
        {
            _path.Append("/Finance/Reports/frm_Bank_Reco_Statement.aspx");
        }
        else 
        if (flag == 2)
        {
            _path.Append("/Finance/Reports/frm_Bank_Reco.aspx");
        }
        else if (flag == 3)
        {
            _path.Append("/Finance/Reports/frm_Bank_Reco_ExcelImport.aspx");
        }

        _path.Append("?Id=" + Request.QueryString["Id"]);
        _path.Append("&Name=" + Request.QueryString["Name"]);
        _path.Append("&Hierarchy_Code=" + Request.QueryString["Hierarchy_Code"]);
        _path.Append("&Main_Id=" + Request.QueryString["Main_Id"]);
        _path.Append("&StartDate=" +  StartDate.ToString());
        _path.Append("&EndDate=" + EndDate.ToString());
        _path.Append("&Is_Consol=" + Request.QueryString["Is_Consol"]);

       
        return _path.ToString();
    }

    
    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["StartDate"] = null;
        Session["EndDate"] = null;

        Is_Consol = WucHierarchyFiltration1.Is_Consol;
        Hierarchy_Code = Convert.ToString(WucHierarchyFiltration1.HierarchyCode);
        Main_Id = Convert.ToInt32(WucHierarchyFiltration1.Main_Id);

        Ledger_ID = 1;
        lbl_Ledger_Name.Text = "Petty Cash";
        DataSet ds = new DataSet();

        int BranchID;
        string BranchName;

        Crypt = Request.QueryString["Branch_id"];
        BranchID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        if (!IsPostBack)
        {
           
            if (BranchID > 0)
            {
                Crypt = Request.QueryString["Branch_Name"];
                BranchName = ClassLibraryMVP.Util.DecryptToString(Crypt);

                lbl_Branch_Name.Text = BranchName;
                lbl_Branch_Name.Visible = true;
                WucHierarchyFiltration1.Visible = false;
                Hierarchy_Code = "BO";
                Main_Id = BranchID;
            }
            else
            {
                lbl_Branch_Name.Text = "";
                lbl_Branch_Name.Visible = false;
                WucHierarchyFiltration1.Visible = true;
            }

            WucGridSearch1.StartDate = Convert.ToDateTime(System.DateTime.Now.Date);
            WucGridSearch1.EndDate = Convert.ToDateTime(System.DateTime.Now.Date);
            StartDate = Convert.ToDateTime(System.DateTime.Now.Date);
            EndDate = Convert.ToDateTime(System.DateTime.Now.Date);
            WucGridSearch1.SetComSepColumnName = "Date=Date,Particulars=Particulars,Voucher Type=Voucher_Type,Voucher No.=Voucher_no,Debit=Debit,Credit=Credit";
            Is_Expand = false;
            WucGridSearch1.ChangePeriodText = "  Show Report  ";

        }
        else
        {
            if (BranchID > 0)
            {
                Hierarchy_Code = "BO";
                Main_Id = BranchID;
            }
        }



        objDailyCashBookPresenter = new DailyCashBookPresenter(this, IsPostBack);

        WucGridSearch1.SetDataGrid = dgLedgerVoucher;

        if (Session["Ledger_Voucher"] != null)
        {

            ds.Tables.Add(SessionDailyCashBook.Tables[0].Copy());
            WucGridSearch1.SetDataSet = ds;
        }

        WucGridSearch1.btnChangedPeriod = new EventHandler(FillOnDateChange);

        HttpContext.Current.Session["LV_DS"] = SessionDailyCashBook;
        HttpContext.Current.Session["St_Dt"] = StartDate.ToString("dd-MMM-yyyy");//lbl_Start_Date.Text;
        HttpContext.Current.Session["End_Dt"] = EndDate.ToString("dd-MMM-yyyy");//lbl_End_Date.Text;
        HttpContext.Current.Session["Ledger_Name"] = lbl_Ledger_Name.Text;



        Session["Temp_Date"] = null;

    }
    #endregion


    #region GridEvents
    protected void dgLedgerVoucher_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        int Voucher_Id, Document_Id;
         LinkButton lnk_View;
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");

            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");

            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='GRIDALTERNATEROWCSS';");//GRIDALTERNATEROWCSS
            }


            if (e.Item.Cells[0].Text == "&nbsp;")
            {
                Voucher_Id = 0;
            }
            else
            {
                Voucher_Id = Convert.ToInt32(e.Item.Cells[0].Text);
            }
            if (e.Item.Cells[1].Text == "&nbsp;")
            {
                Document_Id = 0;
            }
            else
            {
                Document_Id = Convert.ToInt32(e.Item.Cells[1].Text);
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lnk_View = (LinkButton)e.Item.FindControl("lbtn_View");
                //int ID = Util.String2Int(SessionDailyCashBookGrid.Tables[0].Rows[e.Item.ItemIndex]["Voucher_Id"].ToString());
                int ID = Util.String2Int(e.Item.Cells[0].Text.Trim());
                string Path = Util.GetBaseURL() + "/Finance/VoucherView/FrmVoucher.aspx?Id=" + Util.EncryptInteger(ID);
                lnk_View.Attributes.Add("onclick", "return Open_Popup_Window('" + Path + "')");

                DataGrid dg_VoucherDetails = (DataGrid)e.Item.FindControl("dg_VoucherDetails");
                //HtmlGenericControl br_grid = (HtmlGenericControl)e.Item.FindControl("br_grid");

                if (Is_Expand)
                {
                    DataView Dv = new DataView(SessionDailyCashBook.Tables[2], "Voucher_Id=" + Voucher_Id.ToString(), "", DataViewRowState.CurrentRows);
                    if (Dv.Count > 1)
                    {
                        dg_VoucherDetails.Visible = true;
                        dg_VoucherDetails.DataSource = Dv;
                        dg_VoucherDetails.DataBind();

                        lnk_View.Text = "(as per details)";
                        lnk_View.BackColor = System.Drawing.Color.PeachPuff;
                    }
                    else 
                    {
                        dg_VoucherDetails.Visible = false;
                    }
                }
                else { 
                       dg_VoucherDetails.Visible =false;
                      }
            }


            //lbl_LedgerName = (Label)e.Item.FindControl("lbl_LedgerName");
            //lbl_LedgerAmount = (Label)e.Item.FindControl("lbl_LedgerAmount");

            ////tbl_Details.Style.Add("visibility", "hidden");
            //DataRow DrArr = SessionDailyCashBookGrid.Tables[2].Select("Voucher_Id=" + Voucher_Id.ToString())[0];

            //lbl_LedgerName.Text =DrArr["Ledger_Name"].ToString();
            //lbl_LedgerAmount.Text = DrArr["Ledger_Amount"].ToString();
        }


    }

    protected void dgLedgerVoucher_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgLedgerVoucher.CurrentPageIndex = e.NewPageIndex;
        BindDailyCashBookGrid = WucGridSearch1.GetFilterDs();
    }
    #endregion

    public void FillOnDateChange(object sender, EventArgs e)
    {
        objDailyCashBookPresenter.initValues();
    }

    //private DataRow GetVoucherDetailsRow(int voucher_Id)
    //{
    //   DataRow[] DrArr=SessionDailyCashBookGrid.Tables[0].Select("Voucher_Id=" = voucher_Id.ToString());
    //   return DrArr[0];
    //}


    private void SeExpandFlag()
    { 
        if (Is_Expand)
        {
          
            Is_Expand = false;
        }
        else
        {
            
            Is_Expand = true;
        }

    }
    protected void Imgbtn_Expand_Click(object sender, EventArgs e)
    {
        SeExpandFlag();

        WucGridSearch1.SearchRecord();
    }
    protected void btn_Show_Preview_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session["LV_DS"] = SessionDailyCashBook;
        HttpContext.Current.Session["St_Dt"] = StartDate.ToString("dd-MMM-yyyy");//lbl_Start_Date.Text;
        HttpContext.Current.Session["End_Dt"] = EndDate.ToString("dd-MMM-yyyy");//lbl_End_Date.Text;
        HttpContext.Current.Session["Ledger_Name"] = lbl_Ledger_Name.Text;

        StringBuilder path = new StringBuilder(Util.GetBaseURL());
        path.Append("/");
        path.Append("Finance/Reports/Rpt_Viewer/frm_Ledger_Voucher_View.aspx");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Page", "ViewReport('" + path + "');", true);
    }

    //protected void btn_Show_Click(object sender, EventArgs e)
    //{

    //    if (validateUI())
    //    {

    //        queryStr = "&IsConsolidated= " + WucHierarchyFiltration1.Is_Consol + "" + "&Hierarchy_Code=" + WucHierarchyFiltration1.HierarchyCode + "" + "&Main_Id=" + WucHierarchyFiltration1.Main_Id + "" + "&StartDate=" + Util.EncryptString(WucGridSearch1.StartDate.ToString()) + "" + "&EndDate=" + Util.EncryptString(WucGridSearch1.EndDate.ToString()) + "&Division_Id=" + WucHierarchyFiltration1.DivisionID + "";
    //        //string viewUrl = Util.GetBaseURL();
    //        //viewUrl = viewUrl + "/";
    //        //viewUrl = viewUrl + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + queryStr;
    //        //Response.Redirect(viewUrl);
             
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

}
