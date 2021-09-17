<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchParameters.ascx.cs"
    Inherits="Master_Branch_WucBranchParameters" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<table class="TABLE">
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_Parameters" CssClass="PANEL" Font-Bold="False" GroupingText="Parameters"
                runat="server" meta:resourcekey="pnl_ParametersResource1">
                <table width="100%">
                    <tr>
                        <td colspan="6">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 29%" class="TD1">
                            <asp:Label ID="label1" BackColor="#33FFFF" CssClass="LABEL" Font-Bold="False" runat="server"
                                Text="Pre-Printed Stationary" meta:resourcekey="label1Resource1"></asp:Label></td>
                        <td style="width: 20%">
                            <asp:Label ID="label11" BackColor="#33FFFF" Font-Bold="False" runat="server" CssClass="LABEL"
                                Text="Min Stock" meta:resourcekey="label11Resource1"></asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left">
                        </td>
                        <td style="width: 50%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label2" runat="server"></asp:Label></td>
                        <td style="width: 20%">
                            <asp:TextBox ID="Txt_GC" runat="server" BorderWidth="1px" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                                CssClass="TEXTBOXNOS" meta:resourcekey="Txt_GCResource1"></asp:TextBox></td>
                        <td style="width: 1%; text-align: left">
                        </td>
                        <td style="width: 50%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label3" runat="server" Text="MR :"
                                meta:resourcekey="label3Resource1"></asp:Label></td>
                        <td style="width: 20%">
                            <asp:TextBox ID="Txt_CR" BorderWidth="1px" runat="server" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                                CssClass="TEXTBOXNOS" meta:resourcekey="Txt_CRResource1"></asp:TextBox></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 50%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label4" runat="server" Text="MANIFEST :"
                                meta:resourcekey="label4Resource1"></asp:Label></td>
                        <td style="width: 20%">
                            <asp:TextBox ID="Txt_MEMO" BorderWidth="1px" runat="server" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                                CssClass="TEXTBOXNOS" meta:resourcekey="Txt_MEMOResource1"></asp:TextBox></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 50%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label5" runat="server"></asp:Label></td>
                        <td style="width: 20%">
                            <asp:TextBox ID="Txt_LHPO" BorderWidth="1px" runat="server" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                                CssClass="TEXTBOXNOS" meta:resourcekey="Txt_LHPOResource1"></asp:TextBox></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 50%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 29%">
                            <asp:Label ID="lbl_BookingStartTime" runat="server" CssClass="LABEL" meta:resourcekey="lbl_VehicleArrivalTimeResource1"
                                Text="Booking Start Time :"></asp:Label></td>
                        <td style="width: 20%">
                            <uc2:TimePicker ID="wuc_BookingStartTime" runat="server"></uc2:TimePicker>
                        </td>
                        <td style="width: 1%">
                            <asp:Label ID="lbl_BookingEndTime" runat="server" CssClass="LABEL" meta:resourcekey="lbl_UnloadingTimeResource1"
                                Text="End Time :"></asp:Label></td>
                        <td colspan="3" style="width: 50%">
                            <uc2:TimePicker ID="wuc_BookingEndTime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;" colspan="3">
                            &nbsp;</td>
                        <td style="width: 50%;" colspan="3">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="Panel1" CssClass="PANEL" Font-Bold="False" GroupingText="Ledger" runat="server"
                meta:resourcekey="Panel1Resource1">
                <table width="100%">
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 25%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label6" runat="server" Text="Default Cash Ledger :"
                                meta:resourcekey="label6Resource1"></asp:Label></td>
                        <td style="width: 30%" colspan="2">
                            <asp:DropDownList ID="DDL_DefaultCashLedger" runat="server" CssClass="DROPDOWN" meta:resourcekey="DDL_DefaultCashLedgerResource1">
                            </asp:DropDownList></td>
                        <td style="width: 45%;" colspan="3" class="TDMANDATORY">
                            *</td>
                    </tr>
                    <tr>
                        <td style="width: 25%" class="TD1">
                            <asp:Label CssClass="LABEL" Font-Bold="False" ID="label7" runat="server" Text="Default Bank Ledger :"
                                meta:resourcekey="label7Resource1"></asp:Label></td>
                        <td style="width: 30%" colspan="2">
                            <asp:DropDownList ID="DDL_DefaultBankLedger" runat="server" CssClass="DROPDOWN" meta:resourcekey="DDL_DefaultBankLedgerResource1">
                            </asp:DropDownList></td>
                        <td style="width: 45%;" colspan="3" class="TDMANDATORY">
                            *</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </td>
    </tr>
</table>
