<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucItem.ascx.cs"
    Inherits="EC_Master_wucItem" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
 
 
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script> 
<link href="../../CommonStyleSheet.css"  rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="SM_Item" runat="server">
</asp:ScriptManager>


<script type="text/javascript">
 function Update_Item_Details()
 {  
    var hdn_ItemId = document.getElementById('wucItem1_hdn_ItemId');   

    var txt_ItemName = document.getElementById('wucItem1_txt_ItemName');   
    var ddl_commodity = document.getElementById('wucItem1_ddl_commodity');   
    
    window.opener.Set_Item_Details(hdn_ItemId.value,txt_ItemName.value,ddl_commodity.value); 
 }
 
</script>


<table class="TABLE">
    <tr>
        <td colspan="3" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_Item_Heading" runat="server" Text="ITEM" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ItemName"   CssClass="LABEL" Text="Item Name :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%" class="TDMANDATORY">
            <asp:TextBox ID="txt_ItemName" runat="server" Width="640px" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>
            </td>
         <td class="TDMANDATORY" style="width: 1%">*</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_Commodity"   CssClass="LABEL" Text="Commodity Name :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%" class="TDMANDATORY">
            <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
            *</td>
         <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_Description"   CssClass="LABEL" Text="Description :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%;">
            <asp:TextBox ID="txt_Description" runat="server" Width="640px" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="150" Wrap="true"></asp:TextBox>
        </td>
         <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ItemRatePerKg" runat="server" CssClass="LABEL" Text="Rate Per Kg :"></asp:Label></td>
        <td style="width: 69%">
            <asp:TextBox ID="txt_ItemRatePerKg" runat="server" Width="30%"  onkeypress="return Only_Numbers(this,event)"  BorderWidth="1px" CssClass="TEXTBOX" MaxLength="150" Wrap="true"></asp:TextBox>            
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="3" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                     <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABELERROR">Fields With * Mark Are Mandatory</asp:Label>
                     
                    <asp:HiddenField runat="server" ID="hdn_ItemId"></asp:HiddenField>
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
</table>
