<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerGeneral.ascx.cs" Inherits="Finance_Masters_WucLedger" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
 <script type="text/javascript" src="../../Javascript/Common.js"></script>
 <script type="text/javascript" src="../../Javascript/NumCheck.js"></script>
  <script type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>

<script language="javascript" type="text/javascript">

var txt_LedgerName;
var txt_Alias
var ddl_Under
var lbl_BillbyBill
var chk_BillbyBill
var lbl_CreditPeriod
var txt_CreditPeriod
var lbl_Type_of_Duty_Tax
var ddl_Type_of_Duty_Tax

var lbl_Nature_Of_Payment
var ddl_Nature_Of_Payment

var lbl_Service_Tax_Category
var ddl_Service_Tax_Category
var lbl_Is_FBT_Applicable
var Chk_Is_FBT_Applicable

var lbl_FBT_Category
var DDL_FBT_Category

var lbl_Is_TDS_Applicable
var Chk_Is_TDS_Applicable
var lbl_Deductee_Type
var DDL_Deductee_Type
var lbl_Is_Lower_No_Deduction_Applicable
var Chk_Is_Lower_No_Deduction_Applicable
var lbl_Ignore_TDS_Exemption_Limit
var Chk_Is_Ignore_TDS_Exemption_Limit
var lbl_Section_Number
var ddl_Section_Number
var lbl_TDS_Lower_Rate
var txt_TDS_Lower_Rate
var lbl_Is_Service_Tax_Applicable
var Chk_Is_Service_Tax_Applicable
var lbl_Is_Exempted
var Chk_Is_Exempted
var lbl_Notification_Detail
var txt_Notification_Detail
var lbl_Ignore_TDS_Exemption_Limit
var Chk_Is_Ignore_TDS_Exemption_Limit
var TR_Location

var td_BankRecoDate
var lbl_BankRecoDate

var lbl_CreditLimit
var txt_CreditLimit


var lbl_ServiceTaxNo
var txt_ServiceTaxNo
var lbl_ACNo
var txt_ACNo

var lbl_ServiceTaxRegDate
var td_ServiceTaxRegDate

var runScriptOnLoad;



