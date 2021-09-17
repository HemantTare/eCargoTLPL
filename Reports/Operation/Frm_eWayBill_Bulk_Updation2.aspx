<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_eWayBill_Bulk_Updation2.aspx.cs" Inherits="Reports_Operation_Frm_eWayBill_Bulk_Updation2" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="eWay Bill Bulk Updation2"></asp:Label>
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
                            <asp:TextBox ID="txtGSTNo" runat="server" ReadOnly="False" Width="242px">27AAECT9480C1Z5</asp:TextBox></td>
                       <td style="width: 9%;" class="TD1">
                           </td>
                        <td style="width: 24%;">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
        <td style="width:100%">
            &nbsp;</td>
      </tr>     

     <tr>
      <td style="width: 100%">
        <table style="width: 100%">
        <tr>
        <td style="width: 5%;" class="TD1"><asp:label ID="Label1" runat="server" CssClass="LABEL" Text="From Date"/></td>

        <td class="TD1" style="width: 20%;">
                
                 <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtpApplicableFrom" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="true" 
                                        OnSelectionChanged="dtpApplicableFrom_SelectionChanged">
                                    </ComponentArt:Calendar>
                                </td>
                                <td style="height: 24px" runat="server" id="TD_Calender">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdn_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                
                                <asp:AsyncPostBackTrigger ControlID="ddl_Vehicle" />
                                
                            </Triggers>
                        </asp:UpdatePanel>
                        <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtpApplicableFrom.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtpApplicableFrom.ClientObjectId %>;
                            window.<%= dtpApplicableFrom.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>
                
                
                </td>
        
        <td style="width: 8%;" class="TD1"><asp:label ID="Label2" runat="server" CssClass="LABEL" Text="To Date"/></td>
        <td style="width: 24%;">
                 <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtpApplicableTo" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="true">
                                    </ComponentArt:Calendar>
                                </td>
                                <td style="height: 24px" runat="server" id="TD1">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar2.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                       
                        <ComponentArt:Calendar runat="server" ID="Calendar2" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar2.ClientObjectId %>_loaded && window.<%= dtpApplicableTo.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar2.ClientObjectId %>.AssociatedPicker = <%= dtpApplicableTo.ClientObjectId %>;
                            window.<%= dtpApplicableTo.ClientObjectId %>.AssociatedCalendar = <%= Calendar2.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar2.ClientObjectId %>_Associate();
                        </script>

                        
        </td>
        <td style="width: 24;" class="TD1">
            Vehicle No. :&nbsp;
            <asp:DropDownList ID="ddl_Vehicle" runat="server" Width="205px">
            </asp:DropDownList>&nbsp;
        </td>
       
        <td style="width: 15%;">
            </td>
        </tr>
         </table>
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
                <asp:Button ID="btn_jason" runat="server" CssClass="BUTTON" Text="Create json" OnClick="btn_Create_jason_Click1" />
                <asp:Button id="btn_jasonAll" onclick="btn_Create_jasonAll_Click" runat="server" Text="Create json All" CssClass="BUTTON"></asp:Button></td>
                
                <asp:CheckBoxList ID="checkBoxList" runat="server"/>
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
                      
                        <asp:BoundColumn DataField="vehicleNo" HeaderText="Vehicle_No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fromPlace" HeaderText="FromPlace"></asp:BoundColumn>
                        <asp:BoundColumn DataField="transModeName" HeaderText="TransMode"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="fromstatename" HeaderText="FromState"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ewbno" HeaderText="eWayBillNo"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="rsnRemarks" HeaderText="TransDocNo"></asp:BoundColumn>
                        <asp:BoundColumn DataField="transDocDate" HeaderText="TransDocDate"></asp:BoundColumn>

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
