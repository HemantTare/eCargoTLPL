<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTripMemo.aspx.cs" Inherits="Operations_Outward_FrmTripMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Outward/TripMemo.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type ="text/javascript" language ="javascript">
function windowClose()
{
  window.close(); 
}
</script>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Trip Memo</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div>
      <table class="TABLE" width="100%">
        <tr>
          <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Trip Memo"></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="3">
            &nbsp;</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 25%">Trip Memo Date:</td>
          <td style="width: 24%">
            <table border="0" cellpadding="0" width="100%">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px;
                  width: 40%">
                  <ComponentArt:Calendar ID="dtpTripMemoDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                    AutoPostBackOnSelectionChanged="True" OnSelectionChanged="DateChange">
                  </ComponentArt:Calendar>
                </td>
                <td style="height: 24px; width: 15%" runat="server" id="td_cal">
                  <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                    onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                    width="25" />
                </td>
                <td class="TD1" style="width: 45%"></td>
              </tr>
            </table>
          </td>
          <td class="TDMANDATORY" style="width: 1%">*</td>
          <td style="width: 50%">&nbsp</td>
        </tr>
        <tr>
          <td class="TD1">Vehicle No:</td>
          <td>
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
              </Triggers>
              <ContentTemplate>
                <uc2:WucVehicleSearch ID="DDLVehicleSearch" runat="server" />
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">*</td>
          <td style="width: 50%">
            <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
              AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar" PopUp="Custom"
              CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
              DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
              SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
              MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
              PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
            // Associate the picker and the calendar:
                function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                {
                  if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtpTripMemoDate.ClientObjectId %>_loaded)
                  {
                    window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtpTripMemoDate.ClientObjectId %>;
                    window.<%= dtpTripMemoDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                  }
                  else
                  {
                    window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                  }
                }
                 ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
            </script>

          </td>
        </tr>
        <tr>
          <td class="TD1">Trip Memo Type:</td>
          <td>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
              </Triggers>
              <ContentTemplate>
                <asp:DropDownList ID="ddlTripMemoType" Enabled="false" runat="server" CssClass="DROPDOWN">
                  <asp:ListItem Value="1">New</asp:ListItem>
                  <asp:ListItem Value="2">Attached</asp:ListItem>
                </asp:DropDownList>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">&nbsp</td>
          <td style="width: 50%">&nbsp</td>
        </tr>
        <tr>
          <td class="TD1">Trip Memo No:</td>
          <td>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
              </Triggers>
              <ContentTemplate>
                <asp:Label ID="lblTripMemoNo" runat="server" CssClass="LABEL" />
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">&nbsp</td>
          <td style="width: 50%">&nbsp</td>
        </tr>
        <tr>
          <td style="width: 20%" class="TD1">Driver 1:</td>
          <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
              </Triggers>
              <ContentTemplate>                  
                <asp:TextBox ID="txtDriver" runat="server" CssClass="TEXTBOX" onblur="On_txtLostFocus('txtDriver','lstDriver','hdnDriverId')" 
                    onkeyup="Search_txtSearch(event,this,'lstDriver',2);" onkeydown="return on_keydown(event,'txtDriver','lstDriver');" 
                    onfocus="On_Focus('txtDriver','lstDriver');" MaxLength="50" EnableViewState="False" autocomplete="off" ></asp:TextBox>
                <asp:ListBox ID="lstDriver" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtDriver')" runat="server" TabIndex="20"></asp:ListBox>
                <asp:HiddenField ID="hdnDriverId" Value="0" runat="server" />  
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">*</td>
          <td style="width: 50%" align="left" colspan="3">
            <asp:LinkButton ID="lnkAddDriver" Font-Bold="true" OnClientClick="return Add_Driver_Window()" runat="server" Text="Add New"></asp:LinkButton>
            <asp:HiddenField ID="hdnDriverpath" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="4">
            <table style="width: 100%">
              <tr>
                <td style="width: 100%">
                  <div id="Div_Memo" class="DIV" style="height: 370px">
                    <asp:UpdatePanel ID="Upd_Pnl_dg_LHPOHireDetails" UpdateMode="Conditional" runat="server">
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgMemoDetails" />
                        <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
                        <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
                      </Triggers>
                      <ContentTemplate>
                        <asp:DataGrid ID="dgMemoDetails" runat="server" AutoGenerateColumns="False" DataKeyField="Memo_Id"
                          CellPadding="2" CssClass="GRID" Style="border-top-style: none" Width="98%">
                          <FooterStyle CssClass="GRIDFOOTERCSS" />
                          <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                          <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                          <Columns>
                            <asp:TemplateColumn HeaderText="Attach">
                              <HeaderTemplate>
                                <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'dgMemoDetails');" />
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "IsTrue").ToString()) %>'
                                  OnClick="Check_Single(this,'dgMemoDetails');" runat="server" />
                              </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Manifest No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Memo_Date" HeaderText="Manifest Date" DataFormatString="{0:dd-MM-yyyy}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Memo_Type" HeaderText="Manifest Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Branch_Name" HeaderText="Manifest To"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Total GC">
                              <ItemTemplate>
                                <asp:TextBox ID="txt_Tot_GC" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC") %>'
                                  runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True"
                                  Width="80%" MaxLength="7"></asp:TextBox>
                              </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total Articles">
                              <ItemTemplate>
                                <asp:TextBox ID="txt_Tot_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Loaded_Articles") %>'
                                  BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True"
                                  runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Dot(this,event)"
                                  Width="80%" MaxLength="7"></asp:TextBox>
                              </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total Actual Wt.">
                              <ItemTemplate>
                                <asp:TextBox ID="txt_Tot_Act_Wt" BackColor="Transparent" BorderColor="Transparent"
                                  BorderStyle="None" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Loaded_Weight") %>'
                                  runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Dot(this,event)"
                                  Width="80%" MaxLength="7"></asp:TextBox>
                              </ItemTemplate>
                            </asp:TemplateColumn>
                          </Columns>
                        </asp:DataGrid>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                  </div>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td colspan="6" align="right">
            <asp:UpdatePanel ID="Upd_Pnl_TotalofGrid" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dgMemoDetails" />
                <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
              </Triggers>
              <ContentTemplate>
                <table style="width: 50%">
                  <tr>
                    <td style="width: 16%" align="left">
                      <asp:Label ID="lbl_GridTotal" runat="server" CssClass="LABEL" Font-Bold="True" Text="Total:"></asp:Label></td>
                    <td style="width: 7%" align="left">
                      <asp:TextBox ID="txt_TotalGC" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                        Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                      <asp:HiddenField ID="hdn_TotalGC" runat="server" />
                    </td>
                    <td align="center" style="width: 3%">
                      &nbsp;
                    </td>
                    <td style="width: 10%" align="center">
                      <asp:TextBox ID="txt_TotalArticle" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                        Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                      <asp:HiddenField ID="hdn_TotalArticle" runat="server" />
                    </td>
                    <td align="center" style="width: 4%">
                      &nbsp;
                    </td>
                    <td style="width: 14%" align="center">
                      <asp:TextBox ID="txt_TotalArticleWT" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                        Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                      <asp:HiddenField ID="hdn_TotalArticleWT" runat="server" />
                    </td>
                  </tr>
                </table>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td class="TD1" colspan="4" style="text-align: center">
            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />  
              </Triggers>
              <ContentTemplate>
                <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
                  OnClick="btn_Save_Click" />&nbsp
                <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                  AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
                <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                  AccessKey="p" OnClick="btn_Save_Print_Click" />&nbsp&nbsp;
                <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                  OnClientClick="windowClose()" />
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
              </Triggers>
              <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
      <asp:HiddenField ID="hdnKeyID" runat="server" />
      <asp:HiddenField ID="hdnAttachedLHPODate" runat="server" />
      <asp:HiddenField ID="hdnSelectedMemoCount" runat="server" />
      <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dtpTripMemoDate" />
          <asp:AsyncPostBackTrigger ControlID="DDLVehicleSearch" />
        </Triggers>
        <ContentTemplate>
          <asp:HiddenField ID="hdnDVLPID" runat="server" />
          <asp:HiddenField ID="hdnAUSID" runat="server" />
          <asp:HiddenField ID="hdnMainLHPOID" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
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
    </div>
  </form>
</body>
</html>
