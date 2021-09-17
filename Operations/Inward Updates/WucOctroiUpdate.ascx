<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOctroiUpdate.ascx.cs" Inherits="Operations_Octroi_Update_WucOctroiUpdate" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>
<%@ Register Src="~/Operations/Inward Updates/WucOtherChargeLedger.ascx" TagName="WucOtherChargeLedger" TagPrefix="uc3" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Octroi Update/OctroiUpdate.js"></script>

<asp:ScriptManager ID="scm_OctroiUpdate" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
function Allow_To_Save()
{
    var ATS = false;
    var txt_BillNo = document.getElementById('<%=txt_BillNo.ClientID%>');
    var hdn_Ledger = document.getElementById('<%=hdn_Ledger.ClientID%>');
    var hdn_LedgerGroupId=document.getElementById('<%=hdn_LedgerGroupId.ClientID%>');
    var txt_ChequeNo=document.getElementById('<%=txt_ChequeNo.ClientID%>');
    var txt_BankName=document.getElementById('<%=txt_BankName.ClientID%>');

    //alert(hdn_Ledger.value);

    var lbl_Errors =document.getElementById('<%=lbl_Errors.ClientID%>');
    lbl_Errors.innerText='';
     if(hdn_Ledger.value =='')
     {
        lbl_Errors.innerText = "Please Select Ledger Name";
     }
     else if(txt_BillNo.value == '' && control_is_mandatory(txt_BillNo) == true)
     {
        lbl_Errors.innerText = "Please Enter Bill No";
        txt_BillNo.focus();
     }
     else if (hdn_LedgerGroupId.value == 19 && txt_ChequeNo.value == '')
     {
       lbl_Errors.innerText="Please Enter Cheque No";
       txt_ChequeNo.focus();
     }
     else if (hdn_LedgerGroupId.value ==19 && txt_BankName.value == '')
     {
      lbl_Errors.innerText="Please Enter Name Of The Bank";
      txt_BankName.focus();
     }
     else
         ATS = true;
     
     return ATS; 
}
</script>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="OCTROI UPDATE"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TransactionNo" runat="server" CssClass="LABEL" Text="Transaction No :"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Tranaction_No" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label></td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TransactionDate" runat="server" CssClass="LABEL" Text="Transaction Date :"></asp:Label>
        </td>
        <td style="width: 29%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                        <ComponentArt:Calendar ID="dtp_Transaction_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" SelectedDate="2008-10-20">
                        </ComponentArt:Calendar>
                    </td>
                    <td>
                        <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%" colspan="3">
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
              if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_Transaction_Date.ClientObjectId %>_loaded)
              {
                window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_Transaction_Date.ClientObjectId %>;
                window.<%= dtp_Transaction_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
            <asp:Label ID="lbl_LedgerName" runat="server" CssClass="LABEL" Text="Ledger Name:"></asp:Label>
        </td>
        <td style="width: 29%">
            <%--<asp:UpdatePanel ID="up_Ledger" runat="server">
                <ContentTemplate>--%>
                    <cc1:DDLSearch ID="ddl_LedgerName" runat="server" AllowNewText="False" IsCallBack="True"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerForOctroiUpdate"
                        CallBackAfter="2" PostBack="True" OnTxtChange="ddl_LedgerName_TxtChange" />
                    <asp:HiddenField ID="hdn_Ledger" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_LedgerGroupId" runat="server" />
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
        <td class="TD1" style="width: 50%" colspan="3">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_BillNo" runat="server" CssClass="LABEL" Text="Octroi Bill No :"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_BillNo" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">
            <asp:Label ID="lbl_mandatory_txt_BillNo" runat="server" CssClass="LABEL" ForeColor="red" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_BillDate" runat="server" CssClass="LABEL" Text="Octroi Bill Date :"></asp:Label>
        </td>
        <td style="width: 29%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="dtp_Bill_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" SelectedDate="2008-10-20">
                        </ComponentArt:Calendar>
                    </td>
                    <td style="height: 24px">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar2.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar2.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%" colspan="3">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
            <ComponentArt:Calendar runat="server" ID="Calendar2" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

            <script type="text/javascript">
            // Associate the picker and the calendar:
            function ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()
            {
              if (window.<%= Calendar2.ClientObjectId %>_loaded && window.<%= dtp_Bill_Date.ClientObjectId %>_loaded)
              {
                window.<%= Calendar2.ClientObjectId %>.AssociatedPicker = <%= dtp_Bill_Date.ClientObjectId %>;
                window.<%= dtp_Bill_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar2.ClientObjectId %>;
              }
              else
              {
                window.setTimeout('ComponentArt_<%= Calendar2.ClientObjectId %>_Associate()', 100);
              }
            }
             ComponentArt_<%= Calendar2.ClientObjectId %>_Associate();
            </script>

        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
            <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
        </td>
    </tr>
    <tr id="td_gcalreadyupdated" runat="server">
        <td class="TD1" id="td_GetGCUpdated" runat="server" style="width: 20%">
            <asp:Label ID="lbl_GetGCUpdated" runat="server" Text="GC Already Updated :" />
        </td>
        <td id="td_GetGCUpdated_data" runat="server" style="width: 80%" colspan="5">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="txt_GetGCUpdated" runat="server" CssClass="TEXTBOX" Height="30px"
                        TextMode="MultiLine" BorderWidth="1px" ReadOnly="true" /></td>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%;">
            <asp:Panel ID="pnl_OctroiUpdate" runat="server" GroupingText="Octroi Update Details" CssClass="PANEL" Width="100%">
                <div id="Div_octroi" class="DIV" style="height: 200px">
                    <asp:UpdatePanel ID="Upd_pnl_OctroiUpdate" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DataGrid ID="dg_OctroiUpdate" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                CssClass="GRID" Style="border-top-style: none" Width="110%" OnItemDataBound="dg_OctroiUpdate_ItemDataBound">
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <Columns>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Octroi_Form_Type_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Octroi_Form_Type_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Octroi_Paid_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Octroi_Paid_By_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_GC_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GC_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="GC No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_GCNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>'
                                                CssClass="LABEL"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Bkg Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BookingDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Date") %>'
                                                CssClass="LABEL"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Bkg Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BookingBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Booking_Branch") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Dly Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DeliveryBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Delivery_Branch") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Octroi Form Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_OctroiFormType" CssClass="DROPDOWN" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Octroi Paid By">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_OctroiPaidBy" CssClass="DROPDOWN" onchange="CalculateAmount()" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can Edit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CanEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Can_Edit") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Octroi Receipt No">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_OctroiReceiptNo" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Receipt_No") %>' CssClass="TEXTBOXNOS" MaxLength="10"/>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Octroi Amt.">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_OctroiAmount" onblur="CalculateAmount()" onkeypress="return Only_Numbers(this,event)" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Amount") %>' CssClass="TEXTBOXNOS" MaxLength="10"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_Remark" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Remark") %>' CssClass="TEXTBOX" MaxLength="30"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%" />
                                        <HeaderStyle HorizontalAlign="Left"/>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                             <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_TotalGC" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True" />
        </td>
        <td style="width: 29%">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL" Font-Bold="True" />
                    <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="dg_OctroiUpdate" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
             <asp:Label ID="lbl_TotAmt" runat="server" Text="Total Octroi Amount :" CssClass="LABEL" Font-Bold="True" />
        </td>
        <td style="width: 29%" >
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_TotalAmt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                    <asp:HiddenField ID="hdn_TotalAmount" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                    <asp:AsyncPostBackTrigger ControlID="dg_OctroiUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="btnAmount" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>   
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td class="TD1" colspan="6">
        <asp:Panel ID="Panel1" runat="server" GroupingText="Other Charge Details" CssClass="PANEL" Width="100%">
            <uc3:WucOtherChargeLedger ID="WucOtherChargeLedger1" runat="server" />
        </asp:Panel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr id="tr_ChequeDetails" runat="server">
     <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ChequeNo" runat="server" CssClass="LABEL" Text="Cheque No:"></asp:Label>
        </td>
        <td style="width:29%">
        <asp:TextBox ID="txt_ChequeNo" runat="server" CssClass="TEXTBOX" />
        </td>
        <td  class="TDMANDATORY" style="width:1%">
        <asp:Label ID="lbl_MandatoryChequeNo" runat="server" CssClass="LABEL" Text="*" />
        </td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ChequeDate" runat="server" CssClass="LABEL" Text="Cheque Date :"></asp:Label>
        </td>
        <td style="width: 29%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="dtp_ChequeDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" SelectedDate="2008-10-20">
                        </ComponentArt:Calendar>
                    </td>
                    <td style="height: 24px">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
                
            </table>
        </td>
        
    </tr>
     <tr>
        <td class="TD1" style="width: 50%" colspan="3">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
            <ComponentArt:Calendar runat="server" ID="Calendar1" AllowMultipleSelection="False"
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
              if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_ChequeDate.ClientObjectId %>_loaded)
              {
                window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_ChequeDate.ClientObjectId %>;
                window.<%= dtp_ChequeDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
    
    <tr id="tr_BankName" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_BankName" runat="server" CssClass="LABEL" Text="Name Of Bank:"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_BankName" runat="server" CssClass="TEXTBOX"  />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_MandatoryBankName" runat="server" CssClass="LABEL" Text="*" />
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%"/>
        <td style="width:1%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="5" style="width: 79%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="5" style="width: 79%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="5" style="width: 79%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Total_Amount" runat="server" CssClass="LABEL" Text="Grand Total :" Font-Bold="true"></asp:Label>
        </td>
        <td colspan="5" style="width: 79%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <ContentTemplate>
          <asp:TextBox ID="txt_Total_Amount_Value" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="20%" Text="0"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="5" style="width: 79%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
       <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
        </td>
        <td colspan="5" style="width: 79%">
            <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine"
                MaxLength="100"></asp:TextBox>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td colspan="6">
           <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="S" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" AccessKey="E" OnClick="btn_Close_Click"/>
        </td>
    </tr>
    <tr id="tr_btn_GridSummary" runat="server">
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btnAmount" runat="server" CssClass="BUTTON" Text="TotalAmount" OnClick="btnAmount_Click" />&nbsp
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are Mandatory"></asp:Label>&nbsp;&nbsp;
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript"> 
    var tr_btn_GridSummary = document.getElementById('WucOctroiUpdate1_tr_btn_GridSummary');
    tr_btn_GridSummary.style.visibility = 'hidden'; 

</script>

