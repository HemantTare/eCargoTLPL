<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceFinanceDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceFinanceDetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
            <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label>
       </td>
    </tr> 
</table>
<table width="100%" >
    <tr  runat="server" id="tbl_Finance">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                <tr>
                      <td class ="HeadingNoBGColor"  colspan ="2">AUTO VOUCHER DETAILS</td>
                </tr>   
                <tr>
                      <td  colspan ="2">&nbsp;</td>
                </tr>            
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_Finance_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Finance_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Voucher_No" Text='<%#Eval("Voucher_No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Voucher_Date" HeaderText="Voucher Date"/>
                                <asp:BoundColumn DataField="Voucher_Type" HeaderText="Voucher Type"/>
                                <asp:BoundColumn DataField="Ledger_Name" HeaderText="Ledger Name"/>
                                <asp:BoundColumn DataField="Ref_No" HeaderText="Ref. No"/>
                                <asp:BoundColumn DataField="Total_Debit" HeaderText="Total Debit"/>
                                <asp:BoundColumn DataField="Total_Credit" HeaderText="Total Credit"/>
                                <asp:BoundColumn DataField="Narration" HeaderText="Narration"/>
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
    <tr runat="server" id="tbl_ReceiptVoucher">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;" >
                <tr>
                      <td class ="HeadingNoBGColor"  colspan ="2">RECEIPT VOUCHER DETAILS</td>
                </tr>   
                <tr>
                      <td  colspan ="2">&nbsp;</td>
                </tr>            
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_ReceiptVoucher_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_ReceiptVoucher_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Voucher_No" Text='<%#Eval("Voucher_No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Voucher_Date" HeaderText="Voucher Date"/>
                                <asp:BoundColumn DataField="Voucher_Type" HeaderText="Voucher Type"/>
                                <asp:BoundColumn DataField="Ledger_Name" HeaderText="Ledger Name"/>
                                <asp:BoundColumn DataField="Ref_No" HeaderText="Ref. No"/>
                                <asp:BoundColumn DataField="Total_Debit" HeaderText="Total Debit"/>
                                <asp:BoundColumn DataField="Total_Credit" HeaderText="Total Credit"/>
                                <asp:BoundColumn DataField="Narration" HeaderText="Narration"/>
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
    <tr runat="server" id="tbl_ATH_BTH_Voucher">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;" >
                <tr>
                      <td class ="HeadingNoBGColor"  colspan ="2">ATH-BTH VOUCHER DETAILS</td>
                </tr>   
                <tr>
                      <td  colspan ="2">&nbsp;</td>
                </tr>            
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_AthBthVoucher_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_AthBthVoucher_Details_ItemDataBound" >
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Voucher_No" Text='<%#Eval("Voucher_No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Voucher_Date" HeaderText="Voucher Date"/>
                                <asp:BoundColumn DataField="Voucher_Type" HeaderText="Voucher Type"/>
                                <asp:BoundColumn DataField="Ledger_Name" HeaderText="Ledger Name"/>
                                <asp:BoundColumn DataField="Ref_No" HeaderText="Ref. No"/>
                                <asp:BoundColumn DataField="Total_Debit" HeaderText="Total Debit"/>
                                <asp:BoundColumn DataField="Total_Credit" HeaderText="Total Credit"/>
                                <asp:BoundColumn DataField="Narration" HeaderText="Narration"/>
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