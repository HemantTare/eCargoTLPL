<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOctroiFormType.aspx.cs" Inherits="Master_General_FrmOctroiFormType" %>

<%@ Register Src="WucOctroiUpdateFormType.ascx" TagName="WucOctroiUpdateFormType"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Octroi Form Type</title>
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucOctroiUpdateFormType id="WucOctroiUpdateFormType1" runat="server">
        </uc1:WucOctroiUpdateFormType></div>
    </form>
</body>
</html>
