<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucGC.ascx.cs" Inherits="Operations_Booking_wucGC" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_GC" runat="server"></asp:ScriptManager>

<script type="text/javascript">

    function Get_Next_No()
    {
        document.getElementById('<%=btn_Get_GC_No.ClientID%>').click();
        return false;
    }
    function GetOtherCharges()
    {
        document.getElementById('<%=btn_GetOtherCharges.ClientID%>').click();
    }
    function SetDoor_Delivery_Adderess()
    {
        document.getElementById('<%=btn_SetDoorDeliveryAdderess.ClientID%>').click();
    } 
     
    function Set_Consignor_Consignee_Details(Client_Id,Is_Consignor)
    {    
        var hdn_ClientId = document.getElementById('wucGC1_hdn_ClientId');   
        var hdn_Is_Consignor = document.getElementById('wucGC1_hdn_Is_Consignor');   
        
        hdn_ClientId.value = Client_Id;
        hdn_Is_Consignor.value = Is_Consignor;
        
        document.getElementById('<%=btn_SetConsignorConsigneeDetails.ClientID%>').click();
    }   

    function Set_Location_Details(Location_Id,Location_Name,Is_FromLocation)
    {    
        var hdn_LocationId = document.getElementById('wucGC1_hdn_LocationId');   
        var hdn_LocationName = document.getElementById('wucGC1_hdn_LocationName');   
        var hdn_Is_FromLocation = document.getElementById('wucGC1_hdn_Is_FromLocation');   
        
        hdn_LocationId.value = Location_Id;
        hdn_LocationName.value = Location_Name ;    
        hdn_Is_FromLocation.value = Is_FromLocation ;        
        document.getElementById('<%=btn_SetLocationDetails.ClientID%>').click();
    }
       
    function Set_Commodity_Details(Commodity_Id,Commodity_Name)
    {    
        var hdn_CommodityId = document.getElementById('wucGC1_hdn_CommodityId');   
        var hdn_CommodityName = document.getElementById('wucGC1_hdn_CommodityName');     
        
        hdn_CommodityId.value = Commodity_Id;
        hdn_CommodityName.value = Commodity_Name ;        
        document.getElementById('<%=btn_SetCommodityDetails.ClientID%>').click();
    }  
     
    function Set_Item_Details(Item_Id,Item_Name,Commodity_Id)
    {    
        var hdn_ItemId = document.getElementById('wucGC1_hdn_ItemId');   
        var hdn_ItemName = document.getElementById('wucGC1_hdn_ItemName');   
        var hdn_CommodityId = document.getElementById('wucGC1_hdn_CommodityId');   
        
        hdn_ItemId.value = Item_Id;
        hdn_ItemName.value = Item_Name ;
        hdn_CommodityId.value = Commodity_Id;        
        document.getElementById('<%=btn_SetItemDetails.ClientID%>').click();
    }  
       
    function Validate_GCNo()
    {        
        var hdn_GC_No_For_Print = document.getElementById('wucGC1_hdn_GC_No_For_Print');   
        var txt_GC_No_For_Print = document.getElementById('wucGC1_txt_GC_No_For_Print');   
            
        var hdn_Is_Opening_GC = document.getElementById('wucGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucGC1_hdn_MenuItemId');
        
        var txt_Private_Mark = document.getElementById('wucGC1_txt_Private_Mark');   
        var hdn_Private_Mark = document.getElementById('wucGC1_hdn_Private_Mark');   
        
        txt_Private_Mark.value = txt_GC_No_For_Print.value
        hdn_Private_Mark.value = txt_GC_No_For_Print.value
        
        hdn_GC_No_For_Print.value = txt_GC_No_For_Print.value
        document.getElementById('<%=btn_ValidateGCNo.ClientID%>').click();
    }

    function Update_ServiceTaxDetails()
    {
        document.getElementById('<%=btn_Get_ServiceTaxDetails.ClientID%>').click();
    } 
    function ddl_ContractBranch_Change()
    {         
        document.getElementById('<%=btn_ContractBranch.ClientID%>').click();
    } 
</script>

