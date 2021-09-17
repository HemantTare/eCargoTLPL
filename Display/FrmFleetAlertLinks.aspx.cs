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
using ClassLibraryMVP.Security;

public partial class Display_FrmFleetAlertLinks : System.Web.UI.Page
{
    private int _userID;
    private int _menuSystemId;
    public DataSet Ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _userID = UserManager.getUserParam().UserId;
            _menuSystemId = UserManager.getUserParam().SystemId;

            Rights objRights = new Rights();
            objRights.SetRights(_userID, _menuSystemId, 0, 0);

            Raj.EC.Common objCommon = new Raj.EC.Common();
            objCommon.RunTaskScheduler();

            Ds = objCommon.GetPMTaskAlertCount("AlertsList");

            lnk_Btn_Alerts_PM_Task.Text = "Preventive Maintainance(" + Convert.ToString(Ds.Tables[0].Rows[0][1]) + ")";
            lnk_btn_Vehicle_Fitness_Certificate.Text = "Vehicle Fitness Certificate(" + Convert.ToString(Ds.Tables[0].Rows[1][1]) + ")";
            lnk_btn_Vehicle_Permit.Text = " Vehicle Permit(" + Convert.ToString(Ds.Tables[0].Rows[2][1]) + ")";
            lnk_btn_Vehicle_Permit_Temp.Text = "Vehicle Permit Temporary(" + Convert.ToString(Ds.Tables[0].Rows[3][1]) + ")";
            lnk_btn_Vehicle_Permit_Tax.Text = "Vehicle Permit Tax(" + Convert.ToString(Ds.Tables[0].Rows[4][1]) + ")";
            lnk_btn_Vehicle_Insurance.Text = "Vehicle Insurance(" + Convert.ToString(Ds.Tables[0].Rows[5][1]) + ")";

            Add_Attributes();

            string CallFrom;
            CallFrom=Request.QueryString["CallFrom"];

            if (CallFrom == "PM")
            {
                lbl_Alerts.Text = "Vehicle PM Alerts";

                lnk_Btn_Alerts_PM_Task.Visible = true;
                lnk_btn_Vehicle_Fitness_Certificate.Visible = false;
                lnk_btn_Vehicle_Permit.Visible = false;
                lnk_btn_Vehicle_Permit_Temp.Visible = false;
                lnk_btn_Vehicle_Permit_Tax.Visible = false;
                lnk_btn_Vehicle_Insurance.Visible = false;

                Img6.Visible = true;
                Img7.Visible = false;
                Img8.Visible = false;
                Img9.Visible = false;
                Img10.Visible = false;
                Img11.Visible = false;

            }
            else
            {
                lbl_Alerts.Text = "Vehicle Renewals Alerts";

                lnk_Btn_Alerts_PM_Task.Visible = false;
                lnk_btn_Vehicle_Fitness_Certificate.Visible=true;
                lnk_btn_Vehicle_Permit.Visible = true;
                lnk_btn_Vehicle_Permit_Temp.Visible = true;
                lnk_btn_Vehicle_Permit_Tax.Visible = true;
                lnk_btn_Vehicle_Insurance.Visible = true;

                Img6.Visible = false;
                Img7.Visible = true;
                Img8.Visible = true;
                Img9.Visible = true;
                Img10.Visible = true;
                Img11.Visible = true;

            }

        }
    }
    private void Add_Attributes()
    {
        //Preventive Maintainance
        string Url = "../Alerts/PM/FrmPMTaskList.aspx";
        lnk_Btn_Alerts_PM_Task.Attributes.Add("onclick", "return newwindow('" + Url + "')");

        // Vehicle Fitness Certificate
        Url = "../Alerts/Renewals/FrmTaskRenewalList.aspx?TaskDefinationId=" + ClassLibraryMVP.Util.EncryptInteger(2);
        lnk_btn_Vehicle_Fitness_Certificate.Attributes.Add("onclick", "return newwindow('" + Url + " ')");

        //Vehicle Permit
        Url = "../Alerts/Renewals/FrmVehiclePermit.aspx";
        lnk_btn_Vehicle_Permit.Attributes.Add("onclick", "return newwindow('" + Url + " ')");

        //Vehicle Permit Temporary
        Url = "../Alerts/Renewals/FrmVehiclePermitTemporary.aspx";
        lnk_btn_Vehicle_Permit_Temp.Attributes.Add("onclick", "return newwindow('" + Url + " ')");

        //Vehicle Permit Tax
        Url = "../Alerts/Renewals/FrmVehiclePermitTax.aspx";
        lnk_btn_Vehicle_Permit_Tax.Attributes.Add("onclick", "return newwindow('" + Url + " ')");

        // Vehicle Insurance
        Url = "../Alerts/Renewals/FrmTaskRenewalList.aspx?TaskDefinationId=" + ClassLibraryMVP.Util.EncryptInteger(6);
        lnk_btn_Vehicle_Insurance.Attributes.Add("onclick", "return newwindow('" + Url + " ')");
    }    
}
