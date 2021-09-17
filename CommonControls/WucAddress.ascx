<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAddress.ascx.cs" Inherits="Master_Control_Address" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../Javascript/Common.js"></script>

<link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">

  function ValidateWucAddress(lbl_Errors)
  {
    var txt_ddlSearch=document.getElementById('<%=ddl_City.TextBoxClientID%>');
    var IsEMailVisible='<%=txt_Email_Id.Visible%>';

    var objResource=new Resource('<%=hdf_ResourecString.ClientID%>');

    if(txt_ddlSearch.value=='' && (control_is_mandatory(txt_ddlSearch) == true)) 
    {
      lbl_Errors.innerText = objResource.GetMsg("Msg_ddl_City");
      return false;
    }
    else if (!validMobile())
    {
      lbl_Errors.innerText = 'Please enter 10 digit mobile number';
      return false;
    }
    else  if (IsEMailVisible=='True' && !validEMail(document.getElementById('<%=txt_Email_Id.ClientID%>')))
    {
      lbl_Errors.innerText = objResource.GetMsg("Msg_EmailId");
      return false;
    }

    else
    {
      return true;
    }
  }
 
  function validEMail(txt_EMail)
    {
        var isValidEmail;
        isValidEmail = false;
        
        var chk_eMail_Alert=document.getElementById('<%=chk_eMail_Alert.ClientID%>');
        var hdnShowAlertRow = document.getElementById('<%=hdnShowAlertRow.ClientID%>');

        if (hdnShowAlertRow.value == "0" &&
          Trim(txt_EMail.value)!=''  && 
          !(txt_EMail.value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1))
        {
           isValidEmail = false;
           txt_EMail.focus();
        }
        else if (hdnShowAlertRow.value == "1" && chk_eMail_Alert.checked == false &&
        Trim(txt_EMail.value)!=''  && 
        !(txt_EMail.value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1))
        {
           isValidEmail = false;
           txt_EMail.focus();
        }
        else if (hdnShowAlertRow.value == "1" && chk_eMail_Alert.checked == true &&
        !(txt_EMail.value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1))
        {
            isValidEmail = false;
            txt_EMail.focus();
        }
        else 
        {
          isValidEmail = true;
        }
       return isValidEmail;
    }
 
  function validMobile()
    {
        var hdnShowAlertRow = document.getElementById('<%=hdnShowAlertRow.ClientID%>');
        if (hdnShowAlertRow.value == "0")
          return true;

        var isValidMobile;
        isValidMobile = false;

        var chk_SMS_Alert=document.getElementById('<%=chk_SMS_Alert.ClientID%>');        
        var txt_Mobile_No=document.getElementById('<%=txt_Mobile_No.ClientID%>');
  
        if (chk_SMS_Alert.checked == true && 
        txt_Mobile_No.value.length != 10)
        {
            isValidMobile = false;
            txt_Mobile_No.focus();
        }
        else 
        {
          isValidMobile = true;
        }
       return isValidMobile;
    }


function chk_SMS_Alert_Change()
{
  var hdnShowAlertRow = document.getElementById('<%=hdnShowAlertRow.ClientID%>');
  
  if (hdnShowAlertRow.value == "0")
    return;
  
  var chk_SMS_Alert=document.getElementById('<%=chk_SMS_Alert.ClientID%>');
  var lbl_Mobile_Mandatory=document.getElementById('<%=lbl_Mobile_Mandatory.ClientID%>');
  
  if (chk_SMS_Alert.checked)
  {
    lbl_Mobile_Mandatory.innerHTML = '*'
  }
  else
  {
    lbl_Mobile_Mandatory.innerHTML = ''
  }
}

function chk_eMail_Alert_Change()
{
  var hdnShowAlertRow = document.getElementById('<%=hdnShowAlertRow.ClientID%>');
  
  if (hdnShowAlertRow.value == "0")
    return;
    
  var chk_eMail_Alert=document.getElementById('<%=chk_eMail_Alert.ClientID%>');
  var lbl_Email_Mandatory=document.getElementById('<%=lbl_Email_Mandatory.ClientID%>');
  
  if (chk_eMail_Alert.checked)
  {
    lbl_Email_Mandatory.innerHTML = '*'
  }
  else
  {
    lbl_Email_Mandatory.innerHTML = ''
  }
}

</script>
 
