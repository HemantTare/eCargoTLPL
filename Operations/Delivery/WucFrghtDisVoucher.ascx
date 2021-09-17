<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFrghtDisVoucher.ascx.cs"
    Inherits="Operations_Delivery_WucFrghtDisVoucher" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/FrghtDisVoucher.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_FDV" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
 
 

</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" style="width: 100%">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Freight Discount Voucher"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_FrghtDisVoucherNo" runat="server" CssClass="LABEL" Text="Voucher No. :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_FrghtDisVoucher_No" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label></td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lbl_VoucherDate" runat="server" CssClass="LABEL" Text="Voucher Date :"
                            meta:resourcekey="lbl_DDCDateResource1"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                    <ComponentArt:Calendar ID="dtp_Voucher_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True"
                                        OnSelectionChanged="dtp_Voucher_Date_SelectionChanged">
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
                                <asp:HiddenField ID="hdn_Voucher_Date" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtp_Voucher_Date" />
                                <asp:AsyncPostBackTrigger ControlID="dg_FDV" />
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_Voucher_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_Voucher_Date.ClientObjectId %>;
                            window.<%= dtp_Voucher_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        &nbsp;</td>
                    <td style="width: 29%">
                        &nbsp;</td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="5" style="text-align: right" id="td_gccontrol" runat="server">
                        <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server"></uc2:WucSelectedItems>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td style="width: 1%">
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
        <td colspan="5">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="Div_FDV" class="DIV" style="height: 300px;">
                        <asp:DataGrid ID="dg_FDV" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                            Style="border-top-style: none;" Width="97%" OnItemDataBound="dg_FDV_ItemDataBound"
                            meta:resourcekey="dg_FDVResource1">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name" ItemStyle-Width="130px">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Committed_Del_Date" HeaderText="Delivery Date"></asp:BoundColumn>
                                <%--<asp:TemplateColumn HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <ComponentArt:Calendar ID="dtp_Committed_Del_Date" runat="server" CellPadding="2"
                                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                            PickerFormat="Custom" SelectedDate="2008-10-20">
                                        </ComponentArt:Calendar>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="LR<br>Frt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_GC_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                                            runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                            BorderColor="Transparent" Style="text-align: center" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True" meta:resourcekey="Total_GC_AmountResource1"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Discount<br>Amt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_DiscountAmt" runat="server" CssClass="TEXTBOX" MaxLength="10"
                                            meta:resourcekey="txt_DiscountAmtResource1" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmt") %>'
                                            OnTextChanged="txt_DiscountAmt_TextChanged" onkeypress="return Only_Numbers(this,event);"
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_DiscountAmt" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountAmt") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_UnDel_Reason" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_UnDel_Reason_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ItemTemplate>
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
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_FDV" EventName="ItemCommand" />
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
                            <td class="TD1" style="width: 10%">
                            </td>
                            <td style="width: 10%">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_TotalLR" runat="server" Text="Total LR:" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_tolal1Resource1" Width="100px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                                <asp:HiddenField ID="hdnforselectall" runat="server" />
                            </td>
                            <td class="TD1" style="width: 13%">
                            </td>
                            <td align="right" style="width: 10%">
                            </td>
                            <td align="left" style="width: 10%">
                            </td>
                            <td align="center" style="width: 11%">
                            </td>
                            <td align="center" style="width: 11%">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_totaltotal" runat="server" Text="Total Freight:" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_tolal1Resource1" Width="100px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_totaltotalFreight" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_totalFreightResource1" /></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdn_totaltotalFreight" Value="0" runat="server" />
                            </td>
                            <td align="left" style="width: 11%">
                            </td>
                            <td align="center" style="width: 11%; text-align: right;">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label1" runat="server" Text="Total Discount Amt:" CssClass="LABEL"
                                                Font-Bold="True" meta:resourcekey="lbl_tolal1Resource1" Width="139px" /></td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_TotalDiscountAmt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                                meta:resourcekey="lbl_TotalDiscountAmtResource1" /></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdn_TotalDiscountAmt" Value="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtp_Voucher_Date" />
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="dg_FDV" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <table width="98%">
                <tr>
                    <td class="TD1" style="width: 5%">
                        <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label></td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine"
                            MaxLength="250" meta:resourcekey="txt_RemarksResource1" Width ="98%"></asp:TextBox></td>
                    <td style="width: 9%">
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 9%">
                    </td>
                    <td style="width: 10%">
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
        <td colspan="5">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
            <asp:HiddenField ID="hdn_Mode" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" style="text-align: center">
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
        <td colspan="5">
        </td>
    </tr>
    <tr>
        <td colspan="5">
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
