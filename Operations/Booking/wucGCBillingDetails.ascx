<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucGCBillingDetails.ascx.cs"  Inherits="Operations_Booking_wucGCBillingDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc2" %>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_GCBillingDetails" runat="server"></asp:ScriptManager>

<script type="text/javascript">
  
 function Update_ServiceTaxDetails()
 { 
  window.opener.Update_ServiceTaxDetails(); 
 } 
 
 function Allow_To_Exit()
 {   
    var ATE = false;    
    
    if (confirm("Do you want to Exit...")==false)
        {
            ATE=false;		 
        }	       
        else
        {
            ATE=true;		 
          
        }
    if (ATE)
        {  
            window.close();
            return true;
        }
    else
        {
            return false;
        }  
}

//function On_View()
//{    
//    var hdn_Mode = document.getElementById('wucGCBillingDetails1_hdn_Mode');  
//    var btn_Exit = document.getElementById('wucConsigneeDoorDeliveryAddress1_btn_Exit');        
//      
//    var Enable = true;
//        
//    if (val( hdn_Mode.value ) == 4)
//    {           
//        for(i = 0; i < document.forms[0].elements.length; i++) 
//        {        
//            elm = document.forms[0].elements[i];

//            if(elm.id!='')
//            {
//                var elm_id = document.getElementById(elm.id);
//                var elm_name = elm.name;
//                var arr = elm_name.split("$");                                     
//                
//                if (elm.type != 'lable')
//                {
//                   elm.disabled = Enable;
//                }
//            }
//        }

//        btn_Exit.disabled = false;
//    }  
//}

</script>

