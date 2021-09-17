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
using ClassLibraryMVP.Security;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using Newtonsoft.Json.Linq;

public partial class Operations_Outward_FrmVehicleTrackingSMS : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    Raj.EC.Common objComm = new Raj.EC.Common();
    

    private DataSet objDS;

    public string Contact_Person1
    {
        set { txtContactPerson1.Text = value; }
        get { return txtContactPerson1.Text; }
    }

    public string Contact_Person2
    {
        set { txtContactPerson2.Text = value; }
        get { return txtContactPerson2.Text; }
    }

    public string Contact_1_MobileNo1
    {
        set { txt_Contact1MobileNo1.Text = value; }
        get { return txt_Contact1MobileNo1.Text; }
    }

    public string Contact_2_MobileNo1
    {
        set { txt_Contatc2MobileNo1.Text = value; }
        get { return txt_Contatc2MobileNo1.Text; }
    }
    public string Contact_1_MobileNo2
    {
        set { txt_Contact1MobileNo2.Text = value; }
        get { return txt_Contact1MobileNo2.Text; }
    }

    public string Contact_2_MobileNo2
    {
        set { txt_Contatc2MobileNo2.Text = value; }
        get { return txt_Contatc2MobileNo2.Text; }
    }

    public string Driver_Name
    {
        get { return txt_Driver.Text; }
        set { txt_Driver.Text = value; }
    }

    public int Driver_ID
    {
        get { return Util.String2Int(hdn_Driver.Value); }
        set { hdn_Driver.Value = Util.Int2String(value); }
    }

    public string DriverMobileNo
    {
        set { txt_DriverMobileNo.Text = value; }
        get { return txt_DriverMobileNo.Text; }
    }

    public string DriverMobileNo2
    {
        set { txt_DriverMobileNo2.Text = value; }
        get { return txt_DriverMobileNo2.Text; }
    }

    public string Sender_Name
    {
        set { txt_SenderName.Text = value; }
        get { return txt_SenderName.Text; }
    }

    public string Sender_MobileNo
    {
        set { txt_SenderMobileNo.Text = value; }
        get { return txt_SenderMobileNo.Text; }
    }

    public int VehicleID
    {
        get { return DDLVehicle.VehicleID; }
        set
        {
            DDLVehicle.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public string VehicleCategoryIds
    {
        get { return DDLVehicle.VehicleCategoryIds; }
        set
        {
            DDLVehicle.VehicleCategoryIds = value;
        }
    }

    private String NumberPart4
    {
        set { hdn_NumberPart4.Value = value.ToString(); }
        get { return hdn_NumberPart4.Value; }
    }

    private String shorturl
    {
        set { hdn_shorturl.Value = value.ToString(); }
        get { return hdn_shorturl.Value; }
    }

    public int City_Id
    {
        get { return Util.String2Int(hdn_CityId.Value); }
        set { hdn_CityId.Value = Util.Int2String(value); }
    }
    public string City_Name
    {
        get { return txt_City.Text; }
        set { txt_City.Text = value; }
    }
    public int Client_ID
    {
        get { return Util.String2Int(hdn_PartyId.Value); }
        set { hdn_PartyId.Value = Util.Int2String(value); }
    }
    public string Client_Name
    {
        get { return txt_Party.Text; }
        set { txt_Party.Text = value; }
    }
    public string Location
    {
        set { txt_CurrentLocation.Text = value; }
        get { return txt_CurrentLocation.Text; }
    }

    public string VehicleTime
    {
        set { Wuc_VehicleTime.setTime(value); }
        get { return Wuc_VehicleTime.getTime(); }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_SendSMS.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_SendSMS));
        VehicleCategoryIds = "";

        DDLVehicle.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            Wuc_VehicleTime.setTime("24");
            VehicleTime = DateTime.Now.ToShortTimeString();
            txt_City.Focus();
        }
        SetLinks();
        lst_City.Style.Add("visibility", "hidden");
        lst_Party.Style.Add("visibility", "hidden");
        lst_Driver.Style.Add("visibility", "hidden");
    }
    private void SetLinks()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        fRights = uObj.getForm_Rights(325);
        bool can_add = fRights.canAdd();

        if (can_add == true)
        {
            hdn_client_path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(325).AddUrl;
        }
        else
        {
            hdn_client_path.Value = "";
        }

        lbtn_AddClient.Visible = can_add;
    }

    public bool validateUI()
    {
        //SetShortUrl();

        bool _isValid = false;
        
        if (City_Id <= 0)
        {
            lbl_Errors.Text = "Select City Name";
            txt_City.Focus();
        }
        else if (Client_ID <= 0)
        {
            lbl_Errors.Text = "Select Client Name";
            txt_Party.Focus();
        }
        else if (Contact_1_MobileNo1.Length < 10 && Contact_1_MobileNo2.Length < 10 && Contact_2_MobileNo1.Length < 10 && Contact_2_MobileNo2.Length < 10)
        {
            lbl_Errors.Text = "Enter Mobile No. 1 Or Mobile No. 2 In Contact 1 Or Contact 2";
            txt_Contact1MobileNo1.Focus();
        }
        else if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Enter Vehicle No.";
            DDLVehicle.Focus();
        }
        else if (Driver_ID <= 0)
        {
            lbl_Errors.Text = "Enter Driver Name";
            txt_Driver.Focus();
        }
        else if (DriverMobileNo.Length < 10 && DriverMobileNo2.Length < 10)
        {
            lbl_Errors.Text = "Enter Driver Mobile No.";
            txt_DriverMobileNo.Focus();
        }
        else if (Location.Trim() == "")
        {
            lbl_Errors.Text = "Enter Vehicle Location";
            txt_CurrentLocation.Focus();
        }
        else if (Sender_Name.Trim() == "")
        {
            lbl_Errors.Text = "Enter Sender Name";
            txt_SenderName.Focus();
        }
        else if (Sender_MobileNo.Length < 10)
        {
            lbl_Errors.Text = "Enter Sender Mobile No.";
            txt_SenderMobileNo.Focus();
        }
        //else if (shorturl == "" )
        //{
        //    lbl_Errors.Text = "Vehicle Tracking URL Not Generated. Please Try Again. ";
        //    DDLVehicle.Focus();
        //}
        else
        {
            _isValid = true;
            lbl_Errors.Text = "";
        }

        return _isValid;
    }

    protected void btn_SendSMS_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save_Details();
        }
    }

    private void Save_Details()
    {
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Details_Id", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Client_Id", SqlDbType.Int , 0,Client_ID),
            objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0,VehicleID),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,Driver_ID),
            objDAL.MakeInParams("@Driver_Name", SqlDbType.VarChar , 50,Driver_Name.ToUpper()),
            objDAL.MakeInParams("@Driver_Mobile_1", SqlDbType.VarChar , 10,DriverMobileNo),
            objDAL.MakeInParams("@Driver_Mobile_2", SqlDbType.VarChar , 10,DriverMobileNo2),
            objDAL.MakeInParams("@ShortUrl", SqlDbType.VarChar , 100,shorturl),
            objDAL.MakeInParams("@Location", SqlDbType.VarChar , 20,Location.ToUpper()),
            objDAL.MakeInParams("@Time", SqlDbType.VarChar , 10,VehicleTime),
            objDAL.MakeInParams("@Sender_Name", SqlDbType.VarChar , 50,Sender_Name.ToUpper()),
            objDAL.MakeInParams("@Sender_Mobile", SqlDbType.VarChar , 10,Sender_MobileNo),

            objDAL.MakeInParams("@Contact_Person_1", SqlDbType.VarChar , 50,Contact_Person1),
            objDAL.MakeInParams("@Contact_1_Mobile_1", SqlDbType.VarChar , 10,Contact_1_MobileNo1),
            objDAL.MakeInParams("@Contact_1_Mobile_2", SqlDbType.VarChar , 10,Contact_1_MobileNo2),
            
            objDAL.MakeInParams("@Contact_Person_2", SqlDbType.VarChar , 50,Contact_Person2),
            objDAL.MakeInParams("@Contact_2_Mobile_1", SqlDbType.VarChar , 10,Contact_2_MobileNo1),
            objDAL.MakeInParams("@Contact_2_Mobile_2", SqlDbType.VarChar , 10,Contact_2_MobileNo2),
            
            objDAL.MakeInParams("@CreatedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_Vehicle_Tracking_Shared_With_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Get_Vehicle_Tracking_SMS(Convert.ToInt32(objSqlParam[2].Value));
            Response.Write("<script language='javascript'>{self.close()}</script>");
        }
    }

    protected void btn_hidden_Click(object sender, EventArgs e)
    {        
        DataSet ds = new DataSet();
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_ID", SqlDbType.VarChar, 15, Client_ID.ToString()) };

        objDAL.RunProc("dbo.EC_Mst_PTL_FTL_GetClientDetails", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];
            Contact_Person1 = objDR["Contact_Person1"].ToString();
            Contact_1_MobileNo1 = objDR["Contact_1_Mobile_1"].ToString();
            Contact_1_MobileNo2 = objDR["Contact_1_Mobile_2"].ToString();
            Contact_Person2 = objDR["Contact_Person2"].ToString();
            Contact_2_MobileNo1 = objDR["Contact_2_Mobile_1"].ToString();
            Contact_2_MobileNo2 = objDR["Contact_2_Mobile_2"].ToString();
            DDLVehicle.Focus();
        }
        else
        {
            Contact_Person1 = "";
            Contact_1_MobileNo1 = "";
            Contact_1_MobileNo2 = "";
            Contact_Person2 = "";
            Contact_2_MobileNo1 = "";
            Contact_2_MobileNo2 = "";
        }

        ScriptManager.SetFocus(txt_Contact1MobileNo1);
    }

    protected void btn_driver_Click(object sender, EventArgs e)
    {
        if (Driver_ID > 0)
        {
            Common obj = new Common();
            DataSet ds = obj.EC_Common_Pass_Query("select IsNull(Mobile_No,'') Mobile_No_1, IsNull(Phone_No,'') Mobile_No_2 from dbo.EF_Master_Driver where Driver_ID = " + hdn_Driver.Value);

            DriverMobileNo = ds.Tables[0].Rows[0][0].ToString();
            DriverMobileNo2 = ds.Tables[0].Rows[0][1].ToString();
        }
        else
        {
            DriverMobileNo = "";
            DriverMobileNo2 = "";
        }
        ScriptManager.SetFocus(txt_DriverMobileNo);
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCategoryIds = DDLVehicle.GetVehicleParameter("Vehicle_Category_ID");
        hdn_VehicleCategoryIds.Value = VehicleCategoryIds;

        SetVehicleInfoOnVehicleChanged();
    }
    public void SetVehicleInfoOnVehicleChanged()
    {
        if (VehicleID > 0)
        {
            hdn_VehicleID.Value = VehicleID.ToString();
            objDS = GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                
                Driver_Name = objDR["Driver_Name"].ToString();
                Driver_ID = Util.String2Int(objDR["Driver_ID"].ToString());

                DriverMobileNo = objDR["Mobile_No_1"].ToString();
                DriverMobileNo2 = objDR["Mobile_No_2"].ToString();
                NumberPart4 = objDR["Number_Part4"].ToString();
                shorturl = objDR["Short_Url"].ToString();
            }
            else
            {
                DriverMobileNo = "";
                DriverMobileNo2 = "";
                NumberPart4 = "";
                shorturl = "";
            }
            txt_Driver.Focus();
        }
    }
    public DataSet GetVehicleInformationOnVehicleChanged()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0, VehicleCategoryIds)};
        objDAL.RunProc("EC_Opr_LHPOHireDetails_GetValuesOnVahicleChanged", objSqlParam, ref objDS);
        return objDS;
    }

    public void Get_Vehicle_Tracking_SMS(int DetailsID)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] sqlPara = { objdal.MakeInParams("@DetailsID", SqlDbType.Int, 0, DetailsID) };
        objdal.RunProc("Ec_Opr_Get_Vehicle_Tracking_SMS_MSG", sqlPara, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable SessionDT = (DataTable)ds.Tables[0];

            int i;
            string MobileNo, SMSMessage;

            if (SessionDT.Rows.Count > 0)
            {
                for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
                {
                    MobileNo = "";
                    SMSMessage = "";
                    MobileNo = Convert.ToString(SessionDT.Rows[i]["Mobile_No"]);
                    SMSMessage = Convert.ToString(SessionDT.Rows[i]["SMSMessage"]);
                    Sent_Vehicle_Tracking_SMS(MobileNo, SMSMessage);
                }
            }
        }
    }

    public void Sent_Vehicle_Tracking_SMS(string MobileNo, string SMSMessage)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            String sendToPhoneNumber = MobileNo;
            string msg = SMSMessage;

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
    private void SetShortUrl()
    {

        string userkey = "fc4e5fa83a1abae40b2abdfacbb8cd130c078cb0";

        //NumberPart4 = "6516";

        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            String url = "http://speed.elixiatech.com/modules/api/vts/api.php?action=getvehicledata&jsonreq={%22userkey%22:%22" + userkey + "%22,%22searchstring%22:%22" + NumberPart4 + "%22}";

            request = WebRequest.Create(url);

            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            Stream stream = response.GetResponseStream();
            Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new System.IO.StreamReader(stream, ec);
            result = reader.ReadToEnd();

            Newtonsoft.Json.Linq.JObject details = Newtonsoft.Json.Linq.JObject.Parse(result);

            string res = details["Result"].Last.ToString();
            Newtonsoft.Json.Linq.JObject d = Newtonsoft.Json.Linq.JObject.Parse(res);

            //shorturl = d["shorturl"].ToString();
            hdn_shorturl.Value = d["shorturl"].ToString();

            reader.Close();
            stream.Close();
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