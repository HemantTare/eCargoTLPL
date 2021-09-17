<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucActiveSeries.ascx.cs" Inherits="Operations_Document_Allocation_WucActiveSeries" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc2" %>

 <script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>   
 <script type="text/javascript" language="javascript" src="../../Javascript/Operations/Document Allocation/ActiveSeries.js"></script>    
 <asp:ScriptManager ID="scm_ActiveSeries" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
<tr>
    <td class="TDGRADIENT" colspan="6">&nbsp;
        <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="ACTIVE SERIES" meta:resourcekey="lbl_HeadingResource1" ></asp:Label>
   </td>
         
    </tr>
     <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Text="Document Type:"></asp:Label></td>
        <td style="width: 28%">
           <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged" meta:resourcekey="ddl_DocumentTypeResource1"   >
            </asp:DropDownList>
        </td>
        <td style="width: 2%"></td>
       <td style="width: 50%" colspan="3"></td>  
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <uc2:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />   
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                    
                </Triggers>
            </asp:UpdatePanel>      
        </td>
    </tr>
     <tr id="tr_VA" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VA" runat="server" CssClass="LABEL" Text="VA:"></asp:Label></td>
        <td style="width: 28%">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_VA" runat="server" CssClass="DROPDOWN"></asp:DropDownList>
                </ContentTemplate>
                 <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />    
                         <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                 
                 </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 2%"></td>
        <td style="width: 50%" colspan="3"></td>      
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; " valign="top">
            <asp:Label ID="lbl_DocumentSeries" runat="server" CssClass="LABEL" Text="Documented Series:" ></asp:Label></td>
        <td colspan="4" >
           <asp:UpdatePanel ID="Upd_Pnl_dg_DocumentSeries" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_DocumentSeries" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%"   >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>                          
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DocumentSeriesID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Document_Series_Allocation_ID") %>' meta:resourcekey="lbl_DocumentSeriesIDResource1"  ></asp:Label>
                                </ItemTemplate>                               
                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                            </asp:TemplateColumn>     
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                  <asp:RadioButton ID="rdo_Series" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Is_Active") %>'    runat="server" OnCheckedChanged="rdo_Series_CheckedChanged"/>  
                                </ItemTemplate>                                                               
                            </asp:TemplateColumn>                       
                            <asp:TemplateColumn HeaderText="Start No" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_StartNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Start_No") %>' meta:resourcekey="lbl_StartNoResource1"  ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="End No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EndNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "End_No") %>' meta:resourcekey="lbl_EndNoResource1"  ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Balance" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>' meta:resourcekey="lbl_BalanceResource1"  ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>       
                                          
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                    
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />                    
                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">&nbsp;</td>
    </tr>   
    <tr>
        <td colspan="6" align="left"> 
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"/></td>
    </tr>
    <tr>
        <td align="left"  colspan="6">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_DocumentSeries" />
            </Triggers>
            <ContentTemplate>
                &nbsp;
                <asp:HiddenField ID="hdn_DcumentSeriesID" runat="server" />
                &nbsp;
             </ContentTemplate>
         </asp:UpdatePanel>
        </td>
    </tr>
</table>
