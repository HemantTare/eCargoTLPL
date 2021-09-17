<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegistrationFitness.ascx.cs" Inherits="Master_Vehicle_WucRegistrationFitness" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx"TagName="WucDatePicker" TagPrefix="uc1" %>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>


<script type="text/ecmascript">
function ValidateUI_RegistrationFitness()
{
  var ATS=false;
  var txt_RegistrationCertificateNo=document.getElementById('<%=txt_RegistrationCertificateNo.ClientID%>');
  var ddl_RegistrationState=document.getElementById('<%=ddl_RegistrationState.ClientID%>');
  var ddl_RTO=document.getElementById('<%=ddl_RTO.ClientID%>');
  var txt_RegistrationFee=document.getElementById('<%=txt_RegistrationFee.ClientID%>');
  var txt_FitnessCertificateNo=document.getElementById('<%=txt_FitnessCertificateNo.ClientID%>');
  var ddl_FitnessRTO=document.getElementById('<%=ddl_FitnessRTO.ClientID%>');
  var txt_Amount=document.getElementById('<%=txt_Amount.ClientID%>')
  var lbl_Errors=document.getElementById('<%=lbl_Errors.ClientID %>');
 
  if(txt_RegistrationCertificateNo.value=='')
  {
    lbl_Errors.innerText="Please Enter Registration Certificate No";//objResource.GetMsg("MsgRegistraionCertificateNo");
    txt_RegistrationCertificateNo.focus();
  }
  else if(ddl_RegistrationState.value <=0 || ddl_RegistrationState.options.length == 0)
  {
   lbl_Errors.innerText="Please Select Registration State";//objResource.GetMsg("MsgRegistrationState");
   ddl_RegistrationState.focus();
  }
  else if(ddl_RTO.value <=0 || ddl_RTO.options.length == 0)
  {
  lbl_Errors.innerText="Please Select Registraion RTO";//objResource.GetMsg("MsgRegistraionRto");
  ddl_RTO.focus();
  }
  else if(txt_RegistrationFee.value=='')
  {
   lbl_Errors.innerText="Please Enter Registration Fee";// objResource.GetMsg("MsgRegistrationFee");
   txt_RegistrationFee.focus();
  }
  else if(txt_FitnessCertificateNo.value=='')
  {
   lbl_Errors.innerText="Please Enter Fitnes Certificate No";//objResource.GetMsg("MsgFitnesCertificateNo");
   txt_FitnessCertificateNo.focus();
  }
  else if(ddl_FitnessRTO.value <=0 || ddl_FitnessRTO.options.length == 0)
  {
   lbl_Errors.innerText= "Please Select Fitness RTO";// objResource.GetMsg("MsgFitnessRto");
   ddl_FitnessRTO.focus();
  }
  else if(txt_Amount.value=='')
  {
   lbl_Errors.innerText= "Please Enter Amount";// objResource.GetMsg("MsgAmount");
   txt_Amount.focus();
  }
  else
  {
   ATS=true;
  }
  
  return ATS;
}
</script>

