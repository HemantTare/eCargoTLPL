<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmExemptedServiceTax.aspx.cs"
    Inherits="Reports_Booking_FrmExemptedServiceTax" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
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
    <title>Exempted Service Tax Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ExemptedServiceTax" runat="server">
        </asp:ScriptManager>
        <div>
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Exempted Service Tax Details"></asp:Label></td>
                </tr>
            </table>
        </div>
        <table id="tbl_input_screen" style="width: 100%" class="TABLE">
            <tr>
                <td colspan="6">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%" class="TD1">
                    <asp:Label ID="lbl_ReportType" runat="server" Text="Report Type :" CssClass="LABEL"></asp:Label></td>
                <td style="text-align: right;" colspan="2">
                    <asp:DropDownList ID="ddl_ReportType" runat="server" CssClass="DROPDOWN">
                        <asp:ListItem Value="1">Commodity Exempted Service Tax</asp:ListItem>
                        <asp:ListItem Value="2">Service Tax Payable By Consignor</asp:ListItem>
                        <asp:ListItem Value="3">Service Tax Payable By Consignee</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
                <td style="width: 15%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_View_Click" /></td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('view');">View Input</a></td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a></td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <div class="DIV" style="height: 510px; width: 976px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="Upd_Pnl_PaidFreightDetails" UpdateMode="Conditional" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_Grid_Details" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <%--<asp:Panel ID="pnl_PaidFreightDetails" runat="server" Height="500px" ScrollBars="Auto">--%>
                                            <asp:DataGrid ID="dg_Grid_Details" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" AllowCustomPaging="true" ShowFooter="True" CssClass="GRID"
                                                OnItemDataBound="dg_Grid_Details_ItemDataBound" OnPageIndexChanged="dg_Grid_Details_PageIndexChanged" PageSize="15">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR NO"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="GC_Date" HeaderText="LR Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Branch_Name" HeaderText="Booking Branch"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Service_Location_Name" HeaderText="Delivery Branch"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Booking_Type" HeaderText="Bkg Type"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Item" HeaderText="Item"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Sub Total">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"Sub_Total")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Sub_Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Service Tax Amount">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"Service_Tax_Amount")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Service_Tax_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Round Off">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Round_Off")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalRound_Off" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Total LR Amount">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Total_GC_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                            </asp:DataGrid>
                                            <%--</asp:Panel>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
