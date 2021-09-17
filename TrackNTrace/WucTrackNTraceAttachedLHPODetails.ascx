<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceAttachedLHPODetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceAttachedLHPODetails" %>

<table width="100%"> 
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
        <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>   
    <tr  runat="server" id="tr_att_LHPO_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_Att_LHPO_Heading"></asp:Label></td>
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
</table>