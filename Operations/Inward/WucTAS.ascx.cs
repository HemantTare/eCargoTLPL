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
using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;

/// <summary>
/// Author       : Anita Gupta
/// Description  : Truck Arrival System Wuc
/// Date         : 16 Jan 09 
/// </summary>
/// 

public partial class Operations_Inward_WucTAS : System.Web.UI.UserControl, ITASView
{
    #region ClassVariables
    TASPresenter objTASPresenter;
    Common objComm = new Common();
    string _flag = "";
    string Mode = "0";
    #endregion

    #region InitInterface

    public String TASNo
    {
        set { lbl_TASNoValue.Text = value; }
        get { return lbl_TASNoValue.Text; }
    }
    public DateTime TAS_Date
    {
        set
        {
            wuc_TASDate.SelectedDate = value; 
        }
        get { return wuc_TASDate.SelectedDate; }
    }
    public String TAS_Time
    {
        set { wuc_TASTime.setTime(value); }
        get { return wuc_TASTime.getTime();}       
    }
    public IVehicleSearchView VehicleSearchView
    {
        get { return (IVehicleSearchView)WucVehicleSearch1; }
    }

    public int Vehicle_Id
    {
        set { hdn_Vehicle_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Vehicle_Id.Value); }
    }
    public int Vehicle_Category_Id
    {
        set { hdn_Vehicle_Category_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Vehicle_Category_Id.Value); }
    }

    public String Vehicle_Category
    {
        set { lbl_Vehicle_Category.Text = value; }
        get { return lbl_Vehicle_Category.Text; }
    }
    public String LHPO_Date
    {
        set { lbl_LHPO_Date.Text = value; }
        get { return lbl_LHPO_Date.Text; }
    }
    public int LHPO_Id
    {
        get { return Util.String2Int(ddl_LHPO.SelectedValue); }
    }
    public int TAS_Rec_Count
    {
        set { hdn_TAS_Rec_Count.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_TAS_Rec_Count.Value); }
    }

    public void SetLHPO(string text, string value)
    {
        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
        ddl_LHPO.DataValueField = "LHPO_ID";
        ddl_LHPO.Items.Insert(0, new ListItem(text, value));
    }
    public string Flag
    {
        get { return _flag; }
    }
    public String ScheduledArrivalDate
    {
        set { lbl_ScheduledArrivalDateValue.Text = value; }
        get { return lbl_ScheduledArrivalDateValue.Text; }
    }
    public string LHPOFromLocation
    {
        set { lbl_LHPOFromLocation.Text = value; }
        get { return lbl_LHPOFromLocation.Text; }
    }
    public string LHPOToLocation
    {
        set { lbl_LHPOToLocation.Text = value; }
        get { return lbl_LHPOToLocation.Text; }
    }
    public String ScheduledArrivalTime
    {
        set { lbl_ScheduledArrivalTimeValue.Text = value; }
        get { return lbl_ScheduledArrivalTimeValue.Text; }
    }
    public int NoofMinuteDifferenceForLate
    {
        set { hdn_TimeDiffernceforLate.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_TimeDiffernceforLate.Value); }
    }
    public int Reason_For_Late_Arrival
    {
        set { ddl_Reason_For_Late_Arrival.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Reason_For_Late_Arrival.SelectedValue); }
    }
    public String Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public String TAS_Details_Xml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionTASDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "TAS_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    public int MenuItemId
    {
        get { return Common.GetMenuItemId(); }
    }

    #endregion

    #region controlBind

    public DataTable BindReasionForLateTruckArrival
    {
        set
        {
            ddl_Reason_For_Late_Arrival.DataTextField = "Reason";
            ddl_Reason_For_Late_Arrival.DataValueField = "Reason_Id";
            ddl_Reason_For_Late_Arrival.DataSource = value;
            ddl_Reason_For_Late_Arrival.DataBind();
            ddl_Reason_For_Late_Arrival.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_dg_TASDetails
    {
        set
        {
            SessionTASDetailsGrid = value;
            dg_TASDetails.DataSource = value;
            dg_TASDetails.DataBind();
            TAS_Rec_Count = dg_TASDetails.Items.Count;
        }
    }
    
    public DataTable SessionTASDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("TruckArrivalDetailsGrid"); }
        set { StateManager.SaveState("TruckArrivalDetailsGrid", value); }
    }
      
    public DataSet BindLHPO
    {
        set
        {
            ddl_LHPO.DataTextField = "LHPO_No_For_Print";
            ddl_LHPO.DataValueField = "LHPO_ID";
            ddl_LHPO.DataSource = value;
            ddl_LHPO.DataBind();
        }
    }     
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        errorMessage = "";

        if (VehicleSearchView.VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle.";
        }
        else if (LHPO_Id <= 0)
        {
            errorMessage = "Please Select "+ CompanyManager.getCompanyParam().LHPOCaption + " No.";
        }
        else if (Datemanager.IsValidProcessDate("TASD", TAS_Date) == false)
        {
            errorMessage = GetLocalResourceObject("MsgValidDate").ToString();
        }
        else if (dg_TASDetails.Items.Count <= 0)
        {
            errorMessage = GetLocalResourceObject("MsgDetails").ToString();
        }
        //Commented:05 Feb 09 By:Anita
        /*else if (CheckTASAndScheduledArrivalDateTime() == false)
        {
            errorMessage = "TAS Date should be Greater than or Equal to Scheduled Arrival Date and Time.";
        }*/
        else if (DateTime.Compare(Convert.ToDateTime(LHPO_Date), TAS_Date) > 0 && keyID > 0)
        {
            errorMessage = GetLocalResourceObject("MsgDateCompare").ToString() + CompanyManager.getCompanyParam().LHPOCaption + " Date.";
        }      
        else if (CheckScheduleArrivalDateTimeForReason() == false)
        {
            _isValid = false;
            return _isValid;
        }
       
        else
        {
            _isValid = true;
        }       

        return _isValid;
    }

    private bool CheckScheduleArrivalDateTimeForReason()
    {
        bool _isMandotary = false;

        string[] argTAS_Time = new string[2];
        char[] splitter = { ':' };
        argTAS_Time = TAS_Time.Split(splitter);

        int year, month, date, hour, min, sec=0;
        year = Util.String2Int(TAS_Date.Year.ToString());
        month = Util.String2Int(TAS_Date.Month.ToString());
        date = Util.String2Int(TAS_Date.Day.ToString());
        hour = Util.String2Int(argTAS_Time[0].ToString());
        min = Util.String2Int(argTAS_Time[1].ToString());

        //To store TAS Date Time
        DateTime TASDateTime = new DateTime(year, month, date, hour, min, sec);
 
        string[] argSch_Time = new string[2];
        argSch_Time = ScheduledArrivalTime.Split(splitter);

        DateTime tmpSchDateTime = Convert.ToDateTime(ScheduledArrivalDate);
        year = Util.String2Int(tmpSchDateTime.Year.ToString());
        month = Util.String2Int(tmpSchDateTime.Month.ToString());
        date = Util.String2Int(tmpSchDateTime.Day.ToString());
        hour = Util.String2Int(argSch_Time[0].ToString());
        min = Util.String2Int(argSch_Time[1].ToString());

        //To store ScheduleArrival Date Time
        DateTime ScheduleArrivalDateTime = new DateTime(year, month, date, hour, min, sec);

        // Time span 
        TimeSpan MinuteDifference = TASDateTime.Subtract(ScheduleArrivalDateTime);

        Upd_Pnl_hdn_TimeDiffernceforLate.Update();

        if (MinuteDifference.TotalMinutes >= NoofMinuteDifferenceForLate)
        {
            _isMandotary = true;
        }
        if (_isMandotary == true)
        {
            if (Reason_For_Late_Arrival <= 0)
            {
                errorMessage = "Please Select Reason For Late Arrival.";
                return false;
            }
        }
        return true;
    }

    //To check  whether TAS DateTime is greater than or equal to Scheduled Arrival DateTime.
    /*private bool CheckTASAndScheduledArrivalDateTime()
    {
        string[] argTAS_Time = new string[2];
        char[] splitter = { ':' };
        argTAS_Time = TAS_Time.Split(splitter);

        int year, month, date, hour, min, sec = 0;
        year = Int32.Parse(TAS_Date.Year.ToString());
        month = Int32.Parse(TAS_Date.Month.ToString());
        date = Int32.Parse(TAS_Date.Day.ToString());
        hour = Int32.Parse(argTAS_Time[0].ToString());
        min = Int32.Parse(argTAS_Time[1].ToString());

        //To store TAS Date Time
        DateTime TASDateTime = new DateTime(year, month, date, hour, min, sec);

        string[] argSch_Time = new string[2];
        argSch_Time = ScheduledArrivalTime.Split(splitter);

        DateTime tmpSchDateTime = Convert.ToDateTime(ScheduledArrivalDate);
        year = Int32.Parse(tmpSchDateTime.Year.ToString());
        month = Int32.Parse(tmpSchDateTime.Month.ToString());
        date = Int32.Parse(tmpSchDateTime.Day.ToString());
        hour = Int32.Parse(argSch_Time[0].ToString());
        min = Int32.Parse(argSch_Time[1].ToString());

        //To store ScheduleArrival Date Time
        DateTime ScheduleArrivalDateTime = new DateTime(year, month, date, hour, min, sec);

        //If TAS DateTime is Greater then ScheduleArrivalDateTime return true else false.
        if (DateTime.Compare(TASDateTime, ScheduleArrivalDateTime) >= 0)
            return true;

        return false;

    }*/
    private void SetStandardCaption()
    {
        const int GCCaption = 3;
        const int GCDateCaption = 4;

        hdn_LHPOCaption.Value = CompanyManager.getCompanyParam().LHPOCaption;
        lbl_LHPODate.Text =  CompanyManager.getCompanyParam().LHPOCaption + "  Date:";
        lbl_LHPONo.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No:";
        dg_TASDetails.Columns[GCCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "   No";
        dg_TASDetails.Columns[GCDateCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  Date";
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
    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        SessionTASDetailsGrid = null;
    }

    #endregion

    #region events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            wuc_TASDate.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Save_Print,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Save_Print,btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());


        SetStandardCaption();

        if (!IsPostBack)
        {
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            hdf_ResourecString.Value = ObjCommon.GetResourceString("Operations/Inward/App_LocalResources/WucTAS.ascx.resx");

            Set_Default_Value();

            if (keyID <= 0)
            {
                Next_TAS_Number();
            }
            else
            {
                ddl_LHPO.Enabled = false;
                WucVehicleSearch1.SetEnabled = false;
                wuc_TASDate.AutoPostBackOnSelectionChanged = false;
            }
        }

        objTASPresenter = new TASPresenter(this, IsPostBack);
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI() == true)
        {
            _flag = "SaveAndNew";
            objTASPresenter.Save();
        }
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (validateUI() == true)
        {
            _flag = "SaveAndExit";
            objTASPresenter.Save();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {

        _flag = "SaveAndPrint";
        objTASPresenter.Save();

    }
    #endregion

    public void Set_Default_Value()
    {
        if (!IsPostBack)
        {
            string current_time = DateTime.Now.ToShortTimeString();

            wuc_TASTime.setFormat("24");
            wuc_TASTime.setTime(current_time);
         }
    }

    private void Next_TAS_Number()
    {
        TASNo = objComm.Get_Next_Number();
    }

    protected void ddl_LHPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLHPODetails(sender, e);
    }

    public void GetDetails(object sender, EventArgs e)
    {
        GetVehicleDetails(sender, e);
        GetLHPO(sender, e);
        GetLHPODetails(sender, e);
    }
    public void GetVehicleDetails(object sender, EventArgs e)
    {
        hdn_Vehicle_Id.Value = Util.Int2String(WucVehicleSearch1.VehicleID);
        {
            objTASPresenter.Get_VehicleDetails(sender, e);
        }
    }
    public void GetLHPO(object sender, EventArgs e)
    {
        {
            objTASPresenter.Get_LHPO(sender, e);
        }
    }

    public void GetLHPODetails(object sender, EventArgs e)
    {
        {
            objTASPresenter.Get_LHPODetails(sender, e);
        }
    }
 
    protected void wuc_TASDate_SelectionChanged(object sender, EventArgs e)
    {
        if (wuc_TASDate.AutoPostBackOnSelectionChanged != false)
        {
            GetLHPO(sender, e);
            GetLHPODetails(sender, e);
        }
    }   
}

