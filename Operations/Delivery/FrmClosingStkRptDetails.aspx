<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClosingStkRptDetails.aspx.cs"
    Inherits="Operations_Delivery_FrmClosingStkRptDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Stock Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="TABLE">
            <tr>
                <td class="TDGRADIENT">
                    <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Stock Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 99%" valign="top">
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 99%">
                    <asp:UpdatePanel ID="updpnl_CloseStkDtls" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Div_CloseStkDtls" class="DIV" style="height: 400px; width: 99%;">
                                <asp:DataGrid ID="dg_CloseStkDtls" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                    CssClass="GRID" Style="border-top-style: none" Width="90%">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DocNo" HeaderText="Doc No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DocDate" HeaderText="Doc Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalLR" HeaderText="Total LR">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalQTY" HeaderText="Total QTY">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToPayFrt" HeaderText="ToPay Frt">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 99%; text-align: center;" valign="top">
                    <asp:Button ID="btnExit" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btnExit_Click" /></td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
