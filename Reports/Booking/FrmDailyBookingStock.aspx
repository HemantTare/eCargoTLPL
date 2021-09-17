<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyBookingStock.aspx.cs"
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

function Open_Details_Window(Path)
    {
        window.open(Path,'dlybkgStock','width=950,height=900,top=200,left=50,menubar=no,resizable=no,scrollbars=no')
        return false;
    }

   function OpenSummary(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'BkgStockSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Daily Booking Stock</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_BookingRegister" runat="server">
    </asp:ScriptManager>
    <table runat="server" id="Table1" class="TABLE" onclick="rr()">
      <tr>
        <td class="TDGRADIENT" style="width: 100%">
          <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily Booking Stock"></asp:Label>
        </td>
      </tr>
    </table>
    <table runat="server" id="tbl_input_screen1" class="TABLE">
      <tr>
        <td style="width: 50%">
          <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
      </tr>
      <tr>
        <td style="width: 50%">
        </td>
      </tr>
    </table>
    <table runat="server" id="tbl_input_screen2" class="TABLE">
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
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button id="btn_Summary"  runat="server" Text="Summary" CssClass="BUTTON"></asp:Button>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
            Text="Close Window" /></td>
      </tr>
    </table>
    
    <table runat="server" id="tbl_input_screen3" class="TABLE" visible="false">
      <tr>
        <td style="width: 50%">
          <asp:Label ID="lbl_BranchName" Text="" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="DarkViolet" ></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="width: 50%">
        </td>
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
                  OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                  PagerStyle-HorizontalAlign="Left" Width="700px" PageSize="500" OnItemCommand="dg_Grid_ItemCommand">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="GRIDHEADERCSS" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                  <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="SrNo">
                      <HeaderStyle CssClass="HIDEGRIDCOL" />
                      <ItemStyle CssClass="HIDEGRIDCOL" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DlyAreaID" HeaderText="DlyAreaID">
                      <HeaderStyle CssClass="HIDEGRIDCOL" />
                      <ItemStyle CssClass="HIDEGRIDCOL" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DlyLocationID" HeaderText="DlyLocationID">
                      <HeaderStyle CssClass="HIDEGRIDCOL" />
                      <ItemStyle CssClass="HIDEGRIDCOL" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Delivery Area">
                      <ItemTemplate>
                        <asp:LinkButton ID="lbtn_DeliveryArea" Text='<%# DataBinder.Eval(Container, "DataItem.DlyAreaName") %>'
                          Font-Bold="True" Font-Underline="True" runat="server" CommandName="deliveryarea" 
                          CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DlyAreaID") %>'/>
                        <asp:HiddenField ID="hdn_DeliveryArea" runat="server" Value ='<%# DataBinder.Eval(Container, "DataItem.DlyAreaID") %>' />
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Delivery Location">
                      <ItemTemplate>
                        <asp:LinkButton ID="lbtn_DeliveryLocation" Text='<%# DataBinder.Eval(Container, "DataItem.DlyLocationName") %>'
                          Font-Bold="True" Font-Underline="True" runat="server" CommandName="deliverylocation" 
                          CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DlyLocationID") %>'/>
                        <asp:HiddenField ID="hdn_DeliveryLocation" runat="server" Value ='<%# DataBinder.Eval(Container, "DataItem.DlyLocationID") %>' />
                      
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="TotalLR" HeaderText="Total LR">
                      <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalPkgs" HeaderText="Total Pkgs">
                      <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalFreight" HeaderText="Total Freight">
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
