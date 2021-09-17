<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLRMessagePrinting.aspx.cs"
    Inherits="Master_General_FrmGeneralRateDiscount" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Sales/GeneralRateDiscount.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LR Message Printing</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="LR Message Printing"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr runat="server">
                    <td class="TD1" style="width: 20%">
                        Title :</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="TEXTBOX" />
                    </td>
                    <td class="TDMANDATORY" colspan="4">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Valid From</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidFrom" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                        Valid Upto</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidUpto" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        English Message :
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtEnglishMessage" runat="server" Height="138px" TextMode="MultiLine"
                            Width="98%" MaxLength="500"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Hindi Message :&nbsp;
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtHindiMessage" runat="server" Height="138px" TextMode="MultiLine"
                            Width="98%" MaxLength="500" Font-Names="Shivaji01" Font-Size="Large"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        &nbsp;</td>
                    <td colspan="5" width="400px">
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
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
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
 

