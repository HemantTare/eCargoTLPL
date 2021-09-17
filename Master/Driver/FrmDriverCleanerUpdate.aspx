<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCleanerUpdate.aspx.cs"
    Inherits="Master_Driver_FrmDriverCleanerUpdate" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">

function viewwindow_DriverCleanerChange(IsCleaner,ChangeType)
{
    
    
        var Path='../../Master/Driver/FrmDriverCleanerUpdateNew.aspx?IsCleaner=' + IsCleaner;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'DriverCleanerChange', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver - Cleaner Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver - Cleaner Update"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 99%" colspan="2">
                        <asp:RadioButtonList ID="rbl_DriverCleaner" runat="server" RepeatDirection="Horizontal"
                            Font-Bold="true" Font-Size="Medium" ForeColor="#660099" AutoPostBack="true">
                            <asp:ListItem Selected="True" Text="Driver" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Cleaner" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%" colspan="3">
                    </td>
                </tr>
                <tr class="HIDEGRIDCOL">
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 79%;">
                        <asp:RadioButtonList ID="rbl_ChangeType" runat="server" RepeatDirection="Vertical"
                            AutoPostBack="true">
                            <asp:ListItem Selected="True" Text="Incoming" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Vehicle Change" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Outgoing" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Change Details" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="3" align="center">
                        <asp:Button ID="btn_Go" runat="server" CssClass="BUTTON" Text="Go"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%" colspan="3">
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
