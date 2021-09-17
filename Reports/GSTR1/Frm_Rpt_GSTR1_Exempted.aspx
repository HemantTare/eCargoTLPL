<%@ Page AutoEventWireup="true" CodeFile="Frm_Rpt_GSTR1_Exempted.aspx.cs" Inherits="Reports_GSTR1_Frm_Rpt_GSTR1_Exempted"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
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
    <title>GSTR 1 - Exempted</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GSTR 1 - Exempted"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    State :
                    <asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="true" CssClass="DROPDOWN" Width="20%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 15px;">
                    &nbsp;<uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server"></uc2:Wuc_From_To_Datepicker>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 50px;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                            </td>
                            <td style="width: 24%">
                                &nbsp;</td>
                            <td class="TD1" style="width: 14%">
                                &nbsp;</td>
                            <td style="width: 24%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
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
                    &nbsp;<asp:Button ID="btn_ExportToExcel" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcel_Click"
                        Text="Export To Excel" /></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <div style="width: 50%">
                        <table class="TABLE">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                                            <asp:AsyncPostBackTrigger ControlID="dg_Grid1" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <asp:DataGrid ID="dg_Grid1" runat="server" ShowFooter="True" AllowPaging="False"
                                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="False" AutoGenerateColumns="False"
                                                    OnItemDataBound="dg_Grid1_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="Particulars" HeaderText="Particulars"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Nil_Rated" HeaderText="Nil_Rated"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Exempted">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Exempted")%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_SumTotalExempted" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
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
                    </div>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    &nbsp;
                    <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="12">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="BkgBr" HeaderText="BkgBr"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgCity" HeaderText="BkgCity"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgState" HeaderText="BkgState"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyBr" HeaderText="DlyBr"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyCity" HeaderText="DlyCity"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyState" HeaderText="DlyState"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LR_No" HeaderText="LR_No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LRDate" HeaderText="LRDate"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment_Type"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor_Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignor_GSTNo" HeaderText="Consignor_GSTNo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee_Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee_GSTNo" HeaderText="Consignee_GSTNo"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="SubTotal">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "SubTotal")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalSubTotal" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="GST">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "GST")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalGST" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RoundOff">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RoundOff")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalRoundOff" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TotalLRAmount">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLRAmount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalLRAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Upd_Pnl_DeliveryStockList">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID2" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
