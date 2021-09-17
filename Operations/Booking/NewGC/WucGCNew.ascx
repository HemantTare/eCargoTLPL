<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucGCNew.ascx.cs" Inherits="Operations_Booking_WucGCNew" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%--<%@ Register Src="~/CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>

<script language="javascript" type="text/javascript">  
function MyJavascriptFunction()
{    
    var confirm_value = document.createElement("INPUT"); 
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Do you want to Print?"))
    {
        confirm_value.value = "Yes"; 
    }
    else 
    {
        confirm_value.value = "No"; 
    }
    document.forms[0].appendChild(confirm_value); 
}
</script>

<asp:ScriptManager runat="server" ID="scm_gc">
</asp:ScriptManager>
<table class="TABLENOBORDER">
    <tr style="display: none">
        <td style="width: 100%; display: none" colspan="9">
            &nbsp;
            <asp:Label ID="lbl_Errors1" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="9">
            <table style="width: 100%" border="0">
                <tr>
                    <td class="TD1" style="width: 8%">
                        <asp:Label ID="lbl_GCNo" CssClass="LABEL" Text="GC No :" runat="server" meta:resourcekey="lbl_GCNoResource1"></asp:Label>
                    </td>
                    <td style="width: 18%;" align="left">
                        <asp:DropDownList ID="ddl_GC_No" runat="server" Width="35%" CssClass="DROPDOWN" />
                        <asp:Label ID="lbl_GC_No" CssClass="LABEL" Width="2%" runat="server" Text="-" />
                        <asp:TextBox ID="txt_GC_No" runat="server" Width="50%" onblur="Check_Valid_GC_No()"
                            onkeyPress="return Only_Integers(this,event);" CssClass="TEXTBOX" Font-Bold="True"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        <asp:HiddenField ID="hdn_GC_No" runat="server" Value="0" />
                        <asp:HiddenField ID="hdn_Next_No" runat="server" Value="0" />
                        <asp:HiddenField ID="hdn_Start_No" runat="server" Value="0" />
                        <asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />
                    </td>
                    <td class="TD1" style="width: 8%">
                        <asp:Label ID="lbl_BookingDate" CssClass="LABEL" Text="Booking Date :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 12%;">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="wuc_BookingDate" runat="server" MinDate="1900-01-01" Width="95%"
                                        CellPadding="2" AllowDaySelection="True" AllowMonthSelection="True" ClientSideOnSelectionChanged="GC_Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                        PickerFormat="Custom">
                                    </ComponentArt:Calendar>
                                </td>
                                <td style="height: 24px" runat="server" id="TD_Calender">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                        <asp:Label ID="lbl_BookingTime" CssClass="LABEL" Text="Booking Time :" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Booking_Type" CssClass="LABEL" Text="Booking Type :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 12%">
                        <asp:DropDownList ID="ddl_Booking_Type" runat="server" CssClass="DROPDOWN" onchange="On_BookingTypeChange()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_Booking_Type" runat="server" />
                        <div style="display: none">
                            <uc2:TimePicker ID="wuc_BookingTime" runat="server" />
                        </div>
                    </td>
                    <td style="width: 12%">
                        <asp:Label ID="lbl_Dly_Type" CssClass="LABEL" Text="Delivery Type :" runat="server"></asp:Label></td>
                    <td style="width: 17%">
                        <asp:DropDownList ID="ddl_Dly_Type" runat="server" CssClass="DROPDOWN" onchange="On_DeliveryType_Change()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_Dly_Type" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="display: none">
        <td class="TD1" style="width: 10%">
            &nbsp;</td>
        <td style="width: 15%;">
            <asp:LinkButton ID="lnk_Get_Next_No" runat="server" Text="Get Next No" OnClientClick="return Get_Next_No()"></asp:LinkButton>
            <asp:LinkButton ID="lnk_View_Details" runat="server" Text="View Details" OnClientClick="return View_GC_Details();"></asp:LinkButton>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 14%">
            <asp:Label ID="lbl_pickup_reqId" CssClass="LABEL" Text="Pickup Req ID :" runat="server" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnEncrypted_pickuprequestid" runat="server" />
                    <asp:HiddenField ID="hdnPickupReqId" runat="server" />
                    <asp:LinkButton ID="lnk_Pickup_Req" runat="server" OnClientClick="return View_PickUp_Details()"
                        Text="View"></asp:LinkButton>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_pick_request" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 15%">
            <asp:DropDownList ID="ddl_pick_request" AutoPostBack="true" CssClass="DROPDOWN" runat="server"
                OnSelectedIndexChanged="ddl_pick_request_SelectedIndexChanged" />
            <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="GC_Calendar_OnSelectionChanged"
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../../images/"
                PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wuc_BookingDate.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wuc_BookingDate.ClientObjectId %>;
                        window.<%= wuc_BookingDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Pickup_Type" CssClass="LABEL" Text="Pickup Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_Pickup_Type" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="tr_Copy_GC_Details" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_AttachedGCNo" CssClass="LABEL" Text="Copy From GC No :" runat="server"
                meta:resourcekey="lbl_AttachedGCNoResource1"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:TextBox ID="txt_Attached_GC_No" onblur="On_Attached_GC_No_Change();" runat="server"
                onkeyPress="return Only_Integers(this,event);" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TDMANDATORY" style="width: 10%">
            <asp:Button ID="btn_GetAttachedGCDetails" runat="server" CssClass="BUTTON" Text="Get GC Details"
                OnClick="btn_GetAttachedGCDetails_Click" meta:resourcekey="btn_GetAttachedGCDetailsResource1" />
        </td>
        <td style="width: 15%">
            <asp:CheckBox ID="chk_IsAttached" runat="server" onclick="On_IsAttached_Click();"
                Text="Is Attached GC" meta:resourcekey="chk_IsAttachedResource1"></asp:CheckBox>
            <asp:HiddenField ID="hdn_IsAttached" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_CanAttached" runat="server" Value="false" />
            <asp:HiddenField ID="hdn_AttachedGCId" runat="server" Value="0" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td style="width: 10%;" id="td_chk_IsReBook" runat="server">
            <asp:CheckBox ID="chk_IsReBook" runat="server" onclick="On_IsReBooked_Click(); "
                Text="Is ReBook GC" meta:resourcekey="chk_IsReBookResource1"></asp:CheckBox>
            <asp:HiddenField ID="hdn_ReBookGCId" runat="server" Value="0" />
        </td>
        <td class="TD1" style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_CustomerRefNo" CssClass="LABEL" Text="Client Ref No :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_CustomerRefNo" runat="server" CssClass="TEXTBOX" MaxLength="20"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_DelWay_Type" CssClass="LABEL" Text="Delivery Way Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:DropDownList ID="ddl_DelWay_Type" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Service_Type" CssClass="LABEL" Text="Service Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:DropDownList ID="ddl_Service_Type" runat="server" CssClass="DROPDOWN" onchange="On_PickerChangeForServiceType()">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server" id="tr_Agency_Details">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Agency_GCNo" CssClass="LABEL" Text="Agency GC No :" runat="server"
                meta:resourcekey="lbl_Agency_GCNoResource1"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_Agency_GCNo" runat="server" onkeyPress="return Only_Integers(this,event);"
                CssClass="TEXTBOXNOS" onchange="On_Change_Agency_No(this);"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Agency_Branch" CssClass="LABEL" Text="Agency Branch :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_Agency" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                onblur="Agency_LostFocus(this,'WucGCNew1_lst_Agency')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_Agency','Agency',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_Agency');" onfocus="On_Focus('WucGCNew1_txt_Agency','WucGCNew1_lst_Agency','Agency');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_Agency" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_Agency')"
                runat="server" TabIndex="10"></asp:ListBox>
            <asp:HiddenField ID="hdn_AgencyId" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Agency_Ledger" CssClass="LABEL" Text="Agency Ledger :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:TextBox ID="txt_Ledger" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                onblur="Branch_LostFocus(this,'WucGCNew1_lst_Ledger')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_Ledger','Ledger',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_Ledger');" onfocus="On_Focus('WucGCNew1_txt_Ledger','WucGCNew1_lst_Ledger','Ledger');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_Ledger" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_Ledger')"
                runat="server" TabIndex="20"></asp:ListBox>
            <asp:HiddenField ID="hdn_LedgerId" runat="server" />
            <asp:HiddenField ID="hdn_LedgerName" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server" id="tr_Opening_Details">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Bkg_Branch" CssClass="LABEL" Text="Booking Branch :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_Booking_Branch" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                onblur="Branch_LostFocus(this,'WucGCNew1_lst_BookBranch')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_BookBranch','BookingBranch',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_BookBranch');" onfocus="On_Focus('WucGCNew1_txt_Booking_Branch','WucGCNew1_lst_BookBranch','BookingBranch');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_BookBranch" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_Booking_Branch')"
                runat="server" TabIndex="30"></asp:ListBox>
            <asp:HiddenField ID="hdn_BookingBranchId" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Arrived_From" CssClass="LABEL" Text="Arrived From :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_ArrivedFrom_Branch" AutoCompleteType="Disabled" runat="server"
                CssClass="TEXTBOX" onblur="Branch_LostFocus(this,'WucGCNew1_lst_ArrivedFromBranch')"
                onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_ArrivedFromBranch','ArrivedFromBranch',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_ArrivedFromBranch');"
                onfocus="On_Focus('WucGCNew1_txt_ArrivedFrom_Branch','WucGCNew1_lst_ArrivedFromBranch','ArrivedFromBranch');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_ArrivedFromBranch" Style="position: absolute; z-index: 1000"
                onfocus="listboxonfocus('WucGCNew1_txt_ArrivedFrom_Branch')" runat="server" TabIndex="40">
            </asp:ListBox>
            <asp:HiddenField ID="hdn_ArrivedFromBranchId" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Arrived_Date" CssClass="LABEL" Text="Arrived Date :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <uc1:WucDatePicker ID="Wuc_Arrived_Date" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server">
        <td style="width: 100%" colspan="10">
            <table style="width: 100%" border="0">
                <tr>
                    <td class="TD1" style="width: 10%">
                        <asp:Label ID="lbl_From_Location" CssClass="LABEL" Text="From Location :" runat="server"
                            meta:resourcekey="lbl_From_LocationResource1"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txt_From_Location" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                            onblur="Location_LostFocus(this,'WucGCNew1_lst_FromLoc')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_FromLoc','FromLoc',2);"
                            onkeydown="return on_keydown(event,this,'WucGCNew1_lst_FromLoc');" onfocus="On_Focus('WucGCNew1_txt_From_Location','WucGCNew1_lst_FromLoc','FromLoc');"
                            MaxLength="50" EnableViewState="False"></asp:TextBox>
                        <asp:ListBox ID="lst_FromLoc" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_From_Location')"
                            runat="server" TabIndex="50"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hdn_FromLocationId" Value="0"></asp:HiddenField>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        <asp:LinkButton ID="lnk_AddToServiceLocation" runat="server" Text="Add" OnClientClick="return Open_PopPage(0,'AddLocation');"></asp:LinkButton>
                    </td>
                    <td class="TD1" style="width: 10%">
                        <asp:Label ID="lbl_To_Location" CssClass="LABEL" Text="To Location :" runat="server"
                            meta:resourcekey="lbl_To_LocationResource1"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txt_To_Location" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                            onblur="Location_LostFocus(this,'WucGCNew1_lst_ToLoc')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_ToLoc','ToLoc',2);"
                            onkeydown="return on_keydown(event,this,'WucGCNew1_lst_ToLoc');" onfocus="On_Focus('WucGCNew1_txt_To_Location','WucGCNew1_lst_ToLoc','ToLoc');"
                            MaxLength="50" EnableViewState="False"></asp:TextBox>
                        <asp:ListBox ID="lst_ToLoc" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_To_Location')"
                            runat="server" TabIndex="60"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hdn_ToLocationId" Value="0"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_To_Location" Value="0"></asp:HiddenField>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        <asp:LinkButton ID="lnk_FromServiceLocation" runat="server" Text="Add" OnClientClick="return Open_PopPage(1,'AddLocation');"></asp:LinkButton>
                    </td>
                    <td class="TD1" style="width: 10%">
                        <asp:Label ID="lbl_Dly_Branch" CssClass="LABEL" Text="Dly Branch :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="lbl_Dly_Branch_Value" CssClass="LABEL" runat="server" Font-Bold="True"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdn_DeliveryBranchId"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_DeliveryBranchName"></asp:HiddenField>
                    </td>
                    <td style="width: 9%">
                        &nbsp;<asp:Label ID="lbl_PaymentType" CssClass="LABEL" Text="Payment Type:" runat="server"></asp:Label></td>
                    <td style="width: 14%">
                        &nbsp;
                        <asp:DropDownList ID="ddl_PaymentType" runat="server" onchange="PaymentType_Change_Confirmation();"
                            CssClass="DROPDOWN" />
                        <asp:HiddenField ID="hdn_OldPaymentType" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <%--<tr>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_From_Location" CssClass="LABEL" Text="From Location :" runat="server"
                meta:resourcekey="lbl_From_LocationResource1"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_From_Location" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                onblur="Location_LostFocus(this,'WucGCNew1_lst_FromLoc')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_FromLoc','FromLoc',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_FromLoc');" onfocus="On_Focus('WucGCNew1_txt_From_Location','WucGCNew1_lst_FromLoc','FromLoc');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_FromLoc" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_From_Location')"
                runat="server" TabIndex="50"></asp:ListBox>
            <asp:HiddenField runat="server" ID="hdn_FromLocationId" Value="0"></asp:HiddenField>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:LinkButton ID="lnk_AddToServiceLocation" runat="server" Text="Add" OnClientClick="return Open_PopPage(0,'AddLocation');"></asp:LinkButton>
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_To_Location" CssClass="LABEL" Text="To Location :" runat="server"
                meta:resourcekey="lbl_To_LocationResource1"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_To_Location" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                onblur="Location_LostFocus(this,'WucGCNew1_lst_ToLoc')" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_ToLoc','ToLoc',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_ToLoc');" onfocus="On_Focus('WucGCNew1_txt_To_Location','WucGCNew1_lst_ToLoc','ToLoc');"
                MaxLength="50" EnableViewState="False"></asp:TextBox>
            <asp:ListBox ID="lst_ToLoc" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_To_Location')"
                runat="server" TabIndex="60"></asp:ListBox>
            <asp:HiddenField runat="server" ID="hdn_ToLocationId" Value="0"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_To_Location" Value="0"></asp:HiddenField>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:LinkButton ID="lnk_FromServiceLocation" runat="server" Text="Add" OnClientClick="return Open_PopPage(1,'AddLocation');"></asp:LinkButton>
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Dly_Branch" CssClass="LABEL" Text="Dly Branch :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:Label ID="lbl_Dly_Branch_Value" CssClass="LABEL" runat="server" Font-Bold="True"></asp:Label>
            <asp:HiddenField runat="server" ID="hdn_DeliveryBranchId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_DeliveryBranchName"></asp:HiddenField>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>--%>
    <tr id="tr_billing_details" runat="server">
        <td class="TD1" style="width: 10%; height: 21px;">
            <asp:Label ID="lbl_BillingParty" CssClass="LABEL" Text="Billing Party :" runat="server"></asp:Label></td>
        <td align="Left" style="width: 15%; height: 21px;">
            <asp:TextBox AutoCompleteType="Disabled" CssClass="TEXTBOX" EnableViewState="False"
                ID="txt_BillingParty" MaxLength="100" onblur="Billing_LostFocus(this,'WucGCNew1_lst_BillParty')"
                onfocus="On_Focus('WucGCNew1_txt_BillingParty','WucGCNew1_lst_BillParty','BillingParty');"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_BillParty');" onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_BillParty','BillingParty',2);"
                runat="server" />
            <asp:ListBox ID="lst_BillParty" runat="server" Style="z-index: 1000; position: absolute"
                    onfocus="listboxonfocus('WucGCNew1_txt_BillingParty')" TabIndex="90"></asp:ListBox>
            <asp:HiddenField runat="server" ID="hdn_BillingPartyId" />
            <asp:HiddenField ID="hdn_BillingParty" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Billing_Party_CreditLimit" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Billing_Party_ClosingBalance" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Billing_Party_MinimumBalance" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Billing_Party_Ledger_Closing_Balance" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_BillingParty_LedgerId" runat="server"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_IsServiceTaxApplForBillParty"></asp:HiddenField>
        </td>
        <td class="TDMANDATORY" style="width: 1%; height: 21px;">
        </td>
        <td style="width: 10%; height: 21px;" align="Right">
            <asp:Label ID="lbl_BillingLocation" CssClass="LABEL" Text="Location :" runat="server"></asp:Label></td>
        <td align="Left" style="width: 15%; height: 21px;">
            <asp:TextBox ID="txt_BillingLocation" AutoCompleteType="Disabled" Width="90%" onblur="Billing_LostFocus(this,'WucGCNew1_lst_BillLocation')"
                onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_BillLocation','BillingLocation',2);"
                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_BillLocation');" onfocus="On_Focus('WucGCNew1_txt_BillingLocation','WucGCNew1_lst_BillLocation','BillingLocation');"
                runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False" Enabled="False"></asp:TextBox>
            <asp:ListBox ID="lst_BillLocation" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_BillingLocation')"
                runat="server" TabIndex="100"></asp:ListBox>
            <asp:HiddenField runat="server" ID="hdn_BillingLocationId" />
            <asp:HiddenField runat="server" ID="hdn_BillingLocation" />
        </td>
        <td class="TDMANDATORY" style="width: 1%; height: 21px;">
        </td>
        <td style="width: 26%; height: 21px;" colspan="3">
            &nbsp; &nbsp;
        </td>
    </tr>
    <tr style="display: none">
        <td class="TD1" style="width: 10%">
            &nbsp;
        </td>
        <td style="width: 15%;">
            &nbsp;
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            &nbsp;
        </td>
        <td style="width: 15%">
            &nbsp;
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Booking_Sub_Type" CssClass="LABEL" Text="Booking Sub Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_booking_Sub_Type" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="tr_onBookingTypechange" runat="server">
        <td class="TD1" style="width: 10%" id="td_vehicleType" runat="server">
            <asp:Label ID="lbl_VehicleType" CssClass="LABEL" Text="Vehicle Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_VehicleType" runat="server" CssClass="DROPDOWN" onchange="On_VehicleType_Change()">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%" id="td_vehicleno" runat="server">
            <asp:Label ID="lbl_VehicleNo" CssClass="LABEL" Text="Vehicle No :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_VehicleNo" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%" id="td_fesibilityrsno" runat="server">
            <asp:Label ID="lbl_FeasibilityAndRouteSurveyNo" CssClass="LABEL" Text="Feasibility & Route Survey No :"
                runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:TextBox ID="txt_FeasibilityAndRouteSurveyNo" runat="server" Width="95%" CssClass="TEXTBOX"
                MaxLength="10"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="tr_ConsignmentType" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Consignment_Type" CssClass="LABEL" Text="Consignment Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_Consignment_Type" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
            <asp:HiddenField ID="hdn_defaultConsignmentType" runat="server"></asp:HiddenField>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Dly_Against" CssClass="LABEL" Text="Delivery Against :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_Delivery_Against" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Road_Permit" CssClass="LABEL" Text="Road Permit :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%">
            <asp:DropDownList ID="ddl_Road_Permit" runat="server" CssClass="DROPDOWN" onchange="On_RoadPermit_Change()">
            </asp:DropDownList>
            <asp:HiddenField ID="hdn_RoadPermitTypeId" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="tr_STMNo" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_STM_No" CssClass="LABEL" Text="STM No :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_STM_No" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%" runat="server" id="td_RoadPermitSrNo">
            <asp:Label ID="lbl_Road_Permit_SrNo" CssClass="LABEL" Text="Road Permit SrNo :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txt_Road_Permit_SrNo" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr style="display: none">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Exp_Del_Date" CssClass="LABEL" Text="Exp Dly Date :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:Label ID="lbl_ExpDel_Date_Value" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdn_dly_date" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Is_Oct_Appl" CssClass="LABEL" Text="Is Octroi Applicable :" runat="server"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:CheckBox ID="chk_Is_Oct_Appl" runat="server" Enabled="False" />
            <asp:HiddenField ID="hdn_Is_Oct_Appl" runat="server" Value="false" />
            &nbsp;&nbsp;&nbsp; <a id="lnk_Require_Forms" href="javascript:RequiredForms();">
                <asp:Label ID="lbl_Require_Forms" CssClass="LABEL" Text="Required Form" runat="server" />
            </a>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 15%" id="td_ContainerDetails" runat="server">
            <asp:LinkButton ID="lnk_ContainerDetails" runat="server" Text="Container Details"
                OnClientClick="return ContainerDetails();"></asp:LinkButton>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
