<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCleanerBankDetails.aspx.cs"
    Inherits="Master_Driver_FrmCleanerBankDetails" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cleaner Bank Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Cleaner Bank Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Cleaner</td>
                    <td style="width: 79%">
                        &nbsp;<asp:Label ID="lbl_Cleaner" runat="server" CssClass="TEXTBOX" Width="50%"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 79%; height: 15px;">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Beneficiary Name</td>
                    <td style="width: 79%">
                        &nbsp;<asp:TextBox ID="txtBeneficiary" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="150" Width="50%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Account No.</td>
                    <td style="width: 79%">
                        &nbsp;<asp:TextBox ID="txtAccountNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="50" Width="50%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        IFSC Code</td>
                    <td style="width: 79%">
                        &nbsp;<asp:TextBox ID="txtIFSCCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            Width="50%" MaxLength="50"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%"></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Bank Name</td>
                    <td style="width: 79%">
                        &nbsp;<asp:TextBox ID="txtBank" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Width="50%"
                            MaxLength="150"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%"></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
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
