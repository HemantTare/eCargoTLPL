<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchWiseMonthlyPerformance.aspx.cs"
    Inherits="Finance_Reports_FrmBranchWiseMonthlyPerformance" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
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
    <title>Branch Wise Monthly Performance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch Wise Monthly Performance"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%; height: 41px;" class="TD1">
                                <asp:Label ID="lblMonth" runat="server" CssClass="LABEL" Text="Month :" /></td>
                            <td style="width: 24%; height: 41px;">
                                <asp:DropDownList ID="ddl_Month" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td colspan="4" style="height: 41px">
                                <asp:RadioButtonList ID="rdl_BookingRegister" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Booking BranchWise</asp:ListItem>
                                    <asp:ListItem Value="1">Delivery BranchWise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
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
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 100%;">
                                <asp:GridView ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True"  
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Left"
                                    PageSize="25" OnPageIndexChanging="dg_Grid_PageIndexChanging" OnRowCreated="dg_Grid_RowCreated" OnRowDataBound="dg_Grid_RowDataBound" ShowHeader="False">
                                    <AlternatingRowStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurTotalLR" HeaderText="LR">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurTotalArticles" HeaderText="Articles">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurTotalFrt" HeaderText="Freight">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PrevTotalLR" HeaderText="LR">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PrevTotalArticles" HeaderText="Articles">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PrevTotalFrt" HeaderText="Freight">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiffLR" HeaderText="LR">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiffArticles" HeaderText="Articles">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiffFrt" HeaderText="Freight">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <%--<asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" AllowCustomPaging="True"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                     PagerStyle-HorizontalAlign="Left" PageSize="25" OnItemCreated="dg_Grid_ItemCreated">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Branch_Name" HeaderText="Branch Name">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CurTotalLR" HeaderText="LR">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CurTotalArticles" HeaderText="Articles">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CurTotalFrt" HeaderText="Freight">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PrevTotalLR" HeaderText="LR">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PrevTotalArticles" HeaderText="Articles">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PrevTotalFrt" HeaderText="Freight">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn> 
                                        <asp:BoundColumn DataField="DiffLR" HeaderText="LR">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DiffArticles" HeaderText="Articles">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DiffFrt" HeaderText="Freight">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundColumn>  
                                    </Columns>
                                </asp:DataGrid>--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
