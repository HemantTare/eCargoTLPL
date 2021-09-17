<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPOHireDetails.ascx.cs"
  Inherits="Operations_Outward_WucLHPOHireDetails" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID"
  TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
  TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Outward/LHPOHireDetails.js"></script>

<script type="text/javascript" language="javascript">
function SetCommenceDateToLabel()
 {
    var lbl_CommitedDelDateValue = document.getElementById("<%=lbl_CommitedDelDateValue.ClientID %>");
    var txt_TransitDays = document.getElementById("<%=txt_TransitDays.ClientID%>");
    var hdn_CommitedDelDate= document.getElementById("<%=hdn_CommitedDelDate.ClientID%>");
    var LHPODate=new Date();
    LHPODate = <%=WucLHPODate.ClientID%>.GetSelectedDate();
    var CommitedDelDate=new Date();
    CommitedDelDate.setDate(LHPODate.getDate());
    CommitedDelDate.setMonth(LHPODate.getMonth());
    CommitedDelDate.setFullYear(LHPODate.getFullYear());
    CommitedDelDate.setDate(CommitedDelDate.getDate()+Math.ceil(txt_TransitDays.value));    
    lbl_CommitedDelDateValue.innerHTML=(CommitedDelDate).format('MMMM dd, yyyy');
    hdn_CommitedDelDate.value=(CommitedDelDate).format('MMMM dd, yyyy');
 }

</script>

