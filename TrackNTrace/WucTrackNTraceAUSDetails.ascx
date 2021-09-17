<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceAUSDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceAUSDetails" %>


 
<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
       <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
            <asp:Repeater ID="Rep_AUSDetails"  runat="server" OnItemDataBound="Rep_AUSDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%"><%#Eval("AUS_No_Text")%></td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("AUS No")%></td>
                            
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Tot_GC" runat="server" CssClass="LABEL"></asp:Label>
                                <asp:HiddenField ID="hdn_LHPO_Id" Value='<%#Eval("LHPO_Id")%>' runat="server"></asp:HiddenField>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Actual GCs")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%"><%#Eval("AUS_Date_Text")%></td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("AUS Date")%></td>
                            
                            <td class="Feild" style="width:25%">Total Actual Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Actual Articles")%></td>
                        </tr>                       
                        <tr>
                            <td class="Feild" style="width:25%"><%#Eval("AUS_Branch_Text")%></td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("AUS Branch")%></td>
                       
                            <td class="Feild" style="width:25%">Total Actual Weight</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Actual Weight")%></td>
                        </tr>                       
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle No")%></td>
                        
                            <td class="Feild" style="width:25%">Total Received Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Received Articles")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle Capacity</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle Capacity")%></td>
                        
                            <td class="Feild" style="width:25%">Total Received Weight</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Received Weight")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle Type")%></td>
                        
                            <td class="Feild" style="width:25%">Total Loaded Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Loaded Articles")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_No" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="lbtn_LHPO_No" runat="server" Text='<%#Eval("LHPO No")%>'></asp:LinkButton>
                            </td>
                        
                            <td class="Feild" style="width:25%">Total Short Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Short Articles")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_Date" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO Date")%></td>
                        
                            <td class="Feild" style="width:25%">Total Excess Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Excess Articles")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Scheduled Arrival Date & Time</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Scheduled Arrival Date")%> &nbsp; <%#Eval("Scheduled Arrival Time")%></td>
                        
                            <td class="Feild" style="width:25%">Total Damaged Leakage Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Damaged Leakage Articles")%></td>
                        </tr>                        
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle Arrival Date & Time</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle Arrival Date")%> &nbsp; <%#Eval("Vehicle Arrival Time")%></td>
                            
                            <td class="Feild" style="width:25%">Total Damaged Leakage Value</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Damaged Leakage Value")%></td>                            
                        </tr>                       
                        <tr>
                            <td class="Feild" style="width:25%">Truck Unloaded Time</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Truck Unloaded Time")%></td>
                            
                            <td class="Feild" style="width:25%">Total Delivery Commision</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Delivery Commision")%></td>                            
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">UnLoading Supervisior</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("UnLoading Supervisior")%></td>
                        
                            <td class="Feild" style="width:25%">Total To Pay Collection</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total To Pay Collection")%></td>
                        </tr>     
                         <tr>
                           <td class="Feild" style="width:25%"><%#Eval("AUS_UpdatedBy_Text")%></td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="btn_AUS_updated_by" Text='<%#Eval("AUS Updated By")%>' runat="server" ></asp:LinkButton>
                            </td>
                        
                            <td class="Feild" style="width:25%"><%#Eval("Godown Text")%></td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Godown_Name")%></td>
                        </tr>                       
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr runat="server" id="tr_Aus_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_AUS_Heading"></asp:Label></td>
                 </tr>               
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                       <asp:DataGrid ID="DG_GC_AUS_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_AUS_Details_ItemDataBound" >
                           <HeaderStyle  Font-Bold="True" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Manifest No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Memo_No" Text='<%#Eval("Memo No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_GC_No" Text='<%#Eval("GC No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Memo From" HeaderText="Manifest From"/>
                                <asp:BoundColumn DataField="Memo Date" HeaderText="Manifest Date"/>
                                <asp:BoundColumn DataField="Loaded Articles" HeaderText="Loaded Articles"/>
                                <asp:BoundColumn DataField="Received Articles" HeaderText="Received Articles"/>
                                <asp:BoundColumn DataField="Damaged Articles" HeaderText="Damaged Articles"/>
                                <asp:TemplateColumn HeaderText="Manifest Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_MEMO_UpdatedBy" Text='<%#Eval("MEMO Updated By")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_Memo_Id" Value='<%#Eval("Memo_Id")%>' runat="server"></asp:HiddenField>
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