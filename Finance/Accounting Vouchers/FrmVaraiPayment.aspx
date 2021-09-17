<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVaraiPayment.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmVaraiPayment" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript">

var Search_Type;
var lst_control_id;
function Search_txtSearch(e,txtbox,lstBox,SearchType,length)
{    

    Search_Type = SearchType;
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    
    var txtvalue = txtbox.value.toUpperCase();

    
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                if (Search_Type == 'Varnar')
                {
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchVarnar(txtvalue,handleResults);
                }
                else if (Search_Type == 'Vehicle')
                {
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchVehicle(txtvalue,handleResults);
                }

            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function handleResults(results)
{  

  var list_control = document.getElementById(lst_control_id);

 
  var tot = results.value.Rows.length -1;
  var count = 0;
 
  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  { 
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }
  

    if (list_control.options.length == 0)
      hidecontrol(list_control);
    else
      showcontrol(list_control);


}

function On_txtLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != txtbox_value)
    {
    
        if (list_control_index != -1){
            list_control_value = listcontrol.options[list_control_index].value;
            list_control_text = listcontrol.options[list_control_index].text;
        }
        else{
            list_control_value = '0';
            list_control_text = '';
        }
    
        document.getElementById(hdn_control).value = list_control_value;
        document.getElementById(txtbox).value = list_control_text;

       
        if (Search_Type == 'Varnar')
        {
            Raj.EF.CallBackFunction.CallBack.GetTxtVarnarDetails(list_control_value,handleVarnarInfo);
        }
        
    }

}

function handleVarnarInfo(results)
{  

    var varnarid = results.value.Rows[0]["Varnar_Id"];
    var mobileno = results.value.Rows[0]["Mobile_No"];
    var accountno = results.value.Rows[0]["Account_No"];
    var ifsccode = results.value.Rows[0]["IFSC_Code"];
    var bankname = results.value.Rows[0]["Bank_Name"];
    var beneficiary =  results.value.Rows[0]["Beneficiary_Name"];
    var beneficiarymobile =  results.value.Rows[0]["Beneficiary_Mobile"];
    



    document.getElementById("lbl_MobileNo").innerHTML = mobileno;
    document.getElementById("lbl_AccountNo").innerHTML = accountno;
    document.getElementById("lbl_IFSCCode").innerHTML = ifsccode;
    document.getElementById("lbl_BankName").innerHTML = bankname;
    document.getElementById("lbl_Beneficiary").innerHTML = beneficiary;
    document.getElementById("lbl_BeneficiaryMobile").innerHTML = beneficiarymobile;
    

    document.getElementById("hdn_MobileNo").value = mobileno;
    document.getElementById("hdn_AccountNo").value = accountno;
    document.getElementById("hdn_IFSCCode").value = ifsccode;
    document.getElementById("hdn_BankName").value = bankname;
    document.getElementById("hdn_Beneficiary").value = beneficiary;
    document.getElementById("hdn_BeneficiaryMobile").value = beneficiarymobile;


    document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_hidden.ClientID%>').click();

}

function Onblur_QtyRate(txtQty,txtRate,txtAmount)
    {

        var txtQty=document.getElementById(txtQty);
        var txtRate=document.getElementById(txtRate);
        var txtAmount=document.getElementById(txtAmount);
                     
                                      
        txtAmount.value = val(txtQty.value) * val(txtRate.value);
  
   }


function BankClicked()
{
    var trid = document.getElementById("tr_Bank");
    
    if (trid != null) 
    {
        trid.style.display = 'block';
    }
    
}

function OtherClicked()
{

    var trid = document.getElementById("tr_Bank");
    
    if (trid != null) 
    {
        trid.style.display = 'none';
    }

}


