<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleLoanDetails.ascx.cs" Inherits="Master_Vehicle_WucVehicleLoanDetails" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<script language="javascript" type="text/javascript">
 
 var lbl_Errors;
 var tabControl
 var ID;
  function validateUI_VehicleLoanDetails(lbl_Errors)
{
     var ATS=false;
     var ddl_Bank_Name = document.getElementById("<%=ddl_Bank_Name.ClientID %>");
     var txt_Loan_Acct_No = document.getElementById("<%=txt_Loan_Acct_No.ClientID %>");
     var txt_Loan_Amount = document.getElementById("<%=txt_Loan_Amount.ClientID %>");
     var txt_Terms_In_Months = document.getElementById("<%=txt_Terms_In_Months.ClientID %>");
     var txt_Rate_Of_Interest = document.getElementById("<%=txt_Rate_Of_Interest.ClientID %>");
     var txt_EMI_Amount = document.getElementById("<%=txt_EMI_Amount.ClientID %>");
     var ddl_Interest_Type = document.getElementById("<%=ddl_Interest_Type.ClientID %>");
     var ddl_Payment_Mode = document.getElementById("<%=ddl_Payment_Mode.ClientID %>");
 
     var txt_Comments = document.getElementById("<%=txt_Comments.ClientID %>");
     var ddl_Payment_Bank = document.getElementById("<%=ddl_Payment_Bank.ClientID %>");
     var txt_Start_Cheque_No = document.getElementById("<%=txt_Start_Cheque_No.ClientID %>");
   
     lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
//     tabControl=tabControl;
//     ID=ID;
  
    if(!validDropDown(ddl_Bank_Name,'Bank Name')) {}
     
    else if(!validTextBox(ddl_Bank_Name,txt_Loan_Acct_No,'Loan Acct No')) {}

    else if(!validTextBox(ddl_Bank_Name,txt_Loan_Amount,'Loan Amount')) {}

    else if(!validTextBox(ddl_Bank_Name,txt_Terms_In_Months,'Terms In Months')) {}
    
    else if(!validTextBox(ddl_Bank_Name,txt_Rate_Of_Interest,'Rate Of Interest')) {}
    
    else if(!validTextBox(ddl_Bank_Name,txt_EMI_Amount,'EM Amount')) {}
    
    else  if(!validDropDown(ddl_Bank_Name,ddl_Interest_Type,'Interest Type')) {}
    
    else  if(!validDropDown(ddl_Bank_Name,ddl_Payment_Mode,'Payment Mode')) {}
    
    else  if(!validDropDown(ddl_Bank_Name,ddl_Payment_Bank,'Payment Bank')) {}

    else if(ddl_Payment_Mode.value==2 && !validTextBox(ddl_Bank_Name,txt_Start_Cheque_No,'Start Cheque No')) {} //Not For Cash

    else if(ddl_Payment_Mode.value==2 && txt_Start_Cheque_No.value.length < '6')
    {
           lbl_Errors.innerText = 'Cheque No should be Greater than 5 Digits';
           txt_Start_Cheque_No.focus();
           return false;
    }
     
    else if(parseFloat(txt_Rate_Of_Interest.value) > 100)
        {
           lbl_Errors.innerText = 'Rate Of Interest Can not be Greater Than 100%';
           txt_Rate_Of_Interest.focus();
           return false;
        }
        
    else ATS=true;

 return ATS; 
 
}
    function validTextBox(ddl_Bank_Name,txt_ID,Msg)
    {
        var lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
 
          if(ddl_Bank_Name.value !="0" && (Trim(txt_ID.value) == '' || Trim(txt_ID.value) == '0'))
        {  
               lbl_Errors.innerText = 'Please Enter'+' '+Msg;
              //lbl_Errors.innerText=objResource.GetMsg("+Msg");
               //tabControl.SelectTabById(ID);
               txt_ID.focus();  
               return false;
        }
      return true;
    }


   function validDropDown(ddl_Bank_Name,ddl_ID,Msg)
    {
     var lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
    
         if(ddl_Bank_Name.value !="0" && (ddl_ID.selectedIndex==-1 || ddl_ID.value=='0'))
         {
            lbl_Errors.innerText = 'Please Select'+' '+Msg;
            //lbl_Errors.innerText=objResource.GetMsg("+Msg");
            // tabControl.SelectTabById(ID);
            ddl_ID.focus();
            return false;
             
         }
      return true;        
    }
    
    
