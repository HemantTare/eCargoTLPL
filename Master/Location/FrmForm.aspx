<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmForm.aspx.cs" Inherits="Master_Location_FrmForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/jscript">
function validateUI()
{
  
var ATS = false;
var txtFormName = document.getElementById('txtFormName');
var ddlFromState = document.getElementById('ddlFromState');
var ddlToState= document.getElementById('ddlToState');

var lblErrors =document.getElementById('lblErrors');
lblErrors.innerText='';

if (txtFormName.value  == '')
{
   lblErrors.innerText = "Please enter Form name";
   txtFormName.focus();
}
else if (parseInt(ddlFromState.value) == 0)
{
   lblErrors.innerText = "Please Select From State";
   ddlFromState.focus();
}
else if (parseInt(ddlToState.value) == 0)
{
   lblErrors.innerText = "Please Select To State";
   ddlToState.focus();
}
else if (parseInt(ddlFromState.value) == parseInt(ddlToState.value))
{
   lblErrors.innerText = "From State and To State cannot be equal";
   ddlToState.focus();
}

else 
  ATS = true;

 return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Form Master</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div>
      <table class="TABLE" width="100%">
        <tr>
          <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Form Master"></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="3">
            &nbsp;</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            From Name</td>
          <td style="width: 79%">
            <asp:TextBox ID="txtFormName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
              MaxLength="100" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            From State</td>
          <td style="width: 79%">
            <asp:DropDownList ID="ddlFromState" CssClass="DROPDOWN" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            To State</td>
          <td style="width: 79%">
            <asp:DropDownList ID="ddlToState" CssClass="DROPDOWN" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td align="center" colspan="6">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUI()"
              OnClick="btnSave_Click" />
          </td>
        </tr>
        <tr>
          <td align="left" colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
              </Triggers>
              <ContentTemplate>
                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                  Text="Fields with * mark are mandatory"></asp:Label>
                <asp:HiddenField ID="hdnKeyID" runat="server" />
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
