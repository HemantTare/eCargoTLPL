<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFleetAlertLinks.aspx.cs" Inherits="Display_FrmFleetAlertLinks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript">
    
    function newwindow(url)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);       
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=no,scrollbars=yes')
        return false;
    }   
</script>
<head runat="server">
    <title>Fleet Alerts Links</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="height:auto;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: #bbccc3 1px solid; border-top: #bbccc3 1px solid; border-left: #bbccc3 1px solid; border-bottom: #bbccc3 1px solid;">
            <tr>
                <td colspan="2" style="background-image: url(../images/Desk_Header.gif)">&nbsp;
                    <asp:Label ID="lbl_Alerts" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#CE0029" Text="Alerts !"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Image ID="Img5" runat="server" ImageUrl="~/images/Alerts.gif" Height="94px" Width="163px" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%;text-align:left;">&nbsp; &nbsp;
                    <asp:Image ID="Img6" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_Btn_Alerts_PM_Task" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Preventive Maintainance()</asp:LinkButton>
                </td>
                <td style="width: 50%;">
                    <asp:Image ID="Img7" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_btn_Vehicle_Fitness_Certificate" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Vehicle Fitness Certificate()</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td  style="width: 50%;text-align:left;">&nbsp; &nbsp;
                    <asp:Image ID="Img8" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_btn_Vehicle_Permit" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Vehicle Permit()</asp:LinkButton>
                </td>
                <td style="width: 50%;">
                    <asp:Image ID="Img9" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_btn_Vehicle_Permit_Temp" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Vehicle Permit Temporary()</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 50%;text-align:left;">&nbsp; &nbsp;
                    <asp:Image ID="Img10" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_btn_Vehicle_Permit_Tax" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Vehicle Permit Tax()</asp:LinkButton>
                </td>
                <td style="width: 50%;text-align:left;">
                    <asp:Image ID="Img11" runat="server" ImageUrl="~/images/Bullet.gif" />   
                    <asp:LinkButton ID="lnk_btn_Vehicle_Insurance" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Underline="True" ForeColor="#213163">Vehicle Insurance(0)</asp:LinkButton>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            </table>
        </div>
    </form>
    
     <script type="text/javascript">
    self.parent.hideload()
    </script>
</body>
</html>
