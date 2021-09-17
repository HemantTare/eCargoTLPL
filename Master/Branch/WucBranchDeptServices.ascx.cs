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
/// Author        : Shiv kumar mishra
/// Created On    : 07/10/2008
/// Description   : This Page is For Master Branch Department/Services Details
/// </summary>
/// 

public partial class Master_Branch_WucBranchDeptServices : System.Web.UI.UserControl,IBranchDeptServiceView
{
    #region ClassVariables
    BranchDeptServicePresenter objBranchDeptServicePresenter;
    private ScriptManager scm_BranchDept;
    int i = 0;
    int _DeliveryAtID = 0;
    int _DeliveryHubID = 0;
    #endregion

    #region ControlsValues

    public bool IsBookingAllowed
    {
        set { chkbx_isBookingallowed.Checked = value; }
        get { return chkbx_isBookingallowed.Checked; }
    }

    public bool IsDeliveryAllowed
    {
        set{chkbx_isDelivaryallowed.Checked = value; }
        get { return chkbx_isDelivaryallowed.Checked; }
    }
         
    public bool IsCrossingBranch
    {
        set {chkbx_isCrossingbranch.Checked = value;}
        get { return chkbx_isCrossingbranch.Checked;}
    }

    public bool IsFranchiseeBranch
    {
        set { chkbx_isFranchiseebranch.Checked = value; }
        get { return chkbx_isFranchiseebranch.Checked; }
    }
    public bool IsComputersiedBranch
    {
        set { chkbx_isComputerisedbranch.Checked = value; }
        get { return chkbx_isComputerisedbranch.Checked; }
    }
    public bool IsOctroiApplicable
    {
        set { chkbx_isOctroiApplicable.Checked = value; }
        get { return chkbx_isOctroiApplicable.Checked; }
    }

    public int DeliveryAtID
    {
        set { _DeliveryAtID = value < 0 ? 0 : value; }
        get { return _DeliveryAtID; }
    }
    public int DeliveryHubID
    {
        set { _DeliveryHubID = value < 0 ? 0 : value; }
        get { return _DeliveryHubID; }
    }
    #endregion

    #region ControlsBind
       
    public DataTable BindDepartment
    {
        set
        {
            chk_List_Department.DataTextField = "Department_Name";
            chk_List_Department.DataValueField = "Department_Id";
            chk_List_Department.DataSource = value;
            chk_List_Department.DataBind();

            for (i = 0; i <= value.Rows.Count - 1; i++ )
            {
                if (Convert.ToBoolean(value.Rows[i]["Att"]) == true)
                {
                    chk_List_Department.Items[i].Selected = true;
                }
            }

            SessionDepartment = value;
        }
    }

    public DataTable SessionDepartment
    {
        get { return StateManager.GetState<DataTable>("Department"); }
        set { StateManager.SaveState("Department", value); }
    }
    public String BranchDepartmentXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            //_objDs.Tables.Add(SessionDepartment.Copy());

            DataTable dt = null;
            dt = new DataTable();
            dt.Columns.Add("Department_ID");
            DataRow dr;
            for (i = 0; i <= chk_List_Department.Items.Count - 1; i++)
            {
                if (chk_List_Department.Items[i].Selected == true)
                {
                    dr = dt.NewRow();
                    dr["Department_ID"] = chk_List_Department.Items[i].Value;
                    dt.Rows.Add(dr);
                }
            }

            _objDs.Tables.Add(dt);

            _objDs.Tables[0].TableName = "Department_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_BranchDept = value; }
    }

    public void HandelsDefaultHubChangedEvent(object o, EventArgs e)
    {

        _DeliveryHubID = Convert.ToInt32(o);

        if (_DeliveryHubID <= 0 || _DeliveryHubID == keyID)
        {
            chkbx_isCrossingbranch.Checked = true;
            chkbx_isCrossingbranch.Enabled = false;
            //UpdatePanel1.Update();
        }
        else
        {
            //chkbx_isCrossingbranch.Checked = false;
            chkbx_isCrossingbranch.Checked = false;
            chkbx_isCrossingbranch.Enabled = true;
            //UpdatePanel1.Update();
        }


    }

    public void HandelsDeliveryAtChangedEvent(object o, EventArgs e)
    {

        _DeliveryAtID = Convert.ToInt32(o);

        if (_DeliveryAtID <= 0 || _DeliveryAtID == keyID)
        {
            chkbx_isDelivaryallowed.Checked = true;
            chkbx_isDelivaryallowed.Enabled = false;
            //UpdatePanel2.Update();
        }
        else
        {
            chkbx_isDelivaryallowed.Checked = false;
            chkbx_isDelivaryallowed.Enabled = true;
            //UpdatePanel2.Update();
        }

      

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
        if (!IsPostBack)
        {

            Check_EnableDisable();
        }

        objBranchDeptServicePresenter = new BranchDeptServicePresenter(this, IsPostBack);

       
    }

    private void Check_EnableDisable()
    {

        if (keyID > 0)
        {
            if (DeliveryAtID <= 0 || DeliveryAtID == keyID)
            {
                chkbx_isDelivaryallowed.Enabled = false;
            }
            else
            {
                chkbx_isDelivaryallowed.Enabled = true;
            }

            if (DeliveryHubID <= 0 || DeliveryHubID == keyID)
            {
                chkbx_isCrossingbranch.Enabled = false;
            }
            else
            {
                chkbx_isCrossingbranch.Enabled = true;
            }
        }
        else
        {
            chkbx_isDelivaryallowed.Checked = true;
            chkbx_isDelivaryallowed.Enabled = false;

            chkbx_isCrossingbranch.Checked = true;
            chkbx_isCrossingbranch.Enabled = false;
        }
    }
}
