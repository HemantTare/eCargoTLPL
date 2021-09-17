<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucErrorLog.ascx.cs" Inherits="Administration_ErrorLog_WucErrorLog" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
    <asp:ScriptManager ID="scm_ErrorLog" runat="server"></asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        var Grid = null;
        var IsExpanded = false;   
        var Rows = null;      
        var TimeSpan = 70;      
        
        function Toggle(dg_ErrorLog,Image,td_Desc)
        {
              
            Image=document.getElementById(Image);
            td_Desc=document.getElementById(td_Desc);
            Grid=document.getElementById(dg_ErrorLog);
            var PathArray=Image.src.split('/'); 
            var path='';
            if(IsExpanded)
            {
                PathArray[5]='plus.gif';
                path=PathArray[0] + '/' +PathArray[1] + '/' +PathArray[2] + '/' +PathArray[3] + '/' +PathArray[4] + '/' +PathArray[5];               
                Image.src = path;             
                Image.title = 'Expand';
                Grid.rules = 'none';              
                td_Desc.style.display = 'none';                                        
                IsExpanded = false;
            }
            else
            {
                PathArray[5]='minus.gif';
                path=PathArray[0] + '/' +PathArray[1] + '/'+ PathArray[2] + '/' +PathArray[3]+ '/'+PathArray[4]+ '/'+PathArray[5];               
                Image.src = path;               
                Image.title = 'Collapse';
                Grid.rules = 'cols';              
                td_Desc.style.display = 'block';                            
                IsExpanded = true;
            }
            ToggleDivs();                         
        }
        function ToggleDivs()
        {        
            setTimeout("ToggleDivs()",TimeSpan);
        }
    </script>
<table style="width: 100%" class="TABLE">
    <tr>
        <td colspan="4" class="TDGRADIENT" >
           <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="ERROR LOG"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="Select Date:"></asp:Label></td>
        <td class="TD1" style="width: 20%">
            <uc1:WucDatePicker ID="Wuc_Date" runat="server" />
        </td>
        <td style="width: 20%">
            &nbsp;<asp:RadioButtonList ID="rdbl_SelectErrors" runat="server">
            <asp:ListItem Text="App Errors" Value="0"></asp:ListItem>
            <asp:ListItem Selected="True" Text="DB Errors" Value="1"></asp:ListItem>
            </asp:RadioButtonList></td>
        <td style="width: 40%">
            &nbsp;<asp:Button ID="btn_Show" runat="server" Text="Show" CssClass="BUTTON" OnClick="btn_Show_Click" /></td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
          <asp:UpdatePanel ID="Upd_Pnl_dg_ErrorLog" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_dg_ErrorLog" ScrollBars="Vertical" Height="320" runat="server"  GroupingText="Error Log Details " CssClass="PANEL" Width="100%">
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td style="width: 100%; " colspan="8">

            <asp:DataGrid ID="dg_ErrorLog" runat="server" AutoGenerateColumns="False" 
                CssClass="GRID" ShowFooter="True" OnItemDataBound="dg_ErrorLog_ItemDataBound">
                 <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />

                <Columns>
            
                    <asp:BoundColumn DataField="date" HeaderText="Date">                                                
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="time" HeaderText="Time">                  
                    </asp:BoundColumn>                   
                 
                  <asp:TemplateColumn HeaderText="Description">
                        <ItemTemplate>
                            <asp:Image ID="imgTab" runat="server" ImageUrl="~/Images/plus.gif" onclick="javascript:Toggle(this,'div_Disc');"
                                ToolTip="Expand" />&nbsp;<%#DataBinder.Eval(Container.DataItem, "url")%>
                            <table runat="server">
                                <tr runat="server">
                                    <td id="td_Desc" style="background-color:Pink" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "message")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
              </td>
                             </tr>
                             </table>
                             </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />               
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
        </tr>
        </table>

            </td>
    </tr>
    <tr>
        <td colspan="4">
          <asp:UpdatePanel ID="Upd_Pnl_Error" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:Label ID="lbl_Error" CssClass="LABELERROR" runat="server"></asp:Label>
                  </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />               
                    
                </Triggers>
            </asp:UpdatePanel>
            </td>
    </tr>
</table>
