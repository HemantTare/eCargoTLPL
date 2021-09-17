<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmIncompleteProcess.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmIncompleteProcess" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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
function viewwindow_general(DocType,DocNo)
    {
    var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 500;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUpGC6767', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
          return false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incoming Vehicle</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_FrmIncompleteProcess" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Incomplete Process"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td>
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="TABLE">
                        <tbody>
                            <tr>
                                <td style="width: 20%; text-align: right;">
                                    <asp:Label ID="lblReportType" runat="server" CssClass="LABEL" Text="Pending Process:"></asp:Label></td>
                                <td style="width: 30%; text-align: left;">
                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="width: 20%; text-align: right;">
                                    <asp:Label ID="lblMemoType" runat="server" CssClass="LABEL" Text="Memo Type:" Visible="False"></asp:Label></td>
                                <td style="width: 30%; text-align: left;">
                                    <asp:DropDownList ID="ddlMemoType" runat="server" CssClass="DROPDOWN" Visible="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">
                                    <asp:Label ID="lblMemoToLocation" runat="server" CssClass="LABEL" Text="Memo To Location:"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="width: 30%; text-align: left;">
                                    <asp:TextBox ID="txtMemoToLocation" runat="server" CssClass="TEXTBOX" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 30%">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%;">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%;">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 11%;">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_IncommingTrucksAlert" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridPendingMEMOforLHPO" />
                            <asp:AsyncPostBackTrigger ControlID="dg_GridLHPOforUnLoading" />
                            <asp:AsyncPostBackTrigger ControlID="dg_GridPDSforDlyConfirm" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 510px; width: 998px;">
                                <asp:DataGrid ID="dg_GridPendingMEMOforLHPO" runat="server" ShowFooter="true" AllowPaging="True"
                                    AllowCustomPaging="true" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_GridPendingMEMOforLHPO_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    PageSize="15" OnItemDataBound="dg_GridPendingMEMOforLHPO_ItemDataBound" Width="95%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <%--<asp:BoundColumn DataField="Sr No." HeaderText="Sr No."></asp:BoundColumn>--%>
                                        <%--<asp:BoundColumn DataField="LHPONo" HeaderText="LHPONo"></asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="gc_caption" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LRNO")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalRecordCount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LRDate" HeaderText="LR Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceNo" HeaderText="Invoice No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceDate" HeaderText="Invoice Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceFrom" HeaderText="Invoice From"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceTo" HeaderText="Invoice To"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceType" HeaderText="Invoice Type"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="lhpo_caption" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TripMemoNo")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:Label ID="lblTotalRecordCount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>--%>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TripMemoDate" HeaderText="Trip Memo Date"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No Of Parcels" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "NoOfParcel")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotal_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total LR Amount" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalGCAmount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotalGCAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PaymentType" HeaderText="Payment Type"></asp:BoundColumn>
                                         
                                    </Columns>
                                </asp:DataGrid>
                                <asp:DataGrid ID="dg_GridLHPOforUnLoading" runat="server" ShowFooter="True" AllowPaging="True"
                                    AllowCustomPaging="True" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_GridLHPOforUnLoading_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    PageSize="15" OnItemDataBound="dg_GridLHPOforUnLoading_ItemDataBound" Width="90%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Invoice From">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "InvoiceFrom")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblUnLoadingTotalRecordCount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="InvoiceTo" HeaderText="Invoice To">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceNo" HeaderText="Invoice No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceDate" HeaderText="Invoice Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VehicleNo" HeaderText="Vehicle No">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Total GC">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalGC")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotalGC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Articles">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "NoOfParcel")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotalArticles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <asp:DataGrid ID="dg_GridPDSforDlyConfirm" runat="server" ShowFooter="true" AllowPaging="True"
                                    AllowCustomPaging="true" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_GridPDSforDlyConfirm_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    PageSize="15" OnItemDataBound="dg_GridPDSforDlyConfirm_ItemDataBound" Width="90%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <%--<asp:BoundColumn DataField="Sr No." HeaderText="Sr No."></asp:BoundColumn>--%>
                                        <%--<asp:BoundColumn DataField="PDSBranch" HeaderText="PDS Branch"></asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="PDS Branch" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PDSBranch")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDlyConfirmTotalRecordCount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PDSNo" HeaderText="PDS No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PDSDate" HeaderText="PDS Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyMode" HeaderText="Dly Mode">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyModeDescription" HeaderText="Dly Mode Description">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <%--<asp:BoundColumn DataField="TotalPDSGC" HeaderText="Total PDS GC"></asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="Total PDS GC" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalPDSGC")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotalPDSGC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <%--<asp:BoundColumn DataField="TotalPDSArticles" HeaderText="Total PDS Articles"></asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="Total PDS Articles" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalPDSArticles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTotalPDSArticles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