<table style="width: 100%" class="TABLE" border="0">
  <tr>
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_LHPODate" runat="server" CssClass="LABEL" Text="LHPO Date:"></asp:Label></td>
    <td style="width: 29%">
      <table border="0" cellpadding="0">
        <tr>
          <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
            <ComponentArt:Calendar ID="WucLHPODate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
              ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
              SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="WucLHPODate_SelectionChanged">
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
    <td style="width: 1%">
    </td>
    <td style="width: 20%;display:none" class="TD1">
      <asp:Label ID="lbl_VehicleCategory" runat="server" CssClass="LABEL" Text="Vehicle Category:"></asp:Label></td>
    <td style="width: 29%;display:none">
      <asp:DropDownList ID="ddl_VehicleCategory" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
        OnSelectedIndexChanged="ddl_VehicleCategory_SelectedIndexChanged">
      </asp:DropDownList></td>
    <td style="width: 1%;display:none" class="TDMANDATORY" >
      *</td>
  </tr>
  <tr>
    <td class="TD1" style="width: 20%">
    </td>
    <td style="width: 29%">
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= WucLHPODate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= WucLHPODate.ClientObjectId %>;
                            window.<%= WucLHPODate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
    <td style="width: 50%" colspan="3">
    </td>
  </tr>
  <tr>
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_VehicleNo" runat="server" CssClass="LABEL" Text="Vehicle No:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_WucVehicleSearch1" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td style="width: 20%;display:none" class="TD1">
      <asp:Label ID="lbl_VehicleCapacity" runat="server" CssClass="LABEL" Text="Vehicle Capacity:"></asp:Label></td>
    <td style="width: 29%;display:none">
      <asp:UpdatePanel ID="Upd_Pnl_lbl_VehicleCapacityValue" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_VehicleCapacityValue" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%">
    </td>
  </tr>
  <tr>
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_LHPOType" runat="server" CssClass="LABEL" Text="LHPO Type:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_LHPOType" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <asp:DropDownList ID="ddl_LHPOType" Enabled="false" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
            OnSelectedIndexChanged="ddl_LHPOType_SelectedIndexChanged">
          </asp:DropDownList>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td style="width: 20%" class="TD1">
    </td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_hdn_LHPOId" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
        </Triggers>
        <ContentTemplate>
          <asp:HiddenField ID="hdn_LHPOId" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%">
    </td>
  </tr>
  <tr>
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_LHPONo" runat="server" CssClass="LABEL" Text="LHPO No:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_LHPONo" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPOType" />
        </Triggers>
        <ContentTemplate>
          <asp:DropDownList ID="ddl_LHPONo" runat="server" AutoPostBack="true" CssClass="DROPDOWN"
            OnSelectedIndexChanged="ddl_LHPONo_SelectedIndexChanged">
          </asp:DropDownList>
          <asp:TextBox ID="lbl_LHPONoValue" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:TextBox>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td style="width: 20%">
      <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPOType" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_Start_End_No" runat="server" Font-Bold="true"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_hdn_AttachedLHPODate" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
        </Triggers>
        <ContentTemplate>
          <asp:HiddenField ID="hdn_AttachedLHPODate" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%">
    </td>
  </tr>
  <tr style="display:none">
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_ManualRefNo" runat="server" CssClass="LABEL" Text="Manual Ref. No:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_txt_ManualRefNo" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <asp:TextBox ID="txt_ManualRefNo" runat="server" CssClass="TEXTBOX"></asp:TextBox>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
    </td>
    <td style="width: 20%" class="TD1">
    </td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_hdn_ToLocationBranchId" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation" />
        </Triggers>
        <ContentTemplate>
          <asp:HiddenField ID="hdn_ToLocationBranchId" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%">
    </td>
  </tr>
  <tr style="display:none">
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_FromLocation" runat="server" CssClass="LABEL" Text="From Location:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_FromLocation" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <cc1:DDLSearch ID="ddl_FromLocation" runat="server" AllowNewText="True" CallBackAfter="2"
            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetFromServiceLocationOfLHPO"
            IsCallBack="True" OnTxtChange="ddl_FromLocation_TxtChange" PostBack="True" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_ToLocation" runat="server" CssClass="LABEL" Text="To Location:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_ToLocation" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <cc1:DDLSearch ID="ddl_ToLocation" runat="server" AllowNewText="True" CallBackAfter="2"
            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetToServiceLocationOfLHPO"
            IsCallBack="True" OnTxtChange="ddl_ToLocation_TxtChange" PostBack="True" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
  </tr>
  <tr style="display:none">
    <td style="width: 20%;" class="TD1">
      <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_Owner" runat="server" CssClass="LABEL" Text="Owner:">
          </asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_lbl_OwnerValue" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_OwnerValue" runat="server" Font-Bold="True"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
    </td>
    <td style="width: 50%" class="TD1" colspan="3">
    </td>
  </tr>
  <tr runat="server">
    <td runat="server" id="td_Broker" colspan="6" width="100%">
      <asp:UpdatePanel ID="Upd_Pnl_BrokerVisible" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <table style="width: 100%">
            <tr>
              <td style="width: 20%;" class="TD1">
                <asp:Label ID="lbl_BrokerName" runat="server" CssClass="LABEL" Text="Broker Name:"></asp:Label>
              </td>
              <td style="width: 29%">
                <asp:DropDownList ID="ddl_BrokerName" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_BrokerName_SelectedIndexChanged"
                  AutoPostBack="True">
                </asp:DropDownList>
              </td>
              <td style="width: 1%" class="TDMANDATORY">
                <asp:Label ID="lbl_Man1" runat="server" Text="*"></asp:Label>
              </td>
              <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_TDSCertificateTo" runat="server" CssClass="LABEL" Text="TDS Certificate To:"></asp:Label>
              </td>
              <td style="width: 29%">
                <asp:DropDownList ID="ddl_TDSCertificateTo" Width="70%" runat="server" AutoPostBack="false"
                  CssClass="DROPDOWN">
                  <asp:ListItem Value="0">-- Select One --</asp:ListItem>
                  <asp:ListItem Value="1">Owner</asp:ListItem>
                  <asp:ListItem Value="2">Broker</asp:ListItem>
                </asp:DropDownList>
              </td>
              <td style="width: 1%" class="TDMANDATORY">
                <asp:Label ID="lbl_Man2" runat="server" Text="*"></asp:Label>
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
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_Driver1" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <cc1:DDLSearch ID="ddl_Driver1" runat="server" AllowNewText="True" CallBackAfter="2"
            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDriver" IsCallBack="True"
            PostBack="False" Text="" />
          &nbsp;
          <asp:LinkButton ID="lbtn_AddDriver" Font-Bold="true" OnClientClick="return Add_Driver_Window()"
            runat="server" Text="Add New"></asp:LinkButton>
          <asp:HiddenField ID="hdn_Driver_path" runat="server" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td style="width: 20%;display:none" class="TD1">
      <asp:Label ID="lbl_Driver2" runat="server" CssClass="LABEL" Text="Driver 2:"></asp:Label></td>
    <td style="width: 29%;display:none">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_Driver2" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <cc1:DDLSearch ID="ddl_Driver2" runat="server" AllowNewText="True" CallBackAfter="2"
            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDriver" IsCallBack="True"
            PostBack="False" Text="" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%;display:none" class="TDMANDATORY">
    </td>
  </tr>
  <tr style="display:none;">
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_Cleaner" runat="server" CssClass="LABEL" Text="Cleaner:"></asp:Label></td>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_ddl_Cleaner" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
        </Triggers>
        <ContentTemplate>
          <cc1:DDLSearch ID="ddl_Cleaner" runat="server" AllowNewText="True" CallBackAfter="2"
            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetCleaner" IsCallBack="True"
            PostBack="False" Text="" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
    </td>
    <td style="width: 50%;" class="TD1" colspan="3">
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <table style="width: 100%">
        <tr>
          <td style="width: 100%">
            <div id="Div_Memo" class="DIV" style="height: 170px">
              <asp:UpdatePanel ID="Upd_Pnl_dg_LHPOHireDetails" UpdateMode="Conditional" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                  <asp:AsyncPostBackTrigger ControlID="WucLHPODate" />
                </Triggers>
                <ContentTemplate>
                  <asp:DataGrid ID="dg_LHPOHireDetails" runat="server" AutoGenerateColumns="False"
                    DataKeyField="Memo_Id" CellPadding="2" CssClass="GRID" Style="border-top-style: none"
                    Width="98%">
                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                    <Columns>
                      <asp:TemplateColumn HeaderText="Attach">
                        <HeaderTemplate>
                          <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucLHPO1_WucLHPOHireDetails1_dg_LHPOHireDetails');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "IsTrue").ToString()) %>'
                            OnClick="Check_Single(this,'WucLHPO1_WucLHPOHireDetails1_dg_LHPOHireDetails');"
                            runat="server" />
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Manifest No"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Memo_Date" HeaderText="Manifest Date" DataFormatString="{0:dd-MM-yyyy}">
                      </asp:BoundColumn>
                      <asp:BoundColumn DataField="Memo_Type" HeaderText="Manifest Type"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Branch_Name" HeaderText="Manifest To"></asp:BoundColumn>
                      <asp:TemplateColumn HeaderText="Total GC">
                        <ItemTemplate>
                          <asp:TextBox ID="txt_Tot_GC" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC") %>'
                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                            BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True"
                            Width="80%" MaxLength="7"></asp:TextBox>
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Total Articles">
                        <ItemTemplate>
                          <asp:TextBox ID="txt_Tot_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Loaded_Articles") %>'
                            BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True"
                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Dot(this,event)"
                            Width="80%" MaxLength="7"></asp:TextBox>
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Total Actual Wt.">
                        <ItemTemplate>
                          <asp:TextBox ID="txt_Tot_Act_Wt" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Total_Loaded_Weight") %>'
                            runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Dot(this,event)"
                            Width="80%" MaxLength="7"></asp:TextBox>
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn ItemStyle-CssClass="HIDEGRIDCOL" HeaderStyle-CssClass="HIDEGRIDCOL">
                        <ItemTemplate>
                          <asp:HiddenField ID="hdn_CrossingCost" Value='<%# DataBinder.Eval(Container.DataItem, "Crossing_Cost") %>'
                            runat="server" />
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn ItemStyle-CssClass="HIDEGRIDCOL" HeaderStyle-CssClass="HIDEGRIDCOL">
                        <ItemTemplate>
                          <asp:HiddenField ID="hdn_DeliveryCommision" Value='<%# DataBinder.Eval(Container.DataItem, "Delivery_Commision") %>'
                            runat="server" />
                        </ItemTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn ItemStyle-CssClass="HIDEGRIDCOL" HeaderStyle-CssClass="HIDEGRIDCOL">
                        <ItemTemplate>
                          <asp:HiddenField ID="hdn_ToPayCollection" Value='<%# DataBinder.Eval(Container.DataItem, "ToPay_Collection") %>'
                            runat="server" />
                        </ItemTemplate>
                      </asp:TemplateColumn>
                    </Columns>
                  </asp:DataGrid>
                </ContentTemplate>
              </asp:UpdatePanel>
            </div>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td colspan="6" align="right">
      <asp:UpdatePanel ID="Upd_Pnl_TotalofGrid" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_LHPOHireDetails" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
          <table style="width: 50%">
            <tr>
              <td style="width: 16%" align="left">
                <asp:Label ID="lbl_GridTotal" runat="server" CssClass="LABEL" Font-Bold="True" Text="Total:"></asp:Label></td>
              <td style="width: 7%" align="left" visible="false">
                <asp:TextBox ID="txt_TotalGC" Visible="false" runat="server" BackColor="Transparent"
                  BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana"
                  Font-Size="11px" Font-Bold="True" Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                <asp:HiddenField ID="hdn_TotalGC" Visible="false" runat="server" />
              </td>
              <td align="center" style="width: 3%">
                &nbsp;
              </td>
              <td style="width: 10%" align="center">
                <asp:TextBox ID="txt_TotalArticle" runat="server" BackColor="Transparent" BorderColor="Transparent"
                  BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                  Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                <asp:HiddenField ID="hdn_TotalArticle" runat="server" />
              </td>
              <td align="center" style="width: 4%">
                &nbsp;
              </td>
              <td style="width: 14%" align="center">
                <asp:TextBox ID="txt_TotalArticleWT" runat="server" BackColor="Transparent" BorderColor="Transparent"
                  BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                  Width="60%" CssClass="TEXTBOXNOS"></asp:TextBox>
                <asp:HiddenField ID="hdn_TotalArticleWT" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="left" style="width: 16%">
              </td>
              <td align="left" style="width: 7%">
                <asp:HiddenField ID="hdn_TotalCrossingCost" runat="server" />
              </td>
              <td align="center" style="width: 3%">
              </td>
              <td align="center" style="width: 10%">
                <asp:HiddenField ID="hdn_TotalDeliveryCommision" runat="server" />
              </td>
              <td align="center" style="width: 4%">
              </td>
              <td align="center" style="width: 14%">
                <asp:HiddenField ID="hdn_TotalToPayCollection" runat="server" />
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td colspan="6" style="width: 100%">
    </td>
  </tr>
  <tr>
    <td colspan="2" style="vertical-align: top">
      <asp:UpdatePanel ID="Upd_Pnl_pnl_VehicleHireDetails" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
          <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
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
                  <asp:DropDownList ID="ddl_FreightType" Width="93%" onchange="EnabledDisabledControlOnFreightType();CalculateTruckHireCharge(1);"
                    runat="server" CssClass="DROPDOWN">
                  </asp:DropDownList>&nbsp;<font color="red" style="font-weight: bold; font-family: Verdana;
                    font-size: 11px">*</font>
                  <asp:HiddenField ID="hdn_FreightType" runat="server" />
                </td>
              </tr>
              <tr id="tr_WtGuarantee" runat="server">
                <td style="width: 41.1%" class="TD1" runat="server">
                  <asp:Label ID="lbl_WtGuarantee" runat="server" CssClass="LABEL" Text="Wt. Guarantee: "></asp:Label></td>
                <td style="width: 58%" runat="server">
                  <asp:TextBox ID="txt_WtGuarantee" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                    onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>
                  <asp:HiddenField ID="hdn_WtGuarantee" runat="server" />
                </td>
              </tr>
              <tr id="tr_RateKg" runat="server">
                <td style="width: 41.1%" class="TD1" runat="server">
                  <asp:Label ID="lbl_RateKg" runat="server" CssClass="LABEL" Text="Rate/Kg:"></asp:Label></td>
                <td style="width: 58%" runat="server">
                  <asp:TextBox ID="txt_RateKg" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                    onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>
                  <asp:HiddenField ID="hdn_RateKg" runat="server" />
                </td>
              </tr>
              <tr id="tr_ActualKms" runat="server">
                <td style="width: 41.1%" class="TD1" runat="server">
                  <asp:Label ID="lbl_ActualKms" runat="server" CssClass="LABEL" Text="Actual Kms:"></asp:Label></td>
                <td style="width: 58%" align="left" runat="server">
                  <asp:UpdatePanel ID="Upd_Pnl_lbl_ActualKmsValue" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="ddl_FromLocation" />
                      <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation" />
                    </Triggers>
                    <ContentTemplate>
                      <asp:Label ID="lbl_ActualKmsValue" runat="server" Width="93%" CssClass="TEXTBOXNOS"
                        Font-Bold="True">0</asp:Label>
                      <asp:HiddenField ID="hdn_ActualKms" runat="server" />
                    </ContentTemplate>
                  </asp:UpdatePanel>
                </td>
              </tr>
              <tr id="tr_ActualWtPayable" runat="server">
                <td style="width: 41.1%" class="TD1" runat="server">
                  <asp:Label ID="lbl_ActualWtPayable" runat="server" CssClass="LABEL" Text="Actual Wt. Payable:"></asp:Label></td>
                <td style="width: 58%" align="left" runat="server">
                  <asp:Label ID="lbl_ActualWtPayableValue" runat="server" Width="93%" CssClass="TEXTBOXNOS"
                    Font-Bold="True">0</asp:Label>
                  <asp:HiddenField ID="hdn_ActualWtPayable" runat="server" />
                </td>
              </tr>
              <tr id="tr_TruckHireCharge" runat="server">
                <td style="width: 41.1%;" class="TD1" runat="server">
                  <asp:Label ID="lbl_TruckHireCharge" runat="server" CssClass="LABEL" Text="Truck Hire Charge:"></asp:Label></td>
                <td style="width: 58%;" align="left" runat="server">
                  <asp:Label ID="lbl_TruckHireChargeValue" runat="server" Width="93%" CssClass="TEXTBOXNOS"
                    Font-Bold="True">0</asp:Label>
                  <asp:HiddenField ID="hdn_TruckHireCharge" runat="server" />
                </td>
              </tr>
              <tr id="tr_txt_TruckHireCharge" runat="server">
                <td style="width: 41.1%" class="TD1" runat="server">
                  <asp:Label ID="lbl_txt_TruckHireCharge" runat="server" CssClass="LABEL" Text="Truck Hire Charge:"></asp:Label></td>
                <td style="width: 58%" runat="server">
                  <asp:TextBox ID="txt_TruckHireCharge" runat="server" Width="91%" onkeypress="return Only_Numbers(this,event);"
                    onblur="CalculateTruckHireCharge(0)" CssClass="TEXTBOXNOS" MaxLength="18">0</asp:TextBox>
                  &nbsp;<font color="red" style="font-weight: bold; font-family: Verdana; font-size: 11px">*</font></td>
              </tr>
              <tr>
                <td class="TD1" style="width: 41.1%" id="tr_OtherCharges" runat="server">
                  <asp:LinkButton ID="lnkbtn_OtherCharges" runat="server" Text="Other Charges:"></asp:LinkButton>
                </td>
                <td style="width: 58%">
                  <asp:TextBox ID="txt_OtherCharges" runat="server" CssClass="TEXTBOXNOS" MaxLength="18"
                    onkeypress="return Only_Numbers(this,event);" onblur="CalculateTDS();">0</asp:TextBox>
                  <asp:HiddenField ID="hdn_OtherCharges" runat="server" />
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                      <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                      <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                      <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                      <asp:AsyncPostBackTrigger ControlID="ddl_FreightType" />
                      <asp:AsyncPostBackTrigger ControlID="WucLHPOAttachedBranch1" />
                    </Triggers>
                    <ContentTemplate>
                      <table border="0" style="width: 100%">
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_TDSPer" runat="server" Text="TDS %:"></asp:Label></td>
                          <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_TDSPerValue" runat="server" Text="0" Style="text-align: right"
                              Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_TDSPer" runat="server" />
                          </td>
                          <td style="width: 10%">
                            %</td>
                          <td style="width: 28%" align="right">
                            <asp:Label ID="lbl_TDSPerValue1" Style="text-align: right; caption-side: right" CssClass="LABEL"
                              runat="server" Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_TDSAmount" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_ExemptionLimit" Visible="false" runat="server" Text="Exemption Limit%:"></asp:Label></td>
                          <td colspan="3" align="right">
                            <asp:Label ID="lbl_ExemptionLimitPer" Visible="false" runat="server" Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_ExemptionLimit" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_SurchargeAmount" runat="server" Text="Surcharge @:"></asp:Label></td>
                          <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_SurchargePer" Style="text-align: right" CssClass="LABEL" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_Surcharge" runat="server" />
                          </td>
                          <td style="width: 10%">
                            %</td>
                          <td style="width: 28%" align="right">
                            <asp:Label ID="lbl_SurchargeAmountValue" Style="text-align: right; caption-side: right"
                              CssClass="LABEL" runat="server" Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_SurchargeAmount" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_AddlSurchargeCessAmount" runat="server" Text="Addl Surcharge Cess @:"></asp:Label></td>
                          <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_Addl_Surcharges_CessPer" Style="text-align: right" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_Addl_Surcharges_CessPer" runat="server" />
                          </td>
                          <td style="width: 10%">
                            %</td>
                          <td style="width: 28%" align="right">
                            <asp:Label ID="lbl_AddlSurchargeCessAmountValue" CssClass="LABEL" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_AddlSurchargeCessAmount" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_AddlEducationCessAmount" runat="server" Text="Addl Education Cess @:"></asp:Label></td>
                          <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_Addl_Education_CessPer" Style="text-align: right" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_Addl_Education_Cess" runat="server" />
                          </td>
                          <td style="width: 10%">
                            %</td>
                          <td style="width: 28%" align="right">
                            <asp:Label ID="lbl_AddlEducationCessAmountValue" CssClass="LABEL" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdn_AddlEducationCessAmount" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td class="TD1" style="width: 41.1%">
                            <asp:Label ID="lbl_ExemptionLimitAmount" Visible="false" runat="server" Text="ExemptionLimit Amount:"></asp:Label></td>
                          <td colspan="3" align="right">
                            <asp:Label ID="lbl_ExemptionLimitAmountValue" CssClass="LABEL" Visible="false" runat="server"
                              Text="0" Font-Bold="True"></asp:Label>
                          </td>
                        </tr>
                      </table>
                    </ContentTemplate>
                  </asp:UpdatePanel>
                </td>
              </tr>
              <tr>
                <td class="TD1" style="width: 41.1%">
                  <asp:Label ID="lbl_TDSAmount" runat="server" Text="Total TDS Amount:"></asp:Label></td>
                <td style="width: 58%" align="right">
                  <asp:Label ID="lbl_TDSAmountValue" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
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
                  <asp:Label ID="lbl_LoadingCharge" runat="server" Text="Loading Charge:"></asp:Label>
                </td>
                <td style="width: 58%">
                  <asp:TextBox ID="txt_LoadingCharge" runat="server" onkeypress="return Only_Numbers(this,event);"
                    CssClass="TEXTBOXNOS" Text="0" MaxLength="18"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="TD1" style="width: 41.1%">
                  <asp:Label ID="lbl_TotalAfterTDSDeduction" runat="server" Text="Total After TDS Deduction:"></asp:Label></td>
                <td style="width: 58%" align="right">
                  <asp:Label ID="lbl_TotalAfterTDSDedValue" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                <asp:HiddenField ID="hdn_TotalAfterTDSDedValue" runat="server" />
              </tr>
              <tr id="tr_CharityLedger" runat="server">
                <td class="TD1" style="width: 41.1%">
                  <asp:Label ID="lbl_Charity" runat="server" Text="Charity:"></asp:Label>
                </td>
                <td style="width: 58%" align="right">
                  <cc1:DDLSearch ID="ddl_Charity" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetCharityLedger"
                    CallBackAfter="2" AllowNewText="True" PostBack="False" Text=""></cc1:DDLSearch>
                </td>
              </tr>
              <tr id="tr_CharityAmount" runat="server">
                <td class="TD1" style="width: 41.1%">
                  <asp:Label ID="lbl_CharityAmount" runat="server" Text="Charity Amount:"></asp:Label>
                </td>
                <td style="width: 58%" align="right">
                  <asp:TextBox ID="txt_CharityAmount" runat="server" onkeypress="return Only_Numbers(this,event);"
                    CssClass="TEXTBOXNOS" Text="0" onblur="CheckCharityAmount()" MaxLength="18"></asp:TextBox>
                  <asp:HiddenField ID="hdn_CharityAmount" runat="server" />
                </td>
              </tr>
              <tr>
                <td class="TD1" style="width: 41.1%">
                  <asp:Label ID="lbl_TotalTruckHire" runat="server" Text="Total Truck Hire:"></asp:Label></td>
                <td style="width: 58%" align="right">
                  <asp:Label ID="lbl_TotalTruckHireValue" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                <asp:HiddenField ID="hdn_TotalTruckHire" runat="server" />
              </tr>
            </table>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td colspan="4" style="vertical-align: top">
      <table width="100%">
        <tr>
          <td>
            <asp:UpdatePanel ID="Upd_Pnl_pnl_TotalTDSCalculation" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                <asp:AsyncPostBackTrigger ControlID="dg_LHPOHireDetails" />
              </Triggers>
              <ContentTemplate>
                <asp:Panel ID="pnl_TotalTDSCalculation" runat="server" Width="100%" GroupingText=" ">
                  <table style="width: 100%;" border="0">
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_TotalAdvance" runat="server" Text="Total Advance:" CssClass="LABEL"></asp:Label></td>
                      <td style="width: 59%;" align="left">
                        <asp:TextBox ID="txt_TotalAdvance" Width="60%" onblur="CalculateBalanceAmount();SetATHPaybleAlertsBranchesTotalAdvance();EnabledDisabledBalancePayableOnBalanceAmountChange();"
                          runat="server" onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOXNOS"
                          MaxLength="10" OnTextChanged="txt_TotalAdvance_TextChanged"></asp:TextBox></td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_BalanceAmount" runat="server" CssClass="LABEL">Balance Amount:</asp:Label></td>
                      <td style="width: 59%;" align="left">
                        <asp:HiddenField ID="hdn_BalanceAmount" runat="server" />
                        <asp:Label ID="lbl_BalanceAmountValue" Width="61%" runat="server" CssClass="TEXTBOXNOS"
                          Font-Bold="True">0</asp:Label></td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" colspan="3" style="width: 100%">
                        <uc3:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server"></uc3:WucHierarchyWithID>
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td>
            <asp:UpdatePanel ID="Upd_Pnl_OtherPayable" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_ToLocation" />
                <asp:AsyncPostBackTrigger ControlID="dg_LHPOHireDetails" />
              </Triggers>
              <ContentTemplate>
                <asp:Panel ID="Pnl_OtherPayable" Style="vertical-align: top" runat="server" GroupingText="Other Payable"
                  Width="100%">
                  <table style="width: 100%; vertical-align: top" border="0">
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_CrossingCost" runat="server" Text="Crossing Cost:"></asp:Label></td>
                      <td style="width: 59%" align="left">
                        <asp:Label ID="lbl_CrossingCostValue" CssClass="TEXTBOXNOS" runat="server" Width="61%"
                          Text="0" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_CrossingCost" runat="server" />
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_DeliveryCommission" runat="server" Text="Delivery Commission:"></asp:Label></td>
                      <td style="width: 59%" align="left">
                        <asp:Label ID="lbl_DeliveryCommissionValue" CssClass="TEXTBOXNOS" runat="server"
                          Width="61%" Text="0" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_DeliveryCommission" runat="server" />
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%;">
                        <asp:Label ID="lbl_Others" runat="server" Text="Others:"></asp:Label></td>
                      <td style="width: 59%;">
                        <asp:TextBox ID="txt_Others" runat="server" Width="60%" CssClass="TEXTBOXNOS" onblur="CalculateOtherPayable();"
                          onkeypress="return Only_Numbers(this,event);" Text="0" MaxLength="12"></asp:TextBox>
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_TotalPayable" runat="server" Text="Total Payable:"></asp:Label></td>
                      <td style="width: 59%" align="left">
                        <asp:Label ID="lbl_TotalPayableValue" CssClass="TEXTBOXNOS" Width="61%" runat="server"
                          Text="0" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_TotalPayable" runat="server" />
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_ToPayCollection" runat="server" Text="To Pay Collection:"></asp:Label>
                      </td>
                      <td style="width: 59%" align="left">
                        <asp:Label ID="lbl_ToPayCollectionValue" CssClass="TEXTBOXNOS" Width="61%" runat="server"
                          Text="0" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_ToPayCollection" runat="server" />
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 40%">
                        <asp:Label ID="lbl_NetAmount" runat="server" Text="Net Amount:"></asp:Label></td>
                      <td style="width: 59%" align="left">
                        <asp:Label ID="lbl_NetAmountValue" runat="server" CssClass="TEXTBOXNOS" Width="61%"
                          Text="0" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="hdn_NetAmount" runat="server" />
                      </td>
                      <td style="width: 1%" />
                    </tr>
                    <tr>
                      <td colspan="3">
                        <asp:HiddenField ID="hdn_ExemptionLimitAmount" runat="server" />
                        <asp:HiddenField ID="hdn_Total_No_of_GC" runat="server" />
                        <asp:HiddenField ID="hdn_is_page_loaded" Value='0' runat="server" />
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr style="display:none;">
    <td colspan="6">
      <asp:Panel ID="Panel4" runat="server" GroupingText=" " Width="100%">
        <table style="width: 100%">
          <tr>
            <td class="TD1" style="width: 25%;">
              <asp:Label ID="lbl_LHCTerminatedByReceivingCash" runat="Server" Text="LHC Terminated By Receiving Cash(Vasuli)"
                CssClass="LABEL" /></td>
            <td style="width: 23%">
              <asp:CheckBox ID="Chk_LHCTerminatedByReceivingCash" onclick="EnableDisableTerminatedLHCControl();"
                CssClass="CHECKBOX" runat="server" />
            </td>
            <td style="width: 2%" />
            <td style="width: 25%;" class="TD1">
              <asp:Label ID="lbl_ReceivedCashAmt" runat="server" Text="Terminated LHC Received Cash:"
                CssClass="LABEL" />
            </td>
            <td style="width: 23%">
              <asp:TextBox ID="txt_ReceivedAmtTerminatedLHC" runat="server" CssClass="TEXTBOXNOS"
                onkeypress="return Only_Numbers(this,event);" />
            </td>
            <td style="width: 2%" />
          </tr>
          <tr>
            <td class="TD1" style="width: 25%;">
              <asp:Label ID="lbl_LHCTerminatedByDebit" runat="Server" Text="LHC Terminated By Debit "
                CssClass="LABEL" /></td>
            <td style="width: 23%">
              <asp:CheckBox ID="Chk_LHCTerminatedByDebit" onclick="EnableDisableLedegerTerminated();"
                CssClass="CHECKBOX" runat="server" />
            </td>
            <td style="width: 2%" />
            <td style="width: 25%;" class="TD1">
              <asp:Label ID="lbl_LedgerId" runat="server" Text="Ledger Name:" CssClass="LABEL" />
            </td>
            <td style="width: 23%" id="td_ddl_LHCTermiantedByDebitToLedger" runat="server">
              <cc1:DDLSearch ID="ddl_LHCTermiantedByDebitToLedger" runat="server" IsCallBack="True"
                InjectJSFunction="" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerTerminatedLHC"
                CallBackAfter="2" AllowNewText="True" PostBack="False" Text=""></cc1:DDLSearch>
            </td>
            <td style="width: 2%" />
          </tr>
        </table>
        <table style="width: 100%">
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_LoadingSupervisor" runat="server" Text="Loading Supervisor:"></asp:Label></td>
            <td style="width: 28%">
              <asp:UpdatePanel ID="Upd_Pnl_ddl_LoadingSupervisor" UpdateMode="Conditional" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                  <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                  <cc1:DDLSearch ID="ddl_LoadingSupervisor" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSupervisor"
                    CallBackAfter="2" AllowNewText="True" PostBack="False" Text=""></cc1:DDLSearch>
                </ContentTemplate>
              </asp:UpdatePanel>
            </td>
            <td style="width: 2%" class="TDMANDATORY">
              *</td>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_VehicleDepartureTime" runat="server" Text="Vehicle Departure Time:"></asp:Label></td>
            <td style="width: 28%" runat="server" id="td_DepartureTime">
              <asp:UpdatePanel ID="Upd_Pnl_Wuc_VehicleDepartureTime" UpdateMode="Conditional" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
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
              <asp:Label ID="lbl_TransitDays" runat="server" Text="Transit Days:"></asp:Label>
            </td>
            <td style="width: 28%">
              <asp:UpdatePanel ID="Upd_Pnl_txt_TransitDays" UpdateMode="Conditional" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                  <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>
                  <asp:TextBox ID="txt_TransitDays" runat="server" onkeypress="return Only_Numbers(this,event);"
                    onblur="SetCommenceDateToLabel()" CssClass="TEXTBOXNOS" Text="0" MaxLength="4"></asp:TextBox>
                </ContentTemplate>
              </asp:UpdatePanel>
            </td>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_CommitedDelDate" runat="server" Text="Committed Del. Date:"></asp:Label></td>
            <td style="width: 28%">
              <asp:UpdatePanel ID="Upd_Pnl_lbl_CommitedDelDateValue" UpdateMode="Conditional" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                  <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                  <asp:AsyncPostBackTrigger ControlID="WucLHPODate" />
                  <asp:AsyncPostBackTrigger ControlID="ddl_LHPOType" />
                </Triggers>
                <ContentTemplate>
                  <asp:Label ID="lbl_CommitedDelDateValue" runat="server" Font-Bold="True"></asp:Label>
                  <asp:HiddenField ID="hdn_CommitedDelDate" runat="server" />
                </ContentTemplate>
              </asp:UpdatePanel>
            </td>
            <td style="width: 2%">
            </td>
          </tr>
        </table>
      </asp:Panel>
    </td>
  </tr>
  <tr style="display:none;">
    <td style="width: 20%" class="TD1">
      <asp:Label ID="lbl_Remark" runat="server" CssClass="LABEL" Text="Remark:"></asp:Label></td>
    <td colspan="4">
      <%-- <asp:UpdatePanel ID="Upd_Pnl_txt_Remark" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
                <ContentTemplate>--%>
      <asp:TextBox ID="txt_Remark" runat="server" CssClass="TEXTBOX" MaxLength="250" Height="40px"></asp:TextBox>
      <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
    </td>
    <td style="width: 2%">
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>&nbsp;
      <%-- <asp:UpdatePanel ID="Upd_Pnl_HIDDENFields" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPONo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleCategory" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_BrokerName" />
                </Triggers>
                <ContentTemplate>                                      
                    <asp:HiddenField ID="hdn_ExemptionLimitAmount" runat="server" />                    
                    <asp:HiddenField ID="hdn_Total_No_of_GC" runat="server" />                 
                    <asp:HiddenField ID="hdn_is_page_loaded" Value='0' runat="server" />                    
                </ContentTemplate>
            </asp:UpdatePanel>--%>
    </td>
  </tr>
  <tr>
    <td colspan="6" style="display: none">
      <asp:CheckBox ID="chk_Is_ATH_Enabled" runat="server" />
      <asp:CheckBox ID="chk_Is_PostBack_On_Advance_Amt" runat="server" />
      <asp:HiddenField ID="hdn_FromLocation_Parameter" runat="server" />
      <asp:HiddenField ID="hdn_ToLocation_Parameter" runat="server" />
      <asp:HiddenField ID="hdn_Balance_Pay_At_Parameter" runat="server" />
      <asp:HiddenField ID="hdn_DVLPID" runat="server" />
      <asp:HiddenField ID="hdn_DVLPFromBranchID" runat="server" />
    </td>
  </tr>
</table>
<asp:HiddenField ID="hdn_Next_No" runat="server" />
<asp:HiddenField ID="hdn_Start_No" runat="server" Value="0" />
<asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />
<asp:HiddenField ID="hdn_Padded_Next_No" runat="server" />
<asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" />
<asp:HiddenField ID="hdn_Mode" runat="server" />
<asp:HiddenField ID="hdn_max_advance_amount" runat="server" />
<asp:HiddenField ID="hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge" runat="server" />
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
      z-index: 100">
      <span id="ajaxloading">
        <table>
          <tr>
            <td>
              <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
          </tr>
          <tr>
            <td align="center">
              Wait! Action in Progress...</td>
          </tr>
        </table>
      </span>
    </div>
  </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
EnabledDisabledControlOnFreightType();
EnableDisableTerminatedLHCControl();
EnableDisableLedegerTerminated();
</script>

