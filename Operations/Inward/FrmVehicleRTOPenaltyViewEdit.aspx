<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleRTOPenaltyViewEdit.aspx.cs"
    Inherits="Operations_Inward_FrmVehicleRTOPenaltyViewEdit" %>

<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle RTO Penalty</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table class="TABLE" width="100%">
                        <tr>
                            <td class="TDGRADIENT" colspan="4">
                                <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Vehicle RTO Penalty"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%; height: 15px;">
                            </td>
                            <td style="width: 30%; height: 15px;">
                            </td>
                            <td style="width: 30%; height: 15px;">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%; height: 15px;">
                            </td>
                        </tr>
                        <tr runat="server">
                            <td class="TD1" style="width: 20%; height: 21px;">
                                Vehicle No. :</td>
                            <td style="width: 30%; height: 21px;">
                                &nbsp;<uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                            </td>
                            <td style="width: 30%; height: 21px;">
                                &nbsp;
                            </td>
                            <td class="TDMANDATORY" style="width: 20; height: 21px">
                                *</td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Penalty Date :</td>
                            <td style="width: 30%">
                                <uc1:WucDatePicker ID="dtp_PenaltyDate" runat="server" />
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                                *</td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Penalty Time :
                            </td>
                            <td style="width: 30%">
                                <uc1:TimePicker ID="PenaltyTime" runat="server" />
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Challan No. :
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txt_ChallanNo" runat="server" CssClass="TEXTBOX" MaxLength="25" /></td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Place :
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txt_Place" runat="server" CssClass="TEXTBOX" MaxLength="50" /></td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Offence :
                            </td>
                            <td colspan="2" style="width: 60%">
                                <asp:TextBox ID="txt_Offence" runat="server" CssClass="TEXTBOX" MaxLength="250" /></td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Amount :
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txt_Amount" runat="server" CssClass="TEXTBOXNOS" Width="50%" MaxLength="5" /></td>
                            <td style="width: 30%">
                            </td>
                            <td class="TDMANDATORY" style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 15px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
