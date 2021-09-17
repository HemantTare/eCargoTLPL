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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

public partial class Operations_ShortExcess_WucShortExcessSettelment : System.Web.UI.UserControl,IShortExcessSettlementView
{
    #region ClassVariables
    ShortExcessSettlementPresenter objShortExcessSettlementPresenter;
    DataSet objDS = new DataSet();
    Raj.EC.Common objComm = new Raj.EC.Common();

    ClassLibrary.UIControl.DDLSearch DDL_ShortAt;
    DropDownList DDL_GCNO;
    TextBox txt_GCNO, txt_Settled_Articles;
    Label lbl_Excess_Articles,lbl_Short_Articles;
    CheckBox chk;
    int CountGcForSave = 0;
    int i;
    #endregion

    #region OtherMethod

    private bool grid_validation()
    {
        HiddenField hdnArticle = new HiddenField();
        string text = "";
        bool ATS = true;

        if (dg_SESetelment.Items.Count > 0)
        {
            for (i = 0; i <= dg_SESetelment.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_SESetelment.Items[i].FindControl("Chk_Attach");
                DDL_ShortAt = (ClassLibrary.UIControl.DDLSearch)dg_SESetelment.Items[i].FindControl("ddl_Short_At");
                DDL_GCNO = (DropDownList)dg_SESetelment.Items[i].FindControl("ddl_GC_No");
                txt_Settled_Articles = (TextBox)dg_SESetelment.Items[i].FindControl("txt_Settled_Articles");
                lbl_Short_Articles = (Label)dg_SESetelment.Items[i].FindControl("lbl_Short_Articles");

                if(Util.String2Int(SessionShortExcessGrid.Rows[i]["Balance_Articles"].ToString()) < Util.String2Int(lbl_Short_Articles.Text))
                {
                    hdnArticle.Value = SessionShortExcessGrid.Rows[i]["Balance_Articles"].ToString();
                    text = "Excess Articles";
                }
                else
                {
                    hdnArticle.Value = lbl_Short_Articles.Text;
                    text = "Short Articles";
                }

                if (chk.Checked == true && Util.String2Int(DDL_ShortAt.SelectedValue) <= 0)
                {
                    errorMessage = "Please Select Short At Branch";
                    DDL_ShortAt.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(DDL_GCNO.SelectedValue) <= 0)
                {
                    errorMessage = "Please Select "+ CompanyManager.getCompanyParam().GcCaption+ " No";
                    DDL_GCNO.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Settled_Articles.Text) <= 0)
                {
                    errorMessage = "Please Enter Settled Articles";
                    txt_Settled_Articles.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Settled_Articles.Text) > Util.String2Int(hdnArticle.Value))
                {
                    errorMessage = "Settled Articles must be less than or Equal to " + text + " : " + hdnArticle.Value;
                    txt_Settled_Articles.Focus();
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }     

    private void calculate_griddetails()
    {
        if (dg_SESetelment.Items.Count > 0)
        {
            for (i = 0; i <= dg_SESetelment.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_SESetelment.Items[i].FindControl("Chk_Attach");
                txt_Settled_Articles = (TextBox)dg_SESetelment.Items[i].FindControl("txt_Settled_Articles");

                if (txt_Settled_Articles.Text.Trim() == string.Empty)
                {
                    txt_Settled_Articles.Text = "0";
                }

                if (chk.Checked == true)
                {
                    CountGcForSave = CountGcForSave + 1;
                    SessionShortExcessGrid.Rows[i]["Att"] = true;
                    SessionShortExcessGrid.Rows[i]["Short_Articles"] = txt_Settled_Articles.Text;
                }
                else
                {
                    SessionShortExcessGrid.Rows[i]["Att"] = false;
                    SessionShortExcessGrid.Rows[i]["Short_Articles"] = "0";
                }
            }
        }
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        const int GCNoCaption1 = 4;
        dg_SESetelment.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        dg_SESetelment.Columns[GCNoCaption1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
    }
   #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (CountGcForSave <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
            _isValid = false;
        }
        else if (grid_validation() == false)
        {
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
     
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Wuc_Region_Area_Branch1.BranchID;
        }
    }
    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        SessionShortExcessGrid = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit : 21-02-09
    {
        ClearVariables();
    }

    #endregion

    #region ControlsValue
          
    public void Bind_dg_ShortExcessSettlement()
    {
        dg_SESetelment.DataSource = SessionShortExcessGrid;
        dg_SESetelment.DataBind();
    }

    public DataTable SessionShortExcessGrid
    {
        get { return StateManager.GetState<DataTable>("ShortExcessGrid"); }
        set
        {
            StateManager.SaveState("ShortExcessGrid", value);

            if (StateManager.Exist("ShortExcessGrid"))
                Bind_dg_ShortExcessSettlement();
        }
    }

