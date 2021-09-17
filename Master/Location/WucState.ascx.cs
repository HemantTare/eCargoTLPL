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


public partial class Master_Location_WucState : System.Web.UI.UserControl,IStateView
{
    #region ClassVariables
    StatePresenter objStatePresenter;
    Label FormId;
    Label FormName;
    CheckBox Chk;
    DataRow DR = null;
    #endregion

    #region ControlsValue

    public string StateName
    {
        set { txt_StateName.Text = value; }
        get { return txt_StateName.Text; }
    }
    public int RegionId
    {
        set { ddl_Region.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Region.SelectedValue); }

    }
     public string CountryName
    {
        set { lbl_Country.Text = value; }
    }   

    public string NsdlCode
    {
        set { txt_NsdlCode.Text = value; }
        get { return txt_NsdlCode.Text; }
    }
    public string StateCode
    {
        set { txt_StateCode.Text = value; }
        get { return txt_StateCode.Text; }
    }
    public int ChkFormType
    {
        set
        {
            ChkList_FormType.SelectedValue = Util.Int2String(value);
            
        }
        get
        {
            return Util.String2Int(ChkList_FormType.SelectedValue);
        }
    }

    public string SessionStateFormDetails
    {
        get
        {
            return ChkListSessionForm.GetXml();
        }
    }
    #endregion

    #region ControlBind
    public DataSet ChkListSessionForm
    {
        get { return StateManager.GetState<DataSet>("StateForm"); }
        set { StateManager.SaveState("StateForm", value); }
    }
    
    public DataSet BindRegion
    {
        set
        {
            ddl_Region.DataSource = value;
            ddl_Region.DataTextField = "Region_Name";
            ddl_Region.DataValueField = "Region_Id";
            ddl_Region.DataBind();
            ddl_Region.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }

    public DataSet BindChkListFormType
    {
        set
        {
            ChkList_FormType.DataTextField = "Form_Name";
            ChkList_FormType.DataValueField = "Form_ID";
            ChkList_FormType.DataSource = value;
            ChkListSessionForm = value;
            ChkList_FormType.DataBind();

            int i;
            if (keyID > 0)
            {
                for (i = 0; i < ChkListSessionForm.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ChkListSessionForm.Tables[0].Rows[i]["Checked"]))
                    {
                        ChkList_FormType.Items[i].Selected = true;
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


        if (txt_StateName.Text == string.Empty)
        {
            errorMessage = "Please Enter State Name";// GetLocalResourceObject("Msg_State").ToString();
            _isValid = false;
        }
        else if (ddl_Region.SelectedValue == "0")
        {
            errorMessage = "Please Select Region Name";/// GetLocalResourceObject("Msg_ddlRegion").ToString();
            _isValid = false;
        }
        else if (txt_NsdlCode.Text.Trim().Length != 2 || Convert.ToInt32(txt_NsdlCode.Text) <=0  )
        {
            errorMessage = "Enter Valid State GST Code.";/// GetLocalResourceObject("Msg_ddlRegion").ToString();
            _isValid = false;
        }
        else if (txt_StateCode.Text.Trim().Length != 2 )
        {
            errorMessage = "Enter Valid State Code.";/// GetLocalResourceObject("Msg_ddlRegion").ToString();
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
            //return 2;
        }
    }

    #endregion

    #region OtherMethods   

    private DataSet MakeDS()
    {
        int cnt;
       
        DataSet objDS;
        objDS = ChkListSessionForm;
        objDS.Tables[0].TableName = "SessionStateFormDetails";
        objDS.Clear();


        for (cnt = 0; cnt < ChkList_FormType.Items.Count; cnt++)
        {

            if (ChkList_FormType.Items[cnt].Selected == true)
            {
                DR = objDS.Tables[0].NewRow();
                DR["Form_ID"] = ChkList_FormType.Items[cnt].Value;

                objDS.Tables["SessionStateFormDetails"].Rows.Add(DR);
            }


        }


        ChkListSessionForm = objDS;
        return objDS;

    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucState.ascx.resx");
        //}
        objStatePresenter = new StatePresenter(this, IsPostBack);
        

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDS();
        objStatePresenter.Save();

    }

    protected void ddl_Region_SelectedIndexChanged(object sender, EventArgs e)
    {
        objStatePresenter.FillCountryLabel();
        if (ddl_Region.SelectedValue == "0")
        {
            lbl_Country.Text = "";
        }
    }

    #endregion


}
