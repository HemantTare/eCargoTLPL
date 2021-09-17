<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODTrackNTraceMovementDetails.ascx.cs" Inherits="TrackNTrace_WucPODTrackNTraceMovementDetails" %>

<table width="100%" runat="server">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
       <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
    <tr id="tr_att_PODMovement_Details" runat="server">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
 
                <table width="100%">               
                <tr class="TopHeading">
                    <td align="center">
                        Current Status :&nbsp; <asp:Label ID="lbl_Current_Status" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        Current Location :&nbsp; <asp:Label ID="lbl_Current_Location" runat="server"></asp:Label>
                    </td>                    
                </tr>
                <tr>
                      <td class ="HeadingNoBGColor"  colspan ="2">POD DETAILS</td>
                </tr> 
                <tr>
                    <td class ="HeadingNoBGColor"  colspan ="2" align="center"></td>
                 </tr> 
                <tr>
                    <td align="center" style="width:50%" class ="HeadingNoBGColor">COVER GENERATION</td>
                    <td align="center" style="width:50%" class ="HeadingNoBGColor">COVER RECEIVED</td>                      
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td> 
                </tr> 
                <tr>
                    <td colspan="6" >
                         <asp:DataGrid  ID="dg_PODTrackNTraceMovementsDetails" runat="server" CssClass="Grid" 
                            AutoGenerateColumns="False" Width="100%" OnItemDataBound="dg_PODTrackNTraceMovementsDetails_ItemDataBound">
                                                      
                            <HeaderStyle  Font-Bold="true" />
                            
                            <Columns>
                                <asp:TemplateColumn HeaderText="Cover No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Cover_Gen_No" Text='<%#Eval("Cover_No_For_Print")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Cover_Date" HeaderText="Cover Date"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Sent_By" HeaderText="Sent By"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Despatch_From" HeaderText="Despatch From"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_CG_Updated_By" Text='<%#Eval("CG_Updated_By")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>  
                                <asp:TemplateColumn HeaderText="Received No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Cover_Recvd_No" Text='<%#Eval("Cover_Received_No_For_Print")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Cover_Received_Date" HeaderText="Received Date"></asp:BoundColumn>                                         
                                <asp:BoundColumn DataField="Received_At" HeaderText="Received At"></asp:BoundColumn>
                                 <asp:TemplateColumn HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_CR_Updated_By" Text='<%#Eval("CR_Updated_By")%>' runat="server"></asp:LinkButton>
                                        <asp:HiddenField ID="hdn_Cover_Id" Value='<%#Eval("Cover_ID")%>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_Recieved_Id" Value='<%#Eval("Cover_Received_ID")%>' runat="server"></asp:HiddenField>
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