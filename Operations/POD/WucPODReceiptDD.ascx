<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODReceiptDD.ascx.cs" Inherits="Operations_POD_WucPODReceiptDD" %>
<%@ Register Src="../../CommonControls/WucPODSentBy.ascx" TagName="WucPODSentBy"  TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/POD/PODReceiptDD.js"></script>

<asp:ScriptManager ID="scm_PODReceipt_DD" runat="server"></asp:ScriptManager>

<table class="TABLE" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="POD Receipt (Direct Delivery)"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_PODReceivedDate" runat="server" Text="POD Received Date :" CssClass="LABEL"
                meta:resourcekey="lbl_PODReceivedDateResource1"></asp:Label></td>
        <td style="width: 29%">
             <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="WucDatePicker1" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                            PickerCssClass="PICKER" PickerCustomFormat="MMMM dd yyyy" PickerFormat="Custom"
                            SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="WucDatePicker1_SelectionChanged">
                        </ComponentArt:Calendar>
                    </td>
                    <td style="height: 24px" runat="server" id="TD_Calender">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td colspan="1">
        </td>
        <td style="width: 50%" colspan="3">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%" colspan="3"></td>
        <td class="TD1" style="width: 20%"></td>
        <td style="width: 29%">
            <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="False"
                AllowMultipleSelection="False" AllowWeekSelection="False" CalendarCssClass="CALENDER"
                CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                ControlType="Calendar" DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER"
                DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" MonthCssClass="MONTH"
                NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY"
                PopUp="Custom" PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY"
                SwapDuration="300">
            </ComponentArt:Calendar>
                <script type="text/javascript">
                    // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                        if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= WucDatePicker1.ClientObjectId %>_loaded)
                        {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= WucDatePicker1.ClientObjectId %>;
                            window.<%= WucDatePicker1.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                        }
                        else
                        {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                        }
                    }
                    ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                </script>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GCNo" runat="server" Text="GC No :" CssClass="LABEL" meta:resourcekey="lbl_GCNoResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="up_ddl_GCNo" runat="server">
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_GCNo" runat="server" AllowNewText="False" PostBack="True"
                        IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetGCNo"
                        CallBackAfter="1" OnTxtChange="ddl_GCNo_TxtChange"/>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucDatePicker1" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Label ID="lbl_GCNoDisplay" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td colspan="1" class="TDMANDATORY">
            *</td>
        <td style="width: 50%" colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_GCDetail" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_BookingDate" runat="server" Text="Booking Date :" CssClass="LABEL"
                                    meta:resourcekey="lbl_BookingDateResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_BookingDateDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_BookingDateDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_BookingBranch" runat="server" Text="Booking Branch :" CssClass="LABEL"
                                    meta:resourcekey="lbl_BookingBranchResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_BookingBranchDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_BookingBranchDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_BookingType" runat="server" CssClass="LABEL" Text="Booking Type :"
                                    meta:resourcekey="lbl_BookingTypeResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_BookingTypeDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_BookingTypeDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_PaymentType" runat="server" CssClass="LABEL" Text="Payment Type :"
                                    meta:resourcekey="lbl_PaymentTypeResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_PaymentTypeDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_PaymentTypeDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_DeliveredDate" runat="server" Text="Delivered Date :" CssClass="LABEL"
                                    meta:resourcekey="lbl_DeliveredDateResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_DeliveredDateDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_DeliveredDateDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_DeliveryBranch" runat="server" Text="Delivery Branch :" CssClass="LABEL"
                                    meta:resourcekey="lbl_DeliveryBranchResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_DeliveryBranchDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_DeliveryBranchDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_DeliveryRemarks" runat="server" CssClass="LABEL" Text="Delivery Remarks :"
                                    meta:resourcekey="lbl_DeliveryRemarksResource1"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:Label ID="lbl_DeliveryRemarksDisplay" runat="server" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_DeliveryRemarksDisplayResource1"></asp:Label></td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 50%" class="TD1" colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_GCNo" />
                    <asp:AsyncPostBackTrigger ControlID="WucDatePicker1" />
                    <asp:AsyncPostBackTrigger ControlID="txt_Remark" />
                    <asp:AsyncPostBackTrigger ControlID="WucPODSentBy1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="up_PODSent" runat="server">
                <ContentTemplate>
                    <uc2:WucPODSentBy ID="WucPODSentBy1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" Text="Remarks :" CssClass="LABEL" meta:resourcekey="lbl_RemarksResource1"></asp:Label></td>
        <td colspan="4" style="width: 79%">
            <asp:UpdatePanel ID="up_Remarks" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_Remark" runat="server" Height="40px" MaxLength="250" TextMode="MultiLine"
                        CssClass="TEXTBOX" meta:resourcekey="txt_RemarkResource1"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" OnClick="btn_Save_Click" Text="Save & New"/>
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" OnClick="btn_Save_Exit_Click" Text="Save & Exit" />
            <asp:Button ID="btn_Close" runat="server" Text="Exit" CssClass="BUTTON" OnClick="btn_Close_Click" />
              
        </td>
    </tr>
    <tr>
        <td align="left" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" align="left">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
</table>
