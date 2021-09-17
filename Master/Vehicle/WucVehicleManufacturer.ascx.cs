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
/// Author        : Vajiha
/// Created On    : 16/04/2008
/// Description   : This Page is For Master Carrier Category
/// </summary>


public partial class Master_Vehicle_WucVehicleManufacturer : System.Web.UI.UserControl,IVehicleManufacturerView
{
  

    #region ClassVariables
    VehicleManufacturerPresenter objVehicleManufacturerPresenter;
    #endregion


    #region ControlsValues

    public string VehicleManufacturerName
    {
        set
        {
            txt_Vehicle_Manufacturer_Name.Text = value;
        }
        get
        {
            return txt_Vehicle_Manufacturer_Name.Text;
        }
    }

    #endregion


    #region ControlsBind

    #endregion


    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Vehicle_Manufacturer_Name.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Manufacturer Name";// GetLocalResourceObject("Msg_manufacturer_Name").ToString();
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
            lbl_Errors.Text = value;
         }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1 for insert
            //return 1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods

    #endregion


    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Vehicle/App_LocalResources/WucVehicleManufacturer.ascx.resx");
        //}
        objVehicleManufacturerPresenter = new VehicleManufacturerPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleManufacturerPresenter.Save();
    }
    #endregion


}