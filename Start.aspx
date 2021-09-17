<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Start.aspx.vb" Inherits="Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Application("Title")%></title>
    
 <script type="text/javascript">

    function OpenWin()
    {
    var win = window.open('FrmLogin.aspx','',"channelmode=yes,scrollbars=yes,status=yes,toolbar=yes,menubar=no,titlebar=yes,resizable=yes");
    window.close(); 
    }
</script>
    
</head>
<body onload="window.opener='';OpenWin()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
