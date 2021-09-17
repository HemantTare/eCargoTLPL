<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayError.aspx.cs" Inherits="Display_DisplayError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ERROR</title>
</head>
<body>
    <form id="form1" runat="server">
   <div align="center"><table border="0" cellpadding="0" cellspacing="0" style="width: 600px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;background-color:#F6F6F6;"><tr><td>&nbsp;</td></tr><tr><td style="width: 100%; text-align: center;" valign="Top"><img id="Image3" src="../Images/error_img.gif" align="middle" style="border-width:0px;vertical-align: middle" />&nbsp;&nbsp;&nbsp;
   <asp:Label runat="server" id="lbl_Heading" style="color:Red;font-family:verdana;font-size:20px;font-weight:bold;vertical-align: middle"></asp:Label>
   </td></tr><tr><td>&nbsp;</td></tr><tr><td style="width: 100%; text-align: center; height: 14px;">
   <asp:Label runat="server" id="lbl_Description" style="font-family:verdana;font-size:12px;font-weight:bold;"></asp:Label></td></tr><tr><td>&nbsp;</td></tr></table></div>
    </form>
   <%--<script type="text/javascript">    
        self.parent.hideload();    
    </script>--%>
</body>
</html>
