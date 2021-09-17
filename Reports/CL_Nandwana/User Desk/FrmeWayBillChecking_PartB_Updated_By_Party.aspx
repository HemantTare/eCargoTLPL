<%@ Page AutoEventWireup="true" CodeFile="FrmeWayBillChecking_PartB_Updated_By_Party.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmeWayBillChecking_PartB_Updated_By_Party"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../../JavaScript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function viewwindow_general(GC_ID)
{


        var Path='../../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWayBillChecking PART B Updated By Party</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="eWayBillChecking PART B Updated By Party"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" class="TABLE">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 60%">
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="Upd_Pnl_BillingDue" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_BillingDue" runat="server" Height="400px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="true" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="true" AllowCustomPaging="true" AutoGenerateColumns="false"
                                    PageSize="1000" OnItemDataBound="dg_Details_ItemDataBound" OnPageIndexChanged="dg_Details_PageIndexChanged">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Black" ForeColor="White" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="Silver" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="eWayBilll No." HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "eWayBillNo")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_GCNo" Text='<%# DataBinder.Eval(Container, "DataItem.GC_No") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GC_No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
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
