<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucClientBillingDetails.ascx.cs" Inherits="Master_Sales_WucClientBillingDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">

 function SetBillingCycleVisibility()
    { 
        var ddl_BillingCycle = document.getElementById('<%=ddl_BillingCycle.ClientID %>');
        var BillingCycle = ddl_BillingCycle.options[ddl_BillingCycle.selectedIndex].value;        
        
        var lbl_Day = document.getElementById('WucClient1_WucClientBillingDetails1_lbl_Day');
        var ddl_Day = document.getElementById('WucClient1_WucClientBillingDetails1_ddl_Day');
        
        var lbl_Date1 = document.getElementById('WucClient1_WucClientBillingDetails1_lbl_Date1');
        var lbl_Date2 = document.getElementById('WucClient1_WucClientBillingDetails1_lbl_Date2');
        
        var ddl_BillingCycleDay1 = document.getElementById('WucClient1_WucClientBillingDetails1_ddl_BillingCycleDay1');
        var ddl_BillingCycleDay2 = document.getElementById('WucClient1_WucClientBillingDetails1_ddl_BillingCycleDay2');
        
        if(BillingCycle == "1")
        {
            lbl_Day.style.display = "block";
            ddl_Day.style.display = "block";

            lbl_Date1.style.display = "none";
            ddl_BillingCycleDay1.style.display = "none";

            lbl_Date2.style.display = "none";
            ddl_BillingCycleDay2.style.display = "none";
        }
        else if(BillingCycle == "2")
        {
            lbl_Day.style.display = "none";
            ddl_Day.style.display = "none";
            
            lbl_Date1.innerHTML = 'Day 1';
            lbl_Date1.style.display = "block";
            ddl_BillingCycleDay1.style.display = "block";
            
            lbl_Date2.innerHTML = 'Day 2';
            lbl_Date2.style.display = "block";
            ddl_BillingCycleDay2.style.display = "block";
        }
        else if(BillingCycle == "3")
        {
            lbl_Day.style.display = "none";
            ddl_Day.style.display = "none";

            lbl_Date1.style.display = "block";
            ddl_BillingCycleDay1.style.display = "block";
            lbl_Date1.innerHTML = 'Day';
            
            lbl_Date2.style.display = "none";
            ddl_BillingCycleDay2.style.display = "none";
        }
        else if(BillingCycle == "4")
        {
            lbl_Day.style.display = "none";
            ddl_Day.style.display = "none";

            lbl_Date1.style.display = "none";
            ddl_BillingCycleDay1.style.display = "none";
            lbl_Date1.innerHTML = 'Day';
            
            lbl_Date2.style.display = "none";
            ddl_BillingCycleDay2.style.display = "none";
        }
    }

