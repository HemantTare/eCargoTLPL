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

public partial class Finance_Reports_WucAgeing : System.Web.UI.UserControl,IAgeingView
{
    #region ClassVariables
    DataSet ds = new DataSet();
    System.Type t;
    public DateTime startdate;
    public DateTime enddate;
    public string HierarchyCode;
    public Boolean IsConsol;
    public Boolean Is_Condensed;
    public int MainId;
    private int Index; 
    private int To_Days_Value;
    AgeingPresenter objAgeingPresenter;

    #endregion

    #region ControlsValue

    public DateTime StartDate
    {
        get { return startdate; }
        set { startdate = value; }
    }

    public DateTime EndDate
    {
        get { return enddate; }
        set { enddate = value; }
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

    public Boolean IsCondensed
    {
        get { return Is_Condensed; }
        set { Is_Condensed = value; }
    }

    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }


    public int Mode
    {
        get { return Common.GetMode(); }
    }

    public int LedgerGroupId
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["LedgerGroupId"]);

        }
    }

    
    public int LedgerId
    {

        get { return Convert.ToInt32(Request.QueryString["LedgerId"]); }
    }

    public string LedgerName
    {

        get { return Convert.ToString(Request.QueryString["LedgerName"]); }
        //set { lbl_LedgerName.Text = value; }
    }

   
    public DataSet SessionAgeingGrid
    {
        get { return StateManager.GetState<DataSet>("AgeingGrid"); }
        set { StateManager.SaveState("AgeingGrid", value); }
    }
    #endregion

    #region ControlsBind
    public DataSet BindGrid
    {
        set
        {
           dg_Ageing.DataSource=value;
           dg_Ageing.DataBind();
        }


    }


    #endregion


    #region IView Members

    public string errorMessage
    {
         set { lbl_Errors.Text = value; }
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

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        Is_Consol = Convert.ToBoolean(Request.QueryString["Is_Consol"]);
        Hierarchy_Code = Convert.ToString(Request.QueryString["Hierarchy_Code"]);
        Main_Id = Convert.ToInt32(Request.QueryString["Main_Id"]);
        IsCondensed = Convert.ToBoolean(Request.QueryString["IsCondensed"]);
        //LedgerName = Convert.ToString(Request.QueryString["LedgerName"]);
        Session["IsCondensed"] = IsCondensed;



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
      
        objAgeingPresenter = new AgeingPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            BindGrid = SessionAgeingGrid;
        }
    }

    #endregion

    #region GridEvents
      protected void dg_Ageing_ItemDataBound(object sender, DataGridItemEventArgs e)
    {  

        Label lbl_From_Days = (Label)(e.Item.FindControl("lbl_From_Days"));
        TextBox txt_To_Days = (TextBox)(e.Item.FindControl("txt_To_Days"));
       
       if (e.Item.ItemType == ListItemType.Item || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
            if (e.Item.ItemIndex == 0)
            {
                lbtn_Delete.Enabled = false;   
            }

        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            lbl_From_Days.Text = hdn_Generate_From_Value.Value;
        }
        if (e.Item.ItemType == ListItemType.EditItem)
        {
        }
    
    }
    protected void dg_Ageing_ItemCommand(object source, DataGridCommandEventArgs e)
    {
   
        if (e.CommandName == "ADD")
        {
            DataSet ds = SessionAgeingGrid;
            Label lbl_From_Days = (Label)(e.Item.FindControl("lbl_From_Days"));
            TextBox txt_To_Days = (TextBox)(e.Item.FindControl("txt_To_Days"));

            if (txt_To_Days.Text.Trim() == "")
            {
                errorMessage = "Please Enter To Days Value";
                return;
            }

            if (Util.String2Int(txt_To_Days.Text) <= Util.String2Int(hdn_Generate_From_Value.Value))
            {
                errorMessage = "Please Enter To Days Value Greater than From Days Value";
                return;
            }

            lbl_From_Days.Text = hdn_Generate_From_Value.Value;
          
            DataRow dr = ds.Tables["Table"].NewRow();
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                dr["Sr_No"] = Util.String2Int( ds.Tables["Table"].Rows[ds.Tables["Table"].Rows.Count - 1]["Sr_No"].ToString() )+ 1;
            }           
            else 
            {
                dr["Sr_No"] = "0";
            }
            
            dr["From_Days"]=lbl_From_Days.Text;
            dr["To_Days"] = txt_To_Days.Text;
           ds.Tables["Table"].Rows.Add(dr);
           hdn_Generate_From_Value.Value = txt_To_Days.Text;
           SessionAgeingGrid = ds;
           BindGrid = SessionAgeingGrid;  
        }       

        if (e.CommandName == "Delete")
        {
            if (e.Item.ItemIndex != -1)
            {
                DataSet ds = SessionAgeingGrid;
                if (ds.Tables["Table"].Rows.Count == 1)
                    hdn_Generate_From_Value.Value = "0";
                Index = e.Item.ItemIndex;
                Update_Dataset_Value_Delete();
                BindGrid = SessionAgeingGrid;    
            }
         } 

    }
    protected void dg_Ageing_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Ageing.EditItemIndex = -1;
        dg_Ageing.ShowFooter = true;
        BindGrid = SessionAgeingGrid;
    }
    protected void dg_Ageing_UpdateCommand(object source, DataGridCommandEventArgs e)
    {

        DataSet ds = SessionAgeingGrid;
        Label lbl_From_Days = (Label)(e.Item.FindControl("lbl_From_Days"));
        TextBox txt_To_Days = (TextBox)(e.Item.FindControl("txt_To_Days"));
        hdn_Generate_From_Value.Value = ds.Tables["Table"].Rows[e.Item.ItemIndex]["From_Days"].ToString();
        hdn_Old_Value.Value = ds.Tables["Table"].Rows[e.Item.ItemIndex]["To_Days"].ToString();
        Index = e.Item.ItemIndex;
        if (txt_To_Days.Text.Trim() == "")
        {
            errorMessage = "Please Enter To Days Value";
            return;
        }

        if (Util.String2Int(txt_To_Days.Text) <= Util.String2Int(hdn_Generate_From_Value.Value))
        {
            errorMessage = "Please Enter To Days Value Greater than From Days Value";
            return;
        }
            lbl_From_Days.Text = hdn_Generate_From_Value.Value;
            To_Days_Value = Util.String2Int(txt_To_Days.Text);
            ds.Tables["Table"].Rows[e.Item.ItemIndex]["From_Days"]=lbl_From_Days.Text;
            ds.Tables["Table"].Rows[e.Item.ItemIndex]["To_Days"] = txt_To_Days.Text;
            dg_Ageing.EditItemIndex = -1;
            dg_Ageing.ShowFooter = true;
            hdn_Generate_From_Value.Value = txt_To_Days.Text;
        
          hdn_Max_Value.Value = ds.Tables[0].Compute("Max(To_Days)", "Sr_No =" + Convert.ToString(Index + 1)).ToString();

          SessionAgeingGrid = ds;
          if (Util.String2Int(txt_To_Days.Text) < Util.String2Int(hdn_Max_Value.Value))//This is all according to Tally
          {
              //normal
              Update_Dataset_Value_Insert_Normal();
          }
          else
          {
              //double the values
              Update_Dataset_Value_Insert_Double();
          }

          BindGrid = SessionAgeingGrid;
    }
    protected void dg_Ageing_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Ageing.EditItemIndex = e.Item.ItemIndex;
        dg_Ageing.ShowFooter = false;
        BindGrid = SessionAgeingGrid;
        for (int i = 0; i < dg_Ageing.Items.Count; i++)
        {
            dg_Ageing.Items[i].Cells[6].Enabled = false;
        }
    }
    public void  Update_Dataset_Value_Insert_Double()
    {
        DataSet Ds = SessionAgeingGrid;
        if (Ds.Tables["Table"].Rows.Count > 1)
        {
            for (int i = Index+1; i < Ds.Tables["Table"].Rows.Count; i++)
            {
                Ds.Tables["Table"].Rows[i]["From_Days"] = To_Days_Value ;
                To_Days_Value = To_Days_Value * 2;
                Ds.Tables["Table"].Rows[i]["To_Days"] = To_Days_Value;              
            }
            hdn_Generate_From_Value.Value = Ds.Tables["Table"].Rows[Ds.Tables["Table"].Rows.Count - 1]["To_Days"].ToString();
        }
        SessionAgeingGrid = Ds;
    }
    public void Update_Dataset_Value_Insert_Normal()
    {
        DataSet Ds = SessionAgeingGrid;
        if (Ds.Tables["Table"].Rows.Count >= 1)
        {
               Ds.Tables["Table"].Rows[Index+1]["From_Days"] = To_Days_Value;       
               hdn_Generate_From_Value.Value = Ds.Tables["Table"].Rows[Ds.Tables["Table"].Rows.Count - 1]["To_Days"].ToString();
        }
        SessionAgeingGrid = Ds;
    }
    public void Update_Dataset_Value_Delete()
    {
        DataSet Ds = SessionAgeingGrid;

        if (Ds.Tables["Table"].Rows.Count > 0)//delete from Table 
        {
            DataView view = new DataView();
            view.Table = Ds.Tables["Table"];

            view.RowFilter = "Sr_No <" + Index;
            view.RowStateFilter = DataViewRowState.CurrentRows;

            Ds.Tables.Remove("Table");
            Ds.Tables.Add(view.ToTable());
        }
        hdn_Generate_From_Value.Value = Ds.Tables["Table"].Rows[Ds.Tables["Table"].Rows.Count - 1]["To_Days"].ToString();
        SessionAgeingGrid = Ds;
    }

    protected void btn_Ageing_By_BillDate_Click(object sender, EventArgs e)
    {

        Response.Redirect("FrmLedgerAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate +"&IsBillDate=" + Convert.ToBoolean(true) + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));

    }
    protected void btn_Ageing_By_DueDate_Click(object sender, EventArgs e)
    {

        Response.Redirect("FrmLedgerAgeing.aspx?&LedgerGroupId=" + LedgerGroupId + "&LedgerId=" + LedgerId + "&LedgerName=" + LedgerName + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&StartDate=" + StartDate + "&IsBillDate=" + Convert.ToBoolean(false) + "&EndDate=" + EndDate + "&IsCondensed=" + Convert.ToBoolean(1));

    }


    #endregion
}
