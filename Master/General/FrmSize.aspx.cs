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
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter; 
using ClassLibraryMVP;
using Raj.EC;

public partial class Master_General_FrmSize : ClassLibraryMVP.UI.Page, ISizeView
{
    private SizePresenter objSizePresenter;

    #region IView Members

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //  return 34;
        }
    }

    public bool validateUI()
    {
        return true;
    }

    #endregion

    

    #region ISizeView Members
    public string SizeName
    {
        set { txtSizeName.Text = value; }
        get { return txtSizeName.Text; }
    }

    public decimal ApproxChargeWieght
    {
        set { txtApproxChargeWeight.Text = Util.Decimal2String(value); }
        get { return txtApproxChargeWeight.Text == string.Empty ? 0 : Util.String2Decimal(txtApproxChargeWeight.Text); }
    }

    public int Function
    {
        set { ddlFunction.SelectedValue = Util.Int2String(value); }
        get {return Util.String2Int(ddlFunction.SelectedValue);}
    }

    public decimal FactorAmount
    {
        set { txtFactorAmount.Text = Util.Decimal2String(value); }
        get { return txtFactorAmount.Text == string.Empty ? 0 : Util.String2Decimal(txtFactorAmount.Text); }
    }

    public decimal MinChrgQty
    {
        set { txt_MinChrgQty.Text = Util.Decimal2String(value); }
        get { return txt_MinChrgQty.Text == string.Empty ? 0 : Util.String2Decimal(txt_MinChrgQty.Text); }
    }

    public string Description
    {
        set { txtDescription.Text = value; }
        get { return txtDescription.Text; }
    }

    public bool IsDefault
    {
        set { chkIsDefault.Checked = value; }
        get { return chkIsDefault.Checked; }
    }

    public DataTable BindFunction
    {
        set
        {
            ddlFunction.DataSource = value;
            ddlFunction.DataTextField = "FunctionType";
            ddlFunction.DataValueField = "FunctionID";
            ddlFunction.DataBind();
        }
    }

    
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        objSizePresenter = new SizePresenter(this, IsPostBack);

        if (IsPostBack == false)
        {
            if (IsDefault)
            {
                txtSizeName.Enabled = false;
                ddlFunction.Enabled = false;
                txtFactorAmount.Enabled = false;
                txt_MinChrgQty.Enabled = false; 
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objSizePresenter.save();
    }

}
