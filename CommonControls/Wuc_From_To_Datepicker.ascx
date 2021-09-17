<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_From_To_Datepicker.ascx.cs" Inherits="CommonControls_Wuc_From_To_Datepicker" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript">

// JScript File
function Picker_OnSelectionChanged(picker)
{picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate())}

function Calendar_OnSelectionChanged(calendar)
{calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate())}

function Button_OnClick(alignElement, calendar)
{
if (calendar.PopUpObjectShowing)
    {calendar.Hide();}
    else
    {
    calendar.SetSelectedDate(calendar.AssociatedPicker.GetSelectedDate());
    calendar.Show(alignElement);
    }
}

function Button_OnMouseUp(calendar)
{
if (calendar.PopUpObjectShowing)
    {
    event.cancelBubble=true;
    event.returnValue=false;
    return false;
    }
else
    {return true;}
}
</script>

<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td class="TD1" id="td_from_date_caption" runat="server" style="width: 10%">
          <asp:Label ID="lbl_from_date_Caption" runat="server" Text="From Date:" />
        </td>
        
        <td id="td_from_date_data" runat="server" style="width: 23%">
           <table cellpadding="0" border="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar_From_Date.ClientObjectId %>)" style="vertical-align: top;">
                  <ComponentArt:Calendar  
                  id="Picker_From_Date" 
                  runat="server" 
                  PickerFormat="Custom" 
                  ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                  PickerCustomFormat="MMMM d yyyy" 
                  ControlType="Picker" 
                  SelectedDate="2006-12-26"
                  PickerCssClass="PICKER" CellPadding="2"/></td>
                  
                <td style="vertical-align: top;height: 26px">
                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar_From_Date.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar_From_Date.ClientObjectId %>)" class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>'
                    width="25" height="22" />
                </td>
              </tr>
            </table>
        </td>
        
        <td id="td_to_date_caption" runat="server" style="width: 10%">
          <asp:Label ID="lbl_to_date_Caption" runat="server" Text="To Date:" />
        </td>
        
        <td id="td_to_date_data" runat="server" style="width: 23%">
           <table cellpadding="0" border="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar_To_Date.ClientObjectId %>)" style="vertical-align: top;">
                  <ComponentArt:Calendar  
                  id="Picker_To_Date" 
                  runat="server" 
                  PickerFormat="Custom" 
                  ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                  PickerCustomFormat="MMMM d yyyy" 
                  ControlType="Picker" 
                  SelectedDate="2006-12-26"
                  PickerCssClass="PICKER" CellPadding="2"/></td>
                  
                <td style="vertical-align: top;height: 26px">
                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar_To_Date.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar_To_Date.ClientObjectId %>)" class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>'
                    width="25" height="22" />
                </td>
              </tr>
            </table>
        </td>
        
        <td id="td_blank_caption" runat="server" style="width: 10%">&nbsp;</td>
        <td id="td_blank_data_caption" runat="server" style="width: 23%">&nbsp;</td>
    </tr>
    <tr>
        <td id="td1" runat="server">&nbsp</td>
        <td style="text-align: left;">
            <ComponentArt:Calendar runat="server"
              id="Calendar_From_Date"  
              AllowMultipleSelection="false"
              AllowWeekSelection="false"
              AllowMonthSelection="false"
              ControlType="Calendar"
              PopUp="Custom"
              ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
              CalendarTitleCssClass="TITLE" 
              DayHeaderCssClass="DAYHEADER" 
              DayCssClass="DAY" 
              DayHoverCssClass="DAYHOVER" 
              OtherMonthDayCssClass="OTHERMONTHDAY" 
              SelectedDayCssClass="SELECTEDDAY" 
              CalendarCssClass="CALENDER" 
              NextPrevCssClass="NEXTPREV" 
              MonthCssClass="MONTH"
              SwapSlide="Linear"
              SwapDuration="300"
              DayNameFormat="FirstTwoLetters"
              ImagesBaseUrl="~/Images/"
              PrevImageUrl="cal_prevMonth.gif" 
              NextImageUrl="cal_nextMonth.gif"/>  
              
              
            <script type="text/javascript">
            // Associate the picker and the calendar:
            function ComponentArt_<%= Calendar_From_Date.ClientObjectId %>_Associate()
            {
              if (window.<%= Calendar_From_Date.ClientObjectId %>_loaded && window.<%= Picker_From_Date.ClientObjectId %>_loaded)
              {
                window.<%= Calendar_From_Date.ClientObjectId %>.AssociatedPicker = <%= Picker_From_Date.ClientObjectId %>;
                window.<%= Picker_From_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar_From_Date.ClientObjectId %>;
              }
              else
              {
                window.setTimeout('ComponentArt_<%= Calendar_From_Date.ClientObjectId %>_Associate()', 100);
              }
            }
            ComponentArt_<%= Calendar_From_Date.ClientObjectId %>_Associate();
            </script>                
                    
        
        </td>
        
        <td id="td2" runat="server">&nbsp</td>
        <td style="text-align: left;">
            <ComponentArt:Calendar runat="server"
              id="Calendar_To_Date"  
              AllowMultipleSelection="false"
              AllowWeekSelection="false"
              AllowMonthSelection="false"
              ControlType="Calendar"
              PopUp="Custom"
              ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
              CalendarTitleCssClass="TITLE" 
              DayHeaderCssClass="DAYHEADER" 
              DayCssClass="DAY" 
              DayHoverCssClass="DAYHOVER" 
              OtherMonthDayCssClass="OTHERMONTHDAY" 
              SelectedDayCssClass="SELECTEDDAY" 
              CalendarCssClass="CALENDER" 
              NextPrevCssClass="NEXTPREV" 
              MonthCssClass="MONTH"
              SwapSlide="Linear"
              SwapDuration="300"
              DayNameFormat="FirstTwoLetters"
              ImagesBaseUrl="~/Images/"
              PrevImageUrl="cal_prevMonth.gif" 
              NextImageUrl="cal_nextMonth.gif"/>  
              
              
            <script type="text/javascript">
            // Associate the picker and the calendar:
            function ComponentArt_<%= Calendar_To_Date.ClientObjectId %>_Associate()
            {
              if (window.<%= Calendar_To_Date.ClientObjectId %>_loaded && window.<%= Picker_To_Date.ClientObjectId %>_loaded)
              {
                window.<%= Calendar_To_Date.ClientObjectId %>.AssociatedPicker = <%= Picker_To_Date.ClientObjectId %>;
                window.<%= Picker_To_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar_To_Date.ClientObjectId %>;
              }
              else
              {
                window.setTimeout('ComponentArt_<%= Calendar_To_Date.ClientObjectId %>_Associate()', 100);
              }
            }
            ComponentArt_<%= Calendar_To_Date.ClientObjectId %>_Associate();
            </script>                
                    
        
        </td>
    </tr>
</table>
