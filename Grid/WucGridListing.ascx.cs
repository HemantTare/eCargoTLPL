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
using System.Text;
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using Raj.EC;
using System.Text.RegularExpressions;



public partial class Grid_WucGridListing : System.Web.UI.UserControl
{
    DataSet objDS = null;
    Common objCommon = new Common();  
    private int _count;
    Boolean Is_Consol = false;
    int LedgerGroupId;
    string Hierarchy_Code;
    int Main_Id;
    DateTime StartDate;
    DateTime EndDate;
    public string BaseUrl;

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    public int Mode
    {
        get { return Common.GetMode(); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
       
       
       Is_Consol = Convert.ToBoolean(Request.QueryString["IsConsolidated"]);
       Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
       Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
       LedgerGroupId = Convert.ToInt32(Request.QueryString["LedgerGroupId"]);


       Search.SearchClicked += new EventHandler(EventBindGrid);
      
      
       BaseUrl =Util.GetBaseURL();

     if (Session["StartDate"] == null)
       {
           StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
       }
       else
       {
           StartDate = StateManager.GetState<DateTime>("StartDate");
       }

       if (Session["EndDate"] == null)
       {
           EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
       }
       else
       {
           EndDate = StateManager.GetState<DateTime>("EndDate");
       }

     
        if (!Page.IsPostBack)
        {
            FillGridHeaders(dg_Grid);
            Search.FillCombo(dg_Grid);    

            hdn_Sort_Dir.Value = "ASC";
            hdn_Sort_Expression.Value = "Col1";       
         
            Session["objDS"] = null;
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);

            
        }
        Link.text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();    
         
      
  
    }    
       

