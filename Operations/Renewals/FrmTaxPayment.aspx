<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTaxPayment.aspx.cs" Inherits="Operations_Renewals_FrmTaxPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Transactions/Renewals/TaxPayment.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Tax Payment</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
 <script language="javascript" type="text/javascript">
function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('lbl_Errors');     
   var DDL_Vehicle = document.getElementById('WucVehicleSearch1_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
   var ddl_State=document.getElementById('ddl_State');
   var txt_Tax_Amount=document.getElementById('txt_Tax_Amount');
   var txt_Receipt_No=document.getElementById('txt_Receipt_No');

  lbl_Errors.innerText ="";
  
 if (DDL_Vehicle.options.length == 0)
  {
      lbl_Errors.innerText = "Please Select Vehicle No";
      txt_vehicle_search.focus();
  }
  else if (ddl_State.value == 0 || ddl_State.options.length <= 0)
  {
     lbl_Errors.innerText = "Please Select State";
     ddl_State.focus();   
  } 
  else if (txt_Tax_Amount.value == '')
  {
     lbl_Errors.innerText = "Please Enter Tax Amount";
     txt_Tax_Amount.focus();   
  } 
  else if (txt_Receipt_No.value == '')
  {
     lbl_Errors.innerText = "Please Enter Receipt No";
     txt_Receipt_No.focus();   
  }
 else if (Check_Date()== false)
  {
        return ATS;
  }
  else    
      ATS = true;

return ATS;
}


function Check_Date()
{           
    var Wuc_Valid_From=new Date();
    var Wuc_Valid_Upto=new Date();
    var lbl_Errors = document.getElementById('lbl_Errors'); 
    
    Wuc_Valid_From=<%=Wuc_Valid_From.PickerClientID %>.GetSelectedDate();
    Wuc_Valid_Upto= <%=Wuc_Valid_Upto.PickerClientID %>.GetSelectedDate();
     
    if (Wuc_Valid_From>Wuc_Valid_Upto)
    {
         lbl_Errors.innerText = "Valid From Date Should be Less Then Valid Upto Date";
         return false;   
    }
    return true;
}

</script>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="scm_TaxPayment" runat="server"></asp:ScriptManager>
    <div>
    <table style="width: 100%" class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="Tax Payment" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Tax Details No:</td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Tax_Details_No" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:Label></td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">Tax Details Date:</td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_Tax_Details_Date" runat="server" />
        </td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Vehicle No:
        </td>
        <td style="width: 29%">
            <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">State:</td>
        <td style="width: 29%">
         <asp:UpdatePanel ID="Upd_Pnl_ddl_State" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"/>
        </Triggers>
        <ContentTemplate>
            <asp:DropDownList ID="ddl_State" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%; " class="TD1">Permit Type:</td>
        <td style="width: 29%; ">
        <asp:UpdatePanel ID="Upd_Pnl_lbl_Permit_Type" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_State"/>
             <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"/>
        </Triggers>
        <ContentTemplate>
            <asp:Label ID="lbl_Permit_Type" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:Label>
            </ContentTemplate>
         </asp:UpdatePanel>   
            </td>
        <td style="width: 1%; " class="TDMANDATORY">*</td>
        <td style="width: 20%; " class="TD1">Permit No:</td>
        <td style="width: 29%; ;">
          <asp:UpdatePanel ID="Upd_Pnl_lbl_Permit_No" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_State"/>
             <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"/>
        </Triggers>
        <ContentTemplate>
            <asp:Label ID="lbl_Permit_No" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdn_Permit_Type_Id" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 1%; " class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%; " class="TD1">Tax Amount:</td>
        <td style="width: 29%; ">
            <asp:TextBox ID="txt_Tax_Amount" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="12"></asp:TextBox></td>
        <td style="width: 1%; " class="TDMANDATORY">*</td>
        <td style="width: 20%; " class="TD1">Receipt No:</td>
        <td style="width: 29%; ">
            <asp:TextBox ID="txt_Receipt_No" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox></td>
        <td style="width: 1%; " class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Valid From:</td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_Valid_From" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">Valid Upto:</td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_Valid_Upto" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>
      <tr>
                <td style="width: 20%" class="TD1">
                  <asp:Label ID="PaidBy" runat="Server" Text="Paid By:" meta:resourcekey="PaidByResource1"></asp:Label>
                </td>
                <td style="width: 29%">
                    <asp:RadioButtonList ID="rdl_Paid_By" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" onclick="Enabled_Disabled_Controls_On_Cheque()" Selected="True" >Cash</asp:ListItem>
                        <asp:ListItem Value="2" onclick="Enabled_Disabled_Controls_On_Cheque()">Cheque</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td style="width: 1%" class="TDMANDATORY"></td>
                <td style="width: 20%" class="TD1"></td>
                <td style="width: 29%"></td>
                <td style="width: 1%" class="TDMANDATORY"></td>
            </tr>
            <tr id="tr_Cheque_Details" runat="server">
                <td style="width: 20%" class="TD1"><asp:Label ID="lbl_ChequeNo" runat="Server" Text="Cheque No:"></asp:Label></td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_Cheque_No" runat="server" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox></td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
                <td style="width: 20%" class="TD1"><asp:Label ID="lbl_ChequeDate" runat="Server" Text="Cheque Date:"></asp:Label></td>
                <td style="width: 29%">
                    <uc1:WucDatePicker ID="Wuc_Cheque_Date" runat="server" />
                </td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
            </tr>
            <tr id="tr_Bank_Name" runat="server">
                <td style="width: 20%" class="TD1"><asp:Label ID="lbl_BankName" runat="Server" Text="Bank Name:"></asp:Label></td>
                <td style="width: 29%">
                    <asp:DropDownList ID="ddl_Bank_Name" runat="server" CssClass="DROPDOWN" Width="100%"></asp:DropDownList></td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
                <td style="width: 20%" class="TD1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_State" />
                </Triggers>
                <ContentTemplate>
                <asp:HiddenField ID="hdn_State_ID" runat="server" />
                <asp:HiddenField ID="hdn_Temporary_Permit_ID" runat="server" />
               </ContentTemplate>
               </asp:UpdatePanel>
                </td>
                <td style="width: 29%">
                 
                </td>
                <td style="width: 1%" class="TDMANDATORY"></td>
            </tr>
            <tr>
            <td colspan="6">
                <asp:HiddenField ID="hdn_Is_Cheque" runat="server" />
                <asp:HiddenField ID="hdn_Valid_From" runat="server" />
            </td>
            </tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr><td colspan="6">&nbsp;</td></tr>
        <tr>        
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON"  Text="Save" OnClick="btn_save_Click"/></td>
    </tr>
   <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_TaxPayment" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"  EnableViewState="False" ></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
           
        </td>
    </tr>
    
</table>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
Enabled_Disabled_Controls_On_Cheque()
</script>
</body>
</html>
