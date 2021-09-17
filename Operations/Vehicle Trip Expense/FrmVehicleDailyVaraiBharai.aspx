<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleDailyVaraiBharai.aspx.cs"
    Inherits="Operations_VehicleTripExpense_FrmVehicleDailyVaraiBharai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" language="javascript">
function windowClose()
{
  window.close(); 
}

function Allow_To_Save()
{
    var ATS = false;
     var txt_BharaiRemark = document.getElementById("txt_BharaiRemark");
     var txt_VaraiRemark = document.getElementById("txt_VaraiRemark");
    var lbl_Errors = document.getElementById("lbl_Errors");
    if(txt_BharaiRemark.value == '')
    {
        lbl_Errors.innerHTML = 'Enter Bharai Remark';
        txt_BharaiRemark.focus();
    }
    else if(txt_VaraiRemark.value == '')
    {
        lbl_Errors.innerHTML = 'Enter Varai Remark';
        txt_VaraiRemark.focus();
    }
    else
        ATS = true;
    
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Varai - Bharai Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="8">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Daily Varai - Bharai Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 10%">
                        Date:</td>
                    <td style="width: 20%">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td onmouseup="Button_OnMouseUp(<%= CalendarFromDate.ClientObjectId %>)" style="height: 24px">
                                            <ComponentArt:Calendar ID="dtpFromDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                                ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                                PickerFormat="Custom" AutoPostBackOnSelectionChanged="True">
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
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 45%">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Vehicle No :</td>
                    <td style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <Triggers>
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
                    <td style="width: 10%;">
                        Driver :</td>
                    <td style="width: 45%">
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
                    <td class="TDMANDATORY" style="width: 1%">
                        *
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Previous Trip Wt. :&nbsp;</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txt_PreviousTripWeight" runat="server" CssClass="TEXBOX" MaxLength="5"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                            Text="" Width="50%"></asp:TextBox>
                        kg</td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 10%;">
                        Date :</td>
                    <td style="width: 45%;">
                        <uc1:WucDatePicker ID="dtpPreviousTripWeightDate" runat="server"></uc1:WucDatePicker>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Bharai Rs. :</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txt_Bharai" runat="server" CssClass="TEXBOX" MaxLength="5"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                            Text="" Width="50%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 10%;">
                        Bharai Remark :</td>
                    <td style="width: 45%;">
                        <asp:TextBox ID="txt_BharaiRemark" runat="server" CssClass="TEXTBOX" MaxLength="200"
                            TextMode="MultiLine"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                        Varai Rs. :</td>
                    <td style="width: 20%; height: 15px;">
                        <asp:TextBox ID="txt_Varai" runat="server" CssClass="TEXBOX" MaxLength="5"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                            Text="" Width="50%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                        Varai Remark :</td>
                    <td style="width: 45%; height: 15px;">
                        <asp:TextBox ID="txt_VaraiRemark" runat="server" CssClass="TEXTBOX" MaxLength="200"
                            TextMode="MultiLine"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                        Chai Pani Rs. :</td>
                    <td style="width: 20%; height: 15px;">
                        <asp:TextBox ID="txt_ChaiPani" runat="server" CssClass="TEXBOX" MaxLength="5"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                            Text="" Width="50%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                    </td>
                    <td style="width: 45%; height: 15px;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr>
                    <td style="width: 98%">
                        <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
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