    public void FillGridHeaders(DataGrid dg_Grid)
    {

        int i;
        DataTable objDT = new DataTable();
        DataSet objDS1=new DataSet();
        int TotCol;
        int ColCount = 10;

        objDT.Columns.Add("col1");
        objDT.Columns.Add("col2");
        objDT.Columns.Add("col3");
        objDT.Columns.Add("col4");
        objDT.Columns.Add("col5");
        objDT.Columns.Add("col6");
        objDT.Columns.Add("col7");
        objDT.Columns.Add("col8");
        objDT.Columns.Add("col9");
        objDT.Columns.Add("col10");

        if (MenuItemId == 79)
        {
            LedgerBook objLedgerBook = new LedgerBook();
            objDS = objLedgerBook.FillLedgerBookGridList(Is_Consol, Hierarchy_Code,Main_Id, StartDate, EndDate,LedgerGroupId);
            dg_Grid.Columns[2].Visible = false;
            dg_Grid.Columns[3].Visible = false;
        }
        else if (MenuItemId == 81)
        {
            //
        }
           
                TotCol = objDS.Tables[0].Columns.Count;

                for (int j = 0; j <= objDS.Tables[0].Columns.Count - 1; j++)
                {
                    dg_Grid.Columns[j].HeaderText = objDS.Tables[0].Columns[j].ColumnName;
                }
                for (i = 0; i <= objDS.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow objdr = objDT.NewRow();

                    for (int j = 0; j <= objDS.Tables[0].Columns.Count - 1; j++)
                    {
                        object[] itemArray = objdr.ItemArray;
                        itemArray[j] = objDS.Tables[0].Rows[i][j];
                        objdr.ItemArray = itemArray;

                    }
                    objDT.Rows.Add(objdr);
                }
                objDS1.Tables.Add(objDT);
                

                dg_Grid.Columns[0].Visible = false;

                for (i = TotCol + 1; i <= ColCount; i++)
                {
                    dg_Grid.Columns[i - 1].Visible = false;
                }
          
            ClassLibraryMVP.StateManager.SaveState("LedgerBookList", objDS1);
  

            if (IsPostBack)
            {
                MakeDataView();               

            }

            else
            {
                dg_Grid.DataSource = objDS1;
                dg_Grid.DataBind();
               

            }

         SetTotal(objDS1);
  
    }


    
    private void EventBindGrid(object source, EventArgs e)
    {    
      dg_Grid.CurrentPageIndex = 0;
      MakeDataView();
    }
    protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        MakeDataView();
    }

   
    public void MakeDataView()
    {

        DataSet ds_view = new DataSet();
        DataView view = new DataView();
        DataView objdv = new DataView();
        System.Type Datatype;

        if (ClassLibraryMVP.StateManager.IsValidSession("LedgerBookList"))
        {

            StringBuilder Search_Text = new StringBuilder("Col");
            Search_Text.Append(Search.GetComboValue());          
            ds_view = ClassLibraryMVP.StateManager.GetState<DataSet>("LedgerBookList");
            view.Table = ds_view.Tables[0];
            Datatype = view.Table.Columns["Col" + Search.GetComboValue()].DataType;

            if (Datatype.Name == "Integer" || Datatype.Name == "datetime")
            {
                Search_Text.Append(" = '");
                Search_Text.Append(Regex.Replace(Search.GetSearchText(), "'", ""));
                Search_Text.Append("'");
                view.RowFilter = Convert.ToString(Search_Text);
            }
            else
            {
                Search_Text.Append(" Like '");
                Search_Text.Append(Regex.Replace(Search.GetSearchText(), "'", ""));
                Search_Text.Append("*'");
                view.RowFilter = Convert.ToString(Search_Text);
            }
            view.RowStateFilter = DataViewRowState.CurrentRows;
            view.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

            dg_Grid.DataSource = view;
            dg_Grid.DataBind();
            Session["objdv"] = view;
            SetTotal(view);
           
           
        }
      
    }

    protected void Grid_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (Session["LedgerBookList"] != null)
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
              MakeDataView();
        }
    }
    public void SetTotal(object sender)
    {
        DataSet objDS1 = StateManager.GetState<DataSet>("LedgerBookList");
        DataView View =(DataView)Session["objdv"];
        Decimal AmtOpenBal = 0;
        Decimal AmtCloseBal = 0;
       
        if (sender == objDS1)
        {

            foreach (DataRow DR in objDS1.Tables[0].Rows)
            {
                AmtOpenBal = AmtOpenBal + Convert.ToDecimal(DR["Col3"]);
                AmtCloseBal = AmtCloseBal + Convert.ToDecimal(DR["Col4"]);
            }
        }
        else
        {
            foreach (DataRow DR in  View.ToTable().Rows)
            {
                AmtOpenBal = AmtOpenBal + Convert.ToDecimal(DR["Col3"]);
                AmtCloseBal = AmtCloseBal + Convert.ToDecimal(DR["Col4"]);
            }
        }
        if (AmtOpenBal > 0)
        {

            lbl_OpeningBalance.Text = String.Format(Math.Abs(AmtOpenBal).ToString(), "0.00") + " " + "Cr";
        }
        else
        {
            lbl_OpeningBalance.Text = String.Format(Math.Abs(AmtOpenBal).ToString(), "0.00") + " " + "Dr";

        }
        if (AmtCloseBal > 0)
        {
            lbl_ClosingBalance.Text = String.Format(Math.Abs(AmtCloseBal).ToString(), "0.00") + " " + "Cr";
        }
        else
        {
            lbl_ClosingBalance.Text = String.Format(Math.Abs(AmtCloseBal).ToString(), "0.00") + " " + "Dr";
        }

    }

    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        int Id;
        LinkButton lnk_Btn;
        string lnk_name;
        bool Is_Cancel = false;
        DataSet _objDs = null;
        DataSet LedgerBook=StateManager.GetState<DataSet>("LedgerBookList");      


        if (Session["LedgerBookList"] == null)
        {
            _count = 0;
        }
        else
        {
            _objDs = (DataSet)(Session["LedgerBookList"]);
            _count = StateManager.GetState<DataSet>("LedgerBookList").Tables[0].Rows.Count;
        }
        if (_count == 0)
        {
            Lbl_Total_Records.Text = "";
        }
        else
        {
            Lbl_Total_Records.Text = _count.ToString() + " Record(s) Found";
        }
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");

            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
            }

            Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_Id")).Text);
            lnk_name = ((Label)e.Item.Cells[1].FindControl("lbl_Name")).Text;

            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';this.style.cursor='hand'");

            if (MenuItemId == 79)
            {
                e.Item.Attributes.Add("onclick", "return Show_Ledger_Book('" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "' ,'" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "','" + Is_Consol + "','" + Hierarchy_Code + "','" + Main_Id + "','" + Id + "','" + lnk_name + "');");

            }
         
        }


    } 
}
