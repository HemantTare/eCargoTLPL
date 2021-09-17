<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucViewUserInfo.ascx.cs" Inherits="CommonControls_WucViewUserInfo" %>
<table id="ViewUserInfo" runat="server" class="TABLE" border="0" style="width: 100%"> 
 <tr >
           <td class="TD1" style="width: 20%">
           
                <asp:Label ID="lbl_Created_By" runat="Server" Text="Created By :" ></asp:Label>
           </td>
           <td align="left" style="width: 29%">
               <asp:Label ID="lbl_CreatedBy" runat="server"  Font-Bold="true" CssClass="LABEL" ></asp:Label></td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 
           
           <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_Updated_By" runat="Server" Text="Updated By :" ></asp:Label></td>
           <td align="left" style="width: 29%">
               <asp:Label ID="lbl_UpdatedBy" runat="server" Font-Bold="true" CssClass="LABEL" ></asp:Label></td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 

        </tr> 
         <tr >
           <td class="TD1" style="width: 20%">
           
                <asp:Label ID="lbl_Created_On" runat="Server" Text="Created On :" ></asp:Label>
           </td>
           <td align="left" style="width: 29%">
               <asp:Label ID="lbl_CreatedOn" runat="server" Font-Bold="true" CssClass="LABEL" ></asp:Label></td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 
           
           <td class="TD1" style="width: 20%">
                <asp:Label ID="lbl_Updated_On" runat="Server" Text="Updated On :" ></asp:Label></td>
           <td align="left" style="width: 29%">
               <asp:Label ID="lbl_UpdatedOn" runat="server" Font-Bold="true" CssClass="LABEL"></asp:Label></td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 

        </tr> 
         <tr >
           <td class="TD1" style="width: 20%">
           
                <asp:Label ID="lbl_Employee_Name" runat="Server" Text="Employee Name :"  Visible="false"></asp:Label>
           </td>
           <td align="left" style="width: 29%">
               <asp:Label ID="lbl_EmployeeName" runat="server" Font-Bold="true"  CssClass="LABEL"   Visible="false"></asp:Label></td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 
           
           <td class="TD1" style="width: 20%">
                </td>
           <td align="left" style="width: 29%">
             </td>
           <td class="TDMANDATORY" style="width: 1%">
           </td> 

        </tr> 
        </table>