<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucIncludeExcludeUser.ascx.cs" Inherits="CRM_Transaction_WucIncludeExcludeUser" %>
<script language="javascript" type="text/javascript" src="../../Javascript/CRM/Transaction/InclueExcludeUser.js"></script>

<table style="width: 100%" class="TABLE" >
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="INCLUDE/EXCLUDE USER"></asp:Label></td>
    </tr>
    <tr>
        <td class="TDUnderline" colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
   
    <tr>
    
      <td style="width: 35%" class="TD1" >
     <asp:Label ID="lbl_ComplaintNature" runat="server" Text="Complaint Nature:"></asp:Label></td>
       <td style="width: 65%" >
    <asp:Label ID="lbl_ComplaintNatureName" runat="server" Font-Bold="true"></asp:Label>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Label ID="lbl_profile" runat="server" Text="Profile:"></asp:Label>
    <asp:Label ID="lbl_ProfileName" runat="server" Font-Bold="true"></asp:Label></td>
    <td style="width:30%" />
    </tr>
    
    <tr>
        <td colspan="6">        

            <asp:DataGrid ID="dg_UserProfile" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
            CellPadding="2" CssClass="Grid"   style="border-top-style: none" Width ="98%" OnItemDataBound="dg_UserProfile_ItemDataBound">
                <FooterStyle CssClass="GridFooterCss" />
                <HeaderStyle CssClass="GridHeaderCss" />
                <AlternatingItemStyle CssClass ="GridAlternateRowCss" />

                <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                        <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes(this,'dg_UserProfile')" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_Attach" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="User Id" Visible="false">
                    <ItemTemplate>
                     <asp:Label ID="lbl_UserID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "User_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="User Name">
                    <ItemTemplate>
                    <asp:Label ID="lbl_UserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "User_Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Employee Name">
                    <ItemTemplate>
                     <asp:Label ID="lbl_EmployeeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Employee_Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Profile Name">
                    <ItemTemplate>
                     <asp:Label ID="lbl_ProfileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Profile Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Is HO">
                    <ItemTemplate>
                     <asp:Label ID="lbl_IsHO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Is HO") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Branch Id" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lbl_BranchId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Branch_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Branch Name">
                    <ItemTemplate>
                    <asp:Label ID="lbl_BranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Branch Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Area Id" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lbl_AreaId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Area_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Area Name">
                    <ItemTemplate>
                    <asp:Label ID="lbl_AreaName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Area Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Region Id" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lbl_RegionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Region_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Region Name">
                    <ItemTemplate>
                    <asp:Label ID="lbl_RegionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Region Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>          
                    
                    <asp:TemplateColumn HeaderText="Checked" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lbl_Checked" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Checked") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>          
                    
                    </Columns>
                    </asp:DataGrid>
                    </td>
                    </tr>                    
             
   
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save"  OnClick="btn_Save_Click" CssClass="BUTTON"  OnClientClick="return CheckSelection();"/></td>
    </tr>
    
           <tr>
        <td colspan="6">
            
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="true" CssClass="LABELERROR" EnableViewState="False"  ForeColor="Red" Font-Size="11px"
               ></asp:Label>
                             
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
        &nbsp;
         </td>
    </tr>
    
    </table>