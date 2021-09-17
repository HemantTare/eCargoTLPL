<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFreight.ascx.cs" Inherits="Master_Branch_WucFreight" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<asp:ScriptManager ID="scm_Freight" runat="server"></asp:ScriptManager>

<script type="text/javascript">


    function NewWindow()
    {
        
       var Path='FrmFreightCopy.aspx';
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-20);
        var popH = (h-250);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
          window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    

</script>



<table style="width: 100%" class="TABLE">
        <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="FREIGHT CITY TO CITY" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromCity" runat="server" Text="From City:" CssClass="LABEL"></asp:Label></td>
        <td style="width: 28%">
               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_Freight" />                
            </Triggers>
            <ContentTemplate>
            <asp:DropDownList ID="ddl_City" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_City_SelectedIndexChanged">
            </asp:DropDownList>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToState" runat="server" Text="To State:" CssClass="LABEL"></asp:Label></td>
        <td style="width: 28%">
               <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_Freight" />
                
            </Triggers>
            <ContentTemplate>
            <asp:DropDownList ID="ddl_State" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
            </asp:DropDownList>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
            <asp:Button ID="btn_Copy" runat="server" OnClick="btn_Copy_Click" CssClass="BUTTON" Text="Copy" OnClientClick="return NewWindow()"/></td>
    </tr>
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="Upd_Pnl_dg_TransitDays" runat="server">
            <Triggers>               
                <asp:AsyncPostBackTrigger ControlID="ddl_State" />
                <asp:AsyncPostBackTrigger ControlID="ddl_City" />
            </Triggers>
            <ContentTemplate>
                
           
        <asp:DataGrid ID="dg_Freight" runat="server" AutoGenerateColumns="False" CssClass="GRID" AllowPaging="True" AllowSorting="True" Width="100%" CellPadding="2" PageSize="15" OnCancelCommand="dg_Freight_CancelCommand" OnEditCommand="dg_Freight_EditCommand" OnItemDataBound="dg_Freight_ItemDataBound" OnPageIndexChanged="dg_Freight_PageIndexChanged" OnUpdateCommand="dg_Freight_UpdateCommand">
            
                <HeaderStyle CssClass="GRIDHEADERCSS" />
                <Columns>
                   
                    <asp:BoundColumn DataField ="From_City_Id" >
                        <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:BoundColumn>                 
                    
                    <asp:TemplateColumn >
                    <ItemTemplate>
                           <asp:Label ID="lbl_ToCityId" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "To_City_Id")%>'></asp:Label>
                     </ItemTemplate>
                     <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn >
                    <ItemTemplate>
                           <asp:Label ID="lbl_FreightId" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "FreightId")%>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                            <asp:Label ID="lbl_FreightId" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "FreightId")%>'></asp:Label>
                     </EditItemTemplate>
                     <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                                                     
                    <asp:TemplateColumn HeaderText="To City">
                    <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "City_Name")%>
                     </ItemTemplate>
                    </asp:TemplateColumn>
                                      
                    <asp:TemplateColumn HeaderText="FTL Rate" Visible="false">
                        <ItemTemplate>
                          <asp:Label ID="lbl_FTLRate" runat="server" Text ='<%#DataBinder.Eval(Container.DataItem, "FTLRate")%>' Font-Names="Verdana"  ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_FTLRate" runat="server"  onkeypress="return Only_Integers(this,event)"  
                            Text ='<%#DataBinder.Eval(Container.DataItem, "FTLRate")%>' Font-Names="Verdana" BorderWidth="1px"  
                            CssClass="TEXTBOXNOS"   MaxLength="9"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Freight Type">
                        <ItemTemplate>
                          <asp:Label ID="lbl_NormalRate" runat="server" Text ='<%#DataBinder.Eval(Container.DataItem, "NormalRate")%>' 
                          Font-Names="Verdana"  ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_NormalRate" runat="server"  
                            onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)" 
                            Text ='<%#DataBinder.Eval(Container.DataItem, "NormalRate")%>' Font-Names="Verdana" BorderWidth="1px"  
                            CssClass="TEXTBOXNOS"   MaxLength="9"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Special Rate" Visible="false">
                        <ItemTemplate>
                          <asp:Label ID="lbl_SpecialRate" runat="server" Text ='<%#DataBinder.Eval(Container.DataItem, "SpecialRate")%>'
                           Font-Names="Verdana"  ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SpecialRate" runat="server"  onkeypress="return Only_Integers(this,event)" 
                             Text ='<%#DataBinder.Eval(Container.DataItem, "SpecialRate")%>' Font-Names="Verdana" BorderWidth="1px"  CssClass="TEXTBOXNOS"   MaxLength="9"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Distance (in Kms)" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lbl_Distance" runat="server" Text ='<%#DataBinder.Eval(Container.DataItem, "Distance_In_Km")%>' Font-Names="Verdana"  ></asp:Label>                           
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Distance" runat="server"  onkeypress="return Only_Integers(this,event)"  Text ='<%#DataBinder.Eval(Container.DataItem,"Distance_In_Km")%>' Font-Names="Verdana" BorderWidth="1px"  CssClass="TEXTBOXNOS"   MaxLength="9"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                       
                    <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="Edit" >
                    </asp:EditCommandColumn>
                           
                    <asp:TemplateColumn HeaderText="View" Visible="false">
                        <ItemTemplate>
                          <asp:LinkButton ID="lnk_btn_view" runat="server">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle Mode="NumericPages" />
            </asp:DataGrid>
             </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6"
             style="font-weight: bold; font-size: 11px; font-family: Verdana">
            
            <asp:Label ID="lbl_Updated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="UPDATEDLBL" Width="50px"></asp:Label>&nbsp; Updated &nbsp;
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="NOTUPDATEDLBL" Width="50px"></asp:Label>&nbsp; Not Updated
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="up_error" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" ></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
    </tr>
</table>
<%--<table border="1" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td class="TDTRANSIT" style="width: 25%">
            1 - Next Day
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            2 - One Day Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            3 - Two Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            4 - Three Days Transit
        </td>
    </tr>
    <tr>
        <td class="TDTRANSIT" style="width: 25%">
            5 - Four Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            6 - Five Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            20 - No Commitment
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            0 - Source to Source
        </td>
    </tr>
</table>
--%>