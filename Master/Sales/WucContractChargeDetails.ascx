<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractChargeDetails.ascx.cs" Inherits="Master_Sales_WucContractChargeDetails" %>
<script type="text/javascript" src="../../Javascript/Common.js" language="javascript" ></script>

<table style="width: 100%"class="TABLE">
  <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="OTHER STANDARD CHARGES" meta:resourcekey="lbl_HeadingResource1"  ></asp:Label>
        </td>        
    </tr>
     <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td class="GRIDHEADERCSS" style="width: 50%">
            <asp:Label ID="Label1" runat="server" Text="Charge Head" meta:resourcekey="Label1Resource1"></asp:Label></td>
        <td style="width: 35%" class="GRIDHEADERCSS">
            <asp:Label ID="Label2" runat="server" Text="Rate" meta:resourcekey="Label2Resource1"></asp:Label></td>
        <td style="width: 15%" class="GRIDHEADERCSS"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_BiltyCharges" runat="server" CssClass="LABEL" Text="Bilty Charges:" meta:resourcekey="lbl_BiltyChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_BiltyCharges" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_BiltyChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
        <asp:Label ID="lbl_BiltyChargesUnit" runat="server" Text="Rs" meta:resourcekey="lbl_BiltyChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_FOVPer" Text="FOV %:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_FOVPerResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_FOVPer" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_FOVPerResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
        <asp:Label ID="lbl_FOVPerCentMark" runat="server" Text="%" meta:resourcekey="lbl_FOVPerCentMarkResource1" /></td>
    </tr>
     <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_FOVRate" Text="FOV Rate:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_FOVRateResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_FOVRate" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_FOVRateResource1">0</asp:TextBox></td>
        <td style="width: 15%"></td>
    </tr>
     <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_InvoiceRate" Text="Invoice Rate:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_InvoiceRateResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_InvoiceRate" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_InvoiceRateResource1">0</asp:TextBox></td>
        <td style="width: 15%">
        <asp:Label ID="lbl_InvoiceRateUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_InvoiceRateUnitResource1" />
        </td>
    </tr>
     <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_InvoicePerRS" Text="Invoice Per:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_InvoicePerRSResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_InvoicePerRs" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_InvoicePerRsResource1">0</asp:TextBox></td>
        <td style="width: 15%">
        <asp:Label ID="lbl_InvoiceUnit" runat="server" Text="Rs" meta:resourcekey="lbl_InvoiceUnitResource1" />
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_Hamali" Text="Hamali:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_HamaliResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_Hamali" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_HamaliResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
           <asp:Label ID="lbl_HamaliUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_HamaliUnitResource1" /></td>
    </tr>
        <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_HamaliArticle" Text="Hamali:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_HamaliArticleResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_HamaliArticle" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_HamaliArticleResource1" >0</asp:TextBox></td>
        <td style="width: 15%">
           <asp:Label ID="lbl_HamaliArticleUnit" runat="server" Text="Rs/Article" meta:resourcekey="lbl_HamaliArticleUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_LocalCharges" Text="Local Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_LocalChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_LocalCharges" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_LocalChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
          <asp:Label ID="lbl_LocalChargesUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_LocalChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_ToPayCharges" Text="To Pay Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ToPayChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_ToPayCharges" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_ToPayChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
          <asp:Label ID="lbl_ToPayChargesUnit" runat="server" Text="Rs" meta:resourcekey="lbl_ToPayChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_DACCCharges" Text="DACC Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DACCChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_DACCCharges" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_DACCChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
          <asp:Label ID="lbl_DACCChargesUnit" runat="server" Text="Rs" meta:resourcekey="lbl_DACCChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_DoorDeliveryCharges" Text="Door Delivery Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DoorDeliveryChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_DoorDeliveryCharges" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_DoorDeliveryChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
            <asp:Label ID="lbl_DoorDeliveryChargesUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_DoorDeliveryChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_OctroiFormCharges" Text="Octroi Form Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_OctroiFormChargesResource1"></asp:Label></td>
        <td style="width: 35%; ">
            <asp:TextBox ID="txt_OctroiFormCharges" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_OctroiFormChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%; ">
          <asp:Label ID="lbl_OctroiFormChargesUnit" runat="server" Text="Rs" meta:resourcekey="lbl_OctroiFormChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%; ">
            <asp:Label ID="lbl_OctroiServiceCharges" Text="Octroi Service Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_OctroiServiceChargesResource1"></asp:Label></td>
        <td style="width: 35%; ">
            <asp:TextBox ID="txt_OctroiServiceCharges" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_OctroiServiceChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%; ">
         <asp:Label ID="lbl_OctroiServiceChargesPerCent" runat="server" Text="%" meta:resourcekey="lbl_OctroiServiceChargesPerCentResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_GICharges" Text="GI Charges:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_GIChargesResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_GICharges" onkeypress="return Only_Numbers(this,event)" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_GIChargesResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
        <asp:Label ID="lbl_GIChargesUnit" runat="server" Text="Rs" meta:resourcekey="lbl_GIChargesUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_DemurrageDays" Text="Demurrage Days:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DemurrageDaysResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_DemurrageDays" runat="server" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_DemurrageDaysResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            <asp:Label ID="lbl_DemurrageRate"  Text="Demurrage Rate:" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DemurrageRateResource1"></asp:Label></td>
        <td style="width: 35%">
            <asp:TextBox ID="txt_DemurrageRate" runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_DemurrageRateResource1" Text="0"></asp:TextBox></td>
        <td style="width: 15%">
         <asp:Label ID="lbl_DemurrageRateUnit" runat="server" Text="Rs/Kg/Day" meta:resourcekey="lbl_DemurrageRateUnitResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width:50%">
            &nbsp;
        </td>
        <td style="width: 35%">
        </td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />&nbsp;
            <asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit" OnClick="btn_Exit_Click" meta:resourcekey="btn_ExitResource1"/>
            </td>
    </tr>
</table>
