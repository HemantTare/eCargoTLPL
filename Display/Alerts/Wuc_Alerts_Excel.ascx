<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_Alerts_Excel.ascx.cs" Inherits="Alerts_Wuc_Alerts_Excel" %>

<script language="javascript" type="text/javascript">

function OpenIncomingTrucksAlert(IsFromDesktop)
  {
  var Path ='';
  Path='../Reports/User Desk/FrmIncomingTrucksAlert.aspx?IsFromDesktop=' +IsFromDesktop ;
  var w = screen.availWidth;
  var h = screen.availHeight;
  var popW = w;
  var popH = h;
  var leftPos = (w-popW)/2;
  var topPos = (h-popH)/2;
  window.open(Path, 'incoming_truck_alerts_Window', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no,resizable=yes,scrollbars=yes');
  return false;
  }
  </script>
<table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    <tr>
        <td style="width: 30%; vertical-align:middle;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: #bbccc3 1px solid;
                border-top: #bbccc3 1px solid; border-left: #bbccc3 1px solid; border-bottom: #bbccc3 1px solid;">
                <tr>
                    <td colspan="2" style="background-image: url(../images/Desk_Header.gif)">
                        &nbsp;
                        <asp:Label ID="lbl_Alerts" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                            ForeColor="#CE0029" Text="Alerts !"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Image ID="Img5" runat="server" ImageUrl="~/images/Alerts.gif" Height="94px"
                            Width="163px" /></td>
                </tr>
                <tr id="tr_lnkbtn" runat="server" align="left">
                    <td id="td_lnkbtn" runat="server" style="width: 20%">
                        <asp:LinkButton ID="lnk_btnIncomingTruck" runat="server" Text="Incoming Vehicle()" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
