<%@ Page AutoEventWireup="true" CodeFile="Frm_Direct_Delivery_Details.aspx.cs" Inherits="Finance_Reports_Frm_Direct_Delivery_Details"
    Language="C#" %>

<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
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

function viewwindow_GC(GC_ID)
{
 
        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'PendingFreightGCTracking', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Direct Delivery Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Direct Delivery Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    &nbsp;<uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server"></uc1:Wuc_Region_Area_Branch>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 15px;">
                    &nbsp;<uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server"></uc2:Wuc_From_To_Datepicker>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 50px;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                            </td>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 14%">
                                &nbsp;</td>
                            <td style="width: 24%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left; height: 15px;">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="400px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="20">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="LR No.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_LRNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "LRNo") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LRDate" HeaderText="LR Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgBranch" HeaderText="Bkg Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToLocation" HeaderText="To Location"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PayMode" HeaderText="Pay Mode"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalFreight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PayingParty" HeaderText="Paying Party"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ReceivedDate" HeaderText="Dly Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ReceivedBy" HeaderText="Rec. By"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CashAmount" HeaderText="Cash Amt" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChequeAmount" HeaderText="Cheque Amt" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChequeDate" HeaderText="Cheque Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChequeNo" HeaderText="Cheque No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DebitTo" HeaderText="Debit To"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Upd_Pnl_DeliveryStockList">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID2" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
