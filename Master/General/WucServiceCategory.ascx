<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucServiceCategory.ascx.cs" Inherits="Master_Work_Order_WucServiceCategory" %>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Master/General/ServiceCategory.js"></script>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<asp:ScriptManager ID="scm_ServiceCategory" runat="server" />

<table class="TABLE">
    <tr>
      <td class="TDGRADIENT" colspan="3">&nbsp;
        <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="SERVICE CATEGORY" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
      </td>
     </tr>
    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    
   <tr>
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_Category" runat="Server" Text="Service Category:" meta:resourcekey="lbl_CategoryResource1"></asp:Label></td>
     <td style="width:79%">
        <asp:TextBox ID="txt_ServiceCategory" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="50" meta:resourcekey="txt_ServiceCategoryResource1"></asp:TextBox></td>
     <td class="TDMANDATORY"  style="width: 1%">*</td>
   </tr>
   <tr>
     <td class="TD1" style="width:20%;vertical-align:top "><asp:Label ID="lbl_Description" runat="server" Text="Description:" meta:resourcekey="lbl_DescriptionResource1"></asp:Label></td>
     <td style="width:79%">
        <asp:TextBox ID="txt_Description" runat="server" CssClass ="TEXTBOX" Height="60px" TextMode="MultiLine" BorderWidth="1px" MaxLength="255" meta:resourcekey="txt_DescriptionResource1"></asp:TextBox></td>
     <td class="TDMANDATORY"  style="width: 1%"></td>
   </tr>
    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    
    <tr>
      <td align="center" colspan="3">
        <asp:Button ID="btn_Save" runat="server" Text="Save"  CssClass="BUTTON" OnClientClick="return validateUI()"  OnClick="btn_Save_Click"  meta:resourcekey="btn_SaveResource1"/>
      </td>
    </tr>
    
    <tr>
      <td colspan="3">
        <asp:UpdatePanel ID="Upd_Pnl_ServiceCategory" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
      </td>
    </tr>
 </table>