    public String ShortExcessXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionShortExcessGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "ShortExcessGrid";
            return _objDs.GetXml().ToLower();
        }
    }
   
    #endregion      

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objComm.CheckFormRights(btn_Save); 

        Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler (OnBranchSelectionEvent);

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        objShortExcessSettlementPresenter = new ShortExcessSettlementPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            dg_SESetelment.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
            dg_SESetelment.Columns[4].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
            btn_null_sessions.Visible = false;
            OnBranchSelectionEvent(sender,e);
        }
        SetStandardCaption();
    }

    private void OnBranchSelectionEvent(object sender, EventArgs e)
    {
        objShortExcessSettlementPresenter.fillgrid();
    }
    protected void ddl_Short_At_TxtChange(object sender, EventArgs e)
    {
        int Branchid = 0;
        txt_GCNO = (TextBox)sender;
        DDL_ShortAt = (ClassLibrary.UIControl.DDLSearch)txt_GCNO.Parent;
        DataGridItem _item = (DataGridItem)DDL_ShortAt.Parent.Parent;

        DDL_GCNO = (DropDownList)_item.FindControl("ddl_GC_No");
        lbl_Short_Articles = (Label)_item.FindControl("lbl_Short_Articles");
        txt_Settled_Articles = (TextBox)_item.FindControl("txt_Settled_Articles");

        Branchid = Util.String2Int(DDL_ShortAt.SelectedValue);

        if (Branchid > 0)
        {            
            objDS = objShortExcessSettlementPresenter.Fill_GCDetails(1,0,Branchid);
            DDL_GCNO.DataValueField = "GC_Id";
            DDL_GCNO.DataTextField = "GC_No";
            DDL_GCNO.DataSource = objDS;
            DDL_GCNO.DataBind();
            DDL_GCNO.Items.Insert(0, new ListItem("Select " + CompanyManager.getCompanyParam().GcCaption, "0"));

            SessionShortExcessGrid.Rows[_item.ItemIndex]["ShortAt_Id"] = Branchid;
        }
        else
        {
            DDL_GCNO.Items.Clear();
            SessionShortExcessGrid.Rows[_item.ItemIndex]["ShortAt_Id"] = 0;
        }

        lbl_Short_Articles.Text = "0";
        txt_Settled_Articles.Text = "0";
        SessionShortExcessGrid.Rows[_item.ItemIndex]["GC_Id"] = 0;
        SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_Articles"] = 0;
        SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_UnKnown_Id"] = 0;
    }

    protected void ddl_GC_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GC_Id = 0;
        DDL_GCNO = (DropDownList)sender;
        DataGridItem _item = (DataGridItem)DDL_GCNO.Parent.Parent;

        lbl_Short_Articles = (Label)_item.FindControl("lbl_Short_Articles");
        lbl_Excess_Articles = (Label)_item.FindControl("lbl_Exess_Articles");
        txt_Settled_Articles = (TextBox)_item.FindControl("txt_Settled_Articles");
        DDL_ShortAt = (ClassLibrary.UIControl.DDLSearch)_item.FindControl("ddl_Short_At");

        GC_Id = Util.String2Int(DDL_GCNO.SelectedValue);
        
        if (GC_Id > 0)
        {
            DataRow[] _dr;
            objDS = objShortExcessSettlementPresenter.Fill_GCDetails(2, GC_Id, Util.String2Int(DDL_ShortAt.SelectedValue));

            lbl_Short_Articles.Text = objDS.Tables[0].Rows[0]["Articles"].ToString();

            if (Util.String2Int(lbl_Excess_Articles.Text) < Util.String2Int(lbl_Short_Articles.Text))
            {
                txt_Settled_Articles.Text = lbl_Excess_Articles.Text;
            }
            else
            {
                txt_Settled_Articles.Text = objDS.Tables[0].Rows[0]["Articles"].ToString();
            }

            if (objComm.Get_View_Table(SessionShortExcessGrid, "gc_id=" + GC_Id.ToString()).Count <= 0)
            {
                SessionShortExcessGrid.Rows[_item.ItemIndex]["GC_Id"] = GC_Id;
                SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_Articles"] = txt_Settled_Articles.Text;
                SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_UnKnown_Id"] = objDS.Tables[0].Rows[0]["Short_Unknown_Id"].ToString();
            }
            else
            {
                DDL_GCNO.SelectedValue = "0";
                lbl_Short_Articles.Text = "0";
                txt_Settled_Articles.Text = "0";
                SessionShortExcessGrid.Rows[_item.ItemIndex]["GC_Id"] = 0;
                SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_Articles"] = 0;
                SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_UnKnown_Id"] = 0;

                errorMessage = "Duplicate " + CompanyManager.getCompanyParam().GcCaption + " No.";
            }
        }
        else
        {
            errorMessage = "";
            lbl_Short_Articles.Text = "0";
            txt_Settled_Articles.Text = "0";
            SessionShortExcessGrid.Rows[_item.ItemIndex]["GC_Id"] = 0;
            SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_Articles"] = 0;
            SessionShortExcessGrid.Rows[_item.ItemIndex]["Short_UnKnown_Id"] = 0;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {        
        calculate_griddetails();
        objShortExcessSettlementPresenter.Save();
    }
}
