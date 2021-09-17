<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOctroiRecoveryForm.ascx.cs" Inherits="Operations_Octroi_Update_WucOctroiRecoveryForm" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<asp:ScriptManager ID="scm_OctroiRecoveryForm" runat="server"></asp:ScriptManager>

<script type="text/javascript" language="javascript">
    
function Allow_To_Save()
{
    var ATS = true;
     return ATS; 
}
</script>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="7">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="OCTROI RECOVERY FROM" ></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
            <uc2:WucSelectedItems id="WucSelectedItems1" runat="server" />
        </td>
    </tr>
    <tr>                 
        <td >                   
            <asp:Panel ID="pnl_OctroiUpdateRecoveryForm" runat="server" GroupingText="Octroi Update Recovery Details" CssClass="PANEL" Width="100%" >
                <div >
                    <asp:UpdatePanel ID="Upd_pnl_OctroiUpdateRecoveryForm" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                        <asp:DataGrid ID="dg_OctroiUpdateRecoveryForm" runat="server" AutoGenerateColumns="False" 
                        CellPadding="2" CssClass="GRID" style="border-top-style: none" Width="98%" 
                        OnItemDataBound="dg_OctroiUpdateRecoveryForm_ItemDataBound" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS"/>                                                                                                    
                                <Columns>                                                                                                                   
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_GC_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GC_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateColumn>                                                                   
                                    
                                    <asp:TemplateColumn HeaderText=" GC No">
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_GCNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>' CssClass="LABEL"></asp:Label>    
                                        </ItemTemplate> 
                                         <HeaderStyle Width="10%" />                                                              
                                    </asp:TemplateColumn>                                                                  
                                   
                                    <asp:TemplateColumn HeaderText="Booking Date">
                                        <ItemTemplate>
                                          <asp:Label ID="lbl_BookingDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Date") %>' CssClass="LABEL"></asp:Label>                                                                
                                        </ItemTemplate>
                                         <HeaderStyle Width="10%" />                                            
                                    </asp:TemplateColumn>                                                            
                                    <asp:TemplateColumn HeaderText="Booking Branch">
                                        <ItemTemplate>
                                         <asp:Label ID="lbl_BookingBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Branch") %>' />                                                             
                                        </ItemTemplate>
                                          <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Delivery Branch">
                                        <ItemTemplate>
                                         <asp:Label ID="lbl_DeliveryBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Delivery_Branch") %>' />
                                         </ItemTemplate>
                                         <HeaderStyle Width="10%" />                                                                
                                    </asp:TemplateColumn>
                                    
                                   <asp:TemplateColumn HeaderText="Octroi Form Type">
                                       <ItemTemplate>
                                        <asp:Label ID="lbl_OctroiFormType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Octroi_Form_Type") %>' />                                                              
                                       </ItemTemplate>                                                                                                                          
                                         <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>                                        
                                    <asp:TemplateColumn HeaderText="Octroi Receipt No">
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_OctroiReceiptNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Receipt_No") %>' />   
                                          </ItemTemplate> 
                                           <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Octroi Amount">
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_OctroiAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Amount") %>' />
                                        </ItemTemplate>
                                         <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Right" />                                                               
                                    </asp:TemplateColumn> 
                                    
                                   <asp:TemplateColumn HeaderText="Octroi Recovery Form Id " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OctroiRecoveryFormId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Is_Oct_Recovered_From_Consignee") %>' />
                                        </ItemTemplate>
                                       <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Right" />                                                               
                                    </asp:TemplateColumn> 
                                                                                   
                                    <asp:TemplateColumn HeaderText="Octroi Recovery From">
                                        <ItemTemplate>
                                           <asp:DropDownList ID="ddl_OctroiRecoveryForm" runat="server"  CssClass="DROPDOWN">
                                           <asp:ListItem Value="0" >Consignor</asp:ListItem>
                                            <asp:ListItem Value="1">Consignee</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                         <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save" OnClick="btn_Save_Click"/>
        </td>
    </tr>    
</table>