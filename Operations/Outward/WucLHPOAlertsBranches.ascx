<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPOAlertsBranches.ascx.cs" Inherits="Operations_Outward_WucLHPOAlertsBranches" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<table style="width: 100%" class="TABLE">
    <tr>
     <td colspan="3" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
            <tr>        
                 <td>
                    <asp:Panel ID="pnl_dg_AlertBranches" runat="server"  GroupingText="Alert Branches" CssClass="PANEL" Width="100%" >
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                        <tr>
                            <td style="width: 100%; " colspan="8">
                            <asp:UpdatePanel ID="Upd_Pnl_dg_AlertBranches" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DataGrid ID="dg_AlertBranches" runat="server"  AllowSorting="True"
                                AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                                PageSize="15" Width="100%" OnCancelCommand="dg_AlertBranches_CancelCommand" OnDeleteCommand="dg_AlertBranches_DeleteCommand" OnEditCommand="dg_AlertBranches_EditCommand" OnItemCommand="dg_AlertBranches_ItemCommand" OnItemDataBound="dg_AlertBranches_ItemDataBound" OnUpdateCommand="dg_AlertBranches_UpdateCommand" >
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <Columns>       
                                        <asp:TemplateColumn Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BranchID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BranchID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Branch" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc1:DDLSearch ID="ddl_Branch" runat="server" AllowNewText="True" CallBackAfter="2"
                                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" InjectJSFunction=""
                                                IsCallBack="True" PostBack="False" Text=""  />                                  
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <cc1:DDLSearch ID="ddl_Branch" runat="server" AllowNewText="True" CallBackAfter="2"
                                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" InjectJSFunction=""
                                                IsCallBack="True" PostBack="False" Text=""  />                                  
                                            </FooterTemplate>
                                        <ItemStyle Width="35%" />
                                        <FooterStyle Width="35%" />
                                        <HeaderStyle Width="35%"  HorizontalAlign="Center"/>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Description" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Description" runat="server" MaxLength="250" BorderWidth="1px" CssClass="TEXTBOX"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" MaxLength="250" CssClass="TEXTBOX"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                                            </FooterTemplate>
                                        <ItemStyle Width="55%" />
                                        <FooterStyle Width="55%" />
                                        <HeaderStyle Width="55%"  HorizontalAlign="Center"/>
                                        </asp:TemplateColumn>

                                        <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="Edit" >
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
                                <asp:AsyncPostBackTrigger ControlID="dg_AlertBranches" />
                            </Triggers>
                            </asp:UpdatePanel>  
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>                                     
                </td>
            </tr>
    <tr><td colspan="3">&nbsp;</td></tr>
    <tr><td colspan="3" >&nbsp;&nbsp;&nbsp;</td></tr>
    <tr>
        <td colspan="3" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>        
                <td>
                   <asp:Panel ID="Pnl_ATHDetails" runat="server"  GroupingText="ATH Details" CssClass="PANEL" Width="100%" >
                       <table cellpadding="3" cellspacing="3" border="0" width="100%">
                          <tr>
                              <td style="width: 20%; "></td>
                              <td style="width: 20%; " align="right">
                                 <asp:Label ID="lbl_TotalAdvance" Text="Total Advance:" CssClass="LABEL" runat="server"></asp:Label>
                              </td>
                              <td style="width: 20%; ">
                               <asp:UpdatePanel ID="Upd_Pnl_txt_TotalAdvance" UpdateMode="Conditional" runat="server" >
                                   <Triggers>
                                        <asp:AsyncPostBackTrigger  ControlID="dg_ATHDetails"/>
                                   </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_TotalAdvance" runat="server" Width="80%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" onkeypress="return Only_Numbers(this,event)" BorderWidth="1px" CssClass="TEXTBOXNOS" ></asp:TextBox>
                                        <asp:HiddenField ID="hdn_TotalAdvance" runat="server" />
                                     </ContentTemplate>
                               </asp:UpdatePanel>
                               </td>
                               <td style="width: 20%; "></td>
                               <td  colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 100%; " colspan="8">
                                <asp:UpdatePanel ID="Upd_Pnl_ATHDetails" UpdateMode="Conditional" runat="server" >
                                <ContentTemplate>
                                    <asp:DataGrid ID="dg_ATHDetails" runat="server"  AllowSorting="True"
                                    AutoGenerateColumns="False" ShowFooter="True" CellPadding="2" CssClass="GRID" 
                                    PageSize="15" Width="100%" OnItemDataBound="dg_ATHDetails_ItemDataBound" OnCancelCommand="dg_ATHDetails_CancelCommand" OnDeleteCommand="dg_ATHDetails_DeleteCommand" OnEditCommand="dg_ATHDetails_EditCommand" OnItemCommand="dg_ATHDetails_ItemCommand" OnUpdateCommand="dg_ATHDetails_UpdateCommand">
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Advance Payable At     Advance Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AdvancePayableAt" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Hierarchy_Name") %>'></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbl_AdvanceLocation" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Main_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <uc2:WucHierarchyWithID ID="WucHierarchyWithID1"  runat="server" />                                    
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <uc2:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
                                            </FooterTemplate>                               
                                        </asp:TemplateColumn>                            

                                        <asp:TemplateColumn HeaderText="Advance Amount" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AdvanceAmount" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Advance_Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_AdvanceAmount" runat="server" MaxLength="12" onkeypress="return Only_Numbers(this,event)" Width="94%" CssClass="TEXTBOXNOS"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Advance_Amount") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_AdvanceAmount" runat="server" MaxLength="12"  onkeypress="return Only_Numbers(this,event)" Width="94%" CssClass="TEXTBOXNOS"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Advance_Amount") %>'></asp:TextBox>
                                            </FooterTemplate>                                
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Sch. Arr. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SchArrDate" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Schedule_Arr_Date","{0:MMMM dd,yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <ComponentArt:Calendar id="wuc_SchArrDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker" 
                                               AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01"   SelectedDate="<%#DateTime.Now%>"/>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                              <ComponentArt:Calendar id="wuc_SchArrDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"  ControlType="Picker" PickerCssClass="picker" 
                                              AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01"   SelectedDate="<%#DateTime.Now%>"/>
                                            </FooterTemplate>                                
                                        </asp:TemplateColumn> 

                                        <asp:TemplateColumn HeaderText="Sch. Arr. Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SchArrTime" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Schedule_Arr_Time") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <uc1:TimePicker ID="wuc_SchArrTime" runat="server" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <uc1:TimePicker ID="wuc_SchArrTime" runat="server" />
                                            </FooterTemplate>                                
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Ref. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_RefNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Ref_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_RefNo" runat="server" Width="94%"  CssClass="TEXTBOX"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Ref_No") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_RefNo" runat="server" Width="94%" CssClass="TEXTBOX"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Ref_No") %>'></asp:TextBox>
                                            </FooterTemplate>                                
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="Edit" >
                                             <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Add/Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"></asp:LinkButton>
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
                                    <asp:AsyncPostBackTrigger ControlID="dg_ATHDetails" />                        
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
                        <asp:UpdatePanel ID="Upd_Pnl_hdnTotalAdvanceGrid" UpdateMode="Conditional" runat="server" >
                        <ContentTemplate>
                                <asp:HiddenField ID="hdn_ToalAdvanceGrid" runat="server" />   
                                <asp:HiddenField ID="hdn_LHPODate" runat="server" />   
                                <asp:HiddenField ID="hdn_From_BranchId" runat="server" />
                         </ContentTemplate>
                         <Triggers>                    
                            <asp:AsyncPostBackTrigger ControlID="dg_ATHDetails" />                    
                         </Triggers>
                        </asp:UpdatePanel>      
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AlertBranches" />
                                <asp:AsyncPostBackTrigger ControlID="dg_ATHDetails" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="display:none">
                        <asp:HiddenField ID="hdn_FromLocation_Parameter" runat="server" />
                        <asp:HiddenField ID="hdn_Advance_Pay_At_Parameter" runat="server" />
                        <asp:HiddenField ID="hdn_ATH_Grid_Max_Rows" runat="server" />
                        <asp:CheckBox ID="chk_Is_PostBack_On_Advance_Amt" runat="server"/>
                    </td> 
                 </tr>
                <asp:HiddenField ID="hdn_LHPOATHPayDetails" runat="server" />
          </table>
</td>
</tr>
</table>
</td>
</tr>
</table>
<script type="text/javascript" >
SetATHPaybleAlertsBranchesTotalAdvance();
</script>

