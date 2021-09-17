<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDriverInsuranceDependent.ascx.cs"
    Inherits="Master_Driver_WucDriverInsuranceDependent" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc1" %>
<link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<%--<script language="javascript" type="text/javascript" src="~/JavaScript/Common.js"></script>--%>

<script type="text/javascript">
function AllowToSaveWUCDriverInsuranceDependent()
{
var txt_Insu_Premium = document.getElementById('<%=txt_Insu_Premium.ClientID%>');
var txt_Sum_assured = document.getElementById('<%=txt_Sum_assured.ClientID%>');
var txt_Nominee_Name = document.getElementById('<%=txt_Nominee_Name.ClientID%>');
var txt_Policy_Number = document.getElementById('<%=txt_Policy_Number.ClientID%>');

var ddl_Insu_Company = document.getElementById('<%=ddl_Insu_Company.ClientID%>');
var ddl_Insurance_Branch = document.getElementById('<%=ddl_Insurance_Branch.ClientID%>');
var ddl_Insurance_Agent = document.getElementById('<%=ddl_Insurance_Agent.ClientID%>');
var ddl_Nominee_Relation = document.getElementById('<%=ddl_Nominee_Relation.ClientID%>');

var lbl_Errors = document.getElementById('<%=lbl_Errors.ClientID%>');
//var objResource=new Resource('<%=hdf_ResourecString.ClientID%>');


var Insu_Premium = parseFloat(txt_Insu_Premium.value);
if (isNaN(Insu_Premium)) Insu_Premium = 0;

var Sum_assured = parseFloat(txt_Sum_assured.value);
if (isNaN(Sum_assured)) Sum_assured = 0;

var ATS = false;

if (ddl_Insu_Company.value != "0" && ddl_Insurance_Branch.value == '0')
  {
  lbl_Errors.innerHTML = "Please Select Insurance Branch";//objResource.GetMsg("Msg_ddl_InsuranceBranch");
  ddl_Insurance_Branch.focus();
  }
else if (ddl_Insu_Company.value != "0" && txt_Policy_Number.value == '')
  {
  lbl_Errors.innerHTML = "Please Enter Policy Number";//objResource.GetMsg("Msg_Txt_PolicyNumber");
  txt_Policy_Number.focus();
  }
else if (ddl_Insu_Company.value != "0" && Insu_Premium <= 0)
  {
  lbl_Errors.innerHTML = "Please Enter Insurance Premium Greater Than Zero";//objResource.GetMsg("Msg_txt_InsurancePremium");
  txt_Insu_Premium.focus();
  }
else if (ddl_Insu_Company.value != "0" && Sum_assured <= 0)
  {
  lbl_Errors.innerHTML = "Please Enter Sum Assured Value Greater Than Zero";//objResource.GetMsg("Msg_txt_SumAssured");
  txt_Sum_assured.focus();
  }
else if (ddl_Insu_Company.value != "0" && txt_Nominee_Name.value == "")
  {
  lbl_Errors.innerHTML = "Please Enter Nominee Name";//objResource.GetMsg("Msg_txt_NomineeName");
  txt_Nominee_Name.focus();
  }
else if (ddl_Insu_Company.value != "0" && ddl_Nominee_Relation.value == "0")
  {
  lbl_Errors.innerHTML = "Please Select Nominee Relation";//objResource.GetMsg("Msg_ddl_NomineeRelation");
  ddl_Nominee_Relation.focus();
  }
else
  ATS = true;

return ATS;
}

