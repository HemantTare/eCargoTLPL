<%@ Page AutoEventWireup="true" CodeFile="FrmPendingFreightBookingDetails.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmPendingFreightBookingDetails" Language="C#" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>

<script type="text/javascript" src="../../../Javascript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function Check_Single(chkid,gridname)
    {
       	var chk = document.getElementById(chkid);
        var row = chk.parentElement.parentElement.parentElement;
        
        var lbl_TotalReceivable = document.getElementById('lbl_TotalReceivable');
        var hdn_TotalReceivable = document.getElementById('hdn_TotalReceivable');
        
        txt_TotalFreight = row.cells[4].getElementsByTagName('input')[0].value;
        if(chk.checked == true)
        {
           hdn_TotalReceivable.value = parseInt(hdn_TotalReceivable.value) + parseInt(txt_TotalFreight);
           lbl_TotalReceivable.innerHTML = hdn_TotalReceivable.value;
        }
        else
        {
            hdn_TotalReceivable.value = parseInt(hdn_TotalReceivable.value) - parseInt(txt_TotalFreight); 
	        lbl_TotalReceivable.innerHTML = hdn_TotalReceivable.value;
        }
    }
    
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

function viewwindow_general(Path)
{
 
//        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
//        var Path='../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var Path=Path;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_DeliveryArea(PathDlyStk)
{
          
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2; 
                    
        window.open(PathDlyStk, 'CustomPopUpDeliveryArea', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ClientConsignor(ClientId,IsRegular)
{
        if(IsRegular == 'True')
        {
            var Path='../../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        }
        else
        {
            var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        }
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'CustomPopUpAUS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function Open_Details_Window(Path)
{ 
  window.open(Path,'PendingFrtPrint','width=1000,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
  return false;
} 

function Allow_To_Save()
{

    var ATS = false;
    var hdn_TotalReceivable = document.getElementById("hdn_TotalReceivable");
    var txt_CashAmount = document.getElementById("txt_CashAmount");
    var txt_ChequeAmount = document.getElementById("txt_ChequeAmount");
    var txt_ChequeNo = document.getElementById("txt_ChequeNo");
    var txt_ChequeBank = document.getElementById("txt_ChequeBank");
    var txt_TDS = document.getElementById("txt_TDS");
    

    
    var lbl_Errors = document.getElementById("lbl_Errors");
    if (val(hdn_TotalReceivable.value) <= 0)
    {
        lbl_Errors.innerHTML = 'Total Receivable value should be greater than zero.';
    }
    else if((val(txt_CashAmount.value) + val(txt_ChequeAmount.value)) == 0)
    {
        lbl_Errors.innerHTML = 'Cash Amount And Cheque Amount Both Should Not Be Zero.';
        txt_CashAmount.focus();
    }
    else if((val(txt_CashAmount.value) + val(txt_ChequeAmount.value) + val(txt_TDS.value)) != val(hdn_TotalReceivable.value))
    {
        lbl_Errors.innerHTML = 'Cash Amount + Cheque Amount + TDS Amount Should be equal to Total Receivable Amount.';
        txt_CashAmount.focus();
    }
    else if(val(txt_ChequeAmount.value) > 0 && val(txt_ChequeAmount.value) < 50)
    {
        lbl_Errors.innerHTML = 'Cheque Value Cannot Be Less Than Rs. 50';
        txt_ChequeAmount.focus();
    }
    else if(val(txt_ChequeAmount.value) > 0 && txt_ChequeNo.value == '')
    {
        lbl_Errors.innerHTML = 'Please Enter Cheque No.';
        txt_ChequeNo.focus();
    }
    else if(val(txt_ChequeAmount.value) > 0 && txt_ChequeBank.value == '')
    {
        lbl_Errors.innerHTML = 'Please Enter Bank Name.';
        txt_ChequeBank.focus();
    }
    else
        ATS = true;
    
    return ATS;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Freight Booking</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Freight Booking"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                    <asp:Label ID="lbl_division" runat="server" CssClass="LABEL" Text="label" /></td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                                <asp:Label ID="lblBkgBranch" runat="server" Text="Branch :" CssClass="TEXTBOX" Font-Bold="True"></asp:Label></td>
                            <td style="width: 14%">
                                <asp:Label ID="txtlblBkgBranch" runat="server" Text="txtlblBkgBranch" CssClass="TEXTBOX"
                                    Font-Bold="True"></asp:Label></td>
                            <td class="TD1" style="width: 10%">
                                <asp:Label ID="lblConsignor" runat="server" Text="Consignor :" CssClass="TEXTBOX"
                                    Font-Bold="True"></asp:Label></td>
                            <td style="width: 38%">
                                <asp:Label ID="txtlblConsignor" runat="server" Text="txtlblConsignor" CssClass="TEXTBOX"
                                    Font-Bold="True"></asp:Label></td>
                            <td style="width: 9%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td style="width: 24%">
                                &nbsp;&nbsp;<asp:Button ID="btn_Print" runat="server" CssClass="BUTTON" Width="60px"
                                    OnClick="btn_Print_Click" Text="Print" />
                                &nbsp;
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="420px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="100">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Att" Checked='<%#DataBinder.Eval(Container.DataItem,"Att")%>'
                                                    OnClick="Check_Single(this.id,'dg_Grid');" runat="server" Style="text-align: center" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LRDate" HeaderText="LR Date"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="gc_caption No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "LRNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Articles" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_TotalArticles" Text='<%# DataBinder.Eval(Container.DataItem, "TotalArticles") %>'
                                                    runat="server" CssClass="TEXTBOXNOS" ReadOnly="True" BackColor="Transparent"
                                                    BorderColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_TotalFreight" Text='<%# DataBinder.Eval(Container.DataItem, "TotalFreight") %>'
                                                    runat="server" CssClass="TEXTBOXNOS" ReadOnly="True" BackColor="Transparent"
                                                    BorderColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="TABLE" width="100%">
            <tr id="tr_Paymentdetails" runat="server">
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_TotalReceivableH" runat="server" Font-Bold="True" Text="Total Receivable :"></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="lbl_TotalReceivable" runat="server" Font-Bold="True" Text="0"></asp:Label>
                    <asp:HiddenField ID="hdn_TotalReceivable" runat="server" Value="0"></asp:HiddenField>
                </td>
                <td style="width: 25%;">
                </td>
                <td style="width: 25%;">
                </td>
            </tr>
            <tr id="tr_Paymentdetails2" runat="server">
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_CashH" runat="server" Font-Bold="True" Text="Cash Amount : "></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txt_CashAmount" runat="server" Font-Bold="True" Text="" onblur="txtbox_onlostfocus(this);"
                        onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                        MaxLength="5"></asp:TextBox></td>
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_ChequeH" runat="server" Font-Bold="True" Text="Cheque Amount : "></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txt_ChequeAmount" runat="server" Font-Bold="True" Text="" onblur="txtbox_onlostfocus(this);"
                        onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                        MaxLength="5"></asp:TextBox></td>
            </tr>
            <tr id="tr_Paymentdetails3" runat="server">
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_ChequeNo" runat="server" Font-Bold="True" Text="Cheque No. : "></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txt_ChequeNo" runat="server" Font-Bold="True" Text="" onblur="txtbox_onlostfocus(this);"
                        onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"
                        MaxLength="6"></asp:TextBox></td>
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_ChequeDate" runat="server" Font-Bold="True" Text="Cheque Date : "></asp:Label>
                </td>
                <td style="width: 25%;">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server" />
                </td>
            </tr>
            <tr id="tr_Paymentdetails4" runat="server">
                <td style="width: 25%; text-align: right; height: 26px;">
                    <asp:Label ID="lbl_ChequeBank" runat="server" Font-Bold="True" Text="Bank : "></asp:Label>
                </td>
                <td style="width: 25%; height: 26px;">
                    <asp:TextBox ID="txt_ChequeBank" runat="server" Font-Bold="True" Text="" Width="400px"
                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" MaxLength="50"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; height: 26px;">
                </td>
                <td style="width: 25%; height: 26px;">
                </td>
            </tr>
            <tr id="tr_Paymentdetails5" runat="server">
                <td style="width: 25%; text-align: right; height: 26px;">
                    <asp:Label ID="lbl_TDS" runat="server" Font-Bold="True" Text="TDS Deducted By Party : " ForeColor="RED"></asp:Label>
                </td>
                <td style="width: 25%; height: 26px;">
                    <asp:TextBox ID="txt_TDS" runat="server" Font-Bold="True" Text="0"  onkeypress="return Only_Numbers(this,event);"
                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" MaxLength="6"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; height: 26px;">
                </td>
                <td style="width: 25%; height: 26px;">
                </td>
            </tr>
        </table>
        <br />
        <table class="TABLE" width="100%">
            <tr id="tr_Save" runat="server">
                <td style="width: 25%; text-align: center;">
                    <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="S"
                        OnClick="btn_Save_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: left;">
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
