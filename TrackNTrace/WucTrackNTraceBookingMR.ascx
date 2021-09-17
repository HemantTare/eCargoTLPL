<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceBookingMR.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceBookingMR" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
        <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found.">
        </asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
            <asp:Repeater ID="Rep_BookingMR"  runat="server" OnItemDataBound="Rep_BookingMR_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">MR No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("MR No")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_Tot_Amt" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total GC Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">MR Date</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("MR Date")%></td>
                             <td class="Feild" style="width:25%">Service Tax Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Service Tax Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">MR Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("MR Type")%></td>
                            <td class="Feild" style="width:25%">Total MR Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total MR Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">MR Branch</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("MR Branch")%></td>
                       
                            <td class="Feild" style="width:25%">Cash Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Cash Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_No" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="lbtn_GC_No" Font-Bold="true" runat="server" Text='<%#Eval("GC No")%>'></asp:LinkButton>
                            </td>
                       
                            <td class="Feild" style="width:25%">Cheque Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Cheque Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_Date" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("GC Date")%></td>
                        
                            <td class="Feild" style="width:25%">Tax Payable By</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Tax Payable By")%></td>
                        </tr>                    
                        <tr>
                            <td class="Feild" style="width:25%">Consignee</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Consignee")%></td>
                        
                            <td class="Feild" style="width:25%">Booking Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Booking Type")%></td>
                        </tr>                       
                        <tr>
                           <td class="Feild" style="width:25%">Consignor</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Consignor")%></td>
                        
                            <td class="Feild" style="width:25%">Payment Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Payment Type")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">MR Updated By</td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="btn_MR_updated_by" Text='<%#Eval("MR Updated By")%>' runat="server" ></asp:LinkButton>
                                 <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server" ></asp:HiddenField>
                            </td>                        
                            <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%"></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr runat="server" id="tr_cheque_details">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2">CHEQUE DETAILS</td>
                 </tr>
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_Cheque_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server">
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:BoundColumn DataField="Cheque Bank" HeaderText="Cheque Bank"/>
                                <asp:BoundColumn DataField="Cheque Branch" HeaderText="Cheque Branch"/>
                                <asp:BoundColumn DataField="Cheque No" HeaderText="Cheque No"/>
                                <asp:BoundColumn DataField="Cheque Amount" HeaderText="Cheque Amount"/>
                                <asp:BoundColumn DataField="Deposit In" HeaderText="Deposit In"/>                                
                           </Columns>
                       </asp:DataGrid>
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
</table>