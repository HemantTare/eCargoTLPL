<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceBillingDetails.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceBillingDetails" %>

<table width="100%">
    <tr runat="server" id="tr_Error" visible="false">
       <td class ="HeadingNoBGColor" colspan ="2">
        <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found.">
        </asp:Label></td>
    </tr> 
    <tr>
        <td colspan="6" >
            <asp:Repeater ID="Rep_BillDetails"  runat="server" OnItemDataBound="Rep_BillDetails_ItemDataBound">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td class="Feild" style="width:25%">Bill No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Bill No")%></td>
                            <td class="Feild" style="width:25%">
                                <asp:Label ID="lbl_GC_Tot" runat="server" CssClass="LABEL"></asp:Label>
                            </td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total_GC")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Bill Date</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Bill Date")%></td>
                            <td class="Feild" style="width:25%">Total Bill Sub Total</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Bill_Sub_Total")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Bill Type</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Bill_Type")%></td>
                            <td class="Feild" style="width:25%">Total Other Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Bill_Other_Charges_Total")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Billing Client</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Client_Name")%></td>
                            <td class="Feild" style="width:25%">Total Service Tax</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Bill_Service_Tax_Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Billing Hierarchy</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Hierarchy_Name")%></td>
                       
                            <td class="Feild" style="width:25%">Total Octroi Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Bill_Octroi_Amount")%></td>
                        </tr>
                        <tr>
                            <td class="Feild" style="width:25%">Billing Location</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Billing_Location")%></td>
                        
                            <td class="Feild" style="width:25%">Total Oct Form Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total_Oct_Form_Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Bill For</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Bill_For")%></td>
                        
                            <td class="Feild" style="width:25%">Total Oct Service Charges</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Total_Oct_Service_Charges")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Ref. No</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Bill_Ref_No")%></td>
                        
                            <td class="Feild" style="width:25%">Less Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Less_Amount")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Billing Name</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Billing_Name")%></td>
                        
                            <td class="Feild" style="width:25%">Total Bill Amount</td>
                            <td class="NumericValue" style="width:25%"><%#Eval("Bill_Total_Amount")%></td>
                        </tr>
                        <tr>
                           <td class="Feild" style="width:25%">Contact Person</td>
                            <td class="AlphaValue" style="width:25%"><%#Eval("Contact_Person")%></td>
                        
                            <td class="Feild" style="width:25%">Bill Updated By</td>
                            <td class="AlphaValue" style="width:25%">
                                 <asp:LinkButton ID="btn_Bill_updated_by" Text='<%#Eval("Bill_Updated_By")%>' runat="server" ></asp:LinkButton>
                            </td>
                        </tr>                       
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr id="tr_Gc_Details" runat="server">
        <td style="width:100%">
            <div style="width:100%;background-color:#DFE0F8">
                <table style="width:100%;">
                 <tr><td>&nbsp;</td></tr>
                 <tr>
                      <td class ="HeadingNoBGColor" colspan ="2">
                            <asp:Label ID="lbl_GC_Details" runat="server"></asp:Label>
                      </td>
                 </tr>
                 <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                 </tr>
                <tr>
                    <td align="center" colspan ="2" >
                         <asp:DataGrid ID="DG_GC_Details" Width="100%" AutoGenerateColumns="false"  CssClass="Grid" runat="server" OnItemDataBound="DG_GC_Details_ItemDataBound">
                            <AlternatingItemStyle />
                            <HeaderStyle  Font-Bold="true" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_GC_No" Text='<%#Eval("GC_No")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="Bkg Date"/>
                                <asp:BoundColumn DataField="Total_Articles" HeaderText="Articles"/>
                                <asp:BoundColumn DataField="Charged_Weight" HeaderText="Charge Wt"/>
                                <asp:BoundColumn DataField="GC_Sub_Total" HeaderText="Sub Total"/>
                                <asp:TemplateColumn HeaderText="Other Charges">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_OtherCharges" Text='<%#Eval("Other_Charges")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Oct_Form_Charges" HeaderText="Oct Form Charges"/>
                                <asp:BoundColumn DataField="Oct_Service_Charges" HeaderText="Oct Service Charges"/>
                                <asp:BoundColumn DataField="GC_Service_Tax_Amount" HeaderText="Service Tax"/>  
                                <asp:BoundColumn DataField="Octroi_Amount" HeaderText="Oct Amt"/>
                                <asp:BoundColumn DataField="Total_Amount" HeaderText="Total Amt"/>                               
                           </Columns>
                       </asp:DataGrid>
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
</table>