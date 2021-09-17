<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBillAndRecoDetails.ascx.cs" Inherits="Finance_Masters_BillAndRecoDetails" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../JavaScript/Common.js" ></script>
<script type="text/javascript"  language="javascript">
function Allow_To_Save()
{
    return true;
}
</script> 


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<table class="TABLE" width="100%" border="0">
    <tr>
        <td colspan="6" class="TDGRADIENT">
           
            <asp:Label ID="lbl_Heading" Text="LEDGER OPENING" runat="server" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr> 
    <tr>
        <td class="TD1" style="width: 20%">
            Opening Balance :</td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_Opening_Balance" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                MaxLength="18" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD" style="width: 50%">
            <asp:DropDownList ID="ddl_DrCr" runat="server" CssClass="DROPDOWN" Width="11%">
                <asp:ListItem Value="1">Cr</asp:ListItem>
                <asp:ListItem Value="-1">Dr</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
<%--</table>--%>
<tr>
<td colspan="6" style="width: 100%">
<table  border="0" cellpadding="0" cellspacing="0" style="width: 100%" runat="server" id="tbl_BankReco">
    <tr>
        <td class="TD1" style="width: 20%">
            Cleared Amount :</td>
        <td class="TD1" style="width: 29%">
          <asp:TextBox ID="txt_ClearAmount" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                MaxLength="18" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD" style="width: 50%">
            <asp:DropDownList ID="ddl_ClearAmountDrCr" runat="server" CssClass="DROPDOWN" Width="11%">
                <asp:ListItem Value="1">Cr</asp:ListItem>
                <asp:ListItem Value="-1">Dr</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
                 
                <tr>
                    <td style="width: 100%" align="left" colspan="4">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <fieldset>
                                        <legend>Bank Reconcilation</legend>
                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                            <tr>
                                                <td>
                                                  <asp:UpdatePanel ID="update_Bank_Reco" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                    <asp:DataGrid ID="dg_BankReco" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="3" CssClass="Grid" ShowFooter="True" AllowPaging="True" Width="100%" OnItemCommand="dg_BankReco_ItemCommand" OnItemDataBound="dg_BankReco_ItemDataBound" OnPageIndexChanged="dg_BankReco_PageIndexChanged">
                                                         <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <Columns>
                                                        
                                                        
                                                            <asp:TemplateColumn HeaderText="Voucher Date">
                                                                <ItemTemplate>
                                                                    <%#Convert.ToDateTime(Eval("Voucher_Date")).ToString("dd/MM/yyyy")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_VoucherDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                        PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="PICKER"
                                                                        AllowDaySelection="True" AllowMonthSelection="True"  MinDate="1900-01-01" Width="10px" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_VoucherDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                        PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="PICKER"
                                                                        AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="10px" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="15%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Particulars">
                                                                <ItemTemplate>
                                                                    <%#Eval("Particulars")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOX" ID="txt_Particular" MaxLength="50" Width="90%" BorderWidth="1px"
                                                                        Visible="true" runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOX" runat="server" MaxLength="50" Width="90%" BorderWidth="1px"
                                                                        Visible="true" ID="txt_Particular" Text='<%#Eval("Particulars")%>' />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="30%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Cheque No">
                                                                <ItemTemplate>
                                                                    <%#Eval("Cheque_No")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOXNOS" ID="txt_ChequeNo" Width="80%" Columns="2" MaxLength="6"
                                                                        runat="server" BorderWidth="1px" onkeyPress="return Only_Numbers(this,event);"/>
                                                                  </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOXNOS" runat="server" Width="80%" ID="txt_ChequeNo"
                                                                        MaxLength="6" Columns="2" Text='<%#Eval("Cheque_No")%>' BorderWidth="1px" onkeyPress="return Only_Numbers(this,event);" />
                                                                  </EditItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="TD1" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <%#convertToAbs(Eval("Amount"))%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" MaxLength="8" CssClass="TEXTBOXNOS"
                                                                        onkeyPress="return Only_Numbers(this,event);" Visible="true" Width="85%"></asp:TextBox>
                                                                 </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" MaxLength="8"
                                                                        CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" Text='<%#convertToAbs(Eval("Amount"))%>'
                                                                        Width="85%"></asp:TextBox>
                                                                 </EditItemTemplate>
                                                                <HeaderStyle Width="15%" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <ItemStyle CssClass="TD1" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Cr/Dr">
                                                                <ItemTemplate>
                                                                    <%#convertToDrCr(Eval("Amount"))%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_DrCr" runat="server" Visible="true" Width="92%" Font-Names="Verdana"
                                                                        Font-Size="11px">
                                                                        <asp:ListItem Value="1">Cr</asp:ListItem>
                                                                        <asp:ListItem Value="-1">Dr</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_DrCr" runat="server" SelectedValue='<%#Math.Sign(Convert.ToDecimal(Eval("Amount")))%>'
                                                                        Width="92%" Font-Names="Verdana" Font-Size="11px">
                                                                        <asp:ListItem Value="1">Cr</asp:ListItem>
                                                                        <asp:ListItem Value="-1">Dr</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Row No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Row_No" runat="server" CssClass="LABEL" Text='<%#Eval("RowNo")%>'></asp:Label>
                                                                    <%#Eval("RowNo")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lbl_Row_No" runat="server" CssClass="LABEL" Text='<%#Eval("RowNo")%>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update"
                                                              >
                                                                <HeaderStyle Width="10%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Add/Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnk_Add" runat="Server" CommandName="Add" Text="Add"></asp:LinkButton>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete" Enabled="false"></asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="dg_BankReco" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                                                    </Triggers>
                                                    </asp:UpdatePanel>
                                                    </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>

        </td>
    </tr>