//  function validFollowUpdate(FirstPaymentDue_Date)
//  {
//    if (FirstPaymentDue_Date < Date())
//        {
//        lbl_Errors.innerText = "FollowUp date could not be less than Enquiry date.";
//        tabControl.SelectTabById(ID);
//       
//        return false; 
//        }
//        
//    return true;
//  }


 
 function VisibleForPaymentMode()
{
var ddl_Payment_Mode = document.getElementById("<%=ddl_Payment_Mode.ClientID %>");
    var txt_Start_Cheque_No = document.getElementById("<%=txt_Start_Cheque_No.ClientID %>");
   var lbl_StartChequeNo = document.getElementById("<%=lbl_StartChequeNo.ClientID %>");

    
   if(ddl_Payment_Mode.value==2)
   {
       txt_Start_Cheque_No.style.visibility="visible";
       lbl_StartChequeNo.style.visibility="visible";
   }
   else
   {
       txt_Start_Cheque_No.style.visibility="hidden";
       lbl_StartChequeNo.style.visibility="hidden";
       txt_Start_Cheque_No.value = '';
   }
 
}

function SetDateToLable()
 { 
   var lbl_Last_Payment_Due = document.getElementById("<%=lbl_Last_Payment_Due.ClientID %>");
   var txt_Terms_In_Months = document.getElementById("<%=txt_Terms_In_Months.ClientID %>");
      
  var FirstPaymentDue_Date=new Date();
  FirstPaymentDue_Date = <%=dtp_FirstPaymentDue.PickerClientID %>.GetSelectedDate();
  var FirstPaymentDue_Date1=new Date();
  FirstPaymentDue_Date1.setDate(FirstPaymentDue_Date.getDate());
  FirstPaymentDue_Date1.setFullYear(FirstPaymentDue_Date.getFullYear());
  FirstPaymentDue_Date1.setMonth(FirstPaymentDue_Date.getMonth()+Math.ceil(txt_Terms_In_Months.value)-1);
  lbl_Last_Payment_Due.innerHTML=FirstPaymentDue_Date1.toDateString();
    
 }
 function Enable_LoanDetails()
 {
 
  var ddl_BankName=document.getElementById("<%=ddl_Bank_Name.ClientID%>");
  var TDMandatory_BankName=document.getElementById("<%=TDMandatory_BankName.ClientID%>");
  var TDMandatory_LoanAcctNo=document.getElementById("<%=TDMandatory_LoanAcctNo.ClientID%>");
  var TDMandatory_LoanAmount=document.getElementById("<%=TDMandatory_LoanAmount.ClientID%>");
  var TDMandatory_TermsInMonths=document.getElementById("<%=TDMandatory_TermsInMonths.ClientID%>");
  var TDMandatory_RateOfInterest=document.getElementById("<%=TDMandatory_RateOfInterest.ClientID%>");
  var TDMandatory_EmiAmount=document.getElementById("<%=TDMandatory_EmiAmount.ClientID%>");
  var TDMandatory_InterestType=document.getElementById("<%=TDMandatory_InterestType.ClientID%>");
  var TDMandatory_PaymentMode=document.getElementById("<%=TDMandatory_PaymentMode.ClientID%>");
  var TDMandatory_FirstPaymentDue=document.getElementById("<%=TDMandatory_FirstPaymentDue.ClientID%>");
  var Tr_btn_Generate=document.getElementById("<%=Tr_btn_Generate.ClientID%>");
  
  var txt_Loan_Acct_No=document.getElementById("<%=txt_Loan_Acct_No.ClientID%>");
  var txt_Loan_Amount=document.getElementById("<%=txt_Loan_Amount.ClientID%>");
  var txt_Terms_In_Months=document.getElementById("<%=txt_Terms_In_Months.ClientID%>");
  var txt_Rate_Of_Interest=document.getElementById("<%=txt_Rate_Of_Interest.ClientID%>");
  var txt_EMI_Amount=document.getElementById("<%=txt_EMI_Amount.ClientID%>");
  var ddl_Interest_Type=document.getElementById("<%=ddl_Interest_Type.ClientID%>");
  var ddl_Payment_Mode=document.getElementById("<%=ddl_Payment_Mode.ClientID%>");
  var txt_Comments=document.getElementById("<%=txt_Comments.ClientID%>");
  var ddl_Payment_Bank=document.getElementById("<%=ddl_Payment_Bank.ClientID%>");
  var txt_Start_Cheque_No=document.getElementById("<%=txt_Start_Cheque_No.ClientID%>");
  var lbl_Last_Payment_Due=document.getElementById("<%=lbl_Last_Payment_Due.ClientID%>");
  
    if(ddl_BankName.value==0)
       {
                  TDMandatory_BankName.style.visibility="hidden";
                  TDMandatory_LoanAcctNo.style.visibility="hidden";
                  TDMandatory_LoanAmount.style.visibility="hidden";
                  TDMandatory_TermsInMonths.style.visibility="hidden";
                  TDMandatory_RateOfInterest.style.visibility="hidden";
                  TDMandatory_EmiAmount.style.visibility="hidden";
                  TDMandatory_InterestType.style.visibility="hidden";
                  TDMandatory_PaymentMode.style.visibility="hidden";
                  TDMandatory_FirstPaymentDue.style.visibility="hidden";
                  Tr_btn_Generate.style.visibility="hidden";
                  
                  txt_Loan_Acct_No.disabled=true;
                  txt_Loan_Amount.disabled=true;
                  txt_Terms_In_Months.disabled=true;
                  txt_Rate_Of_Interest.disabled=true;
                  txt_EMI_Amount.disabled=true;
                  ddl_Interest_Type.disabled=true;
                  ddl_Payment_Mode.disabled=true;
                  txt_Comments.disabled=true;
                  ddl_Payment_Bank.disabled=true;
                  txt_Start_Cheque_No.disabled=true;
                  
                  txt_Loan_Acct_No.value='';
                  txt_Loan_Amount.value='';
                  txt_Terms_In_Months.value='';
                  txt_Rate_Of_Interest.value='';
                  txt_EMI_Amount.value='';
                  txt_Comments.value='';
                  txt_Start_Cheque_No.value='';
                  ddl_Interest_Type.value=0;
                  ddl_Payment_Mode.value=0;
                  ddl_Payment_Bank.value=0;
                  lbl_Last_Payment_Due.innerText='';
                  
       }
         else 
         {
                  TDMandatory_BankName.style.visibility="visible";
                  TDMandatory_LoanAcctNo.style.visibility="visible";
                  TDMandatory_LoanAmount.style.visibility="visible";
                  TDMandatory_TermsInMonths.style.visibility="visible";
                  TDMandatory_RateOfInterest.style.visibility="visible";
                  TDMandatory_EmiAmount.style.visibility="visible";
                  TDMandatory_InterestType.style.visibility="visible";
                  TDMandatory_PaymentMode.style.visibility="visible";
                  TDMandatory_FirstPaymentDue.style.visibility="visible";
                  Tr_btn_Generate.style.visibility="visible";
                 
                  txt_Loan_Acct_No.disabled = false; 
                  txt_Loan_Amount.disabled = false; 
                  txt_Terms_In_Months.disabled = false; 
                  txt_Rate_Of_Interest.disabled = false;
                  txt_EMI_Amount.disabled = false;
                  ddl_Interest_Type.disabled = false; 
                  ddl_Payment_Mode.disabled = false; 
                  txt_Comments.disabled = false; 
                  ddl_Payment_Bank.disabled = false; 
                  txt_Start_Cheque_No.disabled = false;
  
           }      
  
 }
