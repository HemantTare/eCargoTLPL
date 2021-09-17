<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmReserveGC.aspx.cs" Inherits="Operations_Booking_FrmReserveGC" %>

<%@ Register Src="WucReserveGC.ascx" TagName="WucReserveGC" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    
     <title>
    Reserve <%=CompanyManager.getCompanyParam().GcCaption%>
    </title>
    
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucReserveGC ID="WucReserveGC1" runat="server" />
    
    </div>
    </form>
     <script type="text/javascript">    
        self.parent.hideload();    
    </script>
    
</body>
</html>
