<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLoginChangePwd.aspx.cs" Inherits="Login_FrmLoginChangePwd" %>

<%@ Register Src="WucLoginChangePwd.ascx" TagName="WucLoginChangePwd" TagPrefix="uc1" %>


<%@ Register Src="~/Bars/WucHeader1.ascx" TagName="WucHeader1" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CHANGE PASSWORD</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px 0px">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 100%; height: 20%;">
                    <uc2:WucHeader1 ID="WucHeader1_1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 100%; text-align: center; height: 70%;">
                    <br />
                    <br />
                    <uc1:WucLoginChangePwd id="WucLoginChangePwd1" runat="server">
                    </uc1:WucLoginChangePwd><br />

                    <br />
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%; height: 10%;">
                </td>
            </tr>
        </table>
       </div>
    </form>
</body>
</html>

