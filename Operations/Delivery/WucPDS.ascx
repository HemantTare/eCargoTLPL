<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPDS.ascx.cs" Inherits="Operations_Delivery_WucPDS" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/PreDeliverySheet.js"></script>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript">
    function get_button_nullsession_clientid()
    {
        btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
    }
    
    function Open_Details_Window(Path)
    {
        window.open(Path,'GCDlyReceipt','width=950,height=900,top=200,left=50,menubar=no,resizable=no,scrollbars=no')
        return false;
    }
    
</script>

<asp:ScriptManager ID="scm_pds" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" style="width: 100%">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pre Door Delivery Sheet"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right;">
                        <asp:Label ID="lbl_PDSNo" runat="server" CssClass="LABEL" Text="PDS No :" meta:resourcekey="lbl_PDSNoResource1"
                            Width="77px"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_PDS_No" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                            meta:resourcekey="lbl_PDS_NoResource1"></asp:Label></td>
                    <td style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        &nbsp;<asp:Label ID="lbl_PDSDate" runat="server" CssClass="LABEL" Text="PDS Date :"
                            meta:resourcekey="lbl_PDSDateResource1"></asp:Label></td>
                    <td style="width: 29%; text-align: left;">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                                    <ComponentArt:Calendar ID="dtp_PDS_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" OnSelectionChanged="dtp_PDS_Date_SelectionChanged" AutoPostBackOnSelectionChanged="True">
                                    </ComponentArt:Calendar>
                                </td>
                                <td runat="server" id="TD_Calender">
                                    <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right;">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 29%; text-align: left;">

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_PDS_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_PDS_Date.ClientObjectId %>;
                            window.<%= dtp_PDS_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                        <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right;">
                        <asp:Label ID="lbl_DeliveryMode" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DeliveryModeResource1"
                            Text="Delivery Mode :" Width="103px"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddl_DeliveryMode" CssClass="DROPDOWN" runat="server" meta:resourcekey="ddl_DeliveryModeResource1"
                            OnSelectedIndexChanged="ddl_DeliveryMode_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList></td>
                    <td class="TD1">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;" align="right">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_DriverName" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DriverNameResource1"
                                    Text="Driver Name :"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 29%; text-align: left;">
                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
                &nbsp;
              </ContentTemplate>
            </asp:UpdatePanel>--%>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_DriverName" runat="server" meta:resourcekey="txt_DriverNameResource1"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right; vertical-align: top">
                        <asp:Label ID="lblVehicleNo" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                            Text="Vehicle No :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_PDS_Date" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                    </td>
                    <td class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lbl_MobileNo" runat="server" Text="Mobile No:"></asp:Label></td>
                    <td style="width: 29%; text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_MobileNo" runat="server" meta:resourcekey="txt_MobileNoResource1"
                                    MaxLength="10" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; height: 26px; text-align: right;">
                        <asp:Label ID="lblVendor" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                            Text="Vendor :"></asp:Label></td>
                    <td style="width: 29%; height: 26px; text-align: left;" align="left" rowspan="">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbltxt_Vendor" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    Font-Bold="True" Width="70%"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveryMode" />
                                <asp:AsyncPostBackTrigger ControlID="dtp_PDS_Date" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdn_Vendor_ID" runat="server" />
                    </td>
                    <td class="TD1" style="height: 26px">
                    </td>
                    <td class="TD1" style="width: 20%; height: 26px;">
                        <asp:HiddenField ID="hdn_Ledger_ID" runat="server" />
                        <asp:HiddenField ID="hdn_Credit_Limit" runat="server" />
                    </td>
                    <td style="width: 29%; text-align: right; height: 26px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnAddLR" runat="server" CssClass="BUTTON" Text="Add LR" OnClientClick="return AddLR()" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="5" style="text-align: left">
                        <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="5"  style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_eWayBills" runat="server" Text="lbl_eWayBills" Font-Bold="True" Font-Size="Large" ForeColor="DarkRed" Visible="False"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Div_PDS" class="DIV" style="height: 250px">
                        <asp:DataGrid ID="dg_PDS" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                            Style="border-top-style: none" Width="98%" OnItemDataBound="dg_PDS_ItemDataBound"
                            meta:resourcekey="dg_PDSResource1">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Attach">
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucPDS1_dg_PDS');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            OnClick="Check_Single(this,'WucPDS1_dg_PDS','1');" runat="server" meta:resourcekey="Chk_AttachResource1" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Committed_Del_Date" HeaderText="Expected Delivery Date"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch">
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="250px" />
                                    <ItemStyle Width="250px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DeliveryAreaName" HeaderText="Delivery Area" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="280px" />
                                    <ItemStyle Width="280px" />
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="eWayBillNo" HeaderText="eWayBillNo" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="Packing_Type" HeaderText="Packing Type">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Articles" HeaderText="Booking Articles">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Actual_Wt" HeaderText="Booking Actual Wt">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Bal Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Bal_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Font-Size="11px" Width="50px" Font-Names="Verdana"
                                            ReadOnly="True" meta:resourcekey="txt_Bal_ArtResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Balance Actual Wt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Bal_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Actual_Wt") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                                            meta:resourcekey="txt_Bal_WtResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Del Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Delivery_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Articles") %>'
                                            runat="server" CssClass="TEXTBOXNOS" ReadOnly="True" Width="50px" onkeypress="return Only_Integers(this,event)"
                                            Style="text-align: right" MaxLength="7" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Delivery Actual Wt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Delivery_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Actual_Wt") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            Style="text-align: right" Width="80%" MaxLength="7" meta:resourcekey="txt_Delivery_WtResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Payment_Type" HeaderText="Pay Type">
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="10px" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="LR Frt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_GC_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            Style="text-align: right" Enabled="false" Width="80%" MaxLength="7" meta:resourcekey="txt_Total_GC_AmountResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DeliveryAreaID" HeaderText="DeliveryAreaID">
                                    <ItemStyle CssClass="HIDEGRIDCOL" />
                                    <HeaderStyle CssClass="HIDEGRIDCOL" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="dtp_PDS_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="font-weight: bold; font-size: 11px; width: 50%; font-family: Verdana;
                        text-align: left">
                        <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Width="50px" CssClass="NOTUPDATEDLBL"></asp:Label>&nbsp; Octroi Not Updated</td>
                    <td style="width: 50%; text-align: right">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tr>
                            <td class="TD1" style="width: 15%">
                                <asp:Label ID="Label1" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="Label1Resource1" />
                            </td>
                            <td style="width: 37%;">
                                <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_Total_GCResource1" /><asp:HiddenField ID="hdn_Total_GC" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_tolal1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_tolal1Resource1" />
                            </td>
                            <td class="TD1" style="width: 8%; text-align: left" align="left">
                                <asp:Label ID="lbl_BalArt" runat="server" Text="0" CssClass="HIDEGRIDCOL" Font-Bold="True"
                                    meta:resourcekey="lbl_BalArtResource1" /><asp:HiddenField ID="hdn_BalArt" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_totalDelArt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_totalDelArtResource1" /><asp:HiddenField ID="hdn_totalDelArt"
                                        runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_BalActWt" runat="server" Text="0" CssClass="HIDEGRIDCOL" Font-Bold="True"
                                    meta:resourcekey="lbl_BalActWtResource1" />
                                <asp:HiddenField ID="hdn_BalActWt" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_totalDelWt" runat="server" Text="0" CssClass="HIDEGRIDCOL" Font-Bold="True"
                                    meta:resourcekey="lbl_totalDelWtResource1" />
                                <asp:HiddenField ID="hdn_totalDelWt" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%" align="center">
                                <asp:Label ID="lbl_totalGCAmt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_totalGCAmtResource1" />
                                <asp:HiddenField ID="hdn_totalGCAmt" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="dtp_PDS_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 20%; height: 3px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 3px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 3px;">
                        *</td>
                    <td class="TD1" style="width: 20%; height: 3px;">
                        <asp:Label ID="lbl_DeliveryModeDesc" runat="server" CssClass="LABEL" Text="Delivery Mode Description :"
                            meta:resourcekey="lbl_DeliveryModeDescResource1" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 29%; height: 3px;">
                        <asp:TextBox ID="txt_DeliveryModeDesc" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_DeliveryModeDescResource1"
                            MaxLength="50" Width="230" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lbl_GodownSupervisor" runat="server" CssClass="LABEL" Text="Godown Supervisor :"
                            meta:resourcekey="lbl_GodownSupervisorResource1"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddl_GodownSupervisor" runat="server" AllowNewText="True" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" CallBackAfter="2"
                            Text="" InjectJSFunction="" PostBack="False" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 20%" class="HIDEGRIDCOL">
                        <asp:Label ID="lbl_AssociateName" runat="server" CssClass="LABEL" Text="Associate Name :"
                            meta:resourcekey="lbl_AssociateNameResource1" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 29%;" class="HIDEGRIDCOL">
                        <asp:DropDownList ID="ddl_Associate_Name" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Associate_NameResource1"
                            Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnfrtcalc" runat="server" CssClass="BUTTON" Text="Calculate Tempo Freight"
                            OnClientClick="return FreightCalculator()" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" Width="660" CssClass="TEXTBOX"
                            TextMode="MultiLine" MaxLength="10" meta:resourcekey="txt_RemarksResource1"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>&nbsp;
            <asp:HiddenField ID="hdn_LoginBranch_Id" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
                meta:resourcekey="btn_SaveResource1" OnClick="btn_Save_Click" />&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" meta:resourcekey="btn_Save_ExitResource1" OnClick="btn_Save_Exit_Click" />&nbsp
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                AccessKey="p" OnClick="btn_Save_Print_Click" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                meta:resourcekey="btn_CloseResource1" OnClick="btn_Close_Click" />
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
                OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are Mandatory"
                EnableViewState="False"></asp:Label>
            <asp:HiddenField ID="hdnKeyID" runat="server" />
            &nbsp;
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
