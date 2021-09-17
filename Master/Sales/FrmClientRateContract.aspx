<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClientRateContract.aspx.cs"
    Inherits="Master_Sales_FrmClientRateContract" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">

function ddl_ContractForChange()
{
  var ddl_ContractFor = document.getElementById('ddl_ContractFor');
  var tr_Client = document.getElementById('tr_Client'); 
  var tr_ClientGroup = document.getElementById('tr_ClientGroup'); 


  if (ddl_ContractFor.value == "1")
  {
    tr_Client.style.display = 'inline';  
    tr_ClientGroup.style.display = 'none';  
  }
  else
  {
    tr_Client.style.display = 'none';  
    tr_ClientGroup.style.display = 'inline';    
  }
}


function txt_FOVPercentOnblur()
{
  var txt_FOVPercent = document.getElementById('txt_FOVPercent');
  var tr_FOVExemptUpTo = document.getElementById('tr_FOVExemptUpTo'); 
 
  if (val(txt_FOVPercent.value) > 0 )
  {
    tr_FOVExemptUpTo.style.display = 'inline';  
  }
  else
  {
    tr_FOVExemptUpTo.style.display = 'none';  
  }
}


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rate Contract</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_Comm" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Rate Contract"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 29%; text-align: right; height: 21px;">
                        <asp:Label ID="lbl_RateContractFor" runat="server" Text="Contract For :"></asp:Label>
                    </td>
                    <td style="width: 1%; height: 21px;">
                        <asp:DropDownList ID="ddl_ContractFor" onchange="ddl_ContractForChange()" runat="server"
                            CssClass="DROPDOWN" Width="99%">
                            <asp:ListItem Value="1">Client</asp:ListItem>
                            <asp:ListItem Value="2">ClientGroup</asp:ListItem>
                        </asp:DropDownList></td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr id="tr_Client" runat="server">
                    <td class="TD1" style="height: 15px">
                        Client :</td>
                    <td colspan="4" style="height: 15px">
                        <asp:UpdatePanel ID="UpdatePanelParty" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <cc1:DDLSearch ID="ddlParty" runat="server" AllowNewText="False" CallBackAfter="2"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.RegularContractualClient_Search"
                                    InjectJSFunction="" IsCallBack="True" OnTxtChange="ddlParty_TxtChange" PostBack="True"
                                    Text="" />
                                <asp:HiddenField ID="hdn_Is_Regular_Client" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlParty" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                        *</td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr id="tr_ClientGroup" runat="server">
                    <td class="TD1">
                        Client Group :</td>
                    <td colspan="4">
                        <asp:DropDownList ID="ddl_ClientGroup" runat="server" CssClass="DROPDOWN" Width="50%">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Valid From</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidFrom" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                        Valid Upto</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidUpto" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td style="width: 29%; text-align: right; height: 21px;">
                        <asp:Label ID="lbl_RateType" runat="server" Text="Rate Type :"></asp:Label>
                    </td>
                    <td style="width: 1%; height: 21px;">
                        <asp:DropDownList ID="ddl_RateType" runat="server" CssClass="DROPDOWN" Width="99%">
                            <asp:ListItem Value="1">Any Size</asp:ListItem>
                            <asp:ListItem Value="2">Size-Wise</asp:ListItem>
                        </asp:DropDownList></td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td class="TD1" style="font-weight:bold; height: 15px;">
                        Other Charges</td>
                    <td style="height: 15px">&nbsp;
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td class="TD1" style="height: 15px">
                    </td>
                    <td style="height: 15px">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Stationary Rs.:</td>
                    <td>
                        <asp:TextBox ID="txt_BiltyCharges" onkeypress="return Only_Numbers(this,event)" Text="0"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="6"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                        Hamali per Kg. :</td>
                    <td>
                        <asp:TextBox ID="txt_HamaliPerKg" onkeypress="return Only_Numbers(this,event)" Text="0"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="9" Width="40%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        AOC % :</td>
                    <td>
                        <asp:TextBox ID="txt_AOCPercent" onkeypress="return Only_Numbers(this,event)" Text="0"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="9" ></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                        FOV % :</td>
                    <td>
                        <asp:TextBox ID="txt_FOVPercent" onkeypress="return Only_Numbers(this,event)" onblur="txt_FOVPercentOnblur();" Text="0"
                            runat="server" CssClass="TEXTBOXNOS" MaxLength="9" Width="40%"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="tr_FOVExemptUpTo" runat="server">
                    <td class="TD1">
                        FOV Exempt UpTo Rs.:</td>
                    <td>
                        <asp:TextBox ID="txt_FOVExemptUpTo" onkeypress="return Only_Numbers(this,event)"
                            Text="0" runat="server" CssClass="TEXTBOXNOS" MaxLength="9"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1">
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:UpdatePanel ID="upnl_Comm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_Commodity" class="DIV" style="width: 100%; height: auto; text-align: left">
                                    <asp:DataGrid ID="dg_Commodity" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                        CssClass="Grid" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                        OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                        OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand"
                                        ShowFooter="True">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                        <Columns>
                                            <asp:BoundColumn DataField="RateContractID">
                                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                <FooterStyle CssClass="HIDEGRIDCOL" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RateContractDetailID">
                                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                <FooterStyle CssClass="HIDEGRIDCOL" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="From Location">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_FromLocation" runat="server" AutoPostBack="true"
                                                        CssClass="DROPDOWN" Width="98%">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "FromLocation"))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="45%" />
                                                <ItemStyle HorizontalAlign="Left" Width="45%" />
                                                <FooterStyle HorizontalAlign="Left" Width="45%" />
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_FromLocation" runat="server" AutoPostBack="true"
                                                        CssClass="DROPDOWN" Width="98%">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="To Location">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_ToLocation" runat="server" AutoPostBack="true" CssClass="DROPDOWN"
                                                        Width="98%">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "ToLocation"))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="45%" />
                                                <ItemStyle HorizontalAlign="Left" Width="45%" />
                                                <FooterStyle HorizontalAlign="Left" Width="45%" />
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_ToLocation" runat="server" AutoPostBack="true" CssClass="DROPDOWN"
                                                        Width="98%">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Freight Rate">
                                                <HeaderStyle HorizontalAlign="right" Width="30%" />
                                                <ItemStyle HorizontalAlign="right" Width="30%" />
                                                <FooterStyle HorizontalAlign="right" Width="30%" />
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Rate" runat="server" CssClass="TEXTBOXNOS" MaxLength="6"
                                                        onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        Width="95%"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Rate" runat="server" CssClass="TEXTBOXNOS" MaxLength="7"
                                                        onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Numbers(this,event);"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate")) %>'
                                                        Width="95%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update">
                                                <HeaderStyle Width="5%" />
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add_Commodity" runat="server" CommandName="Add" Text="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Delete_Commodity" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%" />
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
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
