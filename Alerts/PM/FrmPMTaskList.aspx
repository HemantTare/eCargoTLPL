<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPMTaskList.aspx.cs" Inherits="Alerts_PM_FrmPMTaskList" %>

<%@ Register Src="WucPMTaskList.ascx" TagName="WucPMTaskList" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PREVENTIVE MAINTENENCE</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucPMTaskList id="WucPMTaskList1" runat="server">
        </uc1:WucPMTaskList>&nbsp;</div>
    </form>
</body>
</html>
