<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPOIncentivesPenalties.ascx.cs" Inherits="Operations_Outward_WucLHPOIncentivesPenalties" %>


<%--<asp:ScriptManager ID="scm_IncentivePenaltiesDetail" runat="server">
</asp:ScriptManager>--%>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript"  src="../../Javascript/Common.js"></script>

<table class="TABLE" style="width: 100%" >
    <tr>
        
        <td colspan="6">
        <%--<asp:Label ID="lbl_Incentive" runat="server" Text="Incentive Details" CssClass="HEADINGLABEL" meta:resourcekey="lbl_IncentiveResource1" ></asp:Label>--%>
            </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="center">
            <asp:Panel ID="pnl_IncentiveDetails" runat="server" CssClass="PANEL" Height="180px"
                GroupingText="Incentive Details"  Width="98%"  >
                
                <asp:UpdatePanel ID="upd_pnl_dg_IncentiveDetails"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table >
                            <tr>
                                <td colspan="6">
                                    <asp:DataGrid ID="dg_IncentiveDetails" runat="server" Width="800" CssClass="GRID"
                                        AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dg_IncentiveDetails_ItemDataBound" 
                                        OnCancelCommand="dg_IncentiveDetails_CancelCommand" 
                                        OnDeleteCommand="dg_IncentiveDetails_DeleteCommand" OnEditCommand="dg_IncentiveDetails_EditCommand"
                                         OnItemCommand="dg_IncentiveDetails_ItemCommand" OnUpdateCommand="dg_IncentiveDetails_UpdateCommand" >
                                      
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Day Before Comm. Date" >
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_DayBeforeDate" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" BorderWidth="1px" 
                                                    onkeyPress="return Only_Integers(this,event);" Width="94%" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "No_Of_Days") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_DayBeforeDate" runat="server" CssClass="TEXTBOXNOS" MaxLength="9"  BorderWidth="1px"
                                                    onkeyPress="return Only_Integers(this,event);" Width="94%" ></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="20%"  />
                                                 <ItemStyle Width="20%" HorizontalAlign="Right" />
                                                 <FooterStyle Width="20%" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="Amount"  >
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount"  runat="server" CssClass="TEXTBOXNOS"  MaxLength="20" 
                                                    onkeypress="return Only_Numbers(this,event);" Width="94%"
                                                    BorderWidth="1px" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Amount") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" runat="server" Width="94%" CssClass="TEXTBOXNOS"  MaxLength="20" BorderWidth="1px"
                                                    onkeypress="return Only_Numbers(this,event);" 
                                                     ></asp:TextBox>
                                                </EditItemTemplate>
                                                 <HeaderStyle Width="20%" />
                                                 <ItemStyle Width="20%" HorizontalAlign="Right" />
                                                 <FooterStyle Width="20%" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            
                                              <asp:TemplateColumn HeaderText="Remark" >
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX"  
                                                    BorderWidth="1px" Width="94%" MaxLength="100" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Remark") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX"  BorderWidth="1px"  MaxLength="100" ></asp:TextBox>
                                                </EditItemTemplate>
                                                 <HeaderStyle Width="45%"  />
                                                  <ItemStyle Width="45%"  HorizontalAlign="Left"/>
                                                 <FooterStyle Width="45%"  HorizontalAlign="Left"/>
                                            </asp:TemplateColumn>
                                            
                                            
                                               <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"  >
                                                <HeaderStyle Width="5%" />
                                                </asp:EditCommandColumn>
                                                
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" ></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" ></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                            
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    
     <tr>
        <td colspan="6">
            <%--<asp:Label ID="lbl_Penalty" runat="server" Text="Penalty Details" CssClass="HEADINGLABEL" meta:resourcekey="lbl_PenaltyResource1" ></asp:Label>--%>
            </td>
    </tr>
    
    
    <tr>
        <td colspan="6" style="width: 100%" align="center">
            <asp:Panel ID="pnl_PenaltyDetails" runat="server" CssClass="PANEL" Height="180px"
                GroupingText="Penalty Details" Width="98%" >
                
                <asp:UpdatePanel ID="Upd_Pnl_PenaltyDetails"  UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                        <table >
                            <tr>
                                <td colspan="6">
                                    <asp:DataGrid ID="dg_PenaltyDetails" Width="800" runat="server"  CssClass="GRID"
                                        AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_PenaltyDetails_CancelCommand" OnDeleteCommand="dg_PenaltyDetails_DeleteCommand" 
                                         OnEditCommand="dg_PenaltyDetails_EditCommand" OnItemCommand="dg_PenaltyDetails_ItemCommand"
                                          OnItemDataBound="dg_PenaltyDetails_ItemDataBound" OnUpdateCommand="dg_PenaltyDetails_UpdateCommand"
                                        >
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                     
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Day After Comm. Date" >
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_DayAfterDate" runat="server" CssClass="TEXTBOXNOS"
                                                     onkeyPress="return Only_Integers(this,event);" Width="94%" MaxLength="9"
                                                     BorderWidth="1px" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "No_Of_Days") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_DayAfterDate" runat="server" CssClass="TEXTBOXNOS" 
                                                     onkeyPress="return Only_Integers(this,event);" Width="94%" MaxLength="9"
                                                     BorderWidth="1px" ></asp:TextBox>
                                                </EditItemTemplate> 
                                                <HeaderStyle Width="20%" />
                                                 <ItemStyle  Width="20%" HorizontalAlign="Right" />
                                                 <FooterStyle Width="20%" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="Amount"  >
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount"  runat="server" CssClass="TEXTBOXNOS"  MaxLength="20" 
                                                    onkeypress="return Only_Numbers(this,event);"  Width="94%"
                                                     BorderWidth="1px"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Amount") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" runat="server" CssClass="TEXTBOXNOS" 
                                                     onkeypress="return Only_Numbers(this,event);" Width="94%"
                                                     MaxLength="20" BorderWidth="1px" ></asp:TextBox>
                                                </EditItemTemplate>
                                                 <HeaderStyle Width="20%"  />
                                                 <ItemStyle Width="20%" HorizontalAlign="Right" />
                                                 <FooterStyle Width="20%" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            
                                              <asp:TemplateColumn HeaderText="Remark">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX"   Width="94%"
                                                    BorderWidth="1px" MaxLength="100" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Remark") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX"  BorderWidth="1px" MaxLength="100" ></asp:TextBox>
                                                </EditItemTemplate>
                                                 <HeaderStyle Width="45%"  />
                                                   <ItemStyle Width="45%" HorizontalAlign="Left"  />
                                                 <FooterStyle Width="45%" HorizontalAlign="Left"  />
                                            </asp:TemplateColumn>
                                            
                                            
                                               <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit" >
                                                <HeaderStyle Width="5%" />
                                                </asp:EditCommandColumn>
                                                
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" ></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" ></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                            
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_IncentiveDetails" />
                    <asp:AsyncPostBackTrigger ControlID="dg_PenaltyDetails" />
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