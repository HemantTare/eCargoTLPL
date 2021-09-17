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
using Raj.EC.ControlsView;
using Raj.EC;


public partial class Master_Vehicle_WucInsuranceCompanyBranch : System.Web.UI.UserControl, IInsuranceCompanyBranchView
{
    #region ClassVariables
    InsuranceCompanyBranchPresenter objInsuranceCompanyBranchPresenter;
    #endregion


    #region ControlsValues

    public string BranchName
    {
        set
        {
            txt_Branch_Name.Text = value;
        }
        get
        {
            return txt_Branch_Name.Text;
        }
    }


    public string ContactPerson
    {
        set
        {
            txt_Contact_Person.Text = value;
        }
        get
        {
            return txt_Contact_Person.Text;
        }
    }
   
   
    public int InsuranceCompanyId
    {
        set
        {
           ddl_Insurance_Company_Branch_Name.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_Insurance_Company_Branch_Name.SelectedValue);

        }
    } 
    
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    #endregion

    #region ControlsBind


    public DataSet BindInsuranceCompany
    {
        set
        {
            ddl_Insurance_Company_Branch_Name.DataTextField = "Insurance_Company";
            ddl_Insurance_Company_Branch_Name.DataValueField = "Insurance_Company_ID";
            ddl_Insurance_Company_Branch_Name.DataSource = value;
            ddl_Insurance_Company_Branch_Name.DataBind();

            Raj.EC.Common.InsertItem(ddl_Insurance_Company_Branch_Name);
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (Util.String2Int(ddl_Insurance_Company_Branch_Name.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Insurance Company";// GetLocalResourceObject("Msg_ddl_Insurance_Company_branch").ToString();
            _isValid = false;
        }
        else if (txt_Branch_Name.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Branch Name";// GetLocalResourceObject("Msg_txt_BranchName").ToString();
            _isValid = false;
        }
        else if (txt_Contact_Person.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Contact Person";// GetLocalResourceObject("Msg_txt_ContactPerson").ToString();
            _isValid = false;
        }
        
        else if (!WucAddress1.ValidateWucAddress(lbl_Errors))
        {
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
            //return 1;
        }
    }
    #endregion

    #region OtherProperties

    #endregion

    #region OtherMethods  

    #endregion



    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucInsuranceCompanyBranch.ascx.resx");
        //}
        objInsuranceCompanyBranchPresenter = new InsuranceCompanyBranchPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
       objInsuranceCompanyBranchPresenter.Save();
    }

    #endregion



}