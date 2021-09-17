<%@ Page AutoEventWireup="true" CodeFile="FrmShortTripVehiclesMemoDetails.aspx.cs"
    Inherits="Operations_Outward_FrmShortTripVehiclesMemoDetails" Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Short Trips Vehicles Invoice Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Short Trips Vehicles Invoice Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 24%; height: 26px;">
                                <asp:Label ID="lblVehicleNo" runat="server" Text="" Font-Bold="true" ForeColor="DarkMagenta"   Font-Size="Large"  ></asp:Label></td>
                            <td style="width: 10%; height: 26px;" class="TD1">
                                </td>
                            <td style="width: 24%; height: 26px;">
                                </td>
                            <td class="TD1" style="width: 9%; height: 26px;">
                            </td>
                            <td style="width: 9%; height: 26px;">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                            <td style="width: 24%; height: 26px;">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="False" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="25" OnItemDataBound="dg_Details_ItemDataBound">
                                    <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Navy" ForeColor="White" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TripNo" HeaderText="Trip No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Driver" HeaderText="Driver"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Invoice_Date" HeaderText="Invoice Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Invoice_No" HeaderText="Invoice No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Invoice_From" HeaderText="Invoice From"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Invoice_To" HeaderText="Invoice To"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
