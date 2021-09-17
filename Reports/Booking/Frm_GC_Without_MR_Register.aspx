<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_GC_Without_MR_Register.aspx.cs" Inherits="Reports_Booking_Frm_GC_Without_MR_Register" %>

<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>
--%>
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

<%--Author : Harshal Sapre
    Date   : 14-01-09--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GC Without MR</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_BookingStockList" runat="server"></asp:ScriptManager>

        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GC Without MR Register"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <uc2:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    &nbsp;<uc4:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_Rpt_Type" runat="server" CssClass="LABEL" Text="Report Type:"/></td>
                            <td style="width: 24%">
                                <asp:DropDownList ID="ddl_Rpt_Type" runat="server" AutoPostBack="True"
                                    Width="100%" OnSelectedIndexChanged="ddl_Rpt_Type_SelectedIndexChanged">
                                    <asp:ListItem Value="0">To Pay Without MR</asp:ListItem>
                                    <asp:ListItem Value="1">Paid Booking Without MR</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="TD1" style="width: 9%"></td>
                            <td colspan="2">
                    <asp:RadioButtonList ID="rdl_Is_Group_Ledger" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Booking Branch</asp:ListItem>
                        <asp:ListItem Value="2">Delivery Branch</asp:ListItem>
                    </asp:RadioButtonList></td>
                            <td style="width: 24%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" OnClick="btn_view_Click" Text="View" />
                </td>
                <td style="width: 10%">
                    <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                   
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                  <asp:UpdatePanel ID="Upd_Pnl_BookingStockList" UpdateMode="Conditional" runat="server">
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                  </Triggers>
                  <ContentTemplate>
                        <asp:Panel ID="pnl_BookingStockList" runat="server" Height="500px" ScrollBars="Auto">
                            <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true" AllowPaging="True" CssClass="GRID" AllowSorting="True" AllowCustomPaging="true"
                                AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                <Columns>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>                    
                                        <%# DataBinder.Eval(Container.DataItem, "GC_No")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total" Text="Total:" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="GC Date"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Booking_Branch" HeaderText="Booking Branch"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Delivery_Branch" HeaderText="Delivery Branch"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor Name"></asp:BoundColumn>                                 
                                <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name"></asp:BoundColumn>                                 
                                <asp:BoundColumn DataField="Actual_Weight" HeaderText="Actual Weight"></asp:BoundColumn>                                 
                                <asp:TemplateColumn HeaderText="GC Total">
                                    <ItemTemplate>                    
                                        <%# DataBinder.Eval(Container.DataItem, "GC_Total")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                    </FooterTemplate>
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

