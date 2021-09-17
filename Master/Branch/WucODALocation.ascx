<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucODALocation.ascx.cs"
  Inherits="Master_Branch_WucODALocation" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Branch/ODALocation.js"></script>

<asp:ScriptManager ID="scm_Location" runat="server" />

<script type="text/javascript">
 function Update_Location_Details()
 {  
    var hdn_LocationId = document.getElementById('WucODALocation1_hdn_LocationId');   
    var txt_LocationName = document.getElementById('WucODALocation1_txt_LocationName');   
    var hdn_Is_FromLocation = document.getElementById('WucODALocation1_hdn_Is_FromLocation');   

    window.opener.Set_Location_Details(hdn_LocationId.value,txt_LocationName.value,hdn_Is_FromLocation.value); 
 }
 
</script>

<table class="TABLE" style="width: 100%">
  <tr>
    <td class="TDGRADIENT" colspan="7">
      &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Service Location"
        meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
  </tr>
  <tr>
    <td colspan="7">
      &nbsp;
    </td>
  </tr>
  <tr>
    <td class="TD1" style="width: 20%">
      <asp:Label ID="lbl_LocationName" runat="server" Text="Service Location Name:" meta:resourcekey="lbl_LocationNameResource1"></asp:Label></td>
    <td style="width: 29%">
      <asp:TextBox ID="txt_LocationName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="50" meta:resourcekey="txt_LocationNameResource1"></asp:TextBox>
    </td>
    <td class="TDMANDATORY" style="width: 1%">
      *
    </td>
    <td  class="TD1" style="width: 19%">
      <asp:Label ID="Label1" runat="server" CssClass="LABEL" Text="Default Dly Type :"></asp:Label>
    </td>
    <td class="TD1" style="width: 29%">
      <asp:DropDownList ID="ddl_delivery_Type" runat="server" CssClass="DROPDOWN" />
    </td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr>
    <td class="TD1">
      <asp:Label ID="lbl_ControllingBranch" runat="server" Text="Controlling Branch:" meta:resourcekey="lbl_ControllingBranchResource1"></asp:Label></td>
    <td>
      <asp:DropDownList ID="ddl_ControllingBranch" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ControllingBranchResource1">
      </asp:DropDownList>
    </td>
    <td class="TDMANDATORY">
      *
    </td>
    <td class="TD1">
      <asp:Label ID="lbl_DistFromBranch" runat="server" Text="Distance From Branch:" meta:resourcekey="lbl_DistFromBranchResource1"></asp:Label>
    </td>
    <td >
      <asp:TextBox ID="txt_DistFrmBranch" runat="server" onkeypress="return Only_Integers(this,event)"
        onblur="return valid(this)" CssClass="TEXTBOXNOS" MaxLength="4" meta:resourcekey="txt_DistFrmBranchResource1"></asp:TextBox>
    </td>
    <td class="TD1">
      Kms</td>
  </tr>
  
      <tr>
        <td class="TD1">
            <asp:Label ID="lbl_City" runat="Server" Text="City :" CssClass="LABEL"></asp:Label></td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc1:DDLSearch Id="ddl_City" runat="server" AllowNewText="false" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAllSearchCity"
                        IsCallBack="true" OnTxtChange="ddl_City_SelectedIndexChanged"  CallBackAfter="2" Text="" PostBack="True"></cc1:DDLSearch>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_City" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY">
        </td>
        <td class="TD1">
        </td>
        <td>
        </td>
        <td class="TD1">
        </td>
    </tr>

  <tr>
    <td class="TD1">
      <asp:Label ID="lbl_PrimaryPinCode" runat="server" Text="Pin Code Primary:" meta:resourcekey="lbl_PrimaryPinCodeResource1"></asp:Label></td>
    <td>
      <asp:TextBox ID="txt_PrimaryPinCode" onkeypress="return Only_Numbers(this,event)"
        onblur="return valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="6" meta:resourcekey="txt_PrimaryPinCodeResource1"></asp:TextBox>
    </td>
    <td class="TDMANDATORY">
      *
    </td>
    <td class="TD1">
      <asp:Label ID="lbl_SecondaryPinCode" runat="server" Text="Pin Code Secondary:" meta:resourcekey="lbl_SecondaryPinCodeResource1"></asp:Label>
    </td>
    <td>
      <asp:TextBox ID="txt_SecondaryPinCode" onkeypress="return Only_Numbers(this,event)"
        onblur="return valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="6" meta:resourcekey="txt_SecondaryPinCodeResource1"></asp:TextBox>
    </td>
    <td style="width: 1%">
    </td>
  </tr>
  <tr>
    <td class="TD1">
      <asp:Label ID="lbl_IsBooking" Text="Is To Pay Booking Allowed ?" runat="server" meta:resourcekey="lbl_IsBookingResource1"></asp:Label>
    </td>
    <td>
      <asp:CheckBox ID="Chk_IsBookingAllowed" CssClass="CHECKBOX" runat="server" meta:resourcekey="Chk_IsBookingAllowedResource1"
        Checked="True" />
    </td>
    <td class="TD1" style="width: 1%">
    </td>
    <td class="TD1">
    </td>
    <td>
    </td>
    <td class="TD1">
    </td>
  </tr>
  <tr>
    <td class="TD1">
      <asp:Label ID="lbl_IsODALocation" Text="Is It ODA Location?" runat="server" meta:resourcekey="lbl_IsODALocationResource1"></asp:Label>
    </td>
    <td>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:CheckBox ID="Chk_IsODALocation" CssClass="CHECKBOX" runat="server" AutoPostBack="True"
            meta:resourcekey="Chk_IsODALocationResource1" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 1%">
    </td>
    <td class="TD1" >
      <asp:Label ID="lbl_IsOctroi" Text="Is Octroi Applicable?" runat="server" meta:resourcekey="lbl_IsOctroiResource1"></asp:Label>
    </td>
    <td>
      <asp:CheckBox ID="Chk_IsOctroiApplicable" CssClass="CHECKBOX" runat="server" meta:resourcekey="Chk_IsOctroiApplicableResource1" />
    </td>
    <td class="TD1">
    </td>
  </tr>
  <tr id="tr_Charges" runat="server">
    <td class="TD1">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_ChargesUpto" Text="ODA Charges Upto 500 Kg:" runat="server" meta:resourcekey="lbl_ChargesUptoResource1"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:TextBox ID="txt_ChargeUpto" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
            onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)" meta:resourcekey="txt_ChargeUptoResource1"
            MaxLength="16"></asp:TextBox>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td class="TD1">
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_MandatoryRs1" runat="server" Text="Rs" meta:resourcekey="lbl_MandatoryRsResource1"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td class="TD1">
      <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Mandatory1" runat="server" CssClass="TDMANDATORY" meta:resourcekey="lbl_Mandatory1Resource1"
            Text="*"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td class="TD1">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_ChargesAbove" Text="ODA Charges Above 500 Kg:" runat="server"
            meta:resourcekey="lbl_ChargesAboveResource1"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td>
      <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:TextBox ID="txt_ChargeAbove" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
            onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)" meta:resourcekey="txt_ChargeAboveResource1"
            MaxLength="16"></asp:TextBox>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td>
      <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="Chk_IsODALocation" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_MandatoryRs2" runat="server" Text="Rs" meta:resourcekey="lbl_MandatoryRs2Resource1"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td>
      <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Mandatory2" runat="server" CssClass="TDMANDATORY" meta:resourcekey="lbl_Mandatory2Resource1"
            Text="*"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td>
      &nbsp;
    </td>
  </tr>
  <tr>
    <td align="center" colspan="6">
      <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI()"
        OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /></td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:HiddenField ID="hdf_ResourecString" runat="server" />
    </td>
  </tr>
  <tr>
    <td colspan="3" style="height: 94px">
      <asp:UpdatePanel ID="Upd_Pnl_Location" UpdateMode="Conditional" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Save" />
        </Triggers>
        <ContentTemplate>
          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True"
            meta:resourcekey="lbl_ErrorsResource1" EnableViewState="False" Text=" Fields with * mark are mandatory"></asp:Label>
          <asp:HiddenField runat="server" ID="hdn_LocationId"></asp:HiddenField>
          <asp:HiddenField runat="server" ID="hdn_Is_FromLocation"></asp:HiddenField>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td colspan="1" style="height: 94px">
    </td>
  </tr>
</table>
