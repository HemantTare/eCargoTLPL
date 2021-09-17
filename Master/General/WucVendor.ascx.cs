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

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EC.ControlsView;
using Raj.EC;


/// <summary> 
/// Author        : Vajiha
/// Created On    : 22/04/2008
/// Description   : This Page is For Master Vendor.
/// 
/// Updated On    : 25/04/2008
/// Description   : Added Vendor Type Field in The Form and accordingly made Changes in Code.
/// </summary>


public partial class Master_General_WucVendor : System.Web.UI.UserControl, IVendorView
{


    #region ClassVariables
    PageControls pc = new PageControls();
    VendorPresenter objVendorPresenter;
    Common ObjCommon = new Common();
    #endregion


    #region ControlsValues

    public string VendorName
    {
        set
        {
            txt_Vendor_Name.Text = value;
        }
        get
        {
            return txt_Vendor_Name.Text;
        }
    }
    
   
    public string ReferenceName
    {
        set
        {
          txt_Reference_Name.Text= value;
        }
        get
        {
            return txt_Reference_Name.Text;
        }
    }
    public string ReferencePhone
    {
        set
        {
           txt_Reference_Phone.Text = value;
        }
        get
        {
            return txt_Reference_Phone.Text;
        }
    }
    public string ReferenceMobile
    {
        set
        {
            txt_Reference_Mobile.Text = value;
        }
        get
        {
            return txt_Reference_Mobile.Text;
        }
    }
    public int Credit_Days
    {
        set
        {
            txt_Credit_Days.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_Credit_Days.Text);
        }
    }
    public int Credit_Limit
    {
        set
        {
            txt_Credit_Limit.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_Credit_Limit.Text);

        }
    }

    public int Debit_BalLimmit
    {
        set
        {
            txt_DebitBalLimmit.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_DebitBalLimmit.Text);

        }
    }
    //public bool IsTdsApplicable
    //{
    //    set
    //    {
    //        WucTDSApp1.IsTDSApp = value;
    //    }
    //    get
    //    {
    //        return WucTDSApp1.IsTDSApp;
    //    }
    //}

    public ITDSAppView TDSAppView 
    {
        get {return (ITDSAppView)WucTDSApp1;}
    }
    
    //public int TdsId
    //{
    //    set
    //    {
    //        ddl_Tds_Name.SelectedValue = Util.Int2String(value);
    //    }
    //    get
    //    {
    //        return Util.String2Int(ddl_Tds_Name.SelectedValue);

    //    }
    //}
    public int VendorTypeId
    {
        set
        {
            ddl_Vendor_Type.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Vendor_Type.SelectedValue);

        }
    }
    //public decimal TdsExemptionLimit
    //{
    //    set
    //    {
    //        txt_Tds_Exemption_Limit.Text = Util.Decimal2String(value);
    //    }
    //    get
    //    {
    //        return Util.String2Decimal(txt_Tds_Exemption_Limit.Text);
    //    }
    //}
    public string PanNo
    {
        set
        {
            txt_Pan_No.Text = value;
        }
        get
        {
            return txt_Pan_No.Text;
        }
    }
    //public decimal TdsRatePercent
    //{
    //    get
    //    {
    //        return Util.String2Decimal(txt_Tds_Rate_Percent.Text);
    //    }
    //}

    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }


    public string APMCBroker_To_City
    {
        set
        {
            txt_APMCBroker_To_City.Text = value;
        }
        get
        {
            return txt_APMCBroker_To_City.Text;
        }
    }

    #endregion

    #region ControlsBind
    
  
    //public DataSet BindTds
    //{
    //    set
    //    {
    //        ddl_Tds_Name.DataTextField = "TDS_Name";
    //        ddl_Tds_Name.DataValueField = "TDS_ID";
    //        ddl_Tds_Name.DataSource = value;
    //        ddl_Tds_Name.DataBind();
    //    }
    //}

    public DataSet BindVendorType
    {
        set
        {
            ddl_Vendor_Type.DataTextField = "Vendor_Type";
            ddl_Vendor_Type.DataValueField = "Vendor_Type_ID";
            ddl_Vendor_Type.DataSource = value;
            ddl_Vendor_Type.DataBind();

            Raj.EC.Common.InsertItem(ddl_Vendor_Type);
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
    
        if (txt_Vendor_Name.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Vendor Name";
            txt_Vendor_Name.Focus();
            _isValid = false;
        }
        else if (VendorTypeId <= 0)
        {
            ddl_Vendor_Type.Focus();
            errorMessage = "Please Select Vendor Type";
            _isValid = false;
        }
        else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false)
        {
        }
        else if (txt_Reference_Name.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Reference_Name) == true)
        {
            errorMessage = "Please Enter Reference Name";
            txt_Reference_Name.Focus();
            _isValid = false;
        }
        else if (txt_Reference_Phone.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Reference_Phone) == true)
        {
            errorMessage = "Please Enter Reference Phone";
            txt_Reference_Phone.Focus();
            _isValid = false;
        }
        else if (txt_Reference_Mobile.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Reference_Mobile) == true)
        {
            errorMessage = "Please Enter Reference Mobile";
            txt_Reference_Mobile.Focus();
            _isValid = false;
        }
        else if (txt_Pan_No.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Pan_No)== true && (VendorTypeId != 10 && VendorTypeId != 11) )
        {
            errorMessage = "Please Enter PAN No";
            _isValid = false;
        }

        else if (!WucTDSApp1.ValidateWucTDSApp(lbl_Errors))
        {
            _isValid = false;
        }
        //else if (CheckDuplicateDuplicatePanNo())
        //{
        //    errorMessage = "Duplicate Pan No.";
        //}
        else if (ObjCommon.IsCheck_Duplicate("Vendor_Pan_number", keyID, PanNo) == true)
        {
            errorMessage = "Duplicate Pan No.";
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
        set
        {
            lbl_Errors.Text= value;

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
    //private bool CheckDuplicatePanNo()
    //{
    //    bool DuplicatePanNo = objVendorPresenter.ISDuplicatePANNoCheck();
    //    return DuplicatePanNo;
    //}
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);

            //Common ObjCommon = new Common();
            //hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucVendor.ascx.resx");
        }
       objVendorPresenter = new VendorPresenter(this, IsPostBack);         
       //Chk_Is_Tds.Attributes.Add("onclick", "DisableControlOnChecked();");

       if (Request.QueryString["Call_From"] == "DVLP")
       {
           VendorTypeId = 2;
           ddl_Vendor_Type.Enabled = false;
       }
       else if (Request.QueryString["Call_From"] == "GC_WholeselerJK")
       {
           VendorTypeId = 10;
           ddl_Vendor_Type.Enabled = false;
       }
       else if (Request.QueryString["Call_From"] == "GC_BrokerAPMC")
       {
           VendorTypeId = 11;
           ddl_Vendor_Type.Enabled = false;
       }

       if (VendorTypeId == 10 || VendorTypeId == 11)
       {
           tr_RefName.Visible = false;
           tr_RefMobile.Visible = false;
           tr_Credit.Visible = false;
           tr_Debit.Visible = false;
           tr_TDS.Visible = false;
           tr_APMCBroker_To_City.Visible = true;
       }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objVendorPresenter.Save();
        }
    }

    protected void ddl_Vendor_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Util.String2Int(ddl_Vendor_Type.SelectedValue) == 10 || Util.String2Int(ddl_Vendor_Type.SelectedValue) == 11)
        {
            tr_RefName.Visible = false;
            tr_RefMobile.Visible = false;
            tr_Credit.Visible = false;
            tr_Debit.Visible = false;
            tr_TDS.Visible = false;
            txt_APMCBroker_To_City.Visible = true;
        }
        else
        {
            tr_RefName.Visible = true;
            tr_RefMobile.Visible = true;
            tr_Credit.Visible = true;
            tr_Debit.Visible = true;
            tr_TDS.Visible = true;
            txt_APMCBroker_To_City.Visible = false;
        }

    }
    #endregion


}