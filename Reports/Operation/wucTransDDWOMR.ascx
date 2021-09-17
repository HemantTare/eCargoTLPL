<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucTransDDWOMR.ascx.cs"
    Inherits="Reports_Operation_wucTransDDWOMR" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %> 
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="SM_DirectDelivery" runat="server">
</asp:ScriptManager>

<script src="../../Javascript/Common.js" type="text/javascript"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
 

<script type="text/javascript">

function GetTotalAmount()
{debugger;
    var lbl_TotalGCAmountValue = document.getElementById('<%=lbl_TotalGCAmountValue.ClientID %>');
    return val(lbl_TotalGCAmountValue.innerText); 
}

 
   
function HideReceivedByControl()
{debugger;
 var TR_DebitTo=document.getElementById('wucTransDDWOMR1_TR_DebitTo');
 var TR_Cheque=document.getElementById('wucTransDDWOMR1_TR_Cheque');    
 var rbl_CashBank=document.getElementById('wucTransDDWOMR1_Rbl_Receivedby_0');
    
    TR_DebitTo.style.display='none';
    if (rbl_CashBank.checked == true )
    {
      TR_Cheque.style.display='inline';
      TR_DebitTo.style.display='none';
    }
    else 
    {
      TR_Cheque.style.display='none';
      TR_DebitTo.style.display='inline';
    }
}

</script>

<table class="TABLE" width="100%">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_DirectDelivery_Heading" runat="server" Text="Direct Delivery"
                CssClass="HEADINGLABEL" meta:resourcekey="lbl_DirectDelivery_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 100%" colspan="6">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; height: 15px;">
        </td>
        <td align="left" colspan="4" style="height: 15px;">
            &nbsp;</td>
        <td style="width: 1%; height: 15px;">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 15px">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="height: 15px">
            <asp:Panel ID="pnl_Payment" runat="server" GroupingText="Freight Received">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table border="0" width="100%">
                            <tr runat="server" id="tr_ReceivedBy">
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_ReceivedBy" runat="server" CssClass="LABEL" Text="Received By:"></asp:Label>
                                </td>
                                <td style="width: 30%" colspan="2">
                                    <asp:RadioButtonList ID="Rbl_Receivedby" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="false" onclick="HideReceivedByControl();">
                                        <asp:ListItem Value="1" Text="Cash Bank" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Debit To"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="lbl_TotalGCAmountValue" runat="server" Text="Total Freight : "></asp:Label>
                                </td>
                                <td style="width: 30%" colspan="2">
                                    <asp:Label ID="txtlblTotalGCAmountValue" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" runat="server" id="TR_Cheque">
                                    <table border="0" width="100%">
                                        <tr id="tr1" runat="server">
                                            <td class="TD1" style="width: 20%">
                                                <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No : "></asp:Label></td>
                                            <td class="TD1" style="width: 20%">
                                                <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox></td>
                                            <td class="TD1" style="width: 20%">
                                            </td>
                                            <td class="TD1" style="width: 20%">
                                                <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date : "></asp:Label></td>
                                            <td class="TD1" style="width: 20%; text-align: left">
                                                <uc1:WucDatePicker ID="wucChequeDate" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="Tr2" runat="server">
                                            <td class="TD1" style="width: 20%">
                                                <asp:Label ID="lblBankName" runat="server" Text="Bank Name : "></asp:Label></td>
                                            <td class="TD1" colspan="3" style="text-align: left">
                                                <asp:TextBox ID="txtBankName" runat="server" Width="100%"></asp:TextBox></td>
                                            <td class="TD1" style="width: 20%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="TR_DebitTo">
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_DebitTo" runat="server" CssClass="LABEL" Text="Debit To :"></asp:Label></td>
                                <td style="width: 20%;">
                                    <cc1:DDLSearch ID="ddl_DebitTo" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerDirectDly" CallBackAfter="2"
                                        Text="" PostBack="False" />
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                    *</td>
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_BillingBranch" runat="server" CssClass="LABEL" Text="Billing Branch :"></asp:Label></td>
                                <td style="width: 20%;">
                                    <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetMemoToBranch" CallBackAfter="2"
                                        Text="" PostBack="False" />
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                    *</td>
                            </tr>
                        </table>
                    </ContentTemplate> 
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <%-- <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
                <ContentTemplate>--%>
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABEL" ForeColor="Red"
                meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            &nbsp<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
            <asp:HiddenField ID="hdn_Total_Articles" runat="server" />
            <asp:HiddenField ID="hdn_Total_GC_Amount" runat="server" />
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="hdn_gc_caption"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdn_lhpo_caption"></asp:HiddenField>

<script type="text/javascript" language="javascript">
    HideReceivedByControl();  
</script>

