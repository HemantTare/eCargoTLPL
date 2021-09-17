<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTripMemoFinance.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmTripMemoFinance" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>    
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID"
  TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/DatePicker.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/TripMemoFinance.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trip Memo Finance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="8">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Trip Memo Finance"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%;">
                    </td>
                    <td class="TD1" style="width: 26%;" align="left">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 12%; text-align: left">
                    </td>
                    <td style="width: 20%;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="tr_NoDate" runat="server">
                    <td class="TD1" style="width: 15%">
                        Trip Memo No:</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <asp:Label ID="lbl_TripMemoNo" runat="server" Font-Bold="True"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 12%; text-align: right">
                        Trip Memo Date:</td>
                    <td style="width: 20%">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= CalendarT.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtp_TripMemo_Date" runat="server" AutoPostBackOnSelectionChanged="True"
                                        CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                                        PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom">
                                    </ComponentArt:Calendar>
                                </td>
                                <td id="TD_Calender" runat="server" style="height: 24px">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= CalendarT.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= CalendarT.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <ComponentArt:Calendar ID="CalendarT" runat="server" AllowMonthSelection="False"
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
                        function ComponentArt_<%= CalendarT.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalendarT.ClientObjectId %>_loaded && window.<%= dtp_TripMemo_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= CalendarT.ClientObjectId %>.AssociatedPicker = <%= dtp_TripMemo_Date.ClientObjectId %>;
                            window.<%= dtp_TripMemo_Date.ClientObjectId %>.AssociatedCalendar = <%= CalendarT.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalendarT.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalendarT.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp;<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdn_TripMemo_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtp_TripMemo_Date" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_VehicleDriver" runat="server">
                    <td class="TD1" style="width: 15%; vertical-align: top">
                        Vehicle No:</td>
                    <td class="TD1" style="width: 26%; vertical-align: top; text-align: left;" align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleCategoryIds" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 12%; text-align: right; vertical-align: top">
                        Driver Name:</td>
                    <td style="width: 20%; vertical-align: top">
                        <cc1:DDLSearch ID="DDLDriver" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" CallBackAfter="2"
                            PostBack="False" InjectJSFunction="" Text="" />
                        <asp:HiddenField ID="hdnDriverpath" runat="server" />
                        <%--<asp:LinkButton ID="lnkAddDriver" Font-Bold="true" OnClientClick="return Add_Driver_Window()"
                            runat="server" Text="Add New"></asp:LinkButton>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="tr_StartEndBranch" runat="server">
                    <td style="width: 15%" class="TD1">
                        Start Branch:</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <cc1:DDLSearch ID="DDLFromBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            OnTxtChange="DDLFromBranch_SelectedIndexChanged" PostBack="True" InjectJSFunction=""
                            Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 12%" class="TD1">
                        End Branch:</td>
                    <td style="width: 20%">
                        <cc1:DDLSearch ID="DDLToBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="False" InjectJSFunction="" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id ="tr_DateRange" runat="server">
                    <td class="TD1" style="width: 15%">
                        From Date :</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtpFromDate" runat="server" AutoPostBackOnSelectionChanged="True"
                                        CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                                        OnSelectionChanged="dtpFromDate_SelectionChanged" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom">
                                    </ComponentArt:Calendar>
                                </td>
                                <td id="Td2" runat="server" style="height: 24px">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="False" AllowMultipleSelection="False"
                            AllowWeekSelection="False" CalendarCssClass="CALENDER" CalendarTitleCssClass="TITLE"
                            ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                            DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER" DayNameFormat="FirstTwoLetters"
                            ImagesBaseUrl="../../images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                            NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                            PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300">
                        </ComponentArt:Calendar>

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtpFromDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtpFromDate.ClientObjectId %>;
                            window.<%= dtpFromDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 12%">
                        To Date:</td>
                    <td style="width: 20%">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtpToDate" runat="server" AutoPostBackOnSelectionChanged="True"
                                        CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                                        OnSelectionChanged="dtpToDate_SelectionChanged" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom">
                                    </ComponentArt:Calendar>
                                </td>
                                <td id="Td1" runat="server" style="height: 24px">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar2.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <ComponentArt:Calendar ID="Calendar2" runat="server" AllowMonthSelection="False"
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
                        function ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar2.ClientObjectId %>_loaded && window.<%= dtpToDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar2.ClientObjectId %>.AssociatedPicker = <%= dtpToDate.ClientObjectId %>;
                            window.<%= dtpToDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar2.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar2.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdn_To_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdn_From_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:Button ID="btn_Show" runat="server" Text="Show" CssClass="BUTTON" OnClick="btn_Show_Click" /></td>
                </tr>
                <tr>
                    <td class="TD1" colspan="7" style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblHeaderErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TDMANDATORY" colspan="7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_Memo" class="DIV" style="height: 230px">
                                    <asp:DataGrid ID="dg_Memo" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        CssClass="GRID" DataKeyField="Memo_Id" Style="border-top-style: none" Width="98%"
                                        Font-Bold="False" ToolTip="Select From Date and To Date to Change the Data">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Attach">
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="Check_All(this,'dg_Memo');" type="checkbox" disabled="disabled" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Attach" runat="server" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                        OnClick="Check_Single(this,'dg_Memo','1');" Enabled ="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Memo_ID" HeaderText="Memo_ID" HeaderStyle-CssClass="HIDEGRIDCOL"
                                                ItemStyle-CssClass="HIDEGRIDCOL" FooterStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Lhpo_No_For_Print" HeaderText="Trip Memo No."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Invoice No."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Memo_Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FromBranch" HeaderText="From Branch"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ToBranch" HeaderText="To Branch"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Total LR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_Total_GC" Text='<%#DataBinder.Eval(Container.DataItem, "Total_GC") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                                        ReadOnly="true" BackColor="Transparent" BorderStyle="none" BorderColor="transparent"
                                                        Style="text-align: center" Font-Size="11px" Font-Names="Verdana" Width="80%" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total Pkgs">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_totalpackage" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Articles") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                                        ReadOnly="true" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                                        Style="text-align: center" Font-Size="11px" Font-Names="Verdana" Width="80%" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total Freight">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_totalFreight" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                                        ReadOnly="true" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                                        Style="text-align: center" Font-Size="11px" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Freight") %>'
                                                        Width="80%">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass ="HIDEGRIDCOL" />
                                                <ItemStyle CssClass = "HIDEGRIDCOL" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Show" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr runat="server">
                    <td style="width: 15%;">
                    </td>
                    <td style="width: 26%; text-align: left;" valign="top" align="left">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%; text-align: right;">
                                            <asp:Label ID="Label2" runat="server" Text="Total Memo :" CssClass="LABEL" Font-Bold="True" /></td>
                                        <td style="width: 50%; text-align: left">
                                            <asp:Label ID="lbl_Total_Memo" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /></td>
                                        <td style="width: 15%; text-align: left;">
                                        </td>
                                        <td class="TD1" style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                            <asp:HiddenField ID="hdn_Total_Memo" runat="server" />
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Memo" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td colspan="4" style="text-align: left;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%; text-align: right;">
                                            <asp:Label ID="Label1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True" /></td>
                                        <td style="width: 50%; text-align: left">
                                            <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /></td>
                                        <td style="width: 15%; text-align: left;">
                                            <asp:Label ID="lbl_totalpackage" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                            <asp:HiddenField ID="hdn_totalpackage" runat="server" />
                                            </td>
                                        <td class="TD1" style="width: 15%; text-align: left;">
                                        </td>
                                        <td style="width: 15%">
                                            <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                                            <asp:HiddenField ID="hdn_totalFreight" runat="server" />
                                            </td>
                                        <td style="width: 5%; text-align: left;">
                                            <asp:Label ID="lbl_totalFreight" runat="server" Text="0" CssClass="HIDEGRIDCOL" Font-Bold="True" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Memo" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="trBrokerName" runat="server">
                    <td class="TD1" style="width: 15%; vertical-align: top">
                        Broker Name:</td>
                    <td class="TD1" style="width: 26%; vertical-align: top; text-align: left;" align="left">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <cc1:DDLSearch ID="DDLBroker" runat="server" AllowNewText="False" IsCallBack="True"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBrokerForDVLP" CallBackAfter="2"
                                    PostBack="False" InjectJSFunction="" Text="" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--<asp:LinkButton ID="lnkAddBroker" Font-Bold="true" OnClientClick="return Add_Broker_Window()"
                            runat="server" Text="Add New"></asp:LinkButton>--%>
                        <asp:HiddenField ID="hdnBrokerPath" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 12%; text-align: left">
                    </td>
                    <td style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trTDSCertificateTo" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        TDS Certificate To:</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <asp:DropDownList ID="ddlTDSCertificateTo" runat="server" CssClass="DROPDOWN" onchange="TDSCertificateToChange()">
                            <asp:ListItem Value="0">-- Select One --</asp:ListItem>
                            <asp:ListItem Value="1">Owner</asp:ListItem>
                            <asp:ListItem Value="2">Broker</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trIsRCRecieved" runat="server"  class="HIDEGRIDCOL" style="display :none">
                    <td style="width: 15%" class="TD1">
                        Is RC Recieved</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <asp:CheckBox ID="chkIsRCRecieved" onclick="CalculateTDSPercent()" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr id="trIsPanCardRecieved" runat="server" class="HIDEGRIDCOL" style="display :none">
                    <td style="width: 15%" class="TD1">
                        Is Pan Card Recieved</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <asp:CheckBox ID="chkIsPanCardRecieved" onclick="CalculateTDSPercent()" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%" class="TD1">
                        Hire Amount:</td>
                    <td class="TD1" style="width: 26%; text-align: left;" align="left">
                        <asp:TextBox ID="txtHireAmount" runat="server" CssClass="TEXTBOXNOS" onblur="CalculateBalance();"
                            onkeyPress="return Only_Integers(this,event);" Width="100px"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trTDSPercent" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblTDSPercent" runat="server" Text="TDS" CssClass="LABEL"></asp:Label>
                    <asp:HiddenField ID="hdnTDSPercent" runat="server" /></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblTDSAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTDSAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trSurChargePercent" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblSurChargePercent" runat="server" Text="Surcharge" CssClass="LABEL"></asp:Label>
                    <asp:HiddenField ID="hdnSurchargePercent" runat="server" /></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblSurChargeAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnSurChargeAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trAdditionalSurcharge" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblAdditionalSurchargeCessPercent" runat="server" Text="Additional Surcharge Cess"
                            CssClass="LABEL"></asp:Label>
                    <asp:HiddenField ID="hdnAdditionalSurchargeCessPercent" runat="server" /></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblAdditionalSurchargeCessAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnAdditionalSurchargeCessAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trAdditionalEducation" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblAddistionalEducationCessPercent" runat="server" Text="Additional Education Cess"
                            CssClass="LABEL"></asp:Label>
                    <asp:HiddenField ID="hdnAddistionalEducationCessPercent" runat="server" /></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblAddistionalEducationCessAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnAddistionalEducationCessAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trTotalTDSAmount" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblTotalTDS" runat="server" Text="Total TDS Amount:" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblTotalTDSAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTotalTDSAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="trTruckHirePayable" runat="server" class="HIDEGRIDCOL">
                    <td style="width: 15%" class="TD1">
                        <asp:Label ID="lblTHirePayable" runat="server" CssClass="LABEL" Text="Truck Hire Payable:"></asp:Label></td>
                    <td style="width: 26%; text-align: left;" class="TD1" align="left">
                        <asp:Label ID="lblTruckHirePayable" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTruckHirePayable" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1" style="width: 12%">
                    </td>
                    <td class="TDMANDATORY" style="width: 20%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%">
                        <asp:Label ID="lblAdvance" runat="server" CssClass="LABEL" Text="Advance:"></asp:Label></td>
                    <td align="left" class="TD1" style="width: 26%; text-align: left">
                        <asp:TextBox ID="txtAdvance" runat="server" CssClass="TEXTBOXNOS" onblur="CalculateBalance();"
                            onkeypress="return Only_Integers(this,event);" Width="100px"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" colspan="4">
                        <uc3:wuchierarchywithid id="WucHierarchyWithIDATH" runat="server"></uc3:wuchierarchywithid>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%">
                        <asp:Label ID="lblBal" runat="server" CssClass="LABEL" Text="Balance:"></asp:Label></td>
                    <td align="left" class="TD1" style="width: 26%; text-align: left">
                        <asp:Label ID="lblBalance" runat="server" CssClass="TEXTBOXNOS" Width="100px"
                         BorderColor="black" BorderWidth ="1px" ></asp:Label><asp:HiddenField
                            ID="hdnBalance" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" colspan="4">
                        <uc3:wuchierarchywithid id="WucHierarchyWithIDBTH" runat="server"></uc3:wuchierarchywithid>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%" class="TD1">
                        Remarks:</td>
                    <td class="TD1" style="width: 80%; text-align: left;" colspan="5" align="left">
                        <asp:TextBox ID="txtRemarks" CssClass="TEXTBOX" TextMode="MultiLine" Height="30px"
                            MaxLength="100" runat="server" />
                    </td>
                    <td colspan="1" style="width: 80%">
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUI()"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                                <asp:HiddenField ID="hdnlhpoID" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