<table class="TABLE" style="width: 100%;">
    <tr id="tr_Error1" runat="server">
     <td colspan="11" style="width: 100%">
              <asp:UpdatePanel ID="Upd_Error1" runat="server">
                <contenttemplate>
                    &nbsp;<asp:Label runat="server" CssClass="LABELERROR"  ID="lbl_Errors1"></asp:Label>                        
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_New"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit"></asp:AsyncPostBackTrigger>                     
                    <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_GCNo" CssClass="LABEL" Text="GC No :" runat="server" meta:resourcekey="lbl_GCNoResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
        
        <asp:UpdatePanel ID="upd_GC_No_For_Print" runat="server">
                <contenttemplate>                
            <asp:TextBox ID="txt_GC_No_For_Print" runat="server" onchange="Validate_GCNo();" Width="35%" onkeyPress="return Only_Integers(this,event);"
                CssClass="TEXTBOXNOS" MaxLength="8"></asp:TextBox>
                <asp:Label ID="lbl_mandatory_GC_No_For_Print" runat="server" Style="color:red " Text="*"></asp:Label>
            &nbsp; 
            <asp:Label ID="lbl_DocumentNextCounterNo" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label>               
            
            <%--<asp:LinkButton ID="lnk_Get_Next_No" runat="server" Text="Get Next No" TabIndex="9999" OnClientClick="return Get_Next_No();"></asp:LinkButton>--%>
                
            <asp:HiddenField ID="hdn_DocumentNextCounterNo" runat="server" Value="0" />            
            <asp:HiddenField ID="hdn_Next_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GC_No_For_Print" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Start_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />            
            </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Get_GC_No"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 11px">
            </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_CustomerRefNo" CssClass="LABEL" Text="Client Ref No :" runat="server"
                meta:resourcekey="lbl_CustomerRefNoResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:TextBox ID="txt_CustomerRefNo" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_BookingDate" CssClass="LABEL" Text="Booking Date :" runat="server"
                meta:resourcekey="lbl_BookingDateResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
                <table border="0" cellpadding="0">
                 <tr>
                     <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px" >
                        <ComponentArt:Calendar ID="wuc_BookingDate" runat="server" MinDate="1900-01-01" Width="95%"
                             CellPadding="2" AllowDaySelection="True" AllowMonthSelection="True" 
                             ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                             ControlType="Picker" AutoPostBackOnVisibleDateChanged="True" 
                             PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy" PickerFormat="Custom"
                             AutoPostBackOnSelectionChanged="True" OnSelectionChanged="wuc_BookingDate_SelectionChanged"> 
                        </ComponentArt:Calendar>
                     </td>
                     <td style="height: 24px" runat="server" id="TD_Calender">
                         <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                         onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                         width="25" />
                     </td>
                 </tr>
              </table>
            <asp:UpdatePanel ID="upd_hdn_BookingDate" runat="server">
                <contenttemplate>
                    <asp:HiddenField runat="server" ID="hdn_BookingDate"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="wuc_BookingDate"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_BookingDate" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_BookingTime" CssClass="LABEL" Text="Booking Time :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%">
            <uc2:TimePicker ID="wuc_BookingTime" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%"></td>
        <td  style="width: 29%"> </td>
        <td class="TD1" style="width: 11px"> </td>
        <td class="TD1" style="width: 20%"> </td>
        <td  style="width: 29%">
            <ComponentArt:Calendar runat="server" id="Calendar" AllowMultipleSelection="False"
                AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                PopUp="Custom"  CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged" 
                DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" 
                OtherMonthDayCssClass="OTHERMONTHDAY" SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" 
                NextPrevCssClass="NEXTPREV" MonthCssClass="MONTH" SwapDuration="300"
                DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif"/>
                <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wuc_BookingDate.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wuc_BookingDate.ClientObjectId %>;
                        window.<%= wuc_BookingDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%;">
            <asp:LinkButton ID="lnk_Get_Next_No" runat="server" Text="Get Next No" TabIndex="9999" 
                OnClientClick="return Get_Next_No();"></asp:LinkButton>
            <asp:LinkButton ID="lnk_View_Details" runat="server" Text="View Details" TabIndex="9999" 
                OnClientClick="return View_GC_Details('From Rectification');"></asp:LinkButton>            
        </td>
        <td class="TDMANDATORY" style="width: 11px">
        </td>
        <td class="TD1" style="width: 10%;">
        </td>
        <td style="width: 10%;">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%;">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
    </tr>
    
    <tr id="tr_Series" runat="server" >
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%;">
            <asp:Label ID="lbl_Series" CssClass="LABEL" Style="font-weight: bolder" runat="server"
                meta:resourcekey="lbl_SeriesResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 11px">
        </td>
        <td class="TD1" style="width: 10%;">
        </td>
        <td style="width: 10%;">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%;">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
    </tr>
    <tr id="tr_Copy_GC_Details" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_AttachedGCNo" CssClass="LABEL" Text="Old GC No :" runat="server"></asp:Label>
        </td>
        <td colspan="6">
            <asp:TextBox ID="txt_Attached_GC_No_For_Print" onblur="On_Attached_GC_No_For_Print_Change();"
                runat="server" Width="12%" onkeyPress="return Only_Integers(this,event);"
                CssClass="TEXTBOXNOS" MaxLength="8"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btn_GetAttachedGCDetails" runat="server" CssClass="BUTTON" Text="Get GC Details"
                OnClick="btn_GetAttachedGCDetails_Click" />
            &nbsp;
            <asp:CheckBox ID="chk_IsAttached" runat="server" onclick="On_IsAttached_Click(); "
                Text="Is Attached GC"></asp:CheckBox>
            <asp:HiddenField ID="hdn_IsAttached" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_AttachedGCId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_ReBookGCId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_IsReBookGC_ToPay" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_ReBookGC_SubTotal" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GC_Articles_At_Current_Branch" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GC_Status_Id_At_Current_Branch" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Actual_GC_Articles" runat="server" Value="0" />
        </td>       
        <td style="width: 10%;" id="td_chk_IsReBook" runat="server">
            <asp:CheckBox ID="chk_IsReBook" runat="server" onclick="On_IsReBooked_Click(); " Text="Is ReBook GC"></asp:CheckBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
    </tr>
    <tr id="tr_OtherAgencyGC" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_AgencyGCNo" CssClass="LABEL" Text="Agency GC No :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:TextBox ID="txt_AgencyGC_No_For_Print" runat="server" Width="40%" onkeyPress="return Only_Numbers(this,event);" BorderWidth="1px"
                CssClass="TEXTBOXNOS" onchange="On_Change_Agency_No();"></asp:TextBox>
                <asp:Label ID="lbl_mandatory_AgencyGC_No_For_Print" runat="server"  Style="color:red " Text="*"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_Agency" CssClass="LABEL" Text="Agency :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
                <cc1:DDLSearch ID="DDL_Agency" runat="server" CallBackAfter="2" Text="" IsCallBack="True" PostBack="true"
                        AllowNewText="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAgencyForOtherAgencyGC" OnTxtChange="DDL_Agency_TxtChange">
                </cc1:DDLSearch>                 
        </td>
        <td style="width: 1%;">
            <asp:Label ID="lbl_Agency_mandatory" runat="server"  Style="color:red " Text="*"></asp:Label>
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="Label1" CssClass="LABEL" Text="Agency Ledger :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <contenttemplate>  
                    <cc1:DDLSearch ID="DDL_AgencyLedger" runat="server" CallBackAfter="2" Text="" IsCallBack="True"
                        AllowNewText="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerForOtherAgencyGC">
                    </cc1:DDLSearch> 
             </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="DDL_Agency"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
        <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr id="tr_OpeningGC" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_BookingBranch" CssClass="LABEL" Text="Booking Branch :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
                <asp:UpdatePanel ID="upd_BookingBranch" runat="server">
                <contenttemplate>                
                    <cc1:DDLSearch ID="ddl_BookingBranch" runat="server" CallBackAfter="2" Text="" 
                        AllowNewText="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" 
                        IsCallBack="True" Width="95%" InjectJSFunction="On_FreightBasis_Change();" 
                        PostBack="True" OnTxtChange="ddl_BookingBranch_TxtChange">
                    </cc1:DDLSearch>                         
                    <asp:HiddenField runat="server" ID="hdn_BookingBranchId"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingBranch"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 11px">
            <asp:Label ID="lbl_Mandatory_BookingBranch" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_ArrivedFromBranch" runat="server" Text="Arrived From Branch :"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_ArrivedFromBranch" runat="server">
                <contenttemplate>                
                    <cc1:DDLSearch ID="ddl_ArrivedFromBranch" runat="server" CallBackAfter="2" Text="" 
                        AllowNewText="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" 
                        IsCallBack="True" Width="95%" InjectJSFunction="" PostBack="True" 
                        OnTxtChange="ddl_ArrivedFromBranch_TxtChange">
                    </cc1:DDLSearch>
                    <asp:HiddenField runat="server" ID="hdn_ArrivedFromBranchId"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ArrivedFromBranch"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_Mandatory_ArrivedFromBranch" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_ArrivedDate" CssClass="LABEL" Text="Arrived Date :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
            <ComponentArt:Calendar ID="wuc_ArrivedDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" 
                PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True" 
                MinDate="1900-01-01" Width="95%" AutoPostBackOnSelectionChanged="True" 
                OnSelectionChanged="wuc_ArrivedDate_SelectionChanged" AutoPostBackOnVisibleDateChanged="True" />
            <asp:UpdatePanel ID="upd_hdn_ArrivedDate" runat="server">
                <contenttemplate>
                    <asp:HiddenField runat="server" ID="hdn_ArrivedDate"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="wuc_ArrivedDate"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ArrivedDate" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
    </tr>
    
    <tr id="tr_ConsignementType" runat="server">
        <td class="TD1" style="width: 10%">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="Conditional">
              <contenttemplate>
                    <asp:Label ID="lbl_ConsignmentType" CssClass="LABEL" Text="Consignment Type :" runat="server"
                meta:resourcekey="lbl_ConsignmentTypeResource1"></asp:Label>
             </contenttemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
            </triggers>
        </asp:UpdatePanel>  
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_ddl_ConsignmentType" runat="server" updatemode="Conditional">
                <contenttemplate>
                    <asp:DropDownList ID="ddl_ConsignmentType" runat="server" CssClass="DROPDOWN" Width="95%">
                    </asp:DropDownList>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>            
        </td>
        <td class="TDMANDATORY" style="width: 11px">
            <asp:Label ID="lbl_Mandatory_ConsignmentType" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_BookingType" CssClass="LABEL" Text="Booking Type :" runat="server"
                meta:resourcekey="lbl_BookingTypeResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddl_BookingType" runat="server" AutoPostBack="true" onchange="On_BookingType_Change();"
                CssClass="DROPDOWN" Width="95%" OnSelectedIndexChanged="ddl_BookingType_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_BookingType" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_BookingSubType" CssClass="LABEL" Text="Booking Sub Type :" runat="server"
                meta:resourcekey="lbl_BookingSubTypeResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_ddl_BookingSubType" runat="server">
                <contenttemplate>
                    <asp:DropDownList ID="ddl_BookingSubType" runat="server" CssClass="DROPDOWN" Width="95%"></asp:DropDownList>
               </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingType"></asp:AsyncPostBackTrigger>          
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_BookingSubType" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%"></td>
        <td style="width: 10%"></td>
    </tr>    
    
    <tr id="tr_ContainerDetails" runat="server">
        <td class="TD1" style="width: 10%"></td>
        <td style="width: 10%;"></td>
        <td class="TDMANDATORY" style="width: 11px"></td>
        <td class="TD1" style="width: 10%;"></td>
        <td style="width: 10%;">
            <asp:LinkButton ID="lnk_ContainerDetails"  runat="server" 
                Text="Container Details" TabIndex="9999" OnClientClick="return ContainerDetails();">
            </asp:LinkButton>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 10%"></td>
        <td style="width: 10%;" ></td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 10%"></td>
        <td style="width: 10%"></td>
    </tr>
    
    <tr id="tr_DeliveryType" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_DeliveryType" CssClass="LABEL" Text="Delivery Type :" runat="server"
                meta:resourcekey="lbl_DeliveryTypeResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddl_DeliveryType" runat="server" onchange="On_DeliveryType_Change();" CssClass="DROPDOWN" Width="95%">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 11px">
            <asp:Label ID="lbl_mandatory_DeliveryType" runat="server" Text="*"></asp:Label></td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_DeliveryAgainst" CssClass="LABEL" Text="Delivery Against :" runat="server"
                meta:resourcekey="lbl_DeliveryAgainstResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddl_DeliveryAgainst" runat="server" CssClass="DROPDOWN" Width="95%"></asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        <asp:Label ID="lbl_mandatory_ddl_DeliveryAgainst" runat="server" Text="*"></asp:Label>
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_RoadPermitType" CssClass="LABEL" Text="Road Permit :" runat="server"
                meta:resourcekey="lbl_RoadPermitTypeResource1"></asp:Label>
        </td>
        <td colspan="3">
            <asp:UpdatePanel ID="upnl_RoadPermit" runat="server" UpdateMode="Conditional">
                <contenttemplate>  
                    <asp:DropDownList ID="ddl_RoadPermitType" runat="server" AutoPostBack="true" CssClass="DROPDOWN"
                        Width="95%" onchange="RoadPermitType_Change();" OnSelectedIndexChanged="ddl_RoadPermitType_SelectedIndexChanged">
                    </asp:DropDownList>
                      </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Consignee"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ddl_RoadPermitType" runat="server" Text="*"></asp:Label>
        </td>      
        <td style="width: 10%"></td>
    </tr>    
    <tr id="tr_LocationDetails" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_FromLocation" CssClass="LABEL" Text="From Location :" runat="server"
                meta:resourcekey="lbl_FromLocationResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_FromLocation" runat="server">
                <contenttemplate>                
                    <cc1:DDLSearch ID="ddl_FromLocation" runat="server" CallBackAfter="2" Text="" 
                        AllowNewText="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLocation" 
                        IsCallBack="True" Width="95%" InjectJSFunction="On_FreightBasis_Change();" 
                        PostBack="true" onchange="On_FreightBasis_Change();On_PaymentType_Change();" 
                        OnTxtChange="ddl_FromLocation_TxtChange">
                    </cc1:DDLSearch>                    
                    <asp:HiddenField runat="server" ID="hdn_FromLocationId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_LocationId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_LocationName"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Encrepted_FromLocationId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Is_FromLocation"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_CommodityId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_CommodityName"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_ItemId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_ItemName"></asp:HiddenField>                    
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="lnk_AddFromServiceLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetLocationDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingBranch"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 11px;">*</td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_ToLocation" CssClass="LABEL" Text="To Location :" runat="server"
                meta:resourcekey="lbl_ToLocationResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_ToLocation" runat="server">
                <contenttemplate>                
                    <cc1:DDLSearch ID="ddl_ToLocation"  runat="server" Width="95%" onchange="On_FreightBasis_Change();On_PaymentType_Change(); "
                        PostBack="true" InjectJSFunction="On_FreightBasis_Change();" OnTxtChange="ddl_ToLocation_TxtChange"
                        IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLocation"
                        AllowNewText="True" CallBackAfter="2">
                    </cc1:DDLSearch>                        
                    <asp:HiddenField ID="hdn_ToLocationId" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_IsODA" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_IsToPayBookingApplicable" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_ODAChargesUpTo500Kg" runat="server" Value="0"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_ODAChargesAbove500Kg" runat="server" Value="0"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Encrepted_ToLocationId"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="lnk_AddToServiceLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetLocationDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingBranch"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_DeliveryBranch" CssClass="LABEL" Text="Delivery Branch :" runat="server"
                meta:resourcekey="lbl_DeliveryBranchResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_lbl_DeliveryBranchName" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_DeliveryBranchName" Style="font-weight: bolder"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdn_DeliveryBaranchId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_DeliveryBaranchName"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_TransitDays"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_TotalTransitDays"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
        <td class="TD1" style="width: 10%;">             
            <a id="lnk_Require_Forms"  href="javascript:Require_Forms();" >                
                <asp:Label ID="lbl_Require_Forms" CssClass="LABEL" Text="Require Form :" runat="server"
                meta:resourcekey="lnk_Require_FormResource1"></asp:Label>
            </a>
        </td>
        <td style="width: 10%">
            <asp:UpdatePanel ID="upd_chk_IsOctroiApplicable" runat="server">
                <contenttemplate>
                    <asp:CheckBox ID="chk_IsOctroiApplicable"  runat="server" Enabled="False"></asp:CheckBox>
                    <asp:Label ID="lbl_IsOctroiApplicable" runat="server" meta:resourcekey="lbl_IsOctroiApplicableResource1"
                        CssClass="LABEL" Text="Octroi Applicable?"></asp:Label>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_AddLocation" runat="server">
        <td class="TD1" colspan="2">
            <asp:LinkButton ID="lnk_AddFromServiceLocation" runat="server" meta:resourcekey="lnk_AddFromServiceLocation1"
                Text="Add Service Location" TabIndex="9999" OnClientClick="return Add_Service_Location(0,0);"></asp:LinkButton>
        </td>
        <td class="TDMANDATORY" style="width: 11px;"></td>
        <td class="TD1" colspan="2">
            <asp:LinkButton ID="lnk_AddToServiceLocation" runat="server" meta:resourcekey="lnk_AddToServiceLocation1"
                Text="Add Service Location" TabIndex="9999" OnClientClick="return Add_Service_Location(0,1);"></asp:LinkButton>
        </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
        <td class="TD1" style="width: 10%;"></td>
        <td style="width: 10%;"></td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
        <td class="TD1" style="width: 10%;"></td>
        <td style="width: 10%"></td>
    </tr>
    <tr id="tr_VehicleDetails" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_VehicleType" CssClass="LABEL" Text="Vehicle Type :" runat="server"
                meta:resourcekey="lbl_VehicleTypeResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddl_VehicleType" runat="server" CssClass="DROPDOWN" Width="95%"
                AutoPostBack="True" OnSelectedIndexChanged="ddl_VehicleType_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 11px;">
            <asp:Label ID="lbl_mandatory_VehicleType" runat="server" Text="*"></asp:Label>
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_VehicleNo" CssClass="LABEL" Text="Vehicle No :" runat="server"
                meta:resourcekey="lbl_VehicleNoResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:TextBox ID="txt_VehicleNo" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="15" ></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_ExpectedDeliveryDate" CssClass="LABEL" Text="Expected Delivery Date :"
                runat="server" meta:resourcekey="lbl_ExpectedDeliveryDateResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_lbl_ExpectedDeliveryDateVaule" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" Text="Exp Dly Date :" CssClass="LABEL" ID="lbl_ExpectedDeliveryDateVaule" Style="font-weight: bolder"></asp:Label>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="wuc_BookingDate"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_PickupType" CssClass="LABEL" Text="Pickup Type :" runat="server"
                meta:resourcekey="lbl_PickupTypeResource1"></asp:Label>
        </td>
        <td style="width: 10%">
            <asp:DropDownList ID="ddl_PickupType" runat="server" CssClass="DROPDOWN" Width="95%"></asp:DropDownList>
        </td>
    </tr>
    <tr id="tr_STMDetails" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_STMNo" CssClass="LABEL" Text="STM No :" runat="server" meta:resourcekey="lbl_STMNoResource1"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:TextBox ID="txt_STMNo" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 11px"></td>
        <td class="TD1" colspan="2">            
            <asp:Label ID="lbl_FeasibilityAndRouteSurveyNo" CssClass="LABEL" Text="Feasibility & Route Survey No :"
                runat="server" meta:resourcekey="lbl_FeasibilityAndRouteSurveyNoResource1"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txt_FeasibilityAndRouteSurveyNo" runat="server" Width="95%" CssClass="TEXTBOX"
                MaxLength="10"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_RoadPermitSrNo" CssClass="LABEL" Text="Road Permit Sr No :" runat="server"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_RoadPermitSrNo" runat="server" Width="50%" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
            
            <asp:CheckBox ID="chk_IsDACC" onclick="On_DACC_Change()" runat="server" meta:resourcekey="chk_Is_DACCResource1" />
            <asp:Label ID="lbl_Is_DACC" Visible="False" CssClass="LABEL" Text="Is DACC?" runat="server"
                meta:resourcekey="lbl_Is_DACCResource1"></asp:Label>
        </td>
    </tr>
        
    <tr id="tr2" runat="server">
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_PrivateMark" CssClass="LABEL" Text="Private Mark:" runat="server" ></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_PrivateMark" runat="server">
                <contenttemplate>
                    <asp:TextBox ID="txt_Private_Mark" runat="server" Width="95%" 
                        onchange="On_Private_Mark_Change();" CssClass="TEXTBOX" 
                        MaxLength="15"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdn_Private_Mark"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Get_GC_No"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>                            
        </td>
        <td class="TDMANDATORY" style="width: 11px">
            <asp:Label ID="lbl_Mandatory_Private_Mark" runat="server" Text="*"></asp:Label>
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_Delivery_Way_Type" CssClass="LABEL" Text="Delivery Way Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
               <asp:DropDownList ID="ddl_Delivery_Way_Type"  runat="server" CssClass="DROPDOWN"></asp:DropDownList> 
        </td>
        <td class="TD1" style="width: 1%;"></td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_Service_Type" CssClass="LABEL" Text="Service Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 10%;">
               <asp:DropDownList ID="ddl_Service_Type"  runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_Service_Type_SelectedIndexChanged"></asp:DropDownList> 
        </td>
        <td class="TD1" style="width: 1%;"></td>
    </tr>
