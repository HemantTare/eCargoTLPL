<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyBkgStateMClientDtls.aspx.cs"
    Inherits="Finance_Reports_FrmDailyBkgStateMClientDtls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Booking Statement Client Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">

function viewwindow_general(GC_ID)
{

        var Path='../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomDailyBkg', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>


</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DailyBookingStatement" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily Booking Statement Client Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="text-align: left;">
                    <div class="DIV2" style="height: 410px; width: 100%;">
                        <asp:DataGrid ID="dg_GridTBB" runat="server" ShowFooter="true" AllowPaging="false"
                            AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                            Width="90%" OnItemDataBound="dg_GridTBB_ItemDataBound">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="lightgreen" />
                            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Billing Branch" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Billing Branch")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bkg DATE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "GC DATE")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                               <%-- <asp:TemplateColumn HeaderText="LR NO" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "GC NO")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="LR NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Destination" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Destination")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cnr Name" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Cnr Name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cnee Name" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Cnee Name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Pay Mode" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Pay Mode")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Vehicle_No")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Invoice No" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Invoice_No")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
