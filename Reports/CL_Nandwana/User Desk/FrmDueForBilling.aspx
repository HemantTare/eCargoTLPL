<%@ Page AutoEventWireup="true" CodeFile="FrmDueForBilling.aspx.cs" Inherits="Reports_CL_Nandwana_UserDesk_FrmDueForBilling"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function DueForBIllingDetails(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-200);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'DueForBIllingDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Due For Billing</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Due For Billing"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="Upd_Pnl_BillingDue" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_BillingDue" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="True" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="50" OnItemDataBound="dg_Details_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Black" ForeColor="White" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="Orange" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Client Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ClientName" Text='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_NoOfClient" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total :" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No Of LR" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "NoOfLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_NoOfLR" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalFreight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
