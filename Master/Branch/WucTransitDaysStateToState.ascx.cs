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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 07th October 2008
/// Description   : This is the Page For Master Transit Days State To State 
/// </summary>

public partial class Master_Branch_WucTransitDaysStateToState : System.Web.UI.UserControl,ITransitDaysStateToStateView 
{
    #region ClassVariables
    TransitDaysStateToStatePresenter objTransitDaysStateToStatePresenter;    
    #endregion

    #region ControlsValue

    public int TransitDays
    {
        set
        {
            txt_TransitDays.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_TransitDays.Text);
        }

    }
    public int DistanceInKM
    {
        set
        {
            txt_DistanceInKM.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(txt_DistanceInKM.Text);
        }

    }
    public int FromStateID
    {
        set
        {
            ddl_FromState.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_FromState.SelectedValue);
        }

    }
    public int ToStateID
    {
        set
        {
            ddl_ToState.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_ToState.SelectedValue);
        }

    }
    public int VehicleID
    {
        set
        {
            ddl_VehicleType.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_VehicleType.SelectedValue);
        }

    }
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_ToState
    {
        set
        {
            ddl_ToState.DataSource = value;
            ddl_ToState.DataTextField = "State_Name";
            ddl_ToState.DataValueField = "State_Id";
            ddl_ToState.DataBind();
            ddl_ToState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_FromState
    {
        set
        {
            ddl_FromState.DataSource = value;
            ddl_FromState.DataTextField = "State_Name";
            ddl_FromState.DataValueField = "State_Id";
            ddl_FromState.DataBind();
            ddl_FromState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Bind_ddl_Vehicle
    {
        set
        {
            ddl_VehicleType.DataSource = value;
            ddl_VehicleType.DataTextField = "Vehicle_Type";
            ddl_VehicleType.DataValueField = "Vehicle_Type_Id";
            ddl_VehicleType.DataBind();
            ddl_VehicleType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Util.String2Int(ddl_FromState.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select From State";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_ToState.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select To State";
            _isValid = false;
        }
        else if (Util.String2Int(ddl_VehicleType.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Vehicle Type";
            _isValid = false;
        }
        else if (txt_TransitDays.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Transit Days";
            _isValid = false;
        }
        else if (txt_DistanceInKM.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Distance in KM";
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
         //   return Util.DecryptToInt(Request.QueryString["Id"]);
            return -1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods
    
    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objTransitDaysStateToStatePresenter = new TransitDaysStateToStatePresenter(this, IsPostBack);
    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int MsgID = 0;

        string CloseScript = "";

        objTransitDaysStateToStatePresenter.Save();

        if (MsgID == 0)
        {
            CloseScript = "<script language='javascript'> self.close();</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), "CloseScript", CloseScript);
        }
    }
    #endregion
}
