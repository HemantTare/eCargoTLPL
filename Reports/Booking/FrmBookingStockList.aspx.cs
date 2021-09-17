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

//Author : Harshal Sapre
//Date   : 14-01-09
//Desc   : Booking Stock List

public partial class Reports_Booking_FrmBookingStockList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string Path = "../../Reports/Booking/FrmBookingStockListView.aspx?Region_Id=" + Wuc_Region_Area_Branch1.RegionID + "&Area_id=" + Wuc_Region_Area_Branch1.AreaID + "&Branch_id=" + Wuc_Region_Area_Branch1.BranchID + "&As_On_Date=" + WucDatePicker1.SelectedDate;
        string popupScript = "<script language='javascript'>var w = screen.availWidth-10;var h = screen.availHeight-10;var popW =w-2, popH = h-25;var leftPos = (w-popW)/2, topPos = ((h-20)-popH)/2;window.open('" + Path + "', 'CustomPopUp', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes') </script>";
        Page.ClientScript.RegisterStartupScript(typeof(string), "PopupScript", popupScript);
    }

}
