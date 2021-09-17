
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucResetPassword.ascx.cs" Inherits="Admin_Rights_WucResetPassword" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>


<table width="100%" class="TABLE">
  <tr>
    <td class="TDGRADIENT" colspan="6">
      <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="RESET PASSWORD"/>
    </td>
  </tr>
  
  <tr>
  <td colspan="3">&nbsp;</td>
  </tr>

  
  <tr>
    <td style="width: 15%;" class="TD1">
      Select:</td>
    <td style="width: 34%;">
      <asp:DropDownList ID="ddl_UserType" runat="server" CssClass="DROPDOWN" Width="200"
      AutoPostBack="True" OnSelectedIndexChanged="ddl_UserType_SelectedIndexChanged">
      <asp:ListItem Text="Login Id Wise" Value="1" ></asp:ListItem>
      <asp:ListItem Text="User Wise" Value="2" ></asp:ListItem>
      </asp:DropDownList>
    </td>
    <td style="width: 1%;" class="TDMANDATORY"></td>
    <td style="width:15%" />
    <td style="width:34%" />
    <td style="width:1%" />
  </tr>
  
  <tr id="tr_Employee" runat="server">
    <td style="width: 15%;" class="TD1"></td>
    <td style="width: 34%;">
      <asp:RadioButton ID="rbl_Employee" CssClass="LABEL" Text=" Employee"
      GroupName="Employee" runat="server" AutoPostBack="True" OnCheckedChanged="rbl_Employee_CheckedChanged"  />
      &nbsp;
      <asp:RadioButton ID="rbl_NonEmployee" CssClass="LABEL" Text=" Non Employee"
      GroupName="Employee" runat="server" AutoPostBack="True" OnCheckedChanged="rbl_NonEmployee_CheckedChanged"  />
      &nbsp;
      <asp:RadioButton ID="rbl_Client" Visible="false" CssClass="LABEL" Text=" Client"
      GroupName="Employee" runat="server" AutoPostBack="True" OnCheckedChanged="rbl_Client_CheckedChanged"/>
    </td>
    <td style="width: 1%;" class="TDMANDATORY"></td>
    <td style="width:15%" />
    <td style="width:34%" />
    <td style="width:1%" />
  </tr>
    
  <tr>
    <td style="width: 15%;" class="TD1">User :</td>
    <td style="width: 34%;">
      <cc1:DDLSearch ID="ddl_User" runat="server" AllowNewText="True"  CallBackAfter="2" 
      CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetUserForResetPassword" IsCallBack="True"
      OnTxtChange="ddl_User_TxtChange" PostBack="true"/>       
      <asp:HiddenField ID="hdn_user_type_id" runat="server" />
    </td>
    <td style="width: 1%;" class="TDMANDATORY"></td>
    <td style="width:15%" />
    <td style="width:34%" />
    <td style="width:1%" />
  </tr>
    
     
  <tr id="tr_user_details1" runat="server">
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_Department" runat="server" Text="Department:"></asp:Label>
    </td>
    <td style="width: 34%">
      <asp:Label ID="lbl_DepartmentValue" runat="server" Font-Bold="True"></asp:Label>        
    </td>
    <td style="width:1%"></td>
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_Profile" runat="server" Text="Profile:"></asp:Label>
    </td>
    <td style="width: 34%">
      <asp:Label ID="lbl_ProfileValue" runat="server" Font-Bold="True"></asp:Label>
    </td>
    <td style="width: 1%"></td>
  </tr>
 
  
    
  <tr id="tr_user_details2" runat="server">
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_Hierarchy" runat="server" Text="Hierarchy:"></asp:Label>
    </td>
    <td style="width: 34%">
      <asp:Label ID="lbl_HierarchyValue" runat="server" Font-Bold="True"></asp:Label>
    </td>
    <td style="width: 1%"></td>
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_username" runat="server" Text="User:"></asp:Label>
    </td>
    <td style="width: 34%">
      <asp:Label ID="lbl_usernamevalue" runat="server" Font-Bold="True"></asp:Label>
    </td>
    <td style="width: 1%"></td>
  </tr>

  <tr id="tr_user_details3" runat="server">
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_user_id" runat="server" Text="Hierarchy:"></asp:Label>
    </td>
    <td style="width: 34%">
      <asp:Label ID="lbl_user_id_value" runat="server" Font-Bold="True"></asp:Label>
    </td>
  </tr>         
  
  <tr>
    <td colspan="6">
      <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" Font-Bold="true" ForeColor="red"></asp:Label>
    </td>
  </tr>  
      
  <tr>
    <td align="center" colspan="6">
      &nbsp;<asp:Button ID="btn_Reset" runat="server" Text="Reset" CssClass="BUTTON" OnClick="btn_Reset_Click"/>
    </td>
  </tr>
</table>