<table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;<asp:Label ID="lbl_BillingDetails" runat="server" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
  
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
   
    <tr>
        <td colspan="3" align="center" style="height: 77px">
            <asp:Panel ID="pnl_BillingDetails" runat="server" GroupingText="Billing Details" Font-Bold="True"
                Width="100%" CssClass="TABLE" BorderColor="White" BorderStyle="None" BorderWidth="0px">
                <asp:UpdatePanel ID="upd_dg_Billing" runat="server">
                    <contenttemplate>
                        <table width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="TD1" colspan="12">
                                        <asp:DataGrid ID="dg_Billing"  runat="server" 
                                            CssClass="Grid" Width="100%" OnCancelCommand="dg_Billing_CancelCommand" OnUpdateCommand="dg_Billing_UpdateCommand"
                                            OnEditCommand="dg_Billing_EditCommand"
                                            OnItemCommand="dg_Billing_ItemCommand" OnItemDataBound="dg_Billing_ItemDataBound"
                                            OnDeleteCommand="dg_Billing_DeleteCommand" AutoGenerateColumns="False" ShowFooter="True"
                                            PageSize="5" CellPadding="3" AllowSorting="True" DataKeyField="Sr_No">
                                            <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                            <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                            <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Sr No" Visible="False">
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" BorderWidth="1px" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                            ID="txt_Bill_SrNo" onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)"
                                                            ></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Sr_No") %>' CssClass="LABEL" ID="lbl_Bill_SrNo"
                                                            ></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="2%"></HeaderStyle>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" BorderWidth="1px" Width="30px" Enabled="False" CssClass="TEXTBOX"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Sr_No") %>' ID="txt_Bill_SrNo"
                                                            onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)" ></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Billing Hierarchy  &nbsp;&nbsp; Billing Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Hierarchy" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Hierarchy_Name") %>'></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lbl_Location" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Billing_Branch_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <uc2:WucHierarchyWithID ID="WucHierarchyWithID1"  runat="server" />                                    
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <uc2:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
                                                    </FooterTemplate>                               
                                                </asp:TemplateColumn> 
                                                <asp:TemplateColumn HeaderText="Billing Party" >
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <FooterTemplate>                                                 
                                                            <cc1:DDLSearch  ID="ddl_BillingParty" runat="server" IsCallBack="True" 
                                                                CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetSearchBillingParty" 
                                                                AllowNewText="True"/>                                                    
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Billing_Client_Name")) %>'
                                                            CssClass="LABEL" ID="lbl_Billing_Client_Name" ItemStyle-HorizontalAlign="Left" ></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20%" HorizontalAlign="Center"></HeaderStyle>
                                                    <EditItemTemplate>                                                  
                                                          <cc1:DDLSearch  ID="ddl_BillingParty" runat="server" IsCallBack="True" 
                                                            CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetSearchBillingParty" 
                                                              AllowNewText="True"/>                                                    
                                                    </EditItemTemplate>                                                    
                                                </asp:TemplateColumn>                                         
                                               
                                                <asp:TemplateColumn HeaderText="Bill Ratio">
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" Width="80%" CssClass="TEXTBOXNOS" ID="txt_Ratio"
                                                           onkeyPress="return Only_Numbers(this,event);"
                                                           MaxLength="7"  ></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Bill_Ratio"))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Width="80%" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Bill_Ratio")) %>'
                                                            ID="txt_Ratio" MaxLength="7" onkeyPress="return Only_Numbers(this,event);" ></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Description">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_Description" runat="server" Width="80%" CssClass="TEXTBOX" 
                                                           MaxLength="100"  onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" TextMode="MultiLine" ></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>                                                        
                                                     <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "Description")) %>'
                                                        CssClass="LABEL" ID="lbl_Description" ItemStyle-HorizontalAlign="Left" ></asp:Label>                                                            
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_Description" runat="server" Width="80%" CssClass="TEXTBOX" 
                                                           MaxLength="100" onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" TextMode="MultiLine"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Credit Amount" Visible="False">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_CreditLimit" Width="80px" runat="server" Text="" ></asp:Label>                                                        
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Credit_Limit")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl_CreditLimit" Width="80px" runat="server" Text="" ></asp:Label>                                                        
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="15%" />
                                                </asp:TemplateColumn>                                                
                                                
                                                 <asp:TemplateColumn HeaderText="Credit Amount" Visible="False">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_Closing_Balance" Width="80px" runat="server" Text="" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Closing_Balance")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl_Closing_Balance" Width="80px" runat="server" Text="-" ></asp:Label>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="15%" />
                                                </asp:TemplateColumn>
                                                
                                                <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                                    ValidationGroup="Btn_Save_BillingDetails" EditText="Edit" >
                                                    <HeaderStyle Width="5%"></HeaderStyle>
                                                </asp:EditCommandColumn>
                                                <asp:TemplateColumn HeaderText="Delete">
                                                    <FooterTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtn_Add_BillingDetails" Text="Add" CommandName="Add"
                                                            ValidationGroup="Btn_Save_BillingDetails" ></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtn_Delete_BillingDetails" Text="Delete" 
                                                        CommandName="Delete" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%"></HeaderStyle>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" colspan="5">
                                        <asp:Label Style="text-align: right" ID="lbl_BillingDetailsTotal" runat="server" 
                                            CssClass="LABEL" Text="Total :" Width="98%" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalArticles" runat="server" 
                                            CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalArticles" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 10%" class="TD1">                                        
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalRatio" runat="server" 
                                            CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalRatio" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalLength" runat="server" 
                                            CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalLength" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        <asp:Label Style="text-align: right" ID="lbl_TotalHeight" runat="server" 
                                            CssClass="LABEL" Width="98%" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalHeight" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        &nbsp;
                                    </td>
                                    <td style="width: 10%" class="TD1">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Billing" />
                    </triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
        </td>
    </tr>

    <tr>
        <td colspan="2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                 </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="left">
        <td align="center" colspan="2">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="k"  AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" CssClass="SMALLBUTTON"
                Text="Exit" AccessKey="E" />
        </td>
    </tr>
    <tr>
        <td>
            <input id="hdn_Consignee_Addess" runat="server" type="hidden" /></td>
        <td>
            <input id="hdn_Consignee_TelNo" runat="server" type="hidden" />
            <input id="hdn_Co_Name" runat="server" type="hidden" />
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdn_IsMultipleLocationBillingAllowed"></asp:HiddenField>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">    
    //On_View();    
</script>