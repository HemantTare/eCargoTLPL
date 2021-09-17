<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAUSUnloadingDetails.ascx.cs"
  Inherits="Operations_Inward_WucAUSUnloadingDetails" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
  TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
  TagPrefix="uc3" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Inward/AUS.js"></script>

<%--<asp:ScriptManager ID="scm_AUS" runat="server"></asp:ScriptManager>--%>
<table class="TABLE" style="width: 100%;">
  <tr>
    <td style="width: 100%">
      <table style="width: 100%">
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TURNo" CssClass="LABEL" Text="TUR No :" runat="server" meta:resourcekey="lbl_TURNoResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <asp:Label ID="lbl_TURNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"
              meta:resourcekey="lbl_TURNoValueResource1"></asp:Label></td>
          <td style="width: 1%">
          </td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TURDate" CssClass="LABEL" Text="TUR Date :" runat="server" meta:resourcekey="lbl_TURDateResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <table cellpadding="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                  <ComponentArt:Calendar ID="wuc_AUSDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="MMMM d yyyy" PickerFormat="Custom"
                    SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="wuc_AUSDate_SelectionChanged">
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
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
          </td>
          <td style="width: 29%">
          </td>
          <td class="TD1">
          </td>
          <td class="TD1" style="width: 20%">
          </td>
          <td style="width: 29%">
            <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
              AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar" PopUp="Custom"
              CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
              DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
              SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
              MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
              PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

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
          <td style="width: 1%">
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No :" runat="server"
              meta:resourcekey="lbl_Vehicle_NoResource1"></asp:Label></td>
          <td style="width: 29%;">
            <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%;">
            *</td>
          <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_VehicleCategory" CssClass="LABEL" Text="Vehicle Category :" runat="server"
              meta:resourcekey="lbl_VehicleCategoryResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_VehicleCategory" runat="server">
              <ContentTemplate>
                <asp:Label runat="server" CssClass="LABEL" ID="lbl_Vehicle_Category" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Vehicle_CategoryResource1"></asp:Label>
                <asp:HiddenField runat="server" ID="hdn_Vehicle_Category_Id"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdn_Vehicle_Id"></asp:HiddenField>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%;">
          </td>
        </tr>
        <tr id="trTAS" visible="false" runat="server">
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TASNo" CssClass="LABEL" Text="TAS No :" runat="server" meta:resourcekey="lbl_TASNoResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <asp:UpdatePanel ID="upd_TAS" runat="server">
              <ContentTemplate>
                <asp:DropDownList runat="server" Width="98%" AutoPostBack="True" ID="ddl_TAS" CssClass="DROPDOWN"
                  OnSelectedIndexChanged="ddl_TAS_SelectedIndexChanged" meta:resourcekey="ddl_TASResource1">
                </asp:DropDownList>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Manual_Tur_No" CssClass="LABEL" Text="Reference No :" runat="server"
              meta:resourcekey="lbl_Manual_Tur_NoResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <asp:TextBox ID="txt_Manual_Tur_No" CssClass="TEXTBOX" runat="server" meta:resourcekey="txt_Manual_Tur_NoResource1"></asp:TextBox>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LHPONo" CssClass="LABEL" Text="LHPO No :" runat="server" meta:resourcekey="lbl_LHPONoResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <asp:UpdatePanel ID="upd_LHPO" runat="server">
              <ContentTemplate>
                <asp:DropDownList runat="server" Width="98%" AutoPostBack="True" ID="ddl_LHPO" CssClass="DROPDOWN"
                  OnSelectedIndexChanged="ddl_LHPO_SelectedIndexChanged" meta:resourcekey="ddl_LHPOResource1">
                </asp:DropDownList>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LHPODate" CssClass="LABEL" Text="LHPO Date :" runat="server" meta:resourcekey="lbl_LHPODateResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_LHPODate" runat="server">
              <ContentTemplate>
                <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPO_Date" Style="font-weight: bolder"
                  meta:resourcekey="lbl_LHPO_DateResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%">
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromLocation" CssClass="LABEL" Text="LHPO From Location :" runat="server"
              meta:resourcekey="lbl_FromLocationResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <asp:UpdatePanel ID="Upd_PnlFromLocation" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_LHPOFromLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_LHPOFromLocationResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%">
          </td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ToLocation" CssClass="LABEL" Text="LHPO To Location :" runat="server"
              meta:resourcekey="lbl_ToLocationResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <asp:UpdatePanel ID="Upd_PnlToLocation" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_LHPOToLocation" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_LHPOToLocationResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%">
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_BHTAmount" CssClass="LABEL" Text="BTH Amount :" runat="server"
              meta:resourcekey="lbl_BHTAmountResource1"></asp:Label>
          </td>
          <td style="width: 29%;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_BTHAmountValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_BTHAmountValueResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%">
          </td>
          <td style="width: 20%" />
          <td style="width: 29%" />
          <td style="width: 1%" />
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td style="width: 100%">
      <div id="Div_Aus" runat="server" class="DIV2" style ="height:350px">
        <asp:UpdatePanel ID="upd_pnl_dg_AUSUnloading" runat="server">
          <ContentTemplate>
            <asp:DataGrid ID="dg_UnloadingDetails" runat="server" CssClass="GRID" AutoGenerateColumns="False"
              OnItemDataBound="dg_UnloadingDetails_ItemDataBound" meta:resourcekey="dg_UnloadingDetailsResource1" PageSize="15">
              <FooterStyle CssClass="GRIDFOOTERCSS" />
              <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
              <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
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
                    <asp:Label ID="lbl_Loaded_Articles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>'
                      CssClass="LABEL" meta:resourcekey="lbl_Loaded_ArticlesResource1"></asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Loaded Articles Wt">
                  <ItemTemplate>
                    <asp:Label ID="lbl_Loaded_Actual_Wt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Loaded_Actual_Wt") %>'
                      CssClass="LABEL" meta:resourcekey="lbl_Loaded_Actual_WtResource1"></asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Recieved Articles">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Recieved_Article" runat="server" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "Received_articles") %>'
                      onkeyPress="return Only_Integers(this,event);" Width="80%" MaxLength="7" meta:resourcekey="txt_Recieved_ArticleResource1"></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Recieved Articles Wt">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Recieved_Articles_Wt" runat="server" CssClass="TEXTBOXNOS" Text='<%# DataBinder.Eval(Container.DataItem, "Received_Wt") %>'
                      onkeyPress="return Only_Numbers(this,event);" MaxLength="7" Width="80%" meta:resourcekey="txt_Recieved_Articles_WtResource1"></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Recieved Condition">
                  <ItemTemplate>
                    <asp:DropDownList runat="server" ID="ddl_Received_Condintion" CssClass="DROPDOWN"
                      Width="100px" meta:resourcekey="ddl_Received_CondintionResource1">
                    </asp:DropDownList>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Articles Damaged / Leakage">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Damaged_Leakage_Articles" runat="server" CssClass="TEXTBOXNOS"
                      Text='<%# DataBinder.Eval(Container.DataItem, "damaged_articles") %>' onkeyPress="return Only_Integers(this,event);"
                      Width="80%" MaxLength="7" meta:resourcekey="txt_Damaged_Leakage_ArticlesResource1"></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Damaged Leakage Value">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Damaged_Leakage_Value" runat="server" CssClass="TEXTBOXNOS"
                      Text='<%# DataBinder.Eval(Container.DataItem, "Damaged_Value") %>' onkeyPress="return Only_Numbers(this,event);"
                      MaxLength="7" Width="80%" meta:resourcekey="txt_Damaged_Leakage_ValueResource1"></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Additional Freight">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Additional_Freight" runat="server" MaxLength="7" CssClass="TEXTBOXNOS"
                      Text='<%# DataBinder.Eval(Container.DataItem, "Additional_Freight") %>' onkeyPress="return Only_Numbers(this,event);"
                      Width="80%" meta:resourcekey="txt_Additional_FreightResource1"></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateColumn>
              </Columns>
            </asp:DataGrid>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
          </Triggers>
        </asp:UpdatePanel>
      </div>
    </td>
  </tr>
  <tr>
    <td>
    </td>
  </tr>
  <tr>
    <td>
      <asp:UpdatePanel ID="upd_total" runat="server">
        <ContentTemplate>
          <table style="width: 100%;">
            <tr>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
            </tr>
            <tr>
              <td style="width: 100px">
                <asp:Label ID="lblTotal" runat="server" CssClass="LABEL" meta:resourcekey="lbl_TotalResource1"
                  Style="font-weight: bolder" Text="Total :"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbltxtTotal" runat="server" CssClass="LABEL" meta:resourcekey="lbltxtTotalResource1"
                  Style="font-weight: bolder"></asp:Label></td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px; text-align: right;">
                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" meta:resourcekey="lbl_TotalResource1"
                  Style="font-weight: bolder" Text="Total :"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Booking_Articles" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Booking_ArticlesResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Booking_Articles_Wt" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Booking_Articles_WtResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Loaded_Articles" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Loaded_ArticlesResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Loaded_Articles_Wt" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Loaded_Articles_WtResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Received_Articles" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Received_ArticlesResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Received_Articles_Wt" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Received_Articles_WtResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Damage_Leakage_Articles" runat="server" CssClass="LABEL"
                  Style="font-weight: bolder" meta:resourcekey="lbl_Total_Damage_Leakage_ArticlesResource1"></asp:Label></td>
              <td style="width: 100px">
                <asp:Label ID="lbl_Total_Damage_Leakage_Value" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_Total_Damage_Leakage_ValueResource1"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px">
              </td>
              <td style="width: 100px; text-align: right">
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Booking_Articles"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Booking_Articles_Wt"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Loaded_Articles"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Loaded_Articles_Wt"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Received_Articles"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Received_Articles_Wt"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Damage_Leakage_Articles"></asp:HiddenField>
              </td>
              <td style="width: 100px">
                <asp:HiddenField runat="server" ID="hdn_Total_Damage_Leakage_Value"></asp:HiddenField>
              </td>
            </tr>
          </table>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
          <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate" />
          <asp:AsyncPostBackTrigger ControlID="ddl_LHPO" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td>
    </td>
  </tr>
  <tr>
    <td>
      <table style="width: 98%">
        <tr id="trArticles" runat="server" class="HIDEGRIDCOL">
          <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_TotalShortArticles" CssClass="LABEL" Text="Total Short Articles :"
              runat="server" meta:resourcekey="lbl_TotalShortArticlesResource1"></asp:Label>
          </td>
          <td style="width: 15%">
            <asp:UpdatePanel ID="upd_lbl_TotalShortArticlesValue" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_TotalShortArticlesValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_TotalShortArticlesValueResource1"></asp:Label>
                <asp:HiddenField ID="hdn_Total_Short_Articles" runat="server" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
          <td style="width: 20%" class="TD1">
            <asp:UpdatePanel ID="upd_lbl_TotalExcessArticles" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_TotalExcessArticles" runat="server" Text="Total Excess Articles :"
                  CssClass="LABEL" meta:resourcekey="lbl_TotalExcessArticlesResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucAUSExcessDetails1"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 10%">
            <asp:UpdatePanel ID="upd_lbl_TotalExcessArticlesValue" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_TotalExcessArticlesValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_TotalExcessArticlesValueResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td colspan="2">
          </td>
        </tr>
        <tr id="trScheduledArrival" runat="server" class="HIDEGRIDCOL">
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ScheduledArrivalDate" CssClass="LABEL" Text="Scheduled Arrival Date :"
              runat="server" meta:resourcekey="lbl_ScheduledArrivalDateResource1"></asp:Label>
          </td>
          <td style="width: 15%;">
            <asp:UpdatePanel ID="upd_lbl_ScheduledArrivalDateValue" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_ScheduledArrivalDateValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_ScheduledArrivalDateValueResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ScheduledArrivalTime" CssClass="LABEL" Text="Scheduled Arrival Time :"
              runat="server" meta:resourcekey="lbl_ScheduledArrivalTimeResource1"></asp:Label>
          </td>
          <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_lbl_ScheduledArrivalTimeValue" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_ScheduledArrivalTimeValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                  meta:resourcekey="lbl_ScheduledArrivalTimeValueResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td colspan="2">
          </td>
        </tr>
        <tr id="trTASDateTimeReason" runat="server" class="HIDEGRIDCOL">
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_TASDate" CssClass="LABEL" Text="TAS Date :" runat="server" meta:resourcekey="lbl_TASDateResource1"></asp:Label>
          </td>
          <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_TASDate" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <uc1:WucDatePicker ID="wuc_TASDate" runat="server" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
          <td style="width: 15%;" class="TD1">
            <asp:Label ID="lbl_TASTime" CssClass="LABEL" Text="TAS Time :" runat="server" meta:resourcekey="lbl_TASTimeResource1"></asp:Label>
          </td>
          <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_TASTime" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <uc2:TimePicker ID="wuc_TASTime" runat="server" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ReasonforLateArrival" CssClass="LABEL" Text="Reason for Late Arrival :"
              runat="server" meta:resourcekey="lbl_ReasonforLateArrivalResource1"></asp:Label>
          </td>
          <td style="width: 20%;">
            <asp:UpdatePanel ID="upd_TASReason" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <asp:DropDownList ID="ddl_Reason_For_Late_Arrival" runat="server" CssClass="DROPDOWN"
                  meta:resourcekey="ddl_Reason_For_Late_ArrivalResource1">
                </asp:DropDownList>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr class="HIDEGRIDCOL" id="trTASDateTimeDisplay" runat="server">
          <td style="width: 20%;" class="HIDEGRIDCOL">
            <asp:Label ID="lbl_TAS_DateTime" CssClass="HIDEGRIDCOL" Text="TAS Date Time:" runat="server"
              meta:resourcekey="lbl_TAS_DateTimeResource1"></asp:Label>
          </td>
          <td style="width: 15%;" class="HIDEGRIDCOL">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_TAS_DateTime_ForDisplay" CssClass="HIDEGRIDCOL" runat="server"
                  Style="font-weight: bolder" meta:resourcekey="lbl_TAS_DateTime_ForDisplayResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td style="width: 1%" class="HIDEGRIDCOL">
          </td>
          <td style="width: 20%;" class="HIDEGRIDCOL">
            <asp:Label ID="lbl_TASReason_ForDisplay" CssClass="HIDEGRIDCOL" Text="Reason for Late Arrival :"
              runat="server" meta:resourcekey="lbl_TASReason_ForDisplayResource1"></asp:Label>
          </td>
          <td style="width: 20%;" colspan="4" class="HIDEGRIDCOL">
            <asp:UpdatePanel ID="updTASReason_ForDisplay" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_Reason_For_Late_Arrival_ForDisplay" CssClass="HIDEGRIDCOL" runat="server"
                  Style="font-weight: bolder" meta:resourcekey="lbl_Reason_For_Late_Arrival_ForDisplayResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr id="trUnloading" runat="server" class="HIDEGRIDCOL">
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_UnloadingDate" CssClass="LABEL" Text="Unloading Date :" runat="server"
              meta:resourcekey="lbl_UnloadingDateResource1"></asp:Label>
          </td>
          <td style="width: 10%;">
            <asp:UpdatePanel ID="upd_lbl_UnloadingDateValue" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <asp:Label ID="lbl_UnloadingDateValue" CssClass="LABEL" runat="server" Style="font-weight: bolder"
                  meta:resourcekey="lbl_UnloadingDateValueResource1"></asp:Label>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
          <td style="width: 15%;" class="TD1">
            <asp:Label ID="lbl_UnloadingTime" CssClass="LABEL" Text="Unloading Time :" runat="server"
              meta:resourcekey="lbl_UnloadingTimeResource1"></asp:Label>
          </td>
          <td style="width: 10%;">
            <uc2:TimePicker ID="wuc_UnloadingTime" runat="server" />
          </td>
          <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ReasonforLateUnloading" CssClass="LABEL" Text="Reason for Late Unloading :"
              runat="server" meta:resourcekey="lbl_ReasonforLateUnloadingResource1"></asp:Label>
          </td>
          <td style="width: 20%;">
            <asp:DropDownList ID="ddl_Reason_For_Late_Uploading" runat="server" CssClass="DROPDOWN"
              Width="98%" meta:resourcekey="ddl_Reason_For_Late_UploadingResource1">
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td style="width: 20%;" class="TD1" valign="top">
            <asp:Label ID="lbl_UnloadedbySupervisor" CssClass="LABEL" Text="Unloaded by Supervisor :"
              runat="server" meta:resourcekey="lbl_UnloadedbySupervisorResource1"></asp:Label>
          </td>
          <td style="width: 10%;" valign="top">
            <cc1:DDLSearch ID="ddl_Supervisor" runat="server" AllowNewText="True" IsCallBack="True"
              CallBackAfter="2" Text="" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" />
          </td>
          <td class="TDMANDATORY" style="width: 1%" valign="top">
            </td>
          <td colspan="4" style="width: 50%">
            <asp:UpdatePanel ID="upd_OtherDetails" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <table width="100%">
                  <tr>
                    <td style="vertical-align: top">
                      <asp:Panel ID="Pnl_Receivables" runat="server" Width="100%" GroupingText="Receivables"
                        meta:resourcekey="Pnl_ReceivablesResource1">
                        <table style="width: 100%">
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_To_Pay" runat="server" CssClass="LABEL" Text="To Pay :" meta:resourcekey="lbl_To_PayResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_To_Pay_Value" runat="server" CssClass="LABEL" meta:resourcekey="lbl_To_Pay_ValueResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_To_Pay"></asp:HiddenField>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_UpcountryReceivable" runat="server" CssClass="LABEL" Text="Upcountry Receivable :"
                                meta:resourcekey="lbl_UpcountryReceivableResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_UpcountryReceivableValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_UpcountryReceivableValueResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_UpcountryReceivable"></asp:HiddenField>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Additional_Freight" runat="server" CssClass="LABEL" Text="Additional Freight :"
                                meta:resourcekey="lbl_Additional_FreightResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_Total_Additional_Freight" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Total_Additional_FreightResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_Total_Additional_Freight"></asp:HiddenField>
                            </td>
                          </tr>
                          <tr id="tr_OthersPayable" runat="server">
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Others_Payables" runat="server" CssClass="LABEL" Text="Others :"
                                meta:resourcekey="lbl_Others_PayablesResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:TextBox ID="txt_Others_Payables" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                MaxLength="7" BorderWidth="1px" Width="95%" onchange="Calculate_Total_Payable();"
                                meta:resourcekey="txt_Others_PayablesResource1"></asp:TextBox>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Total_Payable" runat="server" CssClass="LABEL" Text="Total Payable :"
                                meta:resourcekey="lbl_Total_PayableResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_Total_Payable_Value" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                                meta:resourcekey="lbl_Total_Payable_ValueResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_Total_Payable_Value"></asp:HiddenField>
                            </td>
                          </tr>
                        </table>
                      </asp:Panel>
                    </td>
                    <td>
                      <asp:Panel ID="pnl_Payables" runat="server" Width="100%" GroupingText="Payables"
                        meta:resourcekey="pnl_PayablesResource1">
                        <table style="width: 100%">
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Delivery_Commision" runat="server" CssClass="LABEL" Text="DeliveryCommision :"
                                meta:resourcekey="lbl_Delivery_CommisionResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_Total_Delivery_Commision" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Total_Delivery_CommisionResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_Total_Delivery_Commision"></asp:HiddenField>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_UpcountryCrossingCost" runat="server" CssClass="LABEL" Text="Upcountry Crossing Cost:"
                                meta:resourcekey="lbl_UpcountryCrossingCostResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_UpcountryCrossingCostValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_UpcountryCrossingCostValueResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_UpcountryCrossingCost"></asp:HiddenField>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Lorry_Hire" runat="server" CssClass="LABEL" Text="Lorry Hire :"
                                meta:resourcekey="lbl_Lorry_HireResource1"></asp:Label>
                            </td>
                            <td style="width: 39%; text-align: right">
                              <asp:TextBox ID="txt_Lorry_Hire" runat="server" CssClass="TEXTBOXNOSASLABEL" MaxLength="7"
                                BackColor="transparent" BorderWidth="1px" Width="95%" meta:resourcekey="txt_Lorry_HireResource1"
                                ReadOnly="true"></asp:TextBox>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Others_Recevable" runat="server" CssClass="LABEL" Text="Others :"
                                meta:resourcekey="lbl_Others_RecevableResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:TextBox ID="txt_Others_Recevable" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                MaxLength="7" BorderWidth="1px" Width="95%" onchange="Calculate_Total_Receivable();"
                                meta:resourcekey="txt_Others_RecevableResource1"></asp:TextBox>
                            </td>
                          </tr>
                          <tr>
                            <td style="width: 60%">
                              <asp:Label ID="lbl_Total_Recevable" runat="server" CssClass="LABEL" Text="Total Recevable :"
                                meta:resourcekey="lbl_Total_RecevableResource1"></asp:Label>
                            </td>
                            <td style="width: 39%" class="TD1">
                              <asp:Label ID="lbl_Total_Recevable_Value" runat="server" CssClass="LABEL" Style="font-weight: bolder"
                                meta:resourcekey="lbl_Total_Recevable_ValueResource1"></asp:Label>
                              <asp:HiddenField runat="server" ID="hdn_Total_Recevable_Value"></asp:HiddenField>
                            </td>
                          </tr>
                        </table>
                      </asp:Panel>
                    </td>
                  </tr>
                </table>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="ddl_TAS"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="dg_UnloadingDetails"></asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <%--<td style="width: 14%">
                    &nbsp;
                    </td>--%>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
          </td>
          <td colspan="5" style="width: 79%">
            <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX"
              meta:resourcekey="txt_RemarksResource1" Width="90%"></asp:TextBox>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_UnloadingDetails"></asp:AsyncPostBackTrigger>
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td>
    </td>
  </tr>
  <tr>
    <td>
      <%--  <asp:UpdatePanel ID="upd_hdn_Supervisor_Id" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdn_Supervisor_Id" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Supervisor"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>  --%>
      <asp:UpdatePanel ID="Upd_Pnl_hdn_TimeDiffernceforLate" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
          <asp:HiddenField ID="hdn_TimeDiffernceforLate" runat="server" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
        </Triggers>
      </asp:UpdatePanel>
      <asp:HiddenField ID="hdn_Supervisor_Id" runat="server" />
    </td>
  </tr>
  <tr>
    <td style="width: 29%">
      <asp:UpdatePanel ID="Upd_Pnl_IsTAS" runat="server">
        <ContentTemplate>
          <asp:HiddenField ID="hdn_IsTAS" runat="server" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
          <asp:AsyncPostBackTrigger ControlID="wuc_AUSDate"></asp:AsyncPostBackTrigger>
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
</table>

<script type="text/javascript" language="javascript">

 Calculate_Total_Receivable();
 Calculate_Total_Payable();
 
</script>

