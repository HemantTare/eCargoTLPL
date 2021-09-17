<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAPMCSMS.aspx.cs" Inherits="Display_FrmAPMCSMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
    
<script type="text/javascript" src="../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>APMC Broker SMS</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="2" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="APMC Broker SMS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        Destination :&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <asp:DropDownList ID="ddlDestinaion" runat="server" CssClass="DROPDOWN" Width="52%">
                            <asp:ListItem Value="0">Select City</asp:ListItem>
                            <asp:ListItem Value="1">SURAT</asp:ListItem>
                            <asp:ListItem Value="2">VAPI</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        Vehicle No. :&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server"  />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="height: 20px">
                        <asp:Button ID="btn_SendSMS" runat="server" Text="Send SMS" CssClass="BUTTON" OnClick="btn_SendSMS_Click" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
