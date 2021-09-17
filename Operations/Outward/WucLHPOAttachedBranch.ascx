<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPOAttachedBranch.ascx.cs" Inherits="Operations_Outward_WucLHPOAttachedBranch" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<table style="width: 100%" class="TABLE">
    <tr>
     <td colspan="3" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
       
                    <asp:Panel ID="pnl_dg_AttachedLHPOBranches" runat="server"  GroupingText="Attached LHPO Branches" CssClass="PANEL" Width="100%" >
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td style="width: 100%; " colspan="8">
       
            <asp:UpdatePanel ID="Upd_Pnl_dg_AttachedLHPOBranches" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_AttachedLHPOBranches" runat="server"  AllowSorting="True"
                        AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" OnCancelCommand="dg_AttachedLHPOBranches_CancelCommand" OnDeleteCommand="dg_AttachedLHPOBranches_DeleteCommand" OnEditCommand="dg_AttachedLHPOBranches_EditCommand" OnItemCommand="dg_AttachedLHPOBranches_ItemCommand" OnItemDataBound="dg_AttachedLHPOBranches_ItemDataBound" OnUpdateCommand="dg_AttachedLHPOBranches_UpdateCommand" >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>       
                                   <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BranchID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BranchID") %>'  ></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateColumn>
                                
                                      
                            
                            <asp:TemplateColumn HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Branch" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <%--<asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server"  ></asp:DropDownList> --%>                                   
                                
                                <cc1:DDLSearch ID="ddl_Branch" runat="server" AllowNewText="True" CallBackAfter="2"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAttachedLHPOBranch" InjectJSFunction=""
                                IsCallBack="True"  />
                        
                                </EditItemTemplate>
                                <FooterTemplate>
                                <%--<asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server"   ></asp:DropDownList>                                    --%>
                                <cc1:DDLSearch ID="ddl_Branch" runat="server" AllowNewText="True" CallBackAfter="2"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAttachedLHPOBranch" InjectJSFunction=""
                                IsCallBack="True"  />
                                </FooterTemplate>
                                <ItemStyle Width="35%" />
                                <FooterStyle Width="35%" />
                                <HeaderStyle Width="35%" />
                            </asp:TemplateColumn>
     <%--                       <asp:TemplateColumn HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Description" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Width="55%" />
                                <FooterStyle Width="55%" />
                                <HeaderStyle Width="55%" />
                            </asp:TemplateColumn>--%>
                            
                           <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel"
                                                            EditText="Edit"  >
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Add/Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete" 
                                                                    ></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add"  ></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                        </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                     </ContentTemplate>
                <Triggers>                    
                    <asp:AsyncPostBackTrigger ControlID="dg_AttachedLHPOBranches" />
                    
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
                <asp:AsyncPostBackTrigger ControlID="dg_AttachedLHPOBranches" />            
            </Triggers>
            <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
                    <asp:HiddenField ID="Hdn_AttBranch" runat="server" Value="0"></asp:HiddenField>
            </ContentTemplate>            
            </asp:UpdatePanel>
         </td>
    </tr>   
    </table>
    </td>
    </tr>
    </table>
    
    
    

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom:50%; left:50%; font-size: 11px; font-family: Verdana; z-index:100">
	<span id="ajaxloading">            
	<table>
	  <tr>
	    <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
	  </tr>
	  <tr>
	    <td align="center" >Wait! Action in Progress...</td>
	  </tr>
	</table>
	</span>
    </div>
  </ProgressTemplate>
 </asp:UpdateProgress>