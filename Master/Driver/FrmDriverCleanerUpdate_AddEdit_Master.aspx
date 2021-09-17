<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCleanerUpdate_AddEdit_Master.aspx.cs"
    Inherits="Master_Driver_FFrmDriverCleanerUpdate_AddEdit_Master" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript">


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver / Cleaner Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver / Cleaner Update"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 25%; height: 15px;">
                    </td>
                    <td style="width: 24%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td style="width: 50%; height: 15px;" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_DriverCleaner" runat="server" Text="Driver :"></asp:Label>
                        <asp:HiddenField ID="hdn_IsCleaner" runat="server" Value="0" />
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtDriverName" runat="server" CssClass="TEXTBOX" MaxLength="10"
                            onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)"
                            Width="90%" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField ID="hdn_DriverId" Value="0" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_NickNameH" runat="server" Text="Nick Name :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtNickName" runat="server" CssClass="TEXTBOX" MaxLength="10" onblur="txtbox_onlostfocus(this)"
                            onfocus="txtbox_onfocus(this)" Width="80%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_LicenseNoH" runat="server" Text="License No. :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="TEXTBOX" MaxLength="10" onblur="txtbox_onlostfocus(this)"
                            onfocus="txtbox_onfocus(this)" Width="80%" ReadOnly="true"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_LicenseExpiryH" runat="server" Text="Expiry Date :"></asp:Label>
                    </td>
                    <td style="width: 30%" align="left" colspan="2">
                        <uc1:WucDatePicker ID="dtp_LicenseExpiryDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 15px;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_AadharNo" runat="server" Text="Aadhar No. :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtAadharNo" runat="server" CssClass="TEXTBOX" MaxLength="10" onblur="txtbox_onlostfocus(this)"
                            onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)"
                            Width="80%" ReadOnly="true"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_Mobile1" runat="server" Text="Mobile No. 1 :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txt_MobileNo1" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                            runat="server" Width="80%" MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_Mobile2" runat="server" Text="Mobile No. 2 :"></asp:Label>
                    </td>
                    <td style="width: 30%" align="left" colspan="2">
                        <asp:TextBox ID="txt_MobileNo2" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                            runat="server" Width="90%" MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="6" align="center">
                        <asp:Button ID="btnSave" runat="server"  Text="Save & Exit" OnClick="btn_Save_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
