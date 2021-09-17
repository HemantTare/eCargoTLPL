<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_Operations_Links.ascx.cs" Inherits="Display_Alerts_Wuc_Operations_Links" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script language="javascript" type="text/javascript">

   function OpenF4Menu(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
   
   function OpenF5Menu(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_F5' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
   function OpenF6Menu(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_F6' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function newwindow1(url)
    {     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2; 
          window.open(url, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=no,scrollbars=yes')
          return false;
    }
    
function FleetLinksDetails(CallFrom) 
{
    var Path = '';
    Path = '../Display/FrmFleetAlertLinks.aspx?CallFrom=' + CallFrom ;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 750;
    var popH = h - 400;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'CustomPopUp_fleetAlertLinks', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
    return false;
}
function LoadPopUp(Path,Menuitem_Id)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w;
    var popH = h - 30;
    var leftPos = (w-popW)/2;
    var topPos = 0;    
    window.open(Path, 'CustomPopUp<%=UserId%>' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
}  
</script>

<script language="javascript" type="text/javascript">
document.onkeydown = checkKeycode
function checkKeycode(e) 
{
  var keycode;
  if (window.event) keycode = window.event.keyCode;
  else if (e) keycode = e.which;  
  
  
  
if (keycode < 115 || keycode > 121)
{return;}
    var hdn_F4 = document.getElementById('<%=hdn_F4.ClientID %>');
    var hdn_F4Path = document.getElementById('<%=hdn_F4Path.ClientID %>');
    var hdn_F5 = document.getElementById('<%=hdn_F5.ClientID %>');
    var hdn_F5Path = document.getElementById('<%=hdn_F5Path.ClientID %>');
    var hdn_F6 = document.getElementById('<%=hdn_F6.ClientID %>');
    var hdn_F6Path = document.getElementById('<%=hdn_F6Path.ClientID %>');
    var hdn_F7Path = document.getElementById('<%=hdn_F7Path.ClientID %>');
    var hdn_F8 = document.getElementById('<%=hdn_F8.ClientID %>');
    var hdn_F8Path = document.getElementById('<%=hdn_F8Path.ClientID %>');
    var hdn_F9 = document.getElementById('<%=hdn_F9.ClientID %>');
    var hdn_F9Path = document.getElementById('<%=hdn_F9Path.ClientID %>');
    var hdn_F10 = document.getElementById('<%=hdn_F10.ClientID %>');
    var hdn_F10Path = document.getElementById('<%=hdn_F10Path.ClientID %>');

    
    
  if (keycode == 115 && hdn_F4.value == 'true')
  { 
     OpenF4Menu(hdn_F4Path.value,30);
  }
  else if (keycode == 116 && hdn_F5.value == 'true')
  { 
     OpenF5Menu(hdn_F5Path.value,51);
  }
  else if (keycode == 117 && hdn_F6.value == 'true')
  { 
     OpenF6Menu(hdn_F6Path.value,73);
  }
  else if (keycode == 118)
  {  
     newwindow1(hdn_F7Path.value);
  }
  else if (keycode == 119 && hdn_F8.value == 'true')
  { 
     LoadPopUp(hdn_F8Path.value,5221);
  }
  else if (keycode == 120 && hdn_F9.value == 'true')
  { 
     LoadPopUp(hdn_F9Path.value,5159);
  }
  else if (keycode == 121 && hdn_F10.value == 'true')
  { 
     LoadPopUp(hdn_F10Path.value,5253);
  }
  else
  {
//    alert('Invalid Shortcut Key!!');
  }
  event.returnValue = false;
  event.keyCode = 0; 

}
</script>

<table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr id="tr_lnkbtn" runat="server" align="left">
    <td id="td_lnkbtn" runat="server" style="width: 100%">
      <asp:Image ID="ImgF4" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF4" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F4 - New LR" />
      <br />
      <asp:Image ID="ImgF5" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF5" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F5 - Invoice" />
      <br />
      <asp:Image ID="ImgF6" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF6" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F6 - Trip Memo" />
      <br />
      <asp:Image ID="ImgF7" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF7" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F7 - Track & Trace" />
      <br />
      <asp:Image ID="ImgF8" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF8" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F8 - Daily Booking Stock" />
      <br />
      <asp:Image ID="ImgF9" runat="server" ImageUrl="~/Images/bullet.gif" />
      <asp:LinkButton ID="lnk_btnF9" runat="server" Font-Names="Verdana" Font-Size="11px"
        Font-Underline="True" ForeColor="#213163" Text="F9 - Delivery Stock List" />
        <br />
        <br />
        <asp:Image ID="ImgF10" runat="server" ImageUrl="~/Images/bullet.gif" />
        <asp:LinkButton ID="lnk_btnF10" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Bold="true"
        Font-Underline="True" ForeColor="Red" Text="F10 - Stock Tally" />
        <br />
        <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/bullet.gif" />
        <asp:LinkButton ID="lnk_efleet" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Bold="true"
        Font-Underline="True" ForeColor="Red" Text="Vehicle Renewals" />
        <br />
        <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/bullet.gif" />
        <asp:LinkButton ID="lnk_efleetPM" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Bold="true"
        Font-Underline="True" ForeColor="Red" Text="Vehicle PM" />
    </td>
  </tr>
</table>
<asp:HiddenField ID="hdn_MenuItemId" runat="server" />
<asp:HiddenField ID="hdn_F4" runat="server" />
<asp:HiddenField ID="hdn_F4Path" runat="server" />
<asp:HiddenField ID="hdn_F5" runat="server" />
<asp:HiddenField ID="hdn_F5Path" runat="server" />
<asp:HiddenField ID="hdn_F6" runat="server" />
<asp:HiddenField ID="hdn_F6Path" runat="server" />
<asp:HiddenField ID="hdn_F7Path" runat="server" />
<asp:HiddenField ID="hdn_F8" runat="server" />
<asp:HiddenField ID="hdn_F8Path" runat="server" />
<asp:HiddenField ID="hdn_F9" runat="server" />
<asp:HiddenField ID="hdn_F9Path" runat="server" />
<asp:HiddenField ID="hdn_F10" runat="server" />
<asp:HiddenField ID="hdn_F10Path" runat="server" />
