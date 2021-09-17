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
using Raj.EC;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

public partial class Operations_VehicleTripExpense_FrmVehicleDailyVaraiBharai : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    string Mode = "0";
    PageControls pc = new PageControls();
    
    string _flag;

    #region properties


    private int Transaction_ID
    {
        set { hdnKeyID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnKeyID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnKeyID.Value);
        }
    }

    private DateTime TransactionDate
    {
        set { dtpFromDate.SelectedDate = value; }
        get { return dtpFromDate.SelectedDate; }
    }

    private int VehicleID
    {
        set { DDLVehicleSearch.VehicleID = value; }
        get { return DDLVehicleSearch.VehicleID; }
    }
    
    
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public int DriverID
    {
        get { return Util.String2Int(DDLDriver.SelectedValue); }
    }
    
    public void SetDriver(string text, string value)
    {
        DDLDriver.DataTextField = "Driver_Name";
        DDLDriver.DataValueField = "Driver_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDLDriver);
    }


    public int  PreviousTripWt
    {
                
        set
        {
            txt_PreviousTripWeight.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_PreviousTripWeight.Text); }

        
    }

    private DateTime PreviousTripWtDate
    {
        set { dtpPreviousTripWeightDate.SelectedDate = value; }
        get { return dtpPreviousTripWeightDate.SelectedDate; }
    }


    public int Bharai
    {
        set
        {
            txt_Bharai.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_Bharai.Text); }

    }

    public string BharaiRemarks
    {
        set {txt_BharaiRemark.Text = value;}
        get { return txt_BharaiRemark.Text; }
    }

    public int Varai
    {
        set
        {
            txt_Varai.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_Varai.Text); }

    }

    public string VaraiRemarks
    {
        set { txt_VaraiRemark.Text = value; }
        get { return txt_VaraiRemark.Text; }
    }

    public int ChaiPani
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            }

            txt_ChaiPani.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_ChaiPani.Text); }

    }


    private string ErrorMsg
    {
        set { lbl_Errors.Text = value; }
    }


    #endregion

    #region events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Util.DecryptToString(Request.QueryString["Mode"].ToString()) == "4")
        {
            DDLVehicleSearch.Can_Add_Vehicle = false;
            DDLVehicleSearch.Can_View_Vehicle = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));
        
        if (!IsPostBack)
        {

            Transaction_ID = Util.DecryptToInt(Request.QueryString["Id"]);
            DDLVehicleSearch.SetAutoPostBack = true;


            if (Transaction_ID <= 0)
            {
                TransactionDate = DateTime.Now;

                DDLVehicleSearch.TransactionDate = dtpFromDate.SelectedDate;
            }
            else
            {
                DDLVehicleSearch.SetEnabled = false;
            }

            clearsessions();

            ReadValues();
        }
        
    }

   

   

    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0, Transaction_ID) };

        objDAL.RunProc("EF_Opr_Trip_Daily_Varai_Bharai_Details", objSqlParam, ref objDS);

        
        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            TransactionDate  = Convert.ToDateTime(objDR["Transaction_Date"].ToString());

            VehicleID = Convert.ToInt32(objDR["Vehicle_ID"].ToString());

            SetDriver(objDR["Driver_Name"].ToString(), objDR["Driver_ID"].ToString());

            if (Util.String2Int(objDR["PreviousTripWeight"].ToString()) > 0)
            {
                PreviousTripWt = Convert.ToInt32(objDR["PreviousTripWeight"].ToString());
                PreviousTripWtDate = Convert.ToDateTime(objDR["PreviousTripDate"].ToString());

            }

            Bharai = Convert.ToInt32(objDR["Bharai"].ToString());

            BharaiRemarks  = objDR["BharaiRemark"].ToString();

            Varai = Convert.ToInt32(objDR["Varai"].ToString());

            VaraiRemarks = objDR["VaraiRemark"].ToString();

            ChaiPani = Convert.ToInt32(objDR["ChaiPani"].ToString());

            Remarks = objDR["Remark"].ToString();

        }

    }

    private void clearsessions()
    {
        objDS  = null;
    }


    #endregion

    #region save
  
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {

            _flag = "SaveAndExit";

            Save();

            clearsessions();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        clearsessions();
        Response.Write("<script language='javascript'>{self.close();}</script>");
    }
    private bool AllowToSave()
    {
        bool ATS = false;
        lbl_Errors.Text = "Fields with * mark are mandatory";
        if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Please Select Vehicle No";
            ScriptManager.SetFocus(DDLVehicleSearch);
        }

        else if (DriverID <= 0)
        {
            lbl_Errors.Text = "Please Select Driver ";
            ScriptManager.SetFocus(DDLDriver);
        }

        else if (Bharai > 0 && BharaiRemarks.Trim().Length == 0 )
        {
            lbl_Errors.Text = "Enter Bharai Remark ";
            ScriptManager.SetFocus(txt_BharaiRemark);
        }

        else if (Varai > 0 && VaraiRemarks.Trim().Length == 0)
        {
            lbl_Errors.Text = "Enter Varai Remark ";
            ScriptManager.SetFocus(txt_VaraiRemark);
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
            objDAL.MakeInParams("@Transaction_ID", SqlDbType.Int, 0,Transaction_ID),
            objDAL.MakeInParams("@Transaction_Date", SqlDbType.DateTime, 0,TransactionDate ),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,VehicleID),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,DriverID),

            objDAL.MakeInParams("@PreviousTripWeight", SqlDbType.Int, 0,PreviousTripWt),
            objDAL.MakeInParams("@PreviousTripDate", SqlDbType.DateTime, 0,PreviousTripWtDate),

            objDAL.MakeInParams("@Bharai", SqlDbType.Int, 0,Bharai ),
            objDAL.MakeInParams("@BharaiRemark", SqlDbType.VarChar, 200,BharaiRemarks),

            objDAL.MakeInParams("@Varai", SqlDbType.Int, 0,Varai),
            objDAL.MakeInParams("@VaraiRemark", SqlDbType.VarChar, 200,VaraiRemarks),

            objDAL.MakeInParams("@ChaiPani", SqlDbType.Int, 0,ChaiPani),

            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 500,Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EF_Opr_Trip_Daily_Varai_Bharai_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }

    #endregion
}
