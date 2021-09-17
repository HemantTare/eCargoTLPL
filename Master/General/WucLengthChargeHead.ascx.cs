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
using Raj.EC.GeneralPresenter;
using Raj.EC.GeneralView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;

public partial class Master_General_WucLengthChargeHead : System.Web.UI.UserControl,ILengthChargeHeadView
{
    #region ClassVariables
    LengthChargeHeadPresenter objLengthChargeHeadPresenter;
    #endregion

    #region ControlsValue

    public decimal LengthCharge
    {
        set { txt_Charge.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_Charge.Text); }
    }
    public string FromLength
    {
        set { txt_FromLength.Text = value; }
        get { return txt_FromLength.Text; }
    }
    public string ToLength
    {
        set { txt_ToLength.Text = value; }
        get { return txt_ToLength.Text; }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

          if (txt_FromLength.Text == string.Empty || Util.String2Int(txt_FromLength.Text) <= 0)
        {
            errorMessage = "Please Enter From Length Greater Than Zero";
            _isValid = false;
        }
        else if (Util.String2Int(txt_ToLength.Text) <=  Util.String2Int(txt_FromLength.Text))
        {
            errorMessage = "Please Enter To Length Greater Than From Length";
            _isValid = false;
        }
        else if (txt_ToLength.Text == string.Empty)
        {
            errorMessage = "Please Enter To Length";
            _isValid = false;
        }
        else if (Util.String2Int(txt_FromLength.Text) > 0 && Util.String2Int(txt_ToLength.Text) <= 0)
        {
            errorMessage = "To Length Cannot Be Less Than From Length";
            _isValid = false;
        }
        else if (txt_Charge.Text == string.Empty )
        {
            errorMessage = "Please Enter Charge Name Greater Than Zero";
            _isValid = false;
        }
        else if (Util.String2Decimal(txt_Charge.Text) <= 0)
        {
            errorMessage = "Please Enter Charge Name Greater Than Zero";
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

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        
        objLengthChargeHeadPresenter = new LengthChargeHeadPresenter(this, IsPostBack);

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        objLengthChargeHeadPresenter.Save();
    }


    #endregion
}