</table>
</td>
</tr>
<tr>
<td colspan="6" style="width: 100%">
<table  border="0" cellpadding="0" cellspacing="0" style="width: 100%" runat="server" id="tbl_BillWise">
    <tr>
        <td style="width: 99%;">
            <table  width="100%">
                <tr>
                    <td style="width: 100%" align="left">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <fieldset>
                                       <legend>BillWise Details</legend>
                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                            <tr>
                                                <td>
                                                <asp:UpdatePanel ID="UPdate_Bill_Details" UpdateMode="Conditional" runat="server" >
                                                <ContentTemplate>
                                                <asp:DataGrid ID="dg_BillWise" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="3" CssClass="Grid" ShowFooter="True" ShowHeader="true" AllowPaging="True"
                                                        Width="100%" OnItemCommand="dg_BillWise_ItemCommand" PageSize="15"
                                                        OnItemDataBound="dg_BillWise_ItemDataBound" OnPageIndexChanged="dg_BillWise_PageIndexChanged">
                                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                        <PagerStyle CssClass="GRIDVIEWPAGERCSS" />
                                                        <PagerStyle Mode="NumericPages" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <Columns>
                                                             <asp:TemplateColumn HeaderText="Ref Type">
                                                                <HeaderStyle width="15%" />
                                                                
                                                                <ItemTemplate >
                                                                    <%#Eval("Ref_Type") %>
                                                                </ItemTemplate>
                                                                        
                                                                <EditItemTemplate >
                                                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN" style ="width:96%">
                                                                    <asp:ListItem Text="New Ref" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="On Account" Value="4"></asp:ListItem>
                                                                  </asp:DropDownList>
                                                                </EditItemTemplate> 
                                                                
                                                                <FooterTemplate >
                                                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN" style ="width:96%">
                                                                    <asp:ListItem Text="New Ref" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="On Account" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                
                                                            </asp:TemplateColumn>
                            
                                                            <asp:TemplateColumn HeaderText="Bill Date">
                                                                <ItemTemplate>
                                                                 <%#Convert.ToDateTime(Eval("Bill_Date")).ToString("dd/MM/yyyy")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_BillDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                        PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="PICKER"
                                                                        AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="10px" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_BillDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                        PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="PICKER"
                                                                        AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="10px" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="15%" />
                                                            </asp:TemplateColumn>
                                                              
                                                            <asp:TemplateColumn HeaderText="Bill Name">
                                                                <ItemTemplate>
                                                                    <%#Eval("Ref_No")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOX" Width="90%" BorderWidth="1px" Visible="true" MaxLength="100"
                                                                        ID="txt_BillName" runat="server" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOX" Width="90%" BorderWidth="1px" runat="server" MaxLength="100"
                                                                        ID="txt_BillName" Text='<%#Eval("Ref_No")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="30%" />
                                                            </asp:TemplateColumn>
                                                           
                                                            <asp:TemplateColumn HeaderText="Credit Days">
                                                                <ItemTemplate>
                                                                    <%#Eval("Credit_Days")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOXNOS" Width="90%" Visible="true" MaxLength="8" BorderWidth="1px"
                                                                        ID="txt_CreditDays" runat="server" onkeyPress="return Only_Numbers(this,event);" />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox CssClass="TEXTBOXNOS" Width="90%" BorderWidth="1px" MaxLength="8" runat="server"
                                                                        ID="txt_CreditDays" onkeyPress="return Only_Numbers(this,event);" Text='<%#Eval("Credit_Days") %>' />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="12%" HorizontalAlign="Right" />
                                                                <ItemStyle CssClass="TD1" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <%#convertToAbs(Eval("Amount"))%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" MaxLength="8" CssClass="TEXTBOXNOS"
                                                                        onkeyPress="return Only_Numbers(this,event);" Visible="true" Width="85%"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" MaxLength="8"
                                                                        CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" Text='<%#convertToAbs(Eval("Amount"))%>'
                                                                        Width="85%"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="15%" CssClass="TD1" />
                                                                <ItemStyle CssClass="TD1" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Cr/Dr">
                                                                <ItemTemplate>
                                                                   <%#convertToDrCr(Eval("Amount"))%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_DrCr" runat="server" Visible="true" Width="92%" Font-Names="Verdana"
                                                                        Font-Size="11px">
                                                                        <asp:ListItem Value="1">Cr</asp:ListItem>
                                                                        <asp:ListItem Value="-1">Dr</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_DrCr" runat="server" SelectedValue='<%#Math.Sign(Convert.ToDecimal(Eval("Amount")))%>'
                                                                        Width="92%" Font-Names="Verdana" Font-Size="11px">
                                                                        <asp:ListItem Value="1">Cr</asp:ListItem>
                                                                        <asp:ListItem Value="-1">Dr</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Row No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Row_No" runat="server" CssClass="LABEL" Text='<%#Eval("RowNo")%>'></asp:Label>
                                                                    <%#Eval("RowNo")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lbl_Row_No" runat="server" CssClass="LABEL" Text='<%#Eval("RowNo")%>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Add/Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnk_Add" runat="Server" CommandName="Add" Text="Add"></asp:LinkButton>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete" Enabled="false"></asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="dg_BillWise" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</td>
