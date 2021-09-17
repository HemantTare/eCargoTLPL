<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDisplay.aspx.cs" Inherits="Display_FrmDisplay" %>

<%@ Register Src="~/Display/Alerts/Wuc_Operations_Links.ascx" TagName="Wuc_Operations_Links"
    TagPrefix="uc7" %>
<%@ Register Src="Alerts/Wuc_Alerts_Nandwana.ascx" TagName="Wuc_Alerts_Excel" TagPrefix="uc6" %>
<%@ Register Src="../Bars/WucBottomBar.ascx" TagName="WucBottomBar" TagPrefix="uc2" %>
<%@ Register Src="../Bars/WucHeader.ascx" TagName="WucHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Login/WucSwitchToDivision.ascx" TagName="WucSwitchDivision" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../Javascript/Display.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">

function hideload()
    { 
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'hidden'; 
    }
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
        
function displayload()
    { 
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'visible'; 
        loading.style.position='absolute';
        loading.style.left=(document.body.clientWidth/2)-20+'px';
        loading.style.top=(document.body.clientHeight/2)-60+'px';
    }
    

 function ChangePassword(url)
    {
 
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 500;
        var popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
          return false;
    }   

function FullSizeWindow(url)
    {
      
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;       
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes,resizable=no,scrollbars=yes')
        return false;
    }
    
function get_button_nullsession_clientid()
{ 
btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

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

function SMSLinksDetails() {
    var Path = '';
    Path = '../Display/FrmSMSLinks.aspx';
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 750;
    var popH = h - 100;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'SMSLinks', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}
</script>

<head runat="server">
    <title>
        <%=Application["Title"]%>
    </title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onload="hideload()" onunload="clearvariables();" style="margin: 0px 0px;">
    <form id="form1" runat="server">
        <div style="position: absolute; font-size: 11px; font-family: Verdana;">
            <span id="loading">
                <img src="../Images/Bar_Circle.gif" />
                Loading .... </span>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <uc1:WucHeader ID="WucHeader1" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%;">
                    <table cellpadding="20" cellspacing="20" width="100%">
                        <tr>
                            <td width="100%" style="height: 186px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-bottom: #8591a3 1px solid">
                                    <tr>
                                        <td style="width: 70%; height: 56px;">
                                            <asp:Image ID="Img_Welcome" runat="server" ImageUrl="~/Images/Welcome.gif" />
                                            <asp:Label ID="lbl_Name" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="20px"
                                                ForeColor="#CE0029" Text="Parikshit"></asp:Label></td>
                                        <td style="width: 30%; vertical-align: bottom; text-align: right; height: 56px;">
                                            <asp:Label ID="lbl_Date" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="14px"
                                                ForeColor="#424242" Text="Label"></asp:Label></td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 60%">
                                        </td>
                                        <td style="width: 20%; text-align: right">
                                            <uc3:WucSwitchDivision ID="WucSwitchDivision2" runat="server" />
                                        </td>
                                        <td style="width: 30%; text-align: right">
                                            <asp:LinkButton ID="lnk_ChangePassword" runat="server" Font-Names="Verdana" Text="Change Password"
                                                Font-Size="11px" Font-Underline="True" ForeColor="#424242"></asp:LinkButton>
                                            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
                                                OnClick="btn_null_session_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" cellspacing="10" style="width: 100%;">
                                    <tr>
                                        <td style="width: 45%;">
                                            &nbsp;<uc6:Wuc_Alerts_Excel ID="Wuc_Alerts_Excel1" runat="server" />
                                        </td>
                                        <td style="width: 10; text-align: center; vertical-align: top;">
<%--                                            <asp:ImageButton ID="imgbtn_GSTDetails" runat="server" ImageUrl="~/Images/GSTLogo1.png" /><br />
                                            <br />
                                            <asp:ImageButton ID="imgbtn_BranchAddres" runat="server" ImageUrl="~/Images/BranchAddress.png" />--%>
                                            <asp:ImageButton ID="imgbtn_SMSLinks" runat="server" ImageUrl="~/Images/SMS.png" />
                                            </td>
                                        <td style="width: 45%;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <uc7:Wuc_Operations_Links ID="Wuc_Operations_Links1" runat="server" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Wuc_Operations_Links1" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 45%;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: #bbccc3 1px solid;
                                                border-top: #bbccc3 1px solid; border-left: #bbccc3 1px solid; border-bottom: #bbccc3 1px solid;">
                                                <tr>
                                                    <td style="background-image: url(../images/Desk_Header.gif);">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="red" Text="Service Info"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%; height: 50px">
                                                        <marquee id="MQ1" behavior="scroll" scrollamount="3" onmouseout="this.start();" onmouseover="this.stop();"
                                                            direction="up">
                                <span style="color: rgb(0, 0, 128); font-weight:bold; font-family:Verdana;font-size: small;font-size: 8pt; color: navy;">
                                    <p style="margin: 0in 0in 0pt; text-align: justify;" id="ServiceInfoLine1" runat="server">
                                    </p>
                                    <p style="margin: 0in 0in 0pt; text-align: justify;" id="ServiceInfoLine2" runat="server">
                                    </p>
                                    <p style="margin: 0in 0in 0pt; text-align: justify;" id="ServiceInfoLine3" runat="server">
                                    </p>
                                </span>
                         </marquee>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10%; vertical-align: top;" align="center">
                                            &nbsp;</td>
                                        <td style="width: 45%;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 50px;
                                                border-right: #bbccc3 1px solid; border-top: #bbccc3 1px solid; border-left: #bbccc3 1px solid;
                                                border-bottom: #bbccc3 1px solid;">
                                                <tr>
                                                    <td style="background-image: url(../images/Desk_Header.gif)">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="navy" Text="General Info"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <marquee id="MQ2" behavior="scroll" scrollamount="3" onmouseout="this.start();" onmouseover="this.stop();"
                                                            direction="up">
                            <span style="color: rgb(0, 0, 0); font-weight:bold;font-family:Verdana;font-size: small;font-size: 8pt; color:Red;">
                                <p style="margin: 0in 0in 1pt; text-align: justify;" id="GeneralInfoLine1" runat="server">
                                </p>
                                <p style="margin: 0in 0in 1pt; text-align: justify;" id="GeneralInfoLine2" runat="server">
                                </p>
                                <p style="margin: 0in 0in 1pt; text-align: justify;" id="GeneralInfoLine3" runat="server">
                                </p>
                            </span>
                     </marquee>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <uc2:WucBottomBar ID="WucBottomBar1" runat="server" />
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    </script>

</body>
</html>
