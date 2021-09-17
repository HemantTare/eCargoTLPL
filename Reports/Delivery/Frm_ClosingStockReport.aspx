<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_ClosingStockReport.aspx.cs"
    Inherits="Reports_Delivery_Frm_ClosingStockReport" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
 
 
function GridClosingStock(Path)
{ 
    window.open(Path,'PaidPendingPDS','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    return false;
}
function Open_Details_Window(Path)
{ 
    window.open(Path,'DDCTempoFrgt','width=450,height=500,top=100,left=300,menubar=no,resizable=no,scrollbars=no')
    return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Stock Report</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClosingStockReport" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Stock Report"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%; height: 17px; text-align: right">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 50%; height: 17px">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 15px; text-align: right">
                    <asp:Label ID="lbl_branch_cation" runat="server" Text="Branch:"></asp:Label></td>
                <td style="width: 10%; height: 15px">
                    <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="false" CssClass="DROPDOWN">
                    </asp:DropDownList></td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 10%; height: 15px">
                    <asp:Label ID="lbl_AsOnDate" runat="server" Text="As On Date :"></asp:Label></td>
                <td style="width: 50%; height: 15px">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" /></td>
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
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridClosingStock" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 510px; width: 100%;">
                                <asp:Label ID="lbl_ClosingStock" runat="server" Font-Bold="True" Visible="False"
                                    Font-Size="Large" Text="      Closing Stock      " BackColor="#FFE0C0" ForeColor="#C04000"
                                    Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridClosingStock" runat="server" ShowFooter="true" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_GridClosingStock_PageIndexChanged" Width="60%" OnItemDataBound="dg_GridClosingStock_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Description" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <%--<ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </ItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Description" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                                <asp:HiddenField ID="hdn_SrNo" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px"  />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LR" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalPkgs")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ToPay Frt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalToPay")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <hr />
                                <asp:Label ID="lblPendingPDS" runat="server" Font-Bold="True" Visible="False" Font-Size="Large"
                                    Text="      Pending PDS     " BackColor="#FFE0C0" ForeColor="#C04000" Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridPendingPDS" runat="server" ShowFooter="true" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnItemDataBound="dg_GridPendingPDS_ItemDataBound" Width="30%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" BackColor="lightgreen" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="PDS Date" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%-- <asp:Label ID="lbl_PDS_Date" runat="server" CssClass="LABEL" Font-Bold="true"
                                                    BackColor="lightgreen"></asp:Label>--%>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No Of PDS" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:Label ID="lbl_TotalPDS" runat="server" CssClass="LABEL" Font-Bold="true" BackColor="lightgreen"></asp:Label>--%>
                                            </FooterTemplate>
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
