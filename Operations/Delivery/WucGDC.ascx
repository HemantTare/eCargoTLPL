<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucGDC.ascx.cs" Inherits="Operations_Delivery_WucGDC" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
  TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/GodownDeliverySheet.js"></script>

<asp:ScriptManager ID="SCM_GDS" runat="Server">
</asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

function fnValidatePAN(Obj) 
{
        var ddl_PhotoIDType = document.getElementById('<%=ddl_PhotoIDType.ClientID %>');
        if (Obj == null) Obj = window.event.srcElement;
        if (ddl_PhotoIDType.value == "1") 
        {
        if (Obj.value != "")
        {
            ObjVal = Obj.value;
            var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
            var code = /([C,P,H,F,A,T,B,L,J,G])/;
            var code_chk = ObjVal.substring(3,4);
            if (ObjVal.search(panPat) == -1) {
                alert("Invalid Pan No");
                Obj.value ="";
                Obj.focus();
                return false;
            }
            if (code.test(code_chk) == false) {
                alert("Invaild PAN Card No.");
                Obj.value ="";
                return false;
            }
        }
        }
   }
   
function DoorToGodown(Path)
    {   
        window.open(Path, 'Update', 'width=1200, height=800,top=50,left=50,menubar=no, resizable=no,scrollbars=yes')
        return false;
    } 
</script>

