<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceStatusDetails.ascx.cs"
    Inherits="TrackNTrace_WucTrackNTraceStatusDetails" %>
<style type="text/css">
      .SHOWSELECTEDLINK{FONT-SIZE: 11px;FONT-FAMILY: Verdana;color:#0033ff;text-decoration:underline;}
</style>
<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
        <td class="HeadingNoBGColor" colspan="2">
            <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
</table>
<table class="TABLE" runat="server" id="tbl_Status">
    <tr>
        <td style="width: 100%">
            <div style="width: 100%; background-color: #DFE0F8">
                <table style="width: 100%;">
                    <tr>
                        <td class="HeadingNoBGColor" colspan="2">
                            CURRENT STATUS</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel runat="server" ID="upnl_status" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:DataGrid ID="DG_GC_Status_Details" Width="100%" AutoGenerateColumns="false"
                                        CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Status_Details_ItemDataBound">
                                        <AlternatingItemStyle />
                                        <HeaderStyle Font-Bold="true" />
                                        <Columns>
                                            <asp:BoundColumn DataField="GC No" HeaderText="LR No" />
                                            <asp:BoundColumn DataField="Total Articles" HeaderText="Total Articles" />
                                            <asp:BoundColumn DataField="Articles" HeaderText="Current Articles" />
                                            <asp:BoundColumn DataField="Current Branch" HeaderText="Current Branch" />
                                            <asp:BoundColumn DataField="Status" HeaderText="Current Status" />
                                            <asp:BoundColumn DataField="Document No" HeaderText="Document No" />
                                            <asp:BoundColumn DataField="Document Date" HeaderText="Document Date" />
                                            <asp:TemplateColumn HeaderText="Status ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdn_Status_Id" Value='<%#Eval("Status ID")%>' runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdn_DelType_Id" Value='<%#Eval("DDC Type ID")%>' runat="server">
                                                    </asp:HiddenField>
                                                    <asp:HiddenField ID="hdn_Document_Id" Value='<%#Eval("Document_ID")%>' runat="server">
                                                    </asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DG_GC_Status_Details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="HeadingNoBGColor" colspan="2" runat="server" id="td_deliveryattempts">
                            <br />
                            DELIVERY ATTEMPTS</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:DataGrid ID="DG_GC_DeliveryAttempts_Details" Width="100%" AutoGenerateColumns="false"
                                        CssClass="Grid" runat="server" >
                                        <AlternatingItemStyle />
                                        <HeaderStyle Font-Bold="true" />
                                        <Columns>
                                            <asp:BoundColumn DataField="GC_No" HeaderText="LR No" />
                                            <asp:BoundColumn DataField="Attempted_On" HeaderText="Attempted On" />
                                            <asp:BoundColumn DataField="DDC_No_For_Print" HeaderText="DDC No" />
                                            <asp:BoundColumn DataField="Articles" HeaderText="Articles" />
                                            <asp:BoundColumn DataField="UnDelivered_Reason" HeaderText="Un Delivered Reason" />
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DG_GC_DeliveryAttempts_Details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
