<%@ Page AutoEventWireup="true" CodeFile="Frm_Cheque_Deposited_In_Bank_Details.aspx.cs"
    Inherits="Finance_Reports_Frm_Cheque_Deposited_In_Bank_Details" Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">

function BranchClicked()
{
    var tdid1 = document.getElementById("td_Branch1");
    var tdid2 = document.getElementById("td_Branch2");
    
    
    if (tdid1 != null) 
    {
        tdid1.style.display = 'block';
    }
    
    if (tdid2 != null) 
    {
        tdid2.style.display = 'block';
    }
    
}

function HOClicked()
{

    var tdid1 = document.getElementById("td_Branch1");
    var tdid2 = document.getElementById("td_Branch2");
    
    if (tdid1 != null) 
    {
        tdid1.style.display = 'none';
    }
    
    if (tdid2 != null) 
    {
        tdid2.style.display = 'none';
    }
}

function openVoucherWindow(DocNo)
    {
        var Path='../../Finance/VoucherView/FrmVoucher.aspx?Id='+ DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cheque Deposited In Bank Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Cheque Deposited In Bank Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 15%" class="TD1">
                    Deposited By :
                </td>
                <td style="width: 15%" align="left">
                    <asp:RadioButtonList ID="rdl_DepositedBy" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True" Text="Branch" OnClick="BranchClicked();"></asp:ListItem>
                        <asp:ListItem Value="1" Text="HO" OnClick="HOClicked();"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="width: 10%" class="TD1" id="td_Branch1" runat="server">
                    Branch :
                </td>
                <td style="width: 60%" align="left" id="td_Branch2" runat="server">
                    <cc1:DDLSearch ID="ddlBranch" runat="server" AllowNewText="False" IsCallBack="True"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                        PostBack="True" InjectJSFunction="" Text="" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 15px;" colspan="4">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server"></uc2:Wuc_From_To_Datepicker>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 50px;" colspan="4">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                            </td>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 14%">
                                &nbsp;</td>
                            <td style="width: 24%">
                                <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" /></td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left; height: 15px;">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="400px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ChqDepositedBy" HeaderText="Deposited By" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChqDepositedOn" HeaderText="Deposited On" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChqDepositedin" HeaderText="Deposited In" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChqReceivedOn" HeaderText="Received On" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Cheque_No" HeaderText="Cheque No" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Cheque_Date" HeaderText="Cheque Date" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChqBank" HeaderText="Chq Bank" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Voucher No.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_VoucherNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Voucher_No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="VoucherType" HeaderText="Voucher Type" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
