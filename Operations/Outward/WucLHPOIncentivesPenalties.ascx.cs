using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;

using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 03-11-08
/// Description : LHPO Incentives Penalties Details
/// </summar>
/// 
/// 
public partial class Operations_Outward_WucLHPOIncentivesPenalties : System.Web.UI.UserControl, ILHPOIncentivesPenaltiesView
{
    #region ClassVariables
    LHPOIncentivesPenaltiesPresenter objLHPOIncentivesPenaltiesPresenter;

    TextBox txt_DaysBeforeDate;
    TextBox txt_DaysAfterDate;
    TextBox txt_Amount;
    TextBox txt_Remarks;
    private ScriptManager scm_IncentivePenaltiesDetail;
    DataTable objDT;
    DataRow DR = null;
    bool isValid = false;

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_IncentivePenaltiesDetail = value; }
    }
    private void DisableControlForRectification()
    {
        dg_IncentiveDetails.Enabled = false;
        dg_PenaltyDetails.Enabled = false;
    }

    #endregion

    #region ControlsBind

    public DataTable BindLHPOIncentivesGrid
    {
        set
        {
            dg_IncentiveDetails.DataSource = value;
            dg_IncentiveDetails.DataBind();
        }
    }

    public DataTable BindLHPOPenaltiesGrid
    {
        set
        {
            dg_PenaltyDetails.DataSource = value;
            dg_PenaltyDetails.DataBind();
        }
    }

    public DataTable SessionBindLHPOIncentiveGrid
    {
        get { return StateManager.GetState<DataTable>("BindLHPOIncentivesGrid"); }
        set { StateManager.SaveState("BindLHPOIncentivesGrid", value); }
    }

    public DataTable SessionBindLHPOPenaltiesGrid
    {
        get { return StateManager.GetState<DataTable>("BindLHPOPenaltiesGrid"); }
        set { StateManager.SaveState("BindLHPOPenaltiesGrid", value); }
    }   
    public string IncentiveDetailsXML
    {
        get
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(SessionBindLHPOIncentiveGrid.Copy());            
            return ds.GetXml();
        }
    }
    public string PenaltyDetailsXML
    {
        get
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(SessionBindLHPOPenaltiesGrid.Copy());
            return ds.GetXml();
        }
    }
    private void CheckLHPOTypeID()
    {
        int LHPOTypeID;
        dg_PenaltyDetails.Enabled = true;
        dg_IncentiveDetails.Enabled = true;
        if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null)
        {
            LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
            if (LHPOTypeID == 2)
            {
                dg_PenaltyDetails.Enabled = false;
                dg_IncentiveDetails.Enabled = false;
            }
            else
            {
                dg_PenaltyDetails.Enabled = true;
                dg_IncentiveDetails.Enabled = true;
            }
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
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
           // return 1;
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      
        objLHPOIncentivesPenaltiesPresenter = new LHPOIncentivesPenaltiesPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
            BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
            if (keyID > 0)
            {
                CheckLHPOTypeID();
            }
        }
        int MenuItemId = Raj.EC.Common.GetMenuItemId();
        if (MenuItemId == 198)
        {
            DisableControlForRectification();
        }
    }
    public void FillGridOnAttachedLHPONoChanged(object o,EventArgs e)
    {
        
        objLHPOIncentivesPenaltiesPresenter.initValues();
        BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
        BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
        upd_pnl_dg_IncentiveDetails.Update();
        Upd_Pnl_PenaltyDetails.Update();
        CheckLHPOTypeID();
    }
    protected void dg_IncentiveDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_DaysBeforeDate = (TextBox)(e.Item.FindControl("txt_DayBeforeDate"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {

            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionBindLHPOIncentiveGrid;
                DR = objDT.Rows[e.Item.ItemIndex];

                txt_DaysBeforeDate.Text = DR["No_Of_Days"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();
                txt_Remarks.Text = DR["Remark"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_DaysBeforeDate = (TextBox)(e.Item.FindControl("txt_DayBeforeDate"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

        objDT = SessionBindLHPOIncentiveGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["No_Of_Days"] = txt_DaysBeforeDate.Text;
            DR["Amount"] = txt_Amount.Text;
            DR["Remark"] = txt_Remarks.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionBindLHPOIncentiveGrid = objDT;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";

        if ( txt_DaysBeforeDate.Text == string.Empty)
        {
            errorMessage = "Please Enter Day Before Commited Delivery Date"; //GetLocalResourceObject("Msg_txt_DayBeforeDate").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_DaysBeforeDate);
        }

       
        else if (txt_Amount.Text == string.Empty || Util.String2Decimal(txt_Amount.Text) <= 0)
        {
            errorMessage = "Please Enter Amount";//GetLocalResourceObject("Msg_txt_Amount").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_Amount);
        }
        else if (txt_Remarks.Text == string.Empty)
        {
            errorMessage = "Please Enter Remarks";//GetLocalResourceObject("Msg_txt_Remarks").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_Remarks);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    protected void dg_IncentiveDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionBindLHPOIncentiveGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["No_Of_Days"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
                    dg_IncentiveDetails.EditItemIndex = -1;
                    dg_IncentiveDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Entry";//GetLocalResourceObject("Msg_Duplicate").ToString();
                scm_IncentivePenaltiesDetail.SetFocus(txt_DaysBeforeDate);
            }
        }
    }

    protected void dg_IncentiveDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_IncentiveDetails.EditItemIndex = e.Item.ItemIndex;
        dg_IncentiveDetails.ShowFooter = false;
        BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void dg_IncentiveDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionBindLHPOIncentiveGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionBindLHPOIncentiveGrid = objDT;
            dg_IncentiveDetails.EditItemIndex = -1;
            dg_IncentiveDetails.ShowFooter = true;
            BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
   
    protected void dg_IncentiveDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_IncentiveDetails.EditItemIndex = -1;
        dg_IncentiveDetails.ShowFooter = true;
        BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void dg_IncentiveDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionBindLHPOIncentiveGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["No_Of_Days"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_IncentiveDetails.EditItemIndex = -1;
                dg_IncentiveDetails.ShowFooter = true;

                BindLHPOIncentivesGrid = SessionBindLHPOIncentiveGrid;
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Entry";// GetLocalResourceObject("Msg_Duplicate").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_DaysBeforeDate);
        }
    }

    protected void dg_PenaltyDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_PenaltyDetails.EditItemIndex = -1;
        dg_PenaltyDetails.ShowFooter = true;
        BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void dg_PenaltyDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionBindLHPOPenaltiesGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["No_Of_Days"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset1(source, e);

            if (isValid == true)
            {
                dg_PenaltyDetails.EditItemIndex = -1;
                dg_PenaltyDetails.ShowFooter = true;

                BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Entry";// GetLocalResourceObject("Msg_Duplicate").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_DaysBeforeDate);
        }
    }

    protected void dg_PenaltyDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txt_DaysAfterDate = (TextBox)(e.Item.FindControl("txt_DayAfterDate"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {

            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionBindLHPOPenaltiesGrid;
                DR = objDT.Rows[e.Item.ItemIndex];

                txt_DaysAfterDate.Text = DR["No_Of_Days"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();
                txt_Remarks.Text = DR["Remark"].ToString();
            }
        }
    }

    protected void dg_PenaltyDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionBindLHPOPenaltiesGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["No_Of_Days"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset1(source, e);
                if (isValid == true)
                {
                    BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
                    dg_PenaltyDetails.EditItemIndex = -1;
                    dg_PenaltyDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Entry";// GetLocalResourceObject("Msg_Duplicate").ToString();
                scm_IncentivePenaltiesDetail.SetFocus(txt_DaysAfterDate);
            }
        }
    }

    protected void dg_PenaltyDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_PenaltyDetails.EditItemIndex = e.Item.ItemIndex;
        dg_PenaltyDetails.ShowFooter = false;
        BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void dg_PenaltyDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionBindLHPOPenaltiesGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionBindLHPOPenaltiesGrid = objDT;
            dg_PenaltyDetails.EditItemIndex = -1;
            dg_PenaltyDetails.ShowFooter = true;
            BindLHPOPenaltiesGrid = SessionBindLHPOPenaltiesGrid;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    private void Insert_Update_Dataset1(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        txt_DaysAfterDate = (TextBox)(e.Item.FindControl("txt_DayAfterDate"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        txt_Remarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

        objDT = SessionBindLHPOPenaltiesGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update1() == true)
        {
            DR["No_Of_Days"] = txt_DaysAfterDate.Text;
            DR["Amount"] = txt_Amount.Text;
            DR["Remark"] = txt_Remarks.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionBindLHPOPenaltiesGrid = objDT;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    private bool Allow_To_Add_Update1()
    {
        lbl_Errors.Text = "";

        if (txt_DaysAfterDate.Text == string.Empty)
        {
            errorMessage = "Please Enter Day After Commited Delivery Date";//GetLocalResourceObject("Msg_txt_DayAfterDate").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_DaysAfterDate);
        }

        else if (txt_Amount.Text == string.Empty || Util.String2Decimal(txt_Amount.Text) <= 0)
        {
            errorMessage = "Please Enter Amount";//GetLocalResourceObject("Msg_txt_Amount").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_Amount);
        }
        else if (txt_Remarks.Text == string.Empty)
        {
            errorMessage = "Please Enter Remarks"; //GetLocalResourceObject("Msg_txt_Remarks").ToString();
            scm_IncentivePenaltiesDetail.SetFocus(txt_Remarks);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
    
}