<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleOpeningTrip.aspx.cs"
    Inherits="Operations_VehicleTripExpense_FrmVehicleOpeningTrip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" language="javascript">
function windowClose()
{
  window.close(); 
}
function Allow_To_Save()
{
    var ATS = false;
    var hdn_TotalAdvance = document.getElementById("hdn_TotalAdvance");
    var lbl_Errors = document.getElementById("lbl_Errors");
    
    if(parseFloat(hdn_TotalAdvance.value) <= 0)
    {
        lbl_Errors.innerHTML = 'Please Enter Advance Details';
        txt_BharaiRemark.focus();
    }
    else
        ATS = true;
    
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opening Trip Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="8">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Opening Trip Sheet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 10%">
                        Date:</td>
                    <td style="width: 20%">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td onmouseup="Button_OnMouseUp(<%= CalendarFromDate.ClientObjectId %>)" style="height: 24px">
                                            <ComponentArt:Calendar ID="dtpFromDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                                ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                                                PickerFormat="Custom" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="dtpFromDate_SelectionChanged">
                                            </ComponentArt:Calendar>
                                        </td>
                                        <td style="height: 24px" runat="server" id="TDFromDate">
                                            <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= CalendarFromDate.ClientObjectId %>)"
                                                onmouseup="Button_OnMouseUp(<%= CalendarFromDate.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                                width="25" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="CalendarFromDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ComponentArt:Calendar runat="server" ID="CalendarFromDate" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate()
                        {
                          if (window.<%= CalendarFromDate.ClientObjectId %>_loaded && window.<%= dtpFromDate.ClientObjectId %>_loaded)
                          {
                            window.<%= CalendarFromDate.ClientObjectId %>.AssociatedPicker = <%= dtpFromDate.ClientObjectId %>;
                            window.<%= dtpFromDate.ClientObjectId %>.AssociatedCalendar = <%= CalendarFromDate.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= CalendarFromDate.ClientObjectId %>_Associate();
                        </script>

                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                        Trip No. :</td>
                    <td style="width: 45%">
                        <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                &nbsp<asp:Label ID="lbl_TripNo" runat="server" Text="000" Font-Bold="true" ForeColor="darkBlue"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Vehicle No :</td>
                    <td style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                            </Triggers>
                            <ContentTemplate>
                                <uc2:WucVehicleSearch ID="DDLVehicleSearch" runat="server" />
                                <asp:HiddenField ID="hdnTripSettledUpTo" runat="server" Value=""></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                        *</td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%;">
                        Current Route :</td>
                    <td style="width: 45%">
                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dtpFromDate" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_CurrentRoute" runat="server" Text="" Font-Bold="true" ForeColor="DarkRed"></asp:Label>
                                <asp:HiddenField ID="hdn_CurrentDailyVehicleLoadingPlanID" runat="server" Value="0">
                                </asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 10%;">
                        Driver :</td>
                    <td style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                <cc1:DDLSearch ID="DDLDriver" runat="server" AllowNewText="False" IsCallBack="True"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" CallBackAfter="2"
                                    PostBack="False" InjectJSFunction="" Text="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                        *</td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 10%;">
                        Cleaner :</td>
                    <td style="width: 45%;">
                        &nbsp;<cc1:DDLSearch ID="DDLCleaner" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchCleanerName" CallBackAfter="2"
                            PostBack="False" InjectJSFunction="" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 15px">
                        &nbsp;</td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr style="height: 300px" valign="top">
                    <td style="width: 40%">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_Advance" runat="server" Height="200px" ScrollBars="None" Font-Bold="true">
                                    <asp:Label ID="lblAdvanceDetails" runat="server" Text="Advance Details" Font-Bold="true"
                                        Height="16px" BackColor="Orange" Width="100%"></asp:Label>
                                    </br>
                                    <asp:DataGrid ID="dg_AdvanceDetails" runat="server" CellPadding="3" CssClass="Grid"
                                        AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_AdvanceDetails_CancelCommand"
                                        OnDeleteCommand="dg_AdvanceDetails_DeleteCommand" OnEditCommand="dg_AdvanceDetails_EditCommand"
                                        OnItemCommand="dg_AdvanceDetails_ItemCommand" OnItemDataBound="dg_AdvanceDetails_ItemDataBound"
                                        OnUpdateCommand="dg_AdvanceDetails_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Branch">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_AdvanceBranch" runat="server" Width="100%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_AdvanceBranch_SelectedIndexChanged" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Branch_Name")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="40%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="40%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_AdvanceBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Closing Cash" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle Width="20%" HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_ClosingCash" runat="server" Font-Bold="true" Width="95%" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingCash")%>'></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ClosingCash" runat="server" Font-Bold="true" Width="95%" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingCash")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Advance">
                                                <HeaderStyle Width="20%" HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                                        onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_GridAdvanceDetails" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_GridAdvanceDetails" Text="Delete"
                                                        CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                    </td>
                    <td style="width: 58%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 40%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_TotalAdvanceHeader" runat="server" Text="Total Advance : " Font-Bold="true"></asp:Label>&nbsp&nbsp
                                <asp:Label ID="lbl_TotalAdvance" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalAdvance" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 58%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%" colspan="2">
                        <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                        <asp:TextBox ID="txt_Remarks" Height="40px" runat="server" CssClass="TEXTBOX" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TD1" style="text-align: center">
                        <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                                    AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
                                <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                                    OnClientClick="windowClose()" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnKeyID" runat="server" />
            &nbsp; &nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
