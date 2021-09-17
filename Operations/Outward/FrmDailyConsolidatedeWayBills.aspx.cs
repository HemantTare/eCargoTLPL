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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;
using System.Net;
using System.IO;


public partial class Operations_Outward_FrmDailyConsolidatedeWayBills : System.Web.UI.Page
{
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList  ddl_Vehicle;
    TextBox txt_eWayBillNo;
    TextBox txt_Mobile1;
    TextBox txt_Mobile2;
    DataRow dr;
   
    bool Allow_To_Save;
    DataTable objDT;
    

    public bool validateUI()
    {
        bool ATS;
        ATS = false;
 

        if (Session_BharaiGrid.Rows.Count <= 0)
        {
            lblErrors.Text = "Please Enter Vehicle, eWay Bill";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    public DateTime LoadingDate
    {
        set
        {
            dtpApplicableFrom.SelectedDate = value;
            hdn_Date.Value = dtpApplicableFrom.SelectedDate.ToString();
        }
        get { return dtpApplicableFrom.SelectedDate; }
    }


    public int Session_Mode
    {
        get { return StateManager.GetState<int>("SessionMode"); }
        set { StateManager.SaveState("SessionMode", value); }
    }
    public int Session_MenuItem
    {
        get { return StateManager.GetState<int>("SessionMenuItem"); }
        set { StateManager.SaveState("SessionMenuItem", value); }
    }
 
    private void Bind_dg_Commodity()
    { 
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();

         
    } 
    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }
    private string ClientCode
    {
        get { return CompanyManager.getCompanyParam().ClientCode.ToLower(); }
    }
    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }
 
    public DataTable BindVehicle
    {
        set { Set_Common_DDL(ddl_Vehicle, "Vehicle_No", "VehicleID", value, true); }
    }

    public DataTable Session_BharaiGrid
    {
        get { return StateManager.GetState<DataTable>("BharaiGrid"); }
        set { StateManager.SaveState("BharaiGrid", value); }
    } 

