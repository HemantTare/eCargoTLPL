<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractTerms.ascx.cs" Inherits="Master_Sales_WucContractTerms" %>

<table style="width: 100%" class="TABLE">
       <tr>
        <td colspan="3">&nbsp;</td>        
    </tr>
     <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" >
            <asp:UpdatePanel ID="Upd_Pnl_dg_ContractTerms" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_ContractTerms" runat="server"  AllowSorting="True"
                        AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" OnCancelCommand="dg_ContractTerms_CancelCommand" OnDeleteCommand="dg_ContractTerms_DeleteCommand" OnEditCommand="dg_ContractTerms_EditCommand" OnItemDataBound="dg_ContractTerms_ItemDataBound"  OnUpdateCommand="dg_ContractTerms_UpdateCommand" OnItemCommand="dg_ContractTerms_ItemCommand" meta:resourcekey="dg_ContractTermsResource1">
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>       
                                   <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Terms_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Term_ID") %>' meta:resourcekey="lbl_Terms_IDResource1" ></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateColumn>
                                
                                      
                            
                            <asp:TemplateColumn HeaderText="Terms Head">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TermsHead" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Term_Head") %>' meta:resourcekey="lbl_TermsHeadResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:DropDownList ID="ddl_TermsHead" CssClass="DROPDOWN"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_TermsHead_SelectedIndexChanged" meta:resourcekey="ddl_TermsHeadResource2"></asp:DropDownList>                                    
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:DropDownList ID="ddl_TermsHead" CssClass="DROPDOWN"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_TermsHead_SelectedIndexChanged" meta:resourcekey="ddl_TermsHeadResource1" ></asp:DropDownList>                                    
                                </FooterTemplate>
                                <ItemStyle Width="35%" />
                                <FooterStyle Width="35%" />
                                <HeaderStyle Width="35%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Description" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' meta:resourcekey="lbl_DescriptionResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' meta:resourcekey="txt_DescriptionResource2" Enabled="False"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' meta:resourcekey="txt_DescriptionResource1" Enabled="False"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Width="55%" />
                                <FooterStyle Width="55%" />
                                <HeaderStyle Width="55%" />
                            </asp:TemplateColumn>
                            
                           <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel"
                                        EditText="Edit" meta:resourcekey="EditCommandColumnResource1" >
                                        <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="Add/Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete" meta:resourcekey="lbtn_DeleteResource1"
                                        ></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add" meta:resourcekey="lbtn_AddResource1" ></asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_ContractTerms" />
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3">
          <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_ContractTerms" />
          </Triggers>
                <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" EnableViewState="false"></asp:Label>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
    </tr>
</table>
