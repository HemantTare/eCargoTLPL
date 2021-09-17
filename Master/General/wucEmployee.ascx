<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucEmployee.ascx.cs" Inherits="EC_Master_wucEmployee" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc2" %>
<script src="../../Javascript/Common.js" type="text/javascript"></script>

<script language="javascript" src="../../Javascript/Master/General/Employee.js" type="text/javascript"></script>


<script type="text/javascript">

function EnableDisableTab(rdb_payroll)
{

if(rdb_payroll.value == "rdb_Consider_Payroll_Yes")
 {
     wucEmployee1_TabStrip1.Tabs('one').SetProperty('Visible',true); 
    wucEmployee1_TabStrip1.Render();

 }
 else if(rdb_payroll.value == "rdb_Consider_Payroll_No")
 {
     wucEmployee1_TabStrip1.Tabs('one').SetProperty('Visible',false); 
    wucEmployee1_TabStrip1.Render();

 }
 else
 {
    wucEmployee1_TabStrip1.Tabs('one').SetProperty('Visible',true); 
    wucEmployee1_TabStrip1.Render();
 }
    

}
</script>
 <!-- TABSTRIP ADDED FOR PAYROLL IN 07-03-09 -->
<asp:ScriptManager ID="SM_Employee" runat="server" />
<ComponentArt:TabStrip id="TabStrip1"
          SiteMapXmlFile="~/XML/tabEmployeeData_Add.xml"
          EnableViewState="False" 
          MultiPageId="MultiPage1"
          runat="server" meta:resourcekey="TabStrip1Resource1">
    </ComponentArt:TabStrip>
      <ComponentArt:MultiPage ID="MultiPage1" CssClass="MultiPage" runat="server"  Style="left: 0px;
    top: 0px; right: 0px; width:100% " meta:resourcekey="MultiPage1Resource1" SelectedIndex="0">
   <ComponentArt:PageView runat="server">
