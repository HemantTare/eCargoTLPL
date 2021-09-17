<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTracePDSDeliveryDetails.ascx.cs"
    Inherits="TrackNTrace_WucTrackNTracePDSDeliveryDetails" %>
<table width="100%" class="TABLE">
    <tr runat="server" id="tr_Error" visible="false">
        <td class="HeadingNoBGColor" colspan="2">
            <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
    <tr runat="server" id="tr_PDSRepDelDetals">
        <td colspan="6">
            <asp:Repeater ID="Rep_PDSDeliveryDetails" runat="server" OnItemDataBound="Rep_PDSDeliveryDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width: 25%">
                                PDS No</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("PDS No")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                <asp:Label ID="lbl_Tot_GC" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Total No Of GC")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                PDS Date</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("PDS Date")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Total Articles</td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Total Articles")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                PDS Branch</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("PDS Branch")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Total Weight</td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Total Weight")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                Dly Mode</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("Delivery_Mode")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Dly Mode Description</td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Delivery_Mode_Description")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                Mobile No</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("MobileNo")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Vehicle No</td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Vehicle_No")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                Vendor Name</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("Vendor_Name")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Total Freight</td>
                            <td class="NumericValue" style="width: 25%">
                                <%#Eval("Total_GC_Amount")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width: 25%">
                                Supervisor Name</td>
                            <td class="AlphaValue" style="width: 25%">
                                <%#Eval("Supervisor Name")%>
                            </td>
                            <td class="Feild" style="width: 25%">
                                Updated By</td>
                            <td class="AlphaValue" style="width: 25%">
                                <asp:LinkButton ID="btn_DDC_UpdatedBy" Text='<%#Eval("Updated By")%>' runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr runat="server" id="tr_PDSDELIVERY_Details">
        <td style="width: 100%">
            <div style="width: 100%; background-color: #DFE0F8">
                <table style="width: 100%;">
                    <tr>
                        <td class="HeadingNoBGColor" colspan="2">
                            DELIVERY DETAILS</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:DataGrid ID="DG_GC_PDSDelivery_Details" Width="100%" AutoGenerateColumns="false"
                                CssClass="Grid" runat="server" OnItemDataBound="DG_GC_PDSDelivery_Details_ItemDataBound">
                                <AlternatingItemStyle />
                                <HeaderStyle Font-Bold="true" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_GC_No" Text='<%#Eval("GC No")%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="Delivered Articles" HeaderText="Delivered Art" />
                                    <asp:BoundColumn DataField="Delivered Weight" HeaderText="Delivered Wt" />
                                    <asp:BoundColumn DataField="Total_Freight" HeaderText="Total Freight" />
                                    <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type" />
                                    <asp:TemplateColumn HeaderText="Updated By">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_UpdatedBy" Text='<%#Eval("Updated By")%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="hdn_DelType_Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_PDS_ID" Value='<%#Eval("PDS_ID")%>' runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_UpdatedBy_id" Value='<%#Eval("UpdatedBy_id")%>' runat="server">
                                            </asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
