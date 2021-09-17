<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmeWayBillsExpiringTodayList.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmeWayBillsExpiringTodayList" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function viewwindow_GCView(GC_ID)
{
        var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function copyToClipboard(string) 
{

    var isIE = /*@cc_on!@*/false || !!document.documentMode;

    if (isIE == true)
    {
        clipboardData.setData("Text", string);
    }
    else
    {
        function handler (event)
        {
            event.clipboardData.setData('text/plain', string);
            event.preventDefault();
            document.removeEventListener('copy', handler, true);
        }

        document.addEventListener('copy', handler, true);
        document.execCommand('copy');
    }
    
    return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWay Bills Expiring Today List</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="eWay Bills Expiring Today List"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblBranchName" runat="server" Font-Size="Medium" Font-Bold="true"
                        ForeColor="#990099" Text=""></asp:Label></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%;">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 11%;">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%;">
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="Upd_Pnl_PendingPDS" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 560px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" CssClass="GRID" AllowSorting="True"
                                    AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    OnItemDataBound="dg_Grid_ItemDataBound" Width="95%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="eWayBillNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "eWayBillNo")%>
                                                &nbsp;
                                                <asp:ImageButton ID="btnCopy" runat="server" onmouseout="this.src='../../../Images/Copy.png'"
                                                    ImageUrl="~/Images/Copy.png" AlternateText="Click To Copy eWayBill No." title="Click To Copy eWayBill No." />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="eWayBillValidUpTo" HeaderText="eWayBill Valid UpTo" DataFormatString="{0:dd/MM/yyyy}"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_GCNo" Text='<%# DataBinder.Eval(Container, "DataItem.LRNo") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.LRNo") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="BkgBranch" HeaderText="Bkg Branch" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgDate" HeaderText="Bkg Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignor_Mobile_No" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
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
