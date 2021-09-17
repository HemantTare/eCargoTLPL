<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMRGeneralDetails.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucMRGeneralDetails" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript">

function validateGeneralDetails(lbl_Error)
{
    var GC_No = document.getElementById('<%=txt_GCNo.ClientID %>');
    var hdn_GCCaption = document.getElementById('<%=hdn_GCCaption.ClientID %>');
    
    if(GC_No.value == '')
    {
        lbl_Error.innerText = 'Please Enter '+ hdn_GCCaption.value +' No.';
        GC_No.focus();
        return false;
    }
    return true;
}
</script>

<asp:Panel ID="Panel1" runat="server" Width="100%">
<table width="100%">
   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_MRNo" runat="server" Text="MR No. :" CssClass="LABEL" ></asp:Label></td>
        <td colspan="2"><%--BackColor="Transparent" BorderColor="Transparent" BorderStyle="Solid" ReadOnly="True" --%>
        <asp:TextBox ID="txt_MRNo" CssClass="TEXTBOX" onkeypress= "return Only_Integers(this,event)" runat="server" Font-Bold="True" Width="45%"></asp:TextBox>
               <asp:Label ID="lbl_Start_End_No" runat="server" CssClass="LABEL" Width="50%" Font-Bold="true"></asp:Label>
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_MRDate" runat="server" Text="MR Date :" CssClass="LABEL" ></asp:Label></td>
        <td style="width: 29%;" class="TDMANDATORY">
             <table border="0" cellpadding="0" width="100%">
                 <tr>
                     <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px;width:40%" >
                        <ComponentArt:Calendar ID="Wuc_MRDate" runat="server" 
                             CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                             ControlType="Picker" PickerCssClass="PICKER" 
                             PickerCustomFormat="dd MMM yyyy"
                             PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="MrDateChange"> 
                        </ComponentArt:Calendar>
                     </td>
                     <td style="height: 24px;width:15%" runat="server" id="td_cal">
                         <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                         onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                         width="25" />
                     </td>
                     <td class="TD1" style="width:45%">
                     </td> 
                 </tr>
              </table>              
          </td> 
        <td style="width: 1%">
        </td>
    </tr>    
     <tr>
        <td class="TD1" style="width: 20%"></td>
        <td  style="width: 29%"> </td>
        <td class="TD1"> </td>
        <td class="TD1" style="width: 20%"> </td>
        <td  style="width: 29%">
            <ComponentArt:Calendar runat="server" id="Calendar" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom"  CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" 
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" 
                OtherMonthDayCssClass="OTHERMONTHDAY" SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" 
                NextPrevCssClass="NEXTPREV" MonthCssClass="MONTH" SwapDuration="300"
                DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif"/>
                <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= Wuc_MRDate.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= Wuc_MRDate.ClientObjectId %>;
                        window.<%= Wuc_MRDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                      }
                      else
                      {
                        window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                      }
                    }
                     ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
               </script>   
        </td>
        <td  style="width: 1%"></td>
    </tr> 
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_GCNo" runat="server" CssClass="LABEL" Text="GC No. :" ></asp:Label></td>
        <td style="width: 29%">
        <asp:TextBox ID="txt_GCNo" runat="server" CssClass="TEXTBOX" Width="50%"></asp:TextBox>&nbsp;
        <asp:Button ID="btn_GetDetails" runat="server" CssClass="BUTTON" Width="30%" Text="Get Details" OnClick="btn_GetDetails_Click" /> 
        </td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BookingDate" runat="server" CssClass="LABEL" Text="Booking Date :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_BookingDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
  
    <tr>
        <td style="width: 20%" class="TD1">
            
            <asp:Label ID="lbl_BookingBranch" runat="server" CssClass="LABEL" Text="Booking Branch :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_BookingBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_DeliveryBranch" runat="server" CssClass="LABEL" Text="Delivery Branch :" ></asp:Label>
            </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_DeliveryBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Consignor" runat="server" CssClass="LABEL" Text="Consignor :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_Consignor" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Consignee" runat="server" CssClass="LABEL" Text="Consignee :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_Consignee" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
  
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BookingType" runat="server" CssClass="LABEL" Text="Booking Type :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_BookingType" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_PaymentType" runat="server" CssClass="LABEL" Text="Payment Type :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_PaymentType" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_GCAmount" runat="server" CssClass="LABEL" Text="GC Amount :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_GCAmount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="60%"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 50%" colspan="3"></td>
      
    </tr>
    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Text="Service Tax :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_ServiceTax" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="60%" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ServiceTaxBy" runat="server" CssClass="LABEL" Text="Service Tax Paid By :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txt_serviceTaxBy" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" ></asp:TextBox>
                <asp:CheckBox runat="server" ID="chk_Is_Mr_DlyFirstTime" style="display: none"/>
                <asp:CheckBox runat="server" ID="chk_Is_CrMOctroi_FirstTime" style="display: none"/>
                </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td colspan="3">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:Label runat="server" ID="lbl_error" CssClass="LABEL" ForeColor="red"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td colspan="3">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdn_GC_ID" runat="server"/>
                <asp:HiddenField ID="hdn_Total_Receivables" runat="server"/>
                <asp:HiddenField ID="hdn_MR_Type_ID" runat="server" />
                <asp:HiddenField ID="hdn_GC_SubTotal" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_GC_Total" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_Payment_Type_ID" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_Booking_Type_ID" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_DocumentID" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_GCCaption" runat="server"/>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" value="0"/>
        <asp:HiddenField ID="hdn_Start_No" runat="server" value="0"/>
        <asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_Next_No" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_Padded_Next_No" runat="server" Value="0" />
    </td>
    </tr>  
</table>
</asp:Panel>