function Hide_Control()
{
    var ddl_Insu_Company = document.getElementById('<%=ddl_Insu_Company.ClientID %>');
       
    var td_MDddlInsuBranch = document.getElementById('<%=td_MDddlInsuBranch.ClientID %>');
    var td_MDtxtPolicyNumber = document.getElementById('<%=td_MDtxtPolicyNumber.ClientID %>');
    var td_MDtxtInsuPremium = document.getElementById('<%=td_MDtxtInsuPremium.ClientID %>');
    var td_MDtxtSumAssured = document.getElementById('<%=td_MDtxtSumAssured.ClientID %>');
    var td_MDtxtNomineeName = document.getElementById('<%=td_MDtxtNomineeName.ClientID %>');
    var td_MDddlNomineeRelation = document.getElementById('<%=td_MDddlNomineeRelation.ClientID %>');
    
    
    if(ddl_Insu_Company.value == '0')
    {
    td_MDddlInsuBranch.style.visibility = 'hidden';
    td_MDtxtPolicyNumber.style.visibility = 'hidden';
    td_MDtxtInsuPremium.style.visibility = 'hidden';
    td_MDtxtSumAssured.style.visibility = 'hidden';
    td_MDtxtNomineeName.style.visibility = 'hidden';
    td_MDddlNomineeRelation.style.visibility = 'hidden';
    
    }
    else
    {
    td_MDddlInsuBranch.style.visibility = 'visible';
    td_MDtxtPolicyNumber.style.visibility = 'visible';
    td_MDtxtInsuPremium.style.visibility = 'visible';
    td_MDtxtSumAssured.style.visibility = 'visible';
    td_MDtxtNomineeName.style.visibility = 'visible';
    td_MDddlNomineeRelation.style.visibility = 'visible';
  
    
    }
    
    
}
</script>

