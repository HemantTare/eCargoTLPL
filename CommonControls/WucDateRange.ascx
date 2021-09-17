<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDateRange.ascx.cs" Inherits="CommonControls_WucDateRange" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<script type="text/javascript">

function Picker_OnSelectionChanged(picker)
{picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate());<%=_setAttribute%>
}

function Calendar_OnSelectionChanged(calendar)
{calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());<%=_setAttribute%>}

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

function OnChanged()
{}


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
    
    function ComponentArt_<%= Calendar1.ClientObjectId %>_Associate()
    {
      if (window.<%= Calendar1.ClientObjectId %>_loaded && window.<%= Picker1.ClientObjectId %>_loaded)
      {
        window.<%= Calendar1.ClientObjectId %>.AssociatedPicker = <%= Picker1.ClientObjectId %>;
        window.<%= Picker1.ClientObjectId %>.AssociatedCalendar = <%= Calendar1.ClientObjectId %>;
      }
      else
      {
        window.setTimeout('ComponentArt_<%= Calendar1.ClientObjectId %>_Associate()', 100);
      }
    }
    ComponentArt_<%= Calendar1.ClientObjectId %>_Associate();
  
    function Validate()
    {
        if (<%=Picker.ClientID %>.GetSelectedDate() > <%=Picker1.ClientID %>.GetSelectedDate() )
        {
            alert("End Date Should Be Greater Than Start Date");
            return false;
        }
        else
            return true;
    }  



</script>




<table width="100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="CHANGE PERIOD"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 30%">
            &nbsp;</td>
        <td style="width: 70%">
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 30%">
            Start Date:</td>
        <td style="width: 70%">
            <table cellpadding="0" border="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="vertical-align: top;">
                  <ComponentArt:Calendar 
                  id="Picker" 
                  runat="server" 
                  PickerFormat="Custom" 
                  PickerCustomFormat="MMMM d yyyy" 
                  ControlType="Picker" 
                  SelectedDate="2006-12-26"
                  ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                  PickerCssClass="PICKER" CellPadding="2" /></td>
                  
                <td style="vertical-align: top;height: 26px">
                   
                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)" class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>'
                    width="25" height="22" />
                    
                </td>
              </tr>
            </table>        
        </td>
    </tr>
    
    <tr>
        <td style="width:30%;"></td>
    
        <td style="width: 70%; text-align: left;">
        
            <ComponentArt:Calendar runat="server"
              id="Calendar" 
              AllowMultipleSelection="false"
              AllowWeekSelection="false"
              AllowMonthSelection="false"
              ControlType="Calendar"
              PopUp="Custom"
              CalendarTitleCssClass="TITLE" 
              ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" 
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
            
        </td>
    </tr>
    
    
    <tr>
        <td class="TD1" style="width: 30%">
            End Date:</td>
        <td style="width: 70%">
            <table cellpadding="0" border="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" style="vertical-align: top;">
                  <ComponentArt:Calendar 
                  id="Picker1" 
                  runat="server" 
                  PickerFormat="Custom" 
                  PickerCustomFormat="MMMM d yyyy" 
                  ControlType="Picker" 
                  SelectedDate="2006-12-26"
                  ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                  PickerCssClass="PICKER" CellPadding="2" /></td>
                  
                <td style="vertical-align: top;height: 26px">
                   
                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar1.ClientObjectId %>)" class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>'
                    width="25" height="22" id="IMG1" />
                    
                </td>
              </tr>
            </table>        
        </td>
    </tr>
    
    <tr>
        <td style="width:30%;"></td>
    
        <td style="width: 70%; text-align: left;">
        
            <ComponentArt:Calendar runat="server"
              id="Calendar1" 
              AllowMultipleSelection="false"
              AllowWeekSelection="false"
              AllowMonthSelection="false"
              ControlType="Calendar"
              PopUp="Custom"
              CalendarTitleCssClass="TITLE" 
              ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" 
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
            
        </td>
    </tr>
    
    

    <tr>
        <td style="width:40%;"></td>
        <td colspan="2" style="width:60%;">
            <asp:Button ID="btn_Show" runat="server" CssClass="BUTTON" Text="Show" Width="100px" Height="22px" OnClientClick='return Validate()'  OnClick="btn_Show_Click" /></td>
    </tr>

    <tr>
        <td>&nbsp;</td>
    </tr>

</table>
