<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTransitDaysStateToState.aspx.cs" Inherits="Master_Branch_FrmTransitDaysStateToState" %>

<%@ Register Src="WucTransitDaysStateToState.ascx" TagName="WucTransitDaysStateToState"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transit Days (State To State)</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTransitDaysStateToState ID="WucTransitDaysStateToState1" runat="server" />
        
    </div>
    </form>
</body>
</html>
