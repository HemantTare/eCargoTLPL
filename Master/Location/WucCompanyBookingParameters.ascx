<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyBookingParameters.ascx.cs" Inherits="Master_Location_WucCompanyBookingParameters" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript"   src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript">
   function EnabeledDisabledRecoveryLedgers()
   {
        var chk_Debittodelivery=document.getElementById('<%=chk_Debittodelivery.ClientID%>');
        var tr_PayforDeliveryBranch=document.getElementById('<%=tr_PayforDeliveryBranch.ClientID%>');
        var tr_PayforBookigBranch=document.getElementById('<%=tr_PayforBookigBranch.ClientID%>');        
        var tr_PayforCrossingBranch=document.getElementById('<%=tr_PayforCrossingBranch.ClientID%>');
        var tr_Delivery_Commision_Income_AC=document.getElementById('<%=tr_Delivery_Commision_Income_AC.ClientID%>');
        var tr_Delivery_Commision_Expense_AC=document.getElementById('<%=tr_Delivery_Commision_Expense_AC.ClientID%>');
        var tr_LHPO_Other_Charges_Expense_AC=document.getElementById('<%=tr_LHPO_Other_Charges_Expense_AC.ClientID%>');
        var tr_LHPO_Other_Charges_Payble_AC=document.getElementById('<%=tr_LHPO_Other_Charges_Payble_AC.ClientID%>');
        var tr_Lorry_Payble_ATH_BTH_AC=document.getElementById('<%=tr_Lorry_Payble_ATH_BTH_AC.ClientID%>');
        var tr_UpcountryCostAC=document.getElementById('<%=tr_UpcountryCostAC.ClientID%>');
            
        tr_PayforDeliveryBranch.style.display="";
        tr_PayforBookigBranch.style.display="";
        tr_PayforCrossingBranch.style.display="";
        tr_Delivery_Commision_Income_AC.style.display="";
        tr_Delivery_Commision_Expense_AC.style.display="";
        tr_LHPO_Other_Charges_Expense_AC.style.display="";
        tr_LHPO_Other_Charges_Payble_AC.style.display="";
        tr_Lorry_Payble_ATH_BTH_AC.style.display="";
        tr_UpcountryCostAC.style.display="";
        if (chk_Debittodelivery.checked==true)
        {                  
            tr_PayforDeliveryBranch.style.display="none";
            tr_PayforBookigBranch.style.display="none";
            tr_PayforCrossingBranch.style.display="none";
            tr_Delivery_Commision_Income_AC.style.display="none";
            tr_Delivery_Commision_Expense_AC.style.display="none";
            tr_LHPO_Other_Charges_Expense_AC.style.display="none";
            tr_LHPO_Other_Charges_Payble_AC.style.display="none";
            tr_Lorry_Payble_ATH_BTH_AC.style.display="none";
            tr_UpcountryCostAC.style.display="none";
        }   
        
   }
