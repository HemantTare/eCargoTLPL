
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="FrmGC.aspx.cs" Inherits="Operations_Booking_FrmGC" %>

<%@ Register Src="wucGC.ascx" TagName="wucGC" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>
    <%=CompanyManager.getCompanyParam().GcCaption%>
    </title>
  
 
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:wucGC ID="wucGC1" runat="server" />
    
    </div>
    </form>
      <script type="text/javascript">
    
      //  self.parent.hideload();
    
    </script>
</body>
</html>
