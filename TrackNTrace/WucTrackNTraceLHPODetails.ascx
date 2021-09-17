<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceLHPODetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceLHPODetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR"  Text="No Data Found."></asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
            <asp:Repeater ID="Rep_LHPODetails"  runat="server" OnItemDataBound="Rep_LHPODetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_No" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO No")%></td>
                            <td class="Feild" style="width:25%">Truck Hire Charge</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Truck_Hire_Charge")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_Date" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO Date")%></td>
                            <td class="Feild" style="width:25%">Other Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Other_Charges")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_Type" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO Type")%></td>
                            <td class="Feild" style="width:25%">Loading Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Loading_Charges")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_Branch" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO Branch")%></td>
                       
                            <td class="Feild" style="width:25%">TDS Percent</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("TDS_Percent")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_From" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO From")%></td>
                       
                            <td class="Feild" style="width:25%">TDS Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("TDS_Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_To" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO To")%></td>
                       
                            <td class="Feild" style="width:25%">Total Truck Hire Payable</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total_Truck_Hire_Payable")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle No")%></td>
                        
                            <td class="Feild" style="width:25%">Total Advance To Be Paid</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total_Advance_To_Be_Paid")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle Capacity</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle Capacity")%></td>
                        
                            <td class="Feild" style="width:25%">Balance Payble Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Balance_Payble_Amount")%></td>
                        </tr>                                         
                        <tr>
                            <td class="Feild" style="width:25%">Actual Departure Time</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Departure Time")%></td>
                        
                            <td class="Feild" style="width:25%">Total No Of Manifest</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total No Of Memo")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Loading Supervisior</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Loading Supervisior")%></td>
                        
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total No Of GC")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Driver 1</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Driver1")%></td>
                            <td class="Feild" style="width:25%">Total Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Articles")%></td>                            
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Driver 2</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Driver2")%></td>
                            <td class="Feild" style="width:25%">Total Weight</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Weight")%></td>                            
                        </tr>
                         <tr>
                            <td class="Feild" style="width:25%">Cleaner</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Cleaner")%></td>
                            <td class="Feild" style="width:25%">Balance Payable At</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Balance payable at")%></td>                            
                        </tr>
                         <tr>
                            <td class="Feild" style="width:25%">Broker</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vendor")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_UpdatedBy" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="btn_LHPO_Updated_by" Text='<%#Eval("LHPO Updated By")%>' runat="server" ></asp:LinkButton>
                            </td>                            
                        </tr>
                         <tr>
                            <td class="Feild" style="width:25%">Owner</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Owner Name")%></td>
                            <td class="Feild" style="width:25%">Owner Pan No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Owner Pan No")%></td>  
                        </tr>     
                         <tr>
                            <td class="Feild" style="width:25%">Is ATH Prepared</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("ISATHPREPARED")%></td>
                            <td class="Feild" style="width:25%">Is BTH Prepared</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("ISBTHPREPARED")%></td>  
                        </tr>                         
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr  runat="server" id="tr_LHPO_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_LHPO_Heading"></asp:Label></td>
                 </tr>               
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                    <asp:UpdatePanel runat="server" ID="upnl_dg_lhpo" UpdateMode="conditional">
                       <ContentTemplate> 
                         <asp:DataGrid ID="DG_GC_LHPO_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_LHPO_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Manifest No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Memo_No" Text='<%#Eval("Memo No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Memo Type" HeaderText="Manifest Type"/>
                                <asp:BoundColumn DataField="Loaded Articles" HeaderText="Loaded Art"/>
                                <asp:BoundColumn DataField="Loaded Weight" HeaderText="Loaded Wt"/>
                                <asp:TemplateColumn HeaderText="AUS/DDC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_AUS_DDC_No" Text='<%#Eval("AUS/DDC No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Unloaded/Deliverd At" HeaderText="Unloaded/Del At"/>
                                
                                <asp:BoundColumn DataField="AUS/DDC Date" HeaderText="AUS/DDC Date"/>
                                <asp:BoundColumn DataField="Received/Deliverd Articles" HeaderText="Rec/Del Art"/>                                
                                <asp:TemplateColumn HeaderText="Manifest Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Memo_UpdatedBy" Text='<%#Eval("Memo Updated By")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_Memo_Type_Id" Value='<%#Eval("Memo Type Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_DelType_Id" Value='<%#Eval("DDC Type ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_Memo_Id" Value='<%#Eval("Memo_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_AUSDDC_Id" Value='<%#Eval("AUS_DDC_Id")%>' runat="server"></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                             
                            </Columns>
                       </asp:DataGrid>  
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DG_GC_LHPO_Details" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
            </table>
            </div>
        </td>
    </tr>
    
    <tr  runat="server" id="tr_ATH_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_ATH_Heading"></asp:Label></td>
                 </tr>               
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                    <asp:UpdatePanel runat="server" ID="upnl_dg_ATH" UpdateMode="conditional">
                       <ContentTemplate> 
                         <asp:DataGrid ID="DG_GC_ATH_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_ATH_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:BoundColumn DataField="ATH Voucher No" HeaderText="ATH No"/>
                                <asp:BoundColumn DataField="ATH Date" HeaderText="ATH Date"/>
                                <asp:BoundColumn DataField="ATH Location" HeaderText="ATH Location"/>
                                <asp:BoundColumn DataField="Advance Payable Amount" HeaderText="Advance Payable Amount"/>
                                <asp:BoundColumn DataField="Total Paid Amount" HeaderText="Total Paid Amount"/>
                                <asp:BoundColumn DataField="Cash Amount" HeaderText="Cash Amount"/>
                                <asp:BoundColumn DataField="Cheque Amount" HeaderText="Cheque Amount"/>                            
                                <asp:BoundColumn DataField="Is_ATH_OnAccount" HeaderText="Is ATH OnAccount"/>
                                <asp:TemplateColumn HeaderText="ATH Updated By">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="ATH_UpdatedBy_id" Text='<%#Eval("ATH_UpdatedBy_id")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateColumn>   
                            </Columns>
                       </asp:DataGrid>  
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
            </table>
            </div>
        </td>
    </tr>
</table>