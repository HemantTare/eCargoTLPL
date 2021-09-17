<%@ Page AutoEventWireup="true" CodeFile="FrmUserDeskCreditDebitCustomerBalancePendingBills.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmUserDeskCreditDebitCustomerBalancePendingBills"
    Language="C#" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function input_screen_action(action)
{
if (action == 'view')
  {
  tbl_input_screen.style.display='inline';
  }
else
  {
  tbl_input_screen.style.display='none';
  }
}

    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Bills</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Bills"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 24%">
                                <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABEL" Text="ClientName" Font-Bold="true" ForeColor="Magenta" /></td>
                            <td style="width: 10%" class="TD1">
                                </td>
                            <td style="width: 24%">
                                &nbsp;</td>
                            <td class="TD1" style="width: 9%">
                            </td>
                            <td style="width: 9%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
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
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="True" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="25" OnItemDataBound="dg_Details_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Bill_Date")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Bill No" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_BillNo" Text='<%# DataBinder.Eval(Container, "DataItem.Bill_No") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Bill_No") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Bill_Amount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_BillAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Received Amount" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Received_Amount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_ReceivedAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Balance" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Balance")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_BalanceAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
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
