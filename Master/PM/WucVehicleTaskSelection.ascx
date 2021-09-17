<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleTaskSelection.ascx.cs" Inherits="Master_PM_WucVehicleTaskSelection" %>
<script type="text/javascript" language="javascript">
    function CheckAllDataGridCheckBoxes(chk,gridname) 
    {
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
            if (elm.type == 'checkbox')
             {
                var elm_name = elm.name;
                var arr = elm_name.split("$");
               
                if (arr[1] == gridname)
                {
                elm.checked = chk.checked;   
                }
            }
        }
        
    }

</script>
<table width="100%" class="TABLE">
    <tr>
        <td colspan="3" class="TDGRADIENT">
           <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VEHICLE TASK SELECTION"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;<asp:DataGrid ID="dg_TaskSelection" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="GRID" 
                 PageSize="15">
                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                <HeaderStyle CssClass="GRIDHEADERCSS" />
                <FooterStyle CssClass="GRIDFOOTERCSS" />
                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                <Columns>
                <asp:TemplateColumn>
                <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" runat="server"   checked="checked"  onclick="CheckAllDataGridCheckBoxes(this,'dg_TaskSelection')"  />
                        </HeaderTemplate>
                     <ItemTemplate>
                               <asp:CheckBox ID="chk_IsSelect" runat="server" Checked="true"/>
                        </ItemTemplate> 
                        </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Template_Task_ID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Template_Task_ID" runat="server" Text='<%#Eval("Template_Task_ID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Task Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Task_Name" runat="server" Text='<%#Eval("Task_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Template Name">
                        <ItemTemplate>
                        <asp:Label ID="lbl_Template_Name" runat="server" Text='<%#Eval("Template_Name")%>'></asp:Label>                            
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Is Custom">
                        <ItemTemplate>
                         <asp:Label ID="lbl_Is_Custom" runat="server" Text='<%#Eval("Is_Custom")%>'></asp:Label>                            
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Task Schedule By">
                        <ItemTemplate>
                        <asp:Label ID="lbl_Task_Schedule_By" runat="server" Text='<%#Eval("Task_Schedule_By")%>'></asp:Label>                            
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                 
                    <asp:TemplateColumn HeaderText="Cost">
                        <ItemTemplate>
                        <asp:Label ID="lbl_Cost" runat="server" Text='<%#Eval("Cost")%>'></asp:Label>                            
                        </ItemTemplate>
                        <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kms"    >
                        <ItemTemplate>
                        <asp:Label ID="lbl_Kms" runat="server" Text='<%#Eval("Kms")%>'></asp:Label>
                            
                        </ItemTemplate>
                        <HeaderStyle  HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Days"    >
                        <ItemTemplate>
                              <asp:Label ID="lbl_Days" runat="server" Text='<%#Eval("Days")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Task_ID"  Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Task_ID" runat="server" Text='<%#Eval("Task_ID")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle   HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid></td>
    </tr>
    <tr>
        <td align="left" colspan="3">
            &nbsp;<asp:Label ID="lbl_Errors" CssClass="LABELERROR" EnableViewState="false" runat="server"></asp:Label>
            <asp:HiddenField ID="hdn_Vehicle_ID" runat="server" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="btn_Apply_Click" /></td>
    </tr>
</table>
