<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClientPreviousBusinessMonthlyDetails.aspx.cs"
    Inherits="Reports_Sales_Billing_FrmClientPreviousBusinessMonthlyDetails" %>

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

function viewwindow_GC(GC_ID)
{
 
        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        window.open(Path, 'ClientBusinessGCTrace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Previous Business Check</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Client Previous Business Check"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 60%; height: 15px;" align="center">
                    <asp:Label ID="lbl_ClientName" runat="server" Text="" Font-Bold="true" Font-Size="Medium"
                        ForeColor="#660066"></asp:Label>
                </td>
                <td style="width: 40%; height: 15px;">
                    <asp:Label ID="lbl_Month" runat="server" Text="" Font-Bold="true" Font-Size="Medium"
                        ForeColor="#990033"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 60%; height: 15px;">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text="" Visible="false"></asp:Label></td>
                <td style="width: 40%; height: 15px;">
                </td>
            </tr>
        </table>
        <table id="tbl_LastBusiness" runat="server" class="TABLE">
            <tr>
                <td style="width: 100%; height: 15px; font-weight: bold; color: Purple; font-size: medium;"
                    align="Center">
                    <table width="100%" style="background-color: #ffccff;">
                        <tr>
                            <td align="Left" style="width: 100%">
                                <asp:UpdatePanel ID="Upd_Pnl1" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Panel ID="pnl_Pnl1" runat="server" Height="500px" ScrollBars="Auto">
                                            <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                                AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnItemDataBound="dg_Grid_ItemDataBound"
                                                OnPageIndexChanged="dg_Grid_PageIndexChanged" PageSize="25">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="LR No.">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_GCNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="LR Date" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "GC_Date")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalLR" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="DlyArea" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "DlyArea")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Item" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Item")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Size" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Size")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Parcels" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_Articles")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Inv. Value" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_Invoice_Value")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalInvValue" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            &nbsp;
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