function Add_New_Varnar_Window()
{
var hdn_Varnar_Path = document.getElementById('hdn_Varnar_Path');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;


if(hdn_Varnar_Path.value == '')
  {
  alert('You Don"t Have Rights to Add Varnar');
  }
else
  {
  window.open(hdn_Varnar_Path.value, 'Varnar', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}


function Allow_To_Save()
{
    var ATS = false;
    var hdn_Varnarld = document.getElementById('hdn_Varnarld');
    var txtVarnar = document.getElementById('txtVarnar');
    var hdn_Total = document.getElementById('hdn_Total');


    var lblErrors = document.getElementById('lblErrors');
    
    
    if (val(hdn_Varnarld.value)  <= 0)
    {
        lblErrors.innerHTML = 'Please Select Varnar';
        txtVarnar.focus();
    }
    else if (val(hdn_Total.value)  <= 0)
    {
        lblErrors.innerHTML = 'Total Cannot Be Zero';
    }
    
    else
    {
        ATS = true;
    }
    return ATS;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Varai Payment</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%" colspan="2">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Varai Payment"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70%;">
                        <table style="width: 100%">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_VoucherDate" CssClass="LABEL" Text="Date :" runat="server"></asp:Label>&nbsp;</td>
                                <td style="width: 79%">
                                    <uc1:WucDatePicker ID="dtpVoucherDate" runat="server"></uc1:WucDatePicker>
                                </td>
                                <td style="width: 1%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_VoucherNo" CssClass="LABEL" Text="Voucher No :" runat="server"></asp:Label>
                                </td>
                                <td style="width: 79%">
                                    <asp:Label ID="lbl_VoucherNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
                                <td style="width: 1%">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    Varnar :</td>
                                <td style="width: 79%;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtVarnar" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                                onblur="On_txtLostFocus('txtVarnar','lstVarnar','hdn_Varnarld'); txtbox_onlostfocus(this);"
                                                onkeyup="Search_txtSearch(event,this,'lstVarnar','Varnar',2);" onkeydown="return on_keydown(event,'txtVarnar','lstVarnar');"
                                                onfocus="On_Focus('txtVarnar','lstVarnar'); txtbox_onfocus(this);" MaxLength="150"
                                                EnableViewState="False" Width="40%"></asp:TextBox>
                                            <asp:ListBox ID="lstVarnar" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtVarnar')"
                                                TabIndex="5" runat="server"></asp:ListBox>
                                            <asp:HiddenField ID="hdn_Varnarld" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdn_Varnar_Path" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdn_EncreptedVarnarId" Value="0" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtVarnar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:LinkButton ID="lbtn_AddVarnar" runat="server" OnClientClick="return Add_New_Varnar_Window();"
                                        Text="Add New"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtn_EditVarnar" runat="server" OnClientClick="return Add_New_Varnar_Window();"
                                        Text="Edit"></asp:LinkButton>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                    *</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    Mobile No. :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_MobileNo" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_MobileNo" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td class="TD1" style="width: 20%; height: 15px;">
                                    Payment Mode :</td>
                                <td style="width: 79%; height: 15px;">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlPayType" runat="server" CssClass="DROPDOWN" Width="40%"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                                    *</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    Beneficiary Name :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_Beneficiary" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_Beneficiary" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr id="tr_BeneficiaryMobile" runat="server">
                                <td class="TD1" style="width: 20%;">
                                    Beneficiary Mobile :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_BeneficiaryMobile" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_BeneficiaryMobile" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr id="tr_Blank1" runat="server">
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr id="tr_AccountNo" runat="server">
                                <td class="TD1" style="width: 20%;">
                                    Account No. :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_AccountNo" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_AccountNo" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr id="tr_Blank2" runat="server">
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr id="tr_IFSCCode" runat="server">
                                <td class="TD1" style="width: 20%;">
                                    IFSC Code :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_IFSCCode" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_IFSCCode" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr id="tr_Blank3" runat="server">
                                <td class="TD1" style="width: 100%;" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr id="tr_BeneficiaryBank" runat="server">
                                <td class="TD1" style="width: 20%;">
                                    Bank :</td>
                                <td style="width: 79%;">
                                    <asp:Label ID="lbl_BankName" runat="server" Font-Bold="true" ForeColor="#660099"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="hdn_BankName" Value="" runat="server" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 100%;" colspan="6">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%" valign="top" align="center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataGrid ID="dg_Grid" AutoGenerateColumns="False" ShowFooter="False" CellPadding="3"
                                    CssClass="Grid" runat="server" Width="80%">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Packing">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PackingType" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing_Type")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Rate/Parcel" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Rate" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Last Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_LastRate" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastRate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td style="width: 20%; height: 15px;" class="TD1">
                        &nbsp;</td>
                    <td style="width: 80%;" colspan="5">
                        <asp:UpdatePanel ID="upnl_Comm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_Commodity" class="DIV" style="height: 150px; width: 100%; text-align: left">
                                    <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                        ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                        OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                        OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand"
                                        Width="85%">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Vehicle">
                                                <HeaderStyle Width="20%" HorizontalAlign="left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_VehicleNo" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="20" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_VehicleNo" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_VehicleId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Vehicle_No")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_VehicleNo" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="20" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem, "Vehicle_No") %>'></asp:TextBox>
                                                    <asp:ListBox ID="lst_VehicleNo" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_VehicleId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Vehicle_Id") %>'>
                                                    </asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="LRNo">
                                                <HeaderStyle Width="20%" HorizontalAlign="left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_LRNo" Width="95%" runat="server" CssClass="TEXTBOX" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "LRNo")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_LRNo" Width="95%" runat="server" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "LRNo") %>'
                                                        onkeyPress="return Only_Numbers(this,event);" MaxLength="10" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Qty">
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <ItemStyle Width="10%" HorizontalAlign="right"></ItemStyle>
                                                <FooterStyle Width="10%" HorizontalAlign="right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Qty" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="5" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Qty"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Qty" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Qty")) %>'
                                                        onkeyPress="return Only_Numbers(this,event);" MaxLength="5" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Packing">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_Packing" runat="server" Width="98%" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Packing_Type"))%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_Packing" runat="server" Width="98%" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Rate">
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <ItemStyle Width="10%" HorizontalAlign="right"></ItemStyle>
                                                <FooterStyle Width="10%" HorizontalAlign="right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Rate" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="6" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Rate" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate")) %>'
                                                        MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount">
                                                <HeaderStyle Width="20%" HorizontalAlign="right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="8" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
                                                        MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td style="width: 29%;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lbl_TotalH" runat="server" Font-Bold="true" ForeColor="#660099" Text="Total : "></asp:Label>
                                <asp:Label ID="lbl_Total" runat="server" Font-Bold="true" ForeColor="#660099" Text="0.00"></asp:Label>
                                <asp:HiddenField ID="hdn_Total" runat="server" Value="0" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; height: 15px;" class="TD1">
                        Remark :</td>
                    <td colspan="5" style="width: 80%; height: 15px;">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" MaxLength="500" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%;" colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Paid Through :&nbsp;</td>
                    <td style="width: 29%;">
                        <asp:RadioButtonList ID="rdl_PayThrough" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem OnClick="BankClicked();" Selected="True" Text="Bank" Value="1"></asp:ListItem>
                            <%--<asp:ListItem OnClick="OtherClicked();" Text="Cash" Value="2"></asp:ListItem>--%>
                            <asp:ListItem OnClick="OtherClicked();" Text="Cash Paid By Driver" Value="3"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td style="width: 29%;">
                        &nbsp;</td>
                    <td style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%;" colspan="6">
                        &nbsp;</td>
                </tr>
                <tr id="tr_Bank" runat="server">
                    <td class="TD1" style="width: 20%;">
                        Payee Bank :&nbsp;</td>
                    <td style="width: 29%;">
                        <asp:DropDownList ID="ddl_BankLedger" runat="server" CssClass="DROPDOWN" Width="95%">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td style="width: 29%;">
                        &nbsp;</td>
                    <td style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%;" colspan="6">
                        &nbsp;</td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdnKeyID" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center;">
                <tr>
                    <td style="width: 100%">
                        &nbsp;<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            OnClientClick="return Allow_To_Save()" OnClick="btn_Save_Exit_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btn_Close_Click">
                        </asp:Button>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_hidden" runat="server" Text="" OnClick="btn_hidden_Click" Style="display: none" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">

