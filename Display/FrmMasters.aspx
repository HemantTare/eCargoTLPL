<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMasters.aspx.cs" Inherits="Display_FrmMasters" %>

<%@ Register Src="../Bars/WucBottomBar.ascx" TagName="WucBottomBar" TagPrefix="uc2" %>

<%@ Register Src="../Bars/WucHeader.ascx" TagName="WucHeader" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="../Javascript/Display.js"></script>

<script type="text/javascript">

function hideload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'hidden'; 
    }
        
function displayload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'visible'; 
        loading.style.position='absolute';
        loading.style.left=(document.body.clientWidth/2)-20+'px';
        loading.style.top=(document.body.clientHeight/2)-60+'px';
    }
    
function LoadPage(url)
{
      if ( window.frames['frm'] ) 
        {
        displayload();
        window.frames['frm'].location = url;
        return false;
        } 
      else if ( document.layers ) 
       {
        displayload();
        document.layers['outer'].document.layers['inner'].src = url;
        return false;
       }
      else return true;
}


function LoadPopUp(Url)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = (h-100);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;

    window.open(Url, 'CustomPopUp<%=UserId%>', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')

}

function get_button_nullsession_clientid()
{
btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
</script>

<head runat="server">
    <title><%=Application["Title"]%></title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>

<body onload="hideload()" onunload = "clearvariables();" style="margin: 0px 0px;">
    <form id="form1" runat="server">
    
       <div style="position: absolute; font-size: 11px; font-family: Verdana;">
            <span id="loading">
                <img src="../Images/Bar_Circle.gif" />
                Loading .... 
            </span>
       </div>
       
       <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <uc1:WucHeader ID="WucHeader1" runat="server" />
                </td>
            </tr>
       </table>
        
        
                       
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                    
                    <td style="vertical-align: top; width: 10%; background-color: #BCBCBC">
                    <ComponentArt:NavBar id="NBMasters" Width="186"
                    CssClass="NAVBAR" 
                    DefaultItemLookID="TopItemLook"
                    DefaultItemSpacing="7"
                    ExpandTransition="None" 
                    ExpandDuration="200"
                    CollapseTransition="None"
                    CollapseDuration="200"
                    ExpandSinglePath="true"
                    ImagesBaseUrl="~/images"
                    runat="server" >


                    <ItemLooks>
                    <ComponentArt:ItemLook LookID="TopItemLook" CssClass="TOPITEM" HoverCssClass="TOPITEMHOVER" />
                    <ComponentArt:ItemLook LookID="Level2ItemLook" LabelPaddingLeft="10px" CssClass="LEVEL2ITEM" HoverCssClass="LEVEL2ITEMHOVER" LeftIconWidth="5px" LeftIconHeight="5px" />
                    <ComponentArt:ItemLook LookID="EmptyLook"/>
                    </ItemLooks>
                    </ComponentArt:NavBar>
                    </td>                    
                    
                    
                     <td style="width: 90%; height:450px; vertical-align: top;">
                          <iframe id="frm" name="frm" width="100%" height="100%" scrolling="yes"  frameborder="0" class="HideHorizontalScrol"> 

                            <ilayer name="outer">  
                                <layer name="inner" src="#" width="100%" height="600px">
                                
                                    <table style="width: 100%;" cellpadding="20" cellspacing="20">
                                    <tr>
                                        <td style="width: 100%;">
                                        </td>
                                    </tr>
                                    </table>
                                
                                </layer>
                            </ilayer>
                            </iframe>
                      </td>
                    </tr>
                </table>



       <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <uc2:WucBottomBar ID="WucBottomBar1" runat="server" />
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
                </td>
            </tr>
       </table>

       
    </form>
            <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>
</body>
</html>
