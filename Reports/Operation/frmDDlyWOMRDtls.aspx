<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDDlyWOMRDtls.aspx.cs"
    Inherits="Reports_Operation_frmDDlyWOMRDtls" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript">
 
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
      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wucTransaction_Date.ClientObjectId %>_loaded)
      {
        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wucTransaction_Date.ClientObjectId %>;
        window.<%= wucTransaction_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
      }
      else
      {
        window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
      }
    }
    ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
    
    function ComponentArt_<%= Calendar1.ClientObjectId %>_Associate()
    {
      if (window.<%= Calendar1.ClientObjectId %>_loaded && window.<%= wucChequeDate.ClientObjectId %>_loaded)
      {
        window.<%= Calendar1.ClientObjectId %>.AssociatedPicker = <%= wucChequeDate.ClientObjectId %>;
        window.<%= wucChequeDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar1.ClientObjectId %>;
      }
      else
      {
        window.setTimeout('ComponentArt_<%= Calendar1.ClientObjectId %>_Associate()', 100);
      }
    }
    ComponentArt_<%= Calendar1.ClientObjectId %>_Associate();
  
    function Validate()
    {
        if (<%=wucTransaction_Date.ClientID %>.GetSelectedDate() > <%=wucChequeDate.ClientID %>.GetSelectedDate() )
        {
            alert("End Date Should Be Greater Than Start Date");
            return false;
        }
        else
            return true;
    }  

</script>

<script type="text/javascript">
   
function HideReceivedByControl()
{  
 var TR_DebitTo=document.getElementById('TR_DebitTo');
 var TR_Cheque=document.getElementById('TR_Cheque');    
 var rbl_CashBank=document.getElementById('Rbl_Receivedby_0');
 var rbl_ChequeBank=document.getElementById('Rbl_Receivedby_1');
    
    TR_DebitTo.style.display='none';
    if (rbl_CashBank.checked == true)
    {
      TR_Cheque.style.display='none';
      TR_DebitTo.style.display='none';
    }
    else if (rbl_ChequeBank.checked == true)
    {
      TR_Cheque.style.display='inline';
      TR_DebitTo.style.display='none';
    }
    else 
    {
      TR_Cheque.style.display='none';
      TR_DebitTo.style.display='inline';
    }
}

function Receivable()
    {
       	var txtlblTotalGCAmountValue = document.getElementById('txtlblTotalGCAmountValue');
        var txt_TDSDeduction = document.getElementById('txt_TDSDeduction');
        var lbl_TotalReceivable = document.getElementById('lbl_TotalReceivable');
        var hdn_TotalReceivable = document.getElementById('hdn_TotalReceivable');

        
        hdn_TotalReceivable.value = parseFloat(txtlblTotalGCAmountValue.innerHTML) - parseFloat(txt_TDSDeduction.value);

        lbl_TotalReceivable.value = hdn_TotalReceivable.value;
        lbl_TotalReceivable.innerHTML = hdn_TotalReceivable.value;
    }



