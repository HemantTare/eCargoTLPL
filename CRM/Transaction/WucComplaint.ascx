<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucComplaint.ascx.cs" Inherits="CRM_WucComplaint" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"  ></script>
<script language="javascript" type="text/javascript" >
 var lbl_Errors
function ValidtateUI()
{
    var ATS=false;
    lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
    var txt_TicketNo = document.getElementById("<%=txt_TicketNo.ClientID%>");
    var ddl_NatureComplaint = document.getElementById("<%=ddl_NatureComplaint.ClientID%>");
    var txt_Name = document.getElementById("<%=txt_Name.ClientID%>");
    var btn_Save = document.getElementById("<%=btn_Save.ClientID%>");    
//    var txt_TelephoneNo = document.getElementById("<%=txt_TelephoneNo.ClientID%>");
//    var txt_Designation = document.getElementById("<%=txt_Designation.ClientID%>");
    var txt_EMailID = document.getElementById("<%=txt_EMailID.ClientID%>");
    var txt_ComplaintDetails = document.getElementById("<%=txt_ComplaintDetails.ClientID%>");
 
    if(!validTextBox(txt_TicketNo,'Ticket No')) {}    
    else if(!validDropDown(ddl_NatureComplaint,'Complaint Nature')) {}    
    else if(!validTextBox(txt_Name,'Name')) {}
    
//    else if(!validTextBox(txt_TelephoneNo,'Telephone No')) {}
//    
//    else if(!validTextBox(txt_Designation,'Designation')) {}

    else if(!validEMail(txt_EMailID,'Valid E-Mail ID')) {}
    else if(!validTextBox(txt_ComplaintDetails,'Complaint Details')) {}    
    else {
           ATS=true;
           btn_Save.value = 'Wait...';
           btn_Save.disabled = true;
           __doPostBack('WucComplaint1$btn_Save','');
         }
 return ATS;  
}

function validTextBox(txt_ID,Msg)
{
    if(Trim(txt_ID.value) == '')
    {  
           lbl_Errors.innerText = 'Please Enter'+' '+Msg;
           txt_ID.focus();  
           return false;
    }
    return true;
}
 
function validDropDown(ddl_ID,Msg)
{
      if(ddl_ID.selectedIndex <=0 || ddl_ID.options[ddl_ID.selectedIndex].value=='')
     {
        lbl_Errors.innerText = 'Please Select'+' '+Msg;
        ddl_ID.focus();
        return false;         
     }
  return true;        
}

  function validEMail(txt_EMail,Msg)
    {
        if (Trim(txt_EMail.value)!=''  && !(txt_EMail.value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1))
        {
           lbl_Errors.innerText = 'Please Enter'+' '+Msg;
           txt_EMail.focus();  
           return false;
         }
       return true;
  }  
  
   function Show_Ticket_History(Url)
    {     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
         window.open(Url, 'CustomPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
         return false;
    }

function OpenWindow(url)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-400);
    
    var leftPos =100;
    var topPos = 100;                
    window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;
}


</script>
 <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
