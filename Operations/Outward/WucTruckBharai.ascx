<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTruckBharai.ascx.cs"
    Inherits="Operations_Outward_WucTruckBharai" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Outward/TruckBharai.js"></script>

<script type="text/javascript">
    function get_button_nullsession_clientid()
    {
        btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
    }
     
    
    function Onblur_Hamali_Paid(txt_Hamali_Paid,hdn_Hamali_Charges)
    {
        var txt_Hamali_Paid=document.getElementById(txt_Hamali_Paid);
        var hdn_Hamali_Charges=document.getElementById(hdn_Hamali_Charges);
        
        if(val(txt_Hamali_Paid.value) > val(hdn_Hamali_Charges.value))
        {
            txt_Hamali_Paid.value = hdn_Hamali_Charges.value;
        }
                                      
   } 
   
</script>

<asp:ScriptManager ID="scm_TruckBharai" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" style="width: 100%">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Truck Bharai"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right;">
                        <asp:Label ID="lblTransactionNo" runat="server" CssClass="LABEL" Text="Transaction No :"
                            Width="117px"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:Label ID="txtlblTransactionNo" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label></td>
                    <td style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                        <asp:Label ID="lblTransactionDate" runat="server" CssClass="LABEL" Text="Transaction Date :"></asp:Label></td>
                    <td style="width: 29%; text-align: left;">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)">
                                    <ComponentArt:Calendar ID="dtpTransactionDate" runat="server" CellPadding="2" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                                        ControlType="Picker" PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy"
                                        PickerFormat="Custom" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="dtpTransactionDate_SelectionChanged">
                                    </ComponentArt:Calendar>
                                </td>
                                <td runat="server" id="TD_Calender">
                                    <img alt="" class="calendar_button" onclick="Button_OnClick(this, <%= Calendar.ClientObjectId %>)"
                                        onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" src="../../images/btn_calendar.gif"
                                        width="25" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right;">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 29%; text-align: left;">
                        <ComponentArt:Calendar runat="server" ID="Calendar" AllowMultipleSelection="False"
                            AllowWeekSelection="False" AllowMonthSelection="False" ControlType="Calendar"
                            PopUp="Custom" CalendarTitleCssClass="TITLE" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
                            DayHeaderCssClass="DAYHEADER" DayCssClass="DAY" DayHoverCssClass="DAYHOVER" OtherMonthDayCssClass="OTHERMONTHDAY"
                            SelectedDayCssClass="SELECTEDDAY" CalendarCssClass="CALENDER" NextPrevCssClass="NEXTPREV"
                            MonthCssClass="MONTH" SwapDuration="300" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="../../images/"
                            PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif" />

                        <script type="text/javascript">
                        // Associate the picker and the calendar:
                        function ComponentArt_<%= Calendar.ClientObjectId %>_Associate()
                        {
                          if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= dtpTransactionDate.ClientObjectId %>_loaded)
                          {
                            window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= dtpTransactionDate.ClientObjectId %>;
                            window.<%= dtpTransactionDate.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
                          }
                          else
                          {
                            window.setTimeout('ComponentArt_<%= Calendar.ClientObjectId %>_Associate()', 100);
                          }
                        }
                         ComponentArt_<%= Calendar.ClientObjectId %>_Associate();
                        </script>

                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 230px; text-align: right; vertical-align: top">
                        <asp:Label ID="lblVehicleNo" runat="server" CssClass="LABEL" meta:resourcekey="lblVendorResource1"
                            Text="Vehicle No :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%; text-align: right;">
                    </td>
                    <td style="width: 29%; text-align: left">
                        <asp:DropDownList ID="ddlMemo" runat="server" AutoPostBack="True" CssClass="DROPDOWN"
                            Visible="False" Width="38px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: left; vertical-align: top">
                        <table style="width: 100%">
                            <tr>
                                <td id="tr_dg_TruckBharai1" runat="server" class="TD1" style="text-align: right;
                                    width: 25%; vertical-align: top">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="Div_TruckBharai" class="DIV1" style="border-top: #000000 thin solid; border-left: #000000 thin solid;
                                                border-bottom: #000000 thin solid; height: 250px;">
                                                <asp:DataGrid ID="dg_TruckBharai" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                                    Style="border-top-style: none" Width="100%" OnItemDataBound="dg_TruckBharai_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Attach">
                                                            <HeaderTemplate>
                                                                <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucTruckBharai1_dg_TruckBharai');"
                                                                    disabled="disabled" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                                    OnClick="Check_Single(this,'WucTruckBharai1_dg_TruckBharai','1');" runat="server" />
                                                                <asp:HiddenField ID="hdnMemo_Id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Memo_Id") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                            <HeaderStyle Width="10px" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Invoice No">
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="To_Name" HeaderText="Destination">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Memo_Id" HeaderText="Memo_Id">
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <br />
                                    <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" OnClick="btnGo_Click" Text="Go" /></td>
                                <td class="TD1" style="text-align: left; width: 75%; vertical-align: top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="Div1" class="DIV1" style="width: 100%; border-right: #000000 thin solid;
                                                border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid;">
                                                <asp:DataGrid ID="dg_SelectedMemoDtls" runat="server" AutoGenerateColumns="False"
                                                    CssClass="GRID" Style="border-top-style: none" Width="100%" OnItemDataBound="dg_SelectedMemoDtls_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Attach">
                                                            <HeaderTemplate>
                                                                <input id="chkAllItems" type="checkbox" onclick="Check_AllSelectedMemo(this,'WucTruckBharai1_dg_SelectedMemoDtls');" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                                    OnClick="Check_SingleSelectedMemo(this,'WucTruckBharai1_dg_SelectedMemoDtls','1');Check_SingleSelectedMemoonTextboxChange();"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="hdnMemo_Id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Memo_Id") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                            <HeaderStyle Width="10px" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No">
                                                            <HeaderStyle Width="50px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Destination" HeaderText="Desti">
                                                            <HeaderStyle Width="50px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Cnr Name" HeaderText="Cnr Name">
                                                            <HeaderStyle Width="200px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="LandMark2" HeaderText="Landmark">
                                                            <HeaderStyle Width="50px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Loaded_Articles" HeaderText="Pkgs">
                                                            <HeaderStyle Width="50px" HorizontalAlign="Right" />
                                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PkgsType" HeaderText="Pkgs Type">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Description" HeaderText="Description">
                                                            <HeaderStyle Width="150px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SizeName" HeaderText="Size">
                                                            <HeaderStyle Width="20px" HorizontalAlign="Left" />
                                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Bharai As Per<br/>LR">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Hamali_Paid") %>
                                                                <asp:HiddenField ID="hdn_Hamali_Charges" Value='<%# DataBinder.Eval(Container.DataItem, "Hamali_Paid") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" HorizontalAlign="Right" />
                                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Bharai<br/>Paid">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_Hamali_Paid" runat="server" CssClass="TEXTBOX" MaxLength="10"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Hamali_Paid") %>' onblur="Check_SingleSelectedMemoonTextboxChange()"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_Hamali_Paid" Value='<%# DataBinder.Eval(Container.DataItem, "Hamali_Paid") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                            <HeaderStyle Width="50px" HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="Memo_Id" HeaderText="Memo_Id">
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                            <asp:AsyncPostBackTrigger ControlID="dg_TruckBharai" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td id="tr_dg_TruckBharai2" runat="server" class="TD1" style="text-align: right;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td class="TD1" style="width: 15%; text-align: right;">
                                                        <asp:Label ID="Label1" runat="server" Text="Total Memo :" CssClass="LABEL" Font-Bold="True"
                                                            Width="94px" />
                                                        <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                                                    </td>
                                                    <td style="width: 37%; text-align: left;">
                                                        <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TD1" style="text-align: right">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table border="0" width="30%">
                                                <tr>
                                                    <td class="HIDEGRIDCOL" style="width: 8%">
                                                        <asp:Label ID="lbl_Total_SelectedMemo" runat="server" CssClass="LABEL" Font-Bold="True"
                                                            Text="0"></asp:Label><asp:HiddenField ID="hdn_Total_SelectedMemo" runat="server" />
                                                        <asp:HiddenField ID="hdn_Total_Hamali_Charges" runat="server" />
                                                        <asp:HiddenField ID="hdn_Total_Hamali_Paid" runat="server" />
                                                    </td>
                                                    <td class="TD1" style="width: 8%; text-align: right;">
                                                        <asp:Label ID="lbl_total1" runat="server" CssClass="LABEL" Font-Bold="True" Text="Total :"></asp:Label>
                                                    </td>
                                                    <td class="TD1" style="width: 8%; text-align: left;">
                                                        <asp:Label ID="lbl_Total_Hamali_Charges" runat="server" CssClass="LABEL" Font-Bold="True"
                                                            Text="0"></asp:Label></td>
                                                    <td align="center" class="TD1" style="width: 8%; text-align: left;">
                                                        <asp:Label ID="lbl_Total_Hamali_Paid" runat="server" CssClass="LABEL" Font-Bold="True"
                                                            Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dtpTransactionDate" />
                                            <asp:AsyncPostBackTrigger ControlID="dg_TruckBharai" />
                                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                        <asp:Label ID="lbl_LoadedBy" runat="server" CssClass="LABEL" Text="Loaded By :"></asp:Label></td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddl_LoadedBy" runat="server" AllowNewText="True" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployee" CallBackAfter="2"
                            Text="" InjectJSFunction="" PostBack="False" />
                    </td>
                    <td class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                        <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label></td>
                    <td colspan="4">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="40px" Width="660" CssClass="TEXTBOX"
                            TextMode="MultiLine" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                        <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource1"></asp:Label></td>
                    <td style="width: 29%">
                        &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label></td>
                    <td class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="5" style="vertical-align: top; text-align: center">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & New" AccessKey="N"
                            meta:resourcekey="btn_SaveResource1" OnClick="btn_Save_Click" Visible="false" />
                        <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print"
                            AccessKey="p" OnClick="btn_Save_Print_Click" />
                        <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            AccessKey="S" meta:resourcekey="btn_Save_ExitResource1" OnClick="btn_Save_Exit_Click" />
                        <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
                            OnClick="btn_null_session_Click" Visible="false" />
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                            meta:resourcekey="btn_CloseResource1" OnClick="btn_Close_Click" /></td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; text-align: left" colspan="2">
                        <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are Mandatory"
                            EnableViewState="False"></asp:Label></td>
                    <td class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                        <asp:HiddenField ID="hdn_GCCaption" runat="server" />
                        <asp:HiddenField ID="hdn_Mode" runat="server" />
                    </td>
                    <td style="width: 29%">
                        <asp:HiddenField ID="hdnKeyID" runat="server" />
                    </td>
                    <td class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                        <asp:HiddenField ID="hdn_LoginBranch_Id" runat="server" />
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 230px; text-align: right">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%; text-align: right">
                    </td>
                    <td style="width: 29%; text-align: left">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript"> 
Check_SingleSelectedMemoonTextboxChange();
</script>

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
