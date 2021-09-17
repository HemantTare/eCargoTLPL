using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ClassLibraryMVP.General;
using Raj.eCargo.Operation.Muster.Presenter;
using Raj.eCargo.Operation.Muster.View;
using ClassLibraryMVP;
using System.Web.UI.HtmlControls;
using Raj.EC.ControlsView;
using ClassLibraryMVP.DataAccess;

public partial class Operations_Muster_wuc_Operation_Muster_Region_Area : System.Web.UI.UserControl
{  
     string Hierarchy_Code;
    int Main_Id;
    int Is_VTrans;
    DataSet _ds = new DataSet();
    #region IView

    public IHierarchiWithIdView HierarchiWithIdView
    {
        get { return (IHierarchiWithIdView)WucHierarchyWithID1; }

    }
       
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int Division_ID
    {
        get { return ClassLibraryMVP.Util.String2Int(ddl_division.SelectedValue); }
    }
   
   
      
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        WucHierarchyWithID1.DDLLocationChange += new EventHandler(ddlHierachyLoc);
        WucHierarchyWithID1.DDLHierarchyChange += new EventHandler(ddlHierachy);
        hdn_Pay_Div_ID.Value = Division_ID.ToString();

        if (!IsPostBack)
        {
            hdn_HCode.Value = WucHierarchyWithID1.HierarchyCode;
            hdn_Mainid.Value = WucHierarchyWithID1.MainId.ToString();
            FillDivisions();
            hdn_Pay_Div_ID.Value = Division_ID.ToString();

        }
    }
    protected void ddlHierachyLoc(object sender, EventArgs e)
    {
        hdn_HCode.Value = WucHierarchyWithID1.HierarchyCode;
        hdn_Mainid.Value = WucHierarchyWithID1.MainId.ToString();
        hdn_Pay_Div_ID.Value = Division_ID.ToString();
        
    }
    protected void ddlHierachy(object sender, EventArgs e)
    {
        hdn_HCode.Value = WucHierarchyWithID1.HierarchyCode;
        hdn_Mainid.Value = WucHierarchyWithID1.MainId.ToString();
        hdn_Pay_Div_ID.Value = Division_ID.ToString();

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

    protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdn_Pay_Div_ID.Value = Division_ID.ToString();
    }
}

