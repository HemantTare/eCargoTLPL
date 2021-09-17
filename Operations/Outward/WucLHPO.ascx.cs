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

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 25th October 2008
/// Description   : This is the Page For LHPO Forms 
/// </summary>

public partial class Operations_Outward_WucLHPO : System.Web.UI.UserControl, ILHPOView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    LHPOPresenter objLHPOPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    string _flag = "";
    string Mode = "0";
    int MenuItemId = Raj.EC.Common.GetMenuItemId();
    #endregion

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get { return 523; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public ILHPOHireDetailsView LHPOHireDetailsView
    {
        get { return (ILHPOHireDetailsView)WucLHPOHireDetails1; }
    }
    public ILHPOAttachedBranchView LHPOAttachedBranchView
    {
        get { return (ILHPOAttachedBranchView)WucLHPOAttachedBranch1; }
    }

    public ILHPOAlertsBranchesView LHPOAlertsBranchesView
    {
        get { return (ILHPOAlertsBranchesView)WucLHPOAlertsBranches1; }
    }

    public ILHPOIncentivesPenaltiesView LHPOIncentivesPenaltiesView
    {
        get { return (ILHPOIncentivesPenaltiesView)WucLHPOIncentivesPenalties1; }
    }

    public DataSet SessionATHDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("ATHDetails"); }
    }

    public void ClearVariables()
    {
        WucLHPOHireDetails1.SessionLHPOHireDetailsGrid = null;
        WucLHPOAttachedBranch1.SessionAttachedLHPOBranchesGrid = null;
        WucLHPOAlertsBranches1.SessionAlertBranchesGrid = null;
        WucLHPOAlertsBranches1.SessionATHDetailsGrid = null;
        WucLHPOIncentivesPenalties1.SessionBindLHPOIncentiveGrid = null;
        WucLHPOIncentivesPenalties1.SessionBindLHPOPenaltiesGrid = null;

    }
    #endregion

    #region Validation
    public bool validateUI()
    {
        bool IsValid = true;

        if (WucLHPOHireDetails1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 0;
            TabStrip1.SelectedTab = TabStrip1.Tabs[0];
            IsValid = false;
        }
        else if (WucLHPOAttachedBranch1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 1;
            TabStrip1.SelectedTab = TabStrip1.Tabs[1];
            IsValid = false;
        }
        else if (WucLHPOAlertsBranches1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 2;
            TabStrip1.SelectedTab = TabStrip1.Tabs[2];
            IsValid = false;
        }
        else if (WucLHPOIncentivesPenalties1.validateUI() == false)
        {
            MultiPage1.SelectedIndex = 3;
            TabStrip1.SelectedTab = TabStrip1.Tabs[3];
            IsValid = false;
        }
        else if (CheckTotalAdvance() == false)
        {
            IsValid = false;
        }
        else if (CheckToLocationWithAttachedLHPOBranches() == false)
        {
            IsValid = false;
        }
        else if (CheckScheduleArrivalDate() == false)
        {
            IsValid = false;
        }
        else
        {
            IsValid = true;
        }

        return IsValid;
    }
    #endregion
    #region OtherMethods
    private bool CheckTotalAdvance()
    {
        string TotalAdvance = "";
        if (SessionATHDetailsGrid.Tables[0].Rows.Count > 0)
        {
            TotalAdvance = SessionATHDetailsGrid.Tables[0].Compute("SUM(Advance_Amount)", string.Empty).ToString();
            LHPOAlertsBranchesView.TotalAdvanceGrid = Convert.ToDecimal(TotalAdvance);
        }
        if (LHPOHireDetailsView.TotalAdvancePaid != LHPOAlertsBranchesView.TotalAdvanceGrid)
        {
            errorMessage = "Total Advance Should be Same";
            MultiPage1.SelectedIndex = 2;
            TabStrip1.SelectedTab = TabStrip1.Tabs[2];
            return false;
        }
        //else if (LHPOHireDetailsView.TotalAdvancePaid != LHPOAlertsBranchesView.TotalAdvanceGrid)
        //{
        //    errorMessage = "Total of Advance Amount Should be equal to Advance Amount";
        //    MultiPage1.SelectedIndex = 2;
        //    TabStrip1.SelectedTab = TabStrip1.Tabs[2];
        //    return false;
        //}
        else
        {
            return true;
        }
        //}
        //return true;
        //else if (LHPOHireDetailsView.TotalAdvancePaid != LHPOAlertsBranchesView.TotalAdvanceGrid)
        //{
        //    errorMessage = "Total of Advance Amount Should be equal to Advance Amount";
        //    MultiPage1.SelectedIndex = 2;
        //    TabStrip1.SelectedTab = TabStrip1.Tabs[2];
        //    return false;
        //}

    }
    private bool CheckScheduleArrivalDate()
    {
        int i;
        if (LHPOHireDetailsView.LHPOTypeID == 1)
        {
            for (i = 0; i < LHPOAlertsBranchesView.SessionATHDetailsGrid.Tables[0].Rows.Count; i++)
            {

                if (Convert.ToDateTime(LHPOAlertsBranchesView.SessionATHDetailsGrid.Tables[0].Rows[i]["Schedule_Arr_Date"]) < LHPOHireDetailsView.LHPODate)
                {
                    errorMessage = "Schedule Arrival Date should be greater than or equal to" + ' ' + CompanyManager.getCompanyParam().LHPOCaption + ' ' + "date";
                    return false;
                }
            }
        }
        return true;
    }
    private bool CheckToLocationWithAttachedLHPOBranches()
    {
        int i;
        LHPOAttachedBranchView.errorMessage = "";
        for (i = 0; i < LHPOAttachedBranchView.SessionAttachedLHPOBranchesGrid.Tables[0].Rows.Count; i++)
        {

            if (Util.String2Int(LHPOAttachedBranchView.SessionAttachedLHPOBranchesGrid.Tables[0].Rows[i]["BranchID"].ToString()) == LHPOHireDetailsView.ToLocationBranchId)
            {
                LHPOAttachedBranchView.errorMessage = "Controlling Branch '" + LHPOAttachedBranchView.SessionAttachedLHPOBranchesGrid.Tables[0].Rows[i]["BranchName"].ToString() + " ' of To Location Cannot be Same as Attached" + ' ' +  CompanyManager.getCompanyParam().LHPOCaption  + ' '+ "Branch";
                MultiPage1.SelectedIndex = 1;
                TabStrip1.SelectedTab = TabStrip1.Tabs[1];
                return false;
            }
        }
        return true;
    }
    public void GetLHPODate(object o, EventArgs e)
    {
        LHPOAlertsBranchesView.LHPODate = LHPOHireDetailsView.LHPODate;
    }

    #endregion
    #region ControlsEvent

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
            pc.AddAttributes(this.Controls);

        if (MenuItemId == 198)
        {
            btn_Save.Visible = false;
            btn_Save_Print.Visible = false;
            btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));
        }
        else
        {
            btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Save_Print, btn_Close));
            btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Save_Print, btn_Close));
            btn_Save_Print.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save, btn_Save_Exit, btn_Close));
            Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        }
        objLHPOPresenter = new LHPOPresenter(this, IsPostBack);

        // hdf_ResourceString.Value = ObjCommon.GetResourceString("Operations/Outward/App_LocalResources/WucLHPOHireDetails.ascx.resx");

        WucLHPOHireDetails1.ddlAttachedLHPONoChanged += new EventHandler(WucLHPOAlertsBranches1.FillGrid);
        WucLHPOHireDetails1.ddlAttachedLHPONoChanged += new EventHandler(WucLHPOAttachedBranch1.FillGrid);
        WucLHPOHireDetails1.ddlAttachedLHPONoChanged += new EventHandler(WucLHPOIncentivesPenalties1.FillGridOnAttachedLHPONoChanged);
        WucLHPOHireDetails1.ddlFromLocationSelectionChanged += new EventHandler(WucLHPOAlertsBranches1.BindATHGridOnFromLocationSelection);

        WucLHPOHireDetails1.txtTotalAdvancePaid = new EventHandler(WucLHPOAlertsBranches1.AssignTotalAdvance);
        WucLHPOAlertsBranches1.GetLHPODate = new EventHandler(GetLHPODate);

        TabStrip1.Tabs[1].Text = "Attached" + ' ' + CompanyManager.getCompanyParam().LHPOCaption + ' ' + "Branches";
        hdn_LHPO_Caption.Value = CompanyManager.getCompanyParam().LHPOCaption;
        //TabStrip1.Tabs[1].Text = "Attached LHC Branches";
        WucLHPOHireDetails1.SetScriptManager = scm_LHPO;
        WucLHPOAttachedBranch1.SetScriptManager = scm_LHPO;
        WucLHPOAlertsBranches1.SetScriptManager = scm_LHPO;
        WucLHPOIncentivesPenalties1.SetScriptManager = scm_LHPO;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        //calculate_griddetails();
        objLHPOPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        //calculate_griddetails();
        if (MenuItemId == 198 && CheckTotalAdvance() == false)
        {
            //
        }
        else
        {
            objLHPOPresenter.Save();
        }
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        // calculate_griddetails();
        objLHPOPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    #endregion
}
