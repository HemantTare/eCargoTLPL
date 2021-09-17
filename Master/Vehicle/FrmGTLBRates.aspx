<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGTLBRates.aspx.cs" Inherits="Master_Vehicle_FrmGTLBRates" %>

<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTLB Rates Master</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GTLB Rates Master"></asp:Label>
                    </td>
                </tr>
                <tr runat="server">
                    <td class="TD1" style="width: 20%;">
                    </td>
                    <td style="width: 29%;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                    </td>
                    <td style="width: 29%;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Applicable From</td>
                    <td id="td_ApplicableFrom" runat="server">
                        <uc1:WucDatePicker ID="dtpApplicableFrom" runat="server"></uc1:WucDatePicker>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Thappi Per Ton</td>
                    <td id="td1" runat="server">
                        <asp:TextBox ID="txt_ThappiPerTon" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" onfocus="this.select();"
                            MaxLength="8" Width="20%" onkeypress="return Only_Numbers(this,event)" Text="0"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Loading Per Ton</td>
                    <td id="td2" runat="server">
                        <asp:TextBox ID="txt_LoadingPerTon" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" onfocus="this.select();"
                            MaxLength="8" Width="20%" onkeypress="return Only_Numbers(this,event)" Text="0"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Warfer Per Day</td>
                    <td id="td3" runat="server">
                        <asp:TextBox ID="txt_WarferPerDay" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" onfocus="this.select();"
                            MaxLength="8" Width="20%" onkeypress="return Only_Numbers(this,event)" Text="0"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td colspan="5" style="text-align: right; width: 80%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                            Text="Save" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
