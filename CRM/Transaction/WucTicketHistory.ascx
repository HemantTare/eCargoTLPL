<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTicketHistory.ascx.cs" Inherits="CRM_Transaction_WucTicketHistory" %>
<asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"  ></script>
 <script language="javascript" type="text/javascript" >
var lbl_Errors
function ValidtateUI()
{
    var ATS=false;
    lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
    var txt_Reply = document.getElementById("<%=txt_Reply.ClientID%>");

    if(!validTextBox(txt_Reply,'Reply')) {}
    else 
    {
       ATS=true;
    }
 return ATS; 
 
}


function validTextBox(txt_ID,Msg)
{
    if(Trim(txt_ID.value) == '')
    {  
           lbl_Errors.innerText = 'Please Enter'+' '+Msg;
           txt_ID.focus();  alert('xcv');
           return false;
    }
    return true;
}



function OpenWindow(url)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-400);
//    var leftPos = (w-popW)/2;
//    var topPos = (h-popH)/2;
    
    var leftPos =100;
    var topPos = 100;
                
    window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;
}
</script>




<table class="TABLE" style="width: 100%" border="0">
    <tr>
            <td class="TDGRADIENT" colspan="6">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL"
                 ></asp:Label>
            </td>
    </tr>
            <tr>
              <td>
               &nbsp;
              </td>
            </tr>
             
    <tr>
    
        <td  style="width: 100%" colspan="3">
        <table border="0" >
        <tr>
           <td style="width: 5%">
               &nbsp;
            </td>
            <td colspan="2" style="width: 55%">
             Complaint Nature :<asp:Label ID="lbl_ComplaintNature" runat="server" Font-Bold="true"></asp:Label>
            </td>
         </tr>  
         <tr>
           <td>
               &nbsp;
          </td>
        </tr>
        <tr>        
           <td style="width: 5%">
               &nbsp;
            </td>
            <td style="width: 55%" colspan="2">
              <asp:Button ID="btn_AssignUsers"  runat="server" Text="Assign To Users" CssClass="BUTTON" OnClick="btn_AssignUsers_Click"/>
              &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_CloseTicket"  runat="server" Text="Close Ticket" CssClass="BUTTON" OnClick="btn_CloseTicket_Click"/>
              &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_ComplaintAnalysis"  runat="server" Text="Complaint Analysis" CssClass="BUTTON" OnClick="btn_ComplaintAnalysis_Click"/>
              &nbsp;&nbsp;&nbsp; <asp:Button ID="btn_Attachments" runat="server" CssClass="BUTTON"/>
            </td>
          <%--  <td style="width: 20%">
            </td>
            <td style="width: 20%">
            </td>--%>
        </tr>
         <tr>
              <td>
               &nbsp;
              </td>
            </tr>
        <tr runat="server" id="tr_Reply">
            <td style="width: 4%" class="TD1" valign="top">
              Reply :
            </td>
            <td style="width: 55%" colspan="2">
                 <asp:TextBox ID="txt_Reply" runat="server" CssClass="TEXTBOX" MaxLength="1000"
                TextMode="MultiLine" Width="98%" Height="60px"></asp:TextBox>
            </td>
            <td class="TDMANDATORY" style="width: 1%" valign="top">*
            </td>
        </tr>
       
        <tr runat="server" id="tr_ReplyButton">
            <td style="width: 60%" colspan="3" align="center">
              <asp:Button ID="btn_Send"  runat="server" Text="Send" CssClass="BUTTON" OnClick="btn_Send_Click"/>
            </td>
        </tr>
        
         <tr>
            <td colspan="3" style="width: 60%">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
            </td>
         </tr>        
        
          
        </table>
        </td>
    </tr>
     <%--<tr>
      <td>
        &nbsp;
      </td>
     </tr>--%>
    <tr>
   <td style="width: 5%">&nbsp;</td>
    <td style="width: 45%" colspan="3" align="center">
     <fieldset>
    <legend></legend>
    
    <div class="DIV1">
    <asp:Repeater ID="rpt_History" runat="server">
    <ItemTemplate>
    <fieldset>
    <legend>History No <%#DataBinder.Eval(Container.DataItem, "Ticket_Sr_No1")%></legend>
    
      <table border="0" style="width: 97%">
           
            <tr>
                <td style="width: 20%" align="left">
                   <asp:Label ID="lbl_BranchCaption" Font-Bold="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Branch")%>'></asp:Label>
                </td>
                <td style="width: 15%" align="left">
                   <asp:Label ID="lbl_Prifile" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem, "Profile_Name")%>'></asp:Label>
                </td>
                <td style="width: 15%" class="TD1">
                   &nbsp;
               </td>
            </tr>
            
             <tr>
                <td style="width: 49%" colspan="3" align="left">
                  <asp:Label ID="lbl_CommentPostedBy" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CommentPostedBy")%>' Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            
            <tr>
                <td style="width: 49%" colspan="3">
                 <asp:TextBox ID="txt_Comments" runat="server" CssClass="TEXTBOX" ReadOnly="true" 
                TextMode="MultiLine" Width="100%" Height="60px" Text='<%#DataBinder.Eval(Container.DataItem, "Status_Description")%>'></asp:TextBox>
                </td>
                <td style="width: 1%">
                </td>
            </tr>
            <tr>
              <td>
               &nbsp;
              </td>
            </tr>
            
             
            
        </table>  
        </fieldset>  
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </fieldset>
    
    </td>
    
    <td style="width: 50%"> &nbsp;</td> 
    
    </tr>
    
     <tr>
              <td>
               &nbsp;
              </td>
            </tr>
            
             <tr>
              <td>
               &nbsp;
              </td>
            </tr>
</table>