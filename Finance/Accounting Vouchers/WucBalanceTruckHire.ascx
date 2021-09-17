<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBalanceTruckHire.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucBalanceTruckHire" %>
<%@ Register Src="~/Finance/Accounting Vouchers/WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc3" %>
<%@ Register Src="~/Finance/Accounting Vouchers/WucOtherChargeDetails.ascx" TagName="WucOtherChargeDetails" TagPrefix="uc4" %>
<%@ Register Assembly="ComponentArt.Web.UI" TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" %>

<asp:ScriptManager ID="scm1" runat="server"></asp:ScriptManager>
<script language="javascript" src="../../Javascript/DatePicker.js" type="text/javascript"></script>
<script language="javascript" src="../../Javascript/ddlsearch.js" type="text/javascript"></script>
<script language="javascript" src="../../Javascript/Common.js"type="text/javascript"></script>

<script type="text/javascript">

function Allow_To_Save()
{
    var lbl_Error = document.getElementById('<%=lbl_Errors.ClientID %>');
    var ddl_BrokerOwner = document.getElementById('WucBalanceTruckHire1_ddl_OwnerBroker_hdnddl_OwnerBroker');
    var vehicleID = getvehicleid(); 
    var ddl_Lhpo = document.getElementById('<%=ddl_LhpoNo.ClientID %>');
    var Total_Payable_Amount = document.getElementById('<%=hdn_Total_Payable_Amount.ClientID %>') 
        
    if(val(ddl_BrokerOwner.value) <= 0)
    {
        lbl_Error.innerText = 'Please Select Owner/Broker';
        return false;
    }
    else if(vehicleID <= 0)
    {
        lbl_Error.innerText = 'Please Select Vehicle';
        return false;
    }
    else if(val(ddl_Lhpo.value) <= 0)
    {
        lbl_Error.innerText = 'Please Select LHPO NO';
        ddl_Lhpo.focus();
        return false;
    }
    else if(val(Total_Payable_Amount.value) <= 0)
    {
        lbl_Error.innerText = 'Total Payable Amount Should be Greater Than 0';
        return false;
    }
    
    if(validateWUCCheque(lbl_Error) == false)
    {return false;}
    
      return true;
}

function GetTotalAmount()
{
    var txt_Toatal_Payable_Amount = document.getElementById('<%=txt_TotalPayableAmount.ClientID %>')    
    return val(txt_Toatal_Payable_Amount.value);
}
</script>

