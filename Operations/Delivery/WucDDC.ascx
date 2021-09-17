<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDDC.ascx.cs" Inherits="Operations_Delivery_WucDDC" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/DoorDeliverySheet.js"></script>

<asp:ScriptManager ID="scm_dds" runat="server">
</asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
 
 function CalculateBalance()
    { 
        var txt_CashReceived = document.getElementById('<%=txt_CashReceived.ClientID %>');
        var hdn_CashReceived = document.getElementById('<%=hdn_CashReceived.ClientID %>');
        
        var lbl_txt_TotalCash = document.getElementById('<%=lbl_txt_TotalCash.ClientID %>');
        var hdn_TotalCash = document.getElementById('<%=hdn_TotalCash.ClientID %>');
        
        var lbl_txt_BalancedCash = document.getElementById('<%=lbl_txt_BalancedCash.ClientID %>');
        var hdn_BalancedCash = document.getElementById('<%=hdn_BalancedCash.ClientID %>');
        lbl_txt_BalancedCash.value = val(lbl_txt_TotalCash.value) - val(txt_CashReceived.value);
               
        hdn_BalancedCash.value = lbl_txt_BalancedCash.value;
        hdn_CashReceived.value = txt_CashReceived.value;
        
        return;
    }

</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" style="width: 100%">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Door Delivery Confirmation(DDC)"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_DDCNo" runat="server" CssClass="LABEL" Text="DDC No :" meta:resourcekey="lbl_DDCNoResource1"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_DDC_No" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                            meta:resourcekey="lbl_DDC_NoResource1"></asp:Label></td>
                    <td style="width: 9px">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lbl_DDCDate" runat="server" CssClass="LABEL" Text="DDC Date :" meta:resourcekey="lbl_DDCDateResource1"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtp_DDS_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True"
                                        OnSelectionChanged="dtp_DDS_Date_SelectionChanged">
                                    </ComponentArt:Calendar>
                                </td>
                                <td style="height: 24px" runat="server" id="TD_Calender">
                                    <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdn_DDS_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                                <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_DDS_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_DDS_Date.ClientObjectId %>;
                            window.<%= dtp_DDS_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
                    <td class="TD1" style="width: 20%; text-align: right">
                        <asp:Label ID="lbl_DeliveryMode" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DeliveryModeResource1"
                            Text="Delivery Mode :" Width="103px"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddl_DeliveryMode" runat="server" AutoPostBack="True" CssClass="DROPDOWN"
                            meta:resourcekey="ddl_DeliveryModeResource1" OnSelectedIndexChanged="ddl_DeliveryMode_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td class="TD1" style="width: 9px">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_DriverName" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DriverNameResource1"
                                    Text="Driver Name :"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_DriverName" runat="server" meta:resourcekey="txt_DriverNameResource1"
                                    Width="97%"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; text-align: right; vertical-align: top;">
                        <asp:Label ID="lblVehicleNo" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                            Text="Vehicle No :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server"></uc3:WucVehicleSearch>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                    </td>
                    <td class="TD1" style="width: 9px">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lblVendor" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                            Text="Vendor :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbltxt_Vendor" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    Font-Bold="True" Width="97%"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdn_Vendor_ID" runat="server" />
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="Label3" runat="server" CssClass="LABEL" Text="Pre Delivery Sheet No :"
                            meta:resourcekey="Label3Resource1"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_PDSNo" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_PDSNo_SelectedIndexChanged" meta:resourcekey="ddl_PDSNoResource1" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="Label5" runat="server" CssClass="LABEL" Text="PDS Date :" meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_PDSDate" runat="server" Font-Bold="True" CssClass="LABEL" meta:resourcekey="lbl_PDSDateResource1"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="Div_DDS" class="DIV" style="height: 300px;">
                        <asp:DataGrid ID="dg_DDS" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                            Style="border-top-style: none;" Width="97%" OnItemDataBound="dg_DDS_ItemDataBound"
                            meta:resourcekey="dg_DDSResource1">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Att">
                                    <%--<HeaderTemplate>
                   <input id="chkAllItems" type="checkbox"" />
                   </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" AutoPostBack="true" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            runat="server" OnCheckedChanged="Chk_Attach_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <%-- <asp:TemplateColumn HeaderText="Attach">
                               <HeaderTemplate>
                                   <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucDDC1_dg_DDS');" />
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="Chk_Attach" AutoPostBack="true" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' OnClick="Check_Single(this,'WucDDC1_dg_DDS');" runat="server" OnCheckedChanged="Chk_Attach_CheckedChanged"/>
                               </ItemTemplate>  
                        </asp:TemplateColumn>--%>
                                <%--<asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No" ItemStyle-Width="50px">
                </asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_GCNoForPrint" Text='<%# DataBinder.Eval(Container, "DataItem.GC_No_For_Print") %>'
                                            Font-Bold="True" Font-Underline="True" runat="server" CommandName="GCNoForPrint"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GC_ID") %>' />
                                        <asp:HiddenField ID="hdn_GCNoForPrint" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.GC_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ConsigneeName" HeaderText="Consignee Name" ItemStyle-Width="130px">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Mobile No" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMobileNo" Text='<%# DataBinder.Eval(Container.DataItem, "MobileNo") %>'
                                            runat="server" CssClass="TEXTBOX" Width="99%" MaxLength="10"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateColumn>
                                <%--<asp:BoundColumn DataField="Delivery_Location_Name" HeaderText="Delivery Location">
                </asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="Delivery Area">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_DeliveryLocation" runat="server" CssClass="DROPDOWN" Width="98%" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_DeliveryLocation_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Balance_Articles" HeaderText="Bal<br>Qty" ItemStyle-Width="50px">
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="Delivery_Articles" HeaderText="Delivered Articles"></asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="Del<br>Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Delivery_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Articles") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: center" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True" meta:resourcekey="txt_Delivery_ArtResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Pay<br>Type">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Payment_Type" Text='<%# DataBinder.Eval(Container.DataItem, "Payment_Type") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: center" Width="100%" Font-Size="11px"
                                            Font-Names="Verdana" ReadOnly="True" meta:resourcekey="Payment_TypeResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LR<br>Frt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_GC_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: center" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True" meta:resourcekey="Total_GC_AmountResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--Width="95%"--%>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Dly<br>Status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_DeliveryStatus" runat="server" CssClass="DROPDOWN" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_DeliveryStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdn_DeliveryStatus" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Committed_Del_Date" HeaderText="Expected Delivery Date"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Actual_Wt" HeaderText="Delivery_Actual_Wt">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Door Delivery Date">
                                    <ItemTemplate>
                                        <ComponentArt:Calendar ID="dtp_DD_Date" runat="server" CellPadding="2" ControlType="Picker"
                                            PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                                            SelectedDate="2008-10-20">
                                        </ComponentArt:Calendar>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Door Delivery Time">
                                    <ItemTemplate>
                                        <uc1:TimePicker ID="TimePicker1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Actual_Status" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Status") %>'
                                            CssClass="TEXTBOX" Font-Bold="True" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: right" Width="90%" Font-Size="11px"
                                            Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_Status_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Status_ID") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdn_Status" Value='<%# DataBinder.Eval(Container.DataItem, "Status") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdn_Actual_Status_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Actual_Status_ID") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="UnDelivered Reason">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_UnDel_Reason" runat="server" CssClass="DROPDOWN">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Delivery Taken By" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Delivery_TakenBy" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Taken_By") %>'
                                            runat="server" CssClass="TEXTBOX" Width="80%" MaxLength="25"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Dly<br>Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_details" Text="Details" Font-Bold="True" Font-Underline="True"
                                            runat="server" CommandName="add" Width="50px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                <%-- <asp:BoundColumn DataField="Local_Tempo_Freight" HeaderText="Local Tempo Freight">
                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Size" HeaderText="Size"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Tempo<br>Frt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Local_Tempo_Freight" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                            meta:resourcekey="txt_Local_Tempo_FreightResource1" Text='<%# DataBinder.Eval(Container.DataItem, "Local_Tempo_Freight") %>'
                                            OnTextChanged="txt_Local_Tempo_Freight_TextChanged" AutoPostBack="true"></asp:TextBox> 
                                        <asp:HiddenField ID="hdn_Local_Tempo_Freight" Value='<%# DataBinder.Eval(Container.DataItem, "Local_Tempo_Freight") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <%-- Width="90%" --%>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Bonus">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Bonus" runat="server" CssClass="TEXTBOX" MaxLength="10" ReadOnly="false"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "Bonus") %>'
                                            OnTextChanged="txt_Local_Tempo_Bonus_TextChanged" AutoPostBack="true" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: center" ></asp:TextBox> 
                                        <asp:HiddenField ID="hdn_Bonus" Value='<%# DataBinder.Eval(Container.DataItem, "Bonus") %>'
                                            runat="server" />
                                            <asp:HiddenField ID="hdnIsConsignorNew" Value='<%# DataBinder.Eval(Container.DataItem, "IsConsignorNew") %>' runat="server" />
                                            <asp:HiddenField ID="hdnIsConsigneeNew" Value='<%# DataBinder.Eval(Container.DataItem, "IsConsigneeNew") %>' runat="server" />
                                            <asp:HiddenField ID="hdn_ActualBonus" Value='<%# DataBinder.Eval(Container.DataItem, "Bonus") %>' runat="server" />
                                    </ItemTemplate>
                                    <%-- Width="90%" --%>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>


                                <asp:TemplateColumn HeaderText="Add.Tempo<br>Frt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_AddTempoFrt" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                            meta:resourcekey="txt_AddTempoFrtResource1" Text='<%# DataBinder.Eval(Container.DataItem, "AddTempoFrt") %>'
                                            OnTextChanged="txt_Local_Tempo_Freight_TextChanged" AutoPostBack="true" ></asp:TextBox> 
                                        <asp:HiddenField ID="hdn_AddTempoFrt" Value='<%# DataBinder.Eval(Container.DataItem, "AddTempoFrt") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <%-- Width="90%" --%>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="GC Id">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_GC_Id" Value='<%# DataBinder.Eval(Container.DataItem, "GC_Id") %>'
                                            runat="server" />
                                        <asp:Label ID="lbl_GC_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Article Id">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_Article_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Article_Id") %>'
                                            runat="server" />
                                        <asp:Label ID="lbl_Article_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Article_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Delivered Actual Wt.">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Delivery_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Actual_Wt") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: right" Width="90%" Font-Size="11px"
                                            Font-Names="Verdana" ReadOnly="True" meta:resourcekey="txt_Delivery_WtResource1"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="BalanceToBePaid">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_BalanceToBePaid" Text='<%# DataBinder.Eval(Container.DataItem, "BalanceToBePaid") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: right" Width="90%" Font-Size="11px"
                                            Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDS" EventName="ItemCommand" /> 
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tr>
                            <td class="TD1" style="width: 10%">
                                <%--<asp:Label ID="Label1" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True"/>--%>
                            </td>
                            <td style="width: 10%">
                                <%--
                        <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"/>--%>
                                <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                                <asp:HiddenField ID="hdnforselectall" runat="server" />
                                <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_totalDelArtResource1" Visible="False" /></td>
                            <td class="TD1" style="width: 13%">
                                &nbsp;</td>
                            <td align="right" style="width: 10%">
                            </td>
                            <td align="left" style="width: 10%">
                                &nbsp;
                            </td>
                            <td align="center" style="width: 11%">
                            </td>
                            <td align="center" style="width: 11%">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_tolal1" runat="server" Text="Total Dly Art:" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_tolal1Resource1" Width="100px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_totalDelArt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_totalDelArtResource1" /></td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<asp:HiddenField ID="hdn_totalDelArt" Value="0" runat="server" />
                            </td>
                            <td align="left" style="width: 11%">
                                &nbsp;
                                <asp:Label ID="lbl_totalDelWt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_totalDelWtResource1" Visible="False" /><asp:HiddenField ID="hdn_totalDelWt"
                                        Value="0" runat="server" />
                            </td>
                            <td align="center" style="width: 11%; text-align: right;">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label1" runat="server" Text="Tempo Frt:" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_tolal1Resource1" Width="123px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_Local_TotalTempo_Freight" runat="server" Text="0" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_Local_TotalTempo_FreightResource1" /></td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<asp:HiddenField ID="hdn_Local_TotalTempo_Freight" Value="0" runat="server" />
                            </td>
                            
                             <td align="center" style="width: 11%; text-align: right;">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label4" runat="server" Text="Bonus:" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_TolalBonusResource1" Width="123px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_TotalBonus" runat="server" Text="0" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_TotalBonusResource1" /></td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<asp:HiddenField ID="hdn_TotalBonus" Value="0" runat="server" />
                            </td>


                             <td align="center" style="width: 11%; text-align: right;">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label6" runat="server" Text="AddTempoFrt:" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_TolalAddTempoFrtResource1" Width="123px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_TotalAddTempoFrt" runat="server" Text="0" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_TotalAddTemoFrtResource1" /></td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<asp:HiddenField ID="hdn_TotalAddTempoFrt" Value="0" runat="server" />
                            </td> 
                                                        
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table width="98%">
                <tr>
                    <td class="TD1" style="width: 5%">
                        <asp:Label ID="lbl_GodownSupervisor" runat="server" meta:resourcekey="lbl_GodownSupervisor1"
                            Text="Godown Supervisor :" CssClass="LABEL"></asp:Label></td>
                    <td class="TD1" style="width: 16%">
                        <cc1:DDLSearch ID="ddl_Supervisor" runat="server" AllowNewText="True" IsCallBack="True"
                            CallBackAfter="2" Text="" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" />
                        <asp:HiddenField ID="hdn_Supervisor_Id" runat="server" />
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_ChequeAmt" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ChequeAmtResource1"
                            Text="Cheque Amt:" Width="84px"></asp:Label></td>
                    <td style="width: 10%">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="lbl_txt_ChequeAmt" runat="server" CssClass="LABEL" meta:resourcekey="lbl_txt_ChequeAmtResource1"
                                    ReadOnly="True" Width="70px"></asp:TextBox><br />
                                <asp:TextBox ID="hdn_ChequeAmt" Text="0" runat="server" Visible="False" Width="70px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                                <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 9%">
                        <asp:Label ID="lbl_Credit" runat="server" CssClass="LABEL" meta:resourcekey="lbl_CreditResource1"
                            Text="Credit Amt:" Width="78px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="lbl_txt_CreditAmt" runat="server" CssClass="LABEL" meta:resourcekey="lbl_txt_CreditAmtResource1"
                                    ReadOnly="True" Width="70px"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="hdn_CreditAmt" Text="0" runat="server" Visible="False" Width="70px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                                <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 9%">
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 5%">
                    </td>
                    <td class="TD1" style="width: 16%">
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_TotalCash" runat="server" CssClass="LABEL" meta:resourcekey="lbl_TotalCashResource1"
                            Text="Total Cash:"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="hdn_TotalCash" runat="server" CssClass="HIDEGRIDCOL" Text="0" Visible="False"
                                    Width="70px" />
                                <br />
                                <asp:TextBox ID="lbl_txt_TotalCash" runat="server" CssClass="LABEL" meta:resourcekey="lbl_txtTotalCashResource1"
                                    ReadOnly="true" Width="70px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                                <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 9%">
                        <asp:Label ID="lbl_CashReceived" runat="server" CssClass="LABEL" meta:resourcekey="lbl_CashReceivedResource1"
                            Text="Cash Received:" Width="98px"></asp:Label></td>
                    <td style="width: 10%">
                        <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
              <ContentTemplate>--%>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_CashReceived" runat="server" onblur="CalculateBalance();return valid(this)"
                                    onkeypress="return Only_Numbers(this,event);" MaxLength="9" Text="0" Width="70px"></asp:TextBox>
                                <asp:HiddenField ID="hdn_CashReceived" Value="0" runat="server" />
                                <asp:HiddenField ID="hdn_Total_CouponBalance" Value="0" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--</ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
              </Triggers>
            </asp:UpdatePanel>--%>
                    </td>
                    <td style="width: 9%">
                        <asp:Label ID="lbl_BalancedCash" runat="server" CssClass="LABEL" meta:resourcekey="lbl_BalancedCashResource1"
                            Text="Balanced Cash:" Width="96px"></asp:Label></td>
                    <td style="width: 10%">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:TextBox ID="lbl_txt_BalancedCash" runat="server" CssClass="LABEL" ReadOnly="true"
                                    Width="70px"></asp:TextBox>
                                <asp:HiddenField ID="hdn_BalancedCash" Value="0" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 5%">
                        <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label></td>
                    <td class="TD1" style="width: 16%">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine"
                            MaxLength="250" meta:resourcekey="txt_RemarksResource1"></asp:TextBox></td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_MobilePay" runat="server" CssClass="LABEL" meta:resourcekey="lbl_MobilePayResource1"
                            Text="Mobile Pay:" Width="84px"></asp:Label></td>
                    <td style="width: 10%">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="lbl_txt_MobilePay" runat="server" CssClass="LABEL" meta:resourcekey="lbl_txt_MobilePayResource1"
                                ReadOnly="True" Width="70px"></asp:TextBox><br />
                            <asp:TextBox ID="hdn_MobilePay" Text="0" runat="server" Visible="False" Width="70px" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                            <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                            <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 9%">
                        <asp:Label ID="lbl_SwipeCard" runat="server" CssClass="LABEL" meta:resourcekey="lbl_SwipeCardResource1"
                            Text="Swipe Card:" Width="84px"></asp:Label></td>
                    <td style="width: 10%">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="lbl_txt_SwipeCard" runat="server" CssClass="LABEL" meta:resourcekey="lbl_txt_SwipeCardResource1"
                                ReadOnly="True" Width="70px"></asp:TextBox><br />
                            <asp:TextBox ID="hdn_SwipeCard" Text="0" runat="server" Visible="False" Width="70px" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                            <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                            <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 9%">
                        <asp:Label ID="Label7" runat="server" CssClass="LABEL" Text="Pending Freight:" Width="96px"></asp:Label></td>
                    <td style="width: 10%">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_PendingFreight" runat="server" CssClass="LABEL" ReadOnly="true"
                                    Width="70px"></asp:TextBox>
                                <asp:HiddenField ID="hdn_PendingFreight" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_PDSNo" />
                            <asp:AsyncPostBackTrigger ControlID="dtp_DDS_Date" />
                            <asp:AsyncPostBackTrigger ControlID="dg_DDS" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <%--<tr>
          <td class="TD1" style="width: 20%">
            &nbsp;</td>
          <td colspan="4">
            &nbsp;</td>
          <td style="width: 1%">
          </td>
        </tr>--%>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
            <asp:HiddenField ID="hdn_Mode" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" OnClick="btn_Save_Exit_Click" meta:resourcekey="btn_Save_ExitResource1" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" meta:resourcekey="btn_CloseResource1" />
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                AccessKey="p" OnClick="btn_Save_Print_Click" />&nbsp
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
                OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
         <asp:Button ID="btn_CouponUpdate" runat="server" Text="UpdateCoupon" OnClick="btn_CouponUpdate_Click"  style="display:none" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="fields with * mark are mandatory"
                meta:resourcekey="Label2Resource1"></asp:Label>
        </td>
    </tr>
</table>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
            z-index: 100">
            <span id="ajaxloading">
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                    </tr>
                    <tr>
                        <td align="center">
                            Wait! Action in Progress...</td>
                    </tr>
                </table>
            </span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script language ="javascript" type ="text/javascript">
//setFocusonPageLoad();        

function update_CouponDetails()
{
    document.getElementById('<%=btn_CouponUpdate.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_CouponUpdate.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_CouponUpdate.ClientID%>').click();
}
</script>
