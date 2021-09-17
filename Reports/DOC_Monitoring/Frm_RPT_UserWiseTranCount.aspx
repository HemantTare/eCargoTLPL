<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_RPT_UserWiseTranCount.aspx.cs" Inherits="Reports_CL_Nandwana_DOC_Monitoring_Frm_RPT_UserWiseTranCount" %>

<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>

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
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Userwise Document Count"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
       <tr>
        <td colspan="2" style="width:100%;">
          &nbsp;</td>
      </tr>
      <tr>
        <td style="width:12%">
            Transaction Date
        </td>
        
        <td style="width:88%">
            <uc2:WucDatePicker ID="WucDatePicker1" runat="server" />
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
        <td  style="width: 100%">
          <%--<asp:UpdatePanel ID="Upd_Pnl_Document_Statistic" UpdateMode="Conditional" runat="server">--%>
              <%--<Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>--%>
              <%--<ContentTemplate>--%>
                <%--<asp:Panel ID="pnl_Document_Statistic" Width="100%" runat="server" ScrollBars="Auto" Height="1000px">--%>
                     <%--<div class="DIV" style="height: 250px; width:976px;" >--%>
                      <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" >

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns >
                         <asp:BoundColumn DataField="EmpName" HeaderText="User Name"   ItemStyle-Width = "100px"/>
                         <asp:BoundColumn DataField="TotalWalkInClient" HeaderText="Walk-In Client"/>
                         <asp:BoundColumn DataField="TotalRegularClient" HeaderText="Regular Client"/>
                         <asp:BoundColumn DataField="TotalContract" HeaderText="Contract"/>
                         <asp:BoundColumn DataField="TotalVendor" HeaderText="Vendor"/>
                         <asp:BoundColumn DataField="TotalVehicle" HeaderText="Vehicle"/>
                         <asp:BoundColumn DataField="TotalDriver" HeaderText="Driver"/>
                         <asp:BoundColumn DataField="TotalLedger" HeaderText="Ledger"/>
                         <asp:BoundColumn DataField="TotalCostCenter" HeaderText="Cost Center"/>
                         <asp:BoundColumn DataField="TotalLR" HeaderText="LR"/>
                         <asp:BoundColumn DataField="TotalMemo" HeaderText="Menifest"/>
                         <asp:BoundColumn DataField="TotalTripMemo" HeaderText="Trip Menifest"/>
                         <asp:BoundColumn DataField="TotalOctroiUpdate" HeaderText="Octroi Update"/>
                         <asp:BoundColumn DataField="TotalDlyBranchUpdate" HeaderText="Dly Branch Update"/>
                         <asp:BoundColumn DataField="TotalConsigneeUpdate" HeaderText="Consignee Update"/>
                         <asp:BoundColumn DataField="TotalAUS" HeaderText="AUS"/>
                         <asp:BoundColumn DataField="TotalPreDly" HeaderText="Pre Dly"/>
                         <asp:BoundColumn DataField="TotalDly" HeaderText="Dly"/>
                         <asp:BoundColumn DataField="TotalPODCover" HeaderText="POD Cover"/>
                         <asp:BoundColumn DataField="TotalPODReceipt" HeaderText="POD Reciept"/>
                         <asp:BoundColumn DataField="TotalPODDly" HeaderText="POD Dly"/>
                         <asp:BoundColumn DataField="TotalTransportBill" HeaderText="Transport Bill"/>
                         <asp:BoundColumn DataField="TotalContraVoucher" HeaderText="Contra Voucher"/>
                         <asp:BoundColumn DataField="TotalCreditNote" HeaderText="Credit Note"/>
                         <asp:BoundColumn DataField="TotalDebitNote" HeaderText="Debit Note"/>
                         <asp:BoundColumn DataField="TotalJournal" HeaderText="Journal"/>
                         <asp:BoundColumn DataField="TotalPayment" HeaderText="Payment"/>
                         <asp:BoundColumn DataField="TotalReciept" HeaderText="Reciept"/>
                         <asp:BoundColumn DataField="TotalBTH" HeaderText="BTH"/>
                         <asp:BoundColumn DataField="TotalDoorDly" HeaderText="Door Dly"/>
                         <asp:BoundColumn DataField="TotalATH" HeaderText="ATH"/>
                         <asp:BoundColumn DataField="TotalMRDly" HeaderText="MR Dly"/>
                         <asp:BoundColumn DataField="TotalCreditMemo" HeaderText="Credit Memo"/>
                         <asp:BoundColumn DataField="TotalPartyReceipt" HeaderText="Party Reciept"/>
                         <asp:BoundColumn DataField="TotalLocalCartage" HeaderText="Local Cartage"/>
                      </Columns>
                  </asp:DataGrid>
                  <%--</div>--%>
             <%--  </asp:Panel>
              </ContentTemplate>--%>
          <%--</asp:UpdatePanel>--%>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
