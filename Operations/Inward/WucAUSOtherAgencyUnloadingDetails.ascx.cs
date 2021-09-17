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
using ClassLibraryMVP;
using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;

public partial class Operations_Inward_WucAUSOtherAgencyUnloadingDetails : System.Web.UI.UserControl, IAUSOtherAgencyUnloadingDetailsView
{
    #region ClassVariables
    AUSOtherAgencyUnloadingDetailsPresenter objAUSOtherAgencyUnloadingDetailsPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common(); 

    TextBox txt_Loaded_Articles, txt_Loaded_Actual_Wt, txt_Recieved_Article, txt_Recieved_Articles_Wt;
    TextBox txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value;
    DropDownList ddl_Received_Condintion;
    PageControls pc = new PageControls();
    string _flag = "";
    string Mode = "0";
    #endregion

    #region InitInterface
   
    public int ArrivedFromBranchId
    {
        set{hdn_ArrivedFromBranchId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ArrivedFromBranchId.Value) <= 0 ? 0 : Util.String2Int(hdn_ArrivedFromBranchId.Value); }
    }
    public int AgencyId
    {
        set
        {
            ddl_Agency.SelectedValue = Util.Int2String(value);
            ddl_ArrivedFromLocation.OtherColumns = AgencyId.ToString();
            ddl_AgencyLedger.OtherColumns = AgencyId.ToString();
        }
        get{return Util.String2Int(ddl_Agency.SelectedValue);}
    }
    public int AgencyLedgerId
    {
        get { return Util.String2Int(ddl_AgencyLedger.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_AgencyLedger.SelectedValue);}
    }
    public int ArrivedFromLoacationId
    {
        set{hdn_ArrivedFromLoacationId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ArrivedFromLoacationId.Value) <= 0 ? 0 : Util.String2Int(hdn_ArrivedFromLoacationId.Value); }
    }  
    public void SetArrived_From(string text, string value)
    {
        ddl_ArrivedFromLocation.DataTextField = "Name";
        ddl_ArrivedFromLocation.DataValueField = "ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_ArrivedFromLocation);
        hdn_ArrivedFromLoacationId.Value = value;
    }

    public void SetAgency_Ledger(string text, string value)
    {
        ddl_AgencyLedger.DataTextField = "Ledger_Name";
        ddl_AgencyLedger.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_AgencyLedger);
    }   

    public int Reason_For_Late_Uploading
    {
        set{ddl_Reason_For_Late_Uploading.SelectedValue = Util.Int2String(value); }
        get{return  Util.String2Int(ddl_Reason_For_Late_Uploading.SelectedValue);}
    }
    public int Total_GC
    {
        set
        {
            lbl_Total_GC.Text = Util.Int2String(value);
            hdn_Total_GC.Value = Util.Int2String(value);
        }
        get { return hdn_Total_GC.Value == string.Empty ? 0 : Util.String2Int(hdn_Total_GC.Value); }
    }
    public Decimal Total_Booking_Articles
    {
        set
        {
            lbl_Total_Booking_Articles.Text = Util.Decimal2String(value);
            hdn_Total_Booking_Articles.Value = Util.Decimal2String(value);
        }
        get {return Util.String2Decimal(hdn_Total_Booking_Articles.Value);}
    }
    public Decimal Total_Booking_Articles_Wt
    {
        set
        {
            lbl_Total_Booking_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Booking_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Booking_Articles_Wt.Value); }
    } 
    public Decimal Total_Loaded_Articles
    {
        set
        {
            lbl_Total_Loaded_Articles.Text = Util.Decimal2String(value);
            hdn_Total_Loaded_Articles.Value = Util.Decimal2String(value);
        }
        get{return Util.String2Decimal(hdn_Total_Loaded_Articles.Value); }
    }

    public Decimal Total_Loaded_Articles_Wt
    {
        set
        {
            lbl_Total_Loaded_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Loaded_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Loaded_Articles_Wt.Value); }
    }
    public Decimal Total_Received_Articles
    {
        set
        {
            lbl_Total_Received_Articles.Text = Util.Decimal2String(value);
            hdn_Total_Received_Articles.Value = Util.Decimal2String(value);
        }
        get
        {
            return Util.String2Decimal(hdn_Total_Received_Articles.Value);
        }
    }
    public Decimal Total_Received_Articles_Wt
    {
        set
        {
            lbl_Total_Received_Articles_Wt.Text = Util.Decimal2String(value);
            hdn_Total_Received_Articles_Wt.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Received_Articles_Wt.Value); }
    }
    public Decimal Total_Damage_Leakage_Articles
    {
        set
        {
            lbl_Total_Damage_Leakage_Articles.Text = Util.Decimal2String(value);
            hdn_Total_Damage_Leakage_Articles.Value = Util.Decimal2String(value);
        }
        get{return Util.String2Decimal(hdn_Total_Damage_Leakage_Articles.Value); }
    }
   
    public Decimal Total_Damage_Leakage_Value
    {
        set
        {
            lbl_Total_Damage_Leakage_Value.Text = Util.Decimal2String(value);
            hdn_Total_Damage_Leakage_Value.Value = Util.Decimal2String(value);
        }
        get{return Util.String2Decimal(hdn_Total_Damage_Leakage_Value.Value);}
    }

    public Decimal Lorry_Hire
    {
        set{txt_Lorry_Hire.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Lorry_Hire.Text.Trim()); }
    }
    public Decimal Other_Payable
    {
        set { txt_Others_Payables.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Others_Payables.Text.Trim()); }
    }
    public Decimal Total_Goods_Dly_Rec
    {
        set
        {
            lbl_To_Pay_Value.Text = Util.Decimal2String(value);
            hdn_To_Pay.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_To_Pay.Value); }
    }
    public Decimal Total_Upcountry_Rec
    {
        set
        {
            lbl_UpcountryReceivableValue.Text = Util.Decimal2String(value);
            hdn_UpcountryReceivable.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_UpcountryReceivable.Value); }
    }
    public Decimal Total_Receivable
    {
        set
        {
            lbl_Total_Receivable_Value.Text = Util.Decimal2String(value);
            hdn_Total_Receivable_Value.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Receivable_Value.Value); }
    }
    public Decimal Total_Service_charge_Payable
    {
        set
        {
            lbl_Total_Delivery_Commision.Text = Util.Decimal2String(value);
            hdn_Total_Delivery_Commision.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Delivery_Commision.Value); }
    }
    public Decimal Total_Upcountry_Crossing_Cost_Payable
    {
        set
        {
            lbl_UpcountryCrossingCostValue.Text = Util.Decimal2String(value);
            hdn_UpcountryCrossingCost.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_UpcountryCrossingCost.Value); }
    }
    public Decimal Total_Payable
    {
        set
        {
            lbl_Total_Payable_Value.Text = Util.Decimal2String(value);
            hdn_Total_Payable_Value.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Payable_Value.Value); }
    }
    public String TURNo
    {
        set{lbl_TURNoValue.Text = value;}
        get{return lbl_TURNoValue.Text;}
    }    
    public String VehicleNo
    {
        set{txt_VehicleNo.Text = value;}
        get{return txt_VehicleNo.Text.Trim() ;}
    }    
    public String LHPO_No_For_Print
    {
        set{txt_LHPO_No_For_Print.Text = value;}
        get{return txt_LHPO_No_For_Print.Text.Trim(); }
    }
    public String ScheduledArivalDate
    {
        set{lbl_ScheduledArrivalDateValue.Text = value;}
        get{return lbl_ScheduledArrivalDateValue.Text; }
    }
    public String ScheduledArivalTime
    {
        set{lbl_ScheduledArrivalTimeValue.Text = value;}
        get{return lbl_ScheduledArrivalTimeValue.Text; }
    }    
    public String ActualArrivalTime
    {
        set{wuc_ActualArrivalTime.setTime(value);}
        get{return wuc_ActualArrivalTime.getTime();}
    }
    public String UnloadingTime
    {
        set{wuc_UnloadingTime.setTime(value);}
        get{return wuc_UnloadingTime.getTime(); }
    }
    public String Remarks
    {
        set{txt_Remarks.Text = value;}
        get{return txt_Remarks.Text;}
    }
       
    public String Unloading_Details_Xml
    {
        get
        {
            DataSet _objDs = new DataSet();
            DataView view = objComm.Get_View_Table(SessionUnloadingDetailsGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

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
            lbl_ScheduledArrivalDateValue.Text = value.ToString("dd MMM yyyy");
            ddl_ArrivedFromLocation.OtherColumns = AgencyId.ToString();
        }
        get{return wuc_AUSDate.SelectedDate; }
    }
    
    public DateTime LHPO_Date
    {
        set{ wuc_LHPODate.SelectedDate = value;}
        get{return wuc_LHPODate.SelectedDate; }
    }    
    public DateTime ActualArrivalDate
    {
        set{wuc_ActualArrivalDate.SelectedDate = value;}
        get{return wuc_ActualArrivalDate.SelectedDate; }
    }    
    public DateTime ActualArrivalDateTime
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
    
    #endregion

    #region controlBind

    public DataTable BindResionForLateArrivalUnloading
    {
        set
        {
            ddl_Reason_For_Late_Uploading.DataTextField = "Reason";
            ddl_Reason_For_Late_Uploading.DataValueField = "Reason_Id";
            ddl_Reason_For_Late_Uploading.DataSource = value;
            ddl_Reason_For_Late_Uploading.DataBind();
            ddl_Reason_For_Late_Uploading.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    
    public DataTable BindAgency
    {
        set
        {            
            ddl_Agency.DataTextField = "Agency_Name";
            ddl_Agency.DataValueField = "Agency_Id";
            ddl_Agency.DataSource = value;
            ddl_Agency.DataBind();
            ddl_Agency.Items.Insert(0, new ListItem("Select One", "0"));            
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
    public int Supervisor
    {
        get {return Util.String2Int(ddl_Supervisor.SelectedValue);}
    }

    public void SetSupervisor(string text, string value)
    {
        ddl_Supervisor.DataTextField = "Emp_Name";
        ddl_Supervisor.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Supervisor);
    }
    
    private void Bind_dg_UnloadingDetails()
    {
        dg_OtherAgencyGCDetails.DataSource = SessionUnloadingDetailsGrid;
        dg_OtherAgencyGCDetails.DataBind();
    }

    public DataTable SessionUnloadingDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("UnloadingDetailsGrid"); }
        set 
        {
            StateManager.SaveState("UnloadingDetailsGrid", value);
            if (StateManager.Exist("UnloadingDetailsGrid"))
                Bind_dg_UnloadingDetails();
        }
    }  
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        errorMessage = "";               

        if (Datemanager.IsValidProcessDate("OPR_AUS", AUS_Date) == false)
        {
            errorMessage = GetLocalResourceObject("Msg_AusDate").ToString(); ;
        }
        else if (AgencyId <= 0)
        {
            errorMessage = "Please Select Agency";
            ddl_Agency.Focus();
        }
        else if (AgencyLedgerId <= 0)
        {
            errorMessage = "Please Select Agency Ledger";
        }
        else if(VehicleNo.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Vehicle No.";
            txt_VehicleNo.Focus();
        }
        else if (LHPO_No_For_Print.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_LHPONo").ToString(); ;
            txt_LHPO_No_For_Print.Focus();
        }
        else if (Total_GC <=0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (grid_validation() == false)
        {
        }      
        else if (UnloadingDateTime < ActualArrivalDateTime)
        {
            errorMessage = "Unloading Date Time Should Not Be Less Than Actual Arrival Date Time.";
        }
        else if (wuc_ActualArrivalDate.SelectedDate > wuc_AUSDate.SelectedDate)
        {
            errorMessage = GetLocalResourceObject("Msg_DateValidationAAD_Against_TUR").ToString(); ;
        }        
        else if (Supervisor <= 0)
        {
            errorMessage = " Please Select Unloaded by";
        }
        else if (wuc_ActualArrivalDate.SelectedDate < wuc_LHPODate.SelectedDate || wuc_AUSDate.SelectedDate < wuc_LHPODate.SelectedDate)
        {
            errorMessage = GetLocalResourceObject("Msg_DateValidationAAD_Against_LHPO").ToString(); ;
        }          
        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_Booking_Articles.Text = hdn_Total_Booking_Articles.Value;
        lbl_Total_Booking_Articles_Wt.Text = hdn_Total_Booking_Articles_Wt.Value;
        lbl_Total_Loaded_Articles.Text = hdn_Total_Loaded_Articles.Value;
        lbl_Total_Loaded_Articles_Wt.Text = hdn_Total_Loaded_Articles_Wt.Value;
        lbl_Total_Received_Articles.Text = hdn_Total_Received_Articles.Value;
        lbl_Total_Received_Articles_Wt.Text = hdn_Total_Received_Articles_Wt.Value;
        lbl_Total_Damage_Leakage_Articles.Text = hdn_Total_Damage_Leakage_Articles.Value;
        lbl_Total_Damage_Leakage_Value.Text = hdn_Total_Damage_Leakage_Value.Value;
        lbl_Total_GC.Text = hdn_Total_GC.Value;

        lbl_To_Pay_Value.Text = hdn_To_Pay.Value;
        lbl_UpcountryReceivableValue.Text = hdn_UpcountryReceivable.Value;
        lbl_Total_Receivable_Value.Text = hdn_Total_Receivable_Value.Value;
        lbl_Total_Delivery_Commision.Text = hdn_Total_Delivery_Commision.Value;
        lbl_UpcountryCrossingCostValue.Text = hdn_UpcountryCrossingCost.Value;
        lbl_Total_Payable_Value.Text = hdn_Total_Payable_Value.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_Booking_Articles.Value = "0";
        hdn_Total_Booking_Articles_Wt.Value = "0";
        hdn_Total_Loaded_Articles.Value = "0";
        hdn_Total_Loaded_Articles_Wt.Value = "0";
        hdn_Total_Received_Articles.Value = "0";
        hdn_Total_Received_Articles_Wt.Value = "0";
        hdn_Total_Damage_Leakage_Articles.Value = "0";
        hdn_Total_Damage_Leakage_Value.Value = "0";
        hdn_Total_GC.Value = "0";

        hdn_To_Pay.Value = "0";
        hdn_UpcountryReceivable.Value = "0";
        hdn_Total_Receivable_Value.Value = "0";
        hdn_Total_Delivery_Commision.Value = "0";
        hdn_UpcountryCrossingCost.Value = "0";
        hdn_Total_Payable_Value.Value = "0";

        lbl_Total_Booking_Articles.Text = "0";
        lbl_Total_Booking_Articles_Wt.Text = "0";
        lbl_Total_Loaded_Articles.Text = "0";
        lbl_Total_Loaded_Articles_Wt.Text = "0";
        lbl_Total_Received_Articles.Text = "0";
        lbl_Total_Received_Articles_Wt.Text = "0";
        lbl_Total_Damage_Leakage_Articles.Text = "0";
        lbl_Total_Damage_Leakage_Value.Text = "0";
        lbl_Total_GC.Text = "0";

        lbl_To_Pay_Value.Text = "0";
        lbl_UpcountryReceivableValue.Text = "0";
        lbl_Total_Receivable_Value.Text = "0";
        lbl_Total_Delivery_Commision.Text = "0";
        lbl_UpcountryCrossingCostValue.Text = "0";
        lbl_Total_Payable_Value.Text = "0";

    }
    private void Get_Actual_Unloading_Details()
    {
        int i = 0;
        Total_GC = 0;
        CheckBox chk;

        if (dg_OtherAgencyGCDetails.Items.Count > 0)
        {
            for (i = 0; i <= dg_OtherAgencyGCDetails.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_OtherAgencyGCDetails.Items[i].FindControl("Chk_Attach");
                txt_Loaded_Articles = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Loaded_Articles");
                txt_Loaded_Actual_Wt = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Loaded_Actual_Wt");
                txt_Recieved_Article = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Recieved_Article");
                txt_Recieved_Articles_Wt = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Recieved_Articles_Wt");
                txt_Damaged_Leakage_Articles = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Damaged_Leakage_Articles");
                txt_Damaged_Leakage_Value = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Damaged_Leakage_Value");
                ddl_Received_Condintion = (DropDownList)dg_OtherAgencyGCDetails.Items[i].FindControl("ddl_Received_Condintion");

                if (chk.Checked == true)
                {
                    Total_GC = Total_GC + 1;
                }
                SessionUnloadingDetailsGrid.Rows[i]["Att"] = chk.Checked;
                SessionUnloadingDetailsGrid.Rows[i]["Loaded_Articles"] = Util.String2Int(txt_Loaded_Articles.Text);
                SessionUnloadingDetailsGrid.Rows[i]["Loaded_Actual_Wt"] = Util.String2Decimal(txt_Loaded_Actual_Wt.Text);
                SessionUnloadingDetailsGrid.Rows[i]["Received_articles"] = Util.String2Int(txt_Recieved_Article.Text);
                SessionUnloadingDetailsGrid.Rows[i]["Received_Wt"] = Util.String2Decimal(txt_Recieved_Articles_Wt.Text);

                SessionUnloadingDetailsGrid.Rows[i]["Received_Condition_ID"] = Util.String2Int(ddl_Received_Condintion.SelectedValue);

                if (Util.String2Int(ddl_Received_Condintion.SelectedValue) == 1)
                {
                    SessionUnloadingDetailsGrid.Rows[i]["damaged_articles"] = 0;
                    SessionUnloadingDetailsGrid.Rows[i]["Damaged_Value"] = 0;
                }
                else
                {
                    SessionUnloadingDetailsGrid.Rows[i]["damaged_articles"] = Util.String2Int(txt_Damaged_Leakage_Articles.Text);
                    SessionUnloadingDetailsGrid.Rows[i]["Damaged_Value"] = Util.String2Decimal(txt_Damaged_Leakage_Value.Text);
                }
            }
        }
    }

    private bool grid_validation()
    {
        CheckBox chk;
        int i;
        bool ATS = true;
        string GC_No;

        if (Total_GC > 0)
        {
            for (i = 0; i <= dg_OtherAgencyGCDetails.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_OtherAgencyGCDetails.Items[i].FindControl("Chk_Attach");
                txt_Loaded_Articles = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Loaded_Articles");
                txt_Loaded_Actual_Wt = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Loaded_Actual_Wt");
                txt_Recieved_Article = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Recieved_Article");
                txt_Recieved_Articles_Wt = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Recieved_Articles_Wt");
                txt_Damaged_Leakage_Articles = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Damaged_Leakage_Articles");
                txt_Damaged_Leakage_Value = (TextBox)dg_OtherAgencyGCDetails.Items[i].FindControl("txt_Damaged_Leakage_Value");
                ddl_Received_Condintion = (DropDownList)dg_OtherAgencyGCDetails.Items[i].FindControl("ddl_Received_Condintion");
                GC_No = dg_OtherAgencyGCDetails.Items[i].Cells[1].Text;

                if (chk.Checked == true && LHPO_Date < Convert.ToDateTime(SessionUnloadingDetailsGrid.Rows[i]["GC_Date"].ToString()))
                {
                    errorMessage = "LHC Date Can't be less than GC Date For GC : " + GC_No;
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Loaded_Articles.Text) <= 0 || txt_Loaded_Articles.Text == string.Empty))
                {
                    errorMessage = "Loaded Articles is mandatory and must be greater than zero";
                    scm_AUSOtherAgency.SetFocus(txt_Loaded_Articles);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Loaded_Articles.Text) > Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Balance_Articles"].ToString()))
                {
                    errorMessage = "Loaded Articles should not be greater than balance Articles";
                    scm_AUSOtherAgency.SetFocus(txt_Loaded_Articles);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Decimal(txt_Loaded_Actual_Wt.Text) <= 0 || txt_Loaded_Actual_Wt.Text == string.Empty))
                {
                    errorMessage = "Loaded Actual Wt. is mandatory and must be greater than zero";
                    scm_AUSOtherAgency.SetFocus(txt_Loaded_Actual_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Loaded_Actual_Wt.Text) > Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Balance_Articles_Wt"].ToString()))
                {
                    errorMessage = "Loaded Actual Wt. should not be greater than balance Actual Wt.";
                    scm_AUSOtherAgency.SetFocus(txt_Loaded_Actual_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Loaded_Articles.Text) != Util.String2Int(SessionUnloadingDetailsGrid.Rows[i]["Balance_Articles"].ToString()) && Util.String2Decimal(txt_Loaded_Actual_Wt.Text) == Util.String2Decimal(SessionUnloadingDetailsGrid.Rows[i]["Balance_Articles_Wt"].ToString())))
                {
                    errorMessage = "Please Enter Proper Loaded Wt.";
                    scm_AUSOtherAgency.SetFocus(txt_Loaded_Actual_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Recieved_Article.Text) <= 0 || txt_Recieved_Article.Text == string.Empty))
                {
                    errorMessage = "Received Articles is mandatory and must be greater than zero";
                    scm_AUSOtherAgency.SetFocus(txt_Recieved_Article);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Recieved_Article.Text) > Util.String2Int(txt_Loaded_Articles.Text))
                {
                    errorMessage = "Received Articles should not be greater than Loaded Articles";
                    scm_AUSOtherAgency.SetFocus(txt_Recieved_Article);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Decimal(txt_Recieved_Articles_Wt.Text) <= 0 || txt_Recieved_Articles_Wt.Text == string.Empty))
                {
                    errorMessage = "Received Wt. is mandatory and must be greater than zero";
                    scm_AUSOtherAgency.SetFocus(txt_Recieved_Articles_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Recieved_Articles_Wt.Text) > Util.String2Decimal(txt_Loaded_Actual_Wt.Text))
                {
                    errorMessage = "Received Wt. should not be greater than Loaded Wt.";
                    scm_AUSOtherAgency.SetFocus(txt_Recieved_Articles_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Recieved_Article.Text) != Util.String2Int(txt_Loaded_Articles.Text) && Util.String2Decimal(txt_Recieved_Articles_Wt.Text) == Util.String2Decimal(txt_Loaded_Actual_Wt.Text)))
                {
                    errorMessage = "Please Enter Proper Recieved Articles Wt.";
                    scm_AUSOtherAgency.SetFocus(txt_Recieved_Articles_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(ddl_Received_Condintion.SelectedValue) != 1 && (Util.String2Int(txt_Damaged_Leakage_Articles.Text) <= 0))
                {
                    errorMessage = "Please Enter Damaged Leakage Articles";
                    scm_AUSOtherAgency.SetFocus(txt_Damaged_Leakage_Articles);
                    txt_Damaged_Leakage_Articles.Enabled = true;
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(ddl_Received_Condintion.SelectedValue) != 1 && (Util.String2Decimal(txt_Damaged_Leakage_Value.Text) <= 0))
                {
                    errorMessage = "Please Enter Damaged Leakage Value";
                    scm_AUSOtherAgency.SetFocus(txt_Damaged_Leakage_Value);
                    txt_Damaged_Leakage_Value.Enabled = true;
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Damaged_Leakage_Articles.Text) > Util.String2Int(txt_Recieved_Article.Text))
                {
                    errorMessage = "Damaged Leakage Articles should not be greater than Recieved Articles";
                    scm_AUSOtherAgency.SetFocus(txt_Damaged_Leakage_Articles);
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

    private void SetStandardCaption()
    {
        lbl_LHPODate.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Date:";
        lbl_LHPO_No_For_Print.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No:";        
        dg_OtherAgencyGCDetails.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + "   No";
        dg_OtherAgencyGCDetails.Columns[2].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  Date";
        dg_OtherAgencyGCDetails.Columns[3].HeaderText = "Agency " + CompanyManager.getCompanyParam().GcCaption + "  No";
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get{return Util.DecryptToInt(Request.QueryString["Id"]);}
    }
    public string Flag
    {
        get { return _flag; }
    }
#endregion
    private void btn_save_attributes()
    {
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Save_Print, btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Save_Print, btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));
    }    

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            wuc_LHPODate.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        
        ddl_Supervisor.DataTextField = "Emp_Name";
        ddl_Supervisor.DataValueField = "Emp_ID";

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            Assign_Hidden_Values_For_Reset();
            hdf_ResourecString.Value = objComm.GetResourceString("Operations/Inward/App_LocalResources/WucAUSOtherAgencyUnloadingDetails.ascx.resx");

            Set_Default_Value(sender, e);

            if (keyID <= 0)
            {
                Next_Actual_Unloading_Number();
            }
        }
        btn_save_attributes();
        objAUSOtherAgencyUnloadingDetailsPresenter = new AUSOtherAgencyUnloadingDetailsPresenter(this, IsPostBack);
        SetStandardCaption();

        ddl_ArrivedFromLocation.OtherColumns = AgencyId.ToString();
        ddl_AgencyLedger.OtherColumns = AgencyId.ToString();
        Assign_Hidden_Values_To_TextBox();
    }
   
    protected void ddl_ArrivedFromLocation_TxtChange(object sender, EventArgs e)
    {
        ArrivedFromLoacationId = Util.String2Int(ddl_ArrivedFromLocation.SelectedValue);
        ddl_ArrivedFromLocation.OtherColumns = AgencyId.ToString();

        DataSet ds = new DataSet();
        ds = objAUSOtherAgencyUnloadingDetailsPresenter.Get_ToLocationDetails();

        if (ds.Tables[0].Rows.Count > 0)
            ArrivedFromBranchId = Util.String2Int(ds.Tables[0].Rows[0]["del_branch_id"].ToString());
        else
            ArrivedFromBranchId = 0;
    }

    public void Set_Default_Value(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string current_time = DateTime.Now.ToString("HH:mm");
            wuc_UnloadingTime.setFormat("24");
            wuc_ActualArrivalTime.setFormat("24");
            wuc_UnloadingTime.setTime(current_time);
            wuc_ActualArrivalTime.setTime(current_time);

            if (keyID <= 0)
            {
                lbl_UnloadingDateValue.Text = DateTime.Now.ToString("dd MMM yyyy");
                lbl_ScheduledArrivalDateValue.Text = wuc_AUSDate.SelectedDate.ToString("dd MMM yyyy");
                lbl_ScheduledArrivalTimeValue.Text = current_time.ToString();
            }
            else
            {
                ddl_Agency.Enabled = false;
                ddl_AgencyLedger.Enabled = false;
            }
        }
    }

    private void Next_Actual_Unloading_Number()
    {
        TURNo = objComm.Get_Next_Number_For_AUS_Other_Agency();
    }

    protected void wuc_AUSDate_SelectionChanged(object sender, EventArgs e)
    {
        lbl_UnloadingDateValue.Text = wuc_AUSDate.SelectedDate.ToString("dd MMM yyyy");
        lbl_ScheduledArrivalDateValue.Text =  wuc_AUSDate.SelectedDate.ToString("dd MMM yyyy");
        if (keyID <= 0)
        {
            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objAUSOtherAgencyUnloadingDetailsPresenter.FillUnloadingDetails();
        }
    }

    protected void ddl_Agency_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ArrivedFromLocation.OtherColumns = AgencyId.ToString();
        ddl_AgencyLedger.OtherColumns = AgencyId.ToString();
        objAUSOtherAgencyUnloadingDetailsPresenter.Get_Agency_Ledger();

        if (keyID <= 0)
        {
            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objAUSOtherAgencyUnloadingDetailsPresenter.FillUnloadingDetails();
        }
    }  
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        Get_Actual_Unloading_Details();
        objAUSOtherAgencyUnloadingDetailsPresenter.Save();
    }   
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        Get_Actual_Unloading_Details();
        objAUSOtherAgencyUnloadingDetailsPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        Get_Actual_Unloading_Details();
        objAUSOtherAgencyUnloadingDetailsPresenter.Save();
    }
    protected void dg_OtherAgencyGCDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid ="";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        string calculate_grid3 = "";
        string calculate_grid4 = "";
        string calculate_grid5 = "";
        string calculate_grid6 = "";
        string calculate_grid7 = "";
        string calculate_grid8 = "";
        string calculate_grid9 = "";
        string calculate_grid10 = "";
        string calculate_grid11 = "";
        CheckBox chk_Attach;

            if (e.Item.ItemIndex != -1)
            {
                chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
                txt_Loaded_Articles = (TextBox)(e.Item.FindControl("txt_Loaded_Articles"));
                txt_Loaded_Actual_Wt = (TextBox)(e.Item.FindControl("txt_Loaded_Actual_Wt"));

                txt_Recieved_Article = (TextBox)(e.Item.FindControl("txt_Recieved_Article"));
                txt_Recieved_Articles_Wt = (TextBox)(e.Item.FindControl("txt_Recieved_Articles_Wt"));
                txt_Damaged_Leakage_Articles = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Articles"));
                txt_Damaged_Leakage_Value = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Value"));
                ddl_Received_Condintion = (DropDownList)(e.Item.FindControl("ddl_Received_Condintion"));

                BindReceivedCondition();

                calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";
                calculate_grid3 = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";
                calculate_grid4 = "Check_Single(" + chk_Attach.ClientID + ",'j','6')";
                calculate_grid5 = "Check_Single(" + chk_Attach.ClientID + ",'j','7')";
                calculate_grid6 = "Check_Single(" + chk_Attach.ClientID + ",'j','8')";
                calculate_grid7 = "Check_Single(" + chk_Attach.ClientID + ",'j','9')";
                calculate_grid8 = "Check_Single(" + chk_Attach.ClientID + ",'j','10')";
                calculate_grid9 = "Check_Single(" + chk_Attach.ClientID + ",'j','11')";
                calculate_grid10 = "Check_Single(" + chk_Attach.ClientID + ",'j','12')";
                calculate_grid11 = "Check_Single(" + chk_Attach.ClientID + ",'j','13')";

                txt_Loaded_Articles.Attributes.Add("onblur", calculate_grid);
                txt_Loaded_Actual_Wt.Attributes.Add("onblur", calculate_grid1);
                txt_Loaded_Articles.Attributes.Add("onfocus", calculate_grid2);
                txt_Loaded_Actual_Wt.Attributes.Add("onfocus", calculate_grid3);


                txt_Recieved_Article.Attributes.Add("onblur", calculate_grid4);
                txt_Recieved_Articles_Wt.Attributes.Add("onblur", calculate_grid5);
                txt_Recieved_Article.Attributes.Add("onfocus", calculate_grid6);
                txt_Recieved_Articles_Wt.Attributes.Add("onfocus", calculate_grid7);

                txt_Damaged_Leakage_Articles.Attributes.Add("onblur", calculate_grid8);
                txt_Damaged_Leakage_Value.Attributes.Add("onblur", calculate_grid9);
                txt_Damaged_Leakage_Articles.Attributes.Add("onfocus", calculate_grid10);
                txt_Damaged_Leakage_Value.Attributes.Add("onfocus", calculate_grid11);
                             
                ddl_Received_Condintion.Attributes.Add("onchange", "Enable_Disable('" +
                   ddl_Received_Condintion.ClientID + "','" + txt_Damaged_Leakage_Articles.ClientID + "','" +
                   txt_Damaged_Leakage_Value.ClientID + "')");

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
    protected void ddl_AgencyLedger_TxtChange(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objAUSOtherAgencyUnloadingDetailsPresenter.FillUnloadingDetails();
        }
        upd_pnl_dg_AUSUnloading.Update();
    }
}