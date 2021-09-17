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

public partial class Reports_CL_Nandwana_UserDesk_FrmeWayBillChecking_Mail_Report : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    private DataSet objDS;

    int VehicleId;
    string VehicleNo;
    DateTime MemoDate;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        VehicleId = Util.String2Int(Request.QueryString["VehicleId"]);
        VehicleNo = Request.QueryString["VehicleNo"];
        MemoDate = Convert.ToDateTime(Request.QueryString["Date"]);

        lbl_VehicleNo.Text = VehicleNo;
        lbl_Date.Text = MemoDate.ToLongDateString().ToString();

    }

    private void SendMail_Consolidated_eWayills()
    {
        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Date",SqlDbType.DateTime ,0,MemoDate),
        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleId),
        objDAL.MakeInParams("@Vehicle_No",SqlDbType.VarChar ,20,VehicleNo),
        objDAL.MakeInParams("@eMail_ID",SqlDbType.VarChar,100,txt_Email_Id.Text)};

        objDAL.RunProc("COM_UserDesk_eWayBills_Checking_Mail_Consolidated_eWayBills", objSqlParam, ref objDS);

        string TotalRecords = objDS.Tables[0].Rows[0][0].ToString();

        if (Util.String2Int(TotalRecords) <= 0)
        {
            lbl_Errors.Text = "No Records Found";
        }
        else
        {
            lbl_Errors.Text = "Mail Sent";
        }

        ClearVariables();
    }

    private void SendMail_Part_B_Updated_By_Party()
    {
        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Date",SqlDbType.DateTime ,0,MemoDate),
        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleId),
        objDAL.MakeInParams("@Vehicle_No",SqlDbType.VarChar ,20,VehicleNo),
        objDAL.MakeInParams("@eMail_ID",SqlDbType.VarChar,100,txt_Email_Id.Text)};

        objDAL.RunProc("COM_UserDesk_eWayBills_Checking_Mail_PartB_Updated_By_Party", objSqlParam, ref objDS);

        string TotalRecords = objDS.Tables[0].Rows[0][0].ToString();

        if (Util.String2Int(TotalRecords) <= 0)
        {
            lbl_Errors.Text = "No Records Found";
        }
        else
        {
            lbl_Errors.Text = "Mail Sent";
        }

        ClearVariables();
    }

    private void SendMail_All_LR_Details()
    {
        DAL objDAL = new DAL();


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Date",SqlDbType.DateTime ,0,MemoDate),
        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleId),
        objDAL.MakeInParams("@Vehicle_No",SqlDbType.VarChar ,20,VehicleNo),
        objDAL.MakeInParams("@eMail_ID",SqlDbType.VarChar,100,txt_Email_Id.Text)};

        objDAL.RunProc("COM_UserDesk_eWayBills_Checking_Mail_All_LR_Details", objSqlParam, ref objDS);

        string TotalRecords = objDS.Tables[0].Rows[0][0].ToString();

        if (Util.String2Int(TotalRecords) <= 0)
        {
            lbl_Errors.Text = "No Records Found";
        }
        else
        {
            lbl_Errors.Text = "Mail Sent";
        }

        ClearVariables();
    }

    public void ClearVariables()
    {
        objDS = null;
    }

    protected void btn_eMail_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {

            if (rbl_ReportType.SelectedValue == "1")
            {
                SendMail_Consolidated_eWayills();
            }
            else if (rbl_ReportType.SelectedValue == "2")
            {
                SendMail_Part_B_Updated_By_Party();
            }
            else if (rbl_ReportType.SelectedValue == "3")
            {
                SendMail_All_LR_Details();
            }
        }
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;
        
        if (txt_Email_Id.Text.Trim() == "") 
        {
            lbl_Errors.Text = "Enter eMail ID";
            ScriptManager.SetFocus(txt_Email_Id);

        }
        //else if (txt_Email_Id.Text.IndexOf("/^'\'w+((-'\'w+)|('\'.'\'w+))*'\'@[A-Za-z0-9]+(('\'.|-)[A-Za-z0-9]+)*'\'.[A-Za-z0-9]+$/") == -1)
        //{
        //    lbl_Errors.Text = "Invalid eMail ID";
        //    ScriptManager.SetFocus(txt_Email_Id);
        //}
        else
        {
            ATS = true;
        }

        return ATS;
    }



}
