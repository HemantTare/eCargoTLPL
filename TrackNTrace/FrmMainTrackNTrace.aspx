<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMainTrackNTrace.aspx.cs" Inherits="TrackNTrace_FrmMainTrackNTrace" %>

<%@ Register Src="WucMainTrackNTrace.ascx" TagName="WucMainTrackNTrace" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <%--<meta http-equiv="page-enter" content="blendtrans(duration=-1)" />--%>
    <%--<meta http-equiv="page-end" content="blendtrans(duration=-1)" />--%>
    <%--<meta http-equiv="page-exit" content="blendtrans(duration=-1)" />--%>
    
    <title>TRACK & TRACE</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
      .Heading{width: 100%; font-weight: bold; font-size: 12px; color:DarkBlue ; font-family: Verdana; background-color: White; text-align: center}
      .TopHeading{ font-weight: bold; font-size: 12px; color:DarkBlue; font-family: Verdana; background-color: White; }
      .SubHeading{font-weight: bold; font-size: 10px; color: Black; font-family: Verdana; background-color:white; text-align: center}
      .Feild{font-weight: bold; font-size: 11px; color: Black; font-family: Verdana; background-color: #F6F6F8; text-align: right}
      .AlphaValue{font-size: 11px; color: Black; font-family: Verdana; background-color:#F6F6F8; text-align: left}
      .NumericValue{font-size: 11px; color: Black; font-family: Verdana; background-color:#F6F6F8; text-align:right}
      .FeildNoBGColor{font-weight: bold; font-size: 11px; color: Black; font-family: Verdana; text-align: left}
      .AlphaValueNoBGColor{font-size: 11px; color: Black; font-family: Verdana;  text-align: left}
      .NumericValueNoBGColor{font-size: 11px; color: Black; font-family: Verdana;  text-align:right}
      .HeadingNoBGColor{width: 100%; font-weight: bold; font-size: 12px; color:DarkBlue ; font-family: Verdana;  text-align: center}
    </style>
    
<script type="text/javascript" language="javascript">
    
   
    function viewwindow_ddc(DelTypeID,DocNo)     //For Delivery Details
    {
        var Path='../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=DDC&Doc_No=' + DocNo +'&DeliveryType_Id='+ DelTypeID;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 400;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUp_Track_And_Traceddc', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    function viewwindow_pds(DelTypeID,DocNo)  //For PDS Delivery Details
    {
        var Path='../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=PDS&Doc_No=' + DocNo +'&DeliveryType_Id='+ DelTypeID;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 400;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUp_Track_And_Tracepds', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    function view_userdetails(userID)     //For User Information
    {
        var Path='../TrackNTrace/FrmTrackNTraceUserInformation.aspx?U_Id='+ userID;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-350);
        var popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'UserPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    function viewwindow(DocType,DocNo)
    {
        var Path='../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 400;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
            
          window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    function openVoucherWindow(DocNo)
    {
        var Path='../Finance/VoucherView/FrmVoucher.aspx?Id='+ DocNo;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    function view_otherchargedetails(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-450);
        var popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

          window.open(Path, 'othercharge', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
</script>
</head>
<body ><%--class="BODY"--%>
    <form id="form1" runat="server">
    <div>
        <uc1:WucMainTrackNTrace ID="WucMainTrackNTrace1" runat="server" />    
    </div>
    </form>
</body>
</html>
