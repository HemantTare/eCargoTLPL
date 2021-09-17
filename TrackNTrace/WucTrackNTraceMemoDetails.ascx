<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceMemoDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceMemoDetails" %>

<script type="text/javascript">
    
    function Open_Print_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;              
     
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }    

</script>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
        
        <asp:LinkButton ID="btn_Print_Memo" runat="server" Text="Print" ForeColor="DarkBlue"></asp:LinkButton>
        
          <asp:UpdatePanel runat="server" ID="upnl_repeter_memo" UpdateMode="conditional">
            <ContentTemplate>  
            <asp:Repeater ID="Rep_MemoDetails"  runat="server" OnItemDataBound="Rep_MemoDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">Manifest No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Memo No")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL"></asp:Label>
                                <asp:HiddenField ID="hdn_LHPOId" Value='<%#Eval("LHPO_ID")%>' runat="server"></asp:HiddenField>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total GC")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Manifest Date</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Memo Date")%></td>
                            <td class="Feild" style="width:25%">Total Loaded Articles</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Tot Load Art")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Manifest Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Memo Type")%></td>
                            <td class="Feild" style="width:25%">Total Loaded Weight</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Tot Load Wt")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Manifest From</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Memo From")%></td>
                       
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Manifest To</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Memo To")%></td>
                       
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>                        
                        <tr>
                            <td class="Feild" style="width:25%">Vehicle No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Vehicle No")%></td>
                        
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Scheduled Arrival Date & Time</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Arrival Date")%> &nbsp;<%#Eval("Arrival Time")%></td>
                        
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>                       
                        <tr>
                           <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_No" runat="server" CssClass="LABEL"></asp:Label>
                           </td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="lbtn_LHPO_No" runat="server" Text='<%#Eval("LHPO No")%>'></asp:LinkButton>
                            </td>
                        
                            <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%"></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_LHPO_Date" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("LHPO Date")%></td>
                            <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%">
                            </td>                            
                        </tr>
                         <tr>
                            <td class="Feild" style="width:25%">Manifest Updated By</td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="btn_Memo_updated_by" Text='<%#Eval("Memo Updated By")%>' runat="server" ></asp:LinkButton>
                            </td>
                            <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%"></td>                            
                        </tr>
                       
                    </table>
                </ItemTemplate>
            </asp:Repeater>
             </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Rep_MemoDetails" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr runat="server" id="tr_Memo_Details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2">MANIFEST DETAILS</td>
                 </tr>               
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                     <asp:UpdatePanel runat="server" ID="upnl_dg_memo" UpdateMode="conditional">
                       <ContentTemplate>  
                             <asp:DataGrid ID="DG_GC_Memo_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Memo_Details_ItemDataBound" >
                                <AlternatingItemStyle />
                                <HeaderStyle  Font-Bold="true" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_GC_No" Text='<%#Eval("GC No")%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
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
                                    <asp:TemplateColumn HeaderText="Updated By">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_GC_UpdatedBy" Text='<%#Eval("GC Updated By")%>' runat="server"></asp:LinkButton>
                                            <asp:HiddenField ID="hdn_Memo_Type_Id" Value='<%#Eval("Memo Type Id")%>' runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_DelType_Id" Value='<%#Eval("DDC Type ID")%>' runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_GC_ID" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_AUS_DDC_ID" Value='<%#Eval("AUS_DDC_ID")%>' runat="server"></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                            
                                </Columns>
                             </asp:DataGrid>
                         </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DG_GC_Memo_Details" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
            </table>
            </div>
        </td>
    </tr>
</table>