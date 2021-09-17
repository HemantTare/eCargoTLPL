/// <summary>
/// Author        : Ankit
/// Created On    : 11/10/2008
/// Description   : This Page is For General Weight range
/// </summary>
/// 
using System;
using System.Data;

using ClassLibraryMVP;
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;

using Raj.EC.MasterModel;

using Raj.EC;


public partial class Master_General_WucWeightRange : System.Web.UI.UserControl,IContractRangeView  
{

    #region ClassVariables
    ContractRangePresenter  objContractRangePresenter;
    ContractRangeModel  objContractRangeModel;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        hdf_ContractRangeTypeId.Value = StateManager.GetState<string>("QueryString");

        objContractRangePresenter = new ContractRangePresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            //Common objCommon = new Common();
            //hdf_ResourceString.Value = objCommon.GetResourceString("Master/General/App_LocalResources/WucContractRange.ascx.resx");
        }
        if (Util.String2Int(hdf_ContractRangeTypeId.Value) == 1)
        {
            lbl_Header.Text = "Weight Range";
            lbl_From.Text = "From Kg";
            lbl_To.Text = "To Kg";
        }
        else if (Util.String2Int(hdf_ContractRangeTypeId.Value) == 2)
        {
            lbl_Header.Text = "Articles Range";
            lbl_From.Text = "From Articles";
            lbl_To.Text = "To Articles";
        }
        else if (Util.String2Int(hdf_ContractRangeTypeId.Value) == 3)
        {
            lbl_Header.Text = "CFT Range";
            lbl_From.Text = "From CFT";
            lbl_To.Text = "To CFT";
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objContractRangeModel = new ContractRangeModel(this);
        if (objContractRangeModel.isDuplicate() == false)
            objContractRangePresenter.Save();
        else
            errorMessage = GetLocalResourceObject("Msg_Range").ToString(); ;
    }

    #region ControlsValues

    public int ContractRangeTypeId
    {
        get { return Util.String2Int(hdf_ContractRangeTypeId.Value); }
        set { hdf_ContractRangeTypeId.Value = value.ToString(); }
    }

    public int From_Unit
    {
        get { return Util.String2Int(txt_FromKg.Text); }
        set { txt_FromKg.Text = value.ToString(); }
    }

    public int To_Unit
    {
        get { return Util.String2Int(txt_ToKg.Text); }
        set { txt_ToKg.Text = value.ToString(); }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_FromKg.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter From Kg";// GetLocalResourceObject("Msg_txt_FromKg").ToString(); 
            txt_FromKg.Focus();
        }
        else if (txt_ToKg.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter To Kg";// GetLocalResourceObject("Msg_txt_ToKg").ToString();
            txt_ToKg.Focus();
        }
        else if (Convert.ToInt32(txt_FromKg.Text.Trim()) >= Convert.ToInt32(txt_ToKg.Text.Trim()))
        { 
            errorMessage = "Please Enter the valid range";// GetLocalResourceObject("Msg_Range").ToString();
            txt_ToKg.Focus();
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
}
