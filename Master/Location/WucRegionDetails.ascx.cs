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


public partial class Master_Location_WucRegionDetails : System.Web.UI.UserControl,IRegionDetailsView
{
    RegionDetailsPresenter objRegionDetailsPresenter;

    #region Validation
    public bool validateUI()
    {
        bool IsValid = false;

        if (WucRegionGeneralDetails1.validateUI() == false)
        {
            MP_RegionDetails.SelectedIndex = 0;
            TB_RegionDetails.SelectedTab = TB_RegionDetails.Tabs[0];

        }
        else if (WucRegionDepartment1.validateUI() == false)
        {
            MP_RegionDetails.SelectedIndex = 1;
            TB_RegionDetails.SelectedTab = TB_RegionDetails.Tabs[1];

        }
        else
        {
            IsValid = true;
        }

        return IsValid;
    }
    #endregion

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get {return -1;}
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public IRegionGeneralDetailsView RegionGeneralDetailsView
    {
        get { return (IRegionGeneralDetailsView)WucRegionGeneralDetails1; }
    }

    public IRegionDepartmentView RegionDepartmentView
    {
        get { return (IRegionDepartmentView)WucRegionDepartment1; }
    }


    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {

        TB_RegionDetails.SiteMapXmlFile = "~/XML/Location/RegionDetails.xml";
       
        objRegionDetailsPresenter = new RegionDetailsPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        WucRegionGeneralDetails1.MakeDSDivision();
        WucRegionDepartment1.MakeDSDepartment();
        objRegionDetailsPresenter.Save();


    }
    #endregion
}
