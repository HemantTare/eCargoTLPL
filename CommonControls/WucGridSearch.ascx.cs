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
using System.Text.RegularExpressions;
using System.Text;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
public partial class CommonControls_WucGridSearch : System.Web.UI.UserControl
{
    #region ClassVariables
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    private string _ColumnName;
    private DataGrid _dg;
    private DataSet _ds=new DataSet();
    DataTable _dt = new DataTable();
    public EventHandler btnChangedPeriod;
    Label lbl_Errors=new Label();
    public String Calendar_Img_Path;
    public string _setAttribute;
    #endregion
    #region ControlsValue
    public DateTime StartDate
    {
        set
        {
            Picker.SelectedDate = value; 
        }
        get
        {
            return Picker.SelectedDate;
        }
    } 
    public DateTime EndDate
    {
        set
        {
            Picker1.SelectedDate = value; 
        }
        get
        {
            return Picker1.SelectedDate;
        }
    }
    public bool VisibleChangePeriod
    {
        set
        {
            //tbl_DateChange.Style.Add("display", value);
            tbl_DateChange.Visible = value;
        }
    }
    public bool VisibleGridSearch
    {
        set
        {
            //tbl_GridSearch.Style.Add("display", value);
            tbl_GridSearch.Visible = value;
        }
    }
    public bool VisibleGotoPage
    {
        set
        {
  //          td_BtnGo.Style.Add("display", value);
//            td_txtPageNo.Style.Add("display", value);
            td_BtnGo.Visible=value;
            td_txtPageNo.Visible=value;

            
        }
    }
    public string ChangePeriodText
    {
        set
        {
            btn_ChangePeriod.Text = value;
        }
    }
    public string SetComSepColumnName
    {
        set
        {
            
            _ColumnName = value;
            BindDDLSearch=_dt;
        }
        get
        {
            return _ColumnName;
        }
    }
    public DataGrid SetDataGrid
    {
        set
        {
            _dg = value;
        }
    }
    public DataTable BindDataGrid
    {
        set
        {
            _dg.DataSource = value;
            _dg.DataBind();
        }
    }
    public DataSet SetDataSet
    {
        set
        {
            _ds = value;
        }
    }
    public DataTable BindDDLSearch
    {
        set
        {         
            ddl_Search.DataSource = FillDDLSearch();
            ddl_Search.DataTextField = "Search_Text";
            ddl_Search.DataValueField = "Search_Value";
            ddl_Search.DataBind();
        }
    }

        public DataSet GetFilterDs()
    {

        DataSet ds_view = new DataSet();
        DataView view = new DataView();
        DataView objdv = new DataView();
        System.Type Datatype;

        StringBuilder Search_Text = new StringBuilder();
        if (txt_Search.Text.Trim() != "")
        {
            Search_Text.Append(ddl_Search.SelectedValue);

          
            view.Table = _ds.Tables[0];
            Datatype = view.Table.Columns[ddl_Search.SelectedValue].DataType;

            if (Datatype.Name == "Decimal" || Datatype.Name == "Integer" || Datatype.Name == "Int64" || Datatype.Name == "Int32" || Datatype.Name == "DateTime")
            {
                Search_Text.Append(" = '");
                Search_Text.Append(Regex.Replace(txt_Search.Text.Trim(), "'", ""));
                Search_Text.Append("'");
                view.RowFilter = Convert.ToString(Search_Text);
            }
            else
            {
                Search_Text.Append(" Like '");
                Search_Text.Append(Regex.Replace(txt_Search.Text.Trim(), "'", ""));
                Search_Text.Append("*'");
                view.RowFilter = Convert.ToString(Search_Text);
            }
            view.RowStateFilter = DataViewRowState.CurrentRows;

            if (view.Count <= 0)
            {
                string Script = "<script language='javascript'> alert('Record Not Found');</script>";
                Page.ClientScript.RegisterStartupScript(typeof(String), "Alert", Script);
            }

            ds_view.Tables.Add(view.ToTable());
        }
        else
        {
            ds_view = _ds;
        }

        return ds_view;
    }


