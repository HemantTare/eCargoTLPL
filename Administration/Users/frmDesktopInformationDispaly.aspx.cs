using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.eCargo;
using Raj.EC;
using ClassLibrary;

public partial class Admin_Users_frmDesktopInformationDispaly : System.Web.UI.Page
{
    Common objcomm = new Common();
    DataSet objds = new DataSet();
    bool IS_VT;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            
            objds = objcomm.Get_Values_Where("EC_Master_VTrans_Desktop_Information", "*", "", "ServiceInfoLine1", false);
            
            txt_SIMLine1.Text = objds.Tables[0].Rows[0]["ServiceInfoLine1"].ToString();
            txt_SIMLine2.Text = objds.Tables[0].Rows[0]["ServiceInfoLine2"].ToString();
            txt_SIMLine3.Text = objds.Tables[0].Rows[0]["ServiceInfoLine3"].ToString();

            txt_GIMLine1.Text = objds.Tables[0].Rows[0]["GeneralInfoLine1"].ToString();
            txt_GIMLine2.Text = objds.Tables[0].Rows[0]["GeneralInfoLine2"].ToString();
            txt_GIMLine3.Text = objds.Tables[0].Rows[0]["GeneralInfoLine3"].ToString();
        }
    }

    protected void On_btnSave_Click(object sender, EventArgs e)
    {
        Application["DesktopInfo"] = null;
        DAL _objDAL = new DAL();
        SqlParameter[] sqlParam = { _objDAL.MakeInParams("@Is_VT", SqlDbType.Bit,1,1),
                                        _objDAL.MakeInParams("@ServiceInfoLine1", SqlDbType.VarChar,100,txt_SIMLine1.Text) ,
                                        _objDAL.MakeInParams("@ServiceInfoLine2", SqlDbType.VarChar,100,txt_SIMLine2.Text) ,
                                        _objDAL.MakeInParams("@ServiceInfoLine3", SqlDbType.VarChar,100,txt_SIMLine3.Text),
                                        _objDAL.MakeInParams("@ServiceInfoLine4", SqlDbType.VarChar,100,""),
                                        _objDAL.MakeInParams("@ServiceInfoLine5", SqlDbType.VarChar,100,""),
                                        _objDAL.MakeInParams("@GeneralInfoLine1", SqlDbType.VarChar,100,txt_GIMLine1.Text),
                                        _objDAL.MakeInParams("@GeneralInfoLine2", SqlDbType.VarChar,100,txt_GIMLine2.Text),
                                        _objDAL.MakeInParams("@GeneralInfoLine3", SqlDbType.VarChar,100,txt_GIMLine3.Text),                                                                    
                                        _objDAL.MakeInParams("@GeneralInfoLine4", SqlDbType.VarChar,100,""),
                                        _objDAL.MakeInParams("@GeneralInfoLine5", SqlDbType.VarChar,100,"")};

        _objDAL.RunProc("dbo.EC_Master_Desktop_Message_Save", sqlParam);

        Response.Redirect("~/display/CloseForm.aspx");
    }
}
