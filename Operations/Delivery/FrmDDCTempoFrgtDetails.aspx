<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDDCTempoFrgtDetails.aspx.cs"
    Inherits="Operations_Delivery_FrmDDCTempoFrgtDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DDC Tempo Freight Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="TABLE">
            <tr>
                <td class="TDGRADIENT">
                    <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="DDC Tempo Freight Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 99%" valign="top">
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 99%">
                    <asp:UpdatePanel ID="updpnl_DDCTempoFrgt" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Div_DDCTempoFrgt" class="DIV" style="height: 400px; width: 99%;">
                                <asp:DataGrid ID="dg_DDCTempoFrgt" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                    CssClass="GRID" Style="border-top-style: none" Width="90%">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DDC_ID" HeaderText="DDC_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_ID" HeaderText="GC_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Size" HeaderText="Size"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Delivered_Articles" HeaderText="Pkgs"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Total_GC_Amount" HeaderText="LR Frt"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DeliveryAreaName" HeaderText="Delivery Area"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Local_Tempo_Freight" HeaderText="Tempo Frt"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Bonus" HeaderText="Bonus"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AddTempoFrt" HeaderText="AddTemoFrt"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="updpnl_PDSTempoFrgt" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Div_PDSTempoFrgt" class="DIV" style="height: 400px; width: 99%;">
                                <asp:DataGrid ID="dg_PDSTempoFrgt" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                    CssClass="GRID" Style="border-top-style: none" Width="90%">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                    <Columns>
                                        <asp:BoundColumn DataField="PDS_ID" HeaderText="PDS_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_ID" HeaderText="GC_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Size" HeaderText="Size"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Delivery_Articles" HeaderText="Pkgs"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Total_GC_Amount" HeaderText="LR Frt"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DeliveryAreaName" HeaderText="Delivery Area"></asp:BoundColumn> 
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
