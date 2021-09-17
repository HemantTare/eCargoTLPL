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

public partial class Master_Location_WucRegion : System.Web.UI.UserControl,IRegionView
{
    #region ClassVariables
    RegionPresenter objRegionPresenter;
    PageControls pc = new PageControls();

    #endregion

    #region ControlsValues

    public string RegionName
    {
        set{ txt_RegionName.Text = value;}
        get{return txt_RegionName.Text;}
    }
    public string RegionCode
    {
        set { txt_RegionCode.Text = value; }
        get { return txt_RegionCode.Text;}
    }
    public int CountryId
    {
        set{ddl_Country.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_Country.SelectedValue);}

    }
    public int BankLedgerId
    {
        set {ddl_BankLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BankLedger.SelectedValue); }
    }
    public int CashLedgerId
    {
        set { ddl_CashLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_CashLedger.SelectedValue); }
    }
   

  
    #endregion

    #region ControlsBind

    public DataSet BindCountry
    {
        set
        {
            ddl_Country.DataTextField = "Country_Name";
            ddl_Country.DataValueField = "Country_ID";
            ddl_Country.DataSource = value;
            ddl_Country.DataBind();

            
        }
    }

    public DataTable BindCashLedger
    {
        set
        {
            ddl_CashLedger.DataTextField = "Ledger_Name";
            ddl_CashLedger.DataValueField = "Ledger_Id";
            ddl_CashLedger.DataSource = value;
            ddl_CashLedger.DataBind();

            ddl_CashLedger.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindBankLedger
    {
        set
        {
            ddl_BankLedger.DataTextField = "Ledger_Name";
            ddl_BankLedger.DataValueField = "Ledger_Id";
            ddl_BankLedger.DataSource = value;
            ddl_BankLedger.DataBind();

            ddl_BankLedger.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

       
        if (txt_RegionCode.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Region Code";// GetLocalResourceObject("Msg_RegionCode").ToString();
            _isValid = false;
        }
        else if (txt_RegionName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Region Name";// GetLocalResourceObject("Msg_RegionName").ToString();
            _isValid = false;
        }
        else if (ddl_Country.SelectedValue == "0")
        {
            errorMessage = "Please Select Country";// GetLocalResourceObject("Msg_ddlCountry").ToString();
            _isValid = false;
        }
        if (Util.String2Int(ddl_CashLedger.SelectedValue) == 0 && pc.Control_Is_Mandatory(ddl_CashLedger) == true)
        {
            lbl_Errors.Text = "Please Select  Cash Ledger";
            _isValid = false;
            ddl_CashLedger.Focus();
        }
        else if (Util.String2Int(ddl_BankLedger.SelectedValue) == 0 && pc.Control_Is_Mandatory(ddl_BankLedger) == true)
        {
            lbl_Errors.Text = "Please Select DefaultBank Ledger";
            _isValid = false;
            ddl_BankLedger.Focus();
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
           // return -1;
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
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucRegion.ascx.resx");
        //}
        pc.AddAttributes(this.Controls);
        objRegionPresenter = new RegionPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objRegionPresenter.Save();
    }
    #endregion

}
