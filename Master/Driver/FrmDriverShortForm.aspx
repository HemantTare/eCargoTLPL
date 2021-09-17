<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverShortForm.aspx.cs"
    Inherits="Master_Driver_FrmDriverShortForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Driver Name</td>
                    <td style="width: 79%" colspan="4">
                        <asp:TextBox ID="txt_DriverName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" onkeypress="return Only_AlphaSpaceNumbers(this,event);" onblur="return Uppercase(this);" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        License No.</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txt_LicenseNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="25" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        License
                        Expiry Date&nbsp;</td>
                    <td style="width: 29%">
                        &nbsp;
                        <uc1:wuc_Date_Picker ID="dtp_ExpiryDate" runat="server"></uc1:wuc_Date_Picker>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        License Issue City</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txt_LicenseIssueCity" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" onkeypress="return Only_AlphaSpaceNumbers(this,event);" onblur="return Uppercase(this);" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 29%">
                        &nbsp;
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Mobile No. 1</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtMobileNo1" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        Mobile No. 2</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtMobileNo2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 80%" colspan="5">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 80%; height: 15px;" colspan="6">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        &nbsp; &nbsp;</td>
                    <td style="width: 80%" colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUIForRegularClientGC();"
                            OnClick="btnSave_Click" />
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

<script type="text/javascript">

 function updateparentwindow(clientname)
 { 
   self.close();
  window.opener.UpdateFromRegularClient(clientname);

 } 

</script>

