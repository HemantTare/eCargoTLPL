<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPartyRecieptVoucher.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucPartyRecieptVoucher" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script type = "text/javascript" src="../../Javascript/Common.js" ></script>   
<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/PartyReceiptVoucher.js"></script>

<script type="text/javascript">

         function Allow_To_Save()
         {
            return true;
         }
         
         function OpenPopup(Path)
         {

            var w = screen.availWidth;
            var h = screen.availHeight;
            var popW = (w-200);
            var popH = (h-200);
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
                
            window.open(Path, 'MainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
            return false;
        }
      
</script>
                  
        <asp:ScriptManager id = "scm_Voucher" runat = "server"></asp:ScriptManager>
        
        <table  style="width:100%; border-top-style: solid; border-top-color: black;" class = "TABLE" width="100%">
        
            <tr>
                <td class = "TDGRADIENT" colspan="6">
                    <asp:Label ID = "Label1" runat = "server" Text="PARTY RECIEPT VOUCHER" CssClass="HEADINGLABEL"></asp:Label>
                </td>
            </tr>
        
          <tr>
                <td style="width:20%" class="TD1">
                <asp:Label ID="lbl_VoucherNo" runat="server" CssClass="LABEL" Text="Voucher No :"></asp:Label>
                </td>
                <td style="width:29%">
                  <asp:TextBox ID="txt_VoucherNo" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="70%"></asp:TextBox>
                </td>
                <td style="width:1%">
                </td>
                <td style="width:20%" class="TD1">
                <asp:Label ID="lbl_VoucherDate" runat="server" CssClass="LABEL" Text="Voucher Date :"></asp:Label>
                </td>
                <td style="width:29%">
                <uc1:WucDatePicker ID="dtp_VoucherDate" runat="server" IsAutoPostBack="false" OnDateSelectionChanged="On_VoucherDateChange" />
                </td>
                <td style="width:1%">
                </td>
          </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_RefNo" runat="server" CssClass="LABEL" Text="Ref No :"></asp:Label>
                </td>
                <td style="width: 29%">
                <asp:TextBox ID="txt_RefNo1" runat="server" CssClass="TEXTBOX" MaxLength="20" Width="70%"></asp:TextBox>
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_CashBankLedger" runat="server" CssClass="LABEL" Text="Cash/Bank Ledger :"></asp:Label>
                </td>
                <td style="width: 29%; font-weight:bold">
                 <cc1:DDLSearch ID="ddl_CashBankLedger" runat="server" AllowNewText="false" IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyRecieptVoucherCashBankLedger" ></cc1:DDLSearch>
                 (Dr)<asp:Label ID="lbl_mancashbank" runat="server" CssClass="LABEL" Text="  *" Width="7%" ForeColor="red"></asp:Label>
                </td>
                <td style="width: 1%" class="TDMANDATORY">
                </td>
                <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_AmountReceived" runat="server" CssClass="LABEL" Text="Amount Recieved :"></asp:Label>
                </td>
                <td style="width: 29%">
                <asp:TextBox ID="txt_AmountReceived" runat="server" CssClass="TEXTBOXNOS" Width="70%" onkeypress="return Only_Numbers(this,event)" MaxLength="15"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" CssClass="LABEL" Text="  *" Width="7%" ForeColor="red"></asp:Label>
                </td>
                <td style="width: 1%" class="TDMANDATORY">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ChequeNo" runat="server" CssClass="LABEL" Text="Cheque No :"></asp:Label>
                </td>
                <td style="width: 29%">
                <asp:TextBox ID="txt_ChequeNo" runat="server" CssClass="TEXTBOX" Width="70%" MaxLength="10" onkeypress="return Only_Numbers(this,event)" ></asp:TextBox>
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date :"></asp:Label>
                </td>
                <td style="width: 29%">
                <uc1:WucDatePicker ID="dtp_ChequeDate" runat="server" />
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ChequeBank" runat="server" CssClass="LABEL" Text="Cheque Bank :"></asp:Label>
                </td>
                <td style="width: 29%">
                <asp:TextBox ID="txt_ChequeBank" runat="server" CssClass="TEXTBOX" Width="86%" MaxLength="100"></asp:TextBox>
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ClientLedger" runat="server" CssClass="LABEL" Text="Client Ledger :"></asp:Label>
                </td>
                <td style="width: 29%;font-weight:bold">
            <%--    <asp:UpdatePanel ID="up_Client" runat="server">
                <ContentTemplate>--%>
                    <cc1:DDLSearch ID="ddl_ClientLedger" runat="server" AllowNewText="false" IsCallBack="true" PostBack="true" OnTxtChange="ddl_ClientLedger_OnSelectedIndexChanged" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyRecieptVoucherClientLedger"></cc1:DDLSearch>
                    (Cr)<asp:Label ID="Label2" runat="server" CssClass="LABEL" Text="  *" Width="7%" ForeColor="red"></asp:Label>
             <%--   </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_ClientLedger" />
                </Triggers>
                </asp:UpdatePanel>--%>
                
                </td>
                <td style="width: 1%" class="TDMANDATORY">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            
            <tr>
            <td colspan="6">
            
 <%--           <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>--%>
                
            <%--<table id="Table_Grid" runat="server" width="100%">
            <tr >
                <td >--%>
                <%--<asp:UpdatePanel ID="up_BillGrid" runat="server" >
                <ContentTemplate>--%>
                <table id="Table_Grid" runat="server" width="100%">
            <tr >
                <td >
               <fieldset id="fld_Grid">
               <legend><asp:Label ID="lbl_BillLegend" runat="server" CssClass="LABEL"></asp:Label> </legend>
               <asp:DataGrid  ID = "dg_BillGrid" runat = "server"
                        CssClass = "Grid"
                        AutoGenerateColumns="False" 
                        ShowFooter = "True" OnCancelCommand="dg_BillGrid_CancelCommand" OnDeleteCommand="dg_BillGrid_DeleteCommand" OnEditCommand="dg_BillGrid_EditCommand" OnItemDataBound="dg_BillGrid_ItemDataBound" OnUpdateCommand="dg_BillGrid_UpdateCommand" 
                        OnItemCommand="dg_BillGrid_ItemCommand" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Ref Type">
                                <HeaderStyle width="12%" />
                                
                                <ItemTemplate >
                                    <%#Eval("Ref_Type")%>
                                </ItemTemplate>
                                        
                                <EditItemTemplate >
                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN" OnSelectedIndexChanged="ddl_RefType_OnSelectedIndexChanged"
                                        style ="width:96%" AutoPostBack="true" >
                                    </asp:DropDownList>
                                </EditItemTemplate> 
                                
                                <FooterTemplate >
                                    <asp:DropDownList ID="ddl_RefType" runat="server"  CssClass = "DROPDOWN" OnSelectedIndexChanged="ddl_RefType_OnSelectedIndexChanged"
                                        style ="width:96%" AutoPostBack="true" 
                                 ></asp:DropDownList>
                                </FooterTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Bill No">
                                <HeaderStyle Width="15%"  />
                                
                                <ItemTemplate >
                                    <%#Eval("Bill_No")%>
                                </ItemTemplate>
                                                          
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_RefNo" runat="server" CssClass = "TEXTBOX"  BorderWidth = "1px" MaxLength="50" 
                                    style ="width:90%"></asp:TextBox>
                                    <cc1:DDLSearch ID="ddl_RefNo" runat="server" AllowNewText="false" InjectJSFunction="SetBillValues" IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyReceiptSearchBillNo"></cc1:DDLSearch>
                                </FooterTemplate>
                                
                                <EditItemTemplate>
                                   <asp:TextBox ID="txt_RefNo" runat="server" CssClass = "TEXTBOX"  BorderWidth = "1px" MaxLength="50"  
                                    style ="width:90%"></asp:TextBox>
                                    <cc1:DDLSearch ID="ddl_RefNo" runat="server" AllowNewText="false" PostBack="true" OnTxtChange="ddl_RefNo_OnSelectedIndexChanged" IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyReceiptSearchBillNo"></cc1:DDLSearch>
                                </EditItemTemplate>
                                                           
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Bill Date">
                                <HeaderStyle width="10%" />
                                                              
                                <ItemTemplate >
                                    <%#Eval("Bill_Date")%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                   <asp:Label ID="lbl_BillDate" runat="server" CssClass="LABEL"></asp:Label>
                                </FooterTemplate>
                                
                                <EditItemTemplate>
                                   <asp:Label ID="lbl_BillDate" runat="server" CssClass="LABEL"></asp:Label>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Bill Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Bill_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_BillAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="15"
                                        style ="width:85%;text-align :right " onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_BillAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="15"
                                        style ="width:85%;text-align :right" onkeypress="return Only_Numbers(this,event)"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Bill_Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Pending Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" Font-Bold="true" />
                                <FooterStyle HorizontalAlign = "right" />
                                
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Pending_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                   <%-- <asp:TextBox ID="txt_PendingAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="9"
                                        style ="width:85%;text-align :right " onkeyup = "valid(this)"></asp:TextBox>--%>
                                        
                                        <asp:Label ID="lbl_PendingAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                   <%--  <asp:TextBox ID="txt_PendingAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="9"
                                        style ="width:85%;text-align :right" onkeyup = "valid(this)" 
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Pending_Amount")))%>'></asp:TextBox>--%>
                                        
                                        <asp:Label ID="lbl_PendingAmount" runat="server" CssClass="LABEL" Font-Bold="true" Text='<%#Math.Abs(Convert.ToDecimal(Eval("Pending_Amount")))%>'></asp:Label>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                                                 
                            <asp:TemplateColumn HeaderText="Received Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Received_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_ReceivedAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="15" onblur="Calculate_AdjustedAmount(this)"
                                        style ="width:85%;text-align :right " onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_ReceivedAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="15" onblur="Calculate_AdjustedAmount(this)"
                                        style ="width:85%;text-align :right" onkeypress="return Only_Numbers(this,event)"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Received_Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="TDS Deduction Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("TDS_Deduction_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_TDSAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="15"
                                        style ="width:85%;text-align :right " onkeypress="return Only_Numbers(this,event)" onblur="Calculate_AdjustedAmount(this)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_TDSAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="15"
                                        style ="width:85%;text-align :right" onkeypress="return Only_Numbers(this,event)" onblur="Calculate_AdjustedAmount(this)"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("TDS_Deduction_Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Frieght Deduction Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Frieght_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_FrieghtAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="15" onblur="Calculate_AdjustedAmount(this)"
                                        style ="width:85%;text-align :right " onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_FrieghtAmount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="15" onblur="Calculate_AdjustedAmount(this)"
                                        style ="width:85%;text-align :right" onkeypress="return Only_Numbers(this,event)"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Frieght_Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                           <asp:TemplateColumn HeaderText="Adjustment Amount">
                                <HeaderStyle width="13%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" Font-Bold="true" />
                                <FooterStyle HorizontalAlign = "right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Adjustment_Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:Label ID="lbl_AdjustmentAmount" runat="server" CssClass = "LABEL" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:Label ID="lbl_AdjustmentAmount" runat="server" CssClass = "LABEL" Font-Bold="true"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Adjustment_Amount")))%>'></asp:Label>
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
                         </fieldset>
                         </td>
                </tr>
                </table>
                <%-- </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_BillGrid" />
                <asp:AsyncPostBackTrigger ControlID="ddl_ClientLedger" />
                <asp:AsyncPostBackTrigger ControlID="dtp_VoucherDate" />
                </Triggers>
                </asp:UpdatePanel>--%>
               
               <%-- </td>
                </tr>
                </table>--%>
                       
