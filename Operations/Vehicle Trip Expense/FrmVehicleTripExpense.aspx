<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleTripExpense.aspx.cs"
    Inherits="Operations_VehicleTripExpense_FrmVehicleTripExpense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript">
function windowClose()
{
  window.close(); 
}

function Calculate_HeadAmount(txt)
{      
    var grid = document.getElementById("dg_GridTripExpense");
    var sum_totalamt=0;

    var lblTotalTripExpense = document.getElementById('lblTotalTripExpense');
    var hdnTotalTripExpense = document.getElementById('hdnTotalTripExpense');

    for(var i=1; i < grid.rows.length; i++)
    {
        txt_amount = grid.rows[i].cells[1].getElementsByTagName('input');

        if(txt_amount[0].type =='text')
        {
            sum_totalamt = sum_totalamt + val(txt_amount[0].value);
        }
    }
    
    lblTotalTripExpense.innerHTML  = sum_totalamt;
    hdnTotalTripExpense.value = sum_totalamt;
}    


function LastTripClick()
{
    var chk_LastTrip = document.getElementById('<%=chk_LastTrip.ClientID %>');
    var chk_VehicleChange = document.getElementById('<%=chk_VehicleChange.ClientID %>');

    if (chk_VehicleChange.checked == true)
    {
         chk_VehicleChange.checked = false;
    }
}

function VehicleChangeClick()
{
    var chk_LastTrip = document.getElementById('<%=chk_LastTrip.ClientID %>');
    var chk_VehicleChange = document.getElementById('<%=chk_VehicleChange.ClientID %>');

    if (chk_VehicleChange.checked == true)
    {
        if (chk_LastTrip.checked == true)
        {
             chk_LastTrip.checked = false;
        }
    }
}

function IsEmtyTripClick()
{
    var chk_EmptyTrip = document.getElementById('<%=chk_EmptyTrip.ClientID %>');
    var txt_CurrentRoute = document.getElementById('<%=txt_CurrentRoute.ClientID %>');
    
    
    if (chk_EmptyTrip.checked == false)
    {
        txt_CurrentRoute.readOnly = true;
    }
    else
    {
        txt_CurrentRoute.readOnly = false;
    }
}


