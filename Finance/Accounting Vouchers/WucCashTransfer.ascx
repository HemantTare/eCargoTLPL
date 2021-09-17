<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCashTransfer.ascx.cs"
    Inherits="Finance_Accounting_Vouchers_CashTransfer" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
 
 
function Allow_To_Exit()
{
    var ATE = false;

    if (confirm("Do you want to Exit...")==false)
        ATE=false;
    else
    {
        window.close();
        ATE=true;
    }
    return ATE;
}  


function txtbox_onfocus(txtbox)
{
    txtbox.style.backgroundColor = "yellow";
    txtbox.select();
}
////*******************************************************************
function txtbox_onlostfocus(txtbox)
{
    txtbox.value = txtbox.value.toUpperCase();
    txtbox.style.backgroundColor = "white";
}
    
</script>

<asp:ScriptManager ID="scm_Voucher" runat="server">
</asp:ScriptManager>
<table style="width: 100%;" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="Label1" runat="server" Text="CASH TRANSFER" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 10%; height: 22px; text-align: right">
        </td>
        <td style="width: 20%; height: 22px">
        </td>
        <td style="height: 22px; width: 30%;">
        </td>
        <td style="width: 20%; height: 22px; text-align: right">
        </td>
        <td style="width: 20%; height: 22px">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right;">
            Voucher
            Date:&nbsp;</td>
        <td style="width: 20%">
            <uc1:WucDatePicker ID="dtp_VoucherDate" runat="server" />
        </td>
        <td style="width: 30%">
        </td>
        <td style="width: 20%; text-align: right;">
            &nbsp;</td>
        <td style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right; height: 24px;">
            Ref No :
        </td>
        <td style="width: 20%; height: 24px;">
            <asp:TextBox ID="txt_RefNo" runat="server" CssClass="TEXTBOX" BorderWidth="1px" Style="width: auto;" MaxLength="25" 
            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
        </td>
        <td style="width: 30%; height: 24px;">
        </td>
        <td style="width: 20%; height: 24px;">
        </td>
        <td style="width: 20%; height: 24px;">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
            Transfer To :</td>
        <td style="width: 20%">
            <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLedgerForVoucherNoCrDr"
                IsCallBack="true" />
        </td>
        <td style="width: 30%"class="TDMANDATORY">
            *
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
            Transfer Through :</td>
        <td style="width: 20%">
            <asp:TextBox ID="txt_PaidToWhom" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Style="width: auto" MaxLength="100" 
            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox></td>
        <td style="width: 30%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
            Details:
        </td>
        <td colspan="2">
            <asp:TextBox ID="txt_Details" runat="server" Height="50px" TextMode="MultiLine"
                CssClass="TEXTBOX" BorderWidth="1px" Width="95%" MaxLength="500" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox></td>
        <td style="width: 20%" class="TDMANDATORY">
            *
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
        </td>
        <td style="width: 20%; text-align: right;">
            Amount :</td>
        <td style="width: 30%">
        <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                            MaxLength="21" onkeypress="return Only_Numbers(this,event)" Width="151px" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"/></td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 30%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td style="width: 10%; text-align: right">
            <asp:Button ID="btn_SaveNew" runat="server" Text="Save & New" CssClass="BUTTON" OnClick="btn_SaveNew_Click" /></td>
        <td style="width: 20%">
            <asp:Button ID="btn_SaveExit" runat="server" Text="Save & Exit" CssClass="BUTTON" OnClick="btn_SaveExit_Click" /></td>
        <td style="width: 30%">
            &nbsp;<asp:Button ID="btn_Exit" runat="server" Text="Exit" CssClass="BUTTON" OnClientClick="return Allow_To_Exit();" /></td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
    </tr>
    <tr>
        <td colspan="5" style="text-align: center">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:UpdatePanel ID="up_lbl_Errors" runat="server">
                <ContentTemplate>
                    &nbsp;<asp:Label ID="lbl_Errors" runat="server" ForeColor="red" Font-Bold="true"
                        EnableViewState="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
