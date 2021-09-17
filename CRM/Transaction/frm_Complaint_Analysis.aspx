<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Complaint_Analysis.aspx.cs" Inherits="Complaint_Analysis_frm_Complaint_Analysis" %>

<%@ Register Src="wuc_Complaint_Analysis.ascx" TagName="wuc_Complaint_Analysis" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>COMPLAINT ANALYSIS</title>
    <link href="FA_Common/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wuc_Complaint_Analysis ID="wuc_Complaint_Analysis1" runat="server" />
    
    </div>
    </form>
      <script type="text/javascript">
    
      //  self.parent.hideload();
    
    </script>
</body>
</html>
