<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_DailyLoadingReport.aspx.cs"
    Inherits="Reports_Operation_Frm_DailyLoadingReport" %>

<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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

function Open_Details_Window(Path)
{ 

  var txtVehicleNo = document.getElementById("txtVehicleNo");
  var From_Date = document.getElementById("hdn_From_Date");
  var To_Date = document.getElementById("hdn_To_Date");
  
  window.open(Path + "&VehicleNo=" + txtVehicleNo.value + "&From_Date=" + From_Date.value + "&To_Date=" + To_Date.value,'PaidTBB','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
  return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Loading report</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DailyLoadingReport" runat="server">
        </asp:ScriptManager>
        <table class="TABLE">
            <tr>
                <td colspan="4" class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="Daily Loading report"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; height: 22px; text-align: right;">
                </td>
                <td style="width: 30%; height: 22px;">
                </td>
                <td style="width: 5%; height: 22px;">
                </td>
                <td style="width: 30%; height: 22px;">
                </td>
            </tr>
            <tr id="BranchAreaRegion" runat="server">
                <td style="width: 15%; height: 22px; text-align: left;" colspan="4">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; height: 22px; text-align: right;">
                </td>
                <td style="width: 30%; height: 22px;">
                </td>
                <td style="width: 5%; height: 22px;">
                </td>
                <td style="width: 30%; height: 22px;">
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right;">
                    <asp:Label ID="lblVehicleNo" runat="server" Text="Vehicle No. :"></asp:Label></td>
                <td style="width: 30%;">
                    <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TEXTBOX" MaxLength="50" Width="128px"></asp:TextBox></td>
                <td style="width: 5%;">
                </td>
                <td style="width: 30%;">
                </td>
            </tr>
            <tr>
                <td style="width: 15%; height: 17px; text-align: right">
                </td>
                <td style="width: 30%; height: 17px">
                </td>
                <td style="width: 5%; height: 17px">
                </td>
                <td style="width: 30%; height: 17px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="LABEL" Text="From Date:"></asp:Label></td>
                <td style="width: 30%">
                    <table border="0" cellpadding="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                <ComponentArt:Calendar ID="dtp_From_Date" runat="server" AutoPostBackOnSelectionChanged="True"
                                    CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                                    OnSelectionChanged="dtp_From_Date_SelectionChanged" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                    PickerFormat="Custom">
                                </ComponentArt:Calendar>
                            </td>
                            <td id="TD_Calender" runat="server" style="height: 24px">
                                <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                    onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                    width="25" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_From_Date" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dtp_From_Date" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="False" AllowMultipleSelection="False"
                        AllowWeekSelection="False" CalendarCssClass="CALENDER" CalendarTitleCssClass="TITLE"
                        ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                        DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER" DayNameFormat="FirstTwoLetters"
                        ImagesBaseUrl="../../images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                        NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                        PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300">
                    </ComponentArt:Calendar>

                    <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_From_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_From_Date.ClientObjectId %>;
                            window.<%= dtp_From_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                    </script>

                </td>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lblToDate" runat="server" CssClass="LABEL" Text="To Date:"></asp:Label></td>
                <td style="width: 30%">
                    <table border="0" cellpadding="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" style="height: 24px">
                                <ComponentArt:Calendar ID="dtp_To_Date" runat="server" AutoPostBackOnSelectionChanged="True"
                                    CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                                    OnSelectionChanged="dtp_To_Date_SelectionChanged" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                    PickerFormat="Custom">
                                </ComponentArt:Calendar>
                            </td>
                            <td id="Td1" runat="server" style="height: 24px">
                                <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar2.ClientObjectId %>)"
                                    onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                    width="25" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_To_Date" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dtp_To_Date" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ComponentArt:Calendar ID="Calendar2" runat="server" AllowMonthSelection="False"
                        AllowMultipleSelection="False" AllowWeekSelection="False" CalendarCssClass="CALENDER"
                        CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                        ControlType="Calendar" DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER"
                        DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" MonthCssClass="MONTH"
                        NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY"
                        PopUp="Custom" PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY"
                        SwapDuration="300">
                    </ComponentArt:Calendar>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                </td>
                <td style="width: 30%; text-align: right;">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="Print View" OnClick="btn_view_Click" /></td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server"></uc4:Wuc_Export_To_Excel>
                </td>
            </tr>
            <tr>
                <td style="width: 15%;">
                </td>
                <td style="width: 30%;">
                </td>
                <td style="width: 10%;">
                </td>
                <td style="width: 30%;">
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
            </tr>
        </table>
        &nbsp;
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
