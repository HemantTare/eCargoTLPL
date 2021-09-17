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
using Raj.EC;

public partial class Master_Location_WucAreaGeneralDetails : System.Web.UI.UserControl,IAreaGeneralDetailsView
{
    #region ClassVariables
    AreaGeneralDetailsPresenter objAreaGeneralDetailsPresenter;
    private int _CityID;
    private int _AreaID = 0;
    //public bool IsActivateDivision = UserManager.getUserParam().IsDivisionReq;
    public bool IsActivateDivision = CompanyManager.getCompanyParam().IsActivateDivision;
    private ScriptManager scm_AreaGeneral;

    #endregion

    #region ControlsValues

    public string AreaCode
    {
        set{txt_AreaCode.Text = value;}
        get { return txt_AreaCode.Text; }
    }
    public string AreaName
    {
        set { txt_AreaName.Text = value; }
        get { return txt_AreaName.Text; }
    }

    public string ContactPerson
    {
        set { txt_ContactPerson.Text = value; }
        get { return txt_ContactPerson.Text; }
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
   

    public int ChkListDivision
    {
        set
        {
            chk_ListDivision.SelectedValue = Util.Int2String(value);

        }
        get
        {
            return Util.String2Int(chk_ListDivision.SelectedValue);
        }
    }
   
    public string SessionChkListDivisionDetails
    {
        get
        {
            return  SessionChkListDivision.GetXml();
        }
        
    }
    public DateTime StartedOn
    {
        set { PickerStartedOn.SelectedDate = value; }
        get { return PickerStartedOn.SelectedDate; }

    }
    public EventHandler OnCityChanged
    {
        set { WucAddress1.OnCityChanged += value; }
    }

    private Boolean EnableDisableDivision
    {
        set { pnl_Division.Visible = value; }
    } 
    #endregion

    #region ControlBind
    public DataSet SessionChkListDivision
    {
        get { return StateManager.GetState<DataSet>("AreaDivision"); }
        set { StateManager.SaveState("AreaDivision", value); }
    }

     public DataSet BindChkListDivision
    {
        set
        {
            chk_ListDivision.DataSource = value;
            chk_ListDivision.DataTextField="Division_Name";           
            chk_ListDivision.DataValueField = "Division_Id";
            chk_ListDivision.DataBind();

            SessionChkListDivision = value;
            int i;
           
                for (i = 0; i < SessionChkListDivision.Tables[0].Rows.Count; i++)
                {
                    if (AreaID <= 0)
                    {
                        chk_ListDivision.Items[i].Selected = true;

                    }
                    else
                    {
                        if (Convert.ToBoolean(SessionChkListDivision.Tables[0].Rows[i]["Checked"]))
                        {
                            chk_ListDivision.Items[i].Selected = true;
                        }
                    }
                } 
            }
    }

    public int AreaID 
    {
        get { return _AreaID;}
        set { _AreaID = value;}
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

         if (txt_AreaCode.Text == string.Empty)
            {
                errorMessage = "Please Enter Area Code";// GetLocalResourceObject("Msg_AreaCode").ToString();
                _isValid=false;
            }
            else if (txt_AreaName.Text== string.Empty)
            {
                errorMessage = "Please Enter Area Name";// GetLocalResourceObject("Msg_AreaName").ToString();
                _isValid=false;
            }
            else if (txt_ContactPerson.Text == string.Empty)
            {
                errorMessage = "Please Enter Contact Person";// GetLocalResourceObject("Msg_ContactPerson").ToString();
                _isValid=false;
            }
            else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }
            else if (ValidateDivision() == false)
            {
                errorMessage = "Please Select Atleast one Division";
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

    #region OtherMethod
    public DataSet MakeDSDivision()
    {
        int cnt;

        DataSet objDS;
        DataRow DR = null;
        objDS = SessionChkListDivision;
        objDS.Tables[0].TableName = "SessionChkListDivisionDetails";
        objDS.Clear();

        if (IsActivateDivision == true)
        {      
           for (cnt = 0; cnt < chk_ListDivision.Items.Count; cnt++)
            {

                if (chk_ListDivision.Items[cnt].Selected == true)
                {
                    DR = objDS.Tables[0].NewRow();
                    DR["Division_ID"] = chk_ListDivision.Items[cnt].Value;

                    objDS.Tables["SessionChkListDivisionDetails"].Rows.Add(DR);
                }
            }
        }
        else
        {
            DR = objDS.Tables[0].NewRow();
            DR["Division_ID"] = UserManager.getUserParam().DivisionId;
            objDS.Tables["SessionChkListDivisionDetails"].Rows.Add(DR);
        }
        SessionChkListDivision = objDS;
        return objDS;
    }   

    public ScriptManager SetScriptManager
    {
        set { scm_AreaGeneral = value; }
    }

    public bool ValidateDivision()
    {
        int i = 0;
        bool _isValid = false;
        if (IsActivateDivision == true)
        {
            for (int cnt = 0; cnt < chk_ListDivision.Items.Count; cnt++)
            {

                if (chk_ListDivision.Items[cnt].Selected)
                {
                    i++;
                    _isValid = true;
                    MakeDSDivision();
                }

                
            }
        }
        else
        {
            _isValid = true;
        }
         return _isValid; 
    }
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        AreaID = 0;

        if (!IsPostBack)
        {
            AreaID = keyID;
            if (IsActivateDivision)
            {
                EnableDisableDivision = true;               
            }
            else
            {
                EnableDisableDivision = false;
            }
        
            //Raj.EC.Common ObjCommon = new Raj.EC.Common();
            //hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucAreaGeneralDetails.ascx.resx");
        }
        objAreaGeneralDetailsPresenter = new AreaGeneralDetailsPresenter(this, IsPostBack);
        WucAddress1.VisibleMobileNo = false;
    }

    public void HandelsCityChangedEvent(object o, EventArgs e)
    {        
        _CityID = Convert.ToInt32(o);
        objAreaGeneralDetailsPresenter.FillDivision();
        Upd_PnlDivision.Update();
    } 
    #endregion

}
