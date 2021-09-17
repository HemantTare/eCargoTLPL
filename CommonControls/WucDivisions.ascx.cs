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
using ClassLibraryMVP.DataAccess;

public partial class CommonControls_WucDivisions : System.Web.UI.UserControl
{


    public bool SetDDLDivisionAutopostback
    {
        set
        {
            ddl_division.AutoPostBack = value;
        }
    }

    public int Division_ID
    {
        get { return ClassLibraryMVP.Util.String2Int(ddl_division.SelectedValue); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { 
            FillDivisions();
            tr_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
        }
    }

    private void FillDivisions()
    {
        DAL objdal = new DAL();
        DataSet DS_Division = new DataSet();
        DS_Division = objdal.RunQuery("Select Division_ID,Division_Name From ec_master_division where Is_Active = 1");
        ddl_division.DataTextField = "Division_Name";
        ddl_division.DataValueField = "Division_ID";
        ddl_division.DataSource = DS_Division;
        ddl_division.DataBind();

        ddl_division.Items.Insert(0, new ListItem("All", "0"));
    }
}
