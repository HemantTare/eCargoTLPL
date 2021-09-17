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
using Raj.EC.ControlsView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 07/10/2008
/// Description   : This Page is For Master Branch Required Forms Details
/// </summary>
/// 

public partial class Master_Branch_WucBranchRequiredForms : System.Web.UI.UserControl,IBranchRequiredFormsView
{
    #region ClassVariables
    BranchRequiredFormsPresenter objBranchRequiredFormsPresenter;
    private ScriptManager scm_BranchForms;
    private int _CityID;
    int i = 0;
    #endregion

    #region ControlsBind

    public int CityID
    {
        set { _CityID = value; }
        get { return _CityID; }
    }

    public DataTable BindRequiredForms
    {
        set
        {
            chk_List_Forms.DataTextField = "Form_Name";
            chk_List_Forms.DataValueField = "Form_Id";
            chk_List_Forms.DataSource = value;
            chk_List_Forms.DataBind();
            for (i = 0; i <= value.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(value.Rows[i]["Att"]) == true)
                {
                    chk_List_Forms.Items[i].Selected = true;
                }
            }
            SessionRequiredForms = value;
            upnl_chk_List_Forms.Update();
        }
    }
    public DataTable SessionRequiredForms
    {
        get { return StateManager.GetState<DataTable>("RequiredForms"); }
        set { StateManager.SaveState("RequiredForms", value); }
    }
    public String BranchRequiredFormsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            //_objDs.Tables.Add(SessionRequiredForms.Copy());

            DataTable dt =null;
            dt = new DataTable();
            dt.Columns.Add("Form_ID");
            DataRow dr;
            for (i = 0 ; i<= chk_List_Forms.Items.Count - 1; i++)
            {
                if (chk_List_Forms.Items[i].Selected == true)
                {
                    dr = dt.NewRow();
                    dr["Form_ID"] = chk_List_Forms.Items[i].Value;
                    dt.Rows.Add(dr);
                }
            }

            _objDs.Tables.Add(dt);

            _objDs.Tables[0].TableName = "RequiredForms_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_BranchForms = value; }
    }

     #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 12;
        }
    }
     #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        objBranchRequiredFormsPresenter = new BranchRequiredFormsPresenter(this, IsPostBack);

    }

    public void HandelsCityChangedEvent(object o, EventArgs e)
    {
        //IAddressView objIAddressView = (IAddressView)o;
        //CityID = objIAddressView.CityId;

        _CityID = Convert.ToInt32(o);
        objBranchRequiredFormsPresenter.fillValues();


    }



}
