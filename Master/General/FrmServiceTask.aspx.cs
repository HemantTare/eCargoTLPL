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
using Raj.EC;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

public partial class Master_General_FrmServiceTask : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;

    public int ServiceTaskID
    {
        set { hdnServiceTaskID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnServiceTaskID.Value); }
    }
    public string ServiceTask
    {
        set { txtServiceTask.Text = value; }
        get { return txtServiceTask.Text; }
    }
    public string Description
    {
        set { txtDescription.Text = value; }
        get { return txtDescription.Text; }
    }
    public bool IsActive
    {
        set { chkIsActive.Checked = value; }
        get { return chkIsActive.Checked; }
    }

    private string ErrorMsg
    {
        set { lbl_Errors.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ServiceTaskID = Util.DecryptToInt(Request.QueryString["Id"]);

            if (ServiceTaskID > 0)
            {
                TrIsActive.Visible = true;
                ReadValues();
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }
    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ServiceTask_ID", SqlDbType.Int, 0, ServiceTaskID) };

        objDAL.RunProc("EF_Mst_ServiceTask_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            ServiceTask = objDR["ServiceTask_Name"].ToString();
            Description = objDR["ServiceTask_Description"].ToString();
            IsActive = Util.String2Bool(objDR["Is_Active"].ToString());

        }
    }

    private bool AllowToSave()
    {
        bool ATS = false;
        lbl_Errors.Text = "Fields with * mark are mandatory";
        if (ServiceTask.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please enter Service Task Name";
            ScriptManager.SetFocus(txtServiceTask);
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    private Message Save()
    {

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@ServiceTask_ID", SqlDbType.Int, 0,ServiceTaskID ),
            objDAL.MakeInParams("@ServiceTask_Name", SqlDbType.VarChar, 100, ServiceTask),
            objDAL.MakeInParams("@Description", SqlDbType.VarChar, 250,Description),
            objDAL.MakeInParams("@IsActive", SqlDbType.Bit, 0,IsActive),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EF_Mst_ServiceTask_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }




}
