<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFreightBranchCopy.ascx.cs" Inherits="Master_Branch_WucFreightBranchCopy" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Branch/FreightBranchCopy.js"></script>
<asp:ScriptManager ID="scm_FreightBranchCopy" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
        <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="FREIGHT BRANCH COPY" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromBranch" runat="server" Text="Copy To Branch:" CssClass="LABEL"></asp:Label></td>
        <td style="width: 28%">
               
            <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToArea" runat="server" Text="To Area:" CssClass="LABEL"></asp:Label></td>
        <td style="width: 28%">
           
            <asp:DropDownList ID="ddl_Area" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CopyFromBranch" runat="server" CssClass="LABEL" Text="Copy From Branch:"></asp:Label></td>
        <td style="width: 28%"><asp:DropDownList ID="ddl_CopyFromBranch" runat="server" CssClass="DROPDOWN" >
        </asp:DropDownList></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Freight" runat="server" Text="Add Rate in +/- :"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_FreightRate" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Minus(this,event)" onblur="valid(this)"></asp:TextBox></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Commodity" runat="server" Text="Commodity:" CssClass="LABEL"></asp:Label></td>
        <td style="width: 28%">
               
            <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">
        <asp:Label ID="lbl_CommodityManadotory" runat="server" Text="*"
        ForeColor="red" CssClass="LABEL"></asp:Label>
        </td>
      <asp:HiddenField ID="hdn_Mode" runat="server" />
        
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Update" runat="server" CssClass="BUTTON" Text="Update" OnClick="btn_Update_Click" OnClientClick="return ValidateUI()" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" text=" Fields with * mark are mandatory"></asp:Label></td>
    </tr>
</table>
