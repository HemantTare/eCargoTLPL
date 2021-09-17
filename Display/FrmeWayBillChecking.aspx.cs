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

public partial class Display_FrmeWayBillChecking : System.Web.UI.Page
{
    #region Declaration
    private DAL objDAL = new DAL();
    private DataSet objDS;

    #endregion

    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set
        {
            WucVehicleSearch1.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public string VehicleCategoryIds
    {
        get { return WucVehicleSearch1.VehicleCategoryIds; }
        set
        {
            WucVehicleSearch1.VehicleCategoryIds = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);



    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCategoryIds = WucVehicleSearch1.GetVehicleParameter("Vehicle_Category_ID");
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

                rbl_ReportType.Items[0].Text = "Consolidated eWayBills (" + objDR["Count_Consol_eWayBillNo"].ToString() + ")";
                rbl_ReportType.Items[1].Text = "Part B Updated By Party (" + objDR["Count_PartB_Updated_By_Party"].ToString() + ")";
                rbl_ReportType.Items[2].Text = "All LR Details (" + objDR["Count_All_LR"].ToString() + ")";
                rbl_ReportType.Items[3].Text = "Error eWayBills (" + objDR["Count_Error_eWayBills"].ToString() + ")";


            }
            else
            {

                rbl_ReportType.Items[0].Text = "Consolidated eWayBills";
                rbl_ReportType.Items[1].Text = "Part B Updated By Party";
                rbl_ReportType.Items[2].Text = "All LR Details";
                rbl_ReportType.Items[3].Text = "Error eWayBills";

            }
        }
    }
    public DataSet GetVehicleInformationOnVehicleChanged()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { 
                        objDAL.MakeInParams("@Date", SqlDbType.DateTime , 0, dtp_Date.SelectedDate ),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID)};
        objDAL.RunProc("COM_UserDesk_eWayBills_Checking_Record_Count", objSqlParam, ref objDS);
        return objDS;
    }


    protected void btn_view_Click(object sender, EventArgs e)
    {

        if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Select Vehicle!";
            ScriptManager.SetFocus(WucVehicleSearch1);
        }
        else
        {
            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");

            if (rbl_ReportType.SelectedValue == "1")
            {
                Path.Append("Reports/CL_Nandwana/User Desk/FrmeWayBillChecking_Consolidated_eWayBills.aspx?Date=" + dtp_Date.SelectedDate + "&VehicleId=" + WucVehicleSearch1.VehicleID + "&VehicleNo=" + WucVehicleSearch1.VehicleNumber);
            }

            else if (rbl_ReportType.SelectedValue == "2")
            {
                Path.Append("Reports/CL_Nandwana/User Desk/FrmeWayBillChecking_PartB_Updated_By_Party.aspx?Date=" + dtp_Date.SelectedDate + "&VehicleId=" + WucVehicleSearch1.VehicleID + "&VehicleNo=" + WucVehicleSearch1.VehicleNumber);
            }

            else if (rbl_ReportType.SelectedValue == "3")
            {
                Path.Append("Reports/CL_Nandwana/User Desk/FrmeWayBillChecking_All_LR.aspx?Date=" + dtp_Date.SelectedDate + "&VehicleId=" + WucVehicleSearch1.VehicleID + "&VehicleNo=" + WucVehicleSearch1.VehicleNumber);
            }

            else if (rbl_ReportType.SelectedValue == "4")
            {
                Path.Append("Reports/CL_Nandwana/User Desk/FrmeWayBillChecking_Error_eWayBills.aspx?Date=" + dtp_Date.SelectedDate + "&VehicleId=" + WucVehicleSearch1.VehicleID + "&VehicleNo=" + WucVehicleSearch1.VehicleNumber);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("Open_Details_Window('");
            sb.Append(Path);
            sb.Append("');");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }


    protected void btn_Mail_Click(object sender, EventArgs e)
    {
        if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Select Vehicle!";
            ScriptManager.SetFocus(WucVehicleSearch1);
        }
        else
        {
            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");

            Path.Append("Reports/CL_Nandwana/User Desk/FrmeWayBillChecking_Mail_Report.aspx?Date=" + dtp_Date.SelectedDate + "&VehicleId=" + WucVehicleSearch1.VehicleID + "&VehicleNo=" + WucVehicleSearch1.VehicleNumber);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("Open_Mail_Window('");
            sb.Append(Path);
            sb.Append("');");
            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }


}
