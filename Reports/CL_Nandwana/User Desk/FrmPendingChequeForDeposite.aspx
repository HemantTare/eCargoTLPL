<%@ Page AutoEventWireup="true" CodeFile="FrmPendingChequeForDeposite.aspx.cs" Inherits="Reports_CL_Nandwana_UserDesk_FrmPendingChequeForDeposite"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../../JavaScript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Cheques For Deposite</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
function checkuncheck(id1,id2)
{
    var id11 = document.getElementById(id1);    
    var id22 = document.getElementById(id2);
    
    if(id11.checked)
        id22.checked = false;
    if(id22.checked)
        id11.checked = false;
}

function checkuncheckDeposited(chk_Deposited,ddlBankLedger)
{
    var chk_Deposited1 = document.getElementById(chk_Deposited);    
    var ddlBankLedger1 = document.getElementById(ddlBankLedger);
    
    if(chk_Deposited1.checked)
    {
        ddlBankLedger1.disabled = false;
    }
    else
    {
        ddlBankLedger1.disabled = true;
    }
}

function openVoucherWindow(DocNo)
    {
        var Path='../../../Finance/VoucherView/FrmVoucher.aspx?Id='+ DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }

function openDetailsWindow(VoucherCreatedBy,ChqSentToHOOn,SentTime,ChqSentToHOBy,ChqRecdAtHOOn,RecdTime,ChqRecdAtHOBy)
    {
        
        var Path='../../../Reports/CL_Nandwana/User Desk/frmPendingChequeForDepositeDetails.aspx?VoucherCreatedBy='+ VoucherCreatedBy 
        + '&ChqSentToHOOn=' + ChqSentToHOOn + '&SentTime=' + SentTime  + '&ChqSentToHOBy=' + ChqSentToHOBy 
        + '&ChqRecdAtHOOn=' + ChqRecdAtHOOn + '&RecdTime=' + RecdTime + '&ChqRecdAtHOBy=' + ChqRecdAtHOBy;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-800);
        var popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
          
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%" colspan="2">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Cheques For Deposite"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 30%">
                    <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label></td>
                <td style="width: 70%" align="right">
                    <asp:Label ID="lbl_Message" runat="server" Text="Records in Golden Color Indicates, Cheques Are Due For Deposite In Bank."
                        ForeColor="DarkMagenta" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 70%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2">
                    <asp:UpdatePanel ID="Upd_Pnl_BillingDue" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_BillingDue" runat="server" Height="475px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="true" AllowPaging="true"
                                    CssClass="GRID" AllowSorting="true" AllowCustomPaging="true" AutoGenerateColumns="false"
                                    PageSize="15" OnItemDataBound="dg_Details_ItemDataBound" OnPageIndexChanged="dg_Details_PageIndexChanged">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Black" ForeColor="White" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="Silver" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Branch_Name")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Text="Total : " Font-Bold="true" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Recieved On" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "VoucherDate")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Time" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Time" Text='<%# DataBinder.Eval(Container, "DataItem.VoucherCreatedOn") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.VoucherCreatedOn") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cheque Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Cheque_Date")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cheque No" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ChequeNo" Text='<%# DataBinder.Eval(Container, "DataItem.Cheque_No") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Cheque_No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cheque Bank" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Bank_Name")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Amount" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Sent To HO" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_SentToHO" Checked='<%#DataBinder.Eval(Container.DataItem,"IsSentToHO")%>'
                                                    runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Recvd AT HO" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_RecevdToHO" Checked='<%#DataBinder.Eval(Container.DataItem,"IsReceivedATHO")%>'
                                                    runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Deposited" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Deposited" Checked='<%#DataBinder.Eval(Container.DataItem,"IsDeposited")%>'
                                                    runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Deposited In" Visible="true" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddl_BankLedger" Checked='<%#DataBinder.Eval(Container.DataItem,"DepositedIn")%>'
                                                    Width="100%" runat="server" OnSelectedIndexChanged="ddl_BankLedger_SelectedIndexChanged"
                                                    Style="text-align: Left" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 100%" align="center" colspan="2">
                    <asp:Button AccessKey="S" ID="btn_Save" OnClick="btn_Save_Click" runat="server" Text="Save"
                        CssClass="BUTTON"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
