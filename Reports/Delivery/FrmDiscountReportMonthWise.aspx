<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDiscountReportMonthWise.aspx.cs"
    Inherits="Reports_Delivery_FrmDiscountReportMonthWise" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
 

function viewwindow_general(RegionID,AreaID,BranchID,From_Date,To_Date,StartDate,EndDate,Name,MONTHID,MONTHNAME,SelectedDate)
{ 
   
        var Path='../../Reports/Delivery/FrmDiscountReportDateWise.aspx?RegionID=' + RegionID + '&AreaID=' + AreaID + '&BranchID=' + BranchID + '&From_Date=' + From_Date + '&To_Date=' + To_Date + '&StartDate=' + StartDate + '&EndDate=' + EndDate + '&Name=' + Name + '&MONTHID=' + MONTHID + '&MONTHNAME=' + MONTHNAME + '&SelectedDate=' + SelectedDate;
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
    <title>Discount Report Month Wise</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClientWiseBookingSummary" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" >
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Discount Report Month Wise"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%; height: 19px; text-align: center">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%; text-align: center;">
                    <asp:Label ID="lblClient_Name" runat="server" CssClass="LABEL" Font-Bold="True" Font-Size="Larger"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                </td>
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
                                        <asp:BoundColumn DataField="MONTHNAME" HeaderText="MONTHNAME">
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
                                        <asp:TemplateColumn HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Date" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "ClientName") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Width="22%"  HorizontalAlign="Center"/>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Totol LR" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
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
                                        <asp:TemplateColumn HeaderText="Totol&lt;br/&gt;Articles" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
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
                                         
                                        <asp:TemplateColumn HeaderText="Total&lt;br/&gt;Freight" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
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
                                        <asp:TemplateColumn HeaderText="Total&lt;br/&gt;Discount" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
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
