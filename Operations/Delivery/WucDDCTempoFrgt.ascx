<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDDCTempoFrgt.ascx.cs"
    Inherits="Operations_Delivery_WucDDCTempoFrgt" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/DDCTempoFrgt.js"></script>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript">
    function get_button_nullsession_clientid()
    {
        btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
    }
    
    function Open_Details_Window(Path)
    { 
        window.open(Path,'DDCTempoFrgt','width=650,height=500,top=100,left=200,menubar=no,resizable=no,scrollbars=no')
        return false;
    }
    function Open_Details_WindowPDS(PathPDS)
    { 
        window.open(PathPDS,'PDSTempoFrgt','width=450,height=500,top=100,left=300,menubar=no,resizable=no,scrollbars=no')
        return false;
    }
    
</script>

<asp:ScriptManager ID="scm_DDCTempoFrgt" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="DDC Tempo Freight"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td style="width: 32%">
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lbl_DDCTempoFrgtNo" runat="server" CssClass="LABEL" Text="Transaction No :"
                meta:resourcekey="lbl_DDCTempoFrgtNoResource1" Width="99%"></asp:Label></td>
        <td class="TD1" style="width: 32%; text-align: left;">
            <asp:Label ID="lbl_DDCTempoFrgt_No" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                meta:resourcekey="lbl_DDCTempoFrgt_NoResource1"></asp:Label></td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
            <asp:Label ID="lbl_DDCTempoFrgtDate" runat="server" CssClass="LABEL" Text="Transaction Date :"
                meta:resourcekey="lbl_DDCTempoFrgtDateResource1"></asp:Label></td>
        <td class="TD1" style="width: 29%; text-align: left">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                        <ComponentArt:Calendar ID="dtp_DDCTempoFrgt_Date" runat="server" CellPadding="2"
                            ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                            PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                            OnSelectionChanged="dtp_DDCTempoFrgt_Date_SelectionChanged">
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
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td class="TD1" style="width: 32%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td class="TD1" style="width: 29%; text-align: left">

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_DDCTempoFrgt_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_DDCTempoFrgt_Date.ClientObjectId %>;
                            window.<%= dtp_DDCTempoFrgt_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lblVehicleNo" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                Text="Vehicle No :"></asp:Label></td>
        <td class="TD1" style="width: 32%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td class="TD1" style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr id="tr_DateRange" runat="server">
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="LABEL" Text="From Date :"></asp:Label></td>
        <td class="TD1" style="width: 32%; text-align: left;">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= CalFromDate.ClientObjectId %>)">
                        <ComponentArt:Calendar ID="dtpFromDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" OnSelectionChanged="dtpFromDate_SelectionChanged" AutoPostBackOnSelectionChanged="True">
                        </ComponentArt:Calendar>
                    </td>
                    <td runat="server" id="TD1">
                        <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= CalFromDate.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= CalFromDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
            <asp:Label ID="lblToDate" runat="server" CssClass="LABEL" Text="To Date :"></asp:Label></td>
        <td class="TD1" style="width: 29%; text-align: left">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= CalToDate.ClientObjectId %>)">
                        <ComponentArt:Calendar ID="dtpToDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" OnSelectionChanged="dtpToDate_SelectionChanged" AutoPostBackOnSelectionChanged="True">
                        </ComponentArt:Calendar>
                    </td>
                    <td runat="server" id="TD2">
                        <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= CalToDate.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= CalToDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td class="TD1" style="width: 32%">
            <ComponentArt:Calendar runat="server" ID="CalFromDate" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalFromDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalFromDate.ClientObjectId %>_loaded && window.<%= dtpFromDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalFromDate.ClientObjectId %>.AssociatedPicker = <%= dtpFromDate.ClientObjectId %>;
                            window.<%= dtpFromDate.ClientObjectId %>.AssociatedCalendar = <%= CalFromDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalFromDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalFromDate.ClientObjectId %>_Associate();
            </script>

        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td class="TD1" style="width: 29%; text-align: left">
            <ComponentArt:Calendar runat="server" ID="CalToDate" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalToDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalToDate.ClientObjectId %>_loaded && window.<%= dtpToDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalToDate.ClientObjectId %>.AssociatedPicker = <%= dtpToDate.ClientObjectId %>;
                            window.<%= dtpToDate.ClientObjectId %>.AssociatedCalendar = <%= CalToDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalToDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalToDate.ClientObjectId %>_Associate();
            </script>

        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td class="TD1" style="width: 32%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td class="TD1" style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td class="TD1" style="width: 32%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td class="TD1" style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Div_DDCTempoFrgt" class="DIV" style="height: 330px">
                        <asp:DataGrid ID="dg_DDCTempoFrgt" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                            Style="border-top-style: none" Width="98%" OnItemDataBound="dg_DDCTempoFrgt_ItemDataBound"
                            meta:resourcekey="dg_DDCTempoFrgtResource1">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Attach">
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucDDCTempoFrgt1_dg_DDCTempoFrgt');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            OnClick="Check_Single(this,'WucDDCTempoFrgt1_dg_DDCTempoFrgt','1');" runat="server"
                                            meta:resourcekey="Chk_AttachResource1" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                    <HeaderStyle Width="10px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="PDS_Date" HeaderText="PDS Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PDSTime" HeaderText="PDS Time">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="PDS_No_For_Print" HeaderText="PDS No">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="PDS No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_PDS_No_For_Print" Text='<%# DataBinder.Eval(Container, "DataItem.PDS_No_For_Print") %>'
                                            Font-Bold="True" Font-Underline="True" runat="server" CommandName="PDS_No_For_Print"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.PDS_ID") %>' />
                                        <asp:HiddenField ID="hdn_PDS_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.PDS_ID") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DDC_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DDCTime" HeaderText="DDC Time">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="DayName" HeaderText="DayName"></asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="DDC No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_DDC_No_For_Print" Text='<%# DataBinder.Eval(Container, "DataItem.DDC_No_For_Print") %>'
                                            Font-Bold="True" Font-Underline="True" runat="server" CommandName="DDC_No_For_Print"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DDC_ID") %>' />
                                        <asp:HiddenField ID="hdn_DDC_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.DDC_ID") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Of LR">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_No_Of_GC" Text='<%# DataBinder.Eval(Container.DataItem, "Total_No_Of_GC") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            BackColor="Transparent" BorderStyle="None" BorderColor="Transparent" Style="text-align: right"
                                            Font-Size="11px" Font-Names="Verdana" Width="80%" MaxLength="7"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Of Pkgs">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_DDC_Articles" Text='<%# DataBinder.Eval(Container.DataItem, "Total_DDC_Articles") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            BackColor="Transparent" BorderStyle="None" BorderColor="Transparent" Style="text-align: right"
                                            Font-Size="11px" Font-Names="Verdana" Width="80%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total LR Frt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_GC_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            BackColor="Transparent" BorderStyle="None" BorderColor="Transparent" Style="text-align: right"
                                            Font-Size="11px" Font-Names="Verdana" Width="80%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn HeaderText="TripWise</br>Tempo Freight">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_TripWise_Tempo_Freight" Text='<%# DataBinder.Eval(Container.DataItem, "TripWise_Tempo_Freight") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            BackColor="Transparent" BorderStyle="None" BorderColor="Transparent" Style="text-align: right"
                                            Font-Size="11px" Font-Names="Verdana" Width="80%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="Tempo Frt</br>To be Paid">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Tempo_Freight_ToBePaid" Text='<%# DataBinder.Eval(Container.DataItem, "Tempo_Freight_ToBePaid") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            OnTextChanged="txt_Total_Tempo_Freight_ToBePaid_TextChanged" AutoPostBack="true"
                                            Width="80%" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="70px" />
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bonus">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Bonus" Text='<%# DataBinder.Eval(Container.DataItem, "Bonus") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            OnTextChanged="txt_Bonus_TextChanged" AutoPostBack="true" Width="80%" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="70px" />
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="AddTempoFrt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_AddTempoFrt" Text='<%# DataBinder.Eval(Container.DataItem, "AddTempoFrt") %>'
                                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            AutoPostBack="true" Width="80%" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="70px" />
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tr>
                            <td class="TD1" style="width: 15%">
                                <asp:Label ID="Label1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="Label1Resource1" />
                            </td>
                            <td style="width: 29%;">
                                <asp:Label ID="lbl_Total_Records" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /><asp:HiddenField
                                    ID="hdn_Total_Records" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_tolal1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True"
                                    meta:resourcekey="lbl_tolal1Resource1" />
                            </td>
                            <td class="TD1" style="width: 8%; text-align: left" align="left">
                                <asp:Label ID="lbl_Total_No_Of_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /><asp:HiddenField
                                    ID="hdn_Total_No_Of_GC" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_Total_DDC_Articles" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /><asp:HiddenField
                                    ID="hdn_Total_DDC_Articles" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_Total_GC_Amount" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_Total_GC_Amount" runat="server" />
                            </td>
                            <%--<td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_TripWise_Tempo_Freight" runat="server" Text="0" CssClass="LABEL"
                                    Font-Bold="True" />
                                <asp:HiddenField ID="hdn_TripWise_Tempo_Freight" runat="server" />
                            </td>--%>
                            <td class="TD1" style="width: 8%" align="center">
                                <asp:Label ID="lbl_Tempo_Freight_ToBePaid" runat="server" Text="0" CssClass="LABEL"
                                    Font-Bold="True" />
                                <asp:HiddenField ID="hdn_Tempo_Freight_ToBePaid" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%" align="center">
                                <asp:Label ID="lbl_Bonus" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_Bonus" runat="server" />
                            </td>
                             <td class="TD1" style="width: 8%" align="center">
                                <asp:Label ID="lbl_AddTempoFrt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_AddTempoFrt" runat="server" />
                            </td>                            
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr class="HIDEGRIDCOL">
        <td class="TD1" style="width: 19%; text-align: right;">
            <%--<asp:Label ID="lblIsCashCheque" runat="server" CssClass="LABEL" Text="Is Cash/Cheque:"></asp:Label>--%>
        </td>
        <td style="width: 32%; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rbtnIsCashCheque" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rbtnIsCashCheque_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Text="Cash/Cheque" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Credit" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%;">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblTotalTempoFrgtTBPaid" runat="server" CssClass="LABEL" Text="Tempo Freight :"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnTotalTempoFrgtTBPaid" runat="server" />
                    <asp:TextBox ID="txtTotalTempoFrgtTBPaid" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event);"
                        Text="0" Width="100px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr class="HIDEGRIDCOL">
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lblCashAmount" runat="server" CssClass="LABEL" Text="Cash Amount :"></asp:Label></td>
        <td style="width: 32%; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnCashAmount" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="txtCashAmount" runat="server" Text="0" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event);"
                        Width="100px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
            <asp:Label ID="lblChequeAmount" runat="server" CssClass="LABEL" Text="Cheque Amount :"></asp:Label></td>
        <td style="width: 29%; text-align: left">
            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnChequeAmount" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="txtChequeAmount" runat="server" Text="0" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event);"
                        Width="100px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr class="HIDEGRIDCOL">
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lblChequeNo" runat="server" CssClass="LABEL" Text="Cheque No. :"></asp:Label></td>
        <td style="width: 32%; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnChequeNo" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="txtChequeNo" runat="server" Text="0" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event);"
                        Width="100px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
            <asp:Label ID="lblChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date :"></asp:Label></td>
        <td style="width: 29%; text-align: left">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp(<%= CalendarChequeDate.ClientObjectId %>)">
                                <ComponentArt:Calendar ID="dtp_ChequeDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                    PickerFormat="Custom">
                                </ComponentArt:Calendar>
                            </td>
                            <td runat="server" id="Td3">
                                <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= CalendarChequeDate.ClientObjectId %>)"
                                    onmouseup="Button_OnMouseUp(<%= CalendarChequeDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                    width="25" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_DDCTempoFrgt_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                    <asp:AsyncPostBackTrigger ControlID="dtpToDate" />
                    <asp:AsyncPostBackTrigger ControlID="dg_DDCTempoFrgt" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnIsCashCheque" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td style="width: 32%">
            <asp:HiddenField ID="hdnFrgtSettlementTypeID" runat="server" />
            <asp:HiddenField ID="hdnFrgtSettlementType" runat="server" />
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td style="width: 29%; text-align: left">
            <ComponentArt:Calendar runat="server" ID="CalendarChequeDate" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalendarChequeDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalendarChequeDate.ClientObjectId %>_loaded && window.<%= dtp_ChequeDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalendarChequeDate.ClientObjectId %>.AssociatedPicker = <%= dtp_ChequeDate.ClientObjectId %>;
                            window.<%= dtp_ChequeDate.ClientObjectId %>.AssociatedCalendar = <%= CalendarChequeDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalendarChequeDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalendarChequeDate.ClientObjectId %>_Associate();
            </script>

        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label></td>
        <td style="width: 32%" colspan="3">
            <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine"
                MaxLength="10" Width="98%"></asp:TextBox></td>
        <td style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 19%; text-align: right">
        </td>
        <td style="width: 32%">
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 19%">
        </td>
        <td style="width: 29%; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="6">
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
                
             <asp:Button ID="btn_Save_Pay" runat="server" CssClass="BUTTON" Text="Save & Pay"
                AccessKey="y" OnClick="btn_Save_Pay_Click" />&nbsp
                
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
