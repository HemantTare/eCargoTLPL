<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLocalDeliveryVehicleContract.aspx.cs"
    Inherits="Master_Vehicle_FrmLocalDeliveryVehicleContract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">
function FixedClicked()
{
    var trid = document.getElementById("tr_FreightCalculation");
    
    if (trid != null) 
    {
        trid.style.display = 'block';
    }
    
}

function VariableClicked()
{

    var trid = document.getElementById("tr_FreightCalculation");
    var txt_FixedRs = document.getElementById("txt_FixedRs");
    
    if (trid != null) 
    {
        trid.style.display = 'none';
        txt_FixedRs.value='0';
    }

}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Local Delivery Vehicle Contract</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Local Delivery Vehicle Contract"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%">
                        Branch :</td>
                    <td style="width: 34%">
                        <cc1:DDLSearch ID="DDLBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="False" InjectJSFunction="" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Vehicle No :</td>
                    <td>
                        <uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 15%">
                        Vendor :</td>
                    <td style="width: 34%">
                        <cc1:DDLSearch ID="DDLVendor" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetVendor" CallBackAfter="2"
                            PostBack="False" InjectJSFunction="" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Valid From :</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidFrom" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                    <td class="TD1">
                        Valid UpTo :</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidUpto" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Freight Settlement :</td>
                    <td>
                        <asp:DropDownList ID="ddlFreightSettlement" runat="server" CssClass="DROPDOWN" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Freight Calculation :</td>
                    <td>
                        <asp:RadioButtonList ID="rdl_FreightCalculation" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True" Text="Fixed" OnClick="FixedClicked();"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Variable" OnClick="VariableClicked();"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr runat="server" id="tr_FreightCalculation">
                    <td class="TD1">
                        Fixed Type :</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlFixedType" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlFixedType" runat="server" CssClass="DROPDOWN" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFixedType_SelectedIndexChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 60%" align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlFixedType" />
                            </Triggers>
                            <ContentTemplate>
                                &nbspRs.
                                &nbsp;<asp:TextBox ID="txt_FixedRs" runat="server" CssClass="TEXTBOXNOS" Width="60px" onkeypress="return Only_Numbers(this,event);" />
                                &nbsp;<asp:Label ID="lbl_FixedRsPer" runat="server" CssClass="TEXTBOX" Text="/Trip" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Remarks :</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtRemarks" CssClass="TEXTBOX" TextMode="MultiLine" Height="30px"
                            MaxLength="250" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
