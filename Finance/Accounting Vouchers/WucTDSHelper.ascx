<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTDSHelper.ascx.cs" Inherits="FA_Common_Accounting_Vouchers_WucTDSHelper" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Finance/TDSHelper.js"></script>

<asp:ScriptManager ID="Script_update" runat="server"></asp:ScriptManager>
<%--<script type="text/javascript">

function Picker_OnSelectionChanged(ToDate)

{
  ToDate.AssociatedCalendar.SetSelectedDate(ToDate.GetSelectedDate());
}
function Calendar_OnSelectionChanged(calendar)
{
  calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());
}
function Button_OnClick(alignElement, calendar)
{
  if (calendar.PopUpObjectShowing)
  {
    calendar.Hide();
  }
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
  {
    return true;
  }
}

</script>
--%>
<table style="width: 100%" class="TABLE" >
    <tr>
        <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="TDS HELPER"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            &nbsp;</td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
          Cash/Bank Ac:</td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_CashBankAc" AutoPostBack="false"  CssClass="DROPDOWN" runat="server" >
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
       
    </tr>
     <tr>
        <td class="TD1" style="width: 30%">
           TDS Ledger Ac:</td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_TdsledgerAc" AutoPostBack="false"  CssClass="DROPDOWN" runat="server" >
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
       
    </tr>
     <tr>
        <td class="TD1" style="width: 30%">
           Deductee Status:</td>
        <td style="width: 69%">        
           <asp:DropDownList ID="ddl_DeducteeStatus" runat="server" CssClass="DROPDOWN">
           <asp:ListItem Text="Company" Value="Company" ></asp:ListItem>
           <asp:ListItem Text="Non Company" Value="Non Company"></asp:ListItem>
           </asp:DropDownList>
        
           </td>
        <td style="width: 1%">
        </td>
        
    </tr>
    <tr>   
    <td style="WIDTH: 30%" class="TD1">To Date: </td>  
        <td style="width: 70%">
            <uc1:WucDatePicker ID="ToDate" runat="server" />
           <%-- <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                        <ComponentArt:Calendar ID="ToDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="dd MMMM yyyy"
                            PickerFormat="Custom" SelectedDate="2005-12-13">
                        </ComponentArt:Calendar>
                    </td>
                    <td>
                        <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 5%">
        </td>
    </tr>
    <tr>       
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 25%">
            <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false"
                AllowWeekSelection="false" CalendarCssClass="CALENDAR" CalendarTitleCssClass="TITLE"
                ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER"  DayNameFormat="FirstTwoLetters"
                ImagesBaseUrl="~/images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                NextPrevCssClass="nextprev" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300"
                SwapSlide="LINEAR">
            </ComponentArt:Calendar>

            <script type="text/javascript">
                // Associate the picker and the calendar:
                function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                {
                  if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= ToDate.ClientObjectId %>_loaded)
                  {
                    window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= ToDate.ClientObjectId %>;
                    window.<%= ToDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                  }
                  else
                  {
                    window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                  }
                }
                ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
            </script>
--%>
        </td>
        <td style="width: 5%">
        </td>
    </tr> 
   
<tr>
<td colspan ="6" style="text-align: center">
   <asp:Button ID="btn_Ok" runat="Server" CssClass="BUTTON"  text="OK" OnClientClick="return validateUI();" OnClick="btn_Ok_Click" /> 
</td>
</tr>

  <tr>
 <td colspan="6">
 <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"  ></asp:Label>
 </td>
 </tr>
</table>