function Allow_To_Save()
{
    var ATS = false;
        ATS = true;
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Trip Expense</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="8">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Vehicle Trip Expense"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 10%">
                        Date:</td>
                    <td style="width: 20%">
                        <uc1:WucDatePicker ID="dtpTripExpenseDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                        Trip No. :</td>
                    <td style="width: 45%">
                        <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                &nbsp<asp:Label ID="lbl_TripNo" runat="server" Text="000" Font-Bold="true" ForeColor="darkBlue"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Vehicle No :</td>
                    <td style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpTripExpenseDate" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                            </Triggers>
                            <ContentTemplate>
                                <uc2:WucVehicleSearch ID="DDLVehicleSearch" runat="server" />
                                <asp:HiddenField ID="hdnTripSettledUpTo" runat="server" Value=""></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                        *</td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%;" colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lbl_PreviousTripWeight" runat="server" Text="Previous Trip Weight :"></asp:Label>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txt_PreviousTripWeight" runat="server" MaxLength="5" onblur="txtbox_onlostfocus(this);"
                                        onkeypress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                        Text="" Width="60%" CssClass="TEXBOX"></asp:TextBox>
                                    Kg</td>
                                <td style="width: 20%" class="TD1">
                                    <asp:Label ID="lbl_WeighingDate" runat="server" Text="Weighing Date :"></asp:Label></td>
                                <td style="width: 40%">
                                    <uc1:WucDatePicker ID="dtpPreviousTripWeightDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        From Date :</td>
                    <td style="width: 20%">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td onmouseup="Button_OnMouseUp(<%= CalendarFromDate.ClientObjectId %>)" style="height: 24px">
                                            <ComponentArt:Calendar ID="dtpFromDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                                ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                                PickerFormat="Custom" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="dtpFromDate_SelectionChanged">
                                            </ComponentArt:Calendar>
                                        </td>
                                        <td style="height: 24px" runat="server" id="TDFromDate">
                                            <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= CalendarFromDate.ClientObjectId %>)"
                                                onmouseup="Button_OnMouseUp(<%= CalendarFromDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                                width="25" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="CalendarFromDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ComponentArt:Calendar runat="server" ID="CalendarFromDate" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalendarFromDate.ClientObjectId %>_loaded && window.<%= dtpFromDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalendarFromDate.ClientObjectId %>.AssociatedPicker = <%= dtpFromDate.ClientObjectId %>;
                            window.<%= dtpFromDate.ClientObjectId %>.AssociatedCalendar = <%= CalendarFromDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 10%;">
                        To Date :</td>
                    <td style="width: 45%;">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td onmouseup="Button_OnMouseUp(<%= CalendarToDate.ClientObjectId %>)" style="height: 24px">
                                            <ComponentArt:Calendar ID="dtpToDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                                ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                                PickerFormat="Custom" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="dtpToDate_SelectionChanged">
                                            </ComponentArt:Calendar>
                                        </td>
                                        <td style="height: 24px" runat="server" id="TDToDate">
                                            <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= CalendarToDate.ClientObjectId %>)"
                                                onmouseup="Button_OnMouseUp(<%= CalendarToDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                                width="25" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="CalendarToDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ComponentArt:Calendar runat="server" ID="CalendarToDate" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalendarToDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalendarToDate.ClientObjectId %>_loaded && window.<%= dtpToDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalendarToDate.ClientObjectId %>.AssociatedPicker = <%= dtpToDate.ClientObjectId %>;
                            window.<%= dtpToDate.ClientObjectId %>.AssociatedCalendar = <%= CalendarToDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalendarToDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalendarToDate.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Driver :</td>
                    <td style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                <cc1:DDLSearch ID="DDLDriver" runat="server" AllowNewText="False" IsCallBack="True"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" CallBackAfter="2"
                                    PostBack="False" InjectJSFunction="" Text="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                        *</td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 10%;">
                        Previous Route :</td>
                    <td style="width: 45%;">
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                            </Triggers>
                            <ContentTemplate>
                                &nbsp<asp:Label ID="lbl_PreviousRoute" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%; height: 35px;">
                    </td>
                    <td style="width: 10%; height: 35px;">
                        Cleaner :</td>
                    <td style="width: 20%; height: 35px;">
                        <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="DDLCleaner" />
                            </Triggers>
                            <ContentTemplate>
                                <cc1:DDLSearch ID="DDLCleaner" runat="server" AllowNewText="False" IsCallBack="True"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchCleanerName" CallBackAfter="2"
                                    PostBack="True" InjectJSFunction="" Text="" OnTxtChange="DDLCleaner_OnSelectedIndexChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 11%; height: 35px;" colspan="2">
                        Empty Return ?
                        <asp:CheckBox ID="chk_EmptyReturn" runat="server" CssClass="CHECKBOX" /></td>
                    <td style="width: 10%; height: 35px;">
                        Return Route :</td>
                    <td style="width: 45%; height: 35px;">
                        <asp:TextBox ID="txt_ReturnRoute" runat="server" Text="" MaxLength="100" Width="80%"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 35px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 11%; height: 35px;" colspan="2">
                        Empty Trip ?
                        <asp:CheckBox ID="chk_EmptyTrip" runat="server" CssClass="CHECKBOX" onclick="IsEmtyTripClick();" /></td>
                    <td style="width: 10%">
                        Current Route :</td>
                    <td style="width: 45%">
                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                                <asp:AsyncPostBackTrigger ControlID="chk_EmptyTrip" />
                            </Triggers>
                            <ContentTemplate>
                                &nbsp<asp:TextBox ID="txt_CurrentRoute" runat="server" MaxLength="100" onblur="txtbox_onlostfocus(this);"
                                    onfocus="txtbox_onfocus(this)" Text="" Width="80%"></asp:TextBox>
                                <asp:HiddenField ID="hdn_CurrentDailyVehicleLoadingPlanID" runat="server" Value="0">
                                </asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 5px">
                        &nbsp;</td>
                </tr>
            </table>
            <table class="TABLE" width="100%" border="0">
                <tr style="height: 320px" valign="top">
                    <td style="width: 40%">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel12">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_TripExpense" runat="server" Height="350px" ScrollBars="None">
                                    <asp:DataGrid ID="dg_GridTripExpense" runat="server" ShowFooter="False" AllowPaging="False"
                                        CssClass="GRID" AllowSorting="False" AllowCustomPaging="False" AutoGenerateColumns="False"
                                        PageSize="15" OnItemDataBound="dg_GridTripExpense_ItemDataBound">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Orange" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="SrNo" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="TripExpenseHeadID" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTripExpenseHeadID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TripExpenseHeadID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expense Head" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtExpenseHead" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                        onfocus="txtbox_onfocus(this)" Width="400px" Text='<%# DataBinder.Eval(Container.DataItem, "TripExpenseHead")%>'></asp:TextBox>
                                                    <asp:Label ID="lblExpenseHead" runat="server" CssClass="TEXTBOX" Width="400px" Text='<%# DataBinder.Eval(Container.DataItem, "TripExpenseHead")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Integers(this,event)"
                                                        onblur="txtbox_onlostfocus(this);Calculate_HeadAmount(this);" onfocus="txtbox_onfocus(this)"
                                                        Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                        onfocus="txtbox_onfocus(this)" Width="300px" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                                <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                                <asp:AsyncPostBackTrigger ControlID="DDLCleaner" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 2%">
                        &nbsp;</td>
                    <td style="width: 48%">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm" Visible="false">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_ToPayVasuli" runat="server" Height="120px" ScrollBars="None" Font-Bold="true">
                                    <asp:Label ID="lblToPayVasuli" runat="server" Text="ToPay Vasuli" Font-Bold="true"
                                        BackColor="Orange" Width="100%"></asp:Label>
                                    <br />
                                    <asp:DataGrid ID="dg_GridToPayVasuli" runat="server" CellPadding="3" CssClass="Grid"
                                        AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_GridToPayVasuli_CancelCommand"
                                        OnDeleteCommand="dg_GridToPayVasuli_DeleteCommand" OnEditCommand="dg_GridToPayVasuli_EditCommand"
                                        OnItemCommand="dg_GridToPayVasuli_ItemCommand" OnItemDataBound="dg_GridToPayVasuli_ItemDataBound"
                                        OnUpdateCommand="dg_GridToPayVasuli_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="LR No.">
                                                <HeaderStyle Width="30%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="30%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="30%" HorizontalAlign="Left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_GCNo" Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="10"
                                                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "GC_No"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_GCNo" Width="95%" runat="server" CssClass="TEXTBOX" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "GC_No")) %>'
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"
                                                        onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount">
                                                <HeaderStyle Width="30%" HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Width="30%" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Width="30%" HorizontalAlign="Right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                                        onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Branch">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_VasuliBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_VasuliBranch_SelectedIndexChanged" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Branch_Name")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="45%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="45%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_VasuliBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_GridToPayVasuli" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_GridToPayVasuli" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                                <asp:Panel ID="pnl_POD" runat="server" Height="120px" ScrollBars="None" Font-Bold="true">
                                    <asp:Label ID="lbl_PODDetails" runat="server" Text="POD Details" Font-Bold="true"
                                        BackColor="Orange" Width="100%"></asp:Label>
                                    <br />
                                    <asp:DataGrid ID="dg_GridPODDetails" runat="server" CellPadding="3" CssClass="Grid"
                                        AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_GridPODDetails_CancelCommand"
                                        OnDeleteCommand="dg_GridPODDetails_DeleteCommand" OnEditCommand="dg_GridPODDetails_EditCommand"
                                        OnItemCommand="dg_GridPODDetails_ItemCommand" OnItemDataBound="dg_GridPODDetails_ItemDataBound"
                                        OnUpdateCommand="dg_GridPODDetails_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="LR No.">
                                                <HeaderStyle Width="30%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="30%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="30%" HorizontalAlign="Left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_GCNoPOD" Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="10"
                                                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "GC_No"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_GCNoPOD" Width="95%" runat="server" CssClass="TEXTBOX" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "GC_No")) %>'
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"
                                                        onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Branch">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_PODBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_PODBranch_SelectedIndexChanged" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Branch_Name")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="45%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="45%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_PODBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_GridPODDetails" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_GridPODDetails" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_GridToPayVasuli" />
                                <asp:AsyncPostBackTrigger ControlID="dg_GridPODDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:CheckBox ID="chk_LastTrip" runat="server" CssClass="CHECKBOX" Text="Is Last Trip ?"
                            Font-Bold="true" ForeColor="Red" onclick="LastTripClick();" />
                        &nbsp &nbsp
                        <asp:CheckBox ID="chk_VehicleChange" runat="server" CssClass="CHECKBOX" Text="Is Vehicle Change ?"
                            Font-Bold="true" ForeColor="Indigo" onclick="VehicleChangeClick();" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                Total :
                                <asp:Label ID="lblTotalTripExpense" runat="server" Text="0" Font-Bold="true"></asp:Label>&nbsp&nbsp
                                <asp:HiddenField ID="hdnTotalTripExpense" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td colspan="2">
                        &nbsp;<asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                        <asp:TextBox ID="txt_Remarks" Height="40px" runat="server" CssClass="TEXTBOX" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TD1" style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                                    AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
                                <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                                    OnClientClick="windowClose()" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" />
                                <asp:AsyncPostBackTrigger ControlID="dg_GridToPayVasuli" />
                                <asp:AsyncPostBackTrigger ControlID="dg_GridPODDetails" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnKeyID" runat="server" />
            &nbsp; &nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
