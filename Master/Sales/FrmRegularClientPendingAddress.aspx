<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRegularClientPendingAddress.aspx.cs"
    Inherits="Master_Sales_FrmRegularClientPendingAddress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Booking/GCNew.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Sales/RegularClient.js"></script>

<script language="javascript" type="text/javascript">

  function Clientsearch()
  {
     
     var hdnCityID = document.getElementById("hdnCityID");
     var lbl_ClientName = document.getElementById("lbl_ClientName");
     var txtMobileNo = document.getElementById("txtMobileNo");
     var txt_CstNo = document.getElementById("txt_CstNo");
     
      var Path ='';
      Path='../../Reports/CL_Nandwana/User Desk/FrmUserDeskClientSearch.aspx?CityID=' + hdnCityID.value + '&ClientName=' + lbl_ClientName.innerHTML + '&MobileNo=' + txtMobileNo.value + '&GSTNo=' + txt_CstNo.value + '&FromClientForm=Yes';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = 0;
      window.open(Path, 'ClientSearch', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 

  function GoogleMaps()
  {
  
      var lblCity = document.getElementById("lblCity");
      var Path ='';
    
        var path = "https://www.google.co.in/maps/place/" + lblCity.innerHTML + "/";
        var ua = window.navigator.userAgent; 
        var msie = ua.indexOf("MSIE ");
        if (parseInt(msie) > 0 ) {
            var shell = new ActiveXObject("WScript.Shell");
            shell.run("chrome.exe "+ path);
        }
        else {
            window.open(path,'height=400,width=600,left=10,top=10,,scrollbars=yes,menubar=no');
        }
      
      return false;
  } 
  
  function SearchGSTNo()
  {
  
    var txt_CstNo = document.getElementById("txt_CstNo");

    var isIE = /*@cc_on!@*/false || !!document.documentMode;

    if (isIE == true)
    {
        clipboardData.setData("Text", txt_CstNo.value);
    }
    else
    {
        function handler (event)
        {
            event.clipboardData.setData('text/plain', txt_CstNo.value);
            event.preventDefault();
            document.removeEventListener('copy', handler, true);
        }

        document.addEventListener('copy', handler, true);
        document.execCommand('copy');
    }


    var Path ='';

//    var path = "https://ewaybillgst.gov.in/Others/TaxPayerSearch.aspx";
    var path ="https://services.gst.gov.in/services/searchtp";
    var ua = window.navigator.userAgent; 
    var msie = ua.indexOf("MSIE ");
    if (parseInt(msie) > 0 ) 
    {
        var shell = new ActiveXObject("WScript.Shell");
        shell.run("chrome.exe "+ path);
    }
    else 
    {
        window.open(path,'height=400,width=600,left=10,top=10,,scrollbars=yes,menubar=no');
    }

    return false;
  } 
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Walkin Client</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" style="width: 100%" id="TABLE1" onclick="return TABLE1_onclick()">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Walkin Client"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Client Name</td>
                    <td style="width: 79%" colspan="4">
                        <asp:Label ID="lbl_ClientName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            Font-Bold="True" MaxLength="100" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Mobile No</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtMobileNo" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                            MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        Phone1</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtPhone1" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                            MaxLength="100" onkeypress="return Only_Numbers(this,event)" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        Address</td>
                    <td style="width: 80%; height: 21px;" colspan="4">
                        <asp:TextBox ID="txtAddress" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="100" Width="99.2%" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        City</td>
                    <td style="width: 29%; height: 21px;">
                        <asp:Label ID="lblCity" CssClass="LABEL" Font-Bold="True" BorderWidth="1px" runat="server" />
                        <asp:HiddenField ID="hdnCityID" runat="server" />
                        <asp:HiddenField ID="hdn_DeliveryAreaID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdn_GST_State_Code" runat="server" />
                        &nbsp;&nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        Pin Code</td>
                    <td style="width: 29%; height: 21px;">
                        <asp:TextBox ID="txt_PinCode" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                            MaxLength="100" /></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Delivery Area</td>
                    <td style="width: 29%">
                        &nbsp;<asp:DropDownList ID="ddlDeliveryArea" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddlDeliveryArea_SelectedIndexChanged"
                            Width="98%">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        Delivery Type</td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddl_dly_type" runat="server" AutoPostBack="false" CssClass="DROPDOWN">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 80%; height: 15px;" colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Is GST No. Available?
                    </td>
                    <td style="width: 29%">
                        <asp:CheckBox ID="Chk_IsServiceTaxPayable" runat="server" CssClass="CHECKBOX" /></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        GST No.&nbsp;</td>
                    <td style="width: 29%">
                        &nbsp;<asp:TextBox ID="txt_CstNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="15" onblur="ValidateGSTOnType(this, document.getElementById('hdn_GST_State_Code').value, '0'); return Uppercase(this);"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 80%; height: 15px;" colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 29%">
                        &nbsp;<asp:LinkButton ID="lnk_btnClientSearch" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="11px" Font-Underline="true" ForeColor="MediumBlue" Text="Search Client"></asp:LinkButton></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%">
                        <asp:LinkButton ID="lnk_btnGSTNoSearch" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="11px" Font-Underline="true" ForeColor="#ff0000" Text="Search GSTNo."></asp:LinkButton></td>
                    <td style="width: 29%">
                        <asp:LinkButton ID="lnk_btnGoogleMap" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="11px" Font-Underline="true" ForeColor="Purple" Text="Google Maps"></asp:LinkButton></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                    </td>
                    <td style="width: 80%; height: 15px;" colspan="6">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUIForRegularClientGC();"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                                <asp:HiddenField ID="hdnGCId" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">

 function updateparentwindow(clientname)
 { 
   self.close();
   window.opener.UpdateFromRegularClient(clientname);

 } 


</script>

