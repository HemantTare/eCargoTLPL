<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverTripAdvance.aspx.cs"
    Inherits="Operations_VehicleTripExpense_FrmDriverTripAdvance" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript">

function HidePaidByControl()
{
    var tr_BankDetails=document.getElementById('tr_BankDetails');
    var tr_BankDetails2=document.getElementById('tr_BankDetails2');    
    var rbl_PaidBy=document.getElementById('rbl_PaidBy_0');
 
    
    tr_BankDetails.style.display='none';
    tr_BankDetails2.style.display='none';
    
    if (rbl_PaidBy.checked == true )
    {
        tr_BankDetails.style.display='none';
        tr_BankDetails2.style.display='none';
    }
    else 
    {
        tr_BankDetails.style.display='inline';
        tr_BankDetails2.style.display='inline';
    }
}

  
function Check_AdvanceAmount(txt)
{

        var lbl_AdvanceToBePaidValue = document.getElementById("lbl_AdvanceToBePaidValue");
        var txt_Advance_Paid = document.getElementById("txt_Advance_Paid");

       if(val(lbl_AdvanceToBePaidValue.innerHTML) < val(txt_Advance_Paid.value))
       {
        txt_Advance_Paid.value= lbl_AdvanceToBePaidValue.innerHTML;
       }
            
}
function Allow_To_Save()
{
    var ATS = false;
    var txt_Remarks = document.getElementById('txt_Remarks');
    var lbl_Errors = document.getElementById('lbl_Errors');
    
    var lbl_AdvanceToBePaidValue = document.getElementById('lbl_AdvanceToBePaidValue');
    var txt_Advance_Paid = document.getElementById('txt_Advance_Paid');
    var hdnTripExpenseAprovalID = document.getElementById('hdnTripExpenseAprovalID');
    
    if (val(hdnTripExpenseAprovalID.value) <= 0)
        {
            lbl_Errors.Text = 'Please Select Vehicle No';
        }

    else if(val(lbl_AdvanceToBePaidValue.innerHTML) < val(txt_Advance_Paid.value))
    {

        lbl_Errors.innerHTML = 'Advance Paid Can Not Be Greater Than Advance To Be Paid';
        txt_Advance_Paid.Focus();

    }
//    else if(txt_Remarks.value == '')
//    {
//        lbl_Errors.innerHTML = 'Please Enter Remark';
//        txt_Remarks.focus();
//    }
    else
    {
        ATS = true;
    }
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Trip Advance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver Trip Advance"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_VoucherNo" CssClass="LABEL" Text="Voucher No :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_VoucherNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
                    <td style="width: 1%">
                        <asp:Label ID="lbl_VoucherDate" CssClass="LABEL" Text="Date :" runat="server"></asp:Label></td>
                    <td class="TD1" style="width: 20%">
                        <uc1:WucDatePicker ID="dtpVoucherDate" runat="server"></uc1:WucDatePicker>
                    </td>
                    <td style="width: 29%;">
                        &nbsp;</td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 29%; height: 15px;">
                    </td>
                    <td class="TD1" style="height: 15px">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 29%; height: 15px;">
                    </td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No :" runat="server"></asp:Label></td>
                    <td style="width: 29%;">
                        &nbsp;<asp:Label ID="lblVehicleNoValue" runat="server" CssClass="LABEL" Text="VehicleNo"
                            Style="font-weight: bolder" Font-Size="Medium" ForeColor="DarkRed"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td style="width: 29%;">
                        &nbsp;</td>
                    <td style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        <asp:Label ID="lblDriver" runat="server" CssClass="LABEL" Text="Driver :"></asp:Label></td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_DriverValue" runat="server" CssClass="LABEL" Text="Driver"
                            Style="font-weight: bolder" Font-Size="Medium" ForeColor="DarkBlue"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        <asp:Label ID="lbl_AdvanceToBePaid" runat="server" CssClass="LABEL" Text="Advance To Be Paid :"></asp:Label></td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_AdvanceToBePaidValue" runat="server" CssClass="LABEL" Text="0.00"
                            Style="font-weight: bolder" Font-Size="Medium"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        <asp:Label ID="lbl_AdvancePaid" runat="server" CssClass="LABEL" Text="Advance Paid :"></asp:Label></td>
                    <td style="width: 29%; height: 15px;">
                        <asp:TextBox ID="txt_Advance_Paid" runat="server" CssClass="TEXTBOXNOS" Text="0.00"
                            Width="30%" onkeyPress="return Only_Integers(this,event)" onblur="txtbox_onlostfocus(this);Check_AdvanceAmount(this);"
                            onfocus="txtbox_onfocus(this)"></asp:TextBox><asp:HiddenField ID="hdnAdvance_Paid"
                                runat="server" Value="0"></asp:HiddenField>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Paid By :</td>
                    <td style="width: 29%; height: 15px;">
                        <asp:RadioButtonList ID="Rbl_Paidby" runat="server" AutoPostBack="false" onclick="HidePaidByControl();"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="true" Text="Cash" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Bank" Value="1"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr id="tr_BankDetails" runat="server">
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Bank Payment By :</td>
                    <td style="width: 29%; height: 15px;">
                        <asp:DropDownList ID="ddl_BankPaymentType" runat="server" CssClass="DROPDOWN" Width="99%">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr id="tr_BankDetails2" runat="server">
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Date :</td>
                    <td style="width: 29%; height: 15px;">
                        <uc1:WucDatePicker ID="dtpChequeDate" runat="server"></uc1:WucDatePicker>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Ref No.</td>
                    <td style="width: 29%; height: 15px;">
                        <asp:TextBox ID="txt_BankRefNo" runat="server" CssClass="TEXTBOX" Width="90%" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)"></asp:TextBox></td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; height: 15px;">
                        Remark :</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 29%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 15px;">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" MaxLength="500" Width="70%"
                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr>
                    <td>
                        &nbsp;<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdnKeyID" runat="server" />
                        <asp:HiddenField ID="hdnTripExpenseAprovalID" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center;">
                <tr>
                    <td style="width: 100%">
                        &nbsp;<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" OnClientClick="return Allow_To_Save()"
                            OnClick="btn_Save_Exit_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btn_Close_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
