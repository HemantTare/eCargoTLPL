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
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

public partial class Operations_Vehcile_Hire_Bill_WucVehicleHireBillDetails : System.Web.UI.UserControl,IVehicleHireBillDetailsView
{
    #region ClassVariables
    VehicleHireBillDetailsPresenter objVehicleHireBillDetailsPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    PageControls pc = new PageControls();
    private Boolean _Is_HOB_Series_Req;
    DataSet objDS;
    string Mode = "0";

    #endregion

    #region ControlsValue
    public int VehicleID
    {
        set { WucVehicleSearch1.VehicleID = value; }
        get { return WucVehicleSearch1.VehicleID; }
    }   
   
    public int BrokerID
    {
        set { ddl_BrokerName.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BrokerName.SelectedValue); }
    }
   
    public int FreightTypeID
    {
        set { ddl_FreightType.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_FreightType.SelectedValue); }
    }
   
    public int FromLocationID
    {
        get { return Util.String2Int(ddl_FromLocation.SelectedValue); }
    }
    public int ToLocationID
    {
        get { return Util.String2Int(ddl_ToLocation.SelectedValue); }
    }
    public int Driver1ID
    {
        get { return Util.String2Int(ddl_Driver1.SelectedValue); }
    }
    public int Driver2ID
    {
        get { return Util.String2Int(ddl_Driver2.SelectedValue); }
    }
    public int CleanerID
    {
        get { return Util.String2Int(ddl_Cleaner.SelectedValue); }
    }   
    public int VehicleCapacity
    {
        set { lbl_VehicleCapacityValue.Text = Util.Int2String(value) + "  Kg"; }
        get { return Util.String2Int(lbl_VehicleCapacityValue.Text); }
    }
    public string HireBillNoForPrint
    {
        set { lbl_VehicleHireBillNoValue.Text = value; }
    }
    
    public int TransitDays
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            txt_TransitDays.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_TransitDays.Text); }
    } 

   
    // -- Decimal Property

    public decimal TruckHireCharge
    {
        set
        {
            lbl_TruckHireChargeValue.Text = Util.Decimal2String(value);
            hdn_TruckHireCharge.Value = Util.Decimal2String(value);
            txt_TruckHireCharge.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TruckHireCharge.Value); }
    }
    public decimal TotalTruckHireCharge
    {
        set
        {
            lbl_TotalTruckHireValue.Text = Util.Decimal2String(value);
            hdn_TotalTruckHire.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalTruckHire.Value); }
    }
    public decimal AdvanceReceived
    {
        set
        {
            txt_AdvanceReceived.Text = Util.Decimal2String(value);
            hdn_AdvanceReceived.Value = Util.Decimal2String(value);
            
        }
        get { return Util.String2Decimal(txt_AdvanceReceived.Text); }
    }
    public decimal BrokeragePayable
    {
        set 
        { txt_BrokeragePayable.Text = Util.Decimal2String(value);
         hdn_BrokeragePayable.Value = Util.Decimal2String(value);
        }
   
        get{return Util.String2Decimal(txt_BrokeragePayable.Text);}
    }
    public decimal CollectionChargePayable
    {
        set
        {
            txt_CollectionChargsPayableValue.Text = Util.Decimal2String(value);
            hdn_CollectionChargesPayable.Value = Util.Decimal2String(value);
            
        }
        get { return Util.String2Decimal(txt_CollectionChargsPayableValue.Text); }
    }
      
    public decimal WtGuarantee
    {
        set { txt_WtGuarantee.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_WtGuarantee.Text); }
    }   

    public decimal RateKg
    {
        set { txt_RateKg.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_RateKg.Text); }
    }

  

    public decimal ActualWtPayableValue
    {
        set
        {
            lbl_ActualWtPayableValue.Text = Util.Decimal2String(value);
            hdn_ActualWtPayable.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ActualWtPayable.Value); }
    }

  
   
    public decimal ActualKms
    {
        set
        {         
            txt_ActualKmsValue.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(txt_ActualKmsValue.Text); }
    }
    public decimal TDSPercentage
    {
        set
        {
            lbl_TDSPerValue.Text = Util.Decimal2String(value);
            hdn_TDSPer.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSPer.Value); }
    }
    public decimal TDSAmount
    {
        set
        {
            lbl_TDSAmountValue.Text = Util.Decimal2String(value);
            hdn_TDSAmountValue.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSAmountValue.Value); }
    }

    public decimal TotalTDSAmount
    {
        set
        {
            lbl_TDSAmountValue.Text = Util.Decimal2String(value);
            hdn_TDSAmountValue.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSAmountValue.Value); }
    } 
   
    public string RefNo
    {
        set { txt_ManualRefNo.Text = value; }
        get { return txt_ManualRefNo.Text; }
    }
    public string Remark
    {
        set { txt_Remark.Text = value; }
        get { return txt_Remark.Text; }
    }   
    public string VehicleDepartureTime
    {
        set { Wuc_VehicleDepartureTime.setTime(value); }
        get { return Wuc_VehicleDepartureTime.getTime(); }
    }
   
   
    public DateTime VehicleHireBillDate
    {
        set { WucHireBillDate.SelectedDate = value; }
        get { return WucHireBillDate.SelectedDate; }
    }

    public DateTime CommittedDelDate
    {
        set
        {
            lbl_CommitedDelDateValue.Text = String.Format("{0:MMMM dd, yyyy}", value);
            hdn_CommitedDelDate.Value = String.Format("{0:MMMM dd, yyyy}", value);
        }
        get { return Convert.ToDateTime(hdn_CommitedDelDate.Value); }
    }
    

    // Integer Property    

      
    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Next_No.Value); }
    }

    public string VehicleHireBillNo
    {
        set
        {
            hdn_Padded_Next_No.Value = value;
            lbl_VehicleHireBillNoValue.Text = value;
        }
        get { return hdn_Padded_Next_No.Value; }
    }

    public int Document_Series_Allocation_ID
    {
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
    } 
    
    public Boolean IsHOBSeriesRequired
    {
        get { return _Is_HOB_Series_Req; }
        set { _Is_HOB_Series_Req = value; }
    }
    public int StartNo
    {
        set { hdn_StartNo.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_StartNo.Value); }
    }

    public int EndNo
    {
        set { hdn_EndNo.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_EndNo.Value); }
    }
   
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_BrokerName
    {
        set
        {
            ddl_BrokerName.DataSource = value;
            ddl_BrokerName.DataTextField = "Vendor_Name";
            ddl_BrokerName.DataValueField = "Vendor_ID";
            ddl_BrokerName.DataBind();
            ddl_BrokerName.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }

    public DataTable Bind_ddl_FreightType
    {
        set
        {
            ddl_FreightType.DataSource = value;
            ddl_FreightType.DataTextField = "LHPO_Freight_Basis";
            ddl_FreightType.DataValueField = "LHPO_Freight_Basis_ID";
            ddl_FreightType.DataBind();
            ddl_FreightType.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
       
        if (VehicleID == -1)
        {
            errorMessage = "Please Select Vehicle No";
            WucVehicleSearch1.Focus();
        }            
      
        else if (ddl_FromLocation.SelectedValue == "")
        {
            errorMessage = "Please Select  From Location";
            ddl_FromLocation.Focus();
        }
        else if (BrokerID <= 0)
        {
            errorMessage = "please Select Broker Name";
            ddl_BrokerName.Focus();
        }
        else if (ddl_ToLocation.SelectedValue == "")
        {
            errorMessage = "Please Select To Location";
            ddl_ToLocation.Focus();
        }       
        
        else if (FreightTypeID == 0)
        {
            errorMessage = "Please Select Freight Type"; 
            ddl_FreightType.Focus();
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


    #region OtherProperties
   
    #endregion


    #region OtherMethods
    public void SetFromLocationID(string FromLocationName, string FromLocationID)
    {
        ddl_FromLocation.DataTextField = "Service_Location_Name";
        ddl_FromLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(FromLocationName, FromLocationID, ddl_FromLocation);
    }
    public void SetToLocationID(string ToLocationName, string ToLocationID)
    {
        ddl_ToLocation.DataTextField = "Service_Location_Name";
        ddl_ToLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(ToLocationName, ToLocationID, ddl_ToLocation);
    }
    public void SetDriver1ID(string Driver1Name, string Driver1ID)
    {
        ddl_Driver1.DataTextField = "Driver_Name";
        ddl_Driver1.DataValueField = "Driver_ID";
        Raj.EC.Common.SetValueToDDLSearch(Driver1Name, Driver1ID, ddl_Driver1);
    }
    public void SetDriver2ID(string Driver2Name, string Driver2ID)
    {
        ddl_Driver2.DataTextField = "Driver_Name";
        ddl_Driver2.DataValueField = "Driver_ID";
        Raj.EC.Common.SetValueToDDLSearch(Driver2Name, Driver2ID, ddl_Driver2);
    }
    public void SetCleanerID(string CleanerName, string CleanerID)
    {
        ddl_Cleaner.DataTextField = "Cleaner_Name";
        ddl_Cleaner.DataValueField = "Cleaner_ID";
        Raj.EC.Common.SetValueToDDLSearch(CleanerName, CleanerID, ddl_Cleaner);
    }
  
    private void SetAllDDLSearch()
    {
        ddl_FromLocation.DataTextField = "Service_Location_Name";
        ddl_FromLocation.DataValueField = "Service_Location_ID";

        ddl_ToLocation.DataTextField = "Service_Location_Name";
        ddl_ToLocation.DataValueField = "Service_Location_ID";

        ddl_Driver1.DataTextField = "Driver_Name";
        ddl_Driver1.DataValueField = "Driver_ID";

        ddl_Driver2.DataTextField = "Driver_Name";
        ddl_Driver2.DataValueField = "Driver_ID";

        ddl_Cleaner.DataTextField = "Cleaner_Name";
        ddl_Cleaner.DataValueField = "Cleaner_ID";
        

    }
    public void Get_Next_No()
    {

        int _Document_Allocation_ID = 0;
        int _Start_No = 0;
        int _End_No = 0;
        int _Next_No = 0;
        string _Padded_Next_No = "";

        ObjCommon.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 5);
        Document_Series_Allocation_ID = _Document_Allocation_ID;
        StartNo = _Start_No;
        EndNo = _End_No;
        Next_No = _Next_No;

        if (_Next_No <= 0)
        {
            Raj.EC.Common.DisplayErrors(1013);
        }
        else if (Next_No > EndNo)
        {
            errorMessage = "Series Should be in between '" + StartNo + "' To  '" + EndNo + " '";
        }

        _Padded_Next_No = _Next_No.ToString("0000000");
        VehicleHireBillNo = _Padded_Next_No;
        

    }

    private void CalculateTruckHireCharge()
    {

        //1	Per Kg
        if (FreightTypeID == 1)
        {

            ActualWtPayableValue = WtGuarantee;
            if (ActualKms > WtGuarantee)
            {
                ActualWtPayableValue = ActualKms;
            }
            TruckHireCharge = (RateKg * ActualWtPayableValue);


        }

        //2	Fixed
        if (FreightTypeID == 2)
        {
            hdn_TruckHireCharge.Value = txt_TruckHireCharge.Text;


        }

        //3	Per Km
        if (FreightTypeID == 3)
        {

            ActualWtPayableValue = WtGuarantee;
            TruckHireCharge = (RateKg * ActualWtPayableValue);
            if (ActualKms > WtGuarantee)
            {
                ActualWtPayableValue = ActualKms;
                TruckHireCharge = (RateKg * ActualKms);
            }

            lbl_TruckHireChargeValue.Text = hdn_TruckHireCharge.Value;


        }

        //4	Per Article
        if (FreightTypeID == 4)
        {
            ActualWtPayableValue = WtGuarantee;
            if (ActualKms > WtGuarantee)
            {
                ActualWtPayableValue = ActualKms;
            }
            TruckHireCharge = (RateKg * ActualWtPayableValue);

        }
        //CalculateTotalTruckHireCharge();
    }

    private void CalculateTotalTruckHireCharge()
    {
        //Calculate TDS Amount on the Basis of TDS Percentage
       
       

            TDSAmount = Math.Round((TruckHireCharge) * TDSPercentage / 100, 2);
            //TotalTDSAmount = Math.Round(TDSAmount + SurchargeAmount + AddlSurchargeCessAmount + AddlEducationCessAmount);
            TotalTruckHireCharge = Math.Round(TruckHireCharge - TotalTDSAmount);

       
    }

    private void SetlinksForDetails()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;
        string path = "";
        string path1 = "";
        string path2 = "";

        fRights = uObj.getForm_Rights(63);
        fRights = uObj.getForm_Rights(64);
        bool can_view = fRights.canRead();

        if (can_view == true)
        {
            objDS = objVehicleHireBillDetailsPresenter.SetVehicleInfoOnVehicleChanged();
           
                int Driver1Id = Util.String2Int(objDS.Tables[0].Rows[0]["Driver_ID"].ToString());
                int Driver2Id = Util.String2Int(objDS.Tables[0].Rows[0]["Driver2_ID"].ToString());
                int CleanerId = Util.String2Int(objDS.Tables[0].Rows[0]["Cleaner_Id"].ToString());
                if (Driver1Id <= 0)
                {
                    lbtn_Driver1Details.Attributes.Add("Onclick","return ShowAlert();");
                }
                else 
                { path = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(63).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Driver1Id);
                  lbtn_Driver1Details.Attributes.Add("onclick", "return viewwindow('" + path + "')");
                }
            if (Driver2Id <= 0)
            {
                lbtn_Driver2Details.Attributes.Add("Onclick", "return ShowAlert();");
            }
            else
            {
                path1 = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(63).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Driver2Id);
                lbtn_Driver2Details.Attributes.Add("onclick", "return viewwindow('" + path1 + "')");
            }
            if (CleanerId <= 0)
            {
                lbtn_CleanerDetails.Attributes.Add("Onclick", "return ShowAlert();");

            }
            else
            {
                path2 = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(64).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(CleanerId);
                lbtn_CleanerDetails.Attributes.Add("onclick", "return viewwindow('" + path2 + "')");
            }
            
        }
        
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
        pc.AddAttributes(this.Controls);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        
        SetAllDDLSearch();

      

        if (!IsPostBack)
        {
            Wuc_VehicleDepartureTime.setFormat("24");
            VehicleDepartureTime = DateTime.Now.ToShortTimeString();
            TransitDays = 0;
            CommittedDelDate = DateTime.Now;
            
            if (keyID > 0)
            {                
                string script = "<script language='javascript'> " + "CalculateTruckHireCharge(0);EnabledDisabledControlOnFreightType(0)" + "</script>";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
            }
        }


        objVehicleHireBillDetailsPresenter = new VehicleHireBillDetailsPresenter(this, IsPostBack);
        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

     
        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                SetlinksForDetails();
            }
            else
            {
                if (IsHOBSeriesRequired == true && keyID <=0 )
                {
                    Get_Next_No();
                }
                else
                {
                    lbl_VehicleHireBillNoValue.Text = ObjCommon.Get_Next_Number();
                }
            }
        }
        
        if (Mode == "4")
        {
           
            //lbtn_AddDriver.Enabled = false;          
            WucHireBillDate.Enabled = false;
            TD_Calender.Visible = false;
        }


       

       // Wuc_VehicleDepartureTime.Visible = false;

    }

    private void VehicleIndexChange(object sender, EventArgs e)
    {

       objDS=objVehicleHireBillDetailsPresenter.SetVehicleInfoOnVehicleChanged();
       if (objDS.Tables[0].Rows.Count > 0)
       {
           SetlinksForDetails();
       }
        objVehicleHireBillDetailsPresenter.GetTDSPercent();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }    
    protected void ddl_BrokerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objVehicleHireBillDetailsPresenter.GetTDSPercent();      
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(1);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }
    protected void WucHireBillDate_SelectionChanged(object sender, EventArgs e)
    {
        CommittedDelDate = VehicleHireBillDate.AddDays(Convert.ToDouble(TransitDays));
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleHireBillDetailsPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    #endregion

}