</script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_Bolling" runat="server" CssClass="PANEL" GroupingText="Payment Mode"
                Width="100%" meta:resourcekey="pnl_BollingResource1">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            &nbsp;</td>
                        <td colspan="4">
                            <asp:Label ID="lbl_BookingPaymentModeAllowed" Font-Bold="True" runat="server" Text="Booking Payment Mode Allowed"
                                meta:resourcekey="lbl_BookingPaymentModeAllowedResource1" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 1%" />
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td colspan="4">
                            <asp:CheckBoxList ID="cbl_BookingPaymentMode" CssClass="CHECKBOXLIST" RepeatDirection="Horizontal"
                                RepeatColumns="4" runat="server" meta:resourcekey="cbl_BookingPaymentModeResource1">
                            </asp:CheckBoxList></td>
                        <td style="width: 1%">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr style="height:150px" valign="top">
        <td colspan="6" style="width: 100%">
            <%--<asp:Panel ID="pnl_BollingDetails" runat="server" CssClass="DIV" Height="150px" GroupingText="Billing Details">--%>
                <asp:UpdatePanel ID="upd_pnl_dg_BillingDetails" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_BillingDetails" />
                    </Triggers>
                    <ContentTemplate>
                        <%--<div class="DIV" runat="server" id="div_client">--%>
                        <asp:DataGrid ID="dg_BillingDetails" runat="server" Width="100%" CssClass="GRID"
                            AutoGenerateColumns="False" ShowFooter="True" OnCancelCommand="dg_BillingDetails_CancelCommand"
                            OnDeleteCommand="dg_BillingDetails_DeleteCommand" OnEditCommand="dg_BillingDetails_EditCommand"
                            OnItemCommand="dg_BillingDetails_ItemCommand" OnItemDataBound="dg_BillingDetails_ItemDataBound"
                            OnUpdateCommand="dg_BillingDetails_UpdateCommand">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Billing Branch" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <cc1:DDLSearch ID="ddl_BillingBranch" CallBackAfter="2" runat="server" IsCallBack="True"
                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" AllowNewText="True"
                                            Text="" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Branch_Name") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <cc1:DDLSearch ID="ddl_BillingBranch" CallBackAfter="2" runat="server" IsCallBack="True"
                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" AllowNewText="True"
                                            Text="" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Contact Person" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_ContactPerson" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Contact_Person") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ContactPerson" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Billing Name" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_BillingName" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100"
                                            runat="server" meta:resourcekey="txt_BillingNameResource1"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Billing_Name") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BillingName" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100"
                                            runat="server" meta:resourcekey="txt_BillingNameResource2"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Address" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Address" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="200"
                                            runat="server" meta:resourcekey="txt_AddressResource1"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Billing_Address") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Address" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="200"
                                            runat="server" meta:resourcekey="txt_AddressResource2"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="City" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <cc1:DDLSearch ID="ddl_City" runat="server" IsCallBack="True" DBTableName="EC_Master_City"
                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetCityForClient" AllowNewText="True"
                                            Text="" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "City_Name") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <cc1:DDLSearch ID="ddl_City" runat="server" IsCallBack="True" DBTableName="EC_Master_City"
                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetCityForClient" AllowNewText="True"
                                            Text="" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Contact No" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_ContactNo" CssClass="TEXTBOXNOS" onblur="return valid(this)"
                                            BorderWidth="1px" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Contact_No") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ContactNo" CssClass="TEXTBOXNOS" onblur="return valid(this)"
                                            BorderWidth="1px" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Email" HeaderStyle-Width="10%">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Email" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "email") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Email" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:EditCommandColumn UpdateText="Update" HeaderStyle-Width="8%" CancelText="Cancel"
                                    EditText="Edit" HeaderText="Edit" meta:resourcekey="EditCommandColumnResource1">
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Delete" HeaderStyle-Width="5%">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                            meta:resourcekey="lbtn_DeleteResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <%-- </div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <%--</asp:Panel>--%>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_BillingCycle" runat="server" CssClass="PANEL" GroupingText="Billing Cycle"
                Width="100%">
                <table style="width: 100%">
                    <tr>
                        <td class="TD1" style="width: 20%; height: 21px;">
                            <asp:Label ID="lbl_Billing" runat="server" CssClass="LABEL" Text="Billing :"></asp:Label>&nbsp;</td>
                        <td style="width: 20%; height: 21px;">
                            <asp:DropDownList ID="ddl_BillingCycle" runat="server" CssClass="DROPDOWN" Width="90%" 
                                onchange="SetBillingCycleVisibility();">
                            </asp:DropDownList>
                        </td>
                        <td class="TD1" style="width: 20%; height: 21px;">
                             <asp:Label ID="lbl_Day" runat="server" CssClass="LABEL" Text="Day :"></asp:Label>
                        </td>
                        <td style="width: 20%; height: 21px;">
                            <asp:DropDownList ID="ddl_Day" runat="server" CssClass="DROPDOWN" Width="90%"></asp:DropDownList>
                        </td>
                        <td style="width: 20%; height: 21px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%; height: 21px;">&nbsp;</td>
                        <td style="width: 20%; height: 21px;">&nbsp;</td>
                        <td class="TD1" style="width: 20%; height: 21px;">&nbsp;</td>
                        <td style="width: 20%; height: 21px;">&nbsp;</td>
                        <td style="width: 20%; height: 21px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%; height: 21px;">
                            <asp:Label ID="lbl_Date1" runat="server" CssClass="LABEL" Text="Day 1:"></asp:Label>
                        </td>
                        <td style="width: 20%; height: 21px;">
                           <asp:DropDownList ID="ddl_BillingCycleDay1" runat="server" CssClass="DROPDOWN" Width="90%">
                                <asp:ListItem Value="1"> 1 </asp:ListItem>
                                <asp:ListItem Value="2"> 2 </asp:ListItem>
                                <asp:ListItem Value="3"> 3 </asp:ListItem>
                                <asp:ListItem Value="4"> 4 </asp:ListItem>
                                <asp:ListItem Value="5"> 5 </asp:ListItem>
                                <asp:ListItem Value="6"> 6 </asp:ListItem>
                                <asp:ListItem Value="7"> 7 </asp:ListItem>
                                <asp:ListItem Value="8"> 8 </asp:ListItem>
                                <asp:ListItem Value="9"> 9 </asp:ListItem>
                                <asp:ListItem Value="10"> 10 </asp:ListItem>
                                <asp:ListItem Value="11"> 11 </asp:ListItem>
                                <asp:ListItem Value="12"> 12 </asp:ListItem>
                                <asp:ListItem Value="13"> 13 </asp:ListItem>
                                <asp:ListItem Value="14"> 14 </asp:ListItem>
                                <asp:ListItem Value="15"> 15 </asp:ListItem>
                                <asp:ListItem Value="16"> 16 </asp:ListItem>
                                <asp:ListItem Value="17"> 17 </asp:ListItem>
                                <asp:ListItem Value="18"> 18 </asp:ListItem>
                                <asp:ListItem Value="19"> 19 </asp:ListItem>
                                <asp:ListItem Value="20"> 20 </asp:ListItem>
                                <asp:ListItem Value="21"> 21 </asp:ListItem>
                                <asp:ListItem Value="22"> 22 </asp:ListItem>
                                <asp:ListItem Value="23"> 23 </asp:ListItem>
                                <asp:ListItem Value="24"> 24 </asp:ListItem>
                                <asp:ListItem Value="25"> 25 </asp:ListItem>
                                <asp:ListItem Value="26"> 26 </asp:ListItem>
                                <asp:ListItem Value="27"> 27 </asp:ListItem>
                                <asp:ListItem Value="28"> 28 </asp:ListItem>
                                <asp:ListItem Value="29"> 29 </asp:ListItem>
                                <asp:ListItem Value="30"> 30 </asp:ListItem>
                                <asp:ListItem Value="31"> 31 </asp:ListItem>
                           </asp:DropDownList>
                        </td>
                        <td class="TD1" style="width: 20%; height: 21px;">
                           <asp:Label ID="lbl_Date2" runat="server" CssClass="LABEL" Text="Day 2:"></asp:Label>
                        </td>
                        <td style="width: 20%; height: 21px;">
                           <asp:DropDownList ID="ddl_BillingCycleDay2" runat="server" CssClass="DROPDOWN" Width="90%">
                                <asp:ListItem Value="1"> 1 </asp:ListItem>
                                <asp:ListItem Value="2"> 2 </asp:ListItem>
                                <asp:ListItem Value="3"> 3 </asp:ListItem>
                                <asp:ListItem Value="4"> 4 </asp:ListItem>
                                <asp:ListItem Value="5"> 5 </asp:ListItem>
                                <asp:ListItem Value="6"> 6 </asp:ListItem>
                                <asp:ListItem Value="7"> 7 </asp:ListItem>
                                <asp:ListItem Value="8"> 8 </asp:ListItem>
                                <asp:ListItem Value="9"> 9 </asp:ListItem>
                                <asp:ListItem Value="10"> 10 </asp:ListItem>
                                <asp:ListItem Value="11"> 11 </asp:ListItem>
                                <asp:ListItem Value="12"> 12 </asp:ListItem>
                                <asp:ListItem Value="13"> 13 </asp:ListItem>
                                <asp:ListItem Value="14"> 14 </asp:ListItem>
                                <asp:ListItem Value="15"> 15 </asp:ListItem>
                                <asp:ListItem Value="16"> 16 </asp:ListItem>
                                <asp:ListItem Value="17"> 17 </asp:ListItem>
                                <asp:ListItem Value="18"> 18 </asp:ListItem>
                                <asp:ListItem Value="19"> 19 </asp:ListItem>
                                <asp:ListItem Value="20"> 20 </asp:ListItem>
                                <asp:ListItem Value="21"> 21 </asp:ListItem>
                                <asp:ListItem Value="22"> 22 </asp:ListItem>
                                <asp:ListItem Value="23"> 23 </asp:ListItem>
                                <asp:ListItem Value="24"> 24 </asp:ListItem>
                                <asp:ListItem Value="25"> 25 </asp:ListItem>
                                <asp:ListItem Value="26"> 26 </asp:ListItem>
                                <asp:ListItem Value="27"> 27 </asp:ListItem>
                                <asp:ListItem Value="28"> 28 </asp:ListItem>
                                <asp:ListItem Value="29"> 29 </asp:ListItem>
                                <asp:ListItem Value="30"> 30 </asp:ListItem>
                                <asp:ListItem Value="31"> 31 </asp:ListItem>
                           </asp:DropDownList>
                        </td>
                        <td style="width: 20%; height: 21px;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_BillingDetails" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
