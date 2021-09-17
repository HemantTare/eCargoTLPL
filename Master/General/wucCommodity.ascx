<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCommodity.ascx.cs"
    Inherits="EC_Master_wucCommodity" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
 
 
 
<link href="../../CommonStyleSheet.css"  rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="SM_COMMODITY" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
 function Update_Commodity_Details()
 {  
    var hdn_CommodityId = document.getElementById('wucCommodity1_hdn_CommodityId');   

    var txt_CommodityName = document.getElementById('wucCommodity1_txt_CommodityName');   
    

    window.opener.Set_Commodity_Details(hdn_CommodityId.value,txt_CommodityName.value); 
 }
 
</script>

<table class="TABLE">
    <tr>
        <td colspan="3" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_Commodity_Heading" runat="server" Text="COMMODITY" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_CommodityName"   CssClass="LABEL" Text="Commodity Name :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:TextBox ID="txt_CommodityName" runat="server" Width="98%" BorderWidth="1px"
                CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
        </td>
         <td class="TDMANDATORY" style="width: 1%">*</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_CommodityType"   CssClass="LABEL" Text="Commodity Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_CommodityType" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
         <td class="TDMANDATORY" style="width: 1%">*</td>
        
    </tr>
    <tr>
         <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_IsRestricted"   CssClass="LABEL" Text="Is Restricted :" runat="server" ></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:CheckBox ID="chk_IsRestricted" runat="server"   />
        </td>
         <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_Is_Perishable"   CssClass="LABEL" Text="Is Perishable :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:CheckBox ID="chk_IsPerishable" runat="server" />
        </td>
         <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_Is_ServiceTaxApplicable"   CssClass="LABEL" Text="Service Tax Applicable :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:CheckBox ID="chk_Is_ServiceTaxApplicable" runat="server" />
        </td>
         <td class="TDMANDATORY" style="width: 1%"> </td>
        
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_Is_CSTApplicable"   CssClass="LABEL" Text="Is CST Applicable :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%">
            <asp:CheckBox ID="chk_IsCSTApplicable" runat="server" />
        </td>
         <td class="TDMANDATORY" style="width: 1%"> </td>
        
    </tr>
    <tr>
        <td colspan="3" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                     <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABEL"
                        ForeColor="Red">Fields With * Mark Are Mandatory</asp:Label>
                        
                    <asp:HiddenField runat="server" ID="hdn_CommodityId"></asp:HiddenField>
                    
                    
                </contenttemplate>
                <triggers>
                   
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" ValidationGroup="Save"
                OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
        </td>
    </tr>
</table>
