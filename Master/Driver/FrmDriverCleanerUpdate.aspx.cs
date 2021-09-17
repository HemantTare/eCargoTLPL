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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Driver_FrmDriverCleanerUpdate : ClassLibraryMVP.UI.Page
{
    Common objCommon = new Common();


    private string IsCleaner
    {
        set
        {
            rbl_DriverCleaner.SelectedValue  = value;
        }
        get
        {
            return rbl_DriverCleaner.SelectedValue;
        }
    }

    private string ChangeType
    {
        set
        {
            rbl_ChangeType.SelectedValue = value;
        }
        get
        {
            return rbl_ChangeType.SelectedValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
        btn_Go.Attributes.Add("onclick", "return viewwindow_DriverCleanerChange('" + IsCleaner + "','" + ChangeType  + "')");

    }


}