</table>
<table class="TABLE" style="width: 100%;">
    <tr id="tr_ConsignorConsigneeDetails" runat="server">
        <td class="TD1" style="width: 32%; vertical-align: top">
            <asp:Panel ID="Pnl_Consignor" runat="server" Width="100%" GroupingText="Consignor"  meta:resourcekey="Pnl_ConsignorResource1">
                <asp:UpdatePanel ID="upd_tbl_Consignor" runat="server">
                    <contenttemplate>
                        <table style="width: 100%">                            
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_ConsignorSearchByCode" runat="server" meta:resourcekey="lbl_ConsignorSearchByCodeResource1"
                                            CssClass="LABEL" Text="Search :"></asp:Label>
                                    </td>
                                    <td style="width: 90%">
                                        <table style="width: 100%">                                            
                                            <tr>
                                                <td style="width: 50%" align="left">
                                                    <asp:CheckBox ID="chk_ConsignorSearchByCode"  runat="server" meta:resourcekey="chk_ConsignorSearchByCodeResource1"
                                                        Text="By code" AutoPostBack="True" OnCheckedChanged="chk_ConsignorSearchByCode_CheckedChanged">
                                                    </asp:CheckBox>
                                                </td>
                                                <td style="width: 49%">
                                                    <asp:LinkButton ID="lnk_NewConsignor"  runat="server" meta:resourcekey="lnk_NewConsignorResource1"
                                                        Text="New Consignor"  TabIndex="9999" OnClientClick="return New_Consignor_Consignee(0,1);"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_ConsignorName" runat="server" meta:resourcekey="lbl_ConsignorNameResource1"
                                            CssClass="LABEL" Text="Name :"></asp:Label>
                                    </td>
                                    <td style="width: 90%" align="left">
                                        <cc1:DDLSearch ID="ddl_Consignor" runat="server" Width="95%" onchange="On_FreightBasis_Change();On_PaymentType_Change();"
                                            OnTxtChange="ddl_Consignor_TxtChange" PostBack="True" InjectJSFunction="ddl_UnitOfMeasurment_Change"
                                            IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchConsignorConsignee"
                                            AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>
                                    </td>
                                </tr>                                
                                <tr id="tr_Consignor_Details" runat="server">
                                    <td style="width: 10%;vertical-align:top" class="TD1">
                                        <asp:Label ID="lbl_ConsignorDetails" runat="server" CssClass="LABEL" Text="Address :"></asp:Label>
                                    </td>
                                    <td style="width: 90%" align="left">                                        
                                    <div id="div_ConsignorDetailsValue" class="DIV" style=" height: 70px; text-align:left ">
                                        <asp:Label ID="lbl_ConsignorDetailsValue" runat="server" CssClass="LABEL"></asp:Label>
                                    </div> 
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 100%" class="TD1" colspan="2">                                    
                                    <table border="0">
                                    <tr>
                                     <td style="width: 100%" class="TD1">
                                        <asp:CheckBox ID="chk_SignedByConsignor"  runat="server" Text="Signed by Consignor?" ></asp:CheckBox>
                                        &nbsp;
                                        <asp:LinkButton ID="lnk_EditConsignor"  runat="server" Text="Edit" 
                                        TabIndex="9999" OnClientClick="return New_Consignor_Consignee(1,1);"></asp:LinkButton>                                            
                                        &nbsp;                                            
                                        <asp:LinkButton ID="lnk_ViewConsignor"  runat="server" AutoPostBack="false"  
                                        TabIndex="9999" Text="View" OnClientClick="return View_Consignor_Consignee(1,1);"></asp:LinkButton>
                                        &nbsp;
                                    </td>
                                    </tr>
                                    </table>
                                    </td>
                                </tr>
                                
                                  <tr id="tr_Hidden_Consignor_Details" runat="server">
                                    <td style="width: 100%" align="left" colspan="2">
                                        <asp:HiddenField ID="hdn_ConsignorId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_EncreptedConsignorId" runat="server" Value="0"></asp:HiddenField>
                                        
                                        <asp:HiddenField ID="hdn_IsRegularConsignor" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_IsServiceTaxApplicableForConsignor" runat="server" Value="0">
                                        </asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorCountryName" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorStateName" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorCountryId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorStateId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorCityId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorAddressLine1" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorAddressLine2" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorCity" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorPinCode" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorTelNo" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorMobileNo" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsignorEmail" runat="server" Value="0"></asp:HiddenField>
                                        
                                        <asp:HiddenField ID="hdn_ConsignorCSTNo" runat="server" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>                          
                        </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Consignor" />
                        <asp:AsyncPostBackTrigger ControlID="chk_ConsignorSearchByCode" />
                        <asp:AsyncPostBackTrigger ControlID="btn_SetConsignorConsigneeDetails"></asp:AsyncPostBackTrigger>
                    </triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
        <td class="TD1" style="width: 33%; vertical-align: top">
            <asp:Panel ID="pnl_Consignee" runat="server" Width="100%" GroupingText="Consignee" meta:resourcekey="pnl_ConsigneeResource1">
                <asp:UpdatePanel ID="upd_tbl_Consignee" runat="server">
                    <contenttemplate>
                        <table style="width: 100%; vertical-align: top">                            
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_ConsigneeSearchByCode" runat="server" meta:resourcekey="lbl_ConsigneeSearchByCodeResource1"
                                            CssClass="LABEL" Text="Search :"></asp:Label>
                                    </td>
                                    <td style="width: 90%">
                                        <table style="width: 100%">                                            
                                                <tr>
                                                    <td style="width: 50%" align="left">
                                                        <asp:CheckBox ID="chk_ConsigneeSearchByCode"  runat="server" meta:resourcekey="chk_ConsigneeSearchByCodeResource1"
                                                            Text="By code" AutoPostBack="True" OnCheckedChanged="chk_ConsigneeSearchByCode_CheckedChanged">
                                                        </asp:CheckBox>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:LinkButton ID="lnk_NewConsignee"  runat="server" meta:resourcekey="lnk_NewConsigneeResource1"
                                                            Text="New Consignee"  TabIndex="9999" OnClientClick="return New_Consignor_Consignee(0,0);"></asp:LinkButton>
                                                    </td>
                                                </tr>                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_ConsigneeName" runat="server" meta:resourcekey="lbl_ConsigneeNameResource1"
                                            CssClass="LABEL" Text="Name :"></asp:Label>
                                    </td>
                                    <td style="width: 90%" align="left">
                                        <cc1:DDLSearch ID="ddl_Consignee" runat="server" onchange="On_FreightBasis_Change();On_PaymentType_Change();" 
                                            OnTxtChange="ddl_Consignee_TxtChange" PostBack="True" InjectJSFunction="ddl_UnitOfMeasurment_Change" 
                                            IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchConsignorConsignee"
                                            AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>
                                    </td>
                                </tr>
                                
                                <tr id="tr_Consignee_Details" runat="server">
                                    <td style="width: 10%;vertical-align:top" class="TD1">
                                        <asp:Label ID="lbl_ConsigneeDetails" runat="server" CssClass="LABEL" Text="Address :"></asp:Label>
                                    </td>
                                    <td style="width: 90%" align="left">                                        
                                        <div id="div_ConsigneeDetails" class="DIV" style="height: 70px; text-align:left ">
                                            <asp:Label ID="lbl_ConsigneeDetailsValue" runat="server" CssClass="LABEL"></asp:Label>
                                        </div> 
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 100%" align="left" colspan="2">                                    
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 60%"  class="TD1"  >
                                                    <asp:Panel ID="pnl_Change_Consignee_Address" runat="server" 
                                                        meta:resourcekey="pnl_Change_Consignee_AddressResource1" BorderWidth="0px" Width="100%" >
                                                        <asp:LinkButton ID="lnk_Change_Door_Delivery_Address"  runat="server" TabIndex="9999" 
                                                            meta:resourcekey="lnk_Change_Door_Delivery_AddressResource1" Text="Door Delivery Address."
                                                            OnClientClick="return Change_Consignee_Address();"></asp:LinkButton>                                                            
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 35%"  class="TD1" >
                                                    <asp:Panel ID="pnl_EditConsignee" runat="server" meta:resourcekey="pnl_Change_Consignee_AddressResource1"
                                                        BorderWidth="0px" Width="100%" align="right">
                                                        <asp:LinkButton ID="lnk_EditConsignee"  runat="server"  TabIndex="9999" Text="Edit" 
                                                            OnClientClick="return New_Consignor_Consignee(1,0);"></asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="lnk_ViewConsignee"  runat="server"  TabIndex="9999" Text="View" 
                                                            OnClientClick="return View_Consignor_Consignee(1,0);"></asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>                                                                                 
                                    </td>
                                </tr>
                                <tr id="tr_Hidden_Consignee_Details" runat="server">                                
                                    <td align="left" colspan="2">
                                        <asp:HiddenField ID="hdn_ConsigneeId" runat="server" Value="0"></asp:HiddenField>                                        
                                        <asp:HiddenField ID="hdn_EncreptedConsigneeId" runat="server" Value="0"></asp:HiddenField>                                        
                                        <asp:HiddenField ID="hdn_IsRegularConsignee" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_IsServiceTaxApplicableForConsignee" runat="server" Value="0">
                                        </asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeCountryName" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeStateName" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeCountryId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeStateId" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeCityId" runat="server" Value="0"></asp:HiddenField>
                                        
                                        <asp:HiddenField runat="server" ID="hdn_ClientId"></asp:HiddenField>
	                                    <asp:HiddenField runat="server" ID="hdn_Is_Consignor"></asp:HiddenField>	          
	          
	                                    <asp:HiddenField ID="hdn_ConsigneeAddressLine1" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeAddressLine2" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeCity" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneePinCode" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeTelNo" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeMobileNo" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeEmail" runat="server" Value="0"></asp:HiddenField>
                                        
                                        <asp:HiddenField ID="hdn_ConsigneeCSTNo" runat="server" Value="0"></asp:HiddenField>
                                        
                                        <asp:HiddenField ID="hdn_ConsigneeDDAddressLine1" runat="server" Value="0"></asp:HiddenField>
                                        <asp:HiddenField ID="hdn_ConsigneeDDAddressLine2" runat="server" Value="0"></asp:HiddenField>                                      
                                    </td>
                                </tr>                             
                        </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Consignee"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="chk_ConsigneeSearchByCode"></asp:AsyncPostBackTrigger>                        
                        <asp:AsyncPostBackTrigger ControlID="btn_SetDoorDeliveryAdderess"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="btn_SetConsignorConsigneeDetails"></asp:AsyncPostBackTrigger>                        
                    </triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
        <td class="TD1" style="width: 35%; vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TD1" style="width: 60%">
                        <asp:Label ID="lbl_PaymentType" CssClass="LABEL" Text="Payment Type :" runat="server"
                            meta:resourcekey="lbl_PaymentTypeResource1"></asp:Label>
                    </td>
                    <td style="width: 39%" align="left">
                        <asp:DropDownList ID="ddl_PaymentType" runat="server" onchange="PaymentType_Change_Confirmation();" CssClass="DROPDOWN">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="tr_Contractual_Client_Details" runat="server">
                    <td class="TD1" style="width: 60%">
                        <asp:Label ID="lbl_ContractualClient" CssClass="LABEL" Text="Contractual Client :"
                            runat="server" meta:resourcekey="lbl_ContractualClientResource1"></asp:Label>
                    </td>
                    <td style="width: 39%" align="left">
                        <asp:UpdatePanel ID="upd_ddl_ContractualClient" runat="server">
                            <contenttemplate>
                                <cc1:DDLSearch ID="ddl_ContractualClient"  runat="server" OnTxtChange="ddl_ContractualClient_TxtChange" PostBack="True"
                                    IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchContractualClient"
                                    AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>
                                <asp:HiddenField ID="hdn_ContractualClientId" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_IsContractApplied" runat="server"></asp:HiddenField>                                
                                <asp:HiddenField ID="hdn_OldPaymentType" runat="server"></asp:HiddenField>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_Contract_Branch_Details" runat="server">
                    <td class="TD1" style="width: 60%">
                        <asp:Label ID="lbl_ContractBranch" CssClass="LABEL" Text="Contract Branch :" runat="server"
                            meta:resourcekey="lbl_ContractBranchResource1"></asp:Label>
                    </td>
                    <td style="width: 39%" align="left">
                        <asp:UpdatePanel ID="upd_ddl_ContractBranch" runat="server">
                            <contenttemplate>
                                <asp:DropDownList ID="ddl_ContractBranch"  runat="server" CssClass="DROPDOWN" onchange="ddl_ContractBranch_Change()">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdn_ContractBranchId" runat="server"></asp:HiddenField>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_BillingParty"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="wuc_PolicyExpDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_Contract_Details" runat="server">
                    <td class="TD1" style="width: 60%">
                        <asp:Label ID="lbl_RateContract" CssClass="LABEL" Text="Rate Contract :" runat="server"
                            meta:resourcekey="lbl_RateContractResource1"></asp:Label>
                    </td>
                    <td style="width: 39%" align="left">
                        <asp:UpdatePanel ID="upd_ddl_Contract" runat="server">
                            <contenttemplate>
                                <asp:DropDownList ID="ddl_Contract"  runat="server" CssClass="DROPDOWN" 
                                    OnSelectedIndexChanged="ddl_Contract_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdn_ContractId" runat="server"></asp:HiddenField>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>              
            </table>          
        </td>
    </tr>
</table>

<table class="TABLE" border="0" style="width: 100%;">
    <tr id="tr_billing_details" runat="server">
        <td style="width: 12%" align="left">
            <asp:CheckBox ID="chk_IsMultipleBilling" runat="server" Text="Multiple Billing" onclick="On_IsMultipleBilling_Click(); " />
        </td>
        <td id="td_lbl_Billing_Party" runat="server" class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_BillingParty" CssClass="LABEL" Text="Billing Party :" runat="server"
                meta:resourcekey="lbl_BillingPartyResource1"></asp:Label>
        </td>        
        <td id="td_ddl_Billing_Party" runat="server"    style="width: 10%;">
            <asp:UpdatePanel ID="upd_ddl_BillingParty" runat="server">
                <contenttemplate>
                    <cc1:DDLSearch ID="ddl_BillingParty"  runat="server" OnTxtChange="ddl_BillingParty_TxtChange" PostBack="True"
                        IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchBillingParty"
                        AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>
                        
                    <asp:HiddenField ID="hdn_BillingPartyId" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_Billing_Party_Credit_Limit" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_Billing_Party_Closing_Balance" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_Billing_Party_Ledger_Id" runat="server"></asp:HiddenField>                     
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_BillingParty"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="chk_IsMultipleBilling"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>       
        <td id="td_ddl_Billing_Branch" runat="server" class="TD1" style="width:50%">
            <asp:UpdatePanel ID="upd_ddl_BillingBranch" runat="server">
                <contenttemplate>
                    <uc1:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />    
                    <asp:HiddenField ID="hdn_BillingBranchId" runat="server"></asp:HiddenField>  
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_BillingParty"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="chk_IsMultipleBilling"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>    
        </td>       
    </tr>
    <tr id="tr_billing_remarks" runat="server">
        <td style="width: 12%;" class="TD1" colspan="2">
            <asp:Label ID="lbl_BillingRemark" CssClass="LABEL" Text="Billing Remarks :" runat="server"
                meta:resourcekey="lbl_BillingRemarkResource1"></asp:Label>
        </td>
        <td style="width: 60%" colspan="2">
            <asp:TextBox ID="txt_BillingRemark" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
