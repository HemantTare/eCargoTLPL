<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReverseVoucher.ascx.cs"
    Inherits="Finance_IBT_WucUnAppVoucherCancellation" %>
<%@ Register Src="../VoucherView/WucVoucher.ascx" TagName="WucVoucher" TagPrefix="uc1" %>
<%--WucUnAppVoucherCancellation--%>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="scm_Reverse" runat="server" />

<script type="text/javascript">

function openPopUp(Path)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = 400;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'BillByBill', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;    
}


 function Allow_To_Save()
 {
    return true;
 }
 
</script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL">REVERSE UNAPPROVED VOUCHER</asp:Label>
            &nbsp;&nbsp;
            <%--            <asp:Button ID="btn_VoucherView" runat="server" CssClass="BUTTON" Text="VoucherView" />
--%>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td align="center" colspan="3" style="width: 20%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
         <uc1:WucVoucher ID="WucVoucher1" runat="server" />   
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td align="center" colspan="3" style="width: 20%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 10%">
            Ledger Name:
        </td>
        <td style="width: 29%">
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                        <cc1:DDLSearch ID="ddl_Ledger_Name" runat="server" CallBackAfter="2" CallBackFunction="Raj.EF.CallBackFunction.CallBack.IBT_SearchLedgersForReverse"
                            OnTxtChange="ddl_Ledger_Name_TxtChange" IsCallBack="true" PostBack="True" />
                    </td>
                    <td style="width: 80%" align="left" class="TDMANDATORY">
                        *
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            &nbsp;</td>
        <td align="center" style="width: 20%" colspan="3">
            <asp:UpdatePanel ID="up_CostCenter" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Button ID="btn_CostCentre" runat="server" CssClass="BUTTON" Text="Cost Centre" />
                            </td>
                            <td>
                                <asp:Button ID="btn_BillwiseDetails" runat="server" CssClass="BUTTON" Text="Billwise Details" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Name" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr>
        <td align="center" colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 10%">
            Reason :</td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_Reason" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" colspan="1" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" />&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;<asp:Label ID="lbl_Error" runat="server" EnableViewState="false" CssClass="LABELERROR"></asp:Label></td>
    </tr>
</table>
