<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReligion.ascx.cs" Inherits="Master_Vehicle_WucReligion" %>
<asp:ScriptManager ID="Scm_Religion" runat="Server" />

<script src="../../Javascript/Master/General/Religion.js" type="text/javascript" language="javascript"></script>
  <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
   

<table class="TABLE" style="width: 100%">
    <tr>
        <td colspan="3" class="TDGRADIENT"> &nbsp;
            <asp:Label ID="lbl_Heading" runat="Server" CssClass="HEADINGLABEL" Text="RELIGION " meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 15%">
           <asp:Label ID="lbl_Region" Text="Religion:" runat="Server" meta:resourcekey="lbl_RegionResource1"></asp:Label>
        </td>
        <td class="TD1" style="width: 84%">
            <asp:TextBox ID="txt_Religion" runat="Server" Width="99%" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="25" meta:resourcekey="txt_ReligionResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClientClick="return validateUI()"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="upd_pnl_Religion_Save" UpdateMode="Conditional" runat="Server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" Text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
 <asp:HiddenField ID="hdf_ResourecString" runat="server" />
