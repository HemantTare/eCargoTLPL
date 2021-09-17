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

public partial class TrackNTrace_FrmGCStatusSMS : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    String GC_No,SMSYes;
    DataSet objDS = new DataSet();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (IsPostBack == false)
        //{
            GC_No = Request.QueryString["GC_No"];
            lbl_LRNo.Text = GC_No;
               
            SMSYes = Util.DecryptToString(Request.QueryString["Msg"].ToString());

            if (SMSYes == "SMSYes")
            {
                tr_SMS1.Visible = true;
                tr_SMS2.Visible = true;
                tr_SMS3.Visible = true;
                tr_SMS4.Visible = true;
            }
            else
            {
                tr_SMS1.Visible = false;
                tr_SMS2.Visible = false;
                tr_SMS3.Visible = false;
                tr_SMS4.Visible = false;
            }

            CurrentStatus();
            

        //}

        txt_MobileNo.Focus();

    }


    private void CurrentStatus()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, GC_No) };

        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Current_Status_SMS", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lbl_MSG.Text = objDS.Tables[0].Rows[0]["StatusOnPage"].ToString();
            lbl_Status.Text = objDS.Tables[0].Rows[0]["Status"].ToString();
        }
        else
        {
            lbl_MSG.Text = "No Data Found"; 
        }
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


    private void Get_SMS()
    {
        DAL objDAL = new DAL();
        DataSet objDS = new DataSet();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, GC_No) };
        objDAL.RunProc("dbo.EC_Opr_TrackNTrace_Current_Status_SMS", objSqlParam, ref objDS);


        if (objDS.Tables[0].Rows.Count > 0)
        {

            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {

                String sendToPhoneNumber = txt_MobileNo.Text;
                string msg = objDS.Tables[0].Rows[0]["StatusSMS"].ToString();

                if (ValidateMobileDetails(sendToPhoneNumber, msg))
                {

                    String userid = "2000126072";
                    String passwd = "Rajan@1234"; // "Rajan@1234";


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


            if (objDS.Tables[0].Rows[0]["IsSMSDeliveryBranchAddress"].ToString() == "1")
            {
                Get_SMSBranchAddress(Convert.ToInt32(objDS.Tables[0].Rows[0]["CurrentBranchID"]));
            }
        }
    }


    private void Get_SMSBranchAddress(int BranchID)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] sqlPara = { objdal.MakeInParams("@Branch_ID", SqlDbType.Int, 0, BranchID),
         objdal.MakeInParams("@MobileNo", SqlDbType.VarChar , 20, txt_MobileNo.Text)};

        objdal.RunProc("EC_Opr_BranchAddress_Shared_SMS_MSG", sqlPara, ref ds);


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
                    String passwd = "Rajan@1234";


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

    private void Save_Details()
    {

       
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@GC_No", SqlDbType.Int, 0, GC_No), 
            objDAL.MakeInParams("@MobileNo", SqlDbType.VarChar, 10,txt_MobileNo.Text),
            objDAL.MakeInParams("@ClientName", SqlDbType.VarChar, 100,txt_ClientName.Text.ToUpper()),
            objDAL.MakeInParams("@CreatedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_GCTrace_Shared_With_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            ClearVariables();
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }

    }

    public void ClearVariables()
    {
        objDS = null;
    }

}
