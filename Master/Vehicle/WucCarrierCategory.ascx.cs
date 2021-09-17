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

public partial class Master_Vehicle_WucCarrierCategory : System.Web.UI.UserControl, ICarrierCategoryView
{

    #region ClassVariables
    CarrierCategoryPresenter objCarrierCategoryPresenter;
    #endregion


    #region ControlsValues

    public string CarrierCategoryName
    {
        set
        {
            txt_Carrier_Category_Name.Text = value;
        }
        get
        {
            return txt_Carrier_Category_Name.Text;
        }
    }

    #endregion


    #region ControlsBind

    #endregion


    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Carrier_Category_Name.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Carrier Category";// GetLocalResourceObject("Msg_CarrierCategoryName").ToString();
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
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Vehicle/App_LocalResources/WucCarrierCategory.ascx.resx");
        //}
        objCarrierCategoryPresenter = new CarrierCategoryPresenter(this, IsPostBack);
        //lbl_Errors.Visible = true;
        //Upd_Pnl_Carrier_Category.Update();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objCarrierCategoryPresenter.Save();
    }
   
   #endregion


}
