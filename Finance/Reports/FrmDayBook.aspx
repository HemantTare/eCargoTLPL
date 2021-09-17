<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDayBook.aspx.cs" Inherits="Finance_Reports_FrmDayBook" %>

<%@ Register Src="WucDayBook.ascx" TagName="WucDayBook" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DAY BOOK</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucDayBook id="WucDayBook1" runat="server">
        </uc1:WucDayBook></div>
    </form>
     <script type="text/javascript">
            self.parent.hideload();
    </script>
</body>
</html>
