<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPrinterNameSetting.aspx.cs" Inherits="Printing_FrmPrinterNameSetting" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Printer Name Setting</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" /> 

</head>

<body>
    <form id="form1" runat="server">   

    <div>
    <asp:ScriptManager ID="scm_PrinterNameSetting" runat="server"></asp:ScriptManager>
   
     <table  class="TABLE" border="0" cellpadding="0" cellspacing="0" style="width: 100%" >
      <tr>
        <td colspan="7" class="TDGRADIENT">&nbsp;
        <asp:Label ID="lbl_Heading" runat="Server" CssClass="HEADINGLABEL" Text="PRINTER NAME SETTING" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
         <tr>
             <td style="width: 20%;" class="TD1">
                 <asp:Label ID="lbl_HierarchyCode" runat="server" CssClass="LABEL"  Font-Bold="true" Text="Hierarchy Code :"></asp:Label>
             </td>
             <td style="width: 29%;">
                 <asp:DropDownList ID="ddl_HierarchyCode" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                     OnSelectedIndexChanged="ddl_HierarchyCode_SelectedIndexChanged">
                 </asp:DropDownList>
             </td>
             <td class="TDMANDATORY" style="width: 1%">             
             </td>
             <td style="width: 20%;" class="TD1">
                 <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Font-Bold="true" Text="Document Type :"></asp:Label>
             </td>
             <td style="width: 29%;">
                 <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                     OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged">
                 </asp:DropDownList>
             </td>
             <td class="TDMANDATORY" style="width: 1%">
                 *
             </td>
         </tr>
         
         <tr>
             <td colspan="3">
                 &nbsp;
             </td>
         </tr>
        
    <tr>
    <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_PrinterName" runat="server" CssClass="LABEL" Font-Bold="true" Text="Printer Name :"></asp:Label>
        </td>
        <td style="width: 29%;">
        <asp:TextBox ID="txt_PrinterNameForCopy" runat="server"   onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" CssClass="TEXTBOX" />
        </td>
        <td  class="TDMANDATORY" style="width:1%">      
         </td>
         <td style="width: 20%;">
         <asp:Button ID="btn_CopyPrinterName" runat="server" CssClass="BUTTON" Text="COPY" OnClick="btn_CopyPrinterName_Click" />
         </td>
        <td style="width: 29%;"></td>
        <td style="width: 1%;"></td>
        
    </tr>
         <tr>
             <td colspan="3">
                 &nbsp;
             </td>
         </tr>
         
         <tr>
             <td colspan="6">
                 <table style="width: 100%">
                     <tr>
                         <td style="width: 100%">
                             <asp:UpdatePanel ID="Upd_Pnl_dg_PrinterName" UpdateMode="Conditional" runat="server">
                                 <Triggers>
                                     <asp:AsyncPostBackTrigger ControlID="ddl_HierarchyCode" />
                                     <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
                                     <asp:AsyncPostBackTrigger ControlID="btn_CopyPrinterName" />
                                 </Triggers>
                                 <ContentTemplate>
                                 <div id ="Div_PrinterName"  class="DIV" style="height:470px">
                                     <asp:DataGrid ID="dg_PrinterName" runat="server" AutoGenerateColumns="false" 
                                         CssClass="GRID" Style="border-top-style: none" Width="98%">
                                         <FooterStyle CssClass="GRIDFOOTERCSS" />  
                                         <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />                                      
                                         <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                         <Columns>
                                                  <asp:TemplateColumn HeaderText="Hierarchy Name">
                                                      <ItemTemplate>
                                                          <asp:Label ID="lbl_HierarchyName" Text='<%# DataBinder.Eval(Container.DataItem, "Hierarchy_Name") %>'
                                                              runat="server" CssClass="LABEL" BackColor="Transparent" BorderColor="Transparent"
                                                              BorderStyle="None" Width="80%"></asp:Label>
                                                      </ItemTemplate>
                                                       <HeaderStyle Width="40%" />     
                                             </asp:TemplateColumn>                                             
                                             
                                                 <asp:TemplateColumn HeaderText="Printer Name">
                                                     <ItemTemplate>
                                                         <asp:TextBox ID="txt_PrinterName" Text='<%# DataBinder.Eval(Container.DataItem, "printer_name") %>'
                                                             runat="server" CssClass="TEXTBOX"  onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" BorderWidth="1"
                                                             BorderStyle="Solid" Width="80%"></asp:TextBox>
                                                     </ItemTemplate>
                                                      <HeaderStyle Width="50%" />     
                                             </asp:TemplateColumn>
                                             
                                             <asp:TemplateColumn HeaderText="Main ID" Visible="false">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbl_MainId" Text='<%# DataBinder.Eval(Container.DataItem, "Main_Id") %>'
                                                             runat="server" CssClass="LABEL"  BorderWidth="1"
                                                             BorderStyle="Solid" Width="80%"></asp:Label>
                                                     </ItemTemplate>
                                                      <HeaderStyle Width="8%" />     
                                             </asp:TemplateColumn>
                                             
                                         </Columns>
                                     </asp:DataGrid>
                                      </div>
                                 </ContentTemplate>
                                
                             </asp:UpdatePanel>
                         </td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td colspan="6">
                 <asp:UpdatePanel ID="Upd_Pnl_Save" UpdateMode="Conditional" runat="server">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_HierarchyCode" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
                     </Triggers>
                     <ContentTemplate>
                         <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"  EnableViewState="False" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </td>
    </tr>  
         <tr>
             <td class="TD1" colspan="6" style="text-align: center">
                 <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" />
             </td>
         </tr>
         
     </table>
    </div>
    </form>    
</body>
</html>
