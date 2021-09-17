<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Service_Tax_Report.aspx.cs"
    Inherits="Reports_CL_Reach_Booking_Frm_Service_Tax_Report" %>

<%--<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
--%>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
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
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ServiceTax" runat="server">
        </asp:ScriptManager>
        <table class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="  Service Tax Register"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 100%">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%;" class="TD1">
                                <asp:Label ID="lbl_division" runat="server" CssClass="LABEL" Text="label" /></td>
                            <td style="width: 24%;">
                                <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                            </td>
                            <td style="width: 9%;" class="TD1">
                            </td>
                            <td style="width: 24%;">
                            </td>
                            <td style="width: 9%;" class="TD1">
                                </td>
                            <td style="width: 24%;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 9%">
                                <asp:Label ID="lbl_PaymentType" runat="server" CssClass="LABEL" Text="Payment Type :" /></td>
                            <td style="width: 24%">
                                 <asp:DropDownList ID="ddl_payment_type" runat="server"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    &nbsp;</td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%;">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%;">
                    <asp:Button ID="btn_export_to_excel" CssClass="BUTTON" Text="Export To Excel" runat="server"
                        OnClick="btn_export_to_excel_Click" />
                </td>
                <td style="width: 11%;">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 11%;">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_BkgSTGrid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" GroupingText="Booking Service Tax" Width="100%"
                                Font-Bold="True">
                                <asp:DataGrid ID="dg_BkgSTGrid" runat="server" Width="98%" ShowFooter="True" AllowPaging="True"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_BkgSTGrid_PageIndexChanged"
                                    Font-Bold="false" PageSize="25" PagerStyle-Mode="NumericPages" OnItemDataBound="dg_BkgSTGrid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="gc_caption No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="gc_caption Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "gc_caption Date")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalCount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Consignor" HeaderText="Consignor Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee" HeaderText="Consignee Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Payment Type" HeaderText="Payment&lt;br&gt;Type"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Total&lt;br&gt;Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total_Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tax Abate&lt;br&gt;@75%">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Tax_Abate")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TaxAbate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Sub&lt;br&gt;Total">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Sub_Total")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SubTotal" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Service Tax&lt;br&gt;Payable">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Service_Tax")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Round Off">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Round_Off")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Round_Off" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Round Off&lt;br&gt;Service Tax">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Round_Off_Service_Tax")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Round_Off_Service_Tax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
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
