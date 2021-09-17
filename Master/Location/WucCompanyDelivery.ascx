<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyDelivery.ascx.cs" Inherits="Master_Location_WucCompanyDelivery" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript"   src="../../Javascript/ddlsearch.js"></script>
<table style="width: 100%" class="TABLE">
 
    <tr>
    
          <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Division" runat="server" CssClass="LABEL" Text="Division" /></td>      
         
        <td style="width: 30%">
            <asp:DropDownList ID="ddl_Division" runat="server" CssClass ="DROPDOWN" OnSelectedIndexChanged="ddl_Division_SelectedIndexChanged" AutoPostBack="true"
            > 
             </asp:DropDownList></td>
 <td style="width: 50%" class="TD1">
        </td>        
    </tr>
     <tr>
    
    <td colspan="6" style="width: 100%;height:200px" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnl_CompanyDelivery" runat="server" GroupingText="Company Delivery Details "
                            CssClass="PANEL" Width="100%" >
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td style="width: 100%;" colspan="8">
                                        <div >
                                            <asp:UpdatePanel ID="Upd_pnl_CompanyDelivery" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="dg_CompanyDelivery" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dg_CompanyDelivery" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                                        ShowFooter="True" Width="100%" OnCancelCommand="dg_CompanyDelivery_CancelCommand"
                                                        OnEditCommand="dg_CompanyDelivery_EditCommand" OnItemCommand="dg_CompanyDelivery_ItemCommand"
                                                        OnItemDataBound="dg_CompanyDelivery_ItemDataBound" OnDeleteCommand="dg_CompanyDelivery_DeleteCommand">
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
                                                                    <%# DataBinder.Eval(Container.DataItem,"Booking_Type") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_BookingType" CssClass="DROPDOWN" runat="server" >
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_BookingType" CssClass="DROPDOWN" runat="server" >
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DeliveryIncomeLedgerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Delivery_Income_Ledger_ID") %>'
                                                                       ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="Delivery Income Ledger">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem,"Delivery_Income_Ledger_Name") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_DeliveryIncomeLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <cc1:DDLSearch ID="ddl_DeliveryIncomeLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
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
                                                            
                                                             <asp:TemplateColumn Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_OctroiReceivableLedgerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Octroi_Receivable_Ledger_ID") %>'
                                                                       ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="1%" />
                                                            </asp:TemplateColumn>
                                                            
                                                            
                                                            <asp:TemplateColumn HeaderText="Octroi Receivable Ledger">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem,"Octroi_Receivable_Ledger_Name") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <cc1:DDLSearch ID="ddl_OctroiReceivableLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
                                                                        CallBackFunction="Raj.EC.CompanyCallBackFunction.CallBack.GetCompanyLedgers"
                                                                         runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <cc1:DDLSearch ID="ddl_OctroiReceivableLedgerName" CallBackAfter="2" IsCallBack="True" AllowNewText="true"
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
                                                <asp:AsyncPostBackTrigger ControlID="dg_CompanyDelivery" />
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
                                            <asp:AsyncPostBackTrigger ControlID="dg_CompanyDelivery" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

    
    </table>
    
                                                           
