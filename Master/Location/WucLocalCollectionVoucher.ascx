<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLocalCollectionVoucher.ascx.cs"
    Inherits="Master_Location_WucLocalCollectionVoucher" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<table style="width: 100%" class="TABLE">

    <tr>
        <td colspan="3" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_LocalDivision" Text="Local Division:" runat="server" CssClass="LABEL"></asp:Label>
        </td>
        <td style="width: 30%">
            <asp:DropDownList ID="ddl_LocalDivision" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_LocalDivision_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </td>
        <td style="width: 50%" class="TD1">
        </td>
    </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnl_LocalCollectionVoucher" runat="server" GroupingText="Local Collection Voucher"
                            CssClass="PANEL" Width="100%">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td style="width: 100%;" colspan="8">
                                        <asp:UpdatePanel ID="Upd_Pnl_dg_LocalCollectionVoucher" UpdateMode="Conditional"
                                            runat="server">
                                            <ContentTemplate>
                                                <asp:DataGrid ID="dg_LocalCollectionVoucher" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    ShowFooter="True" CellPadding="2" CssClass="GRID" PageSize="15" Width="100%"
                                                    OnCancelCommand="dg_LocalCollectionVoucher_CancelCommand" OnDeleteCommand="dg_LocalCollectionVoucher_DeleteCommand"
                                                    OnEditCommand="dg_LocalCollectionVoucher_EditCommand" OnItemCommand="dg_LocalCollectionVoucher_ItemCommand"
                                                    OnItemDataBound="dg_LocalCollectionVoucher_ItemDataBound" OnUpdateCommand="dg_LocalCollectionVoucher_UpdateCommand">
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
                                                        <asp:TemplateColumn HeaderText="Local Collection Expense Ledger">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_LocalLedger" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Ledger_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:DDLSearch ID="ddl_LocalLedger" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                    IsCallBack="True" PostBack="False"/>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:DDLSearch ID="ddl_LocalLedger" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                    CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                     IsCallBack="True" PostBack="False"  />
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
                                                <asp:AsyncPostBackTrigger ControlID="dg_LocalCollectionVoucher" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_LocalDivision" />
                                                
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
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
             <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_DoorDivision" Text="Door Division:" runat="server" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddl_DoorDivision" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_DoorDivision_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 50%"  >
                    </td>
                </tr>
                <tr>
                
                    <td colspan="3" style="width: 100%" align="left">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnl_DoorDeliveryExpenseVoucher" runat="server" GroupingText="Door Delivery Expense Voucher"
                                        CssClass="PANEL" Width="100%">
                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                            <tr>
                                                <td style="width: 100%;" colspan="8">
                                                    <asp:UpdatePanel ID="Upd_Pnl_DoorDeliveryExpenseVoucher" UpdateMode="Conditional"
                                                        runat="server">
                                                        <ContentTemplate>
                                                            <asp:DataGrid ID="dg_DoorDeliveryExpenseVoucher" runat="server" AllowSorting="True"
                                                                AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID"
                                                                PageSize="15" Width="100%" OnCancelCommand="dg_DoorDeliveryExpenseVoucher_CancelCommand"
                                                                OnDeleteCommand="dg_DoorDeliveryExpenseVoucher_DeleteCommand" OnEditCommand="dg_DoorDeliveryExpenseVoucher_EditCommand"
                                                                OnItemCommand="dg_DoorDeliveryExpenseVoucher_ItemCommand" OnItemDataBound="dg_DoorDeliveryExpenseVoucher_ItemDataBound"
                                                                OnUpdateCommand="dg_DoorDeliveryExpenseVoucher_UpdateCommand">
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
                                                                    <asp:TemplateColumn HeaderText="Door Delivery Expense Ledger">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_DoorLedger" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Ledger_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc1:DDLSearch ID="ddl_DoorLedger" runat="server" AllowNewText="True" CallBackAfter="2"
                                                                                CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                                InjectJSFunction="" IsCallBack="True" PostBack="False" Text="" />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <cc1:DDLSearch ID="ddl_DoorLedger" runat="server" AllowNewText="True" CallBackAfter="2"
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
                                                            <asp:AsyncPostBackTrigger ControlID="dg_DoorDeliveryExpenseVoucher" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddl_DoorDivision" />
                                                            
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
                                    <asp:HiddenField ID="hdf_ResourecString" runat="server" />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_LocalCollectionVoucher" />
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
            </td>
            </tr>
</table>
