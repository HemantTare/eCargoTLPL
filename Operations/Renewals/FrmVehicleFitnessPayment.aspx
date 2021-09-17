<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleFitnessPayment.aspx.cs" Inherits="Operations_Renewals_FrmVehicleFitnessPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../JavaScript/Transactions/Renewals/FitnessPayment.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Fitness Payment</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
 
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div>
        <table class="TABLE" width="100%">
            <tr>
                <td colspan="6"class="TDGRADIENT">
                    <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="Fitness Payment"></asp:Label>
                </td>
            </tr>
            <tr> 
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_FitnessNo" runat="Server" Text=" Fitness No:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <asp:Label ID="lbl_Fitness_No" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:Label></td>
                <td style="width: 1%" class="TDMANDATORY">
                </td>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_Date" runat="Server" Text="Date:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <uc1:WucDatePicker ID="Wuc_FitnessDate" runat="server" />
                </td>
                <td style="width: 1%" class="TDMANDATORY">
                </td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                  <asp:Label ID="lbl_VehicleNo" runat="Server" Text="Vehicle No:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                </td>
                <td style="width: 1%" class="TDMANDATORY">*
                </td>
                <td style="width: 20%" class="TD1"><asp:Label ID="lbl_FitnessCertificateNo" runat="Server" Text="Fitness Certificate No:"></asp:Label>
                    </td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_Fitness_Certificate_No" runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox></td>
                <td style="width: 1%" class="TDMANDATORY">*
                </td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_RTO" runat="Server" Text="RTO:"></asp:Label>
                </td>
                <td style="width: 29%">
                <asp:UpdatePanel ID="Upd_Pnl_ddl_RTO" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger  ControlID="WucVehicleSearch1"/>
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_RTO" runat="server" Width="100%" CssClass="DROPDOWN">
                    </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_Amount" runat="Server" Text=" Amount:"></asp:Label>
                </td>
                <td style="width: 29%">
                  <asp:TextBox ID="txt_Amount" runat="server"  onkeypress="return Only_Numbers(this,event)" MaxLength="10" CssClass="TEXTBOXNOS"></asp:TextBox>
                </td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_IssueDate" runat="Server" Text="Issue Date:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <uc1:WucDatePicker ID="Wuc_Issue_Date" runat="server" />
                </td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
                <td style="width: 20%" class="TD1">
                   <asp:Label ID="lbl_ValidUTo" runat="Server" Text="Valid UpTo:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <uc1:WucDatePicker ID="Wuc_Valid_UpTo" runat="server" />
                </td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
            </tr>
            <tr>
                <td style="width: 20%" class="TD1">
                  <asp:Label ID="PaidBy" runat="Server" Text="Paid By:"></asp:Label>
                </td>
                <td style="width: 29%">
                    <asp:RadioButtonList ID="rdl_Paid_By" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" onclick="Enabled_Controls_On_Cheque()" Selected="True">Cash</asp:ListItem>
                        <asp:ListItem Value="2" onclick="Enabled_Controls_On_Cheque()">Cheque</asp:ListItem>
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
                    <asp:DropDownList ID="ddl_Bank_Name" runat="server" CssClass="DROPDOWN" Width="100%">
                    </asp:DropDownList></td>
                <td style="width: 1%" class="TDMANDATORY">*</td>
                <td style="width: 20%" class="TD1"></td>
                <td style="width: 29%"></td>
                <td style="width: 1%" class="TDMANDATORY"></td>
            </tr>
            <tr>
            <td colspan="6">
                <asp:HiddenField ID="hdn_Is_Cheque" runat="server" />
            </td>
            </tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btn_save" runat="server" CssClass="BUTTON"  Text="Save"  OnClick="btn_save_Click"/></td>
            </tr>
   <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_FitnessPayment" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"  EnableViewState="False"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdn_Renewal_ID" runat="server" />
        </td>
    </tr>
 </table>
    </div>
    </form>
    <script type="text/javascript">
    Enabled_Controls_On_Cheque()
 </script>
</body>
</html>
