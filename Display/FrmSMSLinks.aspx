<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSMSLinks.aspx.cs" Inherits="Display_FrmSMSLinks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">

function GSTINDetails() {
    var Path = '';
    Path = '../Display/FrmGSTDetails.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 500;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'GSTINDetails', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function BranchAddressDetails() {
    var Path = '';
    Path = '../Display/FrmBranchAddressUtility.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 500;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'BranchAddressDetails', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function BankDetailsSMS() {
    var Path = '';
    Path = '../Display/FrmBankDetailsSMS.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 500;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'BranchAddressDetails', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function TrackNTraceSMS() 
{
    var Path = '';
    Path = '../TrackNTrace/FrmMainTrackNTrace.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 200;
    var popH = h - 100;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'CustomPopUp_Track_And_Trace', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function VehicleTrackingSMS() 
{
    var Path = '../Operations/Outward/FrmVehicleTrackingSMS.aspx';
    
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 400;
    var popH = h - 200;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    
    window.open(Path, 'CustomPopUp_Vehicle_Tracking', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function MultiVehicleTrackingSMS() 
{
    var Path = '../Operations/Outward/FrmMultipleVehicleTrackingSMS.aspx';
    
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 200;
    var popH = h-50;
    
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    
    window.open(Path, 'CustomPopUp_Multiplr_Vehicle_Tracking', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function APMC_SMS() 
{
    var Path = '';
    Path = '../Display/FrmAPMCSMS.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 500;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'APMCSMS', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}

function eWayBillChecking() 
{
    var Path = '';
    Path = '../Display/FrmeWayBillChecking.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 500;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'eWayBillChecking', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}
</script>

<head runat="server">
    <title>Quick SMS Links</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%" border="1" style="background-color: White">
                <tr>
                    <td class="TDGRADIENT" colspan="2">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Quick SMS Links"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 125px">
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_GSTDetails" runat="server" ImageUrl="~/Images/GSTLogo1.png" />
                    </td>
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_BranchAddres" runat="server" ImageUrl="~/Images/BranchAddress.png" /></td>
                </tr>
                <tr style="height: 125px">
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_BankDetails" runat="server" ImageUrl="~/Images/BankDetails.jpg" /></td>
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_TrackNTrace" runat="server" ImageUrl="~/Images/TrackNTrace.png" /></td>
                </tr>
                <tr style="height: 125px">
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_VehicleTracking" runat="server" ImageUrl="~/Images/VehicleTracking.png" />
                    </td>
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_MultipleVehicleTracking" runat="server" ImageUrl="~/Images/MultiVehicleTracking2.png" />
                    </td>
                </tr>
                <tr id="tr_APMCSMS" style="width: 125px">
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_APMCSMS" runat="server" ImageUrl="~/Images/APMCSMS.jpg"
                            Visible="false" />
                    </td>
                    <td align="center" style="width: 50%">
                        <asp:ImageButton ID="imgbtn_eWayBillChecking" runat="server" ImageUrl="~/Images/CheckeWayBills.jpg" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
