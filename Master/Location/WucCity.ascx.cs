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



public partial class Master_Location_WucCity : System.Web.UI.UserControl,ICityView
{
    #region ClassVariables
    CityPresenter objCityPresenter;
    #endregion

    #region ControlsValue
    public string CityName
    {
        set { txt_CityName.Text = value; }
        get { return txt_CityName.Text; }
    }
    public int StateId
    {
        set { ddl_State.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_State.SelectedValue); }

    }
    public string CountryName
    {
        set { lbl_Country.Text = value; }
    }
    public string RegionName
    {
        set { lbl_Region.Text = value; }
    }
    #endregion

    #region ControlsBind
    public DataSet BindState
    {
        set
        {
            ddl_State.DataSource = value;
            ddl_State.DataTextField = "State_Name";
            ddl_State.DataValueField = "State_Id";
            ddl_State.DataBind();
            ddl_State.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;


        if (txt_CityName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter City Name";// GetLocalResourceObject("Msg_City").ToString();
            _isValid = false;
        }
        else if (ddl_State.SelectedValue == "0")
        {
            errorMessage = "Please Select  State";// GetLocalResourceObject("Msg_ddlState").ToString();
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
            //return -1;
        }
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucCity.ascx.resx");
        //}
        objCityPresenter = new CityPresenter(this, IsPostBack);

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        objCityPresenter.Save();

    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCityPresenter.FillLabel();
        if (ddl_State.SelectedValue == "0")
        {
            lbl_Region.Text = "";
            lbl_Country.Text = "";
        }

    }

    

    #endregion
    
}
