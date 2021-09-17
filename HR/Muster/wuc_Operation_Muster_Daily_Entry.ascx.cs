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
using ClassLibraryMVP.General;
using Raj.eCargo.Operation.Muster.Presenter;
using Raj.eCargo.Operation.Muster.View;
//using Raj.eCargo.Init;
using ClassLibraryMVP;

public partial class Operations_Muster_wuc_Operation_Muster_Daily_Entry : System.Web.UI.UserControl,IOperationMusterDailyEntryView
{
    //string Hierarchy_Code;
    //int Main_Id;
    //int Is_VTrans;
    int Employee_ID;
    int pagesize=20;
    int row = 0;
    int ID;
    PagedDataSource pgds = new PagedDataSource();
    DataTable dtTable = new DataTable();
    private DataSet _DatasetMusterDaily = new DataSet();
    private static int days = DateTime.Today.Day;
    private int dayPrevious1 = days - 1;
    private int dayPrevious2 = days - 2;

    private int MonthCount;
    private DataSet _ds = new DataSet();
    private DataTable dt = new DataTable();
    private bool check_value ;
    DataRow dr;

    public string Hierarchy_Code
    {
        get { return (Request.QueryString["Hierarchy_Code"]); }
    }
    public int Main_Id
    {
        get { return Convert.ToInt32(Request.QueryString["Main_Id"]); }
    }
    public int Division_Id
    {
        get {
            return Convert.ToInt32(Request.QueryString["PayDivId"]); ;
            }
    }
    public DataTable Bind_MusterEntryDaily
    {

        set
        {
            Rpt_MusterDaily.DataSource = value;
            //  SessionMusterDaily = value;
            Rpt_MusterDaily.DataBind();

        }
    }
     
    public DataSet SessionMusterDaily
    {
        get
        {
            //return Calculate_Muster_DS();
            return (DataSet)Session["MusterEntryDaily"];
        }
        set
        {

            Session["MusterEntryDaily"] = value;

        }
    }

    public int SessionRow
    {
        get
        {
            //return Calculate_Muster_DS();
            return (int)Session["SessionRow"];
        }
        set
        {

            Session["SessionRow"] = value;

        }
    }


    public DataSet SessionCalculatedDS
    {
        get { return Calculate_Muster_DS(); }
        set
        {

            Session["CalculatedDS"] = value;

        }
    }
         
    public string errorMessage
    { set { lbl_error.Text = value; } }


    public bool check
    {
        get { return check_value; }
        set { check_value = value; }
    
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public bool validateUI()
    {
        return true;

    }

    OperationMusterDailyEntryPresenter _ObjDailyEntryPresenter;
    protected void Page_Load(object sender, EventArgs e)
    {
       //  Param.ValidateSession();
        _ObjDailyEntryPresenter = new OperationMusterDailyEntryPresenter(this, IsPostBack);
      
        if (!IsPostBack)
        {
         //   CheckUser();
            SetValuesForGrid();
            FillEmployeeDDL();
        }

      

        if (hdn_check.Value == "1")
        {

            if (validateUI() == true)
            {
                hdn_check.Value = "0";
                _ObjDailyEntryPresenter.Save();
                btn_Save.Text = "Save";

                if (check == true)
                {
                    string AlertScript = "<script language='javascript'>" + "alert('Saved Sucessfully');</script>";
                    System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(Page), "AlertScript", AlertScript, false);
                    check = false;
                }
                txt_Search.Text = "".Trim();
                FillEmployeeDDL();
                ddl_Employee_Name.SelectedValue ="0";
                SetValuesForGrid();
                
            }
        }
    }
    public void FillEmployeeDDL()
    {
        ddl_Employee_Name.DataSource = SessionMusterDaily;
        ddl_Employee_Name.DataTextField = "Emp_Code";
        ddl_Employee_Name.DataValueField = "Employee_Id";
        ddl_Employee_Name.DataBind();
        ddl_Employee_Name.Items.Insert(0, new ListItem("All Employee", "0"));
    }
    public void setDataset()
    {
        _ds = (DataSet)Session["MusterEntryDaily"];

        if (_ds != null)
        {
            if (_ds.Tables[1].Rows.Count > 0)
            {
                int ColumnCount, RowCount;
                ColumnCount = _ds.Tables[1].Columns.Count - 1;
                RowCount = _ds.Tables[1].Rows.Count;


                for (int j = 0; j <= RowCount; j++)
                {
                    if (j == 0)
                    {
                        dt.Columns.Add("Description");
                    }
                    else
                    {
                        dt.Columns.Add("d" + j.ToString());
                    }
                }

                dr = dt.NewRow();
                dt.Rows.Add(dr);

                for (int Cj = 0; Cj <= RowCount; Cj++)
                {

                    if (Cj == 0)
                    {
                        dt.Rows[0][Cj] = "Days";
                    }
                    else
                    {
                        dt.Rows[0][Cj] = _ds.Tables[1].Rows[Cj - 1][1];
                    }

                }

                _ds.Tables.Add(dt);

            }
            SessionMusterDaily = _ds;
            if (_ds.Tables[0].Rows.Count > 0)
            {
               // Bind_MusterEntryDaily = SessionMusterDaily.Tables[0];
                Paging_Repeater();
            }
            else
            {

                Bind_MusterEntryDaily = Manipulate_DS();

            }

        }
    }

