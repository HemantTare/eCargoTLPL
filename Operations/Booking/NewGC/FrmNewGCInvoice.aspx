<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCInvoice.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/Common.js"></script>
    <script type="text/javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scm_Invoice"></asp:ScriptManager>
    <div>
    <table class="TABLE" border="0">
            <tr>
               <td style="width:100%" colspan="2">
                 <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Invoice">
                    <ContentTemplate>
                     <div id="Div_Invoice" runat="server" class="DIV" style="height:100px;text-align:left">
                        <asp:DataGrid ID="dg_Invoice" runat="server" Width="96%" CssClass="Grid" AutoGenerateColumns="False" ShowFooter="True"
                            CellPadding="3" OnCancelCommand="dg_Invoice_CancelCommand" OnDeleteCommand="dg_Invoice_DeleteCommand" 
                            OnEditCommand="dg_Invoice_EditCommand" OnItemCommand="dg_Invoice_ItemCommand" OnItemDataBound="dg_Invoice_ItemDataBound" 
                            OnUpdateCommand="dg_Invoice_UpdateCommand">
                            <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                            <Columns>                               
                                <asp:TemplateColumn HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left">
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_Invoice_No" MaxLength="20" 
                                            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>'
                                             MaxLength="20"  ID="txt_Invoice_No" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>                                                                            
                                 <asp:TemplateColumn HeaderText="Chalan No" HeaderStyle-HorizontalAlign="Left">
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_Chalan_No" MaxLength="20" 
                                            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Chalan_No")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "Chalan_No") %>'
                                             MaxLength="20" ID="txt_Chalan_No" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="BE / BL No." HeaderStyle-HorizontalAlign="Left">
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_BE_BL_No"
                                             onkeyPress="return Only_Numbers(this,event);" MaxLength="20" 
                                                onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "BE_BL_No") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Width="80%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "BE_BL_No") %>'
                                            ID="txt_BE_BL_No" MaxLength="20" onkeyPress="return Only_Numbers(this,event);" 
                                            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Invoice Amount">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Invoice_Amount"
                                             MaxLength="10" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Invoice_Amount")) %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20%" HorizontalAlign="Right"></HeaderStyle>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Invoice_Amount"))%>'
                                            ID="txt_Invoice_Amount" MaxLength="10"  onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update" EditText="Edit">
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Delete">
                                    <FooterTemplate>
                                        <asp:LinkButton runat="server" ID="lbtn_Add_Invoice" Text="Add" CommandName="Add"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtn_Delete_Invoice" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                     </div> 
                   </ContentTemplate>
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Invoice"/>
                      </Triggers>
                  </asp:UpdatePanel>
                 </td>
            </tr> 
            <tr>
                <td>
                 <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Invamt">
                    <ContentTemplate> 
                    <table width="100%" border="0">
                         <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lbl_invoiceErrorMsg" runat="server" CssClass="LABELERROR"></asp:Label>
                            </td> 
                            <td style="width: 18%" class="TD1">
                                <asp:Label Style="text-align: right" ID="lbl_InvoiceTotal" runat="server" CssClass="LABEL" Text="Invoice Total :" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 20%" align="center">
                                <asp:Label ID="lbl_TotalInvoiceAmount" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalInvoiceAmount" runat="server"></asp:HiddenField>
                            </td>
                            <td style="width: 10%" align="left"></td>
                        </tr>
                    </table>
                     </ContentTemplate>
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Invoice"/>
                      </Triggers>
                   </asp:UpdatePanel>
                </td>
                <td style="display:none">
                    <asp:CheckBox runat="server" ID="chk_Is_Invoice_Amount_Required" />
                </td> 
            </tr>
        </table>
    </div>
 </form>
</body>
</html>
<script type="text/javascript">
 function updateparentdataset(InvoiceAmount)
 { 
  window.parent.call_InvoiceDetails(InvoiceAmount);
 } 
</script>