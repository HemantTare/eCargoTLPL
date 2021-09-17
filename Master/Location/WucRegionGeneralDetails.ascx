<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegionGeneralDetails.ascx.cs" Inherits="Master_Location_WucRegionGeneralDetailsl" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc2" %>
    
    
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>


<table class="TABLE" width="100%">
<tr>
<td colspan="6" style="width:100%">
<table width="100%">
    <tr>
        <td class="TD1" style="width: 20%">Region Code :</td>
        <td class="TD1" style="width: 29%;text-align:left">
        <asp:Label ID="lbl_ZoneCode"  Font-Bold="True" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ZoneCodeResource1" />
        </td>
        <td class="TD1" style="width: 1%"></td>
        <td class="TD1" style="width: 20%"></td>
        <td class="TD1" style="width: 29%"></td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">Region Name :</td>
        <td class="TD1" style="width: 29%; text-align:left">
        <asp:Label ID="lbl_ZoneName"  Font-Bold="True" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ZoneNameResource1" />
        </td>
        <td class="TD1" style="width: 1%"></td>
        <td class="TD1" style="width: 20%"></td>
        <td class="TD1" style="width: 29%"></td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
   
  
    <tr>
        <td class="TD1" style="width: 20%;">Contact Person :</td>
        <td  style="width: 79%; height: 21px;" colspan="4">
      <asp:TextBox ID="txt_ContactPerson" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="50" Width="100%" meta:resourcekey="txt_ContactPersonResource1"></asp:TextBox> </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *</td>
    </tr>
     <tr>
        <td  colspan="6" style="width:99.7%">
        
        <uc1:WucAddress ID="WucAddress1" runat="server"></uc1:WucAddress>
        </td>
    </tr>   
    <tr>
       
        
        <td class="TD1"   style="width: 20%" >Started On :</td>
        <td style="width: 29%">
      <uc2:wuc_Date_Picker ID="PickerStartedOn" runat="server"  />
</td>
        <td class="TD1" style="width: 1%"></td>
    </tr>
</table>
</td>
</tr>
<tr>
<td colspan="6" style="width:100%">
<asp:Panel ID="pnl_Division" Font-Bold="True" GroupingText="Divisions" runat="server" meta:resourcekey="pnl_DivisionResource1">  
   <table width="100%">
            <tr>      
       
   
        <td style="height: 47px">
        
            <asp:CheckBoxList id="chk_ListDivision" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" meta:resourcekey="chk_ListDivisionResource1">
            </asp:CheckBoxList>
       
      </td>
        </tr>
        </table>
       
   
</asp:Panel>
</td>
</tr>
<tr>
        <td colspan="3">
 <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" ></asp:Label>
  
 </td>
 </tr>
</table>
 
