<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchBankSelection.ascx.cs"
    Inherits="FA_Common_Accounting_Masters_WucBranchBankSelection" %>

<script type="text/javascript" language="javascript">

function Allow_To_Save()
{
    var ddl_BranchName=document.getElementById('<%= ddl_BranchName.ClientID %>');
    var lbl_Errors=document.getElementById('<%= lbl_Errors.ClientID %>');
    var Is_Branch_Selected=document.getElementById('WucBranchBankSelection1_rdl_HO_Branch_0');
    
    if (Is_Branch_Selected.checked==true)
    {
        if(ddl_BranchName.value == '0' || ddl_BranchName.options.length == 0)
        {
            lbl_Errors.innerText='Please Select Branch Name';
            ddl_BranchName.focus();
            return false;
        }
    }
    return true;

}

</script>

<asp:ScriptManager ID="scm_BranchBankSelection" runat="server" />
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="8">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="BRANCH BANK SELECTION"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Select" runat="server" Text="Select:" meta:resourcekey="lbl_SelectResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:RadioButtonList ID="rdl_HO_Branch" BorderWidth="2px" BorderStyle="Solid" BorderColor="SteelBlue"
                runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdl_HO_Branch_SelectedIndexChanged"
                meta:resourcekey="rdl_HO_BranchResource1">
                <asp:ListItem Value="1" Selected="True" onclick="javascript:setTimeout('__doPostBack(\'WucBranchBankSelection1$rdl_HO_Branch$0\',\'\')', 0)"
                    meta:resourcekey="ListItemResource1" Text="Branch"></asp:ListItem>
                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2" Text="HO"></asp:ListItem>
            </asp:RadioButtonList></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:UpdatePanel ID="Upd_Pnl_lbl_Branch_Name" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdl_HO_Branch" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Branch_Name" Text="Branch Name:" runat="server" meta:resourcekey="lbl_Branch_NameResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_BranchName" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdl_HO_Branch" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_BranchName" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_BranchName_SelectedIndexChanged" meta:resourcekey="ddl_BranchNameResource1" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:UpdatePanel ID="Upd_Pnl_lbl_Mandotary" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdl_HO_Branch" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Mandotary" Text="*" runat="server" meta:resourcekey="lbl_MandotaryResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%; vertical-align: top;" class="TD1">
            Select Bank Ledger:</td>
        <td colspan="5" style="width: 70%; text-align: left;" class="TD1">
            <asp:Panel ID="pnl_Ledger" BorderWidth="1px" Width="97%" Height="100px" runat="server"
                meta:resourcekey="pnl_LedgerResource1">
                <asp:UpdatePanel ID="Upd_Pnl_ChkList_Ledger" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_BranchName" />
                        <asp:AsyncPostBackTrigger ControlID="rdl_HO_Branch" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:CheckBoxList ID="ChkList_Ledger" CellSpacing="4" RepeatColumns="4" BorderWidth="0px"
                            runat="server" meta:resourcekey="ChkList_LedgerResource1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON"   OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <asp:UpdatePanel ID="Upd_Pnl_BranchBankSelection" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
