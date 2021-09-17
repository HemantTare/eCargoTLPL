<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPetrolPump.aspx.cs" Inherits="EC_Master_FrmPetrolPump" %>

 

<%@ Register Src="WucPetrolPump.ascx" TagName="WucPetrolPump" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../JavaScript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>PETROL PUMP</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="SC_PetrolPump" runat="server" />
      <uc1:WucPetrolPump ID="WucPetrolPump1" runat="server" />
      
      </div>
    </form>
</body>
</html>