</table>
<table class="TABLENOBORDER">
    <tr>
        <td class="TD1" style="width: 33%; vertical-align: top">
            <asp:Panel ID="Pnl_Consignor" runat="server" Width="100%" GroupingText="Consignor">
                <table style="width: 100%" border="0">
                    <tr id="tr_CnsrsearchByCode" runat="server">
                        <td style="width: 10%;" class="TD1" runat="server">
                            <asp:Label ID="lbl_ConsignorSearchByCode" runat="server" CssClass="LABEL" Text="Search :"
                                Visible="False"></asp:Label>
                        </td>
                        <td align="left" runat="server" colspan="2">
                            <asp:Label ID="lbl_ConsignorMobileNumbers" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lbl_ConsignorPhoneNumbers" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lbl_consignor_Client_Code" Font-Bold="True" runat="server" CssClass="LABEL"
                                ForeColor="#9900FF"></asp:Label>
                        </td>
                        <td align="center" style="width: 10%;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%" class="TD1">
                            <asp:Label ID="lbl_ConsignorName" runat="server" CssClass="LABEL" Text="Name :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txt_ConsignorName" AutoCompleteType="Disabled" Width="90%" onblur="Client_LostFocus(this,'WucGCNew1_lst_Consignors')"
                                onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_Consignors','Consignor',2);"
                                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_Consignors'),hotKey(event,this,'Consignor');"
                                onfocus="On_Focus('WucGCNew1_txt_ConsignorName','WucGCNew1_lst_Consignors','Consignor');"
                                runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox><br />
                            <asp:ListBox ID="lst_Consignors" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_ConsignorName')"
                                runat="server" TabIndex="70" Height="230px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr id="tr_Consignor_Details" runat="server">
                        <td style="width: 10%; vertical-align: top" class="TD1" runat="server">
                            <asp:Label ID="lbl_ConsignorDetails" runat="server" CssClass="LABEL" Text="Address :"></asp:Label>
                        </td>
                        <td align="left" colspan="3" runat="server">
                            <%--              <div id="div_ConsignorDetailsValue" class="DIV" style="height: 70px; text-align: left">--%>
                            <asp:Label ID="lbl_ConsignorDetailsValue" runat="server" CssClass="LABEL"></asp:Label>
                            <asp:TextBox ID="txt_ConsignorDetailsValue" runat="server" ReadOnly="true" CssClass="TEXTBOX"
                                BackColor="transparent" Width="350px"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hdn_ConsignorDetailsValue"></asp:HiddenField>
                            <%--              </div>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBox ID="chk_SignedByConsignor" runat="server" Text="Signed by Consignor?">
                            </asp:CheckBox>
                            &nbsp;
                            <asp:LinkButton ID="lnk_NewConsignor" runat="server" Text="Add New(F2)" OnClientClick="return New_Consignor_Consignee(0,1);"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnk_EditConsignor" runat="server" Text="Edit" OnClientClick="return New_Consignor_Consignee(1,1);"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnk_ViewConsignor" runat="server" Text="View" OnClientClick="return View_Consignor_Consignee(1);"></asp:LinkButton>
                            <asp:CheckBox ID="chk_ConsignorSearchByCode" runat="server" Text="By Mobile" Enabled="false"
                                CssClass="HIDEGRIDCOL" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="left" colspan="4">
                            <asp:HiddenField ID="hdn_ConsignorId" runat="server" Value="0"></asp:HiddenField>
                            <asp:HiddenField ID="hdn_IsRegularConsignor" runat="server" />
                            <asp:HiddenField ID="hdn_IsServiceTaxApplicableForConsignor" runat="server" />
                            <asp:HiddenField ID="hdn_ConsignorStateId" Value="0" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td class="TD1" style="width: 33%; vertical-align: top">
            <asp:Panel ID="pnl_Consignee" runat="server" Width="100%" GroupingText="Consignee">
                <table style="width: 100%; vertical-align: top">
                    <tr id="tr_CsneesearchByCode" runat="server">
                        <td style="width: 10%" class="TD1" runat="server">
                            <asp:Label ID="lbl_ConsigneeSearchByCode" runat="server" CssClass="LABEL" Text="Search :"
                                Visible="False"></asp:Label>
                        </td>
                        <td align="left" runat="server" colspan="2">
                            <asp:Label ID="lbl_ConsigneeMobileNumbers" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lbl_ConsigneePhoneNumbers" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lbl_consignee_Client_Code" Font-Bold="True" runat="server" CssClass="LABEL"
                                ForeColor="#9900FF"></asp:Label></td>
                        <td align="center" style="width: 10%" runat="server">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%" class="TD1">
                            <asp:Label ID="lbl_ConsigneeName" runat="server" CssClass="LABEL" Text="Name :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txt_ConsigneeName" AutoCompleteType="Disabled" Width="90%" onblur="Client_LostFocus(this,'WucGCNew1_lst_Consignees')"
                                onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_Consignees','Consignee',2);"
                                onkeydown="return on_keydown(event,this,'WucGCNew1_lst_Consignees'),hotKey(event,this,'Consignee');"
                                onfocus="On_Focus('WucGCNew1_txt_ConsigneeName','WucGCNew1_lst_Consignees','Consignee');"
                                runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox><br />
                            <asp:ListBox ID="lst_Consignees" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_ConsigneeName')"
                                runat="server" TabIndex="80" Height="230px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr id="tr_Consignee_Details" runat="server">
                        <td style="width: 10%; vertical-align: top" class="TD1" runat="server">
                            <asp:Label ID="lbl_ConsigneeDetails" runat="server" CssClass="LABEL" Text="Address :"></asp:Label>
                        </td>
                        <td align="left" colspan="3" runat="server">
                            <%-- <div id="div_ConsigneeDetails" class="DIV" style="height: 70px; text-align: left">--%>
                            <asp:Label ID="lbl_ConsigneeDetailsValue" runat="server" CssClass="LABEL"></asp:Label>
                            <asp:TextBox ID="txt_ConsigneeDetailsValue" runat="server" Width="350px" ReadOnly="true"
                                CssClass="TEXTBOX" BackColor="transparent"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hdn_ConsigneeDetailsValue"></asp:HiddenField>
                            <%--</div>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <table width="100%" border="0">
                                <tr>
                                    <td style="width: 44%" class="TD1">
                                        <asp:Label ID="lbl_ConsigneeDeliveryAreaName" runat="server" CssClass="LABELERROR"
                                            Width="100%"></asp:Label>
                                    </td>
                                    <td style="width: 5%" class="TD1">
                                        <asp:Panel ID="pnl_Change_Consignee_Address" runat="server" BorderWidth="0px" Width="100%">
                                            <asp:LinkButton ID="lnk_Change_Door_Delivery_Address" runat="server" Text="D" OnClientClick="return Change_Consignee_Address();"></asp:LinkButton>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 55%" class="TD1">
                                        <asp:Panel ID="pnl_EditConsignee" runat="server" BorderWidth="0px" Width="100%" align="right">
                                            <asp:LinkButton ID="lnk_NewConsignee" runat="server" Text="Add New(F2)" OnClientClick="return New_Consignor_Consignee(0,0);"></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnk_EditConsignee" runat="server" Text="Edit" OnClientClick="return New_Consignor_Consignee(1,0);"></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnk_ViewConsignee" runat="server" Text="View" OnClientClick="return View_Consignor_Consignee(0);"></asp:LinkButton>
                                            <asp:CheckBox ID="chk_ConsigneeSearchByCode" runat="server" Text="By Mobile" Enabled="false"
                                                CssClass="HIDEGRIDCOL"></asp:CheckBox></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="left" colspan="4">
                            <asp:HiddenField ID="hdn_ConsigneeId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdn_Consignee_CSTTINNo" runat="server" />
                            <asp:HiddenField ID="hdn_ConsigneeDDAddressLine1" runat="server" />
                            <asp:HiddenField ID="hdn_ConsigneeDDAddressLine2" runat="server" />
                            <asp:HiddenField ID="hdn_IsRegularConsignee" runat="server" />
                            <asp:HiddenField ID="hdn_IsServiceTaxApplicableForConsignee" runat="server" />
                            <asp:HiddenField ID="hdn_ConsigneeStateId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdn_ConsigneeDeliveryAreaID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdn_Consignee_Delivery_Type_Id" runat="server" />
                            <asp:HiddenField ID="hdn_Consignee_Is_ToPay_Allowed" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td class="TD1" style="width: 34%; vertical-align: top; display: none">
            <table class="TABLE">
                <tr id="tr_Contractual_Client_Details" runat="server">
                    <td class="TD1" style="width: 41%">
                        <asp:Label ID="lbl_ContractualClient" CssClass="LABEL" Text="Contract Client:" runat="server"></asp:Label>
                    </td>
                    <td style="width: 59%" align="left">
                        <asp:TextBox ID="txt_ContractClient" AutoCompleteType="Disabled" Width="155px" onblur="ContractClient_LostFocus(this,'WucGCNew1_lst_ContractClient')"
                            onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_ContractClient','ContractClient',2);"
                            onkeydown="return on_keydown(event,this,'WucGCNew1_lst_ContractClient');" onfocus="On_Focus('WucGCNew1_txt_ContractClient','WucGCNew1_lst_ContractClient','ContractClient');"
                            runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox><br />
                        <asp:ListBox ID="lst_ContractClient" Style="position: absolute; z-index: 1000" Width="155px"
                            onfocus="listboxonfocus('WucGCNew1_txt_ContractClient')" runat="server" TabIndex="80">
                        </asp:ListBox>
                        <asp:HiddenField ID="hdn_ContractualClientId" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_ContractClient" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_IsContractApplied" runat="server" Value="0"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_Contract_Branch_Details" runat="server">
                    <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_ContractBranch" CssClass="LABEL" Text="Contract Branch:" runat="server"></asp:Label>
                    </td>
                    <td style="width: 59%" align="left">
                        <asp:DropDownList ID="ddl_ContractBranch" runat="server" CssClass="DROPDOWN" onchange="On_ContractBranch_Change()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_ContractBranchId" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_Contract_Details" runat="server">
                    <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_Contract" CssClass="LABEL" Text="Contract Name:" runat="server"></asp:Label>
                    </td>
                    <td style="width: 59%" align="left">
                        <asp:DropDownList ID="ddl_Contract" runat="server" CssClass="DROPDOWN" onchange="On_Contract_Change()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_ContractId" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="TABLENOBORDER" style="display: none">
    <tr id="tr_multiple_billing_details" runat="server">
        <td style="width: 12%;" class="TD1">
            <asp:Label ID="lbl_IsMultipleBilling" CssClass="LABEL" Text="Multiple Billing :"
                runat="server"></asp:Label>
        </td>
        <td colspan="6">
            <asp:CheckBox ID="chk_IsMultipleBilling" runat="server" onclick="On_IsMultipleBilling_Click();" />
        </td>
    </tr>
