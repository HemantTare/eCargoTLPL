<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODCoverGeneration.ascx.cs" Inherits="Operations_POD_WucPODCoverGeneration" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucPODSentBy.ascx" TagName="WucPODSentBy" TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript"  src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript"  src="../../Javascript/Operations/POD/PODCoverGeneration.js"></script>
<script type="text/javascript"  src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="SCM_PODCG" runat="Server"></asp:ScriptManager>

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

</script>
<table class="TABLE" border="0">
    <tr>
        <td class="TDGRADIENT" style="width: 100%" colspan="6">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="POD Cover Generation" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>      
        <td class="TD1" style="width:20%">
            <asp:Label ID="lbl_CoverNo" runat="server" CssClass="LABEL" Text="Cover No :" meta:resourcekey="lbl_CoverNoResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Cover_No" runat="server" CssClass="LABEL" Style="font-weight: bolder" meta:resourcekey="lbl_Cover_NoResource1"></asp:Label>
        </td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CoverDate" runat="server" CssClass="LABEL" Text="Cover Date :" meta:resourcekey="lbl_CoverDateResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <table border="0" cellpadding="0">
                <tr>
                    <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px" >
                    <ComponentArt:Calendar ID="dtp_Cover_Date" runat="server" 
                         CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                         ControlType="Picker" PickerCssClass="PICKER" 
                         PickerCustomFormat="dd MMM yyyy"
                         PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="dtp_Cover_Date_SelectionChanged"> 
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
        <td style="width: 1%"></td>
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
              if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_Cover_Date.ClientObjectId %>_loaded)
              {
                window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_Cover_Date.ClientObjectId %>;
                window.<%= dtp_Cover_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
        <td colspan="6" style="width:100%">
            <uc1:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />        
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
        <uc1:WucPODSentBy ID="WucPODSentBy1" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6" id="td_gccontrol" runat="server">
            <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />                    
        </td>                   
    </tr>
    <tr>
        <td colspan="6"> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
            <div id ="Div_Cover"  class="DIV" style="height:250px">
                
                <asp:DataGrid ID="dg_PODCover" runat="server" AutoGenerateColumns="False"  
                    CssClass="GRID" style="border-top-style: none" Width="98%" meta:resourcekey="dg_PODCoverResource1">
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                    <Columns>
                    
                        <asp:TemplateColumn HeaderText="Attach">
                               <HeaderTemplate>
                                   <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucPODCoverGeneration1_dg_PODCover');" />
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' OnClick="Check_Single(this,'WucPODCoverGeneration1_dg_PODCover');" runat="server"/>
                               </ItemTemplate>  
                        </asp:TemplateColumn>
                            
                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="GC_Date" HeaderText="Bkg Date" DataFormatString ="{0:dd/MM/yyyy}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Bkg Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Delivery_Branch_Name" HeaderText="Dly Branch" DataFormatString ="{0:dd/MM/yyyy}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking_Type" HeaderText="Bkg Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
               <asp:AsyncPostBackTrigger ControlID="dtp_Cover_Date" />
             </Triggers>
       </asp:UpdatePanel>
        </td>        
    </tr>
    <tr>
        <td colspan="6">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="TD1" style="width: 19%">
                            <asp:Label ID="lbl_totGC" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_totGCResource1"/>
                        </td>
                        <td>
                            <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_Total_GCResource1"/>
                            <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                        </td>
                    </tr>
                </table>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
               <asp:AsyncPostBackTrigger ControlID="dtp_Cover_Date" />
             </Triggers>
       </asp:UpdatePanel>
        </td>
    </tr> 
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
        </td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine" MaxLength="250" meta:resourcekey="txt_RemarksResource1"></asp:TextBox></td>
        <td style="width: 1%"></td>
    </tr> 
    <tr>
        <td colspan="6">           
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" ></asp:Label>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>   
     <tr>
        <td colspan="6">&nbsp;
        </td>
    </tr>  
     <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_GCError" runat="server" />
             <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="fields with * mark are mandatory" meta:resourcekey="Label2Resource1"></asp:Label>&nbsp;
        </td>
    </tr>     
    
</table>
<script type="text/javascript" language="javascript">
</script>