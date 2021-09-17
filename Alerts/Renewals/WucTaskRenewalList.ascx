<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTaskRenewalList.ascx.cs" Inherits="Alerts_Renewals_WucTaskRenewalList" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 1%">
            <asp:Image ID="Img_Left" runat="server" ImageUrl="~/Images/Link.gif" /></td>
        <td style="width: 29%; vertical-align: middle;">
            &nbsp;<asp:Label ID="lbl_Link_Name" runat="server" Text="TASK RENEWAL LIST" CssClass="LABEL" Font-Bold="True"></asp:Label>
            <asp:Image ID="Img_Right" runat="server" ImageUrl="~/Images/Link1.gif" ImageAlign="Middle" />
        </td>
        
    </tr>
</table>

<tr>
         <td colspan="6" style="font-size: xx-small; width: 100%; text-align: left">
             &nbsp;</td>
    </tr>
    
     <table cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
            <td style="width:100%;">                       
           
            <asp:DataGrid ID="dg_VehicleFitnessCertiGrid" runat="server"  
                            AllowPaging="True" CssClass="GRID"  
                            PageSize="15" AllowSorting="True" AutoGenerateColumns="False" 
                            
                            OnPageIndexChanged="Grid_PageIndexChanged" 
                            OnSortCommand="Grid_SortCommand" OnItemDataBound="Grid_ItemDataBound">
                            
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
            
        <Columns>
                                     

                   <asp:TemplateColumn HeaderText="Vehicle_ID"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Eval("Vehicle_ID")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN"  />
                    </asp:TemplateColumn>

                    
                     <asp:TemplateColumn HeaderText="IsDue"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_IsDue" runat="server" Text='<%#Eval("IsDue")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN"  />
                    </asp:TemplateColumn>                                                  
                   
                    
                     <asp:TemplateColumn HeaderText="Menu_Item_Id"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_MenuItemId" runat="server" Text='<%#Eval("Menu_Item_Id")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN"  />
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText="Document_Type_Id"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_DocumentTypeId" runat="server" Text='<%#Eval("Document_Type_Id")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN"  />
                    </asp:TemplateColumn>               
                                                          
                    <asp:TemplateColumn HeaderText="Vehicle No" SortExpression = "Vehicle_No">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Vehicle_No")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  CssClass = "SORTINGLNKBTN"  />
                    </asp:TemplateColumn>
                                       
                    <asp:TemplateColumn HeaderText="Alert On" SortExpression = "Alert On">
                        <ItemTemplate>
                           <%#DataBinder.Eval(Container.DataItem, "Alert On")%>
                        </ItemTemplate><HeaderStyle  CssClass  = "SORTINGLNKBTN"  />                       
                    </asp:TemplateColumn>
                    
                    
                     <asp:TemplateColumn HeaderText="Task" SortExpression = "Task">
                        <ItemTemplate>
                           <%#DataBinder.Eval(Container.DataItem, "Task")%>
                        </ItemTemplate><HeaderStyle  CssClass  = "SORTINGLNKBTN"  />                       
                    </asp:TemplateColumn>                                          
                 
                    
                    <asp:TemplateColumn HeaderText="Task Completion Method" >
                        <ItemTemplate>
                           <asp:LinkButton ID="lnk_TaskList" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Task_Completion_Method")%>' OnClick="lnk_VehicleFitnessCertificate_Click" ></asp:LinkButton>    
                        </ItemTemplate><HeaderStyle  CssClass  = "SORTINGLNKBTN"  />    
                    </asp:TemplateColumn>         
                    </Columns>           
        </asp:DataGrid>                             
          </td>
        </tr>        
        </table> 
      
      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
                <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="2">
                    &nbsp;
                </td>
        </tr>
        
        <tr>
        <td colspan="6" style="font-weight: bold; font-size: 11px; font-family: Verdana">
            <asp:Label ID="lbl_Updated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="UPDATEDLBL" Width="50px"></asp:Label>&nbsp;
            Due &nbsp;&nbsp;
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="NOTUPDATEDLBL" Width="50px"></asp:Label>&nbsp;
            Not Due</td>
    </tr>        
         <tr>
            <td colspan="2">                 
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
                    <asp:HiddenField ID="hdn_TaskDefinationId" runat="Server" />                                       
            </td>
        </tr>   
      </table>     