</table>
<table class="TABLE" border="0" style="width: 100%;">
    <tr>
        <td style="width: 10%" align="left">
            <asp:UpdatePanel ID="upd_chk_PodRequire" runat="server">
                <contenttemplate>
                    <asp:CheckBox ID="chk_PodRequire" runat="server" Text="POD Required" />
                </contenttemplate>
                <triggers>                        
                        <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>                                
                    </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 7%;">
            <asp:Label ID="lbl_GCRisk" CssClass="LABEL" Text="GC Risk :" runat="server" meta:resourcekey="lbl_GCRiskResource1"></asp:Label>
        </td>
        <td style="width: 12%;">
            <asp:UpdatePanel ID="upd_ddl_GCRisk" runat="server">
                <contenttemplate>
                    <asp:DropDownList ID="ddl_GCRisk" runat="server" onchange="ddl_GCRisk_Change()" CssClass="DROPDOWN" >
                    </asp:DropDownList>                    
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 15%;">
            <asp:CheckBox ID="chk_IsInsured" runat="server" Text="Is Insured?" onclick="On_IsInsured_Click();" />
        </td>
        <td style="width: 15%">
       </td>
        <td class="TD1" style="width: 12%;">
        </td>
        <td class="TD1" style="width: 15%" colspan="2">
        </td>
    </tr>   
</table>
<table id="tbl_CommodityGeneral" runat="server" class="TABLE" style="width: 100%; height: 10">
    <tr id="tr_CommodityDetails" runat="server">
        <td>
            <asp:UpdatePanel ID="upd_dg_Commodity" runat="server">
                <contenttemplate>            
                <table class="TABLE" width="100%">
                    <tr>             
                        <td colspan="12"  style="width: 90%">            
                              <div id="Div_Commodity" class="DIV" style=" width:890px; height: 100px ;text-align:left ">
                                    <table width="100%" border="0">                            
                                        <tr>
                                            <td class="TD1" colspan="8">
                                                <asp:DataGrid ID="dg_Commodity"  runat="server" meta:resourcekey="dg_CommodityResource1"
                                                    CssClass="Grid" Width="1000" OnCancelCommand="dg_Commodity_CancelCommand" OnUpdateCommand="dg_Commodity_UpdateCommand"
                                                    OnEditCommand="dg_Commodity_EditCommand"
                                                    OnItemCommand="dg_Commodity_ItemCommand" OnItemDataBound="dg_Commodity_ItemDataBound"
                                                    OnDeleteCommand="dg_Commodity_DeleteCommand" AutoGenerateColumns="False" ShowFooter="True"
                                                    PageSize="5" CellPadding="3" AllowSorting="True" DataKeyField="Sr_No">
                                                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                                    <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                     
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Sr No" Visible="False">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX" ID="txt_Commodity_SrNo"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Sr_No") %>' CssClass="LABEL" ID="lbl_Commodity_SrNo"
                                                                    meta:resourcekey="lbl_Commodity_SrNoResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="2%"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Sr_No") %>' ID="txt_Commodity_SrNo"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Commodity" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" AutoPostBack="True" ID="ddl_Commodity"  
                                                                     CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Commodity_Name")) %>'
                                                                    CssClass="LABEL" ID="lbl_Commodity_Name" ItemStyle-HorizontalAlign="Left"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" AutoPostBack="True" ID="ddl_Commodity" 
                                                                     CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Item" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Item_Id" ID="ddl_Item"
                                                                    CssClass="DROPDOWN" DataTextField="Item_Name">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Item_Name")) %>'
                                                                    CssClass="LABEL" ID="lbl_Item_Name"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%"  HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Item_Id" ID="ddl_Item"
                                                                    CssClass="DROPDOWN" DataTextField="Item_Name">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Packing Type" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Packing_Id" ID="ddl_Packing_Type"
                                                                     CssClass="DROPDOWN" DataTextField="Packing_Type" meta:resourcekey="ddl_Packing_TypeResource1">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Packing_Type")) %>'
                                                                    CssClass="LABEL" ID="lbl_Packing_Type" meta:resourcekey="lbl_Packing_TypeResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Packing_Id" ID="ddl_Packing_Type"
                                                                   CssClass="DROPDOWN" DataTextField="Packing_Type" meta:resourcekey="ddl_Packing_TypeResource2">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Commodity_Id" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Commodity_Name") %>' CssClass="LABEL" ID="lbl_Commodity_Id"
                                                                    meta:resourcekey="lbl_Commodity_IdResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Articles">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Articles" onkeyPress="return Only_Integers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Articles")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Articles")) %>'
                                                                    ID="txt_Articles" MaxLength="7"  onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Weight">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Weight" onkeyPress="return Only_Numbers(this,event);"
                                                                   MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>'
                                                                    ID="txt_Weight" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                       
                                                        <asp:TemplateColumn HeaderText="Length">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Length" onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Length")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Length")) %>'
                                                                    ID="txt_length" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="Width">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Width" onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Width")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Width")) %>'
                                                                    ID="txt_Width" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Height">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Height" onkeyPress="return Only_Numbers(this,event);"
                                                                   MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Height")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Height")) %>'
                                                                    ID="txt_Height" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="Remark">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOX" TextMode="MultiLine" 
                                                                   onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" MaxLength="100"  ></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>                                                        
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOXASLABEL" 
                                                                     Text='<%# (DataBinder.Eval(Container.DataItem, "Remark")) %>' TextMode="MultiLine" ReadOnly="True" ></asp:TextBox>                                                                    
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOX" TextMode="MultiLine" 
                                                                   onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" MaxLength="100"  ></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update" ValidationGroup="Btn_Save_Commodity" EditText="Edit">
                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Delete">
                                                            <FooterTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add" ValidationGroup="Btn_Save_Commodity"></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TD1"  style="width: 30%" >                            
                                                <asp:HiddenField ID="hdn_Is_Service_Tax_Applicable_For_Commodity" runat="server"></asp:HiddenField>                            
                                                <asp:HiddenField ID="hdn_FirstCommodityId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_FirstItemId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_FirstPackingTypeId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_OldFirstCommodityId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_OldFirstItemId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_OldFirstPackingTypeId" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hdn_TotalKiloMeter" runat="server"></asp:HiddenField>
                                                <asp:Label Style="text-align: right" ID="lbl_CommodityTotal" runat="server" meta:resourcekey="lbl_Commodity_TotalResource1"
                                                    CssClass="LABEL" Text="Total :" Width="98%" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalArticles" runat="server" meta:resourcekey="lbl_TotalArticlesResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalArticles" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalWeight" runat="server" meta:resourcekey="lbl_TotalWeightResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalWeight" runat="server"></asp:HiddenField>
                                            </td>                          
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalLength" runat="server" meta:resourcekey="lbl_TotalLengthResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalLength" runat="server"></asp:HiddenField>
                                            </td>
                                              <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalWidth" runat="server" meta:resourcekey="lbl_TotalWidthResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalWidth" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalHeight" runat="server" meta:resourcekey="lbl_TotalHeightResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalHeight" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 10%" class="TD1">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%" class="TD1">
                                                &nbsp;
                                            </td>                                          
                                        </tr>
                                    </table>
                                </div> 
                            </td> 
                        </tr> 
                    </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                         <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                         <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                    </triggers>
                </asp:UpdatePanel>
        </td>        
    </tr>
    <tr id="tr1" runat="server">
        <td>
            &nbsp;&nbsp;
        </td>
    </tr>    
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" ID="lbl_MultipleCommodityGridErrors"
                        meta:resourcekey="lbl_Multiple_Commodity_Grid_ErrorsResource1"></asp:Label>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr id="tr_Add_Commodity_Item" runat="server">
        <td>
            &nbsp;&nbsp;
            <asp:LinkButton ID="lnk_AddCommodity" runat="server" Text="Add Commodity" TabIndex="9999"
                OnClientClick="return Add_Commodity();"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnk_AddItem" runat="server" Text="Add Item" TabIndex="9999" 
                OnClientClick="return Add_Item();"></asp:LinkButton>                
        </td>
    </tr>
</table>
<%--nandwana--%>
<table id="tbl_CommodityNandwana" runat="server" class="TABLE" style="width: 100%; height: 10">
    <tr id="tr3" runat="server">
        <td>
            <asp:UpdatePanel ID="upd_dg_CommodityNandwana" runat="server">
                <contenttemplate>            
                <table class="TABLE" width="100%">
                    <tr>             
                        <td colspan="12"  style="width: 90%">            
                              <div id="div_Nandwana" class="DIV" style=" width:890px; height: 100px ;text-align:left ">
                                    <table width="100%" border="0">                            
                                        <tr>
                                            <td class="TD1" colspan="8">
                                                <asp:DataGrid ID="dg_CommodityNandwana"  runat="server" 
                                                    CssClass="Grid" Width="1000" OnCancelCommand="dg_Commodity_CancelCommand" 
                                                    OnUpdateCommand="dg_Commodity_UpdateCommand" OnEditCommand="dg_Commodity_EditCommand"
                                                    OnItemCommand="dg_Commodity_ItemCommand" OnItemDataBound="dg_Commodity_ItemDataBound"
                                                    OnDeleteCommand="dg_Commodity_DeleteCommand" AutoGenerateColumns="False" ShowFooter="True"
                                                    PageSize="5" CellPadding="3" AllowSorting="True" DataKeyField="Sr_No">
                                                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                                    <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                                    <%--<HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>--%>
                                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                     
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Sr No" Visible="False">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                                    ID="txt_Commodity_SrNo"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Sr_No") %>' CssClass="LABEL" ID="lbl_Commodity_SrNo"
                                                                    meta:resourcekey="lbl_Commodity_SrNoResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="2%"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Sr_No") %>' ID="txt_Commodity_SrNo"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="Articles">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Articles" onkeyPress="return Only_Integers(this,event);"
                                                                   MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Articles")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Articles")) %>'
                                                                    ID="txt_Articles" MaxLength="7"  onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="Commodity" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" AutoPostBack="True" ID="ddl_Commodity"  
                                                                     CssClass="DROPDOWN" meta:resourcekey="ddl_CommodityResource1" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Commodity_Name")) %>'
                                                                    CssClass="LABEL" ID="lbl_Commodity_Name" ItemStyle-HorizontalAlign="Left" meta:resourcekey="lbl_Commodity_NameResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" AutoPostBack="True" ID="ddl_Commodity" 
                                                                     CssClass="DROPDOWN" meta:resourcekey="ddl_CommodityResource2" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Item" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Item_Id" ID="ddl_Item"
                                                                    CssClass="DROPDOWN" DataTextField="Item_Name" meta:resourcekey="ddl_ItemResource1">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Item_Name")) %>'
                                                                    CssClass="LABEL" ID="lbl_Item_Name" meta:resourcekey="lbl_Item_NameResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%"  HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Item_Id" ID="ddl_Item"
                                                                    CssClass="DROPDOWN" DataTextField="Item_Name" meta:resourcekey="ddl_ItemResource2">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Packing Type" >
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Packing_Id" ID="ddl_Packing_Type"
                                                                     CssClass="DROPDOWN" DataTextField="Packing_Type" meta:resourcekey="ddl_Packing_TypeResource1">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Packing_Type")) %>'
                                                                    CssClass="LABEL" ID="lbl_Packing_Type" meta:resourcekey="lbl_Packing_TypeResource1"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList runat="server" Width="120" DataValueField="Packing_Id" ID="ddl_Packing_Type"
                                                                   CssClass="DROPDOWN" DataTextField="Packing_Type" meta:resourcekey="ddl_Packing_TypeResource2">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Commodity_Id" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Commodity_Name") %>' CssClass="LABEL" ID="lbl_Commodity_Id"
                                                                    meta:resourcekey="lbl_Commodity_IdResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>                                                       
                                                        <asp:TemplateColumn HeaderText="Weight">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Weight"
                                                                   onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>'
                                                                    ID="txt_Weight" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                       
                                                        <asp:TemplateColumn HeaderText="Length">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Length"
                                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Length")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Length")) %>'
                                                                    ID="txt_length" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                         <asp:TemplateColumn HeaderText="Width">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Width"
                                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Width")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Width")) %>'
                                                                    ID="txt_Width" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Height">
                                                            <FooterTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Height"
                                                                   onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Height")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Height")) %>'
                                                                    ID="txt_Height" MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>                                                        
                                                        
                                                        <asp:TemplateColumn HeaderText="Remark">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOX" 
                                                                   TextMode="MultiLine" onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" MaxLength="100"  ></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>                                                        
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOXASLABEL" 
                                                                     Text='<%# (DataBinder.Eval(Container.DataItem, "Remark")) %>' 
                                                                     onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" 
                                                                    TextMode="MultiLine" MaxLength="100" ReadOnly="True" ></asp:TextBox>                                                                    
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Remark" runat="server" Width="80%" CssClass="TEXTBOX" 
                                                                   TextMode="MultiLine" onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                            ValidationGroup="Btn_Save_Commodity" EditText="Edit" meta:resourceKey="EditCommandColumnResource1">
                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Delete">
                                                            <FooterTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add"
                                                                    ValidationGroup="Btn_Save_Commodity" meta:resourcekey="lbtn_Add_CommodityResource1"></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"
                                                                    meta:resourcekey="lbtn_Delete_CommodityResource1"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TD1"  style="width: 7%" >                            
                                                    <asp:Label Style="text-align: right" ID="lbl_TotalArticlesNandwana" runat="server" meta:resourcekey="lbl_TotalArticlesResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalArticlesNandwana" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 30%" class="TD1">
                                                
                                            </td>
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalWeightNandwana" runat="server" meta:resourcekey="lbl_TotalWeightResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalWeightNandwana" runat="server"></asp:HiddenField>
                                            </td>                          
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalLengthNandwana" runat="server" meta:resourcekey="lbl_TotalLengthResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalLengthNandwana" runat="server"></asp:HiddenField>
                                            </td>
                                              <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalWidthNandwana" runat="server" meta:resourcekey="lbl_TotalWidthResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalWidthNandwana" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 7%" class="TD1">
                                                <asp:Label Style="text-align: right" ID="lbl_TotalHeightNandwana" runat="server" meta:resourcekey="lbl_TotalHeightResource1"
                                                    CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdn_TotalHeightNandwana" runat="server"></asp:HiddenField>
                                            </td>
                                            <td style="width: 10%" class="TD1">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%" class="TD1">
                                                &nbsp;
                                            </td>
                                            
                                        </tr>
                                    </table>
                                        <%--</asp:Panel>--%>
                                </div> 
                            </td> 
                        </tr> 
                    </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                         
                         <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                         <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                    </triggers>
                </asp:UpdatePanel>
        </td>        
    </tr>    
    <tr id="tr4" runat="server">
        <td>
            &nbsp;&nbsp;
        </td>
    </tr>    
    <tr>
        <td>
            <asp:UpdatePanel ID="updNandwana" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" ID="lbl_MultipleCommodityGridErrorsNandwana"
                        meta:resourcekey="lbl_Multiple_Commodity_Grid_ErrorsResource1"></asp:Label>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    
    <tr id="tr5" runat="server">
        <td>
            &nbsp;&nbsp;
            <asp:LinkButton ID="lnk_AddCommodityNandwana" runat="server" Text="Add Commodity" TabIndex="9999"
                OnClientClick="return Add_Commodity();"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnk_AddItemNandwana" runat="server" Text="Add Item" TabIndex="9999" 
                OnClientClick="return Add_Item();"></asp:LinkButton>                
        </td>
    </tr>
