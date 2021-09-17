<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucSeriesGeneration.ascx.cs" Inherits="Document_Allocation_WucSeriesGeneration" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Document Allocation/SeriesGeneration.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<table style="width: 100%" class="TABLE">
  <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="SERIES GENERATION" meta:resourcekey="lbl_HeadingResource1" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_GeneratedOn" runat="server" CssClass="LABEL" Text="Generated On:" meta:resourcekey="lbl_GeneratedOnResource1"></asp:Label></td>
        <td style="width: 28%">
            <uc1:WucDatePicker ID="WucSeriesGeneratedDate" runat="server" />
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Text="Document Type:" meta:resourcekey="lbl_DocumentTypeResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_DocumentTypeResource1">
            </asp:DropDownList></td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_StartOn" runat="server" CssClass="LABEL" Text="Start No:" meta:resourcekey="lbl_StartOnResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_StartNo" runat="server" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_StartNoResource1" MaxLength="9"></asp:TextBox></td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_EndNo" runat="server"  CssClass="LABEL" Text="End No:" meta:resourcekey="lbl_EndNoResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_EndNo" runat="server" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_EndNoResource1" MaxLength="9"></asp:TextBox></td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label></td>
    </tr>
    <tr runat="server" id="tr_Save">
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"/></td>
    </tr>
    <tr>
        <td align="left" colspan="6">
            <asp:HiddenField ID="hdn_MinStartNo" runat="server" />
            <asp:HiddenField ID="hdn_MaxEndNo" runat="server" />
        </td>
    </tr>
</table>
