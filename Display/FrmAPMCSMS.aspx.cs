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
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

public partial class Display_FrmAPMCSMS : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        ddlDestinaion.Focus();

    }

    public bool validateUI()
    {
        bool _isValid = false;

        if (ddlDestinaion.SelectedItem.Value  == "0")
        {
            lbl_Errors.Text = "Select City.";
            ddlDestinaion.Focus();
        }
        else if (WucVehicleSearch1.VehicleID  <= 0)
        {
            lbl_Errors.Text = "Enter Vehicle No.";
            WucVehicleSearch1.Focus();
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    protected void btn_SendSMS_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            btn_SendSMS.Enabled = false;
            Save_Details();
        }
    }

    private void Save_Details()
    {


        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Destination", SqlDbType.VarChar , 20, ddlDestinaion.SelectedItem.Text  ), 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int , 0,WucVehicleSearch1.VehicleID),
            objDAL.MakeInParams("@CreatedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_APMC_SMS_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }

    }


}
