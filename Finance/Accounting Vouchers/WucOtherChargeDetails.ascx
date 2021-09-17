<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOtherChargeDetails.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucOtherChargeDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<table width="100%">    
     <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="updatepanel" runat="server" >
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_OtherChargeDetails" />
            </Triggers>
            <ContentTemplate>
                    <asp:DataGrid ID="dg_OtherChargeDetails" runat="server" CssClass="GRID" 
                        AutoGenerateColumns="False" ShowFooter="True" 
                        OnCancelCommand="dg_OtherChargeDetails_CancelCommand" 
                        OnDeleteCommand="dg_OtherChargeDetails_DeleteCommand"
                        OnEditCommand="dg_OtherChargeDetails_EditCommand" 
                        OnItemCommand="dg_OtherChargeDetails_ItemCommand" 
                        OnItemDataBound="dg_OtherChargeDetails_ItemDataBound" 
                        OnUpdateCommand="dg_OtherChargeDetails_UpdateCommand" 
                        OnSelectedIndexChanged="dg_OtherChargeDetails_SelectedIndexChanged" >
                           <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                           <HeaderStyle CssClass="GRIDHEADERCSS" />
                           <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                            
                            <asp:TemplateColumn HeaderText = "Add/Less">
                                  <FooterTemplate>
                                    <asp:DropDownList ID="ddl_AddLess" runat="server" CssClass="DROPDOWN" Width="98%">
                                    <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Add" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Less" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                 <ItemTemplate>
                                     <%# DataBinder.Eval(Container.DataItem, "Is_AddLess")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_AddLess" runat="server" CssClass="DROPDOWN" Width="98%">
                                    <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Add" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Less" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>  
                             </asp:TemplateColumn>
                             
                             <asp:TemplateColumn HeaderText = "Ledger Name">
                             <FooterStyle HorizontalAlign="Left" />
                             <HeaderStyle HorizontalAlign="Left" />
                             <ItemStyle HorizontalAlign="Left" />
                             
                                <FooterTemplate>
                                   <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedger" CallBackAfter="2" InjectJSFunction="" PostBack="False" />
                                </FooterTemplate>
                                 <ItemTemplate>
                                     <%# DataBinder.Eval(Container.DataItem, "Ledger_Name")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedger" CallBackAfter="2" InjectJSFunction="" PostBack="False" />
                                </EditItemTemplate>  
                             </asp:TemplateColumn>
                            
                           <asp:TemplateColumn HeaderText="Amount">
                              <ItemStyle HorizontalAlign ="Right" /> 
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" onblur="return valid(this)" Width="95%"
                                    BorderWidth="1px" runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox> 
                                </FooterTemplate>
                                 <ItemTemplate>
                                     <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS"   onblur="return valid(this)" BorderWidth="1px" 
                                     runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9" Width="95%"></asp:TextBox> 
                                </EditItemTemplate> 
                                
                            </asp:TemplateColumn>
                                                  
                            
                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit" >
                            </asp:EditCommandColumn>

                            <asp:TemplateColumn HeaderText="Delete">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" Text="Add" Runat="server" CommandName="Add" ></asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                          </Columns>
                        </asp:DataGrid>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        
        </td>
    </tr>
     <tr>
        <td style="width: 50%" colspan="3"></td>       
        <td colspan="2" align="right">
        <asp:UpdatePanel ID="up_GridTotal" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Text="Total :" Font-Bold="true"></asp:Label>
                <asp:Label ID="lbl_TotalAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>
    
    <tr>
    <td colspan="6" align="left">
        <asp:UpdatePanel ID="up_Error" runat="server">
            <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                    <asp:HiddenField ID="hdn_TotalAmount" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>       
    </td>
    </tr>
</table>



