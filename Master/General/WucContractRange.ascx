<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractRange.ascx.cs" Inherits="Master_General_WucWeightRange" %>

<asp:ScriptManager ID="scm_ContractRange" runat="server">
</asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/Common.js">
</script>

<script type="text/javascript"  src="../../Javascript/Master/General/ContractRange.js">
</script>

<table class="TABLE" style="width: 800px">
    <tr>
        <td class="TDGRADIENT" colspan="7">
            &nbsp;<asp:Label ID="lbl_Header" runat="server" CssClass="HEADINGLABEL" meta:resourcekey="lbl_HeaderResource1"
                Text="Weight Range"></asp:Label></td>
    </tr>
    <tr>
        <td >
            &nbsp;
        </td>
        <td style="width: 29%; ">
        </td>
        <td style="width: 1%; ">
        </td>
        <td style="width: 20%; ;">
        </td>
        <td style="width: 20%; ">
        </td>
        <td >
        </td>
        <td style="width: 1%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_From" runat="server" Text="From Kg :" meta:resourcekey="lbl_FromResource1"></asp:Label></td>
        <td style="width: 20%" class="TDMANDATORY">
            <asp:TextBox ID="txt_FromKg" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS" 
                MaxLength="50" meta:resourcekey="txt_ProfileNameResource1"  onblur="return valid(this)"
                onkeypress="return Only_Integers(this,event)" Width="150px"></asp:TextBox>*
            </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
            
        <td class="TD1" style="width: 20%" >
            <asp:Label ID="lbl_To" runat="server" Text="To Kg :" meta:resourcekey="lbl_ToResource1"></asp:Label></td>
        <td style="width: 20%" class="TDMANDATORY">
            <asp:TextBox ID="txt_ToKg" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="50" onblur="return valid(this)"
                meta:resourcekey="txt_ProfileNameResource1" onkeypress="return Only_Integers(this,event)" Width="150px" ></asp:TextBox>
            </td>
        <td class="TDMANDATORY" style="width: 1%">*
            </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="7">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" meta:resourcekey="btn_saveResource1"
                OnClick="btn_Save_Click" OnClientClick="return ValidateUI()" Text="Save" /></td>
    </tr>
    <tr>
        <td colspan="7">
            <asp:UpdatePanel ID="Upd_Pnl_WeightRange" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"
                        meta:resourcekey="lbl_ErrorsResource1" Text="Fields With * Mark Are Mandatory"></asp:Label>
               </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdf_ContractRangeTypeId" runat="server" />
        </td>
        <td style="width: 29%">
            &nbsp;</td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>
