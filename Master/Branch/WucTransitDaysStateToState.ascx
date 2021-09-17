<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTransitDaysStateToState.ascx.cs" Inherits="Master_Branch_WucTransitDaysStateToState" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Branch/TransitDaysStateToState.js"></script>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="TRANSIT DAYS (STATE TO STATE)" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; ">
            <asp:Label ID="lbl_FromState" runat="server" CssClass="LABEL" Text="From State:" meta:resourcekey="lbl_FromStateResource1"></asp:Label></td>
        <td style="width: 28%; ">
            <asp:DropDownList ID="ddl_FromState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_FromStateResource1">
            </asp:DropDownList></td>
        <td style="width: 2%; " class="TDMANDATORY">*
        </td>
        <td class="TD1" style="width: 20%; ">
            <asp:Label ID="lbl_ToState" runat="server" CssClass="LABEL" Text="To State:" meta:resourcekey="lbl_ToStateResource1"></asp:Label></td>
        <td style="width: 28%; ">
            <asp:DropDownList ID="ddl_ToState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ToStateResource1">
            </asp:DropDownList></td>
        <td style="width: 2%; " class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VehicleType" runat="server" CssClass="LABEL" Text="Vehicle Type:" meta:resourcekey="lbl_VehicleTypeResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:DropDownList ID="ddl_VehicleType" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_VehicleTypeResource1">
            </asp:DropDownList></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TransitDays" runat="server" CssClass="LABEL" Text="Transit Days:" meta:resourcekey="lbl_TransitDaysResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_TransitDays" runat="server" onkeypress="return Only_Integers(this,event)"
            onblur="return valid(this)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_TransitDaysResource1"></asp:TextBox></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DistanceInKM" runat="server" CssClass="LABEL" Text="Distance In KM:" meta:resourcekey="lbl_DistanceInKMResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:TextBox ID="txt_DistanceInKM" runat="server"
             onkeypress="return Only_Integers(this,event)" onblur="return valid(this)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_DistanceInKMResource1"></asp:TextBox></td>
        <td style="width: 2%" class="TDMANDATORY">*
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
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
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td align="center"  colspan="6">
            <asp:Button ID="btn_Save" runat="server"  CssClass="BUTTON" OnClick="btn_Save_Click"
                Text="Save" OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td align="left" colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" text=" Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1"></asp:Label></td>
    </tr>
</table>
