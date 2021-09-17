<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucATH.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucATH" %>
<%@ Register Src="~/Finance/Accounting Vouchers/WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type ="text/javascript"  src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="scm_ATH" runat="server"></asp:ScriptManager>

<script type="text/javascript">

function GetTotalAmount()
{
    var txt_Total_PaidAmount = document.getElementById('<%=txt_TotalPaidAmt.ClientID %>');
    
    return val(txt_Total_PaidAmount.value);
}

function OnFocus_TotalPaidAmt()
{
    var txt_Total_PaidAmount = document.getElementById('<%=txt_TotalPaidAmt.ClientID %>');
    var txt_CashAmount = document.getElementById('WucATH1_WucMRCashChequeDetails1_txt_CashAmount');
    var txt_ChequeAmount = document.getElementById('WucATH1_WucMRCashChequeDetails1_txt_ChequeAmount');
    var hdn_CashAmount = document.getElementById('WucATH1_WucMRCashChequeDetails1_hdn_CashAmount');
    var hdn_ChequeAmount = document.getElementById('WucATH1_WucMRCashChequeDetails1_hdn_ChequeAmount');
    var txt_CreditAmount= document.getElementById('WucATH1_WucMRCashChequeDetails1_txt_CreditAmount');
    
    if(val(txt_Total_PaidAmount.value) < 0 || txt_Total_PaidAmount.value == '')
    {
        txt_CashAmount.value = '0';
        txt_ChequeAmount.value = '0';
    }
    if(val(txt_Total_PaidAmount.value) > 0)
    {
            txt_CashAmount.value = val(txt_Total_PaidAmount.value) - val(txt_ChequeAmount.value);
            
            if (val(txt_CashAmount.value) <= 0)
                txt_CashAmount.value = 0;

            hdn_CashAmount.value = val(txt_CashAmount.value);
    }
}

</script>

<script type="text/javascript">
function Allow_To_Save()
{    
   var ddl_Ledger = document.getElementById('WucATH1_ddl_Ledger_txtBoxddl_Ledger');
   var txt_CreditAmount= document.getElementById('WucATH1_txt_CreditAmount');
   if (ddl_Ledger.value != '')
    {
         if(val(txt_CreditAmount.value) < 0 || txt_CreditAmount.value == '')
         {
            txt_CreditAmount.focus();
            lbl_Error_Client.innerText='Please Enter Credit Amount To';
            return false;
        }
    }
   return true;
}
</script>