    private DataTable Manipulate_DS()
    {
        DataTable dt = new DataTable();

        dr = dt.NewRow();

        dt.Columns.Add("Employee_ID");
        dt.Columns.Add("Employee_Name");
        dt.Columns.Add("d1");
        dt.Columns.Add("d2");
        dt.Columns.Add("d3");
        dt.Columns.Add("d4");
        dt.Columns.Add("d5");
        dt.Columns.Add("d6");
        dt.Columns.Add("d7");
        dt.Columns.Add("d8");
        dt.Columns.Add("d9");
        dt.Columns.Add("d10");
        dt.Columns.Add("d11");
        dt.Columns.Add("d12");
        dt.Columns.Add("d13");
        dt.Columns.Add("d14");
        dt.Columns.Add("d15");
        dt.Columns.Add("d16");
        dt.Columns.Add("d17");
        dt.Columns.Add("d18");
        dt.Columns.Add("d19");
        dt.Columns.Add("d20");
        dt.Columns.Add("d21");
        dt.Columns.Add("d22");
        dt.Columns.Add("d23");
        dt.Columns.Add("d24");
        dt.Columns.Add("d25");
        dt.Columns.Add("d26");
        dt.Columns.Add("d27");
        dt.Columns.Add("d28");
        dt.Columns.Add("d29");
        dt.Columns.Add("d30");
        dt.Columns.Add("d31");

        return dt;
    }

    protected void Rpt_MusterDaily_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
       _ds = (DataSet)Session["MusterEntryDaily"];
       int rowNumber=0;
        int nmb=0;
        nmb=Convert.ToInt32(e.Item.ItemIndex);

        if (PageNumber > 0)
        {
            rowNumber = (pagesize * PageNumber) + nmb;
        }
        else
        {
            rowNumber=nmb;
        }

        
        if (_ds.Tables[1].Rows.Count > 0)
        {
            MonthCount = Convert.ToInt32(_ds.Tables[1].Rows[0]["MonthCount"]);
        }
        if (e.Item.ItemType == ListItemType.Header) // For Header
        {

            Label lbl_Current_Date = (Label)e.Item.FindControl("lbl_Current_Date");
            Label lbl_Current_Day = (Label)e.Item.FindControl("lbl_Current_Day");
            HtmlTableCell TD_MainDay_Header = (HtmlTableCell)e.Item.FindControl("TD_MainDay_Header_" + days);

            lbl_Current_Date.Text = (days).ToString();
            lbl_Current_Day.Text = _ds.Tables[2].Rows[0]["d" + days].ToString();

            TD_MainDay_Header.Visible = false;


            for (int i = 1; i <= 31; i++)
            {
                if (i != days)
                {
                    if (i <= MonthCount)
                    {
                        Label lbl_d = (Label)e.Item.FindControl("lbl_d" + i);
                        lbl_d.Text = _ds.Tables[2].Rows[0]["d" + i].ToString();
                    }
                }
                if (i > MonthCount)
                {
                    HtmlTableCell TD_MainDay_Header1 = (HtmlTableCell)e.Item.FindControl("TD_MainDay_Header_" + i);
                    TD_MainDay_Header1.Visible = false;

                }
            }


        }


