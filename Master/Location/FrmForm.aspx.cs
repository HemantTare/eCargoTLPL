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
using System.Data.SqlClient;
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_Location_FrmForm : ClassLibraryMVP.UI.Page
{
    #region members
    public bool validateUI()
    {
        return true;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            FillDropdowns();
            ReadValues();
        }
    }

    private void FillDropdowns()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        objDAL.RunProc("EC_Mst_City_FillStateValues", ref ds);

        ddlFromState.DataTextField = "State_Name";
        ddlFromState.DataValueField = "State_Id";
        ddlFromState.DataSource = ds;
        ddlFromState.DataBind();
        Common.InsertItem(ddlFromState);

        ddlToState.DataTextField = "State_Name";
        ddlToState.DataValueField = "State_Id";
        ddlToState.DataSource = ds;
        ddlToState.DataBind();
        Common.InsertItem(ddlToState);
    }

    private void ReadValues()
    {
        if (Util.String2Int(hdnKeyID.Value) > 0)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@FormID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
            objDAL.RunProc("EC_Mst_Form_ReadValues", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = ds.Tables[0].Rows[0];

                txtFormName.Text = objDR["FormName"].ToString();
                ddlFromState.SelectedValue = objDR["FromStateID"].ToString();
                ddlToState.SelectedValue = objDR["ToStateID"].ToString();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
       objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
       objDAL.MakeOutParams("@FormID", SqlDbType.Int, 0), 
       objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, Util.String2Int(hdnKeyID.Value)),
       objDAL.MakeInParams("@FormName", SqlDbType.VarChar, 50,txtFormName.Text), 
        objDAL.MakeInParams("@FromStateID",SqlDbType.Int,0, Util.String2Int(ddlFromState.SelectedValue)),
        objDAL.MakeInParams("@ToStateID",SqlDbType.Int,0, Util.String2Int(ddlToState.SelectedValue)),
       objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};


        objDAL.RunProc("[dbo].[EC_Mst_FormSave]", objSqlParam);


        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
        }
        if (objMessage.messageID == 2627)
        {
            lblErrors.Text = "Duplicate Form Name";
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }
}
