<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_Vehicle_Profitability_GCwise.aspx.cs" Inherits="Finance_Reports_Frm_Rpt_Vehicle_Profitability_GCwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Vehicle Profitability GC Wise</title>
     <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
  <div>
        <table class="TABLE">
            <tr>
                <td>
                    <fieldset>
                        <legend><B>Vehicle Profitability GC Wise</B></legend>
                            <asp:DataGrid ID="dg_Vehicle_Profitability_GCwise" runat="server" CssClass="GRID" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dg_Grid_ItemDataBound">
                              <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                              <HeaderStyle CssClass="GRIDHEADERCSS" />
                              <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left"   />
                              <Columns>
                                <asp:TemplateColumn HeaderText="Sr No">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "Sr No")%> 
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="left" />  
                                             <FooterStyle HorizontalAlign="left" />
                                             <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn> 
                                         <asp:TemplateColumn HeaderText="GC No">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "GC No")%> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />  
                                             <FooterStyle HorizontalAlign="left" />
                                             <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn> 
                                         <asp:TemplateColumn HeaderText="GC Date">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "GC Date")%> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />  
                                             <FooterStyle HorizontalAlign="left" />
                                             <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn> 
                                         <asp:TemplateColumn HeaderText="Total Fright Income">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "Total Freight Income")%> 
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbl_Total_Fright_Income" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />  
                                             <FooterStyle HorizontalAlign="Right" />
                                             <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn> 
                                        </Columns>
                            </asp:DataGrid>
                   </fieldset>
                </td>
            </tr>
           </table> 
           
    </form>
</body>
</html>
