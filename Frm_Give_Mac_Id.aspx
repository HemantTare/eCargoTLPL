<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Give_Mac_Id.aspx.cs" Inherits="Frm_Give_Mac_Id" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Get Mac Id</title>
    <link href="CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script language="javascript" type="text/javascript">   

        function GetCPUinfo()
         {
            var a,b,c
            a= new ActiveXObject("Scripting.FileSystemObject");
            b=a.GetDrive(a.GetDriveName("C:"));
            c=b.SerialNumber;
                 if(c != "")
                    {
                     //alert(document.getElementById("CPU").CPUID);
                     
                     document.getElementById( "<%=txt_Mac_ID.ClientID %>").value = c ; //document.getElementById("CPU").CPUID
                     
                    }
                    else
                    {
                        alert("There is some problem in retrieving your machine information");
                        //alert(document.getElementById("CPU").ErrorMsg);
                    }
                      return false;
              }
</script>    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 100%; height: 20%;">
                    <asp:TextBox runat="server" ID="txt_Mac_ID" Text="" ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 100%; text-align: center; height: 70%;">
              <asp:Button ID="btn_login" runat="server" Text="Get Mac ID" CssClass="BUTTON" OnClientClick="return GetCPUinfo();"/>
                </td>
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
