<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_Alerts_Nandwana.ascx.cs"
    Inherits="Display_Alerts_Wuc_Alerts_Nandwana" %>

<script language="javascript" type="text/javascript">

function OpenIncomingTrucksAlert(IsFromDesktop)
  {
  var Path ='';
  Path='../Reports/CL_Nandwana/User Desk/FrmIncomingTrucksAlert_Nandwana.aspx?IsFromDesktop=' +IsFromDesktop ;
  var w = screen.availWidth;
  var h = screen.availHeight;
  var popW = w;
  var popH = h;
  var leftPos = (w-popW)/2;
  var topPos = (h-popH)/2;
  window.open(Path, 'incoming_truck_alerts_Window', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no,resizable=yes,scrollbars=yes');
  return false;
  }

function OpenIncompleteProcess()
  {
      var Path ='';
      Path='../Reports/CL_Nandwana/User Desk/FrmIncompleteProcess.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w;
      var popH = h;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'IncompleteProcess', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no,resizable=yes,scrollbars=yes');
      return false;
  }
  
function OpenCurrentStatistics()
  {
      var Path ='';
      Path='../Reports/CL_Nandwana/User Desk/FrmCurrentStatistics.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-650;
      var popH = h-370;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'CurrentStatistics', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=no,statusbar=no');
      return false;
  }
  
function DlyBranchwiseStock()
  {
      var Path ='';
      Path='../Reports/CL_Nandwana/User Desk/Frm_Bkg_BranchWise_Dly_Stock.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-450;
      var popH = h-250;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'BkgBrchWiseDlyStock', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }  
 
 function PendingDlyStock(Branch_Id)
  {
      var Path ='';
      
      Path='../Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock.aspx?Branch_Id=Branch_Id';
              
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-450;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DlyBrchWiseDlyStock', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
 function BranchwiseCashBalance()
  {
      var Path ='';
      Path='../Finance/Reports/Frm_BranchWiseClosingCash.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-150;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'BranchwiseCashBalance', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }  
 
  function eWayBillVerification()
  {
      var Path ='';
      Path='../Reports/Booking/frm_eWayBillVerification.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = 0;
      window.open(Path, 'eWayBillVerification', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
 
    
   function OpenF4Menu(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function OpenF4MenuSummaryAUS(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-400);
        var popH = (h-200);//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_Summary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function OpenF4MenuPDS(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-200);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
     function OpenF4MenuPDSSummary(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-600);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

function OpenF4MenuSummary(Path)
    {
        var w = screen.availWidth/1.5;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (popW);
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

  function ClientBusiness()
  {
      var Path ='';
      Path='../Reports/Sales Billing/FrmClientPreviousBusiness.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-200;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = 0;
      window.open(Path, 'ClientBusiness', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
    
  function DueForBilling(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-450;
      var popH = h-100;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DueForBilling', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 

  function Clientsearch()
  {
      var Path ='';
      Path='../Reports/CL_Nandwana/User Desk/FrmUserDeskClientSearch.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = 0;
      window.open(Path, 'ClientSearch', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 

    function TripExpenseApproval(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DueForBilling', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
    
  function TripAdvancePending(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-100;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DueForBilling', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function eWayBillVehicleUpdatePending(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'eWayBillVehicleUpdatePending', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }     
  
  function eWayBill_PartB_Updated_Vehicles(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'eWayBill_PartB_Updated_Vehicles', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }     
  
  function PendingTripSheet(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-100;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'PendingTripSheet', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function PendingChequeForDeposite(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-200;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'PendingChequeForDeposite', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function eWayBillVehicleUpdatePendingPDS(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'eWayBillVehicleUpdatePendingPDS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }   
  
  function eWayBill_PartB_Updated_Vehicles_PDS(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'eWayBill_PartB_Updated_Vehicles_PDS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function DlyBranchToPayRecovery(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DlyBranchToPayRecovery', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function CreditDebitCustomerBalance(Path)
  {
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'CreditDebitCustomerBalance', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function PendingDirectDelivery(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'PendingDirectDelivery', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

  function PendingDirectDeliveryDelivered(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-50);
        var popH = h-20;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'PendingDirectDeliveryDelivered', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }  
</script>

<table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr id="tr_lnkbtn" runat="server" align="left" valign="top">
        <td id="td_lnkbtn" runat="server" style="width: 40%; height: 266px;">
            <asp:Image ID="imgQuickLinks" runat="server" ImageUrl="~/Images/bulletH.gif" />
            <asp:Label ID="lbl_QuickLinks" runat="server" Font-Names="Verdana" Font-Size="12px"
                Font-Underline="True" Font-Bold="True" ForeColor="Black" Text="Quick Links" />
            <br />
            <asp:Image ID="imgClientSearch" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnClientSearch" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="true" Font-Bold="True" ForeColor="MediumBlue" Text="Search Client" /><br />
            <asp:Image ID="imgBillingDue" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_BillingDue" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#660000" Text="Due For Billing" />
            <asp:LinkButton ID="lnk_BillingDue" runat="server" Font-Names="Verdana" Font-Size="10px"
                Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" /><br />
            <asp:Image ID="imgCurrStat" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnCurrStat" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="True" ForeColor="#213163" Text="Current Statistics" />
            <br />
            <asp:Image ID="imgBranchwiseCashBalance" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnBranchwiseCashBalance" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" ForeColor="#213163" Text="Branchwise Cash Balance" />
            <br />
            <asp:Image ID="imgClientBusiness" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnClientBusiness" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="True" Font-Bold="True" ForeColor="#6600cc" Text="Client Last Business" />
            <br />
            <asp:Image ID="imgPendingTripSheet" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingTripSheet" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#003300" Text="Pending Trip Sheet" />
            <asp:LinkButton ID="lnk_btnPendingTripSheet" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgTripExpenseApproval" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_TripExpenseApproval" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#660000" Text="Trip Expense" />
            <asp:LinkButton ID="lnk_btnTripExpenseApproval" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgTripAdvancePending" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_TripAdvancePending" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#006699" Text="Advance To Driver" />
            <asp:LinkButton ID="lnk_btnTripAdvancePending" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="Img1" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnIncomingTruck" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="True" ForeColor="#213163" Text="Incoming Vehicle()" /><br />
            <asp:Image ID="imgIncPro" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnIncPro" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="True" Font-Bold="True" ForeColor="#6600cc" Text="Incomplete Process()" /></td>
        <td id="td1" runat="server" style="width: 60%; height: 266px;">
            <asp:Image ID="imgPending" runat="server" ImageUrl="~/Images/bulletH.gif" />
            <asp:Label ID="lbl_Pending" runat="server" Font-Names="Verdana" Font-Size="12px"
                Font-Underline="True" Font-Bold="True" ForeColor="Black" Text="Pending Process" />
            <br />
            <asp:Image ID="imgTripMemonPeding" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_TripMemoPending" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Trip Memo Pending" />
            <asp:LinkButton ID="lnk_TripMemoPendingCount" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgDirectDlyPending" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_DirectDlyPending" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Direct Dly Pending" />
            <asp:LinkButton ID="lnk_DirectDlyPending" runat="server" Font-Names="Verdana" Font-Size="10px"
                Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <asp:LinkButton ID="lnk_DirectDlyPendingDelivered" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="DarkGreen"
                Text="" />
            <br />
            <asp:Image ID="ImgPDS" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_btnPDS" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
                ForeColor="#6600cc" Text="Pending PDS" />
            <asp:LinkButton ID="lnk_btnPDS" runat="server" Font-Names="Verdana" Font-Size="10px"
                Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgInwardPending" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_InwardPending" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Inward Pending" />
            <asp:LinkButton ID="lnk_InwardPending" runat="server" Font-Names="Verdana" Font-Size="10px"
                Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgPendingDlyStock" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingDlyStock" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Dly Stock" />
            <asp:LinkButton ID="lnk_btnPendingDlyStock" runat="server" Font-Names="Verdana" Font-Size="10px"
                Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgDlyBranchwiseStock" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_DlyBranchwiseStock" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="My Pending Stock" />
            <asp:LinkButton ID="lnk_btnDlyBranchwiseStock" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgPendingDlyAddressDlyBrWise" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingDlyAddressDlyBrWise" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="#6600cc" Text="Pending Address Delivery" />
            <asp:LinkButton ID="lnk_btnPendingDlyAddressDlyBrWise" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <asp:LinkButton ID="lnk_btnUpdatedDlyAddressDlyBrWise" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="DarkGreen"
                Text="" />
            <br />
            <asp:Image ID="imgPendingDlyAddress" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingDlyAddress" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Pending Address Booking" />
            <asp:LinkButton ID="lnk_btnPendingDlyAddress" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <asp:LinkButton ID="lnk_btnUpdatedDlyAddress" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="DarkGreen"
                Text="" />
            <br />
            <asp:Image ID="imgeWayBillVerification" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btneWayBillVerificationH" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="DarkGreen" Text="eWay Bill Verification" />
            <asp:LinkButton ID="lnk_btneWayBillVerification" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgeWayBillVehicleUpdate" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_eWayBillVehicleUpdate" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#007878" Text="eWay Bill Vehicle Update" />
            <asp:LinkButton ID="lnk_btneWayBillVehicleUpdate" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            &nbsp;&nbsp;<asp:LinkButton ID="lnk_btnPartBUpdatedVehicles" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Green" Text="" />
            <br />
            <asp:Image ID="imgeWayBillVehicleUpdatePDS" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_eWayBillVehicleUpdatePDS" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="#ff6600" Text="eWay Bill Update PDS" />
            <asp:LinkButton ID="lnk_btneWayBillVehicleUpdatePDS" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            &nbsp;&nbsp;<asp:LinkButton ID="lnk_btnPartBUpdatedVehiclesPDS" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Green" Text="" />
            <br />
            <asp:Image ID="imgeWayBillExpiry" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btneWayBillExpiryH" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#ff0033" Text="eWay Bills Expiring Today" />
            <asp:LinkButton ID="lnk_btneWayBillExpiry" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Underline="True" Font-Bold="True" ForeColor="Black" Text="" />
            <br />
            <asp:Image ID="imgPendingDeliveryFreight" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingDeliveryFreight" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Pending Dly Freight" />
            <asp:LinkButton ID="lnk_PendingDeliveryFreight" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgPendingBookingFreight" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingBookingFreight" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#6600cc" Text="Pending Bkg Freight" />
            <asp:LinkButton ID="lnk_PendingBookingFreight" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgPendingChequeForDeposite" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:Label ID="lbl_PendingChequeForDeposite" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="#000066" Text="Pending Cheques" />
            <asp:LinkButton ID="lnk_btnPendingChequeForDeposite" runat="server" Font-Names="Verdana"
                Font-Size="10px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
            <br />
            <asp:Image ID="imgDlyBranchWiseToPayRecovery" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_DlyBranchWiseToPayRecovery" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="#cc0099" Text="Dly Branch ToPay Recovery" />
            <br />
            <asp:Image ID="imgDebtorsBalance" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_DebtorsBalance" runat="server" Font-Names="Verdana" Font-Size="11px"
                Font-Bold="True" ForeColor="#990000" Text="Credit-Debit Customer Balance" />
            <br />
            <asp:Image ID="imgPreveWayBillVerification" runat="server" ImageUrl="~/Images/bullet.gif" />
            <asp:LinkButton ID="lnk_btnPreveWayBillVerificationH" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Bold="True" ForeColor="DarkGreen" Text="Previous Day Unverified eWay Bills" />
            <asp:LinkButton ID="lnk_btnPreveWayBillVerification" runat="server" Font-Names="Verdana"
                Font-Size="11px" Font-Underline="True" Font-Bold="True" ForeColor="Red" Text="" />
        </td>
    </tr>
</table>
