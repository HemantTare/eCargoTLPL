<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDieselExpense.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmDieselExpense" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>

<script type="text/javascript">

    function CalculateDiffOfKM()
    { 
    
        var txt_CurrentKm = document.getElementById('<%=txt_CurrentKm.ClientID %>');
        var hdn_CurrentKm = document.getElementById('<%=hdn_CurrentKm.ClientID %>');
        
        var lbl_PreviousKm = document.getElementById('<%=lbl_PreviousKm.ClientID %>');
        
        var lbl_KMDiff = document.getElementById('<%=lbl_KMDiff.ClientID %>');
        var hdn_KMDiff = document.getElementById('<%=hdn_KMDiff.ClientID %>');
        
        var txt_DieselQty = document.getElementById('<%=txt_DieselQty.ClientID %>');
        var hdn_DieselQty = document.getElementById('<%=hdn_DieselQty.ClientID %>');
        
        var lbl_Average = document.getElementById('<%=lbl_Average.ClientID %>');
        var hdn_Average = document.getElementById('<%=hdn_Average.ClientID %>');
        
     
        if(val(txt_CurrentKm.value) > 0 && val(lbl_PreviousKm.innerHTML) > 0)
        {
        
            if(val(txt_CurrentKm.value) >= val(lbl_PreviousKm.innerHTML))
            {

                hdn_KMDiff.value = val(txt_CurrentKm.value) - val(lbl_PreviousKm.innerHTML);
                lbl_KMDiff.innerHTML = hdn_KMDiff.value;
                
                if (val(txt_DieselQty.value) > 0 && val(hdn_KMDiff.value) > 0 )
                {
                    hdn_Average.value = round2Fixed(val(hdn_KMDiff.value) / val(txt_DieselQty.value)); 
                    lbl_Average.innerHTML = hdn_Average.value;

                }
                else
                {
                    hdn_Average.value = '0';
                    lbl_Average.innerHTML = hdn_Average.value;
                }
            }
            else
            {
                alert('Current KM Reading Msut Be Greater Or Equal To Previous KM Reading');
                txt_CurrentKm.value = "0";
                hdn_KMDiff.value = '0';
                lbl_KMDiff.innerHTML = hdn_KMDiff.value;
                hdn_Average.value = '0';
                lbl_Average.innerHTML = hdn_Average.value;


            }
        }
        else
        {
            hdn_KMDiff.value = '0';
            lbl_KMDiff.innerHTML = hdn_KMDiff.value;
            hdn_Average.value = '0';
            lbl_Average.innerHTML = hdn_Average.value;

        }
        
        hdn_CurrentKm.value = txt_CurrentKm.value;
        hdn_DieselQty.value = txt_DieselQty.value; 
        
        return;
    }

function round2Fixed(value) 
{
  value = +value;

  if (isNaN(value))
    return NaN;

  // Shift
  value = value.toString().split('e');
  value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + 2) : 2)));

  // Shift back
  value = value.toString().split('e');
  return (+(value[0] + 'e' + (value[1] ? (+value[1] - 2) : -2))).toFixed(2);
}



function Allow_To_Save()
{
    var ATS = false;
    var hdn_VehicleID = document.getElementById('hdn_VehicleID');
    var hdn_CurrentKm = document.getElementById('hdn_CurrentKm');
    var txt_CurrentKm = document.getElementById('txt_CurrentKm');

    var lblErrors = document.getElementById('lblErrors');
    
    
    if (val(hdn_VehicleID.value) <= 0)
    {
        lblErrors.innerHTML = 'Select Vehicle No.';
    }

    else if(val(hdn_CurrentKm.value) <= 0)
    {
        lblErrors.innerHTML = 'Enter Current KM';
        txt_CurrentKm.focus();
    }
    else
    {
        ATS = true;
    }
    return ATS;
}
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diesel Expense (Own Tempo)</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%" colspan="4">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Diesel Expense (Own Tempo)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_VoucherNo" CssClass="LABEL" Text="Voucher No :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_VoucherNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Date" CssClass="LABEL" Text="Date :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <uc1:WucDatePicker ID="dtpVoucherDate" runat="server"></uc1:WucDatePicker>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_VehicleNo" CssClass="LABEL" Text="Vehicle No. :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleCategoryIds" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Pump" runat="server" CssClass="LABEL" Text="Pump :"></asp:Label>&nbsp;</td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_Pump" runat="server" CssClass="TEXTBOX" MaxLength="100" Width="99%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_CurrentKm" CssClass="LABEL" Text="Current Km :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_CurrentKm" runat="server" CssClass="TEXTBOXNOS" MaxLength="8"
                            onblur="txtbox_onlostfocus(this);CalculateDiffOfKM();" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)"
                            Width="50%"></asp:TextBox>
                        <asp:HiddenField ID="hdn_CurrentKm" Value="0" runat="server"></asp:HiddenField>
                    </td>
                    <td style="width: 20%;" class="TD1">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PreviousKmH" CssClass="LABEL" Text="Previous Km :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_PreviousKm" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Bill_No" CssClass="LABEL" Text="Bill No. :" runat="server"></asp:Label></td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_BillNo" runat="server" CssClass="TEXTBOX" MaxLength="10" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)" Width="50%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_KMDiffH" CssClass="LABEL" Text="Difference Of Km :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_KMDiff" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdn_KMDiff" Value="0" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 30%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_DieselQty" CssClass="LABEL" Text="Diesel Filled (Ltrs) :" runat="server"></asp:Label></td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_DieselQty" runat="server" CssClass="TEXTBOX" MaxLength="10"
                            onblur="txtbox_onlostfocus(this);CalculateDiffOfKM();" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                            Width="50%"></asp:TextBox>
                        <asp:HiddenField ID="hdn_DieselQty" Value="0" runat="server"></asp:HiddenField>
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_Amount" CssClass="LABEL" Text="Amount :" runat="server"></asp:Label></td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txt_Amount" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);" Width="50%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_AverageH" CssClass="LABEL" Text="Average :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Average" CssClass="LABEL" Text="0" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdn_Average" Value="0" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 30%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 80%;" colspan="3">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" MaxLength="500" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PaidTo" CssClass="LABEL" Text="Paid To :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_PaidTo" runat="server" CssClass="TEXTBOX" MaxLength="100" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)" Width="99%"></asp:TextBox>
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_PaidToMobile" CssClass="LABEL" Text="Mobile No.  :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txt_PaidToMobile" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)" Width="50%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PaidByH" CssClass="LABEL" Text="Paid By :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_PaidBy" CssClass="LABEL" Text="" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%" class="TD1">
                        &nbsp;
                    </td>
                    <td style="width: 30%">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdnKeyID" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center;">
                <tr>
                    <td style="width: 100%">
                        &nbsp;<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            OnClientClick="return Allow_To_Save()" OnClick="btn_Save_Exit_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btn_Close_Click">
                        </asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
