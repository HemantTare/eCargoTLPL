<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDriverDetails.ascx.cs"
    Inherits="Master_Driver_WucDriverDetails" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Master/Driver/Driver.js"></script>

<script type="text/javascript">
function AllowToSaveWUCDriverDetails()
{

var txt_Driver_Code = document.getElementById('<%=txt_Driver_Code.ClientID%>');
var Txt_Driver_Name = document.getElementById('<%=Txt_Driver_Name.ClientID%>');
var txt_Qualification = document.getElementById('<%=txt_Qualification.ClientID%>');
var txt_License_No = document.getElementById('<%=txt_License_No.ClientID%>');
var txt_Native_Address_Line1 = document.getElementById('<%=txt_Native_Address_Line1.ClientID%>');
var txt_Contact_No = document.getElementById('<%=txt_Contact_No.ClientID%>');
var txt_Refence_Name = document.getElementById('<%=txt_Refence_Name.ClientID%>');
var txt_Refence_Phone = document.getElementById('<%=txt_Refence_Phone.ClientID%>');
var txt_Refence_Mobile = document.getElementById('<%=txt_Refence_Mobile.ClientID%>');
var txt_Opening_Balance = document.getElementById('<%=txt_Opening_Balance.ClientID%>');

var ddl_Driver_Category = document.getElementById('<%=ddl_Driver_Category.ClientID%>');
var ddl_Religion = document.getElementById('<%=ddl_Religion.ClientID%>');
var ddl_License_Issue_City = document.getElementById('<%=ddl_License_Issue_City.ClientID%>');
var ddl_License_Category = document.getElementById('<%=ddl_License_Category.ClientID%>');
var lbl_Errors = document.getElementById('<%=lbl_Errors.ClientID%>');
var Hdn_Driver_Type_ID=document.getElementById('<%=Hdn_Driver_Type_ID.ClientID%>');
var ddl_Name=document.getElementById('WucDriver1_WucDriverDetails1_WucAddress1_ddl_City_txtBoxddl_City');
var Chk_IsLicenseAuthencticated=document.getElementById('WucDriver1_WucDriverDetails1_Chk_IsLicenseAuthencticated');
var txt_LicenseAuthenticatedBy=document.getElementById('WucDriver1_WucDriverDetails1_txt_LicenseAuthenticatedBy');


if(val(Hdn_Driver_Type_ID.value) == 1)
{
 var Opening_Balance = parseFloat(txt_Opening_Balance.value);
 if (isNaN(Opening_Balance)) Opening_Balance = 0;
}

var ATS = false;

  if (val(Hdn_Driver_Type_ID.value) > 0 && txt_Driver_Code.value == '' && control_is_mandatory(txt_Driver_Code) == true)
  {
      lbl_Errors.innerHTML = "Please Enter Driver Code";
      txt_Driver_Code.focus();
  }
  else if (val(Hdn_Driver_Type_ID.value) == 0 && txt_Driver_Code.value == '' && control_is_mandatory(txt_Driver_Code) == true)
  {
      lbl_Errors.innerHTML = "Please Enter Cleaner Code";
      txt_Driver_Code.focus();
  }
  else if (val(Hdn_Driver_Type_ID.value) > 0 && Txt_Driver_Name.value == '')
  {
    lbl_Errors.innerHTML = "Please Enter Driver Name";
    Txt_Driver_Name.focus();
  }
  else if (val(Hdn_Driver_Type_ID.value) == 0 && Txt_Driver_Name.value == '')
  {
    lbl_Errors.innerHTML = "Please Enter Cleaner Name";
    Txt_Driver_Name.focus();
  }
  else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
  else if(val(Hdn_Driver_Type_ID.value) == 1)
  {
    if (ddl_Driver_Category.value == '0'  && control_is_mandatory(ddl_Driver_Category) == true)
    {
        lbl_Errors.innerHTML = "Please Select Driver Category";
        ddl_Driver_Category.focus();
    }
    else if (ddl_Religion.value == '0' && control_is_mandatory(ddl_Religion) == true)
    {     
        lbl_Errors.innerHTML = "Please Select Driver Religion";
        ddl_Religion.focus();
    }      
    else if (txt_Qualification.value == '' && control_is_mandatory(txt_Qualification) == true)
    {
        lbl_Errors.innerHTML = "Please Enter Driver Qualification";
        txt_Qualification.focus();
    }
    else if (txt_License_No.value == '' && control_is_mandatory(txt_License_No) == true)
    {
        lbl_Errors.innerHTML = "Please Enter Driver License Number";
        txt_License_No.focus();
    }
    
    else if (Chk_IsLicenseAuthencticated.checked == true && txt_LicenseAuthenticatedBy.value == '')
    {
        lbl_Errors.innerHTML="Please Enter License Authenticated By";
        txt_LicenseAuthenticatedBy.focus();
    }
    else if (ddl_License_Issue_City.value == '0' && control_is_mandatory(ddl_License_Issue_City) == true)
    {
        lbl_Errors.innerHTML = "Please Select License Issue City";
        ddl_License_Issue_City.focus();
    }  
    else if (ddl_License_Category.value == '0' && control_is_mandatory(ddl_License_Category) == true)
    {
        lbl_Errors.innerHTML = "Please Select License Category";
        ddl_License_Category.focus();
    } 
    else if (txt_Refence_Name.value == '' && control_is_mandatory(txt_Refence_Name) == true)
    {
        lbl_Errors.innerHTML = "Please Enter Reference Name";
        txt_Refence_Name.focus();
    }
    else if (txt_Refence_Phone.value == '' && control_is_mandatory(txt_Refence_Phone) == true)
    {
        lbl_Errors.innerHTML = "Please Enter Reference Phone Number";
        txt_Refence_Phone.focus();
    }
    else if (Opening_Balance < 0)
    {
        lbl_Errors.innerHTML = "Please Enter Opening Balance Greater Than Zero";
        txt_Opening_Balance.focus();
    }
    else 
    {
        ATS=true;
    }
 }
 else
  ATS = true;
 
 return ATS; 
}

