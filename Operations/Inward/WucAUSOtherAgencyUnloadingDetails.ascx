<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAUSOtherAgencyUnloadingDetails.ascx.cs"  Inherits="Operations_Inward_WucAUSOtherAgencyUnloadingDetails" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Inward/AUSOtherAgency.js"></script>

<asp:ScriptManager ID="scm_AUSOtherAgency" runat="server"></asp:ScriptManager>

<table class="TABLE" style="width: 100%;" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="GAS(Other Agency)"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TURNo" CssClass="LABEL" Text="TUR No :" runat="server" meta:resourcekey="lbl_TURNoResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_TURNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TURDate" CssClass="LABEL" Text="TUR Date :" runat="server" meta:resourcekey="lbl_TURDateResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                        <componentart:calendar id="wuc_AUSDate" runat="server" cellpadding="2" clientsideonselectionchanged="Picker_OnSelectionChanged"
                            controltype="Picker" pickercssclass="PICKER" pickercustomformat="dd MMM yyyy"
                            pickerformat="Custom" selecteddate="2008-10-20" autopostbackonselectionchanged="True"
                            onselectionchanged="wuc_AUSDate_SelectionChanged" ></componentart:calendar>
                    </td>
                    <td style="height: 24px">
                        <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                            onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                            width="25" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
        <td class="TD1" style="width:50%" colspan="3"></td>                   
        <td class="TD1" style="width: 20%"></td>
        <td style="width: 29%">
            <componentart:calendar runat="server" id="Calendar" allowmultipleselection="False"
                allowweekselection="False" allowmonthselection="False" controltype="Calendar"
                popup="Custom" calendartitlecssclass="TITLE" clientsideonselectionchanged="Calendar_OnSelectionChanged"
                dayheadercssclass="DAYHEADER" daycssclass="DAY" dayhovercssclass="DAYHOVER" othermonthdaycssclass="OTHERMONTHDAY"
                selecteddaycssclass="SELECTEDDAY" calendarcssclass="CALENDER" nextprevcssclass="NEXTPREV"
                monthcssclass="MONTH" swapduration="300" daynameformat="FirstTwoLetters" imagesbaseurl="../../images/"
                previmageurl="cal_prevMonth.gif" nextimageurl="cal_nextMonth.gif" />

            <script type="text/javascript">
            // Associate the picker and the calendar:
                function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                {
                  if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wuc_AUSDate.ClientObjectId %>_loaded)
                  {
                    window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wuc_AUSDate.ClientObjectId %>;
                    window.<%= wuc_AUSDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                  }
                  else
                  {
                    window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                  }
                }
                ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
            </script>

        </td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Agency" CssClass="LABEL" Text="Agency :" runat="server" meta:resourcekey="lbl_AgencyResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_Agency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Agency_SelectedIndexChanged"
                CssClass="DROPDOWN"></asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
       
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_AgencyLedger" CssClass="LABEL" Text="Agency Ledger :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <contenttemplate>
                    <cc1:DDLSearch ID="ddl_AgencyLedger" runat="server" PostBack="True" IsCallBack="True" 
                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerForOtherAgencyGC"
                    AllowNewText="True" OnTxtChange="ddl_AgencyLedger_TxtChange"></cc1:DDLSearch>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                   <%-- <asp:AsyncPostBackTrigger ControlID="ddl_AgencyLedger"></asp:AsyncPostBackTrigger>--%>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
       
      </tr>
      <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VehicleNo" CssClass="LABEL" Text="Vehicle No :" runat="server" meta:resourcekey="lbl_VehicleNoResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_VehicleNo" runat="server" CssClass="TEXTBOX" onblur="Uppercase(this)" MaxLength="15" Width="50%"/>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ArrivedFrom" CssClass="LABEL" Text="Arrived From :" runat="server" meta:resourcekey="lbl_ArrivedFromResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_ddl_ArrivedFromLocation" runat="server">
                <contenttemplate>
                    <cc1:DDLSearch ID="ddl_ArrivedFromLocation" TabIndex="180" runat="server" OnTxtChange="ddl_ArrivedFromLocation_TxtChange" 
                    PostBack="True" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLocationForOtherAgencyBooking"
                    AllowNewText="True"></cc1:DDLSearch>
                    <asp:HiddenField runat="server" ID="hdn_ArrivedFromLoacationId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_ArrivedFromBranchId"></asp:HiddenField>                            
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ArrivedFromLocation"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>                  
    </tr>                
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LHPO_No_For_Print" CssClass="LABEL" Text="LHPO No :" runat="server" meta:resourcekey="lbl_LHPO_No_For_PrintResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_LHPO_No_For_Print" runat="server" CssClass="TEXTBOX" onblur="Uppercase(this)" MaxLength="15" Width="50%"/>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LHPODate" CssClass="LABEL" Text="LHPO Date :" runat="server" meta:resourcekey="lbl_LHPODateResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <componentart:calendar id="wuc_LHPODate" runat="server" cssclass="TEXTBOX"
                pickerformat="Custom" pickercustomformat="MMMM d yyyy" controltype="Picker" pickercssclass="picker"
                allowdayselection="True" allowmonthselection="True" mindate="1900-01-01" width="95%" tabindex="20"/>
        </td>
        <td style="width: 1%"></td>
    </tr>     
      <tr>
        <td style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="upd_pnl_dg_AUSUnloading" runat="server" UpdateMode="conditional">
                <contenttemplate>
                <div id="Div_Invoice" runat="server" class="DIV" style=" width:880px; height: 230px">
                <asp:DataGrid runat="server" CssClass="GRID" AutoGenerateColumns="False" ID="dg_OtherAgencyGCDetails"
                    OnItemDataBound="dg_OtherAgencyGCDetails_ItemDataBound">
                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Attach">
                           <HeaderTemplate>
                               <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucAUSOtherAgencyUnloadingDetails1_dg_OtherAgencyGCDetails');" />
                           </HeaderTemplate>
                           <ItemTemplate>
                               <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' OnClick="Check_Single(this,'WucAUSOtherAgencyUnloadingDetails1_dg_OtherAgencyGCDetails','1');" runat="server"/>
                           </ItemTemplate>  
                        </asp:TemplateColumn>                      
                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Gc_Date1" HeaderText="GC Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Agency_GC_No" HeaderText="Agency GC No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="From_Location" HeaderText="From Location"></asp:BoundColumn>
                        <asp:BoundColumn DataField="To_Location" HeaderText="To Location"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Delivery_Type" HeaderText="Delivery Type"></asp:BoundColumn>
                        
                        <asp:TemplateColumn HeaderText="Booking Articles">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Booking_Article" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Article") %>' BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>    
                        <asp:TemplateColumn HeaderText="Booking Actual Wt">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Booking_Article_Wt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Article_Wt") %>' BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>                     
                        <asp:TemplateColumn HeaderText="Balance Articles">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Balance_Articles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>' BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>    
                        <asp:TemplateColumn HeaderText="Balance Wt">
                            <ItemTemplate>
                                 <asp:TextBox ID="txt_Balance_Wt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles_Wt") %>' BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>                       
                        <asp:TemplateColumn HeaderText="Loaded Articles">
                            <ItemTemplate>                                  
                                <asp:TextBox ID="txt_Loaded_Articles" Width="80%" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>' CssClass="TEXTBOXNOS"
                                 MaxLength="7" onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Loaded Wt">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Loaded_Actual_Wt" Width="80%" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Actual_Wt") %>' CssClass="TEXTBOXNOS"
                                 MaxLength="7" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Recieved Articles">
                            <ItemTemplate>
                                <asp:TextBox runat="server" Width="80%" MaxLength="5" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "Received_articles") %>'
                                    ID="txt_Recieved_Article" onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Recieved Articles Wt">
                            <ItemTemplate>
                                <asp:TextBox runat="server" Width="80%" MaxLength="5" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "Received_Wt") %>'
                                    ID="txt_Recieved_Articles_Wt" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Recieved Condition">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" Width="100px" ID="ddl_Received_Condintion" CssClass="DROPDOWN"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Articles Damaged / Leakage">
                            <ItemTemplate>
                                <asp:TextBox runat="server" Width="80%" MaxLength="5" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "damaged_articles") %>'
                                    ID="txt_Damaged_Leakage_Articles" onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Damaged Leakage Value">
                            <ItemTemplate>
                                <asp:TextBox runat="server" Width="80%" MaxLength="5" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "Damaged_Value") %>'
                                    ID="txt_Damaged_Leakage_Value" onkeyPress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>      
                        <asp:TemplateColumn HeaderStyle-CssClass="HIDEGRIDCOL">
                            <ItemTemplate>
                                 <asp:HiddenField ID="hdn_Goods_Dly_Rec" Value='<%# DataBinder.Eval(Container.DataItem, "Goods_Dly_Rec") %>' runat="server"/>
                                 <asp:HiddenField ID="hdn_Upcountry_Rec" Value='<%# DataBinder.Eval(Container.DataItem, "Upcountry_Rec") %>' runat="server"/>
                                 <asp:HiddenField ID="hdn_Service_charge_Payable" Value='<%# DataBinder.Eval(Container.DataItem, "Service_charge_Payable") %>' runat="server"/>
                                 <asp:HiddenField ID="hdn_Upcountry_Crossing_Cost_Payable" Value='<%# DataBinder.Eval(Container.DataItem, "Upcountry_Crossing_Cost_Payable") %>' runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateColumn>                 
                    </Columns>
                </asp:DataGrid>
            </div> 
            </contenttemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_AgencyLedger"></asp:AsyncPostBackTrigger>
            </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="upd_total" runat="server" UpdateMode="conditional">
                <contenttemplate>
            <table style="width:100%;">         
                <tr>
                    <td class="TD1" colspan="5">
                    
                        <asp:HiddenField runat="server" ID="hdn_Total_GC"></asp:HiddenField>                    
                        <asp:HiddenField runat="server" ID="hdn_Total_Booking_Articles"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Booking_Articles_Wt"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Loaded_Articles"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Loaded_Articles_Wt"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Received_Articles"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Received_Articles_Wt"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Damage_Leakage_Articles"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdn_Total_Damage_Leakage_Value"></asp:HiddenField>                        
                        <asp:Label runat="server" Text="Total :" CssClass="LABEL" ID="lbl_Total" Style="font-weight: bolder" meta:resourcekey="lbl_TotalResource2" ></asp:Label>
                     </td>
                    <td style="width: 100px">
                        <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_GC" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Booking_Articles" Style="font-weight: bolder" meta:resourcekey="lbl_Total_Booking_ArticlesResource2" ></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Booking_Articles_Wt" Style="font-weight: bolder" meta:resourcekey="lbl_Total_Booking_Articles_WtResource2" ></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Loaded_Articles" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Loaded_ArticlesResource2" ></asp:Label>
                        </td>
                    <td style="width: 100px">
                        <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Loaded_Articles_Wt" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Loaded_Articles_WtResource2" ></asp:Label>
                        </td>
                    <td style="width: 100px">
                          <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Received_Articles" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Received_ArticlesResource2" ></asp:Label>
                        </td>
                    <td style="width: 100px">
                         <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Received_Articles_Wt" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Received_Articles_WtResource2" ></asp:Label>
                        </td>
                    <td style="width: 100px">
                          <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Damage_Leakage_Articles" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Damage_Leakage_ArticlesResource2" ></asp:Label>
                        </td>
                    <td style="width: 100px">
                         <asp:Label runat="server" CssClass="LABEL" ID="lbl_Total_Damage_Leakage_Value" style="FONT-WEIGHT: bolder" meta:resourcekey="lbl_Total_Damage_Leakage_ValueResource2" ></asp:Label>
                        </td>
                </tr>
            </table>            
            </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_AgencyLedger"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table width="100%" border="0">
                <tr>
                    <td colspan="2" style="vertical-align:top;width: 30%">
                       <asp:Panel ID="Panel1" runat="server" Width="100%" GroupingText="Details">
                        <table style="width: 100%" border="0">                           
                            <tr>
                                <td style="width: 50%;" class="TD1">
                                    <asp:Label ID="lbl_ActualArrivalDate" CssClass="LABEL" Text="Actual Arrival Date :"
                                        runat="server" meta:resourcekey="lbl_ActualArrivalDateResource2"></asp:Label>
                                </td>
                                <td style="width: 50%;">
                                    <%--<uc1:wucdatepicker id="wuc_ActualArrivalDate" runat="server" />--%>
                                     <componentart:calendar id="wuc_ActualArrivalDate" runat="server" cellpadding="2" 
                                     clientsideonselectionchanged="Picker_OnSelectionChanged"
                                        controltype="Picker" pickercssclass="PICKER" pickercustomformat="dd MMM yyyy"
                                        pickerformat="Custom" selecteddate="2008-10-20"></componentart:calendar>
                                </td>                  
                            </tr>
                            <tr>
                                <td style="width:50%;" class="TD1">
                                    <asp:Label ID="lbl_ActualArrivalTime" CssClass="LABEL" Text="Actual Arrival Time :"
                                        runat="server" meta:resourcekey="lbl_ActualArrivalTimeResource2"></asp:Label>
                                </td>
                                <td style="width: 50%;">
                                    <uc2:timepicker id="wuc_ActualArrivalTime" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%;" class="TD1">
                                    <asp:Label ID="lbl_UnloadingDate" CssClass="LABEL" Text="Unloading Date :" runat="server" meta:resourcekey="lbl_UnloadingDateResource2"></asp:Label>
                                </td>
                                <td style="width: 50%;">
                                    <asp:UpdatePanel ID="upd_lbl_UnloadingDateValue" UpdateMode="Conditional" runat="server">
                                        <contenttemplate>
                                            <asp:Label runat="server" CssClass="LABEL" ID="lbl_UnloadingDateValue" Style="font-weight: bolder" meta:resourcekey="lbl_UnloadingDateValueResource2" ></asp:Label>                            
                                        </contenttemplate>
                                        <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                        </triggers>
                                    </asp:UpdatePanel>
                                </td>                   
                            </tr>
                            <tr>
                                <td style="width:50%;" class="TD1">
                                    <asp:Label ID="lbl_UnloadingTime" CssClass="LABEL" Text="Unloading Time :" runat="server" meta:resourcekey="lbl_UnloadingTimeResource2"></asp:Label>
                                </td>
                                <td style="width: 50%;">
                                    <uc2:timepicker id="wuc_UnloadingTime" runat="server" />
                                </td>
                            </tr>               
                        </table>
                       </asp:Panel> 
                    </td>
                    <td  style="vertical-align:top;width: 30%" colspan="2">     
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                    <contenttemplate>               
                        <asp:Panel ID="Pnl_Receivables" runat="server" Width="100%" GroupingText="Receivables">
                            <table style="width: 100%" border="0">                   
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_To_Pay" runat="server" CssClass="LABEL" Text="Goods (Delivery) :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1" >
                                        <asp:Label ID="lbl_To_Pay_Value" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_To_Pay" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_UpcountryReceivable" runat="server" CssClass="LABEL" Text="Upcountry Receivable :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1" >
                                        <asp:Label ID="lbl_UpcountryReceivableValue" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_UpcountryReceivable" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_Total_Receivable" runat="server" CssClass="LABEL" Text="Total :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1" >
                                        <asp:Label ID="lbl_Total_Receivable_Value" runat="server" CssClass="LABEL" style="FONT-WEIGHT: bolder" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_Total_Receivable_Value" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                 <tr id="Tr1" runat="server">
                                    <td style="width: 50%;" class="TD1">
                                        <asp:Label ID="lbl_ScheduledArrivalDate" CssClass="LABEL" Text="Scheduled Arrival Date :"
                                            runat="server" meta:resourcekey="lbl_ScheduledArrivalDateResource2"></asp:Label>
                                    </td>
                                    <td style="width: 50%;">
                                       <%-- <asp:UpdatePanel ID="upd_lbl_ScheduledArrivalDateValue" runat="server">
                                            <contenttemplate>--%>
                                                <asp:Label runat="server" CssClass="LABEL" ID="lbl_ScheduledArrivalDateValue" Style="font-weight: bolder"></asp:Label>                        
                                          <%--  </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="TD1">
                                        <asp:Label ID="lbl_ScheduledArrivalTime" CssClass="LABEL" Text="Scheduled Arrival Time :"
                                            runat="server" meta:resourcekey="lbl_ScheduledArrivalTimeResource2"></asp:Label>
                                    </td>
                                    <td style="width: 50%;">
                                        <%--<asp:UpdatePanel ID="upd_lbl_ScheduledArrivalTimeValue" runat="server">
                                            <contenttemplate>--%>
                                                <asp:Label runat="server" CssClass="LABEL" ID="lbl_ScheduledArrivalTimeValue" Style="font-weight: bolder"></asp:Label>
                                          <%--  </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>                             
                            </table>
                        </asp:Panel>
                       </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="ddl_AgencyLedger"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="vertical-align:top;width: 40%" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                    <contenttemplate>
                        <asp:Panel ID="pnl_Payables" runat="server" Width="100%" GroupingText="Payables">
                            <table style="width: 100%" border="0">
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_Delivery_Commision" runat="server" CssClass="LABEL" Text="Service Charges :"></asp:Label>
                                    </td>
                                    <td style="width: 40%"  class="TD1" >
                                        <asp:Label ID="lbl_Total_Delivery_Commision" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_Total_Delivery_Commision" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_UpcountryCrossingCost" runat="server" CssClass="LABEL" Text="Upcountry Crossing Cost:"></asp:Label>
                                    </td>
                                    <td style="width: 40%"  class="TD1" >
                                        <asp:Label ID="lbl_UpcountryCrossingCostValue" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_UpcountryCrossingCost" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_Lorry_Hire" runat="server" CssClass="LABEL" Text="Lorry Hire :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1">
                                        <asp:TextBox ID="txt_Lorry_Hire" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" 
                                        MaxLength="7" Width="95%" Text="0" onchange="Calculate_Total_Receivable();" onblur="Calculate_Total_Receivable();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_Others_Payable" runat="server" CssClass="LABEL" Text="Others :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1">
                                        <asp:TextBox ID="txt_Others_Payables" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" 
                                        MaxLength="7" Width="95%" Text="0" onchange="Calculate_Total_Receivable();" onblur="Calculate_Total_Receivable();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%">
                                        <asp:Label ID="lbl_Total_Payable" runat="server" CssClass="LABEL" Text="Total :"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="TD1" >
                                        <asp:Label ID="lbl_Total_Payable_Value" runat="server" CssClass="LABEL" style="FONT-WEIGHT: bolder" Text="0"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_Total_Payable_Value" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Agency"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="ddl_AgencyLedger"></asp:AsyncPostBackTrigger>
                            </triggers>
                       </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_UnloadedbySupervisor" CssClass="LABEL" Text="Unloaded by Supervisor :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;">
            <cc1:ddlsearch id="ddl_Supervisor" runat="server" allownewtext="True"
                iscallback="True" callbackafter="2" callbackfunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee"/>
        </td>
        <td style="width: 1%;" class="TD1"></td>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ReasonforLateUnloading" CssClass="LABEL" Text="Reason for Late Unloading :"
                runat="server" meta:resourcekey="lbl_ReasonforLateUnloadingResource2"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_Reason_For_Late_Uploading" runat="server" CssClass="DROPDOWN" Width="98%">
            </asp:DropDownList>
        </td>
        <td style="width: 1%;" class="TD1"></td>
    </tr>     
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
        </td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_Remarks" Height="30px" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td style="width: 1%;" class="TD1"></td>
    </tr>
    
     <tr>
        <td colspan="6">  
            <asp:Label runat="server" CssClass="LABELERROR" ID="lbl_Errors"></asp:Label>                
        </td>
    </tr>
    <tr>
        <td colspan="6">  
            &nbsp; 
        </td>
     </tr>
     <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server"  Text="Save & New" OnClick ="btn_Save_Click"  CssClass="BUTTON" />
            <asp:Button ID="btn_Save_Exit" runat="server" Text="Save & Exit" OnClick ="btn_Save_Exit_Click"  CssClass="BUTTON"/>
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" AccessKey="p" OnClick="btn_Save_Print_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server"  Text="Exit" OnClick ="btn_Close_Click" CssClass="BUTTON"/>
        </td>
    </tr>
   
    <tr>
     <td colspan="6">
	     <asp:Label  ID="Label1"  runat="server" text="fields with * mark are mandatory" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
	     <asp:HiddenField ID="hdf_ResourecString" runat="server" />
    </td>
    </tr>    
</table>

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
