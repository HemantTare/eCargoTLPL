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
using Raj.EC.ControlsView;
using Raj.EC.ControlsPresenter;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 20/11/2008
/// Description   : This Page is For POD Cover Generation
/// </summary>
/// 

public partial class Operations_POD_WucPODCoverGeneration : System.Web.UI.UserControl,IPODCoverGenerationView
{
    #region ClassVariables
    PODCoverGenerationPresenter objPODCoverGenerationPresenter;
    PODSentByPresenter objPODSentByPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    int ds_index;
    #endregion

    #region ControlsValues

    public string CoverNo
    {
        set { lbl_Cover_No.Text = value; }
    }
    public DateTime CoverDate
    {
        set { dtp_Cover_Date.SelectedDate = value; }
        get { return dtp_Cover_Date.SelectedDate; }
    }
    public IPODSentByView PODSentByView 
    {
        get {return (IPODSentByView)WucPODSentBy1 ;} 
    }
    
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public string CoverSendHierarchyCode
    {
        set { WucHierarchyWithID1.HierarchyCode = value;}
        get { return WucHierarchyWithID1.HierarchyCode; }
    }
    public int CoverSentMainID
    {
        get 
        {
            if (CoverSendHierarchyCode == "HO")
            {
                return 0;
            }
            else
            {
                return WucHierarchyWithID1.MainId;
            }
        }
        set 
        {
            if (CoverSendHierarchyCode == "HO")
            {
                WucHierarchyWithID1.MainId = 0;
            }
            else
            {
                WucHierarchyWithID1.MainId = value;
            }
        }
    }
    
    
    public int Total_GC
    {
        set
        {
            hdn_Total_GC.Value = Util.Int2String(value);
            lbl_Total_GC.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_GC.Value); }
    }

    public string Flag
    {
        get { return _flag; }
    }

    #endregion

    #region ControlsBind
   
    private void BindPODCoverGrid()
    {
        dg_PODCover.DataSource = SessionBindPODCoverGrid;
        dg_PODCover.DataBind();
    }

    public DataTable SessionBindPODCoverGrid
    {
        get { return StateManager.GetState<DataTable>("BindPODCoverGrid"); }
        set
        {
            StateManager.SaveState("BindPODCoverGrid", value);
            if (StateManager.Exist("BindPODCoverGrid"))
            {
                BindPODCoverGrid();
            }
        }
    }

    public String PODCoverDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindPODCoverGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "PODCoverGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        DropDownList ddl_Hierarchy, ddl_Location;

        ddl_Hierarchy = (DropDownList)WucHierarchyWithID1.FindControl("ddl_hierarchy");
        ddl_Location = (DropDownList)WucHierarchyWithID1.FindControl("ddl_location");


        if (Datemanager.IsValidProcessDate("OPR_PODCG", CoverDate) == false)
        {
            errorMessage = "Please Enter Valid Date";// GetLocalResourceObject("Msg_dtp_CoverDate").ToString();
        }
        else if (CoverSendHierarchyCode == "0")
        {
            errorMessage = "Please Select Hierarchy";// GetLocalResourceObject("Msg_ddl_hierarchy").ToString();
            ddl_Hierarchy.Focus();
        }
        else if (CoverSendHierarchyCode != "HO" && CoverSentMainID == 0)
        {
            errorMessage = "Please Select Location";// GetLocalResourceObject("Msg_ddl_location").ToString();
            ddl_Location.Focus();
        }

        else if (!WucPODSentBy1.validateWUCPODDetails(lbl_Errors))
        {
        }
        else if (Total_GC <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;// GetLocalResourceObject("Msg_PODCoverGrid_validation").ToString();
        }
        else
        {
            _isValid = true;
        }

        if (_isValid == true)
        {
            calculate_griddetails();
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
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region OtherMethod

    private void calculate_griddetails()
    {
        Total_GC = 0;
        CheckBox chk;
        int i;

        if (dg_PODCover.Items.Count > 0)
        {
            objDT = SessionBindPODCoverGrid;

            for (i = 0; i <= dg_PODCover.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_PODCover.Items[i].FindControl("Chk_Attach");

                if (chk.Checked == true)
                {
                    Total_GC = Total_GC + 1;
                }
                objDT.Rows[i]["Att"] = chk.Checked;
            }
        }

    }

    private void Next_PODCover_Number()
    {
        CoverNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (WucSelectedItems1.EnterItem != string.Empty)
        {
            Assign_Hidden_Values_For_Reset();
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objPODCoverGenerationPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindPODCoverGrid;
            WucSelectedItems1.Get_Not_Selected_Items();
        }

    }

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_GC.Text = hdn_Total_GC.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        lbl_Total_GC.Text = "0";
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + " Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + " Nos.Not Found :";
        lbl_totGC.Text = "Total " + CompanyManager.getCompanyParam().GcCaption + ":";
        dg_PODCover.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtp_Cover_Date.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();
        hdn_GCError.Value = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save,btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            btn_null_sessions.Style.Add("display", "none");
            Assign_Hidden_Values_For_Reset();
            WucPODSentBy1.SetPostBack(false);
            WucHierarchyWithID1.Allow_All_Hierarchy = true;
        }
        objPODSentByPresenter = new PODSentByPresenter(WucPODSentBy1, IsPostBack);
        objPODCoverGenerationPresenter = new PODCoverGenerationPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            WucSelectedItems1.SetFoundCaption = "Enter GC Nos. :";// GetLocalResourceObject("Msg_Enter_GCNo").ToString();
            WucSelectedItems1.SetNotFoundCaption = "GC Nos.Not Found :";// GetLocalResourceObject("Msg_GCNo_Not_Found").ToString();
            WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;

            WucHierarchyWithID1.Set_Hierarchy_Caption = "Sent Hierachy :";// GetLocalResourceObject("Msg_Sent_Hierarchy").ToString();

            if (keyID <= 0)
            {
                Next_PODCover_Number();
            }
            else
            {
                td_gccontrol.Style.Add("visibility", "hidden");
            }
        }
        Assign_Hidden_Values_To_TextBox();
        SetStandardCaption();
        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objPODCoverGenerationPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objPODCoverGenerationPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void dtp_Cover_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();

        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objPODCoverGenerationPresenter.fillgrid();
        WucSelectedItems1.dtdetails = SessionBindPODCoverGrid;
        if (keyID > 0)
        {
            Total_GC = SessionBindPODCoverGrid.Rows.Count;
        }
        WucSelectedItems1.Get_Not_Selected_Items();
    }

    public void ClearVariables() // added Ankit
    {
        SessionBindPODCoverGrid = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }
}
