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
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Operations_Inward_FrmVehicleRTOPenaltyViewEdit : ClassLibraryMVP.UI.Page
{
    
    #region members


    private int VehicleID
    {
        set
        {
            DDLVehicle.VehicleID = value;
        }

        get
        {
            return DDLVehicle.VehicleID;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle";
            ScriptManager.SetFocus(DDLVehicle);
        }
        else if (txt_ChallanNo.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Challan No.";
            ScriptManager.SetFocus(txt_ChallanNo);
        }
        else if (txt_Offence.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Offence";
            ScriptManager.SetFocus(txt_Offence);
        }
        else if (Util.String2Decimal(txt_Amount.Text) <= 0)
        {
            lblErrors.Text = "Please Enter Penalty Amount";
            ScriptManager.SetFocus(txt_Amount);
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            PenaltyTime.setFormat("24");

            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"])); 
           
            ReadValues(); 
        }

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Details_ID", SqlDbType.Int, 0, keyID) };
        objDAL.RunProc("EF_Opr_Vehicle_Penalty_Details_Read", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            VehicleID = Convert.ToInt32 (objDR["Vehicle_ID"].ToString());
            dtp_PenaltyDate.SelectedDate = Convert.ToDateTime(objDR["Penalty_Date"].ToString());
            PenaltyTime.setTime (objDR["Penalty_Time"].ToString());
            txt_ChallanNo.Text = objDR["ChallanNo"].ToString();
            txt_Place.Text = objDR["Place"].ToString();
            txt_Offence.Text = objDR["Offence"].ToString();
            txt_Amount.Text = objDR["Amount"].ToString();
        }

        

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

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@Details_ID",SqlDbType.Int,0,keyID), 
            objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,VehicleID), 
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,""),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Penalty_Date",SqlDbType.DateTime,0,dtp_PenaltyDate.SelectedDate),
            objDAL.MakeInParams("@Penalty_Time",SqlDbType.VarChar,10,PenaltyTime.getTime()),
            objDAL.MakeInParams("@ChallanNo",SqlDbType.VarChar,25,txt_ChallanNo.Text ),
            objDAL.MakeInParams("@Place",SqlDbType.VarChar,50,txt_Place.Text ),
            objDAL.MakeInParams("@Offence",SqlDbType.VarChar,250,txt_Offence.Text),
            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,Convert.ToDouble(txt_Amount.Text))
        };

        objDAL.RunProc("dbo.EF_Opr_Vehicle_Penalty_Details_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {

            string _Msg = "Saved SuccessFully";

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }
    
}
