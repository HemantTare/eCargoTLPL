<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUserDeskCreditDebitCustomerBalance.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmUserDeskCreditDebitCustomerBalance" %>

<%@ Register Src="../../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function LedgerMonthly(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'LedgerMonthly', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function viewwindow_ClientRegular(ClientId)
{
        var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'ClientSearchRegularClient', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

  function viewwindow_BillingDetails(ClientId, Ledger_Id, ClientName)
{
        var Path='../../../Reports/CL_Nandwana/User Desk/FrmUserDeskCreditDebitCustomerBalancePendingBillsNew.aspx?ClientId=' + ClientId + '&Ledger_Id=' + Ledger_Id + '&ClientName=' + ClientName;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100;
        var popH = h-100;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'PendingBills', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Credit - Debit Customer Closing Balance</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Credit - Debit Customer Closing Balance"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 10%; height: 15px;">
                    &nbsp;</td>
                <td style="width: 90%; height: 15px;">
                    &nbsp;<asp:RadioButtonList ID="rdl_CreditDebitCustomer" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="0">Credit Customers A/c </asp:ListItem>
                        <asp:ListItem Value="1">Advance Pmt (Wallet) A/c </asp:ListItem>
                        <asp:ListItem Value="2">Sundry Debtors </asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="width: 10%; height: 15px;">
                    &nbsp;</td>
                <td style="width: 90%; height: 15px;">
                    &nbsp;</td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" /></td>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                            <asp:AsyncPostBackTrigger ControlID="rdl_CreditDebitCustomer" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" AllowPaging="true" CssClass="GRID"
                                AllowSorting="true" AllowCustomPaging="true" AutoGenerateColumns="false" PageSize="25"
                                OnItemDataBound="dg_Grid_ItemDataBound" OnPageIndexChanged="dg_Grid_PageIndexChanged">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Black" ForeColor="White" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="Silver" />
                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Client Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_ClientName" runat="server" CssClass="LABEL" Font-Bold="true"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "LedgerName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        FooterStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "City")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Contact Person" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "ContactPerson")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Marketing Executive" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "MarketingExecutive")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Credit Limmit" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "CreditLimit")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_TotalHead" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total : " />
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Closing Balance" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_ClosingBalance" runat="server" CssClass="LABEL" Font-Bold="true"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ClosingBal") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_TotClosingBal" runat="server" CssClass="LABEL" Font-Bold="true" />
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="LastBilled On" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_LastBilled" runat="server" CssClass="LABEL" Font-Bold="true"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "LastBilled") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
