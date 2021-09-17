<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_CRM_Track_And_Trace.aspx.vb" Inherits="CRM_Queries_frm_CRM_Track_And_Trace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Application("Title")%></title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Validate()
        {
            var ATS = false;
            var lbl_Error = document.getElementById('lbl_Error');
            var txt_Trace_No = document.getElementById('txt_Trace_No');
            var rbtnl_0 = document.getElementById('rbtnl_0');
            var tr_Ticket_Details = document.getElementById('tr_Ticket_Details');
            var tr_GC_Details = document.getElementById('tr_GC_Details');

            if(rbtnl_0.value == '0' && txt_Trace_No.value <= '0' || txt_Trace_No.value == '')
            {
                lbl_Error.innerHTML = 'Please enter trace no';
                txt_Trace_No.focus();
            }
            else
            {
                ATS = true;
            }
            return ATS;
        }              
 
        
        function GetGCDetails(GCNo)
        { 
            var path = "../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC&Doc_No="+ GCNo;

            var w = screen.availWidth;
            var h = screen.availHeight;
            var popW = (w-100);
            var popH = (h-100);
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(path,'GCDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
            return false;
        } 
        
        function open_ticket_history(ticketid)
        { 
            var path = "../Transaction/FrmTicketHistory.aspx?Id="+ ticketid +"&Type=TwB0AGgAZQByAA==";

            window.open(path,'HistoryTicket', 'toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=yes, width=850, height=600,left=50,top=20');
            return false;
        } 
        
        function viewwindow_general(Id)
        {
            var Path='../../CRM/Transaction/FrmDisplayInfo.aspx?Ticket_Id=' + Id +  '&Type=QQBzAHMAaQBnAG4AZQBkAFUAcwBlAHIASQBuAGYAbwA=';

            var w = screen.availWidth;
            var h = screen.availHeight;
            var popW = (w-150);
            var popH = 500;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
                    
            window.open(Path, 'UserDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" >
      <table class="TABLE">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_CRM_T_n_T_Header" runat="server" CssClass="HEADINGLABEL" Text="CRM TRACK AND TRACE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:50%" colspan="3">                
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbtnl" runat="server" RepeatDirection="horizontal" AutoPostBack="False">
                                   <asp:ListItem Value="0" Selected ="True" ></asp:ListItem>
                                   <asp:ListItem Value="1" Text="Ticket Wise"  ></asp:ListItem>
                                </asp:RadioButtonList></td>                           
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <asp:TextBox ID="txt_Trace_No" runat="server" MaxLength="20" BorderWidth="1"  CssClass="TEXTBOX"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="Server" Text="Show" CssClass="BUTTON" OnClientClick="return Validate();" />
                            </td>
                        </tr>
                        <tr runat="server" id="TR_TicketNo">
                            <td style="width:50%">
                                <asp:DropDownList ID="ddl_TicketNo" runat="server" AutoPostBack="True" CssClass="DROPDOWN"></asp:DropDownList>
                            </td>
                            <td>                                
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="3" style="width:50%"><asp:Label ID="lbl_status" ForeColor="SaddleBrown" Font-Bold="True" runat="server" Font-Size="X-Large"></asp:Label></td> 
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr id="tr_Ticket_Details" runat="server">
                <td colspan="6">
                    <asp:Panel ID="pnl_Ticket" Width="100%" runat="server" GroupingText="Ticket Details" >
                        <table style="width:100%">
                            <tr>
                                <td class="TD1" style="width:20%">Ticket No:</td>
                                <td style="width:29%">
                                    <asp:LinkButton ID="lbtn_TicketNo" Font-Bold="true" Text="linkbutton" runat="server" CssClass="LABEL"></asp:LinkButton>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Ticket Date:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Ticket_Date" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width:20%">Ticket Status:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Ticket_Status" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Ticket Priority:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Ticket_Priority" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr>                               
                                <td class="TD1" style="width:20%">Pending Hours:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Pending_Hours" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%"></td>
                                <td class="TD1" style="width:29%"></td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width:20%">Currently Assigned To:</td>
                                <td style="width:29%">
                                    <asp:LinkButton ID="lbtn_Assigned_To" Font-Bold="true" Text="linkbutton" runat="server" CssClass="LABEL"></asp:LinkButton>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Next Escalated To:</td>
                                <td style="width:29%">
                                    <asp:LABEL ID="lbtn_Next_Escalated_To" Font-Bold="true" Text="linkbutton" runat="server" CssClass="LABEL"></asp:LABEL>
                                </td>
                                <td class="TD1" style="width:1%"></td>                                
                            </tr>
                             <tr>
                                <td class="TD1" style="width:20%">Complaint By:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Complaint_By" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Name:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Name" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>                              
                            </tr>
                        </table>
                     </asp:Panel>
                </td>
                     
            </tr>
            <tr id="tr_GC_Details" runat="server">
                <td colspan="6">
                    <asp:Panel ID="pnl_gc_Details" Width="100%" runat="server">
                        <table style="width:100%">
                            <tr>
                                <td class="TD1" style="width:20%">
                                    <asp:Label ID="lbl_TD_GC_No" runat="server" CssClass="LABEL"></asp:Label></td>
                                <td style="width:29%">
                                    <asp:LinkButton ID="lbtn_GC_No" Font-Bold="true" Text="linkbutton" runat="server" CssClass="LABEL"></asp:LinkButton>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">
                                   <asp:Label ID="lbl_TD_GC_Date_Time" runat="server" CssClass="LABEL"></asp:Label>
                                </td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_GC_Date_Time" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width:20%; height: 15px;">Booking Branch:</td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_Booking_Branch" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td>
                                <td class="TD1" style="width:20%; height: 15px;">Delivery Branch:</td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_Delivery_Branch" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width:20%">No of Packages:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_No_Of_Packeges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Charge Weight:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Charge_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width:20%; height: 15px;">
                                    <asp:Label ID="lbl_TD_GC_Status" runat="server" CssClass="LABEL"></asp:Label>
                                </td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_GC_Status" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td>
                                <td class="TD1" style="width:20%; height: 15px;">
                                    <asp:Label ID="lbl_TD_GC_Current_Branch" runat="server" CssClass="LABEL"></asp:Label>
                                </td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_GC_Current_Branch" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td> 
                            </tr>
                             <tr>
                                <td class="TD1" style="width:20%; height: 15px;">Consignor Name:</td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_Consignor_Name" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td>
                                <td class="TD1" style="width:20%; height: 15px;">Consignee Name:</td>
                                <td style="width:29%; height: 15px;">
                                    <asp:Label ID="lbl_Consignee_Name" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%; height: 15px;"></td>                               
                            </tr>
                             <tr>
                                <td class="TD1" style="width:20%">Delivery Date:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Delivery_Date_Time" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Remarks:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                             <tr>
                                <td class="TD1" style="width:20%">
                                    <asp:Label ID="lbl_BookingText" runat="server" CssClass="LABEL" Text= "Booking Type:"></asp:Label></td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_BookingType" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:20%">Payment Type:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_PaymentType" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                            </tr>
                            <tr runat="server" id="tr_DeliveryType">
                                <td class="TD1" style="width:20%">Delivery Type:</td>
                                <td style="width:29%">
                                    <asp:Label ID="lbl_DeliveryType" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="TD1" style="width:1%"></td>
                                <td class="TD1" style="width:50%" colspan="3"></td>                              
                            </tr>
                        </table>
                     </asp:Panel>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <table id="tr_Trace" enableviewstate="true" runat="server" class="TABLE">
                       
                    </table>
                </td>
            </tr>--%>

            <tr>
                <td>
                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="true" ForeColor="red" ></asp:Label>
                    <asp:HiddenField ID="hdn_GC_Id" runat="server" />  
                    <asp:HiddenField ID="hdn_TicketId" runat="server" />  
                </td>
            </tr>
            
      </table>
    </form>
</body>
</html>
