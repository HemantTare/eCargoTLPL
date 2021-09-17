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
using Raj.EC.GeneralPresenter;
using Raj.EC.GeneralView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;



public partial class Master_General_WucReason : System.Web.UI.UserControl,IReasonView
{
    #region ClassVariables
    ReasonPresenter objReasonPresenter;
    DataRow DR = null;
    #endregion

    #region ControlsValue

    public string Reason
    {
        set { txt_Reason.Text = value; }
        get { return txt_Reason.Text; }
    }
    public string Description
    {
        set { txt_Description.Text = value; }
        get { return txt_Description.Text; }
    }
    public int ChkProcess
    {
        set
        {
            Chk_ListReasonProcess.SelectedValue = Util.Int2String(value);

        }
        get
        {
            return Util.String2Int(Chk_ListReasonProcess.SelectedValue);
        }
    }

    public string SessionReasonProcessDetails
    {
        get
        {
            return ChkListSessionProcess.GetXml();
        }
    }
    #endregion

    #region ControlsBind
    public DataSet ChkListSessionProcess
    {
        get { return StateManager.GetState<DataSet>("ReasonProcess"); }
        set { StateManager.SaveState("ReasonProcess", value); }
    }
    public DataSet BindChkListReasonProcess
    {
        set
        {
            Chk_ListReasonProcess.DataTextField = "Process_Name";
            Chk_ListReasonProcess.DataValueField = "Process_Id";
            Chk_ListReasonProcess.DataSource = value;
            ChkListSessionProcess = value;
            Chk_ListReasonProcess.DataBind();

            int i;
            if (keyID > 0)
            {
                for (i = 0; i < ChkListSessionProcess.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ChkListSessionProcess.Tables[0].Rows[i]["Checked"]))
                    {
                        Chk_ListReasonProcess.Items[i].Selected = true;
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
        if (txt_Reason.Text == string.Empty)
        {
            errorMessage = "Please Enter Reason";// GetLocalResourceObject("Msg_Reason").ToString();
            _isValid = false;
        }
        else if (!ValidateProcess())
        {
            errorMessage = "Please select at least one process";
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
    //Added : Anita On: 18 Feb 09
    public void ClearVariables()
    {
        ChkListSessionProcess = null;      
    }

    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit: 21-02-09
    {
        ClearVariables();
    }

    #endregion

    #region OtherMethod
    private DataSet MakeDS()
    {
        int cnt;

        DataSet objDS;
        objDS = ChkListSessionProcess;
        objDS.Tables[0].TableName = "SessionReasonProcessDetails";
        objDS.Clear();


        for (cnt = 0; cnt < Chk_ListReasonProcess.Items.Count; cnt++)
        {

            if (Chk_ListReasonProcess.Items[cnt].Selected == true)
            {
                DR = objDS.Tables[0].NewRow();
                DR["Process_ID"] = Chk_ListReasonProcess.Items[cnt].Value;

                objDS.Tables["SessionReasonProcessDetails"].Rows.Add(DR);
            }


        }

        ChkListSessionProcess = objDS;
        return objDS;
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/General/App_LocalResources/WucReason.ascx.resx");
        //}
        objReasonPresenter = new ReasonPresenter(this, IsPostBack);       

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDS();
        objReasonPresenter.Save();

    }

    public bool ValidateProcess()
    {
        bool _isValid = false;
        for (int cnt = 0; cnt < Chk_ListReasonProcess.Items.Count; cnt++)
        {

            if (Chk_ListReasonProcess.Items[cnt].Selected)
            {
                _isValid = true;
                return _isValid;
            }
           
        }
        return _isValid;


    }
    #endregion
}