function FindControls()
{ 
 txt_LedgerName= document.getElementById('<%=txt_LedgerName.ClientID%>');
 txt_Alias= document.getElementById('<%=txt_Alias.ClientID%>');
 ddl_Under= document.getElementById('<%=ddl_Under.ClientID%>');

 lbl_BillbyBill= document.getElementById('<%=lbl_BillbyBill.ClientID%>');
 chk_BillbyBill= document.getElementById('<%=chk_BillbyBill.ClientID%>');


 lbl_CreditPeriod= document.getElementById('<%=lbl_CreditPeriod.ClientID%>');
 txt_CreditPeriod= document.getElementById('<%=txt_CreditPeriod.ClientID%>');

 lbl_Type_of_Duty_Tax= document.getElementById('<%=lbl_Type_of_Duty_Tax.ClientID%>');
 ddl_Type_of_Duty_Tax= document.getElementById('<%=ddl_Type_of_Duty_Tax.ClientID%>');

 lbl_Nature_Of_Payment= document.getElementById('<%=lbl_Nature_Of_Payment.ClientID%>');
 ddl_Nature_Of_Payment= document.getElementById('<%=ddl_Nature_Of_Payment.ClientID%>');


 lbl_Service_Tax_Category= document.getElementById('<%=lbl_Service_Tax_Category.ClientID%>');
 ddl_Service_Tax_Category= document.getElementById('<%=ddl_Service_Tax_Category.ClientID%>');

 lbl_Is_FBT_Applicable= document.getElementById('<%=lbl_Is_FBT_Applicable.ClientID%>');
 Chk_Is_FBT_Applicable= document.getElementById('<%=Chk_Is_FBT_Applicable.ClientID%>');


 lbl_FBT_Category= document.getElementById('<%=lbl_FBT_Category.ClientID%>');
 DDL_FBT_Category= document.getElementById('<%=DDL_FBT_Category.ClientID%>');

 lbl_Is_TDS_Applicable= document.getElementById('<%=lbl_Is_TDS_Applicable.ClientID%>');
 Chk_Is_TDS_Applicable= document.getElementById('<%=Chk_Is_TDS_Applicable.ClientID%>');
 lbl_Deductee_Type= document.getElementById('<%=lbl_Deductee_Type.ClientID%>');
 DDL_Deductee_Type= document.getElementById('<%=DDL_Deductee_Type.ClientID%>');
 lbl_Is_Lower_No_Deduction_Applicable= document.getElementById('<%=lbl_Is_Lower_No_Deduction_Applicable.ClientID%>');
 Chk_Is_Lower_No_Deduction_Applicable= document.getElementById('<%=Chk_Is_Lower_No_Deduction_Applicable.ClientID%>');
 lbl_Ignore_TDS_Exemption_Limit= document.getElementById('<%=lbl_Ignore_TDS_Exemption_Limit.ClientID%>');
 Chk_Is_Ignore_TDS_Exemption_Limit= document.getElementById('<%=Chk_Is_Ignore_TDS_Exemption_Limit.ClientID%>');
 lbl_Section_Number= document.getElementById('<%=lbl_Section_Number.ClientID%>');
 ddl_Section_Number= document.getElementById('<%=ddl_Section_Number.ClientID%>');
 lbl_TDS_Lower_Rate= document.getElementById('<%=lbl_TDS_Lower_Rate.ClientID%>');
 txt_TDS_Lower_Rate= document.getElementById('<%=txt_TDS_Lower_Rate.ClientID%>');
 lbl_Is_Service_Tax_Applicable= document.getElementById('<%=lbl_Is_Service_Tax_Applicable.ClientID%>');
 Chk_Is_Service_Tax_Applicable= document.getElementById('<%=Chk_Is_Service_Tax_Applicable.ClientID%>');
 lbl_Is_Exempted= document.getElementById('<%=lbl_Is_Exempted.ClientID%>');
 Chk_Is_Exempted= document.getElementById('<%=Chk_Is_Exempted.ClientID%>');
 lbl_Notification_Detail= document.getElementById('<%=lbl_Notification_Detail.ClientID%>');
 txt_Notification_Detail= document.getElementById('<%=txt_Notification_Detail.ClientID%>');
 lbl_Ignore_TDS_Exemption_Limit= document.getElementById('<%=lbl_Ignore_TDS_Exemption_Limit.ClientID%>');
 Chk_Is_Ignore_TDS_Exemption_Limit= document.getElementById('<%=Chk_Is_Ignore_TDS_Exemption_Limit.ClientID%>');
 TR_Location= document.getElementById('<%=TR_Location.ClientID%>');

 td_BankRecoDate= document.getElementById('<%=td_BankRecoDate.ClientID%>');
 lbl_BankRecoDate= document.getElementById('<%=lbl_BankRecoDate.ClientID%>');
 
 lbl_CreditLimit= document.getElementById('<%=lbl_CreditLimit.ClientID%>');
 txt_CreditLimit= document.getElementById('<%=txt_CreditLimit.ClientID%>');


 lbl_ServiceTaxNo= document.getElementById('<%=lbl_ServiceTaxNo.ClientID%>');
 txt_ServiceTaxNo= document.getElementById('<%=txt_ServiceTaxNo.ClientID%>');

 lbl_ACNo= document.getElementById('<%=lbl_ACNo.ClientID%>');
 txt_ACNo= document.getElementById('<%=txt_ACNo.ClientID%>');
 
 lbl_ServiceTaxRegDate= document.getElementById('<%=lbl_ServiceTaxRegDate.ClientID%>');
 td_ServiceTaxRegDate= document.getElementById('<%=td_ServiceTaxRegDate.ClientID%>');


}

//visible|hidden|collapse

function HideServiceTaxNo(value)
{
  lbl_ServiceTaxNo.style.visibility=value;
  txt_ServiceTaxNo.style.visibility=value;
//  txt_ServiceTaxNo.value='';
}

function HideServiceTaxRegDate(value)
{
  lbl_ServiceTaxRegDate.style.visibility=value;
  td_ServiceTaxRegDate.style.visibility=value;
}

function HideACNo(value)
{
  lbl_ACNo.style.visibility=value;
  txt_ACNo.style.visibility=value;
//  txt_ACNo.value='';
}




function HideBankRecoDate(value)
{
  lbl_BankRecoDate.style.visibility=value;
  td_BankRecoDate.style.visibility=value;
}


 function HideCreditLimit(value)
{
  lbl_CreditLimit.style.visibility=value;
  txt_CreditLimit.style.visibility=value;
//  txt_CreditLimit.value='0';
}