    public DataTable Session_VehicleDdl
    {
        get { return StateManager.GetState<DataTable>("VehicleDdl"); }
        set { StateManager.SaveState("VehicleDdl", value); }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

            dtpApplicableFrom.SelectedDate = DateTime.Now;
            fillVehicleValues();
            filleWayBillDetails();
        }
    }

    private void fillVehicleValues()
    {
        int Branch_id;

        if (UserManager.getUserParam().HierarchyCode == "BO")
        {
            Branch_id = UserManager.getUserParam().MainId;
        }
        else
        {
            Branch_id = 0;
        }


        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LoadingDate", SqlDbType.DateTime , 0,LoadingDate),
          objDAL.MakeInParams("@RegionID", SqlDbType.Int,0,0),
        objDAL.MakeInParams("@AreaID", SqlDbType.Int,0,0),
        objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_id)
        };
        objDAL.RunProc("EC_Mst_SMS_COnsolidated_eWayBill_FillValues2",objSqlParam, ref objDS);
        Session_VehicleDdl = objDS.Tables[0];  
       
    }
    private void filleWayBillDetails()
    {
        SqlParameter[] objSqlParam ={objDAL.MakeInParams("@DetailsID", SqlDbType.Int, 0,Convert.ToInt32(hdnKeyID.Value))};

        objDAL.RunProc("EC_Opr_eWayBillDetails", objSqlParam, ref objDS);
        
        Session_BharaiGrid = objDS.Tables[0];

        if (objDS.Tables[0].Rows.Count > 0)
        {
            
            DataRow objDR = objDS.Tables[0].Rows[0];

           
            //dtpApplicableFrom.SelectedDate = Convert.ToDateTime(objDR["ApplicableFrom"].ToString());
        }
        Bind_dg_Commodity();
    }

    protected void dtpApplicableFrom_SelectionChanged(object sender, EventArgs e)
    {
        fillVehicleValues();
        filleWayBillDetails();
       
        
    }
    
    protected void dg_Commodity_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Vehicle = (DropDownList)(e.Item.FindControl("ddl_Vehicle"));
                txt_eWayBillNo = (TextBox)(e.Item.FindControl("txt_eWayBillNo"));
                txt_Mobile1 = (TextBox)(e.Item.FindControl("txt_Mobile1"));
                txt_Mobile2 = (TextBox)(e.Item.FindControl("txt_Mobile2"));

                
                BindVehicle = Session_VehicleDdl;  
                
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                //dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
                fillVehicleValues();
                DataRow DR = null;
                DataTable dt = Session_BharaiGrid;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                ddl_Vehicle.SelectedValue = DR["Vehicle_ID"].ToString(); 
                txt_eWayBillNo.Text = DR["eWayBillNo"].ToString();
                txt_Mobile1.Text = DR["Mobile1"].ToString();
                txt_Mobile2.Text = DR["Mobile2"].ToString();
            }
        }
    }
    protected void dg_Commodity_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = e.Item.ItemIndex;
        dg_Commodity.ShowFooter = false;
        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Commodity.EditItemIndex = -1;
        dg_Commodity.ShowFooter = true;
         

        Bind_dg_Commodity();
        ErrorMsg = ""; 

    }
    protected void dg_Commodity_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_BharaiGrid.AcceptChanges();
            dg_Commodity.EditItemIndex = -1;
            dg_Commodity.ShowFooter = true;
            Bind_dg_Commodity();

            lbl_TotalRecords.Text = "Total Records Added : " + Convert.ToString(dr.Table.Rows.Count);
        } 

    }
    protected void dg_Commodity_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_Commodity_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_Commodity.EditItemIndex = -1;
                dg_Commodity.ShowFooter = true;
                 
                Bind_dg_Commodity(); 
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate Vehicle ";
            lblErrors.Text = "Duplicate Vehicle ";

            scm_Comm.SetFocus(ddl_Vehicle);
        }
    }
    protected void dg_Commodity_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_BharaiGrid;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];
                _dtColumnPrimaryKey[0] = objDT.Columns["Vehicle_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Commodity_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_Commodity();
                    dg_Commodity.EditItemIndex = -1; 
                    dg_Commodity.ShowFooter = true;
                  
                }
                //TotalRate = TotalRate;
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate Vehicle";
                lblErrors.Text = "Duplicate Vehicle ";
                scm_Comm.SetFocus(ddl_Vehicle);
            }
        }
    }
    private void Insert_Update_Commodity_Dataset(object source, DataGridCommandEventArgs e)
    { 
        ddl_Vehicle = (DropDownList)(e.Item.FindControl("ddl_Vehicle"));
        txt_eWayBillNo = (TextBox)(e.Item.FindControl("txt_eWayBillNo"));
        txt_Mobile1 = (TextBox)(e.Item.FindControl("txt_Mobile1"));
        txt_Mobile2 = (TextBox)(e.Item.FindControl("txt_Mobile2"));
      
        if (Allow_To_Add_Update_Commodity())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_BharaiGrid.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_BharaiGrid.Rows[e.Item.ItemIndex];
            }

            dr["Vehicle_ID"] = ddl_Vehicle.SelectedValue;
            dr["Vehicle_No"] = Util.String2Int(ddl_Vehicle.SelectedValue) == 0 ? "" : ddl_Vehicle.SelectedItem.Text;
            dr["eWayBillNo"] = txt_eWayBillNo.Text.Trim() == string.Empty ? "0" : txt_eWayBillNo.Text.Trim();
            dr["Mobile1"] = txt_Mobile1.Text.Trim() == string.Empty ? "0" : txt_Mobile1.Text.Trim();
            dr["Mobile2"] = txt_Mobile2.Text.Trim() == string.Empty ? "0" : txt_Mobile2.Text.Trim();  

            if (e.CommandName == "Add")
            {
                Session_BharaiGrid.Rows.Add(dr);
            }

            lbl_TotalRecords.Text = "Total Records Added : " + Convert.ToString(dr.Table.Rows.Count);
        }
    }
 

    public bool Allow_To_Add_Update_Commodity()
    {
        Allow_To_Save = false;
        ErrorMsg = "";
        
        decimal RatePerArticle = txt_eWayBillNo.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_eWayBillNo.Text.Trim());

        if (Util.String2Int(ddl_Vehicle.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Vehicle.";
            lblErrors.Text = "Please Select Vehicle.";
            lblErrors.Visible = true;
            scm_Comm.SetFocus(txt_eWayBillNo);
        }
        else if (txt_eWayBillNo.Text.Trim().Length != 12 && txt_eWayBillNo.Text.Trim().Length != 10)
        {
            ErrorMsg = "Please Enter Proper eWay Bill No.";
            lblErrors.Text = "Please Enter Proper eWay Bill No.";
            lblErrors.Visible = true;
            scm_Comm.SetFocus(txt_eWayBillNo);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }
     

    protected void btn_Item_Click(object sender, EventArgs e)
    {
        dg_Commodity.ShowFooter = true;
        dg_Commodity.DataSource = Session_BharaiGrid;
        dg_Commodity.DataBind();
    }

    protected void ddl_Vehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsComm;

        ddl_Vehicle  = (DropDownList)sender;
        DataGridItem dg_Commodity = (DataGridItem)ddl_Vehicle.Parent.Parent;

        ddl_Vehicle  = (DropDownList)(dg_Commodity.FindControl("ddl_Vehicle"));

        dsComm = fillVehicleMobileNos(Util.String2Int(ddl_Vehicle.SelectedValue));

        txt_Mobile1 = (TextBox)(dg_Commodity.FindControl("txt_Mobile1"));
        txt_Mobile2 = (TextBox)(dg_Commodity.FindControl("txt_Mobile2"));

        if (dsComm.Tables[0].Rows.Count > 0)
        {
            txt_Mobile1.Text = dsComm.Tables[0].Rows[0]["Mobile1"].ToString();
            txt_Mobile2.Text = dsComm.Tables[0].Rows[0]["Mobile2"].ToString();
        }
        scm_Comm.SetFocus(ddl_Vehicle);
    }

    public DataSet fillVehicleMobileNos(int vehicle_id)
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, vehicle_id) };
        objDAL.RunProc("EC_Opr_eWayBillGetVehicleMobileNos", objSqlParam, ref objDS);

        return objDS;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        //DataTable DT = (DataTable)(Session["Session_BharaiGrid"]);

        DataTable DT1 = Session_BharaiGrid.Copy();
        DT1.TableName = "bharaigrid";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string eWayBillDetailsXML = ds.GetXml().ToLower();

 
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@eWayBillDetailsXML",SqlDbType.Xml,0,eWayBillDetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Opr_eWayBillDetails_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            Get_SMS(DateTime.Now);

            //Get_SMS_Error_eWayBills(dtpApplicableFrom.SelectedDate, dtpApplicableFrom.SelectedDate); 

            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        else
        {
            if (objMessage.messageID == 2601)
            {
                lblErrors.Text = "Duplicate Entry Found";
            }
            else
            {
                lblErrors.Text = objMessage.message;
            }
        }

        return objMessage;
    }


    public void Get_SMS(DateTime  TrDate)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();
        
        SqlParameter[] sqlPara = { objdal.MakeInParams("@TrDate", SqlDbType.DateTime, 0, TrDate)};

        objdal.RunProc("Ec_Opr_Sent_Consolidated_eWayBill_SMS_MSG", sqlPara, ref ds);

        int Count, i;

        Count=ds.Tables[0].Rows.Count;

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (i=0; i<=Count; i++)
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

    public void Get_SMS_Error_eWayBills(DateTime FromDate,DateTime ToDate)
    {
        DAL objdal = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] sqlPara = { objdal.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
        objdal.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate)};

        objdal.RunProc("Ec_Opr_Get_Error_eWayBill_SMS_MSG", sqlPara, ref ds);

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

                    String sendToPhoneNumber = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                    string msg = HttpUtility.UrlEncode(ds.Tables[0].Rows[i]["SMSMessage"].ToString());

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


