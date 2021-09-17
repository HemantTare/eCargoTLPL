<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMenifest.ascx.cs" Inherits="Operations_Outward_WucMenifest" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Outward/Menifest.js"></script>
<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>
<asp:ScriptManager ID="scm_memo" runat="server"></asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="MANIFEST"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_menifest_no" runat="server" CssClass="LABEL" Text="Manifest No.:"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:Label ID="lbl_MenifestNo" runat="server" Font-Bold="True"></asp:Label></td>
        <td class="TD1" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="Manifest Date:"></asp:Label>
        </td>
        <td style="width: 29%;" class="TDMANDATORY">
            <table border="0" cellpadding="0" width="100%">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px;
                        width: 40%">
                        <ComponentArt:Calendar ID="Memo_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="Memo_Date_SelectionChanged">
                        </ComponentArt:Calendar>
                    </td>
                    <td style="height: 24px; width: 15%" runat="server" id="td_cal">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                    <td class="TD1" style="width: 45%">
                    <asp:Button ID="btn_SendSMS" runat="server" CssClass="BUTTON" Text="Send SMS" Visible="false" OnClick="btn_SendSMS_Click" /></td>
                </tr>
            </table>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
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
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= Memo_Date.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= Memo_Date.ClientObjectId %>;
                        window.<%= Memo_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_menifest_type" runat="server" CssClass="LABEL" Text="Manifest Type:"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_MenifestType" CssClass="DROPDOWN" runat="server" onchange="enabledisable_memotobranch()"
                AutoPostBack="True" OnSelectedIndexChanged="ddl_MenifestType_SelectedIndexChanged" /></td>
        <td class="TDMANDATORY">
            *</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_menifest_to" runat="server" CssClass="LABEL" Text="Manifest To:"></asp:Label>
        </td>
        <td style="width: 29%">
            <table width="100%">
                <tr id="tr_ddl_memoto">
                    <td style="width: 99%">
                        <%-- <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> --%>
                       <%-- <cc1:DDLSearch ID="ddl_MenifestTo" runat="server" AllowNewText="True" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetMemoToBranch" CallBackAfter="2"
                            Text="" PostBack="true" OnTxtChange="ddl_MenifestTo_TxtChange" />--%>
                            
                        <asp:TextBox ID="txtSearch_MenifestTo" autocomplete="off" runat="server" CssClass="TEXTBOX"
                            onblur="On_txtLostFocus('WucMenifest1_txtSearch_MenifestTo','WucMenifest1_lst_MenifestTo','WucMenifest1_hdn_MenifestToId')" onkeyup="Search_txtSearch(event,this,'WucMenifest1_lst_MenifestTo','MemoToBranch',2);"
                            onkeydown="return on_keydown(event,'WucMenifest1_txtSearch_MenifestTo','WucMenifest1_lst_MenifestTo');" onfocus="On_Focus('WucMenifest1_txtSearch_MenifestTo','WucMenifest1_lst_MenifestTo');"
                            MaxLength="50" EnableViewState="False"></asp:TextBox>
                            <asp:ListBox ID="lst_MenifestTo" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('WucMenifest1_txtSearch_MenifestTo')"
                            runat="server" TabIndex="20"></asp:ListBox>
                            <asp:HiddenField ID="hdn_MenifestToId" Value="0" runat="server" />  
                        <%--  </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_MenifestTo" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="tr_txt_memoto">
                    <td style="width: 99%">
                        <asp:TextBox ID="txt_MenifestTo" runat="server" onblur="Uppercase(this)" CssClass="TEXTBOX"
                            Width="85%" BorderWidth="1px" MaxLength="50" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
            </table>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr id="trVehicleCategory" style="display: none">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_vehicle_cotegory" runat="server" CssClass="LABEL" Text="Vehicle Category:"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_VehicleCotegory" AutoPostBack="true" runat="server" CssClass="DROPDOWN"
                OnSelectedIndexChanged="ddl_VehicleCotegory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TDMANDATORY" style="width: 50%" colspan="3">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_vehicle_no" runat="server" CssClass="LABEL" Text="Vehicle No:"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                    <asp:HiddenField ID="hdn_Number_Part4" runat="server" />
                    <asp:HiddenField ID="hdn_ShortUrl" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_vehicle_capacity" runat="server" CssClass="LABEL" Text="Vehicle Capacity:"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_VehicleCapacity" Width="70%" BorderWidth="1px" Font-Bold="True"
                        runat="server" CssClass="TEXTBOXNOS"></asp:Label>&nbsp;<b>Kg</b>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%" runat="server" id="tr_ALS1">
            <asp:Label ID="Label3" runat="server" CssClass="LABEL" Text="Actual Loading Sheet No:"></asp:Label>
        </td>
        <td style="width: 29%;" runat="server" id="tr_ALS2">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_ALSNo" AutoPostBack="true" runat="server" CssClass="DROPDOWN"
                        OnSelectedIndexChanged="ddl_ALSNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%" runat="server" id="tr_ALS3">
            *</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_loading_supervisor" runat="server" CssClass="LABEL" Text="Loading Supervisor:"></asp:Label>
        </td>
        <td style="width: 29%" runat="server" id="tr_loading_supervisor">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_Loaded_By" runat="server" AllowNewText="True" IsCallBack="True"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" CallBackAfter="2"
                        Text="" PostBack="False" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ALSNo" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_loadingsupervisor" runat="server" Text="*"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Button ID="btnAddLR" runat="server" CssClass="BUTTON" Text="Add LR" OnClientClick="return AddLR()" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc4:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Div_Memo" class="DIV" style="height: 250px">
                        <asp:DataGrid ID="dg_Memo" runat="server" AutoGenerateColumns="False" DataKeyField="GC_Id"
                            CellPadding="2" CssClass="GRID" Style="border-top-style: none" Width="98%" OnItemDataBound="dg_Memo_ItemDataBound">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Attach">
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucMenifest1_dg_Memo');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            OnClick="Check_Single(this,'WucMenifest1_dg_Memo','1');" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Location_Name" HeaderText="Delivery Location"></asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Delivery_Type" HeaderText="Delivery Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="Packing_Type" HeaderText="Packing Type" HeaderStyle-CssClass="HIDEGRIDCOL" ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>--%>
                                <%--<asp:BoundColumn DataField="Booking_Articles" HeaderText="Bkg Articles"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Cnr Name" HeaderText="Cnr Name"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderStyle-Width="0%" ItemStyle-Width="0%" FooterStyle-Width="0%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Booking_Actual_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Actual_Wt") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="0%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Cnee Name" HeaderText="Cnee Name"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderStyle-Width="0%" ItemStyle-Width="0%" FooterStyle-Width="0%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Balance_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="0%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderStyle-Width="0%" ItemStyle-Width="0%" FooterStyle-Width="0%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Balance_Actual_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Actual_Wt") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="0%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Loaded Articles">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Loaded_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                            Style="text-align: right" Width="80%" MaxLength="7"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Loaded Weight">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Loaded_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Weight") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                            Style="text-align: right" Width="80%" MaxLength="7"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_Sub_Total" Value='<%# DataBinder.Eval(Container.DataItem, "Sub_Total") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdn_Booking_Branch_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Booking_Branch_ID") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdn_Payment_Type_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Payment_Type_Id") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ALSNo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Total GC  :" CssClass="LABEL" Font-Bold="True" />&nbsp;
                    <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                    <asp:HiddenField ID="hdn_ALSDATE" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ALSNo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="3">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                &nbsp;</td>
                            <td style="width: 15%">
                                <asp:Label ID="lbl_tolal1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True" />
                            </td>
                            <td class="TD1" style="width: 15%">
                                <asp:Label ID="lbl_tolalLodArt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lbl_tolalLodWt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                            </td>
                            <td style="width: 5%">
                                <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                                <asp:HiddenField ID="hdn_tolalLodArt" runat="server" />
                                <asp:HiddenField ID="hdn_tolalLodWt" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ALSNo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trTotals" style="display: none">
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 33%">
                                <asp:Panel ID="pnl_booking" runat="server" Font-Size="11px" GroupingText="Booking"
                                    Width="99%">
                                    <table width="100%">
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_book_actWt" runat="server" CssClass="LABEL" Text="Actual Wt:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Book_ActualWt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" EnableTheming="True" Font-Bold="True" Font-Names="Verdana"
                                                    Font-Size="11px" ReadOnly="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Book_ActualWt" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_book_topayCol" runat="server" CssClass="LABEL" Text="To Pay Collection:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Book_ToPayCollection" runat="server" TabIndex="4" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana"
                                                    Font-Size="11px" Font-Bold="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Book_ToPayCollection" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 33%">
                                <asp:Panel ID="pnl_crossing" runat="server" Font-Size="11px" GroupingText="Crossing"
                                    Width="99%">
                                    <table width="100%">
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_cros_actWt" runat="server" CssClass="LABEL" Text="Actual Wt:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Cros_ActualWt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" EnableTheming="True" Font-Bold="True" Font-Names="Verdana"
                                                    Font-Size="11px" ReadOnly="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Cros_ActualWt" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_cros_topayCol" runat="server" CssClass="LABEL" Text="To Pay Collection:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Cros_ToPayCollection" runat="server" TabIndex="4" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana"
                                                    Font-Size="11px" Font-Bold="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Cros_ToPayCollection" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 33%">
                                <asp:Panel ID="pnl_total" runat="server" Font-Size="11px" GroupingText="Total" Width="99%">
                                    <table width="100%">
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_tot_actWt" runat="server" CssClass="LABEL" Text="Actual Wt:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Total_ActualWt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" EnableTheming="True" Font-Bold="True" Font-Names="Verdana"
                                                    Font-Size="11px" ReadOnly="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Total_ActualWt" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TD1">
                                                <asp:Label ID="lbl_tot_topayCol" runat="server" CssClass="LABEL" Text="To Pay Collection:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_Total_ToPayCollection" runat="server" TabIndex="4" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana"
                                                    Font-Size="11px" Font-Bold="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdn_Total_ToPayCollection" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ALSNo" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trScheduleArrivalDateTime" style="display: none">
        <td colspan="6">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 25%;">
                        <asp:Label ID="lbl_arridelidate" runat="server" CssClass="LABEL" Text="Scheduled Arrival/Delivery Date:"></asp:Label>
                    </td>
                    <td style="width: 24%;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ComponentArt:Calendar ID="ArrivalDelivery_Date" runat="server" CellPadding="2" ControlType="Picker"
                                    PickerCssClass="PICKER" PickerCustomFormat="dd MMMM yyyy" PickerFormat="Custom"
                                    SelectedDate="2008-10-20">
                                </ComponentArt:Calendar>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Memo_Date" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_MenifestType" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TD1" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 25%;">
                        <asp:Label ID="lbl_arridelitime" runat="server" CssClass="LABEL" Text="Scheduled Arrival/Delivery Time:"></asp:Label>
                    </td>
                    <td style="width: 24%;">
                        <uc1:TimePicker ID="TimePicker1" runat="server" />
                    </td>
                    <td class="TD1" style="width: 1%;">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr>
        <td class="TD1" style="width: 100%;" colspan="6">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
            <asp:Button ID="btn_hidden" runat="server" Text="" OnClick="ddl_MenifestTo_TxtChange"  style="display:none" /> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>                           
            </Triggers>
        </asp:UpdatePanel>       
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
        </td>
        <td style="width: 79%;" colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                TextMode="multiline" Height="40px" MaxLength="250"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%;">
        </td>
    </tr>
    <tr>
        <td align="left" colspan="6" style="text-align: left">
            &nbsp;
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label><br />
            &nbsp;
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR"></asp:Label>&nbsp;
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
            <asp:HiddenField ID="hdn_LoginBranch_Id" runat="server" />
            <asp:HiddenField ID="hdn_Next_No" runat="server" />
            <asp:HiddenField ID="hdn_Padded_Next_No" runat="server" />
            <asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" />
            <asp:HiddenField ID="hdn_ALS_Req" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
                OnClick="btn_Save_Click" />&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                AccessKey="p" OnClick="btn_Save_Print_Click" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" />
            <asp:CheckBox ID="chk_AddLR" Text="Always Chcked" Checked="true" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
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

<script type="text/javascript" language="javascript">
enabledisable_memotobranch();

function update_ArrivalDeliveryDateDetails()
{
    document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_hidden.ClientID%>').click();
}
</script>

