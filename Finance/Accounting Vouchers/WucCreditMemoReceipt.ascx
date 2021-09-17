<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCreditMemoReceipt.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucCreditMemoReceipt" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
    <%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
    <script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Finance/Accounting Vouchers/CreditMemoReceipt.js"></script>
<asp:ScriptManager ID="scm_CreditMemoReceipt" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td  colspan="6" class="TDGRADIENT" >
            <asp:Label ID="lbl_heading_name" runat="server" CssClass="HEADINGLABEL" Font-Bold="True" Text="CREDIT MEMO RECEIPT"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ReceiptNo" runat="server" CssClass="LABEL" Text="Receipt No.:"></asp:Label></td>
        <td style="width: 29%">
            <asp:Label ID="lbl_ReceiptNoValue" runat="server" CssClass="LABEL" Font-Bold="True"
                Text="Label"></asp:Label></td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DateofReceipt" runat="server" CssClass="LABEL" Text="Date of Receipt:"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_ReceiptDate" runat="server" />
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_PartyName" runat="server" CssClass="LABEL" Text="Party Name:"></asp:Label></td>
        <td style="width: 29%">
        <cc1:DDLSearch ID="ddl_PartyName" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedger" CallBackAfter="2" Text="" PostBack="True" OnTxtChange="ddl_PartyName_TxtChange" />
        </td>
        <td style="width: 1%">
            <asp:Label ID="lbl_PartyNameMan" runat="server" CssClass="TDMANDATORY" Text="*"></asp:Label></td>
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
             <table width="100%">
            <tr>
                <td width="100%">
                    <asp:Panel ID="Pnl_CreditMemoDetails" runat="server"  GroupingText="Credit Memo Details"  CssClass="PANEL" Width="100%" >
                   
      
            <asp:Panel ID="pnl_dg" runat="server"   ScrollBars="Vertical" Height="140" CssClass="PANEL" Width="100%" >
                        <table cellpadding="3" cellspacing="3" border="0" width="98%">
                            <tr>
                                <td style="width: 100%; " colspan="8">
        <asp:UpdatePanel ID="Upd_Pnl_Grid" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_PartyName" />
             </Triggers>
        <ContentTemplate>            
            
            <asp:DataGrid ID="dg_CreditMemoDetails" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                CellPadding="3" CssClass="GRID"  DataKeyField = "Sr_No" 
                ShowHeader="true">
                <HeaderStyle CssClass="GRIDHEADERCSS" />
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="2%" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Attach" runat="server" Checked="true" onclick="Txt_Enable()" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sr.No" Visible="False">
                        <HeaderStyle Width="1%" />
                        <ItemTemplate>
                            <asp:Label ID="lbl_Sr_No" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sr_No")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Credit Memo No." ItemStyle-Width="8%">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lbl_CreditMemoNo" runat="server" Text=' <%#(DataBinder.Eval(Container.DataItem, "Credit_Memo_No_For_Print"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Date" ItemStyle-Width="10%">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lbl_Date" runat="server"  Text=' <%#(DataBinder.Eval(Container.DataItem, "Credit_Memo_Date","{0:MMMM dd,yyyy}"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Freight Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="12%">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lbl_FreightAmount" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "FreightAmount"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Octroi Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="10%">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lbl_OctroiAmount" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "Octroi_Amount"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="12%"  ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "ToatlAmount"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="Amount Received" ItemStyle-Width="20%">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:TextBox ID="txt_AmountReceived" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                MaxLength="10" onblur="Txt_Enable()"  Text='<%#(DataBinder.Eval(Container.DataItem, "Amount_Received"))%>'
                                Width="96%">
                            </asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Credit_Memo_ID" runat="server" Text=' <%#(DataBinder.Eval(Container.DataItem, "Credit_Memo_ID"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
              </ContentTemplate>
        </asp:UpdatePanel>
            </td>
                             </tr>
                             </table>
                             </asp:Panel>
                              </asp:Panel>
                            
                            
                </td>
            </tr>
        </table>

            
            </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%" align="right">
            <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="True" Text="Total:"></asp:Label></td>
        <td align="right">
        <asp:UpdatePanel ID="Upd_Pnl_Total" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_PartyName" />
             </Triggers>
        <ContentTemplate>       
            <asp:Label ID="lbl_TotalValue" runat="server" CssClass="LABEL" Font-Bold="True" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HiddenField ID="hdn_Total" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td width="1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CashAmount" runat="server" CssClass="LABEL" Text="Cash Amount:"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_CashAmount" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="10">0</asp:TextBox></td>
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
            <asp:Label ID="lbl_ChequeAmount" runat="server" CssClass="LABEL" Text="Cheque Amount:"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_ChequeAmount" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="10">0</asp:TextBox></td>
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
            <asp:Label ID="lbl_ChequeNo" runat="server" CssClass="LABEL" Text="Cheque No.:"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_ChequeNo" runat="server" CssClass="TEXTBOX" MaxLength="8">0</asp:TextBox></td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date:"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_ChequeDate" runat="server" />
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Bank" runat="server" CssClass="LABEL" Text="Bank:"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_BankName" runat="server" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox></td>
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
            <asp:Label ID="lbl_Remark" runat="server" CssClass="LABEL" Text="Remark:"></asp:Label></td>
        <td colspan="4">
            <asp:TextBox ID="txt_Remark" runat="server" CssClass="TEXTBOX" TextMode="MultiLine" MaxLength="100"></asp:TextBox></td>
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
        <td style="width: 29%"><asp:HiddenField id="hdn_TotalCreditMemo" runat="server"></asp:HiddenField>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6"><asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" ValidationGroup="btn_Save1" OnClick="btn_Save_Click"  /></td>
    </tr>
    <tr>
        <td colspan="6" >
          <asp:UpdatePanel ID = "up_lbl_Errors" runat = "server" >
                    <ContentTemplate >
            <asp:Label ID="lbl_Error" Width="100%" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
               </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "btn_Save" />
                    </Triggers>
                    </asp:UpdatePanel>
             </td>
    </tr>
</table>



<script type="text/javascript">
Txt_Enable();
</script>