<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMarfatiyaBill.aspx.cs"
  Inherits="Finance_Accounting_Vouchers_FrmMarfatiyaBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/MarfatiyaBill.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Marfatiya Bill</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_MarfBill" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table class="TABLE" style="width: 100%">
      <tr>
        <td class="TDGRADIENT" style="width: 100%">
          <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Marfatiya Bill"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 100%">
          <table style="width: 100%" runat= "server" id = "tbl_Header">
            <tr>
              <td class="TD1" style="width: 18%; text-align: right;">
                <asp:Label ID="lbl_BillNo" runat="server" CssClass="LABEL" meta:resourcekey="lbl_BillNoResource1"
                  Text="Bill No :" Width="100%"></asp:Label>
              </td>
              <td style="width: 30%">
                <asp:Label ID="lbl_Bill_No" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Bill_NoResource1"
                  Width="98%"></asp:Label></td>
              <td style="width: 4%">
              </td>
              <td class="TD1" style="width: 18%; text-align: right">
                &nbsp;<asp:Label ID="lbl_BillDate" runat="server" CssClass="LABEL" meta:resourcekey="lbl_BillDateResource1"
                  Text="Bill Date :"></asp:Label></td>
              <td style="width: 30%; text-align: left">
                <table border="0" cellpadding="0">
                  <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                      <ComponentArt:Calendar ID="dtp_Marfatiya_Date" runat="server" AutoPostBackOnSelectionChanged="True"
                        CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                        OnSelectionChanged="dtp_Marfatiya_Date_SelectionChanged" PickerCssClass="PICKER"
                        PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom">
                      </ComponentArt:Calendar>
                    </td>
                    <td id="TD_Calender" runat="server">
                      <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../Images/btn_calendar.gif"
                        width="25" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td class="TD1" style="width: 18%; text-align: right; vertical-align: top;">
              </td>
              <td style="width: 30%">
              </td>
              <td class="TD1" style="width: 4%">
              </td>
              <td class="TD1" style="width: 18%; text-align: right;">
              </td>
              <td style="width: 30%; text-align: left">

                <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_Marfatiya_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_Marfatiya_Date.ClientObjectId %>;
                            window.<%= dtp_Marfatiya_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                </script>

                <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="False" AllowMultipleSelection="False"
                  AllowWeekSelection="False" CalendarCssClass="CALENDER" CalendarTitleCssClass="TITLE"
                  ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                  DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER" DayNameFormat="FirstTwoLetters"
                  ImagesBaseUrl="../../images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                  NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                  PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300">
                </ComponentArt:Calendar>
              </td>
            </tr>
            <tr>
              <td class="TD1" style="width: 18%; text-align: right; vertical-align: top;">
                <asp:Label ID="lbl_MarfatiyaName" runat="server" CssClass="LABEL" meta:resourcekey="lbl_MarfatiyaNameeResource1"
                  Text="Marfatiya Name :" Width="103px"></asp:Label></td>
              <td style="width: 30%">
                <cc1:DDLSearch ID="ddl_DebitTo" runat="server" AllowNewText="True" IsCallBack="True"
                  CallBackFunction="Raj.EC.FinanceModel.MRDeliveryModel.GetLedger" CallBackAfter="2"
                  Text="" PostBack="True" onblur="ddl_DebitTo_Select" OnTxtChange="ddl_DebitTo_TxtChange" />
              </td>
              <td class="TD1" style="width: 4%">
              </td>
              <td align="right" class="TD1" style="width: 18%; text-align: right">
                &nbsp;</td>
              <td style="width: 30%; text-align: left">
                <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
                  CallBackFunction="Raj.EC.FinanceModel.MRDeliveryModel.GetCreditToBranch" CallBackAfter="2"
                  Text="" PostBack="False" Visible="false" />
              </td>
            </tr>
            <tr>
              <td class="TD1" style="vertical-align: top; width: 18%; text-align: right">
              </td>
              <td style="width: 30%">
                &nbsp;</td>
              <td class="TDMANDATORY" style="width: 4%">
              </td>
              <td class="TD1" style="width: 18%; text-align: right;">
              </td>
              <td style="width: 30%; text-align: left">
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div id="Div_Marfatiya" class="DIV" style="height: 250px">
                <asp:DataGrid ID="dg_Marfatiya" runat="server" AutoGenerateColumns="false" CssClass="GRID"
                  meta:resourcekey="dg_MarfatiyaResource1" OnItemDataBound="dg_Marfatiya_ItemDataBound"
                  Width="98%">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                      <HeaderTemplate>
                        <input id="chkAllItems" onclick="Check_All(this,'dg_Marfatiya');" type="checkbox" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="Chk_Attach" runat="server" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                          meta:resourcekey="Chk_AttachResource1" OnClick="Check_Single(this,'dg_Marfatiya','1');" />
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Credit_Memo_ID" HeaderText="Credit_Memo_ID" HeaderStyle-CssClass="HIDEGRIDCOL"
                      ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="GC_ID" HeaderText="GC_ID" HeaderStyle-CssClass="HIDEGRIDCOL"
                      ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Credit_Memo_No" HeaderText="Memo No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Credit_Memo_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Memo Date">
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="LR No">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_GC_No_For_Print" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_GC_No_For_PrintResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="ToPay LR Total">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_ToPay_LR_Total" Text='<%# DataBinder.Eval(Container.DataItem, "ToPay_LR_Total") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_ToPay_LR_TotalResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Delivery Charges">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Delivery_Charges" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Charges") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Delivery_ChargesResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Delivery Service Tax">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Delivery_Service_Tax" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Service_Tax") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="70%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Delivery_Service_TaxResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Memo Total">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Memo_Total" Text='<%# DataBinder.Eval(Container.DataItem, "Memo_Total") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Memo_TotalResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                  </Columns>
                </asp:DataGrid>
              </div>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dtp_Marfatiya_Date" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <table border="0" width="100%">
                <tr>
                  <td class="TD1" style="width: 15%">
                    <asp:Label ID="Label1" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="Label1Resource1"
                      Text="Total GC :"></asp:Label>
                  </td>
                  <td style="width: 8%">
                    <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_Total_GCResource1"
                      Text="0"></asp:Label><asp:HiddenField ID="hdn_Total_GC" runat="server" />
                  </td>
                  <td class="TD1" style="width: 20%">
                    <asp:Label ID="lbl_tolal1" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_tolal1Resource1"
                      Text="Total :"></asp:Label>
                  </td>
                  <td align="left" class="TD1" style="width: 25%; text-align: left">
                    <asp:Label ID="lbl_ToPay_LR_Total" runat="server" CssClass="LABEL" Font-Bold="True"
                      meta:resourcekey="lbl_ToPay_LR_TotalResource1" Text="0"></asp:Label><asp:HiddenField
                        ID="hdn_ToPay_LR_Total" runat="server" />
                  </td>
                  <td class="TD1" style="width: 8%; text-align: left;" align="left">
                    <asp:Label ID="lbl_Delivery_Charges" runat="server" Font-Bold="True"
                      meta:resourcekey="lbl_Delivery_ChargesResource1" Text="0"></asp:Label>
                    <asp:HiddenField ID="hdn_Delivery_Charges" runat="server" />
                    &nbsp;&nbsp;</td>
                  <td class="TD1" style="width: 8%">
                    &nbsp;
                  </td>
                  <td class="TD1" style="width: 8%">
                    <asp:Label ID="lbl_Delivery_Service_Tax" runat="server" Font-Bold="True"
                      meta:resourcekey="lbl_Delivery_Service_TaxResource1" Text="0"></asp:Label>
                    <asp:HiddenField ID="hdn_Delivery_Service_Tax" runat="server" />
                  </td>
                  <td align="center" class="TD1" style="width: 8%">
                    <asp:Label ID="lbl_Memo_Total" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_Memo_TotalResource1"
                      Text="0"></asp:Label>
                    <asp:HiddenField ID="hdn_Memo_Total" runat="server" />
                  </td>
                </tr>
              </table>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dtp_Marfatiya_Date" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <table width="100%">
            <tr>
              <td class="TD1" style="width: 10%; text-align: right">
                <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" meta:resourcekey="lbl_RemarksResource1"
                  Text="Remarks :"></asp:Label>
              </td>
              <td colspan="4">
                <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" Height="40px" MaxLength="10"
                  meta:resourcekey="txt_RemarksResource1" TextMode="MultiLine" Width="660"></asp:TextBox></td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>&nbsp;
        </td>
      </tr>
      <tr>
        <td class="TD1" colspan="6" style="text-align: center">
          <asp:Button ID="btn_Save" runat="server" AccessKey="N" CssClass="BUTTON" meta:resourcekey="btn_SaveResource1"
            OnClick="btn_Save_Click" Text="Save & New" />&nbsp;
          <asp:Button ID="btn_Save_Exit" runat="server" AccessKey="S" CssClass="BUTTON" meta:resourcekey="btn_Save_ExitResource1"
            OnClick="btn_Save_Exit_Click" Text="Save & Exit" />&nbsp;
          <asp:Button ID="btn_Save_Print" runat="server" AccessKey="p" CssClass="BUTTON" OnClick="btn_Save_Print_Click"
            Text="Save & Print" />&nbsp;
          <asp:Button ID="btn_Close" runat="server" AccessKey="E" CssClass="BUTTON" meta:resourcekey="btn_CloseResource1"
            OnClick="btn_Close_Click" Text="EXIT" />&nbsp;
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:HiddenField ID="hdn_GCCaption" runat="server" />
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
            Text="Fields with * mark are Mandatory"></asp:Label>
          <asp:HiddenField ID="hdnKeyID" runat="server" />
          &nbsp;
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