<table class="TABLE" style="width: 100%">
    <tr>
            <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="COMPLAINT REGISTER"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">Ticket No :</td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_TicketNo" runat="server" CssClass="TEXTBOX" ReadOnly="true" Width="70%"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            </td>
        <td class="TD1" style="width: 20%">Ticket Date :</td>
        <td style="width: 29%">
            <asp:Label ID="lbl_TicketDate" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
        <td class="TDMANDATORY" style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_DocGcNo" CssClass="LABLE" runat="server"></asp:Label></td>
        <td style="width: 29%;" class="TDMANDATORY" >
            <cc1:DDLSearch ID="ddl_DocGcNo" runat="server" AllowNewText="false" OnTxtChange="ddl_DocGcNo_SelectedIndexChanged" PostBack="true" IsCallBack="true" CallBackFunction="Raj.EC.CRM.CallBack.GetSearchGcDoc"/>&nbsp;&nbsp;*
        </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
        <td class="TD1" style="width: 20%;">
         <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocGcNo"/>
                 </Triggers>
                 <ContentTemplate> 
                     <asp:Button ID="btn_TicketInfo" runat="server" Text="Earlier Complaints" CssClass="BUTTON"/>
                 </ContentTemplate>
         </asp:UpdatePanel>       
         </td>
         <td style="width: 29%;">         
             <asp:Button ID="btn_Attachments" runat="server" CssClass="BUTTON"/>            
         </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
    </tr>
    <tr>
        <td colspan="6">
         <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocGcNo"/>
                 </Triggers>
            <ContentTemplate> 
                <table style="width: 100%">
                    <tr>
                        <td style="width: 19%" class="TD1">
                      <asp:Label ID="lbl_DocGcDateLable" runat="server" CssClass="LABLE" ></asp:Label></td>
                        <td style="width: 29%">
                       <asp:Label ID="lbl_DocGcDate" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 20%" class="TD1">Commtd Dly Date :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_CommtdDlyDate" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 19%" class="TD1">Booking Station :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_BookingStation" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width:1%"></td>
                        <td style="width: 20%" class="TD1">Delivery Station :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_DeliveryStation" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>
                    </tr>
                    <tr>
                        <td style="width: 19%" class="TD1">
                       <asp:Label ID="lbl_PkgsCaption" runat="server" CssClass="LABLE"></asp:Label></td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_Pkgs" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>
                        <td style="width: 20%" class="TD1">Charged Weight :</td>
                         <td style="width: 29%">
                        <asp:Label ID="lbl_Weight" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>                        
                    </tr>
                    <tr>
                        <td style="width: 19%" class="TD1">Current Branch :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_CurrentBranch" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 20%" class="TD1">Current Status :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_CurrentStatus" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 19%" class="TD1">Delivery Date :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_DeliveryDate" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width:1%"></td>
                        <td style="width: 20%" class="TD1">Is DACC :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_IsDACC" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>
                    </tr>
                     <tr>
                        <td style="width: 19%" class="TD1">Is DOD :</td>
                        <td style="width: 29%">
                        <asp:Label ID="lbl_IsDOD" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width:1%"> </td>
                        <td style="width: 20%" class="TD1">
                        </td>
                        <td style="width: 29%">
                        <asp:Label ID="Label4" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 19%">
                            <asp:Label ID="lbl_BookingTypeCaption" runat="server" CssClass="LABLE" ></asp:Label></td>
                        <td style="width: 29%">
                            <asp:Label ID="lbl_BookingType" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%">
                        </td>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_DeliveryTypeCaption" runat="server" CssClass="LABLE" ></asp:Label></td>
                        <td style="width: 29%">
                            <asp:Label ID="lbl_DeliveryType" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 19%">
                            <asp:Label ID="lbl_PaymentTypeCaption" runat="server" CssClass="LABLE" ></asp:Label></td>
                        <td style="width: 29%">
                            <asp:Label ID="lbl_PaymentType" runat="server" CssClass="LABLE" Font-Bold="true"></asp:Label></td>
                        <td style="width: 1%"></td>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lbl_GcDocNoCaption" runat="server" CssClass="LABLE"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:LinkButton ID="lbtn_ViewTrackTrace" runat="server"></asp:LinkButton></td>
                        <td style="width: 1%"></td>
                    </tr>
                    <tr>
                        <td style="width: 19%" class="TD1" valign="top">Consignor Name,Address :</td>
                        <td style="width: 29%">
                        <asp:TextBox ID="txt_NorName" runat="server" CssClass="TEXTBOX" ReadOnly="true"
                            TextMode="MultiLine"  Height="60px"></asp:TextBox></td>
                        <td style="width: 1%">
                        </td>
                        <td style="width: 20%" class="TD1" valign="top">Consignee Name,Address :</td>
                        <td style="width: 29%">
                        <asp:TextBox ID="txt_NeeName" runat="server" CssClass="TEXTBOX" ReadOnly="true"
                            TextMode="MultiLine" Height="60px"></asp:TextBox></td>
                        <td style="width: 1%"></td>
                    </tr>
                    
                </table>
               </ContentTemplate>
          </asp:UpdatePanel> 
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">Nature Of Complaint :</td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_NatureComplaint" runat="server" CssClass="DROPDOWN"></asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
        <td style="width: 50%;" colspan="3">&nbsp;</td>
    </tr>
    <tr>
     <%--   <td colspan="6">
            <table style="width: 100%">
                <tr>--%>
                    <td class="TD1" style="width: 20%;">Name :</td>
                    <td style="width: 29%;">                    
                        <asp:TextBox ID="txt_Name" runat="server" CssClass="TEXTBOX" MaxLength="100" Width="97%"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">*</td>
                    <td class="TD1" style="width: 15%;">Telephone No :</td>
                    <td style="width: 34%;">
                    <asp:TextBox ID="txt_TelephoneNo" runat="server" onkeyup="valid(this)" onblur="valid(this)" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">Designation :</td>
                    <td style="width: 29%;">                     
                        <asp:TextBox ID="txt_Designation" runat="server" CssClass="TEXTBOX" MaxLength="50" Width="97%"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%;"></td>
                    <td class="TD1" style="width: 15%;">Mobile No :</td>
                    <td style="width: 34%;">
                       <asp:TextBox ID="txt_MobileNo" runat="server" onkeyup="valid(this)" onblur="valid(this)" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox>  
                    </td>                 
                   <td class="TDMANDATORY" style="width: 1%;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">Complaint By :</td>
                    <td style="width: 29%;">
                        <asp:DropDownList ID="ddl_CNeeNor" runat="server" CssClass="DROPDOWN">
                        <asp:ListItem Text="Consignor" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Consignee" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Internal" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%;">*</td>
                    <td class="TD1" style="width: 15%;">E-Mail ID :</td>
                    <td style="width: 34%;">
                       <asp:TextBox ID="txt_EMailID" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%;">&nbsp;</td>
                <%--</tr>
            </table>
        </td>--%>
    </tr>
    
    <tr>
      <td class="TD1" style="width: 20%">&nbsp;</td>
     
        <td colspan="2" style="width: 30%">
        <fieldset><legend>Select Priority</legend>
        <table width="50%">
            <tr>
            <td style="width:50%">
                  <asp:RadioButtonList ID="rdbl_Priority" runat="server" RepeatDirection="Horizontal">
                  </asp:RadioButtonList>
             </td>           
           </tr>
        </table>
        </fieldset>
        </td>      
        <td colspan="2" style="width:49%">
        <fieldset><legend>Complaint Type</legend>
        <table width="50%">
            <tr>
            <td style="width:50%">
                  <asp:RadioButtonList ID="rdbl_ComplaintType" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="0">Is Complaint</asp:ListItem>
                  <asp:ListItem Value="1" Selected="True">Is Query</asp:ListItem>                   
                  </asp:RadioButtonList>
             </td>           
           </tr>
        </table>
        </fieldset>
        </td>        
       <td class="TDMANDATORY" style="width: 1%;"></td>   
    </tr>    
    <tr visible="false" runat="server">
        <td class="TD1" style="width: 20%" valign="top" >Reason :</td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_UndeliveredReason" runat="server" CssClass="TEXTBOX" MaxLength="200" 
                TextMode="MultiLine" Width="100%" Height="60px"></asp:TextBox></td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%" valign="top">Complaint Details :</td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_ComplaintDetails" runat="server" CssClass="TEXTBOX" MaxLength="1000"
                  TextMode="MultiLine" Width="100%" Height="60px"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
    </tr>
    
      
     <tr>
        <td colspan="6">
            <%--<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                 </Triggers>
            <ContentTemplate> --%>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                 <%-- </ContentTemplate>
          </asp:UpdatePanel> --%>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
       <%--  <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                 </Triggers>
            <ContentTemplate> --%>
              <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"   OnClientClick="return ValidtateUI()"/>
           <%--  </ContentTemplate>
          </asp:UpdatePanel> --%>
        </td>
    </tr>
    <tr>
        <td colspan="6">         
            <asp:Label ID="Label1" CssClass="LABELERROR" runat="server" Text=" Fields with * mark are mandatory"></asp:Label>                 
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
        &nbsp;
         </td>
    </tr>
</table>
 
