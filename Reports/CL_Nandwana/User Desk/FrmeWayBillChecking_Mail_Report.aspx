<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmeWayBillChecking_Mail_Report.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmeWayBillChecking_Mail_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../../Javascript/Common.js"></script>

<script type="text/javascript">




</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWayBill Checking - Mail Reports</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="eWayBill Checking - Mail Reports"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Vehicle No. :&nbsp;</td>
                    <td style="width: 50%; height: 15px;">
                        <asp:Label ID="lbl_VehicleNo" runat="server" BorderWidth="1px" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 30%; height: 15px;" align="left">
                        Date :
                        <asp:Label ID="lbl_Date" runat="server" BorderWidth="1px" CssClass="LABEL"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        eMail ID :&nbsp;</td>
                    <td style="width: 50%; height: 15px;">
                        <asp:TextBox ID="txt_Email_Id" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" Width="98%" onfocus="txtbox_onfocus(this)" ></asp:TextBox></td>
                    <td class="TD1" style="width: 30%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td align="left" colspan="2" style="height: 20px; width: 80%">
                        <asp:RadioButtonList ID="rbl_ReportType" runat="server" RepeatDirection="Vertical"
                            Font-Bold="true" ForeColor="#660099" AutoPostBack="true">
                            <asp:ListItem Selected="True" Text="Consolidated eWayBills" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Part B Updated By Party" Value="2"></asp:ListItem>
                            <asp:ListItem Text="All LR Details" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="height: 20px">
                        <asp:Button ID="btn_eMail" runat="server" Text="Send eMail" CssClass="BUTTON" OnClick="btn_eMail_Click" /></td>
                </tr>
                <tr>
                    <td colspan="3">
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