function ValidateLicenseNo(obj, State_Code, DriverTypeID) 
{
    
    var LicenseVal = obj.value;
    var StateCode = State_Code;
    var Driver_Type_ID = DriverTypeID;
    
    var regLiccenseNo = /^([a-zA-Z]){2}([0-9]){13}?$/;

    var regLiccenseNothird13val = /^([0-9]){13}?$/;
    
    if (val(Driver_Type_ID) == 1)
    {
        if (LicenseVal.trim() != '') 
        {
              var first2val = '';
              var third13val = '';
              
            if(LicenseVal.length == 15) 
            {
              first2val = LicenseVal.substr(0, 2);
              third13val = LicenseVal.substr(2, 13);
            }
            
            if (LicenseVal.length < 15) 
            {
                alert('Please Enter 15 License No.');
    //            obj.focus();
                return false;
            }
            else if (first2val != StateCode) 
            {
                alert('First 2 digits must be State Code : '+ first2val);
  //              obj.focus();
                return false;
            }
            else if (!regLiccenseNothird13val.test(third13val)) 
            {
                alert('From 3rd digit to 15th digit must be numbers :' + third13val);
//                obj.focus();
                return false;
            }
            else if (!regLiccenseNo.test(LicenseVal)) 
            {
                alert('Invalid License No.');
                return false;
            }
            else
                return true;
        }
        else 
        {
            alert('Please Enter 15 digit License No.');
            return false;
        }
    }
    else
    {
        return true;    
    }
    
    return true;    
}




