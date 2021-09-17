<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherCostCentre.aspx.cs" Inherits="Finance_VoucherView_FrmVoucherCostCentre" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher CostCentre</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
   

<table class = "TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="6" >
                    <asp:Label ID="lbl_Heading" runat="server"  CssClass="HEADINGLABEL" Text="COST-CENTRE DETAILS"></asp:Label></td>
            </tr>
            <tr>
                <td>
                  &nbsp;
                </td>
            </tr>
            
            
            
            <tr>
               
                <td style="width:20%; text-align: right;">
                    Cost-Centre Details For : 
                </td>
              
                <td style ="width:30%" >
                    <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4">&nbsp;</td> 
            </tr>
            
             <tr>
               
                <td style="width:20%; text-align: right;">
                    Upto : 
                </td>
              
                <td style ="width:30%" >
                    <asp:Label ID="lbl_Upto" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>&nbsp;</td> 
            </tr>
            
            <tr>
                <td colspan="6">
                <fieldset id="fld_Grid">
                <legend>Cost Centre Details :</legend>
                <table cellpadding="3" cellspacing="3" border="0" width="100%">
                  <tr>
                   <td>
                    <asp:DataGrid ID = "dg_CostCentre" runat = "server" style="width:100%" CssClass = "Grid"
                        AutoGenerateColumns="False" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Cost Centre"  >
                                <HeaderStyle Width="5%" Font-Bold ="True"    />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_CostCentre" runat = "server"
                                        style ="width:100%" Text = '<%#Eval("Cost_Centre_Name")%>'>
                                    </asp:label>
                                </ItemTemplate>
                                                  
                            </asp:TemplateColumn>
                            
                         
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle Width="15%" HorizontalAlign ="Right" Font-Bold ="True"  />
                                <ItemStyle HorizontalAlign = "right" />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_Amount" runat = "server" style ="width:100%;text-align:right;" CssClass = "TEXTBOXNOS"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:label>
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                                                                                    
                        </Columns>
                    </asp:DataGrid>
                    </td>
                 </tr>
             </table>
                 </fieldset>
                </td>
            </tr>
            
            <tr>
                <td colspan="6" style="text-align: center;">
                    </td>
            </tr>
            
            <tr>
                <td colspan="6">
                    &nbsp;</td>
            </tr>
      </table>
    </div>
    </form>
</body>
</html>
