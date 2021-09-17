<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPOPenalties.ascx.cs" Inherits="Operations_Outward_WucLHPOPenalties" %>
<table style="width: 100%" class="TABLE">
    <tr>
        <td colspan="3" >
            <asp:UpdatePanel ID="Upd_Pnl_dg_IncentiveDetails" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_IncentiveDetails" runat="server"  AllowSorting="True"
                        AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>       
                                   <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BranchID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Term_ID") %>'  ></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateColumn>
                                
                                      
                            
                            <asp:TemplateColumn HeaderText="Days Before Comm.  Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DaysBeforeCommDate" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Term_Head") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server" AutoPostBack="True" ></asp:DropDownList>                                    
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server" AutoPostBack="True"  ></asp:DropDownList>                                    
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Amount" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Remark" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Remark" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Remark" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            
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
                    <asp:AsyncPostBackTrigger ControlID="dg_IncentiveDetails" />
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3" >
            <asp:UpdatePanel ID="Upd_Pnl_dg_PenaltyDetails" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_PenaltyDetails" runat="server"  AllowSorting="True"
                        AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>       
                                   <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BranchID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Term_ID") %>'  ></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateColumn>
                                
                                      
                            
                            <asp:TemplateColumn HeaderText="Days After Comm. Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DaysAfterCommDate" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Term_Head") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server" AutoPostBack="True" ></asp:DropDownList>                                    
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN"  runat="server" AutoPostBack="True"  ></asp:DropDownList>                                    
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Amount" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Remark" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Remark" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Remark" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:TextBox>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            
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
                                                                <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add" ></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                        </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_PenaltyDetails" />
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="3" style="height: 21px">
            <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"></asp:Label></td>
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