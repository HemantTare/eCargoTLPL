<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucGridSearch.ascx.cs"
    Inherits="CommonControls_WucGridSearch" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript">
function validate()
 {
    var txt_PageNo = document.getElementById('<%=txt_PageNo.ClientID%>');
    var ats = false;
        if(parseFloat(txt_PageNo.value)<=0 || txt_PageNo.value=='')
        {
            alert('Please Enter Page No to Search');
            txt_PageNo.focus();
        }
        else
         ats = true;
         
         return ats;
 } 
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

<table style="width: 100%">
    <tr>
        <td style="width: 65%" align="center">
            <table style="width: 70%" id="tbl_DateChange" runat="server">
                <tr>
                    <td class="TD1" style="width: 15%;">
                        <asp:Label ID="lbl_StartDate" runat="server" Text="Start Date:" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 17%;">
                        <table cellpadding="0" border="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="vertical-align: top;">
                                    <ComponentArt:Calendar ID="Picker" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                        ControlType="Picker" SelectedDate="2006-12-26" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        PickerCssClass="PICKER" CellPadding="2" />
                                </td>
                                <td style="vertical-align: top;">
                                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>' width="25" height="22" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 15%;" class="TD1">
                        <asp:Label ID="lbl_EndDate" runat="server" Text="End Date:" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 18%;">
                        <table cellpadding="0" border="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" style="vertical-align: top;">
                                    <ComponentArt:Calendar ID="Picker1" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                        ControlType="Picker" SelectedDate="2006-12-26" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        PickerCssClass="PICKER" CellPadding="2" />
                                </td>
                                <td style="vertical-align: top;">
                                    <img alt="" onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" onclick="Button_OnClick(this, <%= Calendar1.ClientObjectId %>)"
                                        class="CALENDAR_BUTTON" src='<%=Calendar_Img_Path %>' width="25" height="22"
                                        id="IMG1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5%;">
                        <asp:Button ID="btn_ChangePeriod" runat="server" Text="Change Period" CssClass="BUTTON"
                            OnClick="btn_ChangePeriod_Click" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%">
                    </td>
                    <td style="width: 17%">
                        <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false"
                            AllowWeekSelection="false" CalendarCssClass="CALENDER" CalendarTitleCssClass="TITLE"
                            ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                            DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER" DayNameFormat="FirstTwoLetters"
                            ImagesBaseUrl="~/Images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                            NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                            PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300"
                            SwapSlide="Linear">
                        </ComponentArt:Calendar>
                    </td>
                    <td class="TD1" style="width: 15%">
                    </td>
                    <td style="width: 18%">
                        <ComponentArt:Calendar ID="Calendar1" runat="server" AllowMonthSelection="false"
                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="CALENDER"
                            CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            ControlType="Calendar" DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER"
                            DayNameFormat="FirstTwoLetters" ImagesBaseUrl="~/Images/" MonthCssClass="MONTH"
                            NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY"
                            PopUp="Custom" PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY"
                            SwapDuration="300" SwapSlide="Linear">
                        </ComponentArt:Calendar>
                    </td>
                    <td style="width: 5%">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 65%" align="center">
            <table style="width: 70%" id="tbl_GridSearch" runat="server">
                <tr>
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lbl_Select" runat="server" Text="Select:" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 17%">
                        <asp:DropDownList ID="ddl_Search" runat="server" CssClass="DROPDOWN" Width="98%">
                        </asp:DropDownList></td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txt_Search" runat="server" CssClass="TEXTBOX" Width="98%"></asp:TextBox></td>
                    <td style="width: 10%">
                        <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="BUTTON" Width="100%"
                            OnClick="btn_Search_Click" /></td>
                    <td style="width: 7%" id="td_txtPageNo" runat="server">
                        <asp:TextBox ID="txt_PageNo" runat="server" onkeypress="return Only_Integers(this,event)"
                            CssClass="TEXTBOXNOS" Width="70%"></asp:TextBox></td>
                    <td style="width: 6%" id="td_BtnGo" runat="server">
                        <asp:Button ID="btn_Go" runat="server" OnClientClick="return validate()" Text="Go"
                            CssClass="BUTTON" Width="100%" OnClick="btn_Go_Click" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
