
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTAS.ascx.cs" Inherits="Operations_Inward_WucTAS" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc3" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Inward/AUS.js"  ></script>

<asp:ScriptManager ID="scm_TAS" runat="server"></asp:ScriptManager>

<script type="text/javascript">
function Allow_To_Save()
{
    
    var lbl_Errors=document.getElementById('<%=lbl_Errors.ClientID%>');
    var hdn_Vehicle_Id=document.getElementById('<%=hdn_Vehicle_Id.ClientID %>');
    var ddl_LHPO=document.getElementById('<%=ddl_LHPO.ClientID%>');
    var hdn_TAS_Rec_Count=document.getElementById('<%=hdn_TAS_Rec_Count.ClientID%>');
    var hdn_LHPOCaption = document.getElementById('<%=hdn_LHPOCaption.ClientID%>');
     var objResource=new Resource('<%=hdf_ResourecString.ClientID %>');
    
    var ATS = false;
    
    if (val(hdn_Vehicle_Id.value) <= 0)
    {  
        lbl_Errors.innerText="Please Select Vehicle.";        
    }
    else if (ddl_LHPO.value <= 0)
    {
        lbl_Errors.innerText="Please Select "+ hdn_LHPOCaption.value +" No.";
        ddl_LHPO.focus();
    }
    else if( val(hdn_TAS_Rec_Count.value) <= 0)
    {        
        lbl_Errors.innerText= objResource.GetMsg("MsgDetails");
    }
    else
        ATS=true;
   
    return ATS;
}
</script>


