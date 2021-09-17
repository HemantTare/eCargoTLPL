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

public partial class Operations_Outward_FrmMultipleVehicleTrackingSMS : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    Raj.EC.Common objComm = new Raj.EC.Common();
    private DataSet objDS;
    TextBox txt_VehicleNo, txt_Driver, txt_Mobile1, txt_Mobile2, txt_VehLocation;
    DataRow dr;
    ListBox lst_VehicleNo, lst_Driver;
    LinkButton lbtn_Add_Commodity;
    HiddenField hdn_VehicleId, hdn_DriverId;
    TimePicker tp;
    bool Allow_To_Save;

    private void Bind_dg_Vehicles()
    {
        dg_Vehicles.DataSource = Session_MultipleVehicleGrid;
        dg_Vehicles.DataBind();
    }

    public DataTable Session_MultipleVehicleGrid
    {
        get { return StateManager.GetState<DataTable>("MultipleVehicleGrid"); }
        set { StateManager.SaveState("MultipleVehicleGrid", value); }
    }
    public String MultipleVehicleXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_MultipleVehicleGrid.Copy());
            _objDs.Tables[0].TableName = "VehicleDetails";
            return _objDs.GetXml().ToLower();
        }
    }
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        btn_SendSMS.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_SendSMS));

        if (!IsPostBack)
        {
            //txt_City.Focus();
            binddetails();

            btn_hidden_Click(sender, e);
        }
        SetLinks();
        lst_City.Style.Add("visibility", "hidden");
        lst_Party.Style.Add("visibility", "hidden");
    }

    private void binddetails()
    {

        Client_ID = 2;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn vc = dt.Columns.Add("VehicleID", typeof(Int32));
        vc.AllowDBNull = false;
        vc.Unique = true;
        dt.Columns.Add("VehicleNo", typeof(String));
        dt.Columns.Add("DriverID", typeof(Int32));
        dt.Columns.Add("DriverName", typeof(String));
        dt.Columns.Add("MobileNo1", typeof(String));
        dt.Columns.Add("MobileNo2", typeof(String));
        dt.Columns.Add("VehicleLocation", typeof(String));
        dt.Columns.Add("CurrentTime", typeof(String));

        Session_MultipleVehicleGrid = dt;
        Bind_dg_Vehicles();
    }
    #region grid binding
    protected void dg_Vehicles_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_VehicleNo = (TextBox)(e.Item.FindControl("txt_VehicleNo"));
                hdn_VehicleId = (HiddenField)(e.Item.FindControl("hdn_VehicleId"));
                lst_VehicleNo = (ListBox)(e.Item.FindControl("lst_VehicleNo"));

                txt_Driver = (TextBox)(e.Item.FindControl("txt_Driver"));
                hdn_DriverId = (HiddenField)(e.Item.FindControl("hdn_DriverId"));
                lst_Driver = (ListBox)(e.Item.FindControl("lst_Driver"));

                txt_Mobile1 = (TextBox)(e.Item.FindControl("txt_Mobile1"));
                txt_Mobile2 = (TextBox)(e.Item.FindControl("txt_Mobile2"));
                txt_VehLocation = (TextBox)(e.Item.FindControl("txt_VehLocation"));
                tp = (TimePicker)(e.Item.FindControl("Wuc_VehicleTime"));
                tp.setTime("24");
                tp.setTime(DateTime.Now.ToShortTimeString());

                /////VehicleNo///
                txt_VehicleNo.Attributes.Add("onblur", "On_txtVehicleLostFocus('" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "','" + hdn_VehicleId.ClientID + "','" + hdn_DriverId.ClientID + "','" + txt_Driver.ClientID + "','" + txt_Mobile1.ClientID + "','" + txt_Mobile2.ClientID + "')");
                txt_VehicleNo.Attributes.Add("onkeyup", "Search_txtMultiSearch(event,'" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "','Vehicle',3)");
                txt_VehicleNo.Attributes.Add("onfocus", "On_Focus_Grid('" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "')");
                txt_VehicleNo.Attributes.Add("onkeydown", "return on_keydown(event,'" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "')");

                lst_VehicleNo.Attributes.Add("onfocus", "listboxonfocus('"  + txt_VehicleNo.ClientID + "')");

                ////// Driver///
                txt_Driver.Attributes.Add("onblur", "On_txtDriverLostFocus('" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "','" + hdn_DriverId.ClientID + "','" + txt_Mobile1.ClientID + "','" + txt_Mobile2.ClientID + "')");
                txt_Driver.Attributes.Add("onkeyup", "Search_txtMultiSearch(event,'" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "','Driver',2)");
                txt_Driver.Attributes.Add("onfocus", "On_Focus_Grid('" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "')");
                txt_Driver.Attributes.Add("onkeydown", "return on_keydown(event,'" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "')");

                lst_Driver.Attributes.Add("onfocus", "listboxonfocus('" + txt_Driver.ClientID + "')");

                lst_VehicleNo.Style.Add("visibility", "hidden");
                lst_Driver.Style.Add("visibility", "hidden");
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                dr = Session_MultipleVehicleGrid.Rows[e.Item.ItemIndex];
                ScriptManager.SetFocus(txt_VehicleNo);

                txt_VehicleNo.Text = dr["VehicleNo"].ToString();
                hdn_VehicleId.Value = dr["VehicleID"].ToString();
                txt_Driver.Text = dr["DriverName"].ToString();
                hdn_DriverId.Value = dr["DriverID"].ToString();
                txt_Mobile1.Text = dr["MobileNo1"].ToString();
                txt_Mobile2.Text = dr["MobileNo2"].ToString();
                txt_VehLocation.Text = dr["VehicleLocation"].ToString();
                tp.setTime(dr["CurrentTime"].ToString());

                lst_VehicleNo.Style.Add("visibility", "hidden");
                lst_Driver.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void dg_Vehicles_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Vehicles.EditItemIndex = e.Item.ItemIndex;
        dg_Vehicles.ShowFooter = false;
        Bind_dg_Vehicles();
        //ErrorMsg = "";
        ScriptManager.SetFocus(txt_VehicleNo);
    }
    protected void dg_Vehicles_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Vehicles.EditItemIndex = -1;
        dg_Vehicles.ShowFooter = true;
        if (Session_MultipleVehicleGrid.Rows.Count == 3)
            dg_Vehicles.ShowFooter = false;

        Bind_dg_Vehicles();
        //ErrorMsg = "";

        ScriptManager.SetFocus(txt_VehicleNo);

    }
    protected void dg_Vehicles_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_MultipleVehicleGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_MultipleVehicleGrid.AcceptChanges();
            dg_Vehicles.EditItemIndex = -1;
            dg_Vehicles.ShowFooter = true;
            Bind_dg_Vehicles();
        }

        ScriptManager.SetFocus(txt_VehicleNo);

    }
    protected void dg_Vehicles_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Vehicle_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_Vehicles.EditItemIndex = -1;
                dg_Vehicles.ShowFooter = true;
                if (Session_MultipleVehicleGrid.Rows.Count == 12)
                    dg_Vehicles.ShowFooter = false;
                Bind_dg_Vehicles();
            }
        }
        catch (ConstraintException)
        {
            //ErrorMsg = "Duplicate Packing Type,Item And Size";
            ScriptManager.SetFocus(txt_VehicleNo);
        }
    }
    protected void dg_Vehicles_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                Insert_Update_Vehicle_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Vehicles();
                    dg_Vehicles.EditItemIndex = -1;

                    if (Session_MultipleVehicleGrid.Rows.Count == 12)
                        dg_Vehicles.ShowFooter = false;
                }
            }
            catch (ConstraintException)
            {
                //ErrorMsg = "Duplicate Packing Type,Item And Size";
                ScriptManager.SetFocus(txt_VehicleNo);
            }
        }
    }

    private void Insert_Update_Vehicle_Dataset(object source, DataGridCommandEventArgs e)
    {
        txt_VehicleNo = (TextBox)(e.Item.FindControl("txt_VehicleNo"));
        hdn_VehicleId = (HiddenField)(e.Item.FindControl("hdn_VehicleId"));
        lst_VehicleNo = (ListBox)(e.Item.FindControl("lst_VehicleNo"));

        txt_Driver = (TextBox)(e.Item.FindControl("txt_Driver"));
        hdn_DriverId = (HiddenField)(e.Item.FindControl("hdn_DriverId"));
        lst_Driver = (ListBox)(e.Item.FindControl("lst_Driver"));

        txt_Mobile1 = (TextBox)(e.Item.FindControl("txt_Mobile1"));
        txt_Mobile2 = (TextBox)(e.Item.FindControl("txt_Mobile2"));
        txt_VehLocation = (TextBox)(e.Item.FindControl("txt_VehLocation"));
        tp = (TimePicker)(e.Item.FindControl("Wuc_VehicleTime"));
        if (Allow_To_Add_Update_Vehicles())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_MultipleVehicleGrid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_MultipleVehicleGrid.Rows[e.Item.ItemIndex];
            }

            dr["VehicleNo"] = txt_VehicleNo.Text.Trim();
            dr["VehicleID"] = hdn_VehicleId.Value;
            dr["DriverName"] = txt_Driver.Text.Trim();
            dr["DriverID"] = hdn_DriverId.Value;

            dr["MobileNo1"] = txt_Mobile1.Text.Trim() == string.Empty ? "" : txt_Mobile1.Text.Trim();
            dr["MobileNo2"] = txt_Mobile2.Text.Trim() == string.Empty ? "" : txt_Mobile2.Text.Trim();
            dr["VehicleLocation"] = txt_VehLocation.Text.Trim();
            dr["CurrentTime"] = tp.getTime();

            if (e.CommandName == "Add")
            {
                Session_MultipleVehicleGrid.Rows.Add(dr);
            }
        }

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                /////VehicleNo///
                txt_VehicleNo.Attributes.Add("onblur", "On_txtVehicleLostFocus('" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "','" + hdn_VehicleId.ClientID + "','" + hdn_DriverId.ClientID + "','" + txt_Driver.ClientID + "','" + txt_Mobile1.ClientID + "','" + txt_Mobile2.ClientID + "')");
                txt_VehicleNo.Attributes.Add("onkeyup", "Search_txtMultiSearch(event,'" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "','Vehicle',3)");
                txt_VehicleNo.Attributes.Add("onfocus", "On_Focus_Grid('" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "')");
                txt_VehicleNo.Attributes.Add("onkeydown", "return on_keydown(event,'" + txt_VehicleNo.ClientID + "','" + lst_VehicleNo.ClientID + "')");

                lst_VehicleNo.Attributes.Add("onfocus", "listboxonfocus('" + txt_VehicleNo.ClientID + "')");

                ////// Driver///
                txt_Driver.Attributes.Add("onblur", "On_txtDriverLostFocus('" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "','" + hdn_DriverId.ClientID + "','" + txt_Mobile1.ClientID + "','" + txt_Mobile2.ClientID + "')");
                txt_Driver.Attributes.Add("onkeyup", "Search_txtMultiSearch(event,'" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "','Driver',2)");
                txt_Driver.Attributes.Add("onfocus", "On_Focus_Grid('" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "')");
                txt_Driver.Attributes.Add("onkeydown", "return on_keydown(event,'" + txt_Driver.ClientID + "','" + lst_Driver.ClientID + "')");

                lst_Driver.Attributes.Add("onfocus", "listboxonfocus('" + txt_Driver.ClientID + "')");
            }
        }
    }
    public bool Allow_To_Add_Update_Vehicles()
    {
        Allow_To_Save = false;
        lbl_Errors.Text = "";

        if (Util.String2Int(hdn_VehicleId.Value) <= 0)
        {
            lbl_Errors.Text = "Please Select Vehicle No.";
            ScriptManager.SetFocus(txt_VehicleNo);
        }
        else if (Util.String2Int(hdn_DriverId.Value) <= 0)
        {
            lbl_Errors.Text = "Please Select Driver.";
            ScriptManager.SetFocus(txt_Driver);
        }
        else if (txt_Mobile1.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Mobile 1.";
            ScriptManager.SetFocus(txt_Mobile1);
        }
        else if (txt_Mobile1.Text.Trim().Length < 10)
        {
            lbl_Errors.Text = "Please Enter Valid Mobile 1.";
            ScriptManager.SetFocus(txt_Mobile1);
        }
        else if (txt_Mobile2.Text.Trim() != string.Empty && txt_Mobile2.Text.Trim().Length < 10)
        {
            lbl_Errors.Text = "Please Enter Valid Mobile 2.";
            ScriptManager.SetFocus(txt_Mobile2);
        }
        else if (txt_VehLocation.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Vehicle Location.";
            ScriptManager.SetFocus(txt_VehLocation);
        }
        else
            Allow_To_Save = true;

        return Allow_To_Save;
    }

    #endregion
    public bool validateUI()
    {
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
        else if (Session_MultipleVehicleGrid.Rows.Count <= 0)
        {
            lbl_Errors.Text = "Please Enter Vehicle No.";
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
        else
        {
            _isValid = true;
            lbl_Errors.Text = "";
        }

        return _isValid;
    }
    #region saving
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
            objDAL.MakeInParams("@Client_Id", SqlDbType.Int , 0,Client_ID),
            objDAL.MakeInParams("@Sender_Name", SqlDbType.VarChar , 50,Sender_Name.ToUpper()),
            objDAL.MakeInParams("@Sender_Mobile", SqlDbType.VarChar , 10,Sender_MobileNo),
            objDAL.MakeInParams("@Contact_Person_1", SqlDbType.VarChar , 50,Contact_Person1),
            objDAL.MakeInParams("@Contact_1_Mobile_1", SqlDbType.VarChar , 10,Contact_1_MobileNo1),
            objDAL.MakeInParams("@Contact_1_Mobile_2", SqlDbType.VarChar , 10,Contact_1_MobileNo2),
            objDAL.MakeInParams("@Contact_Person_2", SqlDbType.VarChar , 50,Contact_Person2),
            objDAL.MakeInParams("@Contact_2_Mobile_1", SqlDbType.VarChar , 10,Contact_2_MobileNo1),
            objDAL.MakeInParams("@Contact_2_Mobile_2", SqlDbType.VarChar , 10,Contact_2_MobileNo2),
            objDAL.MakeInParams("@VehicleXML", SqlDbType.Xml, 0,MultipleVehicleXML),
            objDAL.MakeInParams("@CreatedBy", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_Multiple_Vehicle_Tracking_Shared_With_Save", objSqlParam);

        if (Convert.ToInt32(objSqlParam[0].Value) == 0)
        {
            Get_Vehicle_Tracking_SMS(Client_ID);
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
            City_Id = Util.String2Int(objDR["City_ID"].ToString());
            City_Name = objDR["City_Name"].ToString();
            Client_Name = objDR["Client_Name"].ToString();
            Contact_Person1 = objDR["Contact_Person1"].ToString();
            Contact_1_MobileNo1 = objDR["Contact_1_Mobile_1"].ToString();
            Contact_1_MobileNo2 = objDR["Contact_1_Mobile_2"].ToString();
            Contact_Person2 = objDR["Contact_Person2"].ToString();
            Contact_2_MobileNo1 = objDR["Contact_2_Mobile_1"].ToString();
            Contact_2_MobileNo2 = objDR["Contact_2_Mobile_2"].ToString();
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

        //txt_VehicleNo.Focus();
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
    #endregion
   
    #region vehicle tracking
    public void Get_Vehicle_Tracking_SMS(int Client_ID)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        DateTime CurrentDate;

        CurrentDate = DateTime.Now;

        SqlParameter[] sqlPara = { objdal.MakeInParams("@Date", SqlDbType.DateTime, 0, CurrentDate),
        objdal.MakeInParams("@Client_ID", SqlDbType.Int, 0, Client_ID)};
        objdal.RunProc("Ec_Opr_Get_Multiple_Vehicle_Tracking_SMS_MSG", sqlPara, ref ds);
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

        string NumberPart4 = "6516";

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
    #endregion
}
