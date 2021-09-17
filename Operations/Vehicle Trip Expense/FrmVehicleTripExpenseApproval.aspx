<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleTripExpenseApproval.aspx.cs"
    Inherits="Operations_VehicleTripExpense_FrmVehicleTripExpenseApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
function windowClose()
{
  window.close(); 
}

function BranchwiseCashBalance()
  {
      var Path ='';
      Path='../../Finance/Reports/Frm_BranchWiseClosingCash.aspx';
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-50;
      var popH = h-150;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'BranchwiseCashBalance', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  }  
  
function Calculate_ApprovedAmount(txt, gridname)
{

        var grid = document.getElementById(gridname);
        var lblTotalExpenseApproved = document.getElementById("lblTotalExpenseApproved");
        var lblCalculatedTotalExpenseApproved = document.getElementById("lblCalculatedTotalExpenseApproved");
        var hdnTotalExpenseApproved = document.getElementById("hdnTotalExpenseApproved");
        var hdn_OpeningBalance = document.getElementById("hdn_OpeningBalance");
        var hdn_CalculatedOpeningBalance = document.getElementById("hdn_CalculatedOpeningBalance");
        
        var lbl_ClosingBalance = document.getElementById("lbl_ClosingBalance");
        var lbl_CalculatedClosingBalance = document.getElementById("lbl_CalculatedClosingBalance");
        var hdn_ClosingBalance = document.getElementById("hdn_ClosingBalance");
        var hdn_CalculatedClosingBalance = document.getElementById("hdn_CalculatedClosingBalance");
        
        var lbl_ExpectedAdvance = document.getElementById("lbl_ExpectedAdvance");
        var lbl_CalculatedExpectedAdvance = document.getElementById("lbl_CalculatedExpectedAdvance");
        var hdn_ExpectedAdvance = document.getElementById("hdn_ExpectedAdvance");
        
        var i,j=0;
        var txt_approved=0,txt_expense =0,sum_Approvedamt=0;
        for(i=1;i<grid.rows.length;i++)
        {            
            txt_approved = grid.rows[i].cells[2].getElementsByTagName('input'); 
            txt_expense = grid.rows[i].cells[1].getElementsByTagName('input'); 
            if(txt_approved[0].type =='text')
            {
                if(val(txt_approved[0].value)> val(txt_expense[0].value))
                    txt_approved[0].value = txt_expense[0].value;
                    
                sum_Approvedamt = sum_Approvedamt + val(txt_approved[0].value);
            }
        }
        
        lblTotalExpenseApproved.innerHTML = sum_Approvedamt;
        lblCalculatedTotalExpenseApproved.innerHTML = sum_Approvedamt;
        hdnTotalExpenseApproved.value = sum_Approvedamt;
        
        hdn_ClosingBalance.value = val(hdn_OpeningBalance.value) - val(hdnTotalExpenseApproved.value);
        lbl_ClosingBalance.innerHTML = hdn_ClosingBalance.value;
        
        hdn_CalculatedClosingBalance.value = val(hdn_CalculatedOpeningBalance.value) - val(hdnTotalExpenseApproved.value);
        lbl_CalculatedClosingBalance.innerHTML = hdn_CalculatedClosingBalance.value;
        
        var chk_LastTrip = document.getElementById('<%=chk_LastTrip.ClientID %>');
 
        if (chk_LastTrip.checked == false)
        {
            hdn_ExpectedAdvance.value = 3000 - val(hdn_ClosingBalance.value);
            lbl_ExpectedAdvance.innerHTML = hdn_ExpectedAdvance.value;
            lbl_CalculatedExpectedAdvance.innerHTML = 3000 - val(hdn_CalculatedClosingBalance.value);
        }
        else
        {
            hdn_ExpectedAdvance.value = val(hdn_ClosingBalance.value) * -1;
            lbl_ExpectedAdvance.innerHTML = hdn_ExpectedAdvance.value;
            lbl_CalculatedExpectedAdvance.innerHTML = val(hdn_CalculatedClosingBalance.value) * -1;
        }
                
        
        var txt_AdditionalAdvance = document.getElementById('<%=txt_AdditionalAdvance.ClientID %>');
        var lbl_TotalAdvanceToBeGiven = document.getElementById('<%=lbl_TotalAdvanceToBeGiven.ClientID %>');
        var lbl_CalculatedTotalAdvanceToBeGiven = document.getElementById('<%=lbl_CalculatedTotalAdvanceToBeGiven.ClientID %>');
        var hdn_TotalAdvanceToBeGiven = document.getElementById('<%=hdn_TotalAdvanceToBeGiven.ClientID %>');
        
         lbl_TotalAdvanceToBeGiven.innerHTML = val(lbl_ExpectedAdvance.innerHTML) + val(txt_AdditionalAdvance.value);
         lbl_CalculatedTotalAdvanceToBeGiven.innerHTML = val(lbl_CalculatedExpectedAdvance.innerHTML) + val(txt_AdditionalAdvance.value);
         hdn_TotalAdvanceToBeGiven.value = lbl_TotalAdvanceToBeGiven.innerHTML;
            
}

 function CalculateTotalAdvanceToBeGiven()
    { 
    
        var txt_AdditionalAdvance = document.getElementById('<%=txt_AdditionalAdvance.ClientID %>');
        
        var lbl_ExpectedAdvance = document.getElementById('<%=lbl_ExpectedAdvance.ClientID %>');
        var hdn_ExpectedAdvance = document.getElementById('<%=hdn_ExpectedAdvance.ClientID %>');
        var lbl_CalculatedExpectedAdvance = document.getElementById('<%=lbl_CalculatedExpectedAdvance.ClientID %>');
        var lbl_TotalAdvanceToBeGiven = document.getElementById('<%=lbl_TotalAdvanceToBeGiven.ClientID %>');
        var hdn_TotalAdvanceToBeGiven = document.getElementById('<%=hdn_TotalAdvanceToBeGiven.ClientID %>');
        var lbl_CalculatedTotalAdvanceToBeGiven = document.getElementById('<%=lbl_CalculatedTotalAdvanceToBeGiven.ClientID %>');
     
        if(val(lbl_ExpectedAdvance.innerHTML)>=0)
        {
            lbl_TotalAdvanceToBeGiven.innerHTML = val(lbl_ExpectedAdvance.innerHTML) + val(txt_AdditionalAdvance.value);
            lbl_CalculatedTotalAdvanceToBeGiven.innerHTML = val(lbl_CalculatedExpectedAdvance.innerHTML) + val(txt_AdditionalAdvance.value);
            hdn_TotalAdvanceToBeGiven.value = lbl_TotalAdvanceToBeGiven.innerHTML;
        }
        else
        {
            lbl_TotalAdvanceToBeGiven.innerHTML =  val(txt_AdditionalAdvance.value);
            lbl_CalculatedTotalAdvanceToBeGiven.innerHTML =  val(txt_AdditionalAdvance.value);
            hdn_TotalAdvanceToBeGiven.value = lbl_TotalAdvanceToBeGiven.innerHTML;
        }
        
        return;
    }


