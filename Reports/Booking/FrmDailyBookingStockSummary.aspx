<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyBookingStockSummary.aspx.cs"
    Inherits="Reports_Booking_FrmDailyBookingStockSummary" %>

<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions"
    TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
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

function Open_Details_Window(Path)
    {
        window.open(Path,'dlybkgStock','width=950,height=900,top=200,left=50,menubar=no,resizable=no,scrollbars=no')
        return false;
    }

    
function viewwindow_ParentWindow(PathDlyStk)
{
          
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1090;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2; 
              
        window.open(PathDlyStk, 'BookingStockSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Booking Stock Summary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily Booking Stock Summary" BackColor=></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 50%">
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
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    &nbsp;&nbsp;
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
                            <div class="DIV" style="height: 510px; width: 998px;">
                                <asp:DataGrid ID="dg_Grid" runat="server" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False"
                                    PagerStyle-HorizontalAlign="Left" Width="700px" OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Branch" Text='<%# DataBinder.Eval(Container, "DataItem.Branch_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.BkgBranchId") %>' />
                                                <asp:HiddenField ID="hdn_Branch_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.BkgBranchId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="175px" />
                                            <ItemStyle Width="175px" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="SuratPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SuratFreight" HeaderText="Freight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VapiPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VapiFreight" HeaderText="Freight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GujPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GujFreight" HeaderText="Freight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PunePkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PuneFreight" HeaderText="Freight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalPkgs" HeaderText="TotalPkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalFreight" HeaderText="TotalFreight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
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
