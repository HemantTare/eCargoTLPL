<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherBillByBill.ascx.cs" Inherits="Finance_Accounting_Vouchers_VoucherBillByBill" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>
<script type = "text/javascript" src="../../Javascript/Common.js" ></script>   

<script type = "text/javascript" >
 
 function Allow_To_Save()
 {
    return true;
 }
 
</script>


    <asp:ScriptManager ID = "scm_BillByBill" runat = "server"></asp:ScriptManager>
    
        <table class="TABLE" style="width:100%">
            <tr>
                <td class = "TDGRADIENT" colspan="6">
                    <asp:Label ID = "lbl_heading" runat = "server" Text = "BILL-WISE DETAILS" CssClass="HEADINGLABEL"></asp:Label>
                </td>
            </tr>
            <tr>
            <td style="width:20%" class="TD1">
               Bill-Wise Details For :
            </td>
                <td colspan="5" style="width:30%">
                 <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            
            
            <tr>
            <td style="width:20%" class="TD1">
               Upto :
            </td>
                <td colspan="5" style="width:30%">
                 <asp:Label ID="lbl_Upto" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
             
            <tr>
               <td colspan="6">
               <fieldset id="fld_Grid">
               <legend>Bill Details :</legend>
                 <asp:UpdatePanel ID = "UpdatePanel1" runat = "server"  >
                        <ContentTemplate >
                      <asp:DataGrid ID = "dg_BillByBill" runat = "server"
                        CssClass = "Grid"
                        AutoGenerateColumns="False" 
                        ShowFooter = "True" OnItemCommand="dg_BillByBill_ItemCommand" OnItemDataBound="dg_BillByBill_ItemDataBound" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Ref Type">
                                <HeaderStyle width="15%" />
                                
                                <ItemTemplate >
                                    <%#Eval("Ref_Type") %>
                                </ItemTemplate>
                                        
                                <EditItemTemplate >
                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN"
                                    style ="width:96%" AutoPostBack="true" OnSelectedIndexChanged="ddl_RefType_OnSelectedIndexChanged">
                                  </asp:DropDownList>
                                </EditItemTemplate> 
                                
                                <FooterTemplate >
                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN"
                                    style ="width:96%" AutoPostBack="true" OnSelectedIndexChanged="ddl_RefType_OnSelectedIndexChanged"
                                 ></asp:DropDownList>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Name">
                                <HeaderStyle Width="20%"  />
                                
                                <ItemTemplate >
                                    <%#Eval("Ref_No")%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Name" runat="server" CssClass = "TEXTBOX"  BorderWidth = "1px" MaxLength="50" 
                                    style ="width:90%"></asp:TextBox>
                                    <cc1:DDLSearch ID="ddl_Name" runat="server" CallBackAfter="2" PostBack="true" OnTxtChange="ddl_Name_OnSelectedIndexChanged"  IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchBillNo"></cc1:DDLSearch>
                                </FooterTemplate>
                                
                                <EditItemTemplate>
                                   <asp:TextBox ID="txt_Name" runat="server" CssClass = "TEXTBOX"  BorderWidth = "1px" MaxLength="50"  
                                    style ="width:90%"></asp:TextBox>
                                    <cc1:DDLSearch ID="ddl_Name" runat="server" CallBackAfter="2" IsCallBack="true" PostBack="true" OnTxtChange="ddl_Name_OnSelectedIndexChanged" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchBillNo"></cc1:DDLSearch>
                                </EditItemTemplate>
                                
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Credit Days">
                                <HeaderStyle width="10%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                
                                <ItemTemplate >
                                    <%#Eval("Credit_Days") %>
                                </ItemTemplate>
                                
                                <FooterTemplate >
                                    <asp:TextBox ID="txt_CreditDays" runat="server" CssClass = "TEXTBOXNOS"   BorderWidth = "1px" MaxLength="4"
                                    style ="width:96%;text-align:right;" onkeyup = "valid(this)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >

                                    <asp:TextBox ID="txt_CreditDays" runat="server" CssClass = "TEXTBOXNOS"  BorderWidth = "1px"  MaxLength="4"
                                    style ="width:96%;text-align:right" onkeyup = "valid(this)"
                                    text = '<%#Eval("Credit_Days") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle width="15%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="9"
                                        style ="width:85%;text-align :right " onkeyup = "valid(this)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_Amount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="9"
                                        style ="width:85%;text-align :right" onkeyup = "valid(this)" 
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="TDS Ledger">
                                <HeaderStyle width="30%" />
                                
                                <ItemTemplate >
                                    <%#Eval("TDS_Ledger_Name") %>
                                </ItemTemplate>
                                        
                                <EditItemTemplate >
                                    <asp:DropDownList ID="ddl_TDSLedger" runat="server"  CssClass = "DROPDOWN"
                                    style ="width:96%" >
                                  </asp:DropDownList>
                                </EditItemTemplate> 
                                
                                <FooterTemplate >
                                    <asp:DropDownList ID="ddl_TDSLedger" runat="server"  CssClass = "DROPDOWN"
                                    style ="width:96%" 
                                 ></asp:DropDownList>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            
                            
                            <asp:TemplateColumn HeaderText="Cr/Dr">
                                <HeaderStyle width="10%" />
                                
                                <ItemTemplate >
                                    <%#convertToDrCr(Eval("Amount"))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_DrCr" runat="server" CssClass = "DROPDOWN">
                                        <asp:ListItem Value = "Cr" >Cr</asp:ListItem>
                                        <asp:ListItem Value = "Dr" >Dr</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_DrCr" runat="server" CssClass = "DROPDOWN"
                                     SelectedValue='<%#convertToDrCr(Eval("Amount"))%>'>
                                     <asp:ListItem Value = "Cr" >Cr</asp:ListItem>
                                     <asp:ListItem Value = "Dr" >Dr</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                                <HeaderStyle Width = "5%" />
                            </asp:EditCommandColumn>
                            
                              <asp:TemplateColumn >
                                <HeaderStyle Width = "5%" />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_Delete" runat = "server" Text = "Delete" CommandName = "Delete" >
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate >
                                    <asp:LinkButton ID = "lnk_Delete" runat = "server" Text = "Delete" Visible="false"  CommandName = "Delete" >
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate >
                                    <asp:LinkButton ID = "lnk_Add" runat ="server" Text = "Add" 
                                    CommandName = "Add" ></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            
                            
                        </Columns>
                    </asp:DataGrid>
                       </ContentTemplate>
                        <Triggers >
                            <asp:AsyncPostBackTrigger ControlID = "dg_BillByBill" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="3" width="55%" align="right">
                <asp:Label ID="lblTotalAmountText" runat="server" Text="Total Amount:" CssClass="LABEL" Font-Bold="True"></asp:Label>
                </td>
                <td align="right"   width="18%">
                  <asp:UpdatePanel ID = "Upd_Pnl_TotalAmount" runat = "server"  >
                        <ContentTemplate >                      
                <asp:Label ID="lblTotalAmount" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"></asp:Label>
                </ContentTemplate>
                        <Triggers >
                            <asp:AsyncPostBackTrigger ControlID = "dg_BillByBill" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td width="27%" align="left" >
                 <asp:UpdatePanel ID = "UpdatePanel2" runat = "server"  >
                        <ContentTemplate >  
                <asp:Label ID="lblDrCr" runat="server" Text=" Cr" CssClass="LABEL" Font-Bold="True"></asp:Label>
                </ContentTemplate>
                        <Triggers >
                            <asp:AsyncPostBackTrigger ControlID = "dg_BillByBill" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                
            </tr>
            <tr>
                <td style="height: 15px; text-align: center" colspan="5" >
                    <asp:Button ID="btn_Save"  
                    runat="server" Text="Save" CssClass = "BUTTON" OnClick="btn_Save_Click"/></td>
            </tr>
            <tr>
                <td colspan = "5">
                    <asp:UpdatePanel ID = "up_lblerrors" runat = "server"  >
                        <ContentTemplate >
                            <asp:Label ID = "lbl_Errors" runat = "server" ForeColor = "red" Font-Bold = "true" EnableViewState="false"></asp:Label>
                        </ContentTemplate>
                        <Triggers >
                           <%-- <asp:AsyncPostBackTrigger ControlID = "btn_Save" />--%>
                             <asp:AsyncPostBackTrigger ControlID = "dg_BillByBill" />
                        </Triggers>
                    </asp:UpdatePanel>
                 </td>
            </tr>            
            
        </table>
  