<table class="TABLE" style="width: 100%">
  <tr>
    <td class="TDGRADIENT" style="width: 100%">
      &nbsp;
      <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Godown Delivery Confirmation(GDC)"
        meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
  </tr>
  <tr>
    <td style="width: 100%">
      <table style="width: 100%">
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GDCNo" runat="server" CssClass="LABEL" Text="GDC No :" meta:resourcekey="lbl_GDCNoResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <asp:Label ID="lbl_GDC_No" runat="server" CssClass="LABEL" Style="font-weight: bolder"
              meta:resourcekey="lbl_GDC_NoResource1"></asp:Label>
          </td>
          <td style="width: 1%">
          </td>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GDCDate" runat="server" CssClass="LABEL" Text="GDC Date :" meta:resourcekey="lbl_GDCDateResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <table border="0" cellpadding="0">
              <tr>
                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px">
                  <ComponentArt:Calendar ID="dtp_GDC_Date" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                    ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                    SelectedDate="2008-10-20" OnSelectionChanged="dtp_GDC_Date_SelectionChanged" AutoPostBackOnSelectionChanged="True">
                  </ComponentArt:Calendar>
                </td>
                <td style="height: 24px" runat="server" id="TD_Calender">
                  <img alt="" class="calendar_button" height="22" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                    onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                    width="25" />
                </td>
              </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
              <ContentTemplate>
                <asp:HiddenField ID="hdn_GDC_Date" runat="server" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtp_GDC_Date" />
              </Triggers>
            </asp:UpdatePanel>
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
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtp_GDC_Date.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtp_GDC_Date.ClientObjectId %>;
                            window.<%= dtp_GDC_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
          <td class="TD1" colspan="4" id="td_gccontrol" runat="server">
            <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" onblur="setfousonGrid('WucGDC1_dg_GDC');" />
          </td>
          
          <td class="TD1" id="td3" runat="server">
          <asp:Button ID="Button1" runat="server" CssClass="BUTTON" Text="Door To Godown" OnClick="btn_DoorToGodown_Click" />
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div id="Div_GDC" class="DIV" style="height: 250px">
            <asp:DataGrid ID="dg_GDC" runat="server" AutoGenerateColumns="False" CssClass="GRID"
              Style="border-top-style: none" Width="97%" OnItemDataBound="dg_GDC_ItemDataBound"
              meta:resourcekey="dg_GDCResource1" OnItemCreated="dg_GDC_ItemCreated">
              <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
              <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
              <FooterStyle CssClass="GRIDFOOTERCSS" />
              <Columns>
                <asp:TemplateColumn HeaderText="Attach">
                  <HeaderTemplate>
                   <input id="Checkbox1" type="checkbox" onclick="Check_All(this,'WucGDC1_dg_GDC');" />
                    <%--<asp:CheckBox id="chkAllItems" runat="server" OnClick="Check_All(this,'WucGDC1_dg_GDC');" />--%>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                      OnClick="Check_Single(this,'WucGDC1_dg_GDC','1');" runat="server" meta:resourcekey="Chk_AttachResource1" />
                  </ItemTemplate>
                  <ItemStyle Width="10px" />
                  <HeaderStyle Width="10px" />
                </asp:TemplateColumn>
                <%--<asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No" >
                  <ItemStyle Width="50px" />
                  <HeaderStyle Width="50px" />
                </asp:BoundColumn>--%>
                <asp:TemplateColumn HeaderText="GC No">
                  <ItemTemplate>
                    <asp:LinkButton ID="lbtn_GCNoForPrint" Text='<%# DataBinder.Eval(Container, "DataItem.GC_No_For_Print") %>'
                      Font-Bold="True" Font-Underline="True" runat="server" CommandName="GCNoForPrint"
                      CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GC_ID") %>' />
                    <asp:HiddenField ID="hdn_GCNoForPrint" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.GC_ID") %>' />
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ConsigneeName" HeaderText="Consignee Name" >
                  <ItemStyle Width="200px" />
                  <HeaderStyle Width="200px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Mobile No">
                  <ItemTemplate>
                    <asp:TextBox ID="txtMobileNo" Text='<%# DataBinder.Eval(Container.DataItem, "MobileNo") %>'
                      runat="server" CssClass="TEXTBOX" Width="99%" MaxLength="10"></asp:TextBox>
                  </ItemTemplate>
                  <ItemStyle Width="100px" />
                  <HeaderStyle Width="100px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Bal<br>Qty">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Bal_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>'
                      runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                      Style="text-align: right" Font-Size="11px" Font-Names="Verdana" ReadOnly="True" Width ="30px"
                      meta:resourcekey="txt_Bal_ArtResource1"></asp:TextBox>
                  </ItemTemplate>
                  <HeaderStyle Width="50px" />
                  <ItemStyle Width="50px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Del<br>Qty">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Delivery_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Articles") %>'
                      runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                      Style="text-align: right" Font-Size="11px" Font-Names="Verdana" ReadOnly="True" Width ="30px"
                      meta:resourcekey="txt_Delivery_ArtResource1"></asp:TextBox>
                  </ItemTemplate>
                  <ItemStyle Width="50px" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Payment_Type" HeaderText="Pay Type">
                  <HeaderStyle Width="30px" />
                  <ItemStyle Width="30px" /></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="LR Frt.">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Total_GC_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                      runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                      BorderColor="Transparent" Style="text-align: right" Font-Size="11px" Font-Names="Verdana"  Width ="50px"
                      ReadOnly="True" meta:resourcekey="Total_GC_AmountResource1"></asp:TextBox>
                  </ItemTemplate>
                  <ItemStyle Width="50px" />
                  <HeaderStyle Width="50px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Dly<br>Status">
                  <ItemTemplate>
                    <asp:DropDownList ID="ddl_DeliveryStatus" runat="server" CssClass="DROPDOWN" AutoPostBack="true"
                      OnSelectedIndexChanged="ddl_DeliveryStatus_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdn_DeliveryStatus" runat="server" />
                  </ItemTemplate>
                  <ItemStyle Width="40px" />
                  <HeaderStyle Width="40px" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Booking_Branch_Name" HeaderStyle-CssClass="HIDEGRIDCOL"
                  HeaderText="Booking Branch" ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date" DataFormatString="{0:dd/MM/yyyy}">
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Committed_Del_Date" HeaderText="Expected Delivery Date"
                  DataFormatString="{0:dd/MM/yyyy}">
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Packing_Type" HeaderText="Packing Type">
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Booking_Articles" HeaderText="Booking Articles" HeaderStyle-CssClass="HIDEGRIDCOL"
                  ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                <asp:BoundColumn DataField="Booking_Actual_Wt" HeaderText="Booking Actual Wt" HeaderStyle-CssClass="HIDEGRIDCOL"
                  ItemStyle-CssClass="HIDEGRIDCOL"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Balance Actual Wt">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Bal_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Actual_Wt") %>'
                      runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                      Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                      meta:resourcekey="txt_Bal_WtResource1"></asp:TextBox>
                  </ItemTemplate>
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Delivery Actual Wt.">
                  <ItemTemplate>
                    <asp:TextBox ID="txt_Delivery_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery_Actual_Wt") %>'
                      runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                      Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"
                      meta:resourcekey="txt_Delivery_WtResource1"></asp:TextBox>
                  </ItemTemplate>
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Delivery Time">
                  <ItemTemplate>
                    <uc3:TimePicker ID="TimePicker1" runat="server" />
                  </ItemTemplate>
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="GC Id">
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                  <ItemTemplate>
                    <asp:HiddenField ID="hdn_GC_Id" Value='<%# DataBinder.Eval(Container.DataItem, "GC_Id") %>'
                      runat="server" />
                    <asp:HiddenField ID="hdn_Status_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Previous_Status_ID") %>'
                      runat="server" />
                    <asp:Label ID="lbl_GC_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_Id")%>' />
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Article Id">
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                  <ItemTemplate>
                    <asp:HiddenField ID="hdn_Article_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Article_Id") %>'
                      runat="server" />
                    <asp:Label ID="lbl_Article_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Article_Id")%>' />
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="AUS_Date1Time">
                  <ItemTemplate>
                    <asp:Label ID="AUS_Date1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AUS_Date1")%>' />
                    <asp:Label ID="AUS_Time" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AUS_Time")%>' />
                  </ItemTemplate>
                  <ItemStyle CssClass="HIDEGRIDCOL" />
                  <HeaderStyle CssClass="HIDEGRIDCOL" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Dly<br>Details">
                  <ItemTemplate>
                    <asp:LinkButton ID="lbtn_details" Text="Details" Font-Bold="True" Font-Underline="True"
                      runat="server" meta:resourcekey="lbtn_detailsResource1" Width="60px" />
                  </ItemTemplate>
                  <ItemStyle Width="60px" />
                  <HeaderStyle Width="60px" />
                </asp:TemplateColumn>
              </Columns>
            </asp:DataGrid>
          </div>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
          <asp:AsyncPostBackTrigger ControlID="dtp_GDC_Date" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td>
      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
          <td style="font-weight: bold; font-size: 11px; width: 50%; font-family: Verdana;
            text-align: left">
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
              Width="50px" CssClass="NOTUPDATEDLBL"></asp:Label>&nbsp; Octroi Not Updated</td>
          <td style="width: 50%; text-align: right">
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <table width="100%" border="0">
            <tr>
              <td class="TD1" style="width: 10%">
                <asp:Label ID="Label1" runat="server" Text="Total GC :" CssClass="LABEL" Font-Bold="True"
                  meta:resourcekey="Label1Resource1" />
              </td>
              <td style="width: 40%">
                <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                  meta:resourcekey="lbl_Total_GCResource1" />
                <asp:HiddenField ID="hdn_Total_GC" runat="server" />
              </td>
              <td class="TD1" style="width: 8%">
                <asp:Label ID="lbl_tolal1" runat="server" Text="Total :" CssClass="LABEL" Font-Bold="True"
                  meta:resourcekey="lbl_tolal1Resource1" />
              </td>
              <td align="center" style="width: 8%">
                <asp:Label ID="lbl_totalDelArt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                  meta:resourcekey="lbl_totalDelArtResource1" />
                <asp:HiddenField ID="hdn_totalDelArt" runat="server" />
              </td>
              <td align="center" style="width: 8%; display : none">
                <asp:Label ID="lbl_totalDelWt" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                  meta:resourcekey="lbl_totalDelWtResource1" />
                <asp:HiddenField ID="hdn_totalDelWt" runat="server" />
              </td>
              <td class="TD1" style="width: 20%">
                &nbsp;</td>
            </tr>
          </table>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
          <asp:AsyncPostBackTrigger ControlID="dtp_GDC_Date" />
          <asp:AsyncPostBackTrigger ControlID="dg_GDC" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <table width="100%">
      
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DeliveredTo" runat="server" CssClass="LABEL" Text="Delivered To :"
              meta:resourcekey="lbl_DeliveredToResource"></asp:Label>
          </td>
          <td style="width: 20%">
           <asp:TextBox ID="txt_DeliveredTo" runat="server" CssClass="TEXTBOX" MaxLength="100" 
           width="100%" meta:resourcekey="txt_DeliveredToResource"></asp:TextBox>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>

           <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DeliveredToMobile" runat="server" CssClass="LABEL" Text="Mobile No.:"
              meta:resourcekey="lbl_DeliveredToMobileResource"></asp:Label>
          </td>
          <td style="width: 20%">
           <asp:TextBox ID="txt_DeliveredToMobile" runat="server" CssClass="TEXTBOX" MaxLength="10" 
           width="100%" onkeypress = "return Only_Numbers(this,event)" meta:resourcekey="txt_DeliveredToMobileResource"></asp:TextBox>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>


        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_PhotoIDType" runat="server" CssClass="LABEL" Text="Photo ID Type :"
              meta:resourcekey="lbl_PhotoIDTypeResource"></asp:Label>
          </td>
          <td style="width: 20%">
            <asp:DropDownList ID="ddl_PhotoIDType" runat="server" CssClass="DROPDOWN" 
            width="100%" meta:resourcekey="ddl_PhotoIDTypeResource" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>

           <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_PhotoIDNo" runat="server" CssClass="LABEL" Text="Photo ID No.:"
              meta:resourcekey="lbl_PhotoIDNoResource"></asp:Label>
          </td>
          <td style="width: 20%">
           <asp:TextBox ID="txt_PhotoIDNo" runat="server" CssClass="TEXTBOX" MaxLength="15" 
           width="100%" onkeyup="return Uppercase(this);" onblur="fnValidatePAN(this);" meta:resourcekey="txt_PhotoIDNoResource"></asp:TextBox>
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>


        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VehicleType" runat="server" CssClass="LABEL" Text="Vehicle Type :"
              meta:resourcekey="lbl_VehicleTypeResource"></asp:Label>
          </td>
          <td style="width: 20%">
            <asp:DropDownList ID="ddl_VehicleType" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
            width="100%" OnSelectedIndexChanged="ddl_VehicleType_SelectedIndexChanged" meta:resourcekey="ddl_VehicleTypeResource" />
          </td>
          <td style="width: 1%">
            </td>

           <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VehicleNo" runat="server" CssClass="LABEL" Text="Vehicle No.:"
              meta:resourcekey="lbl_VehicleNoResource"></asp:Label>
          </td>
          <td style="width: 20%">
            
            <asp:TextBox ID="txt_Number_Part1" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="onlycharacters(this);Uppercase(this)"
                        MaxLength="3" Width="20%" Enabled="false" meta:resourcekey="txt_Number_Part1Resource1" />
            
            <asp:TextBox ID="txt_Number_Part2" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="Uppercase(this);valid(this)"
                            MaxLength="2" onkeypress = "return Only_Numbers(this,event)"  Width="10%" Enabled="false"  meta:resourcekey="txt_Number_Part2Resource1" />

            <asp:TextBox ID="txt_Number_Part3" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="onlycharacters(this);Uppercase(this)"
                            onkeyup="onlycharacters(this)" MaxLength="2" Width="10%" Enabled="false" meta:resourcekey="txt_Number_Part3Resource1" />

             <asp:TextBox ID="txt_Number_Part4" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="Uppercase(this);valid(this)"
                            MaxLength="4" onkeypress = "return Only_Numbers(this,event)"  Width="30%" Enabled="false" meta:resourcekey="txt_Number_Part4Resource1" />
                        

          </td>
          <td style="width: 1%">
            </td>
        </tr>              
      
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GodownSupervisor" runat="server" CssClass="LABEL" Text="Godown Supervisor :"
              meta:resourcekey="lbl_GodownSupervisorResource1"></asp:Label>
          </td>
          <td style="width: 29%">
            <cc1:DDLSearch ID="ddl_GodownSupervisor" runat="server" AllowNewText="True" IsCallBack="True"
              CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" CallBackAfter="2"
              Text="" InjectJSFunction="" PostBack="False" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td style="width: 50%" colspan="3">
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
          </td>
          <td colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="MultiLine"
              MaxLength="250" meta:resourcekey="txt_RemarksResource1"></asp:TextBox></td>
          <td style="width: 1%">
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
      <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label>&nbsp;
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:HiddenField ID="hdn_GCCaption" runat="server" />
    </td>
  </tr>
  <tr>
    <td class="TD1" colspan="6" style="text-align: center">
      <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
        OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />&nbsp
      <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
        AccessKey="S" OnClick="btn_Save_Exit_Click" meta:resourcekey="btn_Save_ExitResource1" />&nbsp
      <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
        AccessKey="p" OnClick="btn_Save_Print_Click" />&nbsp
      <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
        OnClick="btn_Close_Click" meta:resourcekey="btn_CloseResource1" />
      <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
        OnClick="btn_null_session_Click" />
    </td>
  </tr>
  <tr>
    <td colspan="6">
      &nbsp;</td>
  </tr>
  <tr>
    <td colspan="6">
      &nbsp;<asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="fields with * mark are mandatory"
        meta:resourcekey="Label2Resource1"></asp:Label>&nbsp;
    </td>
  </tr>
</table>
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
