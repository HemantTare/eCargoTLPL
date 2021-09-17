using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using ClassLibraryMVP;
using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;



public partial class Operations_Inward_WucAUSUnloadingDetails : System.Web.UI.UserControl, IAUSUnloadingDetailsView
{

    #region ClassVariables
    AUSUnloadingDetailsPresenter objAUSUnloadingDetailsPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    PageControls pc = new PageControls();
    string Mode = "0";
    TextBox txt_Recieved_Article, txt_Recieved_Articles_Wt;
    TextBox txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value;

    TextBox txt_Additional_Freight;

    Label lbl_Loaded_Actual_Wt, lbl_Loaded_Articles;
    DropDownList ddl_Received_Condintion;
    private ScriptManager _SCM_Unloading;
    #endregion  
    #region InitInterface


    public ScriptManager SetScriptManager
    {
        set { _SCM_Unloading = value; }
    }

    public IVehicleSearchView VehicleSearchView
    {
        get { return (IVehicleSearchView)WucVehicleSearch1; }
    }

    public int LHPO_Id
    {      
        get{return Util.String2Int(ddl_LHPO.SelectedValue);}
    }

    public int TAS_Id
    {
        set { ddl_TAS.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_TAS.SelectedValue); }
    }

    public int NoofMinuteDifferenceForLate
    {
        set{hdn_TimeDiffernceforLate.Value = Util.Int2String(value); }
        get { return ValueOfHdn_Int(hdn_TimeDiffernceforLate); }// Util.String2Int(hdn_TimeDiffernceforLate.Value); }
    }    

    public int Vehicle_Id
    {
        set {hdn_Vehicle_Id.Value = Util.Int2String(value);}
        get { return ValueOfHdn_Int(hdn_Vehicle_Id);}// Util.String2Int(hdn_Vehicle_Id.Value); }
    }

    public int IsTAS
    {
        set { hdn_IsTAS.Value = Util.Int2String(value); }
        get { return ValueOfHdn_Int(hdn_IsTAS); }// Util.String2Int(hdn_Vehicle_Id.Value); }
    }

    public int Total_GC
    {
        //set
        //{
        //    txt_Total_GC.Text = Util.Int2String(value);
        //    hdn_Total_GC.Value = Util.Int2String(value);
        //}
        get
        {
            return dg_UnloadingDetails.Items.Count;
            //return Util.String2Int(txt_Total_GC.Text.Trim() == string.Empty ? "0" : txt_Total_GC.Text.Trim());
        }
    }

    public void SetLHPO(string text, string value)
    {
        ddl_LHPO.DataTextField = "LHPO_No_For_Print";
        ddl_LHPO.DataValueField = "LHPO_ID";
        ddl_LHPO.Items.Insert(0, new ListItem(text, value));
    }

    public void SetTAS(string text, string value)
    {
        ddl_TAS.DataTextField = "TAS_No_For_Print";
        ddl_TAS.DataValueField = "TAS_ID";
        ddl_TAS.Items.Insert(0, new ListItem(text, value));
    }

    public int Reason_For_Late_Arrival
    {
        set{ddl_Reason_For_Late_Arrival.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Reason_For_Late_Arrival.SelectedValue);}
    }

    public string Reason_For_Late_Arrival_Display
    {
        set { lbl_Reason_For_Late_Arrival_ForDisplay.Text = value; }
    }  

    public int Reason_For_Late_Uploading
    {
        set{ddl_Reason_For_Late_Uploading.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Reason_For_Late_Uploading.SelectedValue); }
    }

    public int TotalShortArticlesValue
    {
        set
        {
            lbl_TotalShortArticlesValue.Text = Util.Int2String(value);
            hdn_Total_Short_Articles.Value = Util.Int2String(value);
        }
        get {return ValueOfHdn_Int( hdn_Total_Short_Articles); }
    }

    public int TotalExcessArticlesValue
    {
        set{lbl_TotalExcessArticlesValue.Text = Util.Int2String(value);}
        get { return ValueOfLable_Int (lbl_TotalExcessArticlesValue); }
    }

    public int Total_Booking_Articles
    {
        set
        {
            lbl_Total_Booking_Articles.Text = Util.Int2String(value);
            hdn_Total_Booking_Articles.Value = Util.Int2String(value);
        }
        get { return ValueOfHdn_Int(hdn_Total_Booking_Articles); }
    }
    public int Total_Loaded_Articles
    {
        set
        {
            lbl_Total_Loaded_Articles.Text = Util.Int2String(value);
            hdn_Total_Loaded_Articles.Value = Util.Int2String(value);
        }
        get { return ValueOfHdn_Int(hdn_Total_Loaded_Articles); }
    }
    
    public int Total_Received_Articles
    {
        set
        {
            lbl_Total_Received_Articles.Text = Util.Int2String(value);
            hdn_Total_Received_Articles.Value = Util.Int2String(value);
        }
        get { return ValueOfHdn_Int(hdn_Total_Received_Articles); }
    }    
    public int Total_Damage_Leakage_Articles
    {
        set
        {
            lbl_Total_Damage_Leakage_Articles.Text = Util.Int2String(value);
            hdn_Total_Damage_Leakage_Articles.Value = Util.Int2String(value);
        }
        get
        {
            return ValueOfHdn_Int(hdn_Total_Damage_Leakage_Articles);
        }
    }
    public int Vehicle_Category_Id
    {
        set{hdn_Vehicle_Category_Id.Value = Util.Int2String(value);}
        get { return ValueOfHdn_Int(hdn_Vehicle_Category_Id); }
    }

    public Decimal Total_Additional_Freight
    {
        set
        {
            lbl_Total_Additional_Freight.Text = Util.Decimal2String(value);
            hdn_Total_Additional_Freight.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(hdn_Total_Additional_Freight.Value);
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_Total_Additional_Freight);
            }
        }
    }

   
 
    public Decimal Total_To_Pay_Collection
    {
        set
        {
            lbl_To_Pay_Value.Text = Util.Decimal2String(value);
            hdn_To_Pay.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(hdn_To_Pay.Value);
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_To_Pay);
            }
        }
    }

    public Decimal Lorry_Hire
    {
        set
        {
            txt_Lorry_Hire.Text = Util.Decimal2String(value);            
        }
        get
        {
            //return Util.String2Decimal(txt_Lorry_Hire.Text.Trim() );
            if (pnl_Payables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(txt_Lorry_Hire.Text);
            }
        }
    }


    public Decimal Other_Receavable 
    {
        set
        {
            txt_Others_Recevable.Text = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(txt_Others_Recevable.Text.Trim() );
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfTextBox_Decimal(txt_Others_Recevable);
            }
        }
    }

    public Decimal Other_Payable
    {
        set
        {
            txt_Others_Payables.Text = Util.Decimal2String(value);
           
        }
        get
        {
            //return Util.String2Decimal(txt_Others_Payables.Text.Trim());
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfTextBox_Decimal(txt_Others_Payables);
            }
        }
    }
 


    public Decimal Total_Receable
    {
        set
        {
            lbl_Total_Recevable_Value.Text = Util.Decimal2String(value);
            hdn_Total_Recevable_Value.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(lbl_Total_Recevable_Value.Text.Trim());
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_Total_Recevable_Value);
                // Util.String2Decimal(hdn_Total_Payable_Value.Value.Trim());
            }
        }
    }

    public Decimal Total_Payable
    {
        set
        {
            lbl_Total_Payable_Value.Text = Util.Decimal2String(value);
            hdn_Total_Payable_Value.Value = Util.Decimal2String(value);
        }
        get
        {
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_Total_Payable_Value);
            }
                // Util.String2Decimal(hdn_Total_Payable_Value.Value.Trim());
        }
    }

    public Decimal Total_Delivery_Commision
    {
        set
        {
            lbl_Total_Delivery_Commision.Text = Util.Decimal2String(value);
            hdn_Total_Delivery_Commision.Value = Util.Decimal2String(value);
        }
        get 
        {

            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                //return ValueOfHdn_Decimal(hdn_Total_Delivery_Commision);
                return Util.String2Decimal(hdn_Total_Delivery_Commision.Value);
            }
        }
    }


    public Decimal To_Pay_Collection
    {
        set
        {
            lbl_To_Pay_Value.Text = Util.Decimal2String(value);
            hdn_To_Pay.Value = Util.Decimal2String(value);
        }
        get
        {
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            { 
               
                //return ValueOfHdn_Decimal(hdn_To_Pay);
                return Util.String2Decimal(hdn_To_Pay.Value);
            }
        }
    }
    public Decimal UpCountryReceivable
    {
        set
        {
            lbl_UpcountryReceivableValue.Text = Util.Decimal2String(value);
            hdn_UpcountryReceivable.Value = Util.Decimal2String(value);
          
        }
        get
        {
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_UpcountryReceivable);
            }
        }
    }
    public Decimal UpcountryCrossingCost
    {
        set
        {
            lbl_UpcountryCrossingCostValue.Text = Util.Decimal2String(value);
            hdn_UpcountryCrossingCost.Value = Util.Decimal2String(value);            
        }
        get
        {
            if (Pnl_Receivables.Style["display"] == "none")
            {
                return 0;
            }
            else
            {
                return ValueOfHdn_Decimal(hdn_UpcountryCrossingCost);
            }
        }
    }
    public Decimal Total_Booking_Articles_Wt
    {
        set
        {
            lbl_Total_Booking_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Booking_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get 
        {
            //return Util.String2Decimal(hdn_Total_Booking_Articles_Wt.Value);
            return ValueOfHdn_Decimal(hdn_Total_Booking_Articles_Wt);
        }
    }
    public Decimal Total_Loaded_Articles_Wt
    {
        set
        {
            lbl_Total_Loaded_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Loaded_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(hdn_Total_Loaded_Articles_Wt.Value);
            return ValueOfHdn_Decimal(hdn_Total_Loaded_Articles_Wt);
        }
    }

    public Decimal Total_Received_Articles_Wt
    {
        set
        {
            lbl_Total_Received_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Received_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(hdn_Total_Received_Articles_Wt.Value);
            return ValueOfHdn_Decimal(hdn_Total_Received_Articles_Wt);
        }
    }
    public Decimal Total_Damage_Leakage_Value
    {
        set
        {
            lbl_Total_Damage_Leakage_Value.Text = Util.Decimal2String(value);
            hdn_Total_Damage_Leakage_Value.Value = Util.Decimal2String(value);
        }
        get
        {
            //return Util.String2Decimal(hdn_Total_Damage_Leakage_Value.Value);
            return ValueOfHdn_Decimal(hdn_Total_Damage_Leakage_Value);
        }
    }
    public String TURNo
    {
        set{lbl_TURNoValue.Text = value;}
        get{return lbl_TURNoValue.Text;}
    }
    //Added : Anita On: 30 Jan 09
    public String Manual_TUR_No
    {
        set { txt_Manual_Tur_No.Text = value; }
        get { return txt_Manual_Tur_No.Text;}
    }
    public String Vehicle_Category
    {
        set{lbl_Vehicle_Category.Text = value;}
        get{return lbl_Vehicle_Category.Text;}
    }
    public String LHPO_Date
    {
        set{lbl_LHPO_Date.Text = value;}
        get{return lbl_LHPO_Date.Text;}
    }
    public String ScheduledArivalDate
    {
        set {lbl_ScheduledArrivalDateValue.Text = value;}
        get{ return lbl_ScheduledArrivalDateValue.Text;}
    }
    public String ScheduledArivalTime
    {
        set {lbl_ScheduledArrivalTimeValue.Text = value; }
        get  { return lbl_ScheduledArrivalTimeValue.Text;  }
    }
    public String TASTime
    {
        set 
        { 
            wuc_TASTime.setTime(value);
            //lbl_TAS_DateTime_ForDisplay.Text = TASDateTime.ToString();
           // lbl_TAS_DateTime_ForDisplay.Text = TASDate  + value;
        }
        get {return wuc_TASTime.getTime(); }
    }
    public String UnloadingTime
    {
        set{wuc_UnloadingTime.setTime(value);}
        get{return wuc_UnloadingTime.getTime();}
    }
    public String Remarks
    {
        set{txt_Remarks.Text = value; }
        get{return txt_Remarks.Text; }
    }
    public String Unloading_Details_Xml
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionUnloadingDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "unloading_details";
            return _objDs.GetXml().ToLower();
        }
    }
    public DateTime AUS_Date
    {
        set
        { 
            wuc_AUSDate.SelectedDate = value;
            lbl_UnloadingDateValue.Text = value.ToString("dd MMM yyyy");
        }
        get {return wuc_AUSDate.SelectedDate;}
    }
    public DateTime TASDate
    {
        set{
            wuc_TASDate.SelectedDate = value;
            lbl_TAS_DateTime_ForDisplay.Text = value.ToString("dd MMM yyyy") + " " + TASTime;
           }
        get{return wuc_TASDate.SelectedDate; }
    }
    public DateTime TASDateTime
    {
        get
        {
            char[] _Separator ={ ':' };
            String[] str_ActualArrivalDateTime = new string[3];

            str_ActualArrivalDateTime = UnloadingTime.Split(_Separator);

            DateTime _ActualArrivalDateTime = new DateTime(AUS_Date.Year, AUS_Date.Month, AUS_Date.Day,
                                                           Util.String2Int(str_ActualArrivalDateTime[0]),
                                                           Util.String2Int(str_ActualArrivalDateTime[1]), 0);
            
            return _ActualArrivalDateTime;
        }
    }

    public DateTime UnloadingDateTime
    {
        get
        {
            char[] _Separator ={ ':' };
            String[] str_UnloadingDateTime = new string[3];

            str_UnloadingDateTime = UnloadingTime.Split(_Separator);

            DateTime _UnloadingDateTime = new DateTime(AUS_Date.Year, AUS_Date.Month, AUS_Date.Day,
                                                       Util.String2Int(str_UnloadingDateTime[0]),
                                                       Util.String2Int(str_UnloadingDateTime[1]), 0);

            return _UnloadingDateTime;
        }
    }
    public int MenuItemId
    {
        get { return Raj.EC.Common.GetMenuItemId(); }
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
    public Decimal BTHAmount
    {
        set { lbl_BTHAmountValue.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(lbl_BTHAmountValue.Text); }
    }

    #endregion

    #region controlBind

    public DataTable BindResionForLateArrivalUnloading
    {
        set
        {
            ddl_Reason_For_Late_Arrival.DataTextField = "Reason";
            ddl_Reason_For_Late_Arrival.DataValueField = "Reason_Id";
            ddl_Reason_For_Late_Arrival.DataSource = value;
            ddl_Reason_For_Late_Arrival.DataBind();
            ddl_Reason_For_Late_Arrival.Items.Insert(0, new ListItem("Select One", "0"));

            ddl_Reason_For_Late_Uploading.DataTextField = "Reason";
            ddl_Reason_For_Late_Uploading.DataValueField = "Reason_Id";
            ddl_Reason_For_Late_Uploading.DataSource = value;
            ddl_Reason_For_Late_Uploading.DataBind();
            ddl_Reason_For_Late_Uploading.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionReceivedCondition
    {
        get { return StateManager.GetState<DataTable>("ReceivedCondition"); }
        set { StateManager.SaveState("ReceivedCondition", value); }
    }

    public void BindReceivedCondition()
    {
        ddl_Received_Condintion.DataSource = SessionReceivedCondition;
        ddl_Received_Condintion.DataTextField = "Received_Condition";
        ddl_Received_Condintion.DataValueField = "Received_Condition_ID";
        ddl_Received_Condintion.DataBind();
    }
    public DataSet BindLHPO
    {
        set
        {
            ddl_LHPO.DataTextField = "LHPO_No_For_Print";
            ddl_LHPO.DataValueField = "Main_LHPO_ID";//  "LHPO_ID";
            ddl_LHPO.DataSource = value;
            ddl_LHPO.DataBind();
        }
    }

    public DataSet BindTAS
    {
        set
        {
            ddl_TAS.DataTextField = "TAS_No_For_Print";
            ddl_TAS.DataValueField = "TAS_ID";
            ddl_TAS.DataSource = value;
            ddl_TAS.DataBind(); 
            ddl_TAS.Items.Insert(0,new ListItem("Select One","0"));                
        }
    }

    public DataSet BindSupervisor
    {
        set
        {
            ddl_Supervisor.DataSource = value;
            ddl_Supervisor.DataTextField = "Supervisor_Name";
            ddl_Supervisor.DataValueField = "Supervisor_Id";
            ddl_Supervisor.DataBind();
        }
    }

    public int Supervisor
    {
        get { return Util.String2Int(ddl_Supervisor.SelectedValue); }
    }

    public void SetSupervisor(string text, string value)
    {
        ddl_Supervisor.DataTextField = "Emp_Name";
        ddl_Supervisor.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Supervisor);
        hdn_Supervisor_Id.Value = value;
    }
    public DataTable Bind_dg_UnloadingDetails
    {
        set
        {
            SessionUnloadingDetailsGrid = value;
            dg_UnloadingDetails.DataSource = value;
            dg_UnloadingDetails.DataBind();

            Calculate_Other_Details();
        }
    }
    public DataTable SessionUnloadingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("UnloadingDetailsGrid"); }
        set { StateManager.SaveState("UnloadingDetailsGrid", value); }
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
        else if (CompanyManager.getCompanyParam().IsTASRequired && TAS_Id <=0 )//ddl_TAS.SelectedIndex < 1)
        {
            errorMessage = "Please select TAS No.";
        }
        else if (LHPO_Id <= 0)
        {
            errorMessage = "Please Select LHPO No.";
        }
        else if (Datemanager.IsValidProcessDate("OPR_AUS", AUS_Date) == false)
        {
            errorMessage = "TUR Date Should be Less than or equal to System/Current Date";
        }
        else if (dg_UnloadingDetails.Items.Count <= 0)
        {
            errorMessage = " Unloading Details Are Not Available.";
        }
        else if (Total_Received_Articles <= 0)
        {
            errorMessage = "Total Received Articles Must be Greater Than Zero.";
        }
        //Commented:05 Feb 09 By:Anita
        /*else if (CheckActualArrivalAndScheduledArrivalDateTime()==false && CompanyManager.getCompanyParam().IsTASRequired)
        {
            errorMessage = "Scheduled Arrival Date should be less then or equal to Actual Arrival Date.";
        }*/
        //else if (TASDate > AUS_Date && pc.Control_Is_Mandatory(trTASDateTimeReason) == true)
        //{
        //    errorMessage = "TAS Date Should Not Be Greater Than Unloading Date .";
        //}
        //else if (TASDate > AUS_Date)
        //{
        //    errorMessage = "AUS Date Should Be Greater Than  Or Equal To TAS Date."; 
        //}
        else if (UnloadingDateTime < TASDateTime && pc.Control_Is_Mandatory(trTASDateTimeReason) == true)
        {
            errorMessage = "Unloading Date Time Should Not Be Less Than Actual Arrival Date Time.";
        }
        else if (TASDate < Convert.ToDateTime(lbl_LHPO_Date.Text) && pc.Control_Is_Mandatory(trTASDateTimeReason) == true)
        {
            errorMessage = "TAS Date Can Not be Less than LHPO Date";
        }
        else if (AUS_Date < Convert.ToDateTime(lbl_LHPO_Date.Text))
        {
            errorMessage = " AUS Date Can Not be Less than LHPO Date";
        }
        else if (CheckScheduleArrivalDateTimeReasonForLateArrival() == false && pc.Control_Is_Mandatory(trTASDateTimeReason) == true)
        {
            _isValid = false;
            return _isValid;
        }
        //else if (CheckAUSDateTimeForReasonForLateUnloading() == false)
        //{
        //    _isValid = false;
        //    return _isValid;
        //}
        else if (grid_validation() == false)
        {
            _isValid = false;
        }
        else if (Supervisor <= 0)
        {
            errorMessage = "Please Select Supervisor. ";
        }
        else
        {
            _isValid = true;
        }

        if (_isValid)
        {
            Get_Actual_Unloading_Details();
        }

        return _isValid;
    }

    //Added : Anita On:22 Jan 09
    //Calculate minutes difference between Actual Arrival Date time and Scheduled Arrival Date Time.
    //To check reason for Late Arrival.
    private bool CheckScheduleArrivalDateTimeReasonForLateArrival()
    {
        bool _isMandotary = false;

        string[] argActualArrival_Time = new string[2];
        char[] splitter = { ':' };
        argActualArrival_Time = TASTime.Split(splitter);

        int year, month, date, hour, min, sec = 0;
        year = Util.String2Int(TASDate.Year.ToString());
        month = Util.String2Int(TASDate.Month.ToString());
        date = Util.String2Int(TASDate.Day.ToString());
        hour = Util.String2Int(argActualArrival_Time[0].ToString());
        min = Util.String2Int(argActualArrival_Time[1].ToString());

        //To store TAS Date Time
        DateTime TASDateTime = new DateTime(year, month, date, hour, min, sec);

        string[] argSch_Time = new string[2];
        argSch_Time = ScheduledArivalTime.Split(splitter);

        DateTime tmpSchDateTime = Convert.ToDateTime(ScheduledArivalDate);
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

    //Added : Anita On:22 Jan 09
    //Calculate minutes difference between Actual Arrival Date time and AUS Date Time.
    //To check reason for Late Unloading.
    private bool CheckAUSDateTimeForReasonForLateUnloading()
    {
        bool _isMandotary = false;

        string[] argActualArrival_Time = new string[2];
        char[] splitter = { ':' };
        argActualArrival_Time = TASTime.Split(splitter);

        int year, month, date, hour, min, sec = 0;
        year = Util.String2Int(TASDate.Year.ToString());
        month = Util.String2Int(TASDate.Month.ToString());
        date = Util.String2Int(TASDate.Day.ToString());
        hour = Util.String2Int(argActualArrival_Time[0].ToString());
        min = Util.String2Int(argActualArrival_Time[1].ToString());

        //To store TAS Date Time
        DateTime TASDateTime = new DateTime(year, month, date, hour, min, sec);

        string[] argAUS_Time = new string[2];
        argAUS_Time = UnloadingTime.Split(splitter);

        year = Util.String2Int(AUS_Date.Year.ToString());
        month = Util.String2Int(AUS_Date.Month.ToString());
        date = Util.String2Int(AUS_Date.Day.ToString());
        hour = Util.String2Int(argAUS_Time[0].ToString());
        min = Util.String2Int(argAUS_Time[1].ToString());

        //To store Actual Unloading Date Time
        DateTime AUSDateTime = new DateTime(year, month, date, hour, min, sec);

        // Time span 
        TimeSpan MinuteDifference = AUSDateTime.Subtract(TASDateTime);

        Upd_Pnl_hdn_TimeDiffernceforLate.Update();

        if (MinuteDifference.TotalMinutes >= NoofMinuteDifferenceForLate)
        {
            _isMandotary = true;
        }
        if (_isMandotary == true)
        {
            if (Reason_For_Late_Uploading <= 0)
            {
                errorMessage = "Please Select Reason For Late Unloading.";
                return false;
            }
        }
        return true;
        //Commented : Anita On : 28 Jan 09
        /* int TotalArrivalMinute = 0;
         int TotalUnLoadingMinute = 0;
         int MinuteDifference = 0;

         TimeSpan ArrivalTimespan, Unloadingtimespan;
         ArrivalTimespan = Convert.ToDateTime(wuc_ActualArrivalTime.getTime()).TimeOfDay;
         Unloadingtimespan = Convert.ToDateTime(wuc_UnloadingTime.getTime()).TimeOfDay;

         TotalArrivalMinute = Convert.ToInt32(ArrivalTimespan.TotalMinutes);
         TotalUnLoadingMinute = Convert.ToInt32(Unloadingtimespan.TotalMinutes);

         MinuteDifference = TotalUnLoadingMinute - TotalArrivalMinute;
         Upd_Pnl_hdn_TimeDiffernceforLate.Update();
         if (Convert.ToDateTime(lbl_UnloadingDateValue.Text) > ActualArrivalDate)
         {
             if (Reason_For_Late_Uploading <= 0)
             {
                 errorMessage = GetLocalResourceObject("Msg_ReasonFroLateUnloading").ToString();
                 return false;
             }
         }
         else if (Convert.ToDateTime(lbl_UnloadingDateValue.Text) == ActualArrivalDate)
         {
             if (Reason_For_Late_Uploading <= 0)
             {
                 if (MinuteDifference >= NoofMinuteDifferenceForLate)
                 {
                     errorMessage = GetLocalResourceObject("Msg_ReasonFroLateUnloading").ToString();
                     return false;
                 }
             }
             if (TotalArrivalMinute > TotalUnLoadingMinute)
             {
                 errorMessage = GetLocalResourceObject("Msg_UnloadingTime").ToString();
                 return false;
             }                      
         }        
         return true;*/
    }

    //To check  whether Actual Arrival DateTime is greater than or equal to Scheduled Arrival DateTime.
    /*private bool CheckActualArrivalAndScheduledArrivalDateTime()
    {
        string[] argActualArrival_Time = new string[2];
        char[] splitter = { ':' };
        argActualArrival_Time = ActualArrivalTime.Split(splitter);

        int year, month, date, hour, min, sec = 0;
        year = Int32.Parse(ActualArrivalDate.Year.ToString());
        month = Int32.Parse(ActualArrivalDate.Month.ToString());
        date = Int32.Parse(ActualArrivalDate.Day.ToString());
        hour = Int32.Parse(argActualArrival_Time[0].ToString());
        min = Int32.Parse(argActualArrival_Time[1].ToString());

        //To store TAS Date Time
        DateTime ActualArrivalDateTime = new DateTime(year, month, date, hour, min, sec);
        
        string[] argSch_Time = new string[2];
        argSch_Time = ScheduledArivalTime.Split(splitter);

        DateTime tmpSchDateTime = Convert.ToDateTime(ScheduledArivalDate);
        year = Util.String2Int(tmpSchDateTime.Year.ToString());
        month = Util.String2Int(tmpSchDateTime.Month.ToString());
        date = Util.String2Int(tmpSchDateTime.Day.ToString());
        hour = Util.String2Int(argSch_Time[0].ToString());
        min = Util.String2Int(argSch_Time[1].ToString());
        
        //To store ScheduleArrival Date Time
        DateTime ScheduleArrivalDateTime = new DateTime(year, month, date, hour, min, sec);

        //If Actual Arrival DateTime is Greater then ScheduleArrival DateTime return true else false.
        if (DateTime.Compare(ActualArrivalDateTime, ScheduleArrivalDateTime) > 0)
            return true;

        return false;

    }*/
    private bool grid_validation()
    {
        TextBox txt_Rec_Art, txt_Rec_Wt;
        CheckBox chk;
        int i;
        bool ATS = true;

        if (dg_UnloadingDetails.Items.Count > 0)
        {
            for (i = 0; i <= dg_UnloadingDetails.Items.Count - 1; i++)
            {
                txt_Rec_Art = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Recieved_Article");
                txt_Rec_Wt = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Recieved_Articles_Wt");

                if (Util.String2Int(txt_Rec_Art.Text) <= 0 || txt_Rec_Art.Text == string.Empty)
                {
                    errorMessage = "Recieved Articles is mandatory and must be greater than zero";
                    _SCM_Unloading.SetFocus(txt_Rec_Art);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(txt_Rec_Art.Text) > Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Articles"].ToString()))
                {
                    errorMessage = "Recieved Articles should not be greater than Loaded Articles";
                    _SCM_Unloading.SetFocus(txt_Rec_Art);
                    ATS = false;
                    break;
                }
                else if (Util.String2Decimal(txt_Rec_Wt.Text) <= 0 || txt_Rec_Wt.Text == string.Empty)
                {
                    errorMessage = "Recieved Articles Wt. is mandatory and must be greater than zero";
                    _SCM_Unloading.SetFocus(txt_Rec_Wt);
                    ATS = false;
                    break;
                }
                else if (Util.String2Decimal(txt_Rec_Wt.Text) > Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Actual_Wt"].ToString()))
                {
                    errorMessage = "Recieved Articles Wt. should not be greater than Loaded Articles Wt.";
                    _SCM_Unloading.SetFocus(txt_Rec_Wt);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(txt_Rec_Art.Text) != Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Articles"].ToString()) && Util.String2Decimal(txt_Rec_Wt.Text) == Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Loaded_Actual_Wt"].ToString()))
                {
                    errorMessage = "Please Enter Proper Recieved Articles Wt.";
                    _SCM_Unloading.SetFocus(txt_Rec_Wt);
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

    private void Get_Actual_Unloading_Details()
    {
        int i = 0;

        if (dg_UnloadingDetails.Items.Count > 0 && StateManager.IsValidSession("UnloadingDetailsGrid"))
        {
            for (i = 0; i <= dg_UnloadingDetails.Items.Count - 1; i++)
            {
                txt_Recieved_Article = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Recieved_Article");
                txt_Recieved_Articles_Wt = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Recieved_Articles_Wt");
                txt_Damaged_Leakage_Articles = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Damaged_Leakage_Articles");
                txt_Damaged_Leakage_Value = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Damaged_Leakage_Value");
                ddl_Received_Condintion = (DropDownList)dg_UnloadingDetails.Items[i].FindControl("ddl_Received_Condintion");

                txt_Additional_Freight = (TextBox)dg_UnloadingDetails.Items[i].FindControl("txt_Additional_Freight");

                SessionUnloadingDetailsGrid.Rows[i]["Received_articles"] = ValueOfTextBox_Int(txt_Recieved_Article); // Util.String2Int(txt_Recieved_Article.Text);
                SessionUnloadingDetailsGrid.Rows[i]["Received_Wt"] = ValueOfTextBox_Decimal(txt_Recieved_Articles_Wt);// Util.String2Decimal(txt_Recieved_Articles_Wt.Text);

                SessionUnloadingDetailsGrid.Rows[i]["Received_Condition_ID"] = Util.String2Int(ddl_Received_Condintion.SelectedValue);

                SessionUnloadingDetailsGrid.Rows[i]["Additional_Freight"] = ValueOfTextBox_Decimal(txt_Additional_Freight);// Util.String2Decimal(txt_Additional_Freight.Text);

                if (Util.String2Int(ddl_Received_Condintion.SelectedValue) == 1)
                {
                    SessionUnloadingDetailsGrid.Rows[i]["damaged_articles"] = 0;
                    SessionUnloadingDetailsGrid.Rows[i]["Damaged_Value"] = 0;
                }
                else
                {
                    SessionUnloadingDetailsGrid.Rows[i]["damaged_articles"] = ValueOfTextBox_Int(txt_Damaged_Leakage_Articles); // Util.String2Int(txt_Damaged_Leakage_Articles.Text);
                    SessionUnloadingDetailsGrid.Rows[i]["Damaged_Value"] = ValueOfTextBox_Decimal(txt_Damaged_Leakage_Value);// Util.String2Decimal(txt_Damaged_Leakage_Value.Text);
                }
            }
        }
    }

    public void Calculate_Other_Details()
    {
        if (dg_UnloadingDetails.Items.Count > 0 && StateManager.IsValidSession("UnloadingDetailsGrid"))
        {
           lbltxtTotal.Text = Convert.ToString(dg_UnloadingDetails.Items.Count);
        }
    }

    public Int32 ValueOfTextBox_Int(TextBox T)
    {
        return Util.String2Int (T.Text.Trim() == string.Empty ? "0" : T.Text.Trim());
    }

    public Decimal ValueOfTextBox_Decimal(TextBox T)
    {
        return Util.String2Decimal(T.Text.Trim() == string.Empty ? "0" : T.Text.Trim());
    }

    public Decimal ValueOfLable_Decimal(Label L)
    {
        return Util.String2Decimal(L.Text.Trim() == string.Empty ? "0" : L.Text.Trim());
    }

    public Int32 ValueOfLable_Int(Label L)
    {
        return Util.String2Int(L.Text.Trim() == string.Empty ? "0" : L.Text.Trim());
    }

    public Decimal ValueOfHdn_Decimal(HiddenField H)
    {
        return Util.String2Decimal(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }

    public Int32 ValueOfHdn_Int(HiddenField H)
    {
        return Util.String2Int(H.Value.Trim() == string.Empty ? "0" : H.Value.Trim());
    }

    private void SetStandardCaption()
    {
        const int GCCaption = 3;
        const int GCDateCaption = 4;        

        lbl_LHPODate.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Date:";        
        lbl_LHPONo.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No:";
        
        dg_UnloadingDetails.Columns[GCCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "   No";
        dg_UnloadingDetails.Columns[GCDateCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  Date";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        SetStandardCaption();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            Set_Default_Value();

            if (keyID <= 0)
            {
                Next_Actual_Unloading_Number();                
            }
            else
            {
                //trTAS.Disabled = true;
                ddl_TAS.Enabled = false;
                ddl_LHPO.Enabled = false;
                WucVehicleSearch1.SetEnabled = false;
                
            }
            if (Mode == "4")
            {
                wuc_AUSDate.Enabled = false;
                TD_Calender.Visible = false;
                wuc_TASDate.disableForView = false;
            }

            trTAS.Visible = CompanyManager.getCompanyParam().IsTASRequired;
            //trTASDateTimeReason.Disabled = CompanyManager.getCompanyParam().IsTASRequired;
            
        }

        //Added On 21 Aug 2018
        string Crypt = "", _LHPO_Id = "";
        int _VehicleId;

        Crypt = System.Web.HttpContext.Current.Request.QueryString["VehicleId"];
        _VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        if (_VehicleId > 0)
        {
            Crypt = System.Web.HttpContext.Current.Request.QueryString["LHPOId"];
            _LHPO_Id = ClassLibraryMVP.Util.DecryptToString(Crypt);

            WucVehicleSearch1.VehicleID = _VehicleId;
            objAUSUnloadingDetailsPresenter = new AUSUnloadingDetailsPresenter(this, IsPostBack);
            ddl_LHPO.SelectedValue = _LHPO_Id;
            GetDetails(this, e);

            ddl_LHPO.Enabled = false;
            WucVehicleSearch1.SetEnabled = false;
        }
        else
        {
            objAUSUnloadingDetailsPresenter = new AUSUnloadingDetailsPresenter(this, IsPostBack);
            WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);
        }

        //Commented On 21 Aug 2018
        //objAUSUnloadingDetailsPresenter = new AUSUnloadingDetailsPresenter(this, IsPostBack);
        //WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);

        hdn_IsTAS.Value = CompanyManager.getCompanyParam().IsTASRequired.ToString();
    }

    //protected void ddl_Supervisor_TxtChange(object sender, EventArgs e)
    //{
    //    hdn_Supervisor_Id.Value = ddl_Supervisor.SelectedValue;
    //}

    public void Set_Default_Value()
    {
        if (!IsPostBack)
        {
            //hdn_Supervisor_Id.Value = "0";

            string set_time = DateTime.Now.ToString("HH:mm");

            wuc_UnloadingTime.setFormat("24");
            wuc_UnloadingTime.setTime(set_time);

            wuc_TASTime.setFormat("24");
            wuc_TASTime.setTime(set_time);

            TotalShortArticlesValue = 0;
            TotalExcessArticlesValue = 0;

            ddl_Supervisor.DataTextField = "Emp_Name";
            ddl_Supervisor.DataValueField = "Emp_ID";
        }
        
        Total_Received_Articles = ValueOfHdn_Int(hdn_Total_Received_Articles); // Util.String2Int(hdn_Total_Received_Articles.Value);
        Total_Damage_Leakage_Articles = ValueOfHdn_Int(hdn_Total_Damage_Leakage_Articles);// Util.String2Int(hdn_Total_Damage_Leakage_Articles.Value);
        Total_Damage_Leakage_Value = ValueOfHdn_Decimal(hdn_Total_Damage_Leakage_Value);// Util.String2Decimal(hdn_Total_Damage_Leakage_Value.Value);

        TotalShortArticlesValue = ValueOfHdn_Int(hdn_Total_Loaded_Articles) - ValueOfHdn_Int(hdn_Total_Received_Articles);// Util.String2Int(hdn_Total_Loaded_Articles.Value) - Util.String2Int(hdn_Total_Received_Articles.Value);

        lbl_TASDate.Visible = false;
        wuc_TASDate.Visible = false;
        lbl_TASTime.Visible = false;
        wuc_TASTime.Visible = false;
        lbl_ReasonforLateArrival.Visible = false;
        ddl_Reason_For_Late_Arrival.Visible = false;
    }

    private void Next_Actual_Unloading_Number()
    {
        TURNo = objComm.Get_Next_Number();
    }

    protected void wuc_AUSDate_SelectionChanged(object sender, EventArgs e)
    {
        lbl_UnloadingDateValue.Text = AUS_Date.ToString("dd MMM yyyy");
        upd_lbl_UnloadingDateValue.Update();

        if (keyID<=0)
        {
            if (CompanyManager.getCompanyParam().IsTASRequired == true)
                GetTASDetails(sender, e);
            else
            {
                GetLHPO(sender, e);
                GetLHPODetails(sender, e);
            }
        }
        upd_OtherDetails.Update();
    }

    protected void ddl_LHPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLHPODetails(sender, e);
        TotalShortArticlesValue = 0;
        upd_OtherDetails.Update();
    }

    public void GetDetails(object sender, EventArgs e)
    {
        GetVehicleDetails(sender, e);

        //Added :Anita On:22 Jan 09
        if (CompanyManager.getCompanyParam().IsTASRequired == true)
        {
            objAUSUnloadingDetailsPresenter.Get_TAS(sender, e);
        }
        else
        {
            GetLHPO(sender, e);
            GetLHPODetails(sender, e);
            upd_OtherDetails.Update();
        }

        TotalShortArticlesValue = 0;
    }

    public void GetLHPODetails(object sender, EventArgs e)
    {
        {
            objAUSUnloadingDetailsPresenter.Get_LHPODetails(sender, e);
        }
    }

    public void SetTotalExcessDetails(object sender, EventArgs e)
    {
        TotalExcessArticlesValue = Convert.ToInt32(sender);
    }

    public void GetLHPO(object sender, EventArgs e)
    {
        {
            objAUSUnloadingDetailsPresenter.Get_LHPO(sender, e);
        }
    }

    public void GetTASDetails(object sender, EventArgs e)
    {
        objAUSUnloadingDetailsPresenter.Get_TASDetails(sender, e);
        GetLHPODetails(sender, e);
    }

    public void GetVehicleDetails(object sender, EventArgs e)
    {
        hdn_Vehicle_Id.Value = Util.Int2String(WucVehicleSearch1.VehicleID);
        {
            objAUSUnloadingDetailsPresenter.Get_VehicleDetails(sender, e);
        }
    }

    protected void dg_UnloadingDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            txt_Recieved_Article = (TextBox)(e.Item.FindControl("txt_Recieved_Article"));
            txt_Recieved_Articles_Wt = (TextBox)(e.Item.FindControl("txt_Recieved_Articles_Wt"));
            txt_Damaged_Leakage_Articles = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Articles"));
            txt_Damaged_Leakage_Value = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Value"));

            txt_Additional_Freight = (TextBox)(e.Item.FindControl("txt_Additional_Freight"));

            lbl_Loaded_Articles = (Label)(e.Item.FindControl("lbl_Loaded_Articles"));
            lbl_Loaded_Actual_Wt = (Label)(e.Item.FindControl("lbl_Loaded_Actual_Wt"));

            ddl_Received_Condintion = (DropDownList)(e.Item.FindControl("ddl_Received_Condintion"));

            BindReceivedCondition();

            txt_Recieved_Article.Attributes.Add("onfocus", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',0,'Recieved_Article')");

            txt_Recieved_Article.Attributes.Add("onblur", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',1,'Recieved_Article')");

            txt_Recieved_Articles_Wt.Attributes.Add("onfocus", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',0,'Recieved_Articles_Wt')");

            txt_Recieved_Articles_Wt.Attributes.Add("onblur", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',1,'Recieved_Articles_Wt')");

            txt_Damaged_Leakage_Articles.Attributes.Add("onfocus", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',0,'Damaged_Leakage_Articles')");

            txt_Damaged_Leakage_Articles.Attributes.Add("onblur", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',1,'Damaged_Leakage_Articles')");

            txt_Damaged_Leakage_Value.Attributes.Add("onfocus", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',0,'Damaged_Leakage_Value')");

            txt_Damaged_Leakage_Value.Attributes.Add("onblur", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" + 
                txt_Additional_Freight.ClientID + "',1,'Damaged_Leakage_Value')");

            ddl_Received_Condintion.Attributes.Add("onchange", "Enable_Disable('" +
               ddl_Received_Condintion.ClientID + "','" + txt_Damaged_Leakage_Articles.ClientID + "','" +
               txt_Damaged_Leakage_Value.ClientID + "')");

            txt_Additional_Freight.Attributes.Add("onfocus", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',0,'Recieved_Article')");

            txt_Additional_Freight.Attributes.Add("onblur", "Calculate_Summary('" +
                lbl_Loaded_Articles.ClientID + "','" + lbl_Loaded_Actual_Wt.ClientID + "','" +
                txt_Recieved_Article.ClientID + "','" + txt_Recieved_Articles_Wt.ClientID + "','" +
                txt_Damaged_Leakage_Articles.ClientID + "','" +
                txt_Damaged_Leakage_Value.ClientID + "','" +
                txt_Additional_Freight.ClientID + "',1,'Recieved_Article')");


            if (keyID > 0)
            {
                ddl_Received_Condintion.SelectedValue = SessionUnloadingDetailsGrid.Rows[e.Item.ItemIndex]["Received_Condition_ID"].ToString();
            }
            if (ddl_Received_Condintion.SelectedValue != "1")
            {
                txt_Damaged_Leakage_Articles.Enabled = true;
                txt_Damaged_Leakage_Value.Enabled = true;
            }
            else
            {
                txt_Damaged_Leakage_Articles.Enabled = false;
                txt_Damaged_Leakage_Value.Enabled = false;
            }
        }
    }

    //Added : Anita On:22 Jan 09
    protected void ddl_TAS_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTASDetails(sender, e);       
        upd_TASDate.Update();
        upd_OtherDetails.Update();
    }
}