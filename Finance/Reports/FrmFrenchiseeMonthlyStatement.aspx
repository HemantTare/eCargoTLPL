<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFrenchiseeMonthlyStatement.aspx.cs"
    Inherits="Finance_Reports_FrmFrenchiseeMonthlyStatement" %>

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

function viewwindow_general(MemoNo,Memo_ID,Discount)
{ 
   
        var Path='../../Finance/Reports/FrmFrenchMthlyStmentMemoWise.aspx?MemoNo=' + MemoNo + '&Memo_ID=' + Memo_ID + '&Discount=' + Discount;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
       
        window.open(Path, 'CustomPopUpYearMonthwise', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Frenchisee Monthly Statement</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_FrenchiseeMonthlyStatement" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Frenchisee Monthly Statement"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td colspan="5">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right;">
                    &nbsp;</td>
                <td style="width: 10%; text-align: left;">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lblMonth" runat="server" CssClass="LABEL" Text="Month :" /></td>
                <td style="width: 50%; text-align: left;">
                    <asp:DropDownList ID="ddl_Month" runat="server">
                    </asp:DropDownList></td>
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
                    <asp:UpdatePanel ID="Upd_Pnl_Grid" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 80%;">
                                <asp:GridView ID="dg_Grid" runat="server" ShowFooter="True" ShowHeader="True" AllowPaging="True"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Left"
                                    PageSize="25" OnPageIndexChanging="dg_Grid_PageIndexChanging" OnRowDataBound="dg_Grid_RowDataBound">
                                    <AlternatingRowStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="InwardDate" HeaderText="Inward Date">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="18%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvNo" HeaderText="Invoice No">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="22%" />
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="Frt Discount">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "FrtDiscount") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_FrtDiscount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="ToPay Frt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ToPayFrt")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_ToPayFrt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Frt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PaidFrt")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_PaidFrt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TBB Frt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TBBFrt")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_TBBFrt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frt Discount">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_FrtDiscount" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "FrtDiscount") %>' />
                                                <asp:HiddenField ID="hdn_Memo_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Memo_ID") %> ' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_FrtDiscount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
