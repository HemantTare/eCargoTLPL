<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLogin.ascx.cs" Inherits="WucLogin" %>
<asp:ScriptManager ID="SM_Login" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">   

        function GetCPUinfo()
         {
        // debugger
            var a,b,c
            a= new ActiveXObject("Scripting.FileSystemObject");
            b=a.GetDrive(a.GetDriveName("C:"));
            c=b.SerialNumber;
 
                 if(c != "")
                    {
                     document.getElementById( "<%=myCPUid.ClientID %>").value = c ; //document.getElementById("CPU").CPUID
                     
                    }
                    else
                    {
                        alert("There is some problem in retrieving your machine information");
                      return false;
                    }

              }
</script>

<%--<body>

  <div style="top:100px; left:1px; position: absolute; z-index:200">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
    width="500" height="500">
    <param name="movie" value="flash/holi.swf"/>
    <param name="quality" value="high"/>
    <param name="wmode" value="transparent"/>
    <embed src="flash/holi.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash"</embed></object>  
  </div>
  
</body> --%> 


<div style="text-align: center;">
  <table border="1" cellpadding="0" cellspacing="0" style="width: 460px">
    <tr>
      <td style="width: 158px">
        <asp:Image ID="ImgLogin" runat="server" ImageUrl="~/Images/Login.gif" Height="212px"
          meta:resourcekey="ImgLoginResource2" /></td>
      <td style="width: 292px">
        <table class="TABLE">
          <tr>
            <td colspan="4" class="TDGRADIENT" style="text-align: left;">
              <asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="PLEASE LOGIN HERE"
                meta:resourcekey="Label1Resource2" />
            </td>
          </tr>
          <tr>
            <td class="TD1" style="font-size: xx-small;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr id="TRDivision" runat="Server">
            <td class="TD1" style="width: 40%">
              <asp:Label ID="lbl_SelectDivision" Text="Select Division:" runat="server" meta:resourcekey="lbl_SelectDivisionResource1" /></td>
            <td style="width: 59%">
              <asp:DropDownList ID="ddl_Division" runat="server" Font-Size="9px" Font-Names="Verdana"
                Width="100%" meta:resourcekey="ddl_DivisionResource2" /></td>
            <td style="width: 1%">
              <asp:RequiredFieldValidator ID="RFV_Division" runat="server" ControlToValidate="ddl_Division"
                ErrorMessage="*" meta:resourcekey="RFV_DivisionResource2" /></td>
          </tr>
          <tr>
            <td class="TD1" style="font-size: 1px;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr>
            <td class="TD1" style="width: 40%">
              <asp:Label ID="lbl_SelectYear" runat="server" Text="Select Year:" meta:resourcekey="lbl_SelectYearResource1" /></td>
            <td style="width: 59%">
              <asp:DropDownList ID="ddl_Year" runat="server" Font-Size="9px" Font-Names="Verdana"
                Width="100%" meta:resourcekey="ddl_YearResource2" /></td>
            <td style="width: 1%">
              <asp:RequiredFieldValidator ID="RFV_Year" runat="server" ControlToValidate="ddl_Year"
                ErrorMessage="*" meta:resourcekey="RFV_YearResource2" /></td>
          </tr>
          <tr>
            <td class="TD1" style="font-size: 1px;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr>
            <td style="width: 40%;" class="TD1">
              <asp:Label ID="lbl_login" runat="server" Text="Login:" CssClass="LABEL" meta:resourcekey="lbl_loginResource2" /></td>
            <td style="width: 59%;">
              <asp:TextBox ID="txt_login" runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="15"
                meta:resourcekey="txt_loginResource2" /></td>
            <td style="width: 1%">
              <asp:RequiredFieldValidator ID="RFV_Login" runat="server" ControlToValidate="txt_login"
                ErrorMessage="*" meta:resourcekey="RFV_LoginResource2" /></td>
          </tr>
          <tr>
            <td class="TD1" style="font-size: 1px;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr>
            <td style="width: 40%;" class="TD1">
              <asp:Label ID="lbl_password" runat="server" Text="Password:" CssClass="LABEL" meta:resourcekey="lbl_passwordResource2" /></td>
            <td style="width: 59%;">
              <asp:TextBox ID="txt_password" runat="server" CssClass="TEXTBOX" TextMode="Password"
                BorderWidth="1px" MaxLength="15" meta:resourcekey="txt_passwordResource2" /></td>
            <td style="width: 1%">
              <asp:RequiredFieldValidator ID="RFV_Password" runat="server" ControlToValidate="txt_password"
                ErrorMessage="*" meta:resourcekey="RFV_PasswordResource2" /></td>
          </tr>
          <tr>
            <td style="font-size: xx-small;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr>
            <td align="center" colspan="3">
              <asp:Button ID="btn_login" runat="server" Text="Login" CssClass="BUTTON" OnClientClick="GetCPUinfo();"
                OnClick="btn_login_Click" meta:resourcekey="btn_loginResource2" />
            </td>
          </tr>
          <tr>
            <td colspan="3" style="width: 20%; font-size: 2px;">
              &nbsp;</td>
          </tr>
          <tr>
            <td colspan="3" style="width: 20%; height: 16px; text-align: left;">
              &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True"
                ForeColor="Red" Visible="False" meta:resourcekey="lbl_ErrorsResource1" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <%--Apurva for web Authentication--%>
    <tr visible="false">
      <td visible="false">
        <asp:HiddenField ID="myCPUid" runat="server" />
        <asp:HiddenField ID="DefaultGateway" runat="server" />
        <asp:HiddenField ID="DNSdomain" runat="server" />
        <asp:HiddenField ID="DNSHostName" runat="server" />
      </td>
    </tr>
    <%--End for web Authentication--%>
  </table>
  <br />
  <asp:UpdatePanel ID="UP_Error_Message" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:Label ID="lbl_Error_Message" runat="server" CssClass="LABEL" Font-Bold="True"
        ForeColor="Red" meta:resourcekey="lbl_Error_MessageResource1" />
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btn_login" />
    </Triggers>
  </asp:UpdatePanel>
</div>
