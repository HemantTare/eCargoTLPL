<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_eWayBill_Bulk_Updation.aspx.cs" Inherits="Reports_Operation_Frm_eWayBill_Bulk_Updation" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="eWay Bill Bulk Updation"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
 
      <tr>
        <td style="width:100%; height: 23px;">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
      </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%;" class="TD1">
                           <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">
                            GST No. :</td>
                        <td style="width: 24%;">
                            <asp:TextBox ID="txtGSTNo" runat="server" ReadOnly="True" Width="242px">27AAECT9480C1Z5</asp:TextBox></td>
                       <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
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
                    Text="Close Window" />
                <asp:Button ID="btn_jason" runat="server" CssClass="BUTTON" OnClick="btn_Create_jason_Click1"
                    Text="Create json" /></td>
      </tr>
    </table>
    <table class="TABLE">
      <tr>
        <td>
         <asp:UpdatePanel ID="Upd_Pnl_DeliveryRegister" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <asp:Panel ID="pnl_DeliveryRegister" runat="server" ScrollBars="Auto" Height="450px">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="20" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                        <asp:BoundColumn DataField="ewbno" HeaderText="eWayBillNo"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="transModeName" HeaderText="TransMode"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="vehicletypeName" HeaderText="VehicleType"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="vehicleNo" HeaderText="Vehicle_No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="docNo" HeaderText="TransDocNo"></asp:BoundColumn>
                        <asp:BoundColumn DataField="docDate" HeaderText="TransDocDate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fromPlace" HeaderText="FromPlace"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fromstatename" HeaderText="FromState"></asp:BoundColumn>                                                                    
                        <asp:BoundColumn DataField="reasonname" HeaderText="ReasonForUpdation"></asp:BoundColumn>                                                                    
                        <asp:BoundColumn DataField="Remark" HeaderText="Remarks"></asp:BoundColumn> 


                         </Columns>
                  </asp:DataGrid>
                  </asp:Panel>
        </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
      
  </table>
    
    </form>
</body>
</html>
