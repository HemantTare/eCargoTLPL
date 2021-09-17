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

public partial class Finance_Reports_WucDayBook : System.Web.UI.UserControl,IDayBookView
{
    #region ClassVariable
    DataSet ds = null;
    public string HierarchyCode;
    public Boolean IsConsol;
    public int MainId;
    int MenuItemId;
    DayBookPresenter objDayBookPresenter;    
    #endregion

    #region ControlsValue

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
    public string Hierarchy_Code
    {
        get { return HierarchyCode; }
        set { HierarchyCode = value; }
    }

    public int Main_Id
    {
        get { return MainId; }
        set { MainId = value; }
    }

    public Boolean Is_Consol
    {
        get { return IsConsol; }
        set { IsConsol = value; }
    }

    public string GetVoucherTypeID_XML
    {
        get { return StateManager.GetState<DataSet>("VoucherId").GetXml().ToLower();}
    }
    #endregion

    #region ControlsBind
    public DataSet BindDayBookGrid
    {
        set
        {

            DG_Monthly.DataSource = value;
            Session["DS"] = value;
            MakeView();
            DG_Monthly.CurrentPageIndex = 0;
            DG_Monthly.DataBind();

        }
        get { return (DataSet)Session["DS"]; }
    }

    private void BindGrid()
    {
       // DG_Monthly.CurrentPageIndex = 0;
        objDayBookPresenter.GetData();
    }
    #endregion

    #region IView Members

    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        bool _Is_Valid = true;   
        return _Is_Valid;
    }

    #endregion

    #region OtherMethod
    public void MakeView()
    {
        ds = (DataSet)Session["DS"];

        if (ds != null)
        {
            DataView dv = new DataView(ds.Tables[0]);
           // dv.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;
            DG_Monthly.DataSource = dv;
            //DG_Monthly.CurrentPageIndex = 0;
            DG_Monthly.DataBind();
        }
    }
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(PrepareDTForExportToExcel);
        
        Wuc_Export_To_Excel1.FileName = "Day Book";
        
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);

        string xml = GetVoucherTypeID_XML;

        if (!IsPostBack)
        {
             StartDate = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["StartDate"]));
             EndDate = Convert.ToDateTime(ClassLibraryMVP.Util.DecryptToString(Request.QueryString["EndDate"]));
             WucGridSearch1.SetComSepColumnName = "Date=Voucher_Date,Particulars=Particulars,Voucher Type=Voucher_Name,Voucher No.=Voucher_no,Debit Amount=Debit,Credit Amount=Credit";

        }
        DataSet ds = new DataSet();
        objDayBookPresenter = new DayBookPresenter(this,IsPostBack);

        WucGridSearch1.SetDataGrid = DG_Monthly;

        if (Session["DS"] != null)
        {

            ds.Tables.Add(BindDayBookGrid.Tables[0].Copy());
            WucGridSearch1.SetDataSet = ds;
        }

        WucGridSearch1.btnChangedPeriod = new EventHandler(FillOnDateChange);
       

    }

    #endregion

    #region GridEvents

    protected void DG_Monthly_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        hdn_Sort_Expression.Value = e.SortExpression;

        if (hdn_Sort_Dir.Value == "DESC")
        {
            hdn_Sort_Dir.Value = "ASC";
        }
        else
        {
            hdn_Sort_Dir.Value = "DESC";
        }
        MakeView();
    }

    protected void DG_Monthly_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DG_Monthly.CurrentPageIndex = e.NewPageIndex;
        MakeView();
       
    }

    protected void DG_Monthly_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        int Voucher_Id, Document_Id;
        int Id;
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';this.style.cursor='hand'");
            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTTB';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='GRIDALTERNATEROWCSS';");
            }
            Id = Convert.ToInt32(e.Item.Cells[0].Text);
            //Voucher_Id = Convert.ToInt32(e.Item.Cells[0].Text);
            string path = "../VoucherView/FrmVoucher.aspx?Id=" + Util.EncryptInteger(Id).ToString() + "";
            e.Item.Attributes.Add("onclick", "return Open_Popup_Window('" + path + "')");
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total_Debit, lbl_Total_Credit;

            lbl_Total_Debit = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Debit");
            lbl_Total_Credit = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Credit");

            lbl_Total_Debit.Text = Convert.ToString(ds.Tables[1].Rows[0][0]); //Convert.ToString(BindDayBookGrid.Tables[0].Compute("SUM(Debit)","")); 
            lbl_Total_Credit.Text = Convert.ToString(ds.Tables[1].Rows[0][1]); //Convert.ToString(ds.Tables[2].Rows[0][0]);
        }

    }

    //private StringBuilder BuildLink(int Voucher_Id)
    //{
    //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    //    StringBuilder ViewPath = new StringBuilder(ObjCommon.GetBaseURL()));
    //    if (Util.String2Bool(Param.getUserParam().Is_VT.ToString()) == true)
    //        ViewPath.Append("/Finance_VT/Accounting%20Vouchers/frm_Ledger_Voucher_View_VT.aspx?");
    //    else
    //        ViewPath.Append("/Finance/Accounting%20Vouchers/frm_Ledger_Voucher_View.aspx?");
    //    ViewPath.Append("&Voucher_Id=" + Util.EncryptInteger(Voucher_Id));

    //    return ViewPath;

    //}
    #endregion



    public void FillOnDateChange(object sender, EventArgs e)
    {
        objDayBookPresenter.GetData();
    }

    private void PrepareDTForExportToExcel(object sender, EventArgs e)
    {
        DataSet dsDayBook = new DataSet();
        dsDayBook = BindDayBookGrid.Copy(); 
        DataRow dr = dsDayBook.Tables[0].NewRow();
        dr["Particulars"] = "Total:";
        dr["Debit"] = dsDayBook.Tables[1].Rows[0][0];
        dr["Credit"] = dsDayBook.Tables[1].Rows[0][1];

        dsDayBook.Tables[0].Columns.Remove("Voucher_ID");
        dsDayBook.Tables[0].Columns.Remove("Voucher_Type_ID");
	dsDayBook.Tables[0].Columns.Remove("Particular");

        dsDayBook.Tables[0].Rows.Add(dr);
        Wuc_Export_To_Excel1.SessionExporttoExcel = dsDayBook.Tables[0]; 
    }
}
