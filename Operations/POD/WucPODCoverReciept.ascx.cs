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
/// Author        : Aashish Lad
/// Created On    : 20th November 2008
/// Description   : This is the Page For POD Cover Receipt
/// </summary>
public partial class Operations_POD_WucPODCoverReciept : System.Web.UI.UserControl,IPODCoverRecieptView 
{
    #region ClassVariables
    PODCoverRecieptPresenter objPODCoverRecieptPresenter;
    PODSentByPresenter objPODSentByPresenter;        
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "";
    string Mode = "0";
    #endregion

    #region ControlsValue

    public int CoverNo
    {
        set{ddl_CoverNo.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_CoverNo.SelectedValue);}
    }
   
    public int MainId
    {
        set{hdn_MainId.Value = Util.Int2String(value);}
        get{ return Util.String2Int(hdn_MainId.Value);}
    }
    public string HierachyCode
    {
        set{hdn_HierachyCode.Value = value;}
        get{return hdn_HierachyCode.Value;}
    }
    public string ReceiptNo
    {
        set { lbl_Receipt_No.Text = value; }
        get { return lbl_Receipt_No.Text; }
    }
    public string SentHierachy
    {
        set 
        { 
            lbl_Sent_Hierachy.Text = value;
            if (value == string.Empty)
            {
                lbl_SentHierachy.Visible = false;
            }
            else
            {
                lbl_SentHierachy.Visible = true;
            }
        } 
    }
    public string SentLocation
    {
        set 
        { 
            lbl_Sent_Location.Text = value;
            if (value == string.Empty)
            {
                lbl_SentLocation.Visible = false;
            }
            else
            {
                lbl_SentLocation.Visible = true;
            }
        }
    }     
    public string Remark
    {
        set{txt_Remark.Text = value;}
        get{return txt_Remark.Text; }
    }
    public string SetCoverDate
    {
        set {lbl_Cover_Date.Text = value;}       
    }
    public DateTime ReceiptDate
    {
        set{Wuc_PODCoverRecieptDate.SelectedDate = value;}
        get{return Wuc_PODCoverRecieptDate.SelectedDate;}
    }    
    public DateTime CoverDate
    {
        get { return Convert.ToDateTime(lbl_Cover_Date.Text); }
    }
    public IPODSentByView PODSentByView 
    {
        get 
        { 
            return (IPODSentByView)WucPODSentBy1;
            Upd_Pnl_CoverNoDetails.Update();
        }
    }

    public string Flag
    {
        get { return _flag; }
    }

    #endregion

    #region ControlsBind

    public void Bind_dg_PODCoverReciept()
    {       
        dg_PODCoverReciept.DataSource = SessionPODCoverReciept;
        dg_PODCoverReciept.DataBind();
        hdn_TotalNoofGC.Value = Util.Int2String(SessionPODCoverReciept.Rows.Count);

    }

    public DataTable Bind_ddl_CoverNo
    {
        set
        {
            ddl_CoverNo.DataSource = value;
            ddl_CoverNo.DataTextField = "Cover_No_For_Print";
            ddl_CoverNo.DataValueField = "Cover_ID";
            ddl_CoverNo.DataBind();
            if (keyID <= 0)
            {
                ddl_CoverNo.Items.Insert(0, new ListItem("-- Select Cover No --", "0"));
            }
        }
    }
    public DataTable SessionPODCoverReciept
    {
        get { return StateManager.GetState<DataTable>("SessionPODCoverReciept"); }
        set
        {
            StateManager.SaveState("SessionPODCoverReciept", value);
            if (StateManager.Exist("SessionPODCoverReciept"))
                Bind_dg_PODCoverReciept();
        }
    }
    public string CoverReceivedDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = ObjCommon.Get_View_Table(SessionPODCoverReciept, "IsCheck = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "CoverRecieptDetails";
            return _objDs.GetXml().ToLower();          
        }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (CoverNo <= 0)
        {
            errorMessage = "Please Select Cover No";// GetLocalResourceObject("Msg_CoverNoResource").ToString();
            ddl_CoverNo.Focus();
        }
        else if (Datemanager.IsValidProcessDate("OPR_PODCR", ReceiptDate) == false)
        {
            errorMessage = "Please select valid Receipt Date";// GetLocalResourceObject("Msg_CoverReciept").ToString();
        }
        else if (!WucPODSentBy1.validateWUCPODDetails(lbl_Errors))
        {
            _isValid = false;
        }
        else if (SessionPODCoverReciept.Rows.Count <= 0)
        {
            errorMessage = "Please Select Atleast One Record";// GetLocalResourceObject("Msg_dg_PODCoverRecieptResource").ToString();
            _isValid = false;
        }
        else if (keyID > 0 && Wuc_PODCoverRecieptDate.SelectedDate.Date < Convert.ToDateTime(lbl_Cover_Date.Text).Date) //added Ankit :07-02-09 : 6.30 pm
        {
            errorMessage = "Receipt date should be greater than or equal to Cover date.";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }
    }

    #endregion
    
    #region OtherMethods
    private void SetPODGeneralDetails()
    {
        if (CoverNo <= 0)
        {           
            SentHierachy = "";
            SentLocation = "";
            SetCoverDate = "";
            MainId = 0;
            HierachyCode = "";
            
            PODSentByView.CourierName = "";
            PODSentByView.CourierDocketNo = "";
            PODSentByView.SentByID = 0;
        }
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        dg_PODCoverReciept.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
    }
    #endregion

    #region ControlsEvent
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            Wuc_PODCoverRecieptDate.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        objPODSentByPresenter = new PODSentByPresenter(WucPODSentBy1, IsPostBack);
        objPODCoverRecieptPresenter = new PODCoverRecieptPresenter(this,IsPostBack);
        
        if(!IsPostBack)
        {
           if (keyID > 0)
           {
               ddl_CoverNo.Enabled = false;
               Wuc_PODCoverRecieptDate.AutoPostBackOnSelectionChanged = false;
               Calendar.AutoPostBackOnSelectionChanged = false;
           }
           else
           {
               ReceiptNo = ObjCommon.Get_Next_Number();
           }
        }

        WucPODSentBy1.Enable_Disable(false);
        //SetStandardCaption();
        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }    
  
    protected void ddl_CoverNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objPODCoverRecieptPresenter.fillgrid();
        SetPODGeneralDetails();
    } 
    protected void Wuc_PODCoverRecieptDate_SelectionChanged(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            objPODCoverRecieptPresenter.FillCoverNo();
            ddl_CoverNo_SelectedIndexChanged(sender, e);
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        objPODCoverRecieptPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        objPODCoverRecieptPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    #endregion

    public void ClearVariables() //added Ankit : 21-02-09
    {
        SessionPODCoverReciept = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e) 
    {
        ClearVariables();
    }
   
}
