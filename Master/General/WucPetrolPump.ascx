<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPetrolPump.ascx.cs"
    Inherits="EC_Master_WucPetrolPump" %>
<%@ Register Src="WucPetrolPumpGeneral.ascx" TagName="WucPetrolPumpGeneral" TagPrefix="uc1" %>
<%@ Register Src="WucPetrolPumpFinanceDetails.ascx" TagName="WucPetrolPumpFinanceDetails"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript">
// function Allow_to_Save()
//        {
//             var Lbl_Error=document.getElementById('<%= lbl_Errors.ClientID%>');

//                 if(!ValidateWucAddress(Lbl_Error))
//                            {
//                             
//                              return false;
//                            }
//                            
//                            return true;
//        }
// 
</script>

<ComponentArt:TabStrip ID="TB_PetrolPump" EnableViewState="False" MultiPageId="MP_PetrolPump"
    runat="server" meta:resourcekey="TB_PetrolPumpResource1">
</ComponentArt:TabStrip>
<ComponentArt:MultiPage ID="MP_PetrolPump" CssClass="MULTIPAGE" runat="server" Style="left: 0px;
    top: 1px" meta:resourcekey="MP_PetrolPumpResource1" SelectedIndex="0">
    <ComponentArt:PageView runat="server">
        <uc1:WucPetrolPumpGeneral ID="WucPetrolPumpGeneral1" runat="server" />
    </ComponentArt:PageView>
    <ComponentArt:PageView runat="server">
        <uc2:WucPetrolPumpFinanceDetails ID="WucPetrolPumpFinanceDetails1" runat="server" />
    </ComponentArt:PageView>
</ComponentArt:MultiPage>
<table class="TABLE">
    <tr>
        <td>
            &nbsp;
            <asp:HiddenField ID="hdn_Id" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="text-align: center">
            <%--    <asp:UpdatePanel ID="upd_PetrolPumpSave" runat="server">
        <contenttemplate>--%>
            <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"
                meta:resourcekey="btn_SaveResource1" />
            <%--  </contenttemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
        </triggers>
    </asp:UpdatePanel>     --%>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1" />
        </td>
    </tr>
</table>