function call_UpdateVarnarMaster(VarnarName,MobileNo,BeneficiaryName,BeneficiaryMobile,AccNo,IFSCCode,Bank)
{
    SetVarnarMaster(VarnarName,MobileNo,BeneficiaryName,BeneficiaryMobile,AccNo,IFSCCode,Bank);
}


function SetVarnarMaster(VarnarName,MobileNo,BeneficiaryName,BeneficiaryMobile,AccNo,IFSCCode,Bank)
{  
    document.getElementById("txtVarnar").text = VarnarName;
    document.getElementById("txtVarnar").value = VarnarName;

    document.getElementById("lbl_MobileNo").innerHTML = MobileNo;
    document.getElementById("hdn_MobileNo").value = MobileNo;

    document.getElementById("lbl_Beneficiary").innerHTML = BeneficiaryName;
    document.getElementById("hdn_Beneficiary").value = BeneficiaryName;

    document.getElementById("lbl_BeneficiaryMobile").innerHTML = BeneficiaryMobile;
    document.getElementById("hdn_BeneficiaryMobile").value = BeneficiaryMobile;

    document.getElementById("lbl_AccountNo").innerHTML = AccNo;
    document.getElementById("hdn_AccountNo").value = AccNo;
    
    document.getElementById("lbl_IFSCCode").innerHTML = IFSCCode;
    document.getElementById("hdn_IFSCCode").value = IFSCCode;
    
    document.getElementById("lbl_BankName").innerHTML = Bank;
    document.getElementById("hdn_BankName").value = Bank;

    
}
</script>
