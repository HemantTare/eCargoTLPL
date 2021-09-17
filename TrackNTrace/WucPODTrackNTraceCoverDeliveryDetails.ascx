<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODTrackNTraceCoverDeliveryDetails.ascx.cs" Inherits="TrackNTrace_WucPODTrackNTraceCoverDeliveryDetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
       <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
    <tr runat="server" id="tr_att_PODCD_Details" >
        <td colspan="6" >
            <asp:Repeater ID="Rep_PODCDMasterDetails"  runat="server" OnItemDataBound="Rep_PODCDMasterDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                       <tr>
                            <td class="Feild" style="width:25%">
                            <asp:Label ID="lbl_Delivery_No" runat="server" CssClass="LABEL" Text="Delivery No:"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("POD_Delivery_Sheet_No_For_Print")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Delivery_Date" runat="server" CssClass="LABEL" Text="Delivery Date:"></asp:Label>
                            </td>
                                <td class="AlphaValue" style="width:25%"><%#Eval("Cover_Delivery_Date")%>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Delivered_At" runat="server" CssClass="LABEL" Text="Delivered At:"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Delivered_At")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Delivered_By" runat="server" CssClass="LABEL" Text="Delivered By:"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Delivered_By")%></td>
                        </tr>                     
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_CD_Updated_By" runat="server" CssClass="LABEL" Text="Updated By:"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="lnk_CD_Updated_By" Text='<%#Eval("CD_Updated_By")%>' runat="server" ></asp:LinkButton>
                            </td>                 
                                                        
                        </tr>                                   
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr  runat="server" id="tr_GC_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                  <tr><td>&nbsp;</td></tr>
                  <tr>
                      <td class ="HeadingNoBGColor" colspan ="2">COVER DELIVERY DETAILS</td>
                  </tr>  
                  <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                  <tr>
                    <td align="center" colspan ="2" >
                    <asp:UpdatePanel runat="server" ID="upnl_dg_GC" UpdateMode="conditional">
                       <ContentTemplate> 
                         <asp:DataGrid ID="dg_GC_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="dg_GC_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_GC_No" Text='<%#Eval("GC_No_For_Print")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_GC_ID" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="GC Date" DataFormatString ="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Branch" HeaderText="Bkg Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Branch" HeaderText="Dly Branch" DataFormatString ="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Type" HeaderText="Bkg Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>                           
                            </Columns>
                       </asp:DataGrid>  
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_GC_Details" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
            </table>
            </div>
        </td>
    </tr>
</table>