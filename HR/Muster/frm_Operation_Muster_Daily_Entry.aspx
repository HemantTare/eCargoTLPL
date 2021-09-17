<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Operation_Muster_Daily_Entry.aspx.cs" Inherits="Operations_Muster_frm_Operation_Muster_Daily_Entry" %>

<%@ Register Src="wuc_Operation_Muster_Daily_Entry.ascx" TagName="wuc_Operation_Muster_Daily_Entry"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MUSTER ENTRY DAILY DETAILS</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server">
    
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        <uc1:wuc_Operation_Muster_Daily_Entry id="Wuc_Operation_Muster_Daily_Entry1" runat="server">
        </uc1:wuc_Operation_Muster_Daily_Entry>
       
        
        </div>
        
        
    </form>
    
</body>
 
</html>
