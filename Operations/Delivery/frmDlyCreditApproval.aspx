<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDlyCreditApproval.aspx.cs" Inherits="Operations_Delivery_frmDlyCreditApproval" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="../../Javascript/Operations/Delivery/DlyCreditApproval.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript">
 function ShowHidePayment()
 {
    var tr_Reason = document.getElementById("<%=tr_Reason.ClientID%>");
    var tr_Payment=document.getElementById("<%=tr_Payment.ClientID%>");
    var tr_PaymentCredit=document.getElementById("<%=tr_PaymentCredit.ClientID%>");
    var tr_PaymentMobile=document.getElementById("<%=tr_PaymentMobile.ClientID%>");
    var chkApproved=document.getElementById("<%=chkApproved.ClientID%>");
    
  if (chkApproved.checked)
  {
     tr_Payment.style.display='inline';
     tr_Reason.style.display='none';
     ShowHidePaymentMode();
  }
  else
  {
    tr_Payment.style.display='none';
    tr_PaymentCredit.style.display='none';
    tr_PaymentMobile.style.display='none';
    tr_Reason.style.display='inline';
   }   
 }  
 
 function ShowHidePaymentMode()
 {
    var tr_PaymentCredit=document.getElementById("<%=tr_PaymentCredit.ClientID%>");
    var tr_PaymentMobile=document.getElementById("<%=tr_PaymentMobile.ClientID%>");
    var Rbl_Receivedby = document.getElementById("Rbl_Receivedby_0");
    var txt_BillingParty = document.getElementById("txt_BillingParty");
    var hdn_BillingPartyId = document.getElementById("hdn_BillingPartyId");
    
  if (Rbl_Receivedby.checked)
  {
     tr_PaymentCredit.style.display='inline';
     tr_PaymentMobile.style.display='none';
  }
  else
  {
    txt_BillingParty.value = '';
    hdn_BillingPartyId.value = '0'
    tr_PaymentCredit.style.display='none';
    tr_PaymentMobile.style.display='inline';
   }   
 }  
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Credit Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="TABLE" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Credit Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
            <tr>
            <td colspan="6" style="height:300px;">
                <table style="height:100%;width:100%;">
                    <tr valign="top">
                        <td  style="width: 20%" align="right" valign="top">Enter LR No :
                        </td>
                        <td  style="width: 29%" align="left" valign="top">
                            <asp:TextBox ID="txt_LRNo" runat="server" Width="97%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY" valign="top">*
                        </td>
                        <td colspan="3" align="left" style="width: 50%"> 
                            <asp:Button ID="btn_search" runat="server" CssClass="BUTTON" Text="search" OnClick="btn_Search_Click"/>
                        </td>
                    </tr>
                    <tr id="tr_Consignor" runat="server" visible="false">
                        <td  style="width: 20%" align="right">Consigner Name :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:Label ID="lblConsignor" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td  style="width: 20%" align="right">Consignee Name :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:Label ID="lblConsignee" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                    </tr>
                    <tr id="tr_PaymentMode" runat="server" visible="false">
                        <td  style="width: 20%" align="right">Payment Mode :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td  style="width: 20%" align="right">Total Articles :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:Label ID="lblTotalArticles" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                    </tr>
                    <tr id="tr_TotalAmount" runat="server" visible="false">
                        <td  style="width: 20%" align="right">Total Amount :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:Label ID="lblTotalAmount" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td colspan="3" style="width: 50%"></td>
                    </tr>
                    <tr id="tr_Approved" runat="server" visible="false">
                        <td  style="width: 20%" align="right">Is Approved? :
                        </td>
                        <td  style="width: 29%" align="left">
                            <asp:CheckBox ID="chkApproved" runat="server" OnClick="ShowHidePayment();" ></asp:CheckBox>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td colspan="3" style="width: 50%"></td>
                    </tr>
                    <tr id="tr_Payment" runat="server" style="display:none;">
                        <td  style="width: 20%" align="right">Payment :
                        </td>
                        <td style="width: 29%" align="left">
                            <asp:RadioButtonList ID="Rbl_Receivedby" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false" onclick="ShowHidePaymentMode();">
                                <asp:ListItem Value="1" Text="Credit" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Mobile Payment"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td colspan="3" align="left" style="width: 50%"> 
                        </td>
                    </tr>
                    <tr id="tr_PaymentCredit" runat="server" style="display:none;">
                        <td  style="width: 20%" align="right">Billing Party :
                        </td>
                        <td style="width: 29%" align="left">
                             <asp:TextBox ID="txt_BillingParty" AutoCompleteType="Disabled" onblur="Billing_LostFocus(this,'lst_BillParty')"
                            onkeyup="NewGC_AllSearch(event,this,'lst_BillParty','BillingParty',2);" onkeydown="return on_keydown(event,this,'lst_BillParty');"
                            onfocus="On_Focus('txt_BillingParty','lst_BillParty','BillingParty');" runat="server"
                            CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox><br />
                            <asp:ListBox ID="lst_BillParty" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_BillingParty')"
                                runat="server" TabIndex="90"></asp:ListBox>
                            <asp:HiddenField runat="server" ID="hdn_BillingPartyId" />
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY">*</td>
                        <td colspan="3" align="left" style="width: 50%"> 
                        </td>
                    </tr>
                    <tr id="tr_PaymentMobile" runat="server" style="display:none;">
                        <td  style="width: 20%" align="right">MPayment Transaction Id :
                        </td>
                        <td style="width: 29%" align="left">
                            <asp:TextBox ID="txtMobilePayment" runat="server" Width="97%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY">*</td>
                        <td colspan="3" align="left" style="width: 50%"> 
                        </td>
                    </tr>
                    <tr id="tr_Reason" runat="server" visible="false"> 
                        <td  style="width: 20%" align="right">Reason For Non Approval :
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtReason" runat="server" Width="97%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY">*</td>
                    </tr>
                    <tr>
                        <td  style="width: 20%">
                        </td>
                        <td style="width: 29%" align="left">
                        </td>
                        <td  style="width: 1%" class="TDMANDATORY"></td>
                        <td colspan="3" align="left" style="width: 50%"> 
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
           <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
             <tr>
                <td colspan="6">&nbsp;
                <asp:HiddenField runat="server" ID="hdngcid" />
                <asp:HiddenField runat="server" ID="hdnpdsid" />
                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
                </td>
            </tr>
             <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
            <tr id="tr_btnsave" runat="server" visible="false">
                <td class="TD1" colspan="6" style="text-align: center">
                    <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="N"
                        OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />&nbsp
                    <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                        OnClick="btn_Close_Click" meta:resourcekey="btn_CloseResource1" />
            </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
