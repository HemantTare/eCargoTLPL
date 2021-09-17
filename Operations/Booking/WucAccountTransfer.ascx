<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAccountTransfer.ascx.cs" Inherits="Operations_Booking_WucAccountTransfer" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Search_Text.ascx" TagName="Search_Text" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>
<script type ="text/javascript"  src="../../Javascript/Common.js"></script>
<script type ="text/javascript"  src="../../Javascript/Operations/Booking/AccountTransfer.js"></script>

<asp:ScriptManager ID="scm_accountTransfer" runat="server"></asp:ScriptManager>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="ACCOUNT TRANSFER" meta:resourcekey="lbl_headingResource1"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Branch1" runat="server" CssClass="LABEL" Text="Branch:" meta:resourcekey="lbl_Branch1Resource1"></asp:Label>  </td>
        <td style="width: 29%;">
            <asp:Label ID="lbl_Branch" runat="server" ForeColor="Blue" Font-Underline="True" Font-Bold="True" meta:resourcekey="lbl_BranchResource1"></asp:Label></td>
        <td class="TD1" style="width:1%;"></td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="Date:" meta:resourcekey="lbl_DateResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
             <table border="0" cellpadding="0">
                 <tr>
                     <td onmouseup="Button_OnMouseUp(<%= Calendar.ClientObjectId %>)" style="height: 24px" >
                        <ComponentArt:Calendar 
                             ID="AT_Date" runat="server" 
                             CellPadding="2" 
                             ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                             ControlType="Picker" 
                             PickerCssClass="PICKER" 
                             PickerCustomFormat="dd MMM yyyy"
                             PickerFormat="Custom" SelectedDate="2008-10-20" AutoPostBackOnSelectionChanged="True" OnSelectionChanged="AT_Date_SelectionChanged"> 
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
        <td class="TD1" style="width: 1%"></td>
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
                      if (window.<%= Calendar.ClientObjectId %>_loaded && window.<%= AT_Date.ClientObjectId %>_loaded)
                      {
                        window.<%= Calendar.ClientObjectId %>.AssociatedPicker = <%= AT_Date.ClientObjectId %>;
                        window.<%= AT_Date.ClientObjectId %>.AssociatedCalendar = <%= Calendar.ClientObjectId %>;
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
            <asp:Label ID="lbl_AccountTransferNo" runat="server" CssClass="LABEL" Text="Account Transfer No:" meta:resourcekey="lbl_AccountTransferNoResource1"></asp:Label>
        </td>
        <td  style="width: 29%">
            <asp:Label ID="lbl_Account_Transfer_No" runat="server" Font-Bold="True" meta:resourcekey="lbl_Account_Transfer_NoResource1"></asp:Label></td>
        <td class="TD1"></td>
        <td class="TD1" style="width: 20%;"></td>
        <td class="TD1" style="width: 29%"></td>
        <td class="TD1"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_AssociateName" Visible="false" runat="server" CssClass="LABEL" Text="Associate Name:" meta:resourcekey="lbl_AssociateNameResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_Associate_Name" Visible="false" runat="server" CssClass="DROPDOWN" AutoPostBack="True" meta:resourcekey="ddl_Associate_NameResource1" OnSelectedIndexChanged="ddl_Associate_Name_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td class="TD1" style="width: 1%"></td>       
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Find" runat="server" CssClass="LABEL" Text="Find:" meta:resourcekey="lbl_FindResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <uc2:Search_Text ID="Search_Text1" runat="server" />
        </td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
    <tr>
        <td colspan="6" >
        <asp:UpdatePanel ID="upnl_grid" runat="server" UpdateMode="conditional"  >
        <ContentTemplate>      
        
           <table style="width: 100%">
                <tr>
                    <td style="width: 100%">
                       <div id ="Div_AT"  class="DIV" style="height:310px">
                            <asp:DataGrid ID="dg_AccountTransfer" runat="server" AutoGenerateColumns="False" 
                            DataKeyField="GC_Id" CellPadding  = "2" CssClass="GRID"
                            style="border-top-style: none" Width="98%" meta:resourcekey="dg_AccountTransferResource2">
                            
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                            <HeaderStyle   CssClass="GRIDHEADERCSS"/>
                                <Columns>
                                       <asp:TemplateColumn HeaderText="Attach">
                                           <HeaderTemplate>
                                               <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucAccountTransfer1_dg_AccountTransfer');" />
                                           </HeaderTemplate>
                                           <ItemTemplate>
                                               <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString())%>' OnClick="Check_Single(this,'WucAccountTransfer1_dg_AccountTransfer');" runat="server"/>
                                           </ItemTemplate>  
                                       </asp:TemplateColumn>
                                       <asp:BoundColumn DataField="GC_ID" HeaderText="GC Id" Visible="False" ></asp:BoundColumn>
                                       <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="GC No"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date"  DataFormatString ="{0:dd-MM-yyyy}"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="Delivery_Branch_Name" HeaderText="Delivery Branch"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>

                                       <asp:TemplateColumn HeaderText="Articles">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_Art" Text='<%# DataBinder.Eval(Container.DataItem, "Articles") %>' runat="server" BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly ="True" meta:resourcekey="txt_ArtResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Actual Weight">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_Wt" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Weight") %>' runat="server"  BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="95%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly="True" meta:resourcekey="txt_WtResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>  
                                       <asp:TemplateColumn HeaderText="Freight Amount">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_Frt" Text='<%# DataBinder.Eval(Container.DataItem, "Freight_Amt","{0:f}") %>' runat="server" BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="90%"  Font-Size="11px" Font-Names="Verdana" ReadOnly ="True" meta:resourcekey="txt_FrtResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>
                                       <asp:TemplateColumn  HeaderText="Sub Total">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_subtot" Text='<%# DataBinder.Eval(Container.DataItem, "Sub_Total") %>' runat="server"  BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="95%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly="True" meta:resourcekey="txt_subtotResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>  
                                       <asp:TemplateColumn  HeaderText="Service Tax">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_sertax" Text='<%# DataBinder.Eval(Container.DataItem, "Service_Tax_Amount") %>' runat="server"  BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="95%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly="True" meta:resourcekey="txt_subtotResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>                                       
                                        <asp:TemplateColumn  HeaderText="Total GC Amount">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txt_tot_gcamt" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>' runat="server"  BackColor ="Transparent"  BorderStyle ="None"  BorderColor="Transparent"   style="text-align:right" Width ="95%"  Font-Size="11px" Font-Names="Verdana"  ReadOnly="True" meta:resourcekey="txt_subtotResource2"></asp:TextBox> 
                                           </ItemTemplate>
                                       </asp:TemplateColumn>                                      
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_Associate_Name" />
            <asp:AsyncPostBackTrigger ControlID="AT_Date" />            
        </Triggers>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>   
            <asp:Panel ID="pnl_ATTotal" runat="server" Font-Size="11px" GroupingText=" Total " Width="99%" meta:resourcekey="pnl_ATTotalResource1">
                <table width="100%" cellpadding="2">
                        <tr>
                            <td class="TD1" style="width: 15%">
                                <asp:Label ID="lbl_TotalGC" runat="server" CssClass="LABEL" Text="Total GC :" meta:resourcekey="lbl_TotalGCResource1"></asp:Label>
                            </td>
                            <td style="width: 17%" align="left">
                                <asp:TextBox ID="txt_Total_GC" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" EnableTheming="True" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ReadOnly="True" meta:resourcekey="txt_Total_GCResource2"></asp:TextBox>
                                <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                            </td>
                            <td style="width: 1%"></td>
                            <td class="TD1" style="width: 15%;">
                                <asp:Label ID="lbl_TotalWeight" runat="server" CssClass="LABEL" Text="Total Actual Weight :"></asp:Label>
                            </td>
                            <td style="width: 17%;" align="left">
                                <asp:TextBox ID="txt_Total_Wt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ReadOnly="True" meta:resourcekey="txt_Total_WtResource2"></asp:TextBox>
                                <asp:HiddenField ID="hdn_Total_Wt" runat ="server"/>
                            </td>
                            <td style="width: 1%; "></td>
                            <td class="TD1" style="width: 15%;">
                                <asp:Label ID="lbl_TotalBasicFreight" runat="server" CssClass="LABEL" Text="Total Basic Freight :" meta:resourcekey="lbl_TotalBasicFreightResource1"></asp:Label>
                            </td>
                            <td style="width: 17%;" align="left">
                                <asp:TextBox ID="txt_Total_Basic_Freight" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" meta:resourcekey="txt_Total_Basic_FreightResource2"></asp:TextBox>
                                <asp:HiddenField ID ="hdn_Tot_Basic_Fret" runat ="server"/>
                            </td>
                            <td style="width: 1%"></td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 15%;">
                                <asp:Label ID="lbl_TotalArticles" runat="server" CssClass="LABEL" Text="Total Articles :" meta:resourcekey="lbl_TotalArticlesResource1"></asp:Label>
                            </td>
                            <td style="width: 17%;" align="left">
                                <asp:TextBox ID="txt_Total_Articles" runat="server" TabIndex="4" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" meta:resourcekey="txt_Total_ArticlesResource2"></asp:TextBox>
                                <asp:HiddenField  ID ="hdn_Total_Articles"  runat ="server"  />
                            </td>
                            <td style="width: 1%"></td>                       
                            <td class="TD1" style="width: 15%;"> 
                                <asp:Label ID="lbl_TotalAmount" runat="server" CssClass="LABEL" Text="Total GC Amount :" meta:resourcekey="lbl_TotalAmountResource1"></asp:Label>
                            </td>
                            <td style="width: 17%;" align="left">
                                <asp:TextBox ID="txt_Total_GC_Amt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" EnableTheming="True" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" meta:resourcekey="txt_Total_AmtResource2"></asp:TextBox>
                                <asp:HiddenField  ID ="hdn_Total_GC_Amt"  runat ="server"  />
                            </td>
                            <td style="width: 1%"></td> 
                            <td class="TD1" style="width: 15%;"> 
                                <asp:Label ID="lbl_TotalSerTax" runat="server" CssClass="LABEL" Text="Total Service Tax :" meta:resourcekey="lbl_TotalAmountResource11"></asp:Label>
                            </td>
                            <td style="width: 17%;" align="left">
                                <asp:TextBox ID="txt_Total_Ser_Tax" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" EnableTheming="True" ReadOnly="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" meta:resourcekey="txt_Total_AmtResource2"></asp:TextBox>
                                <asp:HiddenField  ID ="hdn_Total_Ser_Tax"  runat ="server"  />
                            </td>
                            <td style="width: 1%"></td>
                        </tr>                
                  </table>            
            </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_AccountTransfer" />
                <asp:AsyncPostBackTrigger ControlID="ddl_Associate_Name" />
                <asp:AsyncPostBackTrigger ControlID="AT_Date" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">&nbsp;</td> 
    </tr> 
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :" meta:resourcekey="lbl_RemarksResource1"></asp:Label>
        </td>
        <td style="width:79%;" colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX" TextMode="MultiLine"
                Height="40px" MaxLength="250" meta:resourcekey="txt_RemarksResource2"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%;"></td>
    </tr>
    
    <tr>
        <td align="left" class="TD1" colspan="5" style="text-align: left">&nbsp;
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource2"></asp:Label><br />&nbsp;
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_Error_ClientResource2"></asp:Label>
        </td>
        <td class="TD1" style="width: 1%">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" meta:resourcekey="btn_SaveResource2" OnClick="btn_Save_Click"/>&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" meta:resourcekey="btn_Save_ExitResource2" OnClick="btn_Save_Exit_Click"/>&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" meta:resourcekey="btn_CloseResource2" OnClick="btn_Close_Click"/>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">&nbsp;</td> 
    </tr> 
</table>