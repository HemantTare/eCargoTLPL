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
using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;
using Raj.EF;
using Raj.EC;


/// <summary>
/// Author :<Nilesh Kumar Jha>
/// Created On:<22/04/2008>
/// Description :This page is for Vehicle Body
/// </summary>

public partial class Master_Vehicle_WucVehicleBody : System.Web.UI.UserControl,IVehicleBodyView 
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Vehicle/App_LocalResources/WucVehicleBody.ascx.resx");
        //}
        objVehicleBodyPresenter = new VehicleBodyPresenter(this, IsPostBack);   
    }

     #region ClassVariables
        VehicleBodyPresenter objVehicleBodyPresenter;
     #endregion

    #region "ControlVariables"
    public string VehicleBody
    {
        set{txt_VehicleBody.Text = value;}
        get{return txt_VehicleBody.Text;}
    }
    
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_VehicleBody.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Vehicle Body";// GetLocalResourceObject("MsgVehicleBody").ToString();
            txt_VehicleBody.Focus();
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
        get {return Util.DecryptToInt(Request.QueryString["Id"]);}
    }
#endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleBodyPresenter.Save();
    }
}
