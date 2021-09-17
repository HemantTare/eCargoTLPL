<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceDeliveryDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceDeliveryDetails" %>

<table width="100%" class="TABLE">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr> 
    <tr runat="server" id="tr_RepDelDetals">
        <td colspan="6" >
            <asp:Repeater ID="Rep_DeliveryDetails"  runat="server" OnItemDataBound="Rep_DeliveryDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">Delivery No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("DDC No")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Tot_GC" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total No Of GC")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Delivery Date</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("DDC Date")%></td>
                             <td class="Feild" style="width:25%">Total Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Articles")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Delivery Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("DDC Type")%></td>
                            <td class="Feild" style="width:25%">Total Weight</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Weight")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Delivery Branch</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("DDC Branch")%></td>
                       
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>
                        
                        <tr>
                            <td class="Feild" style="width:25%">Delivered To</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Delivered_To")%></td>
                       
                            <td class="Feild" style="width:25%">Mobile No.</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Delivered_to_Mobile")%></td>
                        </tr>

                        <tr>
                            <td class="Feild" style="width:25%">PhotoID Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("PhotoID_Type")%></td>
                       
                            <td class="Feild" style="width:25%">PhotoID No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("PhotoID_No")%></td>
                        </tr>
                        
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle_Type")%></td>
                       
                            <td class="Feild" style="width:25%">Vehicle No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle_No")%></td>
                        </tr>
                        
                                                                        
                        <tr>
                            <td class="Feild" style="width:25%">Supervisor Name</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Supervisor Name")%></td>
                       
                            <td class="Feild" style="width:25%">Updated By</td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="btn_DDC_UpdatedBy" Text='<%#Eval("Updated By")%>' runat="server"></asp:LinkButton>
                            </td>
                        </tr>                      
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr  runat="server" id="tr_DELIVERY_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">                
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2">DELIVERY DETAILS</td>
                 </tr>               
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_Delivery_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Delivery_Details_ItemDataBound">
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Delivery No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_DDC_No" Text='<%#Eval("DDC No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_GC_No" Text='<%#Eval("GC No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Manifest No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Memo_No" Text='<%#Eval("MEMO No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LHPO No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_LHPO_No" Text='<%#Eval("LHPO No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="AUS No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_AUS_No" Text='<%#Eval("AUS No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DDC Date" HeaderText="Delivery Date"/>
                                <asp:BoundColumn DataField="DDC Time" HeaderText="Delivery Time"/>
                                <asp:BoundColumn DataField="DDC Type" HeaderText="Delivery Type"/>                               
                                <asp:BoundColumn DataField="DDC Branch" HeaderText="Delivery Branch"/>
                                <asp:BoundColumn DataField="Delivered Articles" HeaderText="Delivered Art"/>
                                <asp:BoundColumn DataField="Delivered Weight" HeaderText="Delivered Wt"/>                                
                                <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_UpdatedBy" Text='<%#Eval("Updated By")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="hdn_DelType_Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_DelType_Id" Value='<%#Eval("DDC Type ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_Memo_Id" Value='<%#Eval("MEMO_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_LHPO_Id" Value='<%#Eval("LHPO_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_AUS_Id" Value='<%#Eval("AUS_Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_DDC_Id" Value='<%#Eval("DDC_ID")%>' runat="server"></asp:HiddenField>
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
    <tr runat="server" id="tr_DELIVERY_MR">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;" >                 
                 <tr>
                      <td class ="Heading" colspan ="2">DELIVERY MR DETAILS</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                    
                         <asp:DataGrid ID="DG_GC_MR_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_MR_Details_ItemDataBound">
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="MR No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_MR_No" Text='<%#Eval("MR No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="MR Date" HeaderText="MR Date"/>
                                <asp:BoundColumn DataField="MR Branch" HeaderText="MR Branch"/>                              
                                <asp:BoundColumn DataField="MR Amount" HeaderText="MR Amount"/>
                                <asp:TemplateColumn HeaderText="MR Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_MR_UpdatedBy" Text='<%#Eval("MR Updated By")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_MR_Id" Value='<%#Eval("MR_Id")%>' runat="server"></asp:HiddenField>
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
     <tr runat="server" id="tr_Credit_Memo">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;" >                 
                 <tr>
                      <td class ="Heading" colspan ="2">CREDIT MEMO DETAILS</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                    
                         <asp:DataGrid ID="DG_Credit_Memo_Details" Width="100%" AutoGenerateColumns="false"  
                         CssClass="Grid" runat="server" OnItemDataBound="DG_Credit_Memo_Details_ItemDataBound">
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Credit Memo No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Credit_Memo_No" Text='<%#Eval("Credit_Memo_No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <%--<asp:BoundColumn DataField="Credit_Memo_No" HeaderText="Credit Memo No"/>--%>
                                <asp:BoundColumn DataField="Credit_Memo_Date" HeaderText="Credit Memo Date"/>
                                <asp:BoundColumn DataField="Credit_Memo_Branch" HeaderText="Credit Memo Branch"/> 
                                <asp:BoundColumn DataField="Credit_Memo_For" HeaderText="Credit Memo For"/>                             
                                <asp:BoundColumn DataField="Total_MR_Amount" HeaderText="Credit Memo Amount"/>
<%--                                <asp:BoundColumn DataField="Updated_By" HeaderText="Updated By"/>
--%>                                <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_UpdatedBy" Text='<%#Eval("Updated_By")%>' runat="server"></asp:LinkButton>
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