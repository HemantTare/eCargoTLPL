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


public partial class Operations_POD_WucPODDeliverySheet : System.Web.UI.UserControl,IPODDeliverySheetView
{
    #region ClassVariables
    PODDeliverySheetPresenter objPODDeliverySheetPresenter;
    PODSentByPresenter objPODSentByPresenter; 
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    int Total_GC = 0;
    string _flag = "";
    string Mode = "0";
    CheckBox chk;
    #endregion

    #region ControlsValue

    public string PODDeliverySheetNo 
    {
        get { return lbl_PODDeliverySheetNo.Text; }
        set { lbl_PODDeliverySheetNo.Text = value; }
    }
    public string Remark
    {
        set{txt_Remark.Text = value;}
        get{return txt_Remark.Text.Trim();}
    }
    public DateTime PODDeliveryDate 
    {
        get {return Convert.ToDateTime(Wuc_PODDeliverySheetDate.SelectedDate);}
        set { Wuc_PODDeliverySheetDate.SelectedDate = value;}
    }
    public string Flag
    {
        get { return _flag; }
    }
    public void Bind_dg_PODDeliverySheet()
    {
        dg_PODDeliverySheet.DataSource = SessionPODDeliverySheet;
        dg_PODDeliverySheet.DataBind();
    }

    public DataTable SessionPODDeliverySheet
    {
        get { return StateManager.GetState<DataTable>("PODDeliverySheet"); }
        set
        {
            StateManager.SaveState("PODDeliverySheet", value);
            if (StateManager.Exist("PODDeliverySheet"))
                Bind_dg_PODDeliverySheet();
        }
    }

    public String PODDeliverySheetDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = ObjCommon.Get_View_Table(SessionPODDeliverySheet,"IsCheck = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "PODDeliverySheetgrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    public IPODSentByView PODSentByView 
    {
        get{return (IPODSentByView)WucPODSentBy1;}
    }
    public string PODDeliveredTo
    {
        set { txt_PODDeliveredTo.Text = value; }
        get { return txt_PODDeliveredTo.Text; }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (Datemanager.IsValidProcessDate("OPR_PODCD", PODDeliveryDate) == false)
        {
            errorMessage = "Please select valid Delivery Date.";// GetLocalResourceObject("Msg_DeliveryDate").ToString();
        }
        else if (!WucPODSentBy1.validateWUCPODDetails(lbl_Errors))
        {
            _isValid = false;
        }
        else if (Total_GC <= 0)
        {
            errorMessage = "Please Select Atleast One Record";// GetLocalResourceObject("Msg_dg_PODdeliverySheetResource").ToString();
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
        set{lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get {return Util.DecryptToInt(Request.QueryString["Id"]);}
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            Wuc_PODDeliverySheetDate.Enabled = false;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        objPODSentByPresenter = new PODSentByPresenter(WucPODSentBy1, IsPostBack);
        objPODDeliverySheetPresenter = new PODDeliverySheetPresenter(this, IsPostBack);
        
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                PODDeliverySheetNo = ObjCommon.Get_Next_Number();
            }
        }
        
        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);

    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        dg_PODDeliverySheet.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
    }
    protected void Wuc_PODDeliverySheetDate_SelectionChanged(object sender, EventArgs e)
    {
            objPODDeliverySheetPresenter.FillGrid();
    }

    private void calculate_griddetails()
    {
        int i;
        DataTable objdt = new DataTable();

        if (dg_PODDeliverySheet.Items.Count > 0)
        {
            objdt = SessionPODDeliverySheet;

            for (i = 0; i <= dg_PODDeliverySheet.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_PODDeliverySheet.Items[i].FindControl("Chk_Attach");

                if (chk.Checked == true)
                {
                    Total_GC = Total_GC + 1;
                }
                objdt.Rows[i]["IsCheck"] = chk.Checked;
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objPODDeliverySheetPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objPODDeliverySheetPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    public void ClearVariables()
    {
        SessionPODDeliverySheet = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

}