</script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<table class="TABLE">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" GroupingText="Personal Details" meta:resourcekey="Panel1Resource1"
                            CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_DriverCode" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DriverCodeResource2"></asp:Label>
                                    </td>
                                    <td style="width: 34%" class="TDMANDATORY">
                                        <asp:TextBox ID="txt_Driver_Code" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="10" meta:resourcekey="txt_Driver_CodeResource1" Width="220px"></asp:TextBox>
                                        <asp:Label ID="lbl_Mandatory_DriverCode" runat="server" Text="*"></asp:Label></td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                    </td>
                                    <asp:HiddenField ID="hdn_DriverImageName" runat="server" />
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_DriverName" runat="server" CssClass="LABEL" Text=" " meta:resourcekey="lbl_DriverNameResource2"></asp:Label></td>
                                    <td style="width: 34%" class="TDMANDATORY">
                                        <asp:TextBox ID="Txt_Driver_Name" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="50" meta:resourcekey="Txt_Driver_NameResource1" Width="220px"></asp:TextBox>
                                        *</td>
                                    <td style="width: 1%;" runat="server" class="TDMANDATORY" id="td_DriverNameMand">
                                    </td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_NickName" runat="server" Text="Nick Name :" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_NickName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="20" Width="220px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="Td1">
                                        <asp:Label ID="lbl_NikcNameMand" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr id="TR_DriverCategory" runat="server">
                                    <td class="TD1" style="width: 15%" runat="server">
                                        <asp:Label ID="lbl_DriverCategory" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%" runat="server">
                                        <asp:DropDownList ID="ddl_Driver_Category" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Driver_CategoryResource1" /></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%" runat="server">
                                        <asp:Label ID="lbl_IsReliable" runat="server" Text="Is Reliable ?" meta:resourcekey="lbl_IsReliable_Resource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 29%" runat="server">
                                        <asp:CheckBox ID="chk_Is_reliable" runat="server" meta:resourcekey="chk_Is_reliableResource1" /></td>
                                    <td style="width: 1%;" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_BirthDate" runat="server" Text="Birth Date :" meta:resourcekey="lbl_BirthDateResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <uc1:wuc_Date_Picker ID="picker_BirthDate" runat="server" />
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="TD_Mandatory_BirthDate" runat="server">
                                        <asp:Label ID="lbl_BirthDate_Mandatory" runat="server" Text="*" meta:resourcekey="lbl_BirthDate_MandatoryResource1"></asp:Label></td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Religion" runat="server" Text="Religion :" meta:resourcekey="lbl_ReligionResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:DropDownList ID="ddl_Religion" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ReligionResource1" /></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="Td_Mandatory_Religion">
                                        <asp:Label ID="lbl_Religion_Mandatory" runat="server" Text="*" meta:resourcekey="lbl_Religion_MandatoryResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Qualification" runat="server" Text="Qualification :" meta:resourcekey="lbl_QualificationResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Qualification" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="100" meta:resourcekey="txt_QualificationResource1" Width="250px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="Td_Mandatory_Qualification">
                                        <asp:Label ID="lbl_Qualification_Mandatory" runat="server" Text="*" meta:resourcekey="lbl_Qualification_MandatoryResource1"></asp:Label></td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_IsMarried" runat="server" Text="Is Married ?" meta:resourcekey="lbl_IsMarried_Resource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 29%">
                                        <asp:CheckBox ID="chk_Is_Married" runat="server" meta:resourcekey="chk_Is_MarriedResource1" /></td>
                                    <td style="width: 1%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%" id="TD_lbl_LicenseIssueCity" runat="server">
                                        <asp:Label ID="lbl_LicenseIssueCity" runat="server" Text="License Issue City :" meta:resourcekey="lbl_LicenseIssueCityResource1"
                                            CssClass="LABEL"></asp:Label>
                                        <asp:Label ID="lbl_LicenseIssueState" runat="server" Text="License Issue State :"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 34%" id="TD_ddl_LicenseIssueCity" runat="server">
                                        <asp:DropDownList ID="ddl_License_Issue_City" runat="server" CssClass="DROPDOWN"
                                            meta:resourcekey="ddl_License_Issue_CityResource1" />
                                        <asp:DropDownList ID="ddl_License_Issue_State" runat="server" AutoPostBack="true"
                                            CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_License_Issue_State_SelectedIndexChanged" />
                                        <asp:HiddenField ID="hdn_License_Issue_State_Code"  runat="server" />
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_LicenseIssueCity" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_LicenseNo" runat="server" Text="License No :" meta:resourcekey="lbl_LicenseNoResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_License_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onblur="ValidateLicenseNo(this,document.getElementById('WucDriver1_WucDriverDetails1_hdn_License_Issue_State_Code').value, document.getElementById('WucDriver1_WucDriverDetails1_Hdn_Driver_Type_ID').value);"
                                            MaxLength="50" Width="250px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="TD_Mandatory_License_No" runat="server">
                                        *</td>
                                </tr>
                                <tr id="tr_LicenseAuthentication" runat="server">
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_IsLicenseAuthenticated" runat="server" Text="Is License Authenticated? :"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:CheckBox ID="Chk_IsLicenseAuthencticated" CssClass="CHECKBOX" runat="server"
                                            onclick="EnableDisableLicenseAuthenticatedBy();" /></td>
                                    <td style="width: 1%;">
                                    </td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_LicenseAuthenticatedBy" runat="server" Text="License Authenticated By :"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_LicenseAuthenticatedBy" runat="server" CssClass="TEXTBOX" Width="250px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                        <asp:Label ID="lbl_Mandatory_LicenseAuthenticatedby" runat="server" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_LicenseExpiryDate" runat="server" Text="License Expiry Date :"
                                            meta:resourcekey="lbl_LicenseExpiryDateResource1" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <uc1:wuc_Date_Picker ID="picker_License_Expiry_Date" runat="server" />
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_License_Expiry_Date"
                                        runat="server">
                                        <asp:Label ID="lbl_Mandatory_LicenseExpDate" runat="server" Text="*"></asp:Label></td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_LicenseCategory" runat="server" Text="License Category :" meta:resourcekey="lbl_LicenseCategoryResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:DropDownList ID="ddl_License_Category" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_License_CategoryResource1" /></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="Td_Mandatory_License_Category">
                                        <asp:Label ID="lbl_Mandatory_LicenseCategory" runat="server" Text="*"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_BloodGroup" runat="server" CssClass="LABEL" Text="Blood Group"
                                            meta:resourcekey="lbl_BloodGroupResource1"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_BloodGroup" runat="server" CssClass="TEXTBOX" Width="250px"
                                            meta:resourcekey="txt_BloodGroupResource1"></asp:TextBox></td>
                                    <td runat="server" class="TDMANDATORY" style="width: 1%" id="td_BloodGroupMand">
                                    </td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_AadharNo" runat="server" CssClass="LABEL" Text="Aadhar No."></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_AadharNo" runat="server" CssClass="TEXTBOX" Width="250px" MaxLength="12"
                                            onkeypress="return Only_Integers(this,event)"></asp:TextBox></td>
                                    <td runat="server" class="TDMANDATORY" style="width: 1%" id="td_AadharNoMand">
                                        <asp:Label ID="Label1" runat="server" Text="*"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_MobileNo1" runat="server" CssClass="LABEL" Text="Mobile No. 1"></asp:Label></td>
                                    <td style="width: 34%" class="TDMANDATORY">
                                        <asp:TextBox ID="txt_MobileNo1" runat="server" CssClass="TEXTBOX" Width="250px" MaxLength="10"
                                            onkeypress="return Only_Integers(this,event)"></asp:TextBox>*</td>
                                    <td id="td_MobileNo1Mand" runat="server" class="TDMANDATORY" style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_MobileNo2" runat="server" CssClass="LABEL" Text="Mobile No. 2"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_MobileNo2" runat="server" CssClass="TEXTBOX" Width="250px" MaxLength="10"
                                            onkeypress="return Only_Integers(this,event)"></asp:TextBox></td>
                                    <td id="Td5" runat="server" class="TDMANDATORY" style="width: 1%">
                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="TD1">
                                        <asp:Label ID="lbl_History" runat="server" CssClass="LABEL" Text="History/Remarks :"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txt_History" runat="server" CssClass="TEXTBOX" Width="90%" Height="50px"
                                            TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
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
        <%-- <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>--%>
        <td>
            <asp:Panel ID="Panel2" runat="server" GroupingText="Address Details">
                <table width="100%">
                    <tr>
                        <td colspan="5" style="width: 99%;">
                            <uc2:WucAddress ID="WucAddress1" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <%-- </table>
        </td>--%>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" GroupingText="Native Address" meta:resourcekey="Panel3Resource1"
                            CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Native_Address_Line1" Text="Address Line 1 :" runat="server" meta:resourcekey="lbl_Native_Address_Line1Resource1"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txt_Native_Address_Line1" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="100" meta:resourcekey="txt_Native_Address_Line1Resource1"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                        <asp:Label ID="lbl_Td_Mandatory_NativeAddressLine1" ForeColor="Red" runat="server"
                                            meta:resourcekey="lbl_Td_Mandatory_NativeAddressLine1Resource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Native_Address_Line2" Text="Address Line 2 :" runat="server" meta:resourcekey="lbl_Native_Address_Line2Resource1"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txt_Native_Address_Line2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="100" meta:resourcekey="txt_Native_Address_Line2Resource1"></asp:TextBox>
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                        <asp:Label ID="lbl_Td_Native_Address_Line2" runat="server" ForeColor="Red" meta:resourcekey="lbl_Td_Native_Address_Line2Resource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ContactNo" Text="Contact No :" runat="server" meta:resourcekey="lbl_ContactNoResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txt_Contact_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onkeypress="return Only_Integers(this,event)" onblur="valid(this)" MaxLength="20"
                                            meta:resourcekey="txt_Contact_NoResource1"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                        <asp:Label ID="lbl_Td_Mandatory_ContactNo" runat="server" ForeColor="Red" meta:resourcekey="lbl_Td_Mandatory_ContactNoResource1"></asp:Label>
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
                        <asp:Panel ID="Panel4" runat="server" GroupingText="Refernce Details" meta:resourcekey="Panel4Resource1"
                            CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferenceName" Text="1. Reference Name :" runat="server" meta:resourcekey="lbl_ReferenceNameResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Name" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="100" meta:resourcekey="txt_Refence_NameResource1" Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="TD_Mandatory_ReferenceName" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Refered_On" Text="Refered On :" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <uc1:wuc_Date_Picker ID="wuc_ReferedDate" runat="server" />
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td2" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferencePhone" Text="Reference Phone :" runat="server" meta:resourcekey="lbl_ReferencePhoneResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Phone" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onkeypress="return Only_Integers(this,event)" onblur="valid(this)" MaxLength="25"
                                            meta:resourcekey="txt_Refence_PhoneResource1" Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_ReferencePhone" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferenceMobile" Text="Reference Mobile :" runat="server" meta:resourcekey="lbl_ReferenceMobileResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Mobile" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onkeypress="return Only_Integers(this,event)" onblur="valid(this)" MaxLength="25"
                                            meta:resourcekey="txt_Refence_MobileResource1" Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_txt_Refence_Mobile" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferenceName2" Text="2. Reference Name :" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Name2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="100" Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="TD_Mandatory_ReferenceName2" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_Refered_On2" Text="Refered On :" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <uc1:wuc_Date_Picker ID="wuc_ReferedDate2" runat="server" />
                                    </td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td4" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferencePhone2" Text="Reference Phone :" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Phone2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onkeypress="return Only_Integers(this,event)" onblur="valid(this)" MaxLength="25"
                                            Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_ReferencePhone2" runat="server">
                                        *</td>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_ReferenceMobile2" Text="Reference Mobile :" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Refence_Mobile2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            onkeypress="return Only_Integers(this,event)" onblur="valid(this)" MaxLength="25"
                                            Width="300px"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" id="Td_Mandatory_txt_Refence_Mobile2"
                                        runat="server">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="Tr_Mandatory_Others" runat="Server">
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" GroupingText="Others" meta:resourcekey="Panel5Resource1"
                            CssClass="PANEL">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 15%">
                                        <asp:Label ID="lbl_OpeningBalance" runat="server" Text="Opening Bal :" meta:resourcekey="lbl_OpeningBalanceResource1"
                                            CssClass="LABEL"></asp:Label></td>
                                    <td style="width: 34%">
                                        <asp:TextBox ID="txt_Opening_Balance" onkeypress="return Only_Numbers(this,event)"
                                            runat="server" Width="50%" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="16"
                                            meta:resourcekey="txt_Opening_BalanceResource1" ReadOnly="true"></asp:TextBox>
                                        <asp:DropDownList Width="25%" ID="ddl_Account_Effect_Type" runat="server" CssClass="DROPDOWN"
                                            meta:resourcekey="ddl_Account_Effect_TypeResource1" Enabled="false" />&nbsp;
                                        <asp:Label ID="lbl_Mandatroy_OpeningBalance" runat="server" Text="*" meta:resourcekey="lbl_Mandatroy_OpeningBalanceResource1"
                                            Font-Size="11px" Font-Bold="True" Font-Names="Verdana" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td style="width: 1%; text-align: left" class="TDMANDATORY">
                                        &nbsp</td>
                                    <td class="TD1" style="width: 15%">
                                    </td>
                                    <td style="width: 34%">
                                        <asp:CheckBox ID="chk_CreateeCargouser" runat="server" Text="Create eCargo User?"
                                            CssClass="LABEL" /></td>
                                    <td style="width: 1%;" class="TDMANDATORY">
                                        &nbsp</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="Hdn_Driver_Type_ID" runat="server" />
            <asp:HiddenField ID="hdn_Is_Cleaner" runat="Server" />
            &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <%-- <asp:UpdatePanel ID="upd_Pnl_Engine_Body_Save" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Specification" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
</table>

<script type="text/javascript">
EnableDisableLicenseAuthenticatedBy();
</script>