</tr>

 <tr  runat="server" id=tr_Total>
        <td style="width: 20%;" align="right">
          <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" Text="TOTAL :"></asp:Label>
        </td>
         <td  align="Left" style="width: 29%;">
                 
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  ><ContentTemplate>
                           <asp:Label ID="lbl_Total" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="dg_BillWise" />
                           <asp:AsyncPostBackTrigger ControlID="dg_BankReco" />
                           <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                       </Triggers>
                       </asp:UpdatePanel>     
            </td>
            <td style="width: 51%;" colspan="4">
            
            </td>
         </tr>
         
         <tr>
                    <td style="width: 100%;" colspan="6">
                       <asp:UpdatePanel ID="update_Bill_Label" runat="server" UpdateMode="Conditional"  ><ContentTemplate>
                        <asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True"
                            ForeColor="Red"  EnableViewState="false"></asp:Label>
                       </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="dg_BillWise" />
                           <asp:AsyncPostBackTrigger ControlID="dg_BankReco" />
                          <%-- <asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                       </Triggers>
                       </asp:UpdatePanel>     
                    </td>
                </tr>
                
                <tr>
        <td class="TD1" style="text-align: center;" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save"     OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
    <td>
    <asp:UpdatePanel ID="up_HiddenField" runat="server" UpdateMode="Always">
          <ContentTemplate>
                <asp:HiddenField ID="hdn_Row_No" runat="server" />
          </ContentTemplate>
         </asp:UpdatePanel>
    </td>
    </tr>
</table>
        