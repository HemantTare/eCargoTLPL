// JScript File
function Picker_OnSelectionChanged(picker)
{picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate());}

function Calendar_OnSelectionChanged(calendar)
{calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());}

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

