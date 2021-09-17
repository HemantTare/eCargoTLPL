<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehiclePM.ascx.cs" Inherits="Master_PM_WucVehiclePM" %>
<%@ Register Src="~/CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>

<%@ Register Src="~/CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>



    <script type="text/javascript">

    function Open_Add_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
     function Open_Edit_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;

    }
    
    function Open_View_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

</script>



 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
            <td style="width: 50%">
          <asp:Label ID="lbl_Truck_No_Caption" Text="Vehicle No:" CssClass="TEXTBOX" Font-Bold="true" runat="server"/>
            &nbsp;  &nbsp;
          <asp:Label ID="lbl_Truck_No"  Width="25%" CssClass="TEXTBOX" Font-Bold="true" BorderWidth="1px" runat="server"></asp:Label>
                <%--<uc2:WucLinkName ID="Link" runat="server" />--%>
            </td>
                
            <td style="width: 50%; text-align: right;">
                &nbsp;</td>
        </tr>
        
        <tr>
                <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">
                    &nbsp;

                </td>
        </tr>
    </table>
    
    
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
            <td style="width:100%;">                       
           
            <asp:DataGrid ID="dg_Grid" runat="server"  
                            AllowPaging="True" CssClass="GRID"  
                            PageSize="15" AutoGenerateColumns="False" 
                            
                            OnPageIndexChanged="Grid_PageIndexChanged" 
                            OnEditCommand="Grid_EditCommand" OnItemDataBound="Grid_ItemDataBound">
                            
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
            
        <Columns>

                   <asp:TemplateColumn HeaderText="Task_ID"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Eval("Template_Task_ID")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Task Name"   >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Task_Name")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Task Schedule By"    >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Task_Schedule_By")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Is Custom"    >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Is_Custom")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kms"    >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Kms")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Days"    >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Days")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Cost"    >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Cost")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    
                    
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_View" runat="server" Text="View"></asp:LinkButton>    
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    
                    
                    <asp:TemplateColumn HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit" OnClick="lnk_Edit_Click"></asp:LinkButton>    
                        </ItemTemplate>
                    </asp:TemplateColumn>                       
                    
                    
                   
        </Columns>           
        </asp:DataGrid>
        
        
                
          </td>
        </tr>
        
     
        
        
      </table> 
      
      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
                <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="3">
                    &nbsp;
                </td>
        </tr>
        
        <tr>
           <td style="width: 70%" align="right">
           <asp:Label ID="Lbl_Total_Records" runat="server"  CssClass="LABEL"  Font-Bold="True" EnableViewState="False"></asp:Label>&nbsp;
           </td>		
             <td style="width: 15%" align="right">
           <asp:Button ID="btn_Apply_Task_Template" runat="server" Text="Apply Task Template" CssClass="BUTTON" Visible="false"  />&nbsp;
           </td>
     
            <td style="width: 15%" class="TD1">
                &nbsp;
                <asp:Button ID="btn_Add" runat="server" Text="Add New Record" CssClass="BUTTON"    /></td>
        </tr>
        
        
        <tr>
            <td colspan="3">

                  
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
                <asp:HiddenField ID="hdn_Vehicle_ID" runat="server" />
                  
                        
            </td>
        </tr>           
        
    </table>     