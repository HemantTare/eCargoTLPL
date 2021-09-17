<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDDCPaymentDetails.aspx.cs"
    Inherits="Operations_Delivery_FrmDDCPaymentDetails" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Operations/Delivery/DDlyPaymentDetails.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>


<script type="text/javascript">
  
function Validate_CouponNo()
{
    var txt_CouponNo = document.getElementById('txt_CouponNo');
    document.getElementById('<%=btn_ValidateCouponNo.ClientID%>').click();
}

  function updateparent()
    {
        window.opener.update_CouponDetails();
    }
       
</script>


<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM_Coupon" runat="server"></asp:ScriptManager>
        <table class="TABLE" id="TABLE1">
            <tr>
                <td class="TDGRADIENT" colspan="2" style="width: 100%">
                    &nbsp;
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Cheque Details"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" colspan="2" style="text-align: left">
                    &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                    <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>&nbsp;
                </td>
            </tr>
            <asp:Panel ID="pnl_Cheque" runat="server">
                <tr runat="server" id="tr_Del_Mod">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_ChequeNo" runat="server" Text="Cheque No:" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <asp:TextBox ID="txt_ChequeNo" runat="server" CssClass="TEXTBOX" MaxLength="6" Width="75px" />
                    </td>
                </tr>
                <tr runat="server" id="tr_Del_Des">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_ChequeDate" runat="server" Text="Cheque Date:" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtp_ChequeDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
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
                        <%-- <componentart:calendar id="Calendar" runat="server" allowmonthselection="False" allowmultipleselection="False"
            allowweekselection="False" calendarcssclass="CALENDER" calendartitlecssclass="TITLE"
            clientsideonselectionchanged="Calendar_OnSelectionChanged" controltype="Calendar"
            daycssclass="DAY" dayheadercssclass="DAYHEADER" dayhovercssclass="DAYHOVER" daynameformat="FirstTwoLetters"
            imagesbaseurl="../../images/" monthcssclass="MONTH" nextimageurl="cal_nextMonth.gif"
            nextprevcssclass="NEXTPREV" othermonthdaycssclass="OTHERMONTHDAY" popup="Custom"
            previmageurl="cal_prevMonth.gif" selecteddaycssclass="SELECTEDDAY" swapduration="300" ></componentart:calendar>
           --%>
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_ChequeDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_ChequeDate.ClientObjectId %>;
                            window.<%= dtp_ChequeDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                    </td>
                </tr>
                <tr runat="server" id="tr1">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_BankName" runat="server" Text="Bank Name:" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <asp:TextBox ID="txt_BankName" runat="server" CssClass="TEXTBOX" MaxLength="30" Width="229px" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnl_Credit" runat="server">
                <tr id="tr_billing_details" runat="server">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_BillingParty" runat="server" Text="Party Name:" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <asp:TextBox ID="txt_BillingParty" AutoCompleteType="Disabled" onblur="Billing_LostFocus(this,'lst_BillParty')"
                            onkeyup="NewGC_AllSearch(event,this,'lst_BillParty','BillingPartyDelivery',2);" onkeydown="return on_keydown(event,this,'lst_BillParty');"
                            onfocus="On_Focus('txt_BillingParty','lst_BillParty','BillingPartyDelivery');" runat="server"
                            CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox><br />
                        <asp:ListBox ID="lst_BillParty" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_BillingParty')"
                            runat="server" TabIndex="90"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hdn_BillingPartyId" />
                        <asp:HiddenField ID="hdn_BillingParty" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Billing_Party_CreditLimit" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Billing_Party_ClosingBalance" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_BillingParty_LedgerId" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Billing_Party_MinimumBalance" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Billing_Party_Ledger_Closing_Balance" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_IsServiceTaxApplForBillParty" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="tr3">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_BillingLocation" CssClass="LABEL" Text="Billing Branch :" runat="server"></asp:Label>
                    </td>
                    <td class="TD1" runat="server" id="td_Bill_Location" style="text-align: left">
                        <asp:TextBox ID="txt_BillingLocation" AutoCompleteType="Disabled" Width="90%" onblur="Billing_LostFocus(this,'lst_BillLocation')"
                            onkeyup="NewGC_AllSearch(event,this,'lst_BillLocation','BillingLocation',2);"
                            onkeydown="return on_keydown(event,this,'lst_BillLocation');" onfocus="On_Focus('txt_BillingLocation','lst_BillLocation','BillingLocation');"
                            runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False" ReadOnly="True"></asp:TextBox><br />
                        <asp:ListBox ID="lst_BillLocation" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_BillingLocation')"
                            runat="server" TabIndex="100"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hdn_BillingLocationId" />
                        <asp:HiddenField runat="server" ID="hdn_BillingLocation" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnl_Return" runat="server">
                <tr runat="server" id="tr4">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_ReasonForNonDelivery" runat="server" Text="Reason For Non Delivery:"
                            CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <asp:DropDownList ID="ddl_ReasonForNonDelivery" runat="server" CssClass="DROPDOWN">
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:Panel>

            <asp:Panel ID="pnl_Coupon" runat="server">
                <tr runat="server" id="tr2">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_CouponNo" runat="server" Text="Coupon No.:"
                            CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                    <asp:UpdatePanel ID="upd_CouponNo" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_CouponNo" runat="server" CssClass="TD1" onblur="Validate_CouponNo();" >
                            </asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txt_CouponNo"></asp:AsyncPostBackTrigger>             
                        </Triggers>
                    </asp:UpdatePanel>    
                        
                    </td>
                </tr>
                
                <tr runat="server" id="tr6">
                    <td class="TD1" style="width: 20.5%">
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                
                 <tr runat="server" id="tr5">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_GCAmountHead" runat="server" Text="LR Freight :"
                            CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_GCAmount" runat="server" Text="0.00"  Font-Bold="true" ForeColor="DarkGreen"
                            CssClass="LABEL"></asp:Label>
                    </td>
                </tr>
                
                <tr runat="server" id="tr7">
                    <td class="TD1" style="width: 20.5%">
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                
                <tr runat="server" id="tr8">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_CouponValueHead" runat="server" Text="Coupon Value :"
                            CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_CouponValue" runat="server" Text="0.00"  Font-Bold="true" ForeColor="Red"
                            CssClass="LABEL"></asp:Label>
                    </td>
                </tr>

                <tr runat="server" id="tr9">
                    <td class="TD1" style="width: 20.5%">
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                     
                <tr runat="server" id="tr10">
                    <td class="TD1" style="width: 20.5%">
                        <asp:Label ID="lbl_ToBePaidHead" runat="server" Text="Balance :"
                            CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_ToBePaid" runat="server" Text="0.00"  Font-Bold="true" 
                            CssClass="LABEL"></asp:Label>
                    </td>
                </tr>    
                
               <tr>
                    <td class="TD1" style="display:none">
                       <asp:Button ID="btn_ValidateCouponNo" runat="server" CssClass="BUTTON" Text=""
                        OnClick="btn_ValidateCouponNo_Click" />
                        
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
               
               </tr> 
               
            
                                    
            </asp:Panel>
            
            <tr style="display: none">
                <td class="TD1" style="width: 8%;">
                    <asp:Label ID="lbl_BillingHierarchy" CssClass="LABEL" Text="Hierarchy :" runat="server"></asp:Label>&nbsp;
                </td>
                <td style="width: 13%">
                    <asp:DropDownList ID="ddl_BillingHierarchy" runat="server" CssClass="DROPDOWN" onchange="On_BillingHierarchy_Change()">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdn_BillingHierarchy" runat="server"></asp:HiddenField>
                    &nbsp;
                    <asp:HiddenField runat="server" ID="hdn_BillingRemark" />
                    <asp:CheckBox ID="chk_Is_Multiple_Location_Billing_Allowed" runat="server" />
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" style="width: 20.5%">
                </td>
                <td style="width: 79%">
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="2" style="text-align: center;">
                    <asp:Button ID="btn_Save" Width="100px" Font-Size="11px" Font-Names="Verdana" BackColor="#F2F2F2"
                        BorderColor="Black" BorderStyle="solid" BorderWidth="1px" runat="server" Text="Save"
                        OnClick="btn_Save_Click" />&nbsp;
                    <asp:Button ID="btn_Exit" Width="100px" Font-Size="11px" Font-Names="Verdana" BackColor="#F2F2F2"
                        BorderColor="Black" BorderStyle="solid" BorderWidth="1px" runat="server" Text="Exit"
                        OnClick="btn_Exit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript" language="javascript">
        On_PageUnLoad();
    </script>

</body>
</html>
