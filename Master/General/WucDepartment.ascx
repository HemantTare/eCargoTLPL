<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDepartment.ascx.cs" Inherits="Master_General_WucDepartment" %>

<script type="text/javascript" src="../../Javascript/Master/General/Department.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_Department" runat="server" />


<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="DEPARTMENT" meta:resourcekey="lbl_HeadResource1" ></asp:Label></td>
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
         <asp:Label ID="lbl_Department" Text="Department Name:" runat="server" meta:resourcekey="lbl_DepartmentResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3" class="TDMANDATORY">
        
        <asp:TextBox ID="txt_DepartmentName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50" meta:resourcekey="txt_DepartmentNameResource1" ></asp:TextBox>
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
            <asp:Button ID="btn_Save" runat="server" Text='Save'  OnClientClick=" return validateUI()" CssClass="BUTTON" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1"  /></td>
    </tr>
    <tr>
     <td colspan="6">
     </td>
    </tr>
    
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Department" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR"  Font-Bold="True" meta:resourcekey="lbl_ErrorsResource1" EnableViewState="False" Text="Fields With * Mark Are Mandatory" ></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
    </tr>
</table>
