<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBTHMultiple.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucBTHMultiple" %>
<%@ Register Src="~/Finance/Accounting Vouchers/WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>

<asp:ScriptManager ID="SCMBTHMultiple" runat="server"></asp:ScriptManager>
<script language="javascript" src="../../Javascript/DatePicker.js" type="text/javascript"></script>
<script language="javascript" src="../../Javascript/ddlsearch.js" type="text/javascript"></script>
<script language="javascript" src="../../Javascript/Common.js"type="text/javascript"></script>
<script language="javascript" src="../../Javascript/Finance/Accounting Vouchers/BTHMultiple.js"type="text/javascript"></script>

<script type="text/javascript">

function GetTotalAmount()
{
    var txt_Toatal_Payable_Amount = document.getElementById('<%=txt_TotalPayableAmount.ClientID %>')    
    return val(txt_Toatal_Payable_Amount.value);
}


</script>

<table class="TABLE" width="100%">

    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Balance Truck Hire (Multiple Entry)"></asp:Label></td>
    </tr>
    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BTHVoucherNo" runat="server" Text="BTH Voucher No. :" CssClass="LABEL" /></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_BTHVoucherNo" BackColor="Transparent" BorderColor="Transparent" BorderStyle="Solid" ReadOnly="True" 
               CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="86%"></asp:TextBox></td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BTHVoucherDate" runat="server" Text="BTH Voucher Date :" CssClass="LABEL" /></td>
        <td style="width: 29%">
            <ComponentArt:Calendar id="Wuc_BTHVoucherDate" runat="server" PickerFormat="Custom" 
                            PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker" 
                            AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" 
                            SelectedDate="6-DEC-2008" AutoPostBackOnSelectionChanged="true"
                            OnSelectionChanged="Wuc_BTHVoucherDate1_SelectionChanged"/>
        </td>
        <td style="width: 1%"></td>
    </tr>     
    <tr runat="server" id="tr_LhPODate">
        <td style="width: 20%" class="TD1">
            <asp:Label ID="Label1" runat="server" CssClass="LABEL" Text="LHPO From :"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="dtp_LHPO_From" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="Label2" runat="server" CssClass="LABEL" Text="LHPO To :"></asp:Label></td>
        <td style="width: 29%;">
            <uc1:WucDatePicker ID="dtp_LHPO_To" runat="server" /></td>
        <td style="width: 1%;" class="TDMANDATORY"></td>
     </tr>
     <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_referenceNo" runat="server" CssClass="LABEL" Text="Reference No. :"></asp:Label></td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_ReferenceNo" runat="server" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox></td>
        <td style="width: 1%;" class="TDMANDATORY"></td>
        <td style="width: 50%;" colspan="3"></td>
     </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_OwnerBroker" runat="server" CssClass="LABEL" Text="Owner/Broker1 :"></asp:Label></td>
        <td style="width: 29%">
            <cc1:DDLSearch ID="ddl_OwnerBroker" runat="server" CallBackAfter="3" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetOwnerBroker" PostBack="true" OnTxtChange="ddl_OwnerBroker_TxtChange" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">
            <asp:Button ID="btn_Go" runat="server" CssClass="BUTTON" Text= "Get Details" OnClick ="btn_Go_OnClick" />
        </td>
        <td style="width: 29%"></td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
       <td colspan="6">
       <table width="100%">
        <tr>
            <td style="width:100%">
           <%-- <asp:Panel ID="pnl_BTH" runat="server" Height="300px" GroupingText="LHPO Details" ScrollBars="horizontal">--%>
            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="always">
                <ContentTemplate>      
                        <div runat="server" id="div_BTH" style="height:300px;width:100%;overflow:scroll"> 
                        <asp:DataGrid ID="dg_BTH_LHPO" runat="server" AutoGenerateColumns="False" CssClass="GRID" 
                        style="border-top-style: none;" Width="130%" OnItemDataBound="dg_BTH_LHPO_ItemDataBound"><HeaderStyle CssClass="GRIDHEADERCSS"/><Columns><asp:TemplateColumn HeaderText="Attach"><HeaderTemplate><input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucBTHMultiple1_dg_BTH_LHPO');" />
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' OnClick="Check_Single(this,'WucBTHMultiple1_dg_BTH_LHPO','1');" runat="server"/></ItemTemplate>  
                            </asp:TemplateColumn>                          
                            <asp:TemplateColumn HeaderText = "LHPO No"><ItemTemplate><asp:Label ID="lbl_LHPONo" Text='<%# DataBinder.Eval(Container.DataItem, "LHPO_No_For_Print") %>' runat="server"></asp:Label></ItemTemplate>
                            </asp:TemplateColumn> 
                            <asp:TemplateColumn HeaderText = "AUS No"><ItemTemplate><asp:LinkButton ID="lbtn_AUSNo" Text='<%# DataBinder.Eval(Container.DataItem, "AUS_No") %>' runat="server"></asp:LinkButton><asp:HiddenField ID="hdn_AUS_Id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AUS_Id") %>'/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="LHPO_Date" HeaderText="LHPO Date" DataFormatString ="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Manual_Ref_No" HeaderText="Manual Ref No."></asp:BoundColumn>
                            <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Driver_Name" HeaderText="Driver"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LHPO_Branch_Name" HeaderText="LHPO Branch"></asp:BoundColumn>
                            <asp:BoundColumn DataField="From_Location" HeaderText="From Location"></asp:BoundColumn>
                            <asp:BoundColumn DataField="To_Location" HeaderText="To Location"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Truck_Hire_Charge" HeaderText="Hire Amount"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TDS_Amount" HeaderText="TDS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Total_Advance_To_Be_Paid" HeaderText="Advance Payable"></asp:BoundColumn>
                           
                            <asp:TemplateColumn HeaderText = "Actual Balance" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txt_ActualBalance" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Payble_Amount") %>' BackColor="Transparent" BorderStyle="None" BorderColor="Transparent" runat="server" Width="90%" Font-Size="11px" Font-Names="Verdana"></asp:TextBox></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText = "Balance To Be Paid" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txt_BalanceToBePaid" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Paid_Amount") %>' runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTNOS" Width="90%" Font-Size="11px" Font-Names="Verdana"></asp:TextBox></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Is Add/Less"><ItemTemplate><asp:DropDownList ID="ddl_AddLess" runat="server" CssClass="DROPDOWN"><asp:ListItem Text="Select One" Value="0"></asp:ListItem><asp:ListItem Text="Add" Value="1"></asp:ListItem><asp:ListItem Text="Less" Value="2"></asp:ListItem></asp:DropDownList>                                  
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Ledger Name"><ItemTemplate><cc1:DDLSearch ID="ddl_LedgerName" runat="server" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedger"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Other Amount"><ItemTemplate><asp:TextBox ID="txt_Amount" onkeypress="return Only_Numbers(this,event)" runat="server" MaxLength="10" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "Other_Charge_Amount") %>' CssClass="TEXTBOXNOS"></asp:TextBox></ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="HIDEGRIDCOL" ItemStyle-CssClass="HIDEGRIDCOL"><ItemTemplate><asp:HiddenField ID="hdn_Previous_Value" Value='<%# DataBinder.Eval(Container.DataItem, "IsAddLess") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="TDS On Other Charge" ItemStyle-HorizontalAlign="Right"><ItemTemplate><asp:Label ID="lbl_TDSonOtherCharge" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "TDS_On_OtherCharge") %>' CssClass="LABEL"><</asp:Label></ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        </asp:DataGrid>   
                        </div>      
                  </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker"/><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
             </asp:UpdatePanel>
            <%-- </asp:Panel>--%></td>
        </tr>
       </table>       
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table width="100%" border="0">
                <tr>
                  <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Total_Balance_To_Be_Paid" runat="server" CssClass="LABEL" Text="Total Balance To Be Paid :"></asp:Label></td>
                    <td style="width: 14%; " align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_Total_Balance_To_Be_Paid" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%" style="text-align:right;" ></asp:TextBox><asp:HiddenField ID="hdn_Total_Balance_To_Be_Paid" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" /><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%"></td>
                    <td style="width: 20%" class="TD1">
                         <asp:Label ID="lbl_Total_Advance_Amt" runat="server" CssClass="LABEL" Text="Total Other Amount :"></asp:Label></td> 
                    <td style="width: 14%" align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_Total_Advance_Amount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%" style="text-align:right;"></asp:TextBox><asp:HiddenField ID="hdn_Total_Advance_Amount" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" /><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
                        </asp:UpdatePanel>
                    </td> 
                    <td style="width: 1%" class="TDMANDATORY"></td>
                      <td style="width: 15%" class="TD1">
                         <asp:Label ID="lbl_Total_TDS_Amount" runat="server" CssClass="LABEL" Text="Total TDS Amount :"></asp:Label></td> 
                     <td style="width: 14%" align="left">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_Total_TDS_Amount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%" style="text-align:right;"></asp:TextBox><asp:HiddenField ID="hdn_Total_TDS_Amount" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" /><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
                        </asp:UpdatePanel>
                    </td> 
                    <td style="width: 1%" class="TDMANDATORY"></td>
                </tr>
            </table>
        </td>        
    </tr>   
      
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TotalPayableAmount" runat="server" CssClass="LABEL" Text="Total Payable Amount :"></asp:Label></td>
        <td style="width: 29%; ">
         <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_TotalPayableAmount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="60%"></asp:TextBox><asp:HiddenField ID="hdn_Total_Payable_Amount" runat="server" />
                <asp:HiddenField ID="hdn_TotalLHPO" runat="server" />
                <asp:HiddenField ID="hdn_Total_OtherCharges" runat="server" />                
           </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" /><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td> 
    </tr>    
    <tr>
        <td colspan="6">
            <uc2:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;" valign="top">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label></td>
        <td  colspan="5" style="width: 80%;">
            <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" Height="40px" TextMode="MultiLine" Wrap="true"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="center"  colspan="6">
        <asp:Button ID="btn_SaveNew" runat="server" Text="Save & New" CssClass="BUTTON" OnClick="btn_SaveNew_Click" />&nbsp;
        <asp:Button ID="btn_Save" runat="server"  CssClass="BUTTON" Text="Save & Exit" OnClick="btn_Save_Click" />&nbsp;
        <asp:Button ID="btn_SavePrint" runat="server" Text="Save & Print" CssClass="BUTTON" OnClick="btn_SavePrint_Click" />&nbsp;
        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields With * Mark Are Mandatory" ></asp:Label></ContentTemplate>
            </asp:UpdatePanel>
            
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdn_Tax_Rate" runat="server" />
                <asp:HiddenField ID="hdn_Surcharge_Rate" runat="server" />
                <asp:HiddenField ID="hdn_Add_Surcharge_Rate" runat="server" />
                <asp:HiddenField ID="hdn_Add_Edu_Cess_Rate" runat="server" />
             </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" /><asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate"/></Triggers>
            </asp:UpdatePanel>
            
            
        </td>
        </tr>
        <asp:HiddenField ID="Hdn_LHPOCaption" runat="server" />
        <asp:HiddenField ID="Hdn_AUSCaption" runat="server" />
        <asp:HiddenField ID="Hdn_BTH_Setteled_By_Vehicle" runat="server" />
</table>