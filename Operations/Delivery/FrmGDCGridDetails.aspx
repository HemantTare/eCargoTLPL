<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGDCGridDetails.aspx.cs" Inherits="Operations_Delivery_FrmGDCGridDetails" %>

<%@ Register Src="wucDeliveryOtherDetails.ascx" TagName="wucDeliveryOtherDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" language="javascript">
function EnableDisableDeliveryTakenByContact()
 {
    var ddl_DeliveryMode = document.getElementById("<%=ddl_DeliveryMode.ClientID %>");
    var tr_DeliveryTakenBy_Contact = document.getElementById("<%=tr_DeliveryTakenBy_Contact.ClientID%>");
    var lbl_DeliveryTakenBy=document.getElementById("<%=lbl_DeliveryTakenBy.ClientID%>");
    var txt_DeliveryTakenBy=document.getElementById("<%=txt_DeliveryTakenBy.ClientID%>");
    var lbl_ContactNo=document.getElementById("<%=lbl_ContactNo.ClientID%>");
    var txt_ContactNo=document.getElementById("<%=txt_ContactNo.ClientID%>");
    
    if (ddl_DeliveryMode.value == 4)
  {
     tr_DeliveryTakenBy_Contact.style.display='inline';
     lbl_DeliveryTakenBy.style.display='inline';
     txt_DeliveryTakenBy.style.display='inline';
     lbl_ContactNo.style.display='inline';
     txt_ContactNo.style.display='inline';
  }
  else
  {
    tr_DeliveryTakenBy_Contact.style.display='none';
    lbl_DeliveryTakenBy.style.display='none';
     txt_DeliveryTakenBy.style.display='none';
     lbl_ContactNo.style.display='none';
     txt_ContactNo.style.display='none';
   }
   
 }

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server">
        <table class="TABLE" id="TABLE1">
            <tr>
                <td class="TDGRADIENT" colspan="2" style="width: 100%">&nbsp;
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="tr_Del_Mod">
                <td class="TD1" style="width: 20.5%">
                    <asp:Label ID="lbl_DeliveryMode" runat="server" Text="Delivery Mode:" CssClass="LABEL"></asp:Label>
                </td>
                <td style="width: 79%">
                    <asp:DropDownList ID="ddl_DeliveryMode" runat="server" AutoPostBack="true" CssClass="DROPDOWN"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr id="tr_DeliveryTakenBy_Contact" runat="server">
                            <td class="TD1" style="width: 20.5%">
                                <asp:Label ID="lbl_DeliveryTakenBy" runat="server" Text="Delivery Taken By:" CssClass="LABEL"></asp:Label>
                            </td>
                            <td style="width: 28.5%">
                                <asp:TextBox ID="txt_DeliveryTakenBy" runat="server" CssClass="TEXTBOX" MaxLength="30"></asp:TextBox>
                            </td>
                            <td style="width: 1%" class="TDMANDATORY">*
                            </td>
                            <td class="TD1" style="width: 20.5%">
                                <asp:Label ID="lbl_ContactNo" runat="server" Text="Contact No:" CssClass="LABEL" />
                            </td>
                            <td style="width: 28.5%">
                                <asp:TextBox ID="txt_ContactNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="11"/>
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="tr_Del_Des">
                <td class="TD1" style="width: 20.5%">
                    <asp:Label ID="lbl_Description" runat="server" Text="Description:" CssClass="LABEL"></asp:Label>
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txt_Description" runat="server" CssClass="TEXTBOX" Height="40px" TextMode="MultiLine" MaxLength="50"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <uc1:wucDeliveryOtherDetails ID="WucDeliveryOtherDetails1" runat="server" />                        
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="TD1" colspan="2" style="text-align: center;">
                    <asp:Button ID="btn_Save" Width="100px" Font-Size="11px" Font-Names="Verdana" BackColor="#F2F2F2" BorderColor="Black" BorderStyle ="solid" BorderWidth="1px" runat="server" Text="Save" OnClick="btn_Save_Click"/>&nbsp;
                    <asp:Button ID="btn_Exit" Width="100px" Font-Size="11px" Font-Names="Verdana" BackColor="#F2F2F2" BorderColor="Black" BorderStyle ="solid" BorderWidth="1px" runat="server" Text="Exit" OnClick="btn_Exit_Click"/>
                </td>
            </tr> 
            <tr>
                <td colspan="2" >
                    <asp:Label ID="lbl_errors" runat="server" CssClass="LABELERROR"></asp:Label>
                </td>
            </tr>
        </table>        
    </form>
    <%--<script type="text/javascript">
        EnableDisableDeliveryTakenByContact();
        </script>--%>
</body>
</html>
