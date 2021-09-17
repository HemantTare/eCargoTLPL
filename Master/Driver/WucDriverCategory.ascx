<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDriverCategory.ascx.cs" Inherits="Master_Driver_WucDriverCategory" %>
  
 <script language="javascript" type="text/javascript" src="../../Javascript/Master/Driver/DriverCategory.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="scm_DriverCategory" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
        
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="DRIVER CATEGORY" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%"><asp:Label ID="lbl_DriverCategoryName" runat="server" Text="Driver Category :" meta:resourcekey="lbl_DriverCategoryName_Resource1"></asp:Label> </td>
        <td style="width: 79%">
            <asp:TextBox ID="txt_Driver_Category_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="50" Width="550px" meta:resourcekey="txt_Driver_Category_NameResource1" ></asp:TextBox></td>
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
        
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Driver_Category" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />

