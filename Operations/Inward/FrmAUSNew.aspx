<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAusNew.aspx.cs" Inherits="Operations_Inward_FrmAusNew" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Inward/AUS.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
function input_screen_action(action)
{
if (action == 'view')
  {
  tbl_input_screen.style.display='inline';
  }
else
  {
  tbl_input_screen.style.display='none';
  }
}

function viewwindow_AUSNewExcess()
{
        var Path='FrmAUSNewExcessDetails.aspx?Id='+<%=keyID %>;
        var w = screen.availWidth;
        var h = screen.availHeight/2;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path, 'CustomPopUpAUSNewExcess', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_AUSNewDamage()
{
        var Path='FrmAUSNewDamageDetails.aspx?Id='+<%=keyID %>;
        var w = screen.availWidth;
        var h = screen.availHeight/2;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path, 'CustomPopUpAUSNewExcess', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_AUSNewShort()
{
        var Path='FrmAUSNewShortDetails.aspx?Id='+<%=keyID %>;
        var w = screen.availWidth;
        var h = screen.availHeight/2;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path, 'CustomPopUpAUSNewExcess', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truck Unloading Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Truck Unloading Sheet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TURNo" CssClass="LABEL" Text="TUR No :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_TURNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
                    <td style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TURDate" CssClass="LABEL" Text="TUR Date :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <table cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="wuc_AUSDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                        PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True"
                                        OnSelectionChanged="wuc_AUSDate_SelectionChanged">
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
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 29%">
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
                            if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wuc_AUSDate.ClientObjectId %>_loaded)
                            {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wuc_AUSDate.ClientObjectId %>;
                            window.<%= wuc_AUSDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                            }
                            else
                            {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                            }
                            }
                            ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No :" runat="server"></asp:Label></td>
                    <td style="width: 29%;">
                        <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                        *</td>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_VehicleCategory" CssClass="LABEL" Text="Vehicle Category :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="upd_VehicleCategory" runat="server">
                            <ContentTemplate>
                                <asp:Label runat="server" CssClass="LABEL" ID="lbl_Vehicle_Category" Style="font-weight: bolder"
                                    meta:resourcekey="lbl_Vehicle_CategoryResource1"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdn_Vehicle_Category_Id"></asp:HiddenField>
                                <asp:HiddenField runat="server" ID="hdn_Vehicle_Id"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                       
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_LHPONo" CssClass="LABEL" Text="LHPO No :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="upd_LHPO" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" Width="98%" AutoPostBack="True" ID="ddl_LHPO" CssClass="DROPDOWN"
                                    OnSelectedIndexChanged="ddl_LHPO_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_LHPODate" CssClass="LABEL" Text="LHPO Date :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="upd_LHPODate" runat="server">
                            <ContentTemplate>
                                <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPO_Date" Style="font-weight: bolder"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_FromLocation" CssClass="LABEL" Text="LHPO From Location :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="Upd_PnlFromLocation" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_LHPOFromLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_ToLocation" CssClass="LABEL" Text="LHPO To Location :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="Upd_PnlToLocation" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_LHPOToLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_BHTAmount" CssClass="LABEL" Text="BTH Amount :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_BTHAmountValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 20%" />
                    <td style="width: 29%" />
                    <td style="width: 1%" />
                </tr>
            </table>
            <br />
            <table class="TABLE" style="width: 100%;">
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Total_Loaded_LRH" CssClass="LABEL" Text="Total Loaded LR :" Style="font-weight: bolder;
                            font-size: medium;" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Total_Loaded_LR" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                                    font-size: medium;" ForeColor="Purple"></asp:Label>
                                    <asp:HiddenField ID="hdn_Total_Loaded_LR" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 20%" />
                    <td style="width: 29%;display:none;"><uc2:TimePicker ID="wuc_TASTime" runat="server" /></td>                    
                    <td style="width: 1%" />
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_Total_Loaded_ArticlesH" CssClass="LABEL" Text="Total Loaded Articles :"
                            Style="font-weight: bolder; font-size: medium;" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Total_Loaded_Articles" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                                    font-size: medium;" ForeColor="Purple"></asp:Label>
                                    <asp:HiddenField ID="hdn_Total_Loaded_Articles" runat="server" />
                                    <asp:HiddenField ID="hdn_Total_Received_Articles" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_Total_Loaded_WeightH" runat="server" CssClass="LABEL" Text="Total Loaded Weight :"
                            Style="font-weight: bolder; font-size: medium;"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Total_Loaded_Weight" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                                    font-size: medium;" ForeColor="Purple"></asp:Label>
                                    <asp:HiddenField ID="hdn_Total_Loaded_Weight" runat="server" />
                                    <asp:HiddenField ID="hdn_Total_Received_Weight" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" />
                </tr>
            </table>
            <br />
            <table class="TABLE" style="width: 100%;">
                <tr>
                    <td style="width: 20%;">
                    </td>
                    <td style="width: 50%;">
                        <asp:Image ID="img_Short" runat="server" ImageUrl="~/Images/Aus_Short.jpg" /></td>
                    <td style="width: 30%;">
                        <asp:RadioButtonList ID="rbl_Short" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yes" OnClick="viewwindow_AUSNewShort();"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button ID="btn_ShortUpdate" runat="server" Text="btn_ExcessUpdate" OnClick="btn_ShortUpdate_Click"  style="display:none" />
                        </td>
                </tr>
                <tr>
                    <td style="width: 20%;">
                    </td>
                    <td style="width: 50%;">
                        <asp:Image ID="img_Damage" runat="server" ImageUrl="~/Images/Aus_Damage.jpg" /></td>
                    <td style="width: 30%;">
                        <asp:RadioButtonList ID="rbl_Damage" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yes" OnClick="viewwindow_AUSNewDamage();"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button ID="btn_DamageUpdate" runat="server" Text="btn_DamageUpdate" OnClick="btn_DamageUpdate_Click"  style="display:none" />
                        
                        </td>
                </tr>
                <tr>
                    <td style="width: 20%; height: 41px;">
                    </td>
                    <td style="width: 50%; height: 41px;">
                        <asp:Image ID="img_Excess" runat="server" ImageUrl="~/Images/Aus_Excess.jpg" /></td>
                    <td style="width: 30%; height: 41px;">
                        <asp:RadioButtonList ID="rbl_Excess" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbl_Excess_OnSelectedIndexChanged" >
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yes" OnClick="viewwindow_AUSNewExcess();"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                        <asp:Button ID="btn_ExcessUpdate" runat="server" Text="btn_ExcessUpdate" OnClick="btn_ExcessUpdate_Click"  style="display:none" />
                        
                       </td>
                </tr>
            </table>
            <br />
            <table class="TABLE" style="width: 100%; text-align: center;" border="True">
                <tr>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_ShortH" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="Total Short"></asp:Label></td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_DamagedH" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="Total Damaged"></asp:Label></td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_ExcessH" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="Total Excess"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_Short" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="0" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdn_Total_Short" runat="server" Value="0" />
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_Damaged" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="0" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdn_Total_Damaged" runat="server" Value="0" />
                            <asp:HiddenField ID="hdn_Total_Damaged_Value" runat="server" Value="0" />
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_Total_Excess" runat="server" CssClass="LABEL" Style="font-weight: bolder;
                            font-size: medium" Text="0" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdn_Total_Excess" runat="server" Value="0" />
                    </td>
                </tr>                         
            </table>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td>
                      <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
                        <ContentTemplate>
                          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="btn_Save_Print"></asp:AsyncPostBackTrigger>
                          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                        </Triggers>
                      </asp:UpdatePanel>
                    </td>
                  </tr> 
                  <tr><td>&nbsp;</td></tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center;">
                <tr>
                    <td style="width: 100%">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" OnClick="btn_Save_Click"></asp:Button>
                        <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" OnClick ="btn_Save_Exit_Click" ></asp:Button>
                        <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" OnClick="btn_Save_Print_Click" ></asp:Button>
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick ="btn_Close_Click" ></asp:Button>
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr>
                <tr><td>&nbsp;</td></tr>
            </table>
        </div>
    </form>
</body>
</html>

<script language ="javascript" type ="text/javascript">
//setFocusonPageLoad();        

function update_ShortDetails()
{
    document.getElementById('<%=btn_ShortUpdate.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_ShortUpdate.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_ShortUpdate.ClientID%>').click();
}

function update_ExcessDetails()
{
    document.getElementById('<%=btn_ExcessUpdate.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_ExcessUpdate.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_ExcessUpdate.ClientID%>').click();
}

function update_DamageDetails()
{
    document.getElementById('<%=btn_DamageUpdate.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_DamageUpdate.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_DamageUpdate.ClientID%>').click();
}
</script>
