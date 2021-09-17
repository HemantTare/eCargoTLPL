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
using System.Drawing;
using System.Text;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;

using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;

using System.Net;
using System.IO;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 22/10/2008
/// Description   : This Page is For Menifest Operation
/// </summary>
/// 
public partial class Operations_Outward_WucMenifest : System.Web.UI.UserControl,IMenifestView
{
    #region ClassVariables
    MenifestPresenter objMenifestPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DataSet objDS = new DataSet();
    string _flag = "";
    string Mode = "0";
    bool _IsFrom_Edit = true;
    TextBox txt_Menifest_To;
    string _GC_No_XML;
    PageControls pc = new PageControls();

    #endregion

    #region ControlsValues
  
    public int MenifestTypeID
    {
        get { return Util.String2Int(ddl_MenifestType.SelectedValue); }
        set { ddl_MenifestType.SelectedValue = Util.Int2String(value); }
    }
    public int MenifestToID
    {
        get { return Util.String2Int(hdn_MenifestToId.Value); }
        set { hdn_MenifestToId.Value = Util.Int2String(value); }
    }
    public int LoadedById
    {
        get { return Util.String2Int(ddl_Loaded_By.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_Loaded_By.SelectedValue); }
    }
    public string MenifestTo
    {
        get
        {
            if (MenifestTypeID == 1)
               return txtSearch_MenifestTo.Text;
            else
                return txt_MenifestTo.Text;
        }
        set { 
                if (MenifestTypeID == 1)
                    txtSearch_MenifestTo.Text = value; 
                else
                    txt_MenifestTo.Text = value; 
        }
    } 
    public int VehicleCotegoryID
    {
        get { return Util.String2Int(ddl_VehicleCotegory.SelectedValue); }
        set { ddl_VehicleCotegory.SelectedValue = Util.Int2String(value); }
    }
    public int ALSID
    {
        get { return Util.String2Int(ddl_ALSNo.SelectedValue); }
        set { ddl_ALSNo.SelectedValue = Util.Int2String(value); }
    }
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }
    public string  VehicleCapacity
    {
        set { lbl_VehicleCapacity.Text = value; }
    }

    public String Number_Part4
    {
        set { hdn_Number_Part4.Value = value.ToString(); }
        get { return hdn_Number_Part4.Value; }
    }

    public String ShortUrl
    {
        set { hdn_ShortUrl.Value = value.ToString(); }
        get { return hdn_ShortUrl.Value; }
    }

    public DateTime MenifestDate
    {
        set { Memo_Date.SelectedDate = value; }
        get { return Memo_Date.SelectedDate; }
    }
    public string ArrivalDeliveryTime
    {
        set { TimePicker1.setTime(value); }
        get { return TimePicker1.getTime(); }
    }
    public DateTime ArrivalDeliveryDate
    {
        set { ArrivalDelivery_Date.SelectedDate = value; }
        get { return ArrivalDelivery_Date.SelectedDate; }
    }
    public DateTime ALSDate
    {
        set { hdn_ALSDATE.Value = Convert.ToString(value); }
        get { return Convert.ToDateTime(hdn_ALSDATE.Value); }
    }
    public decimal Book_ActualWt
    {
        set { hdn_Book_ActualWt.Value = Util.Decimal2String(value); }
        get {return Util.String2Decimal(hdn_Book_ActualWt.Value); }
    }
    public decimal Book_ToPayCollection
    {
        set { hdn_Book_ToPayCollection.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Book_ToPayCollection.Value); }
    }
    public decimal Cros_ActualWt
    {
        set { hdn_Cros_ActualWt.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Cros_ActualWt.Value); }
    }
    public decimal Cros_ToPayCollection
    {
        set { hdn_Cros_ToPayCollection.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Cros_ToPayCollection.Value); }
    }
    public decimal Total_ActualWt
    {
        set { hdn_Total_ActualWt.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Total_ActualWt.Value); }
    }
    public decimal Total_ToPayCollection
    {
        set { hdn_Total_ToPayCollection.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_Total_ToPayCollection.Value); }
    }
    public int Total_Loded_Articles
    {
        set { hdn_tolalLodArt.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_tolalLodArt.Value); }
    }
    public decimal Total_Loded_Weight
    {
        set { hdn_tolalLodWt.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_tolalLodWt.Value); }
    }
    public int Total_No_Of_GC
    {
        set { hdn_Total_GC.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Total_GC.Value); }
    }
    public string MenifestNo
    {
        set { lbl_MenifestNo.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    public bool IsFrom_Edit
    {
        get { return _IsFrom_Edit; }
    } 
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    //public void SetMenifestToId(string text, string value)
    //{
    //    ddl_MenifestTo.DataTextField = "Branch_Name";
    //    ddl_MenifestTo.DataValueField = "Branch_ID";

    //    Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_MenifestTo);
    //}
    public void SetLoadedById(string text, string value)
    {
        ddl_Loaded_By.DataTextField = "Emp_Name";
        ddl_Loaded_By.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Loaded_By);
    }
    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Next_No.Value); }
    }

    public string MEMO_No
    {
        set
        {
            hdn_Padded_Next_No.Value = value;
            lbl_MenifestNo.Text = value;
        }
        get { return hdn_Padded_Next_No.Value; }
    }

    public int Document_Series_Allocation_ID
    {
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
    }
    
    #endregion

    #region ControlsBind

    public DataTable BindMenifestType
    {
        set
        {
            ddl_MenifestType.DataTextField = "Memo_Type";
            ddl_MenifestType.DataValueField = "Memo_Type_Id";
            ddl_MenifestType.DataSource = value;
            ddl_MenifestType.DataBind();
        }
    }   
    public DataTable BindVehicleCotegory
    {
        set
        {
            ddl_VehicleCotegory.DataTextField = "Vehicle_Category";
            ddl_VehicleCotegory.DataValueField = "Vehicle_Category_ID";
            ddl_VehicleCotegory.DataSource = value;
            ddl_VehicleCotegory.DataBind();
            if (keyID <= 0)
            {
                ddl_VehicleCotegory.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
    }

    public DataTable BindALSNo
    {
        set
        {
            ddl_ALSNo.DataTextField = "ALS_No";
            ddl_ALSNo.DataValueField = "ALS_ID";
            ddl_ALSNo.DataSource = value;
            ddl_ALSNo.DataBind();
            if (keyID <= 0)
            {
                ddl_ALSNo.Items.Insert(0, new ListItem("-- Select ALS No --", "0"));
            }
        }
    }  
    private void Fill_Time()
    {
        string current_time = DateTime.Now.ToShortTimeString();

        TimePicker1.setFormat("24");
        TimePicker1.setTime(current_time);
    }

    public void BindMenifestGrid()
    {
        dg_Memo.DataSource = SessionBindMenifestGrid;
        dg_Memo.DataBind();
    }

    public DataTable SessionBindMenifestGrid
    {
        get { return StateManager.GetState<DataTable>("BindMenifestGrid"); }
        set
        { 
            StateManager.SaveState("BindMenifestGrid", value);

            if (StateManager.Exist("BindMenifestGrid"))
                BindMenifestGrid();
        }
    }

    public String MenifestDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindMenifestGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "MenifestGrid_Details";
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
        TextBox Txt_VehicleNo, Txt_Loaded_By;

        errorMessage = "";
        lbl_Error_Client.Text = "";

        //txt_Menifest_To = (TextBox)ddl_MenifestTo.FindControl("txtBoxddl_MenifestTo");
        Txt_Loaded_By = (TextBox)ddl_Loaded_By.FindControl("txtBoxddl_Loaded_By");
        Txt_VehicleNo = (TextBox)WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");
                      
        if (MenifestTypeID == 0)
        {
            errorMessage = "Please Select Manifest Type";//GetLocalResourceObject("Msg_ddl_MenifestType").ToString();
            ddl_MenifestType.Focus();
        }
        else if (Datemanager.IsValidProcessDate("OPR_MEMO", MenifestDate) == false)
        {
            errorMessage = "Please select valid Manifest Date"; //GetLocalResourceObject("Msg_dtp_MenifestDate").ToString();
        }
        else if (MenifestTypeID == 1 && MenifestToID <= 0)
        {
            errorMessage = "Please Select Manifest To";//GetLocalResourceObject("Msg_ddl_MenifestTo").ToString();
            txtSearch_MenifestTo.Focus();
        }
        else if (MenifestTypeID == 2 && MenifestTo == string.Empty)
        {
            errorMessage = "Please Enter Manifest To";//GetLocalResourceObject("Msg_txt_MenifestTo").ToString();
            txt_MenifestTo.Focus();
        }
        //else if (VehicleCotegoryID <= 0)
        //{
        //    errorMessage = "Please Select Vehicle Category";//GetLocalResourceObject("Msg_ddl_VehicleCotegory").ToString();
        //    ddl_VehicleCotegory.Focus();
        //}     
        else if(VehicleID <= 0)
        {
            errorMessage = "Please enter Vehicle No.";//GetLocalResourceObject("Msg_ddl_Vehicle").ToString();
            Txt_VehicleNo.Focus();
        }
        else if (CompanyManager.getCompanyParam().IsALSRequired == true && ALSID <= 0)
        {
            errorMessage = "Please Select ALS No";
            ddl_ALSNo.Focus();
        }
        else if (CompanyManager.getCompanyParam().IsALSRequired == true && keyID > 0 && MenifestDate < ALSDate)
        {
            errorMessage = "Menifest date can't be less than ALS date : " + ALSDate.ToString("dd MMM yyyy");
        }
        else if (LoadedById <= 0 && pc.Control_Is_Mandatory(tr_loading_supervisor) == true)
        {
            errorMessage = "Please Select Loaded By";
            Txt_Loaded_By.Focus();
        }
        else if (Util.String2Int(hdn_Total_GC.Value) <= 0)
        {
            errorMessage = "Please Select Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (grid_validation() == false)
        {
        }
        else if (Convert.ToDateTime(ArrivalDeliveryDate) < Convert.ToDateTime(MenifestDate))
        {
            errorMessage = "Schedule Arrival Date must be greater than or equal to Manifest Date.";//GetLocalResourceObject("Msg_DeliveryDate_validation").ToString();
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
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    //Added : Anita On : 19 Feb 09
    public void ClearVariables()
    {
        SessionBindMenifestGrid = null;
    }
    #endregion      

    #region grid validation
      
    private bool grid_gc_validation()
    {
        bool ATS = true;

        objDS = objMenifestPresenter.gc_grid_validation();

        if (Util.String2Bool(objDS.Tables[0].Rows[0]["IsDuplicate"].ToString()) == true)
        {
            errorMessage = "Manifest Already Prepare For Same Vehicle No,Same Manifest To Branch and Same Manifest Type For Gc No : " + objDS.Tables[0].Rows[0]["GCNo"].ToString();
            ATS = false;
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }

    private bool grid_validation()
    {
        TextBox txt_Lod_Art, txt_Lod_Wt;
        CheckBox chk;
        int i;
        bool ATS = true;

        if(Total_No_Of_GC > 0)
        {
            objDT = SessionBindMenifestGrid;

            for (i = 0; i <= dg_Memo.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Memo.Items[i].FindControl("Chk_Attach");
                txt_Lod_Art = (TextBox)dg_Memo.Items[i].FindControl("txt_Loaded_Art");
                txt_Lod_Wt = (TextBox)dg_Memo.Items[i].FindControl("txt_Loaded_Wt");

                if (grid_gc_validation() == false)
                {
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Convert.ToDateTime(MenifestDate) < Convert.ToDateTime(objDT.Rows[i]["GC_Date"]))
                {
                    errorMessage = "Manifest Date Can't be less than GC Booking Date" + "  For GC : " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_memo.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Lod_Art.Text) <= 0 || txt_Lod_Art.Text == string.Empty))
                {
                    errorMessage = "Loaded Articles is mandatory and must be greater than zero";//GetLocalResourceObject("Msg_Article_Zero_validation").ToString();
                    scm_memo.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Lod_Art.Text) > Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()))
                {
                    errorMessage = "Loaded Articles should not be greater than balance Articles";//GetLocalResourceObject("Msg_Article_greater_validation").ToString();
                    scm_memo.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Decimal(txt_Lod_Wt.Text) <= 0 || txt_Lod_Wt.Text == string.Empty))
                {
                    errorMessage = "Loaded Actual Wt. is mandatory and must be greater than zero";//GetLocalResourceObject("Msg_ActualWt_Zero_validation").ToString();
                    scm_memo.SetFocus(txt_Lod_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Lod_Wt.Text) > Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString()))
                {
                    errorMessage = "Loaded Actual Wt. should not be greater than balance Actual Wt.";//GetLocalResourceObject("Msg_ActualWt_greater_validation").ToString();
                    scm_memo.SetFocus(txt_Lod_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Lod_Art.Text) != Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()) && Util.String2Decimal(txt_Lod_Wt.Text) == Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString())))
                {
                    errorMessage = "Please Enter Proper Loaded Actual Wt.";
                    scm_memo.SetFocus(txt_Lod_Wt);
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
        Total_No_Of_GC = 0;
        CheckBox chk;
        TextBox txt_Lod_Art, txt_Lod_Wt;
        int  i;

        if (dg_Memo.Items.Count > 0)
        {
            objDT = SessionBindMenifestGrid;

            for (i = 0; i <= dg_Memo.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Memo.Items[i].FindControl("Chk_Attach");
                txt_Lod_Art = (TextBox)dg_Memo.Items[i].FindControl("txt_Loaded_Art");
                txt_Lod_Wt = (TextBox)dg_Memo.Items[i].FindControl("txt_Loaded_Wt");

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                }
                objDT.Rows[i]["Att"] = chk.Checked;
                objDT.Rows[i]["Loaded_Articles"] = Util.String2Int(txt_Lod_Art.Text);
                objDT.Rows[i]["Loaded_Weight"] = Util.String2Decimal(txt_Lod_Wt.Text);
            }
        }

    }

    #endregion

    #region grid validation
   
    private void Assign_Hidden_Values_To_TextBox()
    {
        txt_Book_ActualWt.Text = hdn_Book_ActualWt.Value;
        txt_Book_ToPayCollection.Text = hdn_Book_ToPayCollection.Value;
        txt_Cros_ActualWt.Text = hdn_Cros_ActualWt.Value;
        txt_Cros_ToPayCollection.Text = hdn_Cros_ToPayCollection.Value;
        txt_Total_ActualWt.Text = hdn_Total_ActualWt.Value;
        txt_Total_ToPayCollection.Text = hdn_Total_ToPayCollection.Value;
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        lbl_tolalLodArt.Text = hdn_tolalLodArt.Value;
        lbl_tolalLodWt.Text = hdn_tolalLodWt.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Book_ActualWt.Value = "0";
        hdn_Book_ToPayCollection.Value = "0";
        hdn_Cros_ActualWt.Value = "0";
        hdn_Cros_ToPayCollection.Value = "0";
        hdn_Total_ActualWt.Value = "0";
        hdn_Total_ToPayCollection.Value = "0";
        hdn_Total_GC.Value = "0";
        hdn_tolalLodArt.Value = "0";
        hdn_tolalLodWt.Value = "0";

        txt_Book_ActualWt.Text = "0";
        txt_Book_ToPayCollection.Text = "0";
        txt_Cros_ActualWt.Text = "0";
        txt_Cros_ToPayCollection.Text = "0";
        txt_Total_ActualWt.Text = "0";
        txt_Total_ToPayCollection.Text = "0";
        lbl_Total_GC.Text = "0";
        lbl_tolalLodArt.Text = "0";
        lbl_tolalLodWt.Text = "0";
    }

    private void Next_Menifest_Number()
    {
        if (UserManager.getUserParam().IsMemoSeriesReq == true && keyID <= 0)
        {
            Get_Next_No();
        }
        else
        {
            MenifestNo = objComm.Get_Next_Number();
        }
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        //if (WucSelectedItems1.EnterItem != string.Empty)
        //{
            _IsFrom_Edit = false;
            Assign_Hidden_Values_For_Reset();
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objMenifestPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindMenifestGrid;
            Assign_Hidden_Values_To_TextBox();
            WucSelectedItems1.Get_Not_Selected_Items();



            //string script = "<script language='javascript'> " + "Check_All(" + chk_AddLR.ClientID + "," + dg_Memo.ClientID +");" + "</script>";
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Check_All()", script, false);
        //}
    }

    public void Get_Next_No()
    {

        int _Document_Allocation_ID = 0;
        int _Start_No = 0;
        int _End_No = 0;
        int _Next_No = 0;
        string _Padded_Next_No = "";

        objComm.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 4);
        Document_Series_Allocation_ID = _Document_Allocation_ID;
        Next_No = _Next_No;

        if (_Next_No <= 0)
        {
            Raj.EC.Common.DisplayErrors(1013);
        }

        _Padded_Next_No = _Next_No.ToString("0000000");
        MEMO_No = _Padded_Next_No;

    }
     #endregion
       
    #region control event

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            WucVehicleSearch1.Can_Add_Vehicle = false;
            td_cal.Visible = false;
            Memo_Date.Enabled = false;
            ArrivalDelivery_Date.Enabled = false;

            btn_SendSMS.Visible = true;
            btn_SendSMS.Enabled = true;
        }

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        SetPostBackValues();
        SetALSPostBackValues();

        if (!IsPostBack)
        {
            chk_AddLR.Style.Add("display", "none");
            Assign_Hidden_Values_For_Reset();
            hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId);
            hdn_ALS_Req.Value = Convert.ToString(CompanyManager.getCompanyParam().IsALSRequired);

            Fill_Time();
            if (keyID <= 0)
            {
                Next_Menifest_Number();
            }
         }

        objMenifestPresenter = new MenifestPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            WucVehicleSearch1.SetAutoPostBack = true;
            WucVehicleSearch1.VehicleCategoryIds = "";
            WucVehicleSearch1.TransactionDate = Memo_Date.SelectedDate;
            if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e);
            }
            
        }

        if (VehicleCotegoryID == 5)
        {
            WucVehicleSearch1.Can_Add_Vehicle = true;
        }

        if (keyID > 0)
        {
            td_gccontrol.Style.Add("display", "none");
        }
        lst_MenifestTo.Style.Add("visibility", "hidden");
        Assign_Hidden_Values_To_TextBox();
    }

    private void SetPostBackValues()
    {
        //ddl_MenifestTo.DataTextField = "Branch_Name";
        //ddl_MenifestTo.DataValueField = "Branch_ID";

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Save_Print,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Exit,btn_Save,btn_Save_Print,btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save,btn_Save_Exit,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        Page.MaintainScrollPositionOnPostBack = true;
        SetStandardCaption();
    }

    private void SetALSPostBackValues()
    {
        if (CompanyManager.getCompanyParam().IsALSRequired == true)
        {
            tr_ALS1.Visible = true;
            tr_ALS2.Visible = true;
            tr_ALS3.Visible = true;
            td_gccontrol.Style.Add("display", "none");
        }
        else
        {
            tr_ALS1.Visible = false;
            tr_ALS2.Visible = false;
            tr_ALS3.Visible = false;
            td_gccontrol.Style.Add("display", "inline");
        }

        if (CompanyManager.getCompanyParam().IsALSRequired == true && keyID > 0)
        {
            ddl_ALSNo.Enabled = false;
            ddl_MenifestType.Enabled = false;
            Memo_Date.AutoPostBackOnSelectionChanged = false;
            ddl_VehicleCotegory.Enabled = false;
            WucVehicleSearch1.SetEnabled = false;
            ddl_Loaded_By.Enabled = false;
        }       
    }
    #endregion

    #region save control event

    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        dg_Memo.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objMenifestPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objMenifestPresenter.Save();
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objMenifestPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_SendSMS_Click(object sender, EventArgs e)
    {
        Get_Vehicle_Tracking_SMS(keyID, ShortUrl);
    }

    protected void dg_Memo_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid ="";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        string calculate_grid3 = "";
        CheckBox chk_Attach;
        TextBox Txt_Loded_Art, Txt_Loded_Wt;

        if (e.Item.ItemIndex != -1)
        {
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
            Txt_Loded_Art = (TextBox)e.Item.FindControl("txt_Loaded_Art");
            Txt_Loded_Wt = (TextBox)e.Item.FindControl("txt_Loaded_Wt");

            if (CompanyManager.getCompanyParam().IsALSRequired == false)
            {
                Txt_Loded_Art.Enabled = true;
                Txt_Loded_Wt.Enabled = true;

                calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";
                calculate_grid3 = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";

                Txt_Loded_Art.Attributes.Add("onblur", calculate_grid);
                Txt_Loded_Wt.Attributes.Add("onblur", calculate_grid1);

                Txt_Loded_Art.Attributes.Add("onfocus", calculate_grid2);
                Txt_Loded_Wt.Attributes.Add("onfocus", calculate_grid3);           
            }
            else
            {
                Txt_Loded_Art.Enabled = false;
                Txt_Loded_Wt.Enabled = false;
            }

            if (CompanyManager.getCompanyParam().IsALSRequired == false && CompanyManager.getCompanyParam().IsPartLoadingRequired == true)
            {
                Txt_Loded_Art.Enabled = true;
                Txt_Loded_Wt.Enabled = true;
            }
            else
            {
                disable_Textbox(Txt_Loded_Art, Txt_Loded_Wt);
            }

        }
    }

    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }

   #endregion

    #region on selection changed
    protected void ddl_VehicleCotegory_SelectedIndexChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleCategoryIds = ddl_VehicleCotegory.SelectedValue;
        WucVehicleSearch1.VehicleID = 0;

        VehicleCapacity = "";

        if (CompanyManager.getCompanyParam().IsALSRequired == true && keyID < 0)
        {
            objMenifestPresenter.fillALSNO();

            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objMenifestPresenter.fillMemoDetailsOnALSSelection();
        }

        if (VehicleCotegoryID == 5)
        {
            WucVehicleSearch1.Can_Add_Vehicle = true;
        }
        else
        {
            WucVehicleSearch1.Can_Add_Vehicle = false;
        }
        if (VehicleID > 0)
        {
            WucVehicleSearch1.Can_View_Vehicle = true;
            Set_ShduledarrivalDate();
        }
        else
        {
            WucVehicleSearch1.Can_View_Vehicle = false;
            ArrivalDeliveryDate = MenifestDate;
        }
    }
    protected void Memo_Date_SelectionChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.TransactionDate = Memo_Date.SelectedDate;
        WucVehicleSearch1.callVehicleSearch();

        if (CompanyManager.getCompanyParam().IsALSRequired == false)
        {
            OnGetGCXML(sender, e);
        }
        else if (CompanyManager.getCompanyParam().IsALSRequired == true && keyID < 0)
        {
            objMenifestPresenter.fillALSNO();

            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objMenifestPresenter.fillMemoDetailsOnALSSelection();
        }

        Set_ShduledarrivalDate();
    }
    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCapacity = WucVehicleSearch1.GetVehicleParameter("Vehicle_Capacity");

        Number_Part4 = WucVehicleSearch1.GetVehicleParameter("Number_Part4");

        ShortUrl = WucVehicleSearch1.GetVehicleParameter("ShortUrl");

        //WucVehicleSearch1.Can_View_Vehicle = true;
        if (CompanyManager.getCompanyParam().IsALSRequired == true)
        {
            //WucVehicleSearch1.Can_Add_Vehicle = false;
            objMenifestPresenter.fillALSNO();
            ddl_ALSNo_SelectedIndexChanged(sender, e);
        }
        else if (VehicleCotegoryID == 5)
        {
            WucVehicleSearch1.Can_Add_Vehicle = true;
        }

        Set_ShduledarrivalDate();
    }  

    protected void ddl_MenifestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CompanyManager.getCompanyParam().IsALSRequired == false)
        {
            OnGetGCXML(sender, e);
        }
        else
        {
            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();
            _IsFrom_Edit = false;
            objMenifestPresenter.fillMemoDetailsOnALSSelection();
        }

        if (MenifestTypeID == 1)
        {
            Set_ShduledarrivalDate();
        }
        else
        {
            ArrivalDeliveryDate = MenifestDate;
        }
    }

    protected void ddl_ALSNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            Assign_Hidden_Values_For_Reset();
            Assign_Hidden_Values_To_TextBox();

            objMenifestPresenter.fillMemoDetailsOnALSSelection();
        }
    }

    protected void ddl_MenifestTo_TxtChange(object sender, EventArgs e)
    {
        if (MenifestToID > 0)
        {
            Set_ShduledarrivalDate();
        }
        else
        {
            ArrivalDeliveryDate = MenifestDate;
        }
        UpdatePanel9.Update();
    }

    private void Set_ShduledarrivalDate()
    {
        ArrivalDeliveryDate = objMenifestPresenter.Set_SheduledArrival_Date();
        UpdatePanel9.Update();
    }


    private void Get_Vehicle_Tracking_SMS(int MemoID, string ShortUrl)
    {
        
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] sqlPara = { objdal.MakeInParams("@MemoID", SqlDbType.Int, 0, MemoID),
             objdal.MakeInParams("@ShortUrl", SqlDbType.VarChar , 500, ShortUrl)};
        objdal.RunProc("Ec_Opr_Get_Memo_SMS_MSG", sqlPara, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable SessionDT = (DataTable)ds.Tables[0];

            int i;
            string MobileNo, SMSMessage;

            if (SessionDT.Rows.Count > 0)
            {
                for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
                {
                    MobileNo = "";
                    SMSMessage = "";
                    MobileNo = Convert.ToString(SessionDT.Rows[i]["Mobile_No"]);
                    SMSMessage = Convert.ToString(SessionDT.Rows[i]["SMSMessage"]);
                    Sent_Vehicle_Tracking_SMS(MobileNo, SMSMessage);
                }
            }
        }
    }

    private void Sent_Vehicle_Tracking_SMS(string MobileNo, string SMSMessage)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            String sendToPhoneNumber = MobileNo;
            string msg = SMSMessage;

            if (ValidateMobileDetails(sendToPhoneNumber, msg))
            {
                String userid = "2000126072";
                String passwd = "Rajan@1234";

                String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                request = WebRequest.Create(url);

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                }
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();
            }
        }
        catch (Exception exp)
        {
            string excep = exp.ToString();
        }
        finally
        {
            if (response != null)
                response.Close();
        }
    }
    private bool ValidateMobileDetails(String sendToPhoneNumber, string msg)
    {
        bool Is_Valid = false;
        if (sendToPhoneNumber == "0" || msg == "")
        {
            Is_Valid = false;
        }
        else
        {
            Is_Valid = true;
        }

        return Is_Valid;
    }


     #endregion   
}
