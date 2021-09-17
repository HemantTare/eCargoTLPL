<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDriverLicenseCategory.ascx.cs"
    Inherits="Master_Driver_WucDriverLicenseCategory" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Driver/DriverLicenseCategory.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScmDriverLicenseCategory" runat="server" />

<table class="TABLE" style="width: 100%">
    <tr>
        <td colspan="3" class="TDGRADIENT">
        
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="DRIVER LICENSE CATEGORY" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 21px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%"><asp:Label ID="lbl_DriverLicenseCategory" runat="server" Text="Driver License Category :" meta:resourcekey="lbl_DriverLicenseCategoryResource1" CssClass="LABEL"></asp:Label></td>
        <td class="TD1" style="width: 74%">
            <asp:TextBox ID="txt_DriverLicenseCategory" Width="99%" BorderWidth="1px" runat="server"
                CssClass="TEXTBOX" MaxLength="50" meta:resourcekey="txt_DriverLicenseCategoryResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3" style="width: 1%; text-align: center">
        
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <asp:UpdatePanel ID="Upd_Pnl_DriverLicenseCategory" UpdateMode="Conditional" runat="server">
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
            </Triggers>
              <ContentTemplate>
                 <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            </ContentTemplate>
            
            </asp:UpdatePanel>
           
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />

