<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherCostCentre.ascx.cs" Inherits="Finance_Accounting_Vouchers_VoucherCostCentre" %>
<script type = "text/javascript" src="../../Javascript/Common.js"></script>   
<asp:ScriptManager id = "scm_Voucher" runat = "server"></asp:ScriptManager>

<script type = "text/javascript" >
 
 function Allow_To_Save()
 {
    return true;
 }
 
</script>

<table class = "TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="6" >
                    <asp:Label ID="lbl_Heading" runat="server"  CssClass="HEADINGLABEL" Text="COST-CENTRE DETAILS"></asp:Label></td>
            </tr>
            <tr>
                <td>
                  &nbsp;
                </td>
            </tr>
            
            
            
            <tr>
               
                <td style="width:20%; text-align: right;">
                    Cost-Centre Details For : 
                </td>
              
                <td style ="width:30%" >
                    <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4">&nbsp;</td> 
            </tr>
            
             <tr>
               
                <td style="width:20%; text-align: right;">
                    Upto : 
                </td>
              
                <td style ="width:30%" >
                    <asp:Label ID="lbl_Upto" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>&nbsp;</td> 
            </tr>
            
            <tr>
                <td colspan="6">
                <fieldset id="fld_Grid">
               <legend>Cost Centre Details :</legend>
                 <asp:UpdatePanel ID = "UpdatePanel1" runat = "server" >
                    <ContentTemplate>
                    <asp:DataGrid ID = "dg_CostCentre" runat = "server" style="width:100%" CssClass = "Grid"
                        AutoGenerateColumns="False" OnItemCommand="dg_CostCentre_ItemCommand" OnItemDataBound="dg_CostCentre_ItemDataBound"
                        ShowFooter = "True">
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Cost Centre"  >
                                <HeaderStyle Width="5%" Font-Bold ="True"    />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_CostCentre" runat = "server"
                                        style ="width:100%" Text = '<%#Eval("Cost_Centre_Name")%>'>
                                    </asp:label>
                                </ItemTemplate>
                                
                                <EditItemTemplate>
                                     <asp:DropDownList ID = "ddl_CostCentre" runat = "server"  CssClass = "DROPDOWN"
                                        style ="width:100%">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                
                                <FooterTemplate>
                                     <asp:DropDownList ID = "ddl_CostCentre" runat = "server"  CssClass = "DROPDOWN"
                                        style ="width:100%">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            
                         
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle Width="15%" HorizontalAlign ="Right" Font-Bold ="True"  />
                                <ItemStyle HorizontalAlign = "right" />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_Amount" runat = "server" style ="width:100%;text-align:right;" CssClass = "TEXTBOXNOS"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:TextBox ID = "txt_Amount" runat = "server" style ="width:95%;text-align:right;" CssClass = "TEXTBOXNOS" onkeyup = "valid(this)"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:TextBox>
                                    
                                </EditItemTemplate>
                                <FooterTemplate >
                                    <asp:TextBox ID = "txt_Amount" runat = "server"  style ="width:95%;text-align:right;" CssClass = "TEXTBOXNOS" BorderWidth = "1px"
                                    onkeyup = "valid(this)"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                   
                             <asp:TemplateColumn HeaderText="Add">
                                <HeaderStyle Width="5%"  Font-Bold = "true"  />
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnk_Add" runat="Server" CommandName="Add" Text="Add">
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>       
                            
                            <asp:EditCommandColumn  CancelText="Cancel" EditText="Edit"
                                HeaderText="Edit" UpdateText="Update">
                                <HeaderStyle   Width="5%" Font-Bold = "true" />
                            </asp:EditCommandColumn>
                            
                            <asp:TemplateColumn HeaderText="Delete">
                                <HeaderStyle Width="5%"  Font-Bold = "true"  />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>                                          
                        </Columns>
                    </asp:DataGrid>
                  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "dg_CostCentre" />
                    </Triggers>
                    </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            
            <tr>
                <td colspan="6" style="text-align: center;">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass = "BUTTON" OnClick="btn_Save_Click"/></td>
            </tr>
            
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID = "up_lbl_Errors" runat = "server" >
                    <ContentTemplate >
                        &nbsp;<asp:Label ID = "lbl_Errors" runat = "server" ForeColor = "red" Font-Bold = "true" EnableViewState="false"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                      <%--  <asp:AsyncPostBackTrigger ControlID = "btn_Save" />--%>
                        <asp:AsyncPostBackTrigger ControlID = "dg_CostCentre" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
      </table>