</table>
<table class="TABLENOBORDER" style="display: none">
    <tr>
        <td style="width: 15%" class="TD1">
            <asp:Label ID="lbl_IsPODRequired" runat="server" Text="POD Required :" />
        </td>
        <td style="width: 5%">
            <asp:CheckBox ID="chk_PodRequire" runat="server" />
            <asp:HiddenField ID="hdn_defaultPod" runat="server" />
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_GCRisk" CssClass="LABEL" Text="GC Risk :" runat="server" meta:resourcekey="lbl_GCRiskResource1"></asp:Label>
        </td>
        <td style="width: 15%;">
            <asp:DropDownList ID="ddl_GCRisk" runat="server" onchange="ddl_GCRisk_Change()" CssClass="DROPDOWN" />
            <asp:HiddenField ID="hdn_Default_GCRisk" runat="server"></asp:HiddenField>
        </td>
        <td style="width: 10%" class="TD1">
            <asp:Label ID="lbl_IsInsured" runat="server" Text="Is Insured? :" />
        </td>
        <td style="width: 5%;">
            <asp:CheckBox ID="chk_IsInsured" runat="server" onclick="On_IsInsured_Click();" />
        </td>
        <td style="width: 40%">
        </td>
    </tr>
</table>
<table class="TABLENOBORDER" runat="server" id="tbl_Commodity">
    <tr>
        <td style="width: 100%; vertical-align: top">
            <iframe id="IframeCommodity" name="frm" src="FrmNewGCCommodity.aspx?callfrom=gcform;"
                width="100%" scrolling="no" frameborder="0"></iframe>
        </td>
    </tr>
