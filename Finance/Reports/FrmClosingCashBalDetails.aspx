<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClosingCashBalDetails.aspx.cs"
    Inherits="Finance_Reports_FrmClosingCashBalDetails" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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
    <title>Closing Cash Balance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      .SHOWSELECTEDLINK{FONT-SIZE: 11px;FONT-FAMILY: Verdana;color:#0033ff;text-decoration:underline;}
</style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClosingCashBal" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Cash Balance"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="tr_PaidBooking" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_ClosingCashBal" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 450px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" AllowPaging="true" CssClass="GRID"
                                    AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    Width="50%" OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="LR No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LRNo")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" Text="Total : " CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Dly Branch">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DlyBranch")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_Count" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Consignor" HeaderText="Consignor">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee" HeaderText="Consignee">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Description" HeaderText="Description">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Size" HeaderText="Size">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Pkgs">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Pkgs")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalPkgs" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cash">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Cash")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalCash" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="tr_ToPayRecovery" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridToPayRecovery" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 450px; width: 100%;">
                                <asp:DataGrid ID="dg_GridToPayRecovery" runat="server" ShowFooter="true" AllowPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_GridToPayRecovery_PageIndexChanged"
                                    Width="50%" OnItemDataBound="dg_GridToPayRecovery_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Dly Type">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DlyType")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_ToPayTotal" runat="server" Text="Total : " CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Dly No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DlyNo")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_ToPayTotal_Count" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LRNo" HeaderText="LR No">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="LRDate" HeaderText="LR Date">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgBranch" HeaderText="Bkg Branch">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalAmount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="tr_OtherCashReceipt" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridOtherCashReceipt" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 450px; width: 100%;">
                                <asp:DataGrid ID="dg_GridOtherCashReceipt" runat="server" ShowFooter="true" AllowPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_GridOtherCashReceipt_PageIndexChanged"
                                    Width="50%" OnItemDataBound="dg_GridOtherCashReceipt_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Voucher Type">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "VoucherType")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_VoucherTypeTotal" runat="server" Text="Total : " CssClass="LABEL"
                                                    Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Voucher No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "VoucherNo")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_VoucherNoTotal_Count" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LedgerName" HeaderText="Ledger Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Amount">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalOtherCashAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Narration" HeaderText="Narration">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="tr_PendingPDS" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_PendingPDS" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 450px; width: 100%;">
                                <asp:DataGrid ID="dg_PendingPDS" runat="server" ShowFooter="true" AllowPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_PendingPDS_PageIndexChanged"
                                    Width="80%" OnItemDataBound="dg_PendingPDS_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="PDS No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PDS_No_For_Print")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_PDS_No_For_Print" runat="server" Text="Total : " CssClass="LABEL"
                                                    Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="PDS Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PDS_Date")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_PDS_Date" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Pay Type">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Total Articles">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total_Articles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
