<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyConsolidatedeWayBills.aspx.cs"
    Inherits="Operations_Outward_FrmDailyConsolidatedeWayBills" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMS Daily Consolidated eWay Bills</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="8">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="SMS Daily Consolidated eWay Bills"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" style="width: 5%;">
                </td>
                <td style="width: 20%;">
                </td>
                <td class="TD1" style="width: 5%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 5%;">
                </td>
                <td class="TD1" style="width: 5%;">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 5%;">
                </td>
                <td id="td_ApplicableFrom" runat="server" class="TD1" style="width: 20%;">
                    Loading Date :&nbsp;</td>
                <td class="TDMANDATORY" style="width: 5%;">
                </td>
                <td class="TD1" style="width: 20%;">
                    <%--<uc1:WucDatePicker ID="dtpApplicableFrom"  OnDateSelectionChanged="dtpApplicableFrom_SelectionChanged"  runat="server" ></uc1:WucDatePicker>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdn_Date" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dtpApplicableFrom" />
                    </Triggers>
                </asp:UpdatePanel>
                --%>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                                <ComponentArt:Calendar ID="dtpApplicableFrom" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                    PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="true"
                                    OnSelectionChanged="dtpApplicableFrom_SelectionChanged">
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
                            <asp:HiddenField ID="hdn_Date" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtpApplicableFrom.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtpApplicableFrom.ClientObjectId %>;
                            window.<%= dtpApplicableFrom.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                    </script>

                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 5%;">
                    &nbsp;</td>
                <td class="TDMANDATORY" style="width: 5%">
                </td>
            </tr>
            <%--            <tr>
                <td class="TD1">
                    Branch</td>
                <td>
                    <cc1:DDLSearch ID="ddlBranch" runat="server" AllowNewText="False" IsCallBack="True"
                        CallBackAfter="2" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                        OnTxtChange="ddlBranch_TxtChange" InjectJSFunction="" PostBack="True" Text=""></cc1:DDLSearch>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
                <td class="TD1">
                </td>
                <td>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>--%>
            <tr>
                <td class="TD1" style="width: 5%">
                    &nbsp;</td>
                <td colspan="5">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                        <ContentTemplate>
                            <div id="Div_Commodity" class="DIV" style="height: auto; width: 100%; text-align: left">
                                <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                    ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                    OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                    OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand">
                                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="DetailsID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <%--<asp:BoundColumn DataField="BharaiRateDetailID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="Vehicle No.">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_Vehicle" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    OnSelectedIndexChanged="ddl_Vehicle_SelectedIndexChanged" AutoPostBack="true" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "Vehicle_No")) %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_Vehicle" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    OnSelectedIndexChanged="ddl_Vehicle_SelectedIndexChanged" AutoPostBack="true" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="eWay Bill No">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_eWayBillNo" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="12" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eWayBillNo"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_eWayBillNo" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eWayBillNo")) %>'
                                                    MaxLength="12" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Mobile1">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Mobile1" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                    MaxLength="12" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Mobile1"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Mobile1" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Mobile1")) %>'
                                                    MaxLength="12" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Mobile2">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Mobile2" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                    MaxLength="12" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Mobile2"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Mobile2" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Mobile2")) %>'
                                                    MaxLength="12" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                            EditText="Edit">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 5%">
                </td>
                <td colspan="5" style="text-align: right; width: 40%">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                        <ContentTemplate>
                            <table class="TABLENOBORDER">
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td class="TD1" style="width: 40%">
                                    </td>
                                    <td style="width: 5%" class="TD1">
                                    </td>
                                    <td style="width: 5%" class="TD1">
                                    </td>
                                    <td style="width: 7%; text-align: right; vertical-align: text-top" class="TD1" runat="server"
                                        id="td_length">
                                        &nbsp;
                                    </td>
                                    <td style="width: 7%; text-align: left; vertical-align: text-top" class="TD1" runat="server"
                                        id="td_width">
                                        &nbsp;
                                    </td>
                                    <td style="width: 15%" class="TD1">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel2">
                        <ContentTemplate>
                            &nbsp;<asp:Label ID="lbl_TotalRecords" runat="server" Text="Total Records Added : 0" Font-Bold="true" ForeColor="Navy"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
             <tr>
                <td colspan="6" align="center">&nbsp 
                </td>
            </tr>
            
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                        Text="Save" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Fields with * mark are mandatory"></asp:Label>
                            <asp:HiddenField ID="hdnKeyID" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
