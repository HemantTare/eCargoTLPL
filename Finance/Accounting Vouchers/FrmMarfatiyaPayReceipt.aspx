<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMarfatiyaPayReceipt.aspx.cs"
  Inherits="Finance_Accounting_Vouchers_FrmMarfatiyaPayReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc2" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/MarfatiyaPayReceipt.js"></script>

<script type="text/javascript">

function GetTotalAmount()
{
//    var txt_Total_Receivable = document.getElementById('lbl_Received_Amount');
//     return val(txt_Total_Receivable.value);
     var hdn_Received_Amount = document.getElementById('hdn_Received_Amount');
     return val(hdn_Received_Amount.value);
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Marfatiya Payment Receipt</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_MarfReceipt" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table class="TABLE" style="width: 100%">
      <tr>
        <td class="TDGRADIENT" style="width: 100%">
          <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Marfatiya Payment Receipt"></asp:Label></td>
      </tr>
      <tr>
        <td style="width: 100%">
          <table style="width: 100%" runat= "server" id = "tbl_Header">
            <tr>
              <td class="TD1" style="width: 18%; text-align: right;">
                <asp:Label ID="lbl_ReceiptNo" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ReceiptNoResource1"
                  Text="Receipt No :" Width="100%"></asp:Label>
              </td>
              <td style="width: 30%">
                <asp:Label ID="lbl_Receipt_No" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Receipt_NoResource1"
                  Width="98%"></asp:Label></td>
              <td class="TD1" style="width: 18%; text-align: right">
                &nbsp;<asp:Label ID="lbl_ReceiptDate" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ReceiptDateResource1"
                  Text="Receipt Date :"></asp:Label></td>
              <td style="width: 30%; text-align: left">
                <table border="0" cellpadding="0">
                  <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                      <ComponentArt:Calendar ID="dtp_Receipt_Date" runat="server" AutoPostBackOnSelectionChanged="True"
                        CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged" ControlType="Picker"
                        OnSelectionChanged="dtp_Receipt_Date_SelectionChanged" PickerCssClass="PICKER"
                        PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom">
                      </ComponentArt:Calendar>
                    </td>
                    <td id="TD_Calender" runat="server">
                      <img  alt=""  class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
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
              <td class="TD1" style="width: 18%; text-align: right;">
              </td>
              <td style="width: 30%; text-align: left">

                <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_Receipt_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_Receipt_Date.ClientObjectId %>;
                            window.<%= dtp_Receipt_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
              <td align="right" class="TD1" style="width: 18%; text-align: right">
                &nbsp;</td>
              <td style="width: 30%; text-align: left">
                <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
                  CallBackFunction="Raj.EC.FinanceModel.MRDeliveryModel.GetCreditToBranch" CallBackAfter="2"
                  Text="" PostBack="False" Visible="false" />
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td colspan="6" style="text-align: right">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div id="Div_Marfatiya" class="DIV" style="height: 250px">
                <asp:DataGrid ID="dg_PayReceipt" runat="server" AutoGenerateColumns="false" CssClass="GRID"
                  meta:resourcekey="dg_PayReceiptResource1" OnItemDataBound="dg_PayReceipt_ItemDataBound"
                  Width="98%">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                      <HeaderTemplate>
                        <input id="chkAllItems" onclick="Check_All(this,'dg_PayReceipt');" type="checkbox" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="Chk_Attach" runat="server" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                          meta:resourcekey="Chk_AttachResource1" OnClick="Check_Single(this,'dg_PayReceipt','1');" />
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Bill_ID" HeaderText="Bill_ID" HeaderStyle-CssClass="HIDEGRIDCOL"
                      ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="Bill_No_For_Print" HeaderText="Bill No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Bill_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Bill Date">
                    </asp:BoundColumn> 
                    <asp:TemplateColumn HeaderText="Bill Amount">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Bill_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Bill_Amount") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Bill_AmountResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Received Amount">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Received_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Received_Amount") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="70%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Received_AmountResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn> 
                   <%-- <asp:TemplateColumn HeaderText="Pending Amount">
                      <ItemTemplate>
                        <asp:TextBox ID="txt_Pending_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Pending_Amount") %>'
                          runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                          Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                          meta:resourcekey="txt_Pending_AmountResource1"></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateColumn>--%>
                  </Columns>
                </asp:DataGrid>
              </div>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dtp_Receipt_Date" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td colspan="6" style="text-align: right">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <table border="0" width="100%">
                <tr>
                  <td class="TD1" style="width: 15%">
                    <asp:Label ID="Label1" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="Label1Resource1"
                      Text="Total Bills :"></asp:Label>
                  </td>
                  <td style="width: 8%; text-align: left;">
                    <asp:Label ID="lbl_Total_Bills" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_Total_BillsResource1"
                      Text="0"></asp:Label><asp:HiddenField ID="hdn_Total_Bills" runat="server" />
                  </td>
                  <td class="TD1" style="width: 20%">
                    &nbsp;<asp:Label ID="lbl_tolal1" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_tolal1Resource1"
                      Text="Total :"></asp:Label></td>
                  <td align="left" class="TD1" style="width: 25%; text-align: left">
                    <asp:Label ID="lbl_Bill_Amount" runat="server" CssClass="LABEL" Font-Bold="True"
                      meta:resourcekey="lbl_Bill_AmountResource1" Text="0"></asp:Label><asp:HiddenField
                        ID="hdn_Bill_Amount" runat="server" />
                  </td>
                  <td class="TD1" style="width: 8%; text-align: left;" align="left">
                    &nbsp;</td>
                  <td class="TD1" style="width: 8%">
                    &nbsp;&nbsp;
                  </td>
                  <td class="TD1" style="width: 8%; text-align: center;">
                    <asp:Label ID="lbl_Received_Amount" runat="server" Font-Bold="True"
                      meta:resourcekey="lbl_Received_AmountResource1" Text="0"></asp:Label>
                    <asp:HiddenField ID="hdn_Received_Amount" runat="server" />
                  </td>
                </tr>
              </table>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dtp_Receipt_Date" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td colspan="6">
                <uc2:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
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
