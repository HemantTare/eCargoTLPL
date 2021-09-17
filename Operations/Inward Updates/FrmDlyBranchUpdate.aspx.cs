using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 06-01-09
/// </summary>
/// 

public partial class Operations_Inward_Updates_FrmDlyBranchUpdate : ClassLibraryMVP.UI.Page, IDlyBranchUpdateView
{
    #region ClassVariables
    DlyBranchUpdatePresenter objDlyBranchUpdatePresenter;
    Common objCommon = new Common();
    //DataTable objDT = new DataTable();
    //DataSet objDS = new DataSet();
    string _GC_No_XML;

    #endregion

    #region Properties
    public int BranchId
    {
        get { return Wuc_Region_Area_Branch1.BranchID; }
    }
    public int NewDlyBranchId
    {
        get { return Util.String2Int(ddl_NewDeliveryBranch.SelectedValue); }
    }
    public int ServiceLocationId
    {
        get { return Util.String2Int(ddl_ServiceLocation.SelectedValue); }
    }

    public String Reason
    {
        get { return txt_Reason.Text; }
    }
    public DateTime TransactionDate
    {
        get { return WucDatePicker1.SelectedDate; }
    }
    #endregion

    #region ControlsBind

    public DataTable BindDDLDeliveryBranch
    {
        set
        {
            ddl_NewDeliveryBranch.DataTextField = "Branch_Name";
            ddl_NewDeliveryBranch.DataValueField = "Branch_Id";
            ddl_NewDeliveryBranch.DataSource = value;
            ddl_NewDeliveryBranch.DataBind();
            if (keyID <= 0)
            {
                ddl_NewDeliveryBranch.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
    }

    public DataTable BindDDLServiceLocation
    {
        set
        {
            ddl_ServiceLocation.DataTextField = "Service_Location_Name";
            ddl_ServiceLocation.DataValueField = "Service_Location_ID";
            ddl_ServiceLocation.DataSource = value;
            ddl_ServiceLocation.DataBind();
            if (keyID <= 0)
            {
                ddl_ServiceLocation.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
    }

    public void BindDGDeliveryBranch()
    {
        dg_DlyBranchUpdate.DataSource = SessionDGDeliveryBranch;
        dg_DlyBranchUpdate.DataBind();
    }

    public DataTable SessionDGDeliveryBranch
    {
        get { return StateManager.GetState<DataTable>("BindDlyBranchUpdate"); }
        set
        {
            StateManager.SaveState("BindDlyBranchUpdate", value);
            if (StateManager.Exist("BindDlyBranchUpdate"))
                BindDGDeliveryBranch();
        }
    }

    public String GetBranchXML
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
        if (NewDlyBranchId <= 0)
        {
            errorMessage = "Please Select New Delivery Branch";
            ddl_NewDeliveryBranch.Focus();
        }
        else if (TransactionDate > DateTime.Now)
        {
            errorMessage = "Please Select Valid Transaction Date";
        }
        else if (BranchId <= 0)
        {
            errorMessage = "Please Select Branch";
        }
        else if (dg_DlyBranchUpdate.Items.Count == 0)
        {
            errorMessage = "Please Insert Atleast One " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (Reason == string.Empty)
        {
            errorMessage = "Please Enter Reason";
            txt_Reason.Focus();
        }
        else if (GridValidation() == false)
        {
            _isValid = false;
        }
        else
            _isValid = true;

        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region control event

    protected void Page_Load(object sender, EventArgs e)
    {
        objDlyBranchUpdatePresenter = new DlyBranchUpdatePresenter(this, IsPostBack);
        //objCommon.Disable_save_button_on_click(Page, btn_Save, "ValidateUI()");
        btn_Save.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        if (IsPostBack == false)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;
            objCommon.SetStandardCaptionForGrid(dg_DlyBranchUpdate);
        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(OnGetGCXML);
        SetStandardCaption();
        WucSelectedItems1.SetsmallTextboxWidth();
        if (!IsPostBack)
        {
            hdn_AUSCaption.Value = objCommon.Get_Values_Where("EC_Master_Company_Parameters", "AUS_Caption", "", "AUS_Caption", false).Tables[0].Rows[0]["AUS_Caption"].ToString();
        }
    }

    protected void ddl_NewDeliveryBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDlyBranchUpdatePresenter.ServiceLocation_FillValues();
        if (ServiceLocationId > 0 && NewDlyBranchId > 0)
        {
            dg_DlyBranchUpdate.Visible = true;
            OnGetGCXML(sender, e);
        }
        else
            dg_DlyBranchUpdate.Visible = false;
    }

    protected void ddl_ServiceLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ServiceLocationId > 0 && NewDlyBranchId > 0)
        {
            dg_DlyBranchUpdate.Visible = true;
            OnGetGCXML(sender, e);
        }
        else
            dg_DlyBranchUpdate.Visible = false;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objDlyBranchUpdatePresenter.Save();
    }
    #endregion

    #region other Method
    private void SetStandardCaption()
    {
        WucSelectedItems1.SetFoundCaption = "Enter  " + CompanyManager.getCompanyParam().GcCaption + "  Nos.:";
        WucSelectedItems1.SetNotFoundCaption = CompanyManager.getCompanyParam().GcCaption + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption;
    }

    private bool GridValidation()  //added Ankit : 23-02-09 
    {
        bool ATS = true;
        Label lbl_AUSDate;
        string GCNo;
        int i = 0;

        if (dg_DlyBranchUpdate.Items.Count > 0)
        {
            for (i = 0; i <= dg_DlyBranchUpdate.Items.Count - 1; i++)
            {
                GCNo = dg_DlyBranchUpdate.Items[i].Cells[0].Text;
                lbl_AUSDate = (Label)dg_DlyBranchUpdate.Items[i].FindControl("lbl_AUSDate");

                if (TransactionDate < Convert.ToDateTime(lbl_AUSDate.Text))
                {
                    errorMessage = "Transaction Date can't be less than " + hdn_AUSCaption.Value + " Date(" + lbl_AUSDate.Text + ") For " + CompanyManager.getCompanyParam().GcCaption + " : " + GCNo;
                    ATS = false;
                    break;
                }
            }
        }

        return ATS;
    }

    public void ClearVariables() // added Ankit
    {
        SessionDGDeliveryBranch = null;
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objDlyBranchUpdatePresenter.fillgrid();
        if (SessionDGDeliveryBranch.Rows.Count > 0)
        {
            WucSelectedItems1.dtdetails = SessionDGDeliveryBranch;
            pnl_DlyBranch.Visible = true;
        }
        else
            pnl_DlyBranch.Visible = false;
        WucSelectedItems1.Get_Not_Selected_Items();
    }
    #endregion

}