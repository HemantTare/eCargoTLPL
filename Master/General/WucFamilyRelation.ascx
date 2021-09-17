<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFamilyRelation.ascx.cs" Inherits="Master_General_WucFamilyRelation" %>
 
 <script language="javascript" type="text/javascript" src="../../Javascript/Master/General/FamilyRelation.js"></script>
  <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

 <asp:ScriptManager ID="scm_FamilyRelation" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3"> &nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="FAMILY RELATION" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%"> <asp:Label ID="lbl_FamilyRelationName" runat="server" Text="Family Relation Name:" meta:resourcekey="lbl_FamilyRelationNameResource1"></asp:Label>
            </td>
        <td style="width: 74%">
            <asp:TextBox ID="txt_Family_Relation_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="50" Width="97.5%" meta:resourcekey="txt_Family_Relation_NameResource1" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%">
             *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%"><asp:Label ID="lbl_GenderName" runat="Server" Text="Gender Name:" meta:resourcekey="lbl_GenderNameResource1"></asp:Label></td>
         
        <td style="width: 74%">
            <asp:DropDownList ID="ddl_Gender_Name" runat="server" CssClass ="DROPDOWN" meta:resourcekey="ddl_Gender_NameResource1">
             <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="Male"></asp:ListItem>
             <asp:ListItem Value="1" meta:resourcekey="ListItemResource2" Text="Female"></asp:ListItem>
             </asp:DropDownList></td>
        <td class="TDMANDATORY"  style="width: 1%">
             *</td>
    </tr>
    <tr>
        <td>
           &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return validateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Fuel_Type" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" Text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
            </td>
    </tr>
</table>
 <asp:HiddenField ID="hdf_ResourecString" runat="server" />

