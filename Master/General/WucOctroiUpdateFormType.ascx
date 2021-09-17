<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOctroiUpdateFormType.ascx.cs" Inherits="Master_General_WucOctroiUpdateFormType" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_OctroiUpdateFormType" runat="server" />


<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="OCTROI FORM TYPE"  ></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 20%">
         <asp:Label ID="lbl_OctroiFormType" Text="Octroi Form Type:" runat="server" ></asp:Label></td>
        <td style="width: 79%" colspan="3" class="TDMANDATORY">
        
        <asp:TextBox ID="txt_OctroiFormType" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"  ></asp:TextBox>
            </td>
        <td class="TDMANDATORY" style="width: 1%">*
            </td>
    </tr>
    
    <tr>
        <td>
           &nbsp;
        </td>
    </tr>

<tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save'  CssClass="BUTTON" OnClick="btn_Save_Click"  /></td>
    </tr>
    <tr>
    
    </tr>
    
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_OctroiFormType" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR"  Font-Bold="True"  EnableViewState="False" Text="Fields With * Mark Are Mandatory" ></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
    </tr>
</table>
