using System;
using System.Data;
using System.Web.UI.WebControls;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using System.Data.Sql;
using System.Data.SqlClient;


public partial class Finance_IBT_WucVoucherForApprovalRemark : System.Web.UI.UserControl
{
    DAL objDAL = new DAL();
    Common objCommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Submit.Attributes.Add("onclick",objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Submit));

        WucVoucher1.Tr_Heading = false;
       
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
       if (validate())
        {
            Save();
           
            string CloseScript = "<script language='javascript'> " + "window.opener.location.reload();window.close();" + "</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string),"CloseScript", CloseScript);
        }

    }


    public void Save()
    {
        Message objmsg = new Message();

        SqlParameter[] sqlparam = {
                                   objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,UserManager.getUserParam().HierarchyCode),
                                   objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,UserManager.getUserParam().MainId),
                                   objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int, 0,Util.DecryptToInt(Request.QueryString["Id"])), 
                                   objDAL.MakeInParams("@Division_ID", SqlDbType.VarChar, 10,UserManager.getUserParam().DivisionId), 
                                   objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,txt_Remarks.Text.Trim())
                                   //objDAL.MakeInParams("@Ledger_Id", SqlDbType.VarChar,0,Util.DecryptToInt(Convert.ToInt16(Request.QueryString["Ledger_ID"])))
                                 };

        objDAL.RunProc("FA_Opr_IBT_VoucherReject_Save", sqlparam);

        objmsg.messageID = Convert.ToInt32(sqlparam[0].Value);
        objmsg.message = sqlparam[1].Value.ToString();
    }

    public bool validate()
    {
        if (txt_Remarks.Text.Trim() == "")
        {
            lbl_Error.Text = "Please Enter Remark";
            return false;
        }
        else
        {
            return true;
        }
    }
}