<table class="TABLE" width="100%">
   <tr>
        <td colspan="6">&nbsp;</td> 
    </tr>
    
   <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
            <asp:Panel ID="pnl_Registration_Details" runat="server"  GroupingText="Registration Details" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_Registration_DetailsResource1" >
                
                <table  cellpadding="3" cellspacing="3" border="0" width="100%">
                    <tr>
                       <td style="width:20%" class="TD1">
                       <asp:Label ID = "lbl_Registration_Date" runat="server" Text="Registration Date :" meta:resourcekey="lbl_Registration_DateResource1"></asp:Label>
                       </td>
                       <td style="width:29%"><uc1:WucDatePicker ID="DtpRegistrationDate" runat="server" /></td>
                       <td style="width:1%"></td>
                       <td style="width:20%" class="TD1">
                       <asp:Label ID = "lbl_Registration_Certificate_No" runat="server" Text="Registration Certificate No :" meta:resourcekey="lbl_Registration_Certificate_NoResource1"></asp:Label>
                       </td>
                       <td style="width:29%" class="TD1"><asp:TextBox ID="txt_RegistrationCertificateNo" Width="98%" runat="server" MaxLength="25" CssClass="TEXTBOX" meta:resourcekey="txt_RegistrationCertificateNoResource1"></asp:TextBox> </td>
                       <td style="width:1%" class="TDMANDATORY">*</td>
                    </tr>
                    <tr>
                        <td style="width:20%" class="TD1">
                        <asp:Label ID = "lbl_Registration_State" runat="server" Text="Registration State :" meta:resourcekey="lbl_Registration_StateResource1"></asp:Label>
                        </td>   
                        <td style="width:29%" class="TD1"><asp:DropDownList ID="ddl_RegistrationState" runat="server" Width="100%" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_RegistrationState_SelectedIndexChanged" meta:resourcekey="ddl_RegistrationStateResource1"></asp:DropDownList> </td>
                        <td style="width:1%"  class="TDMANDATORY">*</td>   
                        <td style="width:20%" class="TD1">
                        <asp:Label ID = "lbl_RTO" runat="server" Text="RTO :" meta:resourcekey="lbl_RTOResource1"></asp:Label>
                        </td>   
                        <td style="width:29%" class="TD1">
                            <asp:UpdatePanel ID="UpdatePanleRegstrationDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_RTO"  Width="100%" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_RTOResource1"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="ddl_RegistrationState" />
                             </Triggers>
                            </asp:UpdatePanel> </td>
                             
                        <td style="width:1%"  class="TDMANDATORY">*</td>   
                    </tr>
                    <tr>
                       <td style="width:20%" class="TD1">
                       <asp:Label ID = "lbl_Registration_Fee" runat="server" Text="Registration Fee :" meta:resourcekey="lbl_Registration_FeeResource1"></asp:Label>
                       </td>
                       <td style="width:29%" class="TD1"><asp:TextBox ID="txt_RegistrationFee" onkeypress="return Only_Integers(this,event)" Width="99%" runat="server" MaxLength="18" CssClass="TEXTBOXNOS" meta:resourcekey="txt_RegistrationFeeResource1"></asp:TextBox> </td>
                       <td style="width:1%" class="TDMANDATORY">*</td>
                       <td style="width:20%"></td>
                       <td style="width:29%"></td>
                       <td style="width:1%"></td>
                    </tr>   
                </table>
           </asp:Panel>    
        </td>
    </tr>
    </table>
    </td>
    </tr>
     <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
         <asp:Panel ID="pnl_Fitness_Details" runat="server"  GroupingText="Fitness Details" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_Fitness_DetailsResource1" >
        
         <table cellpadding="3" cellspacing="3" border="0" width="100%">
            <tr>
                <td style="width:20%" class="TD1">
                <asp:Label ID = "lbl_Fitness_Certificate_No" runat="server" text="Fitness Certificate No :" meta:resourcekey="lbl_Fitness_Certificate_NoResource1"></asp:Label></td>
                <td style="width:29%"><asp:TextBox ID="txt_FitnessCertificateNo" Width="99%"  runat="server" CssClass="TEXTBOX" MaxLength="25" meta:resourcekey="txt_FitnessCertificateNoResource1"></asp:TextBox> </td>
                <td style="width:1%" class="TDMANDATORY">*</td>
                <td style="width:20%" class="TD1">
                <asp:Label ID = "lbl_RTO_Fitness" runat="server" Text="RTO :" meta:resourcekey="lbl_RTO_FitnessResource1"></asp:Label></td>
                <td style="width:29%" class="TD1">
                    <asp:UpdatePanel ID="UpdatePanelFitnessRto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_FitnessRTO" runat="server"  Width="99%" CssClass="DROPDOWN" meta:resourcekey="ddl_FitnessRTOResource1">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_RegistrationState" />
                        </Triggers>
                    </asp:UpdatePanel> </td>
                <td style="width:1%" class="TDMANDATORY">*</td>  
           </tr>
           <tr>
                <td style="width:20%" class="TD1">
                <asp:Label ID="lbl_Issue_Date" runat="server" Text="Issue Date :" meta:resourcekey="lbl_Issue_DateResource1"></asp:Label></td> 
                <td style="width:29%"><uc1:WucDatePicker ID="DtpIssueDate" runat="server" /></td> 
                <td style="width:1%"  class="TDMANDATORY">*</td>  
                <td style="width:20%" class="TD1">
                <asp:Label ID="lbl_Valid_Upto" runat="server" Text="Valid UpTo :" meta:resourcekey="lbl_Valid_UptoResource1"></asp:Label></td> 
                <td style="width:29%"><uc1:WucDatePicker ID="DtpValidUpTo" runat="server" /></td> 
                <td style="width:1%" class="TDMANDATORY">*</td>  
          </tr>
          <tr>
                <td style="width:20%" class="TD1">
                <asp:Label ID="lbl_Amount" runat="server" Text="Amount :" meta:resourcekey="lbl_AmountResource1"></asp:Label></td> 
                <td style="width:29%"><asp:TextBox ID="txt_Amount"  Width="99%" runat="server" onkeypress="return Only_Integers(this,event)"  CssClass="TEXTBOXNOS" MaxLength="18" meta:resourcekey="txt_AmountResource1"></asp:TextBox> </td> 
                <td style="width:1%"  class="TDMANDATORY">*</td>  
                <td colspan="3"></td>
          </tr>      
                  
         </table>
       </asp:Panel>  
      </td>
   </tr>
   </table>
   </td>
   </tr>
   <tr>
     <td colspan="6">
         <%--<asp:UpdatePanel ID="UpdatePanellblError" runat="Server" UpdateMode="Conditional">
             <ContentTemplate>
                 <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
             </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                 <asp:AsyncPostBackTrigger ControlID="ddl_RegistrationState" />
             </Triggers>
         </asp:UpdatePanel>--%>
         <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
     </td>
   </tr>
   <%--<tr>
     <td colspan="6" style="text-align:center">
      <asp:Button ID="btn_Save" runat="Server" Text="Save" OnClientClick="return ValidateUI()"  CssClass="BUTTON" OnClick="btn_Save_Click" />
    </td>
   </tr>--%>
              
</table>
