<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTopWalkinClientDetailsMonthly.aspx.cs"
    Inherits="Reports_Sales_Billing_FrmTopWalkinClientDetailsMonthly" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Top Client Details Monthly Summary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Top Client Details Monthly Summary"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Errors" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 10%">
                    &nbsp
                </td>
                <td style="width: 20%">
                    &nbsp
                </td>
                <td style="width: 50%">
                    &nbsp
                </td>
                <td style="width: 20%">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp
                </td>
                <td style="width: 20%">
                    Branch :
                    <asp:Label ID="lbl_Branch" runat="server" Text="Branch" Font-Bold="true" ForeColor="DarkMagenta"></asp:Label>
                </td>
                <td style="width: 50%">
                    Client :
                    <asp:Label ID="lbl_Client" runat="server" Text="Client" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>
                <td style="width: 20%">
                    Total :
                    <asp:Label ID="lbl_TotalLR" runat="server" Text="0" Font-Bold="true" ForeColor="DarkMagenta"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp
                </td>
                <td style="width: 20%">
                    &nbsp
                </td>
                <td style="width: 50%">
                    &nbsp
                </td>
                <td style="width: 20%">
                    &nbsp
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 510px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="false" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="true"
                                    OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                                    PagerStyle-HorizontalAlign="Left" PageSize="100" Width="90%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