<table class="TABLE" width="100%">

    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Balance Truck Hire (BTH)"></asp:Label></td>
    </tr>
    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BTHVoucherNo" runat="server" Text="BTH Voucher No. :" CssClass="LABEL" ></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_BTHVoucherNo" BackColor="Transparent" BorderColor="Transparent" BorderStyle="Solid" ReadOnly="True" 
               CssClass="TEXTBOX"   runat="server" Font-Bold="True" Width="86%"></asp:TextBox>
        </td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_BTHVoucherDate" runat="server" Text="BTH Voucher Date :" CssClass="LABEL" ></asp:Label></td>
        <td style="width: 29%">
            <ComponentArt:Calendar id="Wuc_BTHVoucherDate" runat="server" PickerFormat="Custom" 
                            PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker" 
                            AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" 
                            SelectedDate="6-DEC-2008" AutoPostBackOnSelectionChanged="true"
                            OnSelectionChanged="Wuc_BTHVoucherDate1_SelectionChanged"/>
        </td>
        <td style="width: 1%"></td>
    </tr>     
    <tr>
        <td style="width: 20%; " class="TD1">
            <asp:Label ID="lbl_referenceNo" runat="server" CssClass="LABEL" Text="Reference No. :"></asp:Label></td>
        <td style="width: 29%; ">
            <asp:TextBox ID="txt_ReferenceNo" runat="server" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox></td>
        <td style="width: 1%; " class="TDMANDATORY">
        </td>
        <td style="width: 50%; " class="TD1" colspan="3"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_OwnerBroker" runat="server" CssClass="LABEL" Text="Owner/Broker :"></asp:Label></td>
        <td style="width: 29%">
            <cc1:DDLSearch ID="ddl_OwnerBroker" runat="server" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetOwnerBroker" PostBack="true" OnTxtChange="ddl_OwnerBroker_TxtChange" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 50%" class="TD1" colspan="3"></td>      
    </tr>   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_VehicleNo" runat="server" CssClass="LABEL" Text="Vehicle No :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                <ContentTemplate>
                    <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                 </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_VehicleCategory" runat="server" CssClass="LABEL" Text="Vehicle Category :"></asp:Label></td>
        <td style="width: 29%">
        <asp:UpdatePanel ID="up_VehicleCat" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_VehicleCat" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
            </ContentTemplate>
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>  
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_LhpoNo" runat="server" CssClass="LABEL" Text="LHPO No. :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="up_lhpoNo" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_LhpoNo" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_LhpoNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                    <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_LhpoDate" runat="server" CssClass="LABEL" Text="LHPO Date :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_LhpoDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                    <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>   
    <tr>    
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_manualRefNo" runat="server" CssClass="LABEL" Text="Manual Ref No. :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_ManualRefNo" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                    <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Driver1" runat="server" CssClass="LABEL" Text="Driver1 :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_Driver1" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                    <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_LhpoBranch" runat="server" CssClass="LABEL" Text="LHPO Branch :"></asp:Label></td>
        <td style="width: 29%">
         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                 <ContentTemplate>
                  <asp:TextBox ID="txt_LHPOBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                    <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>      
    </tr>    
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_FromLocation" runat="server" CssClass="LABEL" Text="From Location :"></asp:Label></td>
        <td style="width: 29%">
         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_FromBranch" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToLocation" runat="server" CssClass="LABEL" Text="To Location :"></asp:Label></td>
        <td style="width: 29%">
         <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_ToLocation" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
        </td>
    </tr>   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_HireAmount" runat="server" CssClass="LABEL" Text="Hire Amount :"></asp:Label></td>
        <td style="width: 29%">
         <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_HireAmount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>     
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;" >
        <asp:Label ID="lbl_TDS" runat="server" CssClass="LABEL" Text="TDS :"></asp:Label></td>
        <td style="width: 29%; height: 21px">
         <asp:UpdatePanel ID="UpdatePanel9" runat="server">
             <ContentTemplate>
                <asp:TextBox ID="txt_TDS" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%;" ></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>  
    </tr>  
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Advancepayable" runat="server" CssClass="LABEL" Text="Advance Payable :"></asp:Label></td>
        <td style="width: 29%; ">
         <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_Advancepayable" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>  
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_BalanceToBePaid" runat="server" CssClass="LABEL" Text="Balance To Be Paid :"></asp:Label></td>
        <td style="width: 29%; height: 21px">
         <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_BalanceToBePaid" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
                <asp:AsyncPostBackTrigger ControlID="Wuc_BTHVoucherDate" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>  
    </tr>    
    <tr>
        <td class="TD1" colspan="6">
        <uc4:WucOtherChargeDetails ID="WucOtherChargeDetails1" runat="server" />
        </td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TotalPayableAmount" runat="server" CssClass="LABEL" Text="Toatal Payable Amount :"></asp:Label></td>
        <td style="width: 29%; ">
         <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_TotalPayableAmount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
           </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                <asp:AsyncPostBackTrigger ControlID="ddl_OwnerBroker" />
                <asp:AsyncPostBackTrigger ControlID="ddl_LhpoNo" />
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
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
        <asp:Button ID="btn_Save" runat="server"  CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" />
        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>

        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                    Font-Bold="True" Text="Fields With * Mark Are Mandatory" ></asp:Label>    
                    <asp:HiddenField ID="hdn_Total_Payable_Amount" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
                <asp:HiddenField ID="hdn_LHPODATE" runat="server" />
        </td>
        </tr>
</table>