function HideBillByBill(value)
{
  lbl_BillbyBill.style.visibility=value;
  chk_BillbyBill.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
      chk_BillbyBill.checked=false;
      chk_BillbyBill.disabled=false;
  }
}


 function HideCreditPeriod(value)
{
  lbl_CreditPeriod.style.visibility=value;
  txt_CreditPeriod.style.visibility=value;
//  txt_CreditPeriod.value='0';
}


function HideServiceTaxCategory(value)
{
  lbl_Service_Tax_Category.style.visibility=value;
  ddl_Service_Tax_Category.style.visibility=value;
}



function HideTypeOfDutyTax(value)
{
  lbl_Type_of_Duty_Tax.style.visibility=value;
  ddl_Type_of_Duty_Tax.style.visibility=value;
  if(runScriptOnLoad==0)
  {
  ddl_Type_of_Duty_Tax.selectedIndex=0
  }

}

function HideNatureOfPayment(value)
{
  lbl_Nature_Of_Payment.style.visibility=value;
  ddl_Nature_Of_Payment.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
    ddl_Nature_Of_Payment.selectedIndex=0
  }
}

function HideFBTApplicable(value)
{
  lbl_Is_FBT_Applicable.style.visibility=value;
  Chk_Is_FBT_Applicable.style.visibility=value;
  if(runScriptOnLoad==0)
  {
    Chk_Is_FBT_Applicable.checked=false;
  }
}


function HideFBTCategory(value)
{
  lbl_FBT_Category.style.visibility=value;
  DDL_FBT_Category.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
    DDL_FBT_Category.selectedIndex=0
  }
}

function HideTDSApplicable(value)
{
  lbl_Is_TDS_Applicable.style.visibility=value;
  Chk_Is_TDS_Applicable.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
   Chk_Is_TDS_Applicable.checked=false;
  }
}

function HideDeducteeType(value)
{
  lbl_Deductee_Type.style.visibility=value;
  DDL_Deductee_Type.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
   DDL_Deductee_Type.selectedIndex=0;
  }
}

function HideLowerNoDeductionApplicable(value)
{
  lbl_Is_Lower_No_Deduction_Applicable.style.visibility=value;
  Chk_Is_Lower_No_Deduction_Applicable.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
    Chk_Is_Lower_No_Deduction_Applicable.checked=false;
  }
}

function HideIgnoreTDSExemptionLimit(value)
{
  lbl_Ignore_TDS_Exemption_Limit.style.visibility=value;
  Chk_Is_Ignore_TDS_Exemption_Limit.style.visibility=value;
  
  
  if(GetSelectedValue(ddl_Under)==2)
      lbl_Ignore_TDS_Exemption_Limit.InnerHTML='Ignore TDS1 Exemption Limit';
  else 
      lbl_Ignore_TDS_Exemption_Limit.InnerHTML='Ignore Surcharge Exemption Limit';
                    
  if(runScriptOnLoad==0)
  {
   Chk_Is_Ignore_TDS_Exemption_Limit.checked=false;
  }
    
}

function HideSectionNumber(value)
{
  lbl_Section_Number.style.visibility=value;
  ddl_Section_Number.style.visibility=value;
  
  if(runScriptOnLoad==0)
  {
    ddl_Section_Number.selectedIndex=0;
  }
}


function HideTDSLowerRate(value)
{
  lbl_TDS_Lower_Rate.style.visibility=value;
  txt_TDS_Lower_Rate.style.visibility=value;
  
//  txt_TDS_Lower_Rate.value='0';
}

function HideServiceTaxApplicable(value)
{
  lbl_Is_Service_Tax_Applicable.style.visibility=value;
  Chk_Is_Service_Tax_Applicable.style.visibility=value;
  
    if(runScriptOnLoad==0)
  {
    Chk_Is_Service_Tax_Applicable.checked=false;
  }

}


function HideIsExempted(value)
{
  lbl_Is_Exempted.style.visibility=value;
  Chk_Is_Exempted.style.visibility=value;

  if(runScriptOnLoad==0)
  {
   Chk_Is_Exempted.checked=false;
  }

  
}

function HideNotificationDetail(value)
{
  lbl_Notification_Detail.style.visibility=value;
  txt_Notification_Detail.style.visibility=value;
  
//  txt_Notification_Detail.value='';

}