</table>
<table class="TABLENOBORDER">
    <tr>
        <td style="vertical-align: top; width: 70%" colspan="2">
            <table style="width: 100%" class="TABLE">
                <tr runat="server" id="tr_Commodity">
                    <td style="width: 100%; vertical-align: top">
                        <iframe id="IframeCommodity1" name="frm" src="FrmNewGCCommodity.aspx?callfrom=gcform;"
                            width="100%" scrolling="no" frameborder="0"></iframe>
                    </td>
                </tr>
                <tr runat="server" id="tr_UnitOfMeasurment">
                    <td class="TD1" width="100%">
                        <table class="TABLE">
                            <tr>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_UnitOfMeasurment" runat="server" CssClass="LABEL" Text="Measurment Unit :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:DropDownList ID="ddl_UnitOfMeasurment" runat="server" CssClass="DROPDOWN" onchange="ddl_UnitOfMeasurment_Change()">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbl_UnitOfMeasurmentLength" runat="server" CssClass="LABEL" Text="Length :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_UnitOfMeasurmentLength" onkeypress="return Only_Numbers(this,event);"
                                        runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_UnitOfMeasurmentLength" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    <asp:Label ID="lbl_mandatory_UnitOfMeasurmentLength" runat="server" Text="*"></asp:Label></td>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_UnitOfMeasurmentWidth" runat="server" CssClass="LABEL" Text="Width :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_UnitOfMeasurmentWidth" onkeypress="return Only_Numbers(this,event);"
                                        runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_UnitOfMeasurmentWidth" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    <asp:Label ID="lbl_mandatory_UnitOfMeasurmentWidth" runat="server" Text="*"></asp:Label></td>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_UnitOfMeasurmentHeight" runat="server" CssClass="LABEL" Text="Height :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_UnitOfMeasurmentHeight" onkeypress="return Only_Numbers(this,event);"
                                        runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_UnitOfMeasurmentHeight" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    <asp:Label ID="lbl_mandatory_UnitOfMeasurmentHeight" runat="server" Text="*"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1" colspan="2">
                                    <asp:Label ID="lbl_ConversionInFeet" runat="server" CssClass="LABEL" Text="Conversion In Feet :"></asp:Label>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbl_LengthInFeet" runat="server" CssClass="LABEL" Text="Length :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label Style="text-align: right" ID="lbl_LengthInFeetValue" Width="98%" runat="server"
                                        CssClass="LABEL" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="hdn_LengthInFeet" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_WidthInFeet" runat="server" CssClass="LABEL" Text="Width :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label Style="text-align: right" ID="lbl_WidthInFeetValue" Width="98%" runat="server"
                                        CssClass="LABEL" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="hdn_WidthInFeet" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_HeightInFeet" runat="server" CssClass="LABEL" Text="Height :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label Style="text-align: right" ID="lbl_HeightInFeetValue" Width="98%" runat="server"
                                        CssClass="LABEL" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="hdn_HeightInFeet" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="TD1" width="100%">
                        <table class="TABLE">
                            <tr>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_FreightBasis" runat="server" CssClass="LABEL" Text="Freight Basis :"></asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:DropDownList ID="ddl_FreightBasis" runat="server" CssClass="DROPDOWN" Width="70%"
                                        onchange="ddl_FreightBasis_Change();">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdn_FreightBasisId" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdn_defaultFreightBasisId" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 10%">
                                    &nbsp;</td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    &nbsp;</td>
                                <td style="width: 10%" class="TD1">
                                    &nbsp;</td>
                                <td style="width: 10%">
                                    &nbsp;</td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    &nbsp;</td>
                            </tr>
                            <tr id="tr_Volumetric" runat="server">
                                <td id="Td1" style="width: 10%" class="TD1" runat="server">
                                    <asp:Label ID="lblVolumetricFreightUnit" runat="server" CssClass="LABEL" Text="Volumetric Freight Unit :"></asp:Label>
                                </td>
                                <td id="Td2" style="width: 10%" runat="server">
                                    <asp:DropDownList ID="ddl_VolumetricFreightUnit" runat="server" CssClass="DROPDOWN"
                                        Width="99%" onchange="ddl_VolumetricFreightUnit_Change();">
                                    </asp:DropDownList>
                                </td>
                                <td id="Td3" style="width: 1%" class="TDMANDATORY" runat="server">
                                    <asp:Label ID="lbl_mandatory_VolumetricFreightUnit" runat="server" Text="*"></asp:Label></td>
                                <td id="Td4" style="width: 10%" runat="server">
                                    <asp:Label ID="lbl_TotalCFT" runat="server" CssClass="LABEL" Text="Total CFT :"></asp:Label>
                                </td>
                                <td id="Td5" style="width: 10%" runat="server">
                                    <asp:Label Style="text-align: right" ID="lbl_TotalCFTValue" Width="98%" runat="server"
                                        CssClass="LABEL" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="hdn_TotalCFT" runat="server"></asp:HiddenField>
                                </td>
                                <td id="Td6" style="width: 1%" class="TDMANDATORY" runat="server">
                                    <asp:Label ID="lbl_mandatory_TotalCFT" runat="server"></asp:Label></td>
                                <td id="Td7" style="width: 10%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_TotalCBM" runat="server" CssClass="LABEL" Text="Total CBM :"></asp:Label>
                                </td>
                                <td id="Td8" style="width: 10%" runat="server">
                                    <asp:Label Style="text-align: right" ID="lbl_TotalCBMValue" Width="98%" runat="server"
                                        CssClass="LABEL" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="hdn_TotalCBM" runat="server"></asp:HiddenField>
                                </td>
                                <td id="Td9" style="width: 1%" class="TDMANDATORY" runat="server">
                                    <asp:Label ID="lbl_mandatory_TotalCBM" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="tr_VolumetrickgFactor" runat="server">
                                <td id="Td10" style="width: 10%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_VolumetricToKgFactor" runat="server" CssClass="LABEL" Text="Volumetric To Kg Factor :"></asp:Label>
                                </td>
                                <td id="Td11" style="width: 10%" runat="server">
                                    <asp:TextBox ID="txt_VolumetricToKgFactor" runat="server" CssClass="TEXTBOXNOS" MaxLength="7"
                                        Width="95%" onchange="On_VolumetricToKgFactor_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_VolumetricToKgFactor" runat="server"></asp:HiddenField>
                                </td>
                                <td id="Td12" style="width: 1%" class="TDMANDATORY" runat="server">
                                </td>
                                <td id="Td13" style="width: 10%" runat="server">
                                </td>
                                <td id="Td14" style="width: 10%" runat="server">
                                </td>
                                <td id="Td15" style="width: 1%" class="TDMANDATORY" runat="server">
                                </td>
                                <td id="Td16" style="width: 10%" class="TD1" runat="server">
                                </td>
                                <td id="Td17" style="width: 10%" runat="server">
                                </td>
                                <td id="Td18" style="width: 1%" class="TDMANDATORY" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_ActualWt" runat="server" CssClass="LABEL" Text="Actual Wt.(kgs) :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_ActualWeight" Enabled="False" onkeypress="return Only_Numbers(this,event);"
                                        runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange=" On_Actual_Weight_Change();"></asp:TextBox>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbl_ChargeWt" runat="server" CssClass="LABEL" Text="Charge Wt.(kgs) :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_ChargeWeight" onkeypress="return Only_Numbers(this,event);"
                                        runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange=" On_ChargeWeight_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_ChargeWeight" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_FreightRate" runat="server" CssClass="LABEL" Text="Freight Rate :"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txt_FreightRate" onkeypress="return Only_Numbers(this,event);" runat="server"
                                        CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange="On_Freight_Rate_Change();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_FreightRate" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td width="100%">
                        <iframe id="IframeInvoice" name="frm" src="FrmNewGCInvoice.aspx" width="100%" scrolling="no"
                            frameborder="0"></iframe>
                    </td>
                </tr>
                <tr style="display: none">
                    <td style="vertical-align: top; width: 100%">
                        <table class="TABLE" border="0">
                            <tr runat="server" id="tr_OCRemarks">
                                <td class="TD1" style="width: 14%">
                                    <asp:Label ID="lbl_OtherChargesRemark" CssClass="LABEL" Text="Remark :" runat="server"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txt_OtherChargesRemark" runat="server" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                            </tr>
                            <tr runat="server" id="tr_Instruction">
                                <td class="TD1" style="width: 14%;">
                                    <asp:Label ID="lbl_Instruction" CssClass="LABEL" Text="Instruction :" runat="server"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ddl_Instruction" runat="server" onchange="On_Instruction_Change();"
                                        CssClass="DROPDOWN" Width="99%">
                                    </asp:DropDownList>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr runat="server" id="tr_InstructionRemarks">
                                <td class="TD1" style="width: 14%">
                                    <asp:Label ID="lbl_InstructionRemark" CssClass="LABEL" Text="Instruction / Remark :"
                                        runat="server"></asp:Label>
                                </td>
                                <td colspan="4" align="left">
                                    <asp:TextBox ID="txt_InstructionRemark" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                                        onkeyPress=" return Check_Max_Length_For_Multiline(this,250);" MaxLength="250"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr runat="server" id="tr_Enclosure">
                                <td class="TD1" style="width: 14%">
                                    <asp:Label ID="lbl_Enclosure" CssClass="LABEL" Text="Enclosure's :" runat="server"></asp:Label>
                                </td>
                                <td colspan="4" align="left">
                                    <asp:TextBox ID="txt_Enclosure" runat="server" CssClass="TEXTBOX" MaxLength="150"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                            </tr>
                            <tr runat="server" id="tr_EmployeeSupervisor">
                                <td class="TD1" id="td_lbl_LoadingSuperVisor" runat="server" style="width: 14%">
                                    <asp:Label ID="lbl_LoadingSuperVisor" CssClass="LABEL" Text="Loading Supervisor :"
                                        runat="server"></asp:Label>
                                </td>
                                <td id="td_ddl_LoadingSuperVisor" runat="server" style="width: 35%">
                                    <cc1:DDLSearch ID="ddl_LoadingSuperVisor" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EC.OperationModel.NewGCSearch.GetAllEmployee" CallBackAfter="2" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_Mandatory_LoadingSuperVisor" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="TD1" id="td_lbl_MarketingExecutive" runat="server" style="width: 14%">
                                    <asp:Label ID="lbl_MarketingExecutive" CssClass="LABEL" Text="Marketing Executive :"
                                        runat="server"></asp:Label>
                                </td>
                                <td id="td_ddl_MarketingExecutive" runat="server" style="width: 35%">
                                    <cc1:DDLSearch ID="ddl_MarketingExecutive" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EC.OperationModel.NewGCSearch.GetAllEmployee" CallBackAfter="2" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_Mandatory_MarketingExecutive" runat="server" Text="*"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="TABLENOBORDER">
    <tr>
        <td style="width: 65%; vertical-align: top;">
            <table class="TABLE">
                <tr>
                    <td style="width: 20%" class="TD1">
                        Invoice/Challan No.
                    </td>
                    <td style="width: 30%">
                        <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_Invoice_No" MaxLength="50"
                            onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdn_Invoice_No"></asp:HiddenField>
                    </td>
                    <td style="width: 20%" class="TD1">
                        Invoice Value
                    </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txt_Invoice_Value" runat="server" CssClass="TEXTBOXNOS" onblur="CheckInvoiceAmount(this.value);SetInvoiceDetails(this.value);txtbox_onlostfocus(this)"
                            onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                            Width="98%"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="HiddenField1"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_Private_Mark" CssClass="LABEL" Text="Private Mark :" runat="server"></asp:Label>
                    </td>
                    <td runat="server" id="td_PrivateMark">
                        <asp:TextBox ID="txt_Private_Mark" runat="server" CssClass="TEXTBOX" onchange="On_Private_Mark_Change(this)"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdn_Private_Mark"></asp:HiddenField>
                    </td>
                    <td class="TD1">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <%--<asp:Label ID="lbl_Dly_Type" CssClass="LABEL" Text="Delivery Type :" runat="server"></asp:Label>--%>
                    </td>
                    <td runat="server" id="td19">
                        <%--<asp:DropDownList ID="ddl_Dly_Type" runat="server" CssClass="DROPDOWN" onchange="On_DeliveryType_Change()"
                            onblur="WucGCNew1_txt_Freight.focus();">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_Dly_Type" runat="server" />--%>
                    </td>
                </tr>
                <tr id="tr_AddLocation" runat="server" style="display:none;">
                    <td class="TD1">
                        &nbsp;</td>
                    <td>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                    <td class="TD1">
                        &nbsp;</td>
                    <td class="TD1" runat="server" id="td_Bill_Location">
                        &nbsp;</td>
                </tr>
                <tr id="tr_cashamountchqamount" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_CashAmount" runat="server" CssClass="LABEL" Text="Cash Amount :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CashAmount" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="12" Width="95%" onchange="On_CashAmount_Change();Set_Payment_Details();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_CashAmount" runat="server"></asp:HiddenField>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_ChequeAmount" runat="server" CssClass="LABEL" Text="Cheque Amount :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ChequeAmount" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="12" Width="95%" onblur="On_ChequeAmount_Change();Set_Payment_Details();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_ChequeAmount" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_cheque_Details" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_ChequeNo" runat="server" CssClass="LABEL" Text="Cheque No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ChequeNo" onkeypress="return Only_Integers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="6" Width="95%"></asp:TextBox>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_ChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date :"></asp:Label>
                    </td>
                    <td>
                        <ComponentArt:Calendar ID="wuc_ChequeDate" runat="server" CssClass="TEXTBOX" AutoPostBackOnVisibleDateChanged="True"
                            PickerFormat="Custom" MinDate="1900-01-01" AllowMonthSelection="True" AllowDaySelection="True"
                            PickerCssClass="picker" ControlType="Picker" PickerCustomFormat="MMMM d yyyy">
                        </ComponentArt:Calendar>
                        <asp:HiddenField ID="hdn_ChequeDate" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_cheque_DetailsBank" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_Bank" runat="server" CssClass="LABEL" Text="Bank :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_BankName" runat="server" CssClass="TEXTBOX" MaxLength="50" Width="95%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr id="tr_Paid_Freight_Pending_Reason" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_Reason_Freight_Pending" runat="server" CssClass="LABEL" Text="Reason :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Reason_Freight_Pending" runat="server" CssClass="DROPDOWN"
                            Width="99%" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr id="tr_Paid_Freight_Pending_Person_Details" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_Paid_Freight_Pending_Person_Name" runat="server" CssClass="LABEL"
                            Text="Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Paid_Freight_Pending_Person_Name" runat="server" CssClass="TEXTBOX"
                            MaxLength="50" Width="98%"></asp:TextBox>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_Paid_Freight_Pending_Person_Mobile" runat="server" CssClass="LABEL"
                            Text="Mobile No. :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Paid_Freight_Pending_Person_Mobile" runat="server" CssClass="TEXTBOX"
                            MaxLength="15" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="tr1">
                    <td class="TD1">
                        Remarks:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_GC_Remarks" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                            MaxLength="250"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="tr3">
                    <td class="TD1">
                        e-Way Bill No.:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_eWayBillNo" onkeypress="return Only_Numbers(this,event);" onblur="eWayBillLossFocus(); CheckDuplicateeWayBillNo();"
                            runat="server" CssClass="TEXTBOX" MaxLength="12" Width="50%"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lbl_MultipleeWayBill" runat="server"
                            CssClass="LABEL" Text="Is Multiple eWayBill ?"></asp:Label>
                        <asp:CheckBox ID="chk_IsMultipleeWayBill" runat="server" /></td>
                </tr>
                <tr runat="server" id="tr_WholeselerBroker">
                    <td class="TD1">
                        <asp:Label ID="lbl_WholeselerBroker" runat="server" CssClass="LABEL" Text="APMC Broker :"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Wholeseler" AutoCompleteType="Disabled" onblur="Wholeseler_LostFocus(this,'WucGCNew1_lst_Wholeseler')"
                            onkeyup="NewGC_AllSearch(event,this,'WucGCNew1_lst_Wholeseler','Wholeseler',2);"
                            onkeydown="return on_keydown(event,this,'WucGCNew1_lst_Wholeseler');" onfocus="On_Focus('WucGCNew1_txt_Wholeseler','WucGCNew1_lst_Wholeseler','Wholeseler');"
                            runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False" Width="70%"></asp:TextBox><br />
                        <asp:ListBox ID="lst_Wholeseler" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucGCNew1_txt_Wholeseler')"
                            runat="server" TabIndex="90" Width="455px"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hdn_WholeselerId" />
                        <asp:HiddenField ID="hdn_Wholeseler" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <%--****************************************************************************************--%>
                <tr style="display: none">
                    <td class="TD1" style="width: 8%;">
                        <asp:Label ID="lbl_BillingHierarchy" CssClass="LABEL" Text="Hierarchy :" runat="server"></asp:Label>
                        <asp:Label ID="lbl_BillingRemark" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 13%">
                        <asp:DropDownList ID="ddl_BillingHierarchy" runat="server" CssClass="DROPDOWN" onchange="On_BillingHierarchy_Change()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_BillingHierarchy" runat="server"></asp:HiddenField>
                        <asp:TextBox ID="txt_BillingRemark" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdn_BillingRemark" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top; width: 35%">
            <table class="TABLE">
                <tr>
                    <td style="width: 65%" class="TD1">
                        <asp:Label ID="lbl_ServiceTaxPayableBy" runat="server" CssClass="LABEL" Text="GST Payable By :"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <asp:DropDownList ID="ddl_ServiceTaxPayableBy" runat="server" CssClass="DROPDOWN"
                            Width="100%" Enabled="False">
                            <asp:ListItem Text="Consignor" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Consignee" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Transporter" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdn_ServiceTaxPayableBy" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Text="Freight :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Freight" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_Freight_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_Freight" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Discount :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Discount" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_Discount_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_Discount" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_DiscountId" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_LocalCharge" runat="server" CssClass="LABEL" Text="Local Charge :"
                            meta:resourcekey="lbl_LocalChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_LocalCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" onchange="On_LocalCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_LocalCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_LoadingCharge" runat="server" CssClass="LABEL" Text="Hamali Charge :"
                            meta:resourcekey="lbl_LoadingChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_LoadingCharge" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_LoadingCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_LoadingCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_StationaryCharge" runat="server" CssClass="LABEL" Text="Statistics Charges :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_StationaryCharge" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_StationaryCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_StationaryCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_FOVRiskCharge" runat="server" CssClass="LABEL" Text="FOV / Risk Charge :"
                            meta:resourcekey="lbl_FOVRiskChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_FOVRiskCharge" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_FOVRiskCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_FOVRiskCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_DDCharge" runat="server" CssClass="LABEL" Text="DD Charge :" meta:resourcekey="lbl_DDChargeResource1"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:TextBox ID="txt_DDCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_DDCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_DDCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td class="TD1">
                        ODA Charges :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ODACharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_ODA_Change();" onblur="WucGCNew1_btn_Save_New.focus();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_ODACharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="tr_lengthChargeValue">
                    <td class="TD1">
                        <asp:Label ID="lbl_LengthCharge" runat="server" CssClass="LABEL" Text="Length Charges :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_LengthCharge" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_LengthCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_LengthCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="Tr2">
                    <td class="TD1">
                        <asp:Label ID="lbl_AOC" runat="server" CssClass="LABEL" Text="AOC :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_AOC" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_AOC_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_AOC" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_AOCPercent" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_SubTotal" runat="server" CssClass="LABEL" Text="Sub Total :"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_SubTotalValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_SubTotal" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Text="GST :"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_ServiceTaxValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_ServiceTax" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Actual_ServiceTax" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Round Off(+/-) :
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_RoundOff" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_RoundOff" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        <asp:Label ID="lbl_TotalGCAmount" runat="server" CssClass="LABEL" Text="Total GC Amount :"
                            meta:resourcekey="lbl_TotalGCAmountResource1"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_TotalGCAmountValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_TotalGCAmount" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="tr_lengthCharge">
                    <td class="TD1">
                        <asp:Label ID="lbl_LengthChargeHead" runat="server" CssClass="LABEL" Text="Length Charge Head :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_LengthChargeHead" runat="server" CssClass="DROPDOWN" Width="100px"
                            onchange="On_ddlLengthChargeChange(this)">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="TD1">
                        <asp:Label ID="lbl_Abatment" runat="server" CssClass="LABEL" Text="Abatement :"></asp:Label></td>
                    <td class="TD1">
                        <asp:Label ID="lbl_AbatmentValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_Abatment" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="TD1">
                        <asp:Label ID="lbl_TaxableAmount" runat="server" CssClass="LABEL" Text="Taxable Amount :"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_TaxableAmountValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_TaxableAmount" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_ToPayCharge" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_ToPayCharge" runat="server" CssClass="LABEL" Text="To Pay Charge :"
                            meta:resourcekey="lbl_ToPayChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ToPayCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_ToPayCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_ToPayCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_DACC" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_DACCCharge" runat="server" CssClass="LABEL" Text="IBA Charge :"
                            meta:resourcekey="lbl_DACCChargeResource1"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:TextBox ID="txt_DACCCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_DACCCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_DACCCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="TD1">
                        <asp:LinkButton ID="lnk_OtherCharges" runat="server" Text="Other Charges :" OnClientClick="return On_OtherCharges_Click();"
                            meta:resourcekey="lnk_OtherChargesResource1"></asp:LinkButton>
                        <asp:Label ID="lbl_OtherCharges" runat="server" Text="Other Charges :" meta:resourcekey="lbl_OtherChargesResource1" />
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_OtherChargesValue" runat="server" CssClass="TEXTBOXNOS"></asp:Label>
                        <asp:HiddenField ID="hdn_OtherCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="tr_NFormChargeValue">
                    <td class="TD1">
                        <asp:Label ID="lbl_NFormCharge" runat="server" CssClass="LABEL" Text="NForm Charge :"
                            meta:resourcekey="lbl_NFormChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_NFormCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                            CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_NFormCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_NFormCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_ReBookCharge" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_ReBookCharge" runat="server" CssClass="LABEL" Text="ReBook Charges :"
                            meta:resourcekey="lbl_ReBookChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ReBookGCAmount" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="12" onchange="On_ReBookGCAmount_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_ReBookGCAmount" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr runat="server" id="tr_UnloadingChargeValue">
                    <td class="TD1">
                        <asp:Label ID="lbl_UnloadingCharge" runat="server" CssClass="LABEL" Text="Unloading Charges :"
                            meta:resourcekey="lbl_UnloadingChargeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_UnloadingCharge" onkeypress="return Only_Numbers(this,event);"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_UnloadingCharge_Change();"></asp:TextBox>
                        <asp:HiddenField ID="hdn_UnloadingCharge" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr id="tr_ReBookOctroiAmount" runat="server">
                    <td class="TD1">
                        <asp:Label ID="lbl_ReBookOctroiAmount" runat="server" CssClass="LABEL" Text="ReBook Octroi Amount :"></asp:Label>
                    </td>
                    <td class="TD1">
                        <asp:Label ID="lbl_ReBookOctroiAmountValue" runat="server" CssClass="TEXTBOXNOS"
                            Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_ReBookGC_OctroiAmount" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Updated" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Applicable" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_ReBook_GCOctroiPaidByID" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="2">
                        <asp:HiddenField ID="hdn_BankLR_GC" runat="server" Value="0"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_HamaliPerKg" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_DACCCharges" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="TABLENOBORDER" style="display: none">
    <tr id="tr_PaymentDetails" runat="server">
        <td style="width: 10%" class="TD1">
            <asp:Label ID="lbl_Advance" runat="server" CssClass="LABEL" Text="Advance :"></asp:Label>
        </td>
        <td style="width: 7%">
            <asp:TextBox ID="txt_Advance" onkeypress="return Only_Numbers(this,event);" runat="server"
                CssClass="TEXTBOXNOS" MaxLength="12" Width="95%" onchange="On_AdvanceAmount_Change();Set_Payment_Details();"></asp:TextBox>
            <asp:HiddenField ID="hdn_Advance" runat="server"></asp:HiddenField>
        </td>
        <td style="width: 10%">
            &nbsp;</td>
        <td style="width: 15%">
            &nbsp;</td>
    </tr>
