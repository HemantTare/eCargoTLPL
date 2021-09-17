<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucWrongDelivery.ascx.cs"
    Inherits="Operations_Inward_Updates_WucWrongDelivery" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

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

<table class="TABLE" width="100%">
    <tr>
        <td class="TDGRADIENT" colspan="7">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Wrong Delivery"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_GCNo" runat="server" CssClass="LABEL" Text="GC No. :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_GCNo" runat="server" CssClass="TEXTBOX" Width="50%"></asp:TextBox>&nbsp;
            <asp:Button ID="btn_GetDetails" runat="server" CssClass="BUTTON" Width="30%" Text="Get Details"
                OnClick="btn_GetDetails_Click" />
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 29%">
            &nbsp;</td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BookingBranch" runat="server" CssClass="LABEL" Text="Booking Branch :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="txt_BookingBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    <asp:HiddenField ID="hdfn_Booking_Branch_ID" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_DeliveryBranch" runat="server" CssClass="LABEL" Text="Delivery Branch :"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="txt_DeliveryBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    <asp:HiddenField ID="hdfn_Delivery_Branch_ID" runat="server" />
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
            <asp:Label ID="lbl_InformedBy" runat="server" CssClass="LABEL" Text="Informed By :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_InformedBy" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_InformedContactNo" runat="server" CssClass="LABEL" Text="Contact No :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_InformedContactNo" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CollectedBy" runat="server" CssClass="LABEL" Text="Collected By :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_CollectedBy" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CollectedContactNo" runat="server" CssClass="LABEL" Text="Contact No :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_CollectedContactNo" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lblVehicleNo" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                Text="Vehicle No :"></asp:Label></td>
        <td style="width: 29%">
            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server"></uc3:WucVehicleSearch>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 50%" colspan="3">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Received_Condition" runat="server" CssClass="LABEL" Text="Parcel Condition :"
                Width="103px"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Received_Condition" runat="server" AutoPostBack="True"
                        CssClass="DROPDOWN">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 29%">
            &nbsp;</td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Description" runat="server" CssClass="LABEL" Text="Description :"></asp:Label></td>
        <td class="TD1" colspan="3">
            <asp:TextBox ID="txt_Description" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                Width="98%"></asp:TextBox>
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblIsCheque" runat="server" CssClass="LABEL" Text="Is Cheque :"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:CheckBox ID="chkIsCheque" runat="server" AutoPostBack="true" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblChequeNo" runat="server" CssClass="LABEL" Text="Cheque No :"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                    <asp:AsyncPostBackTrigger ControlID="chkIsCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="8"
                        onkeyPress="return Only_Integers(this,event);" Width="84px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                    <asp:AsyncPostBackTrigger ControlID="chkIsCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date :"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                    <asp:AsyncPostBackTrigger ControlID="chkIsCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp(<%= CalChequeDate.ClientObjectId %>)">
                                <ComponentArt:Calendar ID="dtpChequeDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                    PickerFormat="Custom">
                                </ComponentArt:Calendar>
                            </td>
                            <td id="ChequeDateButton" runat="server">
                                <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= CalChequeDate.ClientObjectId %>)"
                                    onmouseup="Button_OnMouseUp(<%= CalChequeDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                    width="25" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_GetDetails" />
                    <asp:AsyncPostBackTrigger ControlID="chkIsCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
            <ComponentArt:Calendar ID="CalChequeDate" runat="server" AllowMonthSelection="False"
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
                        function ComponentArt_<%= CalChequeDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalChequeDate.ClientObjectId %>_loaded && window.<%= dtpChequeDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalChequeDate.ClientObjectId %>.AssociatedPicker = <%= dtpChequeDate.ClientObjectId %>;
                            window.<%= dtpChequeDate.ClientObjectId %>.AssociatedCalendar = <%= CalChequeDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalChequeDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalChequeDate.ClientObjectId %>_Associate();
            </script>

        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Button ID="btn_Save_Exit" runat="server" AccessKey="S" CssClass="BUTTON" meta:resourcekey="btn_Save_ExitResource1"
                OnClick="btn_Save_Exit_Click" Text="Save " /></td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
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
                    <asp:HiddenField ID="hdn_GC_ID" runat="server" />
                    <asp:HiddenField ID="hdn_GCCaption" runat="server" />
                    &nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