function HideIgnoreTDSExemptionLimit(value)
{
  lbl_Ignore_TDS_Exemption_Limit.style.visibility=value;
  Chk_Is_Ignore_TDS_Exemption_Limit.style.visibility=value;
  
   if(runScriptOnLoad==0)
   {
   Chk_Is_Ignore_TDS_Exemption_Limit.checked=false;
   }

}


function HideLocation(value)
{
  TR_Location.style.visibility=value;
}


function set_runScriptOnLoad(value)
{
  runScriptOnLoad=value;
}

function OnUnderChanged()
{
 
 HideAllControls();
 var splitted= ddl_Under.options[ddl_Under.selectedIndex].value.split('Ö')
 
 var ResLadgerId =splitted[1];
 if(ResLadgerId==19 || ResLadgerId==28)
 {
    HideLocation('visible');
    HideBankRecoDate('visible');
    HideACNo('visible');
 }
 
 else if(ResLadgerId==24 || ResLadgerId==27)
 {
   HideBillByBill('visible');
   HideTDSApplicable('visible');
   HideServiceTaxApplicable('visible');
   HideCreditLimit('visible');
 }
 

 else if(ResLadgerId==25)
 {
   HideBillByBill('visible');
   HideTypeOfDutyTax('visible');
 }
 
 else if(ResLadgerId==-1)
 {
   HideFBTApplicable('visible');
 }
 
}


function HideAllControls()
{
    HideBillByBill('hidden');
    HideCreditPeriod('hidden');
    HideDeducteeType('hidden');
    HideFBTApplicable('hidden');
    HideFBTCategory('hidden');
    HideIgnoreTDSExemptionLimit('hidden');
    HideIgnoreTDSExemptionLimit('hidden');
    HideIsExempted('hidden');
    HideLocation('hidden');
    HideLowerNoDeductionApplicable('hidden');
    HideNatureOfPayment('hidden');
    HideNotificationDetail('hidden');
    HideSectionNumber('hidden');
    HideServiceTaxApplicable('hidden');
    HideTDSApplicable('hidden');
    HideTDSLowerRate('hidden');
    HideTDSLowerRate('hidden');
    HideTypeOfDutyTax('hidden');
    HideBankRecoDate('hidden');
    HideCreditLimit('hidden');
    HideServiceTaxCategory('hidden');
    HideBankRecoDate('hidden');
    
    HideACNo('hidden');
    HideServiceTaxNo('hidden');
    HideServiceTaxRegDate('hidden');
    
//    chk_BillbyBill.checked=false;
    chk_BillbyBill.disabled=false;

}

function OnBillByBill()
{
    if(chk_BillbyBill.checked)
    {
      HideCreditPeriod('visible');
    }
    else
    {
      HideCreditPeriod('hidden');
    }
}


function OnTDSApplicable()
{
    if(Chk_Is_TDS_Applicable.checked)
    {
      HideDeducteeType('visible');
      HideLowerNoDeductionApplicable('visible');
//      HideIgnoreTDSExemptionLimit('visible');
      HideCreditPeriod('visible');
      chk_BillbyBill.checked=true;
      chk_BillbyBill.disabled=true;

    }
    else
    {
      HideDeducteeType('hidden');
      HideLowerNoDeductionApplicable('hidden');
//      HideIgnoreTDSExemptionLimit('hidden');
      chk_BillbyBill.disabled=false;
      
      HideTDSLowerRate('hidden');
      HideSectionNumber('hidden');
    }
}



function OnLowerNoDeductionApplicable()
{
    if(Chk_Is_Lower_No_Deduction_Applicable.checked)
    {
      HideSectionNumber('visible');
    }
    else
    {
      HideSectionNumber('hidden');
   }
}



function OnSectionNumber()
{
    if(GetSelectedValue(ddl_Section_Number)=="197")
    {
      HideTDSLowerRate('visible');
    
    }
    else
    {
      HideTDSLowerRate('hidden');
   }
}

function OnServiceTaxApplicable()
{
    if(Chk_Is_Service_Tax_Applicable.checked)
    {
      HideIsExempted('visible');
      HideServiceTaxNo('visible');
      HideServiceTaxRegDate('visible');
    }
    else
    {
      HideIsExempted('hidden');
      Chk_Is_Exempted.checked=false;
      HideServiceTaxNo('hidden');
      HideServiceTaxRegDate('hidden');
      HideNotificationDetail('hidden');
   }
}



