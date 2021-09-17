<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODCoverReciept.ascx.cs" Inherits="Operations_POD_WucPODCoverReciept" %>
<%@ Register Src="~/CommonControls/WucPODSentBy.ascx" TagName="WucPODSentBy" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script src="../../Javascript/ddlsearch.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/POD/PODCoverReciept.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/CommonReports.js"></script>
    
<asp:ScriptManager ID="scm_PODCoverReciept" runat="server"></asp:ScriptManager>
<script type="text/javascript" >
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
</script>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="POD Cover Receipt" meta:resourcekey="lbl_HeadResource1" ></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ReceiptNo" runat="server" Text="Receipt No :"></asp:Label></td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Receipt_No" CssClass="LABEL" runat="server" Font-Bold="True"></asp:Label></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ReceiptDate" runat="server" Text="Receipt Date :"></asp:Label></td>
        <td style="width: 29%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="Wuc_PODCoverRecieptDate" runat="server" CellPadding="2"     ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                            PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                            SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="Wuc_PODCoverRecieptDate_SelectionChanged">
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
                        if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= Wuc_PODCoverRecieptDate.ClientObjectId %>_loaded)
                        {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= Wuc_PODCoverRecieptDate.ClientObjectId %>;
                            window.<%= Wuc_PODCoverRecieptDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
            <asp:Label ID="lbl_CoverNo" runat="server" Text="Cover No :"></asp:Label></td>
        <td style="width: 29%">
                  <asp:UpdatePanel ID="Upd_Pnl_ddl_CoverNo" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Wuc_PODCoverRecieptDate" />
                     </Triggers>
                    <ContentTemplate>  
                        <asp:DropDownList ID="ddl_CoverNo"  CssClass="DROPDOWN" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_CoverNo_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CoverDate" runat="server" Text="Cover Date :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="Upd_Pnl_txt_CoverDate" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_CoverNo" />
                 </Triggers>
                <ContentTemplate>   
                    <asp:Label ID="lbl_Cover_Date" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_CoverNoDetails" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_CoverNo" />
                      <asp:AsyncPostBackTrigger ControlID="Wuc_PODCoverRecieptDate" />
                 </Triggers>
                <ContentTemplate>            
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lbl_SentHierachy" runat="server" Text="Sent Hierachy :"></asp:Label></td>
                        <td style="width: 29%" align="left">
                            <asp:Label ID="lbl_Sent_Hierachy" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_HierachyCode" runat="server" />
                        </td>
                        <td style="width: 1%"></td>
                        <td style="width: 20%">
                            <asp:Label ID="lbl_SentLocation" runat="server" Text="Sent Location :"></asp:Label></td>
                        <td style="width: 29%" align="left">
                            <asp:Label ID="lbl_Sent_Location" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_MainId" runat="server" />
                        </td>
                        <td style="width: 1%"></td>
                    </tr>                     
                    <tr>
                        <td colspan="6">
                            <uc1:WucPODSentBy ID="WucPODSentBy1" runat="server" />
                        </td>
                    </tr>
                </table>
                  </ContentTemplate>
            </asp:UpdatePanel>          
        </td>
    </tr>
    <tr>
        <td colspan="6">
         
            <div id="Div_Receipt" class="DIV" style="height: 250px">
                <asp:UpdatePanel ID="Upd_Pnl_dg_PODCoverReciept" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataGrid ID="dg_PODCoverReciept" runat="server" AutoGenerateColumns="False"
                            CellPadding="2" CssClass="GRID" DataKeyField="GC_ID" 
                            Style="border-top-style: none" Width="98%">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Attach">
                                    <%--<HeaderTemplate>
                                      <input id="chkAllItems" onclick="Check_All(this,'WucPODCoverReciept1_dg_PODCoverReciept');"  type="checkbox" visible="false"/>
                                    </HeaderTemplate>--%>
                                    <ItemTemplate> <%--OnClick="Check_Single(this,'WucPODCoverReciept1_dg_PODCoverReciept');" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "IsCheck").ToString()) %>'--%>
                                        <asp:CheckBox ID="Chk_Attach" Enabled="false" Checked="true" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No."></asp:BoundColumn>
                                <asp:BoundColumn DataField="BookingDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Booking Date"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BookingBranch" HeaderText="Booking Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DeliveryBranch" HeaderText="Delivery Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Previous_Transaction_Date" Visible="false" DataFormatString="{0:dd-MM-yyyy}"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:HiddenField id="hdn_TotalNoofGC" runat="server"></asp:HiddenField>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_CoverNo" />
                         <asp:AsyncPostBackTrigger ControlID="Wuc_PODCoverRecieptDate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>                 
        </td>
    </tr>
     <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label></td>
        <td colspan="4">
            <asp:TextBox ID="txt_Remark" runat="server" Height="40px" MaxLength="250" TextMode="MultiLine" CssClass="TEXTBOX"></asp:TextBox></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="text-align: center"  colspan="6" >        
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                Font-Bold="True" Text="Fields With * Mark Are Mandatory"></asp:Label>&nbsp;&nbsp;
        </td>
    </tr>
     <tr>
        <td>&nbsp;</td>
    </tr>
</table>
