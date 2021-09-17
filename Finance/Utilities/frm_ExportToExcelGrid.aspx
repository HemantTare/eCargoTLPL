<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_ExportToExcelGrid.aspx.cs" Inherits="FA_Common_Reports_ExportToExcelGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel runat="server" ID="Panel1">
    
    
    <asp:DataGrid ID="dg_Excel" runat="server" 
                Width="100%" CssClass="GRID"  AutoGenerateColumns="false">
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <Columns>
 

                 <asp:TemplateColumn   ItemStyle-Width="8%" HeaderText="Sr No">
                   <ItemTemplate>
                    <asp:Label ID="lbl_srno" runat="server"  Font-Bold="true"   Text='<%#DataBinder.Eval(Container.DataItem, "Sr_No")%>'></asp:Label>
                 </ItemTemplate> 
                </asp:TemplateColumn>
                <asp:BoundColumn ItemStyle-Width="10%"  HeaderText="Date" ReadOnly="true" DataField="Cheque_Date"  ></asp:BoundColumn>
                 <asp:TemplateColumn HeaderText="Particulars">
                  <HeaderStyle Width = "30%"/>
                     <ItemTemplate>
                      <asp:Label ID ="lbl_Particulars" runat="server"> <%#DataBinder.Eval(Container.DataItem, "Particulars")%> </asp:Label>
                      <br />
                      <asp:Label ID ="lbl_Narration" runat="server" ForeColor ="Red"> <%#DataBinder.Eval(Container.DataItem, "Narration")%> </asp:Label>
                     </ItemTemplate>
                 </asp:TemplateColumn>
                <asp:BoundColumn ItemStyle-Width="15%"  HeaderText="Voucher Type" ReadOnly="true" DataField="Voucher_Type"  ></asp:BoundColumn>

                <asp:BoundColumn ItemStyle-Width="10%"  HeaderText="Cheque No" ReadOnly="true" DataField="Cheque_No" ></asp:BoundColumn>

              
       
                 
                 
                 
                  <asp:TemplateColumn HeaderText="Bank Date">
                         <ItemTemplate>
                            <asp:Label ID="lbl_srno" runat="server"  Font-Bold="true"   Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Bank_Date")).ToString("dd/MM/yyy")%>'></asp:Label>
                         </ItemTemplate> 
                 </asp:TemplateColumn>  
                
                  <asp:BoundColumn  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%"  HeaderText="Debit" ReadOnly="true" DataField="Debit"  ></asp:BoundColumn>
                
                  <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%"  HeaderText="Credit" ReadOnly="true" DataField="Credit"  ></asp:BoundColumn>                    
                 </Columns>
            </asp:DataGrid>
            
    </asp:Panel>
    </div>
    </form>
</body>
</html>