function OnDeducteeType()
{
    if(GetSelectedValue(DDL_Deductee_Type)==5 ||GetSelectedValue(DDL_Deductee_Type)==8)
    {
       HideIgnoreTDSExemptionLimit('hidden');
    }
    else
    {
       HideIgnoreTDSExemptionLimit('visible');
    }
}


function OnTypeOfDutyTax()
{

      HideServiceTaxCategory('hidden');
      HideNatureOfPayment('hidden');
      HideCreditPeriod('hidden');
      
      if(runScriptOnLoad==0)
      {
        HideIgnoreTDSExemptionLimit('hidden');
      }

      chk_BillbyBill.disabled=false;
      chk_BillbyBill.checked=false;
    if(GetSelectedValue(ddl_Type_of_Duty_Tax)=="Service Tax")
    {
      HideServiceTaxCategory('visible');
    }
    
    else if(GetSelectedValue(ddl_Type_of_Duty_Tax)=="TDS")
    {
      HideNatureOfPayment('visible');
      HideCreditPeriod('visible');
      chk_BillbyBill.checked=true;
      chk_BillbyBill.disabled=true;

    }
}

function OnNatureOfPayment()
{
    if( GetSelectedValue(ddl_Nature_Of_Payment)==4||
        GetSelectedValue(ddl_Nature_Of_Payment)==5||
        GetSelectedValue(ddl_Nature_Of_Payment)==6||
        GetSelectedValue(ddl_Nature_Of_Payment)==10||
        GetSelectedValue(ddl_Nature_Of_Payment)==13||
        GetSelectedValue(ddl_Nature_Of_Payment)==14||
        GetSelectedValue(ddl_Nature_Of_Payment)==16||
        GetSelectedValue(ddl_Nature_Of_Payment)==0
      )
    {    
      HideIgnoreTDSExemptionLimit('hidden');    
    }
    else
    {
      HideIgnoreTDSExemptionLimit('visible');
    }
}

function OnExempted()
{
    if(Chk_Is_Exempted.checked)
    {
      HideNotificationDetail('visible');
    }
    else
    {
      HideNotificationDetail('hidden');
    }
}

function OnFBTApplicable()
{
    if(Chk_Is_FBT_Applicable.checked)
    {
      HideFBTCategory('visible');
    }
    else
    {
      HideFBTCategory('hidden');
    }
}


