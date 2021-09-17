<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceCrossingDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceCrossingDetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
            <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label>
       </td>
    </tr> 
</table>
<table width="100%" runat="server" id="tbl_Crosssing">
    <tr>
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr>
                      <td class ="HeadingNoBGColor"  colspan ="2">CROSSING DETAILS</td>
                 </tr>                 
                 <tr>
                       <td align="center" style="width:50%" class ="HeadingNoBGColor">
                            <asp:Label ID="lbl_OutwardText" runat="server"/>
                       </td>
                       <td align="center" style="width:50%" class ="HeadingNoBGColor">
                            <asp:Label ID="lbl_InwardText" runat="server"/>
                       </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lbl_Error_GC_Crossing" Font-Bold="true"  CssClass="LABEL" runat="server" ForeColor="Red" ></asp:Label>
                    </td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_Crossing_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Crossing_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Manifest No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Memo_No" Text='<%#Eval("Manifest No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Memo Date" HeaderText="Manifest Date"/>
                                <asp:BoundColumn DataField="Memo Type" HeaderText="Manifest Type"/>
                                <asp:BoundColumn DataField="Manifest From" HeaderText="Manifest From"/>
                                <asp:BoundColumn DataField="Manifest To" HeaderText="Manifest To"/>
                                <asp:BoundColumn DataField="Loaded Articles" HeaderText="Load Art"/>
                                <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"/>
                                <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_MemoUpdated" Text='<%#Eval("MEMO Updated By")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_LHPO_No" Text='<%#Eval("LHPO No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="AUS/DDC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_AUS_No" Text='<%#Eval("AUS/DDC No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                               
                                <asp:BoundColumn DataField="Unloaded/Deliverd At" HeaderText="Unlo/Del At"/>
                                <asp:BoundColumn DataField="AUS/DDC Date" HeaderText="AUS/DDC Date"/>
                                <asp:BoundColumn DataField="Recived/Deliverd Articles" HeaderText="Rec/Del Art"/>
                                <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_AusUpdated" Text='<%#Eval("AUS/DDC Updated By")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Memo Type ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_Memo_Type_Id" Value='<%#Eval("Memo Type Id")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_DelType_Id" Value='<%#Eval("DDC Type ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_Memo_Id" Value='<%#Eval("Memo_ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_LHPO_Id" Value='<%#Eval("LHPO_ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_AUS_DDC_Id" Value='<%#Eval("AUS_DDC_Id")%>' runat="server"></asp:HiddenField>
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
    <tr runat="server" id="tr_stock_transfer">
         <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">                   
                    <tr>
                          <td class ="HeadingNoBGColor"  colspan ="2">STOCK TRANSFER</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan ="2" >   
                             <asp:DataGrid ID="dg_stock_transfer" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="dg_stock_transfer_ItemDataBound">
                                <AlternatingItemStyle />
                                <HeaderStyle  Font-Bold="true" HorizontalAlign="Left"/>
                                <ItemStyle  HorizontalAlign="Left"/>
                                <Columns>
                                    <asp:BoundColumn DataField="Transaction_Date" HeaderText="Transaction Date"/>
                                    <asp:BoundColumn DataField="Old_Current_Branch" HeaderText="Old Current Branch"/>
                                    <asp:BoundColumn DataField="New_Current_Branch" HeaderText="New Current Branch"/>
                                    <asp:TemplateColumn HeaderText="Updated By">
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
    <tr runat="server" id="tr_del_branch_update">
         <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">                   
                    <tr>
                          <td class ="HeadingNoBGColor"  colspan ="2">DELIVERY BRANCH UPDATE</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan ="2" >   
                             <asp:DataGrid ID="dg_deliveryBranchUpdate" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="dg_deliveryBranchUpdate_ItemDataBound">
                                <AlternatingItemStyle />
                                <HeaderStyle  Font-Bold="true" HorizontalAlign="Left"/>
                                <ItemStyle  HorizontalAlign="Left"/>
                                <Columns>
                                    <asp:BoundColumn DataField="Transaction_Date" HeaderText="Transaction Date"/>
                                    <asp:BoundColumn DataField="Old_Delivery_Branch" HeaderText="Old Dly Branch"/>
                                    <asp:BoundColumn DataField="New_Delivery_Branch" HeaderText="New Dly Branch"/>
                                    <asp:BoundColumn DataField="Old_Location_Name" HeaderText="Old Dly Location"/>
                                    <asp:BoundColumn DataField="New_Location_Name" HeaderText="New Dly Location"/>

                                    <asp:TemplateColumn HeaderText="Updated By">
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
