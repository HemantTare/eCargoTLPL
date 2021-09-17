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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;

using System.Text;
using System.IO;
using Ionic.Zip;
using System.Collections.Generic;

using System.Net;

public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBill_Pending_Vehicle_Update_Json_Creation_PDS : System.Web.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();

    private DataSet objDS,ds;
    DataTable objDT = new DataTable();
    private DAL objDAL = new DAL();

    string TotalRecords ;

    private int VehicleId
    {
        set { hdnVehicleId.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnVehicleId.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnVehicleId.Value);
        }
    }

    private String Current_PDS_ID
    {
        set { hdnCurrent_PDS_ID.Value = value.ToString(); }
        get
        {return hdnCurrent_PDS_ID.Value;
        }
    }

    public string Vehicle_No
    {
        set { lblVehicleNoValue.Text = value; }
        get { return lblVehicleNoValue.Text; }
    }

    public string Consolidated_eWayBillNo
    {
        set { txt_Consolidated_eWayBillNo.Text = value; }
        get { return txt_Consolidated_eWayBillNo.Text; }
    }


    public string Mobile_No1
    {
        set { txt_Mobile1.Text = value; }
        get { return txt_Mobile1.Text; }
    }

    public string Mobile_No2
    {
        set { txt_Mobile2.Text = value; }
        get { return txt_Mobile2.Text; }
    }

    public DataTable Session_Branch_Mobile
    {
        get { return StateManager.GetState<DataTable>("Branch_Mobile_Grid"); }
        set { StateManager.SaveState("Branch_Mobile_Grid", value); }

    }

    public String PDSBranchMobileNosXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_Branch_Mobile.Copy());
            _objDs.Tables[0].TableName = "Branch_Mobile_GridDetails";
            return _objDs.GetXml().ToLower();
        }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        errorMessage = "";

        if (!IsPostBack)
        {

            string Crypt = "";

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Vehicle_Id"];
            VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Current_PDS_ID"];
            Current_PDS_ID = ClassLibraryMVP.Util.DecryptToString(Crypt);


            

            if (VehicleId > 0)
            {
                Vehicle_No = Util.DecryptToString(Request.QueryString["Vehicle_No"].ToString());

                fillVehicleMobileNos();

                FillPDSBranchMobileNosGrid();

            }
        }

        txt_Consolidated_eWayBillNo.Focus();
    }

    private void FillPDSBranchMobileNosGrid()
    {

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Current_PDS_ID", SqlDbType.VarChar,1000,Current_PDS_ID)
        };

        objDAL.RunProc("[EC_Opr_eWayBill_Pending_Vehicle_Get_PDS_Branch_Mobile]", objSqlParam, ref ds);

        Session_Branch_Mobile = ds.Tables[0];
        Bind_dg_Branch_Mobile();
    }

    private void Bind_dg_Branch_Mobile()
    {
        dg_BranchMobileNos.DataSource = Session_Branch_Mobile;
        dg_BranchMobileNos.DataBind();
    }


    protected void dg_BranchMobileNos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int BranchID;
            TextBox txtContactPersonMobile;

            txtContactPersonMobile = (TextBox)e.Item.FindControl("txtContactPersonMobile");

            BranchID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "PDS_Branch_ID").ToString());

        }
    }


    protected void btn_CreateJson_Click(object sender, EventArgs e)
    {
        if (VehicleId > 0)
        {

            string version, versionno, userGstin, userGstinNo, vehicleUpdts, tripSheets, tripSheetEwbBills, fromPlace, fromPlaceeWayBills;
            int found;

            found = 0;

            version = "version";
            versionno = "1.0.0618";
            userGstin = "userGstin";
            userGstinNo = "27AAECT9480C1Z5";
            vehicleUpdts = "vehicleUpdts";
            tripSheets = "tripSheets";
            tripSheetEwbBills = "tripSheetEwbBills";

            JsonCreation("form", e);

            fromPlace = "";
            fromPlaceeWayBills = "";

            ds.Tables[4].Columns.Remove("SrNo");
            ds.Tables[4].Columns.Remove("BkgStateGstNo");
            ds.Tables[4].Columns.Remove("transModeName");
            ds.Tables[4].Columns.Remove("fromstatename");
            ds.Tables[4].Columns.Remove("reasonname");
            ds.Tables[4].Columns.Remove("reason");
            ds.Tables[4].Columns.Remove("remark");



            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                //FileStream fStream = new FileStream(@"C:\Jason\MyJason.json", FileMode.Create, FileAccess.Write);
                //StreamWriter sWriter = new StreamWriter(fStream);



                string sFileName = System.IO.Path.GetRandomFileName();
                string sGenName = "Consolidated_E-WayBill_JSON_" + Vehicle_No  + ".json";

                using (System.IO.StreamWriter sWriter = new System.IO.StreamWriter(
                       Server.MapPath("~/JasonFiles/" + sFileName + ".txt")))
                {
                    //sWriter.WriteLine(txtText.Text);

                    JsonString.Append("{");
                    sWriter.WriteLine("{");

                    JsonString.Append("\"" + version + "\":" + "\"" + versionno + "\",");
                    sWriter.WriteLine("\"" + version + "\":" + "\"" + versionno + "\",");

                    JsonString.Append("\"");

                    JsonString.Append("\"" + tripSheets + "\":[");
                    sWriter.WriteLine("\"" + tripSheets + "\":[");

                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        sWriter.WriteLine("{");
                        JsonString.Append(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");
                        sWriter.WriteLine(" \"" + userGstin + "\":" + "\"" + userGstinNo + "\",");


                        for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                        {
                            found = 0;
                            if (j < ds.Tables[4].Columns.Count - 1)
                            {

                                if (j == 0 || j == 1 || j == 6 || j == 7 || j == 2 || j == 3 || j == 5)
                                {
                                    if (j == 3)
                                    {
                                        fromPlace = ds.Tables[4].Rows[i][j].ToString();
                                    }

                                    JsonString.Append(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                    sWriter.WriteLine(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                }
                            }
                            else if (j == ds.Tables[4].Columns.Count - 1)
                            {
                                JsonString.Append(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                                sWriter.WriteLine(" \"" + ds.Tables[4].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[4].Rows[i][j].ToString() + "\",");
                            }
                        }


                        JsonString.Append(" \"" + tripSheetEwbBills + "\":[");
                        sWriter.WriteLine(" \"" + tripSheetEwbBills + "\":[");

                        for (int l = 0; l < ds.Tables[4].Rows.Count; l++)
                        {
                            for (int k = 0; k < ds.Tables[4].Columns.Count; k++)
                            {
                                if (k == 3)
                                {
                                    fromPlaceeWayBills = ds.Tables[4].Rows[l][k].ToString();
                                }

                                if (k == 4)
                                {
                                    if (fromPlace == fromPlaceeWayBills)
                                    {
                                        if (found > 0)
                                        {
                                            JsonString.Append("},");
                                            sWriter.WriteLine("},");
                                            JsonString.Append("");
                                            sWriter.WriteLine("");

                                            i = i + 1;
                                        }

                                        JsonString.Append("{");
                                        sWriter.WriteLine("{");

                                        JsonString.Append(" \"" + ds.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[4].Rows[l][k].ToString());
                                        sWriter.WriteLine(" \"" + ds.Tables[4].Columns[k].ColumnName.ToString() + "\":" + ds.Tables[4].Rows[l][k].ToString());

                                        found = found + 1;

                                    }
                                    else
                                    {
                                        found = 0;
                                    }
                                }
                            }
                        }
                        JsonString.Append("}");
                        sWriter.WriteLine("}");

                        JsonString.Append("");
                        sWriter.WriteLine("");

                        JsonString.Append("]");
                        sWriter.WriteLine("]");
                        sWriter.WriteLine("");

                        if (i == ds.Tables[4].Rows.Count - 1)
                        {
                            JsonString.Append("}");
                            sWriter.WriteLine("}");
                        }
                        else
                        {
                            JsonString.Append("},");
                            sWriter.WriteLine("},");
                            sWriter.WriteLine("");
                        }
                    }
                    JsonString.Append("]");
                    sWriter.WriteLine("]");

                    JsonString.Append("}");
                    sWriter.WriteLine("}");

                    sWriter.Close();

                    System.IO.FileStream fs = null;
                    fs = System.IO.File.Open(Server.MapPath("~/JasonFiles/" +
                             sFileName + ".txt"), System.IO.FileMode.Open);
                    byte[] btFile = new byte[fs.Length];
                    fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    Response.AddHeader("Content-disposition", "attachment; filename=" +
                                       sGenName);
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(btFile);
                    Response.End();
                }
            }
            else
            {
                //return null;
                //txt_Jason.Text = JsonString.ToString();
            }

        }
        else
        {
            lbl_Errors.Text = "Not A Valid Vehicle";
        }

    }

    private void JsonCreation(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();


        int Region_Id = 0;
        int Area_id = 0;
        int Branch_id = UserManager.getUserParam().MainId;


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),            
            objDAL.MakeInParams("@VehicleID", SqlDbType.Int,0,VehicleId),
            objDAL.MakeInParams("@Current_PDS_ID", SqlDbType.VarChar,1000,Current_PDS_ID )
        };

        objDAL.RunProc("[EC_Opr_eWayBill_Pending_Vehicle_Update_JSON_PDS]", objSqlParam, ref ds);

        TotalRecords = ds.Tables[1].Rows[0][0].ToString();


    }


    private void SetContactPersonMobile()
    {
        TextBox txtContactPersonMobile;
        int i;

        if (dg_BranchMobileNos.Items.Count > 0)
        {
            for (i = 0; i <= dg_BranchMobileNos.Items.Count - 1; i++)
            {
                txtContactPersonMobile = (TextBox)dg_BranchMobileNos.Items[i].FindControl("txtContactPersonMobile");
                Session_Branch_Mobile.Rows[i]["phone1"] = Util.String2Decimal(txtContactPersonMobile.Text);
            }
        }
    }

    protected void txtContactPersonMobile_TextChanged(object sender, EventArgs e)
    {
        SetContactPersonMobile();
    }

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }



    private bool AllowToSave()
    {
        bool ATS = false;
        lbl_Errors.Text = "";

        if (txt_Consolidated_eWayBillNo.Text.Trim().Length != 12 && txt_Consolidated_eWayBillNo.Text.Trim().Length != 10)
        {
            lbl_Errors.Text = "Please Enter Valid Consolidated eWay Bill No.";
            txt_Consolidated_eWayBillNo.Focus();
        }
        else if (txt_Mobile1.Text.Trim() == "" && txt_Mobile2.Text.Trim() == "")
        {
            lbl_Errors.Text = "Please Enter Atleast One Mobile No.";
            txt_Mobile1.Focus();
        }
        else if (objComm.IsCheck_Duplicate("Consolidated eWayBillPDS", 0, txt_Consolidated_eWayBillNo.Text) == true)
        {
            lbl_Errors.Text = "Duplicate Consolidated eWayBill";
            txt_Consolidated_eWayBillNo.Focus();

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
            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0, VehicleId),
            objDAL.MakeInParams("@ConsolidatedeWayBillNo", SqlDbType.VarChar, 25, Consolidated_eWayBillNo),
            objDAL.MakeInParams("@Mobile1", SqlDbType.VarChar, 20, Mobile_No1),
            objDAL.MakeInParams("@Mobile2", SqlDbType.VarChar, 20, Mobile_No2),
            objDAL.MakeInParams("@Current_PDS_ID", SqlDbType.VarChar,1000,Current_PDS_ID ),
            objDAL.MakeInParams("@PDSBranchMobileNosXML",SqlDbType.Xml,0,PDSBranchMobileNosXML),
            objDAL.MakeInParams("@UpdatedBy", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_eWayBill_Pending_Vehicle_Update_PDS_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";

            Get_SMS(Consolidated_eWayBillNo); 

            ClearVariables();

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    private void fillVehicleMobileNos()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Current_PDS_ID", SqlDbType.Int, 0, Current_PDS_ID) };
        objDAL.RunProc("EC_Opr_eWayBillGetVehicleMobileNosPDS", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lblMobile1.Text = objDR["Driver1"].ToString();
            lblMobile2.Text = objDR["Driver2"].ToString();

            Mobile_No1 = objDR["Mobile1"].ToString();
            Mobile_No2 = objDR["Mobile2"].ToString();
        }
    }

    public void ClearVariables()
    {
        objDS = null;
        ds = null;
        Session_Branch_Mobile = null;

    }

    public void Get_SMS(string Consol_eWayBillNo)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] sqlPara = { objdal.MakeInParams("@Consol_eWayBillNo", SqlDbType.VarChar , 25, Consol_eWayBillNo),
         objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0, VehicleId)};

        objdal.RunProc("EC_Opr_eWayBill_Pending_Vehicle_Update_Sent_SMS_PDS", sqlPara, ref ds);

        int Count, i;

        Count = ds.Tables[0].Rows.Count;

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (i = 0; i <= Count; i++)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {

                    String sendToPhoneNumber = ds.Tables[0].Rows[i]["DriverMobile"].ToString();
                    string msg = ds.Tables[0].Rows[i]["Msg"].ToString();

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
