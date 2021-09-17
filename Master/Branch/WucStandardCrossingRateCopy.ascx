<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucStandardCrossingRateCopy.ascx.cs" Inherits="Master_Branch_WucStandardCrossingRateCopy" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Branch/StandardCrossingRateCopy.js"></script>
<asp:ScriptManager ID="scm_StandardCrossingRateCopy" runat="server"></asp:ScriptManager>



<table style="width: 100%" class="TABLE">
        <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="STANDARD CROSSING RATE COPY" meta:resourcekey="lbl_HeadingResource1" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromBranch" runat="server" Text="Copy To Branch:" CssClass="LABEL" meta:resourcekey="lbl_FromBranchResource1"></asp:Label></td>
        <td style="width: 28%">
               
            <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_BranchResource1"  >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToArea" runat="server" Text="To Area:" CssClass="LABEL" meta:resourcekey="lbl_ToAreaResource1"></asp:Label></td>
        <td style="width: 28%">
           
            <asp:DropDownList ID="ddl_Area" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_AreaResource1" >
            </asp:DropDownList>
           
            </td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CopyFromBranch" runat="server" CssClass="LABEL" Text="Copy From Branch:" meta:resourcekey="lbl_CopyFromBranchResource1"></asp:Label></td>
        <td style="width: 28%"><asp:DropDownList ID="ddl_CopyFromBranch" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_CopyFromBranchResource1" >
        </asp:DropDownList></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Hamali" runat="server" Text="Hamali Rate +/- :" meta:resourcekey="lbl_HamaliResource1" ></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_Hamali" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Minus(this,event)" meta:resourcekey="txt_HamaliResource1"></asp:TextBox></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_HireRate" runat="server" Text="Hire Rate +/- :" meta:resourcekey="lbl_HireRateResource1"></asp:Label>
        </td>
        <td style="width: 28%">
        <asp:TextBox ID="txt_HireRate" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers_With_Minus(this,event)" meta:resourcekey="txt_HireRateResource1"></asp:TextBox>
        </td>
        <td style="width: 2%" class="TDMANDATORY">*
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
