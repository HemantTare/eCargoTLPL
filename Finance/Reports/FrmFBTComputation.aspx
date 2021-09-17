<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFBTComputation.aspx.cs" Inherits="FA_Common_Reports_FrmFBTComputation" %>

<%@ Register Src="WucFBTComputation.ascx" TagName="WucFBTComputation" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FBT Computation</title>
       
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucFBTComputation id="WucFBTComputation1" runat="server">
        </uc1:WucFBTComputation></div>
    </form>
     <script type="text/javascript">
                self.parent.hideload();
            </script>
</body>
</html>
