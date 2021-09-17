<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODDeliverySheet.ascx.cs" Inherits="Operations_POD_WucPODDeliverySheet" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucPODSentBy.ascx" TagName="WucPODSentBy" TagPrefix="uc1" %>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script src="../../Javascript/ddlsearch.js" type="text/javascript"></script>
<script src="../../Javascript/Operations/POD/PODDeliverySheet.js" type="text/javascript"></script>
<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<asp:ScriptManager ID="scm_PODDeliverySheet" runat="server"></asp:ScriptManager>

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
</script>
<table class="TABLE" width="100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="POD Delivery Sheet" meta:resourcekey="lbl_HeadResource1" ></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td style="width: 100%" colspan="6"></td>       
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_PODDeliverySheetNo1" runat="server" Text="POD Delivery Sheet No :"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_PODDeliverySheetNo" CssClass="LABEL" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Date" runat="server" Text="Delivery Date :"></asp:Label>
        </td>
        <td style="width: 29%">
        <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="Wuc_PODDeliverySheetDate" runat="server" CellPadding="2"     ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                            PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                            SelectedDate="2008-11-21" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="Wuc_PODDeliverySheetDate_SelectionChanged">
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
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td style="width: 50%" colspan="3"></td>       
        <td style="width: 20%"></td>
        <td style="width: 29%">
        <componentart:calendar id="Calendar" runat="server" allowmonthselection="False"
                allowmultipleselection="False" allowweekselection="False" calendarcssclass="CALENDER"
                calendartitlecssclass="TITLE" clientsideonselectionchanged="Calendar_OnSelectionChanged"
                controltype="Calendar" daycssclass="DAY" dayheadercssclass="DAYHEADER" dayhovercssclass="DAYHOVER"
                daynameformat="FirstTwoLetters" imagesbaseurl="../../images/" monthcssclass="MONTH"
                nextimageurl="cal_nextMonth.gif" nextprevcssclass="NEXTPREV" othermonthdaycssclass="OTHERMONTHDAY"
                popup="Custom" previmageurl="cal_prevMonth.gif" selecteddaycssclass="SELECTEDDAY"
                swapduration="300" ></componentart:calendar>

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= Wuc_PODDeliverySheetDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= Wuc_PODDeliverySheetDate.ClientObjectId %>;
                            window.<%= Wuc_PODDeliverySheetDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <asp:Label ID="lbl_PODDeliveredTo" CssClass="LABEL" Text="Delivered To :" runat="server" ></asp:Label> 
        </td>
        <td style="width: 29%;">
        <asp:TextBox  ID="txt_PODDeliveredTo" runat="server" CssClass="TEXTBOX"  MaxLength="50" />
        </td>
        <td style="width: 1%"></td>
        <td style="width:50%" colspan="3"></td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            <uc1:WucPODSentBy ID="WucPODSentBy1" runat="server"></uc1:WucPODSentBy>
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 100%" colspan="6"></td>       
    </tr>
    <tr>
        <td class="TD1" colspan="6">
         <div id="Div_Receipt" class="DIV" style="height: 300px">
        <asp:UpdatePanel ID="Upd_Pnl_dg_PODDeliverySheet" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <asp:DataGrid ID="dg_PODDeliverySheet" runat="server" AutoGenerateColumns="False"
                    CellPadding="2" CssClass="GRID" DataKeyField="GC_ID" 
                    Style="border-top-style: none" Width="98%" meta:resourcekey="dg_PODDeliverySheetResource1">
                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Attach">
                            <HeaderTemplate>
                                <input id="chkAllItems" onclick="Check_All(this,'WucPODDeliverySheet1_dg_PODDeliverySheet');"
                                    type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Attach" runat="server" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "IsCheck").ToString()) %>'
                                    meta:resourcekey="Chk_AttachResource1"  />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Booking Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingBranch" HeaderText="Booking Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DeliveryBranch" HeaderText="Delivery Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                    </Columns>
                  </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="Wuc_PODDeliverySheetDate" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        </td>
    </tr>
   <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
   <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" Text="Remarks :"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txt_Remark" runat="server" TextMode="MultiLine" Height="40px" CssClass="TEXTBOX" MaxLength="250"></asp:TextBox></td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="text-align: center"  colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                    Font-Bold="True" Text="Fields With * Mark Are Mandatory"></asp:Label>    
        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
</table>
