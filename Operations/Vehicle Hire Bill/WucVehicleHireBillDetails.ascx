<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleHireBillDetails.ascx.cs" Inherits="Operations_Vehcile_Hire_Bill_WucVehicleHireBillDetails" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js" language="javascript"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Vehicle Hire Bill/VehicleHireBill.js"></script>

<asp:ScriptManager ID="scm_VehicleHireBillDetails" runat="server"></asp:ScriptManager>

<script type="text/javascript" language="javascript">
function SetCommenceDateToLabel()
 {
    var lbl_CommitedDelDateValue = document.getElementById("<%=lbl_CommitedDelDateValue.ClientID %>");
    var txt_TransitDays = document.getElementById("<%=txt_TransitDays.ClientID%>");
    var hdn_CommitedDelDate= document.getElementById("<%=hdn_CommitedDelDate.ClientID%>");
    var HireBillDate=new Date();
    HireBillDate = <%=WucHireBillDate.ClientID%>.GetSelectedDate();
    var CommitedDelDate=new Date();
    CommitedDelDate.setDate(HireBillDate.getDate());
    CommitedDelDate.setMonth(HireBillDate.getMonth());
    CommitedDelDate.setFullYear(HireBillDate.getFullYear());
    CommitedDelDate.setDate(CommitedDelDate.getDate()+Math.ceil(txt_TransitDays.value));    
    lbl_CommitedDelDateValue.innerHTML=(CommitedDelDate).format('MMMM dd, yyyy');
    hdn_CommitedDelDate.value=(CommitedDelDate).format('MMMM dd, yyyy');
    
 }
 </script>
 
 <script type="text/javascript">
    function viewwindow(Path)            
    {        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
      window.open(Path, 'ViewDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      return false;
    }    

</script>
<script type="text/javascript">
function ShowAlert()
{
 alert('There Are No Details To Be Displayed');
 return false;
}
</script>

<table style="width: 100%" class="TABLE">
 <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="VEHICLE HIRE BILL"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_HireBillNo" runat="server" CssClass="LABEL" Text="Hire Bill No:"
                ></asp:Label></td>
        <td style="width: 30%">
            <asp:Label ID="lbl_VehicleHireBillNoValue" runat="server" CssClass="LABEL" />
            </td>
        <td style="width: 2%">           
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_HireBillDate" runat="server" CssClass="LABEL" Text="Hire Bills Date:" ></asp:Label></td>
        <td style="width: 28%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <ComponentArt:Calendar ID="WucHireBillDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                            ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                            PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True"
                            OnSelectionChanged="WucHireBillDate_SelectionChanged"> 
                        </ComponentArt:Calendar>
                    </td>
                    <td style="height: 24px" runat="server" id="TD_Calender">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 30%">
        </td>
        <td class="TDMANDATORY" style="width: 2%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 28%">
            <ComponentArt:Calendar ID="Calendar" runat="server" AllowMonthSelection="False" AllowMultipleSelection="False"
                AllowWeekSelection="False" CalendarCssClass="CALENDER" CalendarTitleCssClass="TITLE"
                ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" ControlType="Calendar"
                DayCssClass="DAY" DayHeaderCssClass="DAYHEADER" DayHoverCssClass="DAYHOVER" DayNameFormat="FirstTwoLetters"
                ImagesBaseUrl="../../images/" MonthCssClass="MONTH" NextImageUrl="cal_nextMonth.gif"
                NextPrevCssClass="NEXTPREV" OtherMonthDayCssClass="OTHERMONTHDAY" PopUp="Custom"
                PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="SELECTEDDAY" SwapDuration="300">
            </ComponentArt:Calendar>

            <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= WucHireBillDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= WucHireBillDate.ClientObjectId %>;
                            window.<%= WucHireBillDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
            </script>

        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_VehicleNo" runat="server" CssClass="LABEL" Text="Vehicle No:"
                ></asp:Label></td>
        <td style="width: 30%">
            <asp:UpdatePanel ID="Upd_Pnl_WucVehicleSearch1" UpdateMode="Conditional" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_VehicleCapacity" runat="server" CssClass="LABEL" Text="Vehicle Capacity:"
                ></asp:Label></td>
        <td style="width: 28%">
            <asp:UpdatePanel ID="Upd_Pnl_lbl_VehicleCapacityValue" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_VehicleCapacityValue" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%">
        </td>
    </tr>  
  
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_RefNo" runat="server" CssClass="LABEL" Text="Ref. No:"
                ></asp:Label></td>
        <td style="width: 30%">
            <asp:UpdatePanel ID="Upd_Pnl_txt_ManualRefNo" UpdateMode="Conditional" runat="server">
                <Triggers>
                    
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_ManualRefNo" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 28%">            
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_FromLocation" runat="server" CssClass="LABEL" Text="From Location:"
                ></asp:Label></td>
        <td style="width: 30%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_FromLocation" UpdateMode="Conditional" runat="server">
                <Triggers>
                    
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_FromLocation" runat="server" AllowNewText="True" CallBackAfter="2"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetServiceLocation"
                        InjectJSFunction="" IsCallBack="True" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToLocation" runat="server" CssClass="LABEL" Text="To Location:"
                ></asp:Label></td>
        <td style="width: 28%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_ToLocation" UpdateMode="Conditional" runat="server">
                <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_ToLocation" runat="server" AllowNewText="True" CallBackAfter="2"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetServiceLocation" InjectJSFunction=""
                        IsCallBack="True" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *
        </td>
    </tr>
   
    <tr id="Tr1" runat="server">
        <td runat="server" id="td_Broker" colspan="6" width="100%">
            <asp:UpdatePanel ID="Upd_Pnl_BrokerVisible" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 20%;" class="TD1">
                                <asp:Label ID="lbl_BrokerName" runat="server" CssClass="LABEL" Text="Broker Name:"
                                    ></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <asp:DropDownList ID="ddl_BrokerName" runat="server" CssClass="DROPDOWN" 
                                    OnSelectedIndexChanged="ddl_BrokerName_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 2%" class="TDMANDATORY">
                                <asp:Label ID="lbl_Man1" runat="server" Text="*"></asp:Label>
                            </td>
                            <td style="width: 20%" class="TD1">
                                
                            </td>
                            <td style="width: 28%">                               
                            </td>
                            <td style="width: 2%" class="TDMANDATORY">                              
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Driver1" runat="server" CssClass="LABEL" Text="Driver 1:"></asp:Label></td>
        <td style="width: 30%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_Driver1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />                    
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_Driver1" runat="server" AllowNewText="True" CallBackAfter="2"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDriver" InjectJSFunction=""
                        IsCallBack="True" PostBack="False" Text="" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtn_Driver1Details" Font-Bold="true" runat="server" Text="Details"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">           
        </td>       
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_Driver2" runat="server" CssClass="LABEL" Text="Driver 2:"></asp:Label></td>
        <td style="width: 30%;">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_Driver2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />                    
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_Driver2" runat="server" AllowNewText="True" CallBackAfter="2"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDriver" InjectJSFunction=""
                        IsCallBack="True" PostBack="False" Text="" />
                        &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtn_Driver2Details" Font-Bold="true" runat="server" Text="Details"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%;" class="TDMANDATORY">
        </td>
        <td style="width: 20%;" class="TD1">
        </td>
        <td style="width: 28%;">
        </td>
        <td style="width: 2%;">
        </td>
    </tr>
   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Cleaner" runat="server" CssClass="LABEL" Text="Cleaner:" ></asp:Label></td>
        <td style="width: 30%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_Cleaner" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_Cleaner" runat="server" AllowNewText="True" CallBackAfter="2"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetCleaner" InjectJSFunction=""
                        IsCallBack="True" PostBack="False" Text="" />
                         &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtn_CleanerDetails" Font-Bold="true" runat="server" Text="Details"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 30%">
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:UpdatePanel ID="Upd_Pnl_pnl_VehicleHireDetails" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                    
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="pnl_VehicleHireDetails" runat="server" Width="100%" GroupingText="Vehicle Hire Details">
                        <table width="100%">
                            <tr>
                                <td style="width: 41.1%" class="TD1">
                                    <asp:Label ID="lbl_FreightType" runat="server" CssClass="LABEL" Text="Freight Type:"></asp:Label></td>
                                <td style="width: 58%">
                                    <asp:DropDownList ID="ddl_FreightType" Width="93%" onchange="EnabledDisabledControlOnFreightType();CalculateTruckHireCharge(1);" runat="server" CssClass="DROPDOWN">
                                    </asp:DropDownList>&nbsp;<font color="red" style="font-weight: bold; font-family: Verdana;font-size: 11px">*</font>                              
                                     <asp:HiddenField ID="hdn_FreightType" runat="server" />                             
                                      </td>
                            </tr>
                            <tr id="tr_WtGuarantee" runat="server">
                                <td id="Td1" style="width: 41.1%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_WtGuarantee" runat="server" CssClass="LABEL" Text="Wt. Guarantee: "></asp:Label></td>
                                <td id="Td2" style="width: 58%" runat="server">
                                    <asp:TextBox ID="txt_WtGuarantee" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                                        onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>                                     
                                        <asp:HiddenField ID="hdn_WtGuarantee" runat="server" />
                                        </td>
                            </tr>
                            <tr id="tr_RateKg" runat="server">
                                <td id="Td3" style="width: 41.1%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_RateKg" runat="server" CssClass="LABEL" Text="Rate/Kg:"></asp:Label></td>
                                <td id="Td4" style="width: 58%" runat="server">
                                    <asp:TextBox ID="txt_RateKg" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                                        onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>
                                        <asp:HiddenField ID="hdn_RateKg" runat="server" />
                                        </td>
                            </tr>
                            <tr id="tr_ActualKms" runat="server">
                                <td id="Td5" style="width: 41.1%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_ActualKms" runat="server" CssClass="LABEL" Text="Actual  Kms:"></asp:Label></td>
                                <td id="Td6" style="width: 58%" align="left" runat="server">
                                <asp:TextBox ID="txt_ActualKmsValue" onblur="CalculateTruckHireCharge(0)" onkeypress="return Only_Numbers(this,event);"
                                runat="server" Width="93%" CssClass="TEXTBOXNOS">0</asp:TextBox>
                                <asp:HiddenField ID="hdn_ActualKms" runat="server" />                                
                                </td>
                            </tr>
                            <tr id="tr_ActualWtPayable" runat="server">
                                <td id="Td7" style="width: 41.1%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_ActualWtPayable" runat="server" CssClass="LABEL" Text="Actual Wt. Payable:"></asp:Label></td>
                                <td id="Td8" style="width: 58%" align="left" runat="server">
                                    <asp:Label ID="lbl_ActualWtPayableValue" runat="server" Width="93%" CssClass="TEXTBOXNOS"
                                        Font-Bold="True">0</asp:Label>
                                    <asp:HiddenField ID="hdn_ActualWtPayable" runat="server" />
                                </td>
                            </tr>
                            <tr id="tr_TruckHireCharge" runat="server">
                                <td id="Td9" style="width: 41.1%; height: 41px;" class="TD1" runat="server">
                                    <asp:Label ID="lbl_TruckHireCharge" runat="server" CssClass="LABEL" Text="Truck Hire Charge:"></asp:Label></td>
                                <td id="Td10" style="width: 58%; height: 41px;" align="left" runat="server">
                                    <asp:Label ID="lbl_TruckHireChargeValue" runat="server" Width="93%" CssClass="TEXTBOXNOS"
                                        Font-Bold="True">0</asp:Label>
                                    <asp:HiddenField ID="hdn_TruckHireCharge" runat="server" />
                                </td>
                            </tr>
                            <tr id="tr_txt_TruckHireCharge" runat="server">
                                <td id="Td11" style="width: 41.1%" class="TD1" runat="server">
                                    <asp:Label ID="lbl_txt_TruckHireCharge" runat="server" CssClass="LABEL" Text="Truck Hire Charge:"></asp:Label></td>
                                <td id="Td12" style="width: 58%" runat="server">
                                    <asp:TextBox ID="txt_TruckHireCharge" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                                        onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>
                                    &nbsp;<font color="red" style="font-weight: bold; font-family: Verdana; font-size: 11px">*</font></td>
                            </tr>                           
                            <tr>    
                            
                             <td colspan="2">                          
                                   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_FreightType" />
                                        
                                        
                                    </Triggers>
                                    <ContentTemplate>                              
                                     <table border="0" style="width: 100%">
                                     
                                        <tr>
                                            <td class="TD1" style="width: 41.1%">
                                                <asp:Label ID="lbl_TDSPer" runat="server" Text="TDS %:" ></asp:Label></td>
                                                                                      
                                            <td style="width: 58%" align="right">
                                            <asp:Label ID="lbl_TDSPerValue" runat="server" Text="0" Style="text-align: right"
                                                    Font-Bold="True" ></asp:Label>
                                                <asp:HiddenField ID="hdn_TDSPer" runat="server" />                                                
                                                    </td>
                                                     <td style="width: 10%">
                                                %</td>
                                        </tr>                         
                                       </table>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                   
                                </td>
                                
                            </tr>
                              
                            <tr>
                                <td class="TD1" style="width: 41.1%">
                                    <asp:Label ID="lbl_TDSAmount" runat="server" Text="Total TDS Amount:" ></asp:Label></td>
                                <td style="width: 58%" align="right">
                                    <asp:Label ID="lbl_TDSAmountValue" runat="server" Text="0" Font-Bold="True" ></asp:Label></td>
                                    <asp:HiddenField ID="hdn_TDSAmountValue" runat="server" />
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 41.1%">
                                </td>
                                <td style="width: 58%" align="right">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 41.1%">
                                    <asp:Label ID="lbl_TotalTruckHire" runat="server" Text="Total Truck Hire:" ></asp:Label></td>
                                <td style="width: 58%" align="right">
                                    <asp:Label ID="lbl_TotalTruckHireValue" runat="server" Text="0" Font-Bold="True"
                                        ></asp:Label></td>
                                        <asp:HiddenField ID="hdn_TotalTruckHire" runat="server" />
                            </tr>                           
                        </table>
                        
                    </asp:Panel>                     
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        
        <td style="width: 2%;" class="TDMANDATORY">
        </td>
        <td colspan="2" style="vertical-align:top">
            <asp:UpdatePanel ID="Upd_Pnl_pnl_TotalTDSCalculation" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="pnl_TotalTDSCalculation" runat="server" Width="100%" GroupingText=" ">
                        <table style="width: 100%;">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_AdvanceReceived" runat="server" Text="Advance Received:" CssClass="LABEL"></asp:Label></td>
                                <td style="width: 28%;" align="left">
                                    <asp:TextBox ID="txt_AdvanceReceived" Width="60%" runat="server" 
                                         onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOXNOS"
                                        MaxLength="18">0</asp:TextBox></td>
                                        <asp:HiddenField ID="hdn_AdvanceReceived" runat="server" />
                                <td style="width: 2%" />
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                </td>
                                <td style="width: 28%">
                                </td>
                                <td style="width: 2%" />
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_BrokeragePayable" runat="server" CssClass="LABEL">Brokerage Payable:</asp:Label></td>
                                <td style="width: 28%;" align="left">
                                     <asp:TextBox ID="txt_BrokeragePayable" Width="60%" runat="server" onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOXNOS"
                                        MaxLength="18">0</asp:TextBox></td>
                                        <asp:HiddenField ID="hdn_BrokeragePayable" runat="server" />
                                <td style="width: 2%" />
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_CollectionChargesPayable" runat="server" CssClass="LABEL">Collection Charges Payable:</asp:Label>
                                </td>
                                <td style="width: 28%;" align="left">
                                 <asp:HiddenField ID="hdn_CollectionChargesPayable" runat="server" />
                                    <asp:TextBox ID="txt_CollectionChargsPayableValue" Width="60%" runat="server"  onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOXNOS" >0</asp:TextBox>
                                </td>
                                <td style="width: 2%" />
                            </tr>                          
                        </table>
                   </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%;">
        </td>
    </tr>
       <tr>
        <td colspan="6">
            <asp:Panel ID="Panel4" runat="server" GroupingText=" " Width="100%" >
                <table style="width: 100%">
                <tr>
                  <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_VehicleDepartureTime" runat="server" Text="Vehicle Departure Time:"
                                ></asp:Label></td>
                        <td style="width: 28%" runat="server" id="td_DepartureTime">
                            <asp:UpdatePanel ID="Upd_Pnl_Wuc_VehicleDepartureTime" UpdateMode="Conditional" runat="server">
                                <Triggers>                                                              
                                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                </Triggers>
                                <ContentTemplate>
                                    <uc4:TimePicker ID="Wuc_VehicleDepartureTime" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 2%">
                        </td>
                        
                    </tr>               
                    <tr>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_TransitDays" runat="server" Text="Transit Days:" ></asp:Label>
                        </td>
                        <td style="width: 28%">
                            <asp:UpdatePanel ID="Upd_Pnl_txt_TransitDays" UpdateMode="Conditional" runat="server">
                                <Triggers>                                                                       
                                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_TransitDays" runat="server" onkeypress="return Only_Numbers(this,event);"
                                         onblur="SetCommenceDateToLabel()" CssClass="TEXTBOXNOS" Text="0" MaxLength="4"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </td>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_CommitedDelDate" runat="server" Text="Committed Del. Date:" ></asp:Label></td>
                            <td style="width: 28%">
                                <asp:UpdatePanel ID="Upd_Pnl_lbl_CommitedDelDateValue" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_CommitedDelDateValue" runat="server" Font-Bold="True" ></asp:Label>
                                        <asp:HiddenField ID="hdn_CommitedDelDate" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 2%">
                            </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%">
                        </td>
                        <td style="width: 28%">
                        </td>
                        <td style="width: 2%">
                        </td>
                        <td class="TD1" style="width: 20%">
                        </td>
                        <td style="width: 28%">
                        </td>
                        <td style="width: 2%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
      <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Remark" runat="server" CssClass="LABEL" Text="Remark:" ></asp:Label></td>
        <td colspan="4">
            <asp:UpdatePanel ID="Upd_Pnl_txt_Remark" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_Remark" runat="server" CssClass="TEXTBOX" MaxLength="250" 
                       Height="40px" TextMode="MultiLine" ></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
    <td colspan="6" />
    </tr>
    <tr>
<td class="TD1" colspan="6" style="text-align: center">

   <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save"  OnClick="btn_Save_Click"/>&nbsp
   <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>

</td>
</tr>

    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                ></asp:Label>&nbsp;
            <asp:UpdatePanel ID="Upd_Pnl_HIDDENFields" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />

                </Triggers>
                <ContentTemplate>                                      
                </ContentTemplate>
            </asp:UpdatePanel>
            &nbsp;&nbsp;
        </td>
    </tr>
 



 </table>
<asp:HiddenField ID="hdn_Next_No" runat="server" />
<asp:HiddenField ID="hdn_Padded_Next_No" runat="server" />
<asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" />
<asp:HiddenField ID="hdn_StartNo" runat="server" />
<asp:HiddenField ID="hdn_EndNo" runat="server" />

<script type="text/javascript">
EnabledDisabledControlOnFreightType();
</script>

