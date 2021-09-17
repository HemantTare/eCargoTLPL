<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDetailedDailyBkgStk.aspx.cs"
  Inherits="Reports_Booking_FrmDailyBookingStock" %>

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
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Daily Booking Stock</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table runat="server" id="Table1" class="TABLE">
      <tr>
        <td class="TDGRADIENT" colspan="5">
          <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Detailed Daily Booking Stock"></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="width: 10%">
          &nbsp;<uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
        </td>
        <td style="width: 10%">
          &nbsp;</td>
        <td style="width: 10%">
          <a href="javascript:input_screen_action('view');"></a>
        </td>
        <td style="width: 10%">
          <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
            Text="Close Window" /></td>
        <td style="width: 50%">
          <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>
      </tr>
      <tr>
        <td colspan="5">
          <asp:UpdatePanel ID="Upd_Pnl_dtlsBookingRegister" UpdateMode="Conditional" runat="server">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
            </Triggers>
            <ContentTemplate>
              <div class="DIV" style="height: 510px; width: 998px;">
                <asp:DataGrid ID="dg_Grid" runat="server" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False"
                  OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                  PagerStyle-HorizontalAlign="Left" Width="900px" PageSize="25000">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="GRIDHEADERCSS" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                  <Columns>
                    <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No">
                      <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="GC_Date" HeaderText="LR Date">
                      <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="BkgBranch" HeaderText="Booking Branch">
                      <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DlyLocation" HeaderText="Delivery Location">
                      <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Consignor_name" HeaderText="Consignor Name">
                      <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Consignee_name" HeaderText="Consignee Name">
                      <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalPkgs" HeaderText="Total Packags">
                      <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ActualWt" HeaderText="Actual Wt">
                      <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalFreight" HeaderText="Total Freight">
                      <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                  </Columns>
                </asp:DataGrid>&nbsp;
              </div>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