<table  class="TABLE" border="0">
   <tr>
       <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Truck Arrival Sheet" meta:resourcekey="lbl_HeadResource1"></asp:Label>
       </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TASNo" CssClass="LABEL" Text="TAS No :" runat="server" meta:resourcekey="lbl_TASNoResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_TASNoValue"  CssClass="LABEL"  Style="font-weight: bolder"  runat="server" meta:resourcekey="lbl_TASNoValueResource1"></asp:Label></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TASDate" CssClass="LABEL" Text="TAS Date :" runat="server" meta:resourcekey="lbl_TASDateResource1"></asp:Label> 
        </td>
        <td style="width: 29%;">
            <table  cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                    <ComponentArt:Calendar ID="wuc_TASDate" runat="server" Height="24px"
                         CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                         ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy"
                         PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="wuc_TASDate_SelectionChanged" > 
                    </ComponentArt:Calendar>
                    </td>
                    <td runat="server" id="TD_Calender">
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
        <td style="width: 50%" colspan="3"></td>
        <td class="TD1" style="width: 20%"></td>
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
                  if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= wuc_TASDate.ClientObjectId %>_loaded)
                  {
                    window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= wuc_TASDate.ClientObjectId %>;
                    window.<%= wuc_TASDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td style="width: 50%" colspan="3"></td>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_TASTime" CssClass="LABEL" Text="TAS Time :" runat="server" meta:resourcekey="lbl_TASTimeResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <uc2:TimePicker ID="wuc_TASTime" runat="server" />
        </td>
        <td style="width: 1%;"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No :" runat="server" meta:resourcekey="lbl_Vehicle_NoResource1"></asp:Label></td>
        <td style="width: 29%">
            <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
        <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_VehicleCategory" CssClass="LABEL" Text="Vehicle Category :" runat="server" meta:resourcekey="lbl_VehicleCategoryResource1"></asp:Label> 
        </td>
        <td style="width: 29%; ">
          <asp:UpdatePanel ID="upd_VehicleCategory" runat="server">
              <ContentTemplate>
                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_Vehicle_Category" Style="font-weight: bolder" meta:resourcekey="lbl_Vehicle_CategoryResource1"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdn_Vehicle_Category_Id"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Vehicle_Id" ></asp:HiddenField>                                  
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                </Triggers>
          </asp:UpdatePanel>
       </td>
       <td style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_LHPONo" CssClass="LABEL" Text="LHPO No :" runat="server" meta:resourcekey="lbl_LHPONoResource1"></asp:Label> </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="upd_LHPO" runat="server">
            <contenttemplate>
                <asp:DropDownList runat="server" Width="98%" AutoPostBack="True" ID="ddl_LHPO" CssClass="DROPDOWN" 
                    OnSelectedIndexChanged="ddl_LHPO_SelectedIndexChanged" meta:resourcekey="ddl_LHPOResource1">
                  </asp:DropDownList>                          
            </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td  class="TDMANDATORY" style="width: 1%">*</td>      
        <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_LHPODate"   CssClass="LABEL" Text="LHPO Date :" runat="server" meta:resourcekey="lbl_LHPODateResource1" ></asp:Label> 
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_LHPODate" runat="server">
                <contenttemplate>
                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPO_Date" Style="font-weight: bolder" meta:resourcekey="lbl_LHPO_DateResource1"></asp:Label>
            </contenttemplate>
                <triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
    <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_FromLocation"   CssClass="LABEL" Text="LHPO From Location :" runat="server" meta:resourcekey="lbl_FromLocationResource1" ></asp:Label> 
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="Upd_PnlFromLocation" runat="server">
                <contenttemplate>
                    <asp:Label  ID="lbl_LHPOFromLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder" meta:resourcekey="lbl_LHPOFromLocationResource1"></asp:Label>
            </contenttemplate>
                <triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
        
         <td class="TD1" style="width: 20%">
             <asp:Label ID="lbl_ToLocation"   CssClass="LABEL" Text="LHPO To Location :" runat="server" meta:resourcekey="lbl_ToLocationResource1" ></asp:Label> 
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="Upd_PnlToLocation" runat="server">
                <contenttemplate>
                    <asp:Label  ID="lbl_LHPOToLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder" meta:resourcekey="lbl_LHPOToLocationResource1"></asp:Label>
            </contenttemplate>
                <triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td  style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="upd_pnl_dg_TASUnloading" runat="server">
                <contenttemplate>
                    <%--<asp:Panel  ID="pnl_TASUnloading" runat="server" GroupingText="TAS Details" Width="100%" Height="200px" ScrollBars="Both" meta:resourcekey="pnl_TASUnloadingResource1">--%>
                <div id="Div_TASUnloading" class="DIV" style=" width:100%; height: 200px">                    
                                   
                <asp:DataGrid  ID="dg_TASDetails" runat="server" CssClass="GRID" AutoGenerateColumns="False" meta:resourcekey="dg_TASDetailsResource1">                
                <FooterStyle CssClass="GRIDFOOTERCSS" />
                <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"/>
                
                <Columns>
                <asp:BoundColumn DataField="Memo_Id" HeaderText="Manifest Id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="article_id" HeaderText="article id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Manifest No"></asp:BoundColumn>
                <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No"></asp:BoundColumn>
                <asp:BoundColumn DataField="Gc_Date" HeaderText="GC Date"></asp:BoundColumn>
                <asp:BoundColumn DataField="Booking_Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                <asp:BoundColumn DataField="Delivery_Location" HeaderText="Delivery Location"></asp:BoundColumn>
                <asp:BoundColumn DataField="Delivery_Type" HeaderText="Delivery Type"></asp:BoundColumn>
                <asp:BoundColumn DataField="Booking_Article" HeaderText="Booking Articles"></asp:BoundColumn>
                <asp:BoundColumn DataField="Booking_Article_Wt" HeaderText="Booking Actual Wt"></asp:BoundColumn>
                                        
                <asp:TemplateColumn HeaderText="Loaded Articles">
                    <ItemTemplate>                
                        <asp:Label  ID="lbl_Loaded_Articles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>' 
                        CssClass="LABEL" meta:resourcekey="lbl_Loaded_ArticlesResource1"></asp:Label>                                            
                    </ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="Loaded Actual Wt.">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Loaded_Actual_Wt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Actual_Wt") %>' 
                        CssClass="LABEL" meta:resourcekey="lbl_Loaded_Actual_WtResource1"></asp:Label>        
                    </ItemTemplate>
                </asp:TemplateColumn>
                
                </Columns>
                </asp:DataGrid>
                <%--</asp:Panel> --%>
                </div> 
                </contenttemplate>
                    <triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                    </triggers>
                </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="upd_TAS_Count" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdn_TAS_Rec_Count" ></asp:HiddenField>   
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                </Triggers>                            
             </asp:UpdatePanel>
             <asp:HiddenField ID="hdn_LHPOCaption" runat="server" />  
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ScheduledArrivalDate" CssClass="LABEL" Text="Scheduled Arrival Date :" runat="server" meta:resourcekey="lbl_ScheduledArrivalDateResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_lbl_ScheduledArrivalDateValue" runat="server">
                <contenttemplate>
                    <asp:Label  ID="lbl_ScheduledArrivalDateValue" runat="server" CssClass="LABEL" Style="font-weight: bolder" meta:resourcekey="lbl_ScheduledArrivalDateValueResource1"></asp:Label>
            </contenttemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
            </triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">&nbsp;</td>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ScheduledArrivalTime" CssClass="LABEL" Text="Scheduled Arrival Time :" runat="server" meta:resourcekey="lbl_ScheduledArrivalTimeResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_lbl_ScheduledArrivalTimeValue" runat="server">
                <contenttemplate>                            
                <asp:Label  ID="lbl_ScheduledArrivalTimeValue" runat="server" CssClass="LABEL" Style="font-weight: bolder" meta:resourcekey="lbl_ScheduledArrivalTimeValueResource1"></asp:Label>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_TASDate"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ReasonforLateArrival" CssClass="LABEL" Text="Reason for Late Arrival :" runat="server" meta:resourcekey="lbl_ReasonforLateArrivalResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_Reason_For_Late_Arrival" runat="server" CssClass="DROPDOWN" Width="98%" meta:resourcekey="ddl_Reason_For_Late_ArrivalResource1"></asp:DropDownList>
        </td>
        <td style="width: 1%"></td>
        <td style="width: 50%" colspan="3"></td>
    </tr> 
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
        </td>
        <td colspan="5" style="width: 79%">
            <asp:TextBox ID="txt_Remarks" Height="40px" runat="server" CssClass="TEXTBOX" MaxLength="250" TextMode="MultiLine" meta:resourcekey="txt_RemarksResource1"></asp:TextBox>
        </td>
    </tr>   
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
                <ContentTemplate>                
                     <asp:Label  ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="dg_TASDetails"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>                 
        </td>
    </tr>    
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_hdn_TimeDiffernceforLate" UpdateMode="Conditional" runat="server">
              <ContentTemplate>         
                    <asp:HiddenField ID="hdn_TimeDiffernceforLate" runat="server" />
              </ContentTemplate>
              <triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
              </triggers>
            </asp:UpdatePanel>
        </td>
    </tr> 
     <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" AccessKey="N" OnClick ="btn_Save_Click" Text="Save & New" CssClass="BUTTON" meta:resourcekey="btn_SaveResource1" />&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click" meta:resourcekey="btn_Save_ExitResource1"/>&nbsp
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" AccessKey="p" OnClick="btn_Save_Print_Click" meta:resourcekey="btn_Save_PrintResource1"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click" meta:resourcekey="btn_CloseResource1"/>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label  ID="Label2"  runat="server" text="Fields with * mark are mandatory" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="Label2Resource1"></asp:Label>
        </td>
    </tr> 
    <tr>
        <td>&nbsp;</td>
    </tr>
    <asp:HiddenField ID="hdf_ResourecString" runat="server" />
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