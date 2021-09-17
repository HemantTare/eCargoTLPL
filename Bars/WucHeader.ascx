<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucHeader.ascx.cs" Inherits="Bars_WucHeader" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript">
    function newwindow1(url)
    {
     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

          window.open(url, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=no,scrollbars=yes')
          return false;
    }


    function openwindow(url)
    {
     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-10);
        var popH = (h-100);       
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(url, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=yes,scrollbars=yes')
          return false;
    }

    function openwindowFleet(url)
    {
     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-10);
        var popH = (h-100);       
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(url, 'MainPopUp_Fleet', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=yes,scrollbars=yes')
          return false;
    }
    function onlogoutclick(url)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 700;
        var popH = 350;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(url, 'logouturl', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=yes,scrollbars=yes')
          return false;
    }    
</script>

<body>

<div id="Div1" style="position:absolute;width:100%;top:0;left:0;font-family:Verdana;font-size:12px;font-weight:bold;">
    <marquee id="MQ1" behavior="scroll"  scrolldelay="150" direction="left"><%=Application["Message"]%>
    </marquee>
</div> 

<table border="0" width="100%" cellpadding ="0" cellspacing="0">
    <tr>
        <td width="1%">
            <img align="top" src="../Images/LogoLeft.gif" />
        </td>
        <td  background="../Images/LogoMiddle.gif" width="98%" align="center">
            <%--<img align="top" src="../Images/LogoMiddle.gif" />  --%>     
            <asp:Label ID="lbl_companyName" runat="server" ForeColor="Brown" Font-Italic="true" Font-Size="Larger"></asp:Label>
        </td>
        <td width="1%">
            <img align="top" src="../Images/LogoRight.gif" />
        </td>
    </tr>
 </table>

<table class="HTABLE2" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td style="width: 95%;">            
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>                    
                        <td>  
                              <asp:Image ID="ImgAdmin" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgAdminResource1"/>
                              <asp:Label ID="lbl_division" Width="400px" runat="server" Text="DIVISION : "   ForeColor="Red" Font-Size="9px" Font-Bold="True"  Font-Names="verdana" style="left: 342px; position: absolute; top: 80px; z-index:300" meta:resourcekey="lbl_divisionResource1"></asp:Label>
                              <asp:Label ID="lbl_FY" Width="400px" runat="server" Text="FINANCIAL YEAR : "   ForeColor="Red" Font-Size="9px" Font-Bold="True"  Font-Names="verdana" style="left: 342px; position: absolute; top: 90px; z-index:200" meta:resourcekey="lbl_FYResource1"></asp:Label>
                              <asp:Label ID="lbl_login_details" Width="280px" runat="server" Text="LOGIN BRANCH:BHIWANDI | LOGIN USER:BO0004"   ForeColor="Red" Font-Size="9px" Font-Bold="True"  Font-Names="verdana" style="left: 700px; position: absolute; top: 90px; z-index:200" meta:resourcekey="lbl_login_detailsResource1"></asp:Label>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="lnk_Btn_Admin" runat="Server" Text="ADMINISTRATION" CssClass="LINKBUTTON" OnClick="lnk_Btn_Admin_Click" meta:resourcekey="lnk_Btn_AdminResource1"></asp:LinkButton>
                        </td>                    
                        <td>
                            <asp:Image ID="ImgMasters" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgMastersResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_Masters" runat="Server" Text="MASTERS" CssClass="LINKBUTTON" OnClick="Lnk_Btn_Masters_Click" meta:resourcekey="Lnk_Btn_MastersResource1"></asp:LinkButton>
                        </td>                        
                        <td>
                            <asp:Image ID="ImgOpr" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgOprResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_Opr" runat="Server" Text="OPERATIONS" CssClass="LINKBUTTON" OnClick="Lnk_Btn_Opr_Click" meta:resourcekey="Lnk_Btn_OprResource1"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Image ID="ImgFinance" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgFinanceResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_Finance" runat="Server" Text="FINANCE" CssClass="LINKBUTTON" OnClick="Lnk_Btn_Finance_Click" meta:resourcekey="Lnk_Btn_FinanceResource1"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Image ID="ImgFleet" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgFleetResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_Fleet" runat="Server" Text="FLEET" CssClass="LINKBUTTON" OnClick="Lnk_Btn_Fleet_Click" meta:resourcekey="Lnk_Btn_FleetResource1"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Image ID="ImgCRM" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgCRMResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_CRM" runat="Server" Text="CRM" CssClass="LINKBUTTON" OnClick="Lnk_Btn_CRM_Click" meta:resourcekey="Lnk_Btn_CRMResource1"></asp:LinkButton>
                        </td>                        
                        <td>
                            <asp:Image ID="ImgReports" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgReportsResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_Reports" runat="Server" Text="REPORTS" CssClass="LINKBUTTON" OnClick="Lnk_Btn_Reports_Click" meta:resourcekey="Lnk_Btn_ReportsResource1"></asp:LinkButton>
                        </td>                        
                        <td>
                            <asp:Image ID="ImgUserDesk" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="ImgUserDeskResource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="Lnk_Btn_User_Desk" runat="Server" Text="USER DESK" CssClass="LINKBUTTON" OnClick="Lnk_Btn_User_Desk_Click" meta:resourcekey="Lnk_Btn_User_DeskResource1"></asp:LinkButton>
                        </td> 
                        <td>
                            <asp:Image ID="Image1" runat="server" BorderWidth="0px" ImageUrl="~/Images/VBullet.gif" ImageAlign="Middle" meta:resourcekey="Image1Resource1"/>
                        </td>                        
                        <td style="vertical-align: top;">
                            <asp:LinkButton ID="lnk_track_and_trace" runat="Server" Text="TRACK & TRACE" CssClass="LINKBUTTON" meta:resourcekey="lnk_track_and_traceResource1"></asp:LinkButton>
                        </td>                         
                    </tr>
                </table>
            </td>
            <td style="width: 5%;text-align: right;">                
                <%--<a href="../Logout.aspx" class="LOGOUT">Logout</a>--%>&nbsp;
                <asp:LinkButton ID="Lnk_Btn_Logout" runat="Server" OnClick="Lnk_Btn_Logout_Click" Text="LOGOUT" CssClass="LINKBUTTON"></asp:LinkButton>

            </td>
        </tr>
    </table>

</body>
