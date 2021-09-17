<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucChangePassword.ascx.cs"
    Inherits="Login_WucChangePassword" %>

<script type="text/javascript">
function password_valid()
{
var txt_new_password = document.getElementById('<%=txt_NewPassword.ClientID%>');
if (txt_new_password.value.length < 4)
  {
  alert("password Should Have atleast 4 characters");
  txt_new_password.focus();
  return false;
  }
}

function ValidateUI()
{
  var _isValid=false;
  var txt_OldPassword=document.getElementById('<%=txt_OldPassword.ClientID%>');
  var txt_NewPassword=document.getElementById('<%=txt_NewPassword.ClientID%>');
  var txt_ConPassword=document.getElementById('<%=txt_ConPassword.ClientID%>');
  var lbl_Error=document.getElementById('<%=lbl_Error.ClientID%>');
  var lbl_Login=document.getElementById('<%=lbl_Login.ClientID%>');
  var reg='(?=\\w*\\d)';
  var reg1='(?!^[0-9]*$)(?!^[a-zA-Z]* $)^([a-zA-Z0-9]{6,10})$';
  
  if(txt_OldPassword.value=='')
  {
   lbl_Error.innerText="Plase Enter Old Password!";
   txt_OldPassword.focus();
  }
  else if(txt_NewPassword.value=='')
  {
   lbl_Error.innerText="Please Enter New Password!";
   txt_NewPassword.focus();
  }
  else if(txt_NewPassword.value.length < 6)
  {
   lbl_Error.innerText="Password should not be less than 6 Digit";
   txt_NewPassword.focus();
  }
//  else if(!txt_NewPassword.value.match(reg))
//  {
//   //alert(reg);
//   lbl_Error.innerText="Password Should contains atleast one Digit";
//   txt_NewPassword.focus();
//  }
//  else if(!txt_NewPassword.value.match(reg1))
//  {
//   //alert(reg1);
//   lbl_Error.innerText="Password Should contains atleast one alphabet";
//   txt_NewPassword.focus();
//  }
  else if(txt_ConPassword.value=='')
  {
   lbl_Error.innerText="Please Enter Confirm Password!";
   txt_ConPassword.focus();
  }
  else if(lbl_Login.innerText.toUpperCase()==txt_NewPassword.value.toUpperCase())
  {
   lbl_Error.innerText="User Name And Password Should Not Be Same";
   txt_NewPassword.focus();
  }
  else if(txt_OldPassword.value.toUpperCase()==txt_NewPassword.value.toUpperCase())
  {
   lbl_Error.innerText="Please Enter different password!";
   txt_NewPassword.focus();
  }
 
  else if(txt_NewPassword.value.toUpperCase() != txt_ConPassword.value.toUpperCase())
  {
   lbl_Error.innerText="New Password and Confirm Password do not match!";
  }
  
  else 
  {
   _isValid=true;
  }
  
  return  _isValid;
  
}
</script>

<asp:ScriptManager ID="scm_ChangePassword" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 450px">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="CHANGE PASSWORD"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 32%">
            &nbsp;
        </td>
        <td style="width: 36%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 32%" class="TD1">
            Login:</td>
        <td colspan="5" style="text-align: left;">
            <asp:Label ID="lbl_Login" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 32%" class="TD1">
            Old Password:</td>
        <td colspan="4" style="text-align: left">
            <%--<asp:UpdatePanel ID="up_OldPassword" runat="server" UpdateMode="always">
                <ContentTemplate>--%>
            <asp:TextBox ID="txt_OldPassword" runat="Server" MaxLength="25" BorderWidth="1px"
                CssClass="TEXTBOX" TextMode="Password"></asp:TextBox>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 32%" class="TD1">
            New Password:</td>
        <td style="text-align: left" colspan="4">
            <%--<asp:UpdatePanel ID="up_NewPass" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <asp:TextBox ID="txt_NewPassword" runat="server" MaxLength="25" BorderWidth="1px"
                CssClass="TEXTBOX" TextMode="Password"></asp:TextBox>
            <%--</ContentTemplate>
                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>--%>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 25%" class="TD1">
            Confirm Password:</td>
        <td style="text-align: left" colspan="4">
            <%-- <asp:UpdatePanel ID="up_ConfirmPass" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <asp:TextBox ID="txt_ConPassword" runat="server" MaxLength="25" BorderWidth="1px"
                CssClass="TEXTBOX" TextMode="Password"></asp:TextBox>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_submit" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
            <asp:HiddenField ID="hdn_IsTrue" runat="Server" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_submit" runat="server" CssClass="BUTTON" Text="Submit" OnClientClick="return ValidateUI()"
                ValidationGroup="btn_Submit" OnClick="btn_submit_Click" /></td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
            <%-- <asp:UpdatePanel ID="up_Errors" runat="server">
                <ContentTemplate>--%>
            <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
</table>
