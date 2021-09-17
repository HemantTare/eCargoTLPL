<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDiscountReport.aspx.cs"
    Inherits="Reports_Delivery_DiscountReport" %>

<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions"
    TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>
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

function viewwindow_general(RegionID,AreaID,BranchID,From_Date,To_Date,StartDate,EndDate,Name,MONTHID,MONTHNAME)
{ 

        var Path='../../Reports/Delivery/FrmDiscountReportMonthWise.aspx?RegionID=' + RegionID + '&AreaID=' + AreaID + '&BranchID=' + BranchID + '&From_Date=' + From_Date + '&To_Date=' + To_Date + '&StartDate=' + StartDate + '&EndDate=' + EndDate + '&Name=' + Name + '&MONTHID=' + MONTHID + '&MONTHNAME=' + MONTHNAME;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
         
        window.open(Path, 'DiscountReportMonthWise', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Discount Report</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DiscountReport" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Discount Report"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 10%; height: 19px;">
                </td>
                <td style="width: 23%; height: 19px;">
                </td>
                <td style="width: 10%; height: 19px;">
                </td>
                <td style="width: 23%; height: 19px;">
                </td>
                <td style="width: 10%; height: 19px;">
                </td>
                <td style="width: 23%; height: 19px;">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    <asp:Label ID="lbl_ConsigneeName" runat="server" CssClass="LABEL" Text="Consignee Name:" /></td>
                <td style="width: 23%">
                    <asp:TextBox ID="txt_ConsigneeName" CssClass="TEXTBOX" runat="server"></asp:TextBox></td>
                <td style="width: 10%">
                </td>
                <td style="width: 23%">
                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <asp:RadioButtonList ID="rdbtn_MonthDayWise" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rdbtn_MonthDayWise_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True">Month Wise</asp:ListItem>
                        <asp:ListItem>Date Wise</asp:ListItem>
                    </asp:RadioButtonList>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
                <td colspan="2">
                    <table style="width: 100%" runat="server" id="tbl_DateRange">
                        <tr>
                            <td style="width: 100px">
                                <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 23%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 23%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 23%">
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
                <td>
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
                            <div class="DIV1" style="height: 510px; width: 960px;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" AllowCustomPaging="True"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SRNO" HeaderText="SRNO">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTHID" HeaderText="MONTHID">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ClientName" HeaderText="ClientName">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="StartDate" HeaderText="StartDate">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="EndDate" HeaderText="EndDate">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="YearID" HeaderText="YearID">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTHID" HeaderText="MONTHID">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Month Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_MONTHNAME" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "MONTHNAME") %>' />
                                                <asp:HiddenField ID="hdn_MONTHID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MONTHID") %> ' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="22%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Totol LR">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotLR" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Totol&lt;br/&gt;Articles">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Tot_Art")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Tot_Art" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total&lt;br/&gt;Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotGCAmt")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotGCAmt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="13%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total&lt;br/&gt;Discount">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotDiscount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotDiscount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="12%" />
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
