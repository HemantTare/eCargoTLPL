<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="CommonStyleSheet.css"  rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:DropDownList ID="DropDownList1" runat="server" CssClass="DROPDOWN" AutoPostBack="false">
      <asp:ListItem Text ="yes" Value ="yes" ></asp:ListItem>
      <asp:ListItem Text ="no" Value ="no" ></asp:ListItem>
      <asp:ListItem Text ="ezone" Value ="ezone" ></asp:ListItem>
      <asp:ListItem Text ="maybe" Value ="maybe" ></asp:ListItem>
      <asp:ListItem Text ="atlas" Value ="atlas" ></asp:ListItem>
      </asp:DropDownList>
    
    </div>
    </form>
</body>
</html>
