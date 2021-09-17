<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucALS.ascx.cs" Inherits="Operations_Outward_WucALS" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript"  src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type ="text/javascript"  src="../../Javascript/Common.js"></script>
<script type ="text/javascript"  src="../../Javascript/Operations/Outward/ALS.js"></script>

<asp:ScriptManager ID="scm_ALS" runat="server"></asp:ScriptManager>

<table class="TABLE"> 
    <tr> 
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Actual Loading Sheet"></asp:Label>
        </td>
    </tr>
    
    <tr> 
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_ALS_no" runat="server" CssClass="LABEL" Text="ALS No.:"></asp:Label>  </td>
        <td style="width: 29%;">
            <asp:Label ID="lbl_ALSNo" runat="server" Font-Bold="True"></asp:Label></td>
        <td class="TD1" style="width:1%;"></td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="ALS Date:"></asp:Label>
        </td>
        <td style="width: 29%;" class="TDMANDATORY">
             <table border="0" cellpadding="0">
                 <tr>
                     <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px" >
                        <ComponentArt:Calendar ID="ALS_Date" runat="server" 
                             CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                             ControlType="Picker" PickerCssClass="PICKER" 
                             PickerCustomFormat="dd MMM yyyy"
                             PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="ALS_Date_SelectionChanged"> 
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
                DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif"/>
                <script type="text/javascript">
                // Associate the picker and the calendar:
                    function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                    {
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= ALS_Date.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= ALS_Date.ClientObjectId %>;
                        window.<%= ALS_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
            <asp:Label ID="lbl_vehicle_cotegory" runat="server" CssClass="LABEL" Text="Vehicle Category:"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_VehicleCotegory" AutoPostBack="true" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_VehicleCotegory_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>       
        <td class="TD1" style="width: 50%" colspan="3"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;vertical-align:top">
            <asp:Label ID="lbl_vehicle_no" runat="server" CssClass="LABEL" Text="Vehicle No:"></asp:Label>
        </td>
        <td style="width: 29%;">
          <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>       
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_vehicle_capacity" runat="server" CssClass="LABEL" Text="Vehicle Capacity:"></asp:Label>
        </td>
        <td style="width: 29%;">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_VehicleCapacity" Width="70%" BorderWidth="1px" Font-Bold="True" runat="server" CssClass="TEXTBOXNOS"></asp:Label>&nbsp;<b>Kg</b>
               </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                   <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCotegory" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <uc4:WucSelectedItems ID="WucSelectedItems1" runat="server" />  
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ALS_Date" />
                 </Triggers>
           </asp:UpdatePanel>         
        </td>
    </tr>
    
    <tr>
        <td colspan="6" >
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
            <div id ="Div_ALS"  class="DIV" style="height:250px">
                <asp:DataGrid ID="dg_ALS" runat="server" AutoGenerateColumns="False" 
                DataKeyField="GC_Id" CellPadding  = "2" CssClass="GRID"
                style="border-top-style: none" Width="98%" OnItemDataBound="dg_ALS_ItemDataBound">
                
                <FooterStyle CssClass="GRIDFOOTERCSS" />
                <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"/>
                    <Columns>
                           <asp:TemplateColumn HeaderText="Attach">
                               <HeaderTemplate>
                                   <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucALS1_dg_ALS');" />
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' OnClick="Check_Single(this,'WucALS1_dg_ALS','1');" runat="server"/>
                               </ItemTemplate>  
                           </asp:TemplateColumn>
                           <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No"></asp:BoundColumn>
                           <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date"  DataFormatString ="{0:dd-MM-yyyy}"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Delivery_Location_Name" HeaderText="Delivery Location"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Delivery_Type" HeaderText="Delivery Type"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Packing_Type" HeaderText="Packing Type" HeaderStyle-CssClass="HIDEGRIDCOL" ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Booking_Articles" HeaderText="Bkg Articles"></asp:BoundColumn>
                           <asp:TemplateColumn HeaderText="Bkg Actual Wt">
                               <ItemTemplate>
                                   <asp:TextBox ID="txt_Booking_Actual_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Booking_Actual_Wt") %>' runat="server" BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox> 
                               </ItemTemplate>
                           </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Balance Articles">
                               <ItemTemplate>
                                   <asp:TextBox ID="txt_Balance_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>' runat="server" BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox> 
                               </ItemTemplate>
                           </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Balance Actual Wt">
                               <ItemTemplate>
                                   <asp:TextBox ID="txt_Balance_Actual_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Actual_Wt") %>' runat="server" BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True"></asp:TextBox> 
                               </ItemTemplate>
                           </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Loaded Articles">
                               <ItemTemplate>
                                   <asp:TextBox ID="txt_Loaded_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>' runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" style="text-align:right"  Width="80%" MaxLength="7"></asp:TextBox> 
                               </ItemTemplate>
                           </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Loaded Weight">
                               <ItemTemplate>
                                   <asp:TextBox ID="txt_Loaded_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Weight") %>' runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" style="text-align:right"  Width="80%" MaxLength="7"></asp:TextBox> 
                               </ItemTemplate>
                           </asp:TemplateColumn>                                                                                      
                    </Columns>
                </asp:DataGrid>
            </div>
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                   <asp:AsyncPostBackTrigger ControlID="ALS_Date" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3"> 
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True"/>&nbsp;
                    <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"/>
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                   <asp:AsyncPostBackTrigger ControlID="ALS_Date" />
                 </Triggers>
           </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="3">    
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
             <ContentTemplate> 
            <table width="100%">
                <tr>
                    <td style="width:50%">&nbsp;</td>
                    <td style="width:15%">
                        <asp:Label ID="lbl_tolal1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True"/>
                    </td>
                    <td class="TD1" style="width:15%">
                        <asp:Label ID="lbl_tolalLodArt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"/>
                    </td>
                    <td style="width:15%">
                        <asp:Label ID="lbl_tolalLodWt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"/>
                    </td>
                    <td style="width:5%">
                        <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                        <asp:HiddenField ID="hdn_tolalLodArt" runat="server" />
                        <asp:HiddenField ID="hdn_tolalLodWt" runat="server" />
                    </td>
                </tr>
            </table>    
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                   <asp:AsyncPostBackTrigger ControlID="ALS_Date" />
                 </Triggers>
           </asp:UpdatePanel>        
        </td>
    </tr>  
    <tr>
        <td colspan="6">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_supervisor" runat="server" CssClass="LABEL" Text="Supervisor Name :"></asp:Label>
                    </td>
                    <td style="width:29%;">
                        <cc1:DDLSearch ID="ddl_Supervisior" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" CallBackAfter="2" PostBack="False"/>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">*</td>
                    <td class="TD1" style="width: 50%;" colspan="3">&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
   
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
        </td>
        <td style="width:79%;" colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX" TextMode="multiline"
                Height="40px" MaxLength="250"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%;"></td>
    </tr>
    
    <tr>
        <td align="left" colspan="6" style="text-align: left">&nbsp;  
              <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label><br />&nbsp;
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR"></asp:Label>&nbsp;
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
        </td>       
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" AccessKey="P" OnClick="btn_Save_Print_Click" />&nbsp;
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
        </td>
    </tr>   
     <tr>
        <td colspan="6">
             <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
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