<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyBookingStatementPaidPrinOrExport.aspx.cs"
    Inherits="Finance_Reports_FrmDailyBookingStatementPaidPrinOrExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Booking Statement Client Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">

function GridPaidToPay(Path)
{ 
    window.open(Path,'PaidTBBDetails','width=1000,height=800,top=25,left=25,menubar=no,resizable=yes,scrollbars=yes')
    self.close();
    return false;
}
</script>


</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DailyBookingStatement" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily Booking Statement Paid Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
            <td style="text-align: left; width:50%" >
                    
                <asp:Button ID="btn_Print" CssClass="BUTTON" runat="server" Text="Print"  />
                    
                </td>
                <td style="text-align: left;width:50%">
                    
                <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel" runat="server"  />
                    
                </td>
            </tr>
            
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