</script>

<table class="TABLE">
    <tr>
        <td colspan="8">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnl_Loan_Details" runat="server" GroupingText="Loan Details" CssClass="PANEL"
                            Width="100%" meta:resourcekey="pnl_Loan_DetailsResource1">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Bank_Name" runat="server" Text="Bank Name :" meta:resourcekey="lbl_Bank_NameResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:DropDownList ID="ddl_Bank_Name" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Bank_NameResource1">
                                        </asp:DropDownList></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_BankName">
                                        *</td>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Loan_Acct_No" runat="server" Text="Loan Acct. No. : " meta:resourcekey="lbl_Loan_Acct_NoResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:TextBox ID="txt_Loan_Acct_No" runat="server" CssClass="TEXTBOX" Width="99%"
                                            MaxLength="20" meta:resourcekey="txt_Loan_Acct_NoResource1"></asp:TextBox></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_LoanAcctNo">
                                        *</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;" class="TD1">
                                        <asp:Label ID="lbl_Loan_Amount" runat="server" Text="Loan Amount :" meta:resourcekey="lbl_Loan_AmountResource1" />
                                    </td>
                                    <td style="width: 29%;">
                                        <asp:TextBox ID="txt_Loan_Amount" runat="server" CssClass="TEXTBOXNOS" Width="97%"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Loan_AmountResource1"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="TDMandatory_LoanAmount">
                                        *</td>
                                    <td style="width: 20%;" class="TD1">
                                        <asp:Label ID="lbl_Terms_in_Months" runat="server" Text="Terms in Months :" meta:resourcekey="lbl_Terms_in_MonthsResource1" />
                                    </td>
                                    <td style="width: 29%;">
                                        <asp:TextBox ID="txt_Terms_In_Months" runat="server" CssClass="TEXTBOXNOS" Width="99%"
                                            MaxLength="3" onkeypress="return Only_Numbers(this,event)" onblur="SetDateToLable()"
                                            meta:resourcekey="txt_Terms_In_MonthsResource1"></asp:TextBox></td>
                                    <td style="width: 1%;" class="TDMANDATORY" runat="server" id="TDMandatory_TermsInMonths">
                                        *</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Rate_of_Interest" runat="server" Text="Rate of Interest :" meta:resourcekey="lbl_Rate_of_InterestResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:TextBox ID="txt_Rate_Of_Interest" runat="server" Width="90%" CssClass="TEXTBOXNOS"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Rate_Of_InterestResource1"></asp:TextBox>%</td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_RateOfInterest">
                                        *</td>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_EMI_Amount" runat="server" Text="EMI Amount :" meta:resourcekey="lbl_EMI_AmountResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:TextBox ID="txt_EMI_Amount" runat="server" Width="99%" CssClass="TEXTBOXNOS"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_EMI_AmountResource1"></asp:TextBox></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_EmiAmount">
                                        *</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Interest_Type" runat="server" Text="Interest Type :" meta:resourcekey="lbl_Interest_TypeResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:DropDownList ID="ddl_Interest_Type" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Interest_TypeResource1">
                                        </asp:DropDownList></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_InterestType">
                                        *</td>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Payment_Mode" runat="server" Text="Payment Mode :" meta:resourcekey="lbl_Payment_ModeResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:DropDownList ID="ddl_Payment_Mode" runat="server" CssClass="DROPDOWN" Width="100%"
                                            OnChange="VisibleForPaymentMode()" meta:resourcekey="ddl_Payment_ModeResource1">
                                        </asp:DropDownList></td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_PaymentMode">
                                        *</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_First_Payment_Due" runat="server" Text="First Payment Due :" meta:resourcekey="lbl_First_Payment_DueResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <uc1:wuc_Date_Picker ID="dtp_FirstPaymentDue" runat="server" InjectJSfunction="SetDateToLable()" />
                                    </td>
                                    <td style="width: 1%" class="TDMANDATORY" runat="server" id="TDMandatory_FirstPaymentDue">
                                        *</td>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Last_Payment" runat="server" Text="Last Payment Due :" meta:resourcekey="lbl_Last_PaymentResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:Label ID="lbl_Last_Payment_Due" runat="server" Font-Bold="True" meta:resourcekey="lbl_Last_Payment_DueResource1"></asp:Label></td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Comments" runat="server" Text="Comments :" meta:resourcekey="lbl_CommentsResource1" />
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txt_Comments" runat="server" CssClass="TEXTBOX" Width="100%" TextMode="MultiLine"
                                            MaxLength="500" meta:resourcekey="txt_CommentsResource1"></asp:TextBox></td>
                                    <td style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label ID="lbl_Payment_Bank" runat="server" Text="Payment Bank :" meta:resourcekey="lbl_Payment_BankResource1" />
                                    </td>
                                    <td style="width: 29%">
                                        <asp:DropDownList ID="ddl_Payment_Bank" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Payment_BankResource1">
                                        </asp:DropDownList></td>
                                    <td style="width: 1%" class="TDMANDATORY">
                                        *</td>
                                    <td style="width: 20%" class="TD1">
                                        <asp:Label runat="server" ID="lbl_StartChequeNo" Text="Start Cheque No. :" meta:resourcekey="lbl_StartChequeNoResource1"></asp:Label></td>
                                    <td style="width: 29%">
                                        <asp:TextBox ID="txt_Start_Cheque_No" runat="server" CssClass="TEXTBOXNOS" Width="99%"
                                            onkeypress="return Only_Numbers(this,event)" MaxLength="6" meta:resourcekey="txt_Start_Cheque_NoResource1"></asp:TextBox></td>
                                    <td style="width: 1%">
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr_btn_Generate">
                                    <td style="width: 20%;" runat="server">
                                    </td>
                                    <td style="width: 29%;" runat="server">
                                    </td>
                                    <td style="width: 1%;" runat="server">
                                    </td>
                                    <td style="width: 20%;" runat="server">
                                    </td>
                                    <td style="width: 29%;" align="right" runat="server">
                                        <asp:Button ID="btn_Generate" Width="50%" CssClass="BUTTON" runat="server" Text="Generate"
                                            OnClick="btn_Generate_Click" OnClientClick="return validateUI_VehicleLoanDetails()" /></td>
                                    <td style="width: 1%;" runat="server">
                                    </td>
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
                            <asp:Panel ID="pnl_Payment_Details" runat="server" GroupingText="Payment Details"
                                CssClass="PANEL" Width="100%" meta:resourcekey="pnl_Payment_DetailsResource1">
                                <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                    <tr>
                                        <td colspan="3">
                                            <div class="DIV">
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dg_Payment_Details" runat="server" AutoGenerateColumns="False"
                                                            CssClass="GRID" Width="98%" OnCancelCommand="dg_Payment_Details_CancelCommand"
                                                            OnEditCommand="dg_Payment_Details_EditCommand" OnItemCommand="dg_Payment_Details_ItemCommand"
                                                            OnItemDataBound="dg_Payment_Details_ItemDataBound" meta:resourcekey="dg_Payment_DetailsResource1">
                                                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                            <HeaderStyle CssClass="GRIDHEADERCSS" HorizontalAlign="Left" />
                                                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="SrNo" HeaderStyle-Width="5%">
                                                                    <ItemStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Due Date" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Due_Date") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15%" />
                                                                    <EditItemTemplate>
                                                                        <ComponentArt:Calendar ID="dtp_DueDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                                            ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                                            MinDate="1900-01-01" Width="5px" SelectedDate='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Due_Date")) %>'
                                                                            meta:resourcekey="dtp_DueDateResource1">
                                                                        </ComponentArt:Calendar>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Cheque No" HeaderStyle-Width="10%">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Cheque_No") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txt_ChequeNo" Width="75%" Text='<%# DataBinder.Eval(Container.DataItem, "Cheque_No") %>'
                                                                            CssClass="TEXTBOX" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"
                                                                            MaxLength="50" meta:resourcekey="txt_ChequeNoResource1"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Bank Name" HeaderStyle-Width="30%">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Bank_Name") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddl_BankName" Width="80.5%" CssClass="DROPDOWN"
                                                                            meta:resourcekey="ddl_BankNameResource1">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Principle Amount">
                                                                    <ItemStyle Width="12%" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Principle_Amount") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txt_PrincipleAmount" Width="75%" Text='<%# DataBinder.Eval(Container.DataItem, "Principle_Amount") %>'
                                                                            CssClass="TEXTBOXNOS" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"
                                                                            MaxLength="18" meta:resourcekey="txt_PrincipleAmountResource1"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Interest" HeaderStyle-Width="10%">
                                                                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Interest") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txt_Interest" Width="75%" Text='<%# DataBinder.Eval(Container.DataItem, "Interest") %>'
                                                                            CssClass="TEXTBOXNOS" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"
                                                                            MaxLength="18" meta:resourcekey="txt_InterestResource1"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="EMI Amount" HeaderStyle-Width="10%">
                                                                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "EMI_Amount") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txt_EMIAmount" Width="75%" Text='<%# DataBinder.Eval(Container.DataItem, "EMI_Amount") %>'
                                                                            CssClass="TEXTBOXNOS" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"
                                                                            MaxLength="18" meta:resourcekey="txt_EMIAmountResource1"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                </asp:TemplateColumn>
                                                                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="Edit"
                                                                    meta:resourcekey="EditCommandColumnResource1">
                                                                    <ItemStyle Width="5%" />
                                                                </asp:EditCommandColumn>
                                                            </Columns>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:DataGrid>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="dg_Payment_Details" />
                                                        <asp:AsyncPostBackTrigger ControlID="btn_Generate" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </td>
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
                            <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"/>
        </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_Save" />
      </Triggers>
      </asp:UpdatePanel>--%>
                            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                meta:resourcekey="lbl_ErrorsResource1" />
                        </td>
                    </tr>
                </table>


                <script type="text/javascript">
                  Enable_LoanDetails();
                  VisibleForPaymentMode();
                </script>