</table>

<table class="TABLE" style="width: 100%; height: 10">
    <tr>
        <td colspan="12" class="TD1" style="width: 10%">
        </td>
    </tr>
</table>
<table class="TABLE" style="width: 100%;">
    <tr id="tr_UnitofMeasurmentDetails" runat="server">
        <td colspan="6" class="TD1" style=" vertical-align: top">
           <table class="TABLE" style=" vertical-align: top;">
                <tr>
                <td class="TD1" width="100%">
                    <asp:UpdatePanel ID="udp_tbl_Unitofmeasurement" runat="server">
                        <contenttemplate>
                            <table style="width: 100%" border="0" class="TABLE">                       
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_UnitOfMeasurment" runat="server" meta:resourcekey="lbl_UnitOfMeasurmentResource1"
                                            CssClass="LABEL" Text="Measurment Unit :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:DropDownList ID="ddl_UnitOfMeasurment" runat="server" meta:resourcekey="ddl_UnitOfMeasurmentResource1"
                                            CssClass="DROPDOWN" Width="99%" onchange="ddl_UnitOfMeasurment_Change()">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="lbl_UnitOfMeasurmentLength" runat="server" meta:resourcekey="lbl_UnitOfMeasurmentLengthResource1"
                                            CssClass="LABEL" Text="Length :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txt_UnitOfMeasurmentLength" onkeypress="return Only_Numbers(this,event);" runat="server"
                                            CssClass="TEXTBOXNOS"   MaxLength="7"  Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_UnitOfMeasurmentLength" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        <asp:Label ID="lbl_mandatory_UnitOfMeasurmentLength" runat="server" Text="*"></asp:Label></td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_UnitOfMeasurmentWidth" runat="server" meta:resourcekey="lbl_UnitOfMeasurmentWidthResource1"
                                            CssClass="LABEL" Text="Width :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txt_UnitOfMeasurmentWidth" onkeypress="return Only_Numbers(this,event);" runat="server"
                                            CssClass="TEXTBOXNOS" MaxLength="7" Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_UnitOfMeasurmentWidth" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        <asp:Label ID="lbl_mandatory_UnitOfMeasurmentWidth" runat="server" Text="*"></asp:Label></td> 
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_UnitOfMeasurmentHeight" runat="server" meta:resourcekey="lbl_UnitOfMeasurmentHeightResource1"
                                            CssClass="LABEL" Text="Height :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txt_UnitOfMeasurmentHeight" onkeypress="return Only_Numbers(this,event);" runat="server"
                                            CssClass="TEXTBOXNOS" MaxLength="7"  Width="95%" onchange="On_Measurment_Unit_Change();"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_UnitOfMeasurmentHeight" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        <asp:Label ID="lbl_mandatory_UnitOfMeasurmentHeight" runat="server" Text="*"></asp:Label></td> 
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1" colspan="2">
                                        <asp:Label ID="lbl_ConversionInFeet" runat="server" meta:resourcekey="lbl_ConversionInFeetResource1"
                                            CssClass="LABEL" Text="Conversion In Feet :"></asp:Label>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="lbl_LengthInFeet" runat="server" meta:resourcekey="lbl_LengthInFeetResource1"
                                            CssClass="LABEL" Text="Length :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label Style="text-align: right" ID="lbl_LengthInFeetValue" width="98%"  runat="server" 
                                                CssClass="LABEL" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_LengthInFeet" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_WidthInFeet" runat="server" meta:resourcekey="lbl_WidthInFeetResource1"
                                            CssClass="LABEL" Text="Width :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label Style="text-align: right" ID="lbl_WidthInFeetValue" width="98%"  runat="server" 
                                                CssClass="LABEL" Font-Bold="True"></asp:Label>
                                        
                                        <asp:HiddenField ID="hdn_WidthInFeet" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_HeightInFeet" runat="server" meta:resourcekey="lbl_HeightInFeetResource1"
                                            CssClass="LABEL" Text="Height :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label Style="text-align: right" ID="lbl_HeightInFeetValue" width="98%"  runat="server" 
                                                CssClass="LABEL" Font-Bold="True"></asp:Label>
                                        
                                        <asp:HiddenField ID="hdn_HeightInFeet" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>                        
                            </table>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                            <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td> 
            </tr> 
            
            <tr>
                <td class="TD1" width="100%">
                    <asp:UpdatePanel ID="upd_tbl_Volumetric" runat="server">
                        <contenttemplate>
                            <table  class="TABLE" style="vertical-align: top; width: 100%">
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_FreightBasis" runat="server" meta:resourcekey="lbl_FreightBasisResource1"
                                            CssClass="LABEL" Text="Freight Basis :"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddl_FreightBasis"  runat="server" CssClass="DROPDOWN" Width="70%" onchange="ddl_FreightBasis_Change();  ">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        &nbsp;
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        &nbsp;
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="tr_Volumetric" runat="server">
                                    <td id="Td1" style="width: 10%" class="TD1" runat="server">
                                        <asp:Label ID="lblVolumetricFreightUnit" runat="server" CssClass="LABEL" Text="Volumetric Freight Unit :"></asp:Label>
                                    </td>
                                    <td id="Td2" style="width: 10%" runat="server">
                                        <asp:DropDownList ID="ddl_VolumetricFreightUnit"  runat="server" CssClass="DROPDOWN"
                                            Width="99%" onchange="ddl_VolumetricFreightUnit_Change();">
                                        </asp:DropDownList>
                                    </td>
                                    <td id="Td3" style="width: 1%" class="TDMANDATORY" runat="server">
                                        <asp:Label ID="lbl_mandatory_VolumetricFreightUnit" runat="server" Text="*"></asp:Label></td>
                                    <td id="Td4" style="width: 10%" runat="server">
                                        <asp:Label ID="lbl_TotalCFT" runat="server" CssClass="LABEL" Text="Total CFT :"></asp:Label>
                                    </td>
                                    <td id="Td5" style="width: 10%" runat="server">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalCFTValue" width="98%"  runat="server" 
                                                CssClass="LABEL" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalCFT" runat="server"></asp:HiddenField>
                                    </td>
                                    <td id="Td6" style="width: 1%" class="TDMANDATORY" runat="server">
                                        <asp:Label ID="lbl_mandatory_TotalCFT" runat="server" Text=""></asp:Label></td>
                                    <td id="Td7" style="width: 10%" class="TD1" runat="server">
                                        <asp:Label ID="lbl_TotalCBM" runat="server" CssClass="LABEL" Text="Total CBM :"></asp:Label>
                                    </td>
                                    <td id="Td8" style="width: 10%" runat="server">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalCBMValue" width="98%"  runat="server" 
                                                CssClass="LABEL" Font-Bold="True"></asp:Label>
                                        
                                        <asp:HiddenField ID="hdn_TotalCBM" runat="server"></asp:HiddenField>
                                    </td>
                                    <td id="Td9" style="width: 1%" class="TDMANDATORY" runat="server">
                                        <asp:Label ID="lbl_mandatory_TotalCBM" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr id="tr_VolumetrickgFactor" runat="server">
                                    <td id="Td10" style="width: 10%" class="TD1" runat="server">
                                        <asp:Label ID="lbl_VolumetricToKgFactor" runat="server" CssClass="LABEL" Text="Volumetric To Kg Factor :"></asp:Label>
                                    </td>
                                    <td id="Td11" style="width: 10%" runat="server">
                                        <asp:TextBox ID="txt_VolumetricToKgFactor" runat="server" CssClass="TEXTBOXNOS"   MaxLength="7"  BorderWidth="1px"
                                            Width="95%" onchange="On_VolumetricToKgFactor_Change();"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_VolumetricToKgFactor" runat="server"></asp:HiddenField>
                                    </td>
                                    <td id="Td12" style="width: 1%" class="TDMANDATORY" runat="server">
                                    </td>
                                    <td id="Td13" style="width: 10%" runat="server">
                                    </td>
                                    <td id="Td14" style="width: 10%" runat="server">
                                    </td>
                                    <td id="Td15" style="width: 1%" class="TDMANDATORY" runat="server">
                                    </td>
                                    <td id="Td16" style="width: 10%" class="TD1" runat="server">
                                    </td>
                                    <td id="Td17" style="width: 10%" runat="server">
                                    </td>
                                    <td id="Td18" style="width: 1%" class="TDMANDATORY" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_ActualWt" runat="server" meta:resourcekey="lbl_ActualWtResource1"
                                            CssClass="LABEL" Text="Actual Wt.(kgs) :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txt_ActualWeight" onkeypress="return Only_Numbers(this,event);" runat="server" meta:resourcekey="txt_ActualWtResource1"
                                            CssClass="TEXTBOXNOS"   MaxLength="7"  Width="95%" onchange=" On_Actual_Weight_Change();"></asp:TextBox>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="lbl_ChargeWt" runat="server" meta:resourcekey="lbl_ChargeWtResource1"
                                            CssClass="LABEL" Text="Charge Wt.(kgs) :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txt_ChargeWeight" onkeypress="return Only_Numbers(this,event);" runat="server" meta:resourcekey="txt_ChargeWtResource1"
                                            CssClass="TEXTBOXNOS"   MaxLength="7"  Width="95%" onchange=" On_ChargeWeight_Change();"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_ChargeWeight" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label ID="lbl_FreightRate" runat="server" meta:resourcekey="lbl_FreightRateResource1"
                                            CssClass="LABEL" Text="Freight Rate :"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:UpdatePanel ID="upd_txt_FreightRate" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txt_FreightRate" onkeypress="return Only_Numbers(this,event);" runat="server" meta:resourcekey="txt_FreightRateResource1"
                                                    CssClass="TEXTBOXNOS"   MaxLength="7"  Width="95%" onchange="On_Freight_Rate_Change(); "></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>
                                                
                                                <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis"></asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_VolumetricFreightUnit"></asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_BookingSubType" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_RoadPermitType" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_Consignee" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                </tr>
                            </table>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfMeasurment" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                            <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
                <tr>
                    <td   align="left"  width="100%">
                        <asp:UpdatePanel ID="upd_dg_Invoice" runat="server">
                            <contenttemplate>
                                <table class="TABLE" width="100%">
                                        <tr>
                                            <td align="left" width="100%">
                                                <div id="Div_Invoice" runat="server" class="DIV"  style=" width:98%; height: 100px; text-align:left ">
                                                    <table width="95%">
                                                            <tr>
                                                                <td align="left" colspan="12">
                                                                    <asp:DataGrid ID="dg_Invoice"  runat="server" meta:resourcekey="dg_InvoiceResource1"
                                                                        CssClass="Grid" Width="100%" OnCancelCommand="dg_Invoice_CancelCommand" OnUpdateCommand="dg_Invoice_UpdateCommand"
                                                                        OnSortCommand="dg_Invoice_SortCommand" OnEditCommand="dg_Invoice_EditCommand"
                                                                        OnItemCommand="dg_Invoice_ItemCommand" OnItemDataBound="dg_Invoice_ItemDataBound"
                                                                        OnDeleteCommand="dg_Invoice_DeleteCommand" AutoGenerateColumns="False" ShowFooter="True"
                                                                        PageSize="5" CellPadding="3" AllowSorting="True" DataKeyField="Sr_No">
                                                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                                                        <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateColumn HeaderText="Sr No" Visible="False">
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX" ID="txt_Invoice_SrNo"></asp:TextBox>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" Text='<%# Eval("Sr_No") %>' CssClass="LABEL" ID="lbl_Invoice_SrNo"
                                                                                        meta:resourcekey="lbl_Invoice_SrNoResource1"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="2%"></HeaderStyle>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Sr_No") %>' ID="txt_Invoice_SrNo"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Invoice No">
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_Invoice_No" MaxLength="20"></asp:TextBox>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>'
                                                                                         MaxLength="20"  ID="txt_Invoice_No"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            
                                                                             <asp:TemplateColumn HeaderText="Chalan No">
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_Chalan_No" MaxLength="20" ></asp:TextBox>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container.DataItem, "Chalan_No")%>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "Chalan_No") %>'
                                                                                         MaxLength="20" ID="txt_Chalan_No"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            
                                                                            
                                                                            <asp:TemplateColumn HeaderText="Invoice Amount">
                                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" ID="txt_Invoice_Amount"
                                                                                         MaxLength="10" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Invoice_Amount")) %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDecimal (DataBinder.Eval(Container.DataItem, "Invoice_Amount")) %>'
                                                                                        ID="txt_Invoice_Amount" MaxLength="10"  onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="BE / BL No.">
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox runat="server" Width="98%" CssClass="TEXTBOX" ID="txt_BE_BL_No"
                                                                                         onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container.DataItem, "BE_BL_No") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" Width="80%" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "BE_BL_No") %>'
                                                                                        ID="txt_BE_BL_No" MaxLength="10"   onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                                                ValidationGroup="Btn_Save_Invoice" EditText="Edit" meta:resourceKey="EditCommandColumnResource2">
                                                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                                            </asp:EditCommandColumn>
                                                                            <asp:TemplateColumn HeaderText="Delete">
                                                                                <FooterTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lbtn_Add_Invoice" Text="Add" CommandName="Add"
                                                                                        ValidationGroup="Btn_Save_Invoice" meta:resourcekey="lbtn_Add_InvoiceResource1"></asp:LinkButton>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_Invoice" Text="Delete" CommandName="Delete"
                                                                                        meta:resourcekey="lbtn_Delete_InvoiceResource1"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                 </div> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table width="95%">
                                                        <tr>
                                                            <td style="width: 22%" class="TD1">
                                                                <asp:Label Style="text-align: right" ID="lbl_InvoiceTotal" runat="server" meta:resourcekey="lbl_InvoiceTotalResource1"
                                                                    CssClass="LABEL" Text="Invoice Total :" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="width: 14%" align="right">
                                                                <asp:Label Style="text-align: right" ID="lbl_TotalInvoiceAmount" runat="server" 
                                                                    CssClass="LABEL" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hdn_TotalInvoiceAmount" runat="server"></asp:HiddenField>
                                                            </td>
                                                            <td style="width: 55%" align="left">
                                                            </td>
                                                        </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_InvoiceGridErrors" runat="server" meta:resourcekey="lbl_InvoiceGridErrorsResource1"
                                                    CssClass="LABEL" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                </table>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Invoice"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" width="100%">
                        <asp:UpdatePanel ID="upd_tbl_OtherDetails" UpdateMode="Conditional" runat="server">
                            <contenttemplate>
                        <table class="TABLE">
                            <tr>
                                <td class="TD1" style="width: 10%">
                                    <asp:Label ID="lbl_OtherChargesRemark" CssClass="LABEL" Text="Other Charges Remark :"
                                        runat="server" meta:resourcekey="lbl_OtherChargesRemarkResource1"></asp:Label>
                                </td>
                                <td colspan="8" align="left">
                                    <asp:TextBox ID="txt_OtherChargesRemark" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%"></td>
                            </tr>                            
                            <tr>
                                <td class="TD1" style="width: 10%; ">
                                    <asp:Label ID="lbl_Instruction" CssClass="LABEL" Text="Instruction :" runat="server" meta:resourcekey="lbl_InstructionResource1"></asp:Label>
                                </td>
                                <td colspan="8"  align="left">
                                    <asp:DropDownList ID="ddl_Instruction" runat="server" onchange="On_Instruction_Change();" CssClass="DROPDOWN" Width="99%">
                                    </asp:DropDownList>                                        
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;"></td>
                            </tr>
                            
                            <tr>
                                <td class="TD1" style="width: 10%; ">
                                    <asp:Label ID="lbl_InstructionRemark" CssClass="LABEL" Text="Instruction / Remark :"
                                        runat="server" meta:resourcekey="lbl_Instruction_RemarkResource1"></asp:Label>
                                </td>
                                <td colspan="8" style="height: 34px" align="left">
                                    <asp:TextBox ID="txt_InstructionRemark" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                                        onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" MaxLength="100"></asp:TextBox>                                        
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 10%">
                                    <asp:Label ID="lbl_Enclosure" CssClass="LABEL" Text="Enclosure's :" runat="server"  meta:resourcekey="lbl_EnclosureResource1"></asp:Label>
                                </td>
                                <td colspan="8" align="left">
                                    <asp:TextBox ID="txt_Enclosure" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%"></td>
                            </tr>
                            <tr>
                                <td class="TD1"  id="td_lbl_LoadingSuperVisor" runat="server" style="width: 10%">
                                    <asp:Label ID="lbl_LoadingSuperVisor" CssClass="LABEL" Text="Loading Supervisor :"
                                        runat="server" meta:resourcekey="lbl_LoadingSuperVisorResource1"></asp:Label>
                                </td>
                                <td  id="td_ddl_LoadingSuperVisor" runat="server" colspan="3" align="left">
                                    <cc1:DDLSearch ID="ddl_LoadingSuperVisor" runat="server" AllowNewText="True"
                                        OnTxtChange="ddl_LoadingSuperVisor_TxtChange" PostBack="False" IsCallBack="True"
                                        Width="95%" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAllEmployee" CallBackAfter="2"  />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_Mandatory_LoadingSuperVisor" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="TD1"  id="td_lbl_MarketingExecutive" runat="server" style="width: 40%">
                                    <asp:Label ID="lbl_MarketingExecutive" CssClass="LABEL" Text="Marketing Executive :"
                                        runat="server" meta:resourcekey="lbl_MarketingExecutiveResource1"></asp:Label>
                                </td>
                                <td  id="td_ddl_MarketingExecutive" runat="server" class="TD1" colspan="3" align="right">
                                    <cc1:DDLSearch ID="ddl_MarketingExecutive" runat="server" AllowNewText="True"
                                        OnTxtChange="ddl_MarketingExecutive_TxtChange" PostBack="False" IsCallBack="True"
                                        Width="95%" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAllEmployee" CallBackAfter="2"  />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_Mandatory_MarketingExecutive" runat="server" Text="*"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </contenttemplate>
                            <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_Save_New"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddl_RoadPermitType"></asp:AsyncPostBackTrigger>
                         
                    </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>             
        </td>
          <td colspan="5"  style="vertical-align: top;">
            <asp:UpdatePanel ID="upd_tbl_Charges" UpdateMode="Conditional" runat="server">
                <contenttemplate>
                    <table style="vertical-align: top; width: 100%" class="TABLE">                        
                        <tr>
                            <td style="width: 30%" class="TD1">
                                <asp:Label ID="lbl_ServiceTaxPayableBy" runat="server" meta:resourcekey="lbl_ServiceTaxPayableByResource1"
                                    CssClass="LABEL" Text="Service Tax Payable By :"></asp:Label>
                            </td>
                            <td style="width: 69%">
                                <asp:DropDownList ID="ddl_ServiceTaxPayableBy"  runat="server" CssClass="DROPDOWN" Width="100%" Enabled="false">
                                    <asp:ListItem Text="Consignor" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Consignee" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Transporter" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 30%" class="TD1">
                                <asp:Label ID="lbl_LengthChargeHead" runat="server" CssClass="LABEL" Text="Length Charge Head :"></asp:Label>
                            </td>
                            <td style="width: 69%">
                                <asp:DropDownList ID="ddl_LengthChargeHead" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_LengthChargeHead_SelectedIndexChanged"  Width="100" >
                                </asp:DropDownList>                                                                     
                                <asp:HiddenField ID="hdn_LengthChargeHeadId" runat="server"></asp:HiddenField>                                
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_Freight" runat="server" meta:resourcekey="lbl_FreightResource1" CssClass="LABEL" Text="Freight :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_Freight" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS"  MaxLength="10" onchange="On_Freight_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_Freight" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 100%" align="left" colspan="2">&nbsp;</td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_LocalCharge" runat="server" meta:resourcekey="lbl_LocalChargeResource1"
                                    CssClass="LABEL" Text="Local Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_LocalCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" onchange="On_LocalCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_LocalCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_LoadingCharge" runat="server" meta:resourcekey="lbl_LoadingChargeResource1"
                                    CssClass="LABEL" Text="Hamali Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_LoadingCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_LoadingCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_LoadingCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr> 
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_StationaryCharge" runat="server" meta:resourcekey="lbl_StationaryChargeResource1"
                                    CssClass="LABEL" Text="Bilty Charges :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_StationaryCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_StationaryCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_StationaryCharge" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_MaxStationaryCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_FOVRiskCharge" runat="server" meta:resourcekey="lbl_FOVRiskChargeResource1"
                                    CssClass="LABEL" Text="FOV / Risk Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_FOVRiskCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_FOVRiskCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_FOVRiskCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr id="tr_ToPayCharge" runat="server">
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_ToPayCharge" runat="server" meta:resourcekey="lbl_ToPayChargeResource1"
                                    CssClass="LABEL" Text="To Pay Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_ToPayCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_ToPayCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_ToPayCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_DDCharge" runat="server" meta:resourcekey="lbl_DDChargeResource1"
                                    CssClass="LABEL" Text="DD Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                                <asp:TextBox ID="txt_DDCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_DDCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_DDCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr id="tr_DACC" runat="server">
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_DACCCharge" runat="server" meta:resourcekey="lbl_DACCChargeResource1" CssClass="LABEL" Text="IBA Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                                <asp:TextBox ID="txt_DACCCharge" onkeypress="return Only_Numbers(this,event);" runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_DACCCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_DACCCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">                                
                                <asp:LinkButton ID="lnk_OtherCharges"  runat="server" meta:resourcekey="lbl_OtherChargeResource1" Text="Other Charges."
                                    OnClientClick="return Other_Charges();"></asp:LinkButton> 
                                <asp:Label ID="lbl_OtherCharges" runat="server" meta:resourcekey="lbl_OtherChargeResource1" Text="Other Charges."
                                CssClass="LABEL" ></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">                            
                                <asp:Label ID="lbl_OtherChargesValue" runat="server" CssClass="TEXTBOXNOS"></asp:Label>
                                <asp:HiddenField ID="hdn_OtherCharge" runat="server"></asp:HiddenField>                                
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_NFormCharge" runat="server" CssClass="LABEL" Text="NForm Charge :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_NFormCharge" onkeypress="return Only_Numbers(this,event);" runat="server" CssClass="TEXTBOXNOS"  
                                     MaxLength="10" onchange="On_NFormCharge_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_NFormCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        
                        <tr id="tr_ReBookCharge" runat="server">
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_ReBookCharge" runat="server" meta:resourcekey="lbl_ReBookChargeResource1"
                                    CssClass="LABEL" Text="ReBook Charges :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_ReBookGCAmount" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="12" onchange="On_ReBookGCAmount_Change();"></asp:TextBox>
                                <asp:HiddenField ID="hdn_ReBookGCAmount" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_LengthCharge" runat="server" CssClass="LABEL" Text="Length Charges :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_LengthCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_LengthCharge_Change();" ></asp:TextBox>
                                <asp:HiddenField ID="hdn_LengthCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_UnloadingCharge" runat="server" CssClass="LABEL" Text="Unloading Charges :"></asp:Label>
                            </td>
                            <td style="width: 39%">
                                <asp:TextBox ID="txt_UnloadingCharge" onkeypress="return Only_Numbers(this,event);" runat="server"
                                    CssClass="TEXTBOXNOS" MaxLength="10" onchange="On_UnloadingCharge_Change();" ></asp:TextBox>
                                <asp:HiddenField ID="hdn_UnloadingCharge" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_SubTotal" runat="server" meta:resourcekey="lbl_SubTotalResource1"
                                    CssClass="LABEL" Text="Sub Total :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                                <asp:Label ID="lbl_SubTotalValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>                                        
                                <asp:HiddenField ID="hdn_SubTotal" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_Abatment" runat="server" meta:resourcekey="lbl_AbatmentResource1"
                                    CssClass="LABEL" Text="Abatement :"></asp:Label></td>
                            <td style="width: 39%" class="TD1">
                                <asp:Label ID="lbl_AbatmentValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>                                
                                <asp:HiddenField ID="hdn_Abatment" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_TaxableAmount" runat="server" meta:resourcekey="lbl_TaxableAmountResource1"
                                    CssClass="LABEL" Text="Taxable Amount :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                            <asp:Label ID="lbl_TaxableAmountValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>                                
                                <asp:HiddenField ID="hdn_TaxableAmount" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_ServiceTax" runat="server" meta:resourcekey="lbl_ServiceTaxResource1"
                                    CssClass="LABEL" Text="Service Tax :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">                            
                                <asp:Label ID="lbl_ServiceTaxValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                                <asp:HiddenField ID="hdn_ServiceTax" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr id="tr_ReBookOctroiAmount" runat="server">
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_ReBookOctroiAmount" runat="server" CssClass="LABEL" Text="ReBook Octroi Amount :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">                            
                                <asp:Label ID="lbl_ReBookOctroiAmountValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>
                                <asp:HiddenField ID="hdn_ReBookGC_OctroiAmount" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Updated" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Applicable" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_ReBook_GCOctroiPaidByID" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 60%" class="TD1">
                                <asp:Label ID="lbl_TotalGCAmount" runat="server" meta:resourcekey="lbl_TotalGCAmountResource1" CssClass="LABEL" Text="Total GC Amount :"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                                <asp:Label ID="lbl_TotalGCAmountValue" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"></asp:Label>                                      
                                <asp:HiddenField ID="hdn_TotalGCAmount" runat="server"></asp:HiddenField>
                            </td>
                        </tr>                       
                        <tr>
                            <td style="width: 100%" class="TD1" colspan="2">
                                <asp:HiddenField ID="hdn_BankLR_GC" runat="server" Value="0"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_HamaliPerKg" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_DACCCharges" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdn_FOVPercentage" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                    </table>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation" />
                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VolumetricFreightUnit" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Invoice" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Consignor" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Consignee" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch" />
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient" />
                    <asp:AsyncPostBackTrigger ControlID="btn_ReadContractDetails" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType" />                    
                    <asp:AsyncPostBackTrigger ControlID="btn_GetOtherCharges" />                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingSubType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_RoadPermitType" />                     
                    <asp:AsyncPostBackTrigger ControlID="chk_IsMultipleBilling" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Get_ServiceTaxDetails" />                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_LengthChargeHead" />                    
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_BookingDate"></asp:AsyncPostBackTrigger>    
                    <asp:AsyncPostBackTrigger ControlID="ddl_BillingParty"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

   <table style="vertical-align: top; width: 100%" class="TABLE">
     <tr id = "tr_PaymentDetails" runat="server" >
             <td class="TD1" width="100%">
              <asp:UpdatePanel ID="upd_tbl_PaymentDetails" UpdateMode="Conditional" runat="server">
                <contenttemplate>
                    <table border="0" width="100%" class="TABLE">
                        <tr>
                        <td style="width: 10%" class="TD1" >                                  
                            <asp:Label ID="lbl_Advance" runat="server" meta:resourcekey="lbl_AdvanceResource1"
                                    CssClass="LABEL" Text="Advance :"></asp:Label>
                                </td>
                                <td style="width: 7%">                                    
                                        <asp:TextBox ID="txt_Advance" onkeypress="return Only_Numbers(this,event);" runat="server"
                                        CssClass="TEXTBOXNOS"   MaxLength="12"   Width="95%" onchange="On_AdvanceAmount_Change();Set_Payment_Details();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_Advance" runat="server"></asp:HiddenField>
                                </td>                                
                                <td style="width: 10%" class="TD1">
                                     <asp:Label ID="lbl_CashAmount" runat="server" meta:resourcekey="lbl_CashAmountResource1"
                                        CssClass="LABEL" Text="Cash Amount :"></asp:Label>
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox ID="txt_CashAmount" onkeypress="return Only_Numbers(this,event);" runat="server"
                                        CssClass="TEXTBOXNOS"  MaxLength="12"  Width="95%" onchange="On_CashAmount_Change();Set_Payment_Details();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_CashAmount" runat="server"></asp:HiddenField>
                                </td>                              
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_ChequeAmount" runat="server" meta:resourcekey="lbl_ChequeAmountResource1"
                                        CssClass="LABEL" Text="Cheque Amount :"></asp:Label>
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox onblur="Set_Cheque_Details();" ID="txt_ChequeAmount"
                                         onkeypress="return Only_Numbers(this,event);" runat="server"
                                        CssClass="TEXTBOXNOS"   MaxLength="12"   Width="95%" onchange="On_ChequeAmount_Change();Set_Payment_Details();Set_Cheque_Details();"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_ChequeAmount" runat="server"></asp:HiddenField>
                                </td>                                                                
                                <td style="width: 10%">
                                    &nbsp;
                                </td>                                
                                <td style="width: 15%">
                                    &nbsp;
                                </td>                                
                            </tr>  
                           
                           <tr id= "tr_cheque_Details" runat = "server">
                            <td style="width: 10%" class="TD1" >                                  
                                <asp:Label ID="lbl_ChequeNo" runat="server" meta:resourcekey="lbl_ChequeNoResource1"
                                        CssClass="LABEL" Text="Cheque No :"></asp:Label>
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox ID="txt_ChequeNo"
                                         onkeypress="return Only_Integers(this,event);" runat="server" CssClass="TEXTBOXNOS"   MaxLength="6"   Width="95%"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_ChequeNo" runat="server"></asp:HiddenField>
                                </td>
                                                                                              
                                <td style="width: 10%" class="TD1">
                                     <asp:Label ID="lbl_ChequeDate" runat="server" meta:resourcekey="lbl_ChequeDateResource1"
                                        CssClass="LABEL" Text="Cheque Date :"></asp:Label>
                                </td>
                                <td style="width: 7%">
                                    <ComponentArt:Calendar ID="wuc_ChequeDate"  runat="server" CssClass="TEXTBOX"
                                        BorderWidth="1px" Width="98%" AutoPostBackOnVisibleDateChanged="true" OnSelectionChanged="wuc_ChequeDate_SelectionChanged"
                                        AutoPostBackOnSelectionChanged="false" MinDate="1900-01-01" AllowMonthSelection="True"
                                        AllowDaySelection="True" PickerCssClass="picker" ControlType="Picker" PickerCustomFormat="MMMM d yyyy"
                                        PickerFormat="Custom">
                                    </ComponentArt:Calendar>
                                    <asp:HiddenField ID="hdn_ChequeDate" runat="server"></asp:HiddenField>
                                </td>
                                
                                <td style="width: 10%" class="TD1">
                                    <asp:Label ID="lbl_Bank" runat="server" meta:resourcekey="lbl_BankResource1" CssClass="LABEL"
                                        Text="Bank :"></asp:Label>
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox onblur="On_BankName_Change();" ID="txt_BankName"
                                        runat="server" CssClass="TEXTBOX" MaxLength="10" Width="95%"></asp:TextBox>
                                    <asp:HiddenField ID="hdn_BankName" runat="server"></asp:HiddenField>
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>                                
                                <td style="width: 15%">
                                    &nbsp;
                                </td>                                
                            </tr>  
                    </table>
               </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation" />
                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VolumetricFreightUnit" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Invoice" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Consignor" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Consignee" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch" />
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient" />
                    <asp:AsyncPostBackTrigger ControlID="btn_ReadContractDetails" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType" />                    
                    <asp:AsyncPostBackTrigger ControlID="btn_GetOtherCharges" />                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_BookingSubType" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_RoadPermitType" />                     
                    <asp:AsyncPostBackTrigger ControlID="chk_IsMultipleBilling" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Get_ServiceTaxDetails" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LengthChargeHead" />
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>                     
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr> 
</table>
 
