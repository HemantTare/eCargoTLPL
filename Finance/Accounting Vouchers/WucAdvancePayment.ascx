<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAdvancePayment.ascx.cs"
    Inherits="FA_WucAdvancePayment" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="WucTDSPaymentDeduction.ascx" TagName="WucTDSPaymentDeduction" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<%--<script type ="text/javascript" language ="javascript" src ="../../Javascript/NumCheck.js"></script>--%>

<script language="javascript" type="text/javascript">
var lbl_Errors;
 
function Allow_To_Save()
{
    var ATS=false;
    lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
    
    var ddl_CashBankLedger = document.getElementById("<%=ddl_CashBankLedger.ClientID%>");
    
    var txt_ChequeNo = document.getElementById("<%=txt_ChequeNo.ClientID%>");

    var txt_RefNoPartyLedger = document.getElementById("<%=txt_RefNoPartyLedger.ClientID%>");

    var txt_Amount = document.getElementById("<%=txt_Amount.ClientID%>");

    var ddl_TDSLedger = document.getElementById("<%=ddl_TDSLedger.ClientID%>");

    var txt_RefNoTDSLedger = document.getElementById("<%=txt_RefNoTDSLedger.ClientID%>");

    if(!validDropDown(ddl_CashBankLedger,'Cash/Bank Ledger')) {}
    
    else if(ddl_CashBankLedger.options[ddl_CashBankLedger.selectedIndex].value.split('Ö')[1]=='19'&& !validTextBox(txt_ChequeNo,'Cheque No')) 
    {
    
    }
    
    else if(!validTextBox(txt_RefNoPartyLedger,'Party Ledger Ref No ')) {}
    
    else if(!validTextBox(txt_Amount,'Amount')) {}

//    else if(!validDropDown(ddl_TDSLedger,'TDS Ledger')) {}

//    else if(!validTextBox(txt_RefNoTDSLedger,'TDS Ledger Ref No ')) {}

     
    else ATS=true;

 return ATS; 
 
}
 

function validTextBox(txt_ID,Msg)
{
    if(Trim(txt_ID.value) == '')
    {  
           lbl_Errors.innerText = 'Please Enter'+' '+Msg;
           txt_ID.focus();  
           return false;
    }
    return true;
}

 
function validDropDown(ddl_ID,Msg)
{
      if(ddl_ID.selectedIndex==-1 || ddl_ID.options[ddl_ID.selectedIndex].value=='0')
     {
     lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
        lbl_Errors.innerText = 'Please Select'+' '+Msg;
        ddl_ID.focus();
        return false;
         
     }
  return true;        
}
 
  
   function visibleTDS(text,value)
   {
   
       if(Trim(value)=='')
       {return;}
        
        var tbl_TDS = document.getElementById("<%=tbl_TDS.ClientID%>");
        var splitted = value.split('Ö');
        
        if(splitted[1]==1)
        {
            tbl_TDS.style.visibility="visible";
        }
        else
        {
           tbl_TDS.style.visibility="hidden";
        }
        
   }
   
   function visibleCheckNo()
   { 
        var ddl_CashBankLedger = document.getElementById("<%=ddl_CashBankLedger.ClientID%>");
        var tbl_CheckNo = document.getElementById("<%=tbl_ChequeNo.ClientID%>");
        
        if(ddl_CashBankLedger.options[ddl_CashBankLedger.selectedIndex].value=="0") 
           return false;
           
        if(ddl_CashBankLedger.options[ddl_CashBankLedger.selectedIndex].value.split('Ö')[1]=='19')
        {
            tbl_CheckNo.style.visibility="visible";
        }
        else
        {
           tbl_CheckNo.style.visibility="hidden";
        }
   }
  
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 100%" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="ADVANCE PAYMENT"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            Payment No :</td>
        <td style="width: 29%;">
            <asp:Label ID="lbl_PaymentNo" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 20%;">
            Vouche Date :
        </td>
        <td style="width: 29%;">
            <uc3:WucDatePicker ID="dtp_VoucheDate" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Ref. No :</td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_RefNo" runat="server" CssClass="TEXTBOX" MaxLength="25" Width="95.5%"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            Cash/Bank Ledger :</td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_CashBankLedger" onchange="visibleCheckNo()" runat="server"
                CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *
        </td>
        <td style="width: 50%;" colspan="3">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_CashBankLedger" />
                </Triggers>
                <ContentTemplate>
                    <table runat="server" id="tbl_ChequeNo">
                        <tr>
                            <td class="TD1" style="width: 40%;">
                                Cheque No :</td>
                            <td style="width: 58%;">
                                <asp:TextBox ID="txt_ChequeNo" runat="server" onkeyup="valid(this)" onblur="valid(this)"
                                    CssClass="TEXTBOXNOS" MaxLength="25" onkeypress="valid(this)" Width="95.5%"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 2%;">
                                *
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            Party Ledger :</td>
        <td style="width: 29%;">
            <cc1:DDLSearch ID="ddl_PartyLedger" runat="server" AllowNewText="false" InjectJSFunction="visibleTDS"
                IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchPartyLedger" />
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *</td>
        <td class="TD1" style="width: 20%;">
            Ref. No. :</td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_RefNoPartyLedger" runat="server" CssClass="TEXTBOX" MaxLength="25"
                Width="95.5%"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            Amount :</td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Amount" runat="server" onkeyup="valid(this)" onblur="valid(this)"
                onkeypress="valid(this)" CssClass="TEXTBOXNOS" MaxLength="18" Width="95.5%"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">
            *
        </td>
        <td class="TD1" style="width: 20%;">
            &nbsp;
        </td>
        <td style="width: 29%;">
            &nbsp;
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Ok" />
                </Triggers>
                <ContentTemplate>
                    <table style="width: 100%" runat="server" id="tbl_TDS" border="0">
                        <tr>
                            <td style="width: 20%" class="TD1">
                                TDS Ledger :</td>
                            <td style="width: 29%">
                                <asp:DropDownList ID="ddl_TDSLedger" runat="server" CssClass="DROPDOWN">
                                </asp:DropDownList></td>
                            <td style="width: 1%" class="TDMANDATORY">
                                *
                            </td>
                            <td style="width: 20%" class="TD1">
                                Ref. No. :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txt_RefNoTDSLedger" runat="server" CssClass="TEXTBOX" MaxLength="25"
                                    Width="95.5%"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 29%">
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                                &nbsp;
                            </td>
                            <td class="TD1" style="width: 20%">
                                <asp:Button ID="btn_Ok" runat="server" Text="Get TDS Details" CssClass="BUTTON" OnClick="btn_Ok_Click" />
                            </td>
                            <td style="width: 29%">
                                &nbsp;
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td class="TD1" style="width: 79%" colspan="5" align="center">
                                <fieldset>
                                    <legend>TDS PAYMENT DEDUCTION</legend>
                                    <uc2:WucTDSPaymentDeduction ID="WucTDSPaymentDeduction1" runat="server" />
                                </fieldset>
                            </td>
                            <td class="TD1" style="width: 1%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%" valign="top">
            Narration :</td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_Narration" runat="server" CssClass="TEXTBOX" MaxLength="200"
                TextMode="MultiLine" Width="100%" Height="60px"></asp:TextBox></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                    <asp:AsyncPostBackTrigger ControlID="btn_Ok" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"/></td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            &nbsp;
        </td>
    </tr>
</table>