<table border="0" width="100%">
  <tr runat="server" id="tr_AddressLine1">
    <td class="TD1" style="width: 20%">
      <asp:Label ID="lbl_AddressLine1" runat="Server" Text="Address Line 1 :" meta:resourcekey="lbl_AddressLine1Resource1"
        CssClass="LABEL"></asp:Label></td>
    <td style="width: 79%" colspan="4">
      <asp:TextBox ID="txt_AddressLine1" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="100" Width="99%" meta:resourcekey="txt_AddressLine1Resource1"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr runat="server" id="tr_AddressLine2">
    <td class="TD1" style="width: 20%">
      <asp:Label ID="lbl_AddressLine2" runat="Server" Text="Address Line 2 :" meta:resourcekey="lbl_AddressLine2Resource1"
        CssClass="LABEL"></asp:Label></td>
    <td style="width: 79%" colspan="4">
      <asp:TextBox ID="txt_AddressLine2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="100" Width="99%" meta:resourcekey="txt_AddressLine2Resource1"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr>
    <td class="TD1" style="width: 20%" runat="server" id="td_lblCity">
      <asp:Label ID="lbl_City" runat="Server" Text="City :" CssClass="LABEL"></asp:Label>
    </td>
    <td style="width: 29%">
      <table>
        <tr>
          <td style="width: 80%" runat="server" id="td_ddlCity">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
                <cc1:DDLSearch ID="ddl_City" runat="server" AllowNewText="false" OnTxtChange="ddl_City_SelectedIndexChanged"
                  PostBack="True" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchCity" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_City" />
              </Triggers>
            </asp:UpdatePanel>
          </td>
          <td class="TDMANDATORY" style="width: 20%">
            <asp:Label ID="lbl_Mandatory_City" runat="server" CssClass="LABEL" Text="*"></asp:Label>
          </td>
        </tr>
      </table>
    </td>
    <td class="TDMANDATORY" style="width: 1%" align="left">
    </td>
    <td class="TD1" style="width: 20%" runat="server" id="td_lblPinCode">
      <asp:Label ID="lbl_PinCode" runat="Server" Text="Pin Code :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtPinCode">
      <asp:TextBox ID="txt_PinCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="6" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"
        Width="250px"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr id="Tr1" runat="server">
    <td id="td_lblState" runat="server" class="TD1" style="width: 20%">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
          <asp:Label ID="lblState" runat="server" Text="State :" CssClass="LABEL"></asp:Label>
          <asp:HiddenField ID="hdnGSTStateCode" runat="server"></asp:HiddenField>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_City" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td id="td_State" runat="server" align="left" style="width: 29%">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_State" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_City" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td id="td_lblCountry" runat="server" class="TD1" style="width: 20%">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
          <asp:Label ID="lblCountry" runat="server" Text="Country :" CssClass="LABEL"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_City" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td id="td_Country" runat="server" align="left" style="width: 29%">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Country" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_City" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr id="Tr2" runat="server">
    <td id="td_lblRegion" runat="server" class="TD1" style="width: 20%">
      <asp:Label ID="lblZone" runat="server" Text="Region :" CssClass="LABEL"></asp:Label></td>
    <td id="td_Region" runat="server" align="left" style="width: 29%">
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Zone" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_City" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td id="Td7" runat="server" class="TD1" style="width: 20%">
    </td>
    <td id="Td8" runat="server" align="left" style="width: 29%">
    </td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr runat="server" id="tr_Std_Mobile">
    <td class="TD1" style="width: 20%" runat="server" id="td_lblStdCode">
      <asp:Label ID="lbl_Std_Code" runat="Server" Text="Std Code :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtStdCode">
      <asp:TextBox ID="txt_Std_Code" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="15" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td class="TD1" style="width: 20%" runat="server" id="td_lblMobile">
      <asp:Label ID="lbl_Mobile_No" runat="Server" Text="Mobile No :" CssClass="LABEL"></asp:Label></td>
    <td align="left" style="width: 29%" runat="server" id="td_txtMobile">
      <asp:TextBox ID="txt_Mobile_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="10" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"
        Width="250px"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
      <asp:Label ID="lbl_Mobile_Mandatory" runat="Server" Text="" CssClass="LABELERROR"></asp:Label>
    </td>
  </tr>
  <tr runat="server" id="tr_PhoneNo">
    <td class="TD1" style="width: 20%" runat="server" id="td_lblPhone1">
      <asp:Label ID="lbl_Phone1" runat="Server" Text="Phone No 1 :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtPhone1">
      <asp:TextBox ID="txt_Phone1" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="20" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td class="TD1" style="width: 20%" runat="server" id="td_lblPhone2">
      <asp:Label ID="lbl_Phone2" runat="Server" Text="Phone No 2 :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtPhone2">
      <asp:TextBox ID="txt_Phone2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="20" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"
        Width="250px"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
  </tr>
  <tr runat="server" id="tr_Fax_Email">
    <td class="TD1" style="width: 20%" runat="server" id="td_lblFaxNo">
      <asp:Label ID="lbl_Fax_No" runat="Server" Text="Fax No :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtFaxNo">
      <asp:TextBox ID="txt_Fax_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="20" onkeypress="return Only_Integers(this,event)" onblur="valid(this)"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td class="TD1" style="width: 20%" runat="server" id="td_lblEmailID">
      <asp:Label ID="lbl_Email_Id" runat="Server" Text="Email Id :" CssClass="LABEL"></asp:Label>
    </td>
    <td align="left" style="width: 29%" runat="server" id="td_txtEmail">
      <asp:TextBox ID="txt_Email_Id" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
        MaxLength="100" Width="250px"></asp:TextBox></td>
    <td class="TDMANDATORY" style="width: 1%">
      <asp:Label ID="lbl_Email_Mandatory" runat="Server" Text="" CssClass="LABELERROR"></asp:Label>
    </td>
  </tr>
  <tr runat="server" id="trAlert" visible="false">
    <td class="TD1" style="width: 20%" id="td_lblEmailAlert" runat="server">
      Email Alert?
    </td>
    <td class="TD" style="width: 29%" id="td_chkEmailAlert" runat="server">
      <asp:CheckBox ID="chk_eMail_Alert" onclick = "chk_eMail_Alert_Change()" runat="server" />
    </td>
    <td class="TDMANDATORY" style="width: 1%">
      &nbsp;
    </td>
    <td class="TD1" style="width: 20%" id="td_lblSMSAlert" runat="server">
      SMS Alert?
    </td>
    <td class="TD" style="width: 29%" id="td_chkSMSAlert" runat="server">
      <asp:CheckBox ID="chk_SMS_Alert" runat="server" onclick = "chk_SMS_Alert_Change()" />
    </td>
    <td class="TDMANDATORY" style="width: 1%">
      &nbsp;
    </td>
  </tr>
</table>
<%--</fieldset> 
--%>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />
<asp:HiddenField ID="hdn_RegionDetailID" runat="server" Value="0" />
<asp:HiddenField ID="hdnShowAlertRow" runat="server" Value="0" />

<script type="text/javascript">
chk_SMS_Alert_Change();
chk_eMail_Alert_Change();
</script>