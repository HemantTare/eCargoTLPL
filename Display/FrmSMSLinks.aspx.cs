using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using ClassLibraryMVP.Security;

public partial class Display_FrmSMSLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Rights.GetObject().getForm_Rights(324).canAdd()) == true)
        {
            imgbtn_VehicleTracking.Visible = true;
            imgbtn_MultipleVehicleTracking.Visible = true;
        }
        else
        {
            imgbtn_VehicleTracking.Visible = false;
            imgbtn_MultipleVehicleTracking.Visible = false;
        }

        if (!IsPostBack)
        {
            imgbtn_GSTDetails.Attributes.Add("onclick", "return GSTINDetails()");
            imgbtn_BranchAddres.Attributes.Add("onclick", "return BranchAddressDetails()");
            imgbtn_BankDetails.Attributes.Add("onclick", "return BankDetailsSMS()");
            imgbtn_TrackNTrace.Attributes.Add("onclick", "return TrackNTraceSMS()");
            imgbtn_VehicleTracking.Attributes.Add("onclick", "return VehicleTrackingSMS();");
            imgbtn_MultipleVehicleTracking.Attributes.Add("onclick", "return MultiVehicleTrackingSMS();");
            imgbtn_APMCSMS.Attributes.Add("onclick", "return APMC_SMS();");
            imgbtn_eWayBillChecking.Attributes.Add("onclick", "return eWayBillChecking();");

        }

        if (UserManager.getUserParam().MainId == 53 || UserManager.getUserParam().HierarchyCode == "HO" || UserManager.getUserParam().HierarchyCode == "AD")
        {
            imgbtn_APMCSMS.Visible = true;
        }

        if (UserManager.getUserParam().HierarchyCode == "HO" || UserManager.getUserParam().HierarchyCode == "AD")
        {
            imgbtn_eWayBillChecking.Visible = true;
        }

        if ((Rights.GetObject().getForm_Rights(5265).canAdd()) == true)
        {
            imgbtn_eWayBillChecking.Visible = true;
        }
        else
        {
            imgbtn_eWayBillChecking.Visible = false;

        }

    }    
}
