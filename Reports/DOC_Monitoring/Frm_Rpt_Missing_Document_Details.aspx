<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_Missing_Document_Details.aspx.cs" Inherits="Reports_CL_Nandwana_DOC_Monitoring_Frm_Rpt_Missing_Document_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>List of Missing Document No</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager id="scm_Missing_Document" runat="Server"></asp:ScriptManager>
       <table class="TABLE" style="width: 100%">
        <tr>                        
                            <td class="Feild" style="width:25%;">
                                <asp:Label ID="lbl_Start_No" runat="server"></asp:Label></td>                         
                            <td class="Feild" style="width:25%; ">
                                <asp:Label ID="lbl_End_No" runat="server"></asp:Label></td> 
                                 <td class="Feild" style="width:25%;">
                                <asp:Label ID="lbl_Document" runat="server"></asp:Label></td>                         
                            <td class="Feild" style="width:25%;">
                                <asp:Label ID="lbl_Branch" runat="server"></asp:Label></td>        
                                    
                             
            </tr>
    </table>
      <table class="TABLE" style="width: 100%">   
             <tr>                        
                            <td class="Feild" style="width:10%; height: 21px;">Sr No</td>                         
                            <td class="Feild" style="width:90%; height: 21px;">Missing Document No</td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="upd_pnl_Missing_Document" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Rep_Missing" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_Missing_Document" runat="server" ScrollBars="Auto" Height="400px">
                  <asp:Repeater ID="Rep_Missing"  runat="server">
                <ItemTemplate>
                    <table width="35%">
                        <tr>                        
                            <td class="NumericValue" style="width:15%"><%#Eval("Sr No")%></td>                           
                            <td class="NumericValue" style="width:20%"><%#Eval("Missing")%></td>
                        </tr>                      
                    </table>
                </ItemTemplate>
            </asp:Repeater>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </td>
            </tr>
        </table>
    </form>
</body>
</html>