<%--                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_ClientLedger" />
                </Triggers>
                </asp:UpdatePanel>--%>
     
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6">
               <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>--%>
                    <table id="Table_Amount" runat="server" width="100%">
                     <tr>
                        <td class="TD1" style="width: 20%">
                        </td>
                        <td style="width: 29%">
                        </td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 20%;text-align:right;font-weight:bold">Total Adjusted Amount :
                        </td>
                        <td style="width: 29%" align="right">
                            <asp:TextBox ID="txt_TotalAdjustedAmount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="70%"></asp:TextBox>
                        </td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                    </table>
               <%-- </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_ClientLedger" />
                </Triggers>
                </asp:UpdatePanel>--%>
            </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td align="right" style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td align="right" style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td align="right" style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="6">
                <fieldset id="Fieldset1">
               <legend>Other Deduction Ledgers (Dr):</legend>
                <table border="0" width="100%">
                  <tr>
                   <td>
                <asp:UpdatePanel ID="up_OtherGrid" runat="server">
                <ContentTemplate>
            
               <asp:DataGrid ID = "dg_OtherGrid" runat = "server"
                        CssClass = "Grid"
                        AutoGenerateColumns="False" 
                        ShowFooter = "True" OnItemCommand="dg_OtherGrid_ItemCommand" OnItemDataBound="dg_OtherGrid_ItemDataBound" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            
                            <asp:TemplateColumn HeaderText="Other Deduction Ledger">
                                <HeaderStyle Width="60%" HorizontalAlign="Left"/>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Left" />
                                <ItemTemplate >
                                    <%#Eval("OtherLedgerName")%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                   <cc1:DDLSearch ID="ddl_OtherDeductionLedger" runat="server" AllowNewText="false"  IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyRecieptVoucherOtherDeductionLedger" OnTxtChange="ddl_OtherDeductionLedger_TxtChange"></cc1:DDLSearch>
                                </FooterTemplate>
                                
                                <EditItemTemplate>
                                   <cc1:DDLSearch ID="ddl_OtherDeductionLedger"  runat="server" AllowNewText="false"  IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPartyRecieptVoucherOtherDeductionLedger" OnTxtChange="ddl_OtherDeductionLedger_TxtChange"></cc1:DDLSearch>
                                </EditItemTemplate>
                                
                                
                            </asp:TemplateColumn>
                            
                           
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle width="20%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Amount")))%>
                                </ItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px" MaxLength="15"
                                        style ="width:85%;text-align :right " onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                </FooterTemplate>
                                
                                <EditItemTemplate >
                                     <asp:TextBox ID="txt_Amount" runat="server" CssClass = "TEXTBOX" BorderWidth = "1px"  MaxLength="15"
                                        style ="width:85%;text-align :right" onkeypress="return Only_Numbers(this,event)"
                                        text = '<%#Math.Abs(Convert.ToDecimal(Eval("Amount")))%>'></asp:TextBox>
                                </EditItemTemplate>
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Bill By Bill"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "True"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_BillbyBill"  runat = "server" Text = "BillByBill" Visible='<%#Convert.ToBoolean(Eval("IsBillByBill"))%>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Cost Centre"  >
                                <HeaderStyle Width = "10%" Font-Bold = "True"  />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_CostCentre"  runat = "server" Text = "CostCentre" Visible='<%#Convert.ToBoolean(Eval("IsCostCentre"))%>'></asp:LinkButton>
                                </ItemTemplate>
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
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_OtherGrid" />
                </Triggers>
                </asp:UpdatePanel>
                </td>
                </tr>
                </table>
                </fieldset>
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="6">
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="6">
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="6">
                </td>
            </tr>
          
          <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td style="width: 20%;text-align:right;font-weight:bold">Total Deduction :
                </td>
                <td align="right" style="width: 29%">
                <asp:UpdatePanel ID="up_Other" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_TotalDeduction" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="70%"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_OtherGrid" />
                </Triggers>
                </asp:UpdatePanel>
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td align="right" style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                </td>
                <td align="right" style="width: 29%">
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
              <td style="width:20%; text-align: right; vertical-align:top">Narration :</td>
              <td colspan = "4" >
                 <asp:TextBox ID = "txt_Narration" runat = "server"
                   TextMode = "MultiLine" CssClass = "TEXTBOX" BorderWidth = "1px" Width = "98%" Height="60px" MaxLength="200"></asp:TextBox>
                   &nbsp;</td>
              <td style="width: 1%" class="TDMANDATORY"></td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: right">
                </td>
                <td colspan="4">
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: right">
                </td>
                <td colspan="4">
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: right">
                </td>
                <td colspan="4">
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: right">
                </td>
                <td colspan="4">
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="left">
                
                <asp:UpdatePanel ID="up_Error" runat="server">
                <ContentTemplate>
                    <asp:Label ID = "lbl_Errors" runat = "server" ForeColor = "red" Font-Bold = "true" EnableViewState="false" Text="Fields Mark With * are Mandatory"></asp:Label>
                </ContentTemplate>
              <%--  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>--%>
                </asp:UpdatePanel>
                    
                </td>
            </tr>
          
        </table>
        
   <asp:HiddenField ID="hdf_GridControlID" runat="server" />

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