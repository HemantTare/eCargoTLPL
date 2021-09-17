<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucNonEmployeeUsers.ascx.cs" Inherits="Master_General_WucNonEmployeeUsers" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Master/General/UserMaster.js"></script>


<link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="scm_UserMaster" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="7">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="NON EMPLOYEE USERS" ></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>    
        <tr>
        <td class="TD1" style="width: 20%">
            &nbsp;<asp:Label ID="lbl_branch"  CssClass="LABEL" runat="server" Text="Branch :"></asp:Label></td>
        <td style="width: 29%" colspan="3">
        <asp:DropDownList ID="ddl_branch" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>  
    <tr>
        <td class="TD1" style="width: 20%">
         <asp:Label ID="lbl_NonEmpUserName" Text="User Name:" runat="server" ></asp:Label></td>
         <td style="width: 29%; " colspan="4">
            <asp:TextBox ID="txt_NonEmpUserName" runat="server" BorderWidth="1px" Width="99%" CssClass="TEXTBOX" MaxLength="100" ></asp:TextBox>
        </td>
         <td class="TDMANDATORY" style="width: 1%; ">*</td>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>      
   <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Profile" runat="Server"  CssClass="LABEL" Text="Profile :" ></asp:Label> </td>
       
        <td style="width: 29%" >
            <asp:DropDownList ID="ddl_profile" runat="server" CssClass="DROPDOWN" >
            </asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD" style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
   
     <tr>
        <td  colspan="6" style="width:99.7%">
        
        <uc1:WucAddress ID="WucAddress1" runat="server"></uc1:WucAddress>
        </td>
    </tr> 
    <tr id="TR_IsActive" visible="false" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_IsActive" runat="server" Text="Is Active :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:CheckBox ID="chk_IsActive" runat="server" CssClass="CHECKBOX" /></td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD" style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>    
    </tr>
    <tr>
        <td>
           &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON"  OnClientClick="return validateUI()" OnClick="btn_Save_Click" /></td>
    </tr>
     <tr>
     <td colspan="6">
         &nbsp;</td>
    </tr>
    
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_UserMaster" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	         <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR"  Font-Bold="True" Text="Fields with * mark are mandatory"></asp:Label>
	        </ContentTemplate>
          </asp:UpdatePanel>
        </td>
    </tr>
    </table>  
       
       
       

       