</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Direct Delivery Without MR (Enter Details) </title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="SM_DirectDelivery" runat="server">
            </asp:ScriptManager>
            <table class="TABLE" width="100%">
                <tr>
                    <td colspan="4" class="TDGRADIENT">
                        <asp:Label ID="lbl_DirectDelivery_Heading" runat="server" Text="Direct Delivery"
                            CssClass="HEADINGLABEL" meta:resourcekey="lbl_DirectDelivery_HeadingResource1"></asp:Label></td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblLRNo" CssClass="TDTEXT" runat="server" Text="LR No : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblLRNo" CssClass="LABEL" runat="server" Text="0"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblLRDate" runat="server" CssClass="TDTEXT" Text="LR Date : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblLRDate" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblTransaction_Date" CssClass="TDTEXT" runat="server" Text="Transaction Date : "></asp:Label></td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="vertical-align: top">
                                    <ComponentArt:Calendar ID="wucTransaction_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                        PickerFormat="Custom" SelectedDate="2006-12-26">
                                    </ComponentArt:Calendar>
                                </td>
                                <td style="vertical-align: top">
                                    <img alt="" class="CALENDAR_BUTTON" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="<%=Calendar_Img_Path %>"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <%--<uc1:WucDatePicker ID="wucTransaction_Date" runat="server" />--%>
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
                    <td colspan="1" style="width: 20%; text-align: right">

                        <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wucTransaction_Date.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wucTransaction_Date.ClientObjectId %>;
                        window.<%= wucTransaction_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                      }
                      else
                      {
                        window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                      }
                    }
                     ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblBkgBranch" CssClass="TDTEXT" runat="server" Text="Bkg Branch : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblBkgBranch" CssClass="LABEL" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblDlyLocation" CssClass="TDTEXT" runat="server" Text="Dly Location : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblDlyLocation" CssClass="LABEL" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lblInvoiceNo" CssClass="TDTEXT" runat="server" Text="Invoice No : "></asp:Label></td>
                    <td class="TD1" align="left" colspan="1" style="width: 30%; text-align: left;">
                        <asp:Label ID="txtlblInvoiceNo" CssClass="LABEL" runat="server" Text="0"></asp:Label></td>
                    <td colspan="1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lblInvoiceDate" CssClass="TDTEXT" runat="server" Text="Invoice Date : "></asp:Label></td>
                    <td class="TD1" align="left" colspan="1" style="width: 30%; text-align: left;">
                        <asp:Label ID="txtlblInvoiceDate" CssClass="LABEL" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblInvoiceFrom" CssClass="TDTEXT" runat="server" Text="Invoice From : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblInvoiceFrom" CssClass="LABEL" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblInvoiceTo" CssClass="TDTEXT" runat="server" Text="Invoice To : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblInvoiceTo" CssClass="LABEL" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lblVehicleNo" CssClass="TDTEXT" runat="server" Text="Vehicle No : "></asp:Label></td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblVehicleNo" CssClass="LABEL" runat="server" Text=""></asp:Label></td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lbl_TotalGCAmountValue" CssClass="TDTEXT" runat="server" Text="Total Freight : "></asp:Label>
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="txtlblTotalGCAmountValue" CssClass="LABEL" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lbl_TDSDeduction" runat="server" CssClass="TDTEXT" Text="TDS : "></asp:Label></td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:TextBox ID="txt_TDSDeduction" runat="server" CssClass="TEXTBOX" Text="0" 
                        onkeypress="return Only_Numbers(this,event)" onblur="Receivable();"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="1" style="width: 20%; text-align: right">
                    </td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                    </td>
                    <td colspan="1" style="width: 20%; text-align: right">
                        <asp:Label ID="lbl_TotalReceivableH" runat="server" CssClass="TDTEXT" Text="Total Receivale :"></asp:Label></td>
                    <td align="left" class="TD1" colspan="1" style="width: 30%; text-align: left">
                        <asp:Label ID="lbl_TotalReceivable" runat="server" CssClass="TDTEXT" Text="0"></asp:Label></td>
                        
                </tr>
                <tr>
                    <td colspan="4" style="height: 15px">
                        <asp:Panel ID="pnl_Payment" CssClass="PANEL" runat="server" GroupingText="Freight Received"
                            Width="98%" HorizontalAlign="Center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table border="0" width="100%">
                                        <tr runat="server">
                                            <td class="TD1" style="width: 20%">
                                            </td>
                                            <td colspan="5" style="text-align: left;">
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr_ReceivedBy">
                                            <td style="width: 20%;" class="TD1">
                                                <asp:Label ID="lbl_ReceivedBy" runat="server" CssClass="LABEL" Text="Received By:"></asp:Label>
                                            </td>
                                            <td style="text-align: left;" colspan="5">
                                                <asp:RadioButtonList ID="Rbl_Receivedby" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="false" onclick="HideReceivedByControl();">
                                                    <asp:ListItem Value="1" Text="Cash" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Cheque"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Debit To"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" runat="server" id="TR_Cheque">
                                                <table border="0" width="100%">
                                                    <tr id="tr1" runat="server">
                                                        <td class="TD1" style="width: 20%">
                                                            <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No : "></asp:Label></td>
                                                        <td class="TD1" style="width: 30%; text-align: left">
                                                            <asp:TextBox ID="txtChequeNo" runat="server" onkeypress="return Only_Numbers(this,event);"></asp:TextBox></td>
                                                        <td class="TD1" style="width: 20%">
                                                            <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date : "></asp:Label></td>
                                                        <td class="TD1" style="width: 30%; text-align: left">
                                                            <%--<uc1:WucDatePicker ID="wucChequeDate" runat="server" />--%>
                                                            <table border="0" cellpadding="0">
                                                                <tr>
                                                                    <td onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" style="vertical-align: top">
                                                                        <ComponentArt:Calendar ID="wucChequeDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                                                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                                                            PickerFormat="Custom" SelectedDate="2006-12-26">
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                    <td style="vertical-align: top">
                                                                        <img id="IMG1" alt="" class="CALENDAR_BUTTON" height="22" onclick="Button_OnClick(this, <%= Calendar1.ClientObjectId %>)"
                                                                            onmouseup="Button_OnMouseUp(<%= Calendar1.ClientObjectId %>)" src="<%=Calendar_Img_Path %>"
                                                                            width="25" />
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                    </tr>
                                                    <tr id="Tr2" runat="server">
                                                        <td class="TD1" style="width: 20%">
                                                            <asp:Label ID="lblBankName" runat="server" Text="Bank Name : "></asp:Label></td>
                                                        <td class="TD1" colspan="2" style="text-align: left">
                                                            <asp:TextBox ID="txtBankName" runat="server" Width="98%"></asp:TextBox></td>
                                                        <td class="TD1" style="width: 20%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="TR_DebitTo">
                                            <td style="width: 20%;" class="TD1">
                                                <asp:Label ID="lbl_DebitTo" runat="server" CssClass="LABEL" Text="Debit To :"></asp:Label></td>
                                            <td style="width: 20%;">
                                                <cc1:DDLSearch ID="ddl_DebitTo" runat="server" AllowNewText="True" IsCallBack="True"
                                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerDirectDly" CallBackAfter="2"
                                                    Text="" PostBack="False" />
                                            </td>
                                            <td style="width: 1%;" class="TDMANDATORY">
                                                *</td>
                                            <td style="width: 20%;" class="TD1">
                                                <asp:Label ID="lbl_BillingBranch" runat="server" CssClass="LABEL" Text="Billing Branch :"></asp:Label></td>
                                            <td style="width: 20%;">
                                                <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
                                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetMemoToBranch" CallBackAfter="2"
                                                    Text="" PostBack="False" />
                                            </td>
                                            <td style="width: 1%;" class="TDMANDATORY">
                                                *</td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%-- <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
                <ContentTemplate>--%>
                        <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABEL" ForeColor="Red"
                            meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                        <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        &nbsp<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                            OnClick="btn_Close_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
                        <asp:HiddenField ID="hdn_Total_Articles" runat="server" />
                        <asp:HiddenField ID="hdn_Total_GC_Amount" runat="server" />
                        <asp:HiddenField ID="hdn_TotalReceivable" runat="server" />                        
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="hdn_gc_caption"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_lhpo_caption"></asp:HiddenField>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
    HideReceivedByControl();  
</script>

