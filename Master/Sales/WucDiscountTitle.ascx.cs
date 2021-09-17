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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;


public partial class Master_Sales_WucDiscountTitle : System.Web.UI.UserControl, IDiscountTitleView
{
    #region ClassVariables
    DiscountTitlePresenter objDiscountTitlePresenter;
    Label FormId;
    Label FormName;
    CheckBox Chk;
    DataRow DR = null;
    #endregion

    #region ControlsValue

    public string DiscountTitleName
    {
        set { txt_DiscountTitleName.Text = value; }
        get { return txt_DiscountTitleName.Text; }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }  
    #endregion

    #region ControlBind 
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;


        if (txt_DiscountTitleName.Text == string.Empty)
        {
            errorMessage = "Please Enter Discount Title Name";// GetLocalResourceObject("Msg_DiscountTitle").ToString();
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
            //return 2;
        }
    }

    #endregion

    #region OtherMethods   

     
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
            objDiscountTitlePresenter = new DiscountTitlePresenter(this, IsPostBack); 
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
         objDiscountTitlePresenter.Save(); 
    }
     

    #endregion


}
