<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucBranchLogin.ascx.vb" Inherits="Master_General_wucBranchLogin" %>

<script language="javascript" type="text/javascript">   

        function GetCPUinfo()
         {
        
            var a,b,c
            a= new ActiveXObject("Scripting.FileSystemObject");
            b=a.GetDrive(a.GetDriveName("C:"));
            c=b.SerialNumber;
 
                 if(c != "")
                    {
                     document.getElementById( "<%=hdn_CPUid.ClientID %>").value = c ; //document.getElementById("CPU").CPUID
                     
                    }
                    else
                    {
                        alert("There is some problem in retrieving your machine information");
                      return false;
                    }

              }
</script>

<table class="TABLE" border="0" width="100%">
<tr>
<td colspan ="4" class="TDGRADIENT" style="text-align: left">
  &nbsp;<asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="LOGIN AS"/>
</td>
</tr>

<tr>
  <td class="TD1" style="font-size: xx-small;" colspan="3">&nbsp;</td>
</tr>

<tr>
<td class="TD1" style="width: 20%">Login Hierarchy:</td>
<td style="width: 80%">
  <asp:RadioButtonList ID="rbl_login_heirarchy" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" TabIndex="1">
    <asp:ListItem Value="0" Text="Branch"/>
    <asp:ListItem Value="1" Text="Area"/>
    <asp:ListItem Value="2" Text="Region"/>
  </asp:RadioButtonList>
</td>
</tr>

<tr>
<td class="TD1" style="width: 20%">Select Year:</td>
<td style="width: 80%">
  <asp:DropDownList ID="ddl_Year" CssClass="DROPDOWN" runat="server" FONT-SIZE="9px" Font-Names="Verdana" Width="100%" TabIndex="2"/>
</td>
</tr>

<tr>
<td style="width: 20%;" class="TD1">
  <asp:Label ID="lblBranch" runat="server" Text="Select Branch:" CssClass="LABEL"/>
</td>
<td style="width: 80%;">
  <asp:DropDownList ID="ddlBranch" CssClass="DROPDOWN" runat="server" FONT-SIZE="9px" Font-Names="Verdana" Width="100%" AutoPostBack="True" TabIndex="3"/>
</td>
</tr>

<tr>
<td class="TD1" style="width: 20%">Order By:</td>
<td style="width: 80%">
  <asp:RadioButtonList ID="rbl_order_by" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" TabIndex="4">
    <asp:ListItem Value="0" Text="User Name"/>
    <asp:ListItem Value="1" Text="Employee Name"/>
    <asp:ListItem Value="2" Text="User Profile"/>
  </asp:RadioButtonList>
</td>
</tr>

<tr>
<td style="width: 20%;" class="TD1">
  <asp:Label ID="lbluser" runat="server" Text="Select User:" CssClass="LABEL"/>
</td>
<td style="width: 80%;">
  <asp:DropDownList ID="ddluser" CssClass="DROPDOWN" runat="server" FONT-SIZE="9px" Font-Names="Verdana" Width="100%" TabIndex="5"/>
  <asp:HiddenField ID="hdn_user_name" runat="server" />
  <asp:HiddenField ID="hdn_password" runat="server" />
  <asp:HiddenField ID="hdn_emp_id" runat="server" />
  <asp:HiddenField ID="hdn_CPUid" runat="server" />
</td>
</tr>

<tr id="trdivision" runat="server">
<td class="TD1" style="width: 20%"><asp:Label ID="lbl_division_caption" runat="server" Text="Select Branch:" CssClass="LABEL"/></td>
<td style="width: 80%">
  <asp:DropDownList ID="ddl_division" CssClass="DROPDOWN" runat="server" FONT-SIZE="9px" Font-Names="Verdana" Width="100%" TabIndex="6"/>
</td>
</tr>

<tr>
<td align="center" colspan="3">
  <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="BUTTON" OnClientClick="GetCPUinfo()" TabIndex="7" />
</td>
</tr>

<tr>
<td class="TD1" colspan="3">
  <a href="javascript:history.go(-1)" tabindex="8">Back</a>
</td>
</tr>

<tr>
<td colspan="3" style="width: 20%; text-align: left;">
&nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True"
ForeColor="Red" Text="Invalid Login or Password !" Visible="False"/>
</td>
</tr>
</table>