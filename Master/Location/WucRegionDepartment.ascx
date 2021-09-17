<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegionDepartment.ascx.cs" Inherits="Master_Location_WucRegionDepartment" %>


<table class="TABLE" width="100%">
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_Department" GroupingText="Department" runat="server"
                meta:resourcekey="pnl_DepartmentResource1">
                <table width="100%">
                    <tr>
                        <td style="height: 47px">
                            <asp:CheckBoxList ID="chk_ListDepartment" BorderColor="Black" BorderWidth="1px" CssClass="CHECKBOXLIST"
                                runat="server" RepeatDirection="Horizontal" RepeatColumns="3" meta:resourcekey="chk_ListDepartmentResource1">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="Pnl_Parameter" Font-Bold="False" GroupingText="Parameters" runat="server"
                meta:resourcekey="pnl_ParameterResource1">
                
                <table style="width: 250px">
                    <tr>
                        <td class="TD1" style="width: 90px">
                            <asp:Label ID="lbl_CashLimit" Text="Cash Limit:" Width="90px" runat="server" meta:resourcekey="lbl_CashLimitResource1"></asp:Label></td>
                        <td class="TD1" style="width: 150px">
                            <asp:TextBox ID="txt_CashLimit" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                MaxLength="6" meta:resourcekey="txt_CashLimitResource1" onkeypress="return Only_Numbers(this,event)"
                                onblur="valid(this)" Width="150px"></asp:TextBox>
                        </td>
                        <td class="TDMANDATORY" style="width: 10px">
                            <asp:Label ID="lbl_Mandatory_CashLimit" runat="server" Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 90px">
                            <asp:Label ID="lbl_BankLimit" Text="Bank Limit :" runat="server" meta:resourcekey="lbl_BankLimitResource1"
                                Width="90px"></asp:Label></td>
                        <td class="TD1" style="width: 150px">
                            <asp:TextBox ID="txt_BankLimit" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                MaxLength="6" meta:resourcekey="txt_BankLimitResource1" onkeypress="return Only_Numbers(this,event)"
                                onblur="valid(this)" Width="150px"></asp:TextBox>
                        </td>
                        <td class="TDMANDATORY" style="width: 10px">
                            <asp:Label ID="lbl_mandatory_BankLimit" runat="server" Text="*"></asp:Label></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
<%--            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
--%>        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </td>
    </tr>
</table>