<table class="TABLE" style="width: 100%;">   
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" ID="lbl_Errors"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                        <asp:HiddenField ID="hdn_In_Valid_Credit_Limit_Client_Name" runat="server"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_New"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit"></asp:AsyncPostBackTrigger>
                     
                    <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:UpdatePanel ID="upd_button" runat="server">
                <contenttemplate>
                
                    <asp:Button ID="btn_Save_New" runat="server" Text="Save & New" CssClass="BUTTON" 
                    ValidationGroup="Save" OnClick="btn_Save_New_Click" AccessKey="N" />
                    &nbsp
                
                    <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" 
                    ValidationGroup="Save" AccessKey="S" OnClick="btn_Save_Exit_Click"/>
                    &nbsp
                
                    <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" 
                    ValidationGroup="Save" AccessKey="P" OnClick="btn_Save_Print_Click"/>
                    &nbsp
                
                    <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"/>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_New"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit"></asp:AsyncPostBackTrigger>
                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Close"></asp:AsyncPostBackTrigger>--%>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_btn_ReadContractDetails" runat="server" style="display:none;visibility:hidden">
        <td align="center">
        
            <asp:Button ID="btn_ReadContractDetails" runat="server" CssClass="BUTTON" Text="Read Contract Details"
                OnClick="btn_ReadContractDetails_Click" meta:resourcekey="btn_ReadContractDetailsResource1" />
        
            <asp:Button ID="btn_GetOtherCharges" runat="server" CssClass="BUTTON" Text="Get Other Charges"
                OnClick="btn_GetOtherCharges_Click" />
        
            <asp:Button ID="btn_SetDoorDeliveryAdderess" runat="server" CssClass="BUTTON" Text="Set Door Delivery Adderess"
                OnClick="btn_SetDoorDeliveryAdderess_Click" />
        
            <asp:Button ID="btn_SetConsignorConsigneeDetails" runat="server" CssClass="BUTTON"
                Text="Set Consignor Consignee Details" OnClick="btn_SetConsignorConsigneeDetails_Click" />
        
            <asp:Button ID="btn_SetLocationDetails" runat="server" CssClass="BUTTON" Text="Set Location Details"
                OnClick="btn_SetLocationDetails_Click" />
                
            <asp:Button ID="btn_SetCommodityDetails" runat="server" CssClass="BUTTON" Text="Set Commodity Details"
                OnClick="btn_SetCommodityDetails_Click" />
           
            <asp:Button ID="btn_SetItemDetails" runat="server" CssClass="BUTTON" Text="Set Item Details"
                OnClick="btn_SetItemDetails_Click" />
                
            <asp:Button ID="btn_ValidateGCNo" runat="server" CssClass="BUTTON" Text="Validate GC No"
                OnClick="btn_ValidateGCNo_Click" />
                
            <asp:Button ID="btn_Get_ServiceTaxDetails" runat="server" CssClass="BUTTON" Text="Get Service Tax Details"
                OnClick="btn_Get_ServiceTaxDetails_Click" />
                
            <asp:Button ID="btn_SetOtherCharges" runat="server" CssClass="BUTTON" Text="Set Other Charges"
                OnClick="btn_SetOtherCharges_Click" />
                
            <asp:Button ID="btn_Get_GC_No" runat="server" CssClass="BUTTON" Text="Get Next No"
                OnClick="btn_Get_GC_No_Click" />
             
            <asp:Button ID="btn_ContractBranch" runat="server" CssClass="BUTTON" Text="Get Service Tax Details"
                OnClick="btn_ContractBranch_Click" />
                                                
             <ComponentArt:Calendar ID="wuc_ApplicationStartDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="95%"
                AutoPostBackOnSelectionChanged="True" OnSelectionChanged="wuc_ApplicationStartDate_SelectionChanged"
                AutoPostBackOnVisibleDateChanged="True" />
             <asp:UpdatePanel ID="upd_hdn_ApplicationStartDate" runat="server">
                <contenttemplate>
                    <asp:HiddenField runat="server" ID="hdn_ApplicationStartDate"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="wuc_ApplicationStartDate"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<table>
    <tr id="tr_Consingor_Consignee_Details" runat="server">
        <td>
         <asp:UpdatePanel ID="upd_tr_Consingor_Consignee_Details" runat="server">
            <contenttemplate>
            <table>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeAddressLine1" runat="server" meta:resourcekey="lbl_ConsigneeAddressLine1Resource1"
                            CssClass="LABEL" Text="Address 1 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeAddressLine1" runat="server" meta:resourcekey="txt_ConsigneeAddressLine1Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                            onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeAddressLine2" runat="server" meta:resourcekey="lbl_ConsigneeAddressLine2Resource1"
                            CssClass="LABEL" Text="Address 2 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeAddressLine2" runat="server" meta:resourcekey="txt_ConsigneeAddressLine2Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                            onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeCity" runat="server" meta:resourcekey="lbl_ConsigneeCityResource1"
                            CssClass="LABEL" Text="City :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeCity" runat="server" meta:resourcekey="txt_ConsigneeCityResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneePinCode" runat="server" meta:resourcekey="lbl_ConsigneePinCodeResource1"
                            CssClass="LABEL" Text="PinCode :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneePinCode" runat="server" meta:resourcekey="txt_ConsigneePinCodeResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeTelNo" runat="server" meta:resourcekey="lbl_ConsigneeTelNoResource1"
                            CssClass="LABEL" Text="Tel. No :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeTelNo" runat="server" meta:resourcekey="txt_ConsigneeTelNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeMobileNo" runat="server" meta:resourcekey="lbl_ConsigneeMobileNoResource1"
                            CssClass="LABEL" Text="Mobile No. :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeMobileNo" runat="server" meta:resourcekey="txt_ConsigneeMobileNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeEmail" runat="server" meta:resourcekey="lbl_ConsigneeEmailResource1"
                            CssClass="LABEL" Text="Email :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeEmail" runat="server" meta:resourcekey="txt_ConsigneeEmailResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeCSTNo" runat="server" meta:resourcekey="lbl_ConsigneeCSTNoResource1"
                            CssClass="LABEL" Text="CST No. :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeCSTNo" runat="server" meta:resourcekey="txt_ConsigneeCSTNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr  id = "tr_ConsigneeTINNo" runat="server" >
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeTINNo" runat="server" meta:resourcekey="lbl_ConsigneeTINNoResource1"
                            CssClass="LABEL" Text="TIN No. :"></asp:Label>
                    </td>
                    <td style="width: 79%" align="left">
                        <asp:TextBox ID="txt_ConsigneeTINNo" runat="server" meta:resourcekey="txt_ConsigneeTINNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorAddressLine1" runat="server" meta:resourcekey="lbl_ConsignorAddressLine1Resource1"
                            CssClass="LABEL" Text="Address 1 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorAddressLine1" runat="server" meta:resourcekey="txt_ConsignorAddressLine1Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                            onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorAddressLine2" runat="server" meta:resourcekey="lbl_ConsignorAddressLine2Resource1"
                            CssClass="LABEL" Text="Address 2 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorAddressLine2" runat="server" meta:resourcekey="txt_ConsignorAddressLine2Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                            onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorCity" runat="server" meta:resourcekey="lbl_ConsignorCityResource1"
                            CssClass="LABEL" Text="City :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorCity" runat="server" meta:resourcekey="txt_ConsignorCityResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorPinCode" runat="server" meta:resourcekey="lbl_ConsignorPinCodeResource1"
                            CssClass="LABEL" Text="PinCode :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorPinCode" runat="server" meta:resourcekey="txt_ConsignorPinCodeResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorTelNo" runat="server" meta:resourcekey="lbl_ConsignorTelNoResource1"
                            CssClass="LABEL" Text="Tel. No :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorTelNo" runat="server" meta:resourcekey="txt_ConsignorTelNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorMobileNo" runat="server" meta:resourcekey="lbl_ConsignorMobileNoResource1"
                            CssClass="LABEL" Text="Mobile No. :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorMobileNo" runat="server" meta:resourcekey="txt_ConsignorMobileNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorEmail" runat="server" meta:resourcekey="lbl_ConsignorEmailResource1"
                            CssClass="LABEL" Text="Email :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorEmail" runat="server" meta:resourcekey="txt_ConsignorEmailResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsignorCSTNo" runat="server" meta:resourcekey="lbl_ConsignorCSTNoResource1"
                            CssClass="LABEL" Text="CST No. :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsignorCSTNo" runat="server" meta:resourcekey="txt_ConsignorCSTNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                                
                <tr id = "tr_ConsignorTINNo" runat="server" >
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_ConsignorTINNo" runat="server" meta:resourcekey="lbl_ConsignorTINNoResource1"
                            CssClass="LABEL" Text="TIN No. :"></asp:Label>
                    </td>
                    <td style="width: 79%" align="left">
                        <asp:TextBox ID="txt_ConsignorTINNo" runat="server" meta:resourcekey="txt_ConsignorTINNoResource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="50" Width="95%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>                               
                                
                <tr id="tr_lbl_ConsigneeDDAddressLine1" runat="server">
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeDDAddressLine1" runat="server" meta:resourcekey="lbl_ConsigneeDDAddressLine1Resource1"
                            CssClass="LABEL" Text="DD Address 1 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeDDAddressLine1" runat="server" meta:resourcekey="txt_ConsigneeDDAddressLine1Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                          onkeyPress=" return Check_Max_Length_For_Multiline(this,100);"   ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr id="tr_lbl_ConsigneeDDAddressLine2" runat="server">
                    <td style="width: 30%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeDDAddressLine2" runat="server" meta:resourcekey="lbl_ConsigneeDDAddressLine2Resource1"
                            CssClass="LABEL" Text="Address 2 :"></asp:Label>
                    </td>
                    <td style="width: 69%" align="left">
                        <asp:TextBox ID="txt_ConsigneeDDAddressLine2" runat="server" meta:resourcekey="txt_ConsigneeDDAddressLine2Resource1"
                            CssClass="TEXTBOXASLABEL" MaxLength="100" Width="95%" TextMode="MultiLine"
                            onkeyPress=" return Check_Max_Length_For_Multiline(this,100);" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>                
            </table>
             </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Consignee"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="chk_ConsigneeSearchByCode"></asp:AsyncPostBackTrigger>
                        
                        <asp:AsyncPostBackTrigger ControlID="btn_SetDoorDeliveryAdderess"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="btn_SetConsignorConsigneeDetails"></asp:AsyncPostBackTrigger>
                        
                        <asp:AsyncPostBackTrigger ControlID="ddl_Consignor" />
                        <asp:AsyncPostBackTrigger ControlID="chk_ConsignorSearchByCode" />
                        <asp:AsyncPostBackTrigger ControlID="btn_SetConsignorConsigneeDetails"></asp:AsyncPostBackTrigger>
                    </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<table>
    <tr id="tr_InsurenceDetails" runat="server">
        <td>
            <table>
                <tr>
                    <td style="width: 10%" align="left">
                    </td>
                    <td class="TD1" style="width: 7%;">
                    </td>
                    <td style="width: 12%;">
                    </td>
                    <td class="TD1" style="width: 15%;">
                        <asp:Label ID="lbl_InsuranceCompany" CssClass="LABEL" Text="Insurance Company :"
                            runat="server" meta:resourcekey="lbl_InsuranceCompanyResource1"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txt_InsuranceCompany" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                    </td>
                    <td class="TD1" style="width: 12%;">
                        <asp:Label ID="lbl_PolicyNo" CssClass="LABEL" Text="Policy No. :" runat="server"
                            meta:resourcekey="lbl_PolicyNoResource1"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 15%" colspan="2">
                        <asp:TextBox ID="txt_PolicyNo" runat="server" Width="95%" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%" align="left">
                        <asp:Label ID="lbl_PolicyExpDate" CssClass="LABEL" Text="Policy Exp Date :" runat="server"
                            meta:resourcekey="lbl_PolicyExpDateResource1"></asp:Label>
                    </td>
                    <td style="width: 7%;">
                        <ComponentArt:Calendar ID="wuc_PolicyExpDate" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                            PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                            AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" AutoPostBackOnSelectionChanged="True"
                            OnSelectionChanged="wuc_PolicyExpDate_SelectionChanged" AutoPostBackOnVisibleDateChanged="True"
                            Width="95%">
                        </ComponentArt:Calendar>
                        <asp:UpdatePanel ID="upd_hdn_PolicyExpDate" runat="server">
                            <contenttemplate>
                                <asp:HiddenField runat="server" ID="hdn_PolicyExpDate"></asp:HiddenField>
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="wuc_PolicyExpDate"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 12%;">
                    </td>
                    <td class="TD1" style="width: 15%;">
                        <asp:Label ID="lbl_PolicyAmount" CssClass="LABEL" Text="Policy Amount :" runat="server"
                            meta:resourcekey="lbl_PolicyAmountResource1"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txt_PolicyAmount" runat="server" Width="95%" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
                    </td>
                    <td class="TD1" style="width: 12%;">
                        <asp:Label ID="lbl_RiskAmount" CssClass="LABEL" Text="Risk Amount :" runat="server"
                            meta:resourcekey="lbl_RiskAmountResource1"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 15%" colspan="2">
                        <asp:TextBox ID="txt_RiskAmount" runat="server" Width="95%" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="TABLE" style="width: 100%;">
    <tr id="tr_Hidden_Fields" runat="server">
        <td class="TD1" style="width: 100%">
            <asp:HiddenField ID="hdn_PreviousArticleID" runat="server" />
            <asp:HiddenField ID="hdn_PreviousStatusID" runat="server" />
            <asp:HiddenField ID="hdn_PreviousDocumentID" runat="server" />
            <asp:HiddenField ID="hdn_PreviousDocumentNoForPrint" runat="server" />
            <asp:HiddenField ID="hdn_PreviousDocumentDate" runat="server" />
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_MenuItemId"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Previous_SubTotal" runat="server" />
            <asp:HiddenField ID="hdn_Previous_GrandTotal" runat="server" />
            <asp:HiddenField ID="hdn_Is_POD_Checked" runat="server" />
            <asp:HiddenField ID="hdn_Default_Booking_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Payment_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Delivery_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Road_Permit_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Measurment_Unit" runat="server" />
            <asp:HiddenField ID="hdn_Default_Freight_Basis" runat="server" />
            <asp:HiddenField ID="hdn_Default_Risk_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Consignment_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Pickup_Type" runat="server" />
            <asp:HiddenField ID="hdn_Default_Commodity_Weight" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cheque_Branch_Ledger_Name" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cheque_Bank_Ledger_Name" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cash_Ledger_Name" runat="server" />
            <asp:HiddenField ID="hdn_Default_Bank_Ledger_Id" runat="server" />
            <asp:HiddenField ID="hdn_Default_Cash_Ledger_Id" runat="server" />
            <asp:HiddenField ID="hdn_Valid_Cheque_Start_Days" runat="server" />
            <asp:HiddenField ID="hdn_Valid_Cheque_End_Days" runat="server" />            
            <asp:HiddenField ID="hdn_Remark_Max_Length" runat="server" />            
            <asp:HiddenField ID="hdn_LoadingSuperVisor_RequiredFor_BookingType" runat="server" />
            <asp:HiddenField ID="hdn_Container_Details_RequiredFor_BookingType" runat="server" />
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
            <asp:HiddenField ID="hdn_GC_No_Length" runat="server" />
            <asp:HiddenField ID="hdn_No_For_Padd" runat="server" />
            <asp:HiddenField ID="hdn_CompanyParameter_Standard_BasicFreightUnitId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_CompanyParameter_Standard_FreightRatePer" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Is_GCNumberEditable" runat="server" />
            <asp:HiddenField ID="hdn_Is_Contract_Required_For_TBB_GC" runat="server" />
            <asp:HiddenField ID="hdn_Is_Invoice_Amount_Required" runat="server" />
            <asp:HiddenField ID="hdn_Is_FOV_Calculated_As_Per_Standard" runat="server" />
            <asp:HiddenField ID="hdn_Is_Auto_Booking_MR_For_Paid_Booking" runat="server" />
            <asp:HiddenField ID="hdn_Is_ToPay_Charge_Require" runat="server"  /> 
            <asp:HiddenField ID="hdn_Is_Multiple_Location_Billing_Allowed" runat="server"  />            
            <asp:HiddenField ID="hdn_Is_Consignor_Consignee_Details_Shown" runat="server"  />
            <asp:HiddenField ID="hdn_Is_Validate_Freight_On_Article" runat="server"  />
            <asp:HiddenField ID="hdn_Is_Item_Required" runat="server"  />
            <asp:HiddenField ID="hdn_Is_Validate_Credit_Limit" runat="server"  />
            <asp:HiddenField ID="hdn_ClientCode" runat="server"  />             
            <asp:HiddenField ID="hdn_Focus_At_Control" runat="server"  />            
            <asp:HiddenField ID="hdn_Can_Add_Location" runat="server"  />
            <asp:HiddenField ID="hdn_Can_Add_Consignor" runat="server"  />
            <asp:HiddenField ID="hdn_Can_Edit_Consignor" runat="server"  />
            <asp:HiddenField ID="hdn_Can_View_Consignor" runat="server"  />             
            <asp:HiddenField ID="hdn_Can_Add_Consignee" runat="server"  />
            <asp:HiddenField ID="hdn_Can_Edit_Consignee" runat="server"  />
            <asp:HiddenField ID="hdn_Can_View_Consignee" runat="server"  />            
            <asp:HiddenField ID="hdn_Can_Add_Commodity" runat="server"  />
            <asp:HiddenField ID="hdn_Can_Add_Item" runat="server"  />            
            <asp:HiddenField ID="hdn_Is_Opening_GC" runat="server" Value="0" />   
            <asp:HiddenField ID="hdn_Is_Agency_GC" runat="server" Value="0" />           
            <asp:HiddenField ID="hdn_VAId" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_DocumentSeriesAllocationID" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_GCId" runat="server" Value="0" />            
            <asp:HiddenField ID="hdn_Encrepted_Rectification_GC_Id" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Rectification_GC_Id" runat="server" Value="0" />            
            <asp:HiddenField ID="hdn_GCStartNo" runat="server" />
            <asp:HiddenField ID="hdn_GCEndNo" runat="server" />
            <asp:HiddenField ID="hdn_Actualwtold" runat="server" />
            <asp:HiddenField ID="hdn_Chargewtold" runat="server" />
            <asp:HiddenField ID="hdn_BookingBranchName" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <contenttemplate>
                    <asp:CheckBox ID="chk_Is_ST_Abatment_Required" runat="server" />
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Service_Type"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upd_hdn_LoadingSuperVisor" runat="server">
                <contenttemplate>
                    <asp:HiddenField runat="server" ID="hdn_LoadingSuperVisorId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_MarketingExecutiveId"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_LoadingSuperVisor"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MarketingExecutive"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_Hidden_Fields1" runat="server">
        <td>
            <asp:UpdatePanel ID="upd_Contract_Details" runat="server">
                <contenttemplate>
                    <asp:HiddenField runat="server" ID="hdn_Minimun_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Special_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_BiltiCharges"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Standard_DDCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_DDCharge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Standard_FOVRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_Invoice_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_Invoice_Per_How_Many_Rs"></asp:HiddenField>                                        
                    <asp:HiddenField runat="server" ID="hdn_Standard_FOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_FOVPercentage"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_FreightAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_HamaliCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_LocalCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_LocalCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_ServiceTaxAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_ToPayCharges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_ServiceTaxPercent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_MinimumFOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_MinimumChargeWeight"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_MinimumHamaliPerKg"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_HamaliPerKg"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_HamaliPerArticles"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Standard_CFTFactor"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_DACCCharges"></asp:HiddenField>                       
                    <asp:HiddenField runat="server" ID="hdn_Standard_Octroi_Form_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_Octroi_Service_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_GI_Charges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_Demurrage_Days"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_Demurrage_Rate"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Standard_LengthCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Standard_NForm_Charge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Additional_Freight"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Freight_Charge_Discount_Percent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Hamali_Charge_Discount_Percent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Fov_Charge_Discount_Percent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_ToPay_Charge_Discount_Percent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_DD_Charge_Discount_Percent"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contract_UnitOfFreightId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contract_FreightBasisId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contract_FreightSubUnitId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contract_FreightUnitItemId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contract_FreightSubUnitItemId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contract_CFTFactor"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_BiltiCharges"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_DDCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_DDCharge"></asp:HiddenField>                                        
                    <asp:HiddenField runat="server" ID="hdn_Contractual_FOVRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Invoice_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Invoice_Per_How_Many_Rs"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_FOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_FOVPercentage"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_FreightAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_HamaliCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_LocalCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_LocalCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_ServiceTaxAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_ToPayCharges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_ServiceTaxPercent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_MinimumFOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_MinimumChargeWeight"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_MinimumHamaliPerKg"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_HamaliPerKg"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_HamaliPerArticles"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_CFTFactor"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_DACCCharges"></asp:HiddenField>                       
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Octroi_Form_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Octroi_Service_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_GI_Charges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Demurrage_Days"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Contractual_Demurrage_Rate"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_LengthCharge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Contractual_NForm_Charge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_MinimunFreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_SpecialFreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_BiltiCharges"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_DDCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_DDCharge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_DACCCharge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_FOVRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Invoice_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Invoice_Per_How_Many_Rs"></asp:HiddenField>                                        
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_FOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_FOVPercentage"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_FreightAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_FreightRate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_HamaliCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_LocalCharge_Rate"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_LocalCharge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_ServiceTaxAmount"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_ToPayCharges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_ServiceTaxPercent"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_MinimumChargeWeight"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_MinimumHamaliPerKg"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_MinimumFOV"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_HamaliPerKg"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_HamaliPerArticles"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_CFTFactor"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_DACCCharges"></asp:HiddenField>                                        
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Octroi_Form_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Octroi_Service_Charge"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_GI_Charges"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Demurrage_Days"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_Demurrage_Rate"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_LengthCharge"></asp:HiddenField>                    
                    <asp:HiddenField runat="server" ID="hdn_Applicable_Standard_NForm_Charge"></asp:HiddenField>                    
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_VolumetricFreightUnit"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_PaymentType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Contract"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ContractBranch"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_Commodity"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="dg_CommodityNandwana" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_ContractualClient"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ReadContractDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetCommodityDetails"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_SetItemDetails"></asp:AsyncPostBackTrigger>                    
                    <asp:AsyncPostBackTrigger ControlID="wuc_BookingDate"></asp:AsyncPostBackTrigger>                    
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
  </table>

<script type="text/javascript">
 Hide_Controls_For_OtherAgency_GC();
</script>

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom:50%; left:50%; font-size: 11px; font-family: Verdana; z-index:100">
	<span id="ajaxloading">            
	<table>
	  <tr>
	    <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
	  </tr>
	  <tr>
	    <td align="center" >Wait! Action in Progress...</td>
	  </tr>
	</table>
	</span>
    </div>
  </ProgressTemplate>
 </asp:UpdateProgress>