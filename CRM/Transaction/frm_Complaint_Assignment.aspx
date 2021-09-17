<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Complaint_Assignment.aspx.cs" Inherits="CRM_frm_Complaint_Assignment" %>

<%@ Register Src="wuc_Complaint_Assignment.ascx" TagName="wuc_Complaint_Assignment" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Complaint Assignment</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wuc_Complaint_Assignment ID="wuc_Complaint_Assignment1" runat="server" />
    
    </div>
    </form>
      <script type="text/javascript">
    
      //  self.parent.hideload();
    
    </script>
</body>
</html>
