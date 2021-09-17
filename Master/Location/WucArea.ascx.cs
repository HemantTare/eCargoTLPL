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


public partial class Master_Location_WucArea : System.Web.UI.UserControl,IAreaView
{
    
    AreaPresenter objAreaPresenter;
    PageControls pc = new PageControls();

    #region Validation
    public bool validateUI()
    {
        bool IsValid = false;

        if (WucAreaGeneralDetails1.validateUI() == false)
        { 
            MP_AreaDetails.SelectedIndex= 0;
            TB_AreaDetails.SelectedTab = TB_AreaDetails.Tabs[0];
            
        }
        else if (WucAreaDepartment1.validateUI() == false)
        {
            MP_AreaDetails.SelectedIndex = 1;
            TB_AreaDetails.SelectedTab = TB_AreaDetails.Tabs[1];
            
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

    public IAreaGeneralDetailsView AreaGeneralDetailsView
    {
        get { return (IAreaGeneralDetailsView)WucAreaGeneralDetails1; }
    }

    public IAreaDepartmentView AreaDepartmentView
    {
        get { return (IAreaDepartmentView)WucAreaDepartment1; }
    }

    
    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
        {
        
        TB_AreaDetails.SiteMapXmlFile="~/XML/Location/Area.xml";
        WucAreaGeneralDetails1.OnCityChanged = new EventHandler(WucAreaGeneralDetails1.HandelsCityChangedEvent);
        WucAreaGeneralDetails1.SetScriptManager = SC_Area;
        WucAreaDepartment1.SetScriptManager = SC_Area;
        objAreaPresenter=new AreaPresenter(this,IsPostBack);
        pc.AddAttributes(this.Controls);       

       
     }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        WucAreaGeneralDetails1.ValidateDivision();
        WucAreaGeneralDetails1.MakeDSDivision();
        WucAreaDepartment1.MakeDSDepartment();        
        objAreaPresenter.Save();
       
        
    }
    #endregion
}

