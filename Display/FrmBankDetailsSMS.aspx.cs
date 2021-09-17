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

public partial class Display_FrmBankDetailsSMS : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        txt_MobileNo.Focus();

    }

    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_MobileNo.Text.Length < 10)
        {
            lbl_Errors.Text = "Enter Valid Mobile No.";
            txt_MobileNo.Focus();
        }
        else if (txt_ClientName.Text.Length < 3)
        {
            lbl_Errors.Text = "Enter Client Name Or Contact Person Name";
            txt_ClientName.Focus();
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
            Get_SMS();
            Save_Details();
        }
    }

    private void Save_Details()
    {

        String  BankName= ddlBank.SelectedItem.Text;

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@BankName", SqlDbType.VarChar ,100, BankName.ToUpper()), 
            objDAL.MakeInParams("@MobileNo", SqlDbType.VarChar, 10,txt_MobileNo.Text),
            objDAL.MakeInParams("@ClientName", SqlDbType.VarChar, 100,txt_ClientName.Text.ToUpper()),
            objDAL.MakeInParams("@CreatedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_BankDetails_Shared_With_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }

    }


    private void Get_SMS()
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        String BankName = ddlBank.SelectedItem.Text;

        SqlParameter[] sqlPara = { objdal.MakeInParams("@BankName", SqlDbType.VarChar , 100, BankName),
         objdal.MakeInParams("@MobileNo", SqlDbType.VarChar , 20, txt_MobileNo.Text)};

        objdal.RunProc("EC_Opr_BankDetails_Shared_SMS_MSG", sqlPara, ref ds);


        if (ds.Tables[0].Rows.Count > 0)
        {

            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {

                String sendToPhoneNumber = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                string msg = ds.Tables[0].Rows[0]["SMSMsg"].ToString();

                if (ValidateMobileDetails(sendToPhoneNumber, msg))
                {

                    String userid = "2000126072";
                    String passwd = "Rajan@1234"; //"Rajan@1234";


                    String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                    request = WebRequest.Create(url);

                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                    }
                    Stream stream = response.GetResponseStream();
                    Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader reader = new System.IO.StreamReader(stream, ec);
                    result = reader.ReadToEnd();
                    Console.WriteLine(result);
                    reader.Close();
                    stream.Close();
                }
            }
            catch (Exception exp)
            {
                string excep = exp.ToString();
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }

    public bool ValidateMobileDetails(String sendToPhoneNumber, string msg)
    {
        bool Is_Valid = false;
        if (sendToPhoneNumber == "0" || msg == "")
        {
            Is_Valid = false;
        }
        else
        {
            Is_Valid = true;
        }

        return Is_Valid;
    }
}