<table class="TABLE" border="0" width="100%">
    <tr>
        <td colspan="6" class="TDGRADIENT" style="width: 100%">
            &nbsp;
            <asp:Label ID="lbl_Employee_Heading" runat="server" Text="EMPLOYEE" CssClass="HEADINGLABEL"
                meta:resourcekey="lbl_Employee_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="left" style="width: 100%">
            <uc1:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server"></uc1:WucHierarchyWithID>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_EmployeeCode" CssClass="LABEL" Text="Employee Code :" runat="server"
                meta:resourcekey="lbl_EmployeeCodeResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_EmployeeCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="10" meta:resourcekey="txt_EmployeeCodeResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
         <asp:Label ID="lbl_MandatoryEmployeeCode" runat="server" Text="*" meta:resourcekey="lbl_MandatoryEmployeeCodeResource1" ></asp:Label>
        </td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FirstName" CssClass="LABEL" Text="First Name :" runat="server"
                meta:resourcekey="lbl_FirstNameResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_FirstName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" meta:resourcekey="txt_FirstNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_MiddleName" CssClass="LABEL" Text="Middle Name :" runat="server"
                meta:resourcekey="lbl_MiddleNameResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_MiddleName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" meta:resourcekey="txt_MiddleNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_MiddleName" runat="server" Text="*" meta:resourcekey="lbl_mandatory_MiddleNameResource1"></asp:Label>
        </td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_LastName" CssClass="LABEL" Text="Last Name :" runat="server" meta:resourcekey="lbl_LastNameResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_LastName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" meta:resourcekey="txt_LastNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Department" CssClass="LABEL" Text="Department :" runat="server"
                meta:resourcekey="lbl_DepartmentResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_Department" runat="server" Width="100%" CssClass="DROPDOWN"
                meta:resourcekey="ddl_DepartmentResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_UserProfile" CssClass="LABEL" Text="User Profile :" runat="server"
                meta:resourcekey="lbl_UserProfileResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:UpdatePanel ID="upd_UserProfile" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_UserProfile" runat="server" Width="100%" CssClass="DROPDOWN"
                        meta:resourcekey="ddl_UserProfileResource1" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Qualification" CssClass="LABEL" Text="Qualification :" runat="server"
                meta:resourcekey="lbl_QualificationResource1"></asp:Label>
        </td>
        <td style="width: 79%" colspan="4">
            <asp:TextBox ID="txt_Qualification" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50" meta:resourcekey="txt_QualificationResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Email" CssClass="LABEL" Text="Email :" runat="server" meta:resourcekey="lbl_EmailResource1"></asp:Label>
        </td>
        <td style="width: 79%" colspan="4">
            <asp:TextBox ID="txt_Email" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100"
                meta:resourcekey="txt_EmailResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_Mandatory_Email" runat="server" Text="*" meta:resourcekey="lbl_Mandatory_EmailResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_PreviousJobProfile" CssClass="LABEL" Text="Previous Job Profile :"
                runat="server" meta:resourcekey="lbl_PreviousJobProfileResource1"></asp:Label>
        </td>
        <td style="width: 79%" colspan="4">
            <asp:TextBox ID="txt_PreviousJobProfile" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="250" meta:resourcekey="txt_PreviousJobProfileResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_PreviousJobProfile" runat="server" Text="*" meta:resourcekey="lbl_mandatory_PreviousJobProfileResource1"></asp:Label>
        </td>
    </tr>
    <tr id="TR_ISeCargoUser" runat="server">
        <td class="TD1" style="width: 20%" runat="server">
            <asp:Label ID="lbl_IseCargoUser" CssClass="LABEL" Text="Is eCargo User :" runat="server"
                meta:resourcekey="lbl_IseCargoUserResource1"></asp:Label>
        </td>
        <td style="width: 29%" runat="server">
            <asp:CheckBox ID="chk_IseCargoUser" runat="server" meta:resourcekey="chk_IseCargoUserResource1" />
        </td>
        <td style="width: 1%;" runat="server">
            &nbsp;</td>
        <td style="width: 50%;" colspan="3" runat="server">
            &nbsp;</td>
    </tr>
    <tr id="TR_IsActive" runat="server">
        <td class="TD1" style="width: 20%" runat="server">
         <!--added Ankit 02-02-09 5.30 pm -->
            <asp:Label ID="lbl_IsActive" runat="server" Text="Is Active :" meta:resourcekey="lbl_IsActiveResource1"></asp:Label></td>
        <td style="width: 29%" runat="server">
            <asp:CheckBox ID="chk_IsActive" runat="server" Text=" " meta:resourcekey="chk_IsActiveResource1" /></td>
        <td style="width: 1%" runat="server">
        </td>
        <td class="TD1" style="width: 20%" runat="server">
            <!--added Anita 05-03-09 -->
            <asp:Label ID="lbl_Is_Account_Locked" runat="server" Text="Lock Account:" Visible="False"></asp:Label></td>
        <td style="width: 29%" runat="server">
            <asp:CheckBox ID="chk_Is_Account_Locked" runat="server" Text=" "  Visible="False"/></td>
        <td style="width: 1%" runat="server">
        </td>
        
    </tr>
    <tr>
             <!--added  06-03-09 FOR Payroll -->
        <td class="TD1" style="width: 20%">
            Consider For Payroll:</td>
        <td style="width: 29%">
            <asp:RadioButton ID="rdb_Consider_Payroll_Yes" runat="server" Checked="True" Text="Yes" GroupName="Payroll" onclick="EnableDisableTab(this)"/>
            <asp:RadioButton ID="rdb_Consider_Payroll_No" runat="server" Text="No" GroupName="Payroll"  onclick="EnableDisableTab(this)"/></td>
        <td style="width: 1%">
        </td>
        <td style="width: 50%;" colspan="3">
            </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr id="tr_Divisions" runat="server">
        <td colspan="6" runat="server">
            <asp:Panel ID="pnl_Applicable_For_Following_Divisions" runat="server" GroupingText="Applicable For Following Divisions"
                meta:resourcekey="pnl_Applicable_For_Following_DivisionsResource1">
                <table width="100%">
                    <tr>
                        <td style="width: 20%" class="TD1">
                        </td>
                        <td style="width: 30%" colspan="2">
                            <asp:UpdatePanel ID="upd_Division" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="ChkLst_Division" CellSpacing="5" BorderWidth="0px" CssClass="CHECKBOXLIST"
                                        runat="server" meta:resourcekey="ChkLst_DivisionResource1" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 50%;" colspan="3">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
     </table>
   </ComponentArt:PageView>
   <ComponentArt:PageView runat="server">
        <!-- Add content for page 2 here -->
  <table style="width: 100%" class="TABLE">
        <tr >
            <td class="TDGRADIENT" colspan="6" style="width: 100%" >
            &nbsp;<asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="EMPLOYEE DETAILS FOR PAYROLL "></asp:Label></td>
        </tr>
        <tr>
             <td colspan="6" style="width: 100%">
                    &nbsp;
              </td>
        </tr>
        <tr>
                <td class="TD1" style="width: 20%">
                    Birth Date :</td>
                <td style="width: 29%">
                    <uc2:wuc_Date_Picker ID="birth_date" runat="server" />
                </td>
                <td style="width: 1%" colspan="4">
                    <asp:HiddenField ID="Hidden_System_Date" runat="server" />
                </td>
         </tr>
         <tr>
                <td class="TD1" style="width: 20%">
                    Date Of Joinning :</td>
                <td style="width: 29%">
                     <uc2:wuc_Date_Picker ID="date_Joinning" runat="server" />
                </td>
                <td style="width: 1%">
                </td>
              <td style="width: 50%;" colspan="3">
              &nbsp;</td>
          </tr>
          <tr>
                <td class="TD1" style="width: 20%">
                    Basic Salary :</td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_BasicRate" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                        MaxLength="9"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                        FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_BasicRate" BehaviorID="ctl00_FilteredTextBoxExtender4" Enabled="True">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
             
                 <td class="TD1" style="width: 20%">
                    Weekly Off :</td>
                <td style="width: 29%">
                    <asp:DropDownList ID="ddl_Weekly_off" runat="server" CssClass="DROPDOWN">
                        <asp:ListItem Selected="True" Value="0">Sunday</asp:ListItem>
                        <asp:ListItem Value="1">Monday</asp:ListItem>
                        <asp:ListItem Value="2">Tuesday</asp:ListItem>
                        <asp:ListItem Value="3">Wednesday</asp:ListItem>
                        <asp:ListItem Value="4">Thursday</asp:ListItem>
                        <asp:ListItem Value="5">Friday</asp:ListItem>
                        <asp:ListItem Value="6">Saturday</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 1%" >
                </td>
        </tr>
        <tr>
             <td class="TD1" style="width: 20%">
                    Probation Period :</td>
                <td style="width: 29%; font-weight: bold;">
                    &nbsp;<asp:TextBox ID="txt_ProbationPeriod" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                        MaxLength="3" Width="20%"></asp:TextBox>&nbsp; (In Months)
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                        FilterType="Numbers" TargetControlID="txt_ProbationPeriod" BehaviorID="ctl00_FilteredTextBoxExtender9" Enabled="True">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 1%" >
                </td>
                <td class="TD1" style="width: 20%">
                    <asp:CheckBox ID="chk_Confirmation" runat="server" Text="Confirmation Date :" onclick="CheckConfirmation(this)"
                        TextAlign="Left" />
                </td>
                <td style="width: 29%" id="tbl_Confirmation" runat="server">
                    <uc2:wuc_Date_Picker ID="Confirmation_date" runat="server" />
                </td>
                <td style="width: 1%">
                </td>
         </tr>
        
        <tr>
             <td colspan="6">
                    &nbsp;
              </td>
        </tr>
        
        <!--  <tr>
                <td colspan="2">
                    <input id="btn_Previous1" type="button" value="<< Previous" class="BUTTON" onclick="Wuc_Add_Employee1_TabStrip1.SelectTabById('zero');" /></td>
                <td style="width: 1%">
                </td>
                <td style="width: 20%" colspan="3">
                </td>
               <td colspan="2" style="text-align: right">
                    <input id="btn_Next2" type="button" value="Next >>" class="BUTTON" onclick="Wuc_Pay_Master_Employee1_TabStrip1.SelectTabById('two');" /></td>
            </tr>-->
        <tr>
             <td colspan="6">
                    &nbsp;
              </td>
        </tr>
    </table>
</ComponentArt:PageView>  
  
</ComponentArt:MultiPage>
 <table  width="100%"  class="TABLE">
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABELERROR"
                meta:resourcekey="lbl_ErrorsResource1" Text="Fields With * Mark Are Mandatory"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return Allow_to_Save();"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource2" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
            <asp:HiddenField ID="hdn_Area_Id" runat="server" />
            <asp:HiddenField ID="hdn_Region_Id" runat="server" />
            <asp:HiddenField ID="hdn_Is_HO" runat="server" />
        </td>
    </tr>
</table>
 <script type="text/javascript" >
 
 CheckConfirmation(this);

 </script>
