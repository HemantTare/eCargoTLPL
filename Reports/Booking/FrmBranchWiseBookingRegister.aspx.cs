using System;

//Author : Ankit champaneriya
//Desc   : Booking Register
//Date   : 03-01-09

public partial class Reports_Booking_FrmBranchWiseBookingRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string Path = "../../Reports/Booking/FrmBranchWiseBookingRegisterView.aspx?Region_Id=" + Wuc_Region_Area_Branch1.RegionID + "&Area_id=" + Wuc_Region_Area_Branch1.AreaID + "&Branch_id=" + Wuc_Region_Area_Branch1.BranchID + "&From_Date=" + Wuc_From_To_Datepicker1.SelectedFromDate + "&To_date=" + Wuc_From_To_Datepicker1.SelectedToDate + "&Booking_Mode=" + rdl_BookingRegister.SelectedValue + "&Booking_Type_Id=" + Wuc_GC_Parameters1.Booking_Type_ID + "&Delivery_Type_Id=" + Wuc_GC_Parameters1.Delivery_Type_ID + "&Payment_Type_Id=" + Wuc_GC_Parameters1.Payment_Type_ID;
        string popupScript = "<script language='javascript'>var w = screen.availWidth-10;var h = screen.availHeight-10;var popW =w-2, popH = h-25;var leftPos = (w-popW)/2, topPos = ((h-20)-popH)/2;window.open('" + Path + "', 'CustomPopUp', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes') </script>";
        Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", popupScript);
    }

}
