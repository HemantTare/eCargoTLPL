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

/// <summary>
/// Author        : Vajiha
/// Created On    : 22/04/2008
/// Description   : This Page is For Master Vehicle Vendor.
/// 
/// Updated On    : 25/04/2008
/// Description   : Added Vendor Type Field in The Form and accordingly made Changes in Code.
/// </summary>


public partial class Master_Vehicle_WucVehicleVendor : System.Web.UI.UserControl, IVehicleVendorView
{


    #region ClassVariables
    VehicleVendorPresenter objVehicleVendorPresenter;
    #endregion


    #region ControlsValues

    public string VehicleVendorName
    {
        set
        {
            txt_Vehicle_Vendor_Name.Text = value;
        }
        get
        {
            return txt_Vehicle_Vendor_Name.Text;
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
    public bool IsTdsApplicable
    {
         set 
        { 
           Chk_Is_Tds.Checked=value; 
        }
        get 
        {
            return  Convert.ToBoolean(Chk_Is_Tds.Checked);
        }
    }
    public int TdsId
    {
        set
        {
            ddl_Tds_Name.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Tds_Name.SelectedValue);

        }
    }
    public int VendorTypeId
    {
        set
        {
            ddl_Vehicle_Vendor_Type.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Vehicle_Vendor_Type.SelectedValue);

        }
    }
    public string TdsExemptionLimit
    {
        set
        {
           txt_Tds_Exemption_Limit.Text = value;
        }
        get
        {
            return txt_Tds_Exemption_Limit.Text;
        }
    }
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
    public string TdsRatePercent
    {
        get
        {
            return txt_Tds_Rate_Percent.Text;
        }
    }

    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    #endregion

    #region ControlsBind
    
  
    public DataSet BindTds
    {
        set
        {
            ddl_Tds_Name.DataTextField = "TDS_Name";
            ddl_Tds_Name.DataValueField = "TDS_ID";
            ddl_Tds_Name.DataSource = value;
            ddl_Tds_Name.DataBind();

            Raj.EC.Common.InsertItem(ddl_Tds_Name);
        }
    }

    public DataSet BindVendorType
    {
        set
        {
            ddl_Vehicle_Vendor_Type.DataTextField = "Vendor_Type";
            ddl_Vehicle_Vendor_Type.DataValueField = "Vendor_Type_ID";
            ddl_Vehicle_Vendor_Type.DataSource = value;
            ddl_Vehicle_Vendor_Type.DataBind();

            Raj.EC.Common.InsertItem(ddl_Vehicle_Vendor_Type );
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Vehicle_Vendor_Name.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Vehicle Vendor Name";// GetLocalResourceObject("VehicleVendorName").ToString();
            _isValid = false;
        }
        else if (VehicleVendorName.Length < 2)
        {
            errorMessage = "Vehicle Vendor Name Should be Atleast Two Characters.";/// GetLocalResourceObject("msgVehicleVendorNameLength").ToString();
            _isValid = false;
        }

        else if (ddl_Vehicle_Vendor_Type.SelectedValue == "0" && Util.String2Int(ddl_Vehicle_Vendor_Type.SelectedValue) == -1)
        {
            errorMessage = "Please Select Vendor Type";// GetLocalResourceObject("msgVehicleVendorType").ToString();
            _isValid = false;
        }
        else if (txt_Reference_Name.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Reference Name";// GetLocalResourceObject("msgRefName").ToString();
            _isValid = false;
        }
        else if (txt_Reference_Phone.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Reference Phone";// GetLocalResourceObject("msgRefPhone").ToString();
            _isValid = false;
        }
        else if (txt_Reference_Mobile.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Reference Mobile";// GetLocalResourceObject("msgRefMobile").ToString();
            _isValid = false;
        }
        else if (txt_Pan_No.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Pan No.";// GetLocalResourceObject("msgPanNo").ToString();
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
            errorMessage = value;

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

    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
       objVehicleVendorPresenter = new VehicleVendorPresenter(this, IsPostBack);         
       //Chk_Is_Tds.Attributes.Add("onclick", "DisableControlOnChecked();");
                  
       
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleVendorPresenter.Save();
    }

    #endregion


}