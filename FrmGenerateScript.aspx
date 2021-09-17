<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGenerateScript.aspx.cs" Inherits="FrmGenerateScript" %>

<%@ Register Src="CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>


<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <uc1:WucDatePicker ID="WucDatePicker1" runat="server" />
      <br />
      <br />
      <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate SQL Script" /><br />
      <br />
      <asp:Button ID="Button2" runat="server" Text="Generate Folders" OnClick="Button2_Click" /><br />
      </div>
    </form>
     <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>  
</body>
</html>
