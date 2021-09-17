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

using Raj.EC.ControlsView;
using Raj.EC;

/// <summary>
/// author pankaj
/// created on 30 apr 08
/// this is main user control in which other 2 user control of driver are kept
/// </summary>
public partial class Master_Geo_WucDriver : System.Web.UI.UserControl,IDriverView
{
    DriverPresenter objDriverPresenter;
    PageControls pc = new PageControls();

    #region Validation
    public bool validateUI()
    {
        bool IsValid = false;

        if (WucDriverDetails1.validateUI() == false)
        {
            MP_Driver.SelectedIndex = 0;
            TB_Driver.SelectedTab = TB_Driver.Tabs[0];
        }
        else if (WucDriverInsuranceDependent1.validateUI() == false)
        {
            MP_Driver.SelectedIndex = 1;
            TB_Driver.SelectedTab = TB_Driver.Tabs[1];
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
    }

    public string errorMessage
    { 
        set { lbl_Errors.Text = value; } 
    }

    public IDriverDetailsView DriverDetailsView
    {
        get { return (IDriverDetailsView)WucDriverDetails1; }
    }

    public IDriverInsuranceDependentView DriverInsuranceDependentView
    {
        get { return (IDriverInsuranceDependentView)WucDriverInsuranceDependent1; }
    }

    public IAttachmentsView AttachmentsView
    {
        get { return (IAttachmentsView)WucAttachments1; }
    }
    //Added : Anita On : 18 Feb 09
    public void ClearVariables()
    {
        WucDriverInsuranceDependent1.SessionDependentDetailsGrid = null;
        WucDriverInsuranceDependent1.SessionDepRelationDropDown = null;
        
        WucAttachments1.SessionAttachmentsGrid = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {
        hdn_Id.Value = StateManager.GetState<string>("QueryString");
        if (hdn_Id.Value == "0")
        {
            Page.Title = "CLEANER";
            TB_Driver.SiteMapXmlFile = "~/XML/Fleet/Cleaner.xml";
        }
        else if (hdn_Id.Value == "1")
        {
            Page.Title = "DRIVER";
            TB_Driver.SiteMapXmlFile = "~/XML/Fleet/Driver.xml";
        }
        else if (hdn_Id.Value == "2")
        {
            Page.Title = "Market Driver";
            TB_Driver.SiteMapXmlFile = "~/XML/Fleet/Driver.xml";
            TB_Driver.Tabs[1].Visible = false;
        }
        else 
        {
            hdn_Id.Value = "2";
            Page.Title = "Market Driver";
            TB_Driver.SiteMapXmlFile = "~/XML/Fleet/Driver.xml";
            TB_Driver.Tabs[1].Visible = false;
        }
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Driver/App_LocalResources/WucDriver.ascx.resx");
        //}
        
        objDriverPresenter = new DriverPresenter(this, IsPostBack);
        
        WucAttachments1.AttachmentFormId = 4;

        WucDriverInsuranceDependent1._Driverinformation = SC_Driver;
        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            btn_null_sessions.Style.Add("display", "none");
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objDriverPresenter.Save();
    }
    #endregion
}