</table>
<table class="TABLENOBORDER" style="width: 100%;">
    <tr>
        <td style="width: 100%">
            <asp:Label runat="server" CssClass="LABELERROR" ID="lbl_Errors"></asp:Label>
            <asp:HiddenField ID="hdn_In_Valid_Credit_Limit_Client_Name" runat="server"></asp:HiddenField>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btn_Save_New" runat="server" CssClass="BUTTON" Text="Save & New"
                AccessKey="N" OnClick="btn_Save_New_Click" OnClientClick="return MyJavascriptFunction();" />
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                AccessKey="P" OnClick="btn_Save_Print_Click" />&nbsp
            <asp:Button ID="btn_Save_Repeat" runat="server" CssClass="BUTTON" Text="Save & Repeat"
                AccessKey="R" OnClick="btn_Save_Repeat_Click" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClientClick="return Allow_To_Exit();" /><%--OnClick="btn_Close_Click"--%>
        </td>
    </tr>
</table>
<table class="TABLENOBORDER" style="width: 100%;">
    <tr style="display: none">
        <td style="width: 100%">
            <asp:HiddenField ID="hdn_CompanyParameter_Standard_BasicFreightUnitId" runat="server"
                Value="0" />
            <asp:HiddenField ID="hdn_CompanyParameter_Standard_FreightRatePer" runat="server"
                Value="0" />
            <asp:CheckBox ID="chk_Is_GCNumberEditable" runat="server" />
            <asp:CheckBox ID="chk_Is_Contract_Required_For_TBB_GC" runat="server" />
            <asp:CheckBox ID="chk_Is_Invoice_Amount_Required" runat="server" />
            <asp:CheckBox ID="chk_Is_Item_Required" runat="server" />
            <asp:CheckBox ID="chk_Is_Validate_Credit_Limit" runat="server" />
            <asp:CheckBox ID="chk_Is_Consignor_Consignee_Details_Shown" runat="server" />
            <asp:CheckBox ID="chk_Is_ST_Abatment_Required" runat="server" />
            <asp:HiddenField ID="hdn_AbatePercentage" runat="server" />
            <asp:HiddenField ID="hdn_ClientCode" runat="server" />
            <asp:HiddenField ID="hdn_TransitDays" runat="server" />
            <asp:HiddenField ID="hdn_Distance_In_Km" runat="server" />
            <asp:HiddenField ID="hdn_Mode" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_MenuItemId" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Previous_SubTotal" runat="server" />
            <asp:HiddenField ID="hdn_Previous_GrandTotal" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cheque_Branch_Ledger_Name" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cheque_Bank_Ledger_Name" runat="server" />
            <asp:HiddenField ID="hdn_Default_Bank_Ledger_Id" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cash_Ledger_Id" runat="server" />
            <asp:HiddenField ID="hdn_Valid_Cheque_Start_Days" runat="server" />
            <asp:HiddenField ID="hdn_Valid_Cheque_End_Days" runat="server" />
            <asp:HiddenField ID="hdn_Remark_Max_Length" runat="server" />
            <asp:HiddenField ID="hdn_LoadingSuperVisor_RequiredFor_BookingType" runat="server" />
            <asp:HiddenField ID="hdn_Container_Details_RequiredFor_BookingType" runat="server" />
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
            <asp:HiddenField ID="hdn_Is_InsuranceDetails_Filled" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_ContainerDetails_Filled" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_Paid_Allowed" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_To_Pay_Allowed" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_FOC_Allowed" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_To_Be_Billed_Allowed" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_No_For_Padd" runat="server" />
            <asp:HiddenField ID="hdn_GCStartNo" runat="server" />
            <asp:HiddenField ID="hdn_GCEndNo" runat="server" />
            <asp:HiddenField ID="hdn_DocumentSeriesAllocationID" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GCDate_ForRectify" runat="server" />
            <asp:HiddenField ID="hdn_Is_Validate_Freight_On_Article" runat="server" />
            <asp:HiddenField ID="hdn_Focus_At_Control" runat="server" />
            <asp:HiddenField ID="hdn_Can_Add_Location" runat="server" />
            <asp:HiddenField ID="hdn_Can_Add_Consignor" runat="server" />
            <asp:HiddenField ID="hdn_Can_Edit_Consignor" runat="server" />
            <asp:HiddenField ID="hdn_Can_View_Consignor" runat="server" />
            <asp:HiddenField ID="hdn_Can_Add_Consignee" runat="server" />
            <asp:HiddenField ID="hdn_Can_Edit_Consignee" runat="server" />
            <asp:HiddenField ID="hdn_Can_View_Consignee" runat="server" />
            <asp:HiddenField ID="hdn_Can_Add_Commodity" runat="server" />
            <asp:HiddenField ID="hdn_Can_Add_Item" runat="server" />
            <asp:HiddenField ID="hdn_Is_Opening_GC" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_Agency_GC" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_VAId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GCId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Rectify_GCId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Actualwtold" runat="server" />
            <asp:HiddenField ID="hdn_Chargewtold" runat="server" />
            <asp:HiddenField ID="hdn_DivisionId" runat="server" />
            <asp:HiddenField ID="hdn_year" runat="server" />
            <asp:HiddenField ID="hdn_month" runat="server" />
            <asp:HiddenField ID="hdn_date" runat="server" />
            <asp:HiddenField ID="hdn_GC_No_Length" runat="server" />
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
            <asp:CheckBox ID="chk_Is_ToPay_Charge_Require" runat="server" />
            <asp:CheckBox ID="chk_Is_FOV_Calculated_As_Per_Standard" runat="server" />
            <asp:CheckBox ID="chk_Is_Auto_Booking_MR_For_Paid_Booking" runat="server" />
            <asp:CheckBox ID="chk_Is_Multiple_Location_Billing_Allowed" runat="server" />
            <asp:CheckBox ID="chk_IsODA" runat="server" />
            <asp:CheckBox ID="chk_IsToPayBkgApplicable" runat="server" />
            <asp:HiddenField runat="server" ID="hdn_TotalInvoiceAmount" />
            <asp:HiddenField runat="server" ID="hdn_Is_Service_Tax_Applicable_For_Commodity" />
            <asp:HiddenField runat="server" ID="hdn_Is_Multiple_Party_Billing_Allowed" />
            <asp:HiddenField runat="server" ID="hdn_ODAChargesUpTo500Kg" Value="0" />
            <asp:HiddenField runat="server" ID="hdn_ODAChargesAbove500Kg" Value="0" />
            <asp:HiddenField runat="server" ID="hdn_TotalArticles" />
            <asp:HiddenField runat="server" ID="hdn_ItemValueForFOV" />
            <asp:HiddenField runat="server" ID="hdn_TotalWeight" />
            <asp:HiddenField runat="server" ID="hdn_TotalRate" />
            <asp:HiddenField runat="server" ID="hdn_TotalAmount" />
            <asp:HiddenField runat="server" ID="hdn_TotalLength" />
            <asp:HiddenField runat="server" ID="hdn_TotalWidth" />
            <asp:HiddenField runat="server" ID="hdn_TotalHeight" />
            <asp:HiddenField runat="server" ID="hdn_FirstCommodityId" />
            <asp:HiddenField runat="server" ID="hdn_FirstItemId" />
            <asp:HiddenField runat="server" ID="hdn_FirstPackingTypeId" />
            <asp:HiddenField runat="server" ID="hdn_Contract_UnitOfFreightId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Contract_FreightBasisId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Contract_FreightSubUnitId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_LoginUserHierarchy"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_TotalBharaiAmt" />
            <asp:HiddenField runat="server" ID="hdn_RateContractId" />
            <asp:HiddenField runat="server" ID="hdn_StationaryCharge_RateContract" />
            <asp:HiddenField runat="server" ID="hdn_HamaliPerKg_RateContract" />
            <asp:HiddenField runat="server" ID="hdn_AOCPercentage_RateContract" />
            <asp:HiddenField runat="server" ID="hdn_FOVPercentage_RateContract" />
            <asp:HiddenField runat="server" ID="hdn_FOVExemptUpTo_RateContract" />
            <ComponentArt:Calendar ID="wuc_ApplicationStartDate" runat="server" CssClass="TEXTBOX"
                BorderWidth="1px" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker"
                PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01"
                Width="95%" />
        </td>
    </tr>
    <tr style="display: none">
        <td>
            <asp:HiddenField runat="server" ID="hdn_RateCard_MinimumChargeWeight"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_BiltiCharges"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_MaxBiltiCharges"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_FOV"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_FOVPercentage"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_FOVRate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Fov_Charge_Discount_Percent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_ToPayCharges"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_DACCCharges"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_LocalCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_HamaliCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_HamaliPerKg"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_HamaliPerArticles"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Hamali_Charge_Discount_Percent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_DDCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_DDCharge_Rate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_DD_Charge_Discount_Percent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_CFTFactor"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Octroi_Form_Charge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Octroi_Service_Charge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_GI_Charges"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Demurrage_Days"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Demurrage_Rate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Invoice_Rate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Invoice_Per_How_Many_Rs"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_Freight_Charge_Discount_Percent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_RateCard_ToPay_Charge_Discount_Percent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_FreightRate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_FreightAmount"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_HamaliCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_DDCharge_Rate"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_DDCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_FOV"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_LengthCharge"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_ServiceTaxPercent"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_Standard_ServiceTaxAmount"></asp:HiddenField>
            <asp:HiddenField ID="hdndocumentID" runat="server" />
            <asp:HiddenField ID="hdnissaveandnewandprint" runat="server" Value="0" />
            <asp:HiddenField ID="hdnprinturl" runat="server" />
            <asp:UpdatePanel runat="server" ID="upnl_consr" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button runat="server" ID="btn_ConsrId" OnClick="btn_ConsrId_Click"></asp:Button>
                    <asp:Button runat="server" ID="btn_ConseeId" OnClick="btn_ConseeId_Click"></asp:Button>
                    <asp:HiddenField runat="server" ID="hdn_EncreptedConsigneeId" Value="0" />
                    <asp:HiddenField runat="server" ID="hdn_EncreptedConsignorId" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_ConsrId" />
                    <asp:AsyncPostBackTrigger ControlID="btn_ConseeId" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">

