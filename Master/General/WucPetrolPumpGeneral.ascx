<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPetrolPumpGeneral.ascx.cs"
    Inherits="EC_Master_WucPetrolPumpGeneral" %>
<%@ Register Src="../../CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />


<script type="text/javascript" src ="../../Javascript/Common.js" ></script>
<script type="text/javascript"  src ="../../Javascript/ddlsearch.js">
</script>
 

<table class="TABLE">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="text-align: center">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 100%" align="center">
            <asp:Panel ID="Panel1" runat="server" GroupingText="Petrol Pump Details" meta:resourcekey="Panel1Resource1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="6">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 19%" class="TD1" align="left">
                            <asp:Label ID="lbl_PetrolPumpName"   CssClass="LABEL" runat="server" Text="Petrol Pump Name :" meta:resourcekey="lbl_PetrolPumpNameResource1"></asp:Label>
                        </td>
                        <td colspan="4" align="left" class="TDMANDATORY">
                            <asp:TextBox ID="txt_PetrolPumpName" runat="server" Width="570px" BorderWidth="1px"
                                CssClass="TEXTBOX" MaxLength="100" meta:resourcekey="txt_PetrolPumpNameResource1"></asp:TextBox>
                            *</td>
                         <td class="TDMANDATORY" style="width: 1%"></td>
                    </tr>
                <tr>
                    <td style="width: 19%" class="TD1" align="left">
                        <asp:Label ID="lbl_MailingName"   CssClass="LABEL" runat="server" Text="Mailing Name :" meta:resourcekey="lbl_MailingNameResource1"></asp:Label>
                    </td>
                    <td colspan="4" align="left" class="TDMANDATORY">
                        <asp:TextBox ID="txt_MailingName" Width="570px" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" meta:resourcekey="txt_MailingNameResource1"></asp:TextBox>
                        *</td>
                     <td class="TDMANDATORY" style="width: 1%"></td>
                </tr>
                <tr>
                    <td style="width: 19%" class="TD1" align="left">
                        <asp:Label ID="lbl_PetrolCompany"   CssClass="LABEL" runat="server" Text="Petrol Company :" meta:resourcekey="lbl_PetrolCompanyResource1"></asp:Label>
                    </td>
                    <td colspan="4" align="left" class="TDMANDATORY">
                        <asp:TextBox ID="txt_PetrolCompany" Width="570px" runat="server" BorderWidth="1px"
                            CssClass="TEXTBOX" MaxLength="100" meta:resourcekey="txt_PetrolCompanyResource1"></asp:TextBox>
                        *</td>
                    <td class="TDMANDATORY" style="width: 1%"></td>
                </tr>
                <tr>
                    <td style="width: 19%" class="TD1" align="left">
                        <asp:Label ID="lbl_ContactPerson"   CssClass="LABEL" runat="server" Text="Contact Person :" meta:resourcekey="lbl_ContactPersonResource1"></asp:Label>
                    </td>
                    <td colspan="4" align="left" class="TDMANDATORY">
                        <asp:TextBox ID="txt_ContactPerson" Width="570px" runat="server" BorderWidth="1px"
                            CssClass="TEXTBOX" MaxLength="100" meta:resourcekey="txt_ContactPersonResource1"></asp:TextBox>
                        *</td>
                             <td class="TDMANDATORY" style="width: 1%"></td>
                   
                </tr>
                <tr>
                    <td class="TD1" colspan="6">
                        <uc1:wucaddress id="WucAddress1" runat="server"></uc1:wucaddress>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 116px;" align="center">
            <table class="TABLE" id="TABLE1">
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td class="TD1" align="left" style="width: 29%; height: 15px;">
                        <asp:Label Width="99%"   CssClass="LABEL" Style="text-align: right" ID="lbl_RegistrationHead" runat="server"
                            Text="Registration Head" meta:resourcekey="lbl_RegistrationHeadResource1"></asp:Label>
                    </td>
                    <td style="width: 20%; height: 15px;" class="TD1">
                        <asp:Label Width="98%"   CssClass="LABEL" Style="text-align: left" ID="lbl_RegistrationNumber" runat="server"
                            Text="Registration Number" meta:resourcekey="lbl_RegistrationNumberResource1"></asp:Label>
                    </td>
                    <td style="width: 29%; height: 15px;" align="left">
                    </td>
                    <td style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td class="TD1" align="left" style="width: 29%">
                        <asp:Label ID="lbl_CSTNo"   CssClass="LABEL" Style="text-align: right" runat="server" Width="99%" Text="CST No :" meta:resourcekey="lbl_CSTNoResource1"></asp:Label>
                    </td>
                    <td style="width: 20%; height: 21px;" class="TD1">
                        <asp:TextBox ID="txt_CSTNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Width="99%"
                            MaxLength="50" meta:resourcekey="txt_CSTNoResource1"></asp:TextBox>
                    </td>
                       <td class="TDMANDATORY" style="width: 1%" align="left">
                           &nbsp;<asp:Label ID="lbl_Mandatory_CSTNo" runat="server" Text="*"></asp:Label></td>
                    <td style="width: 29%" align="left">
                    </td>
                    <%--<td style="width: 1%">
                    </td>--%>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td class="TD1" align="left" style="width: 29%">
                        <asp:Label ID="lbl_TINNo"   CssClass="LABEL" Style="text-align: right" Width="99%" runat="server" Text="TIN No :" meta:resourcekey="lbl_TINNoResource1"></asp:Label>
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:TextBox ID="txt_TINNo" runat="server" Width="99%" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="50" meta:resourcekey="txt_TINNoResource1"></asp:TextBox>
                    </td>
                       <td class="TDMANDATORY" style="width: 1%" align="left">
                           &nbsp;<asp:Label ID="lbl_Mandatory_TINNo" runat="server" Text="*"></asp:Label></td>
                    <td style="width: 29%" align="left">
                    </td>
                    <%--<td style="width: 1%">
                    </td>--%>
                </tr>
            </table>
        </td>
    </tr>
    
    
     <tr>
        <td colspan="9" style="text-align: left">
           <%-- <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <contenttemplate> --%>
                  <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" 
                  Font-Bold="True" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
               <%--</contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="Btn_Save" />
                </triggers>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
    
 
    <tr>
        <td colspan="6" style="text-align: center">
            &nbsp;</td>
    </tr>
    
</table>
 
