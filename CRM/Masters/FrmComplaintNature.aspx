<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmComplaintNature.aspx.cs" Inherits="CRM_Masters_FrmComplaintNature" %>

<%@ Register Src="WucComplaintNature.ascx" TagName="WucComplaintNature" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
     </head> 
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucComplaintNature ID="WucComplaintNature1" runat="server" />    
    </div>
    </form>
</body>
</html>
