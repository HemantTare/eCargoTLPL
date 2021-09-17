<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyTripHireParameters.ascx.cs" Inherits="Master_Location_WucCompanyTripHireParameters" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Location/Company.js"></script>

<table style="width: 100%" class="TABLE">

   
                <tr>
                    <td style="width: 25%" class="TD1">
                        <asp:Label ID="lbl_IsTreatAdvanceForOwnTruckAsExpense" Text="Is Treat Advance For Own Truck As Expense:"
                            runat="server" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                      <asp:CheckBox ID="Chk_IsTreatAdvanceForOwnTruckAsExpense" OnClick="EnableExpenseLedgerOnChecked();" runat="server" />
                      <%--  <asp:CheckBox ID="Chk_Box" runat="server" />--%>
                    </td>
                    <td style="width: 46%" class="TD1" colspan="3">                 
                    <table id="tbl_ExpenseLedger" runat="server">
                    <tr id="tr_ExpenseLedger" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_TripExpenseLedger" runat="server" CssClass="LABEL" Text="Trip Expense Ledger:" ></asp:Label></td>
                    <td >
                        <cc1:DDLSearch ID="ddl_TripExpenseLedger" runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails"
                            IsCallBack="True"  CallBackAfter="2" PostBack="false"/>
                    </td>
                    <td style="width: 1%" class="TDMANDATORY">
                        *</td>                       
                        </tr>
                    </table>
                    </td> 
                                  
                   </tr>              
            
            
                <tr>
        <td style="width: 25%" class="TD1">
            <asp:Label ID="lbl_TripHireDivision" Text="Trip Hire Division:" runat="server" CssClass="LABEL"></asp:Label>
        </td>
       
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_TripHireDivision" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_TripHireDivision_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td style="width: 46%" class="TD1" colspan="3">
        </td>
        </tr>
        
          <tr>
        <td style="width: 25%" class="TD1">
            <asp:Label ID="lbl_LHPONatureOfPaymentForTDSDeduction" Text="LHPO Nature Of Payment For TDS Dedcution:" runat="server" CssClass="LABEL"></asp:Label>
        </td>
       
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_LHPONatureOfPayment" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        
        <td style="width: 46%" class="TD1" colspan="3">
        </td>
    </tr>
    
                <tr>
                    <td colspan="5">
                        <asp:Panel ID="pnl_TripHireParameters" runat="server" GroupingText="Trip Hire Parameters"
                            CssClass="PANEL" Width="100%">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td style="width: 100%;" colspan="8">
                                    <div>
                                        <asp:UpdatePanel ID="Upd_Pnl_dg_TripHireParameters" UpdateMode="Conditional"
                                            runat="server">
                                            <ContentTemplate>
                                                <asp:DataGrid ID="dg_TripHireParameters" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    ShowFooter="True" CellPadding="2" CssClass="GRID" PageSize="15" Width="100%"
                                                    OnCancelCommand="dg_TripHireParameters_CancelCommand" OnDeleteCommand="dg_TripHireParameters_DeleteCommand"
                                                    OnEditCommand="dg_TripHireParameters_EditCommand" OnItemCommand="dg_TripHireParameters_ItemCommand"
                                                    OnItemDataBound="dg_TripHireParameters_ItemDataBound" OnUpdateCommand="dg_TripHireParameters_UpdateCommand" >
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    
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
                                                                <asp:Label ID="lbl_DivisionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Division_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Booking Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_BookingType" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_BookingType" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddl_BookingType" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemStyle Width="35%" />
                                                            <FooterStyle Width="35%" />
                                                            <HeaderStyle Width="35%" HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>                                                       
                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="Truck Hire Expense Ledger">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TruckHireExpenseAmount" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Truck_Hire_Expense_Ledger_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:DDLSearch ID="ddl_TruckHireExpenseAmountLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:DDLSearch ID="ddl_TruckHireExpenseAmountLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </FooterTemplate>
                                                            <ItemStyle Width="55%" />
                                                            <FooterStyle Width="55%" />
                                                            <HeaderStyle Width="55%" HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="TDS Ledger">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TDSLedger" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "TDS_Ledger_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:DDLSearch ID="ddl_TDSLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:DDLSearch ID="ddl_TDSLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </FooterTemplate>
                                                            <ItemStyle Width="55%" />
                                                            <FooterStyle Width="55%" />
                                                            <HeaderStyle Width="55%" HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Loading Charges">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_LoadingCharges" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Loading_Charges_Ledger_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:DDLSearch ID="ddl_LoadingChargesName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:DDLSearch ID="ddl_LoadingChargesName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                            </FooterTemplate>
                                                            <ItemStyle Width="55%" />
                                                            <FooterStyle Width="55%" />
                                                            <HeaderStyle Width="55%" HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>                                                       
                                                        
                                                        
                                                        
                                                        <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel"
                                                            EditText="Edit">
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Add/Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add"></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dg_TripHireParameters" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_TripHireDivision" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                   </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
             <tr>
                    <td style="width: 25%" class="TD1">
                        <asp:Label ID="lbl_ATHDivision" Text="ATH Division:" runat="server" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddl_ATHDivision" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_ATHDivision_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 46%"  colspan="3" >
                    </td>
                </tr>
                <tr>
                
                    <td colspan="5" style="width: 100%" align="left">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnl_ATH" runat="server" GroupingText="ATH"
                                        CssClass="PANEL" Width="100%">
                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                            <tr>
                                                <td style="width: 100%;" colspan="8">
                                                    <asp:UpdatePanel ID="Upd_Pnl_ATH" UpdateMode="Conditional"
                                                        runat="server">
                                                        <ContentTemplate>
                                                            <asp:DataGrid ID="dg_ATH" runat="server" AllowSorting="True"
                                                                AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID"
                                                                PageSize="15" Width="100%" OnCancelCommand="dg_ATH_CancelCommand"
                                                                OnDeleteCommand="dg_ATH_DeleteCommand" OnEditCommand="dg_ATH_EditCommand"
                                                                OnItemCommand="dg_ATH_ItemCommand" OnItemDataBound="dg_ATH_ItemDataBound" OnUpdateCommand="dg_ATH_UpdateCommand">                                                              
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
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
                                                                <asp:Label ID="lbl_DivisionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Division_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Booking Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_BookingType" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Type") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="ddl_BookingType" runat="server" CssClass="DROPDOWN">
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddl_BookingType" runat="server" CssClass="DROPDOWN">
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        <ItemStyle Width="35%" />
                                                                        <FooterStyle Width="35%" />
                                                                        <HeaderStyle Width="35%" HorizontalAlign="Center" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Fuel Expense Ledger">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_FuelLedger" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Ledger_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc1:DDLSearch ID="ddl_FuelExpenseLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                                CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                                InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <cc1:DDLSearch ID="ddl_FuelExpenseLedgerName" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                                CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                                InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle Width="55%" />
                                                                        <FooterStyle Width="55%" />
                                                                        <HeaderStyle Width="55%" HorizontalAlign="Center" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel"
                                                                        EditText="Edit">
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                                    </asp:EditCommandColumn>
                                                                    <asp:TemplateColumn HeaderText="Add/Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                                <PagerStyle Mode="NumericPages" />
                                                            </asp:DataGrid>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="dg_ATH" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddl_ATHDivision" />
                                                            
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_TripHireParameters" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            </table>
                     </td>
                     </tr>    

</table>
 <script type="text/javascript">
EnableExpenseLedgerOnChecked();
</script>

