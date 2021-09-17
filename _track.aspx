<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_track.aspx.cs" Inherits="TrackNTrace_FrmGCLiveTracking" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="Javascript/Common.js"></script>
<script type="text/javascript" src="JQuery/jquery-3.3.1.min.js"></script>


<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">

 function GoogleMaps(Path)
  {
         var ua = window.navigator.userAgent; 
        var msie = ua.indexOf("MSIE "); 
        
        if (parseInt(msie) > 0 ) 
        {

            // If Internet Explorer, do you action
            var shell = new ActiveXObject("WScript.Shell");
            shell.run("chrome.exe " + Path);
            window.open("", "_self", "");
            window.close();
            return false;

        }
        else 
        {
        
//            $("#map").show();
            $("#divlinkbtn").hide();
            var pathtoopen = '';
            if (Path.substr(0,4).toLowerCase() == 'http')
                pathtoopen = Path;
            else
                pathtoopen = 'http://' + Path;

            window.open(pathtoopen,"_self")
            
//            $("#map").html("<iframe width='100%' height='600px' frameborder='0' scrolling='no' marginheight='0' marginwidth='0' src=" + pathtoopen +"></iframe>");
        }
  }
 

</script>
<head runat="server">
    <title>LR Status</title>
    <link href="CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager" runat="server"  />--%>
        <div id="divlinkbtn" style="height: 600px;">
            <table class="TABLE" width="100%">
                <tr>
                    <td align="left" valign="top">            
                        <iframe id="ifrm1" height="600px" width="100%" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" ></iframe>

                       <asp:LinkButton id="lnk_btnGoogleMap" runat="server" Text="Google Maps" Font-Bold="True" ForeColor="Purple" 
                       Font-Underline="true" Font-Size="11px" Font-Names="Verdana" OnClick="lnk_btnGoogleMap_Click" Visible="false"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div id="map" style="display:none;">
        </div>
    </form>
</body>
</html>
