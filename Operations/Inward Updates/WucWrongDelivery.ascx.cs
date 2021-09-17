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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;

public partial class Operations_Inward_Updates_WucWrongDelivery : System.Web.UI.UserControl, IWrongDeliveryView
{
    WrongDeliveryPresenter objWrongDeliveryPresenter;
    Common objCommon = new Common();
    public EventHandler OnGetDetails;
    string _flag = "";
    
    #region Control Values


    public string Flag
    {
        get { return _flag; }
    }
    public string GCNo 
    {
        get { return txt_GCNo.Text.Trim() == string.Empty ? "0" : txt_GCNo.Text.Trim(); }
        set { txt_GCNo.Text = value; }
    }

    public int Booking_Branch_ID
    {
        get { return Util.String2Int(hdfn_Booking_Branch_ID.Value); }
        set { hdfn_Booking_Branch_ID.Value = Util.Int2String(value); }
    }
    public string BookingBranch 
    {
        get { return txt_BookingBranch.Text.Trim(); }
        set { txt_BookingBranch.Text = value; }
    }
    
    public string DeliveryBranch 
    {
        get { return txt_DeliveryBranch.Text.Trim(); }
        set { txt_DeliveryBranch.Text = value; }
    }
    public int Delivery_Branch_Id
    {
        get { return Util.String2Int(hdfn_Delivery_Branch_ID.Value); }
        set { hdfn_Delivery_Branch_ID.Value = Util.Int2String(value); }
    }
    
    public string InformedBy 
    {
        get { return txt_InformedBy.Text.Trim(); }
        set { txt_InformedBy.Text = value; }
    }
    
    public string InformedContactNo 
    {
        get { return txt_InformedContactNo.Text.Trim(); }
        set { txt_InformedContactNo.Text = value; }
    }
     
    public string CollectedBy 
    {
        get { return txt_CollectedBy.Text.Trim(); }
        set { txt_CollectedBy.Text = value; }
    }
    
