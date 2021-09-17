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
using Raj.EF;
using Raj.EC;
/// <summary>
/// Author        : Ashish Lad
/// Created On    : 22/04/2008
/// Description   : This is the Form  For Master Vehicle Model
/// </summary>


public partial class Master_Vehicle_WucVehicleModel : System.Web.UI.UserControl,IVehicleModelView 
{
    

    #region ClassVariables
    VehicleModelPresenter objVehicleModelPresenter;
    #endregion
    
    #region ControlsValues

    public string VehicleModelName
    {
        set{txt_Vehicle_Model_Name.Text = value;}
        get{return txt_Vehicle_Model_Name.Text;}
    }
    public int ManufacturerID
    {
       set { ddl_Manufacturer.SelectedValue = Util.Int2String(value); }
       get { return Util.String2Int(ddl_Manufacturer.SelectedValue); }
    }

    public decimal ThappiWeight
    {
        set
        {
            txt_ThappiWt.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txt_ThappiWt.Text == string.Empty ? 0 : Util.String2Decimal(txt_ThappiWt.Text); }
    }

    #endregion
    
    #region ControlsBind
    public DataSet Bind_ddl_Manufacturer
    {
        set
        {
            ddl_Manufacturer.DataTextField = "Manufacturer";
            ddl_Manufacturer.DataValueField = "Manufacturer_ID";
            ddl_Manufacturer.DataSource = value;
            ddl_Manufacturer.DataBind();
            //ddl_Manufacturer.Items.Insert(0, new ListItem("Select One", "0"));
            Raj.EC.Common.InsertItem(ddl_Manufacturer);
        }
    }

    #endregion
    
    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Vehicle_Model_Name.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Vehicle Model Name"; // GetLocalResourceObject("MsgModelName").ToString();
            txt_Vehicle_Model_Name.Focus();
        }
        else if(ManufacturerID <= 0)
        {
            lbl_Errors.Text = "Please Select Manufacturer Name";// GetLocalResourceObject("MsgManufacturerName").ToString();
            ddl_Manufacturer.Focus();
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

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Vehicle/App_LocalResources/WucVehicleModel.ascx.resx");
        //}
        objVehicleModelPresenter = new VehicleModelPresenter(this, IsPostBack);

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleModelPresenter.Save();
    }
    #endregion
}
