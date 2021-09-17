<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyToPayAccountingBranchDtls.aspx.cs"
    Inherits="Finance_Reports_FrmDailyToPayAccountingBranchDtls" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily ToPay Accounting Branch Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DiscountReportDateWise" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily ToPay Accounting Branch Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 16%">
                </td>
                <td style="width: 16%; text-align: right;">
                    &nbsp;</td>
                <td style="width: 16%; text-align: left;">
                    <%--  <asp:Label ID="lblYearID" runat="server" CssClass="LABEL" Font-Bold="True" Font-Size="Larger"></asp:Label>--%>
                </td>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                </td>
            </tr>
            <tr>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblRegion" runat="server" Text="Region :" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtRegion" runat="server" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblArea" runat="server" Text="Area :" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtArea" runat="server" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblBranch" runat="server" Text="Branch :" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtBranch" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 16%; text-align: right">
                <asp:Label ID="lblAsOnDate" runat="server" Text="As On Date :" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 16%; text-align: left">
                    <asp:Label ID="txtAsOnDate" runat="server" Font-Bold="True"  ></asp:Label></td>
                <td style="width: 16%; text-align: right">
                </td>
                <td style="width: 16%; text-align: left">
                    </td>
                <td style="width: 16%; text-align: right">
                </td>
                <td style="width: 16%; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
                <td style="width: 16%">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
                <td style="width: 16%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 850px;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" AllowPaging="True" AllowCustomPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="LR No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LR_No")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LR Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LR_Date")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total&lt;br/&gt;Freight" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalFrt")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_Frt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Destination">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Destination")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Consignor Name">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Consignor_Name")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Consignee Name">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Consignee_Name")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Vehicle No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Vehicle_No")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Invoice No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Invoice_No")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
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
