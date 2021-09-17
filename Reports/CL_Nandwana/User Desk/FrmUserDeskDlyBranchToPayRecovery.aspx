<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUserDeskDlyBranchToPayRecovery.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmUserDeskDlyBranchToPayRecovery" %>

<%@ Register Src="../../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

 function PendingDlyStock(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DlyBrchWiseDlyStockSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  
 function PendingPDSSummary(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'PendingPDSSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
 function PendingFRTSummary(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-150;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'PendingFRTSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 

function GridDetails(Path)
{ 
    window.open(Path,'CashBalance','width=2000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    return false;
} 

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Branch Wise ToPay Recovery</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Branch Wise ToPay Recovery"></asp:Label>
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
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    &nbsp;
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
                            <div class="DIV" style="height: 510px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False"
                                    PagerStyle-HorizontalAlign="Left" Width="90%" OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Turquoise" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Branch_Name" HeaderText="Branch">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="StockLR" HeaderText="LR">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="StockPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_StockFrt" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "StockFrt") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PDSLR" HeaderText="LR">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PDSPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_PDSFrt" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "PDSFrt") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PendingFrtLR" HeaderText="LR">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PendingFrtPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_PendingFrtFrt" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "PendingFrtFrt") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TotalLR" HeaderText="LR">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalPkgs" HeaderText="Pkgs">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalFrt" HeaderText="Freight">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="ClosingCash" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_ClosingCash" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingCash") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="Gold"/>
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