    public void SearchRecord()
    {
        //DataSet ds_view = new DataSet();
        //DataView view = new DataView();
        //DataView objdv = new DataView();
        //System.Type Datatype;

        //StringBuilder Search_Text = new StringBuilder();
        //_dg.CurrentPageIndex = 0;
        //if (txt_Search.Text.Trim() != "")
        //{
        //    Search_Text.Append(ddl_Search.SelectedValue);

        //    ds_view = _ds;
        //    view.Table = ds_view.Tables[0];
        //    Datatype = view.Table.Columns[ddl_Search.SelectedValue].DataType;

        //    if (Datatype.Name == "Decimal" || Datatype.Name == "Integer" || Datatype.Name == "Int64" || Datatype.Name == "Int32" || Datatype.Name == "DateTime")
        //    {
        //        Search_Text.Append(" = '");
        //        Search_Text.Append(Regex.Replace(txt_Search.Text.Trim(), "'", ""));
        //        Search_Text.Append("'");
        //        view.RowFilter = Convert.ToString(Search_Text);
        //    }
        //    else
        //    {
        //        Search_Text.Append(" Like '");
        //        Search_Text.Append(Regex.Replace(txt_Search.Text.Trim(), "'", ""));
        //        Search_Text.Append("*'");
        //        view.RowFilter = Convert.ToString(Search_Text);
        //    }
        //    view.RowStateFilter = DataViewRowState.CurrentRows;          
        //    _dg.DataSource = view;

        //    if (view.Count <= 0)
        //    {
        //        string Script = "<script language='javascript'> alert('Record Not Found');</script>";
        //        Page.ClientScript.RegisterStartupScript(typeof(String), "Alert", Script);
        //    }

        //}
        //else
        //{
        //    _dg.DataSource = _ds;
        //}


        _dg.CurrentPageIndex = 0;
        _dg.DataSource = GetFilterDs();
        _dg.DataBind();
        _dg.CurrentPageIndex = 0;
     
    }
    private DataTable FillDDLSearch()
    {
        DataTable _dt = new DataTable();
        Char[] c = new char[1];
        c[0] = ',';
        string[] s = new string[200];
        Char[] _aliasCharSep = new char[1];
        _aliasCharSep[0] = '=';
        string[] _aliasColumn = new string[200];
        int i;
        string SearchTextName;

        DataRow _dr;
        _dt.Columns.Add("Search_Text");
        _dt.Columns.Add("Search_Value");

        s = SetComSepColumnName.Split(c);
        for (i = 0; i < s.Length; i++)
        {
            SearchTextName = s[i];
            _aliasColumn = SearchTextName.Split(_aliasCharSep);

            if (_aliasColumn.Length > 1)
            {
                SearchTextName = _aliasColumn[0];
                s[i] = _aliasColumn[1];             
            }
            if (SearchTextName.Contains("_"))
            {
                SearchTextName = SearchTextName.Replace("_", " ");
            }

            _dr = _dt.NewRow();
            _dr["Search_Text"] = SearchTextName;
            _dr["Search_Value"] = s[i];
            _dt.Rows.Add(_dr);            
        }
        return _dt;
    }
    #endregion

    #region OtherMethods

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {    
       
        String Base_Url;
        Base_Url = Util.GetBaseURL();
        Calendar_Img_Path = Base_Url + "/Images/btn_calendar.gif";
        Picker.Attributes.Add("Picker_OnSelectionChanged(picker)", "return SetDateToLable()");              
    }  
   
    #endregion
    protected void btn_ChangePeriod_Click(object sender, EventArgs e)
    {
        if (btnChangedPeriod != null)
            btnChangedPeriod(sender, e);  
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        SearchRecord();
        txt_Search.Focus();
    }


    protected void btn_Go_Click(object sender, EventArgs e)
    {
        int ds_goto;
        double ds_total;
        int total_pages;
        int pageIndex = 0;
        pageIndex = _dg.CurrentPageIndex;
        DataTable dt = new DataTable();
        //dt = (setdataset.tables[0]);
        
        dt = _ds.Tables[0];
        ds_total = dt.Rows.Count;

        //if (txt_PageNo.Text == "")
        //{
        //    ds_goto = 0;
        //}
        ds_goto = Convert.ToInt32(txt_PageNo.Text);
        _dg.CurrentPageIndex = ds_goto - 1;
        //_dg.CurrentPageIndex = 0;
        _dg.DataSource = _ds;
        total_pages = Convert.ToInt32(Math.Ceiling((ds_total) / (_dg.PageSize)));
        if (ds_goto > total_pages)
        {
            string Script = "<script language='javascript'> alert('Requested Page Does Not Exist');</script>";
            Page.ClientScript.RegisterStartupScript(typeof(String), "Alert", Script);
            txt_PageNo.Focus();
            _dg.CurrentPageIndex = pageIndex;
            //_dg.CurrentPageIndex = 0;
        }
        _dg.DataBind();
    
    }
}