    public string CollectedContactNo 
    {
        get { return txt_CollectedContactNo.Text.Trim(); }
        set { txt_CollectedContactNo.Text = value; }
    }

    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }
    public int Received_Condition_ID
    {
        get { return Util.String2Int(ddl_Received_Condition.SelectedValue); }
        set { ddl_Received_Condition.SelectedValue = Util.Int2String(value); }
    }
    public string Received_ConditionDescription
    {
        get { return ddl_Received_Condition.SelectedItem.Text; }
        set { ddl_Received_Condition.SelectedItem.Text = value; }
    } 
    
    public string Description 
    {
        get { return txt_Description.Text.Trim(); }
        set { txt_Description.Text = value; }
    }
    public bool IsToPay
    {
        get { return Convert.ToBoolean(ViewState["_IsToPay"]) ; }
        set { ViewState["_IsToPay"] = value; }
    }
    public bool IsCheque
    {
        get { return chkIsCheque.Checked; }
        set { chkIsCheque.Checked = value; }
    } 
    public int ChequeNo
    {
        get 
        {
            if (IsCheque == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(txtChequeNo.Text.Trim());
            }
        }
        set { txtChequeNo.Text =  Util.Int2String(value); } 
    }
    public DateTime ChequeDate
    {
        set { dtpChequeDate.SelectedDate = value; }
        get { return dtpChequeDate.SelectedDate; }
    }
        
    
    public int GC_ID 
    {
        get { return hdn_GC_ID.Value == "0" ? 0 : Util.String2Int(hdn_GC_ID.Value); }
        set { hdn_GC_ID.Value = value.ToString();}
    }  
       
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set{lbl_error.Text = value;}
    }

    public DataTable BindReceived_Condition
    {
        set
        {
            ddl_Received_Condition.DataTextField = "Received_Condition";
            ddl_Received_Condition.DataValueField = "Received_Condition_ID";
            ddl_Received_Condition.DataSource = value;
            ddl_Received_Condition.DataBind();
            ddl_Received_Condition.Items.Insert(0, new ListItem("-- Select One --", "0"));
            
        }
    }
 
    private void SetPostBackValues()
    {
        //WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

    }
    private void On_PageLoad()
    {
        WucVehicleSearch1.Can_Add_Vehicle = false;
        WucVehicleSearch1.Can_View_Vehicle = false;

        if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
        {
            txt_GCNo.MaxLength = 15;
        }
        else
        {
            txt_GCNo.MaxLength = Util.String2Int(objCommon.Get_Values_Where("EC_Master_Company_GC_Parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString());
            txt_GCNo.Attributes.Add("onkeypress", "return Only_Integers(" + txt_GCNo.ClientID + ",event)");
        }

        
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        txt_GCNo.Focus();
 
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        WucVehicleSearch1.Can_Add_Vehicle = false; //added by sushant 11-11-2013
        WucVehicleSearch1.Can_View_Vehicle = false; //added by sushant 11-11-2013 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPostBackValues();
        if (!IsPostBack)
        {
            On_PageLoad(); 
        }

        if (keyID > 0)
        {
            btn_GetDetails.Enabled = false;
            txt_GCNo.Enabled = false;  
        }
        objWrongDeliveryPresenter = new WrongDeliveryPresenter(this, IsPostBack);
        SetStandardCaption();
        CheckIsCheque();
    }
    private void HideUnHideIsToPay()
    {
        if (IsToPay == true)
        {
            chkIsCheque.Visible = true;
            chkIsCheque.Enabled = true;
            lblIsCheque.Visible = true;

            txtChequeNo.Enabled = true;
            dtpChequeDate.Enabled = true;

            lblChequeNo.Visible = true;
            lblChequeDate.Visible = true;
            txtChequeNo.Visible = true;
            dtpChequeDate.Visible = true;
            ChequeDateButton.Visible = true;
        }
        else
        {
            chkIsCheque.Visible = false;
            chkIsCheque.Enabled = false;
            lblIsCheque.Visible = false;

            IsCheque = false;
            ChequeNo = 0;
            ChequeDate = DateTime.Now.Date;

            txtChequeNo.Enabled = false;
            dtpChequeDate.Enabled = false;

            lblChequeNo.Visible = false;
            lblChequeDate.Visible = false;
            txtChequeNo.Visible = false;
            dtpChequeDate.Visible = false;
            ChequeDateButton.Visible = false;
        }
        //CheckIsCheque();
    }
    private void CheckIsCheque()
    {
        if (chkIsCheque.Checked == true)
        {
            txtChequeNo.Enabled = true;
            dtpChequeDate.Enabled = true;

            lblChequeNo.Visible = true;
            lblChequeDate.Visible = true;
            txtChequeNo.Visible = true;
            dtpChequeDate.Visible = true;
            ChequeDateButton.Visible = true; 
        }
        else
        {
            txtChequeNo.Enabled = false;
            dtpChequeDate.Enabled = false;

            ChequeNo = 0;
            ChequeDate = DateTime.Now.Date;

            lblChequeNo.Visible = false;
            lblChequeDate.Visible = false;
            txtChequeNo.Visible = false;
            dtpChequeDate.Visible = false;
            ChequeDateButton.Visible = false;  
        }
    }

    private void SetStandardCaption()
    {
        lbl_GCNo.Text = CompanyManager.getCompanyParam().GcCaption + " No";     
    }

    protected void btn_GetDetails_Click(object sender, EventArgs e)
    {
        objWrongDeliveryPresenter.Get_GC_Details();                      
        if (OnGetDetails != null)
        {
            OnGetDetails(sender, e);
        }
        HideUnHideIsToPay();
    }
    protected void ddl_Received_Condition_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    public bool validateUI()
    { 
    
        bool _isValid = false;


        if (Received_Condition_ID <= 0)
        {
            errorMessage = "Please Select Parcel Condition";
            _isValid = false;
            ddl_Received_Condition.Focus();
        } 
        else if (GCNo == string.Empty)
        {
            errorMessage = "Please Enter " + CompanyManager.getCompanyParam().GcCaption + " No.";
            _isValid = false;
            txt_GCNo.Focus();
        }
        else if (chkIsCheque.Checked == true && txtChequeNo.Text.Length < 6)
        {
            errorMessage = "Please Enter Cheque Details";
            _isValid = false;
            txtChequeNo.Focus();
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
         
        objWrongDeliveryPresenter.save();
    }
}
