<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceCreditMemo.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceCreditMemo" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2"><asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
            <asp:Repeater ID="Rep_CreditMemo"  runat="server" OnItemDataBound="Rep_CreditMemo_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">Credit Memo No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Credit_Memo_No")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_Tot_Amt" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total GC Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Credit Memo Date</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Credit_Memo_Date")%></td>
                             <td class="Feild" style="width:25%">Service Tax Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Service Tax Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Credit Memo For</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Credit_Memo_For")%></td>
                            <td class="Feild" style="width:25%">Total Credit Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total Credit Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Credit Memo Branch</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Credit_Memo_Branch")%></td>
                       
                             <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%"></td>
                        </tr>   
                         <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_No" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="lbtn_GC_No" Font-Bold="true" runat="server" Text='<%#Eval("GC No")%>'></asp:LinkButton>
                            </td>                       
                            <td class="Feild" style="width:25%"></td>
                            <td class="AlphaValue" style="width:25%"></td>
                        </tr>                      
                        <tr>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_Date" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("GC Date")%></td>
                        
                            <td class="Feild" style="width:25%">Octroi Form Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Octroi Form Charges")%></td>
                        </tr>                    
                        <tr>
                            <td class="Feild" style="width:25%">Consignee</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Consignee")%></td>
                        
                            <td class="Feild" style="width:25%">Octroi Service Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Octroi Service Charges")%></td>
                        </tr>                       
                        <tr>
                           <td class="Feild" style="width:25%">Consignor</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Consignor")%></td>
                        
                            <td class="Feild" style="width:25%">GI Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("GI Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Tax Payable By</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Tax Payable By")%></td>
                        
                            <td class="Feild" style="width:25%">Detention Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Detention Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Booking Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Booking Type")%></td>
                        
                            <td class="Feild" style="width:25%">Hamali Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Hamali Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Payment Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Payment Type")%></td>
                        
                            <td class="Feild" style="width:25%">Local Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Local Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Demurage Days</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Demurage Days")%></td>
                        
                            <td class="Feild" style="width:25%">Demurage Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Demurage Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Addi Chrg Remarks</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Addi Chrg Remarks")%></td>
                        
                            <td class="Feild" style="width:25%">Additional Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Additional Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Disc Amt Remarks</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Disc Amt Remarks")%></td>
                        
                            <td class="Feild" style="width:25%">Discount Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Discount Amount")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Amount Taxable</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Amount Taxable")%></td>
                        
                            <td class="Feild" style="width:25%">Service Tax Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Service Tax Amount")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Credit Memo Tax Payable By</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Credit Tax Payable By")%></td>
                        
                            <td class="Feild" style="width:25%">Rebooked Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Rebooked Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Debit To Ledger</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Ledger_Name")%></td>
                        
                            <td class="Feild" style="width:25%">Billing Branch</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Branch_Name")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Updated By</td>
                            <td class="AlphaValue" style="width:25%">
                                <asp:LinkButton ID="btn_updated_by" Text='<%#Eval("Updated_By")%>' runat="server" ></asp:LinkButton>
                            </td>
                        
                            <td class="Feild" style="width:25%"></td>
                            <td class="NumericValue" style="width:25%"></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>   
</table>