function viewwindow_DriverHistory(DriverHistory)
{
          
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-50);
        var popH = h-200;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
 
        window.open(DriverHistory, 'DriverTripHistory', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


function LastTripClick()
{
    var chk_LastTrip = document.getElementById('<%=chk_LastTrip.ClientID %>');
    var chk_VehicleChange = document.getElementById('<%=chk_VehicleChange.ClientID %>');
    
    var lbl_ExpectedAdvance = document.getElementById('<%=lbl_ExpectedAdvance.ClientID %>');
    
    var tr_DriverBalanceDeposit = document.getElementById('tr_DriverBalanceDeposit'); 
    
   
    
    if (chk_LastTrip.checked == true)
    {
        if (chk_VehicleChange.checked == true)
        {
             chk_VehicleChange.checked = false;
        }
        
        if(val(lbl_ExpectedAdvance.innerHTML)<0)
        {
            
            tr_DriverBalanceDeposit.style.display = 'inline'; 
        }
        
    }
    else
    {   
        tr_DriverBalanceDeposit.style.display = 'none'; 
    }
}


function VehicleChangeClick()
{
    var chk_LastTrip = document.getElementById('<%=chk_LastTrip.ClientID %>');
    var chk_VehicleChange = document.getElementById('<%=chk_VehicleChange.ClientID %>');
    
    var tr_DriverBalanceDeposit = document.getElementById('tr_DriverBalanceDeposit'); 
    
    if (chk_VehicleChange.checked == true)
    {
        if (chk_LastTrip.checked == true)
        {
             chk_LastTrip.checked = false;
        }
        
        tr_DriverBalanceDeposit.style.display = 'none'; 
    }
}


function AdjustInSalaryClick()
{
    var chk_AdjustInSalary = document.getElementById('<%=chk_AdjustInSalary.ClientID %>');
    var lbl_BalanceDepositToBranch = document.getElementById('<%=lbl_BalanceDepositToBranch.ClientID %>');
    var DDLBalanceDepositBranch = document.getElementById('<%=DDLBalanceDepositBranch.ClientID %>');
    
    var td_DepositeBranch = document.getElementById('td_DepositeBranch');
    
    
    if (chk_AdjustInSalary.checked == true)
    {
        td_DepositeBranch.style.display = 'none'; 
        
    }
    else
    {
        td_DepositeBranch.style.display = 'inline'; 
        
    }
    
}

function Allow_To_Save()
{
    var ATS = false;
   
        ATS = true;
    
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Trip Expense Approval</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%" border="0">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Vehicle Trip Expense Approval"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Date:</td>
                    <td style="width: 15%">
                        <uc1:WucDatePicker ID="dtpTripExpenseDate" runat="server" />
                    </td>                   
                    <td style="width: 15%"></td>
                    <td style="width: 10%">Trip No. :</td>
                    <td style="width: 45%">
                        &nbsp<asp:Label ID="lbl_TripNo" runat="server" Text="000" Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                    </td>                   
                </tr>
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">Vehicle No:</td>
                    <td style="width: 15%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_VehicleNo" runat="server" Font-Bold="true" Text="VehicleNo"></asp:Label></td>
                   
                    <td style="width: 15%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">
                        <asp:Label ID="lbl_PreviousTripWeightH" runat="server" Text="Previous Trip Weight :"></asp:Label></td>
                    <td style="width: 45%; height: 15px;">
                        <asp:Label ID="lbl_PreviousTripWeight" runat="server" Font-Bold="true" Text="PreviousTripWeight"
                            ForeColor="DarkRed"></asp:Label></td>                   
                </tr>               
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">From Date :</td>
                    <td style="width: 15%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_FromDate" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                   
                    <td style="width: 15%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">To Date :</td>
                    <td style="width: 45%; height: 15px;">
                        &nbsp<asp:Label ID="lbl_ToDate" runat="server" Text="" Font-Bold="true" ForeColor=""></asp:Label>
                    </td>
                </tr>               
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">Driver :</td>
                    <td style="width: 15%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_DriverName" runat="server" Font-Bold="true" Text="DriverName"></asp:Label></td>
                    <td style="width: 15%; height: 15px;">
                        <asp:Button ID="btn_DriverHistory" runat="server" CssClass="BUTTON" Text="History"
                            AccessKey="H" Width="80%" BackColor="MediumSpringGreen" /></td>
                    <td style="width: 10%; height: 15px;">Cleaner :</td>
                    <td style="width: 45%; height: 15px;">
                        <asp:Label ID="lbl_Cleaner" runat="server" Font-Bold="true" Text="ClearName"></asp:Label></td>                   
                </tr>
                <tr>
                    <td style="width: 5%; height: 15px;">
                    </td>
                    <td style="width: 10%; height: 15px;">Previous Route :&nbsp;</td>
                    <td style="width: 15%; height: 15px;">
                        &nbsp;<asp:Label ID="lbl_PreviousRoute" runat="server" Text="NA" Font-Bold="true"></asp:Label></td>
                    <td style="width: 15%; height: 15px; text-align: center;">
                        </td>
                    <td style="width: 10%; height: 15px;">Return Route :</td>
                    <td style="width: 45%; height: 15px;">
                        <asp:Label ID="lbl_ReturnRoute" runat="server" Text="NA" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_IsEmptyReturn" runat="server" Font-Bold="true" ForeColor="Red"  Text="Empty Return"></asp:Label></td>                    
                </tr>
                <tr>
                    <td style="width: 5%; height: 15px;"></td>
                    <td style="width: 10%; height: 15px;">Opening Balance :</td>
                    <td style="width: 15%; height: 15px;" align="right">
                        &nbsp;<asp:Label ID="lbl_OpeningBalance" runat="server" Font-Bold="true" Text="0.00"
                            ForeColor="DarkRed"></asp:Label>
                        <asp:HiddenField ID="hdn_OpeningBalance" runat="server" Value="0"></asp:HiddenField>
                    </td>
                    <td style="width: 15%; height: 15px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_CalculatedOpeningBalance" runat="server" Font-Bold="true" Text="10000.00" ForeColor="DarkGreen"></asp:Label>
                        <asp:HiddenField ID="hdn_CalculatedOpeningBalance" runat="server" Value="10000.00"></asp:HiddenField>
                    </td>
                    <td style="width: 10%; height: 15px;">Current Route :</td>
                    <td style="width: 45%; height: 15px;">
                        <asp:Label ID="lbl_CurrentRoute" runat="server" Text="NA" Font-Bold="true" ForeColor="DarkRed"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_IsEmptyTrip" runat="server" Font-Bold="true" ForeColor="Red" Text="Empty Trip"></asp:Label></td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr style="height: 350px">
                    <td style="width: 40%">
                        <asp:Panel ID="pnl_TripExpense" runat="server" Height="350px" ScrollBars="None">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DataGrid ID="dg_GridTripExpense" runat="server" ShowFooter="False" AllowPaging="False"
                                        CssClass="GRID" AllowSorting="False" AllowCustomPaging="False" AutoGenerateColumns="False"
                                        PageSize="15" OnItemDataBound="dg_GridTripExpense_ItemDataBound">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Orange" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="SrNo" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="TripExpenseHeadID" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTripExpenseHeadID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TripExpenseHeadID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expense Head" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ExpenseHead" runat="server" Font-Bold="true" Width="300px" Text='<%# DataBinder.Eval(Container.DataItem, "TripExpenseHead")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expense" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Expense" runat="server" CssClass="TEXTBOXNOSASLABEL" Width="80px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Expense")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Approved" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%-- OnTextChanged="txtApproved_TextChanged" AutoPostBack="true" --%>
                                                    <asp:TextBox ID="txtApproved" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Integers(this,event)"
                                                        Width="80px" onfocus="txtbox_onfocus(this)" onfocusout="txtbox_onlostfocus(this);"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                        onfocus="txtbox_onfocus(this)" Width="300px" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 48%" valign="top">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_Advance" runat="server" Height="250px" ScrollBars="None" Font-Bold="true">
                                    <asp:LinkButton ID="lnk_btnBranchClosingBalance" runat="server" Text="Branch Wise Cash Balance"
                                        Font-Bold="true" Height="16px" BackColor="LemonChiffon" ForeColor="#330099" Width="100%"></asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblAdvanceDetails" runat="server" Text="Advance Details" Font-Bold="true" Height="16px" BackColor="Orange" Width="100%"></asp:Label>
                                    <br />
                                    <asp:DataGrid ID="dg_AdvanceDetails" runat="server" CellPadding="3" CssClass="Grid"
                                        AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_AdvanceDetails_CancelCommand"
                                        OnDeleteCommand="dg_AdvanceDetails_DeleteCommand" OnEditCommand="dg_AdvanceDetails_EditCommand"
                                        OnItemCommand="dg_AdvanceDetails_ItemCommand" OnItemDataBound="dg_AdvanceDetails_ItemDataBound"
                                        OnUpdateCommand="dg_AdvanceDetails_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <%--<HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>--%>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Branch">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_AdvanceBranch" runat="server" Width="100%" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_AdvanceBranch_SelectedIndexChanged" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Branch_Name")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="40%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="40%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_AdvanceBranch" runat="server" Width="98%" CssClass="DROPDOWN"
                                                        AutoPostBack="true" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Closing Cash" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle Width="20%" HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_ClosingCash" runat="server" Font-Bold="true" Width="95%" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingCash")%>'></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ClosingCash" runat="server" Font-Bold="true" Width="95%" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingCash")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Advance">
                                                <HeaderStyle Width="20%" HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" MaxLength="10" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_GridAdvanceDetails" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_GridAdvanceDetails" Text="Delete"
                                                        CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_TotalAdvanceHeader" runat="server" Text="Total Advance : " Font-Bold="true"></asp:Label>&nbsp&nbsp
                                <asp:Label ID="lbl_TotalAdvance" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalAdvance" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="height: 50px">
                    <td class="TD1" style="width: 100%" colspan="3">
                        <table class="TABLE" border="0">
                            <tr>
                                <td class="TD1" style="width: 40%; height: 20px;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            Total :
                                            <asp:Label ID="lblTotalTripExpense" runat="server" Text="1000.00" Font-Bold="true"></asp:Label>&nbsp&nbsp
                                            <asp:HiddenField ID="hdnTotalTripExpense" runat="server" Value="0"></asp:HiddenField>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblTotalExpenseApproved" runat="server" Text="1000.00" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:HiddenField ID="hdnTotalExpenseApproved" runat="server" Value="0"></asp:HiddenField>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; height: 20px;" align="left">
                                   <asp:Label ID="lblCalculatedTotalExpenseApproved" runat="server" Text="0.00" Font-Bold="true" ForeColor="DarkGreen"></asp:Label></td>
                                <td class="TD1" style="width: 50%; height: 20px;">
                                    &nbsp</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 40%; height: 20px;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            Closing Balance :
                                            <asp:Label ID="lbl_ClosingBalance" runat="server" Text="2345.00" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                            <asp:HiddenField ID="hdn_ClosingBalance" runat="server" Value="0"></asp:HiddenField>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%;" align="left">
                                    <asp:Label ID="lbl_CalculatedClosingBalance" runat="server" Text="0.00" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
                                    <asp:HiddenField ID="hdn_CalculatedClosingBalance" runat="server" Value="0"></asp:HiddenField>
                                </td>
                                <td style="width: 50%; height: 20px;" align="left">
                                    &nbsp; &nbsp;
                                    <asp:CheckBox ID="chk_LastTrip" runat="server" CssClass="CHECKBOX" Text="Is Last Trip ?"
                                        Font-Bold="true" ForeColor="Red" onclick="Calculate_ApprovedAmount('txtApproved','dg_GridTripExpense'); LastTripClick();" />
                                    &nbsp &nbsp &nbsp
                                    <asp:CheckBox ID="chk_VehicleChange" runat="server" CssClass="CHECKBOX" Text="Is Vehicle Change ?"
                                        Font-Bold="true" ForeColor="Indigo" onclick="Calculate_ApprovedAmount('txtApproved','dg_GridTripExpense'); VehicleChangeClick(); Calculate_ApprovedAmount('txtApproved','dg_GridTripExpense');" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 40%; height: 30px;">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            Expected Advance :
                                            <asp:Label ID="lbl_ExpectedAdvance" runat="server" Text="2345.00" Font-Bold="true"
                                                ForeColor="DarkRed"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:HiddenField ID="hdn_ExpectedAdvance" runat="server" Value="0"></asp:HiddenField>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; height: 30px;" align="left">
                                    <asp:Label ID="lbl_CalculatedExpectedAdvance" runat="server" Text="0.00" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
                                </td>
                                <td style="width: 50%; height: 30px;" align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 40%; height: 30px;">
                                    Additional Advance :
                                    <asp:TextBox ID="txt_AdditionalAdvance" runat="server" Text="0" MaxLength="50" Width="40%"
                                        onblur="txtbox_onlostfocus(this);CalculateTotalAdvanceToBeGiven();" onfocus="txtbox_onfocus(this)"
                                        onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                                <td style="width: 10%; height: 30px;" align="left"> &nbsp;</td>
                                <td style="width: 50%; height: 30px;" align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 40%; height: 30px;">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            Total Advance To Be Given :
                                            <asp:Label ID="lbl_TotalAdvanceToBeGiven" runat="server" Font-Bold="true" ForeColor="DarkRed" Text="0.00"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:HiddenField ID="hdn_TotalAdvanceToBeGiven" runat="server" Value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" EventName="ItemCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; height: 30px;" align="left">
                                    <asp:Label ID="lbl_CalculatedTotalAdvanceToBeGiven" runat="server" Font-Bold="true" ForeColor="DarkGreen" Text="0.00"></asp:Label>
                                </td>
                                <td style="width: 50%; height: 30px;" align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="tr_DriverBalanceDeposit" runat="server" style="display: none;">
                                <td class="TD1" style="width: 40%; height: 30px;">
                                    <asp:Label ID="lbl_DriverBalance" runat="server" Font-Bold="true" ForeColor="DarkRed"
                                        Text="Balance of Rs. "></asp:Label>
                                    <asp:TextBox ID="txt_DriverBalance" runat="server" CssClass="TEXTBOX" MaxLength="50"
                                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                                        Text="0" Width="20%"></asp:TextBox></td>
                                <td style="width: 10%; height: 30px;" align="left"><asp:CheckBox ID="chk_AdjustInSalary" runat="server" CssClass="CHECKBOX" Text="Adjust With Salary ?"
                                        Font-Bold="true" ForeColor="Red" onclick="AdjustInSalaryClick();" />
                                   
                                </td>
                                <td style="width: 50%; height: 30px;" align="left" runat="server" id="td_DepositeBranch">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_BalanceDepositToBranch" runat="server" Font-Bold="true" ForeColor="DarkRed"
                                        Text="Deposited In Branch : "></asp:Label> &nbsp;&nbsp;&nbsp; <cc1:DDLSearch ID="DDLBalanceDepositBranch" runat="server" AllowNewText="False" CallBackAfter="2"
                                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" InjectJSFunction=""
                                                IsCallBack="True" PostBack="True" Text="" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chk_LastTrip" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="TABLE" width="100%" border="0">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80%">
                                    <asp:TextBox ID="txt_Remarks" Height="30px" runat="server" CssClass="TEXTBOX" MaxLength="250"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2">
                        &nbsp;<asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="dg_GridTripExpense" />
                                <asp:AsyncPostBackTrigger ControlID="dg_AdvanceDetails" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TD1" style="text-align: center">
                        <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                            OnClientClick="windowClose()" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnKeyID" runat="server" Value="0" />
            &nbsp; &nbsp;&nbsp;
            <asp:HiddenField ID="hdnTripExpenseID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDriver_ID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnCanCloseTrip" runat="server" Value="0" />
            &nbsp; &nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
