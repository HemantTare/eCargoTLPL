<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegion.ascx.cs" Inherits="Master_Location_WucRegion" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Location/Region.js"></script>

<asp:ScriptManager ID="scm_Region" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="REGION"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_RegionCode" Text="Region Code:" runat="server" meta:resourcekey="lbl_RegionCodeResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_RegionCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="5" meta:resourcekey="txt_RegionCodeResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_RegionName" Text="Region Name:" runat="server" meta:resourcekey="lbl_RegionNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_RegionName" runat="server" CssClass="TEXTBOX" MaxLength="50"
                meta:resourcekey="txt_RegionNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Country" Text="Country:" runat="server" meta:resourcekey="lbl_CountryResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_Country" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_CountryResource1">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label CssClass="LABEL" Font-Bold="False" ID="lbl_CashLedger" runat="server" Text="Cash Ledger :"></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_CashLedger" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 1%;" class="TDMANDATORY">
         <asp:Label ID="lbl_Mandatory_CashLedger" runat="server" Text="*"></asp:Label></td>
  
        <td style="width: 20%" class="TD1">
            <asp:Label CssClass="LABEL" Font-Bold="False" ID="lbl_BankLedger" runat="server" Text="Bank Ledger :"
                ></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_BankLedger" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList></td>
        <td style="width: 1%;"  class="TDMANDATORY">
          <asp:Label ID="lbl_Mandatory_BankLeger" runat="server" Text="*"></asp:Label></td>
   </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI()"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
           <%-- <asp:UpdatePanel ID="Upd_PnlRegion" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>--%>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"
                        Text="Fields With * Mark Are Mandatory"></asp:Label>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
</table>
