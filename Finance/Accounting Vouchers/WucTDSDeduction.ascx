<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucTDSDeduction.ascx.cs"
    Inherits="Accounting_Vouchers_wucTDSDeduction" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Assembly="KeySortDropDownList" Namespace="KeySortDropDownList.Thycotic.Web.UI.WebControls"
    TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>

<script language="javascript" src="../../Javascript/ddlsearch.js" type="text/javascript"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type = "text/javascript" >
 
 function Allow_To_Save()
 {
    return true;
 }
 
</script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="SM_TDSDeduction" runat="server">
</asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_TDSDeduction_Heading" runat="server" Text="TDS Deduction" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_Voucher" runat="server" GroupingText=" " Width="100%">
                <table width="100%">
                    <tr>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_Journal" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red"
                                Text="JOURNAL"></asp:Label>
                            No:</td>
                        <td style="width: 25%">
                            <asp:Label ID="lbl_Journal_No" runat="server" Font-Bold="True" CssClass="LABEL" Text="0"></asp:Label>
                        </td>
                        <td style="width: 5%">
                        </td>
                        <td class="TD1" style="width: 20%">
                            Select Date:</td>
                        <td style="width: 25%">
                            <%--<ComponentArt:Calendar ID="Picker_Voucher_Date" runat="server" CellPadding="2" ControlType="Picker"
                                PickerCssClass="picker" PickerCustomFormat="dd MMMM yyyy" PickerFormat="Custom"
                                SelectedDate="2005-12-13">
                            </ComponentArt:Calendar>--%>
                            <table>
                                <tr>
                                    <td onmouseup="Button_OnMouseUp(<%=Calendar.ClientObjectId %>)" style="height: 24px">
                                        <ComponentArt:Calendar ID="Picker" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                            ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="dd MMM yyyy"
                                            PickerFormat="Custom" SelectedDate="2005-12-13">
                                        </ComponentArt:Calendar>
                                    </td>
                                    <td>
                                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                            width="25" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%">
                            <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="false"
                                AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
                                PopUp="Custom" CalendarTitleCssClass="title" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                                DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
                                SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
                                MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="FirstTwoLetters"
                                ImagesBaseUrl="../../images/" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%">
                            Reference No:</td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txt_Reference_No" runat="server" BorderWidth="1px" MaxLength="20"
                                CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 5%">
                        </td>
                        <td class="TD1" style="width: 20%">
                        </td>
                        <td style="width: 25%">
                            <asp:UpdatePanel ID="updateRefresh" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_Refresh" runat="server" Text="Refresh" CssClass="BUTTON" ValidationGroup="Save"
                                        OnClick="btn_Refresh_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="Panel1" runat="server" GroupingText="Voucher Details" Width="100%">
                <table width="100%">
                    <tr>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_Ledger_Group" runat="server" CssClass="LABEL" Text="Ledger Group:"></asp:Label>
                        </td>
                        <td style="width: 50%" colspan="3">
                            <%--<asp:TextBox ID="TextBox2" runat="server" BorderWidth="1px" MaxLength="20" CssClass="TEXTBOX"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddl_Ledger_Group" runat="server" Width="80%" OnSelectedIndexChanged="ddl_Ledger_Group_SelectedIndexChanged"
                                CssClass="DROPDOWN" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <%--<td style="width: 5%">
                        </td>
                        <td class="TD1" style="width: 20%">
                        </td>--%>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%">
                            Ledger Account:</td>
                        <td style="width: 50%" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel51" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_Ledger_Account" runat="server" Width="80%" OnSelectedIndexChanged="ddl_Ledger_Account_SelectedIndexChanged"
                                        CssClass="DROPDOWN" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <%--<td style="width: 5%">
                        </td>
                        <td class="TD1" style="width: 20%">
                        </td>--%>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <%-- <tr>
                        <td colspan="6" class="TD1" style="width: 20%">
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td class="TD1" style="width: 20%">
                            TDS Ledger:</td>
                        <td style="width: 50%" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddl_TDS_Ledger" runat="server" Width="80%" OnSelectedIndexChanged="ddl_TDS_Ledger_SelectedIndexChanged"
                                        CssClass="DROPDOWN" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <%--<td style="width: 5%">
                        </td>
                        <td class="TD1" style="width: 20%">
                        </td>--%>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_TDS_Ledger_For" runat="server" GroupingText="TDS Ledger For" Width="100%">
                        <table width="100%">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_Total_Amount_Paid_Payable" runat="server" Font-Bold="True" CssClass="LABEL"
                                        Text="Total Amount Paid / Payable"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Total_Amount_Paid_Amount" runat="server" Font-Bold="True" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Tax @
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Tax_Percent" Style="text-align: left" runat="server" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                    %
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Tax_Amount" Style="text-align: left" runat="server" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    SurCharge @
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Surcharge_Percent" Style="text-align: left" runat="server" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                    %
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Surcharge_Amount" Style="text-align: left" runat="server" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Additional SurCharge (Cess) @
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Addtional_Surcharge_Percent" Style="text-align: left" runat="server"
                                        CssClass="LABEL" Text="0"></asp:Label>
                                    %
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Addtional_Surcharge_Amount" Style="text-align: left" runat="server"
                                        CssClass="LABEL" Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Addl Ed Cess @
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Addl_Ed_Cess_Percent" Style="text-align: left" runat="server"
                                        CssClass="LABEL" Text="0"></asp:Label>
                                    %
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Addl_Ed_Cess_Amount" Style="text-align: left" runat="server" CssClass="LABEL"
                                        Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Total TDS
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Total_TDS_Amount" Style="text-align: left" Font-Bold="True" runat="server"
                                        CssClass="LABEL" Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Less TDS Deducted Till Date
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Less_TDS_Deducted_Till_Date_Amount" Style="text-align: left" Font-Bold="True"
                                        runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    Net TDS To Deduct
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td class="TD1" style="width: 5%">
                                </td>
                                <td style="width: 10%" align="right">
                                    <asp:Label ID="lbl_Net_TDS_To_Deduct_Amount" Style="text-align: left" Font-Bold="True"
                                        runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel211" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_Pending_Bill_For" runat="server" GroupingText="Pending Bill For "
                        Width="100%">
                        <table width="100%">
                            <tr>
                                <td class="TD1" style="width: 10%">
                                    Type Of Ref
                                    <td style="width: 1%">
                                    </td>
                                </td>
                                <td style="width: 25%">
                                    Name
                                </td>
                                <td class="TD1" style="width: 10%">
                                    Credit Days
                                </td>
                                <td class="TD1" style="width: 10%">
                                    Gross Amount
                                </td>
                                <td class="TD1" style="width: 10%">
                                    Amount
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 10%">
                                    <asp:Label ID="lbl_Type_Of_Ref" Font-Bold="True" CssClass="LABEL" Text="TDS" runat="server"></asp:Label>
                                </td>
                                <td style="width: 1%">
                                </td>
                                <td style="width: 25%">
                                    <asp:UpdatePanel ID="UpdatePanel334" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_Name" runat="server" Width="80%" OnSelectedIndexChanged="ddl_Name_SelectedIndexChanged"
                                                CssClass="DROPDOWN" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Name" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TD1" style="width: 10%">
                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Credit_Days" Font-Bold="True" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Name" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TD1" style="width: 10%">
                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Gross_Amount" Font-Bold="True" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Name" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TD1" style="width: 10%">
                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_Amount" Font-Bold="True" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Name" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Name" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; vertical-align: top;">
            Narration:</td>
        <td colspan="4">
            <asp:TextBox ID="txt_Narration" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px" Width="98%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
        </td>
        <td style="width: 5%">
        </td>
    </tr>
    <tr>
        <td colspan="6" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%" align="center">
            <asp:UpdatePanel ID="updateSave" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" ValidationGroup="Save"
                        OnClick="btn_Save_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Group" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Account" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_TDS_Ledger" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="false" />&nbsp;
                </ContentTemplate>
               <%-- <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdn_Vendor_Id" runat="server" />

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
    
    
    function Allow_to_Save()
    {
    
    return true;
    }
    
</script>