        if (e.Item.ItemIndex != -1)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) // For items
            {
                Label lbl_Employee_Name = (Label)e.Item.FindControl("lbl_Employee_Name");
                TextBox txt_currentDay = (TextBox)(e.Item.FindControl("txt_currentDay"));
                TextBox txt_CurrentOtHrs = (TextBox)(e.Item.FindControl("txt_CurrentOtHrs"));
                TextBox txt_Current_Ext_Hrs = (TextBox)(e.Item.FindControl("txt_Current_Ext_Hrs"));
                HtmlTableCell TD_MainDay = (HtmlTableCell)e.Item.FindControl("TD_MainDay_" + days);

                if (Util.String2Int(ddl_Employee_Name.SelectedValue) <= 0)
                {
                    txt_currentDay.Text = _ds.Tables[0].Rows[rowNumber]["d" + days].ToString();
                    txt_CurrentOtHrs.Text = _ds.Tables[0].Rows[rowNumber]["ot_" + days].ToString();
                    txt_Current_Ext_Hrs.Text = _ds.Tables[0].Rows[rowNumber]["ExtHrs_" + days].ToString();
                }
                else
                {
                    txt_currentDay.Text = _ds.Tables[0].Rows[row]["d" + days].ToString();
                    txt_CurrentOtHrs.Text = _ds.Tables[0].Rows[row]["ot_" + days].ToString();
                    txt_Current_Ext_Hrs.Text = _ds.Tables[0].Rows[row]["ExtHrs_" + days].ToString();
                }
               

                TD_MainDay.Visible = false;


                for (int i = 1; i <= 31; i++)
                {
                    TextBox txt_Day = (TextBox)(e.Item.FindControl("txt_d" + i));
                    if (Util.String2Int(ddl_Employee_Name.SelectedValue) <= 0)
                    {
                        Employee_ID = Convert.ToInt32(_ds.Tables[0].Rows[rowNumber]["Employee_ID"]);
                    }
                    string Employee_Name = _ds.Tables[0].Rows[rowNumber]["Employee_Name"].ToString();
                    Employee_Name=Employee_Name.Replace("'", "");

                    string Employee_Code = _ds.Tables[0].Rows[rowNumber]["Employee_Code"].ToString();

                    string Ext_Hrs = _ds.Tables[0].Rows[rowNumber]["ExtHrs_" + i].ToString();
                    string Ot_Hrs = _ds.Tables[0].Rows[rowNumber]["ot_" + i].ToString();

                    //     txt_Day.Attributes.Add("onclick", " return Open_Popup_Window('" + Employee_Name + "','" + Employee_Code + "','" + txt_Day.Text + "','" + Ot_Hrs + "','" + Ext_Hrs + "','" + i.ToString() + "')");

                    txt_Day.Attributes.Add("onclick", " return Open_Popup_Window('" + Employee_Name + "','" + Employee_Code + "','" + txt_Day.Text + "','" + Ot_Hrs + "','" + Ext_Hrs + "','" + i.ToString() + "','" +Employee_ID.ToString() + "')");
       

                    txt_Day.Attributes.Add("onmouseover", "this.className='COMMONHOVER1';");
                    txt_Day.Attributes.Add("onmouseout", "this.className='COMMONHOUT1';");

          
                    if (i > MonthCount)
                    {
                        HtmlTableCell TD_MainDay1 = (HtmlTableCell)e.Item.FindControl("TD_MainDay_" + i);
                        TD_MainDay1.Visible = false;
                    }
                }

            }
        }

    }

    public DataSet Calculate_Muster_DS()
    {
        _DatasetMusterDaily = (DataSet)Session["MusterEntryDaily"];
        int rowNumber = 0;
        int nmb = 0;
      
       // int CountIMax = _DatasetMusterDaily.Tables[0].Rows.Count;
        int CountIMax = Rpt_MusterDaily.Items.Count;
        if (CountIMax > 0)
        {

            for (int i = 0; i < CountIMax; i++)
            {
                nmb = i;
                if (PageNumber > 0)
                {
                    rowNumber = (pagesize * PageNumber) + nmb;
                }
                else
                {
                    rowNumber = nmb;
                }

                if (_DatasetMusterDaily.Tables[1].Rows.Count > 1 && CountIMax == 1)
                {
                    row = SessionRow;
                    rowNumber=row;                   
                }


                TextBox txt_currentDay = (TextBox)Rpt_MusterDaily.Items[i].FindControl("txt_currentDay");
                TextBox txt_CurrentOtHrs = (TextBox)Rpt_MusterDaily.Items[i].FindControl("txt_CurrentOtHrs");
                TextBox txt_Current_Ext_Hrs = (TextBox)Rpt_MusterDaily.Items[i].FindControl("txt_Current_Ext_Hrs");

                TextBox txt_DayPrevious1 = (TextBox)Rpt_MusterDaily.Items[i].FindControl("txt_d" + dayPrevious1);
                TextBox txt_DayPrevious2 = (TextBox)Rpt_MusterDaily.Items[i].FindControl("txt_d" + dayPrevious2);

                
                string String_OtHrs = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["ot_TotalHours"].ToString();
                string String_ExtHrs = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Tot_ExtHrs"].ToString();
                string String_Present_days = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Present_days"].ToString();
                string String_Paid_days = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Paid_days"].ToString();
                string String_Absent_Days = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Absent_Days"].ToString();
                string String_Present_Hours = _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Present_Hours"].ToString();


                decimal DsOtHrs = Util.String2Decimal(String_OtHrs);
                decimal DsExtHrs = Util.String2Decimal(String_ExtHrs);
                decimal DsPresent_days = Util.String2Decimal(String_Present_days);
                decimal DsPaid_days = Util.String2Decimal(String_Paid_days);
                int DsAbsent_Days = Util.String2Int(String_Absent_Days);
                int DsPresent_Hours = Util.String2Int(String_Present_Hours);
               
                if (txt_currentDay.Text.Trim() == "A")
                {
                    txt_CurrentOtHrs.Text = "0".Trim();
                    txt_Current_Ext_Hrs.Text = "0".Trim();
                    if (_DatasetMusterDaily.Tables[0].Rows[i]["d" + days].ToString() != "A")
                    {
                        DsAbsent_Days = DsAbsent_Days + 1;
                        DsPresent_days = DsPresent_days - 1;
                        DsPaid_days = DsPaid_days - 1;
                        DsPresent_Hours = DsPresent_Hours - 8;
                        if (txt_DayPrevious2.Text.Trim() == "A")
                        {
                            if (txt_DayPrevious1.Text.Trim() != "P" || txt_DayPrevious1.Text.Trim() != "H")
                            {
                                txt_DayPrevious1.Text = "A".Trim();
                                DsAbsent_Days = DsAbsent_Days + 1;
                                DsPresent_days = DsPresent_days - 1;
                                DsPaid_days = DsPaid_days - 1;
                                DsPresent_Hours = DsPresent_Hours - 8;
                            }
                        }
                    }

                }
                if (txt_currentDay.Text.Trim() == "P" || txt_currentDay.Text.Trim() == "P")
                {
                    if (_DatasetMusterDaily.Tables[0].Rows[i]["d" + days].ToString() == "A")
                    {
                        DsAbsent_Days = DsAbsent_Days - 1;
                        DsPresent_days = DsPresent_days + 1;
                        DsPaid_days = DsPaid_days + 1;
                        DsPresent_Hours = DsPresent_Hours + 8;
                        //if (txt_DayPrevious2.Text.Trim() == "A")
                        //{
                        //    if (txt_DayPrevious1.Text.Trim() != "P" || txt_DayPrevious1.Text.Trim() != "H")
                        //    {
                        //        //txt_DayPrevious1.Text = "P".Trim();
                        //        DsAbsent_Days = DsAbsent_Days - 1;
                        //        DsPresent_days = DsPresent_days + 1;
                        //        DsPaid_days = DsPaid_days + 1;
                        //        DsPresent_Hours = DsPresent_Hours + 8;
                        //    }
                        //}
                    }
                }

                decimal OtHrs = Util.String2Decimal(txt_CurrentOtHrs.Text);
                decimal ExtHrs = Util.String2Decimal(txt_Current_Ext_Hrs.Text);

                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["d" + days] = txt_currentDay.Text;
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["ot_" + days] = txt_CurrentOtHrs.Text;
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["ExtHrs_" + days] = txt_Current_Ext_Hrs.Text;

                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["ot_TotalHours"] = Convert.ToString(DsOtHrs + OtHrs);
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Tot_ExtHrs"] = Convert.ToString(DsExtHrs + ExtHrs);

                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Present_days"] = Convert.ToString(DsPresent_days);
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Paid_days"] = Convert.ToString(DsPaid_days);
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Absent_Days"] = Convert.ToString(DsAbsent_Days);
                _DatasetMusterDaily.Tables[0].Rows[rowNumber]["Present_Hours"] = Convert.ToString(DsPresent_Hours);

            }
        }
        _DatasetMusterDaily.Tables[0].AcceptChanges();
        SessionMusterDaily = _DatasetMusterDaily;
        return _DatasetMusterDaily;
    }

    public void SetValuesForGrid()
    {

        _ObjDailyEntryPresenter.GetValues(Hierarchy_Code, Main_Id, Division_Id);
        setDataset();
       
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //if (validateUI() == true)
        //{
           
        //    _ObjDailyEntryPresenter.Save();
        //    btn_Save.Text = "Save";
           
        //    if (check == true)
        //    {
        //        //string AlertScript = "<script language='javascript'>" + "alert('Saved Sucessfully');</script>";
        //        //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "AlertScript", AlertScript, false);
        //        errorMessage = "Saved Sucessfully";
        //    }
        //}
    }
 
    /////////////////////////////////////////////////////
    private void Paging_Repeater()
    {
       
        DataSet ds = new DataSet();
        ds=SessionMusterDaily;
      //  PagedDataSource pgds = new PagedDataSource();

        int i = 0;
        {
            pgds.DataSource = ds.Tables[0].DefaultView;
            pgds.AllowPaging = true;
            pgds.PageSize = pagesize;
            pgds.CurrentPageIndex = PageNumber;
            if (pgds.PageCount > 1)
            {
                ArrayList pages = new ArrayList();
                for (i = 0; i <= pgds.PageCount - 1; i++)
                {
                    pages.Add(i + 1);
                }
                rptPages.Visible = true;
                rptPages.DataSource = pages;
                rptPages.DataBind();
            }
            else
            {
                rptPages.Visible = false;
            }
        }

        Rpt_MusterDaily.DataSource = pgds;
        Rpt_MusterDaily.DataBind();
        
    }
    private int PageNumber
    {
        get
        {
            if (Convert.ToInt32(ViewState["PageNumber"]) != 0)
            {
                return Convert.ToInt32(ViewState["PageNumber"]);
            }
            else
            {
                return 0;
            }
        }
        set { ViewState["PageNumber"] = value; }
    }

    protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Calculate_Muster_DS();
        PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
       
        Paging_Repeater();
       
       

    }
    //=========================================ADDED ON 8-JAN-2009================================
    protected void ddl_Employee_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_error.Text = "";
   
        FilteringDT();
       
    
    }

    public void FilteringDT()
    {
        Calculate_Muster_DS();
        if (Util.String2Int(ddl_Employee_Name.SelectedValue) <= 0)
        {
            Paging_Repeater();
        }
        else
        {
            DataSet ds = SessionMusterDaily;
            DataView dv = ds.Tables[0].DefaultView;
            DataTable dt = new DataTable();
            dv.RowFilter = "Employee_Id =" + Util.String2Int(ddl_Employee_Name.SelectedValue);
            dt = dv.ToTable();
            Employee_ID = Util.String2Int(ddl_Employee_Name.SelectedValue);
            row = Convert.ToInt32(dt.Rows[0]["SrNo"])-1;
            SessionRow = row;
            Rpt_MusterDaily.DataSource = dt;
            Rpt_MusterDaily.DataBind();
            dv.RowFilter = "";
            if (Util.String2Int(ddl_Employee_Name.SelectedValue) == 0)
            {
                rptPages.Visible = true;
            }
            else
            {
                rptPages.Visible = false;
            }
        }
       
    }
    public void Search()
    {
        if (txt_Search.Text != "")
        {
            MakeDataView();
        }
        else
        {
            Filter_Employee_Details();
        }
    }
    public void MakeDataView()
    {
        DataSet ds = new DataSet();
        ds = SessionMusterDaily;
        DataView view = new DataView();
        view.Table = ds.Tables[0];
        view.AllowNew = true;
        view.RowFilter = ddl_Search.SelectedValue + " Like '" + txt_Search.Text + "*'";
        view.RowStateFilter = DataViewRowState.CurrentRows;
        ddl_Employee_Name.DataSource = view;
        ddl_Employee_Name.DataBind();
        ddl_Employee_Name.Items.Add(new ListItem("All Employee", "0"));
        dtTable = view.ToTable();

        if (dtTable.Rows.Count == 0)
        {
            lbl_records.Text = "No Records Found !";
        }

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
       
        Search();
        FilteringDT();
        PageNumber = 0;
      
    }
    protected void txt_Search_TextChanged(object sender, EventArgs e)
    {
        Search();
        FilteringDT();
        PageNumber = 0;
    }
    protected void ddl_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
          Filter_Employee_Details();
    }
    public void Filter_Employee_Details()
    {
        ddl_Employee_Name.DataSource = SessionMusterDaily;
        ddl_Employee_Name.DataTextField = ddl_Search.SelectedValue;
        ddl_Employee_Name.DataValueField = "Employee_Id";
        ddl_Employee_Name.DataBind();
        ddl_Employee_Name.Items.Insert(0, new ListItem("All Employee", "0"));
    }
    //=========================================ADDED ON 8-JAN-2009 END================================
}
