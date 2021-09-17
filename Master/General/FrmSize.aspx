<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSize.aspx.cs" Inherits="Master_General_FrmSize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../Javascript/Master/General/Size.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Size Master</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div>
      <table class="TABLE" width="100%">
        <tr>
          <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Size Master" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="4">
            &nbsp;</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Size Name</td>
          <td style="width: 25%">
            <asp:TextBox ID="txtSizeName" runat="server" CssClass="TEXTBOX" MaxLength="50" Width="189px" />
          </td>
            <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TDMANDATORY" style="width: 55%">
            </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Approx. Charge Wt(kg)</td>
          <td style="width: 25%">
            <asp:TextBox ID="txtApproxChargeWeight" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" Width="70px" />
          </td>
            <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TDMANDATORY" style="width: 55%">
            </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Function</td>
          <td style="width: 25%">
            <asp:DropDownList ID="ddlFunction" runat="server" CssClass="DROPDOWN" Width="92px">
            </asp:DropDownList></td>
            <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TDMANDATORY" style="width: 55%">
            </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Factor/Amount</td>
          <td style="width: 25%">
            <asp:TextBox ID="txtFactorAmount" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" Width="70px" />
          </td>
            <td class="TDMANDATORY" style="width: 1%">
            *</td>
          <td class="TDMANDATORY" style="width: 55%">
            </td>
        </tr>
          <tr>
              <td class="TD1" style="width: 20%">
                  Min. Chrg. Qty.</td>
              <td style="width: 25%">
                  <asp:TextBox ID="txt_MinChrgQty" runat="server" MaxLength ="5" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" Width="68px" /></td>
              <td class="TDMANDATORY" style="width: 1%">
              *</td>
              <td class="TDMANDATORY" style="width: 55%">
                  </td>
          </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Description</td>
          <td style="width: 25%">
            <asp:TextBox ID="txtDescription" runat="server" CssClass="TEXTBOX" MaxLength="255" 
            TextMode="MultiLine" Height="60px" Width="189px"/>
          </td>
            <td class="TDMANDATORY" style="width: 1%">
            </td>
          <td class="TDMANDATORY" style="width: 55%">
            &nbsp;</td>
        </tr>
        <tr>
          <td colspan="4">
            &nbsp;</td>
        </tr>
        <tr>
          <td align="center" colspan="4">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUI()" OnClick="btn_Save_Click" />
            <asp:HiddenField ID="hdnSizeID" runat="server" />
            <asp:CheckBox ID="chkIsDefault" Visible="false" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="4">
            <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
              </Triggers>
              <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                  Text="Fields with * mark are mandatory"></asp:Label>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
