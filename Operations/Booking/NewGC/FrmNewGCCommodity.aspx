<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCCommodity.aspx.cs"
    Inherits="Operations_Booking_FrmNewGCCommodity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Javascript/Common.js"></script>

    <script type="text/javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>

</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <div style="width: 100%">
            <table class="TABLENOBORDER" border="0">
                <tr>
                    <td style="width: 100%" colspan="7">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                            <ContentTemplate>
                                <div id="Div_Commodity" class="DIV" style="height: 112px; width: 100%; text-align: left">
                                    <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                        ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                        OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                        OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Articles">
                                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_NewArticles" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                        onkeyPress="return Only_Integers(this,event);" MaxLength="7" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Articles") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_NewArticles" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Articles")%>' MaxLength="7" onkeyPress="return Only_Integers(this,event);"
                                                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Packing Type">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_NewPacking_Type" AutoCompleteType="Disabled" runat="server"
                                                        CssClass="TEXTBOX" MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_PackingType" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_NewPackingTypeId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Packing_Type")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_NewPacking_Type" AutoCompleteType="Disabled" runat="server"
                                                        CssClass="TEXTBOX" MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_PackingType" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_NewPackingTypeId" runat="server" Value="0"></asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Item">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_ddlItem" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_ItemType" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:CheckBox ID="chk_Is_ServiceTax_ForCommodity" runat="server" CssClass="HIDEGRIDCOL" />
                                                    <asp:HiddenField ID="hdfn_ItemRatePerKg" Value="0" runat="server" />
                                                    <asp:HiddenField ID="hdn_ddlItemId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Item_Name")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_ddlItem" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_ItemType" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:CheckBox ID="chk_Is_ServiceTax_ForCommodity" Checked='<%# DataBinder.Eval(Container.DataItem, "Is_Service_Tax_App_For_Commodity")%>'
                                                        runat="server" CssClass="HIDEGRIDCOL" />
                                                    <asp:HiddenField ID="hdfn_ItemRatePerKg" runat="server" />
                                                    <asp:HiddenField ID="hdn_ddlItemId" runat="server" Value="0"></asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Actual Wt">
                                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_ActualWt" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="7" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ActualWt")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_ActualWt" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ActualWt")) %>'
                                                        MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Size">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Size" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_Size" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_SizeId" runat="server" Value="0"></asp:HiddenField>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "SizeName")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Size" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_Size" Style="position: absolute; z-index: 1000" runat="server"
                                                        TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_SizeId" runat="server" Value="0"></asp:HiddenField>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Charge Wt">
                                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Weight" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="7" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Weight" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Weight")) %>'
                                                        MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Rate">
                                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Rate" Enabled="false" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                        onkeyPress="return Only_Numbers(this,event);" MaxLength="7" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Rate" Enabled="false" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Rate")) %>' MaxLength="7"
                                                        onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount">
                                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="7" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfn_ToBe" runat="server" />
                                                    <asp:HiddenField ID="hdfn_BharaiAmt" runat="server" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="right"></HeaderStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
                                                        MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfn_ToBe" runat="server" />
                                                    <asp:HiddenField ID="hdfn_BharaiAmt" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                EditText="Edit">
                                                <HeaderStyle Width="10%"></HeaderStyle>
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
                                <asp:AsyncPostBackTrigger ControlID="btn_Commodity" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Item" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="7">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                            <ContentTemplate>
                                <table class="TABLENOBORDER">
                                    <tr>
                                        <td colspan="7">
                                            <asp:Label ID="lbl_CommodityErrorMsg" runat="server" CssClass="LABELERROR"></asp:Label>
                                            <asp:HiddenField ID="hdn_Bkg_TypeID" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="display: none">
                                            <asp:CheckBox ID="chk_Is_Item_required" runat="server"></asp:CheckBox>
                                            <asp:HiddenField ID="hdn_Default_Commodity_Weight" runat="server" />
                                            <asp:HiddenField ID="hdn_FirstCommodityId" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_FirstItemId" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_FirstPackingTypeId" runat="server"></asp:HiddenField>
                                            <asp:Button ID="btn_Commodity" runat="server" OnClick="btn_Commodity_Click"></asp:Button>
                                            <asp:Button ID="btn_Item" runat="server" OnClick="btn_Item_Click"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:LinkButton ID="lnk_AddCommodity" runat="server" Text="Add Commodity" OnClientClick="return Open_PopPage(0,'AddCommodity');"></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lnk_AddItem" runat="server" Text="Add Item" OnClientClick="return Open_PopPage(0,'AddItem');"></asp:LinkButton>
                                        </td>
                                        <td class="TD1" style="width: 10%">
                                            <asp:Label Style="text-align: right" ID="lbl_CommodityTotal" runat="server" CssClass="LABEL"
                                                Text="Total :" Width="98%" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 7%" class="TD1">
                                            <asp:Label Style="text-align: right" ID="lbl_TotalArticles" runat="server" CssClass="LABEL"
                                                Text="0" Width="98%" Font-Bold="True"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalArticles" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="width: 7%" class="TD1">
                                            <asp:Label Style="text-align: right" ID="lbl_TotalWeight" runat="server" CssClass="LABEL"
                                                Text="0" Width="98%" Font-Bold="True"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalWeight" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="width: 7%" class="TD1" runat="server" id="td_length">
                                            <asp:Label Style="text-align: right" ID="lbl_TotalRate" runat="server" CssClass="LABEL"
                                                Text="0" Width="98%" Font-Bold="True"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalRate" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="width: 7%" class="TD1" runat="server" id="td_width">
                                            <asp:Label Style="text-align: right" ID="lbl_TotalAmount" runat="server" CssClass="LABEL"
                                                Text="0" Width="98%" Font-Bold="True"></asp:Label>
                                            <%--<asp:Label Style="text-align: right" ID="lbl_ItemValueForFOV" runat="server" CssClass="LABEL"
                                                Text="0" Width="98%" Font-Bold="True"></asp:Label>--%>
                                            <asp:HiddenField ID="hdn_TotalAmount" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_TotalBharaiAmt" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="width: 15%" class="TD1">
                                            <asp:HiddenField ID="hdn_Is_Service_Tax_Applicable_For_Commodity" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_CommodityId" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_CommodityName" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_ItemId" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_ItemValueForFOV" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdn_ItemName" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Commodity" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Item" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">

 function updateparentdataset(Article,Weight,Rate,Amount,Is_ServiceTaxForCommodity,ItemValueForFOV,TotalBharaiAmt)
 {  
  window.parent.call_CommodityDetails(Article,Weight,Rate,Amount,Is_ServiceTaxForCommodity,ItemValueForFOV,TotalBharaiAmt);
 } 
 function setfocusoninvoice()
 {  
  window.parent.document.getElementById("WucGCNew1_txt_Invoice_No").focus();
 } 
function Set_Commodity_Details(Commodity_Id,Commodity_Name)
{
    document.getElementById('hdn_CommodityId').value = Commodity_Id; 
    document.getElementById('hdn_CommodityName').value = Commodity_Name;    

    document.getElementById('<%=btn_Commodity.ClientID%>').click();
}
 
function Set_Item_Details(Item_Id,Item_Name,Commodity_Id)
{
    document.getElementById('hdn_ItemId').value = Item_Id;
    document.getElementById('hdn_ItemName').value = Item_Name;
    document.getElementById('hdn_CommodityId').value = Commodity_Id;

    document.getElementById('<%=btn_Item.ClientID%>').click();
}
</script>