</script>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_Debittodelivery" runat="server" Text="Is Debit To Delivery Branch For To Pay GC Booking:"></asp:Label></td>
        <td style="width: 25%">
            <asp:CheckBox ID="chk_Debittodelivery" onclick="EnabeledDisabledRecoveryLedgers()" runat="server" /></td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr runat="server" id="tr_PayforBookigBranch">
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_toPayforBookigBranch" runat="server" Text="To Pay Recovery Ledger For Booking Branch:"></asp:Label></td>
        <td style="width: 25%">
            <cc1:DDLSearch ID="ddl_toPayforBookigBranch" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
            <asp:Label ID="Label3" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label></td>
    </tr>
    <tr runat="server" id="tr_PayforCrossingBranch">
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_toPayforCrossingBranch" runat="server" Text="To Pay Recovery Ledger For Crossing  Branch:"></asp:Label></td>
        <td style="width: 25%">
            <cc1:DDLSearch ID="ddl_toPayforCrossingBranch" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
            <asp:Label ID="Label2" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label></td>
    </tr>
    <tr runat="server" id="tr_PayforDeliveryBranch">
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_toPayforDeliveryBranch" runat="server" Text="To Pay Recovery Ledger For Delivery  Branch:"></asp:Label></td>
        <td style="width: 25%">
            <cc1:DDLSearch ID="ddl_toPayforDeliveryBranch" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
            <asp:Label ID="Label1" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label></td>
    </tr>
    <tr runat="server" id = "tr_Delivery_Commision_Income_AC">
        <td class="TD1" style="width: 50%">
        <asp:Label ID="lbl_Delivery_Commision_Income_AC" runat="server" Text="Delivery Commision Income A/C :"></asp:Label>
        </td>
        <td style="width: 25%">
        <cc1:DDLSearch ID="ddl_Delivery_Commision_Income_AC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        <asp:Label ID="Label4" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="tr_Delivery_Commision_Expense_AC">
        <td class="TD1" style="width: 50%">
        <asp:Label ID="lbl_Delivery_Commision_Expense_AC" runat="server" Text="Delivery Commision Expense A/C :"></asp:Label>
        </td>
        <td style="width: 25%">
        <cc1:DDLSearch ID="ddl_Delivery_Commision_Expense_AC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        <asp:Label ID="Label5" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="tr_LHPO_Other_Charges_Expense_AC">
        <td class="TD1" style="width: 50%">
        <asp:Label ID="lbl_LHPO_Other_Charges_Expense_AC" runat="server" Text="LHPO Other Charges Expense A/C :"></asp:Label>
        </td>
        <td style="width: 25%">
        <cc1:DDLSearch ID="ddl_LHPO_Other_Charges_Expense_AC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        <asp:Label ID="Label6" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="tr_LHPO_Other_Charges_Payble_AC">
        <td class="TD1" style="width: 50%">
        <asp:Label ID="lbl_LHPO_Other_Charges_Payble_AC" runat="server" Text="LHPO Other Charges Payble A/C :"></asp:Label>
        </td>
        <td style="width: 25%">
        <cc1:DDLSearch ID="ddl_LHPO_Other_Charges_Payble_AC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        <asp:Label ID="Label7" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="tr_Lorry_Payble_ATH_BTH_AC">
        <td class="TD1" style="width: 50%">
        <asp:Label ID="Label8" runat="server" Text="Lorry Payble (ATH/BTH) AC :"></asp:Label>
        </td>
        <td style="width: 25%">
        <cc1:DDLSearch ID="ddl_Lorry_Payble_ATH_BTH_AC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        <asp:Label ID="Label9" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="tr_UpcountryCostAC">
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_UpcountryCostAC" runat="server" Text="Upcountry Cost A/C:"></asp:Label></td>
        <td style="width: 25%">
            <cc1:DDLSearch ID="ddl_UpcountryCostAC" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
            <asp:Label ID="Label11" runat="server" CssClass="TDMANDATORY" Text="*"></asp:Label></td>
    </tr>

    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsTreatBookingIncomeAdvIncome" runat="Server" Text="Is Treat Booking Income as Advance Income UpTo Delivery?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsTreatBookingIncomeAdvIncome" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsToBilledAccountingGCWise" runat="Server" Text="Is To Be Billed Accounting GC Wise?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsToBilledAccountingGCWise" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsBookingMoneyReceiptRequired" runat="Server" Text="Is Booking Money Receipt Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsBookingMoneyReceiptRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_StbLedger" runat="server" CssClass="LABEL" Text="Short Term Bill Ledger :" /></td>
        <td style="width: 25%">
         <cc1:DDLSearch ID="ddl_StbLedger" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
         CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"  runat="server" />
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    
    <tr>
         <td class="TD1" style="width:50%;">
          <asp:Label ID="lbl_Division" runat="server" CssClass="LABEL" Text="Division :" />
             </td>      
         
        <td style="width: 25%">
            <asp:DropDownList ID="ddl_Division" runat="server" CssClass ="DROPDOWN" OnSelectedIndexChanged="ddl_Division_SelectedIndexChanged" AutoPostBack="true"> 
             </asp:DropDownList></td>
             <td style="width: 25%" />
        
    </tr>
    <tr>
    
    <td colspan="6" style="width: 100%;height:200px" align="left" >
            <table cellpadding="5" cellspacing="5" border="0"  width="100%">
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnl_BookingParameters" runat="server" GroupingText="Booking Parameters Details "
                            CssClass="PANEL" Width="100%" >
                            <table cellpadding="3"  cellspacing="3" border="0"  width="100%">
                                <tr>
                                    <td style="width: 100%;" colspan="8">
                                        <div >
                                            <asp:UpdatePanel  ID="Upd_pnl_BookingParameters" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dg_BookingParameters"  runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                                        ShowFooter="True" Width="100%" OnCancelCommand="dg_BookingParameters_CancelCommand"
                                                        OnEditCommand="dg_BookingParameters_EditCommand" OnItemCommand="dg_BookingParameters_ItemCommand"
                                                        OnItemDataBound="dg_BookingParameters_ItemDataBound" OnDeleteCommand="dg_BookingParameters_DeleteCommand">
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                          <Columns>
                                                          <asp:TemplateColumn Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" ></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                        <asp:Label ID="lbl_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" ></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:Label ID="lbl_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" ></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateColumn>
                                                          
                                                          <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_BookingTypeId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Type_ID") %>'
                                                                        ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                           <asp:TemplateColumn HeaderText="Booking Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_BookingType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Type") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_BookingType" CssClass="DROPDOWN" runat="server" >
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_BookingType" CssClass="DROPDOWN" runat="server" >
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                               <HeaderStyle Width="20%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PaymentTypeId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Payment_Type_ID") %>'
                                                                        ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn HeaderText="Payment Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PaymentType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Payment_Type") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_PaymentType" CssClass="DROPDOWN" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_PaymentType_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_PaymentType" CssClass="DROPDOWN" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_PaymentType_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                 <HeaderStyle Width="20%" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AdvanceBookingIncomeLedgerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Advance_Booking_Income_Ledger_ID") %>'
                                                                        ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="Advance Booking Income Ledger">
                                                                <ItemTemplate>
                                                                      <%# DataBinder.Eval(Container.DataItem, "Advance_Booking_Income_Ledger_Name")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_AdvanceBookingLedgerName" CallBackAfter="2" IsCallBack="true" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"  runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate> 
                                                                    <cc1:DDLSearch ID="ddl_AdvanceBookingLedgerName" CallBackAfter="2" IsCallBack="True"  AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_BookingIncomeLedgerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Income_Ledger_ID") %>'
                                                                       ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="Booking Income Ledger">
                                                                <ItemTemplate>
                                                                     <%# DataBinder.Eval(Container.DataItem, "Booking_Income_Ledger_Name")%>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_BookingIncomeLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                        runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <cc1:DDLSearch ID="ddl_BookingIncomeLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                        runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ServiceTaxLedgerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Service_Tax_Ledger_ID") %>'
                                                                       ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="Service Tax Ledger">
                                                                <ItemTemplate>
                                                                              <%# DataBinder.Eval(Container.DataItem,"Service_Tax_Ledger_Name") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_ServiceTaxLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <cc1:DDLSearch ID="ddl_ServiceTaxLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                               <asp:TemplateColumn HeaderText="Other Charge Ledger">
                                                                <ItemTemplate>
                                                                              <%# DataBinder.Eval(Container.DataItem, "OtherChargeLedger_Name")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_OtherChargeLedger" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <cc1:DDLSearch ID="ddl_OtherChargeLedger" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel"
                                                                EditText="Edit" >
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Add/Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"
                                                                        ></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtn_Add" CommandName="Add" runat="server" Text="Add" ></asp:LinkButton>
                                                                </FooterTemplate>
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="dg_BookingParameters" />
                                                <asp:AsyncPostBackTrigger ControlID="dg_BookingParameters" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_Division" />                                                
                                            </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
                </td>
                    </tr>
                <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_BookingParameters" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                             <asp:HiddenField ID="hdn_CompanyId" runat="server" />

    
    </table>
    <script type="text/javascript">
    EnabeledDisabledRecoveryLedgers();
    </script>