On_PageUnLoad();
On_View();
DisableToLocation();
function call_CommodityDetails(Article,Weight,Rate,Amount,Is_ServiceTaxForCommodity,ItemValueForFOV,TotalBharaiAmt)
{
    SetCommodityDetails(Article,Weight,Rate,Amount,Is_ServiceTaxForCommodity,ItemValueForFOV,TotalBharaiAmt);
}
function call_InvoiceDetails(InvoiceAmount)
{
    SetInvoiceDetails(InvoiceAmount);
}
function call_OtherCharges(TotalOtherCharges)
{
    SetTotalOtherCharges(TotalOtherCharges);
}
function call_ConsigneeDDAddress(add1,add2)
{
    SetConsigneeDDAddress(add1,add2);
}
function call_InsuranceUpdate(isInsurancefilled)
{
    SetIsInsuranceDetailsfilled(isInsurancefilled);
}
function set_ServiceTaxForBillingParty(Is_ServiceTaxForBillingParty)
{
    SetIsServiceTaxForBillingParty(Is_ServiceTaxForBillingParty);
}
function call_ContainerDetails(isContainerfilled)
{
    SetIsContainerDetailsfilled(isContainerfilled);
}
function Set_Consignor_Consignee_Details(Client_Id,Is_Consignor)
{
    SetClientDetails(Client_Id,Is_Consignor); 
}
function Set_Location_Details(Location_Id,Location_Name,Is_FromLocation)
{
   SetLocationDetails(Location_Id,Location_Name,Is_FromLocation);   
}

function GetBkgBranchIDForCommodity()
{
    return GetBkgBranchID();
}

function GetDlyBranchIDForCommodity()
{
    return GetDlyBranchID();
}

function GetSerLocationIDForCommodity()
{
    return GetSerLocationID();
}

function GetFromSerLocationIDForCommodity()
{
    return GetFromSerLocationID();
}

function GetDly_TypeIDForCommodity()
{ 
    return GetDly_TypeID();
}
function GetBkg_TypeIDForCommodity()
{ 
    return GetBkg_TypeID();
}

function GetRateContract_IDForCommodity()
{ 
    return GetRateContract_ID();
}

function GetPayment_TypeIDForCommodity()
{ 
    return GetPayment_TypeID();
}

Print();
</script>

