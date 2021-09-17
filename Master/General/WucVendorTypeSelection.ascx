<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVendorTypeSelection.ascx.cs" Inherits="Master_General_WucVendorTypeSelection" %>
<asp:ScriptManager ID="scm_VendorTypeSelection" runat="server" />
  <script language="javascript" type="text/javascript" src="../../../JavaScript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../../JavaScript/Fleet/Master/General/VendorTypeSelection.js"></script>

<table class="TABLE">
    <tr>
      <td class="TDGRADIENT" colspan="8">&nbsp;
        <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VENDOR TYPE SELECTION" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
      </td>
     </tr>
    <tr>
      <td colspan="8">&nbsp;</td>
    </tr>
    <tr>
     <td class="TD1" style="width:20%">
     <asp:Label ID="lbl_KeyName" runat="server" Text="Key Name:" meta:resourcekey="lbl_KeyNameResource1"></asp:Label></td>
     <td style="width:29%">
     <asp:DropDownList ID="ddl_KeyName" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_KeyName_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddl_KeyNameResource1" /></td>     
     <td class="TDMANDATORY" style="width: 1%">*</td>
  
     <td class="TD1" style="width:20%"></td>
     <td style="width:29%"></td>     
     <td style="width: 1%"></td>
    
   </tr>
   <tr>
      
        <td style="width: 20%; vertical-align: top;" class="TD1">
         <asp:Label ID="lbl_VendorType" runat="server" Text="Select Vendor Type:" meta:resourcekey="lbl_VendorTypesResource1"></asp:Label></td>
        <td colspan="5" style="width: 70%; text-align: left;" class="TD1">
            <asp:Panel ID="pnl_Vendor_Type" BorderWidth="1px"  Width="90%" Height="100px" runat="server" meta:resourcekey="pnl_Vendor_TypeResource1" >
               <asp:UpdatePanel ID="Upd_Panel_Godown" runat="server" UpdateMode="Conditional">
                    <Triggers>                   
                     <asp:AsyncPostBackTrigger ControlID="ddl_KeyName" />                        
                    </Triggers>
                    <ContentTemplate>              
               <asp:CheckBoxList ID="ChkList_VendorType" CellSpacing="5" RepeatColumns="4" BorderWidth="0px" runat="server" meta:resourcekey="ChkList_VendorTypeResource1" />
               </ContentTemplate>
                </asp:UpdatePanel>      
                </asp:Panel> 
                     
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
        </td>
    </tr>
    <tr>
      <td align="center" colspan="6">
        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
      </td>
    </tr>
    
    <tr>
      <td colspan="8">
        <asp:UpdatePanel ID="Upd_Pnl_Service" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1" Text="Fields with * mark are mandatory"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
      </td>
    </tr>
 </table>
 <asp:HiddenField ID="hdf_ResourecString" runat="server" />
