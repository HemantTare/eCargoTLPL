<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFreightCopy.ascx.cs" Inherits="Master_Branch_WucFreightCopy" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Branch/FreightCopy.js"></script>
<asp:ScriptManager ID="scm_FreightCopy" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
        <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="FREIGHT COPY" meta:resourcekey="lbl_HeadingResource1" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromCity" runat="server" Text="Copy To City:" CssClass="LABEL" meta:resourcekey="lbl_FromCityResource1"></asp:Label></td>
        <td style="width: 28%">
               
            <asp:DropDownList ID="ddl_City" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_CityResource1"  >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToState" runat="server" Text="To State:" CssClass="LABEL" meta:resourcekey="lbl_ToStateResource1"></asp:Label></td>
        <td style="width: 28%">
           
            <asp:DropDownList ID="ddl_State" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_StateResource1" >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CopyFromCity" runat="server" CssClass="LABEL" Text="Copy From City:" meta:resourcekey="lbl_CopyFromCityResource1"></asp:Label></td>
        <td style="width: 28%"><asp:DropDownList ID="ddl_CopyFromCity" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_CopyFromCityResource1" >
        </asp:DropDownList></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Rate" runat="server" Text="Add Rate in +/- :" meta:resourcekey="lbl_RateResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_Rate" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Minus(this,event)" onblur="valid(this)" meta:resourcekey="txt_RateResource1"></asp:TextBox></td>
        <td style="width: 2%" class="TDMANDATORY" >*
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Update" runat="server" CssClass="BUTTON" Text="Update" OnClick="btn_Update_Click" OnClientClick="return ValidateUI()" meta:resourcekey="btn_UpdateResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" text=" Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1"></asp:Label></td>
    </tr>
</table>
