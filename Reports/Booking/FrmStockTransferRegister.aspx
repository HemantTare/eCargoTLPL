<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmStockTransferRegister.aspx.cs" Inherits="Reports_Booking_FrmStockTransferRegister" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
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
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="scm_StockTransferRegister" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Stock Transfer Register"></asp:Label>
        </td>
      </tr>
    </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
        <tr>
            <td style="width:100%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
        </tr>
            <tr>               
               <td style="width:100%">
              <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch2" runat="server" />
              </td>

            </tr>
            <tr>
                <td style="width: 100%">
                    <uc6:WucFilter ID="WucFilter1" runat="Server" />
                </td>
            </tr>
            <tr>
                <td style="width:100%">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
        </table>
        <table class="TABLE" >
      <tr>
            <td style="width:10%;">
              <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click"  />
            </td>
            <td style="width:10%;">
              <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>


            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
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
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" AllowPaging="True" AllowCustomPaging="true"
                                     CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                     OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="9">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>                                           
                                    
                                      <asp:TemplateColumn HeaderText="gc_caption No">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_No_of_Gc" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>                                      
                                       
                                        
                                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Bkg Branch" HeaderText="Bkg Branch"></asp:BoundColumn>                                        
                                        <asp:BoundColumn DataField="Dly Branch" HeaderText="Dly Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transfer From Branch" HeaderText="Transfer From Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transfer To Branch" HeaderText="Transfer To Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transfer Date" HeaderText="Transfer Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transferred By" HeaderText="Transferred By"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason"  ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
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