function GetSelectedValue(ddl_Id)
{
  return ddl_Id.options[ddl_Id.selectedIndex].value;
}
 </script>
 
       
    <table class="TABLE">
                         
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%" class="TD1">
                                Name:</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txt_LedgerName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    MaxLength="100"></asp:TextBox></td>
                            <td style="width: 1%">
                                
                            </td>
                            <td style="width: 20%" class="TD1">
                                Alias:</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txt_Alias" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Under:</td>
                            <td style="width: 29%">
                                <asp:DropDownList ID="ddl_Under" runat="server" onchange="OnUnderChanged()"
                                    CssClass="DROPDOWN" >
                                </asp:DropDownList>
                            </td>
                            <td style="width: 1%">
                                 
                            </td>
                            <td style="width: 20%" class="TD1">
                                
                                        <asp:Label ID="lbl_BillbyBill" runat="server" CssClass="LABEL" Font-Bold="False"
                                            ForeColor="Black">Maintain Bal. Bill by Bill:</asp:Label>
                                   
                            </td>
                            <td style="width: 29%" align="left">
                                
                                        <asp:CheckBox ID="chk_BillbyBill" runat="server" onclick="OnBillByBill()"/>
                                        <asp:Label ID="lbl_CreditPeriod" runat="server" CssClass="LABEL" Font-Bold="False"
                                            ForeColor="Black">Default Credit Period: </asp:Label>
                                        <asp:TextBox ID="txt_CreditPeriod" runat="server" Width="13%" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                            onkeyup="valid(this)" onkeyPress="return Only_Numbers(this,event);" onblur="valid(this)"></asp:TextBox>
                                   
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%; ">
                                
                                        <asp:Label ID="lbl_Type_of_Duty_Tax" runat="server" Text="Type of Duty Tax:"></asp:Label>
                                    
                            </td>
                            <td style="width: 29%; ">
                                
                                        <asp:DropDownList ID="ddl_Type_of_Duty_Tax" runat="server"  onchange="OnTypeOfDutyTax()" 
                                    CssClass="DROPDOWN" >
                                            <asp:ListItem Text="---Select One---" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="FBT">FBT</asp:ListItem>
                                            <asp:ListItem Value="Service Tax" >Service Tax</asp:ListItem>
                                            <asp:ListItem Value="TDS" >TDS</asp:ListItem>
                                            <asp:ListItem Value="Others" >Others</asp:ListItem>                                         
                                </asp:DropDownList>
                                
                               
                            </td>
                            <td style="width: 1%; ">
                             </td>
                            <td style="width: 20%; " class="TD1">
                                <asp:Label ID="lbl_CreditLimit" runat="server" Text="Credit Limit :"></asp:Label></td>
                            <td style="width: 29%; " align="left">
                                <asp:TextBox ID="txt_CreditLimit" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="9"
                                    onkeypress="return Only_Numbers_With_Dot(this,event);" onkeyup="valid(this)"
                                    Width="50%"></asp:TextBox></td>
                            <td style="width: 1%; ">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                              
                                        <asp:Label ID="lbl_Nature_Of_Payment" runat="server" Text="Nature Of Payment:"></asp:Label>
                                     
                            </td>
                            <td colspan="4" style="width: 78%">
                                 <asp:DropDownList ID="ddl_Nature_Of_Payment" runat="server" CssClass="DROPDOWN" onchange="OnNatureOfPayment()"></asp:DropDownList>
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                
                                        <asp:Label ID="lbl_Service_Tax_Category" runat="server" 
                                        Text="Service Tax Category :"></asp:Label>
                                    
                            </td>
                            <td style="width: 29%">
                                
                                        <asp:DropDownList ID="ddl_Service_Tax_Category" runat="server"
                                    CssClass="DROPDOWN"  >
                                                                                   
                                </asp:DropDownList>
                                  
                            </td>
                            <td style="width: 1%">
                             </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_BankRecoDate" runat="server" Text="Effective Date Of Bank Reco :"></asp:Label></td>
                            <td style="width: 29%" align="left" runat="server" id="td_BankRecoDate">
                                <uc1:WucDatePicker id="dtp_BankRecoDate" runat="server">
                                </uc1:WucDatePicker></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td style="width: 20%" class="TD1">
                                 
                                        <asp:Label ID="lbl_Is_FBT_Applicable" runat="server" Text="Is FBT Applicable:"></asp:Label>
                                   
                            </td>
                            <td style="width: 29%" align="left">
                                
                                        <asp:CheckBox ID="Chk_Is_FBT_Applicable" onclick="OnFBTApplicable()"
                                            runat="server"  />
                                   
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                               
                                        <asp:Label ID="lbl_FBT_Category" runat="server" Text="FBT Category:"></asp:Label>
                                   
                            </td>
                            <td style="width: 29%">
                                
                                        <asp:DropDownList ID="DDL_FBT_Category" runat="server" CssClass="DROPDOWN" >
                                        </asp:DropDownList>
                               
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 20%" class="TD1">
                             
                                        <asp:Label ID="lbl_Is_TDS_Applicable" runat="server" Text="Is TDS Applicable:"></asp:Label>
                                 
                            </td>
                            <td style="width: 29%" align="left">
                               
                                        <asp:CheckBox ID="Chk_Is_TDS_Applicable"  onclick="OnTDSApplicable()" 
                                            runat="server" />
                                  
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                
                                        <asp:Label ID="lbl_Deductee_Type" runat="server" Text="Deductee Type:"></asp:Label>
                                  
                            </td>
                            <td style="width: 29%">
                                
                                        <asp:DropDownList ID="DDL_Deductee_Type" runat="server" CssClass="DROPDOWN"  onchange="OnDeducteeType()"
                                        >
                                        </asp:DropDownList>
                                   
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" class="TD1">
                              
                                        <asp:Label ID="lbl_Is_Lower_No_Deduction_Applicable" runat="server" Text="Is Lower/No Deduction Applicable:"></asp:Label>
                                    
                            </td>
                            <td style="width: 29%" align="left">
                               
                                        <asp:CheckBox ID="Chk_Is_Lower_No_Deduction_Applicable"  onclick="OnLowerNoDeductionApplicable()"  
                                            runat="server"  />
                                  
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                               
                                        <asp:Label ID="lbl_Ignore_TDS_Exemption_Limit" runat="server" 
                                        Text="Ignore TDS Exemption Limit:"></asp:Label>
                                  
                            </td>
                            <td style="width: 29%" align="left">
                               
                                        <asp:CheckBox ID="Chk_Is_Ignore_TDS_Exemption_Limit"
                                            runat="server" />
                           
                            </td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" class="TD1">
                              
                                        <asp:Label ID="lbl_Section_Number" runat="server" Text="Section Number:"></asp:Label>
                                   
                            </td>
                            <td style="width: 29%" align="left">
                             
                                    <asp:DropDownList ID="ddl_Section_Number" runat="server"  onchange="OnSectionNumber()" 
                                         CssClass="DROPDOWN" >
                                            <asp:ListItem Text="---Select One---" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="197A" Selected="True">197A</asp:ListItem>
                                            <asp:ListItem Value="197" >197</asp:ListItem>                                            
                                    </asp:DropDownList>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_ACNo" runat="server" Text="A/C No. :"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txt_ACNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    Width="50%"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" class="TD1">
                             
                                        <asp:Label ID="lbl_TDS_Lower_Rate" runat="server" Text="TDS Lower Rate %:"></asp:Label>
                               
                            </td>
                            <td style="width: 29%" align="left">
                            
                                       <asp:TextBox ID="txt_TDS_Lower_Rate" runat="server" Width="50%" BorderWidth="1px" 
                                       CssClass="TEXTBOXNOS" onkeyup="valid(this)" onkeyPress="return Only_Numbers_With_Dot(this,event);"
                                       ></asp:TextBox>
                                
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_Income_Tax_No" runat="server" Text="Income Tax No :"></asp:Label></td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txt_Income_Tax_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Width="50%" MaxLength="100"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
               
                        <tr>
                            <td class="TD1" style="width: 20%">
                             
                                        <asp:Label ID="lbl_Is_Service_Tax_Applicable" runat="server" Text="Is Service Tax Applicable:"></asp:Label>
                                  
                            </td>
                            <td style="width: 29%" align="left">
                                
                                        <asp:CheckBox ID="Chk_Is_Service_Tax_Applicable"   onclick="OnServiceTaxApplicable()"  
                                            runat="server"  />
                                   
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                                 <asp:Label ID="lbl_TIN_Sales_Tax_No" runat="server" Text="TIN Sales Tax No :"></asp:Label></td>
                            <td style="width: 29%" align="left">
                                 <asp:TextBox ID="txt_TIN_Sales_Tax_No" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    Width="50%"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
        <tr>
            <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ServiceTaxNo" runat="server" Text="Service Tax No.:"></asp:Label>
            </td>
            <td align="left" style="width: 29%">
                <asp:TextBox ID="txt_ServiceTaxNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                    Width="50%"></asp:TextBox></td>
            <td style="width: 1%">
            </td>
            <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_ServiceTaxRegDate" runat="server" Text="Service Tax Reg. Date :"></asp:Label></td>
            <td align="left" style="width: 29%" runat="server" id="td_ServiceTaxRegDate">
                <uc1:WucDatePicker ID="dtp_ServiceTaxRegDate" runat="server" />
            </td>
            <td style="width: 1%">
            </td>
        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                &nbsp;<asp:Label ID="lbl_Is_Exempted" runat="server" Text="Is Exempted:" ></asp:Label></td>
                            <td align="left" style="width: 29%">
                                &nbsp;<asp:CheckBox ID="Chk_Is_Exempted"   onclick="OnExempted()"
                                             runat="server" /></td>
                            <td style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                              
                                        <asp:Label ID="lbl_Notification_Detail" runat="server" Text="Notification Detail:"></asp:Label></td>
                            <td style="width: 29%">
                              
                                        <asp:TextBox ID="txt_Notification_Detail" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                            MaxLength="255"></asp:TextBox></td>
                            <td style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                               
                                        <table style="width: 100%">
                                            <tr id="TR_Location" runat="server">
                                                <td class="TD1" colspan="6">
                                                <fieldset><legend>Bank Reconcillation Done By</legend>
                                                    <uc2:WucHierarchyWithID  id="WucHierarchyWithID1" runat="server">
                                                    </uc2:WucHierarchyWithID></fieldset></td>
                                            </tr>
                                        </table>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6" style="height: 24px">
                                <asp:UpdatePanel ID="Upd_Ledger" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                                    </Triggers>
                                   <ContentTemplate> 
                                         <asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" EnableViewState="false"
                                               ></asp:Label>
                                    </ContentTemplate>
                                 </asp:UpdatePanel> 
                                    </td>
                        </tr>
                    </table>
              
   
   

