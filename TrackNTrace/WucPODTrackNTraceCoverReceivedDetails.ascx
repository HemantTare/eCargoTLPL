<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODTrackNTraceCoverReceivedDetails.ascx.cs" Inherits="TrackNTrace_WucPODTrackNTraceCoverReceivedDetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
       <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
    <tr id="tr_att_PODCR_Details" runat="server">
        <td colspan="6" >
            <asp:Repeater ID="Rep_PODCRMasterDetails"  runat="server" OnItemDataBound="Rep_PODCRMasterDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">                    
                        <tr>
                            <td class="Feild" style="width:25%">
                            <asp:Label ID="lbl_Receipt_No" runat="server" CssClass="LABEL" Text="Receipt No"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Cover_Received_No_For_Print")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Cover_No" runat="server" CssClass="LABEL" Text="Cover No"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="lnk_CG_No" Text='<%#Eval("Cover_No_For_Print")%>' runat="server" ></asp:LinkButton>
                                 <asp:HiddenField ID="hdn_Cover_Id" Value='<%#Eval("Cover_Id")%>' runat="server" ></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="Label1" runat="server" CssClass="LABEL" Text="Receipt Date"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Cover_Received_Date")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Cover_Date" runat="server" CssClass="LABEL" Text="Cover Date"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Cover_Date")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Cover_Recvd_Hierarchy" runat="server" CssClass="LABEL" Text="Received By"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Received_At")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Cover_Sent_Hierarchy" runat="server" CssClass="LABEL" Text="Made By"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Despatch_From")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_CR_Updated_By" runat="server" CssClass="LABEL" Text="Updated By"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="lnk_CR_Updated_By" Text='<%#Eval("CR_Updated_By")%>' runat="server" ></asp:LinkButton>
                            </td>  
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Sent_By" runat="server" CssClass="LABEL" Text="Sent By"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Sent_By")%></td> 
                                                        
                        </tr>
                        <tr>
                           <td style="width:50%" colspan="2"></td>                         
                           <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_CG_Updated_By" runat="server" CssClass="LABEL" Text="Updated By"></asp:Label>
                           </td>
                           <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="lnk_CG_Updated_By" Text='<%#Eval("CG_Updated_By")%>' runat="server" ></asp:LinkButton>
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
                      <td class ="HeadingNoBGColor" colspan ="2">COVER RECEIVED DETAILS</td>
                 </tr>
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                    <asp:UpdatePanel runat="server" ID="upnl_dg_GC" UpdateMode="conditional">
                       <ContentTemplate> 
                         <asp:DataGrid ID="dg_GC_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" 
                         runat="server" OnItemDataBound="dg_GC_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_GC_No" Text='<%#Eval("GC_No_For_Print")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
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