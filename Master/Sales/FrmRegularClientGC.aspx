<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRegularClientGC.aspx.cs"
  Inherits="Master_Sales_FrmRegularClientGC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Booking/GCNew.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Sales/RegularClient.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Walkin Client</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div>
      <table class="TABLE" style="width: 100%">
        <tr>
          <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Walkin Client"></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Client Name</td>
          <td style="width: 79%" colspan="4">
            <asp:TextBox ID="txtClientName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
              MaxLength="100" onkeypress="return Only_AlphaSpaceNumbers(this,event);" onblur="return Uppercase(this);" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Mobile No</td>
          <td style="width: 29%">
            <asp:TextBox ID="txtMobileNo" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
              MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TD1" style="width: 20%">
            Phone1</td>
          <td style="width: 29%">
            <asp:TextBox ID="txtPhone1" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
              MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
            &nbsp</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Address</td>
          <td style="width: 80%" colspan="5">
            <asp:TextBox ID="txtAddress" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
              MaxLength="100" />
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            City</td>
          <td style="width: 80%" colspan="6">
            <asp:Label ID="lblCity" CssClass="LABEL" Font-Bold="True" BorderWidth="1px"
              runat="server" meta:resourcekey="lbl_Vehicle_CategoryResource1" />
              <asp:HiddenField ID="hdnBranchID" runat="server" />
              <asp:HiddenField ID="hdnCityID" runat="server" />
              <asp:HiddenField ID="hdnGSTStateCode" runat="server" />
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Is GST No. Available ?</td>
          <td style="width: 80%" colspan="6">
            <asp:CheckBox ID="ChkIsServiceTaxPayableByClient" CssClass="CHECKBOX" runat="server" />
          </td>
        </tr>
        
        <tr>
          <td class="TD1" style="width: 20%">
            GST No. </td>
          <td style="width: 80%" colspan="6">
            <asp:TextBox ID="txtCSTNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX" onblur="ValidateGSTOnType(this,document.getElementById('hdnGSTStateCode').value,false); return Uppercase(this);" MaxLength="15" Width="50%"/>
          </td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td align="center" colspan="6">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUIForRegularClientGC();"
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
<script type="text/javascript">

 function updateparentwindow(clientname)
 { 
   self.close();
  window.opener.UpdateFromRegularClient(clientname);

 } 

</script>
