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


public partial class Master_Location_WucAreaDepartment : System.Web.UI.UserControl, IAreaDepartmentView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    AreaDepartmentPresenter objAreaDepartmentPresenter;
    private ScriptManager scm_AreaDept;

    #endregion

    #region ControlsValues

    public Decimal CashLimit
    {
        set { txt_CashLimit.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_CashLimit.Text.Trim() == string.Empty ? "0.00" : txt_CashLimit.Text.Trim()); }
    }
    public Decimal BankLimit
    {
        set { txt_BankLimit.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_BankLimit.Text.Trim() == string.Empty ? "0.00" : txt_BankLimit.Text.Trim()); }

    }
    public int ChkListDepartment
    {
        set
        {
            chk_ListDepartment.SelectedValue = Util.Int2String(value);

        }
        get
        {
            return Util.String2Int(chk_ListDepartment.SelectedValue);
        }
    }

    public string SessionChkListDepartmentDetails
    {
        get
        {
            return SessionChkListDepartment.GetXml();
        }

    }
    public int BankLedgerId
    {
        set { ddl_BankLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BankLedger.SelectedValue); }
    }
    public int CashLedgerId
    {
        set { ddl_CashLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_CashLedger.SelectedValue); }
    }
    #endregion

    #region ControlBind
    public DataSet SessionChkListDepartment
    {
        get { return StateManager.GetState<DataSet>("AreaDepartment"); }
        set { StateManager.SaveState("AreaDepartment", value); }
    }

    public DataSet BindChkListDepartment
    {
        set
        {
            chk_ListDepartment.DataTextField = "Department_Name";
            chk_ListDepartment.DataValueField = "Department_Id";
            chk_ListDepartment.DataSource = value;
            SessionChkListDepartment = value;
            chk_ListDepartment.DataBind();

            int i;
            if (keyID > 0)
            {
                for (i = 0; i < SessionChkListDepartment.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(SessionChkListDepartment.Tables[0].Rows[i]["Checked"]))
                    {
                        chk_ListDepartment.Items[i].Selected = true;
                    }
                }
            }
        }
    }
    public DataTable BindCashLedger
    {
        set
        {
            ddl_CashLedger.DataTextField = "Ledger_Name";
            ddl_CashLedger.DataValueField = "Ledger_Id";
            ddl_CashLedger.DataSource = value;
            ddl_CashLedger.DataBind();

            ddl_CashLedger.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindBankLedger
    {
        set
        {
            ddl_BankLedger.DataTextField = "Ledger_Name";
            ddl_BankLedger.DataValueField = "Ledger_Id";
            ddl_BankLedger.DataSource = value;
            ddl_BankLedger.DataBind();

            ddl_BankLedger.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    #endregion

    #region OtherMethod
    public DataSet MakeDSDepartment()
    {
        int cnt;

        DataSet objDS;
        DataRow DR = null;
        objDS = SessionChkListDepartment;
        objDS.Tables[0].TableName = "SessionChkListDepartmentDetails";
        objDS.Clear();

        for (cnt = 0; cnt < chk_ListDepartment.Items.Count; cnt++)
        {

            if (chk_ListDepartment.Items[cnt].Selected == true)
            {
                DR = objDS.Tables[0].NewRow();
                DR["Department_ID"] = chk_ListDepartment.Items[cnt].Value;

                objDS.Tables["SessionChkListDepartmentDetails"].Rows.Add(DR);
            }
        }
        SessionChkListDepartment = objDS;
        return objDS;
    }

    public ScriptManager SetScriptManager
    {
        set { scm_AreaDept = value; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_CashLimit.Text == string.Empty && pc.Control_Is_Mandatory(txt_CashLimit) == true)
        {
            errorMessage = "Please Enter Cash Limit";// GetLocalResourceObject("Msg_CashLimit").ToString();
            _isValid = false;
        }
        else if (txt_BankLimit.Text == string.Empty && pc.Control_Is_Mandatory(txt_BankLimit) == true)
        {
            errorMessage = "Please Enter Bank Limit";// GetLocalResourceObject("Msg_BankLimit").ToString();
            _isValid = false;
        }
        if (Util.String2Int(ddl_CashLedger.SelectedValue) == 0 && pc.Control_Is_Mandatory(ddl_CashLedger) == true)
        {
            lbl_Errors.Text = "Please Select  Cash Ledger";
            _isValid = false;
            ddl_CashLedger.Focus();
        }
        else if (Util.String2Int(ddl_BankLedger.SelectedValue) == 0 && pc.Control_Is_Mandatory(ddl_BankLedger) == true)
        {
            lbl_Errors.Text = "Please Select DefaultBank Ledger";
            _isValid = false;
            ddl_BankLedger.Focus();
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
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucAreaDepartment.ascx.resx");
        //}
        objAreaDepartmentPresenter = new AreaDepartmentPresenter(this, IsPostBack);

    }

    #endregion
}