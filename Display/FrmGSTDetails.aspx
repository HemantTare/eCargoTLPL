<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGSTDetails.aspx.cs" Inherits="Display_FrmGSTDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GSTIN Details</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="2">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="GSTIN Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        <asp:Label ID="lbl_CompanyNameH" runat="server" Font-Bold="true" Font-Size="Medium"
                            ForeColor="DarkBlue" Text="Company Name :"></asp:Label>&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <asp:Label ID="lbl_CompanyName" runat="server" Font-Bold="true" Font-Size="Medium"
                            ForeColor="DarkMagenta" Text="TURANTH LOGISTIC PVT LTD"></asp:Label></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        <asp:Label ID="lbl_GSTINH" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="DarkBlue"
                            Text="GSTIN :"></asp:Label>&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <asp:Label ID="lbl_GSTIN" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="DarkMagenta"
                            Text="27AAECT9480C1Z5"></asp:Label></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        <asp:Label ID="lbl_TransporterIDh" runat="server" Font-Bold="true" Font-Size="Medium"
                            ForeColor="DarkBlue" Text="Transporter ID :"></asp:Label>&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <asp:Label ID="lbl_TransporterID" runat="server" Font-Bold="true" Font-Size="Medium"
                            ForeColor="DarkMagenta" Text="27AAECT9480C1Z5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        <asp:Label ID="lbl_MobileNo" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Black"
                            Text="Enter Mobile No. :"></asp:Label></td>
                    <td style="width: 60%; height: 15px;">
                        <asp:TextBox ID="txt_MobileNo" runat="server" CssClass="TEXTBOX" MaxLength="10" Width="200px"
                            onkeypress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%; height: 15px;">
                        <asp:Label ID="lbl_ClientName" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Black"
                            Text="Client Name :"></asp:Label>&nbsp;</td>
                    <td style="width: 60%; height: 15px;">
                        <asp:TextBox ID="txt_ClientName" runat="server" CssClass="TEXTBOX" MaxLength="100" onfocus="txtbox_onfocus(this)"
                             onblur="txtbox_onlostfocus(this);" Width="400px"></asp:TextBox></td>
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
