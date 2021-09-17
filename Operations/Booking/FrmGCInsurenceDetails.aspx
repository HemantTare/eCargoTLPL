
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGCInsurenceDetails.aspx.cs" Inherits="Operations_Booking_FrmGCInsurenceDetails" %>

<%@ Register Src="wucGCInsurenceDetails.ascx" TagName="wucGCInsurenceDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>
    <%=CompanyManager.getCompanyParam().GcCaption%>
    </title>
  
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wucGCInsurenceDetails ID="wucGCInsurenceDetails1" runat="server" />
    
    </div>
    </form>
      <script type="text/javascript">
    
      //  self.parent.hideload();
    
    </script>
</body>
</html>
