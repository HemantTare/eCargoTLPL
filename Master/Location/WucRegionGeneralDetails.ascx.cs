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

public partial class Master_Location_WucRegionGeneralDetailsl : System.Web.UI.UserControl,IRegionGeneralDetailsView
{
     #region ClassVariables
     RegionGeneralDetailsPresenter objRegionGeneralDetailsPresenter;
    public bool IsActivateDivision = UserManager.getUserParam().IsDivisionReq;

    #endregion

    #region ControlsValues

    public string ZoneCode
    {
        set{lbl_ZoneCode.Text=value;}
    }
    public string ZoneName
    {
        set {lbl_ZoneName.Text=value; }
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
    #endregion

    #region ControlBind
    public DataSet SessionChkListDivision
    {
        get { return StateManager.GetState<DataSet>("RegionDivision"); }
        set { StateManager.SaveState("RegionDivision", value); }
    }

     public DataSet BindChkListDivision
    {
        set
        {
            chk_ListDivision.DataTextField="Division_Name";           
            chk_ListDivision.DataValueField = "Division_Id";
            chk_ListDivision.DataSource = value;
            SessionChkListDivision = value;
            chk_ListDivision.DataBind();

            int i;
            if (keyID > 0)
            {
                for (i = 0; i < SessionChkListDivision.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(SessionChkListDivision.Tables[0].Rows[i]["Checked"]))
                    {
                        chk_ListDivision.Items[i].Selected = true;
                    }
                }
            }
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_ContactPerson.Text.Trim() == "")
        {
            errorMessage = "Please Enter Contact Person";
            txt_ContactPerson.Focus();
        }
        else if (IsActivateDivision == true && ValidateDivision() == false)
        {
            errorMessage = "Please Select Atleast one Division";
            _isValid = false;
        }
        else if (!WucAddress1.ValidateWucAddress(lbl_Errors))
        {
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

        if (IsActivateDivision)
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

    public bool ValidateDivision()
    {
        int i = 0;
        bool _isValid = false;
        for (int cnt = 0; cnt < chk_ListDivision.Items.Count; cnt++)
        {
            if (chk_ListDivision.Items[cnt].Selected)
            {
                cnt++;
                _isValid = true;
                MakeDSDivision();
            }
            else
            {
               // _isValid = false;
            }
        }
        return _isValid;
    }
     
    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsActivateDivision )
            {
                pnl_Division.Visible = true;
            }
            else
            {
                pnl_Division.Visible = false;
            }
        }
        objRegionGeneralDetailsPresenter = new RegionGeneralDetailsPresenter(this, IsPostBack);
        WucAddress1.VisibleMobileNo = false;
        AddressView.RegionDetailID = keyID;
    } 
    #endregion
}


