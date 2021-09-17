<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAUSExcessDetails.ascx.cs"
    Inherits="Operations_Inward_WucAUSExcessDetails" %>
<table class="TABLE" style="width: 100%">
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
          <%--  <asp:Panel ID="pnl_AUSExcessDetails" runat="server" CssClass="PANEL" Height="200px"
                GroupingText="AUS Excess Details" Width="100%" ScrollBars="Both">--%>
                <asp:UpdatePanel ID="upd_pnl_dg_AUSExcessDetails" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                      <div id ="Div_PDS"  class="DIV" style="height:250px">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="6">
                                    <asp:DataGrid ID="dg_ExcessDetails" runat="server" Width="100%" CssClass="GRID" AutoGenerateColumns="False"
                                        ShowFooter="True" OnCancelCommand="dg_ExcessDetails_CancelCommand" OnDeleteCommand="dg_ExcessDetails_DeleteCommand"
                                        OnEditCommand="dg_ExcessDetails_EditCommand" OnItemCommand="dg_ExcessDetails_ItemCommand"
                                        OnItemDataBound="dg_ExcessDetails_ItemDataBound" OnUpdateCommand="dg_ExcessDetails_UpdateCommand"
                                        OnSelectedIndexChanged="dg_ExcessDetails_SelectedIndexChanged">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="GC No">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_GCNo" runat="server" CssClass="TEXTBOXNOS" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                        BorderWidth="1px" MaxLength="10"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "GC_No") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_GCNo" runat="server" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                        CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="10"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Excess Articles">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_ExcessArticles" onkeypress="return Only_Integers(this,event)"
                                                        Width="94%" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="4" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Excess_Articles") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_ExcessArticles" onkeypress="return Only_Integers(this,event)"
                                                        Width="94%" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="4" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Marking On The Package">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Marking" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                        Width="94%" MaxLength="50"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Marking_On_Package") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Marking" runat="server" CssClass="TEXTBOX" BorderWidth="1px" 
                                                        Width="94%" MaxLength="50"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="120px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Packing Type">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_PackingType" runat="server" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Packing_Type") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_PackingType" runat="server" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="130px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Commodity">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Commodity_Name")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="120px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Item">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_Item" runat="server" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Item_Name")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_Item" runat="server" CssClass="DROPDOWN" >
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="120px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remarks">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                         MaxLength="100" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Remarks") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="120px" />
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                                <HeaderStyle Width="50px" />
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" ></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 2%; height: 21px;">
                                </td>
                                <td style="width: 20%; height: 21px;">
                                    <asp:Label ID="lbl_TotalExcessArticles" Text="Total :" CssClass="LABEL" Style="font-weight: bolder"
                                        runat="server">
                                    </asp:Label>
                                    <asp:Label ID="lbl_TotalExcessArticlesValue" CssClass="LABEL" Style="font-weight: bolder"
                                        runat="server">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        </div> 
                    </ContentTemplate>
                </asp:UpdatePanel>
           <%-- </asp:Panel>--%>
           
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_ExcessDetails" />
                </Triggers>
            </asp:UpdatePanel>
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