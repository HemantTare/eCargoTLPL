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

/// <summary>
/// Author        : Ashish Lad
/// Created On    : 02/05/2008
/// Description   : This is the Form  For Master Vehicle Hire Details
/// </summary>


public partial class Master_Vehicle_WucVehicleHireDetails : System.Web.UI.UserControl, IVehicleHireDetailsView 
{
    #region ClassVariables
    VehicleHireDetailsPresenter objVehicleHireDetailsPresenter;
    private ScriptManager scm_VehicleHireDetails;
    #endregion
    
    #region iview implementaion
      
    public bool MultipleTripADay
    {
        set{chk_Multiple_Trips_A_Day.Checked = value;}
        get{return chk_Multiple_Trips_A_Day.Checked;}
    }

    public int HireTypeID
    {
        set{ddl_Hire_Type.SelectedValue = Util.Int2String(value);}
        get { return Util.String2Int(ddl_Hire_Type.SelectedValue); }
    }

    public int RegionID
    {
        get 
        {
            if (rdl_MaintainedBy.Items[0].Selected == true)
                return MaintainedID;
            else
                return 0;
        }
    }

    public int AreaID
    {
        get
        {
            if (rdl_MaintainedBy.Items[1].Selected == true)
                return MaintainedID;
            else
                return 0;
        }
    }

    public int BranchID
    {
        get
        {
            if (rdl_MaintainedBy.Items[2].Selected == true)
                return MaintainedID;
            else
                return 0;
        }
    }

    public decimal HireAmount
    {
        set{txt_Hire_Amount.Text =Util.Decimal2String(value);}
        get{return Util.String2Decimal(txt_Hire_Amount.Text);}
    }

    public int MaintainedID
    {

        set{ddl_MaintainedBy.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(ddl_MaintainedBy.SelectedValue);}
    }

    public int MaintainedByID
    {
        set{rdl_MaintainedBy.SelectedValue = Util.Int2String(value);}
        get{return Util.String2Int(rdl_MaintainedBy.SelectedValue);}
    }

    public string MaintainedBy_Caption
    {

        set{lbl_MaintainedBy.Text = value;}
        get{return lbl_MaintainedBy.Text;}
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
            //return 128;
        }
    }


    public DataTable Bind_ddl_HireType
    {
        set
        {
            ddl_Hire_Type.DataTextField = "Vehicle_Hire_Type";
            ddl_Hire_Type.DataValueField = "Vehicle_Hire_Type_Id";
            ddl_Hire_Type.DataSource = value;
            ddl_Hire_Type.DataBind();
            ddl_Hire_Type.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_MaintainedBy
    {
        set
        {
            ddl_MaintainedBy.DataTextField = "MaintainedBy";
            ddl_MaintainedBy.DataValueField = "MaintainedBy_ID";
            ddl_MaintainedBy.DataSource = value;
            ddl_MaintainedBy.DataBind();
            ddl_MaintainedBy.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_VehicleHireDetails = value; }
    }
    #endregion

    #region validation
    public bool validateUI()
    {


        return true;
    }



        #endregion

    #region  ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
          objVehicleHireDetailsPresenter = new VehicleHireDetailsPresenter(this, IsPostBack);

          if (MaintainedByID == 1)
          {
              MaintainedBy_Caption = "Region Name : ";
          }
          else if (MaintainedByID == 2)
          {
              MaintainedBy_Caption = "Area Name : ";
          }
          else if (MaintainedByID == 3)
          {
              MaintainedBy_Caption = "Branch Name : ";
          }
    }
    protected void rdl_MaintainedBy_SelectedIndexChanged(object sender, EventArgs e)
    {       
        objVehicleHireDetailsPresenter.FillDDLMaintainBy();
    }    
    #endregion
}