<table class="TABLE"> 
    <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Advance Truck Hire(ATH)" meta:resourcekey="lbl_headingResource1"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_ATHVoucher_No" runat="server" CssClass="LABEL" Text="ATH Voucher No :" meta:resourcekey="lbl_ATHVoucher_NoResource1"></asp:Label>  </td>
        <td style="width: 29%;">
            <asp:Label ID="lbl_ATHVoucherNo" runat="server" Font-Bold="True" meta:resourcekey="lbl_ATHVoucherNoResource1"></asp:Label></td>
        <td class="TD1" style="width:1%;"></td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="ATH Voucher Date :" meta:resourcekey="lbl_DateResource1"></asp:Label>
        </td>
        <td style="width: 29%;" class="TDMANDATORY">
             <table border="0" cellpadding="0">
                 <tr>
                     <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px" >
                        <ComponentArt:Calendar ID="ATHV_Date" runat="server" 
                             CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                             ControlType="Picker" PickerCssClass="PICKER" 
                             PickerCustomFormat="dd MMM yyyy"
                             PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="ATHV_Date_SelectionChanged"> 
                        </ComponentArt:Calendar>
                     </td>
                     <td style="height: 24px" >
                         <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                         onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif" width="25" />
                     </td>
                 </tr>
              </table>
          </td>                
        <td class="TDMANDATORY" style="width: 1%"></td>
    </tr> 
     <tr>
        <td class="TD1" style="width: 20%"></td>
        <td  style="width: 29%"> </td>
        <td class="TD1"> </td>
        <td class="TD1" style="width: 20%"> </td>
        <td  style="width: 29%">
            <ComponentArt:Calendar runat="server" id="Calendar" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom"  CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" 
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" 
                OtherMonthDayCssClass="OTHERMONTHDAY" SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" 
                NextPrevCssClass="NEXTPREV" MonthCssClass="MONTH" SwapDuration="300"
                DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />
                <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= ATHV_Date.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= ATHV_Date.ClientObjectId %>;
                        window.<%= ATHV_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                      }
                      else
                      {
                        window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                      }
                    }
                     ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
               </script>   
        </td>
        <td  style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Reference_no" runat="server" CssClass="LABEL" Text="Reference No :" meta:resourcekey="lbl_Reference_noResource1"></asp:Label>
        </td>
        <td  style="width: 29%">
            <asp:TextBox ID="txt_ReferenceNo" CssClass="TEXTBOX" runat="server" BorderWidth="1px" MaxLength="50" meta:resourcekey="txt_ReferenceNoResource1"/></td>
        <td class="TDMANDATORY"></td>
        <td class="TD1" style="width: 50%" colspan="3"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Vehicle_no" runat="server" CssClass="LABEL" Text="Vehicle No :" meta:resourcekey="lbl_Vehicle_noResource1"></asp:Label>
        </td>
        <td style="width: 29%;">       
            <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />             
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_vehicle_cotegory" runat="server" CssClass="LABEL" Text="Vehicle Category :"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <asp:Label ID="lbl_VehicleCotegory" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_VehicleCotegoryResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>       
    </tr>
    <tr>
         <td class="TD1" style="width: 50%" colspan="3"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_owner_text" runat="server" CssClass="LABEL" Text="Vehicle Owner :"></asp:Label>
        </td>
        <td style="width: 29%;">
        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="lbl_Owner" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_OwnerResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>        
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
      
            <asp:Label ID="lbl_LHPO_no" runat="server" CssClass="LABEL"></asp:Label>
        </td>
        <td style="width: 29%;">
           <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:DropDownList ID="ddl_LHPONo" AutoPostBack="true" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_LHPONoResource1" OnSelectedIndexChanged="ddl_LHPONo_SelectedIndexChanged"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>       
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LHPO_date" runat="server" CssClass="LABEL"></asp:Label>
        </td>
        <td style="width: 29%;">
         <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <asp:Label ID="lbl_LHPODate" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_LHPODateResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Manual_Ref_No" runat="server" CssClass="LABEL" Text="Manual Ref. No :" meta:resourcekey="lbl_Manual_Ref_NoResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
         <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
            <asp:Label ID="lbl_ManualRefNo" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_ManualRefNoResource1"></asp:Label>
            </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>       
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Driver_1" runat="server" CssClass="LABEL" Text="Driver 1 :" meta:resourcekey="lbl_Driver_1Resource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <asp:Label ID="lbl_Driver1" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_Driver1Resource1"></asp:Label>
            </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
     <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_advance_payable" runat="server" CssClass="LABEL" Text="Advance Payable :" meta:resourcekey="lbl_advance_payableResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="lbl_AdvancePayable" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_AdvancePayableResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="lbl_broker_text" runat="server" CssClass="LABEL" Text="LHPO Broker :"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td style="width: 29%;">
         <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="lbl_Broker" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_BrokerResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width:1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_total_paid_amt" runat="server" CssClass="LABEL" Text="Total Paid Amount :"></asp:Label>
        </td>
        <td style="width: 29%;">
             <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always"><%--onmouseclick="OnFocus_TotalPaidAmt()" onblur="OnFocus_TotalPaidAmt()"--%>
             <ContentTemplate>
                    <asp:TextBox ID="txt_TotalPaidAmt" onkeypress="return Only_Numbers(this,event)" Width="60%" MaxLength="8" runat="server" CssClass="TEXTBOXNOS"></asp:TextBox>
             </ContentTemplate>
            <%-- <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
               <asp:AsyncPostBackTrigger ControlID="ATHV_Date" />
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>--%>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 50%" colspan="3"></td>
    </tr>
    <tr>
        <td colspan="5">
            <uc1:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
        </td>
        <td colspan="1"></td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
            <table width="100%">
            <tr id="tr_CreditToLedger" runat="server">
                <td class="TD1" style="width: 20%">
                    <asp:Label ID="lbl_CreditToLedgerId" runat="server" Text="Credit To Ledger:"></asp:Label></td>
                <td style="width: 29%" id="td_ddl_Ledger" runat="server">
                    <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetATHLedger" IsCallBack="true" />
                </td>
                <td style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                    <asp:Label ID="lbl_CreditAmount" runat="server" Text="Credit Amount:"></asp:Label></td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_CreditAmount" CssClass="TEXTBOXNOS" Width="50%" MaxLength="10" onkeypress="return Only_Numbers(this,event)" runat="server"></asp:TextBox></td>
                <td style="width: 1%">
                </td>
            </tr>    
            </table> 
           </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
             </Triggers>
           </asp:UpdatePanel>    
        </td>    
    </tr>
    <tr runat="server" id="tr_petrol_grid">
        <td colspan="6" >
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>   
                <asp:Panel id ="pnp_Petrol" runat="server" CssClass="PANEL">
                <asp:DataGrid ID="dg_Petrol" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="GRID" style="border-top-style: none" 
                Width="98%" ShowFooter="True" OnCancelCommand="dg_Petrol_CancelCommand" OnDeleteCommand="dg_Petrol_DeleteCommand" OnEditCommand="dg_Petrol_EditCommand" OnItemCommand="dg_Petrol_ItemCommand" OnItemDataBound="dg_Petrol_ItemDataBound" OnUpdateCommand="dg_Petrol_UpdateCommand" meta:resourcekey="dg_PetrolResource1">
                
                <FooterStyle CssClass="GRIDFOOTERCSS" />
                <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                <HeaderStyle CssClass="GRIDHEADERCSS"/>
                   <Columns>
                                    <asp:TemplateColumn HeaderText="Petrol Pump">
                                        <FooterTemplate>
                                             <cc1:DDLSearch ID="ddl_PetrolPump" CallBackAfter="2" DBTableName="EC_Master_Petrol_Pump" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPetrolPump" Text="" PostBack="False"/>
                                        </FooterTemplate>
                                         <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Petrol_Pump_Name") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                              <cc1:DDLSearch ID="ddl_PetrolPump" CallBackAfter="2" DBTableName="EC_Master_Petrol_Pump" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetPetrolPump" Text="" PostBack="False"/>
                                        </EditItemTemplate>
                                        <ItemStyle Width="20%"/>
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText = "Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="10" runat="server"></asp:TextBox> 
                                        </FooterTemplate>
                                         <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Petrol_Amount") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="10" runat="server" ></asp:TextBox> 
                                        </EditItemTemplate>  
                                        <ItemStyle Width="20%" />
                                        <HeaderStyle Width="20%"/>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText = "Slip No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_SlipNo" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" MaxLength="10" runat="server"></asp:TextBox> 
                                        </FooterTemplate>
                                         <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Petrol_Slip_No") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_SlipNo" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" MaxLength="10" runat="server"></asp:TextBox> 
                                        </EditItemTemplate> 
                                        <ItemStyle Width="20%" />
                                        <HeaderStyle Width="20%" />
                                   </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Remarks">
                                         <FooterTemplate>
                                            <asp:TextBox ID="txt_Remarks" CssClass="TEXTBOX" MaxLength="25" runat="server"></asp:TextBox> 
                                        </FooterTemplate>
                                         <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Remarks") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_Remarks" CssClass="TEXTBOX" MaxLength="25" runat="server"></asp:TextBox> 
                                        </EditItemTemplate> 
                                        <ItemStyle Width="30%" />
                                        <HeaderStyle Width="30%" />
                                    </asp:TemplateColumn>
                                  
                                    <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                        <HeaderStyle Width="10%" />
                                    </asp:EditCommandColumn>

                                    <asp:TemplateColumn HeaderText="Delete">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtn_Add" Text="Add" Runat="server" CommandName="Add"></asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateColumn>                                    
                        </Columns>
                </asp:DataGrid>
            </asp:Panel>
           </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="dg_Petrol" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr runat="server" id="tr_petrol_amt">
        <td class="TD1" style="width: 20%;">
              <asp:Label ID="lbl_Total_Amt"  runat="server" Text="Total Petrol Amount :" CssClass="LABEL"/>&nbsp;
        </td>
        <td colspan="2"> 
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="lbl_label" runat="server" Width="35%"></asp:Label><asp:Label ID="lbl_TotalPetrolAmt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"/>
                    <asp:HiddenField ID="hdn_StartNo" runat="server" />
                    <asp:HiddenField ID="hdn_EndNo" runat="server" />
                    <asp:HiddenField ID="hdn_NextNo" runat="server" />
                    <asp:HiddenField ID="hdn_Allocation_Id" runat="server" />
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="dg_Petrol" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="3"></td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
        </td>
        <td style="width:79%;" colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX" TextMode="MultiLine"
                Height="40px" MaxLength="250"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%;"></td>
    </tr>
    
    <tr>
        <td align="left" colspan="6" style="text-align: left">&nbsp;  
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label><br />&nbsp;
            </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="dg_Petrol" />
                  <%-- <asp:AsyncPostBackTrigger ControlID="btn_Save" />--%>
                 </Triggers>
           </asp:UpdatePanel>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>       
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="N" OnClick="btn_Save_Click"/>
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>

        </td>
    </tr>   
     <tr>
        <td colspan="6">
             <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="fields with * mark are mandatory"></asp:Label>
        </td>
    </tr>  
</table>