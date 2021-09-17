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
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 11/01/2008 
/// Description   : This Page is For ALS Operation
/// </summary> 
/// 
public partial class Operations_Outward_WucALS : System.Web.UI.UserControl,IALSView
{
    #region ClassVariables
    ALSPresenter objALSPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DataSet objDS = new DataSet();
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    #endregion

    #region ControlsValues
       
    public int VehicleCotegoryID
    {
        get { return Util.String2Int(ddl_VehicleCotegory.SelectedValue); }
        set { ddl_VehicleCotegory.SelectedValue = Util.Int2String(value); }
    }
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }
    public string VehicleCapacity
    {
        set { lbl_VehicleCapacity.Text = value; }
    }
    public DateTime ALSDate
    {
        set { ALS_Date.SelectedDate = value; }
        get { return ALS_Date.SelectedDate; }
    }
    public int SupervisorID
    {
        get { return Util.String2Int(ddl_Supervisior.SelectedValue); }
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
    public string ALSNo
    {
        set { lbl_ALSNo.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
      
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public void SetSupervisorID(string text, string value)
    {
        ddl_Supervisior.DataTextField = "Emp_Name";
        ddl_Supervisior.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Supervisior);
    }
   
    #endregion

    #region ControlsBind
       
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

    public void BindALSGrid()
    {
        dg_ALS.DataSource = SessionBindALSGrid;
        dg_ALS.DataBind();
    }

    public DataTable SessionBindALSGrid
    {
        get { return StateManager.GetState<DataTable>("BindALSGrid"); }
        set
        {
            StateManager.SaveState("BindALSGrid", value);

            if (StateManager.Exist("BindALSGrid"))
                BindALSGrid();
        }
    }

    public String ALSDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindALSGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "ALSGrid_Details";
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
        TextBox Txt_VehicleNo,Txt_Supervisor;

        errorMessage = "";
        lbl_Error_Client.Text = "";

        Txt_VehicleNo = (TextBox)WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");
        Txt_Supervisor = (TextBox)ddl_Supervisior.FindControl("txtBoxddl_Supervisior");

        if (Datemanager.IsValidProcessDate("OPR_ALS", ALSDate) == false)
        {
            errorMessage = "Invalid ALS Date";
        }       
        else if (VehicleCotegoryID <= 0)
        {
            errorMessage = "Please Select Vehicle Cotegory";
            ddl_VehicleCotegory.Focus();
        }
        else if (VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle";
            Txt_VehicleNo.Focus();
        }
        else if (Util.String2Int(hdn_Total_GC.Value) <= 0)
        {
            errorMessage = "Please Select Atleast One " + hdn_GCCaption.Value;
        }
        else if (grid_validation() == false)
        {
        }
        else if (SupervisorID <= 0)
        {
            errorMessage = "Please Select Supervisor";
            Txt_Supervisor.Focus();
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
        SessionBindALSGrid = null;
    }
    #endregion      

    #region OtherMethod

    private bool grid_validation()
    {
        TextBox txt_Lod_Art, txt_Lod_Wt;
        CheckBox chk;
        int i;
        bool ATS = true;

        if (Total_No_Of_GC > 0)
        {
            objDT = SessionBindALSGrid;

            for (i = 0; i <= dg_ALS.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_ALS.Items[i].FindControl("Chk_Attach");
                txt_Lod_Art = (TextBox)dg_ALS.Items[i].FindControl("txt_Loaded_Art");
                txt_Lod_Wt = (TextBox)dg_ALS.Items[i].FindControl("txt_Loaded_Wt");

                if (chk.Checked == true && Convert.ToDateTime(ALSDate) < Convert.ToDateTime(objDT.Rows[i]["GC_Date"]))
                {
                    errorMessage = "ALS Date Can't be less than " + hdn_GCCaption.Value + " Booking Date For " + hdn_GCCaption.Value + " : " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_ALS.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Lod_Art.Text) <= 0 || txt_Lod_Art.Text == string.Empty))
                {
                    errorMessage = "Loaded Articles is mandatory and must be greater than zero";
                    scm_ALS.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Lod_Art.Text) > Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()))
                {
                    errorMessage = "Loaded Articles should not be greater than balance Articles";
                    scm_ALS.SetFocus(txt_Lod_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Decimal(txt_Lod_Wt.Text) <= 0 || txt_Lod_Wt.Text == string.Empty))
                {
                    errorMessage = "Loaded Actual Wt. is mandatory and must be greater than zero";
                    scm_ALS.SetFocus(txt_Lod_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Lod_Wt.Text) > Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString()))
                {
                    errorMessage = "Loaded Actual Wt. should not be greater than balance Actual Wt.";
                    scm_ALS.SetFocus(txt_Lod_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && (Util.String2Int(txt_Lod_Art.Text) != Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()) && Util.String2Decimal(txt_Lod_Wt.Text) == Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString())))
                {
                    errorMessage = "Please Enter Proper Loaded Actual Wt.";
                    scm_ALS.SetFocus(txt_Lod_Wt);
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
        int i;

        if (dg_ALS.Items.Count > 0)
        {
            objDT = SessionBindALSGrid;

            for (i = 0; i <= dg_ALS.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_ALS.Items[i].FindControl("Chk_Attach");
                txt_Lod_Art = (TextBox)dg_ALS.Items[i].FindControl("txt_Loaded_Art");
                txt_Lod_Wt = (TextBox)dg_ALS.Items[i].FindControl("txt_Loaded_Wt");

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

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        lbl_tolalLodArt.Text = hdn_tolalLodArt.Value;
        lbl_tolalLodWt.Text = hdn_tolalLodWt.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_tolalLodArt.Value = "0";
        hdn_tolalLodWt.Value = "0";

        lbl_Total_GC.Text = "0";
        lbl_tolalLodArt.Text = "0";
        lbl_tolalLodWt.Text = "0";
    }

    private void Next_ALS_Number()
    {      
       ALSNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {       
        Assign_Hidden_Values_For_Reset();
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objALSPresenter.fillgrid();
        WucSelectedItems1.dtdetails = SessionBindALSGrid;
        Assign_Hidden_Values_To_TextBox();
        WucSelectedItems1.Get_Not_Selected_Items();
    }
       
    #endregion

    #region AddVehicle

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCapacity = WucVehicleSearch1.GetVehicleParameter("Vehicle_Capacity");
        WucVehicleSearch1.Can_View_Vehicle = true;
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            WucVehicleSearch1.Can_Add_Vehicle = false;
            TD_Calender.Visible = false;
            ALS_Date.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_Supervisior.DataTextField = "Emp_Name";
        ddl_Supervisior.DataValueField = "Emp_ID";

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Save_Print,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Save_Print, btn_Close));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save, btn_Save_Exit, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        SetStandardCaption();
        if (keyID > 0)
        {
            td_gccontrol.Style.Add("display", "none");
        }  

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();

            if (keyID <= 0)
            {
                Next_ALS_Number();
            }           
        }

        objALSPresenter = new ALSPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            WucVehicleSearch1.SetAutoPostBack = true;
            WucVehicleSearch1.VehicleCategoryIds = ddl_VehicleCotegory.SelectedValue;

            if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e);
            }
        }
        if (VehicleCotegoryID == 5)
        {
            WucVehicleSearch1.Can_Add_Vehicle = true;
        }

        Assign_Hidden_Values_To_TextBox();
    }
    private void SetStandardCaption()
    {
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter " + hdn_GCCaption.Value + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        Label1.Text = "Total " + hdn_GCCaption.Value + " :";
        dg_ALS.Columns[1].HeaderText = hdn_GCCaption.Value + "  No";
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objALSPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objALSPresenter.Save();
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objALSPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void ALS_Date_SelectionChanged(object sender, EventArgs e)
    {
        OnGetGCXML(sender, e);
    }
    protected void ddl_VehicleCotegory_SelectedIndexChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleCategoryIds = ddl_VehicleCotegory.SelectedValue;
        WucVehicleSearch1.VehicleID = 0;
        VehicleCapacity = "";

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
        }
        else
        {
            WucVehicleSearch1.Can_View_Vehicle = false;
        }

    }
    protected void dg_ALS_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid = "";
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


            if (CompanyManager.getCompanyParam().IsPartLoadingRequired == false)
            {
                disable_Textbox(Txt_Loded_Art, Txt_Loded_Wt);
            }
            else
            {
                calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";
                calculate_grid3 = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";

                Txt_Loded_Art.Attributes.Add("onblur", calculate_grid);
                Txt_Loded_Wt.Attributes.Add("onblur", calculate_grid1);

                Txt_Loded_Art.Attributes.Add("onfocus", calculate_grid2);
                Txt_Loded_Wt.Attributes.Add("onfocus", calculate_grid3);
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
    
}
