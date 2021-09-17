<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClosingCashTrans.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmClosingCashTrans" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript">
function input_screen_action(action)
{
if (action == 'view')
  {
  tbl_input_screen.style.display='inline';
  }
else
  {
  tbl_input_screen.style.display='none';
  }
}
function ValidateSave()
{
 
   var hdfn_ClosingBalance = document.getElementById('hdfn_ClosingBalance').value;
   var txtRupeeTypeTotal = document.getElementById('txtRupeeTypeTotal');
   var lbl_Error = document.getElementById('lbl_Error');
    
   if (hdfn_ClosingBalance != txtRupeeTypeTotal.value)
   {
        lbl_Error.innerHTML = "Please Check Closing Balance and Rupees Values Entered"; 
        lbl_Error.style.display='inline';
        return false;
   }
   else
   {
        lbl_Error.style.display='none';
        return true;
   } 
}

function calRupeeCount(txtbox,rupeeType,lbl)
{ 
  var txt_box = txtbox.value * rupeeType;
  lbl.value = txt_box;
  lbl.innerHTML = txt_box; 
}

function CalculateRupeeDetails()
{
    var RupeeType1000 = document.getElementById('txtRupeeType1000').value;
    var RupeeType500 = document.getElementById('txtRupeeType500').value;
    var RupeeType100 = document.getElementById('txtRupeeType100').value;
    var RupeeType50 = document.getElementById('txtRupeeType50').value;
    var RupeeType20 = document.getElementById('txtRupeeType20').value;
    var RupeeType10 = document.getElementById('txtRupeeType10').value;
    var RupeeType5 = document.getElementById('txtRupeeType5').value;
    var RupeeType2 = document.getElementById('txtRupeeType2').value;
    var RupeeType1 = document.getElementById('txtRupeeType1').value;
    var RupeeTypePaisa = document.getElementById('txtRupeeTypePaisa').value;
    var RupeeTypeTotal = document.getElementById('txtRupeeTypeTotal').value;
    var txtRupeeTypeTotal = document.getElementById('txtRupeeTypeTotal');
    
    RupeeTypeTotal = '0'; 
    RupeeTypeTotal = parseInt(RupeeType1000 * 1000) 
                   + parseInt(RupeeType500 * 500) 
                   + parseInt(RupeeType100 * 100) 
                   + parseInt(RupeeType50 * 50)
                   + parseInt(RupeeType20 * 20) 
                   + parseInt(RupeeType10 * 10) 
                   + parseInt(RupeeType5 * 5) 
                   + parseInt(RupeeType2 * 2)
                   + parseInt(RupeeType1 * 1) 
                   + parseFloat(RupeeTypePaisa);
  
   txtRupeeTypeTotal.innerHTML = RupeeTypeTotal;                        
   txtRupeeTypeTotal.value = RupeeTypeTotal;                        
    
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Cash Balance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClosingCashBal" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Cash Balance"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right;">
                    <asp:Label ID="lbl_SelectDate" runat="server" Text="Select Date : "></asp:Label></td>
                <td style="width: 10%">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server"></uc2:WucDatePicker>
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');"></a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');"></a>
                </td>
                <td style="width: 50%">
                    &nbsp;<asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE" cellpadding="5" cellspacing="5">
            <tr>
                <td style="text-align: left; size: 30%; vertical-align:text-top">
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="DIV1" style="width: 100%; height: 99%">
                                <asp:Label ID="lbl_ClosingStock" runat="server" BackColor="#FFE0C0" Font-Bold="True"
                                    Font-Size="Large" ForeColor="#C04000" Text="      Closing Stock      " Visible="False"
                                    Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridClosingStock" runat="server" AllowCustomPaging="false" AllowPaging="false"
                                    AllowSorting="True" AutoGenerateColumns="false" CssClass="GRID" OnPageIndexChanged="dg_GridClosingStock_PageIndexChanged"
                                    ShowFooter="true" Width="99%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Left" HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Right" HeaderText="LR" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Right" HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalPkgs")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Right" HeaderText="ToPay Frt" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalToPay")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridClosingStock" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="text-align: left; size: 40%">
                    <asp:UpdatePanel ID="Upd_ClosingCashBal" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 400px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                                    AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                                    Width="99%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Description" HeaderText="Description">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Details" HeaderText="Details">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                            <asp:HiddenField ID="hdfn_ClosingBalance" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="text-align: center; vertical-align: top; size: 30%">
                    <asp:UpdatePanel ID="Upd_ClosingCashAmountDetails" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <table class="TABLE">
                                <tr>
                                    <td style="width: 30%" class="GRIDHEADERCSS">
                                        <asp:Label ID="lblRupeeType" runat="server" Text="Rupee Type" Font-Bold="true" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%" class="GRIDHEADERCSS">
                                        <asp:Label ID="lblRupeeCount" runat="server" Text="Rupee Count" Font-Bold="true"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 40%" class="GRIDHEADERCSS">
                                        <asp:Label ID="lblEqual" runat="server" Text="=" Font-Bold="true" CssClass="LABEL"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                    <td style="width: 40%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType1000" runat="server" Text="1000" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType1000" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,1000,lblTotalRupeeType1000);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType1000" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType500" runat="server" Text="500" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType500" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,500,lblTotalRupeeType500);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType500" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType100" runat="server" Text="100" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType100" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,100,lblTotalRupeeType100);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType100" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType50" runat="server" Text="50" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType50" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,50,lblTotalRupeeType50);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType50" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType20" runat="server" Text="20" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType20" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,20,lblTotalRupeeType20);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType20" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType10" runat="server" Text="10" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType10" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,10,lblTotalRupeeType10);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType10" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType5" runat="server" Text="5" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType5" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,5,lblTotalRupeeType5);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType5" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType2" runat="server" Text="2" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType2" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,2,lblTotalRupeeType2);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType2" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeType1" runat="server" Text="1" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeType1" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,1,lblTotalRupeeType1);"
                                            onkeypress="return Only_Integers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeType1" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;">
                                        <asp:Label ID="lblRupeeTypePaisa" runat="server" Text="Paisa" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRupeeTypePaisa" runat="server" Text="0" onblur="CalculateRupeeDetails();calRupeeCount(this,1,lblTotalRupeeTypePaisa);"
                                            onkeypress="return Only_Numbers(this,event);" CssClass="TEXTBOXNOS"></asp:TextBox>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalRupeeTypePaisa" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right;" class="GRIDFOOTERCSS">
                                        <asp:Label ID="lblRupeeTypeTotal" runat="server" Text="Total :" Font-Bold="true"
                                            CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td style="width: 30%" class="GRIDFOOTERCSS">
                                    </td>
                                    <td style="width: 40%; text-align: right;" class="GRIDFOOTERCSS">
                                        <asp:Label ID="txtRupeeTypeTotal" runat="server" Text="" Font-Bold="true" CssClass="LABEL"></asp:Label></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%--<tr>
                <td colspan="3" style="text-align: center; size: 70%">
                </td>
            </tr>--%>
            <tr>
                <td colspan="3" style="vertical-align: top; text-align: left; size: 30%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: top; text-align: center; size: 30%">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"
                        OnClientClick="return ValidateSave();" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: top; text-align: center; size: 30%">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
