<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPendingChequeForDepositeDetails.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_frmPendingChequeForDepositeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cheque Details</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%" style="background-color: #e3ffff">
                <tr>
                    <td class="TDGRADIENT" colspan="2">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Cheque Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="Left" style="width: 90%">
                        <asp:Label ID="lbl_ChqSentToHOOnH" runat="server" CssClass="LABEL" Text="Sent On"
                            Font-Bold="true"></asp:Label>
                        <asp:Label ID="lbl_ChqSentToHOOn" runat="server" CssClass="LABEL" Text="lbl_ChqSentToHOOn"
                            Font-Bold="true" ForeColor="Navy"></asp:Label>
                        <asp:Label ID="lbl_SentTime" runat="server" CssClass="LABEL" Text="lbl_SentTime"
                            Font-Bold="true" ForeColor="Navy"></asp:Label>
                        <asp:Label ID="lbl_ChqSentToHOByH" runat="server" CssClass="LABEL" Text=" By " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lbl_ChqSentToHOBy" runat="server" CssClass="LABEL" Text="lbl_ChqSentToHOBy"
                            Font-Bold="true" ForeColor="Navy"></asp:Label></td>
                    <td align="Left" style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="Left" style="width: 90%">
                        <asp:Label ID="lbl_ChqRecdAtHOOnH" runat="server" CssClass="LABEL" Text="Recvd On "
                            Font-Bold="true"></asp:Label>
                        <asp:Label ID="lbl_ChqRecdAtHOOn" runat="server" CssClass="LABEL" Text="lbl_ChqRecdAtHOOn"
                            Font-Bold="true" ForeColor="Navy"></asp:Label>
                        <asp:Label ID="lbl_RecdTime" runat="server" CssClass="LABEL" Text="lbl_RecdTime"
                            Font-Bold="true" ForeColor="Navy"></asp:Label>
                        <asp:Label ID="lbl_ChqRecdAtHOByH" runat="server" CssClass="LABEL" Text=" By " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lbl_ChqRecdAtHOBy" runat="server" CssClass="LABEL" Text="lbl_ChqRecdAtHOBy"
                            Font-Bold="true" ForeColor="Navy"></asp:Label></td>
                    <td align="Left" style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
