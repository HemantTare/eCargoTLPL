<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGroupSummary.aspx.cs" Inherits="Finance_Reports_FrmGroupSummary" %>

<%@ Register Src="WucGroupSummary.ascx" TagName="WucGroupSummary" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GROUP SUMMARY</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucGroupSummary ID="WucGroupSummary1" runat="server" />
        &nbsp;</div>
    </form>
    <script type="text/javascript">
            self.parent.hideload();
    </script>
</body>
</html>
