<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBookingStockList.aspx.cs" Inherits="Reports_Booking_FrmBookingStockList" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc4" %>

<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>

<%-- Author : Harshal Sapre
     Date   : 14-01-09
     Desc   : Booking Stock List --%>
     
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Register</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%">
                        <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Booking Stock List"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td class="TD1" style="width: 13%">
                                    As On Date:</td>
                                <td style="width: 20%">
                                    <uc4:WucDatePicker ID="WucDatePicker1" runat="server" />
                                </td>
                                <td style="width: 14%">
                                </td>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 13%">
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    self.parent.hideload();
    </script>

</body>
</html>
