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
using Raj.EC;


public partial class Master_Location_WucCompanyParameters : System.Web.UI.UserControl,ICompanyParametersView
{
    #region ClassVariable
    CompanyParametersPresenter objCompanyParametersPresenter;
    PageControls pc = new PageControls();
    private ScriptManager scm_CompanyParameter;

    #endregion

    #region Controls Value
    public bool IsActivateDivision
    {
        set { Chk_IsActivateDivision.Checked = value; }
        get { return Chk_IsActivateDivision.Checked; }
    }

    public bool IsAccTransferRequired
    {
        set { Chk_IsAccTransferRequired.Checked = value; }
        get { return Chk_IsAccTransferRequired.Checked; }
    }

    public bool IsActivateCoLoaderBusiness
    {
        set { Chk_IsActivateColoaderBusiness.Checked = value; }
        get { return Chk_IsActivateColoaderBusiness.Checked; }
    }
    public bool IsBookOwnTruckHire
    {
        set { Chk_IsBookOwnTruckHire.Checked = value; }
        get { return Chk_IsBookOwnTruckHire.Checked; }

    }
    public bool IsMarketTruckLedgerAccTruckWise
    {
        set { Chk_IsMarketTruckLedgerAccTruckWise.Checked = value; }
        get { return Chk_IsMarketTruckLedgerAccTruckWise.Checked; }
    }
    public bool IsAttachedTruckLedgerAccTruckWise
    {
        set { Chk_IsAttachedTruckLedgerAccTruckWise.Checked = value; }
        get { return Chk_IsAttachedTruckLedgerAccTruckWise.Checked; }
    }

    public bool IsManagedTruckLedgerAccTruckWise
    {
        set { Chk_IsManagedTruckLedgerAccTruckWise.Checked = value; }
        get { return Chk_IsManagedTruckLedgerAccTruckWise.Checked; }
    }

    public int StdBasicFreightUnit
    {
        set { ddl_StdBasicFrieghtUnit.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_StdBasicFrieghtUnit.SelectedValue); }
    }
    public Decimal StdFreightRateForSundry
    {
        set { txt_StdFrieghtRateForSundry.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_StdFrieghtRateForSundry.Text); }
    }
    public int CompanyId
    {
        set { hdn_CompanyId.Value = Convert.ToString(value); }
        get { return Util.String2Int(hdn_CompanyId.Value); }
    }

    public bool IsPartLoadingRequired
    {
        set { Chk_IsPartLoadingRequired.Checked = value; }
        get { return Chk_IsPartLoadingRequired.Checked; }
    }
    public int MinDiffMEMOandTAS
    {
        set { txt_MinDiffMemo.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_MinDiffMemo.Text); }
    }

    public bool IsGCNumberEditable
    {
        set { Chk_IsGCNumberEditable.Checked = value; }
        get { return Chk_IsGCNumberEditable.Checked; }

    }

    public bool IsContractRequiredForTBBGC
    {
        set {Chk_IsContractRequiredForTBBGC.Checked = value; }
        get { return Chk_IsContractRequiredForTBBGC.Checked; }

    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;

       

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
       // get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        set { keyID = value; }
        get { return keyID; }
       
    }


    #endregion

    #region ControlsBind
    public DataSet BindStdBasicFreightUnit
    {
        set
        {
            ddl_StdBasicFrieghtUnit.DataTextField = "Standard_Basic_Freight_Unit";
            ddl_StdBasicFrieghtUnit.DataValueField = "Standard_Basic_Freight_Unit_ID";
            ddl_StdBasicFrieghtUnit.DataSource = value;
            ddl_StdBasicFrieghtUnit.DataBind();
            ddl_StdBasicFrieghtUnit.Items.Insert(0, new ListItem("Select One", "0"));
            

        }
    }
    #endregion

    #region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyParameter = value; }
    }
    #endregion


    #region PageLoadEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        objCompanyParametersPresenter = new CompanyParametersPresenter(this, IsPostBack);
    }
    #endregion


}
