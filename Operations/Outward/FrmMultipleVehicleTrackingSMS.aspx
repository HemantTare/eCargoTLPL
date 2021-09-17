<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMultipleVehicleTrackingSMS.aspx.cs" Inherits="Operations_Outward_FrmMultipleVehicleTrackingSMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="../../JQuery/jquery-1.12.4.min.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Outward/MultipleVehicleTrackingSMS.js"></script>

<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Multi Vehicle Tracking</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
<script type="text/javascript">

function Add_Client_Window()
{
var hdn_client_path = document.getElementById('<%=hdn_client_path.ClientID %>');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_client_path.value == '')
  {
  alert('you don"t have rights to add vehicle');
  }
else
  {
  window.open(hdn_client_path.value, 'memo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}
</script>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Multiple Vehicle Tracking SMS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 50%;" colspan="3"></td>
                    <td class="TD1" style="width: 20%;"></td>
                    <td style="width: 29%; ">
                        <asp:LinkButton ID="lbtn_AddClient" Font-Bold="true" OnClientClick="return Add_Client_Window()" runat="server" Text="Add Client"></asp:LinkButton>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">City :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_City" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                onblur="On_txtLostFocus('txt_City','lst_City','hdn_CityId')" onkeyup="Search_txtSearch(event,this,'lst_City','City',2);"
                                onkeydown="return on_keydown(event,'txt_City','lst_City');" onfocus="On_Focus('txt_City','lst_City');"
                                MaxLength="50" EnableViewState="False"></asp:TextBox>
                                <asp:ListBox ID="lst_City" TabIndex="20" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_City')"
                                runat="server"></asp:ListBox>
                                <asp:HiddenField ID="hdn_CityId" Value="0" runat="server" />                        
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_City" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY" >*</td>
                    <td class="TD1" style="width: 20%;">Client Name :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanelParty" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Party" autocomplete="off" runat="server" CssClass="TEXTBOX" 
                                onblur="On_txtLostFocus('txt_Party','lst_Party','hdn_PartyId')" onkeyup="Search_txtSearch(event,this,'lst_Party','Party',2);"
                                onkeydown="return on_keydown(event,'txt_Party','lst_Party');"
                                onfocus="On_Focus('txt_Party','lst_Party');"
                                MaxLength="50" EnableViewState="False"></asp:TextBox>
                                <asp:ListBox ID="lst_Party" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_Party')" TabIndex="21" 
                                runat="server" ></asp:ListBox>
                                <asp:HiddenField ID="hdn_PartyId" Value="0" runat="server" />                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY" >*</td>
                </tr>
              
                <tr>
                    <td class="TD1" style="width: 20%;">Contact Person 1 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtContactPerson1" runat="server" CssClass="TEXTBOX" MaxLength="100" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                    <td class="TD1" style="width: 50%;" colspan="3"></td>
                </tr>
              
                <tr>
                    <td class="TD1" style="width: 20%;">Mobile No. 1 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Contact1MobileNo1" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY" >*</td>
                    <td class="TD1" style="width: 20%;">Mobile No. 2 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Contact1MobileNo2" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                </tr>
                
                <tr>
                    <td class="TD1" style="width: 20%;">Contact Person 2 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtContactPerson2" runat="server" CssClass="TEXTBOX" MaxLength="100" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" ></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                    <td class="TD1" style="width: 50%; " colspan="3"></td>
                </tr>
                
                <tr>
                    <td class="TD1" style="width: 20%;">Mobile No. 1 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Contatc2MobileNo1" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                    <td class="TD1" style="width: 20%;">Mobile No. 2 :</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Contatc2MobileNo2" runat="server" CssClass="TEXTBOX" MaxLength="10" 
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_Party" />
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">&nbsp;</td>
                </tr>
             
                <tr>
                    <td style="width: 100%" colspan="7">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                            <ContentTemplate>
                                <div id="Div_Vehicle" class="DIV" style="height: 300px; width: 100%; text-align: right">
                                    <asp:DataGrid ID="dg_Vehicles" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                        ShowFooter="True" OnCancelCommand="dg_Vehicles_CancelCommand" OnDeleteCommand="dg_Vehicles_DeleteCommand"
                                        OnEditCommand="dg_Vehicles_EditCommand" OnItemCommand="dg_Vehicles_ItemCommand"
                                        OnItemDataBound="dg_Vehicles_ItemDataBound" OnUpdateCommand="dg_Vehicles_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Vehicle No.">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_VehicleNo" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="20" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_VehicleNo" Style="position: absolute; z-index: 1000" runat="server" TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_VehicleId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "VehicleNo")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_VehicleNo" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="20" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_VehicleNo" Style="position: absolute; z-index: 1000" runat="server" TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_VehicleId" runat="server" Value="0"></asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Driver Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Driver" autocomplete="off"  Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_Driver" Style="position: absolute; z-index: 1000" runat="server" TabIndex="51"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_DriverId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "DriverName"))%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Driver" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX" MaxLength="100" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_Driver" Style="position: absolute; z-index: 1000" runat="server" TabIndex="51"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_DriverId" runat="server" Value="0"></asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Mobile No 1">
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Mobile1" runat="server" CssClass="TEXTBOX" Width="95%" MaxLength="15" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" 
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "MobileNo1"))%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Mobile1" runat="server" CssClass="TEXTBOX" Width="95%" MaxLength="15" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" 
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Mobile No 2">
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Mobile2" runat="server" CssClass="TEXTBOX" Width="95%" MaxLength="15" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" 
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "MobileNo2"))%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Mobile2" runat="server" CssClass="TEXTBOX" Width="95%" MaxLength="15" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" 
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Vehicle Location">
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_VehLocation" Width="95%" runat="server" CssClass="TEXTBOX" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" ></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "VehicleLocation")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" HorizontalAlign="left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_VehLocation" Width="95%" runat="server" CssClass="TEXTBOX" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Time">
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                <FooterTemplate>
                                                    <uc1:TimePicker ID="Wuc_VehicleTime" runat="server" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "CurrentTime")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <uc1:TimePicker ID="Wuc_VehicleTime" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update" EditText="Edit" >
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Add_Vehicle" Text="Add" CommandName="Add"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtn_Delete_Vehicle" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Vehicles" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                <tr>
                    <td class="TD1" style="width: 20%;">Sender Name :</td>
                    <td style="width: 29%;">
                        <asp:TextBox ID="txt_SenderName" runat="server" CssClass="TEXTBOX" MaxLength="50" onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)"></asp:TextBox>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY" >*</td>
                    <td class="TD1" style="width: 20%;">Mobile No. :</td>
                    <td style="width: 29%;">
                        <asp:TextBox ID="txt_SenderMobileNo" runat="server" CssClass="TEXTBOX" MaxLength="10" onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY" >*</td>
                </tr>
                
                <tr>
                    <td class="TD1" style="width: 100%;" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                        <asp:Button ID="btn_hidden" runat="server" Text="" OnClick="btn_hidden_Click"  style="display:none" /> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>        
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%;" colspan="6">
                    <asp:HiddenField ID="hdn_client_path" runat="server" />
                    <asp:HiddenField ID="hdn_shorturl" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6" style="height: 20px">
                        <asp:Button ID="btn_SendSMS" runat="server" Text="Send SMS" CssClass="BUTTON" OnClick="btn_SendSMS_Click" />&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Vehicles" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
            </table>
        </div>
    </form>
</body>
</html>
<script language ="javascript" type ="text/javascript">
//setFocusonPageLoad();        

function update_PartyDetails()
{
    document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_hidden.ClientID%>').click();
}

</script>