<table class="TABLE">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr id="tr_InsuranceDeatils" class="HIDEGRIDCOL">
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnl_Dep_Details" runat="server" GroupingText="Insurance Details" Width="100%"
                            meta:resourcekey="pnl_Dep_DetailsResource1" CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_InsuranceCompany" Text="Insurance Company :" runat="server" meta:resourcekey="lbl_InsuranceCompanyResource1"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 34%">
                                        <asp:DropDownList ID="ddl_Insu_Company" runat="server" CssClass="DROPDOWN" Width="100%"
                                            AutoPostBack="True" meta:resourcekey="ddl_Insu_CompanyResource1" onchange="Hide_Control()"
                                            OnSelectedIndexChanged="ddl_Insu_Company_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="TDMANDATORY" style="width: 1%" runat="server" id="td_MDddlInsuComp">
                                    </td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_InsuranceBranch" runat="server" Text="Insurance Branch :" meta:resourcekey="lbl_InsuranceBranchResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_Insurance_Branch" runat="server" CssClass="DROPDOWN" Width="100%"
                                                    meta:resourcekey="ddl_Insurance_BranchResource1">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="TDMANDATORY" style="width: 1%" runat="server" id="td_MDddlInsuBranch">
                                        *
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_PloicyNumber" runat="server" Text="Policy No :" meta:resourcekey="lbl_PloicyNumberResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Policy_Number" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="50" meta:resourcekey="txt_Policy_NumberResource1" /></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="td_MDtxtPolicyNumber">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_InsuranceAgent" runat="server" Text="Insurance Agent:" meta:resourcekey="lbl_InsuranceAgentResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:DropDownList ID="ddl_Insurance_Agent" runat="server" CssClass="DROPDOWN" Width="100%"
                                            meta:resourcekey="ddl_Insurance_AgentResource1">
                                        </asp:DropDownList></td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_InsurancePremium" runat="server" Text="Insurance Premium :" meta:resourcekey="lbl_InsurancePremiumResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Insu_Premium" onkeypress="return Only_Integers(this,event)"
                                            runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="20" meta:resourcekey="txt_Insu_PremiumResource1"></asp:TextBox></td>
                                    <td class="TDMANDATORY" style="width: 1%" runat="server" id="td_MDtxtInsuPremium">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Sum_assured" runat="server" Text="Sum Assured :" meta:resourcekey="lbl_Sum_assuredResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Sum_assured" onkeypress="return Only_Integers(this,event)" runat="server"
                                            CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="20" meta:resourcekey="txt_Sum_assuredResource1" /></td>
                                    <td class="TDMANDATORY" style="width: 1%" runat="server" id="td_MDtxtSumAssured">
                                        *</td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_InsuranceExpiryDate" runat="server" Text="Insurance Expiry :"
                                            meta:resourcekey="lbl_InsuranceExpiryDateResource1" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <uc1:wuc_Date_Picker ID="Insurance_Expiry_Date" runat="server"></uc1:wuc_Date_Picker>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_NomineeName" runat="server" Text="Nominee Name :" meta:resourcekey="lbl_NomineeNameResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Nominee_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="100" meta:resourcekey="txt_Nominee_NameResource1" /></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="td_MDtxtNomineeName">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_NomineeRelation" runat="server" Text="Nominee Relation :" meta:resourcekey="lbl_NomineeRelationResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:DropDownList ID="ddl_Nominee_Relation" runat="server" CssClass="DROPDOWN" Width="100%"
                                            meta:resourcekey="ddl_Nominee_RelationResource1" /></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="td_MDddlNomineeRelation">
                                        *
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnl_Dependent_Details" runat="server" GroupingText="Dependent Details"
                            Width="100%" meta:resourcekey="pnl_Dependent_DetailsResource1" CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td style="width: 100%;" colspan="8">
                                        <%--<asp:UpdatePanel ID="upd_pnl_dg_Dep_Details" runat="server" >
                                            
                                            <ContentTemplate>--%>
                                        <%--<asp:UpdatePanel ID="upd_pnl_dg_Dep_Details" runat="server" UpdateMode="Always">
                                            <ContentTemplate>--%>
                                        <asp:DataGrid ID="dg_Dep_Details" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                                            CssClass="Grid" runat="server" OnCancelCommand="dg_Dep_Details_CancelCommand"
                                            OnEditCommand="dg_Dep_Details_EditCommand" OnItemCommand="dg_Dep_Details_ItemCommand"
                                            OnItemDataBound="dg_Dep_Details_ItemDataBound" OnUpdateCommand="dg_Dep_Details_UpdateCommand"
                                            OnDeleteCommand="dg_Dep_Details_DeleteCommand" meta:resourcekey="dg_Dep_DetailsResource1">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                                HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                                BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                            </HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="5%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Relation">
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddl_Relation" CssClass="DROPDOWN" runat="server" Width="99%"
                                                            meta:resourcekey="ddl_RelationResource1" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Family_Relation") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddl_Relation" CssClass="DROPDOWN" runat="server" Width="99%"
                                                            meta:resourcekey="ddl_RelationResource2" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Dependent Name">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_Dep_Name_Add" CssClass="TEXTBOX" MaxLength="100" runat="server"
                                                            BorderWidth="1px" meta:resourcekey="txt_Dep_Name_AddResource1" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Dependent_Name") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_Dep_Name_Edit" CssClass="TEXTBOX" runat="server" MaxLength="100"
                                                            BorderWidth="1px" meta:resourcekey="txt_Dep_Name_EditResource1" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30%" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Age">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_Age_Add" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS"
                                                            MaxLength="3" runat="server" BorderWidth="1px" meta:resourcekey="txt_Age_AddResource1" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Age") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_Age_Edit" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS"
                                                            runat="server" MaxLength="3" BorderWidth="1px" meta:resourcekey="txt_Age_EditResource1" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Mobile No.">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_MobileNo_Add" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                                                            MaxLength="10" runat="server" BorderWidth="1px"  />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "MobileNo") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_MobileNo_Edit" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                                                            runat="server" MaxLength="10" BorderWidth="1px"  />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Staying At">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_StatyingAt_Add"  CssClass="TEXTBOX"
                                                            MaxLength="20" runat="server" BorderWidth="1px"  />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "StayingAt") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_StayingAt_Edit"  CssClass="TEXTBOX"
                                                            runat="server" MaxLength="20" BorderWidth="1px"  />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30%" />
                                                </asp:TemplateColumn>
                                                
                                                <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                                                    meta:resourcekey="EditCommandColumnResource1">
                                                    <HeaderStyle Width="10%" />
                                                </asp:EditCommandColumn>
                                                <asp:TemplateColumn HeaderText="Delete">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                                            meta:resourcekey="lbtn_DeleteResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                        <%--</ContentTemplate>
                                                
                                                </asp:UpdatePanel>--%>
                                        <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <%--<asp:UpdatePanel ID="upd_pnl_Error" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_Dep_Details" />
          </Triggers>
          <ContentTemplate>--%>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                meta:resourcekey="lbl_ErrorsResource1" />
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
            <%-- <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                meta:resourcekey="lbl_ErrorsResource1" />--%>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />

<script type="text/javascript">
Hide_Control();

</script>

