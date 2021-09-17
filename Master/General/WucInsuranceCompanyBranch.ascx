<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucInsuranceCompanyBranch.ascx.cs"
    Inherits="Master_Vehicle_WucInsuranceCompanyBranch" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Master/General/InsuranceCompanyBranch.js"></script>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js" ></script>
<script language="javascript" type="text/javascript" src="../../JavaScript/ddlsearch.js" ></script>

<asp:ScriptManager ID="scm_InsuranceCompanyBranch" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6"> &nbsp;
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="INSURANCE COMPANY BRANCH" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%"><asp:Label ID="lbl_InsuranceCompnay" runat="Server" Text="Insurance Company :" meta:resourcekey="lbl_Insurance_Compnay_Resource1"></asp:Label> </td>
           
        <td style="width: 29%" >
            <asp:DropDownList ID="ddl_Insurance_Company_Branch_Name" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Insurance_CompanyBranch_Name_Resource1">
            </asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD" style="width: 29%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
       
            <td class="TD1" style="width: 20%;"><asp:Label ID="lbl_BranchName" runat="Server" Text="Branch Name :" meta:resourcekey="lbl_Branch_NameResource1"></asp:Label>  </td>
             
            <td style="width: 29%" >
                <asp:TextBox ID="txt_Branch_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                    MaxLength="25" Width="99%" meta:resourcekey="txt_Branch_Name_Resource1"></asp:TextBox></td>
            <td class="TDMANDATORY" style="width: 1%">
                *</td>
                            
            <td class="TD1" style="width: 20%;"><asp:Label ID="lbl_ContactPerson" runat="Server" Text="Contact Person :" meta:resourcekey="lbl_ContactPersonResource1"></asp:Label>  </td>
              
            <td style="width: 29%" >
                <asp:TextBox ID="txt_Contact_Person" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                    MaxLength="50" Width="99%" meta:resourcekey="txt_ContactPerson_Resource1"></asp:TextBox></td>
            <td class="TDMANDATORY" style="width: 1%">
                *</td>
       
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            <uc1:WucAddress ID="WucAddress1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 100%; text-align: center;" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"
                OnClientClick="return validateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_Insurance_Company_Branch" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />
