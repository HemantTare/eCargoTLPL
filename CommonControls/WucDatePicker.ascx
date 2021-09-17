<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDatePicker.ascx.cs" Inherits="CommonControls_WucDatePicker" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Date_Picker.js"></script>

<script type="text/javascript">

// JScript File
function Picker_OnSelectionChanged(picker)
{
picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate());<%=_injectJSfunction%>
DateChange();
}

function Calendar_OnSelectionChanged(calendar)
{
calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());<%=_injectJSfunction%>
DateChange();
}

function DateChange()
{
var hdnMenuItemID = document.getElementById('dtpLoadingDate_hdnMenuItemID');

if (hdnMenuItemID != null && hdnMenuItemID.value == "262")// daily vehicle loading plan
  {
  DVLPDateChange();
  }
}

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


function DisablePicker(PickerClientID)
{
//alert('1');
var  DateString='<%=Picker.SelectedDate.ToShortDateString()%>'
var splitted=DateString.split('/');
var PickerDate=new Date();
PickerDate.setDate(splitted[0])
PickerDate.setMonth(splitted[1]-1)
PickerDate.setFullYear(splitted[2])
PickerClientID.SetSelectedDate(PickerDate);
}
</script>


<table  border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%">
        
           <table cellpadding="0" border="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="vertical-align: top;">
                  <ComponentArt:Calendar  
                  id="Picker" 
                  runat="server" 
                  PickerFormat="Custom" 
                  ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                  PickerCustomFormat="MMMM d yyyy" 
                  ControlType="Picker" 
                  SelectedDate="2006-12-26"
                  PickerCssClass="PICKER" CellPadding="2" OnSelectionChanged="Picker_SelectionChanged" /></td>
                  
                <td style="vertical-align: top;height: 26px" runat="server" id="TD_Calender">
                   
                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)" class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>'
                    width="25" height="22" />
                    
                </td>
              </tr>
            </table>        
        
        
        </td>
    </tr>
    <tr>
        <td style="width: 100%; text-align: left;">
        
            <ComponentArt:Calendar runat="server"
              id="Calendar"  
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
              NextImageUrl="cal_nextMonth.gif" OnSelectionChanged="Calendar_SelectionChanged"/>  
              
              
            <script type="text/javascript">
            // Associate the picker and the calendar:
            function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
            {
              if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= Picker.ClientObjectId %>_loaded)
              {
                window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= Picker.ClientObjectId %>;
                window.<%= Picker.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
              }
              else
              {
                window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
              }
            }
            ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
            </script>                
                    

          <asp:HiddenField ID="hdnMenuItemID" runat="server" />
        </td>
    </tr>
</table>
